namespace NoahDesign.Folder_WinForm
{
  partial class AddInInfo
  {
    /// <summary>
    /// 필수 디자이너 변수입니다.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// 사용 중인 모든 리소스를 정리합니다.
    /// </summary>
    /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
    protected override void Dispose( bool disposing )
    {
      if ( disposing && ( components != null ) )
      {
        components.Dispose();
      }
      base.Dispose( disposing );
    }

    #region Windows Form 디자이너에서 생성한 코드

    /// <summary>
    /// 디자이너 지원에 필요한 메서드입니다. 
    /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
    /// </summary>
    private void InitializeComponent()
    {
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( AddInInfo ) );
      this.linkHome = new System.Windows.Forms.Button();
      this.buttonClose = new System.Windows.Forms.Button();
      this.textBox6 = new System.Windows.Forms.TextBox();
      this.textBox7 = new System.Windows.Forms.TextBox();
      this.linkCybozu = new System.Windows.Forms.Button();
      this.pictureBox1 = new System.Windows.Forms.PictureBox();
      this.label1 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      ( ( System.ComponentModel.ISupportInitialize )( this.pictureBox1 ) ).BeginInit();
      this.SuspendLayout();
      // 
      // linkHome
      // 
      this.linkHome.BackColor = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 78 ) ) ) ), ( ( int )( ( ( byte )( 184 ) ) ) ), ( ( int )( ( ( byte )( 206 ) ) ) ) );
      this.linkHome.Cursor = System.Windows.Forms.Cursors.Hand;
      this.linkHome.FlatAppearance.BorderSize = 0;
      this.linkHome.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.linkHome.Font = new System.Drawing.Font( "Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( ( byte )( 0 ) ) );
      this.linkHome.ForeColor = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 64 ) ) ) ), ( ( int )( ( ( byte )( 64 ) ) ) ), ( ( int )( ( ( byte )( 64 ) ) ) ) );
      this.linkHome.Location = new System.Drawing.Point( 491, 165 );
      this.linkHome.Name = "linkHome";
      this.linkHome.Size = new System.Drawing.Size( 93, 24 );
      this.linkHome.TabIndex = 5;
      this.linkHome.Text = "Noah Design";
      this.linkHome.UseVisualStyleBackColor = false;
      this.linkHome.Click += new System.EventHandler( this.Button1_Click );
      // 
      // buttonClose
      // 
      this.buttonClose.BackColor = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 78 ) ) ) ), ( ( int )( ( ( byte )( 184 ) ) ) ), ( ( int )( ( ( byte )( 206 ) ) ) ) );
      this.buttonClose.Cursor = System.Windows.Forms.Cursors.Hand;
      this.buttonClose.FlatAppearance.BorderSize = 0;
      this.buttonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.buttonClose.Font = new System.Drawing.Font( "Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( ( byte )( 0 ) ) );
      this.buttonClose.ForeColor = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 64 ) ) ) ), ( ( int )( ( ( byte )( 64 ) ) ) ), ( ( int )( ( ( byte )( 64 ) ) ) ) );
      this.buttonClose.Location = new System.Drawing.Point( 587, 165 );
      this.buttonClose.Name = "buttonClose";
      this.buttonClose.Size = new System.Drawing.Size( 93, 51 );
      this.buttonClose.TabIndex = 6;
      this.buttonClose.Text = "Close";
      this.buttonClose.UseVisualStyleBackColor = false;
      this.buttonClose.Click += new System.EventHandler( this.ButtonClose_Click );
      // 
      // textBox6
      // 
      this.textBox6.BackColor = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 34 ) ) ) ), ( ( int )( ( ( byte )( 36 ) ) ) ), ( ( int )( ( ( byte )( 49 ) ) ) ) );
      this.textBox6.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.textBox6.Font = new System.Drawing.Font( "Meiryo UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( ( byte )( 128 ) ) );
      this.textBox6.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.textBox6.Location = new System.Drawing.Point( 23, 187 );
      this.textBox6.Name = "textBox6";
      this.textBox6.Size = new System.Drawing.Size( 288, 14 );
      this.textBox6.TabIndex = 8;
      this.textBox6.Text = "E-mail : km-zbm00@pub.taisei.co.jp";
      // 
      // textBox7
      // 
      this.textBox7.BackColor = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 34 ) ) ) ), ( ( int )( ( ( byte )( 36 ) ) ) ), ( ( int )( ( ( byte )( 49 ) ) ) ) );
      this.textBox7.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.textBox7.Font = new System.Drawing.Font( "Meiryo UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( ( byte )( 128 ) ) );
      this.textBox7.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.textBox7.Location = new System.Drawing.Point( 71, 202 );
      this.textBox7.Name = "textBox7";
      this.textBox7.Size = new System.Drawing.Size( 152, 14 );
      this.textBox7.TabIndex = 10;
      this.textBox7.Text = "kim@noah-desing.jp";
      // 
      // linkCybozu
      // 
      this.linkCybozu.BackColor = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 78 ) ) ) ), ( ( int )( ( ( byte )( 184 ) ) ) ), ( ( int )( ( ( byte )( 206 ) ) ) ) );
      this.linkCybozu.Cursor = System.Windows.Forms.Cursors.Hand;
      this.linkCybozu.FlatAppearance.BorderSize = 0;
      this.linkCybozu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.linkCybozu.Font = new System.Drawing.Font( "Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( ( byte )( 0 ) ) );
      this.linkCybozu.ForeColor = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 64 ) ) ) ), ( ( int )( ( ( byte )( 64 ) ) ) ), ( ( int )( ( ( byte )( 64 ) ) ) ) );
      this.linkCybozu.Location = new System.Drawing.Point( 491, 192 );
      this.linkCybozu.Name = "linkCybozu";
      this.linkCybozu.Size = new System.Drawing.Size( 93, 24 );
      this.linkCybozu.TabIndex = 11;
      this.linkCybozu.Text = "Tutorials";
      this.linkCybozu.UseVisualStyleBackColor = false;
      this.linkCybozu.Click += new System.EventHandler( this.LinkCybozu_Click );
      // 
      // pictureBox1
      // 
      this.pictureBox1.BackgroundImage = ( ( System.Drawing.Image )( resources.GetObject( "pictureBox1.BackgroundImage" ) ) );
      this.pictureBox1.Location = new System.Drawing.Point( 22, 18 );
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new System.Drawing.Size( 63, 52 );
      this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
      this.pictureBox1.TabIndex = 12;
      this.pictureBox1.TabStop = false;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font( "Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( ( byte )( 0 ) ) );
      this.label1.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.label1.Location = new System.Drawing.Point( 89, 15 );
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size( 251, 24 );
      this.label1.TabIndex = 13;
      this.label1.Text = "ADVANCED BIM EDITOR";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Font = new System.Drawing.Font( "Meiryo UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( ( byte )( 128 ) ) );
      this.label3.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.label3.Location = new System.Drawing.Point( 20, 171 );
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size( 150, 14 );
      this.label3.TabIndex = 15;
      this.label3.Text = "Noah Design, Jaebum Kim";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Font = new System.Drawing.Font( "Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( ( byte )( 128 ) ) );
      this.label4.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.label4.Location = new System.Drawing.Point( 89, 38 );
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size( 111, 15 );
      this.label4.TabIndex = 16;
      this.label4.Text = "Release: 19.08.01";
      this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // AddInInfo
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 12F );
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 34 ) ) ) ), ( ( int )( ( ( byte )( 36 ) ) ) ), ( ( int )( ( ( byte )( 49 ) ) ) ) );
      this.ClientSize = new System.Drawing.Size( 696, 234 );
      this.ControlBox = false;
      this.Controls.Add( this.linkCybozu );
      this.Controls.Add( this.buttonClose );
      this.Controls.Add( this.linkHome );
      this.Controls.Add( this.label4 );
      this.Controls.Add( this.label1 );
      this.Controls.Add( this.pictureBox1 );
      this.Controls.Add( this.textBox7 );
      this.Controls.Add( this.textBox6 );
      this.Controls.Add( this.label3 );
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.Name = "AddInInfo";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "バージョン情報";
      ( ( System.ComponentModel.ISupportInitialize )( this.pictureBox1 ) ).EndInit();
      this.ResumeLayout( false );
      this.PerformLayout();

    }

    #endregion
    private System.Windows.Forms.Button linkHome;
    private System.Windows.Forms.TextBox textBox6;
    private System.Windows.Forms.TextBox textBox7;
    private System.Windows.Forms.Button linkCybozu;
    private System.Windows.Forms.PictureBox pictureBox1;
    public System.Windows.Forms.Button buttonClose;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label4;
  }
}

