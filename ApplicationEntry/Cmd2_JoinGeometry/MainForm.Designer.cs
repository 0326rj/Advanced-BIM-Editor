namespace NoahDesign.Cmd2_JoinGeometry
{
  partial class MainForm
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
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.radioButton2 = new System.Windows.Forms.RadioButton();
      this.radioButton1 = new System.Windows.Forms.RadioButton();
      this.buttonClose = new System.Windows.Forms.Button();
      this.buttonStartJoin = new System.Windows.Forms.Button();
      this.progressBar1 = new System.Windows.Forms.ProgressBar();
      this.labelProgressBar = new System.Windows.Forms.Label();
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tabPage1 = new System.Windows.Forms.TabPage();
      this.panel1 = new System.Windows.Forms.Panel();
      this.buttonVersionInfo = new System.Windows.Forms.Button();
      this.tabPage2 = new System.Windows.Forms.TabPage();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.radioButton3 = new System.Windows.Forms.RadioButton();
      this.radioButton4 = new System.Windows.Forms.RadioButton();
      this.panel2 = new System.Windows.Forms.Panel();
      this.buttonClose2 = new System.Windows.Forms.Button();
      this.button2 = new System.Windows.Forms.Button();
      this.progressBar2 = new System.Windows.Forms.ProgressBar();
      this.label1 = new System.Windows.Forms.Label();
      this.groupBox1.SuspendLayout();
      this.tabControl1.SuspendLayout();
      this.tabPage1.SuspendLayout();
      this.panel1.SuspendLayout();
      this.tabPage2.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.panel2.SuspendLayout();
      this.SuspendLayout();
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.radioButton2);
      this.groupBox1.Controls.Add(this.radioButton1);
      this.groupBox1.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.groupBox1.Location = new System.Drawing.Point(6, 6);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(574, 91);
      this.groupBox1.TabIndex = 0;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "結合項目選択";
      // 
      // radioButton2
      // 
      this.radioButton2.AutoSize = true;
      this.radioButton2.Location = new System.Drawing.Point(9, 47);
      this.radioButton2.Name = "radioButton2";
      this.radioButton2.Size = new System.Drawing.Size(156, 19);
      this.radioButton2.TabIndex = 2;
      this.radioButton2.TabStop = true;
      this.radioButton2.Text = "壁(意匠, 構造)インスタンス";
      this.radioButton2.UseVisualStyleBackColor = true;
      // 
      // radioButton1
      // 
      this.radioButton1.AutoSize = true;
      this.radioButton1.Location = new System.Drawing.Point(9, 22);
      this.radioButton1.Name = "radioButton1";
      this.radioButton1.Size = new System.Drawing.Size(126, 19);
      this.radioButton1.TabIndex = 1;
      this.radioButton1.TabStop = true;
      this.radioButton1.Text = "床(スラブ)インスタンス";
      this.radioButton1.UseVisualStyleBackColor = true;
      // 
      // buttonClose
      // 
      this.buttonClose.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
      this.buttonClose.Location = new System.Drawing.Point(447, 51);
      this.buttonClose.Name = "buttonClose";
      this.buttonClose.Size = new System.Drawing.Size(130, 33);
      this.buttonClose.TabIndex = 1;
      this.buttonClose.Text = "閉じる";
      this.buttonClose.UseVisualStyleBackColor = true;
      this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
      // 
      // buttonStartJoin
      // 
      this.buttonStartJoin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(36)))), ((int)(((byte)(49)))));
      this.buttonStartJoin.FlatAppearance.BorderSize = 0;
      this.buttonStartJoin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.buttonStartJoin.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
      this.buttonStartJoin.ForeColor = System.Drawing.SystemColors.ControlLight;
      this.buttonStartJoin.Location = new System.Drawing.Point(4, 51);
      this.buttonStartJoin.Name = "buttonStartJoin";
      this.buttonStartJoin.Size = new System.Drawing.Size(301, 33);
      this.buttonStartJoin.TabIndex = 2;
      this.buttonStartJoin.Text = "結合開始";
      this.buttonStartJoin.UseVisualStyleBackColor = false;
      // 
      // progressBar1
      // 
      this.progressBar1.Location = new System.Drawing.Point(4, 22);
      this.progressBar1.Name = "progressBar1";
      this.progressBar1.Size = new System.Drawing.Size(573, 21);
      this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
      this.progressBar1.TabIndex = 3;
      // 
      // labelProgressBar
      // 
      this.labelProgressBar.AutoSize = true;
      this.labelProgressBar.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.labelProgressBar.Location = new System.Drawing.Point(11, 5);
      this.labelProgressBar.Name = "labelProgressBar";
      this.labelProgressBar.Size = new System.Drawing.Size(55, 15);
      this.labelProgressBar.TabIndex = 4;
      this.labelProgressBar.Text = "50 / 500";
      // 
      // tabControl1
      // 
      this.tabControl1.Controls.Add(this.tabPage1);
      this.tabControl1.Controls.Add(this.tabPage2);
      this.tabControl1.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tabControl1.Location = new System.Drawing.Point(-1, 5);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(598, 234);
      this.tabControl1.TabIndex = 5;
      // 
      // tabPage1
      // 
      this.tabPage1.Controls.Add(this.panel1);
      this.tabPage1.Controls.Add(this.groupBox1);
      this.tabPage1.Location = new System.Drawing.Point(4, 24);
      this.tabPage1.Name = "tabPage1";
      this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage1.Size = new System.Drawing.Size(590, 206);
      this.tabPage1.TabIndex = 0;
      this.tabPage1.Text = "結合";
      this.tabPage1.UseVisualStyleBackColor = true;
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.buttonVersionInfo);
      this.panel1.Controls.Add(this.buttonClose);
      this.panel1.Controls.Add(this.buttonStartJoin);
      this.panel1.Controls.Add(this.progressBar1);
      this.panel1.Controls.Add(this.labelProgressBar);
      this.panel1.Location = new System.Drawing.Point(3, 113);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(580, 89);
      this.panel1.TabIndex = 6;
      // 
      // buttonVersionInfo
      // 
      this.buttonVersionInfo.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
      this.buttonVersionInfo.Location = new System.Drawing.Point(311, 51);
      this.buttonVersionInfo.Name = "buttonVersionInfo";
      this.buttonVersionInfo.Size = new System.Drawing.Size(130, 33);
      this.buttonVersionInfo.TabIndex = 6;
      this.buttonVersionInfo.Text = "バージョン情報";
      this.buttonVersionInfo.UseVisualStyleBackColor = true;
      this.buttonVersionInfo.Click += new System.EventHandler(this.buttonVersionInfo_Click);
      // 
      // tabPage2
      // 
      this.tabPage2.Controls.Add(this.groupBox2);
      this.tabPage2.Controls.Add(this.panel2);
      this.tabPage2.Location = new System.Drawing.Point(4, 24);
      this.tabPage2.Name = "tabPage2";
      this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage2.Size = new System.Drawing.Size(590, 206);
      this.tabPage2.TabIndex = 1;
      this.tabPage2.Text = "結合解除";
      this.tabPage2.UseVisualStyleBackColor = true;
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this.radioButton3);
      this.groupBox2.Controls.Add(this.radioButton4);
      this.groupBox2.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.groupBox2.Location = new System.Drawing.Point(6, 6);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(574, 91);
      this.groupBox2.TabIndex = 8;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "結合項目選択";
      // 
      // radioButton3
      // 
      this.radioButton3.AutoSize = true;
      this.radioButton3.Location = new System.Drawing.Point(9, 47);
      this.radioButton3.Name = "radioButton3";
      this.radioButton3.Size = new System.Drawing.Size(156, 19);
      this.radioButton3.TabIndex = 2;
      this.radioButton3.TabStop = true;
      this.radioButton3.Text = "壁(意匠, 構造)インスタンス";
      this.radioButton3.UseVisualStyleBackColor = true;
      // 
      // radioButton4
      // 
      this.radioButton4.AutoSize = true;
      this.radioButton4.Location = new System.Drawing.Point(9, 22);
      this.radioButton4.Name = "radioButton4";
      this.radioButton4.Size = new System.Drawing.Size(126, 19);
      this.radioButton4.TabIndex = 1;
      this.radioButton4.TabStop = true;
      this.radioButton4.Text = "床(スラブ)インスタンス";
      this.radioButton4.UseVisualStyleBackColor = true;
      // 
      // panel2
      // 
      this.panel2.Controls.Add(this.buttonClose2);
      this.panel2.Controls.Add(this.button2);
      this.panel2.Controls.Add(this.progressBar2);
      this.panel2.Controls.Add(this.label1);
      this.panel2.Location = new System.Drawing.Point(3, 113);
      this.panel2.Name = "panel2";
      this.panel2.Size = new System.Drawing.Size(580, 89);
      this.panel2.TabIndex = 7;
      // 
      // buttonClose2
      // 
      this.buttonClose2.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
      this.buttonClose2.Location = new System.Drawing.Point(447, 51);
      this.buttonClose2.Name = "buttonClose2";
      this.buttonClose2.Size = new System.Drawing.Size(130, 33);
      this.buttonClose2.TabIndex = 1;
      this.buttonClose2.Text = "閉じる";
      this.buttonClose2.UseVisualStyleBackColor = true;
      this.buttonClose2.Click += new System.EventHandler(this.buttonClose2_Click);
      // 
      // button2
      // 
      this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(36)))), ((int)(((byte)(49)))));
      this.button2.FlatAppearance.BorderSize = 0;
      this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.button2.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
      this.button2.ForeColor = System.Drawing.SystemColors.ControlLight;
      this.button2.Location = new System.Drawing.Point(4, 51);
      this.button2.Name = "button2";
      this.button2.Size = new System.Drawing.Size(301, 33);
      this.button2.TabIndex = 2;
      this.button2.Text = "解除開始";
      this.button2.UseVisualStyleBackColor = false;
      // 
      // progressBar2
      // 
      this.progressBar2.Location = new System.Drawing.Point(4, 22);
      this.progressBar2.Name = "progressBar2";
      this.progressBar2.Size = new System.Drawing.Size(573, 21);
      this.progressBar2.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
      this.progressBar2.TabIndex = 3;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.Location = new System.Drawing.Point(11, 5);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(55, 15);
      this.label1.TabIndex = 4;
      this.label1.Text = "50 / 500";
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
      this.ClientSize = new System.Drawing.Size(593, 233);
      this.Controls.Add(this.tabControl1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.MaximizeBox = false;
      this.Name = "MainForm";
      this.RightToLeft = System.Windows.Forms.RightToLeft.No;
      this.ShowIcon = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "ジオメトリ結合 / 解除 Utility_V1_By キムジェボム";
      this.Load += new System.EventHandler(this.MainForm_Load);
      this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyUp);
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.tabControl1.ResumeLayout(false);
      this.tabPage1.ResumeLayout(false);
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.tabPage2.ResumeLayout(false);
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      this.panel2.ResumeLayout(false);
      this.panel2.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.RadioButton radioButton2;
    private System.Windows.Forms.RadioButton radioButton1;
    private System.Windows.Forms.Button buttonClose;
    private System.Windows.Forms.Button buttonStartJoin;
    private System.Windows.Forms.ProgressBar progressBar1;
    private System.Windows.Forms.Label labelProgressBar;
    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tabPage1;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.TabPage tabPage2;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.RadioButton radioButton3;
    private System.Windows.Forms.RadioButton radioButton4;
    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.Button buttonClose2;
    private System.Windows.Forms.Button button2;
    private System.Windows.Forms.ProgressBar progressBar2;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button buttonVersionInfo;
  }
}