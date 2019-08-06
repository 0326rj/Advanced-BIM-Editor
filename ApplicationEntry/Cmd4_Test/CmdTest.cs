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

      try
      {
        if ( ele is Floor )
        {
          Floor floor = _doc.GetElement( id ) as Floor;
          var topFace = HostObjectUtils.GetTopFaces( floor );
           
          face = _doc
            .GetElement( topFace[0] )
            .GetGeometryObjectFromReference( topFace[0] ) as Face;

          FaceAnalyzer( _doc, floor );

        }
        TaskDialog.Show( "...", face.Area.ToString() );
      }
      catch ( Exception ex )
      {
        TaskDialog.Show( "...", ex.Message );
        return Result.Failed;
      }

      return Result.Succeeded;
    }


    private void FaceAnalyzer( Document doc, Floor floor )
    {
      var refe = HostObjectUtils.GetTopFaces( floor );

      var singleFace = doc
        .GetElement( refe[0] )
        .GetGeometryObjectFromReference( refe[0] ) as Face;

      var lines = new List<Line>();

      if ( singleFace.Area > 0 )
      {
        var crvs = singleFace.GetEdgesAsCurveLoops() as IList<Curve>;

        foreach ( var crv in crvs )
        {
          if ( crv is Line )
            lines.Add( crv as Line );
        }
      }

      if ( lines != null )
      {
        var ptList = new List<XYZ>();

        foreach ( var line in lines )
        {
          XYZ p1 = line.GetEndPoint( 0 );
          XYZ p2 = line.GetEndPoint( 1 );
          var midpt = Util.Midpoint( p1, p2 );
          ptList.Add( midpt );
        }

        TaskDialog.Show( "...", ptList.Count.ToString() );
      }
    }
  }
}

