namespace NoahDesign.Cmd5_Test
{
  partial class Stud_Form
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
      this.components = new System.ComponentModel.Container();
      this.button_start = new System.Windows.Forms.Button();
      this.button_cancel = new System.Windows.Forms.Button();
      this.progressBar1 = new System.Windows.Forms.ProgressBar();
      this.imageList1 = new System.Windows.Forms.ImageList(this.components);
      this.tabPage2 = new System.Windows.Forms.TabPage();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.listView1 = new System.Windows.Forms.ListView();
      this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.comboBox2 = new System.Windows.Forms.ComboBox();
      this.label7 = new System.Windows.Forms.Label();
      this.textBox1 = new System.Windows.Forms.TextBox();
      this.label8 = new System.Windows.Forms.Label();
      this.label9 = new System.Windows.Forms.Label();
      this.label11 = new System.Windows.Forms.Label();
      this.label12 = new System.Windows.Forms.Label();
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tabPage2.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.tabControl1.SuspendLayout();
      this.SuspendLayout();
      // 
      // button_start
      // 
      this.button_start.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
      this.button_start.Location = new System.Drawing.Point(743, 485);
      this.button_start.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.button_start.Name = "button_start";
      this.button_start.Size = new System.Drawing.Size(108, 38);
      this.button_start.TabIndex = 1;
      this.button_start.Text = "確認";
      this.button_start.UseVisualStyleBackColor = true;
      // 
      // button_cancel
      // 
      this.button_cancel.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
      this.button_cancel.Location = new System.Drawing.Point(631, 485);
      this.button_cancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.button_cancel.Name = "button_cancel";
      this.button_cancel.Size = new System.Drawing.Size(108, 38);
      this.button_cancel.TabIndex = 1;
      this.button_cancel.Text = "キャンセル";
      this.button_cancel.UseVisualStyleBackColor = true;
      // 
      // progressBar1
      // 
      this.progressBar1.Location = new System.Drawing.Point(7, 486);
      this.progressBar1.Name = "progressBar1";
      this.progressBar1.Size = new System.Drawing.Size(618, 37);
      this.progressBar1.TabIndex = 2;
      // 
      // imageList1
      // 
      this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
      this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
      this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
      // 
      // tabPage2
      // 
      this.tabPage2.Controls.Add(this.groupBox1);
      this.tabPage2.Location = new System.Drawing.Point(4, 24);
      this.tabPage2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.tabPage2.Name = "tabPage2";
      this.tabPage2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.tabPage2.Size = new System.Drawing.Size(848, 446);
      this.tabPage2.TabIndex = 1;
      this.tabPage2.Text = "LGS入力設定";
      this.tabPage2.UseVisualStyleBackColor = true;
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.label12);
      this.groupBox1.Controls.Add(this.label11);
      this.groupBox1.Controls.Add(this.label9);
      this.groupBox1.Controls.Add(this.label8);
      this.groupBox1.Controls.Add(this.textBox1);
      this.groupBox1.Controls.Add(this.label7);
      this.groupBox1.Controls.Add(this.comboBox2);
      this.groupBox1.Controls.Add(this.listView1);
      this.groupBox1.Location = new System.Drawing.Point(11, 11);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(825, 173);
      this.groupBox1.TabIndex = 9;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "スタッド設定";
      // 
      // listView1
      // 
      this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
      this.listView1.HideSelection = false;
      this.listView1.Location = new System.Drawing.Point(209, 44);
      this.listView1.Name = "listView1";
      this.listView1.Size = new System.Drawing.Size(600, 115);
      this.listView1.TabIndex = 3;
      this.listView1.UseCompatibleStateImageBehavior = false;
      this.listView1.View = System.Windows.Forms.View.Details;
      // 
      // columnHeader1
      // 
      this.columnHeader1.Text = "種類";
      this.columnHeader1.Width = 100;
      // 
      // columnHeader2
      // 
      this.columnHeader2.Text = "記号";
      this.columnHeader2.Width = 150;
      // 
      // columnHeader3
      // 
      this.columnHeader3.Text = "A x B x t";
      this.columnHeader3.Width = 150;
      // 
      // columnHeader4
      // 
      this.columnHeader4.Text = "L (長さ)";
      this.columnHeader4.Width = 100;
      // 
      // comboBox2
      // 
      this.comboBox2.FormattingEnabled = true;
      this.comboBox2.Location = new System.Drawing.Point(8, 45);
      this.comboBox2.Name = "comboBox2";
      this.comboBox2.Size = new System.Drawing.Size(160, 23);
      this.comboBox2.TabIndex = 10;
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Location = new System.Drawing.Point(5, 27);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(79, 15);
      this.label7.TabIndex = 11;
      this.label7.Text = "入力方法設定";
      // 
      // textBox1
      // 
      this.textBox1.Location = new System.Drawing.Point(8, 88);
      this.textBox1.Name = "textBox1";
      this.textBox1.Size = new System.Drawing.Size(160, 23);
      this.textBox1.TabIndex = 12;
      this.textBox1.Text = "303";
      this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // label8
      // 
      this.label8.AutoSize = true;
      this.label8.Location = new System.Drawing.Point(172, 95);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(31, 15);
      this.label8.TabIndex = 13;
      this.label8.Text = "mm";
      // 
      // label9
      // 
      this.label9.AutoSize = true;
      this.label9.Location = new System.Drawing.Point(6, 70);
      this.label9.Name = "label9";
      this.label9.Size = new System.Drawing.Size(56, 15);
      this.label9.TabIndex = 14;
      this.label9.Text = "基本ピッチ";
      // 
      // label11
      // 
      this.label11.AutoSize = true;
      this.label11.Location = new System.Drawing.Point(6, 122);
      this.label11.Name = "label11";
      this.label11.Size = new System.Drawing.Size(0, 15);
      this.label11.TabIndex = 17;
      // 
      // label12
      // 
      this.label12.AutoSize = true;
      this.label12.Location = new System.Drawing.Point(209, 27);
      this.label12.Name = "label12";
      this.label12.Size = new System.Drawing.Size(79, 15);
      this.label12.TabIndex = 18;
      this.label12.Text = "標準規格リスト";
      // 
      // tabControl1
      // 
      this.tabControl1.Controls.Add(this.tabPage2);
      this.tabControl1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
      this.tabControl1.Location = new System.Drawing.Point(-1, 7);
      this.tabControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(856, 474);
      this.tabControl1.TabIndex = 0;
      // 
      // Stud_Form
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(855, 530);
      this.Controls.Add(this.progressBar1);
      this.Controls.Add(this.button_start);
      this.Controls.Add(this.button_cancel);
      this.Controls.Add(this.tabControl1);
      this.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "Stud_Form";
      this.ShowIcon = false;
      this.Text = "Advanced BIM Editor - LGS Maganer";
      this.tabPage2.ResumeLayout(false);
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.tabControl1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion
    private System.Windows.Forms.Button button_cancel;
    private System.Windows.Forms.Button button_start;
    private System.Windows.Forms.ProgressBar progressBar1;
    private System.Windows.Forms.ImageList imageList1;
    private System.Windows.Forms.TabPage tabPage2;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.Label label12;
    private System.Windows.Forms.Label label11;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.TextBox textBox1;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.ComboBox comboBox2;
    private System.Windows.Forms.ListView listView1;
    private System.Windows.Forms.ColumnHeader columnHeader1;
    private System.Windows.Forms.ColumnHeader columnHeader2;
    private System.Windows.Forms.ColumnHeader columnHeader3;
    private System.Windows.Forms.ColumnHeader columnHeader4;
    private System.Windows.Forms.TabControl tabControl1;
  }
}