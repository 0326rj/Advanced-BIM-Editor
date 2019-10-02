namespace ApplicationEntry.Cmd1_ConcreteVolume
{
  partial class FormConcreteVolume
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
      this.button1 = new System.Windows.Forms.Button();
      this.button2 = new System.Windows.Forms.Button();
      this.listCount = new System.Windows.Forms.Label();
      this.listView1 = new System.Windows.Forms.ListView();
      this.familyNames = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.typeNames = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.elementIdsColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.button3 = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.buttonStart = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // button1
      // 
      this.button1.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
      this.button1.Location = new System.Drawing.Point(132, 720);
      this.button1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(70, 32);
      this.button1.TabIndex = 2;
      this.button1.Text = "全選択";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.Button1_Click);
      // 
      // button2
      // 
      this.button2.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
      this.button2.Location = new System.Drawing.Point(209, 720);
      this.button2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.button2.Name = "button2";
      this.button2.Size = new System.Drawing.Size(70, 32);
      this.button2.TabIndex = 3;
      this.button2.Text = "全解除";
      this.button2.UseVisualStyleBackColor = true;
      this.button2.Click += new System.EventHandler(this.Button2_Click);
      // 
      // listCount
      // 
      this.listCount.AutoSize = true;
      this.listCount.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
      this.listCount.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.listCount.Location = new System.Drawing.Point(12, 723);
      this.listCount.Name = "listCount";
      this.listCount.Size = new System.Drawing.Size(91, 15);
      this.listCount.TabIndex = 8;
      this.listCount.Text = "項目数 : 99999";
      this.listCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // listView1
      // 
      this.listView1.CheckBoxes = true;
      this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.familyNames,
            this.typeNames,
            this.elementIdsColumn});
      this.listView1.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
      this.listView1.HideSelection = false;
      this.listView1.LabelWrap = false;
      this.listView1.Location = new System.Drawing.Point(14, 49);
      this.listView1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.listView1.Name = "listView1";
      this.listView1.Size = new System.Drawing.Size(495, 663);
      this.listView1.TabIndex = 9;
      this.listView1.UseCompatibleStateImageBehavior = false;
      this.listView1.View = System.Windows.Forms.View.Details;
      this.listView1.SelectedIndexChanged += new System.EventHandler(this.ListView1_SelectedIndexChanged);
      // 
      // familyNames
      // 
      this.familyNames.Text = "カテゴリ名";
      this.familyNames.Width = 186;
      // 
      // typeNames
      // 
      this.typeNames.Text = "タイプ名";
      this.typeNames.Width = 150;
      // 
      // elementIdsColumn
      // 
      this.elementIdsColumn.Text = "要素 ID";
      this.elementIdsColumn.Width = 100;
      // 
      // button3
      // 
      this.button3.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
      this.button3.Location = new System.Drawing.Point(401, 720);
      this.button3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.button3.Name = "button3";
      this.button3.Size = new System.Drawing.Size(108, 32);
      this.button3.TabIndex = 11;
      this.button3.Text = "閉じる";
      this.button3.UseVisualStyleBackColor = true;
      this.button3.Click += new System.EventHandler(this.Button3_Click_1);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
      this.label1.Location = new System.Drawing.Point(15, 9);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(179, 15);
      this.label1.TabIndex = 1;
      this.label1.Text = "体積を求める部材を選択してください";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
      this.label2.ForeColor = System.Drawing.Color.Black;
      this.label2.Location = new System.Drawing.Point(15, 28);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(303, 15);
      this.label2.TabIndex = 13;
      this.label2.Text = "対象カテゴリ : 壁 ・ 床 ・ 構造柱 ・ 構造フレーム ・ 構造基礎";
      // 
      // buttonStart
      // 
      this.buttonStart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(36)))), ((int)(((byte)(49)))));
      this.buttonStart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
      this.buttonStart.FlatAppearance.BorderSize = 0;
      this.buttonStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.buttonStart.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
      this.buttonStart.ForeColor = System.Drawing.SystemColors.ControlLight;
      this.buttonStart.Location = new System.Drawing.Point(286, 720);
      this.buttonStart.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.buttonStart.Name = "buttonStart";
      this.buttonStart.Size = new System.Drawing.Size(108, 32);
      this.buttonStart.TabIndex = 14;
      this.buttonStart.Text = "モデル作成";
      this.buttonStart.UseVisualStyleBackColor = false;
      this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
      // 
      // FormConcreteVolume
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(524, 760);
      this.Controls.Add(this.buttonStart);
      this.Controls.Add(this.listView1);
      this.Controls.Add(this.button3);
      this.Controls.Add(this.listCount);
      this.Controls.Add(this.button2);
      this.Controls.Add(this.button1);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.label2);
      this.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.MaximizeBox = false;
      this.Name = "FormConcreteVolume";
      this.ShowIcon = false;
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "体積計算 Utility_V1_By キムジェボム";
      this.Load += new System.EventHandler(this.FormConcreteVolume_Load);
      this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FormConcreteVolume_KeyUp);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Button button2;
    private System.Windows.Forms.Label listCount;
    private System.Windows.Forms.ListView listView1;
    private System.Windows.Forms.ColumnHeader familyNames;
    private System.Windows.Forms.ColumnHeader typeNames;
    private System.Windows.Forms.Button button3;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.ColumnHeader elementIdsColumn;
    private System.Windows.Forms.Button buttonStart;
  }
}