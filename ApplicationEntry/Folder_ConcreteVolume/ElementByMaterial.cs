using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.DB;

namespace NoahDesign.Folder_ConcreteVolume
{
  class ElementByMaterial
  {
    private readonly Element _element;
    private readonly Material _materials;

    public ElementByMaterial(Element element, Material material)
    {
      if ( element == null ) throw new ArgumentNullException( "element" );
      if ( material == null ) throw new ArgumentNullException( "material" );

      _element = element;
      _materials = material;
    }

    public Material Material
    {
      get { return _materials; }
    }
    public Element Element
    {
      get { return _element; }
    }


    FilteredElementCollector FilterForMaterials(Document doc )
    {
      return new FilteredElementCollector( doc )
        .OfClass( typeof( Material ) );
    }

    static string FaceMaterialName(Document doc, Face face )
    {
      ElementId id = face.MaterialElementId;
      Material m = doc.GetElement( id ) as Material;
      return m.Name;
    }


    public Material GetMaterial(Document doc, FamilyInstance fi )
    {
      Material material = null;

      foreach ( Parameter p in fi.Parameters )
      {
        var def = p.Definition;

        if ( p.StorageType == StorageType.ElementId 
          && def.ParameterGroup == BuiltInParameterGroup.PG_MATERIALS
          && def.ParameterType == ParameterType.Material)
        {
          ElementId matrialId = p.AsElementId();

          if ( -1 == matrialId.IntegerValue )
          {
            if ( null != fi.Category )
            {
              material = fi.Category.Material;

              if ( null == material )
              {
                ElementId id = Material.Create( doc, "GoodConditionMat" );
                Material mat = doc.GetElement( id ) as Material;

                mat.Color = new Color( 255, 0, 0 );
                fi.Category.Material = mat;
                material = fi.Category.Material;
              }
            }
          }

          break;
        }
      }
      return material;
    }

  }
}
