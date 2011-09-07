using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SVNGeneratorForSQL2005;

namespace GenerateSVNFilesForSQL2005
{
 public partial class Form1 : Form
 {
  public Form1()
  {
   InitializeComponent();
  }

  private void Form1_Load(object sender, EventArgs e)
  {
   database.Text = Properties.Settings.Default.Database;
   serverName.Text = Properties.Settings.Default.ServerName;
   login.Text = Properties.Settings.Default.UserName;
   password.Text = Properties.Settings.Default.Password;
   svnDirectory.Text = Properties.Settings.Default.SVNDirectory;
   checkedListBox1.SetItemChecked(0, Properties.Settings.Default.StoredProcedures);
   checkedListBox1.SetItemChecked(1, Properties.Settings.Default.Tables);
   checkedListBox1.SetItemChecked(2, Properties.Settings.Default.FullTextIndices);
   checkedListBox1.SetItemChecked(3, Properties.Settings.Default.Functions);
   checkedListBox1.SetItemChecked(4, Properties.Settings.Default.Views);
  }

  private void Form1_FormClosing(object sender, FormClosingEventArgs e)
  {
   Properties.Settings.Default.Database = database.Text;
   Properties.Settings.Default.ServerName  = serverName.Text;
   Properties.Settings.Default.UserName  = login.Text;
   Properties.Settings.Default.Password  = password.Text;
   Properties.Settings.Default.SVNDirectory = svnDirectory.Text;
   Properties.Settings.Default.StoredProcedures = checkedListBox1.GetItemChecked(0);
   Properties.Settings.Default.Tables  = checkedListBox1.GetItemChecked(1);
   Properties.Settings.Default.FullTextIndices  = checkedListBox1.GetItemChecked(2);
   Properties.Settings.Default.Functions = checkedListBox1.GetItemChecked(3);
   Properties.Settings.Default.Views = checkedListBox1.GetItemChecked(4);
   Properties.Settings.Default.Save();
   toolStripStatusLabel1.Text = "Ready";
  }

  private void button1_Click(object sender, EventArgs e)
  {
   Cursor = Cursors.WaitCursor;
   SVNGenerator svg = new SVNGenerator(serverName.Text, database.Text, login.Text, password.Text,
    svnDirectory.Text, checkedListBox1.GetItemChecked(0), checkedListBox1.GetItemChecked(1), checkedListBox1.GetItemChecked(2), checkedListBox1.GetItemChecked(3), checkedListBox1.GetItemChecked(4), checkedListBox1.GetItemChecked(5), false );
   svg.OnMessageHandler(Notification);
   svg.Process();
   Notification("Ready");
   Cursor = Cursors.Default ;
  }

  public void Notification(string Notification)
  {
    toolStripStatusLabel1.Text = Notification;
    // statusStrip1.Refresh();
   Application.DoEvents();
  }
 }
}