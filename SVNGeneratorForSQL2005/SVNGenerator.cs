using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

//using SQLDMO;

using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;

//using DotSVN.Common.Exceptions;
//using DotSVN.Common.Util;

namespace SVNGeneratorForSQL2005
{
 /// <summary>
 /// SVNGenerator creates individual files for sprocs/tables/views/udfs/etc, suitable for SVN Source Control
 /// </summary>
 public class SVNGenerator
 {

  /// <summary>
  /// Way to interact with the calling program to send status messages back
  /// </summary>
  /// <param name="message">Message strung</param>
  public delegate void MessageHandler(string message);

  /// <summary>
  /// Message Handler interface
  /// </summary>
  private MessageHandler mvMessageHandler;

  //Private variables
  string pSQLServer = "";
  string pDatabaseName = "";
  string pUserLogin = "";
  string pPassword = "";
  string pSVNDirectory = "";
  bool pStoredProcedures = false;
  bool pTables = false;
  bool pFullTextIndices = false;
  bool pFunctions = false;
  bool pViews = false;
  bool pUDDT = false;
  bool pServiceBroker = false;


  /// <summary>
  /// SQL Server name
  /// </summary>
  public string SQLServer
  {
   get { return pSQLServer; }
   set { pSQLServer = value; }
  }

  /// <summary>
  /// Database name
  /// </summary>
  public string DatabaseName
  {
   get { return pDatabaseName; }
   set { pDatabaseName = value; }
  }

  /// <summary>
  /// UserLogin... If empty, will try trusted connection
  /// </summary>
  public string UserLogin
  {
   get { return pUserLogin; }
   set { pUserLogin = value; }
  }

  /// <summary>
  /// Password... If empty, will try trusted connection
  /// </summary>
  public string Password
  {
   get { return pPassword; }
   set { pPassword = value; }
  }

  /// <summary>
  /// Directory where the files will be created
  /// </summary>
  public string SVNDirectory
  {
   get { return pSVNDirectory; }
   set { pSVNDirectory = value; }
  }

  /// <summary>
  /// Process Stored Procedures
  /// </summary>
  public bool StoredProcedures
  {
   get { return pStoredProcedures; }
   set { pStoredProcedures = value; }
  }

  /// <summary>
  /// Process Tables
  /// </summary>
  public bool Tables
  {
   get { return pTables; }
   set { pTables = value; }
  }

  /// <summary>
  /// Process FullTextIndexing
  /// </summary>
  public bool FullTextIndices
  {
   get { return pFullTextIndices; }
   set { pFullTextIndices = value; }
  }

  /// <summary>
  /// Process User Defined Functions
  /// </summary>
  public bool Functions
  {
   get { return pFunctions; }
   set { pFunctions = value; }
  }

  /// <summary>
  /// Process Views
  /// </summary>
  public bool Views
  {
   get { return pViews; }
   set { pViews = value; }
  }

  /// <summary>
  /// User Defined Data Types
  /// </summary>
  public bool UDDT
  {
   get { return pUDDT; }
   set { pUDDT = value; }
  }


  /// <summary>
  /// New SVNGenerator
  /// </summary>
  public SVNGenerator() { }

  /// <summary>
  /// Create an instance of the SVNGenerator and set all properties
  /// </summary>
  /// <param name="SQLServer">Name of the SQL Server</param>
  /// <param name="DatabaseName">Database name</param>
  /// <param name="UserLogin">UserLogin... If blank, will try trusted connection</param>
  /// <param name="Password">Password... If blank will try trusted connection</param>
  /// <param name="SVNDirectory">Directory where the files will be created</param>
  /// <param name="StoredProcedures">Process stored procedures</param>
  /// <param name="Tables">Process tables</param>
  /// <param name="FullTextIndices">Process FullTextIndexing</param>
  /// <param name="Functions">Process user defined functions</param>
  /// <param name="Views">Process views</param>
  /// <param name="UDDT">User Defined Data Types</param>
  public SVNGenerator(string SQLServer, string DatabaseName, string UserLogin, string Password, string SVNDirectory,
    bool StoredProcedures, bool Tables, bool FullTextIndices, bool Functions, bool Views, bool UDDT, bool ServiceBroker)
  {
   pSQLServer = SQLServer;
   pDatabaseName = DatabaseName;
   pUserLogin = UserLogin;
   pPassword = Password;
   pSVNDirectory = SVNDirectory;
   pStoredProcedures = StoredProcedures;
   pTables = Tables;
   pFullTextIndices = FullTextIndices;
   pFunctions = Functions;
   pViews = Views;
   pUDDT = UDDT;
   pServiceBroker = ServiceBroker;
  }

