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
    internal static void Split_Wall_By_Grid( Document doc, Wall wall )
    {
      LocationCurve wallCurve = wall.Location as LocationCurve;
      Line wallLine;

      if ( !( wallCurve.Curve is Line ) )
      {
        TaskDialog.Show( " ", "曲面壁は切断できません。" );
      }
      else
      {
        wallLine = wallCurve.Curve as Line;

        var copied = ElementTransformUtils.CopyElement( doc, wall.Id, new XYZ() );
      }
      
    }

    private static UV Get_Intersection_UV( Line line1, Line line2 )
    {
      IntersectionResultArray results;
      SetComparisonResult result = line1.Intersect( line2, out results );

      if ( result != SetComparisonResult.Overlap )
        throw new InvalidOperationException( "交差していません。" );
      if ( results == null || results.Size != 1 )
        throw new InvalidOperationException( "交差していません。" );

      IntersectionResult iResult = results.get_Item( 0 );
      return iResult.UVPoint;
    }
  }
}
