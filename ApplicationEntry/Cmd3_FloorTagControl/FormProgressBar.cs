using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NoahDesign.Cmd3_FloorTagControl
{
  public partial class FormProgressBar : Form
  {
    public FormProgressBar()
    {
      InitializeComponent();
    }

    private void FormProgressBar_Load( object sender, EventArgs e )
    {
      progressBar1.Style = ProgressBarStyle.Continuous;  
    }
  }
}
