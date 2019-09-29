using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationEntry.Cmd1_ConcreteVolume;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;

namespace NoahDesign.Cmd0_FormTest
{
  [Transaction( TransactionMode.Manual )]
  class CmdFormTest : IExternalCommand
  {
    UIApplication uiapp;
    public UIApplication Uiapp 
    { 
      get { return uiapp; } 
      set { uiapp = value; }
    }

    UIDocument uidoc;
    public UIDocument Uidoc
    {
      get { return uidoc; } 
      set { uidoc = value; }
    }

    Document doc;
    public Document Doc
    {
      get { return doc; } 
      set { doc = value; }
    }

    private static WindowHandle _hWndRevit = null;

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
      uiapp = commandData.Application;
      uidoc = uiapp.ActiveUIDocument;
      doc = uidoc.Document;

      TestForm winForm = new TestForm( uidoc, doc, commandData );      
      try
      {
        using ( Transaction trans = new Transaction(doc, "Show TestForm") )
        {
          if ( _hWndRevit != null )
          {
            winForm.Show( _hWndRevit );
          }
        }
        return Result.Succeeded;
      }
      catch ( Exception ex )
      {
        message = ex.Message;
        return Result.Cancelled;
      } 
    }
  }
}
