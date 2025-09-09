/***************************************************************************************
 * <copyright file="webclinical\default.aspx.cs" company="EVADO HOLDING PTY. LTD.">
 *     
 *      Copyright (c) 2011 - 2025 EVADO HOLDING PTY. LTD.  All rights reserved.
 *     
 *      The use and distribution terms for this software are contained in the file
 *      named license.txt, which can be found in the root of this distribution.
 *      By using this software in any fashion, you are agreeing to be bound by the
 *      terms of this license.
 *     
 *      You must not remove this notice, or any other, from this software.
 *     
 * </copyright>
 * 
 * Description: 
 *  This class contains the code behind functions for the default clinical web site
 *
 ****************************************************************************************/

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Web;
using System.Web.Security;
using System.IO;
using System.Text;
using System.Net.Http;

///Evado. namespace references.

using Evado.UniForm.Web;
using Evado.UniForm.Model;
using Evado.Model;

namespace Evado.UniForm.WebClient
{
  /// <summary>
  /// This is the code behind class for the home page.
  /// </summary>
  public partial class DefaultPage : EvPersistentPageState
  {
    #region Class variable initialisations

    private const string SESSION_USER = "EUWC_SESSION";


    private const string CONST_VIDEO_SUFFIX = "_VIDEO";

    private readonly string Css_Class_Field_Group = "field-group large";
    private readonly string Css_Class_Group_Title = "group-title";
    private readonly string Css_Class_Field_Group_Container = "field-group-container";

    public const string CONST_FIELD_LOWER_SUFFIX = "_Lower";
    public const string CONST_FIELD_UPPER_SUFFIX = "_Upper";
    private const float WidthPixelFactor = 8F;

    private EuClientSession UserSession = new EuClientSession ( );

    Evado.Model.EvEventCodes LastEventCode = Evado.Model.EvEventCodes.Ok;

    private int iWindowWidthPixels = 0;

    private float bodyWidthPixels = 0;


    ///*********************************************************************************
    #endregion

    #region page event methods.

    // =====================================================================================
    /// <summary>
    /// Page_Load event method
    /// 
    /// Description:
    /// Load the web page event method 
    /// 
    /// </summary>
    /// <param name="sender">Event object</param>
    /// <param name="E">Event arguments</param>
    // ---------------------------------------------------------------------------------
    protected void Page_Load( object sender, System.EventArgs E )
    {
      Global.WriteToEventLog ( this.User.Identity.Name,
        "Evado.UniForm.AdminClient.DefaultPage.Page_Load event method. IsNewSession " + this.Session.IsNewSession,
       System.Diagnostics.EventLogEntryType.Information );

      Global.ClearDebugLog ( );
      Global.LogAppendGlobal ( );
      this.LogMethod ( "Page_Load event" );
      this.LogDebug ( "EnablePageHistory: " + Global.EnablePageHistory ); try
      {
        this.LogValue ( "UserHostAddress: " + Request.UserHostAddress );
        this.LogDebug ( "UserHostName: " + Request.UserHostName );

        this.LogDebug ( "LogonUserIdentity IsAuthenticated: {0}.", Request.LogonUserIdentity.IsAuthenticated );
        this.LogDebug ( "LogonUserIdentity Name: {0}.", Request.LogonUserIdentity.Name );
        this.LogDebug ( "User.Identity.Name: {0}.", User.Identity.Name );
        this.LogDebug ( "Session.IsNewSession: {0}.", this.Session.IsNewSession );
        this.LogDebug ( "Authentication Type:{0}.", Global.AuthenticationMode );
        this.LogDebug ( "Session.Timeout: {0}.", this.Session.Timeout  );

        if ( this.Session.IsNewSession == true )
        {
          this.UserSession.SessionTimeDateStamp = DateTime.Now;
        }

        // 
        // Initialise the method variables and objects.
        // 
        this.InitialiseGlobalVariables ( );

        //
        // load the session variables.
        //
        this.LoadSessionVariables ( );

        //
        // Process post back events.
        //
        if ( this.IsPostBack == true )
        {
          this.GetPostBackPageCommand ( );
        }
        else
        {
          //
          // Process non post back events.
          //
          this.GetRequestPageCommand ( );

          if ( Global.AuthenticationMode == System.Web.Configuration.AuthenticationMode.Windows
            && this.UserSession.ServerStatus != EuAppData.StatusCodes.Login_Authenticated )
          {
            this.LogDebug ( "Windows Authentication" );

            this.UserSession.PageCommand = new Evado.UniForm.Model.EuCommand ( );
            this.UserSession.PageCommand.Id = Guid.NewGuid ( );
            this.UserSession.PageCommand.Type = Evado.UniForm.Model.EuCommandTypes.Network_Login_Command;
            this.UserSession.UserId = Evado.Model.EvStatics.removeDomainName ( User.Identity.Name );
            this.UserSession.PageCommand.AddParameter (
              Evado.UniForm.Model.EuStatics.PARAMETER_LOGIN_USER_ID,
              this.UserSession.UserId );
          }
        }

        //
        // Read in the Command from the post back event.
        //
        this.LogDebug ( "CURRENT PageCommand: " + this.UserSession.PageCommand.getAsString ( false, true ) );
        this.LogDebug ( "fsLoginBox.Visible: " + this.fsLoginBox.Visible );
        //
        // Send the anonymous command and display the returned page.
        //
        this.LogDebug ( "PageCommand.Type: " + this.UserSession.PageCommand.Type );
        switch ( this.UserSession.PageCommand.Type )
        {
          case Evado.UniForm.Model.EuCommandTypes.Anonymous_Command:
          {
            this.LogDebug ( "Anonymous_Command" );

            //
            // Send the Command to the server.
            //
            this.SendPageCommand ( );

            this.LogValue ( "Commence page generation" );
            //
            // Generate the page layout.
            //
            this.GeneratePage ( );
            break;

          }
          case Evado.UniForm.Model.EuCommandTypes.Network_Login_Command:
          {
            this.LogDebug ( "Network Login_Command" );

            this.SendWindowsLoginCommand ( );
            break;
          }
          case Evado.UniForm.Model.EuCommandTypes.Login_Command:
          {
            this.LogDebug ( "Login_Command" );

            this.RequestLogin ( );
            break;
          }

          default:
          {
            if ( this.fsLoginBox.Visible == false )
            {
              //
              // Update the Command with page data objects.
              //
              this.GetCommandParameters ( );

              this.LogDebug ( "CURRENT PageCommand: " + this.UserSession.PageCommand.getAsString ( false, true ) );

              //
              // Send the Command to the server.
              //
              this.SendPageCommand ( );

              //this.SendFileRequest ( "ross-evado.png", "image/png" );

              //this.SendFileRequest ( "ross-home-page.png", "image/png" );

              this.LogDebug ( "LogoFilename: " + this.UserSession.AppData.LogoFilename );

              //
              // The client recieves a login request to display the login page.
              //
              if ( this.UserSession.AppData.Status == Evado.UniForm.Model.EuAppData.StatusCodes.Login_Request )
              {
                if ( Global.AuthenticationMode == System.Web.Configuration.AuthenticationMode.Windows )
                {
                  this.LogValue ( "WINDOW AUTHENTICATION REQUEST LOGIN" );
                  this.SendWindowsLoginCommand ( );
                }
                else
                {
                  this.LogValue ( "REQUEST LOGIN" );
                  this.RequestLogin ( );
                }
              }
              else
              {
                this.LogValue ( "Commence page generation" );

                //
                // Generate the page layout.
                //
                this.GeneratePage ( );
              }
              // 
              // Write footer
              // 
              this.litCopyright.Text = Global.AssemblyAttributes.Copyright;
              this.litFooterText.Text = EuLabels.Footer_Text;
              this.litVersion.Text = "Version: " + Global.AssemblyAttributes.FullVersion;

              //
              // output the debug serialisations
              //
              this.OutputSerialisedData ( );
            }

            break;
          }//END default case

        }//END switch statement

      } // End Try
      catch ( Exception Ex )
      {
        this.litErrorMessage.Text = "Error Event opening this page. "
          + Evado.Model.EvStatics.getExceptionAsHtml ( Ex );

        EvEventLog.LogPageError ( this, Evado.Model.EvStatics.getException ( Ex ) );

        this.LogValue ( "PAGE GENERATION ERROR: " + Evado.Model.EvStatics.getException ( Ex ) );

      } // End catch.

      this.LogValue ( "Page generation completed." );

      this.SaveSessionVariables ( );

      this.LogMethodEnd ( "Page_Load" );

      //
      // write out the debug log.
      //
      Global.OutputtDebugLog ( );
      //
      // write out the client log.
      //
      Global.OutputClientLog ( );

      Global.ClearLogs ( );

    }//END Page_Load event method

