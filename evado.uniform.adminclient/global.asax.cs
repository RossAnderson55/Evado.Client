/***************************************************************************************
*						      COPYRIGHT (C) Evado Holdings Pty. Ltd.	 2013 - 2018
*
*								             ALL RIGHTS RESERVED
*
*	  Author:	Ross Anderson
*	  Date:	  15 September 2005
*
*	  Version 2.0.0
*		Evado..Cms. - Cms -  Clinical Trial Software - Web Site
*		
*   Global System Access object class
*
****************************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Configuration;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.SessionState;
using System.Web.Security;
using System.IO;
using System.Net;
using System.Net.Http;

//Evado..Cms. namespace references.

namespace Evado.UniForm.AdminClient
{
  public partial class Global : System.Web.HttpApplication
  {
    #region Constants

    public const String CONST_EXTERNAL_COMMAND_EXTENSION = ".COMMAND.JSON";

    /// <summary>
    ///  This constant defines a trial identifier
    /// </summary>
    public const string CONST_CUSTOMER_GUID = "CUSTOMER_GUID";

    public const String SESSION_USER_ID = "EUWC_USER_ID";
    public const String SESSION_A1 = "EUWC_A1";
    public const String SESSION_ROLES = "EUWC_ROLES";

    public const String CONST_CLIENT_BASE_URL = "./client.aspx";

    public const String CONFIG_PAGE_DEEFAULT_LOGO = "PDLOGO";
    public const String CONFIG_EVENT_LOG_SOURCE_KEY = "EventLogSource";
    public const String CONST_ENABLE_DETAILED_LOGGING = "ENABLE_DETAILED_LOGGING";
    public const String CONFIG_ENABLE_PAGE_MENU_KEY = "ENABLE_MENU";
    public const String CONFIG_ENABLE_PAGE_HISTORY_KEY = "ENABLE_HISTORY";


    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region Global Variables and Objects
    // Variable containing the application path.  Used to generate the base URL.
    public static string EventLogSource = ConfigurationManager.AppSettings [ Global.CONFIG_EVENT_LOG_SOURCE_KEY ];

    /// <summary>
    /// THis object contains the assembly attributes.
    /// </summary>
    public static Evado.UniForm.AdminClient.WebAttributes AssemblyAttributes = new Evado.UniForm.AdminClient.WebAttributes ( );

    /// <summary>
    /// This object contains the current Authenication Mode 
    /// </summary>
    public static System.Web.Configuration.AuthenticationMode AuthenticationMode = System.Web.Configuration.AuthenticationMode.None;

    /// <summary>
    /// This is the http web client for the Uniform service.
    /// </summary>
    public static HttpClient HttpClient;

    /// <summary>
    /// This string contains the temporary directory path
    /// </summary>
    public static string TempPath = String.Empty;

    /// <summary>
    /// This string contains the local binary path
    /// </summary>
    public static string BinaryFilePath = @"temp\";

    /// <summary>
    /// This string contains the application directory path
    /// </summary>
    public static string ApplicationPath = String.Empty;

    /// <summary>
    /// This string contains the staic data directory path
    /// </summary>
    public static string StaticDataFilePath = String.Empty;

    /// <summary>
    /// This string contains the application directory path
    /// </summary>
    public static string TitlePrefix = String.Empty;

    /// <summary>
    /// this field defines the application path.
    /// </summary>
    public static string LogFilePath = @"logs\";

    /// <summary>
    /// This string contains the service root URl. 
    /// </summary>
    public static string WebServiceUrl = string.Empty;

    /// <summary>
    /// This string contains the relative service url. 
    /// </summary>
    public static string RelativeWcfRestURL = "euws/client/";

    /// <summary>
    /// This string contains the relative binary download url. 
    /// </summary>
    public static string RelativeBinaryDownloadURL = "images/temp/";

    /// <summary>
    /// This string contains the relative binary upload url. 
    /// </summary>
    public static string RelativeBinaryUploadURL = "images/defalut.aspx";

    /// <summary>
    /// This string contains the Yourtube embedded Url. 
    /// </summary>
    public static string YouTubeEmbeddedUrl = "https://www.youtube.com/embed/";

    /// <summary>
    /// This string contains the Yourtube embedded Url. 
    /// </summary>
    public static string VimeoEmbeddedUrl = "https://player.vimeo.com/video/";


    /// <summary>
    /// This string contains the relative binary upload url. 
    /// </summary>
    public static string DefaultLogoUrl = @"default_logo.jpg";

    /// <summary>
    /// This field contains a list of external commands.
    /// </summary>
    public static Dictionary<String, Evado.UniForm.Model.EuCommand> ExternalCommands = new Dictionary<String, Evado.UniForm.Model.EuCommand> ( );

    /// <summary>
    /// This boolean enables the debug display.
    /// </summary>
    public static bool DebugLogOn = false;
    /// <summary>
    /// This boolean enables the debug display.
    /// </summary>
    public static bool EnableDetailedLogging = false;
    /// <summary>
    /// This boolean enables the debug display.
    /// </summary>
    public static bool DebugDisplayOn = false;
    /// <summary>
    /// This boolean enables page group menu.
    /// </summary>
    public static bool EnablePageMenu = false;
    /// <summary>
    /// This boolean enables page group menu.
    /// </summary>
    public static bool EnablePageHistory = false;

    /// <summary>
    /// This boolean enables the Java debug display.
    /// </summary>
    public static bool JavaDebug = false;

    /// <summary>
    /// This boolean enables the display of data serialisation.
    /// </summary>
    public static bool DisplaySerialisation = false;

    /// <summary>
    /// This boolean enables the date pickers.
    /// </summary>
    public static bool EnableDatePicker = false;

    /// <summary>
    /// This string defines the client version.
    /// </summary>
    public static string ClientVersion = "V3_0";


    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region Application Error step

    protected void Application_Error ( Object sender, EventArgs e )
    {
      /// 
      /// If an exception is thrown in the application then log it to an event log.
      /// 
      Exception x = Server.GetLastError ( ).GetBaseException ( );
      EventLog.WriteEntry ( EventLogSource, x.ToString ( ), EventLogEntryType.Error );
    }


    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region Application Start step
    /// <summary>
    /// This event method is called when the application starts.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Application_OnStart ( Object sender, EventArgs e )
    {
      try
      {
        Global._ClientLog = new System.Text.StringBuilder ( ); 
        Global._DebuLog = new System.Text.StringBuilder ( );
        //
        // get the application path from the runtime.
        //
        Global._ClientLog = new System.Text.StringBuilder ( );
        Global._DebuLog = new System.Text.StringBuilder ( );
        Global.ApplicationPath = HttpRuntime.AppDomainAppPath;

        Global.LogGlobalMethod ( "Application_OnStart event" );
        Global.LogGlobalDebug ( "Startup Log:" );

        Global.LogGlobalDebug ( "eventLogSource: " + Global.EventLogSource );

        Global.ApplicationPath = HttpRuntime.AppDomainAppPath;
        Global.LogGlobalDebug ( "Application path: " + Global.ApplicationPath );

        //
        // Load the Application Environmental parameters for the application.
        //
        AssemblyAttributes = new Evado.UniForm.AdminClient.WebAttributes ( );

        Global.TempPath = Global.ApplicationPath + @"temp\";

        Global.LogGlobalDebug ( "TempPath: " + Global.TempPath );


        //
        // Load the Application Environmental parameters for the application.
        //
        AssemblyAttributes = new Evado.UniForm.AdminClient.WebAttributes ( );

        Global.BinaryFilePath = Global.ApplicationPath + @"temp\";

        // 
        // Initialise the method variables and objects.
        //      
        System.Web.Configuration.AuthenticationSection section = (System.Web.Configuration.AuthenticationSection)

        System.Web.Configuration.WebConfigurationManager.GetSection ( "system.web/authentication" );

        // 
        // Log the authentication mode
        // 
        Global.AuthenticationMode = section.Mode;
        Global.LogGlobalDebug ( "Authentication Type: " + Global.AuthenticationMode );

        //
        // Load the web configuration values.
        //
        this.LoadConfigurationValues ( );

        Global.LogGlobal ( "Copyright: " + Global.AssemblyAttributes.Copyright );

        Global.LogGlobalDebug ( "Version: " + Global.AssemblyAttributes.FullVersion );

        this.LoadExternalCommands ( );

        // 
        // Clean up the Page states by deleting old page state files
        // 

        string viewStatePath =
            Path.Combine ( Global.ApplicationPath, "PersistedViewState" );

        Evado.Model.EvStatics.Files.DeleteUserPageStateFiles ( viewStatePath );

        Global.LogGlobalDebug ( "Delete View State Files: " + Evado.Model.EvStatics.Files.Log );
        // 
        // Log the start up log.
        // 
        EventLog.WriteEntry ( EventLogSource, Global._ClientLog.ToString ( ), EventLogEntryType.Information );

      }
      catch ( Exception Ex )
      {
        Global.LogGlobalDebug ( "Application Startup error.\r\n" + Ex.ToString ( ) );

        EventLog.WriteEntry ( EventLogSource, Global._ClientLog.ToString ( ), EventLogEntryType.Error );

      } // Close catch   

      Global.LogMethodEnd ( "Application_Start" );

    }//END Application Start Event Method

    // ==================================================================================
    /// <summary>
    /// This method load the web configuration values.
    /// </summary>
    // -----------------------------------------------------------------------------------
    private void LoadConfigurationValues ( )
    {
      Global.LogGlobalMethod ( "LoadConfigurationValues" );

      //
      // Set the application log path  LogPath
      //
      Global.LogFilePath = Global.ApplicationPath + Global.LogFilePath;

      if ( ConfigurationManager.AppSettings [ Evado.Model.EvStatics.CONFIG_LOG_FILE_PATH ] != null )
      {
        string stConfigLogPath = ConfigurationManager.AppSettings [ Evado.Model.EvStatics.CONFIG_LOG_FILE_PATH ];

        if ( stConfigLogPath.Contains ( ":" ) == true )
        {
          Global.LogFilePath = stConfigLogPath;
        }
        else
        {
          Global.LogFilePath = Global.ApplicationPath + stConfigLogPath;
        }
      }

      Global.LogGlobal ( "Log file path: " + Global.LogFilePath );

      if ( ConfigurationManager.AppSettings [ Evado.Model.EvStatics.CONFIG_STATIC_FILE_PATH_KEY ] != null )
      {
        Global.StaticDataFilePath = ConfigurationManager.AppSettings [ Evado.Model.EvStatics.CONFIG_STATIC_FILE_PATH_KEY ];
      }

      Global.LogGlobal ( "Static Data File Path: " + Global.StaticDataFilePath );

      // 
      // Set the web service URl
      // 
      if ( ConfigurationManager.AppSettings [ "WebServiceUrl" ] != null )
      {
        Global.WebServiceUrl = (String) ConfigurationManager.AppSettings [ "WebServiceUrl" ].Trim ( );
      }

      Global.LogGlobal ( "WebServiceUrl: " + Global.WebServiceUrl );

      //
      // Set teh application log path
      //
      if ( ConfigurationManager.AppSettings [ "RelativeWcfRestURL" ] != null )
      {
        Global.RelativeWcfRestURL = ConfigurationManager.AppSettings [ "RelativeWcfRestURL" ];
      }
      Global.LogGlobal ( "RelativeWcfRestURL: " + Global.RelativeWcfRestURL );

      //
      // Set teh application log path
      //
      if ( ConfigurationManager.AppSettings [ "RelativeBinaryDownloadURL" ] != null )
      {
        Global.RelativeBinaryDownloadURL = ConfigurationManager.AppSettings [ "RelativeBinaryDownloadURL" ].Trim ( );
      }

      Global.LogGlobal ( "RelativeBinaryDownloadURL: " + Global.RelativeBinaryDownloadURL );

      Global.RelativeBinaryDownloadURL = Global.concatinateHttpUrl ( Global.WebServiceUrl, Global.RelativeBinaryDownloadURL );

      Global.LogGlobal ( "Formatted RelativeBinaryDownloadURL: " + Global.RelativeBinaryDownloadURL );

      // 
      // Set the binary file url
      // 
      if ( ConfigurationManager.AppSettings [ "RelativeBinaryUploadURL" ] != null )
      {
        Global.RelativeBinaryUploadURL = ConfigurationManager.AppSettings [ "RelativeBinaryUploadURL" ].Trim ( );
      }

      Global.LogGlobal ( "RelativeBinaryUploadURL: " + Global.RelativeBinaryUploadURL );

      Global.RelativeBinaryUploadURL = Global.concatinateHttpUrl ( Global.WebServiceUrl, Global.RelativeBinaryUploadURL );

      Global.LogGlobal ( "Formatted RelativeBinaryUploadUR2: " + Global.RelativeBinaryUploadURL );

      if ( ConfigurationManager.AppSettings [ Global.CONFIG_PAGE_DEEFAULT_LOGO ] != null )
      {
        Global.DefaultLogoUrl = ConfigurationManager.AppSettings [ Global.CONFIG_PAGE_DEEFAULT_LOGO ];
      }

      Global.DefaultLogoUrl = Global.concatinateHttpUrl ( Global.RelativeBinaryDownloadURL, Global.DefaultLogoUrl );

      Global.LogGlobal ( "Default Logo URL: " + Global.DefaultLogoUrl );

      // 
      // Set the You tube embedded URL
      // 
      if ( ConfigurationManager.AppSettings [ "VIMEO_EMBED_URL" ] != null )
      {
        Global.VimeoEmbeddedUrl = ConfigurationManager.AppSettings [ "VIMEO_EMBED_URL" ].Trim ( );
      }

      Global.LogGlobal ( "VimeoEmbeddedUrl: " + Global.VimeoEmbeddedUrl );

      // 
      // Set the Vimeo embedded URL
      // 
      if ( ConfigurationManager.AppSettings [ "YOU_TUBE_EMBED_URL" ] != null )
      {
        Global.YouTubeEmbeddedUrl = ConfigurationManager.AppSettings [ "YOU_TUBE_EMBED_URL" ].Trim ( );
      }

      Global.LogGlobal ( "YourtubeEmbeddedUrl: " + Global.YouTubeEmbeddedUrl );

      // 
      // Set the debug mode.
      // 
      if ( ConfigurationManager.AppSettings [ CONST_ENABLE_DETAILED_LOGGING ] != null )
      {
        string value = ConfigurationManager.AppSettings [ CONST_ENABLE_DETAILED_LOGGING ].ToLower ( );
        if ( Evado.Model.EvStatics.getBool ( value ) == true )
        {
          Global.EnableDetailedLogging = true;
        }
      }
      Global.LogGlobal ( "EnableDetailedLogging: " + Global.EnableDetailedLogging );

      // 
      // Set the debug mode.
      // 
      if ( ConfigurationManager.AppSettings [ "DebugLogOn" ] != null )
      {
        string value = ConfigurationManager.AppSettings [ "DebugLogOn" ].ToLower ( );
        if ( Evado.Model.EvStatics.getBool ( value ) == true )
        {
          Global.DebugLogOn = true;
        }
      }
      Global.LogGlobal ( "DebugLogOn: " + Global.DebugLogOn );
      // 
      // Set the debug mode.
      // 
      if ( ConfigurationManager.AppSettings [ "DebugDisplayOn" ] != null )
      {
        if ( ( ConfigurationManager.AppSettings [ "DebugDisplayOn" ] ).ToLower ( ) == "true" )
        {
          Global.DebugDisplayOn = true;
        }
      }
      Global.LogGlobal ( "DebugDisplayOn: " + Global.DebugDisplayOn );

      if ( ConfigurationManager.AppSettings [ "JavaDebug" ] != null )
      {
        if ( ( ConfigurationManager.AppSettings [ "JavaDebug" ] ).ToLower ( ) == "true" )
        {
          Global.JavaDebug = true;
        }
      }
      Global.LogGlobal ( "JavaDebug: " + Global.JavaDebug );

      if ( ConfigurationManager.AppSettings [ "DisplaySerialisation" ] != null )
      {
        if ( ( ConfigurationManager.AppSettings [ "DisplaySerialisation" ] ).ToLower ( ) == "true" )
        {
          Global.DisplaySerialisation = true;
        }
      }
      Global.LogGlobal ( "DisplaySerialisation: " + Global.DisplaySerialisation );

      // 
      // Set the web service URlCONFIG_ENABLE_PAGE_HISTORY_KEY
      // 
      if ( ConfigurationManager.AppSettings [ CONFIG_ENABLE_PAGE_MENU_KEY ] != null )
      {
        String value = ConfigurationManager.AppSettings [ CONFIG_ENABLE_PAGE_MENU_KEY ].Trim ( );

        Global.EnablePageMenu = Evado.Model.EvStatics.getBool ( value );
      }

      Global.LogGlobal ( "EnablePageMenu: " + Global.EnablePageMenu );

      // Set the web service URl
      // 
      if ( ConfigurationManager.AppSettings [ CONFIG_ENABLE_PAGE_HISTORY_KEY ] != null )
      {
        String value = ConfigurationManager.AppSettings [ CONFIG_ENABLE_PAGE_HISTORY_KEY ].Trim ( );

        Global.EnablePageHistory = Evado.Model.EvStatics.getBool ( value );
      }

      Global.LogGlobal ( "EnablePageHistory: " + Global.EnablePageHistory );


      // 
      // Set the debug mode.
      // 
      if ( ConfigurationManager.AppSettings [ "EnableDatePicker" ] != null )
      {
        if ( ( ConfigurationManager.AppSettings [ "EnableDatePicker" ] ).ToLower ( ) == "true" )
        {
          Global.EnableDatePicker = true;
        }
      }
      Global.LogGlobal ( "EnableDatePicker: " + Global.EnableDatePicker );

      //
      // Set the application Version
      //
      Global.ClientVersion = "V" + AssemblyAttributes.MinorVersion.Replace ( ".", "_" );

      Global.LogGlobal ( "ClientVersion: " + Global.ClientVersion );

      // 
      // Define the database error message suffix.
      // 
      if ( ConfigurationManager.AppSettings [ "TitlePrefix" ] != null )
      {
        Global.TitlePrefix = ConfigurationManager.AppSettings [ "TitlePrefix" ];
      }
      Global.LogGlobal ( "TitlePrefix: " + Global.TitlePrefix );

      Global.LogMethodEnd ( "LoadConfigurationValues" );
    }

    // ==================================================================================
    /// <summary>
    /// This method loads the external command in to a dictonary for external commands.
    /// </summary>
    // -----------------------------------------------------------------------------------
    private void LoadExternalCommands ( )
    {
      Global.LogGlobalMethod ( "LoadExternalCommands" );
      Global.LogGlobal ( "Static Data File Path '{0}'.", Global.StaticDataFilePath );
      //
      // initialise the methods variables and objects.
      //
      String extension = Global.CONST_EXTERNAL_COMMAND_EXTENSION;
      Global.LogGlobal ( "extension '{0}'.", extension );

      Global.ExternalCommands = new Dictionary<string, Model.EuCommand> ( );

      if ( Global.StaticDataFilePath != String.Empty )
      {
        List<String> fileNames = Evado.Model.EvStatics.Files.getDirectoryFileList ( Global.StaticDataFilePath, extension );

        Global.LogGlobal ( "fileNames.Count {0}.", fileNames.Count );

        //
        // iterate through the file list deserialising the JSON to load the external command.
        //
        foreach ( String fileName in fileNames )
        {
          Evado.UniForm.Model.EuCommand newCommand = Evado.Model.EvStatics.Files.readJsonFile<Evado.UniForm.Model.EuCommand> (
            Global.StaticDataFilePath, fileName );

          String commandKey = fileName.Replace ( extension, String.Empty );
          commandKey = commandKey.ToLower ( );

          if ( newCommand != null )
          {
            Global.LogGlobal ( "Key {0} >> {1}.", commandKey, newCommand.getAsString ( false, false ) );

            Global.ExternalCommands.Add ( commandKey.ToLower ( ), newCommand );
          }
        }
      }

      Global.LogGlobal ( "External Command count: " + Global.ExternalCommands.Count );

      Global.LogMethodEnd ( "LoadExternalCommands" );


    }//ENd LoadExternalCommands method

    // ==================================================================================
    /// <summary>
    /// This static method concatinates a relative and root URl depending upon
    /// whether the relative URl contains a root domain name.
    /// </summary>
    /// <param name="RootUrl">String: root url </param>
    /// <param name="RelativeUrl">String relative url</param>
    /// <returns>String: contatinated url.</returns>
    // -----------------------------------------------------------------------------------
    public static String concatinateHttpUrl ( String RootUrl, String RelativeUrl )
    {
      RelativeUrl = RelativeUrl.ToLower ( );
      if ( RelativeUrl.Contains ( "http:" ) == false
        && RelativeUrl.Contains ( "https:" ) == false )
      {
        RelativeUrl = RootUrl + RelativeUrl;
      }

      return RelativeUrl;
    }

    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region Session Start step

    protected void Session_Start ( Object sender, EventArgs e )
    {
      // 
      // Initialise the methods variables and objects.
      // 
      Global.LogGlobalMethod ( "Session_Start event" );
      String stUserId = String.Empty;
      try
      {
        stUserId = User.Identity.Name;

        Global.LogGlobalDebug ( "Network User Id: '" + User.Identity.Name + "'" );
        Global.LogGlobalDebug ( "Network Roles: " );
        string roles = String.Empty;
        foreach ( string role in Roles.GetRolesForUser ( ) )
        {
          if ( roles != String.Empty )
          {
            roles += ";";
          }
          roles += role;
        }

        Global.LogGlobalDebug ( "User Roles: " + roles );

        Session [ Global.SESSION_ROLES ] = roles;

        stUserId = Evado.Model.EvUserProfileBase.removeUserIdDomainName ( User.Identity.Name );
        Session [ Global.SESSION_USER_ID ] = stUserId;

        Global.LogGlobal ( "Evado.UniForm.Service.Session_Start event method. FINISHED" );
      }
      catch ( Exception Ex )
      {
        Global.LogGlobal ( "Domain User: " + stUserId );
        Global.LogGlobal ( Ex.ToString ( ) );

        EventLog.WriteEntry ( EventLogSource, Global._ClientLog.ToString ( ), EventLogEntryType.Error );

      } // Close catch   

    }// Close Session_Start Event method.

    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region Application Begin Request step

    protected void Application_BeginRequest ( Object sender, EventArgs e )
    {

    }

    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region Application End Request step

    protected void Application_EndRequest ( Object sender, EventArgs e )
    {
    }

    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region Application Authenticate Request step

    protected void Application_AuthenticateRequest ( Object sender, EventArgs e )
    {

    }

    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region Session End step

    protected void Session_End ( Object sender, EventArgs e )
    {
      String UserId = (String) Session [ Global.SESSION_USER_ID ];

      EventLog.WriteEntry ( EventLogSource, "User : " + UserId + " had logged out of the application", EventLogEntryType.Information );

    }

    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region Application End step

    protected void Application_End ( Object sender, EventArgs e )
    {
      try
      {
        string EventDescription = "Evado Holdings Pty. Ltd. Evado Holdings Pty. Ltd. Application End";
        /// 
        /// Write an event entry when the application ends.
        /// 
        //ApplicationEvents.LogAction (
        //  EventDescription, BsApplicationEvent.Application.End, String.Empty, "Evado Holdings Pty. Ltd. Executive" );
        /// 
        /// Write an event entry when the application ends.
        /// 
        EventLog.WriteEntry ( EventLogSource, EventDescription, EventLogEntryType.Information );
      }
      catch ( Exception Ex )
      {
        EventLog.WriteEntry ( EventLogSource, Ex.ToString ( ), EventLogEntryType.Error );
      } // Close catch   
    }
    #endregion

    #region Static Application log methods

    //
    // Define the debug lot string builder.
    //
    private static System.Text.StringBuilder _ClientLog = new System.Text.StringBuilder ( );

    private const String CONST_SERVICE_LOG_FILE_NAME = @"web-client-log-";

    //  =================================================================================
    /// <summary>
    ///   This static method removes a user from the online user list.
    /// 
    /// </summary>
    //   ---------------------------------------------------------------------------------
    public static void LogValue ( String Value )
    {
      Global._ClientLog.AppendLine ( Value );

      if ( Global.DebugLogOn == true )
      {
        Global._DebuLog.AppendLine ( Value );
      }
    }

    //  =================================================================================
    /// <summary>
    ///   This static method removes a user from the online user list.
    /// </summary>
    //   ---------------------------------------------------------------------------------
    private static void LogGlobalMethod ( String Value )
    {
      string logValue = Evado.Model.EvStatics.CONST_METHOD_START
        + DateTime.Now.ToString ( "dd-MM-yy hh:mm:ss" ) + ": "
        + "Evado.UniForm.Webclient.Global." + Value + " Method";

      Global.LogValue ( logValue );

    }

    //  =================================================================================
    /// <summary>
    ///   This static method removes a user from the online user list.
    /// 
    /// </summary>
    //   ---------------------------------------------------------------------------------
    private static void LogGlobal ( String Value )
    {
      string logValue = DateTime.Now.ToString ( "dd-MM-yy hh:mm:ss" ) + " Global: "
        + Value;

      Global.LogValue ( logValue );
    }

    // ==================================================================================
    /// <summary>
    /// This method appendes debuglog string to the debug log for the class and adds
    /// a new line at the end of the text.
    /// </summary>
    /// <param name="Format">String: format text.</param>
    /// <param name="Arguments">Array of objects as parameters.</param>
    // ----------------------------------------------------------------------------------
    private static void LogGlobal ( String Format, params object [ ] Arguments )
    {
      string logValue = String.Format ( DateTime.Now.ToString ( "dd-MM-yy hh:mm:ss" ) + " Global: "
        + String.Format ( Format, Arguments ) );

      Global.LogValue ( logValue );
    }

    //  =

    //  =================================================================================
    /// <summary>
    ///   This static method removes a user from the online user list.
    /// 
    /// </summary>
    //   ---------------------------------------------------------------------------------
    private static void LogMethodEnd ( String Value )
    {
      String value = Evado.Model.EvStatics.CONST_METHOD_END;

      value = value.Replace ( " END OF METHOD ", " END OF " + Value + " METHOD " );

      Global.LogGlobal ( value );
    }

    //  =================================================================================
    /// <summary>
    ///   This static method removes a user from the online user list.
    /// 
    /// </summary>
    //   ---------------------------------------------------------------------------------
    public static void OutputClientLog ( )
    {

      String stContent =  Global._ClientLog.ToString ( );

      String logFileName = CONST_SERVICE_LOG_FILE_NAME
        + DateTime.Now.ToString ( "yy-MM" ) + ".log";

      if ( Global.EnableDetailedLogging == true )
      {
        logFileName = CONST_SERVICE_LOG_FILE_NAME
         + DateTime.Now.ToString ( "yy-MM-dd" ) + ".log";

        Evado.Model.EvStatics.Files.saveFileAppend ( Global.LogFilePath + logFileName, stContent );
      }
      else
      {
        Evado.Model.EvStatics.Files.saveFile ( Global.LogFilePath + logFileName, stContent );
      }

      Global._ClientLog = new System.Text.StringBuilder ( ); 

    }//END writeOutDebugLog method

    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region Static Debug log methods

    private static System.Text.StringBuilder _DebuLog = new System.Text.StringBuilder ( );

    private const String CONST_DEBUG_LOG_FILE_NAME = @"web-client-debug-";

    public static String Debuglog = Global._DebuLog.ToString ( );

    // =================================================================================
    /// <summary>
    /// This method clears the debug log file.
    /// </summary>
    // ----------------------------------------------------------------------------------
    public static void ClearDebugLog ( )
    {
      //
      // Define the filename
      //
      String LogFileName = Global.LogFilePath + CONST_DEBUG_LOG_FILE_NAME
        + DateTime.Now.ToString ( "yy-MM" ) + ".log";

      //
      // IF Debug is turned off exit method.
      //
      if ( Global.DebugLogOn == false )
      {
        return;
      }

      Global._DebuLog.Clear ( );

      // 
      // Open the stream to the file.
      // 
      System.IO.File.Delete ( LogFileName );

    }

    //  =================================================================================
    /// <summary>
    ///   This static method removes a user from the online user list.
    /// 
    /// </summary>
    //   ---------------------------------------------------------------------------------
    public static void LogDebugValue ( String Value )
    {
      if ( Global.DebugLogOn == true )
      {
        Global._DebuLog.AppendLine ( Value );
      }
    }

    //  =================================================================================
    /// <summary>
    ///   This static method removes a user from the online user list.
    /// 
    /// </summary>
    //   ---------------------------------------------------------------------------------
    private static void LogGlobalDebug ( String Value )
    {
      string logValue = DateTime.Now.ToString ( "dd-MM-yy hh:mm:ss" ) + ":"
       + "Global:" + Value;

      if ( Global.DebugLogOn == true )
      {
        Global._DebuLog.AppendLine ( logValue );
      }
    }

    //  =================================================================================
    /// <summary>
    ///   This static method removes a user from the online user list.
    /// 
    /// </summary>
    //   ---------------------------------------------------------------------------------
    public static void OutputtDebugLog ( )
    {
      //
      // Define the filename
      //
      String logFileName = CONST_DEBUG_LOG_FILE_NAME
        + DateTime.Now.ToString ( "yy-MM" ) + ".log";

      //
      // IF Debug is turned off exit method.
      //
      if ( Global.DebugLogOn == false )
      {
        return;
      }

      //
      // if the debug log path is defined output the debug log to the given path.
      //
      if ( Global.TempPath == String.Empty )
      {
        return;
      }

      //
      // Output the debug log to debug log page.
      //
      String stContent = String.Empty;

      if ( Global._DebuLog.Length == 0 )
      {
        stContent = 
          String.Format( "EVADO UniFORM Web Client ASP.NET - DEBUG LOG\r\n"
          + "Saved at: {0} \r\n No Debug Content",  
          DateTime.Now.ToString ( "dd MMM yyyy HH:mm:ss" ) );
      }
      else
      {
        stContent =
          String.Format( "EVADO UniFORM Web Client ASP.NET - DEBUG LOG\r\n"
          + "Saved at: {0} \r\n{1}",  
          DateTime.Now.ToString ( "dd MMM yyyy HH:mm:ss" ),
           Global._DebuLog.ToString ( ) );
      }

      Evado.Model.EvStatics.Files.saveFile ( Global.LogFilePath + logFileName, stContent );

    }//END writeOutDebugLog method

    //  =================================================================================
    /// <summary>
    ///   This static method removes a user from the online user list.
    /// 
    /// </summary>
    //   ---------------------------------------------------------------------------------
    public static void OutputtDebugLog_SAVE ( )
    {
      //
      // Define the filename
      //
      String logFileName = CONST_DEBUG_LOG_FILE_NAME
        + "SAVE_" + DateTime.Now.ToString ( "yy-MM" ) + ".log";

      //
      // IF Debug is turned off exit method.
      //
      if ( Global.DebugLogOn == false )
      {
        return;
      }

      //
      // if the debug log path is defined output the debug log to the given path.
      //
      if ( Global.TempPath == String.Empty )
      {
        return;
      }

      //
      // Output the debug log to debug log page.
      //
      String stContent = String.Empty;

      if ( Global._DebuLog.Length == 0 )
      {
        stContent = "EVADO UniFORM Web Client ASP.NET - DEBUG LOG\r\n"
          + "Saved: " + DateTime.Now.ToString ( "dd MMM yyyy HH:mm:ss" )
          + "No Debug Content</p>";
      }
      else
      {
        stContent += "EVADO  UniFORM Web Client ASP.NET - DEBUG LOG\r\n"
          + "Saved: " + DateTime.Now.ToString ( "dd MMM yyyy HH:mm:ss" )
           + Global._DebuLog.ToString ( );
      }

      stContent = Evado.Model.EvStatics.getHtmlAsString ( stContent );


      Evado.Model.EvStatics.Files.saveFile ( Global.LogFilePath + logFileName, stContent );

    }//END writeOutDebugLog method


    #endregion

    #region Required Designer Variable
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public Global ( )
    {
      InitializeComponent ( );
    }

    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region Web Form Designer generated code
    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent ( )
    {
      this.components = new System.ComponentModel.Container ( );
    }
    #endregion
  }
}

