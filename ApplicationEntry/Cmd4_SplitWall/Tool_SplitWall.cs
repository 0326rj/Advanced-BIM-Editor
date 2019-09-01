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
  internal static class Tool_SplitWall
  {
    internal static List<Wall> Get_Split_Wall_By_Grids( Document doc, Wall wall, List<Grid> grids )
    {
      List<Wall> resultWalls = new List<Wall>(); 

      // Get wall base level by level ID
      var wall_Top_LevelId = wall.get_Parameter( BuiltInParameter.WALL_HEIGHT_TYPE ).AsElementId();
      var wall_height = wall.get_Parameter( BuiltInParameter.WALL_USER_HEIGHT_PARAM ).AsDouble();
      double wall_base_Offset = wall.get_Parameter( BuiltInParameter.WALL_BASE_OFFSET ).AsDouble();
      double wall_top_Offset = wall.get_Parameter( BuiltInParameter.WALL_TOP_OFFSET ).AsDouble();

      bool flag = false;

      List<XYZ> truePoints = new List<XYZ>();
      List<Grid> trueGrids = new List<Grid>();
      List<Wall> addWalls = new List<Wall>();
      addWalls.Add( wall );

      foreach ( Grid grid in grids )
      {
        flag = IsGridInterstionWall( wall, grid );

        if ( flag == true )
        {
          truePoints.Add( Get_Interstion_true_point( wall, grid ) );  
        }
      }

      var wallCurve = ( ( LocationCurve )wall.Location ).Curve;
      var wallStartPoint = wallCurve.GetEndPoint( 0 );
      var wallEndPoint = wallCurve.GetEndPoint( 1 );

      if ( ( wallStartPoint.X < wallEndPoint.X ) && ( wallStartPoint.Y < wallEndPoint.Y ) )
      {
        truePoints.Sort( ( a, b ) =>
        {
          int result = a.X.CompareTo( b.X );
          if ( result == 0 ) result = a.Y.CompareTo( b.Y );
          return result;
        } );
      }
      else if ( ( wallStartPoint.X < wallEndPoint.X ) && ( wallStartPoint.Y > wallEndPoint.Y ) )
      {
        truePoints.Sort( ( a, b ) =>
        {
          int result = a.X.CompareTo( b.X );
          if ( result == 0 ) result = a.Y.CompareTo( b.Y );
          return result;
        } );
      }
      else
      {
        truePoints.Sort( ( a, b ) =>
        {
          int result = a.X.CompareTo( b.X );
          if ( result == 0 ) result = a.Y.CompareTo( b.Y );
          return result;
        } );
        truePoints.Reverse();
      }

      Line firstLine = Line.CreateBound( wallStartPoint, truePoints.FirstOrDefault() );
      ( ( LocationCurve )wall.Location ).Curve = firstLine;

      if ( WallUtils.IsWallJoinAllowedAtEnd( wall, 1 ) )
      {
        WallUtils.DisallowWallJoinAtEnd( wall, 1 );
      }

      Line lastLine = Line.CreateBound( truePoints.LastOrDefault(), wallEndPoint );

      Wall lastWall = Wall.Create(
        doc,
        lastLine,
        wall.GetTypeId(),
        wall.LevelId,
        wall_height,
        wall_base_Offset,
        wall.Flipped,
        true );

      lastWall.get_Parameter( BuiltInParameter.WALL_HEIGHT_TYPE ).Set( wall_Top_LevelId );
      lastWall.get_Parameter( BuiltInParameter.WALL_TOP_OFFSET ).Set( wall_top_Offset );

      if ( WallUtils.IsWallJoinAllowedAtEnd( lastWall, 0 ) )
      {
        WallUtils.DisallowWallJoinAtEnd( lastWall, 0 );
      }

      for ( int i = 0; i < ( truePoints.Count - 1 ); i++ )
      {

        Wall newWalls = Wall.Create(
          doc,
          Line.CreateBound( truePoints[i], truePoints[i + 1] ),
          wall.GetTypeId(),
          wall.LevelId,
          wall_height,
          wall_base_Offset,
          wall.Flipped,
          true );

        newWalls.get_Parameter( BuiltInParameter.WALL_HEIGHT_TYPE ).Set( wall_Top_LevelId );
        newWalls.get_Parameter( BuiltInParameter.WALL_TOP_OFFSET ).Set( wall_top_Offset );

        if ( WallUtils.IsWallJoinAllowedAtEnd( newWalls, 1 ) )
        {
          WallUtils.DisallowWallJoinAtEnd( newWalls, 1 );
        }

        resultWalls.Add( newWalls );
      }
      return resultWalls;
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
      if ( result == SetComparisonResult.BothEmpty )
        return null;

      IntersectionResult iResult = results.get_Item( 0 );
      return iResult.XYZPoint;
    }


    internal static bool IsGridInterstionWall( Wall wall, Grid grid )
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


    private static XYZ Get_Interstion_true_point( Wall wall, Grid grid )
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
      return interPt;
    }


    private static List<XYZ> Delete_Wall_Edge_Points(Wall wall, List<XYZ> pointsInWallLine )
    {
      var targetPts = pointsInWallLine;
      var wallCurve = ( ( LocationCurve )wall.Location ).Curve;
      var startPt = wallCurve.GetEndPoint( 0 );
      var endPt = wallCurve.GetEndPoint( 1 );

      foreach ( XYZ wallPoint in targetPts )
      {
        if ( (wallPoint.X == startPt.X) && (wallPoint.Y == startPt.Y) )
        {
          targetPts.Remove( wallPoint );
        }
        if ( ( wallPoint.X == endPt.X ) && ( wallPoint.Y == endPt.Y ) )
        {
          targetPts.Remove( wallPoint );
        }
      }
      return targetPts;
    }
  }
}