    ///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region  private methods

    // ==================================================================================	
    /// <summary>
    /// Description:
    ///	this method set value to session variables
    ///	
    /// </summary>
    // --------------------------------------------------------------------------------
    public void InitialiseGlobalVariables( )
    {
      this.LogMethod ( "initialiseGlobalVariables" );
      // 
      // Initialise the method variables and objects.
      // 
      this.litPageContent.Text = String.Empty;
      this.litHeaderTitle.Text = String.Empty;
      this.litExitCommand.Text = String.Empty;
      this.litCommandContent.Text = String.Empty;
      this.litHistory.Text = String.Empty;
      this.litPageMenu.Text = String.Empty;
      this.UserSession.CommandGuid = Guid.Empty;
      this.litCommandContent.Visible = true;
      this.PagedGroups.Visible = false;

      //
      // process post back false steps.
      //
      if ( IsPostBack == false )
      {
        this.LogMethod ( "IsPostBack == FALSE " );
        this.LogDebug ( "Global.WebServiceUrl: " + Global.WebServiceUrl );
        this.LogDebug ( "Global.Debug " + Global.DebugLogOn );
        this.LogDebug ( "Global.DisplaySerialisation: " + Global.DisplaySerialisation );
        this.fldPassword.Value = String.Empty;

        this.litSerialisedLinks.Visible = Global.DisplaySerialisation;

        //
        // Initialise the Command history list.
        //
        this.UserSession.InitialiseHistory ( );

        this.__CommandId.Value = EuStatics.CONST_LOGIN_COMMAND_ID.ToString ( );

        this.litPageContent.Visible = true;
        if ( Global.EnablePageHistory == true )
        {
          this.litHistory.Visible = true;
        }
        if ( Global.EnablePageMenu == true )
        {
          this.litPageMenu.Visible = true;
        }

        this.LogDebug ( "END IsPostBack == FALSE " );
      }
      else
      {
        if ( this.windowWidth.Value != String.Empty )
        {
          this.iWindowWidthPixels = EvStatics.getInteger ( this.windowWidth.Value );

          this.bodyWidthPixels = this.iWindowWidthPixels * 0.98F;
        }
      }

      if ( Global.AuthenticationMode != System.Web.Configuration.AuthenticationMode.Windows )
      {
        this.LogDebug ( "NOT Windows Authentication" );

        this.UserSession.PageCommand = new Evado.UniForm.Model.EuCommand ( );
        this.UserSession.UserId = String.Empty;
        this.UserSession.Password = String.Empty;
      }

      //
      // get the base Url for the page.
      //
      this.UserSession.PageUrl = this.Request.RawUrl;

      if ( this.UserSession.PageUrl.Contains ( "?" ) == true )
      {
        int intCount = this.Request.RawUrl.IndexOf ( '?' );
        this.UserSession.PageUrl = this.Request.RawUrl.Substring ( 0, intCount );
      }
      this.LogDebug ( "RawUrl: " + this.UserSession.PageUrl );

    }//END initialiseGlobalVariables method

    // ==================================================================================	
    /// <summary>
    /// Description:
    ///	this method set value to session variables
    ///	
    /// </summary>
    // --------------------------------------------------------------------------------
    public void LoadSessionVariables( )
    {
      this.LogMethod ( "loadSessionVariables" );
      //
      // Retrieve the application data object.
      //
      if ( Session [ SESSION_USER ] != null )
      {
        this.UserSession = ( EuClientSession ) Session [ SESSION_USER ];
      }

      this.LogDebug ( "ApplicationData.Id: " + this.UserSession.AppData.Id );
      this.LogDebug ( "SessionId: " + this.UserSession.ServerSessionId );
      this.LogDebug ( "UserNetworkId: " + this.UserSession.UserId );
      this.LogDebug ( "Password: " + this.UserSession.Password );
      this.LogDebug ( "PageCommand: " + this.UserSession.PageCommand.getAsString ( false, false ) );
      this.LogDebug ( "Command History length: " + this.UserSession.CommandHistoryList.Count );
      this.LogDebug ( "Icon list length: " + this.UserSession.IconList.Count );
      this.LogDebug ( "SessionTimeDateStamp: {0}.", this.UserSession.SessionTimeDateStamp.ToString ( "dd-MM-yy HH:mm:ss" ) );

      this.LogMethodEnd ( "loadSessionVariables" );

    }//END loadSessionVariables method

    // ==================================================================================	
    /// <summary>
    /// Description:
    ///	this method set value to session variables
    ///	
    /// </summary>
    // --------------------------------------------------------------------------------
    public void SaveSessionVariables( )
    {
      this.LogMethod ( "SaveSessionVariables" );

      this.LogDebug ( "ApplicationData.Id: " + this.UserSession.AppData.Id );
      this.LogDebug ( "SessionId: " + this.UserSession.ServerSessionId );
      this.LogDebug ( "UserNetworkId: " + this.UserSession.UserId );
      this.LogDebug ( "Password: " + this.UserSession.Password );
      this.LogDebug ( "PageCommand: " + this.UserSession.PageCommand.getAsString ( false, false ) );
      this.LogDebug ( "Command History length: " + this.UserSession.CommandHistoryList.Count );
      this.LogDebug ( "Icon list length: " + this.UserSession.IconList.Count );

      //
      // Save the session data object.
      //
      this.Session [ SESSION_USER ] = this.UserSession;

      this.LogMethodEnd ( "SaveSessionVariables" );

    }//END SaveSessionVariables method

