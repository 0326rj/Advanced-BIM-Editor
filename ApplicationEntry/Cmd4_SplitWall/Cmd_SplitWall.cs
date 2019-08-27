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

      Wall cutWall = null;
      var refGrid = _uidoc.Selection.PickObject( ObjectType.Element );
      var elementGrid = _doc.GetElement( refGrid );
      

      try
      {
        if ( elementGrid.Category.Id != new ElementId(BuiltInCategory.OST_Grids) )
        {
          TaskDialog.Show( _appTitle, "切断基準のグリッドを選択してください。" );
          return Result.Failed;
        }
        else
        {
          var refWall = _uidoc.Selection.PickObject( ObjectType.Element );
          var elementWall = _doc.GetElement( refWall );

          if ( elementWall.Category.Id != new ElementId( BuiltInCategory.OST_Walls ) )
          {
            TaskDialog.Show( _appTitle, "対象の壁を選択してください。" );
            return Result.Failed;
          }
          else
          {
            Grid grid = elementGrid as Grid;
            Wall wall = elementWall as Wall;
            

            using ( Transaction trans = new Transaction( _doc, "Wall Split" ) )
            {
              trans.Start();
              
              using ( SubTransaction subTrans1 = new SubTransaction( _doc ) )
              {
                subTrans1.Start();

                cutWall = Tools.Get_Split_Wall( _doc, wall, grid );

                subTrans1.Commit();
              }

              trans.Commit();
            }
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

