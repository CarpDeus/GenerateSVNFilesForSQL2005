using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;
using System.Text.RegularExpressions;

namespace ConsoleGenerateSVNFilesForSQL2005
{
 class svnImplement
 {
  public svnImplement() { }
  public svnImplement(string SQLServer, string DatabaseName, string UserLogin, string Password, string SVNDirectory,
bool StoredProcedures, bool Tables, bool FullTextIndices, bool Functions, bool Views, bool UDDT)
  {

   SVNGeneratorForSQL2005.SVNGenerator svg = new SVNGeneratorForSQL2005.SVNGenerator(SQLServer, DatabaseName, UserLogin, Password, SVNDirectory,
       StoredProcedures, Tables, FullTextIndices, Functions, Views, UDDT, false );
   svg.OnMessageHandler(Notification);
   svg.Process();
   Notification("Done");
  }

  void Notification(string Notification)
  {
   Console.WriteLine(Notification);
  }
  /// <summary>
  /// Get the BuildInformation for revisions
  /// </summary>
  /// <returns></returns>
  public string BuildInfo()
  {
   return new SVNGeneratorForSQL2005.SVNGenerator().BuildInfo();
  }
 }

 /// <summary>
 /// Arguments class- Parsed Arguments where valid argurments can being with
 /// -, / or -- followed by argument name.
 /// 
 /// When next token is another argument, argument is treated as a flag.
 /// Space, = or : terminate the token of the argument name definition. Anything after that
 /// up to the next argument identifier is defined as the value of that argument.
 /// 
 /// Values beginning with " run until the next " 
 /// 
 /// Valid argument examples include:
 /// -argument1 value1 --argument2 /argument3="This example contains spaces- though it could contain dashes-" /argument4 Hello /argument5:world
 /// These break down into:
 ///   argument1 = value1
 ///   argument2 exists
 ///   argument3 = This is a test
 ///   argument4 = Hello
 ///   argument5 = world
 /// </summary>
 public class Arguments
 {
  
  private StringDictionary pArguments;

  // Constructor
  public Arguments(string[] Args)
  {

   pArguments = new StringDictionary();
   Regex Splitter = new Regex(@"^-{1,2}|^/|=|:",
       RegexOptions.IgnoreCase | RegexOptions.Compiled);

   Regex matchRemoval = new Regex(@"^['""]?(.*?)['""]?$",
       RegexOptions.IgnoreCase | RegexOptions.Compiled);

   string Arguments = null;
   string[] argumentPieces;

   foreach (string individualArgument in Args)
   {
    // Look for new arguments and possible values
    argumentPieces = Splitter.Split(individualArgument, 3);

    switch (argumentPieces.Length)
    {
     // Either small value or space seperator
     case 1:
      if (Arguments != null)
      {
       if (!pArguments.ContainsKey(Arguments))
       {
        argumentPieces[0] =
            matchRemoval.Replace(argumentPieces[0], "$1");

        pArguments.Add(Arguments.ToLower(), argumentPieces[0]);
       }
       Arguments = null;
      }
      // else Error: no parameter waiting for a value (skipped)
      break;

     // Only found a parameter token, no value
     case 2:
      // The last parameter is still waiting. 
      // With no value, set it to true.
      if (Arguments != null)
      {
       if (!pArguments.ContainsKey(Arguments))
        pArguments.Add(Arguments, "true");
      }
      Arguments = argumentPieces[1];
      break;

     // Parameter with enclosed value
     case 3:
      // The last parameter is still waiting. 
      // With no value, set it to true.
      if (Arguments != null)
      {
       if (!pArguments.ContainsKey(Arguments))
        pArguments.Add(Arguments, "true");
      }

      Arguments = argumentPieces[1];

      // Remove possible enclosing characters (",')
      if (!pArguments.ContainsKey(Arguments))
      {
       argumentPieces[2] = matchRemoval.Replace(argumentPieces[2], "$1");
       pArguments.Add(Arguments, argumentPieces[2]);
      }

      Arguments = null;
      break;
    }
   }
   // In case a parameter is still waiting
   if (Arguments != null)
   {
    if (!pArguments.ContainsKey(Arguments))
     pArguments.Add(Arguments, "true");
   }
  }

  // Retrieve a parameter value if it exists 
  // (overriding C# indexer property)
  public string this[string Param]
  {
   get
   {
    return (pArguments[Param]);
   }
  }
  
 }

}