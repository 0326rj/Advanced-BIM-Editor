﻿namespace NoahDesign.Cmd3_FloorTagControl
{
  partial class FormProgressBar
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose( bool disposing )
    {
      if ( disposing && ( components != null ) )
      {
        components.Dispose();
      }
      base.Dispose( disposing );
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.progressBar1 = new System.Windows.Forms.ProgressBar();
      this.SuspendLayout();
      // 
      // progressBar1
      // 
      this.progressBar1.Location = new System.Drawing.Point(12, 12);
      this.progressBar1.Name = "progressBar1";
      this.progressBar1.Size = new System.Drawing.Size(440, 34);
      this.progressBar1.Step = 1;
      this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
      this.progressBar1.TabIndex = 0;
      // 
      // FormProgressBar
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(464, 61);
      this.Controls.Add(this.progressBar1);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "FormProgressBar";
      this.ShowIcon = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "適用しています...";
      this.TopMost = true;
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ProgressBar progressBar1;
  }
}