    // ==================================================================================
    /// <summary>
    /// This method send the Command back to the server objects.
    /// </summary>
    // ---------------------------------------------------------------------------------
    private void SendPageCommand( )
    {
      Global.WriteToEventLog ( this.User.Identity.Name,
        "Evado.UniForm.AdminClient.DefaultPage.SendPageCommand method",
       System.Diagnostics.EventLogEntryType.Information );
      this.LogMethod ( "sendPageCommand" );
      this.LogValue ( "DebugLogOn {0}.", Global.DebugLogOn );
      this.LogDebug ( "Sessionid: {0}.", this.UserSession.ServerSessionId );
      this.LogDebug ( "User NetworkId: {0}.", this.UserSession.UserId );
      this.LogDebug ( "AppDate Url: {0}.", this.UserSession.AppData.Url );
      this.LogDebug ( "Global.ClientVersion: {0}.", Global.ClientVersion );
      this.LogDebug ( "GetRequestHeader 'Host: '{0}'. ", this.GetRequestHeader ( "Host" ) );

      this.UserSession.ClientVersion = Global.ClientVersion;

      //
      // Initialise the service client class for the transaction.
      //
      Evado.ServiceClients.EuServiceClients serviceClient = new ServiceClients.EuServiceClients ( )
      { 
        HttpClient = Global.HttpClient,
        UserSession = this.UserSession,
        WebServiceUrl = Global.WebServiceUrl,
        FileServiceUrl = Global.FileServiceUrl,
        StaticImageUrl = Global.StaticImageUrl,
        TempUrl = Global.TempUrl,
        TempPath = Global.TempPath,
        DebugLogOn = Global.DebugLogOn,
      };


      // Request.UserHostName
      // Add the header data
      //

      this.UserSession.PageCommand.SetHeaderValue (
        Evado.UniForm.Model.EuCommandHeaderParameters.UserId,
        this.UserSession.UserId );

      this.UserSession.PageCommand.SetHeaderValue (
        Evado.UniForm.Model.EuCommandHeaderParameters.DeviceId,
        Evado.UniForm.Model.EuStatics.CONST_WEB_CLIENT );

      this.UserSession.PageCommand.SetHeaderValue (
        Evado.UniForm.Model.EuCommandHeaderParameters.Client_Url,
        this.GetRequestHeader ( "Host" ) );

      this.UserSession.PageCommand.SetHeaderValue (
        Evado.UniForm.Model.EuCommandHeaderParameters.OSVersion,
        Evado.UniForm.Model.EuStatics.CONST_WEB_NET_VERSION );

      this.UserSession.PageCommand.SetHeaderValue (
        Evado.UniForm.Model.EuCommandHeaderParameters.User_Url,
        Request.UserHostName );

      this.UserSession.PageCommand.SetHeaderValue (
        Evado.UniForm.Model.EuCommandHeaderParameters.DateTime,
        DateTime.Now.ToString ( "dd MMM yyyy HH:mm:ss" ) );

      this.LogValue ( "SENT: PageCommand: " + this.UserSession.PageCommand.getAsString ( false, false ) );

      //
      // Send the command to the service.
      //
      var result = serviceClient.SendPageCommand ( this.UserSession.PageCommand );

      if( result != EvEventCodes.Ok)
      {
        this.UserSession.AppData = new Evado.UniForm.Model.EuAppData ( );
        this.UserSession.AppData.Id = Guid.NewGuid ( );
        this.UserSession.AppData.Page.Id = this.UserSession.AppData.Id;
        this.UserSession.AppData.Page.Title = "Service Access Error.";
        Evado.UniForm.Model.EuGroup group = this.UserSession.AppData.Page.AddGroup (
          "Service Access Error Report", false );

        if ( Global.DebugLogOn == true )
        {
          group.Description = "Web Service URL: " + serviceClient.ServiceUrl;
        }
        else
        {
          group.Description = "Error Occured Accessing the Cllient Service - contact your administrator.";
        }
      }

      Global.LogValue ( serviceClient.Log );
    

      this.LogMethodEnd ( "sendPageCommand" );

    }//END sendPageCommand method

    // ==================================================================================
    /// <summary>
    /// This method sends a request to the file service.
    /// </summary>
    /// <param name="filename">String: file name of the file to be up loaded.</param>
    /// <param name="MimeType">String: the mime type for the file.</param>
    // ---------------------------------------------------------------------------------
    private Evado.Model.EvEventCodes SendFileRequest( String filename, String MimeType )
    {
      this.LogMethod ( "SendFileRequest" );
      this.LogValue ( "filename {0}, MimeType {1}.", filename, MimeType );

      //
      // Initialise the service client class for the transaction.
      //
      Evado.ServiceClients.EuServiceClients serviceClient = new ServiceClients.EuServiceClients ( )
      {
        HttpClient = Global.HttpClient,
        UserSession = this.UserSession,
        WebServiceUrl = Global.WebServiceUrl,
        FileServiceUrl = Global.FileServiceUrl,
        StaticImageUrl = Global.StaticImageUrl,
        TempUrl = Global.TempUrl,
        TempPath = Global.TempPath,
        DebugLogOn = Global.DebugLogOn,
      };


      var result = serviceClient.SendFileRequest ( filename, MimeType );

      Global.LogValue ( serviceClient.Log );

      if( result != EvEventCodes.Ok )
      {
        this.UserSession.AppData = new Evado.UniForm.Model.EuAppData ( );
        this.UserSession.AppData.Id = Guid.NewGuid ( );
        this.UserSession.AppData.Page.Id = this.UserSession.AppData.Id;
        this.UserSession.AppData.Page.Title = "Service Access Error.";
        Evado.UniForm.Model.EuGroup group = this.UserSession.AppData.Page.AddGroup (
          "Service Access Error Report", false );

        if ( Global.DebugLogOn == true )
        {
          group.Description = "Web Service URL: " + serviceClient.ServiceUrl;
        }
        else
        {
          group.Description = "Error Occured Accessing the File Service - contact your administrator.";
        }

        return result;
      }

      return EvEventCodes.Ok;
      /*
      //
      // Display a serialised instance of the object.
      //
      EuFile fileObject = new EuFile ( );
      string jsonData = String.Empty;
      string filePath = String.Empty;
      Evado.Model.EvEventCodes serviceStatus = Evado.Model.EvEventCodes.Ok;
      List<String> FileSegmentList = new List<string> ( );
      Newtonsoft.Json.JsonSerializerSettings jsonSettings = new Newtonsoft.Json.JsonSerializerSettings
      {
        NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore
      };

      try
      {
        fileObject.UserId = this.UserSession.UserId;
        fileObject.ClientSession = this.UserSession.AppData.SessionId;
        fileObject.FileName = filename;
        fileObject.MimeType = MimeType;
        fileObject.Action = EuFile.WebAction.Upload;

        filePath = Global.TempPath + fileObject.FileName;

        this.LogDebug ( "filePath: '{0}'. ", filePath );

        fileObject.FileData = System.IO.File.ReadAllBytes ( filePath );

        this.LogDebug ( "User: {0}, Filename; {1}, Mime: {2}, Data length {3}",
          fileObject.UserId,
          fileObject.FileName,
          fileObject.MimeType,
          fileObject.FileData.Length );

        //
        // serialise the Command prior to sending to the web service.
        //
        this.LogDebug ( "Serialising the PageComment object" );

        jsonData = Newtonsoft.Json.JsonConvert.SerializeObject ( fileObject );

        this.LogDebug ( "jsonData.Length  {0}.", jsonData.Length );

        if ( jsonData.Length < CONST_FILE_SEGMENT_LENGTH )
        {
          this.LogDebug ( "START: single-segment file transfer." );
          //
          // process the segment 
          //
          serviceStatus = this.SendFileSegment ( jsonData, 999 );

          this.LogDebug ( "serviceStatus: {0},", serviceStatus );

          if ( serviceStatus != Evado.Model.EvEventCodes.Ok )
          {
            this.LogMethodEnd ( "SendFileRequest" );
            return serviceStatus;
          }

          this.LogDebug ( "END: single-segment file transfer." );
        }
        else
        {
          this.LogDebug ( "START: multi-segment file transfer." );
          
          string end = jsonData.Substring ( jsonData.Length - 100 );
          this.LogDebug ( "file end: {0},", end );

          //
          // convert the file object json into segements of less than 40000 characters.
          //
          for ( int startIndex = 0 ; startIndex < jsonData.Length ; startIndex += CONST_FILE_SEGMENT_LENGTH )
          {
            int segmentLength = CONST_FILE_SEGMENT_LENGTH;

            if ( startIndex + segmentLength > jsonData.Length )
            {
              this.LogDebug ( "last segment found." );

              segmentLength = jsonData.Length - startIndex;
            }
            this.LogDebug ( "startIndex: {0}, segementLength {1}.", startIndex, segmentLength );

            string segmentData = jsonData.Substring ( startIndex, segmentLength );

            this.LogDebug ( "segment: length {0}, data {1}.", segmentData.Length, segmentData );

            int diff = startIndex + segmentLength - jsonData.Length;
            this.LogDebug ( "difference {0}.", diff );

            FileSegmentList.Add ( segmentData );
          }

          this.LogDebug ( "FileSegmentList.Count: {0}.", FileSegmentList.Count );

          StringBuilder testdata = new StringBuilder ( );
          for ( int segmentCount = 0 ; segmentCount < FileSegmentList.Count ; segmentCount++ )
          {
            testdata.Append ( FileSegmentList [ segmentCount ] );
            this.LogDebug ( "testdata.Length: {0}.", testdata.Length );

            //
            // calcuate the segment number.
            //
            int segment = segmentCount + 1;
            if ( segment == FileSegmentList.Count )
            {
              segment = 999;
            }
            this.LogDebug ( "segment: {0}.", segment );

            //
            // process the segment 
            //
            serviceStatus = this.SendFileSegment ( FileSegmentList [ segmentCount ], segment );

            this.LogDebug ( "serviceStatus: {0},", serviceStatus );

            if ( serviceStatus != Evado.Model.EvEventCodes.Ok
              && serviceStatus != Evado.Model.EvEventCodes.Uniform_File_Segement_Processed )
            {
              this.LogMethodEnd ( "SendFileRequest" );
              return serviceStatus;
            }

          }//End set segment iteration loop.

          this.LogDebug ( "END: multi-segment file transfer." );
        }//END multi-segment file transfer.

        //
        // if the response is OK and data is empty delete the file.
        //
        if ( serviceStatus == Evado.Model.EvEventCodes.Ok )
        {
          this.LogDebug ( "Deleting  {0}.", fileObject.FileName );

          // Evado.Model.EvStatics.Files.DeleteFile ( Global.TempPath, fileObject.FileName );
        }

        this.LogMethodEnd ( "SendFileRequest" );
        return serviceStatus;

      }
      catch ( Exception Ex )
      {
        this.litErrorMessage.Text = "Web Service Error. " + Evado.Model.EvStatics.getExceptionAsHtml ( Ex );

        this.LogDebug ( "Web Service Error. " + Evado.Model.EvStatics.getException ( Ex ) ); ;
      }
      this.LogMethodEnd ( "SendFileRequest" );

      return Evado.Model.EvEventCodes.Uniform_File_Service_Error;
      
  */
    }//END UploadFile method

