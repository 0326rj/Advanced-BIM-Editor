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

using Exceptions = Autodesk.Revit.Exceptions;
using MyUtils;

namespace NoahDesign.Folder_Command
{
  class CmdAdvanceFaceWall : IExternalCommand
  {
    public Result Execute( ExternalCommandData commandData, ref string message, ElementSet elements )
    {
      UIApplication uiapp = commandData.Application;
      UIDocument uidoc = uiapp.ActiveUIDocument;
      Document doc = uidoc.Document;

      try
      {


        return Result.Succeeded;
      }
      catch ( Exception e )
      {
        message = e.Message;
        return Result.Failed;  
      }
    }
  }
}
