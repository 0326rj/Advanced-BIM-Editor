#region Namespace

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

#endregion

namespace NoahDesign.Cmd2_JoinGeometry
{
  public partial class MainForm : System.Windows.Forms.Form
  {

    #region Fields

    Document doc;

    UIDocument uidoc;

    ExternalCommandData commandData; 

    #endregion

    public MainForm( Document doc, UIDocument uidoc, ExternalCommandData commandData )
    {
      this.doc = doc;
      this.uidoc = uidoc;
      this.commandData = commandData;
      
      InitializeComponent();
    }

    private void MainForm_Load( object sender, EventArgs e )
    {
      this.KeyPreview = true;
    }
    
    private void buttonVersionInfo_Click( object sender, EventArgs e )
    {
      using ( Folder_WinForm.AddInInfo formVersion = new Folder_WinForm.AddInInfo() )
        formVersion.ShowDialog();
    }

    private void buttonClose_Click( object sender, EventArgs e )
    {
      this.Dispose(); this.Close();
    }

    private void buttonClose2_Click( object sender, EventArgs e )
    {
      this.Dispose(); this.Close();
    }

    private void MainForm_KeyUp( object sender, KeyEventArgs e )
    {
      if ( e.KeyCode == Keys.Escape )
        this.Close();
    }

  }


}
