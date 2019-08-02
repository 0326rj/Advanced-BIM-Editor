using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using NoahDesign.Folder_WinForm;

namespace NoahDesign.Folder_Command
{
  [Transaction( TransactionMode.Manual )]
  public class ShowVersionInfo : IExternalCommand
  {
    public Result Execute(
        ExternalCommandData commandData,
        ref string message,
        ElementSet elements )
    {
      using ( AddInInfo addinInfo = new AddInInfo() )
      {
        addinInfo.ShowDialog();

        if ( addinInfo.DialogResult == System.Windows.Forms.DialogResult.Cancel )
          return Result.Cancelled;
      }
      return Result.Succeeded;
    }
  }
}
