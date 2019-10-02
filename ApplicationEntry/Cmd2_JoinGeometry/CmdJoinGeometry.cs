using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using MyUtils;

namespace NoahDesign.Cmd2_JoinGeometry
{
  [Transaction( TransactionMode.Manual )]
  class CmdJoinGeometry : IExternalCommand
  {

    #region Fields

    UIApplication uiapp;

    Application app;

    UIDocument uidoc;

    Document doc;

    

    #endregion

    private static  WindowHandle _hWndRevit = null;

    private void SetHandle()
    {
      if ( null == _hWndRevit )
      {
        Process process = Process.GetCurrentProcess();
        IntPtr h = process.MainWindowHandle;
        _hWndRevit = new WindowHandle( h );
      }
    }

    public Result Execute( ExternalCommandData commandData, ref string message, ElementSet elements )
    {
      SetHandle();
      this.uiapp = commandData.Application;
      this.app = commandData.Application.Application;
      this.uidoc = commandData.Application.ActiveUIDocument;
      this.doc = commandData.Application.ActiveUIDocument.Document;

      MainForm mainForm = new MainForm( doc, uidoc, commandData );

      try
      {
        using ( Transaction trans = new Transaction( doc, "tx" ) )
        {
          trans.Start();

          if ( _hWndRevit != null )
          {
            mainForm.Show( _hWndRevit );
          }
          
          trans.Commit();
        }

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