    // ==================================================================================
    /// <summary>
    /// This method sends a request to the file service.
    /// </summary>
    /// <param name="SegementData">String: the file segement data.</param>
    /// <param name="Segment">String: thenfile segmenet.</param>
    // ---------------------------------------------------------------------------------
    private Evado.Model.EvEventCodes SendFileSegment( String SegementData, int Segment )
    {
      this.LogMethod ( "SendFileSegment" );
      this.LogDebug ( "Segement {0}, Data: {1}", Segment, SegementData );

      //
      // Display a serialised instance of the object.
      //
      string responseText = String.Empty;
      string WebServiceUrl = String.Format ( EuLabels.File_Service_Url_Template,
        Global.WebServiceUrl,
        EuStatics.APPLICATION_SERVICE_FILE_RELATIVE_URL,
        this.UserSession.UserId,
        Segment );
      string filePath = String.Empty;

      this.LogDebug ( "WebServiceUrl: '{0}'. ", WebServiceUrl );
      try
      {
        //
        // The post command 
        //
        responseText = this.SendPost ( WebServiceUrl, SegementData );

        if ( responseText == null
          || responseText == String.Empty )
        {
          this.LogDebug ( "responseText null." );
          this.LogMethodEnd ( "SendFileSegment" );

          return Evado.Model.EvEventCodes.Uniform_File_Service_Returned_Null;
        }

        Evado.Model.EvEventCodes serviceStatus = Evado.Model.EvStatics.parseEnumValue<Evado.Model.EvEventCodes> ( responseText );

        this.LogDebug ( "serviceStatus: {0},", serviceStatus );

        this.LogMethodEnd ( "SendFileSegment" );
        return serviceStatus;

      }
      catch ( Exception Ex )
      {
        this.LogDebug ( "Web Service Error. " + Evado.Model.EvStatics.getException ( Ex ) ); ;
      }
      this.LogMethodEnd ( "SendFileSegment" );

      return Evado.Model.EvEventCodes.Uniform_File_Service_Error;

    }//END SendFileSegment method

    // =================================================================================
    /// <summary>
    /// This methods sends a post to the web service.
    /// </summary>
    /// <param name="WebServiceUrl">String: The web service URI</param>
    /// <param name="PostContent">String: string content for the web service.</param>
    /// <returns>String: Response Text</returns>
    // ---------------------------------------------------------------------------------
    private String SendPost(
      String WebServiceUrl,
      String PostContent )
    {
      Global.WriteToEventLog ( this.User.Identity.Name,
        "Evado.UniForm.AdminClient.DefaultPage.SendPost method",
       System.Diagnostics.EventLogEntryType.Information );
      this.LogMethod ( "SendPost" );
      this.LogDebug ( "WebServiceUrl {0}", WebServiceUrl );
      //
      // Initialise the methods variables and objects.
      //
      String responseText = String.Empty;
      Uri uri = new Uri ( WebServiceUrl );

      var content = new StringContent ( PostContent, Encoding.UTF8, "application/json" );

      using ( var handler = new HttpClientHandler ( )
      {
        CookieContainer = this.UserSession.CookieContainer,
        UseCookies = true
      } )
      {
        using ( Global.HttpClient = new HttpClient ( handler ) )
        {
          using ( content )
          {
            HttpResponseMessage response = Global.HttpClient.PostAsync ( uri, content ).Result;

            this.LogDebug ( "response.StatusCode {0}.", response.StatusCode );
            if ( response.StatusCode != System.Net.HttpStatusCode.OK )
            {
              switch ( response.StatusCode )
              {
                case System.Net.HttpStatusCode.InternalServerError:
                {
                  LastEventCode = Evado.Model.EvEventCodes.WebServices_Internal_Server_Error;
                  break;
                }
                case System.Net.HttpStatusCode.NotFound:
                {
                  LastEventCode = Evado.Model.EvEventCodes.WebServices_Not_Found_Error;
                  break;
                }
                case System.Net.HttpStatusCode.BadRequest:
                {
                  LastEventCode = Evado.Model.EvEventCodes.WebServices_Bad_Request_Error;
                  break;
                }
                case System.Net.HttpStatusCode.Conflict:
                {
                  LastEventCode = Evado.Model.EvEventCodes.WebServices_Conflict_Error;
                  break;
                }
                case System.Net.HttpStatusCode.RequestEntityTooLarge:
                {
                  LastEventCode = Evado.Model.EvEventCodes.WebServices_Request_Entity_Too_Large_Error;
                  break;
                }
                case System.Net.HttpStatusCode.MethodNotAllowed:
                {
                  LastEventCode = Evado.Model.EvEventCodes.WebServices_Method_Not_Allowed_Error;
                  break;
                }
                case System.Net.HttpStatusCode.NoContent:
                {
                  LastEventCode = Evado.Model.EvEventCodes.WebServices_No_Content_Error;
                  break;
                }
                default:
                {
                  LastEventCode = Evado.Model.EvEventCodes.WebServices_General_Failure_Error;
                  break;
                }
              }//ENd switch statement

              Global.LogError ( "WebService URL {0}, raised error event {1}", WebServiceUrl, LastEventCode );
              this.LogMethodEnd ( "sendPost" );
              return null;
            }

            responseText = response.Content.ReadAsStringAsync ( ).Result;

          }//END using content
        }//END using httpClient
      }//END using handler

      this.LogMethodEnd ( "sendPost" );
      return responseText;
    }

    // =====================================================================================
    /// <summary>
    /// Description:
    ///  This method logs the user transaction sent to the service
    /// 
    /// </summary>
    // -------------------------------------------------------------------------------------
    private String GetRequestHeader( String HeaderKey )
    {
      //
      // Log the HTTP header elements
      //
      if ( this.Request.Headers.Keys.Count > 0 )
      {
        foreach ( string key in this.Request.Headers.Keys )
        {
          if ( key.ToLower ( ) == HeaderKey.ToLower ( ) )
          {
            return this.Request.Headers [ key ].ToString ( );
          }
        }
      }
      return String.Empty;

    }//END logUserTransaction method

