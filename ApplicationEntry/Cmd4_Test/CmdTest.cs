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
  // Test1
  // Z coordが異なるポイント同士、ModelCurveを作成

  [Transaction( TransactionMode.Manual )] 
  class CmdTest : IExternalCommand
  {
    #region Property
    public UIApplication _uiapp { get; private set; }
    public Application _app { get; private set; }
    public UIDocument _uidoc { get; private set; }
    public Document _doc { get; private set; }

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

      List<Curve> crvlist = new List<Curve>();
      List<XYZ> pts = new List<XYZ>();

      try
      {
        if ( ele is Floor )
        {
          Floor floor = _doc.GetElement( id ) as Floor;
          var topFace = HostObjectUtils.GetTopFaces( floor );

          face = _doc
            .GetElement( topFace[0] )
            .GetGeometryObjectFromReference( topFace[0] ) as Face;

          using ( Transaction tx = new Transaction( _doc, "trans" ) )
          {
            tx.Start();

            PlanarFace pf = face as PlanarFace;
            Plane plane = Plane.CreateByNormalAndOrigin( pf.FaceNormal, pf.Origin );
            
            SketchPlane sp = SketchPlane.Create( _doc, plane );

            var crvloop = face.GetEdgesAsCurveLoops();

            foreach ( var crv in crvloop[0] )
            {
              var pt1 = crv.GetEndPoint( 0 );
              var pt2 = crv.GetEndPoint( 1 );
              crvlist.Add( crv );
            }

            foreach ( var c in crvlist )
            {
              var pt = c.GetEndPoint( 0 );
              pts.Add( pt );
            }

            var p1 = pts[0];
            var p2 = pts[1];
            var p3 = pts[2];
            var p4 = pts[3];

            var mid1 = Util.Midpoint( p1, p2 );
            var mid2 = Util.Midpoint( p3, p4 );
            var mid3 = Util.Midpoint( p2, p3 );
            var mid4 = Util.Midpoint( p4, p1 );

            Line line1 = Line.CreateBound( mid3, mid4 );
            Line line2 = Line.CreateBound( mid1, mid2 );

            _doc.Create.NewModelCurve( line1, sp );
            _doc.Create.NewModelCurve( line2, sp );
            

            tx.Commit();
          }
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

