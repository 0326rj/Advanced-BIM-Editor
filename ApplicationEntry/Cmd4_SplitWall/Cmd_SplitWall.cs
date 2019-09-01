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

      List<Wall> walls = new List<Wall>();
      List<Grid> selectedGrids = new List<Grid>();
      List<Element> elementList = new List<Element>();

      IList<Reference> selectedElements;
      try
      {
        selectedElements = _uidoc.Selection
         .PickObjects( ObjectType.Element );
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
      else
      {
        return Result.Cancelled;
      }

      if ( ( selectedGrids.Count > 0 && selectedGrids.Count <= 100 )
        && ( walls.Count > 0 && walls.Count <= 100 ) )
      {
        using ( Transaction trans = new Transaction( _doc, "Wall Split" ) )
        {      
          try
          {
            trans.Start();
            IEnumerator<Wall> enumerator = walls.GetEnumerator();
            while ( enumerator.MoveNext() )
            {
              Tool_SplitWall.Get_Split_Wall_By_Grids( _doc, enumerator.Current, selectedGrids );
            }
            trans.Commit();
          }
          catch
          {
            TaskDialog.Show( _appTitle,
              "通り芯交差する壁を選択してください。",
              TaskDialogCommonButtons.Ok );
            trans.RollBack();
          }        
        }
      }
      else
      {
        TaskDialog.Show( _appTitle,
          "選択した通り心、壁の数が多すぎます。\n"
          + "数を調整してください。",
          TaskDialogCommonButtons.Ok );
        
        return Result.Cancelled;
      }
      return Result.Succeeded;
    }
  }
}

