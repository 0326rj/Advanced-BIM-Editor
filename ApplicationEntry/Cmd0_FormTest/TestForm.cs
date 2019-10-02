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
    UIDocument uidoc;
    Document doc;
    ExternalCommandData commandData;
    WindowHandle handle;

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


    public TestForm( UIDocument uidoc, Document doc, ExternalCommandData commandData, WindowHandle handle )
    {
      this.uidoc = uidoc;
      this.doc = doc;
      this.commandData = commandData;
      this.handle = handle;

      InitializeComponent();
    }

    private void TestForm_Load( object sender, EventArgs e )
    {
      this.KeyPreview = true;
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

      int totalElementSum = 0;
      foreach ( var catName in categoryDict.Keys )
      {
        var elements = new FilteredElementCollector( doc, doc.ActiveView.Id )
          .OfCategoryId( categoryDict[catName] )
          .ToElementIds()
          .ToList();

        string builtInCatName = EnumParseUtility<BuiltInCategory>
          .Parse( ( BuiltInCategory )categoryDict[catName].IntegerValue );

        int count = elements.Count;
        totalElementSum += count;

        ListViewItem viewItem = new ListViewItem( catName );
        viewItem.SubItems.Add( builtInCatName );
        viewItem.SubItems.Add( count.ToString() );
        listViewCategories.Items.Add( viewItem );
      }

      labelSum.Text = totalElementSum.ToString();
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
      treeViewFamily.Nodes.Clear();


      if ( listViewCategories.CheckedItems.Count == 0 )
        return;

      int checkedCount = listViewCategories.CheckedItems.Count;
      for ( int i = 0; i < checkedCount; i++ )
      {
        string catName = listViewCategories.CheckedItems[i].Text;
        var catId = categoryDict[catName];
        var bic = ( BuiltInCategory )catId.IntegerValue;

        var collection = new FilteredElementCollector( doc, doc.ActiveView.Id )
            .OfCategoryId( catId )
            .WhereElementIsNotElementType()
            .ToElements()
            .ToList();

        
      }
      treeViewFamily.Sort();
      treeViewFamily.CheckBoxes = true;
      treeViewFamily.ExpandAll();
    }



    //private void buttonLookup_Click( object sender, EventArgs e )
    //{
    //  treeViewFamily.Nodes.Clear();
    //  List<Element> elements = new List<Element>();

    //  if ( listViewCategories.CheckedItems.Count == 0 )
    //    return;

    //  int checkedCount = listViewCategories.CheckedItems.Count;
    //  for ( int i = 0; i < checkedCount; i++ )
    //  {
    //    string catName = listViewCategories.CheckedItems[i].Text;
    //    var catId = categoryDict[catName];

    //    var collector = new FilteredElementCollector( doc, doc.ActiveView.Id )
    //      .OfCategoryId( catId )
    //      .WhereElementIsNotElementType()
    //      .ToElements()
    //      .ToList();

    //    String format = String.Format( "{0}_{1} (ID: {2})", i + 1, catName, catId.IntegerValue );
    //    TreeNode familyNameNode = new TreeNode( format );

    //    Dictionary<Element, string> dictFamily = new Dictionary<Element, string>();
    //    List<Element> typeList = new List<Element>();
    //    foreach ( Element ele in collector )
    //    {
    //      var familyName = ele.get_Parameter( BuiltInParameter.ELEM_FAMILY_PARAM ).AsValueString();
    //      if ( familyName.Length != 0 )
    //      {
    //        dictFamily.Add( ele, familyName );
    //        typeList.Add( ele );
    //      }
    //    }


    //    if ( dictFamily.Keys.Count != 0 )
    //    {
    //      Dictionary<string, Element> dictType = new Dictionary<string, Element>();
    //      foreach ( var nameValue in dictFamily.Values.Distinct() )
    //      {
    //        Element ele = dictFamily.FirstOrDefault( x => x.Value.Contains( nameValue ) ).Key;
    //        dictType.Add( nameValue, ele );

    //        familyNameNode.Nodes.Add( nameValue );
    //      }

    //      foreach ( var key in dictType.Keys )
    //      {
    //        int count = 0;
    //        Element ele = dictType[key];

    //        familyNameNode.Nodes[count].Nodes.Add( ele.Name );
    //        count++;
    //      }

    //      treeViewFamily.Nodes.Add( familyNameNode );
    //    }
    //  }

    //  treeViewFamily.CheckBoxes = true;
    //  treeViewFamily.ExpandAll();
    //}


    private void buttonSelectFamily_Click( object sender, EventArgs e )
    {

    }


    #region 폼닫기 / 헬프/ 초기화버튼
    private void buttonRefreshForm_Click( object sender, EventArgs e )
    {
      this.Dispose();
      TestForm reloadForm = new TestForm( uidoc, doc, commandData, handle );
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
      this.Dispose(); this.Close();
    }

    private void TestForm_KeyUp( object sender, KeyEventArgs e )
    {
      if ( e.KeyCode == Keys.Escape )
      {
        this.buttonClose_Click( sender, e );
        this.Dispose();
      }

      if ( e.KeyCode == Keys.R )
      {
        this.buttonClose_Click( sender, e );
        this.Dispose();
        TestForm newForm = new TestForm( uidoc, doc, commandData, handle );
        newForm.Show();
      }       
    }

    #endregion


  }
}
