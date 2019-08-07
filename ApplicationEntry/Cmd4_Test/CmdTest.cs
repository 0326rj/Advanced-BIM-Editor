#region 
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
using NoahDesign.Folder_WinForm;
using System.Diagnostics;
using BuildingCoder;
#endregion

namespace NoahDesign.Cmd4_Test
{
  // Note: To get the reference of a face that is part of a solid,
  // need to set Option.ComputeReferences = true.

  [Transaction(TransactionMode.Manual)]
  class CmdTest : IExternalCommand
  {
    #region Property
    public UIApplication _uiapp { get; set; }
    public Application _app { get; set; }
    public UIDocument _uidoc { get; set; }
    public Document _doc { get; set; }

    public List<ElementId> _elementId { get; private set; }
    public List<Element> _elements { get; private set; }
    public List<Face> _faces { get; private set; } 
    #endregion

    public Result Execute( ExternalCommandData commandData,
      ref string message, ElementSet elements )
    {
      _uiapp = commandData.Application;
      _app = _uiapp.Application;
      _uidoc = _uiapp.ActiveUIDocument;
      _doc = _uidoc.Document;

      var id = _uidoc.Selection.PickObject( ObjectType.Element ).ElementId;
      var ele = _doc.GetElement( id );

      Face face = null;

      try
      {
        if ( ele is Floor )
        {
          Floor floor = _doc.GetElement( id ) as Floor;
          var topFace = HostObjectUtils.GetTopFaces( floor );
           
          face = _doc
            .GetElement( topFace[0] )
            .GetGeometryObjectFromReference( topFace[0] ) as Face;
        }

        using ( Transaction tx = new Transaction( _doc, "trans" ) )
        {
          tx.Start();

          UV uV = new UV();
          var normal = face.ComputeNormal( uV );
          XYZ pt = new XYZ( normal.X, normal.Y, normal.Z );

          Plane plane = Plane.CreateByNormalAndOrigin( normal, pt );
          SketchPlane sketchPlane = SketchPlane.Create( _doc, plane );


          var loop = face.GetEdgesAsCurveLoops();
          foreach ( var crv in loop[0] )
          {

            _doc.Create.NewModelCurve( crv, sketchPlane );

          }

          tx.Commit();
        }  
      }
      catch ( Exception ex )
      {
        TaskDialog.Show( "...", ex.Message );
        return Result.Failed;
      }

      return Result.Succeeded;
    }
  }
}

