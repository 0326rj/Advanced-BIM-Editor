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

namespace NoahDesign.Cmd5_Test
{
  // Test2
  // Auto Array family Instances in Wall solid
  // 2018.8 Jaebum Kim

  [Transaction(TransactionMode.Manual)]
  public class CmdTest2 : IExternalCommand
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


      return Result.Succeeded;
    }

    private List<Face> GetWallTopFace( Wall wall )
    {
      List<Face> topfaces = new List<Face>();
      var topFace = HostObjectUtils.GetTopFaces( wall );

      foreach ( var tf in topFace )
      {
        Face face = _doc
         .GetElement( tf )
         .GetGeometryObjectFromReference( tf ) as Face;
        topfaces.Add( face );
      }
      return topfaces;
    }

    private List<Face> GetWallBottomFace( Wall wall )
    {
      List<Face> bottomfaces = new List<Face>();
      var bttFace = HostObjectUtils.GetTopFaces( wall );

      foreach ( var bf in bttFace )
      {
        Face face = _doc
         .GetElement( bf )
         .GetGeometryObjectFromReference( bf ) as Face;
        bottomfaces.Add( face );
      }
      return bottomfaces;
    }

    private void CreateColumnArray(Document doc, Face topFace, Face bottomFace )
    {

    }


  }
}

// https://forums.autodesk.com/t5/revit-api-forum/column-creation-questoin/td-p/2097223