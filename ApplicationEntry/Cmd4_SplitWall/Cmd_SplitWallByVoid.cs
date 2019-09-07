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
using MyUtils;
#endregion

namespace NoahDesign.Cmd4_SplitWall
{
  [Transaction(TransactionMode.Manual)]
  class Cmd_SplitWallByVoid : IExternalCommand
  {
    #region Property
    public UIApplication _uiapp { get; private set; }
    public Application _app { get; private set; }
    public UIDocument _uidoc { get; private set; }
    public Document _doc { get; private set; }

    private string _appTitle;
    public string AppTitle
    {
      get { return _appTitle; }
      set { _appTitle = value; }
    }
    #endregion

    public Result Execute( ExternalCommandData commandData, ref string message, ElementSet elements )
    {
      _uiapp = commandData.Application;
      _app = _uiapp.Application;
      _uidoc = _uiapp.ActiveUIDocument;
      _doc = _uidoc.Document;
      _appTitle = "JK Wall Split Tool";

      IList<Reference> selectedElements;
      List<Element> elementList = new List<Element>();
      List<Wall> walls = new List<Wall>();

      try
      {
        selectedElements = _uidoc.Selection.PickObjects( ObjectType.Element );
      }
      catch
      {
        return Result.Cancelled;
      }

      if ( selectedElements != null )
      {
        foreach ( Reference reference in selectedElements )
        {
          Element element = _doc.GetElement( reference );
          elementList.Add( element );
        }

        foreach ( var e in elementList )
        {
          if ( e is Wall )
          {
            walls.Add( e as Wall );
          }
        }
      }

      try
      {
        using ( Transaction trans = new Transaction( _doc, "split wall by void" ) )
        {
          trans.Start();

          foreach ( Wall wall in walls )
          {
            Tool_SplitWall.Get_Split_Wall_By_Voids( _doc, wall );
          }

          trans.Commit();
        }
      }
      catch ( Exception ex )
      {
        message = ex.Message;
        return Result.Cancelled;
      }

      return Result.Succeeded;
    }
  }
}
