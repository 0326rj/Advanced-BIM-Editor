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

namespace NoahDesign.Cmd5_Test
{
  // Test2
  // Auto Array family Instances in Wall solid
  // 2018.8 Jaebum Kim

  [Transaction(TransactionMode.Manual)]
  public class CmdTest2 : IExternalCommand
  {
    private const string _Name_stud = "1_スタッド";
    private const string _Name_symbol = "スタッド_WS90";

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
    #endregion

    public Result Execute( ExternalCommandData commandData,
      ref string message, ElementSet elements )
    {
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
            FamilySymbol studSymbol = GetSymbol( _doc, _Name_stud, _Name_symbol );

            // Create Stud Instance
            var stud = Stud.Create_Stud_In_Wall( _doc, targetWall, studSymbol );
          }
          else
          {
            TaskDialog.Show( "...", "error" );
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

    /// <summary>
    /// 패밀리명, 타입명으로 문서상의 패밀리 심볼을 취득한다.
    /// </summary>
    /// <param name="doc"></param>
    /// <param name="familyName"></param>
    /// <param name="symbolName"></param>
    /// <returns></returns>
    private FamilySymbol GetSymbol( Document doc, string familyName, string symbolName )
    {
      using ( var collector = new FilteredElementCollector( doc ) )
      {
        using ( var families = collector.OfClass( typeof( Family ) ) )
        {
          foreach ( Family family in families )
          {
            if ( family.Name == familyName )
            {
              foreach ( var symbolId in family.GetFamilySymbolIds() )
              {
                var symbol = doc.GetElement( symbolId ) as FamilySymbol;
                if ( symbol.Name == symbolName )
                  return symbol;
              }
            }
          }
        }
      }
      return null;
    }


  }
}