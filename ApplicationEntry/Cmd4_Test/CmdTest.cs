#region 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using NoahDesign.Folder_WinForm;
using System.Diagnostics;
#endregion

namespace NoahDesign.Cmd4_Test
{
  // Note: To get the reference of a face that is part of a solid,
  // need to set Option.ComputeReferences = true.

  [Transaction(TransactionMode.Manual)]
  class CmdTest : IExternalCommand
  {
    public UIDocument _uidoc { get; private set; }
    public Document _doc { get; private set; }

    public Result Execute( ExternalCommandData commandData, ref string message, ElementSet elements )
    {
      _uidoc = commandData.Application.ActiveUIDocument;
      _doc = _uidoc.Document;

      try
      {
        var elementId = _uidoc.Selection.PickObject( ObjectType.Element );
        var element = _doc.GetElement( elementId );

        var floorElement = ( Floor )element;
        HostObject hostObj = floorElement as HostObject;
   
        Options options = new Options();
        options.ComputeReferences = true;

        var topfaces = HostObjectUtils.GetTopFaces( hostObj );
        foreach ( var refe in topfaces )
        {
          var e = _doc.GetElement( refe );
          var geoObj = e.get_Geometry( options ) as GeometryObject;

          var face = geoObj as Face;

          TaskDialog.Show( "...", face.Area.ToString() );
        }      
      }
      catch ( Exception ex )
      {
        TaskDialog.Show( "...", ex.Message );
        return Result.Cancelled;
      }

      return Result.Succeeded;
    }




  }
}
