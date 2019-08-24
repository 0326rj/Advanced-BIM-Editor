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

namespace NoahDesign.Cmd5_Test
{
  static class RunnerManager
  {

    internal static List<ModelCurve> CreateWallTopModelLines( Document doc, Wall wall )
    {

      var am = wall.GetAnalyticalModel();
      var wallCurves = am.GetCurves( AnalyticalCurveType.RawCurves );

      List<Line> wallLines = new List<Line>();
      foreach ( Line line in wallCurves )
      {
        wallLines.Add( line );
      }

      List<Line> horizontalLines = new List<Line>();
      List<Line> verticalLines = new List<Line>();

      if ( wallLines.Count > 3 )
      {
        
        foreach ( Line line in wallLines )
        {
          if ( !( line.Direction.Z == 1.0 || line.Direction.Z == -1.0 ) )
          {
            if ( !(line.Direction.X == 0.0 && line.Direction.Y == 0.0) )
            {
              horizontalLines.Add( line );
            }
          }
        }
      }

      List<SketchPlane> sketchPlanes = new List<SketchPlane>();
      List<ModelCurve> modelCurves = new List<ModelCurve>();
      foreach ( var line in horizontalLines )
      {
        sketchPlanes.Add( StudManager.NewSkethPlanePassLine( line, doc ) );
        foreach ( var sketchPlane in sketchPlanes )
        {
          ModelCurve mc = doc.Create.NewModelCurve( line, sketchPlane );
          modelCurves.Add( mc );
        }
      }

      return modelCurves;
    }

  }
}
