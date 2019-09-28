#region Namespace
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.ApplicationServices;


using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System.Diagnostics;
using MyUtils;
#endregion

namespace NoahDesign.Cmd0_FormTest
{
  public partial class TestForm : System.Windows.Forms.Form
  {
    /// <summary>
    /// 
    /// </summary>
    Autodesk.Revit.UI.UIDocument uidoc;
    Autodesk.Revit.DB.Document doc;

    /// <summary>
    /// 
    /// </summary>
    public List<BuiltInCategory> _builtInCategories { get; set; }

    /// <summary>
    /// 
    /// </summary>
    Dictionary<string, ElementId> categoryDict = new Dictionary<string, ElementId>();

    /// <summary>
    /// 
    /// </summary>
    bool _catChangedEventSuppress;


    public TestForm( Autodesk.Revit.UI.UIDocument uidoc, Autodesk.Revit.DB.Document doc )
    {
      InitializeComponent();
      this.uidoc = uidoc;
      this.doc = doc;
    }

    private void TestForm_Load( object sender, EventArgs e )
    {
      AddAppliableCategories();
    }

    /// <summary>
    /// listViewCategories에 카테고리와 빌트인카테고리명을 추가한다.
    /// </summary>
    void AddAppliableCategories()
    {
      listViewCategories.Items.Clear();
      Categories categories = doc.Settings.Categories;
      foreach ( Category category in categories )
      {
        var collection = new FilteredElementCollector( doc, doc.ActiveView.Id )
          .OfCategoryId( category.Id )
          .ToElementIds()
          .ToList();

        if ( collection.Count != 0 )
          categoryDict.Add( category.Name, category.Id );
      }

      foreach ( var catName in categoryDict.Keys )
      {
        var elements = new FilteredElementCollector( doc, doc.ActiveView.Id )
          .OfCategoryId( categoryDict[catName] )
          .ToElementIds()
          .ToList();

        string builtInCatName = EnumParseUtility<BuiltInCategory>
          .Parse( ( BuiltInCategory )categoryDict[catName].IntegerValue );
        int count = elements.Count;

        ListViewItem viewItem = new ListViewItem( catName );
        viewItem.SubItems.Add( builtInCatName );
        viewItem.SubItems.Add( count.ToString() );
        listViewCategories.Items.Add( viewItem );     
      }
    }



    private void buttonSelectAll_Click( object sender, EventArgs e )
    {
      for ( int i = 0; i < listViewCategories.Items.Count; i++ )
        listViewCategories.Items[i].Checked = true;
    }

    private void buttonUnselectAll_Click( object sender, EventArgs e )
    {
      for ( int i = 0; i < listViewCategories.Items.Count; i++ )
        listViewCategories.Items[i].Checked = false;
    }


    private void buttonSelectObject_Click( object sender, EventArgs e )
    {
      List<ElementId> collection = new List<ElementId>();

      int checkedCount = listViewCategories.CheckedItems.Count;
      if ( checkedCount == 0 )
        return;

      for ( int ii = 0; ii < checkedCount; ii++ )
      {
        string key = listViewCategories.CheckedItems[ii].Text;
        ElementId categoryId = categoryDict[key];

        var set = new FilteredElementCollector( doc, doc.ActiveView.Id )
          .OfCategoryId( categoryId )
          .WhereElementIsNotElementType()
          .ToElementIds()
          .ToList();
        collection.AddRange( set );
      }
      if ( collection.Count != 0 )
      {
        Selection selection = uidoc.Selection;
        selection.SetElementIds( collection );
      }
    }


