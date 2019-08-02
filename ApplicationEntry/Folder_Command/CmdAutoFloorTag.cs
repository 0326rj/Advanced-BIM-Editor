using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.Revit.Attributes;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.Exceptions;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;

namespace NoahDesign.Folder_Command
{
  [Transaction(TransactionMode.Manual)]
  class CmdAutoFloorTag : IExternalCommand
  {   
    const string _param_name1 = "床スラブ_CON天端レベル";
    const string _param_name2 = "床スラブ_構造体天端レベル";
    const string _param_name3 = "床スラブ_部分ふかし";

    public Result Execute( ExternalCommandData commandData,
      ref string message, ElementSet elements )
    {
      UIApplication uiapp = commandData.Application;
      UIDocument uidoc = uiapp.ActiveUIDocument;
      //Application app = uiapp.Application;
      Document doc = uidoc.Document;

      FilteredElementCollector collector =
        new FilteredElementCollector( doc, doc.ActiveView.Id );

      ICollection<Element> collection = collector
        .OfClass( typeof( Floor ) )
        .ToElements();

      try
      {
        SetParameters( doc, collection ); // パラメータ入力

        using ( Transaction t = new Transaction( doc, "trans" ) )
        {
          t.Start( "create" );    
          CreateIndependentTag( doc, collection, doc.ActiveView.Id ); // タグ生成
          CreateSpotDimension( doc, collection );
          t.Commit();
        }
        TaskDialog.Show( "Result", "配置完了 : 床タグ " );
      }
      catch ( Exception e )
      {
        message = e.Message;
        return Result.Failed;
      }
      return Result.Succeeded;
    }


    /// <summary>
    /// 인스턴스파라메터 첫번째, 두번째 값을 동시에 설정한다
    /// </summary>
    /// <param name="doc"></param>
    /// <param name="viewId"></param>
    private static void SetParameters(Document doc, ICollection<Element> collection )
    {
      var slabList = new List<Floor>();
      var pList = new List<string>();

      foreach ( var e in collection )
        slabList.Add( e as Floor );

      foreach ( var slab in slabList )
      {
        pList.Add( slab.get_Parameter( BuiltInParameter.FLOOR_HEIGHTABOVELEVEL_PARAM )
          .AsValueString() );

        // Transaction
        using ( Transaction t1 = new Transaction( doc, "FirstTrans" ) )
        {
          t1.Start( "FirstTrans" );
          foreach ( string value in pList )
          {
            if ( int.Parse( value ) > 0 )
            {
              slab.LookupParameter( _param_name1 ).Set( "+" + value );
              if ( int.Parse( value ) < 0 )
                slab.LookupParameter( _param_name1 ).Set( "-" + value );
            }
            else
              slab.LookupParameter( _param_name1 ).Set( value );
          }
          t1.Commit();
        }
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="doc"></param>
    /// <param name="collection"></param>
    /// <param name="viewId"></param>
    /// <returns></returns>
    private static List<IndependentTag> CreateIndependentTag(
      Document doc,
      ICollection<Element> collection,
      ElementId viewId )
    {
      var tagMode = TagMode.TM_ADDBY_CATEGORY;
      var tagorn = TagOrientation.Horizontal;

      List<IndependentTag> tagList = new List<IndependentTag>();
      foreach ( Element e in collection ) // 슬래브태그 생성
      {
        Reference referenceToTag = new Reference( e );
        XYZ hostLocation = DsPointToXYZ( e, doc.ActiveView );

        var tag = IndependentTag.Create(
          doc, viewId, referenceToTag,
          false, tagMode, tagorn, new XYZ() );

        tag.TagHeadPosition = hostLocation;
        tagList.Add( tag );
      }
      return tagList;
    }

    private static List<SpotDimension> CreateSpotDimension(
      Document doc,
      ICollection<Element> collection)
    {
      View view = doc.ActiveView;

      List<SpotDimension> sdList = new List<SpotDimension>();
      foreach ( Element e in collection )
      {
        var refslab = GetSlabTopReference( doc, e );
        XYZ p = DsPointToXYZ( e, view );
        XYZ pt = new XYZ( p.X + 1.0, p.Y, p.Z );

        var sd = doc.Create.NewSpotElevation( view, refslab, p, pt, pt, p, true );
        //sd.get_Parameter( BuiltInParameter.SPOT_ELEV_SINGLE_OR_UPPER_PREFIX ).Set( "(" );
        //sd.get_Parameter( BuiltInParameter.SPOT_ELEV_SINGLE_OR_UPPER_SUFFIX ).Set( ")" );
        sdList.Add( sd );  
      }
      return sdList;
    }

    private static Reference GetSlabTopReference( Document doc, Element e )
    {
      Reference ret = null;
      Options opt = doc.Application.Create.NewGeometryOptions();
      opt.ComputeReferences = true;

      var geo = e.get_Geometry( opt );
      foreach ( var obj in geo )
      {
        var inst = obj as GeometryInstance;
        if ( null != inst )
        {
          geo = inst.GetSymbolGeometry();
          break;
        }
      }

      var solid = geo
        .OfType<Solid>()
        .First<Solid>( x => null != x );

      double z = double.MinValue;

      foreach ( Autodesk.Revit.DB.Face f in solid.Faces )
      {
        BoundingBoxUV b = f.GetBoundingBox();
        var p = b.Min;
        var q = b.Max;
        var midparam = p + 0.6 * ( q - p );
        XYZ midpoint = f.Evaluate( midparam );
        XYZ normal = f.ComputeNormal( midparam );

        if ( PointsUpwards( normal ) )
        {
          if ( midpoint.Z > z )
          {
            z = midpoint.Z;
            ret = f.Reference;
          }
        }
      }
      return ret;
    }

    private static bool PointsUpwards( XYZ v )
    {
      const double _minimumSlope = 0.3;

      double horizontalLength = ( v.X * v.X ) + ( v.Y * v.Y );
      double verticalLength = ( v.Z * v.Z );

      return 0 < v.Z
        && _minimumSlope
        < verticalLength / horizontalLength;
    }

    private static XYZ DsPointToXYZ( Element e, View view )
    {
      var max = e.get_BoundingBox( view ).Max;
      var min = e.get_BoundingBox( view ).Min;
      XYZ result = DsMidPoint( max, min );
      return result;
    }

    private static XYZ DsMidPoint(XYZ p1, XYZ p2 )
    {
      double value1 = p1.X;
      double value2 = p1.Y;
      double value3 = p1.Z;
      double value4 = p2.X;
      double value5 = p2.Y;
      double value6 = p2.Z;
      XYZ pt = new XYZ( ( value1 + value4 ), ( value2 + value5 ), ( value3 + value6 ) );
      XYZ result = 0.5 * pt;
      return result;
    }
  }
}
