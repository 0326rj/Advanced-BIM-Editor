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

namespace NoahDesign.Cmd5_MetalFraming
{
  internal static class Tools
  {


    /// <summary>
    /// 패밀리명, 타입명으로 문서상의 패밀리 심볼을 취득한다.
    /// </summary>
    /// <param name="doc"></param>
    /// <param name="familyName"></param>
    /// <param name="symbolName"></param>
    /// <returns></returns>
    internal static FamilySymbol GetSymbol( Document doc, string familyName, string symbolName )
    {
      using ( var collector = new FilteredElementCollector( doc ) )
      {
        using ( var families = collector.OfClass( typeof( Family ) ) )
        {
          foreach ( Family family in families )
          {
            if ( family.Name == familyName )
            {
              foreach ( var symbolId in family.GetFamilySymbolIds() )
              {
                var symbol = doc.GetElement( symbolId ) as FamilySymbol;
                if ( symbol.Name == symbolName )
                  return symbol;
              }
            }
          }
        }
      }
      return null;
    }

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

    /// <summary>
    /// 레빗 엘레멘트의 색, 투명도를 변경한다.
    /// </summary>
    /// <param name="doc"></param>
    /// <param name="targetElement"></param>
    /// <param name="transparency"></param>
    public static void Change_Color_Object(
      Document doc,
      Element targetElement,
      int transparency
      )
    {
      var color_Surface = new Color( 026, 055, 085 );
      var color_Edges = new Color( 136, 136, 136 );

      FillPatternElement patternElement;
      patternElement = FillPatternElement
        .GetFillPatternElementByName( doc, FillPatternTarget.Drafting, "Solid fill" );

      OverrideGraphicSettings ogs = new OverrideGraphicSettings();
      ogs.SetProjectionFillColor( color_Surface );
      ogs.SetProjectionFillPatternId( patternElement.Id );
      ogs.SetProjectionLineColor( color_Edges );
      ogs.SetSurfaceTransparency( transparency );
      doc.ActiveView.SetElementOverrides( targetElement.Id, ogs );
    }

    //internal static Level GetWallBaseLevel( Document doc, Wall wall )
    //{
    //  ElementId baseLevelId = wall.get_Parameter( BuiltInParameter.WALL_BASE_CONSTRAINT ).AsElementId();
    //  Level baseLevel = doc.GetElement( baseLevelId ) as Level;

    //  return baseLevel;
    //}


    //internal static Level GetWallTopLevel( Document doc, Wall wall )
    //{
    //  ElementId topLevelId = wall.get_Parameter( BuiltInParameter.WALL_HEIGHT_TYPE ).AsElementId();
    //  Level topLevel = doc.GetElement( topLevelId ) as Level;

    //  return topLevel;
    //}


  }
}
