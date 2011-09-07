namespace GenerateSVNFilesForSQL2005
{
 partial class Form1
 {
  /// <summary>
  /// Required designer variable.
  /// </summary>
  private System.ComponentModel.IContainer components = null;

  /// <summary>
  /// Clean up any resources being used.
  /// </summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
   if (disposing && (components != null))
   {
    components.Dispose();
   }
   base.Dispose(disposing);
  }

  #region Windows Form Designer generated code

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
   System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
   this.label1 = new System.Windows.Forms.Label();
   this.serverName = new System.Windows.Forms.TextBox();
   this.login = new System.Windows.Forms.TextBox();
   this.label2 = new System.Windows.Forms.Label();
   this.password = new System.Windows.Forms.TextBox();
   this.label3 = new System.Windows.Forms.Label();
   this.database = new System.Windows.Forms.TextBox();
   this.label4 = new System.Windows.Forms.Label();
   this.svnDirectory = new System.Windows.Forms.TextBox();
   this.label5 = new System.Windows.Forms.Label();
   this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
   this.button1 = new System.Windows.Forms.Button();
   this.statusStrip1 = new System.Windows.Forms.StatusStrip();
   this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
   this.imgLogo = new System.Windows.Forms.PictureBox();
   this.statusStrip1.SuspendLayout();
   ((System.ComponentModel.ISupportInitialize)(this.imgLogo)).BeginInit();
   this.SuspendLayout();
   // 
   // label1
   // 
   this.label1.AutoSize = true;
   this.label1.Location = new System.Drawing.Point(21, 24);
   this.label1.Name = "label1";
   this.label1.Size = new System.Drawing.Size(72, 13);
   this.label1.TabIndex = 0;
   this.label1.Text = "Server Name:";
   // 
   // serverName
   // 
   this.serverName.Location = new System.Drawing.Point(99, 21);
   this.serverName.Name = "serverName";
   this.serverName.Size = new System.Drawing.Size(331, 20);
   this.serverName.TabIndex = 1;
   // 
   // login
   // 
   this.login.Location = new System.Drawing.Point(99, 47);
   this.login.Name = "login";
   this.login.Size = new System.Drawing.Size(331, 20);
   this.login.TabIndex = 3;
   // 
   // label2
   // 
   this.label2.AutoSize = true;
   this.label2.Location = new System.Drawing.Point(21, 50);
   this.label2.Name = "label2";
   this.label2.Size = new System.Drawing.Size(36, 13);
   this.label2.TabIndex = 2;
   this.label2.Text = "Login:";
   // 
   // password
   // 
   this.password.Location = new System.Drawing.Point(99, 73);
   this.password.Name = "password";
   this.password.Size = new System.Drawing.Size(331, 20);
   this.password.TabIndex = 5;
   // 
   // label3
   // 
   this.label3.AutoSize = true;
   this.label3.Location = new System.Drawing.Point(21, 76);
   this.label3.Name = "label3";
   this.label3.Size = new System.Drawing.Size(56, 13);
   this.label3.TabIndex = 4;
   this.label3.Text = "Password:";
   // 
   // database
   // 
   this.database.Location = new System.Drawing.Point(99, 99);
   this.database.Name = "database";
   this.database.Size = new System.Drawing.Size(331, 20);
   this.database.TabIndex = 7;
   // 
   // label4
   // 
   this.label4.AutoSize = true;
   this.label4.Location = new System.Drawing.Point(21, 102);
   this.label4.Name = "label4";
   this.label4.Size = new System.Drawing.Size(56, 13);
   this.label4.TabIndex = 6;
   this.label4.Text = "Database:";
   // 
   // svnDirectory
   // 
   this.svnDirectory.Location = new System.Drawing.Point(99, 125);
   this.svnDirectory.Name = "svnDirectory";
   this.svnDirectory.Size = new System.Drawing.Size(331, 20);
   this.svnDirectory.TabIndex = 9;
   // 
   // label5
   // 
   this.label5.AutoSize = true;
   this.label5.Location = new System.Drawing.Point(21, 128);
   this.label5.Name = "label5";
   this.label5.Size = new System.Drawing.Size(77, 13);
   this.label5.TabIndex = 8;
   this.label5.Text = "SVN Directory:";
   // 
   // checkedListBox1
   // 
   this.checkedListBox1.FormattingEnabled = true;
   this.checkedListBox1.Items.AddRange(new object[] {
            "Stored procedures",
            "Tables",
            "Full Text Indices",
            "Functions",
            "Views",
            "User Defined Data Types"});
   this.checkedListBox1.Location = new System.Drawing.Point(99, 160);
   this.checkedListBox1.Name = "checkedListBox1";
   this.checkedListBox1.Size = new System.Drawing.Size(148, 94);
   this.checkedListBox1.TabIndex = 10;
   // 
   // button1
   // 
   this.button1.Location = new System.Drawing.Point(253, 160);
   this.button1.Name = "button1";
   this.button1.Size = new System.Drawing.Size(177, 64);
   this.button1.TabIndex = 11;
   this.button1.Text = "Generate";
   this.button1.UseVisualStyleBackColor = true;
   this.button1.Click += new System.EventHandler(this.button1_Click);
   // 
   // statusStrip1
   // 
   this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
   this.statusStrip1.Location = new System.Drawing.Point(0, 257);
   this.statusStrip1.Name = "statusStrip1";
   this.statusStrip1.Size = new System.Drawing.Size(663, 22);
   this.statusStrip1.TabIndex = 12;
   this.statusStrip1.Text = "statusStrip1";
   // 
   // toolStripStatusLabel1
   // 
   this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
   this.toolStripStatusLabel1.Size = new System.Drawing.Size(38, 17);
   this.toolStripStatusLabel1.Text = "Ready";
   // 
   // imgLogo
   // 
   this.imgLogo.Cursor = System.Windows.Forms.Cursors.Default;
   this.imgLogo.Image = ((System.Drawing.Image)(resources.GetObject("imgLogo.Image")));
   this.imgLogo.Location = new System.Drawing.Point(447, 12);
   this.imgLogo.Name = "imgLogo";
   this.imgLogo.Size = new System.Drawing.Size(199, 133);
   this.imgLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
   this.imgLogo.TabIndex = 18;
   this.imgLogo.TabStop = false;
   // 
   // Form1
   // 
   this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
   this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
   this.ClientSize = new System.Drawing.Size(663, 279);
   this.Controls.Add(this.imgLogo);
   this.Controls.Add(this.statusStrip1);
   this.Controls.Add(this.button1);
   this.Controls.Add(this.checkedListBox1);
   this.Controls.Add(this.svnDirectory);
   this.Controls.Add(this.label5);
   this.Controls.Add(this.database);
   this.Controls.Add(this.label4);
   this.Controls.Add(this.password);
   this.Controls.Add(this.label3);
   this.Controls.Add(this.login);
   this.Controls.Add(this.label2);
   this.Controls.Add(this.serverName);
   this.Controls.Add(this.label1);
   this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
   this.Name = "Form1";
   this.Text = "Form1";
   this.Load += new System.EventHandler(this.Form1_Load);
   this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
   this.statusStrip1.ResumeLayout(false);
   this.statusStrip1.PerformLayout();
   ((System.ComponentModel.ISupportInitialize)(this.imgLogo)).EndInit();
   this.ResumeLayout(false);
   this.PerformLayout();

  }

  #endregion

  private System.Windows.Forms.Label label1;
  private System.Windows.Forms.TextBox serverName;
  private System.Windows.Forms.TextBox login;
  private System.Windows.Forms.Label label2;
  private System.Windows.Forms.TextBox password;
  private System.Windows.Forms.Label label3;
  private System.Windows.Forms.TextBox database;
  private System.Windows.Forms.Label label4;
  private System.Windows.Forms.TextBox svnDirectory;
  private System.Windows.Forms.Label label5;
  private System.Windows.Forms.CheckedListBox checkedListBox1;
  private System.Windows.Forms.Button button1;
  private System.Windows.Forms.StatusStrip statusStrip1;
  private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
  internal System.Windows.Forms.PictureBox imgLogo;
 }
}

