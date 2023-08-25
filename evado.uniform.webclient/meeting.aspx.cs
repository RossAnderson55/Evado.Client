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
using System.Collections.Specialized;
using System.Net.Http;
using System.Text;

///Evado. namespace references.

using Evado.UniForm.Web;
using Evado.UniForm.Model;
using Evado.UniForm.WebClient;

namespace Evado.UniForm.WebClient
{
  /// <summary>
  /// This is the code behind class for the home page.
  /// </summary>
  public partial class MeetingPage : EvPersistentPageState
  {
    #region Class variable initialisations

    private const string SESSION_USER = "EUWC_SESSION";

    private EucSession UserSession = new EucSession ( );

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
      this.LogMethod ( "Page_Load event" );
      try
      {
        this.LogDebug ( "QueryString: " + Request.QueryString.ToString ( ) );

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
        if ( this.IsPostBack == false )
        {
          this.messageDiv.Visible = true;
          this.videoDiv.Visible = false;
          this.videoFrame.Visible = false;
          //
          // Process non post back events.
          //
          this.GetRequestPageCommand ( );
        }

        //
        // select the command type 
        //
        switch ( this.UserSession.PageCommand.Type )
        {
          case Evado.UniForm.Model.EuCommandTypes.Anonymous_Command:
            {
              this.LogDebug ( "Anonymous_Command" );

              //
              // Send the Command to the server.
              //
              this.SendPageCommand ( );

              this.LogValue ( "Initialise video session" );

              //
              // set configure the video meeting settings.
              //
              this.ConfigureVideoMeetingSettings ( );



              break;

            }

          default:
            {
              this.litErrorMessage.Text = EuLabels.Video_Meeting_Parameter_Error_Message;

              break;
            }//END default case

        }//END switch statement

        this.LogMethodEnd ( "Page_Load" );

      } // End Try
      catch ( Exception Ex )
      {
        EvEventLog.LogPageError ( this, Evado.Model.EvStatics.getException ( Ex ) );

        this.LogDebug ( Evado.Model.EvStatics.getException ( Ex ) );

      } // End catch.