    // ==================================================================================
    /// <summary>
    /// This method outputs the serialised data values.
    /// </summary>
    // ---------------------------------------------------------------------------------
    private void OutputSerialisedData( )
    {
      this.LogMethod ( "outSerialisedData" );
      this.LogDebug ( " Global.ApplicationPath: '" + Global.ApplicationPath + "' " );

      if ( Global.DisplaySerialisation == false )
      {
        this.LogDebug ( "serialisation is false" );

        this.LogMethodEnd ( "outSerialisedData " );
        return;
      }

      ///
      /// Display a serialised instance of the object.
      ///
      string serialisedText = String.Empty;
      Newtonsoft.Json.JsonSerializerSettings jsonSettings = new Newtonsoft.Json.JsonSerializerSettings
      {
        NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore
      };

      ///
      /// Define the error group.
      ///
      Evado.UniForm.Model.EuGroup serialisationGroup = new Evado.UniForm.Model.EuGroup ( "Serialisation", String.Empty, false );
      serialisationGroup.Layout = Evado.UniForm.Model.EuGroupLayouts.Full_Width;

      serialisedText = Evado.Model.EvStatics.SerialiseXmlObject<Evado.UniForm.Model.EuAppData> ( this.UserSession.AppData );

      /// 
      /// Open the stream to the file.
      /// 
      using ( StreamWriter sw = new StreamWriter ( Global.ApplicationPath + @"temp\serialised_application_data.xml" ) )
      {
        sw.Write ( serialisedText );

      }/// End StreamWrite


      Evado.UniForm.Model.EuField groupField = serialisationGroup.createHtmlLinkField (
        "lnkxmllad",
        "XML Serialised Application Data Object",
        "temp/serialised_application_data.xml" );

      serialisedText = Evado.Model.EvStatics.SerialiseXmlObject<Evado.UniForm.Model.EuCommand> ( this.UserSession.PageCommand );

      // 
      // Open the stream to the file.
      //
      using ( StreamWriter sw = new StreamWriter ( Global.ApplicationPath + @"temp\serialised_command.xml" ) )
      {
        sw.Write ( serialisedText );

      }/// End StreamWrite

      groupField = serialisationGroup.createHtmlLinkField ( "lnkXmlcmd", "XML Serialised Command Object", @"temp/serialised_command.xml" );

      ///
      /// Display a serialised instance of the objec.
      ///
      serialisedText = Newtonsoft.Json.JsonConvert.SerializeObject (
        this.UserSession.AppData,
        Newtonsoft.Json.Formatting.Indented,
        jsonSettings );


      /// 
      /// Open the stream to the file.
      /// 
      using ( StreamWriter sw = new StreamWriter ( Global.ApplicationPath + @"temp\serialised_application_data.json.txt" ) )
      {
        sw.Write ( serialisedText );

      }/// End StreamWrite

      groupField = serialisationGroup.createHtmlLinkField ( "lnkjsonad", "JSON Serialised Application Data Object", "temp/serialised_application_data.json.txt" );

      ///
      ///  JSON Command 
      ///
      serialisedText = Newtonsoft.Json.JsonConvert.SerializeObject ( this.UserSession.PageCommand );

      serialisedText = serialisedText.Replace ( "\r\n", "" );
      serialisedText = serialisedText.Replace ( "\\n", "" );
      serialisedText = serialisedText.Replace ( "\\r", "" );
      serialisedText = serialisedText.Replace ( "\r\n{,", "\r\n{,\r\n" );
      serialisedText = serialisedText.Replace ( "],", "],\r\n" );
      serialisedText = serialisedText.Replace ( "\"},", "\"},\r\n" );
      serialisedText = serialisedText.Replace ( ":[", ":[\r\n" );
      serialisedText = serialisedText.Replace ( "],", "],\r\n" );
      serialisedText = serialisedText.Replace ( "]}", "]\r\n}" );
      serialisedText = serialisedText.Replace ( ",\"", ",\r\n\"" );
      /// 
      /// Open the stream to the file.
      /// 
      using ( StreamWriter sw = new StreamWriter ( Global.ApplicationPath + @"\temp\json_serialised_command.txt" ) )
      {
        sw.Write ( serialisedText );

      }/// End StreamWrite

      groupField = serialisationGroup.createHtmlLinkField ( "lnkjsonCmd", "JSON Serialised Command Object", "temp/json_serialised_command.txt" );


      this.LogDebug ( "Serialiation field count:" + serialisationGroup.FieldList.Count );

      if ( serialisationGroup.FieldList.Count > 0 )
      {
        ///
        /// set the field set attributes.
        ///
        StringBuilder sbHtml = new StringBuilder ( );

        this.generateGroupHeader ( sbHtml, serialisationGroup, false );

        foreach ( Evado.UniForm.Model.EuField pageField in serialisationGroup.FieldList )
        {
          if ( pageField.Type == Evado.Model.EvDataTypes.Http_Link )
          {
            String stUrl = pageField.Value;
            String stFieldRowStyling = "class='group-row field layout-normal cf " + this.fieldBackgroundColorClass ( pageField ) + "' ";
            String stFieldTitleStyling = "style='' class='cell title cell-link-value cf'";

            this.LogDebug ( "Field URL: " + stUrl );

            sbHtml.Append ( "<div id='" + pageField.Id + "-row' " + stFieldRowStyling + " > \r\n" );

            sbHtml.Append ( "<div " + stFieldTitleStyling + "> "
             + "<span><strong><a href='" + stUrl + "' target='_blank' >" + pageField.Title + "</a></strong></span></div>\r\n" );

            ///
            /// Insert the field footer elemements
            ///
            this.createFieldFooter ( sbHtml, pageField );
          }
        }

        this.generateGroupFooter ( sbHtml );

        String stHtml = sbHtml.ToString ( );

        stHtml = stHtml.Replace ( Global.FileServiceUrl, "euws/" );

        this.litSerialisedLinks.Text = stHtml;
      }

      this.LogMethodEnd ( "outSerialisedData " );


    }//END outSerialisedData method

    ///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region  icon  methods

    // ==================================================================================

    /// <summary>
    /// This method searches through the page group fields to find a matching field..
    /// </summary>
    /// <param name="DataId">String: The html field Id.</param>
    /// <returns>Field object.</returns>
    // ---------------------------------------------------------------------------------
    private String AddIconHtml( String Icon )
    {
      //
      // Iterate through the page groups and fields to find the matching field.
      //
      foreach ( EuKeyValuePair valuePair in this.UserSession.IconList )
      {
        String key = valuePair.Key;
        key = key.ToLower ( );

        if ( key == Icon.ToLower ( ) )
        {
          return "<img src=\"" + valuePair + "\" alt=\"valuePair.Key\" />";

        }//END icon selection


      }//END icon list iteration.

      return String.Empty;

    }//END getField method

    ///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region  commands methods

    // ==================================================================================	
    /// <summary>
    /// Description:
    ///	this method set value to session variables
    ///	
    /// </summary>
    // --------------------------------------------------------------------------------
    public void GetPostBackPageCommand( )
    {
      this.LogMethod ( "getPageCommand" );
       
      //
      // read in the posted back Command id
      //
      this.ReadinCommandId ( );

      //
      // If the object is empty or reset is sent then refresh the object from the server.
      //
      if ( this.UserSession.CommandGuid == Guid.Empty )
      {
        this.LogDebug ( "Send empty command to the server. " );

        this.UserSession.CommandGuid = EuStatics.CONST_LOGIN_COMMAND_ID;
      }
      else
      {
        if ( this.UserSession.CommandGuid != this.UserSession.PageCommand.Id )
        {
          this.LogDebug ( "Get the new command object." );

          this.UserSession.PageCommand = this.GetCommandObject ( this.UserSession.CommandGuid );
        }
      }
      this.LogDebug ( "PageCommand: " + this.UserSession.PageCommand.getAsString ( false, true ) );

      this.LogMethodEnd ( "getPageCommand" );

    }//END getPageCommand method

