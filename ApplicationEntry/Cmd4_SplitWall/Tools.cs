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


    internal static Wall Get_Split_Wall( Document doc, Wall wall, Grid grid )
    {
      // Get wall base level by level ID
      var wall_Top_LevelId = wall.get_Parameter( BuiltInParameter.WALL_HEIGHT_TYPE ).AsElementId();
      var wall_height = wall.get_Parameter( BuiltInParameter.WALL_USER_HEIGHT_PARAM ).AsDouble();
      double wall_Top_Offset = wall.get_Parameter( BuiltInParameter.WALL_TOP_OFFSET ).AsDouble();
      double wall_base_Offset = wall.get_Parameter( BuiltInParameter.WALL_BASE_OFFSET ).AsDouble();

      Curve wallCurve = ( ( LocationCurve )wall.Location ).Curve;
      Curve gridCurve = grid.Curve;
      var startParam = wallCurve.GetEndParameter( 0 );
      var endParam = wallCurve.GetEndParameter( 1 );
      var startPoint = wallCurve.Evaluate( startParam, false );
      var endPoint = wallCurve.Evaluate( endParam, false ); 

      if ( wallCurve is Line && gridCurve is Line )
      {
        Line wallLine = ( Line )wallCurve;
        Line gridLine = ( Line )gridCurve;
        XYZ wallPt = wallLine.Origin;
        XYZ gridPt = gridLine.Origin;

        Transform toWallLine = Transform.CreateTranslation( XYZ.BasisZ * (wallPt.Z - gridPt.Z) );
        Line newGridLine = gridLine.CreateTransformed( toWallLine ) as Line;

        var interPt = Get_Intersection_UV( ( Line )wallCurve, newGridLine );

        string str = String.Format( "X = {0}\n" + "Y = {1}\n" + "Z = {2}\n", interPt.X, interPt.Y, interPt.Z );
        string str2 = String.Format( "X = {0}\n" + "Y = {1}\n" + "Z = {2}\n", startPoint.X, startPoint.Y, startPoint.Z );
        TaskDialog.Show( "...", str + "\n\n" + str2 );

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

        return wall_2;
      }
      else
        return null;
    }


    private static XYZ Get_Intersection_UV( Line line1, Line line2 )
    {
      IntersectionResultArray results;
      SetComparisonResult result = line1.Intersect( line2, out results );

      if ( result != SetComparisonResult.Overlap )
        throw new InvalidOperationException( "交差していません。" );
      if ( results == null || results.Size != 1 )
        throw new InvalidOperationException( "交差していません。" );

      IntersectionResult iResult = results.get_Item( 0 );
      return iResult.XYZPoint;
    }
  }
}