  /// <summary>
  /// Determine whether to write the data out to disk
  /// </summary>
  /// <param name="fileName">Name of file that will be replaced</param>
  /// <param name="scriptOutput">What will be written to the file</param>
  private void WriteData(string fileName, string  scriptOutput)
  {
   
   // If the file doesn't exist, we need to write
   if (!System.IO.File.Exists(fileName))
    System.IO.File.WriteAllText(fileName, scriptOutput);
   else
   {
    // The file exists so get the current contents of the file
    string scriptInput = System.IO.File.ReadAllText(fileName);
    // If the current contents are different from the new, write the new
    if (scriptOutput != scriptInput)
     System.IO.File.WriteAllText(fileName, scriptOutput);
   }
  }

  /// <summary>
  /// Process a database to create SVN files, passing in all parameters at this point
  /// </summary>
  /// <param name="SQLServer">Name of the SQL Server</param>
  /// <param name="DatabaseName">Database name</param>
  /// <param name="UserLogin">UserLogin... If blank, will try trusted connection</param>
  /// <param name="Password">Password... If blank will try trusted connection</param>
  /// <param name="SVNDirectory">Directory where the files will be created</param>
  /// <param name="StoredProcedures">Process stored procedures</param>
  /// <param name="Tables">Process tables</param>
  /// <param name="FullTextIndices">Process FullTextIndexing</param>
  /// <param name="Functions">Process user defined functions</param>
  /// <param name="Views">Process views</param>
  /// <param name="UDDT">Process User Defined Data Types</param>
  public void Process(string SQLServer, string DatabaseName, string UserLogin, string Password, string SVNDirectory,
    bool StoredProcedures, bool Tables, bool FullTextIndices, bool Functions, bool Views, bool UDDT, bool ServiceBroker)
  {
   pSQLServer = SQLServer;
   pDatabaseName = DatabaseName;
   pUserLogin = UserLogin;
   pPassword = Password;
   pSVNDirectory = SVNDirectory;
   pStoredProcedures = StoredProcedures;
   pTables = Tables;
   pFullTextIndices = FullTextIndices;
   pFunctions = Functions;
   pViews = Views;
   pUDDT = UDDT;
   pServiceBroker = ServiceBroker;
   Process();
  }

