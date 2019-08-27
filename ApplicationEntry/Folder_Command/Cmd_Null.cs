using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;

namespace NoahDesign.Folder_Command
{
  [Transaction(TransactionMode.Manual)]
  class Cmd_Null : IExternalCommand
  {
    public Result Execute( ExternalCommandData commandData, ref string message, ElementSet elements )
    {
      try
      {


        return Result.Succeeded;
      }
      catch ( Exception ex )
      {
        message = ex.Message;
        return Result.Failed;
      }
    }
  }
}
