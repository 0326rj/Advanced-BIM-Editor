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
  // 벽체를 그리드를 기준으로 UV치 획득, Split한다.

  [Transaction( TransactionMode.Manual )] 
  class Cmd_SplitWall : IExternalCommand
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

    public Result Execute( ExternalCommandData commandData,
      ref string message, ElementSet elements )
    {
      _uiapp = commandData.Application;
      _app = _uiapp.Application;
      _uidoc = _uiapp.ActiveUIDocument;
      _doc = _uidoc.Document;
      _appTitle = "JK Wall Split Tool";

      var selectedElements = _uidoc.Selection
        .PickObjects( ObjectType.Element );

      List<Wall> walls = new List<Wall>();
      List<Grid> selectedGrids = new List<Grid>();

      List<Element> elementList = new List<Element>();


      if ( selectedElements != null )
      {
        foreach ( Reference reference in selectedElements )
        {
          Element element = _doc.GetElement( reference );
          elementList.Add( element );
        }

        foreach ( var e in elementList )
        {
          if ( e is Grid )
          {
            selectedGrids.Add( e as Grid );
          }
          else if ( e is Wall )
          {
            walls.Add( e as Wall );
          }
        }
      }

      try
      {
        if ( selectedGrids == null && walls.Count > 1 )
        {
          TaskDialog.Show( _appTitle, "切断基準のグリッドと壁をを選択してください。" );
          return Result.Failed;
        }
        else
        {
          using ( Transaction trans = new Transaction( _doc, "Wall Split" ) )
          {
            trans.Start();

            foreach ( Grid grid in selectedGrids )
            {
              Wall cutWall = Tools.Get_Split_Wall_By_Grid( _doc, walls[0], grid );
            }

            //Tools.Get_Split_Wall_By_Grids( walls[0], selectedGrids );

            trans.Commit();
          }
        }
      }
      catch ( Exception ex )
      {
        message = ex.Message;
        return Result.Failed;
      }
      return Result.Succeeded;
    }
  }
}

