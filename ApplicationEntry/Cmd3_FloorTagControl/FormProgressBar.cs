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
    string _format;

    public FormProgressBar( int max )
    {

      InitializeComponent();

      progressBar1.Minimum = 0;
      progressBar1.Maximum = max; 
      progressBar1.Value = 0;

      Show();
      Application.DoEvents();
    }

    public void Increment()
    {
      ++progressBar1.Value;
      Application.DoEvents();
    }

  }
}
