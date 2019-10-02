using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NoahDesign.Folder_Component;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using NoahDesign.Cmd1_ConcreteVolume;
using MyUtils;

namespace ApplicationEntry.Cmd1_ConcreteVolume
{
  public partial class FormConcreteVolume : System.Windows.Forms.Form
  {
    #region Field
    WindowHandle _handle;
    private double _volume = 0;
    private int _index = 0;
    private CmdConcreteUtil _dataBuffer;
    private Dictionary<int, Element> _dictFilters = new Dictionary<int, Element>();
    private List<Element> _retrieved = new List<Element>();
    private List<Element> _insideMass = new List<Element>();
    #endregion

    #region WindowsForm
    public FormConcreteVolume( CmdConcreteUtil dataBuffer, WindowHandle handle )
    {
      InitializeComponent();
      _dataBuffer = dataBuffer;
      _handle = handle;
    }

    private void FormConcreteVolume_Load( object sender, EventArgs e )
    {
      this.KeyPreview = true;
      InitializeFilterData();
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


    private void buttonStart_Click( object sender, EventArgs e )
    {
      if ( listView1.CheckedItems.Count != 0 )
      {
        foreach ( ListViewItem item in listView1.CheckedItems )
          _insideMass.Add( _dictFilters[item.Index] );

        using ( Transaction subTransaction = new Transaction( _dataBuffer.Document, "Trans" ) )
        {
          //subTransaction.Start();
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

              if ( solid.Volume > 0 )
                _volume += inter.Volume;
            }
            _volume = UnitUtils.ConvertFromInternalUnits( _volume, DisplayUnitType.DUT_CUBIC_METERS );

            if ( intersecs != null )
            {
              if ( _volume > 0 )
              {
                Util_ConcreteVolume.CreateDirectShape( _dataBuffer.Document, intersecs, _volume );
                TaskDialog.Show( _dataBuffer.TaskDialogTitle, "完了しました。\n" +
                "体積は「コメント」パラメータを参考してください。", TaskDialogCommonButtons.Close );

              }
              else
              {
                TaskDialog.Show( _dataBuffer.TaskDialogTitle, "体積が 0判定です。\n" +
                "正しくない部材が選択されています。", TaskDialogCommonButtons.Close );
              }

              this.DialogResult = DialogResult.OK;
              this.Close();
            }
            else
            {
              TaskDialog.Show( _dataBuffer.TaskDialogTitle,
                "マスの範囲に該当するインスタンスがありまんせでした。" );
              this.DialogResult = DialogResult.Retry;
              this.Refresh();
              return;
            }
          }
          catch ( Exception )
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
      else
      {
        TaskDialog.Show( _dataBuffer.TaskDialogTitle, "選択されている項目がありません。" );
        this.DialogResult = DialogResult.None;
      }
    }

    /// <summary>
    /// Windowを閉じる
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Button3_Click_1( object sender, EventArgs e )
    {
      FormConcreteVolume.ActiveForm.Close();
      this.Dispose();
    }


    /// <summary>
    /// 選択されたItemがない場合、何もしない
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ListView1_SelectedIndexChanged( object sender, EventArgs e )
    {
      if ( listView1.Items.Count == 0 || listView1.SelectedItems.Count == 0 )
        return;
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

    private void FormConcreteVolume_KeyUp( object sender, KeyEventArgs e )
    {
      if ( e.KeyCode == Keys.Escape )
        this.Dispose(); this.Close();
    }
  }
}
