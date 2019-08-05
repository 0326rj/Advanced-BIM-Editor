using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;

namespace NoahDesign.Cmd2_ConcreteFormwork
{
  [Transaction( TransactionMode.Manual )]
  public class CmdConcreteFormwork : IExternalCommand
  {

    #region Property
    private string _taskDialogTitle;
    private Document _doc;
    private UIDocument _uidoc;
    private Element _element;
    private ElementId _elementId;

    public String TaskDialogTitle
    {
      get { return _taskDialogTitle; }
      private set { _taskDialogTitle = value; }
    }

    public Document Document
    {
      get { return _doc; }
    }

    public UIDocument UIDocument
    {
      get { return _uidoc; }
    }

    public Element Element
    {
      get { return _element; }
    }

    public ElementId ElementId
    {
      get { return _elementId; }
    }
    #endregion

    public Result Execute( ExternalCommandData commandData,
      ref string message, ElementSet elements )
    {
      _uidoc = commandData.Application.ActiveUIDocument;
      _doc = _uidoc.Document;
      _taskDialogTitle = "JK RC型枠数量";

      Transaction t = new Transaction( _doc, "Formwork" );

      if ( _uidoc.ActiveView.ViewType == ViewType.ThreeD )
      {
        try
        {
          _elementId = _uidoc.Selection.PickObject( ObjectType.Element ).ElementId;
          _element = _doc.GetElement( _elementId );
        }
        catch 
        {
          TaskDialog.Show( TaskDialogTitle, "キャンセルされました。" );
          return Result.Cancelled;
        }

        if ( _element.Category.Id == new ElementId( BuiltInCategory.OST_GenericModel ) )
        {
          t.Start();
          using ( FormConcreteFormwork displayForm = new FormConcreteFormwork( this ) )
          {
            displayForm.Refresh();
            displayForm.ShowDialog();
          }
          t.Commit();
        }
        else
        {
          TaskDialog.Show( TaskDialogTitle, "「一般モデル」を選択してください。",
            TaskDialogCommonButtons.Retry );
          return Result.Cancelled;
        }
      }
      else
      {
        TaskDialog.Show( TaskDialogTitle, "「3Dビュー」で実行してください。" );
        return Result.Cancelled;
      }
      return Result.Succeeded;
    }
  }
}
