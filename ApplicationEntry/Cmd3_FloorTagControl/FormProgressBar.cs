﻿using System;
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
    private int _max;

    public FormProgressBar( int max )
    {
      InitializeComponent();

      _max = max;

      progressBar1.Minimum = 0;
      progressBar1.Maximum = max; 
      progressBar1.Value = 0;

      Show();
      Application.DoEvents();
    }

    public void Increment()
    {
      for ( int i = 0; i < _max; i++ )
      {
        progressBar1.PerformStep();
      }
    }
  }
}
