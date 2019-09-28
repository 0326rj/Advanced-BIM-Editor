#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using NoahDesign.Folder_WinForm;
using System.Diagnostics;
using MyUtils;
#endregion

namespace NoahDesign.Cmd0_FormTest
{
  static class Everywhere
  {
    internal static Selection SelectObject( UIDocument uidoc, Document doc, BuiltInCategory bic )
    {
      var collection = new FilteredElementCollector( doc )
        .OfCategory( bic )
        .WhereElementIsNotElementType()
        .ToElementIds();

      Selection sel = uidoc.Selection;
      sel.SetElementIds( collection );
      return sel;
    }

    
  }
}
