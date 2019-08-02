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
using System.Collections;

namespace NoahDesign.Folder_Component
{
  public class FamilyAndType
  {
    public static List<string> GetFamilyTypesInDocument( Document doc )
    {
      List<string> list = new List<string>();
      foreach ( var t in doc.FamilyManager.Types )
      {
        var str = t.ToString();
        list.Add( str );
      }
      return list;
    }

    public static List<Element> GetAllProjectElements( Document doc )
    {
      List<Element> elementList;
      FilteredElementCollector elemTypeCtor = ( new FilteredElementCollector( doc ) )
        .WhereElementIsElementType();
      FilteredElementCollector notElemTypeCtor = ( new FilteredElementCollector( doc ) )
        .WhereElementIsNotElementType();
      FilteredElementCollector allElementCtor = elemTypeCtor.UnionWith( notElemTypeCtor );
      try
      {
        elementList = !allElementCtor.Any() ? new List<Element>() : allElementCtor.ToList();
      }
      catch
      {
        elementList = new List<Element>();
      }
      return elementList;
    }
  }
}
