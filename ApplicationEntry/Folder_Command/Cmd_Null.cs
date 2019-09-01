using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.Attributes;
using MyUtils;

namespace NoahDesign.Folder_Command
{
  [Transaction(TransactionMode.Manual)]
  class Cmd_Null : IExternalCommand
  {
    public Result Execute( ExternalCommandData commandData, ref string message, ElementSet elements )
    {
      UIDocument uidoc = commandData.Application.ActiveUIDocument;
      Document doc = uidoc.Document;

      var r = uidoc.Selection.PickObject( ObjectType.Element );
      var e = doc.GetElement( r );

      try
      {
        if ( e is Grid )
        {
          using ( Transaction trans = new Transaction( doc, "trans" ) )
          {
            trans.Start();
            Grid_Rotator( doc, ( Grid )e, Math.PI );
            trans.Commit();
          }
        }
        else
        {
          return Result.Failed;
        }

        return Result.Succeeded;
      }
      catch ( Exception ex )
      {
        message = ex.Message;
        return Result.Failed;
      }
    }


    private static Grid Grid_Rotator( Document doc, Grid grid, double angle )
    {
      Grid result = grid;
      var mid1 = Util.Midpoint( ( Line )result.Curve );
      var mid2 = new XYZ( mid1.X, mid1.Y, mid1.Z + 1 );
      Line axis = Line.CreateBound( mid1, mid2 );
      ElementTransformUtils.RotateElement( doc, result.Id, axis, Math.PI );
      return result;
    }
  }
}
