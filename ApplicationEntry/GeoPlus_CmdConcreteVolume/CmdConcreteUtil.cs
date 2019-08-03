using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationEntry.Folder_WinForm;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using NoahDesign.Folder_WinForm;

namespace NoahDesign.Folder_Command
{
  [Transaction( TransactionMode.Manual )]
  public class CmdConcreteUtil : IExternalCommand
  {
    #region Property
    private Document m_doc;
    private UIDocument m_uidoc;
    private Element m_element;
    private ElementId m_elementId;
    private List<ElementId> m_elementIds;
    private string m_taskDialogTitle = "JK 体積計算";

    public Document Document
    {
      get { return m_doc; }
    }

    public UIDocument UIDocument
    {
      get { return m_uidoc; }
    }  

    public Element Element
    {
      get { return m_element; }
    }

    public ElementId ElementId
    {
      get { return m_elementId; }
    }

    public List<ElementId> ElementIds
    {
      get { return m_elementIds; }
      set { m_elementIds = value; }
    }

    public string TaskDialogTitle
    {
      get { return m_taskDialogTitle; }
      set { m_taskDialogTitle = value; }
    }

    #endregion

    public Result Execute( ExternalCommandData commandData,
      ref string message, ElementSet elements )
    {      
      m_uidoc = commandData.Application.ActiveUIDocument;
      m_doc = m_uidoc.Document;

      Transaction t = new Transaction( m_doc, "ConcreteModel" );
  
      if ( m_uidoc.ActiveView.ViewType == ViewType.ThreeD )
      {
        try
        {
          m_elementId = m_uidoc.Selection.PickObject( ObjectType.Element ).ElementId;
          m_element = m_uidoc.Document.GetElement( m_elementId );
        }
        catch
        {
          TaskDialog.Show( m_taskDialogTitle, "キャンセルされました。" );
          return Result.Cancelled;
        }
       
        if ( m_element.Category.Id == new ElementId( BuiltInCategory.OST_GenericModel ) )
        {
          t.Start();
          using ( FormConcreteVolume displayForm = new FormConcreteVolume( this ) )
          {
            displayForm.Refresh();
            displayForm.ShowDialog();         
          }
          t.Commit();
        }
        else
        {
          TaskDialog.Show( m_taskDialogTitle, "「一般モデル」を選択してください。",
            TaskDialogCommonButtons.Retry );
          return Result.Cancelled;
        }
      }
      else
      {
        TaskDialog.Show( m_taskDialogTitle, "「3Dビュー」で実行してください。" );
        return Result.Cancelled;
      }
      return Result.Succeeded;
    }
  }
}