    //==================================================================================	
    /// <summary>
    /// this method gets the command Id from the Request URL string.
    /// </summary>
    /// <returns>Bool: true = external command found.</returns>
    // --------------------------------------------------------------------------------
    private bool GetRequestPageCommand( )
    {
      this.LogMethod ( "GetRequestPageCommand" );
      // 
      // Extract the URL parameters and instantiate the local variables.
      // 
      int loop1;
      string Key, Value;

      this.LogDebug ( "Request.RequestType: {0}.", Request.RequestType );
      this.LogDebug ( "Request.Url: {0}.", Request.Url );
      this.LogDebug ( "Request.RawUrl: {0}.", Request.RawUrl );
      this.LogDebug ( "Request.Url: {0}.", Request.Url );
      this.LogDebug ( "Request.QueryString: {0}.", Request.QueryString.ToString ( ) );
      this.LogDebug ( "Request.QueryString.Count: {0}.", Request.QueryString.Count );
      // 
      // Load SpecialisationValueCollection object.
      // 
      NameValueCollection coll = Request.QueryString;

      this.LogDebug ( "Parameter Collection count: " + coll.Count );
      // 
      // Get names of all keys into a string array.
      // 
      String [ ] aKeys = coll.AllKeys;

      if ( aKeys.Length == 0 )
      {
        this.LogDebug ( "No query string parameters." );
        this.LogMethodEnd ( "ReadUrlParameters" );
        return false;
      }

      // 
      // loop through the key collection to extract the page parameters
      // 
      for ( loop1 = 0 ; loop1 < aKeys.Length ; loop1++ )
      {
        Key = Server.HtmlEncode ( aKeys [ loop1 ] ).ToString ( );
        String [ ] aValues = coll.GetValues ( aKeys [ loop1 ] );
        Value = Server.HtmlEncode ( aValues [ 0 ] ).ToString ( );

        this.LogDebug ( "Key: {0}, Value {1} ", Key, Value );

        string parameter = Key.ToLower ( );

        this.LogDebug ( "Parameter: {0} ", parameter );

        if ( Global.ExternalCommands.ContainsKey ( parameter ) == true )
        {
          Evado.UniForm.Model.EuCommand command = Global.ExternalCommands [ parameter ];

          Guid guid = Evado.Model.EvStatics.getGuid ( Value );

          this.LogDebug ( string.Format ( "Guid: {0} ", guid ) );

          if ( guid != Guid.Empty )
          {
            command.SetGuid ( guid );
            this.UserSession.PageCommand = command;
            this.UserSession.UserId = this.Session.SessionID;
            Session [ Evado.UniForm.Model.EuStatics.SESSION_USER_ID ] = this.UserSession.UserId;
            this.UserSession.ExternalCommand = this.UserSession.PageCommand;

          }//END guid found.
          continue;

        }//END external commands found
        else
        {
          if ( Key.ToLower ( ) == "id" )
          {
            Guid commandId = Evado.Model.EvStatics.getGuid ( Value );

            this.UserSession.PageCommand = this.GetCommandObject ( commandId );
            continue;
          }
        }

        if ( parameter == "cu_guid" )
        {
          Guid guid = Evado.Model.EvStatics.getGuid ( Value );

          this.LogDebug ( string.Format ( "Guid: {0} ", guid ) );

          this.UserSession.PageCommand.AddParameter ( "CUSTOMER_GUID", guid );

          this.UserSession.ExternalCommand = this.UserSession.PageCommand;
          continue;
        }

      }//END paraemter iteration loop

      this.LogDebug ( "Finished query parameter iteration loop." );

      this.LogDebug ( this.UserSession.PageCommand.getAsString ( false, true ) );

      this.LogMethodEnd ( "GetRequestPageCommand" );
      return false;

    }//END GetRequestPageCommand method.

    // ==================================================================================
    /// <summary>
    /// This method updates the web application with the form field values.
    /// </summary>
    // ---------------------------------------------------------------------------------
    private void ReadinCommandId( )
    {
      this.LogMethod ( "readinCommandId" );

      if ( this.__CommandId.Value.ToLower ( ) == "login" )
      {
        this.LogDebug ( "Request Login command" );
        this.UserSession.RequestLogin = true;
        this.requestLogout ( );

        this.LogMethodEnd ( "readinCommandId" );
        return;
      }

      try
      {
        if ( this.__CommandId.Value.Length == 36 )
        {
          this.LogDebug ( "Command value: " + this.__CommandId.Value );
          this.UserSession.CommandGuid = new Guid ( this.__CommandId.Value );
        }
      }
      catch
      {
        this.LogDebug ( "Command Id not a guid" );
        this.UserSession.CommandGuid = Guid.Empty;
      }

      this.LogDebug ( "Post back CommandId: " + this.UserSession.CommandGuid );
      this.LogMethodEnd ( "readinCommandId" );

    }//END readinCommandId method

    // ==================================================================================

    /// <summary>
    /// This method returns the page Command matching the Command Id passed to the 
    /// method from the Application data object.
    /// </summary>
    /// <param name="CommandId">GUID: Id for the Command to be retrieved</param>
    /// <returns>CliemtPageCommand object.</returns>
    // ---------------------------------------------------------------------------------
    private Evado.UniForm.Model.EuCommand GetCommandObject( Guid CommandId )
    {
      this.LogMethod ( "getCommandObject" );
      this.LogDebug ( "CommandId: " + CommandId );
      try
      {

        if ( CommandId == EuStatics.CONST_LOGIN_COMMAND_ID )
        {
          this.LogDebug ( "Commandid = LoginCommandId return empty command." );

          this.LogMethodEnd ( "getCommandObject" );
          return new Evado.UniForm.Model.EuCommand ( );
        }

        //
        // Look for a history Command.
        //
        Evado.UniForm.Model.EuCommand historyCommand = this.UserSession.GetHistoryCommand ( CommandId );

        if ( historyCommand.Id != Guid.Empty
          && historyCommand.Id != EuStatics.CONST_LOGIN_COMMAND_ID )
        {
          this.LogDebug ( "Return history command: " + historyCommand.Title );
          this.LogMethodEnd ( "getCommandObject" );
          return historyCommand;
        }

        //
        // if the exit Command then return the exit Command object.
        //
        if ( this.UserSession.AppData.Page.Exit != null )
        {
          if ( this.UserSession.AppData.Page.Exit.Id == CommandId )
          {
            this.LogDebug ( "Returning page exit command: " + this.UserSession.AppData.Page.Exit.Title );

            this.LogMethodEnd ( "getCommandObject" );
            return this.UserSession.AppData.Page.Exit;
          }
        }

        //
        // Iterate through the list to find the correct Command.
        //
        foreach ( Evado.UniForm.Model.EuCommand command in this.UserSession.AppData.Page.CommandList )
        {

          if ( command.Id == CommandId )
          {
            this.LogDebug ( "Returning page` command: " + command.Title );
            this.LogMethodEnd ( "getCommandObject" );
            return command;
          }
        }

        //
        // Iterate through the list group to find the correct Command.
        //
        foreach ( Evado.UniForm.Model.EuGroup group in this.UserSession.AppData.Page.GroupList )
        {
          //
          // Iterate through the list to find the correct Command.
          //
          foreach ( Evado.UniForm.Model.EuCommand command in group.CommandList )
          {
            this.LogDebug ( "Group {0}, command: id {1} - {2}", group.Title, command.Id, command.Title );

            if ( command.Id == CommandId )
            {
              this.LogDebug ( "Returning page group " + group.Title
                + " command: " + command.Title );

              this.LogMethodEnd ( "getCommandObject" );
              return command;
            }
          }//END iteration loop

        }//END iteration loop
      }
      catch ( Exception Ex )
      {
        this.LogDebug ( Evado.Model.EvStatics.getException ( Ex ) );
      }
      this.LogDebug ( "No command found." );
      this.LogMethodEnd ( "getCommandObject" );

      return new Evado.UniForm.Model.EuCommand ( );

    }//END getCommand method

