namespace NoahDesign.Cmd2_ConcreteFormwork
{
  partial class FormConcreteFormwork
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
      this.listView1 = new System.Windows.Forms.ListView();
      this.familyNames = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.typeNames = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.elementIdsColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.ButtonClose = new System.Windows.Forms.Button();
      this.buttonOk = new System.Windows.Forms.Button();
      this.button2 = new System.Windows.Forms.Button();
      this.button1 = new System.Windows.Forms.Button();
      this.listCount = new System.Windows.Forms.Label();
      this.buttonTutorial = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // listView1
      // 
      this.listView1.CheckBoxes = true;
      this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.familyNames,
            this.typeNames,
            this.elementIdsColumn});
      this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
      this.listView1.HideSelection = false;
      this.listView1.HoverSelection = true;
      this.listView1.LabelWrap = false;
      this.listView1.Location = new System.Drawing.Point(12, 54);
      this.listView1.Name = "listView1";
      this.listView1.Size = new System.Drawing.Size(505, 514);
      this.listView1.TabIndex = 10;
      this.listView1.UseCompatibleStateImageBehavior = false;
      this.listView1.View = System.Windows.Forms.View.Details;
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
      // ButtonClose
      // 
      this.ButtonClose.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
      this.ButtonClose.Location = new System.Drawing.Point(424, 574);
      this.ButtonClose.Name = "ButtonClose";
      this.ButtonClose.Size = new System.Drawing.Size(93, 41);
      this.ButtonClose.TabIndex = 15;
      this.ButtonClose.Text = "キャンセル";
      this.ButtonClose.UseVisualStyleBackColor = true;
      this.ButtonClose.Click += new System.EventHandler(this.ButtonClose_Click);
      // 
      // buttonOk
      // 
      this.buttonOk.BackColor = System.Drawing.Color.PaleTurquoise;
      this.buttonOk.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
      this.buttonOk.Location = new System.Drawing.Point(325, 574);
      this.buttonOk.Name = "buttonOk";
      this.buttonOk.Size = new System.Drawing.Size(93, 41);
      this.buttonOk.TabIndex = 14;
      this.buttonOk.Text = "モデル / 集計表 作成";
      this.buttonOk.UseVisualStyleBackColor = false;
      this.buttonOk.Click += new System.EventHandler(this.ButtonOk_Click);
      // 
      // button2
      // 
      this.button2.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
      this.button2.Location = new System.Drawing.Point(259, 574);
      this.button2.Name = "button2";
      this.button2.Size = new System.Drawing.Size(60, 41);
      this.button2.TabIndex = 13;
      this.button2.Text = "全解除";
      this.button2.UseVisualStyleBackColor = true;
      this.button2.Click += new System.EventHandler(this.Button2_Click);
      // 
      // button1
      // 
      this.button1.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
      this.button1.Location = new System.Drawing.Point(193, 574);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(60, 41);
      this.button1.TabIndex = 12;
      this.button1.Text = "全選択";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.Button1_Click);
      // 
      // listCount
      // 
      this.listCount.AutoSize = true;
      this.listCount.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
      this.listCount.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.listCount.Location = new System.Drawing.Point(12, 574);
      this.listCount.Name = "listCount";
      this.listCount.Size = new System.Drawing.Size(91, 15);
      this.listCount.TabIndex = 16;
      this.listCount.Text = "項目数 : 99999";
      this.listCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // buttonTutorial
      // 
      this.buttonTutorial.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
      this.buttonTutorial.Location = new System.Drawing.Point(424, 9);
      this.buttonTutorial.Name = "buttonTutorial";
      this.buttonTutorial.Size = new System.Drawing.Size(93, 27);
      this.buttonTutorial.TabIndex = 17;
      this.buttonTutorial.Text = "使い方";
      this.buttonTutorial.UseVisualStyleBackColor = true;
      this.buttonTutorial.Click += new System.EventHandler(this.ButtonTutorial_Click);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
      this.label1.Location = new System.Drawing.Point(11, 9);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(173, 15);
      this.label1.TabIndex = 18;
      this.label1.Text = "型枠の対象部材を選択してください";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
      this.label2.ForeColor = System.Drawing.Color.Black;
      this.label2.Location = new System.Drawing.Point(11, 28);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(303, 15);
      this.label2.TabIndex = 19;
      this.label2.Text = "対象カテゴリ : 壁 ・ 床 ・ 構造柱 ・ 構造フレーム ・ 構造基礎";
      // 
      // FormConcreteFormwork
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(529, 624);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.buttonTutorial);
      this.Controls.Add(this.listCount);
      this.Controls.Add(this.ButtonClose);
      this.Controls.Add(this.buttonOk);
      this.Controls.Add(this.button2);
      this.Controls.Add(this.button1);
      this.Controls.Add(this.listView1);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "FormConcreteFormwork";
      this.ShowIcon = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "JK コンクリート型枠数量";
      this.Load += new System.EventHandler(this.FormConcreteFormwork_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ListView listView1;
    private System.Windows.Forms.ColumnHeader familyNames;
    private System.Windows.Forms.ColumnHeader typeNames;
    private System.Windows.Forms.ColumnHeader elementIdsColumn;
    private System.Windows.Forms.Button ButtonClose;
    private System.Windows.Forms.Button buttonOk;
    private System.Windows.Forms.Button button2;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Label listCount;
    private System.Windows.Forms.Button buttonTutorial;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
  }
}