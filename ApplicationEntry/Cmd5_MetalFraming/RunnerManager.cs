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

      // 벽체 커브가 4개인 일반형태 일때 (개구부 없음)
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

          Tools.MoveToCoreLayer( wall, upInstance );
          Tools.MoveToCoreLayer( wall, downInstance );
        }

        


      }


    }





  
  }
}
