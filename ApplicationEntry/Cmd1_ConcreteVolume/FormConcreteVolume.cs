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

namespace ApplicationEntry.Cmd1_ConcreteVolume
{
  public partial class FormConcreteVolume : System.Windows.Forms.Form
  {
    #region Class Member
    const string url = "https://www.youtube.com/channel/UChQ0_nEY87RxvhSqmC7jVmw/playlists";
    private double volume = 0;
    private int index = 0;
    private Dictionary<int, Element> m_dictFilters = new Dictionary<int, Element>();
    private List<Element> m_retrieved = new List<Element>();
    private List<Element> m_insideMass = new List<Element>();
    #endregion

    #region WindowsForm
    public FormConcreteVolume( CmdConcreteUtil dataBuffer )
    {    
      InitializeComponent();
      m_dataBuffer = dataBuffer;
    }

    
    private void FormConcreteVolume_Load( object sender, EventArgs e )
    {
      //this.SuspendLayout();
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

    // an instance of ConcreteVolume class
    CmdConcreteUtil m_dataBuffer = null;

    /// <summary>
    /// OK버튼으로 DirectShape를 최종 생성
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Button3_Click( object sender, EventArgs e )
    {
      listView1.Refresh();

      if ( listView1.CheckedItems.Count != 0 )
      {
        foreach ( ListViewItem item in listView1.CheckedItems )
          m_insideMass.Add( m_dictFilters[item.Index] );

        using ( SubTransaction subTransaction = new SubTransaction( m_dataBuffer.Document ) )
        {
          List<Solid> intersecs = new List<Solid>();
          List<Solid> solids = new List<Solid>();

          Solid massSolid = Util_ConcreteVolume.GetElementSolid( m_dataBuffer.Element );

          try
          {          
            foreach ( var item in m_insideMass )
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
                volume += inter.Volume;       
            }
            volume = UnitUtils.ConvertFromInternalUnits( volume, DisplayUnitType.DUT_CUBIC_METERS );
           
            if ( intersecs != null  )
            {          
              if ( this.volume > 0 )
              {
                Util_ConcreteVolume.CreateDirectShape( m_dataBuffer.Document, intersecs, volume );
                TaskDialog.Show( m_dataBuffer.TaskDialogTitle, "完了しました。\n" +
                "体積は「コメント」パラメータを参考してください。", TaskDialogCommonButtons.Close );
              }
              else
              {
                TaskDialog.Show( m_dataBuffer.TaskDialogTitle, "体積が 0判定です。\n" +
                "正しくない部材が選択されています。", TaskDialogCommonButtons.Close );
              }

              this.DialogResult = DialogResult.OK;      
              this.Close();
            }
            else
            {
              TaskDialog.Show( m_dataBuffer.TaskDialogTitle,
                "マスの範囲に該当するインスタンスがありまんせでした。" );
              this.DialogResult = DialogResult.Retry;
              this.Refresh();
              return;         
            }              
          }
          catch ( Exception ex )
          {
            TaskDialog.Show( m_dataBuffer.TaskDialogTitle, "範囲を正しく指定してください。\n" +
              "RCではないインスタンスが含まれています。");
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
        TaskDialog.Show( m_dataBuffer.TaskDialogTitle, "選択されている項目がありません。" );
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
    /// Tutorialページのリンク
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Button4_Click( object sender, EventArgs e )
    {
      System.Diagnostics.Process.Start( url );
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
      m_retrieved = RetrieveElementsByCategories( m_dataBuffer.Document );

      foreach ( Element e in m_retrieved )
      {
        m_dictFilters.Add( index, e );
        index++;
      }
      
      foreach ( int i in m_dictFilters.Keys )
      {       
        ListViewItem lvt = new ListViewItem( m_dictFilters[i].Category.Name );
        lvt.SubItems.Add( m_dictFilters[i].Name );
        lvt.SubItems.Add( m_dictFilters[i].Id.ToString() );       
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
      var bboxMax = m_dataBuffer.Element.get_BoundingBox( doc.ActiveView ).Max;
      var bboxMin = m_dataBuffer.Element.get_BoundingBox( doc.ActiveView ).Min;
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
  }
}
