#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.DB.Structure;
using System.Diagnostics;
using MyUtils;
using NoahDesign.Cmd5_MetalFraming;
#endregion

namespace NoahDesign.Cmd4_SplitWall
{
  internal static class Tools
  {
    /// <summary>
    /// 선택된 모든 그리드와 벽을 다중처리하여 그리드선을 기준으로 벽체를 Split한다.
    /// </summary>
    /// <param name="doc"></param>
    /// <param name="walls"></param>
    /// <param name="grids"></param>
    /// <returns></returns>
    internal static List<Wall> Get_Split_Wall_By_Grids( Document doc, Wall wall, List<Grid> grids )
    {
      bool flag = false;
      List<Grid> trueGrids = new List<Grid>();

      if ( grids.Count >= 1 && grids.Count <= 100 )
      {
        List<Wall> addWalls = new List<Wall>();
        addWalls.Add( wall );
       
        foreach ( Grid grid in grids )
        {
          flag = IsGridInterstionWall( wall, grid );

          if ( flag == true )
          {
            trueGrids.Add( grid );
          }
        }

        var wallCurve = ( LocationCurve )wall.Location;
        var wallLine = wallCurve.Curve as Line;
        var d = wallLine.Direction;



        if ( d.X > 0 && d.Y > 0 )
        {
          for ( int i = 0; i < trueGrids.Count; i++ )
          {
            addWalls.Add( Get_Split_Wall_By_Grid( doc, addWalls[i], trueGrids[i] ) );
          }
        }
        else if ( d.X > 0 && d.Y < 0 )
        {
          for ( int i = 0; i < trueGrids.Count; i++ )
          {
            addWalls.Add( Get_Split_Wall_By_Grid( doc, addWalls[i], trueGrids[i] ) );
          }
        }
        else
        {
          trueGrids.Reverse();
          for ( int i = 0; i < trueGrids.Count; i++ )
          {
            addWalls.Add( Get_Split_Wall_By_Grid( doc, addWalls[i], trueGrids[i] ) );
          }
        }

        return addWalls;
      }
      else
        return null;
    }
  


    internal static Wall Get_Split_Wall_By_Grid( Document doc, Wall wall, Grid grid )
    {
      // Get wall base level by level ID
      var wall_Top_LevelId = wall.get_Parameter( BuiltInParameter.WALL_HEIGHT_TYPE ).AsElementId();
      var wall_height = wall.get_Parameter( BuiltInParameter.WALL_USER_HEIGHT_PARAM ).AsDouble();
      double wall_base_Offset = wall.get_Parameter( BuiltInParameter.WALL_BASE_OFFSET ).AsDouble();
      double wall_top_Offset = wall.get_Parameter( BuiltInParameter.WALL_TOP_OFFSET ).AsDouble();

      var wallCurve = ( ( LocationCurve )wall.Location ).Curve;
      var gridCurve = grid.Curve;

      var startParam = wallCurve.GetEndParameter( 0 );
      var endParam = wallCurve.GetEndParameter( 1 );
      var startPoint = wallCurve.Evaluate( startParam, false );
      var endPoint = wallCurve.Evaluate( endParam, false ); 

      if ( wallCurve is Line && gridCurve is Line )
      {
        var wallLine = ( Line )wallCurve;
        var gridLine = ( Line )gridCurve;       
        var wallPt = wallLine.Origin;
        var gridPt = gridLine.Origin;

        var toWallLine = Transform.CreateTranslation( XYZ.BasisZ * (wallPt.Z - gridPt.Z) );
        var newGridLine = gridLine.CreateTransformed( toWallLine ) as Line;

        var interPt = Get_Intersection_Point( wallCurve, newGridLine );

        if ( interPt != null )
        {
          var midPoint = new XYZ( interPt.X, interPt.Y, startPoint.Z );

          Line newLine1 = Line.CreateBound( startPoint, midPoint );

          if ( WallUtils.IsWallJoinAllowedAtEnd( wall, 1 ) )
          {
            WallUtils.DisallowWallJoinAtEnd( wall, 1 );
          }

        //align original wall with the new curve
          ( ( LocationCurve )wall.Location ).Curve = newLine1;

          Line newLine2 = Line.CreateBound( midPoint, endPoint );

          Wall wall_2 = Wall.Create(
            doc,
            newLine2,
            wall.GetTypeId(),
            wall.LevelId,
            wall_height,
            wall_base_Offset,
            wall.Flipped,
            true );

          wall_2.get_Parameter( BuiltInParameter.WALL_HEIGHT_TYPE ).Set( wall_Top_LevelId );
          wall_2.get_Parameter( BuiltInParameter.WALL_TOP_OFFSET ).Set( wall_top_Offset );
          return wall_2;
        }
        else
        {
          return null;
        }  
      }
      else
      {
        return null;
      }
        
    }


    private static XYZ Get_Intersection_Point( Curve line1, Curve line2 )
    {
      IntersectionResultArray results;
      SetComparisonResult result = line1.Intersect( line2, out results );

      if ( result != SetComparisonResult.Overlap )
        return null;
      //throw new InvalidOperationException( "交差していない通り心が含まれています。" );
      if ( results == null || results.Size != 1 )
        return null;
      //throw new InvalidOperationException( "交差していない通り心が含まれています。" );
      if ( result == SetComparisonResult.BothEmpty)
        return null;

      IntersectionResult iResult = results.get_Item( 0 );
      return iResult.XYZPoint;
    }


    private static bool IsGridInterstionWall( Wall wall, Grid grid )
    {
      XYZ interPt = null;
      var wallCurve = ( ( LocationCurve )wall.Location ).Curve;
      var gridCurve = grid.Curve;
      var wallLine = ( Line )wallCurve;
      var gridLine = ( Line )gridCurve;
      var wallPt = wallLine.Origin;
      var gridPt = gridLine.Origin;
      var toWallLine = Transform.CreateTranslation( XYZ.BasisZ * ( wallPt.Z - gridPt.Z ) );
      var newGridLine = gridLine.CreateTransformed( toWallLine ) as Line;
   
      interPt = Get_Intersection_Point( wallCurve, newGridLine );

      if ( !( interPt is null ) )
        return true;
      else
        return false;
    }
  }
}