    private void buttonLookup_Click( object sender, EventArgs e )
    {
      List<Element> elements = new List<Element>();

      listViewFamilyType.Items.Clear();
      treeViewFamily.Nodes.Clear();

      if ( listViewCategories.CheckedItems.Count == 0 )
        return;

      int checkedCount = listViewCategories.CheckedItems.Count;
      for ( int i = 0; i < checkedCount; i++ )
      {
        string key = listViewCategories.CheckedItems[i].Text;
        ElementId catId = categoryDict[key];

        var collection = new FilteredElementCollector( doc, doc.ActiveView.Id )
          .OfCategoryId( catId )
          .WhereElementIsNotElementType()
          .ToElements()
          .ToList();
        elements.AddRange( collection );
      }

      if ( elements.Count != 0 )
      {
        List<string> familyNames = new List<string>();
        foreach ( Element element in elements )
        {
          string familyName = element.get_Parameter( BuiltInParameter.ELEM_FAMILY_PARAM ).AsValueString();
          familyNames.Add( familyName );
        }

        familyNames.Distinct();
        foreach ( string n in familyNames ) 
        {
          TreeNode familyNode = new TreeNode( n );
          treeViewFamily.Nodes.Add( familyNode );
        }
        
      }

      


    }


    private void listViewCategories_SelectedIndexChanged( object sender, EventArgs e )
    {
      if ( _catChangedEventSuppress )
        return;

      listViewFamilyType.Items.Clear();

      List<ElementId> checkedCategoryIds = new List<ElementId>();
      List<Element> checkedElements = new List<Element>();

      int itemCount = listViewCategories.Items.Count;
      for ( int ii = 0; ii < itemCount; ii++ )
      {
        string key = listViewCategories.Items[ii].Text;
        ElementId catId = categoryDict[key];
        checkedCategoryIds.Add( catId );

        var collection = new FilteredElementCollector( doc, doc.ActiveView.Id )
          .OfCategoryId( catId )
          .WhereElementIsNotElementType()
          .ToElements()
          .ToList();
        checkedElements.AddRange( collection );
      }

      for ( int ii = 0; ii < checkedElements.Count; ii++ )
      {
        ListViewItem viewItem = new ListViewItem( checkedElements[ii].Name );
        listViewFamilyType.Items.Add( viewItem );
      }
    }



    private void buttonSelectFamily_Click( object sender, EventArgs e )
    {

    }



    #region 폼닫기 / 헬프/ 초기화버튼
    private void buttonRefreshForm_Click( object sender, EventArgs e )
    {
      this.Dispose();
      TestForm reloadForm = new TestForm( uidoc, doc );
      reloadForm.Show();
    }

    private void buttonHelp_Click( object sender, EventArgs e )
    {
      Folder_WinForm.AddInInfo helpForm = new Folder_WinForm.AddInInfo();
      helpForm.ShowDialog();
      helpForm.StartPosition = FormStartPosition.CenterParent;
    }

    private void buttonClose_Click( object sender, EventArgs e )
    {
      this.Close();
    }
    #endregion






    //private void listViewCategories_SelectedIndexChanged( object sender, EventArgs e )
    //{
    //  if ( _catChangedEventSuppress )
    //    return;

    //  List<ElementId> checkedCategoryIds = new List<ElementId>();
    //  List<Element> checkedElements = new List<Element>();

    //  int itemCount = listViewCategories.Items.Count;
    //  for ( int ii = 0; ii < itemCount; ii++ )
    //  {
    //    bool addItemToChecked = false;
    //    if ( null != e && e.Index == ii )
    //    {
    //      addItemToChecked = ( e.NewValue == CheckState.Checked );
    //      if ( !addItemToChecked )
    //        continue;
    //    }

    //    if ( addItemToChecked )
    //    {
    //      string key = listViewCategories.Items[ii].Text;
    //      ElementId catId = categoryDict[key];
    //      checkedCategoryIds.Add( catId );

    //      var collection = new FilteredElementCollector( doc, doc.ActiveView.Id )
    //        .OfCategoryId( catId )
    //        .WhereElementIsNotElementType()
    //        .ToElements()
    //        .ToList();
    //      checkedElements.AddRange( collection );
    //    }
    //  }
  }
}