    ///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region  panelled group methods.

    // =====================================================================================
    /// <summary>
    /// btnLogin_OnClick event method
    /// 
    /// Description:
    /// This method executes the user login event.
    /// 
    /// </summary>
    /// <param name="sender">Event object</param>
    /// <param name="E">Event arguments</param>
    // ---------------------------------------------------------------------------------
    protected void btnPageLeft_OnClick(
      object sender,
      System.EventArgs E )
    {
      this.LogMethod ( "btnPageLeft_OnClick event" );
    }

    // =====================================================================================
    /// <summary>
    /// btnLogin_OnClick event method
    /// 
    /// Description:
    /// This method executes the user login event.
    /// 
    /// </summary>
    /// <param name="sender">Event object</param>
    /// <param name="E">Event arguments</param>
    // ---------------------------------------------------------------------------------
    protected void btnPageRight_OnClick(
      object sender,
      System.EventArgs E )
    {
      this.LogMethod ( "btnPageRight_OnClick event" );
    }

    //*********************************************************************************
    #endregion.

    #region  Login methods

    // =====================================================================================
    /// <summary>
    /// Description:
    /// This method disiplays the user login.
    /// 
    /// </summary>
    // ---------------------------------------------------------------------------------
    private void RequestLogin( )
    {
      Global.WriteToEventLog ( this.User.Identity.Name,
        "Evado.UniForm.AdminClient.DefaultPage.RequestLogin method",
       System.Diagnostics.EventLogEntryType.Information );
      this.LogMethod ( "RequestLogin" );
      this.LogDebug ( "DefaultLogo {0}.", Global.DefaultLogoUrl );
      //
      // Initialise the methods variables and object.s
      //
      this.UserSession.InitialiseHistory ( );
      this.UserSession.AppData.Title = EuLabels.User_Login_Title;
      if ( Global.TitlePrefix != String.Empty )
      {
        this.Title = String.Format ( "{0}: {1}", Global.TitlePrefix, this.UserSession.AppData.Title );
      }
      else
      {
        this.Title = this.UserSession.AppData.Title ;
      }
      this.imgLogo.Src = Global.DefaultLogoUrl;

      this.fsLoginBox.Visible = true;
      this.litExitCommand.Visible = false;
      this.litCommandContent.Visible = false;
      this.litPageContent.Visible = false;
      this.litHistory.Visible = false;
      this.litPageMenu.Visible = false;

      this.UserSession.AppData.Page.Exit = new Evado.UniForm.Model.EuCommand ( );

      this.litExitCommand.Text = String.Empty;
      this.__CommandId.Value = EuStatics.CONST_LOGIN_COMMAND_ID.ToString ( );

      //
      // display the logo if one is defined.
      //
      if ( this.UserSession.AppData.LogoFilename != String.Empty )
      {
        this.UserSession.AppData.LogoFilename = Evado.Model.EvStatics.concatinateHttpUrl (
          Global.StaticImageUrl, this.UserSession.AppData.LogoFilename );

        this.imgLogo.Src = this.UserSession.AppData.LogoFilename.ToLower ( );
      }
      this.LogDebug ( "this.imgLogo.Src {0}.", this.imgLogo.Src );

      this.pLogo.Visible = false;
      if ( this.imgLogo.Src != String.Empty )
      {
        this.pLogo.Visible = true;
      }
      this.LogMethodEnd ( "RequestLogin" );
    }

    // =====================================================================================
    /// <summary>
    /// btnLogin_OnClick event method
    /// 
    /// Description:
    /// This method executes the user login event.
    /// 
    /// </summary>
    /// <param name="sender">Event object</param>
    /// <param name="E">Event arguments</param>
    // ---------------------------------------------------------------------------------
    protected void btnLogin_OnClick(
      object sender,
      System.EventArgs E )
    {
      Global.WriteToEventLog ( this.User.Identity.Name,
        "Evado.UniForm.AdminClient.DefaultPage.btnLogin_OnClick event method",
       System.Diagnostics.EventLogEntryType.Information );

      this.LogMethod ( "btnLogin_OnClick event" );
      this.LogDebug ( "UserId: " + this.fldUserId.Value );
      this.LogDebug ( "Password: " + this.fldPassword.Value );
      //
      // Initialise the methods variables and object.s
      //
      this.litLoginError.Text = String.Empty;
      this.litLoginError.Visible = false;

      //
      // If the external command exists then load it as the last command.
      //
      if ( this.UserSession.ExternalCommand != null )
      {
        this.UserSession.PageCommand = this.UserSession.ExternalCommand;
      }

      //
      // if credentials are missing display error message.
      //
      if ( this.fldUserId.Value == String.Empty
        || this.fldPassword.Value == String.Empty )
      {
        this.litLoginError.Text = "<p>You must enter a userId and password to login.<p>";
        this.litLoginError.Visible = true;

        return;
      }

      this.UserSession.UserId = this.fldUserId.Value;
      this.UserSession.Password = this.fldPassword.Value;

      Session [ Evado.UniForm.Model.EuStatics.SESSION_USER_ID ] = this.UserSession.UserId;

      this.SendLoginCommand ( this.fldUserId.Value, this.fldPassword.Value );

      //
      // Display the pages information.
      //
      this.fsLoginBox.Visible = false;
      this.litPageContent.Visible = true;

      if ( Global.EnablePageHistory == true )
      {
        this.litHistory.Visible = true;
      }
      if ( Global.EnablePageMenu == true )
      {
        this.litPageMenu.Visible = true;
      }

      this.LogDebug ( "AppData: " + this.UserSession.AppData.getAtString ( ) );
      if ( this.UserSession.AppData.Page.Exit != null )
      {
        this.LogDebug ( "AppData.Page.Exit: " + this.UserSession.AppData.Page.Exit.getAsString ( false, false ) );
      }

      //
      // Generate the page layout.
      //
      this.GeneratePage ( );

      this.LogDebug ( "Sessionid: " + this.UserSession.ServerSessionId );
      this.LogDebug ( "User NetworkId: " + this.UserSession.UserId );

      this.OutputSerialisedData ( );

      Global.OutputtDebugLog ( );

      this.LogMethodEnd ( "btnLogin_OnClick" );

    }//END btnLogin_OnClick event method

    // =====================================================================================
    /// <summary>
    /// btnLogin_OnClick event method
    /// </summary>
    // ---------------------------------------------------------------------------------
    protected void SendWindowsLoginCommand( )
    {
      Global.WriteToEventLog ( this.User.Identity.Name,
        "Evado.UniForm.AdminClient.DefaultPageSendWindowsLoginCommandRequestLogin method",
       System.Diagnostics.EventLogEntryType.Information );
      this.LogMethod ( "SendWindowsLoginCommand" );


      string roles = String.Empty;
      foreach ( string role in Roles.GetRolesForUser ( ) )
      {
        if ( roles != String.Empty )
        {
          roles += ";";
        }
        roles += role;
      }

      //
      // Create a page object.
      //
      this.UserSession.PageCommand = new Evado.UniForm.Model.EuCommand ( "Login",
        Evado.UniForm.Model.EuCommandTypes.Network_Login_Command,
        "Default",
        "Default",
        Evado.UniForm.Model.EuMethods.Null );

      this.UserSession.PageCommand.AddParameter ( 
        Evado.UniForm.Model.EuStatics.PARAMETER_LOGIN_USER_ID, this.UserSession.UserId );
      this.UserSession.PageCommand.AddParameter ( 
        Evado.UniForm.Model.EuStatics.PARAMETER_NETWORK_GROUPS, roles );


      this.LogDebug ( "Login PageCommand: " + this.UserSession.PageCommand.getAsString ( false, false ) );

      //
      // get a Command object from the server.
      //
      this.SendPageCommand ( );

      this.LogDebug ( "Status: " + this.UserSession.AppData.Status );

      //
      // Generate the page layout.
      //
      this.GeneratePage ( );

      this.OutputSerialisedData ( );

      Global.OutputtDebugLog ( );

      this.LogMethodEnd ( "sendWindowsLoginCommand" );

    }//END btnLogin_OnClick event method

