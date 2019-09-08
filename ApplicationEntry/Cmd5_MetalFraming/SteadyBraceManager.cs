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
using System.Diagnostics;
using MyUtils;
#endregion

namespace NoahDesign.Cmd5_MetalFraming
{
  static class SteadyBraceManager
  {

    internal static void Create_SteadyBrace_In_Wall(
      Document doc,
      Wall wall,
      FamilySymbol steadyBraceSymbol,
      double pitch = 1200.0 )
    {
      var wallHeightFeet = wall.get_Parameter( BuiltInParameter.WALL_USER_HEIGHT_PARAM ).AsDouble();
      var wallHeightMeter = UnitConvert.FeetToMillimeters( wallHeightFeet );
      var wallCurveLoop = wall.GetAnalyticalModel().GetCurves( AnalyticalCurveType.RawCurves );
      double pitchFeet = UnitConvert.MillimetersToFeet( pitch );


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


          XYZ pt = new XYZ( 0, 0, 1 );
          XYZ zNormal = pt.Normalize();
          double copyPitch = UnitConvert.MillimetersToFeet( 1200 );
          var temp = wallHeightMeter / 1200;
          var copyCount = ( int )temp;

          if ( wallHeightMeter > 1200 && wallHeightMeter < 2400 )
          {


            var bottomInstance = doc.Create
              .NewFamilyInstance( topBottomLines[1], steadyBraceSymbol, null, StructuralType.Brace );

            bottomInstance.Location.Move( zNormal * pitchFeet );
          }
          else if ( wallHeightMeter >= 2400 )
          {
            var bottomInstance = doc.Create
              .NewFamilyInstance( topBottomLines[1], steadyBraceSymbol, null, StructuralType.Brace );

            LinearArray.ArrayElementWithoutAssociation(
              doc,
              doc.ActiveView,
              bottomInstance.Id,
              copyCount + 1,
              zNormal * copyPitch,
              ArrayAnchorMember.Second );

            doc.Delete( bottomInstance.Id );
          }


        }

      }

    }



  }
}
