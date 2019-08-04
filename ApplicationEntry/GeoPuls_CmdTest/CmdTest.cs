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
    const string _familyName = "DynamoControlled_slab1";
    const string _typeName1 = "set1";
    const string _typeName2 = "set2";
    const string _typeName3 = "set3";
    const string _typeName4 = "set4";
    const string _typeName5 = "set5";
    const string _typeName6 = "set6";

    const string _param_name1 = "床スラブ_CON天端レベル";
    const string _param_name2 = "床スラブ_構造体天端レベル";
    const string _param_name3 = "床スラブ_部分ふかし";
  
    private BuiltInParameter tenba = BuiltInParameter.FLOOR_HEIGHTABOVELEVEL_PARAM;

    #endregion

    #region Property
    private UIDocument uidoc;
    private Document doc;

    public UIDocument UIDocument
    {
      get { return uidoc; }
    }

    public Document Document
    {
      get { return doc; }
    }

    #endregion

    ElementId _tagType1 = new ElementId( 4545935 ); // ふかしあり
    ElementId _tagType2 = new ElementId( 4545933 ); // 一般
    ElementId _tagType3 = new ElementId( 4545945 ); // 一般_上部
    ElementId _tagType4 = new ElementId( 5218253 ); // 下端打放し
    ElementId _tagType5 = new ElementId( 4545937 ); // 断熱材あり
    ElementId _tagType6 = new ElementId( 5218251 ); // 断熱材・ふかしあり
    ElementId _tagType7 = new ElementId( 4946329 ); // 構造体勾配
    ElementId _tagType8 = new ElementId( 4946329 ); // 構造体勾配

    public Result Execute( ExternalCommandData commandData, ref string message, ElementSet elements )
    {
      uidoc = commandData.Application.ActiveUIDocument;
      doc = uidoc.Document;

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
              IndependentTag tag = CreateFloorTag( doc, item );

              Element e = doc.GetElement( item );
         
              ChangeTagTypeByFloorCondition( doc, e as Floor , tag );
            }
            TaskDialog.Show( "title", "タグ生成完了" );
          }
          else
          {
            TaskDialog.Show( "Auto Tag System", "床を選択してください。" );
            return Result.Cancelled;
          }
          tx.Commit();
        }
        catch ( Exception ex )
        {
          TaskDialog.Show( "...", ex.Message );
          return Result.Failed;
        }      
      }
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

    void ChangeTagType( Document doc, IndependentTag independentTag )
    {
      var hostElement = independentTag.GetTaggedLocalElement();

      if ( hostElement.Category.Id == new ElementId( BuiltInCategory.OST_Floors ) )
      {

      }
    }

    void ChangeTagTypeByFloorCondition(Document doc, Floor floor, IndependentTag tag )
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

          if ( comStruct.LayerCount == 1 && stl.Function == MaterialFunctionAssignment.Structure )
          {
            tag.ChangeTypeId( _tagType2 );
          }

          else if ( comStruct.LayerCount == 2 )
          {
            if ( stl.Function == MaterialFunctionAssignment.Insulation )
              tag.ChangeTypeId( _tagType5 );
            else
              tag.ChangeTypeId( _tagType1 );        
          }

          else if ( comStruct.LayerCount == 3 
            && stl.Function == MaterialFunctionAssignment.Insulation
            && stl.Function == MaterialFunctionAssignment.Structure
            && stl.Function == MaterialFunctionAssignment.Substrate)
          {
            tag.ChangeTypeId( _tagType6 );
          }


        }
      }
    }
  }
}
