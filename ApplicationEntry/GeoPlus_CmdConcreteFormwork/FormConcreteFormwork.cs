using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NoahDesign.Folder_Command;
using NoahDesign.Folder_Component;
using BuildingCoder;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;


namespace NoahDesign.Folder_WinForm
{
  public partial class FormConcreteFormwork : System.Windows.Forms.Form
  {
    #region Class Member

    CmdConcreteFormwork _dataBuffer = null;

    const string _url = "https://www.youtube.com/channel/UChQ0_nEY87RxvhSqmC7jVmw/playlists";
    private double _area = 0;
    private int _index = 0;
    private Dictionary<int, Element> _dictFilters = new Dictionary<int, Element>();
    private List<Element> _retrieved = new List<Element>();
    private List<Element> _insideMass = new List<Element>();

    #endregion

    #region WindowsForm
    public FormConcreteFormwork( CmdConcreteFormwork dataBuffer )
    {
      InitializeComponent();
      _dataBuffer = dataBuffer;
    }

    private void FormConcreteFormwork_Load( object sender, EventArgs e )
    {
      InitializeFilterData();
    }

    private void ButtonOk_Click( object sender, EventArgs e )
    {
      listView1.Refresh();

      if ( listView1.CheckedItems.Count != 0 )
      {
        foreach ( ListViewItem item in listView1.CheckedItems )
          _insideMass.Add( _dictFilters[item.Index] );

        using ( SubTransaction subTransaction = new SubTransaction( _dataBuffer.Document ) )
        {
          List<Solid> intersecs = new List<Solid>();
          List<Solid> solids = new List<Solid>();

          Solid massSolid = Util_ConcreteVolume.GetElementSolid( _dataBuffer.Element );

          try
          {
            foreach ( var item in _insideMass )
            {
              var solid = Util_ConcreteVolume.GetElementSolid( item );
              solids.Add( solid );
            }

            foreach ( Solid solid in solids )
            {
              var inter = BooleanOperationsUtils.ExecuteBooleanOperation( massSolid, solid,
              BooleanOperationsType.Intersect );
              intersecs.Add( inter );
            }

            if ( intersecs != null )
            {
              Util_ConcreteVolume.CreateDirectShapeNoneVolume( _dataBuffer.Document, intersecs );
              TaskDialog.Show( _dataBuffer.TaskDialogTitle, "完了しました。\n" +
              "体積は「コメント」パラメータを参考してください。", TaskDialogCommonButtons.Close );
            }

          }
          catch ( Exception ex )
          {
            TaskDialog.Show( _dataBuffer.TaskDialogTitle, "範囲を正しく指定してください。\n" +
              "RCではないインスタンスが含まれています。" );
            this.Dispose();
            this.DialogResult = DialogResult.Retry;
          }
          finally
          {
            this.Dispose();
          }
        }
      }

    }

    private void Button1_Click( object sender, EventArgs e )
    {
      for ( int i = 0; i < listView1.Items.Count; i++ )
        listView1.Items[i].Checked = true;
    }

    private void Button2_Click( object sender, EventArgs e )
    {
      for ( int i = 0; i < listView1.Items.Count; i++ )
        listView1.Items[i].Checked = false;
    }

    private void ButtonClose_Click( object sender, EventArgs e )
    {
      FormConcreteFormwork.ActiveForm.Close();
      this.Dispose();
    }

    private void ButtonTutorial_Click( object sender, EventArgs e )
    {
      System.Diagnostics.Process.Start( _url );
    } 
    #endregion

    #region Class Implementations 
    /// <summary>
    /// 선택한 매스에 간섭한 엘레멘트만 취득하기.
    /// </summary>
    void InitializeFilterData()
    {
      _retrieved = RetrieveElementsByCategories( _dataBuffer.Document );

      foreach ( Element e in _retrieved )
      {
        _dictFilters.Add( _index, e );
        _index++;
      }

      foreach ( int i in _dictFilters.Keys )
      {
        ListViewItem lvt = new ListViewItem( _dictFilters[i].Category.Name );
        lvt.SubItems.Add( _dictFilters[i].Name );
        lvt.SubItems.Add( _dictFilters[i].Id.ToString() );
        listView1.Items.Add( lvt );
      }

      listView1.Text.Trim();
      listView1.GridLines = true;
      listView1.AutoResizeColumns( ColumnHeaderAutoResizeStyle.ColumnContent );
      listView1.AutoResizeColumns( ColumnHeaderAutoResizeStyle.HeaderSize );
      listView1.Columns[0].Width = 150;
      listView1.Columns[1].Width = 150;
      listView1.Columns[2].Width = 100;

      string typeCount = String.Format( "項目数 : {0}", listView1.Items.Count );
      listCount.Text = typeCount;
    }

    /// <summary>
    /// 複数のBuiltInCategoryからElement Listを返す
    /// </summary>
    /// <param name="doc"></param>
    /// <returns></returns>
    List<Element> RetrieveElementsByCategories( Document doc )
    {
      listView1.Refresh();

      // Mass BoundingBoxIntersectsFilter 
      var bboxMax = _dataBuffer.Element.get_BoundingBox( doc.ActiveView ).Max;
      var bboxMin = _dataBuffer.Element.get_BoundingBox( doc.ActiveView ).Min;
      var outline = new Outline( bboxMin, bboxMax );
      BoundingBoxIntersectsFilter bboxFilter = new BoundingBoxIntersectsFilter( outline );

      List<Element> listOfElements = new List<Element>();

      List<BuiltInCategory> bics = new List<BuiltInCategory> {
        BuiltInCategory.OST_Columns,
        BuiltInCategory.OST_StructuralColumns,
        BuiltInCategory.OST_StructuralFraming,
        BuiltInCategory.OST_StructuralFoundation,
        BuiltInCategory.OST_Walls,
        BuiltInCategory.OST_Floors };
      foreach ( BuiltInCategory bic in bics )
      {
        var collection = new FilteredElementCollector( doc, doc.ActiveView.Id )
          .WherePasses( bboxFilter )
          .OfCategory( bic )
          .WhereElementIsNotElementType()
          .ToElements();

        foreach ( Element e in collection )
          listOfElements.Add( e );
      }
      return listOfElements;
    }
    #endregion


    
    void GetSideFaces(List<Face> verticalFaces, Solid solid )
    {
      FaceArray faces = solid.Faces;
      foreach ( Face f in faces )
      {
        if ( f is PlanarFace )
        {
          if ( Util.IsVertical( f as PlanarFace ) )
            verticalFaces.Add( f );
        }
        if ( f is CylindricalFace )
        {
          if ( Util.IsVertical( f as CylindricalFace ) )
            verticalFaces.Add( f );
        }
      }
    }


  }
}
