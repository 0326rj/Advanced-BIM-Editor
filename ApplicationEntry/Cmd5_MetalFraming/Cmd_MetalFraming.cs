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
using System.IO;
#endregion

namespace NoahDesign.Cmd5_MetalFraming
{
  // Test2
  // Auto Array family Instances in Wall solid
  // 2018.8 Jaebum Kim

  [Transaction(TransactionMode.Manual)]
  public class Cmd_MetalFraming : IExternalCommand
  {
    private const string _Name_stud_fam = "1_スタッド";
    private const string _Name_stud_sym = "スタッド_WS90";

    private const string _Name_runner_up_fam = "3_ランナー_上部";
    private const string _Name_runner_up_sym = "ランナーWR90";

    private const string _Name_runner_dn_fam = "2_ランナー_下部";
    private const string _Name_runner_dn_sym = "ランナーWR90";

    #region Property
    private UIApplication _uiapp;
    private UIDocument _uidoc;
    private Document _doc;
    

    public UIApplication Uiapp
    {
      get { return _uiapp; }
      set { _uiapp = value; }
    }

    public UIDocument Uidoc
    {
      get { return _uidoc; }
      set { _uidoc = value; }
    }

    public Document Doc
    {
      get { return _doc; }
      set { _doc = value; }
    }

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
      _appTitle = "JK 壁下地ゼネレーター";
      _uiapp = commandData.Application;
      _uidoc = _uiapp.ActiveUIDocument;
      _doc = _uidoc.Document;

      var eref = _uidoc.Selection.PickObject( ObjectType.Element );
      var elementSelected = _doc.GetElement( eref );

      using ( Transaction tx = new Transaction( _doc, "tx" ) ) 
      {
        try
        {
          tx.Start();
          if ( elementSelected != null && elementSelected is Wall )
          {
            Wall targetWall = elementSelected as Wall;

            // Get Stud FamilySymbol in this Document
            FamilySymbol _studSymbol = Tools.GetSymbol( _doc, _Name_stud_fam, _Name_stud_sym );
            FamilySymbol _runnerSymbol = Tools.GetSymbol( _doc, _Name_runner_up_fam, _Name_runner_up_sym );


            if ( _studSymbol != null )
            {
              // Change Wall Color
              Tools.Change_Color_Object( _doc, elementSelected, 75 );

              // Create Stud Instance
              var stud = StudManager.Create_Stud_In_Wall( _doc, targetWall, _studSymbol );

              // Array Stud
              var arr = StudManager.Array_Stud_In_Wall( _doc, targetWall, stud, 1 );

              // Create Runner 
            }
            else
            {
              TaskDialog.Show( AppTitle, "ファミリがロードされていません。" );
            }           
          }
          else
          {
            TaskDialog.Show( AppTitle, "壁が選択されませんでした。" );
          }
          tx.Commit();
          return Result.Succeeded;
        }
        catch ( Exception ex)
        {
          message = ex.Message;
          return Result.Failed;
        }     
      }     
    }




  }
}