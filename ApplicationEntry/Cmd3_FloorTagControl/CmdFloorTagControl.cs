﻿#region 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.ApplicationServices;
using NoahDesign.Folder_WinForm;
using System.Diagnostics;
#endregion

// 2019. 08. 06 Jaebum Kim

namespace NoahDesign.Cmd3_FloorTagControl
{
  [Transaction(TransactionMode.Manual)]
  public class CmdFloorTagControl : IExternalCommand
  {
    #region Field
    const string _param_name1 = "床スラブ_CON天端レベル";
    const string _param_name2 = "床スラブ_構造体天端レベル";
    const string _param_name3 = "床スラブ_部分ふかし";

    ElementId _tagType1 = new ElementId( 4545935 ); // ふかしあり
    ElementId _tagType2 = new ElementId( 4545933 ); // 一般
    ElementId _tagType3 = new ElementId( 4545945 ); // 一般_上部
    ElementId _tagType4 = new ElementId( 5218253 ); // 下端打放し
    ElementId _tagType5 = new ElementId( 4545937 ); // 断熱材あり
    ElementId _tagType6 = new ElementId( 5218251 ); // 断熱材・ふかしあり
    ElementId _tagType7 = new ElementId( 4946329 ); // 構造体勾配
    ElementId _tagType8 = new ElementId( 4946329 ); // 構造体勾配
    #endregion

    #region Property
    private string _dialogTitle;
    private UIApplication _uiapp;
    private Application _app;
    private UIDocument _uidoc;
    private Document _doc;
    private List<Reference> _floorRefs;
    private double _heightLevel1;
    private double _heightLevel2;

    public IndependentTag _independentTag { get; set; }

    public string DialogTitle
    {
      get { return _dialogTitle; }
      set { _dialogTitle = value; }
    }

    public UIApplication UIApplication
    {
      get { return _uiapp; }
    }

    public Application Application
    {
      get { return _app; }
    }

    public UIDocument UIDocument
    {
      get { return _uidoc; }
    }

    public Document Document
    {
      get { return _doc; }
    }

    public List<Reference> FloorRefs
    {
      get { return _floorRefs; }
      set { _floorRefs = value; }
    }

    public double HeightLevel1
    {
      get { return _heightLevel1; }
      set { _heightLevel1 = value; }
    }

    public double HeightLevel2
    {
      get { return _heightLevel2; }
      set { _heightLevel2 = value; }
    }
    #endregion

    /// <summary>
    /// 
    /// </summary>
    /// <param name="commandData"></param>
    /// <param name="message"></param>
    /// <param name="elements"></param>
    /// <returns></returns>
    public Result Execute(
      ExternalCommandData commandData,
      ref string message,
      ElementSet elements )
    {
      _uiapp = commandData.Application;
      _app = _uiapp.Application;
      _uidoc = _uiapp.ActiveUIDocument;
      _doc = _uidoc.Document;
      DialogTitle = "JK Automatic Tag Control";

      


      try
      {
        FloorRefs = _uidoc.Selection
          .PickObjects( ObjectType.Element ) as List<Reference>;
      }
      catch ( Exception ex )
      {
        TaskDialog.Show( _dialogTitle, "Cancelled", TaskDialogCommonButtons.Ok );
        return Result.Cancelled;
      }

      using ( Transaction tx = new Transaction( _doc, "TestCommand" ) )
      {
        try
        {
          using ( FormProgressBar progress = new FormProgressBar( _floorRefs.Count ) )
          {
            tx.Start();

            if ( _floorRefs != null &&
              ( _doc.ActiveView.ViewType == ViewType.EngineeringPlan ||
              _doc.ActiveView.ViewType == ViewType.CeilingPlan ||
              _doc.ActiveView.ViewType == ViewType.FloorPlan ) )
            {
              List<Element> els = new List<Element>();

              foreach ( var floors in _floorRefs )
              {
                Element e = _doc.GetElement( floors );

                if ( e is Floor )
                {
                  // 1. タグ生成
                  _independentTag = CreateFloorTag( _doc, floors );
                  // 2. タイプ変換
                  ChangeTagTypeByFloorCondition( _doc, e as Floor, _independentTag );
                  // 3. Parameter入力
                  SyncFloorParameterValue( e as Floor );
                }
                else
                {
                  TaskDialog.Show( DialogTitle, "No slab selected." );
                  tx.RollBack();
                  return Result.Failed;
                }

                progress.Increment();
              }
            }
            else
            {
              TaskDialog.Show( DialogTitle, "Please select the slab only.",
                TaskDialogCommonButtons.Close );
              return Result.Cancelled;
            }


            tx.Commit();

            
          }


         
        }
        catch ( Exception ex )
        {
          TaskDialog.Show( DialogTitle, ex.Message );
          return Result.Failed;
        }
      }

      TaskDialog.Show( DialogTitle, "Slab tag creation completed successfully.",
        TaskDialogCommonButtons.Ok );
      return Result.Succeeded;
    }

