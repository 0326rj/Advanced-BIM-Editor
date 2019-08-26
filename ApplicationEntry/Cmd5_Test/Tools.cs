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
#endregion

namespace NoahDesign.Cmd5_Test
{
  internal static class Tools
  {


    /// <summary>
    /// Curve를 Line으로 변경할수 있으면 지정한 각도로 Normal Vector를 반환한다.
    /// </summary>
    /// <param name="curve"></param>
    /// <param name="radian"></param>
    /// <returns></returns>
    internal static XYZ Rotate_Vector_From_Line( Curve curve, double radian )
    {
      if ( curve is Line && curve != null )
      {
        Transform rotate = Transform.CreateRotation( XYZ.BasisZ, radian );
        Line line = curve as Line;
        Line rotatedLine = line.CreateTransformed( rotate ) as Line;
        var p1 = rotatedLine.GetEndPoint( 0 );
        var p2 = rotatedLine.GetEndPoint( 1 );
        var rotatedVector = ( p2 - p1 ).Normalize();
        return rotatedVector;
      }
      else
        return null;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="doc"></param>
    /// <param name="familyInstance"></param>
    /// <returns></returns>
    internal static ModelCurve New_ModelLine_By_AnalyticalModel( Document doc, FamilyInstance familyInstance )
    {
      var analyticalModel = familyInstance.GetAnalyticalModel();
      var baseCurves = analyticalModel.GetCurves( AnalyticalCurveType.BaseCurve );

      if ( baseCurves != null )
      {
        Line axisLine = baseCurves[0] as Line;
        SketchPlane sketchPlane = NewSkethPlanePassLine( axisLine, doc );
        ModelCurve modelCurve = doc.Create.NewModelCurve( axisLine, sketchPlane );
        return modelCurve;
      }
      else
        return null;
    }

    internal static SketchPlane NewSkethPlanePassLine( Line aline, Document doc )
    {
      XYZ norm;
      if ( aline.GetEndPoint( 0 ).X == aline.GetEndPoint( 1 ).X )
        norm = new XYZ( 1, 0, 0 );
      else if ( aline.GetEndPoint( 0 ).Y == aline.GetEndPoint( 1 ).Y )
        norm = new XYZ( 0, 1, 0 );
      else
        norm = new XYZ( 0, 0, 1 );
      XYZ point = aline.GetEndPoint( 0 );
      Plane plane = Plane.CreateByNormalAndOrigin( norm, point );
      SketchPlane sketchPlane = SketchPlane.Create( doc, plane );
      return sketchPlane;
    }

    internal static Level GetWallBaseLevel( Document doc, Wall wall )
    {
      ElementId baseLevelId = wall.get_Parameter( BuiltInParameter.WALL_BASE_CONSTRAINT ).AsElementId();
      Level baseLevel = doc.GetElement( baseLevelId ) as Level;

      return baseLevel;
    }


    internal static Level GetWallTopLevel( Document doc, Wall wall )
    {
      ElementId topLevelId = wall.get_Parameter( BuiltInParameter.WALL_HEIGHT_TYPE ).AsElementId();
      Level topLevel = doc.GetElement( topLevelId ) as Level;

      return topLevel;
    }
  }
}