  /// <summary>
  /// Process a database to create SVN files using all of the existing settings
  /// </summary>
  public void Process()
  {

   // CommitChangesToSubversion(pSVNDirectory);
   // Make sure the svn directory end with a \
   if (!pSVNDirectory.EndsWith(@"\"))
    pSVNDirectory = pSVNDirectory + @"\";

   // Get a sql server instance
   //SQLServer srv = new SQLServerClass();
   ServerConnection conn = new ServerConnection();
   Server srv = null;
   
   ScriptingOptions scriptO = new ScriptingOptions();
   scriptO.IncludeHeaders = false;
   scriptO.ToFileOnly = false;
   try
   {
    // Determine if we need to use a trusted connection
    if (pUserLogin.Trim() == string.Empty && pPassword.Trim() == string.Empty)
    {
     conn.LoginSecure = true;
     //     srv.Connect(pSQLServer, null, null);
    }
    else
    {
     conn.LoginSecure = false;
     conn.Login = pUserLogin;
     conn.Password = pPassword;
     //Server svr = new Server(conn);
     //Console.WriteLine(svr.Name + " " + svr.Information.VersionString);
    }
    conn.ServerInstance = pSQLServer;
    //srv.Connect(pSQLServer, pUserLogin, pPassword);
    srv = new Server(conn);
    // Get a 2005 database instance
    //Database2 db = (Database2)srv.Databases.Item(pDatabaseName, "dbo");
    Database db = srv.Databases[pDatabaseName];

    //If we are processing stored procedures
    if (pStoredProcedures)
    {
     string directoryForOutput = string.Format(@"{0}{1}", SVNDirectory, "procedure");
     // Verify output directory exists and create if it doesn't
     if (!System.IO.Directory.Exists(directoryForOutput))
      System.IO.Directory.CreateDirectory(directoryForOutput);

     // Loop through all of the procedures
     foreach (StoredProcedure sp in db.StoredProcedures)
     {
      if (!sp.IsSystemObject) // If it's not a system object
      {
       // Inform the calling program
       NotifyCaller(string.Format("Processing procedure {0}.{1}", sp.Schema, sp.Name));
       // Define the file name
       string fileName = string.Format(@"{0}\{1}.{2}.sql", directoryForOutput, sp.Schema, sp.Name, "procedure");
       // generate the script to a string
       //StringCollection scriptOutput = sp.Script(scriptO);
       //string scriptOutput = sp.Script(SQLDMO.SQLDMO_SCRIPT_TYPE.SQLDMOScript_Drops
       // | SQLDMO.SQLDMO_SCRIPT_TYPE.SQLDMOScript_Default, null, SQLDMO_SCRIPT2_TYPE.SQLDMOScript2_Default);
       // Attempt to write

       WriteData(fileName, RecreateObject(sp, scriptO));
      }
     }

     // Now determine if we need to delete any files
     foreach (string checkFileName in System.IO.Directory.GetFiles(directoryForOutput))
     {
      //srv.Databases["Galaxy4"].StoredProcedures.Contains("AddNewDomain");
      string[] FileChecker = checkFileName.Replace(directoryForOutput, "").Replace(@"\", "").Split(".".ToCharArray());
      NotifyCaller(string.Format("Verifying {0}.{1}", FileChecker[0], FileChecker[1]));
      if (!db.StoredProcedures.Contains(FileChecker[1], FileChecker[0]))
       System.IO.File.Delete(checkFileName);
     }
    }

    // If we are processing tables
    if (pTables)
    {
     string directoryForOutput = string.Format(@"{0}{1}", SVNDirectory, "table");
     // Verify output directory exists and create if it doesn't
     if (!System.IO.Directory.Exists(directoryForOutput))
      System.IO.Directory.CreateDirectory(directoryForOutput);

     // Loop through all the tables
     foreach (Table tb in db.Tables)
     {
      if (!tb.IsSystemObject) // If it's not a system object
      {
       // Inform the calling program
       NotifyCaller(string.Format("Processing table {0}.{1}", tb.Schema, tb.Name));

       //// Process Indices
       //foreach (SQLDMO.Index2 idx in tb.Indexes)
       //{
       // NotifyCaller(string.Format("Processing table index {0}.{1}.{2}", tb.Schema, tb.Name, idx.Name));
       // string idxFileName = string.Format(@"{0}\{1}.{2}.{3}.sql", directoryForOutput, tb.Schema, tb.Name, idx.Name);
       // string idxScriptOutput = "";
       // idxScriptOutput = idx.Script(SQLDMO.SQLDMO_SCRIPT_TYPE.SQLDMOScript_Drops
       //                | SQLDMO.SQLDMO_SCRIPT_TYPE.SQLDMOScript_Default
       //                , null, SQLDMO_SCRIPT2_TYPE.SQLDMOScript2_Default
       //                );
       // WriteData(idxFileName, idxScriptOutput);
       //}

       // Define the file name
       string fileName = string.Format(@"{0}\{1}.{2}.sql", directoryForOutput, tb.Schema, tb.Name);

       // Which script we generate depends on whether we are processing full text indexing or not
       scriptO.DriAll = true;
       scriptO.DriIndexes = true;
       scriptO.FullTextCatalogs = tb.FullTextIndex != null;
       scriptO.FullTextIndexes = tb.FullTextIndex != null;
       scriptO.NonClusteredIndexes = true;
       scriptO.ClusteredIndexes = true;
       scriptO.Indexes = true;
       // Attempt to write 
       WriteData(fileName, RecreateObject(tb, scriptO));
      }
     }
     // Now determine if we need to delete any files
     foreach (string checkFileName in System.IO.Directory.GetFiles(directoryForOutput))
     {
      string[] FileChecker = checkFileName.Replace(directoryForOutput, "").Replace(@"\", "").Split(".".ToCharArray());
      NotifyCaller(string.Format("Verifying {0}.{1}", FileChecker[0], FileChecker[1]));
      if (!db.Tables.Contains(FileChecker[1], FileChecker[0]))
       System.IO.File.Delete(checkFileName);
     }
    }

    //If we are processing user defined functions
    if (pFunctions)
    {
     string directoryForOutput = string.Format(@"{0}{1}", SVNDirectory, "user-defined-function");
     // Verify output directory exists and create if it doesn't
     if (!System.IO.Directory.Exists(directoryForOutput))
      System.IO.Directory.CreateDirectory(directoryForOutput);

     // Loop through all of the user defined functions
     foreach (UserDefinedFunction udf in db.UserDefinedFunctions)
     {
      if (!udf.IsSystemObject)    // If it's not a system object
      {

       // Inform the calling program
       NotifyCaller(string.Format("Processing user-defined-function {0}.{1}", udf.Schema, udf.Name));
       // Define the file name
       string fileName = string.Format(@"{0}\{1}.{2}.sql", directoryForOutput, udf.Schema, udf.Name);
       // generate the script to a string

       // Attempt to write
       WriteData(fileName, RecreateObject(udf, scriptO));
      }
     }
     // Now determine if we need to delete any files
     foreach (string checkFileName in System.IO.Directory.GetFiles(directoryForOutput))
     {

      string[] FileChecker = checkFileName.Replace(directoryForOutput, "").Replace(@"\", "").Split(".".ToCharArray());
      NotifyCaller(string.Format("Verifying {0}.{1}", FileChecker[0], FileChecker[1]));
      if (!db.UserDefinedFunctions.Contains(FileChecker[1], FileChecker[0]))
       System.IO.File.Delete(checkFileName);
     }
    }

    //If we are processing Full Text Indices
    if (pFullTextIndices)
    {
     string directoryForOutput = string.Format(@"{0}{1}", SVNDirectory, "full-text-catalog");
     // Verify output directory exists and create if it doesn't
     if (!System.IO.Directory.Exists(directoryForOutput))
      System.IO.Directory.CreateDirectory(directoryForOutput);

     // Loop through all of the Catalogs
     foreach (FullTextCatalog ftc in db.FullTextCatalogs)
     {
      // Inform the calling program
      NotifyCaller(string.Format("Processing Full Text Catalog {0}", ftc.Name));
      // Define the file name
      string fileName = string.Format(@"{0}\{1}.sql", directoryForOutput, ftc.Name);
      scriptO.FullTextIndexes = true;
      scriptO.FullTextCatalogs = true;

      // generate the script to a string
      WriteData(fileName, RecreateObject(ftc, scriptO));
     }
     // Now determine if we need to delete any files
     foreach (string checkFileName in System.IO.Directory.GetFiles(directoryForOutput))
     {

      string[] FileChecker = checkFileName.Replace(directoryForOutput, "").Replace(@"\", "").Split(".".ToCharArray());
      NotifyCaller(string.Format("Verifying {0}.{1}", FileChecker[0], FileChecker[1]));
      if (!db.FullTextCatalogs.Contains(FileChecker[0]))
       System.IO.File.Delete(checkFileName);
     }
    }

    //If we are processing views
    if (pViews)
    {
     string directoryForOutput = string.Format(@"{0}{1}", SVNDirectory, "view");
     // Verify output directory exists
     if (!System.IO.Directory.Exists(directoryForOutput))
      System.IO.Directory.CreateDirectory(directoryForOutput);
     // Loop through all of the views
     foreach (View vw in db.Views)
     {
      if (!vw.IsSystemObject) // If it's not a system object
      {
       // Inform the calling program
       NotifyCaller(string.Format("Processing view {0}.{1}", vw.Schema, vw.Name));
       // Define the file name
       string fileName = string.Format(@"{0}\{1}.{2}.sql", directoryForOutput, vw.Schema, vw.Name);
       // generate the script to a string

       // Attempt to write
       WriteData(fileName, RecreateObject(vw, scriptO));
      }
     }
     // Now determine if we need to delete any files
     foreach (string checkFileName in System.IO.Directory.GetFiles(directoryForOutput))
     {

      string[] FileChecker = checkFileName.Replace(directoryForOutput, "").Replace(@"\", "").Split(".".ToCharArray());
      NotifyCaller(string.Format("Verifying {0}.{1}", FileChecker[0], FileChecker[1]));
      if (!db.Views.Contains(FileChecker[1], FileChecker[0]))
       System.IO.File.Delete(checkFileName);
     }
    }

    //If we are processing User Defined Data Types
    if (pUDDT)
    {
     string directoryForOutput = string.Format(@"{0}{1}", SVNDirectory, "user-defined-data-type");
     // Verify output directory exists
     if (!System.IO.Directory.Exists(directoryForOutput))
      System.IO.Directory.CreateDirectory(directoryForOutput);
     // Loop through all of the views
     foreach (UserDefinedDataType ud in db.UserDefinedDataTypes)
     {
      // Inform the calling program
      NotifyCaller(string.Format("Processing user defined data type {0}.{1}", ud.Schema, ud.Name));
      // Define the file name
      string fileName = string.Format(@"{0}\{1}.{2}.sql", directoryForOutput, ud.Schema, ud.Name);
      // generate the script to a string
      // Attempt to write
      WriteData(fileName, RecreateObject(ud, scriptO));
     }
     // Now determine if we need to delete any files
     foreach (string checkFileName in System.IO.Directory.GetFiles(directoryForOutput))
     {
      string[] FileChecker = checkFileName.Replace(directoryForOutput, "").Replace(@"\", "").Split(".".ToCharArray());
      NotifyCaller(string.Format("Verifying {0}.{1}", FileChecker[0], FileChecker[1]));
      if (!db.UserDefinedTypes.Contains(FileChecker[1], FileChecker[0]))
       System.IO.File.Delete(checkFileName);
     }
    }

    /*
    //If we are processing Service Brokers
    if (pServiceBroker )
    {
     string directoryForOutput = string.Format(@"{0}{1}", SVNDirectory, "service-broker");
     // Verify output directory exists
     if (!System.IO.Directory.Exists(directoryForOutput))
      System.IO.Directory.CreateDirectory(directoryForOutput);

//     Microsoft.SqlServer.Management.Smo.Broker.ServiceQueue 
     Microsoft.SqlServer.Management.Smo.Broker.ServiceBroker sb = new Microsoft.SqlServer.Management.Smo.Broker.ServiceBroker();
     

     //Start with Message Types
     directoryForOutput = string.Format(@"{0}{1}", SVNDirectory, @"service-broker\message-types");
     if (!System.IO.Directory.Exists(directoryForOutput))
      System.IO.Directory.CreateDirectory(directoryForOutput);

     // Loop through all of the views
     foreach (UserDefinedDataType ud in db.UserDefinedDataTypes)
     {
      // Inform the calling program
      NotifyCaller(string.Format("Processing user defined data type {0}.{1}", ud.Schema, ud.Name));
      // Define the file name
      string fileName = string.Format(@"{0}\{1}.{2}.sql", directoryForOutput, ud.Schema, ud.Name);
      // generate the script to a string
      // Attempt to write
      WriteData(fileName, RecreateObject(ud, scriptO));
     }
     // Now determine if we need to delete any files
     foreach (string checkFileName in System.IO.Directory.GetFiles(directoryForOutput))
     {
      string[] FileChecker = checkFileName.Replace(directoryForOutput, "").Replace(@"\", "").Split(".".ToCharArray());
      NotifyCaller(string.Format("Verifying {0}.{1}", FileChecker[0], FileChecker[1]));
      if (!db.UserDefinedTypes.Contains(FileChecker[1], FileChecker[0]))
       System.IO.File.Delete(checkFileName);
     }
    }
    */

   }
   catch (Exception ex)
   {
    NotifyCaller(ex.ToString());
   }

   // Subversion
   try { CommitChangesToSubversion(pSVNDirectory); NotifyCaller("Commited Changes"); }
   catch (Exception ex) { NotifyCaller(ex.ToString()); }
  }

  private void CommitChangesToSubversion(string directoryPath)
  {
   
   //using (SvnClient client = new SvnClient())
   //{
    
   // SvnCommitArgs sargs = new SvnCommitArgs();
   // sargs.LogMessage = string.Format("SVNForSQL2005 {0}", System.DateTime.Now.ToUniversalTime());
   // SvnCommitResult sres = null; // new SvnCommitResult();
   // //ICollection<string> pathList = new string[];
   // //pathList.Add(directoryPath + @"\full-text-catalog");
   // //pathList.Add(directoryPath + @"\procedure");
   // //pathList.Add(directoryPath + @"\table");
   // //pathList.Add(directoryPath + @"\user-defined-data-type");
   // //pathList.Add(directoryPath + @"\user-defined-function");
   // //pathList.Add(directoryPath + @"\view");

   // client.Commit(pathList, sargs, out sres);//, out svn
   // //client.Commit(directoryPath, sargs);
   //}
  }


  string RecreateObject(IScriptable scriptableObject, ScriptingOptions sCO)
  {
   StringBuilder sb = new StringBuilder();

   // Generate Drop script
   ScriptingOptions so = new ScriptingOptions();
   so.ScriptDrops = true;
   so.IncludeIfNotExists = true;

   foreach (string stmt in scriptableObject.Script(so))
   {
    sb.AppendLine(stmt);
   }

   // Add batch separator
   sb.AppendLine("GO");

   // Now generate Crate script and add it to the resulting ScriptCollection
   sCO.ScriptDrops = false;
   sCO.IncludeIfNotExists = false;

   foreach (string stmt in scriptableObject.Script(sCO))
   {
    sb.AppendLine(stmt);
    if (stmt.Contains("SET ANSI_NULLS") || stmt.Contains("SET QUOTED_IDENTIFIER"))
     sb.AppendLine("GO");
   }
  
   //for (int i = 0; i < sc.Count; i++)
   // sb.Append(sc[i]);
   return sb.ToString();
  }

  /// <summary>
  /// Get the BuildInformation for revisions
  /// </summary>
  /// <returns></returns>
  public string BuildInfo()
  {
   System.Version AppVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
   return (string.Format("{4}\tVersion: {0}.{1}.{2}.{3}", AppVersion.Major.ToString(), AppVersion.Minor.ToString(),
     AppVersion.Build.ToString(), AppVersion.Revision.ToString(),System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.Name));
  }

  void NotifyCaller(string msg)
  {
   if (mvMessageHandler != null)
    mvMessageHandler(msg);
  }

  public void OnMessageHandler(MessageHandler msgString)
  {
   if (msgString != null)
    mvMessageHandler = msgString;
  }

 }
}