      // 
      // Write footer
      // 
      this.litCopyright.Text = Global.AssemblyAttributes.Copyright;
      this.litFooterText.Text = EuLabels.Footer_Text;
      this.litVersion.Text = "Version: " + Global.AssemblyAttributes.FullVersion;

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

          }//END guid found.
          continue;

        }//END external commands found

        if ( parameter == "cu_guid"
          || parameter == "cuguid"
          || parameter == "cguid" ) 
        {
          Guid guid = Evado.Model.EvStatics.getGuid ( Value );

          this.LogDebug ( string.Format ( "Customer Guid: {0} ", guid ) );

          this.UserSession.PageCommand.AddParameter ( Evado.UniForm.Model.EuStatics.CONST_CUSTOMER_GUID, guid );
          continue;
        }

      }//END paraemter iteration loop

      this.LogDebug ( "Finished query parameter iteration loop." );

      this.LogDebug ( this.UserSession.PageCommand.getAsString ( false, true ) );

      this.LogMethodEnd ( "GetRequestPageCommand" );
      return false;

    }//END GetRequestPageCommand method.

    //==================================================================================	
    /// <summary>
    /// this method gets the command Id from the Request URL string.
    /// </summary>
    /// <returns>Bool: true = external command found.</returns>
    // --------------------------------------------------------------------------------
    private void ConfigureVideoMeetingSettings ( )
    {
      this.LogMethod ( "ConfigureVideoMeetingSettings" );
      // 
      // Extract the URL parameters and instantiate the local variables.
      // 
      String meetingUrl = this.UserSession.AppData.GetParameter ( EuAppData.ParameterList.Meeting_Url );
      String displayName = this.UserSession.AppData.GetParameter ( EuAppData.ParameterList.Meeting_DisplayName );
      String meetingParameters = this.UserSession.AppData.GetParameter ( EuAppData.ParameterList.Meeting_Parameters );

      this.LogDebug ( "meetingUrl {0}. ", meetingUrl );
      this.LogDebug ( "displayName {0}. ", displayName );
      this.LogDebug ( "meetingParameters {0}. ", meetingParameters );
      try
      {
        if ( meetingUrl != null )
        {
          //
          // set the html controls visibility.
          //
          this.messageDiv.Visible = false;
          this.videoDiv.Visible = true;
          this.videoFrame.Visible = true;

          //
          // initialise the methods variables and objects.
          //
          string frameUrl = meetingUrl;
          string parameters = String.Empty;

          //
          // define the meeting parameters.
          //
          if ( meetingParameters != String.Empty )
          {
            parameters += meetingParameters;
          }

          if ( displayName != String.Empty )
          {
            parameters += "&displayName=" + displayName;
          }

          if ( parameters.Length > 0 )
          {
            frameUrl += "?" + parameters;
          }
          this.LogDebug ( "frameUrl {0}. ", frameUrl );

          this.videoFrame.Attributes [ "src" ] = frameUrl;

          this.LogDebug ( "videoFrame.src: '{0}'.",
            this.videoFrame.Attributes [ "src" ] );
        }
      }
      catch ( Exception ex )
      {
        this.LogDebug ( Evado.Model.EvStatics.getException ( ex ) );
      }
      this.LogMethodEnd ( "ConfigureVideoMeetingSettings" );

    }//END SetVideoFrameSettings method.

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
      this.UserSession.CommandGuid = Guid.Empty;

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
        this.UserSession = (EucSession) Session [ SESSION_USER ];
      }

      this.LogDebug ( "ApplicationData.Id: " + this.UserSession.AppData.Id );
      this.LogDebug ( "SessionId: " + this.UserSession.ServerSessionId );
      this.LogDebug ( "UserNetworkId: " + this.UserSession.UserId );
      this.LogDebug ( "PageCommand: " + this.UserSession.PageCommand.getAsString ( false, false ) );

      this.LogMethodEnd ( "loadSessionVariables" );

    }//END loadSessionVariables method


    // ==================================================================================
    /// <summary>
    /// This method send the Command back to the server objects.
    /// </summary>
    // ---------------------------------------------------------------------------------
    private void SendPageCommand ( )
    {
      this.LogMethod ( "sendPageCommand" );
      this.LogValue ( "DebugLogOn {0}.", Global.DebugLogOn );
      this.LogDebug ( "Sessionid: " + this.UserSession.ServerSessionId );
      this.LogDebug ( "User NetworkId: " + this.UserSession.UserId );
      this.LogDebug ( "AppDate Url: " + this.UserSession.AppData.Url );
      this.LogDebug ( "Global.WebServiceUrl: " + Global.WebServiceUrl );
      this.LogDebug ( "Global.ClientVersion: " + Global.ClientVersion );
      this.LogDebug ( "GetRequestHeader 'Host' : '{0}'. ", this.GetRequestHeader ( "Host" ) );

      //
      // Display a serialised instance of the object.
      //
      string serialisedText = String.Empty;
      string baseUrl = Global.WebServiceUrl;
      string serviceUri = EuStatics.APPLICATION_SERVICE_CLIENT_RELATIVE_URL + Global.ClientVersion
        + "?command=command&session=" + this.UserSession.ServerSessionId;
      Newtonsoft.Json.JsonSerializerSettings jsonSettings = new Newtonsoft.Json.JsonSerializerSettings
      {
        NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore
      };

      //
      // Replace the default URI with the services URL if provided.
      //
      if ( this.UserSession.AppData.Url != String.Empty )
      {
        baseUrl = this.UserSession.AppData.Url;
      }

      String WebServiceUrl = baseUrl + serviceUri;
      this.LogDebug ( "WebServiceUrl:{0}.", WebServiceUrl );
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

      serialisedText = Newtonsoft.Json.JsonConvert.SerializeObject ( this.UserSession.PageCommand );

      //
      // Initialise the web request.
      //
      this.LogDebug ( "Creating the WebRequest." );

      try
      {
        //
        // The post command 
        //
        serialisedText = this.sendPost ( WebServiceUrl, serialisedText );

        this.LogDebug ( "JSON Serialised text length: " + serialisedText.Length );

        if ( Global.DebugLogOn == true )
        {
          Evado.Model.EvStatics.Files.saveFile ( Global.TempPath, @"meeting-json-data.txt", serialisedText );
        }

        this.LogDebug( "serialisedText {0}. ", serialisedText );

        //
        // deserialise the application data 
        //
        this.UserSession.AppData = new Evado.UniForm.Model.EuAppData ( );

        this.LogDebug ( "Deserialising JSON to Evado.UniForm.Model.EuAppData object." );

        this.UserSession.AppData = Newtonsoft.Json.JsonConvert.DeserializeObject<Evado.UniForm.Model.EuAppData> ( serialisedText );

        this.LogDebug ( "Application object: " + this.UserSession.AppData.getAtString ( ) );

        //
        // Set the anonymouse page access mode.
        // True enables anonymous access mode hiding:
        // - Exit Command
        // - History commands
        // - Page Commands
        //

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

    // =================================================================================
    /// <summary>
    /// This methods sends a post to the web service.
    /// </summary>
    /// <param name="WebServiceUrl">String: The web service URI</param>
    /// <param name="PostContent">String: string content for the web service.</param>
    /// <returns>String: Response Text</returns>
    // ---------------------------------------------------------------------------------
    private String sendPost (
      String WebServiceUrl,
      String PostContent )
    {
      this.LogMethod ( "sendPost" );
      this.LogDebug ( "stWebServiceUrl {0}, Content:\r\n{1}\r\n",
        WebServiceUrl,
        PostContent.Replace ( ",", ",\r\n" ) );
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
         + "Evado.Uniform.Webclient.MeetingPage:" + Value + " Method";

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
      string logValue = DateTime.Now.ToString ( "dd-MM-yy hh:mm:ss" ) 
        + " MeetingPage: " + Value;

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
      string logValue = DateTime.Now.ToString ( "dd-MM-yy hh:mm:ss" )
        + " MeetingPage: " + String.Format ( Format, args );

      Global.LogDebugValue ( logValue );
    }

    //  =================================================================================
    /// <summary>
    ///   This method log debug the passed value
    /// </summary>
    /// <param name="Value">String: value.</param>
    //   ---------------------------------------------------------------------------------
    public void LogDebug ( String Value )
    {
      string logValue = DateTime.Now.ToString ( "dd-MM-yy hh:mm:ss" ) 
        +" MeetingPage: " + Value;

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
      string logValue = DateTime.Now.ToString ( "dd-MM-yy hh:mm:ss" )
        + " MeetingPage: " + String.Format ( Format, args );

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
