using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.DB;

namespace NoahDesign.Folder_Component
{
  public static class Util_ConcreteVolume
  {
    public static void Shuffle<T>( this IList<T> list )
    {
      Random rng = new Random();
      int n = list.Count;
      while ( n > 1 )
      {
        n--;
        int k = rng.Next( n + 1 );
        T value = list[k];
        list[k] = list[n];
        list[n] = value;
      }
    }

    public static Solid BooleanOperation_Intersect( Solid target1, Solid target2 )
    {
      return BooleanOperationsUtils
        .ExecuteBooleanOperation( target1, target2,
        BooleanOperationsType.Intersect );
    }

    public static Solid GetSelectedSolid( UIDocument uidoc )
    {
      ElementId id = null;

      id = uidoc.Selection.PickObject( ObjectType.Element ).ElementId;
      Element element = uidoc.Document.GetElement( id );
      Solid solid = GetElementSolid( element );
      return solid;
    }

    public static Solid GetAllSolidsUnion( Document doc )
    {
      Solid union = null;
      var elements = GetAllModelElements( doc );
      var solList = GetSolidsFromElements( elements );

      union = BooleanSolidListByUnion( solList );
      return union;
    }

    public static Solid BooleanSolidListByUnion( List<Solid> solids )
    {
      Solid union = null;
      foreach ( GeometryObject obj in solids )
      {
        Solid solid = obj as Solid;

        if ( null != solid && 0 < solid.Faces.Size )
        {
          if ( null == union )
            union = solid;
          else
          {
            union = BooleanOperationsUtils
              .ExecuteBooleanOperation( union, solid,
              BooleanOperationsType.Union );
          }
        }
      }
      return union;
    }

    public static IList<Element> GetAllModelElements( Document doc )
    {
      List<Element> elements = new List<Element>();

      FilteredElementCollector collector
        = new FilteredElementCollector( doc )
          .WhereElementIsNotElementType();

      foreach ( Element e in collector )
      {
        if ( null != e.Category && e.Category.HasMaterialQuantities )
          elements.Add( e );
      }
      return elements;
    }

    public static List<Solid> GetSolidsFromFamilySymbol(
      this IList<FamilySymbol> symbols,
      Transform elementtransform = null )
    {
      if ( symbols == null )
        return null;
      if ( elementtransform != null && !elementtransform.IsConformal )
        return null;

      Options geoOptions = new Options();
      geoOptions.ComputeReferences = true;
      geoOptions.IncludeNonVisibleObjects = false;

      List<GeometryElement> geoElemList = new List<GeometryElement>();
      List<Solid> solids = new List<Solid>();

      foreach ( FamilySymbol elem in symbols )
      {
        GeometryElement geoElem = elem.get_Geometry( geoOptions );
        geoElemList.Add( geoElem );

        if ( geoElemList != null )
        {
          foreach ( GeometryElement geomEle in geoElemList )
            geomEle.GetSolid( solids, elementtransform );
        }
      }
      return solids;
    }

    public static List<Solid> GetSolidsFromElements(
      this IList<Element> elements,
      Transform elementtransform = null )
    {
      if ( elements == null )
        return null;

      if ( elementtransform != null && !elementtransform.IsConformal )
        return null;

      Options geoOptions = new Options();
      geoOptions.ComputeReferences = true;
      geoOptions.IncludeNonVisibleObjects = false;

      List<GeometryElement> geoElemList = new List<GeometryElement>();
      List<Solid> solids = new List<Solid>();

      foreach ( Element elem in elements )
      {
        GeometryElement geoElem = elem.get_Geometry( geoOptions );
        geoElemList.Add( geoElem );

        if ( geoElemList != null )
        {
          foreach ( GeometryElement ge in geoElemList )
          {
            ge.GetSolid( solids, elementtransform );
          }
        }
      }
      return solids;
    }


    // 선택한 매스에 간접하는 솔리스객체만 리스트화하여 리턴....
    public static List<Solid> GetSolidsFromElementsByIntersection( Solid mass,
      IList<Element> elements,
      Transform elementtransform = null )
    {
      if ( elements == null )
        return null;

      if ( elementtransform != null && !elementtransform.IsConformal )
        return null;

      Options geoOptions = new Options();
      geoOptions.ComputeReferences = true;
      geoOptions.IncludeNonVisibleObjects = false;

      List<GeometryElement> geoElemList = new List<GeometryElement>();
      List<Solid> solids = new List<Solid>();

      foreach ( Element elem in elements )
      {
        GeometryElement geoElem = elem.get_Geometry( geoOptions );
        geoElemList.Add( geoElem );

        if ( geoElemList != null )
        {
          foreach ( GeometryElement ge in geoElemList )
          {
            ge.GetSolid( solids, elementtransform );
          }
        }
      }
      return solids;
    }