    // =====================================================================================
    /// <summary>
    /// btnLogin_OnClick event method
    // ---------------------------------------------------------------------------------
    private void SendLoginCommand( String UserId, String Password )
    {
      Global.WriteToEventLog ( this.User.Identity.Name,
        "Evado.UniForm.AdminClient.DefaultPage.SendLoginCommand method",
       System.Diagnostics.EventLogEntryType.Information );

      this.LogMethod ( "SendLoginCommand" );
      this.LogDebug ( "PageCommand: " + this.UserSession.PageCommand.getAsString ( false, false ) );

      //
      // Create login command if it has not already been loaded.
      //
      if ( this.UserSession.PageCommand.Type != Evado.UniForm.Model.EuCommandTypes.Login_Command )
      {
        this.UserSession.PageCommand = new Evado.UniForm.Model.EuCommand ( "Login",
          Evado.UniForm.Model.EuCommandTypes.Login_Command,
          "Default",
          "Default",
          Evado.UniForm.Model.EuMethods.Null );
      }

      this.UserSession.PageCommand.AddParameter ( Evado.UniForm.Model.EuStatics.PARAMETER_LOGIN_USER_ID, UserId );
      this.UserSession.PageCommand.AddParameter ( Evado.UniForm.Model.EuStatics.PARAMETER_LOGIN_PASSWORD, Password );

      this.LogDebug ( "Login PageCommand: " + this.UserSession.PageCommand.getAsString ( false, true ) );

      //
      // get a Command object from the server.
      //
      this.SendPageCommand ( );

      this.LogDebug ( "Status: " + this.UserSession.AppData.Status );

      //
      // If the login is validated then display the home page.
      //
      if ( this.UserSession.AppData.Status == Evado.UniForm.Model.EuAppData.StatusCodes.Login_Failed )
      {
        this.litLoginError.Text = "<p>" + this.UserSession.AppData.Message + "</p>";
        this.litLoginError.Visible = true;
        this.UserSession.Password = String.Empty;

        this.RequestLogin ( );

        return;
      }
      else
        if ( this.UserSession.AppData.Status == Evado.UniForm.Model.EuAppData.StatusCodes.Login_Count_Exceeded )
      {
        this.litLoginError.Text = "<p>" + this.UserSession.AppData.Message + "</p>";
        this.litLoginError.Visible = true;

        return;
      }


      this.LogMethodEnd ( "SendLoginCommand" );
    }//END btnLogin_OnClick event method

    // =====================================================================================
    /// <summary>
    /// btnLogout_Click event method
    /// 
    /// Description:
    /// This method executes the user login event.
    /// 
    /// </summary>
    /// <param name="sender">Event object</param>
    /// <param name="E">Event arguments</param>
    // ---------------------------------------------------------------------------------
    protected void btnLogout_Click(
      object sender,
      System.EventArgs E )
    {
      Global.WriteToEventLog ( this.User.Identity.Name,
        "Evado.UniForm.AdminClient.DefaultPage.btnLogout_Click event method",
       System.Diagnostics.EventLogEntryType.Information );

      this.LogMethod ( "btnLogout_Click" );
      requestLogout ( );
    }

    // =====================================================================================
    /// <summary>
    /// requestLogout method
    /// 
    /// Description:
    /// This method executes the user login event.
    /// 
    /// </summary>
    // ---------------------------------------------------------------------------------
    protected void requestLogout( )
    {
      Global.WriteToEventLog ( this.User.Identity.Name,
        "Evado.UniForm.AdminClient.DefaultPage.requestLogout method",
       System.Diagnostics.EventLogEntryType.Information );
      this.LogMethod ( "requestLogout" );
      this.fldPassword.Value = String.Empty;
      //
      // Create a page object.
      //
      this.UserSession.PageCommand = new Evado.UniForm.Model.EuCommand ( "Logout",
        Evado.UniForm.Model.EuCommandTypes.Logout_Command,
        "Default",
        "Default",
        Evado.UniForm.Model.EuMethods.Null );

      //
      // get a Command object from the server.
      //
      this.SendPageCommand ( );

      //
      // display the login panel.
      //
      this.RequestLogin ( );

    }//END  btnLogout_Click event method

    ///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region Logging methods.
    //  =================================================================================
    /// <summary>
    ///   This static method removes a user from the online user list.
    /// </summary>
    //   ---------------------------------------------------------------------------------
    public void LogMethod( String Value )
    {
      string logValue = Evado.Model.EvStatics.CONST_METHOD_START
         + DateTime.Now.ToString ( "dd-MM-yy hh:mm:ss" ) + ": "
         + "Evado.Uniform.Webclient.DefaultPage:" + Value + " Method";

      Global.LogValue ( logValue );
    }

    //  =================================================================================
    /// <summary>
    ///   This static method removes a user from the online user list.
    /// 
    /// </summary>
    //   ---------------------------------------------------------------------------------
    public void LogMethodEnd( String Value )
    {
      String value = Evado.Model.EvStatics.CONST_METHOD_END;

      value = value.Replace ( " END OF METHOD ", " END OF " + Value + " METHOD " );

      Global.LogValue ( value );
    }

    //  =================================================================================
    /// <summary>
    ///   This method log the passed value
    /// </summary>
    /// <param name="Value">String: value.</param>
    //   ---------------------------------------------------------------------------------
    public void LogValue( String Value )
    {
      string logValue = DateTime.Now.ToString ( "dd-MM-yy hh:mm:ss" ) + ": "
       + "DefaultPage:" + Value;

      Global.LogValue ( logValue );
    }

    // ==================================================================================
    /// <summary>
    /// This method appendes debuglog string to the debug log for the class and adds
    /// a new line at the end of the text.
    /// </summary>
    /// <param name="Format">String: format text.</param>
    /// <param name="args">Array of objects as parameters.</param>
    // ----------------------------------------------------------------------------------
    public void LogValue( String Format, params object [ ] args )
    {
      string logValue = DateTime.Now.ToString ( "dd-MM-yy hh:mm:ss" ) + ": "
       + "DefaultPage:" + String.Format ( Format, args );

      Global.LogValue ( logValue );
    }

    //  =================================================================================
    /// <summary>
    ///   This method log debug the passed value
    /// </summary>
    /// <param name="Value">String: value.</param>
    //   ---------------------------------------------------------------------------------
    public void LogDebug( String Value )
    {
      string logValue = DateTime.Now.ToString ( "dd-MM-yy hh:mm:ss" ) + ": "
       + "DefaultPage:" + Value;

      Global.LogDebugValue ( logValue );
    }

    // ==================================================================================
    /// <summary>
    /// This method appendes debuglog string to the debug log for the class and adds
    /// a new line at the end of the text.
    /// </summary>
    /// <param name="Format">String: format text.</param>
    /// <param name="args">Array of objects as parameters.</param>
    // ----------------------------------------------------------------------------------
    public void LogDebug( String Format, params object [ ] args )
    {
      string logValue = DateTime.Now.ToString ( "dd-MM-yy hh:mm:ss" ) + ": "
       + "DefaultPage:" + String.Format ( Format, args );

      Global.LogDebugValue ( logValue );
    }

    ///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    ///================================== END CLASS SOURCE CODE ===========================

    #region Web FormRecord Designer generated code
    override protected void OnInit( EventArgs e )
    {
      ///
      /// CODEGEN: This call is required by the ASP.NET Web FormRecord Designer.
      ///
      InitializeComponent ( );
      base.OnInit ( e );
    }

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent( )
    {

    }
    #endregion
  }
}
