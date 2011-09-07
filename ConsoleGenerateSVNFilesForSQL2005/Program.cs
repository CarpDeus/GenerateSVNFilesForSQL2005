using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleGenerateSVNFilesForSQL2005
{
 class Program
 {
  static void Main(string[] args)
  {
   Arguments CommandLine = new Arguments(args);
   bool DisplayHelp = false;
   if (CommandLine["?"] != null)
    DisplayHelp = true;
   string ErrorMessage = "";

   string SQLServer = (CommandLine["server"] == null ? CommandLine["s"] : CommandLine["server"]);
   string DatabaseName = (CommandLine["databasename"] == null ? CommandLine["db"] : CommandLine["databasename"]);
   string UserLogin = (CommandLine["u"]);
   string Password = (CommandLine["pwd"]);
   string SVNDirectory = (CommandLine["svndirectory"]);
   bool StoredProcedures = (CommandLine["procedures"] != null);
   bool Tables = (CommandLine["tables"] != null);
   bool FullTextIndices = (CommandLine["fulltextindices"] != null || CommandLine["fti"] != null);
   bool Functions = (CommandLine["functions"] != null);
   bool Views = (CommandLine["views"] != null);
   bool UDDT = (CommandLine["uddts"] != null);
   bool ServiceBroker = (CommandLine["servicebroker"] != null || CommandLine["sb"] != null);

   if (!DisplayHelp && SQLServer == null)
   {
    ErrorMessage = ErrorMessage + "SQLServer is REQUIRED!\r\n";
   }

   if (!DisplayHelp && DatabaseName == null)
   {
    ErrorMessage = ErrorMessage + "DatabaseName is REQUIRED!\r\n";
   }
   if (!DisplayHelp && SVNDirectory == null)
   {
    ErrorMessage = ErrorMessage + "SVNDirectory is REQUIRED!\r\n";
   }



   if (DisplayHelp || ErrorMessage != string.Empty)
   {

    /// <summary>
    System.Version AppVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;

    Console.Clear();
    if (ErrorMessage != string.Empty)
     Console.WriteLine(ErrorMessage);
    Console.WriteLine("{4}\tVersion: {0}.{1}.{2}.{3}", AppVersion.Major.ToString(), AppVersion.Minor.ToString(),
     AppVersion.Build.ToString(), AppVersion.Revision.ToString(), System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.Name);
    svnImplement svn = new svnImplement();
    Console.WriteLine(svn.BuildInfo());
    Console.Write("\r\n\r\n{0}", System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.Name);
    
    Console.Write(" -? -s[erver]:ServerName -db:DatabaseName -u:UserLogin ");
    Console.WriteLine("\t-pwd:password -procedures -tables -fti -functions -views");
    Console.WriteLine("-? shows this message");
    Console.WriteLine("-s[erver] Fully qualified name of the database server");
    Console.WriteLine("-db Database name");
    Console.WriteLine("-u User login -pwd Password (do not include to use trusted connection)");
    Console.WriteLine("-procedures Flag to include stored procedures");
    Console.WriteLine("-tables Flag to include tables");
    Console.WriteLine("-fti Flag to include Full Text Indexing information");
    Console.WriteLine("-functions Flag to include User Defined Functions");
    Console.WriteLine("-views Flag to include Views");
    Console.WriteLine("-uddts Flag to include User Defined Data Types");
    Console.WriteLine("-servicebroker (sb) Flag to include Service Broker pieces");
   }
   else
   {
    new svnImplement(SQLServer, DatabaseName, (UserLogin == null ? "" : UserLogin), (Password == null ? "" : Password), SVNDirectory, StoredProcedures, Tables, FullTextIndices, Functions, Views, UDDT);
   }
  }
 }
}