    #region Floor Tag Creation Method

    /// <summary>
    /// 
    /// </summary>
    /// <param name="doc"></param>
    /// <param name="floors"></param>
    /// <returns></returns>
    private IndependentTag CreateFloorTag( Document doc, Reference floors )
    {
      var tag = IndependentTag.Create(
        doc,
        doc.ActiveView.Id,
        floors,
        false,
        TagMode.TM_ADDBY_CATEGORY,
        TagOrientation.Horizontal,
        new XYZ() );

      XYZ hostLocation = PointToXYZ( doc.GetElement( floors ), doc );
      MoveTag( tag, hostLocation );
      return tag;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tag"></param>
    /// <param name="pt"></param>
    private static void MoveTag( IndependentTag tag, XYZ pt )
    {
      tag.TagHeadPosition = pt;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="element"></param>
    /// <param name="doc"></param>
    /// <returns></returns>
    private static XYZ PointToXYZ( Element element, Document doc )
    {
      var max = element.get_BoundingBox( doc.ActiveView ).Max;
      var min = element.get_BoundingBox( doc.ActiveView ).Min;
      XYZ result = MidPoint( max, min );
      return result;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <returns></returns>
    private static XYZ MidPoint( XYZ p1, XYZ p2 )
    {
      double x1 = p1.X;
      double y1 = p1.Y;
      double z1 = p1.Z;
      double x2 = p2.X;
      double y2 = p2.Y;
      double z2 = p2.Z;

      XYZ pt = new XYZ( ( x1 + x2 ), ( y1 + y2 ), ( z1 + z2 ) );
      XYZ result = 0.5 * pt;
      return result;
    }
    #endregion

    #region Floor Tag Type Changing Method
    /// <summary>
    /// 
    /// </summary>
    /// <param name="doc"></param>
    /// <param name="floor"></param>
    /// <param name="tag"></param>
    void ChangeTagTypeByFloorCondition(
      Document doc,
      Floor floor,
      IndependentTag tag )
    {
      FloorType floorType = floor.FloorType;

      if ( floorType.IsValidObject )
      {
        CompoundStructure comStruct = floorType.GetCompoundStructure();
        Categories categories = doc.Settings.Categories;

        Category floorCat = categories.get_Item( BuiltInCategory.OST_Floors );
        // Material floorMat = floorCat.Material;

        foreach ( CompoundStructureLayer stl in comStruct.GetLayers() )
        {
          // Material layerMaterial = doc.GetElement( stl.MaterialId ) as Material;

          // レイヤーが 1つの場合、タイプ判定
          if ( comStruct.LayerCount == 1 &&
            stl.Function == MaterialFunctionAssignment.Structure )
          {
            tag.ChangeTypeId( _tagType2 );
          }

          // レイヤーが 2つの場合、タイプ判定
          else if ( comStruct.LayerCount == 2 )
          {
            if ( ( stl.LayerId == 0 )
              && ( stl.Function == MaterialFunctionAssignment.Substrate ) )
            {
              tag.ChangeTypeId( _tagType1 );
            }
            else if ( ( stl.LayerId == 1 )
              && ( stl.Function == MaterialFunctionAssignment.Substrate ) )
            {
              tag.ChangeTypeId( _tagType4 );
            }
            else if ( ( stl.LayerId == 1 )
              && ( stl.Function == MaterialFunctionAssignment.Insulation ) )
            {
              tag.ChangeTypeId( _tagType5 );
            }
          }

          // レイヤーが 3つの場合、タイプ判定
          else if ( comStruct.LayerCount == 3 )
          {
            if ( ( stl.LayerId == 2 )
              && ( stl.Function == MaterialFunctionAssignment.Insulation ) )
            {
              tag.ChangeTypeId( _tagType6 );
            }
            else
            {
              tag.ChangeTypeId( _tagType1 );
            }
          }

          // レイヤーが 4つ以上の場合、無効
          else if ( comStruct.LayerCount > 3 )
          {
            TaskDialog.Show( DialogTitle,
              "The slab contains more than four layers." );
            break;
          }
          else
            break;
        }
      }
    }
    #endregion

    #region Floor Parameter Value Sync Method
    /// <summary>
    /// 
    /// </summary>
    /// <param name="floor"></param>
    private void SyncFloorParameterValue( Floor floor )
    {

      #region No1. 床スラブ_CON天端レベル 設定
      _heightLevel1 = floor
          .get_Parameter( BuiltInParameter.FLOOR_HEIGHTABOVELEVEL_PARAM ).AsDouble();

      var hl1 = UnitUtils
        .Convert( _heightLevel1, DisplayUnitType.DUT_DECIMAL_FEET
        , DisplayUnitType.DUT_MILLIMETERS );

      if ( hl1 == 0 )
      {
        floor.LookupParameter( _param_name1 ).Set( "±0" );
      }
      else if ( hl1 > 0 )
      {
        String str = String.Format( "+{0:N0}", hl1 );
        floor.LookupParameter( _param_name1 ).Set( str );
      }
      else
      {
        floor.LookupParameter( _param_name1 ).Set( hl1.ToString() );
      }
      #endregion

      #region No2. 床スラブ_構造体天端レベル 設定
      double firstLayerThk = 0;
      FloorType floorType = floor.FloorType;
      CompoundStructure comStruct = floorType.GetCompoundStructure();

      foreach ( CompoundStructureLayer stl in comStruct.GetLayers() )
      {
        if ( comStruct.LayerCount >= 2 )
        {
          // *** GetFirtsCoreLayerIndex() 
          if ( ( stl.LayerId <= comStruct.GetFirstCoreLayerIndex() ) 
            && ( stl.Function != MaterialFunctionAssignment.Structure ) )
          {
            firstLayerThk += stl.Width;
          }                
        }
        else
          return;
      }

      var level = floor
        .get_Parameter( BuiltInParameter.FLOOR_HEIGHTABOVELEVEL_PARAM ).AsDouble();

      var frt = UnitUtils
        .Convert( firstLayerThk, DisplayUnitType.DUT_DECIMAL_FEET
        , DisplayUnitType.DUT_MILLIMETERS );
      var lvh = UnitUtils
        .Convert( level, DisplayUnitType.DUT_DECIMAL_FEET
        , DisplayUnitType.DUT_MILLIMETERS );

      HeightLevel2 = ( lvh - frt );

      if ( ( HeightLevel2 != 0 ) && ( hl1 != HeightLevel2 ) )
      {
        if ( HeightLevel2 > 0 )
        {
          String str = String.Format( "+{0:N0}", HeightLevel2 );
          floor.LookupParameter( _param_name2 ).Set( str );
        }
        else
          floor.LookupParameter( _param_name2 ).Set( HeightLevel2.ToString() );
      }
      else
        floor.LookupParameter( _param_name2 ).Set( "" );

      #endregion

      // No3. 床スラブ_部分ふかし (作成予定)
    }
    #endregion

  }
}
