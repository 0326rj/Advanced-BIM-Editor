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
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.DB.IFC;
using System.Diagnostics;
using MyUtils;
#endregion

namespace NoahDesign.Cmd5_MetalFraming
{
  internal static class RunnerManager
  {
    internal static void Create_Runner_In_Wall(
      Document doc,
      Wall wall,
      FamilySymbol runnerSymbol )
    {
      
      var wallCurveLoop = wall.GetAnalyticalModel().GetCurves( AnalyticalCurveType.RawCurves );

      // 벽체 커브가 4개인 일반형태 일때
      if ( wallCurveLoop.Count >= 4 )
      {
        List<Line> topBottomLines = new List<Line>();

        foreach ( Curve curve in wallCurveLoop )
        {
          if ( ( ( Line )curve ).Direction.Z == 0 )
          {
            topBottomLines.Add( ( Line )curve );
          }
        }    

        if ( topBottomLines.Count == 2 )
        {
          Level wallLevel = doc.GetElement( wall.LevelId ) as Level;

          topBottomLines.OrderByDescending( x => x.Direction.Z );

          var upInstance = doc.Create
            .NewFamilyInstance( topBottomLines[0], runnerSymbol, null, StructuralType.Beam );
          var downInstance = doc.Create
            .NewFamilyInstance( topBottomLines[1], runnerSymbol, null, StructuralType.Beam );

          downInstance.get_Parameter( BuiltInParameter.STRUCTURAL_BEND_DIR_ANGLE ).Set( Math.PI );
        }
      }

    }

    



    /// <summary>
    /// Wall 객체와 그 AnalyticalModel Line의 완전 평면직선을 인수로 받아
    /// Wall 상부 직선만을 ModelCurve화 시킨다.
    /// (Wall 인수는 insert 조작을 위해 입력)
    /// </summary>
    /// <param name="doc"></param>
    /// <param name="wall"></param>
    internal static void Get_Wall_Top_ModelLines( Document doc, Wall wall )
    {
      List<Line> horizontalLines = New_Horizontal_Lines_By_Wall( doc, wall );
      if ( horizontalLines.Count == 2 )
      {
        if ( horizontalLines[0].Origin.Z > horizontalLines[1].Origin.Z )
        {
          SketchPlane sketchPlane = Tools.NewSkethPlanePassLine( horizontalLines[0], doc );
          ModelCurve horizontalCurve = doc.Create.NewModelCurve( horizontalLines[0], sketchPlane );
        }
        else
        {
          SketchPlane sketchPlane = Tools.NewSkethPlanePassLine( horizontalLines[1], doc );
          ModelCurve horizontalCurve = doc.Create.NewModelCurve( horizontalLines[1], sketchPlane );
        }
      }
    }

    /// <summary>
    /// Wall 객체를 인수로 받아 AnalyticalModel Line의 완전 평면직선을 반환한다.
    /// </summary>
    /// <param name="doc"></param>
    /// <param name="wall"></param>
    /// <returns></returns>
    public static List<Line> New_Horizontal_Lines_By_Wall( Document doc, Wall wall )
    {
      List<Line> horizontalLine = new List<Line>();
      var analyticalModel = wall.GetAnalyticalModel();
      var baseCurves = analyticalModel.GetCurves( AnalyticalCurveType.RawCurves );

      if ( baseCurves != null )
      {
        foreach ( var curve in baseCurves )
        {
          Line axisLine = curve as Line;
          
          // 완전 평면방향 벡터인 직선만 필터시킨다.
          if ( !( axisLine.Direction.Z == 1.0 || axisLine.Direction.Z == -1.0 ) )
          {
            horizontalLine.Add( axisLine );             
          }
        }
        return horizontalLine;
      }
      else
        return null;
    }
  }
}
