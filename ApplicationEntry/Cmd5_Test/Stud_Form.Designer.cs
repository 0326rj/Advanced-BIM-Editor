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
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tabPage1 = new System.Windows.Forms.TabPage();
      this.pictureBox1 = new System.Windows.Forms.PictureBox();
      this.panel1 = new System.Windows.Forms.Panel();
      this.label5 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.textBox_offsetDown = new System.Windows.Forms.TextBox();
      this.textBox_offsetUp = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.comboBox1 = new System.Windows.Forms.ComboBox();
      this.label1 = new System.Windows.Forms.Label();
      this.tabPage2 = new System.Windows.Forms.TabPage();
      this.tabPage3 = new System.Windows.Forms.TabPage();
      this.button_start = new System.Windows.Forms.Button();
      this.button_cancel = new System.Windows.Forms.Button();
      this.button_LoadRunner = new System.Windows.Forms.Button();
      this.label6 = new System.Windows.Forms.Label();
      this.tabControl1.SuspendLayout();
      this.tabPage1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
      this.panel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // tabControl1
      // 
      this.tabControl1.Controls.Add(this.tabPage1);
      this.tabControl1.Controls.Add(this.tabPage2);
      this.tabControl1.Controls.Add(this.tabPage3);
      this.tabControl1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
      this.tabControl1.Location = new System.Drawing.Point(-2, 2);
      this.tabControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(524, 448);
      this.tabControl1.TabIndex = 0;
      // 
      // tabPage1
      // 
      this.tabPage1.Controls.Add(this.pictureBox1);
      this.tabPage1.Controls.Add(this.panel1);
      this.tabPage1.Location = new System.Drawing.Point(4, 24);
      this.tabPage1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.tabPage1.Name = "tabPage1";
      this.tabPage1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.tabPage1.Size = new System.Drawing.Size(516, 420);
      this.tabPage1.TabIndex = 0;
      this.tabPage1.Text = "ランナー入力";
      this.tabPage1.UseVisualStyleBackColor = true;
      // 
      // pictureBox1
      // 
      this.pictureBox1.Image = global::NoahDesign.Properties.Resources._03;
      this.pictureBox1.Location = new System.Drawing.Point(255, 7);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new System.Drawing.Size(254, 229);
      this.pictureBox1.TabIndex = 5;
      this.pictureBox1.TabStop = false;
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.label6);
      this.panel1.Controls.Add(this.button_LoadRunner);
      this.panel1.Controls.Add(this.label5);
      this.panel1.Controls.Add(this.label4);
      this.panel1.Controls.Add(this.textBox_offsetDown);
      this.panel1.Controls.Add(this.textBox_offsetUp);
      this.panel1.Controls.Add(this.label3);
      this.panel1.Controls.Add(this.label2);
      this.panel1.Controls.Add(this.comboBox1);
      this.panel1.Controls.Add(this.label1);
      this.panel1.Location = new System.Drawing.Point(8, 7);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(241, 401);
      this.panel1.TabIndex = 4;
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(197, 206);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(31, 15);
      this.label5.TabIndex = 9;
      this.label5.Text = "mm";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(197, 153);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(31, 15);
      this.label4.TabIndex = 8;
      this.label4.Text = "mm";
      // 
      // textBox_offsetDown
      // 
      this.textBox_offsetDown.Location = new System.Drawing.Point(11, 203);
      this.textBox_offsetDown.Name = "textBox_offsetDown";
      this.textBox_offsetDown.Size = new System.Drawing.Size(186, 23);
      this.textBox_offsetDown.TabIndex = 7;
      // 
      // textBox_offsetUp
      // 
      this.textBox_offsetUp.Location = new System.Drawing.Point(10, 150);
      this.textBox_offsetUp.Name = "textBox_offsetUp";
      this.textBox_offsetUp.Size = new System.Drawing.Size(186, 23);
      this.textBox_offsetUp.TabIndex = 6;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(10, 185);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(141, 15);
      this.label3.TabIndex = 5;
      this.label3.Text = "下部拘束からのOFFSET値";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(10, 132);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(141, 15);
      this.label2.TabIndex = 4;
      this.label2.Text = "上部拘束からのOFFSET値";
      // 
      // comboBox1
      // 
      this.comboBox1.FormattingEnabled = true;
      this.comboBox1.Location = new System.Drawing.Point(10, 98);
      this.comboBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.comboBox1.Name = "comboBox1";
      this.comboBox1.Size = new System.Drawing.Size(186, 23);
      this.comboBox1.TabIndex = 2;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(8, 79);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(102, 15);
      this.label1.TabIndex = 3;
      this.label1.Text = "ランナーTYPE 選択";
      // 
      // tabPage2
      // 
      this.tabPage2.Location = new System.Drawing.Point(4, 24);
      this.tabPage2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.tabPage2.Name = "tabPage2";
      this.tabPage2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.tabPage2.Size = new System.Drawing.Size(516, 420);
      this.tabPage2.TabIndex = 1;
      this.tabPage2.Text = "スタッド入力";
      this.tabPage2.UseVisualStyleBackColor = true;
      // 
      // tabPage3
      // 
      this.tabPage3.Location = new System.Drawing.Point(4, 24);
      this.tabPage3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.tabPage3.Name = "tabPage3";
      this.tabPage3.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.tabPage3.Size = new System.Drawing.Size(516, 420);
      this.tabPage3.TabIndex = 2;
      this.tabPage3.Text = "振れ止め入力";
      this.tabPage3.UseVisualStyleBackColor = true;
      // 
      // button_start
      // 
      this.button_start.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
      this.button_start.Location = new System.Drawing.Point(289, 454);
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
      this.button_cancel.Location = new System.Drawing.Point(403, 454);
      this.button_cancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.button_cancel.Name = "button_cancel";
      this.button_cancel.Size = new System.Drawing.Size(108, 38);
      this.button_cancel.TabIndex = 1;
      this.button_cancel.Text = "キャンセル";
      this.button_cancel.UseVisualStyleBackColor = true;
      // 
      // button_LoadRunner
      // 
      this.button_LoadRunner.Location = new System.Drawing.Point(10, 36);
      this.button_LoadRunner.Name = "button_LoadRunner";
      this.button_LoadRunner.Size = new System.Drawing.Size(140, 30);
      this.button_LoadRunner.TabIndex = 10;
      this.button_LoadRunner.Text = "ファミリロード";
      this.button_LoadRunner.UseVisualStyleBackColor = true;
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(10, 18);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(155, 15);
      this.label6.TabIndex = 11;
      this.label6.Text = "ファミリがロードされていない場合";
      // 
      // Stud_Form
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(522, 499);
      this.Controls.Add(this.button_cancel);
      this.Controls.Add(this.tabControl1);
      this.Controls.Add(this.button_start);
      this.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "Stud_Form";
      this.ShowIcon = false;
      this.Text = "JK 壁下地ゼネレーター(Test Version)";
      this.tabControl1.ResumeLayout(false);
      this.tabPage1.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tabPage1;
    private System.Windows.Forms.TabPage tabPage2;
    private System.Windows.Forms.TabPage tabPage3;
    private System.Windows.Forms.Button button_cancel;
    private System.Windows.Forms.PictureBox pictureBox1;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.TextBox textBox_offsetDown;
    private System.Windows.Forms.TextBox textBox_offsetUp;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.ComboBox comboBox1;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button button_start;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Button button_LoadRunner;
  }
}