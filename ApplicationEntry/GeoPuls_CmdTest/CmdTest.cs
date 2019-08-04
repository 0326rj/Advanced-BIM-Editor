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
using BuildingCoder;
using System.Diagnostics;

namespace NoahDesign.GeoPuls_CmdTest
{
  [Transaction(TransactionMode.Manual)]
  public class CmdTest : IExternalCommand
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

    private string dialogTitle;
    private UIDocument uidoc;
    private Document doc;
    private string heightLevel1;
    private double heightLevel2;

    public string DialogTitle
    {
      get { return dialogTitle; }
      set { dialogTitle = value; }
    }

    public UIDocument UIDocument
    {
      get { return uidoc; }
    }

    public Document Document
    {
      get { return doc; }
    }

    public string HeightLevel1
    {
      get { return heightLevel1; }
      set { heightLevel1 = value; }
    }

    public double HeightLevel2
    {
      get { return heightLevel2; }
      set { heightLevel2 = value; }
    }

    #endregion

    public Result Execute( ExternalCommandData commandData, ref string message, ElementSet elements )
    {
      uidoc = commandData.Application.ActiveUIDocument;
      doc = uidoc.Document;
      DialogTitle = "Automatic Tag Control";

      List<Reference> selectedFloors = uidoc.Selection.PickObjects( ObjectType.Element ) as List<Reference>;
      
      using ( Transaction tx = new Transaction( doc, "TestCommand" ) )
      {
        try
        {
          tx.Start();

          if ( selectedFloors != null &&
            (doc.ActiveView.ViewType == ViewType.EngineeringPlan ||
            doc.ActiveView.ViewType == ViewType.CeilingPlan ||
            doc.ActiveView.ViewType == ViewType.FloorPlan) )
          {
            List<Element> els = new List<Element>();
            foreach ( var item in selectedFloors )
            {
              Element e = doc.GetElement( item );

              if ( e is Floor )
              {
                IndependentTag tag = CreateFloorTag( doc, item );
                ChangeTagTypeByFloorCondition( doc, e as Floor, tag );

                ResetTagParameter( e as Floor );
              }
              else
              {
                TaskDialog.Show( DialogTitle, "床が選択されませんでした。" );
                return Result.Failed;
              }         
            }            
          }
          else
          {
            TaskDialog.Show( DialogTitle, "床を選択してください。" );
            return Result.Cancelled;
          }

          tx.Commit();
        }
        catch ( Exception ex )
        {
          TaskDialog.Show( DialogTitle, ex.Message );
          return Result.Failed;
        }      
      }

      TaskDialog.Show( DialogTitle, "タグ生成完了" );
      return Result.Succeeded;
    }

    #region Floor Tag Creation Method
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

    private static void MoveTag( IndependentTag tag, XYZ pt )
    {
      tag.TagHeadPosition = pt;
    }

    private static XYZ PointToXYZ( Element element, Document doc )
    {
      var max = element.get_BoundingBox( doc.ActiveView ).Max;
      var min = element.get_BoundingBox( doc.ActiveView ).Min;
      XYZ result = MidPoint( max, min );
      return result;
    }

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

    #region Floor Tag Type Control

    void ChangeTagTypeByFloorCondition( Document doc, Floor floor, IndependentTag tag )
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

          // 바닥 레이어가 1개인 경우
          if ( comStruct.LayerCount == 1 )
          {
            tag.ChangeTypeId( _tagType2 );
          }

          // 바닥 레이어가 2개인 경우
          else if ( comStruct.LayerCount == 2 )
          {
            if ( stl.LayerId == 0 && stl.Function == MaterialFunctionAssignment.Substrate )
            {
              tag.ChangeTypeId( _tagType1 );
            }
            else if ( stl.LayerId == 1 && stl.Function == MaterialFunctionAssignment.Substrate )
            {
              tag.ChangeTypeId( _tagType4 );
            }
            else if ( stl.LayerId == 1 && stl.Function == MaterialFunctionAssignment.Insulation )
            {
              tag.ChangeTypeId( _tagType5 );
            }
          }

          // 바닥 레이어가 3개인 경우
          else if ( comStruct.LayerCount == 3 )
          {
            if ( stl.LayerId == 2 && stl.Function == MaterialFunctionAssignment.Insulation )
            {
              tag.ChangeTypeId( _tagType6 );
            }
            else
            {
              tag.ChangeTypeId( _tagType1 );
            }
          }

          else if ( comStruct.LayerCount > 3 )
          {
            TaskDialog.Show( DialogTitle, "床の構造レイヤーが4つ以上のものが含まれています。" );
          }

          else
            return;
        }
      }
    }
    #endregion

    void ResetTagParameter( Floor floor )
    {
      // No1. 床スラブ_CON天端レベル
      heightLevel1 = floor.get_Parameter( BuiltInParameter.FLOOR_HEIGHTABOVELEVEL_PARAM ).AsValueString();

      if ( heightLevel1 == "0" )
      {
        HeightLevel1 = "±0";
        floor.LookupParameter( _param_name1 ).Set( HeightLevel1 );
      }
      else
      {
        floor.LookupParameter( _param_name1 ).Set( HeightLevel1 );
      }


      // No2. 床スラブ_構造体天端レベル

      double structThk = 0;

      FloorType floorType = floor.FloorType;
      CompoundStructure comStruct = floorType.GetCompoundStructure();

      foreach ( CompoundStructureLayer stl in comStruct.GetLayers() )
      {
        if ( stl.Function != MaterialFunctionAssignment.Structure )
        {
          structThk += stl.Width;
          //heightLevel2 = floor.get_Parameter( BuiltInParameter.FLOOR_HEIGHTABOVELEVEL_PARAM ).AsDouble();
        }     
      }
      double floorThk = floor.get_Parameter( BuiltInParameter.FLOOR_ATTR_THICKNESS_PARAM ).AsDouble();
      var level = floor.get_Parameter( BuiltInParameter.FLOOR_HEIGHTABOVELEVEL_PARAM ).AsDouble();

      var stt = UnitUtils.Convert( structThk, DisplayUnitType.DUT_DECIMAL_FEET, DisplayUnitType.DUT_MILLIMETERS );
      var flt = UnitUtils.Convert( floorThk, DisplayUnitType.DUT_DECIMAL_FEET, DisplayUnitType.DUT_MILLIMETERS );
      var lvh = UnitUtils.Convert( level, DisplayUnitType.DUT_DECIMAL_FEET, DisplayUnitType.DUT_MILLIMETERS );

      HeightLevel2 = ( flt - stt ) - lvh;

      floor.LookupParameter( _param_name2 ).Set( HeightLevel2.ToString() );

      var srt = String.Format( "floorThk : {0}\n structThk : {1} ", flt, stt );
      TaskDialog.Show( "ddd", srt );




      // No3. 床スラブ_部分ふかし
    }
  }
}
