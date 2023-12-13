/***************************************************************************************
 * <copyright file="webclinical\default.aspx.cs" company="EVADO HOLDING PTY. LTD.">
 *     
 *      Copyright (c) 2011 - 2020 EVADO HOLDING PTY. LTD.  All rights reserved.
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
using System.Net;
using System.IO;
using System.Text;
using System.Net.Http;

///Evado. namespace references.

using Evado.UniForm.Web;
using Evado.UniForm.Model;
using Evado.Model;
using System.Runtime.Remoting.Messaging;

namespace Evado.UniForm.WebClient
{
  /// <summary>
  /// This is the code behind class for the home page.
  /// </summary>
  public partial class ClientPage : EvPersistentPageState
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

    private const int CONST_FILE_SEGMENT_LENGTH = 40000;

    private EucSession UserSession = new EucSession ( );

    private bool LocalCommand = false;


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
    protected void Page_Load ( object sender, System.EventArgs E )
    {
      Global.ClearDebugLog ( );
      Global.LogAppendGlobal ( );
      this.LogMethod ( "Page_Load event" );
      this.LogDebug ( "EnablePageHistory: " + Global.EnablePageHistory );
      try
      {
        this.LogValue ( "UserHostAddress: " + Request.UserHostAddress );
        this.LogDebug ( "UserHostName: " + Request.UserHostName );

        this.LogDebug ( "LogonUserIdentity IsAuthenticated: " + Request.LogonUserIdentity.IsAuthenticated );
        this.LogDebug ( "LogonUserIdentity Name: " + Request.LogonUserIdentity.Name );
        this.LogDebug ( "User.Identity.Name: " + User.Identity.Name );
        this.LogDebug ( "Authentication Type: " + Global.AuthenticationMode );

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
                if ( this.LocalCommand == false )
                {
                  //
                  // Update the Command with page data objects.
                  //
                  this.GetPageCommandParameters ( );

                  this.LogDebug ( "CURRENT PageCommand: " + this.UserSession.PageCommand.getAsString ( false, true ) );

                  //
                  // Send the Command to the server.
                  //
                  this.SendPageCommand ( );

                  //this.SendFileRequest ( "evado.jpg", "image/jpg" );

                  this.LogDebug ( "LogoFilename: " + this.UserSession.AppData.LogoFilename );
                }
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
    public void InitialiseGlobalVariables ( )
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
        this.LogDebug ( "Global.DefaultFileBaseUrl: " + Global.FileServiceUrl );
        this.LogDebug ( "Global.Debug " + Global.DebugLogOn );
        this.LogDebug ( "Global.DisplaySerialisation: " + Global.DisplaySerialisation );
        this.fldPassword.Value = String.Empty;

        this.litSerialisedLinks.Visible = Global.DisplaySerialisation;

        //
        // Initialise the Command history list.
        //
        this.initialiseHistory ( );

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
    public void LoadSessionVariables ( )
    {
      this.LogMethod ( "loadSessionVariables" );
      //
      // Retrieve the application data object.
      //
      if ( Session [ SESSION_USER ] != null )
      {
        this.UserSession = ( EucSession ) Session [ SESSION_USER ];
      }

      this.LogDebug ( "ApplicationData.Id: " + this.UserSession.AppData.Id );
      this.LogDebug ( "SessionId: " + this.UserSession.ServerSessionId );
      this.LogDebug ( "UserNetworkId: " + this.UserSession.UserId );
      this.LogDebug ( "Password: " + this.UserSession.Password );
      this.LogDebug ( "PageCommand: " + this.UserSession.PageCommand.getAsString ( false, false ) );
      this.LogDebug ( "Command History length: " + this.UserSession.CommandHistoryList.Count );
      this.LogDebug ( "Icon list length: " + this.UserSession.IconList.Count );

      this.LogMethodEnd ( "loadSessionVariables" );

    }//END loadSessionVariables method

    // ==================================================================================	
    /// <summary>
    ///	this method set value to session variables
    /// </summary>
    // --------------------------------------------------------------------------------
    public void SaveSessionVariables ( )
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
    /// This method sends the Command back to the server objects.
    /// </summary>
    // ---------------------------------------------------------------------------------
    private void SendPageCommand ( )
    {
      this.LogMethod ( "sendPageCommand" );
      this.LogValue ( "DebugLogOn {0}.", Global.DebugLogOn );
      this.LogDebug ( "Sessionid: " + this.UserSession.ServerSessionId );
      this.LogDebug ( "User NetworkId: " + this.UserSession.UserId );
      this.LogDebug ( "AppDate Url: " + this.UserSession.AppData.Url );
      this.LogDebug ( "Global.RelativeWcfRestURL: " + EuStatics.APPLICATION_SERVICE_CLIENT_RELATIVE_URL );
      this.LogDebug ( "Global.ClientVersion: " + Global.ClientVersion );
      this.LogDebug ( "GetRequestHeader 'Host' : '{0}'. ", this.GetRequestHeader ( "Host" ) );

      //
      // Display a serialised instance of the object.
      //
      string jsonData = String.Empty;
      string baseUrl = Global.WebServiceUrl;
      string serviceUri = EuStatics.APPLICATION_SERVICE_CLIENT_RELATIVE_URL + Global.ClientVersion
        + "?command=command&session=" + this.UserSession.ServerSessionId;
      Newtonsoft.Json.JsonSerializerSettings jsonSettings = new Newtonsoft.Json.JsonSerializerSettings
      {
        NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore
      };
      this.LogDebug ( "baseUrl: {0}.", baseUrl );
      this.LogDebug ( "WebServiceUrl:  {0}.", Global.WebServiceUrl );
      this.LogDebug ( "FileServiceUrl:  {0}.", Global.FileServiceUrl );
      this.LogDebug ( "ImagesUrl: {0}.", Global.StaticImageUrl );
      this.LogDebug ( "TempUrl: {0}.", Global.TempUrl );

      String WebServiceUrl = baseUrl + serviceUri;
      this.LogDebug ( "WebServiceUrl: {0}.", WebServiceUrl );
      //
      // Set the default application if non are set.
      //
      if ( this.UserSession.PageCommand.ApplicationId == String.Empty )
      {
        this.UserSession.PageCommand.ApplicationId = "Default";
      }


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
      // serialise the Command prior to sending to the web service.
      //
      this.LogDebug ( "Serialising the PageComment object" );

      jsonData = Newtonsoft.Json.JsonConvert.SerializeObject ( this.UserSession.PageCommand );

      //
      // Initialise the web request.
      //
      this.LogDebug ( "Creating the WebRequest." );

      try
      {
        //
        // The post command 
        //
        jsonData = this.SendPost ( WebServiceUrl, jsonData );

        this.LogDebug ( "JSON Serialised text length: " + jsonData.Length );

        if ( Global.DebugLogOn == true )
        {
          Evado.Model.EvStatics.Files.saveFile ( Global.TempPath, @"json-post-data.txt", jsonData );
        }

        //
        // deserialise the application data 
        //
        this.UserSession.AppData = new Evado.UniForm.Model.EuAppData ( );

        this.LogDebug ( "Deserialising JSON to Evado.UniForm.Model.EuAppData object." );

        this.UserSession.AppData = Newtonsoft.Json.JsonConvert.DeserializeObject<Evado.UniForm.Model.EuAppData> ( jsonData );

        this.LogDebug ( "Application object: " + this.UserSession.AppData.getAtString ( ) );
        this.LogDebug ( "Page Command count: " + this.UserSession.AppData.Page.CommandList.Count );

        this.LogDebug ( "ExitCommand: " + this.UserSession.AppData.Page.Exit.getAsString ( false, false ) );
        //
        // Add the exit Command to the history.
        //
        this.addHistoryCommand ( this.UserSession.AppData.Page.Exit );

        if ( this.UserSession.CommandHistoryList.Count > 0 )
        {
          for ( int i = 0; i < this.UserSession.CommandHistoryList.Count; i++ )
          {
            EuCommand command = this.UserSession.CommandHistoryList [ i ];

            this.LogDebug ( "{0} Command: ", i, command.getAsString ( false, true ) );
          }
        }

        //
        // Update the user session id
        //
        this.UserSession.ServerSessionId = this.UserSession.AppData.SessionId;

        this.LogDebug ( "ServerUserSessionId: " + this.UserSession.ServerSessionId );
      }
      catch ( Exception Ex )
      {
        this.litErrorMessage.Text = "Web Service Error. " + Evado.Model.EvStatics.getExceptionAsHtml ( Ex );

        this.LogDebug ( "Web Service Error. " + Evado.Model.EvStatics.getException ( Ex ) ); ;

        EvEventLog.LogPageError ( this, Evado.Model.EvStatics.getException ( Ex ) );

        this.UserSession.AppData = new Evado.UniForm.Model.EuAppData ( );
        this.UserSession.AppData.Id = Guid.NewGuid ( );
        this.UserSession.AppData.Page.Id = this.UserSession.AppData.Id;
        this.UserSession.AppData.Page.Title = "Service Access Error.";
        Evado.UniForm.Model.EuGroup group = this.UserSession.AppData.Page.AddGroup (
          "Service Access Error Report", Evado.UniForm.Model.EuEditAccess.Disabled );

        if ( Global.DebugLogOn == true )
        {
          group.Description = "Web Service URL: " + baseUrl
          + "\r\nWeb Service Error. " + Evado.Model.EvStatics.getException ( Ex );
        }
        else
        {
          group.Description = "Error Occured Accessing the Web Service - contact your administrator.";
        }
      }

      this.LogMethodEnd ( "sendPageCommand" );

    }//END sendPageCommand method

    // ==================================================================================
    /// <summary>
    /// This method sends a request to the file service.
    /// </summary>
    /// <param name="filename">String: file name of the file to be up loaded.</param>
    /// <param name="MimeType">String: the mime type for the file.</param>
    // ---------------------------------------------------------------------------------
    private Evado.Model.EvEventCodes SendFileRequest ( String filename, String MimeType )
    {
      this.LogMethod ( "SendFileRequest" );
      this.LogValue ( "filename {0}, MimeType {1}.", filename, MimeType );

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

        if ( Global.DebugLogOn == true )
        {
          Evado.Model.EvStatics.Files.saveFile ( Global.TempPath, @"jsonData-1.txt", jsonData );
        }

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
          /*
          */
          string end = jsonData.Substring ( jsonData.Length - 100 );
          this.LogDebug ( "file end: {0},", end );

          //
          // convert the file object json into segements of less than 40000 characters.
          //
          for ( int startIndex = 0; startIndex < jsonData.Length; startIndex += CONST_FILE_SEGMENT_LENGTH )
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
          for ( int segmentCount = 0; segmentCount < FileSegmentList.Count; segmentCount++ )
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

    }//END UploadFile method

    // ==================================================================================
    /// <summary>
    /// This method sends a request to the file service.
    /// </summary>
    /// <param name="SegementData">String: the file segement data.</param>
    /// <param name="Segment">String: thenfile segmenet.</param>
    // ---------------------------------------------------------------------------------
    private Evado.Model.EvEventCodes SendFileSegment ( String SegementData, int Segment )
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
    private String SendPost (
      String WebServiceUrl,
      String PostContent )
    {
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
            HttpResponseMessage respone = Global.HttpClient.PostAsync ( uri, content ).Result;

            this.LogDebug ( "StatusCode {0}.", respone.StatusCode );

            responseText = respone.Content.ReadAsStringAsync ( ).Result;

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
    private String GetRequestHeader ( String HeaderKey )
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
    private void OutputSerialisedData ( )
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
      Evado.UniForm.Model.EuGroup serialisationGroup = new Evado.UniForm.Model.EuGroup ( "Serialisation", String.Empty, Evado.UniForm.Model.EuEditAccess.Disabled );
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
    private String AddIconHtml ( String Icon )
    {
      //
      // Iterate through the page groups and fields to find the matching field.
      //
      foreach ( EucKeyValuePair valuePair in this.UserSession.IconList )
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
    public void GetPostBackPageCommand ( )
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
    private bool GetRequestPageCommand ( )
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
      for ( loop1 = 0; loop1 < aKeys.Length; loop1++ )
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
    private void ReadinCommandId ( )
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
    private Evado.UniForm.Model.EuCommand GetCommandObject ( Guid CommandId )
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
        Evado.UniForm.Model.EuCommand historyCommand = this.getHistoryCommand ( CommandId );

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

    #region  update page methods

    // ==================================================================================
    /// <summary>
    /// This method updates the  application data with the form field values.
    /// </summary>
    // ---------------------------------------------------------------------------------
    private void GetPageCommandParameters ( )
    {
      this.LogMethod ( "getPageCommandParameters" );
      //
      // If the Command method is to upate the page then update the data object with 
      // Page field values.
      //
      if ( this.UserSession.PageCommand.Method != Evado.UniForm.Model.EuMethods.Save_Object
        && this.UserSession.PageCommand.Method != Evado.UniForm.Model.EuMethods.Delete_Object
        && this.UserSession.PageCommand.Method != Evado.UniForm.Model.EuMethods.Custom_Method )
      {
        return;
      }

      this.LogDebug ( "Updating command parameters. " );

      //
      // Upload the page images.
      //
      this.UploadPageImages ( );

      //
      // Get the data from the returned page fields.
      //
      this.GetPageDataValues ( );

      //
      // Get the data from the returned page fields.
      //
      this.UpdateFieldAnnotations ( );

      //
      // Update the Command parmaters witih the page values.
      //
      this.UpdateWebPageCommandObject ( );

    }//END updateApplicationDataObject method

    // ==================================================================================
    /// <summary>
    /// This method updates the web application with the form field values.
    /// </summary>
    // ---------------------------------------------------------------------------------
    private void GetPageDataValues ( )
    {
      this.LogMethod ( "getPageDataValues" );
      //
      // Get the field collection.
      //
      NameValueCollection ReturnedFormFields = Request.Form;

      // 
      // Get names of all keys into a string array.
      // 
      String [ ] aKeys = ReturnedFormFields.AllKeys;

      this.LogDebug ( "Key length: " + aKeys.Length );

      // 
      // Iterate the keys to find the value for the selected formDataId
      // 
      for ( int loop1 = 0; loop1 < aKeys.Length; loop1++ )
      {
        this.LogDebug ( aKeys [ loop1 ] + " >> " + ReturnedFormFields.Get ( aKeys [ loop1 ] ) );
      }

      // 
      // Iterate through the test fields updating the fields that have changed.
      // 
      foreach ( Evado.UniForm.Model.EuGroup group in this.UserSession.AppData.Page.GroupList )
      {

        for ( int count = 0; count < group.FieldList.Count; count++ )
        {
          group.FieldList [ count ] = this.UpdateFormField (
            group.FieldList [ count ],
            ReturnedFormFields,
            group.EditAccess );

          this.LogDebug ( group.FieldList [ count ].FieldId
            + " > " + group.FieldList [ count ].Title
            + " >> " + group.FieldList [ count ].Type
            + " >>> " + group.FieldList [ count ].Value );

        }//END test field iteration.

      }//END the iteration loop.

      this.LogMethodEnd ( "getPageDataValues" );

    }//END getPageDataValues method

    // ==================================================================================

    /// <summary>
    /// This method updates the new field annotations.
    /// </summary>
    // ---------------------------------------------------------------------------------
    private void UpdateFieldAnnotations ( )
    {
      this.LogMethod ( "updateFieldAnnotations" );
      //
      // Get the field collection.
      //
      NameValueCollection ReturnedFormFields = Request.Form;

      // 
      // Get names of all keys into a string array.
      // 
      String [ ] aKeys = ReturnedFormFields.AllKeys;

      this.LogDebug ( "Key length: " + aKeys.Length );

      // 
      // Iterate the keys to find the value for the selected formDataId
      // 
      for ( int loop1 = 0; loop1 < aKeys.Length; loop1++ )
      {
        EucKeyValuePair keyPair = new EucKeyValuePair ( );
        //
        // Skip all non annotation and returned field values.
        //
        if ( aKeys [ loop1 ].Contains ( Evado.UniForm.Model.EuField.CONST_FIELD_QUERY_SUFFIX ) == false
          && aKeys [ loop1 ].Contains ( Evado.UniForm.Model.EuField.CONST_FIELD_ANNOTATION_SUFFIX ) == false )
        {
          continue;
        }

        this.LogDebug ( "" + aKeys [ loop1 ] + " >> " + ReturnedFormFields.Get ( aKeys [ loop1 ] ) );

        int inAnnotationKey = this.GetAnnotationIndex ( aKeys [ loop1 ] );

        this.LogDebug ( " inAnnotationKey: " + inAnnotationKey );
        //
        // Get the data value.
        //
        if ( inAnnotationKey < 0 )
        {
          this.LogDebug ( " >> New Item" );
          //
          // Set the object value and add it to the field annotation list.
          //
          keyPair.Key = aKeys [ loop1 ];
          keyPair.Value = ReturnedFormFields.Get ( aKeys [ loop1 ] );

          this.LogDebug ( " Key: " + keyPair.Key + " value: " + keyPair.Value );

          this.UserSession.FieldAnnotationList.Add ( keyPair );

        }
        else
        {
          //
          // Update the annotated value.
          //
          keyPair = this.UserSession.FieldAnnotationList [ inAnnotationKey ];
          keyPair.Value = ReturnedFormFields.Get ( aKeys [ loop1 ] );
        }

      }//END return field values.

      for ( int count = 0; count < this.UserSession.FieldAnnotationList.Count; count++ )
      {
        EucKeyValuePair keyPair = this.UserSession.FieldAnnotationList [ count ];

        this.LogDebug ( "Key: " + keyPair.Key + " >> " + keyPair.Value );
      }

      this.LogDebug ( "FieldAnnotationList: " + this.UserSession.FieldAnnotationList.Count );

    }//END updateFieldAnnotations method

    // =============================================================================== 
    /// <summary>
    /// This method returns the annotation's list index.
    /// </summary>
    /// <param name="Key">The page annotation field id</param>
    /// <returns>Int: the annotation list index.</returns>
    // ---------------------------------------------------------------------------------
    private int GetAnnotationIndex ( String Key )
    {
      //this.writeDebug = "<hr/>Evado.UniForm.WebClient.ClientPage.getAnnotationIndex method.  Key: " + Key
      //  + " AnnotationList count: " + this.UserSession.FieldAnnotationList.Count ;
      //
      // Iterate through the annotation list to find a matching element
      //
      for ( int i = 0; i < this.UserSession.FieldAnnotationList.Count; i++ )
      {
        //
        // Get annotation.
        //
        EucKeyValuePair annotation = this.UserSession.FieldAnnotationList [ i ];
        // this.writeDebug = "[0]" + annotation [ 0 ];

        //
        // Return the matching value.
        //
        if ( annotation.Key == Key )
        {
          /// this.writeDebug = " >> FOUND ";
          return i;
        }
      }

      ///
      /// for not match return empty string.
      ///
      return -1;

    }//END getAnnotationValue method 

    // =============================================================================== 
    /// <summary>
    /// updateFormField method.
    /// 
    /// Description:
    ///   This method updates a test field object.
    /// 
    /// </summary>
    /// <param name="FormField">Field object containing test field data.</param>
    /// <param name="ReturnedFormFields">Containing the returned formfield values.</param>
    /// <param name="FormState">Current FormRecord state</param>
    /// <returns>Returns a Field object.</returns>
    // ---------------------------------------------------------------------------------
    private Evado.UniForm.Model.EuField UpdateFormField (
      Evado.UniForm.Model.EuField FormField,
      NameValueCollection ReturnedFormFields,
      Evado.UniForm.Model.EuEditAccess GroupStatus )
    {
      this.LogMethod ( "updateFormField" );
      this.LogDebug ( "FormField.DataId: " + FormField.FieldId );
      this.LogDebug ( "FormField.DataType: " + FormField.Type );
      this.LogDebug ( "FormField.Status: " + FormField.EditAccess );
      this.LogDebug ( "GroupStatus: " + GroupStatus );

      // 
      // Initialise methods variables and objects.
      // 
      string stValue = String.Empty;
      string stAnnotation = String.Empty;
      string stQuery = String.Empty;
      string stAssessmentStatus = String.Empty;

      //
      // If a binary or image file return it without processing.
      //
      if ( FormField.Type == Evado.Model.EvDataTypes.Binary_File
        || FormField.Type == Evado.Model.EvDataTypes.Image )
      {
        this.LogDebug ( "Binary or Image field found but not processed" );
        return FormField;
      }

      /**********************************************************************************/
      // 
      // If the test is in EDIT mode update the fields values.
      // 
      if ( FormField.EditAccess == Evado.UniForm.Model.EuEditAccess.Enabled )
      {
        //
        // If field type is a single value update it 
        //
        switch ( FormField.Type )
        {
          case Evado.Model.EvDataTypes.Check_Box_List:
            {
              FormField.Value = this.GetCheckButtonListFieldValue (
                ReturnedFormFields,
                FormField.FieldId,
                FormField.Value,
                FormField.OptionList.Count );
              break;
            }
          case Evado.Model.EvDataTypes.Address:
            {
              FormField.Value = this.GetAddressFieldValue (
                ReturnedFormFields,
                FormField.FieldId );
              break;
            }

          case Evado.Model.EvDataTypes.Name:
            {
              FormField.Value = this.GetNameFieldValue (
                ReturnedFormFields,
                FormField.FieldId );
              break;
            }

          case Evado.Model.EvDataTypes.Streamed_Video:
            {
              // 
              // Iterate through the option list to compare values.
              // 
              string videoUrl = this.GetReturnedFormFieldValue ( ReturnedFormFields, FormField.FieldId );

              this.LogDebug ( "videoUrl:" + videoUrl );

              FormField.Value = videoUrl;
              break;
            }
          case Evado.Model.EvDataTypes.Http_Link:
            {
              // 
              // Iterate through the option list to compare values.
              // 
              string httpUrl = this.GetReturnedFormFieldValue ( ReturnedFormFields, FormField.FieldId + EuField.CONST_HTTP_URL_FIELD_SUFFIX );
              string httpTitle = this.GetReturnedFormFieldValue ( ReturnedFormFields, FormField.FieldId + EuField.CONST_HTTP_TITLE_FIELD_SUFFIX );

              this.LogDebug ( "httpUrl:" + httpUrl + " httpTitle:" + httpTitle );

              FormField.Value = httpUrl + "^" + httpTitle;
              break;
            }

          case Evado.Model.EvDataTypes.Integer_Range:
          case Evado.Model.EvDataTypes.Float_Range:
          case Evado.Model.EvDataTypes.Double_Range:
          case Evado.Model.EvDataTypes.Date_Range:
            {
              FormField.Value = this.GetRangeFieldValue (
                ReturnedFormFields,
                FormField.FieldId );
              break;
            }

          case Evado.Model.EvDataTypes.Signature:
            {
              FormField.Value = this.getSignatureFieldValue (
                ReturnedFormFields,
                FormField.FieldId );
              break;
            }
          case Evado.Model.EvDataTypes.Table:
            {
              FormField = this.UpdateFormTableFields (
                           FormField,
                           ReturnedFormFields );
              break;
            }
          case Evado.Model.EvDataTypes.Computed_Field:
            {
              FormField.Value = this.updateComputedField ( FormField );

              this.LogDebug ( "Computed_Field: FormField.Value: {0}.", FormField.Value );
              break;
            }
          default:
            {
              stValue = this.GetReturnedFormFieldValue ( ReturnedFormFields, FormField.FieldId );

              this.LogDebug ( "Field stValue: " + stValue );
              // 
              // Does the returned field value exist
              // 
              if ( stValue != null )
              {
                if ( FormField.Value != stValue )
                {
                  if ( FormField.Type == Evado.Model.EvDataTypes.Numeric )
                  {
                    this.LogDebug ( "Numeric Field Change: Id: '" + FormField.FieldId
                     + "' Old: '" + FormField.Value + "' New: '" + stValue + "' " );

                    FormField.Value = Evado.Model.EvStatics.convertTextNullToNumNull ( stValue );
                  }

                  // 
                  // Set field value.
                  // 
                  FormField.Value = stValue;

                }//END Update field value.

              }//END Value exists.

              break;
            }

        }//END Switch

      }//END updating field

      // 
      // stReturn the test field object.
      // 
      return FormField;

    }//END updateFormField method

    // =============================================================================== 
    /// <summary>
    /// This method updates the Computed form fields.
    /// 
    /// </summary>
    /// <param name="ComputedField">Evado.Uniform.Model.EuField object</param>
    // ---------------------------------------------------------------------------------
    private String updateComputedField ( EuField ComputedField )
    {
      this.LogMethod ( "updateComputedField" );
      // 
      // Initialise methods variables and objects.
      //
      String computedFormula = ComputedField.GetParameter ( EuFieldParameters.Computed_Formula );

      if ( String.IsNullOrEmpty ( computedFormula ) == true )
      {
        this.LogDebug ( "EXIT: formula is empty" );
        this.LogMethodEnd ( "updateComputedField" );
        return ComputedField.Value;
      }

      int OpenBracketIndex = computedFormula.IndexOf ( '(' );
      int CloseBracketIndex = computedFormula.IndexOf ( ')' );
      float fieldValue = 0;
      this.LogDebug ( "computedFormula: {0}.", computedFormula );
      try
      {
        if ( computedFormula.Contains ( "(" ) == false )
        {
          this.LogDebug ( "EXIT: formula incomplete" );
          this.LogMethodEnd ( "updateComputedField" );
          return ComputedField.Value;
        }
        computedFormula = computedFormula.Replace ( ")", "" );
        String [ ] arComputerFormula = computedFormula.Split ( '(' );

        String formula = arComputerFormula [ 0 ];
        String fields = arComputerFormula [ 1 ]; ;

        this.LogDebug ( "Formula: {0}, fields: '{1}'.", formula, fields );

        String [ ] arFields = fields.Split ( ';' );

        switch ( formula )
        {
          case EuField.COMPUTED_FUNCTION_SUM_FIELDS:
            {
              //
              // Iterate through the field idenifiers retrieving the field value 
              // and if numeric add it to the fieldValue variale.
              //
              foreach ( string fielId in arFields )
              {
                EuField field = this.UserSession.AppData.Page.getField ( fielId );
                this.LogDebug ( "fielid: {0}, field.FieldId: {1}, Value: {2}.",
                  fielId, field.FieldId, field.Value );

                float fValue = Evado.Model.EvStatics.getFloat ( field.Value, 0 );
              if ( fValue == 0 )
              {
                this.LogDebug ( "ERROR: Empty or not a numeric value." );
                continue;
              }

              if ( field == null )
              {
                this.LogDebug ( "ERROR: FIELD NULL." );
                continue;
              }

              fieldValue += fValue;
              }
              break;
            }
          case EuField.COMPUTED_FUNCTION_SUM_CATEGORY:
            {
              //
              // get the computed fields fied category
              //
              string fieldCategory = fields.Trim ( );
              this.LogDebug ( "ComputedField: fieldCategory: {0}.", fieldCategory );

              //
              // Iterate through the page groups 
              //
              foreach ( EuGroup group in this.UserSession.AppData.Page.GroupList )
              {
                //
                // Iterate through the fields in each group. Retrieving the field
                // and if in the computed field category add its value to the fieldValue variale.
                //
                foreach ( EuField field in group.FieldList )
                {
                  string category = field.GetParameter ( EuFieldParameters.Category );

                  this.LogDebug ( "field: fieldCategory: {0}.", category );

                  if ( category.ToUpper ( ) != fieldCategory.ToUpper ( ) )
                  {
                    continue;
                  }
                  this.LogDebug ( "Add Value  {0}.", field.Value );

                  float fValue = Evado.Model.EvStatics.getFloat ( field.Value, 0 );
                if ( fValue == 0 )
                {
                  this.LogDebug ( "ERROR: Empty or not a numeric value." );
                  continue;
                }

                fieldValue += fValue;
                }
              }
              break;
            }
          case EuField.COMPUTED_FUNCTION_SUM_COLUMN:
            {
              if ( arFields.Length == 0 )
              {
                break;
              }
              //
              // Retrieve the tale field identifier and tale column (0-9).
              //
              String taleFieldId = arFields [ 0 ].Trim ( );
              String columnId = arFields [ 1 ].Trim ( );
              int column = Evado.Model.EvStatics.getInteger ( columnId );
              column--;

              if ( column < 0 || column > 9 )
              {
                break;
              }

              //
              // retrieve the tale field
              //
              EuField field = this.UserSession.AppData.Page.getField ( taleFieldId );

              //
              // iterate through each row adding the column value if a floating number.
              //
              foreach (  Evado.Model.EvTableRow row in field.Table.Rows )
              {
                float fValue = Evado.Model.EvStatics.getFloat ( row.Column [ column ], 0 );
              if ( fValue == 0 )
              {
                this.LogDebug ( "ERROR: Empty or not a numeric value." );
                continue;
              }

              fieldValue += fValue;
              }

              break;
            }
        }//End Switch

        ComputedField.Value = fieldValue.ToString ( );
        this.LogDebug ( "ComputedField.Value: '{0}'.",
          ComputedField.Value );
      }
      catch ( Exception Ex )
      {
        this.LogValue ( Evado.Model.EvStatics.getException ( Ex ) );
      }

      this.LogMethodEnd ( "updateComputedField" );

      return ComputedField.Value;

    }//END updateComputedField method

    // =============================================================================== 
    /// <summary>
    /// updateChecklistField method.
    /// 
    /// Description:
    ///   This method updates the common TestReport static test fields
    /// 
    /// </summary>
    /// <param name="ReturnedFormFields">List of returned html form fields.</param>
    /// <param name="htmlDataId">The html form field to be udpated.</param>
    /// <param name="CurrentValue">The current form field value.</param>
    /// <param name="OptionList">THe form field option list.</param>
    /// <returns>Returns a string containing the Java Scripts.</returns>
    // ---------------------------------------------------------------------------------
    private string getSignatureFieldValue (
      NameValueCollection ReturnedFormFields,
      string htmlDataId )
    {
      this.LogMethod ( "getSignatureFieldValue" );
      this.LogValue ( "htmlDataId: " + htmlDataId );
      // 
      // Initialise methods variables and objects.
      // 
      string signatureValueFieldId = htmlDataId + "_sig";
      string signatureNameFieldId = htmlDataId + "_name";

      String stSignature = this.GetReturnedFormFieldValue ( ReturnedFormFields, signatureValueFieldId );
      string stName = this.GetReturnedFormFieldValue ( ReturnedFormFields, signatureNameFieldId );

      this.LogValue ( "stSignature: " + stSignature );
      this.LogValue ( "stName: " + stName );

      if ( stSignature == null )
      {
        return String.Empty;
      }
      if ( stSignature == String.Empty )
      {
        return String.Empty;
      }

      if ( stName == null )
      {
        stName = String.Empty;
      }

      this.LogValue ( "Converting signature to signatureBlock object." );
      Evado.Model.EvSignatureBlock signatureBlock = new Evado.Model.EvSignatureBlock ( );
      signatureBlock.Signature = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Evado.Model.EvSegement>> ( stSignature );
      signatureBlock.Name = stName;
      signatureBlock.DateStamp = DateTime.Now;

      string stSignatureBlock = Newtonsoft.Json.JsonConvert.SerializeObject ( signatureBlock );
      this.LogDebug ( "stSignatureBlock:" + stSignatureBlock );

      return stSignatureBlock;

    }//END getCheckButtonListFieldValue method


    // =============================================================================== 
    /// <summary>
    /// updateChecklistField method.
    /// 
    /// Description:
    ///   This method updates the common TestReport static test fields
    /// 
    /// </summary>
    /// <param name="ReturnedFormFields">List of returned html form fields.</param>
    /// <param name="htmlDataId">The html form field to be udpated.</param>
    /// <param name="CurrentValue">The current form field value.</param>
    /// <param name="OptionList">THe form field option list.</param>
    /// <returns>Returns a string containing the Java Scripts.</returns>
    // ---------------------------------------------------------------------------------
    private string GetCheckButtonListFieldValue (
      NameValueCollection ReturnedFormFields,
      string htmlDataId,
      string CurrentValue,
      int OptionListCount )
    {
      this.LogMethod ( "getCheckButtonListFieldValue" );
      this.LogValue ( "htmlDataId: " + htmlDataId );
      this.LogValue ( "OptionList: " + OptionListCount );
      // 
      // Initialise methods variables and objects.
      // 
      string [ ] arrValues = new String [ 0 ];
      string stThisValue = String.Empty;

      arrValues = this.GetReturnedFormFieldValueArray ( ReturnedFormFields, htmlDataId );

      if ( arrValues != null )
      {
        // 
        // Iterate through the option list to compare values.
        // 
        for ( int index = 0; index < arrValues.Length; index++ )
        {
          if ( stThisValue != String.Empty )
          {
            stThisValue += ";";
          }

          stThisValue += arrValues [ index ];

        }//END Value exists.

      }
      this.LogDebug ( "stThisValue:" + stThisValue );

      return stThisValue;

    }//END getCheckButtonListFieldValue method

    // =============================================================================== 
    /// <summary>
    /// updateNameFieldValue method.
    /// 
    /// Description:
    ///   This method updates the common TestReport static test fields
    /// 
    /// </summary>
    /// <param name="ReturnedFormFields">List of returned html form fields.</param>
    /// <param name="htmlDataId">The html form field to be udpated.</param>
    /// <param name="CurrentValue">The current form field value.</param>
    /// <param name="OptionList">THe form field option list.</param>
    /// <returns>Returns a string containing the Java Scripts.</returns>
    // ---------------------------------------------------------------------------------
    private string GetNameFieldValue (
      NameValueCollection ReturnedFormFields,
      string htmlDataId )
    {
      this.LogMethod ( "getNameFieldValue" );
      this.LogValue ( "htmlDataId: " + htmlDataId );
      // 
      // Initialise methods variables and objects.
      // 
      string stTitle = String.Empty;
      string stFirstName = String.Empty;
      string stMiddleName = String.Empty;
      string stFamilyName = String.Empty;

      // 
      // Iterate through the option list to compare values.
      // 
      stTitle = this.GetReturnedFormFieldValue ( ReturnedFormFields, htmlDataId + "_Title" );
      stFirstName = this.GetReturnedFormFieldValue ( ReturnedFormFields, htmlDataId + "_FirstName" );
      stMiddleName = this.GetReturnedFormFieldValue ( ReturnedFormFields, htmlDataId + "_MiddleName" );
      stFamilyName = this.GetReturnedFormFieldValue ( ReturnedFormFields, htmlDataId + "_FamilyName" );

      this.LogDebug ( "stFirstName:" + stFirstName + " stMiddleName:" + stMiddleName + " stFamilyName:" + stFamilyName + "\r\n" );

      return stTitle + ";" + stFirstName + ";" + stMiddleName + ";" + stFamilyName;

    }//END getCheckButtonListFieldValue method

    // =============================================================================== 
    /// <summary>
    /// updateNameFieldValue method.
    /// 
    /// Description:
    ///   This method updates the common TestReport static test fields
    /// 
    /// </summary>
    /// <param name="ReturnedFormFields">List of returned html form fields.</param>
    /// <param name="htmlDataId">The html form field to be udpated.</param>
    /// <param name="CurrentValue">The current form field value.</param>
    /// <param name="OptionList">THe form field option list.</param>
    /// <returns>Returns a string containing the Java Scripts.</returns>
    // ---------------------------------------------------------------------------------
    private string GetRangeFieldValue (
      NameValueCollection ReturnedFormFields,
      string htmlDataId )
    {
      this.LogMethod ( "getRangeFieldValue" );
      this.LogValue ( "htmlDataId: " + htmlDataId );
      // 
      // Initialise methods variables and objects.
      // 
      string stLowerValue = String.Empty;
      string stUpperValue = String.Empty;

      // 
      // Iterate through the option list to compare values.
      // 
      stLowerValue = this.GetReturnedFormFieldValue ( ReturnedFormFields, htmlDataId + ClientPage.CONST_FIELD_LOWER_SUFFIX );
      stUpperValue = this.GetReturnedFormFieldValue ( ReturnedFormFields, htmlDataId + ClientPage.CONST_FIELD_UPPER_SUFFIX );

      this.LogDebug ( "stLowerValue: {0},  stUpperValue: {1} " );

      return stLowerValue + ";" + stUpperValue;

    }//END getCheckButtonListFieldValue method

    // =============================================================================== 
    /// <summary>
    /// updateNameFieldValue method.
    /// 
    /// Description:
    ///   This method updates the common TestReport static test fields
    /// 
    /// </summary>
    /// <param name="ReturnedFormFields">List of returned html form fields.</param>
    /// <param name="htmlDataId">The html form field to be udpated.</param>
    /// <param name="CurrentValue">The current form field value.</param>
    /// <param name="OptionList">THe form field option list.</param>
    /// <returns>Returns a string containing the Java Scripts.</returns>
    // ---------------------------------------------------------------------------------
    private string GetAddressFieldValue (
      NameValueCollection ReturnedFormFields,
      string htmlDataId )
    {
      this.LogMethod ( "getNameFieldValue" );
      this.LogValue ( "htmlDataId: " + htmlDataId );
      // 
      // Initialise methods variables and objects.
      // 
      string stAddress1 = String.Empty;
      string stAddress2 = String.Empty;
      string stSuburb = String.Empty;
      string stState = String.Empty;
      string stPostCode = String.Empty;
      string stCountry = String.Empty;

      // 
      // Iterate through the option list to compare values.
      // 
      stAddress1 = this.GetReturnedFormFieldValue ( ReturnedFormFields, htmlDataId + "_Address1" );
      stAddress2 = this.GetReturnedFormFieldValue ( ReturnedFormFields, htmlDataId + "_Address2" );
      stSuburb = this.GetReturnedFormFieldValue ( ReturnedFormFields, htmlDataId + "_Suburb" );
      stState = this.GetReturnedFormFieldValue ( ReturnedFormFields, htmlDataId + "_State" );
      stPostCode = this.GetReturnedFormFieldValue ( ReturnedFormFields, htmlDataId + "_PostCode" );
      stCountry = this.GetReturnedFormFieldValue ( ReturnedFormFields, htmlDataId + "_Country" );

      this.LogDebug ( "\r\n stAddress1:" + stAddress1
        + " stAddress2:" + stAddress2
        + " stSuburb:" + stSuburb
        + " stState:" + stState
        + " stPostCode:" + stPostCode
        + " stCountry:" + stCountry
        + "\r\n" );

      return stAddress1 + ";" + stAddress2 + ";" + stSuburb + ";" + stState + ";" + stPostCode + ";" + stCountry;

    }//END getCheckButtonListFieldValue method

    // =============================================================================== 
    /// <summary>
    ///   This method updates the test table field values.
    /// 
    /// </summary>
    /// <param name="FormField">EvFormField object containing test field data.</param>
    /// <param name="ReturnedFormFields">Containing the returned formfield values.</param>
    /// <returns>Returns a EvFormField object.</returns>
    // ---------------------------------------------------------------------------------
    private Evado.UniForm.Model.EuField UpdateFormTableFields(
      Evado.UniForm.Model.EuField FormField,
      NameValueCollection ReturnedFormFields )
    {
      this.LogMethod ( "updateFormTableFields" );
      this.LogValue ( " FieldId: " + FormField.FieldId );
      // 
      // Iterate through the rows and columns of the table filling the 
      // data object with the test values.
      // 
      for ( int row = 0 ; row < FormField.Table.Rows.Count ; row++ )
      {
        for ( int Col = 0 ; Col < FormField.Table.Header.Length ; Col++ )
        {
          EvTableHeader header = FormField.Table.Header [ Col ];

          if ( header.Text == String.Empty )
          {
            continue;
          }

//
// reset boolean data types as update not resetn selected values.
//
          if ( header.DataType == EvDataTypes.Boolean )
          {
            FormField.Table.Rows [ row ].Column [ Col ] = String.Empty;
          }

          // 
          // construct the test table field name.
          // 
          string tableFieldId = FormField.FieldId + "_" + ( row + 1 ) + "_" + ( Col + 1 );
          this.LogDebug ( "\r\n form fieldId: " + tableFieldId );

          // 
          // Get the table field and update the test field object.
          // 
          string value = this.GetReturnedFormFieldValue ( ReturnedFormFields, tableFieldId );

          // 
          // Does the returned field value exist
          // 
          if ( value != null )
          {
            this.LogDebug ( " value: " + value
               + " DataType: " + FormField.Table.Header [ Col ].DataType );

            //
            // If NA is entered set to numeric null.
            //
            switch ( header.DataType )
            {
              case Evado.Model.EvDataTypes.Numeric:
              {
                if ( value.ToLower ( ) == Evado.Model.EvStatics.CONST_NUMERIC_NOT_AVAILABLE.ToLower ( ) )
                {
                  value = Evado.Model.EvStatics.CONST_NUMERIC_NULL.ToString ( );
                }
                FormField.Table.Rows [ row ].Column [ Col ] = value;
                break;
              }
              case Evado.Model.EvDataTypes.Computed_Field:
              {
                this.UpdateTableComputedColumn ( FormField.Table, Col );
                break;
              }
              default:
              {
                FormField.Table.Rows [ row ].Column [ Col ] = value;
                break;
              }
            }

          }//END value exists.

        }//END column interation loop

      }//END row interation loop

      return FormField;

    }//END updateFormFieldTable method

    // =============================================================================== 
    /// <summary>
    /// This method computes the table computer column calculation.
    /// </summary>
    /// <param name="Table">fEvado.Model.EvTable object</param>
    /// <param name="ColumnId">int column identifier</param>
    /// <returns>String value </returns>
    // ---------------------------------------------------------------------------------
    private void UpdateTableComputedColumn( Evado.Model.EvTable Table, int ColumnId )
    {
      this.LogMethod ( "UpdateTableComputedColumn" );

      String value = String.Empty;
      EvTableHeader header = Table.Header [ ColumnId ];

      if ( String.IsNullOrEmpty ( header.OptionsOrUnit ) == false )
      {
        this.LogMethodEnd ( "UpdateTableComputedColumn" );
        return;
      }

      string formula = header.OptionsOrUnit;
      this.LogDebug ( "Formula: {0}.", formula );

      //
      // The computed column formula selection.
      //
      for ( int rowIndex = 0 ; rowIndex < Table.Rows.Count ; rowIndex++ )
      {
        EvTableRow row = Table.Rows [ rowIndex ];
        float fltValue = 0;
        float fltValue1 = 0;
        float fltValue2 = 0;

        //
        // Iterate through the row values.
        //
        for ( int columnIndex = 0 ; columnIndex < row.Column.Length && columnIndex < Table.Header.Length ; columnIndex++ )
        {
          //
          // Skip the computed column.
          //
          if ( columnIndex == ColumnId )
          {
            continue;
          }

          //
          // create teh column identifier.
          //
          string colId = ( columnIndex + 1 ).ToString ( "00" );
          this.LogDebug ( "colId: {0}.", colId );


          //
          // SKIP if not in the selection column list.
          //
          if ( formula.Contains ( colId ) == false )
          {

            this.LogDebug ( "Continue Colid not found in formula" );
            continue;
          }

          //
          // it not possibel numeric values exit.
          //
          if ( header.DataType != EvDataTypes.Numeric
            && header.DataType != EvDataTypes.Integer
            & header.DataType != EvDataTypes.Multi_Text_Values )
          {
            this.LogDebug ( "Continue column data type not compatible." );
            continue;
          }

          float fltColValue = 0;

          if ( header.DataType == EvDataTypes.Multi_Text_Values )
          {
            fltColValue = EvStatics.SumStringValues ( row.Column [ columnIndex ] );
          }
          else
          {
            fltColValue = EvStatics.getFloat ( row.Column [ columnIndex ] );

            if ( fltColValue == Evado.Model.EvStatics.CONST_NUMERIC_NULL
              || fltColValue == Evado.Model.EvStatics.CONST_NUMERIC_ERROR )
            {
              continue;
            }
          }


          if ( formula.Contains ( EvTableHeader.COMPUTED_FUNCTION_SUM_ROW_COLUMNS ) == true )
          {
            this.LogDebug ( "Row Sum row function found" );
            fltValue += fltColValue;
          }

          if ( header.OptionsOrUnit.Contains ( EvTableHeader.COMPUTED_FUNCTION_MULTIPLE_ROW_COLUMNS ) == true )
          {
            this.LogDebug ( "Row multiply function found" );
            string formula1 = header.OptionsOrUnit.Replace ( EvTableHeader.COMPUTED_FUNCTION_MULTIPLE_ROW_COLUMNS, String.Empty );
            formula1 = formula1.Replace ( "(", String.Empty );
            formula1 = formula1.Replace ( ")", String.Empty );
            String [ ] parms = formula1.Split ( ';' );

            if ( parms.Length < 2 )
            {
              continue;
            }

            //
            // get the first value.
            //
            if ( parms [ 0 ] == colId )
            {
              fltValue1 = fltColValue;
            }

            //
            // get the first value.
            //
            if ( parms [ 1 ] == colId )
            {
              fltValue2 = fltColValue;
            }
          }

          if ( fltValue1 != 0
            & fltValue2 != 0 )
          {
            fltValue = fltValue1 * fltValue2;
          }
        }//END row column iteration loop

        if ( fltValue != 0 )
        {
          row.Column [ ColumnId ] = fltValue.ToString ( );
          this.LogDebug ( "Row: {0}, Column:{1}, value: {2}.", rowIndex, ColumnId, row.Column [ ColumnId ] );
        }
      }//END row iteration loop

      this.LogMethodEnd ( "UpdateTableComputedColumn" );
    }//END UpdateTableComputedColumn method

    // =============================================================================== 
    /// <summary>
    /// getReturnedFormFieldValue method.
    /// 
    /// Description:
    ///   This method generates the Java script object variables for the test.
    /// 
    /// </summary>
    /// <param name="ReturnedFormFields">Name Value Collection</param>
    /// <param name="FormDataId">FormRecord field id to be retrieved.</param>
    /// <returns>Returns a string containing the field value.</returns>
    // ---------------------------------------------------------------------------------
    private string GetReturnedFormFieldValue (
      NameValueCollection ReturnedFormFields,
      String FormDataId )
    {
      this.LogMethod ( "getReturnedFormFieldValue" );
      this.LogDebug ( "FormDataId: " + FormDataId );
      // 
      // Initialise the method variables and objects.
      // 
      String [ ] aKeys;
      int index;

      // 
      // Get names of all keys into a string array.
      // 
      aKeys = ReturnedFormFields.AllKeys;

      // 
      // Iterate the keys to find the value for the selected formDataId
      // 
      for ( index = 0; index < aKeys.Length; index++ )
      {
        String key = aKeys [ index ].ToString ( );
        String [ ] aValues = ReturnedFormFields.GetValues ( aKeys [ index ] );

        // 
        // If there is a match then return the value.
        // 
        if ( aKeys [ index ].ToString ( ).ToLower ( ) == FormDataId.ToLower ( ) )
        {
          this.LogDebug ( "Index: " + index + ", key: " + key
            + " Value: " + aValues [ 0 ] );
          //
          // stReturn the first value.
          //
          return aValues [ 0 ].ToString ( ).Trim ( );
        }

      }//END For loop.

      // 
      // If not found return an empty string.
      // 
      return null;

    }//END getReturnedFormFieldValue method

    // =============================================================================== 
    /// <summary>
    /// getReturnedFormFieldValue method.
    /// 
    /// Description:
    ///   This method generates the Java script object variables for the test.
    /// 
    /// </summary>
    /// <param name="ReturnedFormFields">Name Value Collection</param>
    /// <param name="FormDataId">FormRecord field id to be retrieved.</param>
    /// <returns>Returns a string containing the field value.</returns>
    // ---------------------------------------------------------------------------------
    private string [ ] GetReturnedFormFieldValueArray (
      NameValueCollection ReturnedFormFields,
      String FormDataId )
    {
      this.LogMethod ( "getReturnedFormFieldValueArray method" );
      this.LogDebug ( "FormDataId: " + FormDataId );
      // 
      // Initialise the method variables and objects.
      // 
      String [ ] aKeys;
      int index;

      // 
      // Get names of all keys into a string array.
      // 
      aKeys = ReturnedFormFields.AllKeys;

      // 
      // Iterate the keys to find the value for the selected formDataId
      // 
      for ( index = 0; index < aKeys.Length; index++ )
      {
        String key = aKeys [ index ].ToString ( );
        String [ ] aValues = ReturnedFormFields.GetValues ( aKeys [ index ] );

        string str = String.Empty;
        foreach ( String st in aValues )
        {
          str += st + " > ";
        }
        this.LogDebug ( "aValues: " + str );
        // 
        // If there is a match then return the value.
        // 
        if ( aKeys [ index ].ToString ( ).ToLower ( ) == FormDataId.ToLower ( ) )
        {
          //
          // stReturn the first value.
          //
          return aValues;
        }

      }//END For loop.

      // 
      // If not found return an empty string.
      // 
      return null;

    }//END getReturnedFormFieldValue method

    // ==================================================================================

    /// <summary>
    /// This method searches through the page group fields to find a matching field..
    /// </summary>
    // ---------------------------------------------------------------------------------
    private void UploadPageImages ( )
    {
      this.LogMethod ( "UploadPageImages method" );
      this.LogDebug ( "Global.BinaryFilePath: " + Global.BinaryFilePath );
      this.LogDebug ( "Number of files: " + Context.Request.Files.Count );
      try
      {
        // 
        // Initialise the methods variables.
        // 
        string stExtension = String.Empty;

        // 
        // Exit the method of not files are included in the post back.
        // 
        if ( Context.Request.Files.Count == 0 )
        {
          this.LogDebug ( " No images to upload. Exit method." );

          return;
        }

        //
        // Iterate through the uploaded files.
        //
        foreach ( String requestFieldName in Context.Request.Files.AllKeys )
        {
          this.LogDebug ( "requestFieldName: " + requestFieldName );

          //
          // Skip the dummy test upload.
          //
          if ( requestFieldName == "TestFileUpload" )
          {
            continue;
          }

          // 
          // Get the posted file.
          // 
          HttpPostedFile uploadedFileObject = Context.Request.Files.Get ( requestFieldName );

          //
          // If the file is empty continue to the next file.
          //
          if ( uploadedFileObject.ContentLength == 0 )
          {
            continue;
          }


          string fileName = Path.GetFileName ( uploadedFileObject.FileName );
          fileName = fileName.Replace ( " ", "_" );
          this.LogDebug ( "Uploaded file name: " + fileName );
          this.LogDebug ( "length: " + uploadedFileObject.ContentLength );

          //
          // Retrieve the UniFORM field id.
          // 
          String stFieldId = requestFieldName;
          int index = stFieldId.LastIndexOf ( Evado.UniForm.Model.EuField.CONST_IMAGE_FIELD_SUFFIX );
          stFieldId = stFieldId.Substring ( 0, index );
          this.LogDebug ( "UniFORM FieldId: {0} Value: {1}", stFieldId, fileName );

          //
          // Update the image field value with the uploaded filename.
          //
          this.UserSession.AppData.SetFieldValue ( stFieldId, fileName );

          this.LogDebug ( "UniFORM FieldId: " + stFieldId );

          string fullFilePath = Global.TempPath + fileName;

          this.LogDebug ( "Image file path: " + fullFilePath );

          //
          // Save the file to disk.
          //
          uploadedFileObject.SaveAs ( fullFilePath );

          //
          // set the image to the image service.
          //
          this.SendFileRequest ( fileName, uploadedFileObject.ContentType );

          string stEventContent = "Uploaded Image " + uploadedFileObject.FileName + " saved to "
            + fullFilePath + " at " + DateTime.Now.ToString ( "dd-MMM-yyyy HH:mm:ss" );

          this.LogValue ( stEventContent );
          EventLog.WriteEntry ( Global.EventLogSource, stEventContent, EventLogEntryType.Information );


        }//END upload file iteration loop

      }  // End Try
      catch ( Exception Ex )
      {
        this.LogValue ( "Exception Event:<br>" + Evado.Model.EvStatics.getException ( Ex ) );
      }
      // End catch.

      ///
      /// write out the debug log.
      ///
      Global.OutputtDebugLog ( );

    }//END UploadPageImages method

    // ==================================================================================

    /// <summary>
    /// This method searches through the page group fields to find a matching field..
    /// </summary>
    /// <param name="DataId">String: The html field Id.</param>
    /// <returns>Field object.</returns>
    // ---------------------------------------------------------------------------------
    private Evado.UniForm.Model.EuField GetField (
      String DataId )
    {
      //
      // Iterate through the page groups and fields to find the matching field.
      //
      foreach ( Evado.UniForm.Model.EuGroup group in this.UserSession.AppData.Page.GroupList )
      {
        foreach ( Evado.UniForm.Model.EuField field in group.FieldList )
        {
          String stDataId = field.FieldId;

          if ( stDataId == DataId )
          {
            return field;

          }//END field selection

          if ( stDataId.Contains ( DataId ) == true )
          {
            return field;

          }//END field selection

        }//END Group Field list iteration.

      }//END page group list iteration.

      return null;

    }//END getField method

    // ==================================================================================

    /// <summary>
    /// This method updates the Command parameters with field values.
    /// </summary>
    // ---------------------------------------------------------------------------------
    private void UpdateWebPageCommandObject ( )
    {
      this.LogMethod ( "updateWebPageCommandObject" );
      this.LogDebug ( "Page.EditAccess: " + this.UserSession.AppData.Page.EditAccess );
      this.LogDebug ( "FieldAnnotationList.Count: " + this.UserSession.FieldAnnotationList.Count );
      //
      // Initialise the methods variables and objects.
      //
      Evado.UniForm.Model.EuEditAccess fieldStatus = Evado.UniForm.Model.EuEditAccess.Disabled;
      Evado.UniForm.Model.EuEditAccess groupStatus = Evado.UniForm.Model.EuEditAccess.Disabled;

      //
      // Iterate through the page groups and fields to find the matching field.
      //
      foreach ( Evado.UniForm.Model.EuGroup group in this.UserSession.AppData.Page.GroupList )
      {
        //
        // Set the edit access.
        //
        groupStatus = group.EditAccess;

        //
        // Iterat through the group fields.
        //
        foreach ( Evado.UniForm.Model.EuField field in group.FieldList )
        {
          //
          // Set the edit access.
          //
          fieldStatus = field.EditAccess;

          this.LogDebug ( "Group: " + group.Title
            + ", field.FieldId: " + field.FieldId
            + ", Status: " + fieldStatus );

          if ( field.Type == Evado.Model.EvDataTypes.Read_Only_Text
            || field.Type == Evado.Model.EvDataTypes.External_Image
            || field.Type == Evado.Model.EvDataTypes.Line_Chart
            || field.Type == Evado.Model.EvDataTypes.Null
            || field.Type == Evado.Model.EvDataTypes.Pie_Chart
            || field.Type == Evado.Model.EvDataTypes.Donut_Chart
            || field.Type == Evado.Model.EvDataTypes.Sound
            || field.FieldId == String.Empty )
          {
            this.LogDebug ( " >> FIELD SKIPPED" );
            continue;
          }

          this.LogDebug ( "Group: " + group.Title
            + ", FieldId: " + field.FieldId
            + " - " + field.Title
            + " - " + field.Value
            + " >> METHOD PARAMETER UPDATED " );

          if ( field.Type != Evado.Model.EvDataTypes.Table )
          {
            this.UserSession.PageCommand.AddParameter ( field.FieldId, field.Value );
          }
          else
          {
            this.updateWebPageCommandTableObject ( field );
          }

        }//END Group Field list iteration.

      }//END page group list iteration.

      this.LogDebug ( "Command parameter count: " + this.UserSession.PageCommand.Parameters.Count );

      //
      // Add annotation fields
      //
      for ( int count = 0; count < this.UserSession.FieldAnnotationList.Count; count++ )
      {
        EucKeyValuePair arrAnnotation = this.UserSession.FieldAnnotationList [ count ];

        this.LogDebug ( "Annotation Field: " + arrAnnotation.Key
          + ", Value: " + arrAnnotation.Value );

        this.UserSession.PageCommand.AddParameter ( arrAnnotation.Key, arrAnnotation.Value );
      }

      this.LogDebug ( "Page command: " + this.UserSession.PageCommand.getAsString ( true, true ) );

    }//END updateWebPageCommandObject method

    // ==================================================================================

    /// <summary>
    /// This method updates the Command parameters with field values.
    /// </summary>
    // ---------------------------------------------------------------------------------
    private void updateWebPageCommandTableObject (
      Evado.UniForm.Model.EuField field )
    {
      //
      // Iterate through the rows in the table.
      //
      for ( int iRow = 0; iRow < field.Table.Rows.Count; iRow++ )
      {
        //
        // Iterate through the columns in the table.
        //
        for ( int iCol = 0; iCol < field.Table.ColumnCount; iCol++ )
        {
          //
          // If the cel is not readonly and has a value then add it to the parameters.
          //
          if ( field.Table.Rows [ iRow ].Column [ iCol ] != String.Empty )
          {
            string stName = field.FieldId + "_" + ( iRow + 1 ) + "_" + ( iCol + 1 );
            this.UserSession.PageCommand.AddParameter ( stName, field.Table.Rows [ iRow ].Column [ iCol ] );

          }//END has a value.

        }//END column iteration loop.

      }//END row iteration loop.

    }//END updateWebPageCommandTableObject method


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
    protected void btnPageLeft_OnClick (
      object sender,
      System.EventArgs E )
    {
      this.LogMethod ( "btnPageLeft_OnClick event method" );
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
    protected void btnPageRight_OnClick (
      object sender,
      System.EventArgs E )
    {
      this.LogMethod ( "btnPageRight_OnClick event method" );
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
    private void RequestLogin ( )
    {
      this.LogMethod ( "RequestLogin" );
      this.LogDebug ( "DefaultLogo {0}.", Global.DefaultLogoUrl );
      //
      // Initialise the methods variables and object.s
      //
      this.initialiseHistory ( );
      this.UserSession.AppData.Title = EuLabels.User_Login_Title;
      this.Title = Global.TitlePrefix + " - "+ this.UserSession.AppData.Title;
      this.imgLogo.Src = Global.DefaultLogoUrl;
      this.meetingStatus.Value = Evado.Model.EvMeeting.States.Null.ToString ( );

      this.fsLoginBox.Visible = true;
      this.litExitCommand.Visible = false;
      this.litCommandContent.Visible = false;
      this.litPageContent.Visible = false;
      this.litHistory.Visible = false;
      this.litPageMenu.Visible = false;

      //
      // Reset the meeting parameters.
      //
      this.meetingUrl.Value = String.Empty;
      this.meetingDisplayName.Value = String.Empty;
      this.meetingParameters.Value = String.Empty;
      this.meetingStatus.Value = String.Empty; ;

      this.UserSession.AppData.Page.Exit = new Evado.UniForm.Model.EuCommand ( );

      this.litExitCommand.Text = String.Empty;
      this.__CommandId.Value = EuStatics.CONST_LOGIN_COMMAND_ID.ToString ( );

      //
      // display the logo if one is defined.
      //6
      if ( this.UserSession.AppData.LogoFilename != String.Empty )
      {
        this.UserSession.AppData.LogoFilename = Evado.Model.EvStatics.concatinateHttpUrl (
          Global.StaticImageUrl, this.UserSession.AppData.LogoFilename );

        this.imgLogo.Src = this.UserSession.AppData.LogoFilename.ToLower ( );
      }

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
    protected void btnLogin_OnClick (
      object sender,
      System.EventArgs E )
    {
      this.LogMethod ( "btnLogin_OnClick event method" );
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
    /// 
    /// Description:
    /// This method executes the user login event.
    /// 
    /// </summary>
    /// <param name="sender">Event object</param>
    /// <param name="E">Event arguments</param>
    // ---------------------------------------------------------------------------------
    protected void SendWindowsLoginCommand ( )
    {
      this.LogMethod ( "sendWindowsLoginCommand event method" );


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

      this.UserSession.PageCommand.AddParameter ( Evado.UniForm.Model.EuStatics.PARAMETER_LOGIN_USER_ID, this.UserSession.UserId );
      this.UserSession.PageCommand.AddParameter ( Evado.UniForm.Model.EuStatics.PARAMETER_NETWORK_ROLES, roles );


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
    /// 
    /// Description:
    /// This method executes the user login event.
    /// 
    /// </summary>
    /// <param name="sender">Event object</param>
    /// <param name="E">Event arguments</param>
    // ---------------------------------------------------------------------------------
    private void SendLoginCommand ( String UserId, String Password )
    {
      this.LogMethod ( "SendLoginCommand method" );
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
    protected void btnLogout_Click (
      object sender,
      System.EventArgs E )
    {
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
    protected void requestLogout ( )
    {
      this.LogMethod ( "requestLoogout method" );
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
    public void LogMethod ( String Value )
    {
      string logValue = Evado.Model.EvStatics.CONST_METHOD_START
         + DateTime.Now.ToString ( "dd-MM-yy hh:mm:ss" ) + ": "
         + "Evado.Uniform.Webclient.ClientPage:" + Value + " Method";

      Global.LogValue ( logValue );
    }

    //  =================================================================================
    /// <summary>
    ///   This static method removes a user from the online user list.
    /// 
    /// </summary>
    //   ---------------------------------------------------------------------------------
    public void LogMethodEnd ( String Value )
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
    public void LogValue ( String Value )
    {
      string logValue = DateTime.Now.ToString ( "dd-MM-yy hh:mm:ss" ) + ": "
       + "ClientPage:" + Value;

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
    public void LogValue ( String Format, params object [ ] args )
    {
      string logValue = DateTime.Now.ToString ( "dd-MM-yy hh:mm:ss" ) + ": "
       + "ClientPage:" + String.Format ( Format, args );

      Global.LogValue ( logValue );
    }

    //  =================================================================================
    /// <summary>
    ///   This method log debug the passed value
    /// </summary>
    /// <param name="Value">String: value.</param>
    //   ---------------------------------------------------------------------------------
    public void LogDebug ( String Value )
    {
      string logValue = DateTime.Now.ToString ( "dd-MM-yy hh:mm:ss" ) + ": "
       + "ClientPage:" + Value;

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
    public void LogDebug ( String Format, params object [ ] args )
    {
      string logValue = DateTime.Now.ToString ( "dd-MM-yy hh:mm:ss" ) + ": "
       + "ClientPage:" + String.Format ( Format, args );

      Global.LogDebugValue ( logValue );
    }

    ///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion
    ///================================== END CLASS SOURCE CODE ===========================

    #region Web FormRecord Designer generated code
    override protected void OnInit ( EventArgs e )
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
    private void InitializeComponent ( )
    {

    }
    #endregion
  }
}