    public static void GetSolid(
      this GeometryElement geoElement,
      List<Solid> solids,
      Transform elementTransform )
    {
      GeometryElement geoelementtransform = geoElement;
      if ( elementTransform != null )
        geoelementtransform = geoElement.GetTransformed( elementTransform );

      if ( geoelementtransform != null )
      {
        foreach ( GeometryObject geoObj in geoelementtransform )
        {
          Solid body = geoObj as Solid;
          if ( body != null )
          {
            if ( !body.Volume.AreEqual( 0.0, 0.0001 ) && body.Faces.Size != 0 )
              solids.Add( body );
          }
          else
          {
            GeometryInstance instance = geoObj as GeometryInstance;
            if ( instance != null )
            {
              var transform = instance.Transform;
              GeometryElement instgeom = instance.GetSymbolGeometry( transform );
              instgeom.GetSolid( solids, elementTransform );
            }
          }
        }
      }
    }

    public static Solid GetElementSolid( Element e )
    {
      Options goption = new Options();
      goption.ComputeReferences = true;
      GeometryElement gelem = e.get_Geometry( goption );
      Solid resultSolid = null;
      //foreach (GeometryObject gobj in gelem.Objects)
      IEnumerator<GeometryObject> Objects = gelem.GetEnumerator();
      while ( Objects.MoveNext() )
      {
        GeometryObject gobj = Objects.Current;

        GeometryInstance gIns = gobj as GeometryInstance;
        if ( gIns != null )
        {
          GeometryElement finalGeom = gIns.GetInstanceGeometry();
          //foreach (GeometryObject gobj2 in finalGeom.Objects)
          IEnumerator<GeometryObject> Objects1 = finalGeom.GetEnumerator();
          while ( Objects1.MoveNext() )
          {
            GeometryObject gobj2 = Objects1.Current;

            Solid tSolid = gobj2 as Solid;
            if ( tSolid != null && tSolid.Faces.Size > 0 && tSolid.Volume > 0 )
            {
              resultSolid = tSolid;
              break;
            }
          }
        }
        if ( resultSolid == null )
        {
          Solid tSolid2 = gobj as Solid;
          if ( tSolid2 != null && tSolid2.Faces.Size > 0 && tSolid2.Volume > 0 )
          {
            resultSolid = tSolid2;
            break;
          }
        }
      }
      return resultSolid;
    }

    public static bool AreEqual( this double dVal1, double dVal2, double dTol )
    {
      double dValue = dVal1 - dVal2;
      if ( Math.Abs( dValue ) <= dTol )
        return true;
      return false;
    }

    public static void CreateDirectShape( Document doc, List<Solid> solidList, double volume)
    {
      #region DS Setting

      Category category = Category.GetCategory( doc, BuiltInCategory.OST_GenericModel );
      DirectShapeType directShapeType = DirectShapeType.Create( doc, "VolumeModel", category.Id );
      DirectShape ds = DirectShape.CreateElement( doc, category.Id );

      List<GeometryObject> geometryObjectList = new List<GeometryObject>();
      foreach ( var solid in solidList )
      {
        if ( ds.IsValidGeometry( solid ) )
          geometryObjectList.Add( solid );
      }
      ds.SetTypeId( directShapeType.Id );
      ds.SetName( "JK_VolumeModel" );

      // 코멘트 파라미터에 볼륨 기입
      var volumeNum = String.Format( "{0:N3} m³", volume );
      ds.get_Parameter( BuiltInParameter.ALL_MODEL_INSTANCE_COMMENTS )
        .Set( volumeNum ); 

      #endregion

      ds.SetShape( geometryObjectList );

      #region DS Color Settings
      FillPatternElement solidFill;
      solidFill = FillPatternElement
        .GetFillPatternElementByName( doc, FillPatternTarget.Drafting, "Solid fill" );

      Color greenblue = new Color( 130, 250, 220 ); // green
      Color lineColor = new Color( 0, 0, 0 ); // black
      Color navy = new Color( 035, 053, 102 ); // navy
      OverrideGraphicSettings ogs = new OverrideGraphicSettings();

      ogs.SetProjectionLineColor( lineColor );
      ogs.SetProjectionFillColor( greenblue );
      ogs.SetCutFillPatternId( solidFill.Id );
      ogs.SetProjectionFillPatternVisible( true );
      ogs.SetProjectionFillPatternId( solidFill.Id );   
      ogs.SetSurfaceTransparency( 0 ); // Transparency
      ogs.SetCutFillColor( navy );
      ogs.SetCutLineColor( lineColor );

      doc.ActiveView.SetElementOverrides( ds.Id, ogs );
      #endregion
    }
  }
}
