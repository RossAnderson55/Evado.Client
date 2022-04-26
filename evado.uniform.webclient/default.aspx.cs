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
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.ComponentModel;
using System.Diagnostics;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Security;
using System.Net;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

///Evado. namespace references.

using Evado.UniForm.Web;
using Evado.UniForm.Model;

namespace Evado.UniForm.WebClient
{
  /// <summary>
  /// This is the code behind class for the home page.
  /// </summary>
  public partial class DefaultPage : EvPersistentPageState
  {
    #region Class variable initialisations

    private const string SESSION_ApplictionDataObject = "EUWC_ApplicationDataObject";
    private const string SESSION_LastPageCommand = "EUWC_LastPageCommand";
    private const string SESSION_ExternalCommand = "EUWC_ExternalCommand";
    private const string SessionPageCommandId = "EUWC_CommandId_";
    private const string SessionGroupCommandId = "EUWC_GroupCommandId_";
    private const string SESSION_ServerSesionId = "EUWC_ServerSesionId";
    private const string SESSION_CookieContainer = "EUWC_CookieContainer";
    private const string Service_CommandHistoryList = "EUWC_CommandHistoryList";
    private const string SESSION_ICON_URL_LIST = "EUWC_IUL";

    private const string PageField_CommandId = "__CommandId";

    private const string CONST_VIDEO_SUFFIX = "_VIDEO";

    private static string Css_Class_Field_Group = "field-group large";
    private static string Css_Class_Group_Title = "group-title";
    private static string Css_Class_Field_Group_Container = "field-group-container";

    public const string stField_LowerSuffix = "_Lower";
    public const string stField_UpperSuffix = "_Upper";
    private const float WidthPixelFactor = 8F;

    private Evado.UniForm.Model.AppData _AppData = new Evado.UniForm.Model.AppData ( );
    private Evado.UniForm.Model.Command _PageCommand = new Evado.UniForm.Model.Command ( );
    private String _PageUrl = String.Empty;
    private String _Server_SessionId = String.Empty;
    private String _UserNetworkId = String.Empty;
    private Guid _CommandGuid = Guid.Empty;

    private StringBuilder _DebugLog = new StringBuilder ( );
    /// <summary>
    /// This list contains a list of the server commands sent to the client.
    /// </summary>
    private List<Evado.UniForm.Model.Command> _CommandHistoryList = new List<Evado.UniForm.Model.Command> ( );

    private CookieContainer _CookieContainer = new CookieContainer ( );

    private Evado.UniForm.Model.Group _CurrentGroup = new Evado.UniForm.Model.Group ( );

    private int _GroupValueColumWidth = 60;
    private int _PanelDisplayGroupIndex = -1;

    private List<KeyValuePair> _FieldAnnotationList = new List<KeyValuePair> ( );

    private List<KeyValuePair> _IconList = new List<KeyValuePair> ( );

    private string _A1 = String.Empty;

    private bool _RequestLogin = false;

    private bool _PlotScriptLoaded = false;

    private Guid LoginCommandId
    {
      get
      {
        return new Guid ( "5d79cd52-3c2b-407c-96d9-000000000000" );
      }
    }

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
      Global.LogMethod ( "Page_Load event" );
      try
      {
        Global.LogClient ( "UserHostAddress: " + Request.UserHostAddress );
        Global.LogDebug ( "UserHostName: " + Request.UserHostName );

        Global.LogDebug ( "LogonUserIdentity IsAuthenticated: " + Request.LogonUserIdentity.IsAuthenticated );
        Global.LogDebug ( "LogonUserIdentity Name: " + Request.LogonUserIdentity.Name );
        Global.LogDebug ( "User.Identity.Name: " + User.Identity.Name );
        Global.LogDebug ( "Authentication Type: " + Global.AuthenticationMode );
        // 
        // Initialise the method variables and objects.
        // 
        this.initialiseGlobalVariables ( );

        //
        // load the session variables.
        //
        this.loadSessionVariables ( );

        //
        // Process post back events.
        //
        if ( this.IsPostBack == true )
        {
          this.getPageCommand ( );
        }
        else
        {
          //
          // Process non post back events.
          //
          this.ReadUrlParameters ( );

          if ( Global.AuthenticationMode == System.Web.Configuration.AuthenticationMode.Windows )
          {
            Global.LogDebug ( "Windows Authentication" );

            this._PageCommand = new Evado.UniForm.Model.Command ( );
            this._PageCommand.Id = Guid.NewGuid ( );
            this._PageCommand.Type = Evado.UniForm.Model.CommandTypes.Network_Login_Command;
          }
        }

        //
        // Read in the Command from the post back event.
        //
        Global.LogDebug ( "CURRENT PageCommand: " + this._PageCommand.getAsString ( false, true ) );
        Global.LogDebug ( "fsLoginBox.Visible: " + this.fsLoginBox.Visible );

        //
        // Send the anonymous command and display the returned page.
        //
        Global.LogDebug ( "PageCommand.Type: " + this._PageCommand.Type );
        switch ( this._PageCommand.Type )
        {
          case Evado.UniForm.Model.CommandTypes.Anonymous_Command:
            {
              Global.LogDebug ( "Anonymous_Command" );

              //
              // Send the Command to the server.
              //
              this.sendPageCommand ( );

              Global.LogClient ( "Commence page generation" );
              //
              // Generate the page layout.
              //
              this.generatePage ( );
              break;

            }
          case Evado.UniForm.Model.CommandTypes.Network_Login_Command:
            {
              Global.LogDebug ( "Network Login_Command" );

              Session [ SESSION_LastPageCommand ] = this._PageCommand;

              this.sendWindowsLoginCommand ( );
              break;
            }
          case Evado.UniForm.Model.CommandTypes.Login_Command:
            {
              Global.LogDebug ( "Login_Command" );

              Session [ SESSION_LastPageCommand ] = this._PageCommand;

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
                this.getPageCommandParameters ( );

                Global.LogDebug ( "CURRENT PageCommand: " + this._PageCommand.getAsString ( false, true ) );

                //
                // Send the Command to the server.
                //
                this.sendPageCommand ( );
                Global.LogDebug ( "LogoFilename: " + this._AppData.LogoFilename );

                //
                // The client recieves a login request to display the login page.
                //
                if ( this._AppData.Status == Evado.UniForm.Model.AppData.StatusCodes.Login_Request )
                {
                  if ( Global.AuthenticationMode == System.Web.Configuration.AuthenticationMode.Windows )
                  {
                    Global.LogClient ( "WINDOW AUTHENTICATION REQUEST LOGIN" );
                    this.sendWindowsLoginCommand ( );
                  }
                  else
                  {
                    Global.LogClient ( "REQUEST LOGIN" );
                    this.RequestLogin ( );
                  }
                }
                else
                {
                  Global.LogClient ( "Commence page generation" );
                  //
                  // Generate the page layout.
                  //
                  this.generatePage ( );
                }
                //
                // output the debug serialisations
                //
                this.outputSerialisedData ( );
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

        Global.LogEvent ( "PAGE GENERATION ERROR: " + Evado.Model.EvStatics.getException ( Ex ) );

      } // End catch.

      Global.LogClient ( "Page generation completed." );
      // 
      // Write footer
      // 
      this.litCopyright.Text = Global.AssemblyAttributes.Copyright;
      this.litFooterText.Text = EuLabels.Footer_Text;
      this.litVersion.Text = "Version: " + Global.AssemblyAttributes.FullVersion + Global.DevStage;


      Global.LogDebugMethodEnd ( "Page_Load" );

      //
      // write out the debug log.
      //
      Global.OutputtDebugLog ( );
      //
      // write out the client log.
      //
      Global.OutputClientLog();

      //
      // Save the icon list.
      //
      Session [ SESSION_ICON_URL_LIST ] = this._IconList;

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
    public void initialiseGlobalVariables ( )
    {
      Global.LogDebugMethod ( "initialiseGlobalVariables" );
      // 
      // Initialise the method variables and objects.
      // 
      this.litPageContent.Text = String.Empty;
      this.litHeaderTitle.Text = String.Empty;
      this.litExitCommand.Text = String.Empty;
      this.litCommandContent.Text = String.Empty;
      this.litHistory.Text = String.Empty;
      this.litPageMenu.Text = String.Empty;
      this._CommandGuid = Guid.Empty;
      this.litCommandContent.Visible = true;
      this.PagedGroups.Visible = false;

      //
      // Initialise the Js Library
      //
      this.litJsLibrary.Text = String.Empty;

      //
      // process post back false steps.
      //
      if ( IsPostBack == false )
      {
        Global.LogDebugMethod ( "IsPostBack == FALSE " );
        Global.LogDebug ( "Global.WebServiceUrl: " + Global.WebServiceUrl );
        Global.LogDebug ( "Global.ImageUrl: " + Global.RelativeBinaryUploadURL );
        Global.LogDebug ( "Global.Debug " + Global.DebugLogOn );
        Global.LogDebug ( "Global.DisplaySerialisation: " + Global.DisplaySerialisation );
        this.fldPassword.Value = String.Empty;

        this.litSerialisedLinks.Visible = Global.DisplaySerialisation;

        //
        // Initialise the Command history list.
        //
        this.initialiseHistory ( );

        this.__CommandId.Value = this.LoginCommandId.ToString ( );

        this.litPageContent.Visible = true;
        if ( Global.EnablePageHistory == true )
        {
          this.litHistory.Visible = true;
        }
        if ( Global.EnablePageMenu == true )
        {
          this.litPageMenu.Visible = true;
        }

        Global.LogDebug ( "END IsPostBack == FALSE " );
      }

      //
      // get the base Url for the page.
      //
      this._PageUrl = this.Request.RawUrl;

      if ( this._PageUrl.Contains ( "?" ) == true )
      {
        int intCount = this.Request.RawUrl.IndexOf ( '?' );
        this._PageUrl = this.Request.RawUrl.Substring ( 0, intCount );
      }
      Global.LogDebug ( "RawUrl: " + this._PageUrl );

    }//END initialiseGlobalVariables method

    // ==================================================================================	
    /// <summary>
    /// Description:
    ///	this method set value to session variables
    ///	
    /// </summary>
    // --------------------------------------------------------------------------------
    public void loadSessionVariables ( )
    {
      Global.LogDebugMethod ( "loadSessionVariables" );
      //
      // Retrieve the application data object.
      //
      if ( Session [ SESSION_ApplictionDataObject ] != null )
      {
        this._AppData = (Evado.UniForm.Model.AppData) Session [ SESSION_ApplictionDataObject ];
      }

      if ( Session [ Service_CommandHistoryList ] != null )
      {
        this._CommandHistoryList = (List<Evado.UniForm.Model.Command>) Session [ Service_CommandHistoryList ];
      }


      if ( Session [ Global.SESSION_USER_ID ] != null )
      {
        this._UserNetworkId = (String) Session [ Global.SESSION_USER_ID ];
      }


      if ( Session [ Global.SESSION_A1 ] != null )
      {
        this._A1 = (String) Session [ Global.SESSION_A1 ];
      }

      if ( Session [ SESSION_CookieContainer ] != null )
      {
        this._CookieContainer = (CookieContainer) Session [ SESSION_CookieContainer ];
      }

      if ( Session [ Service_CommandHistoryList ] != null )
      {
        this._CommandHistoryList = (List<Evado.UniForm.Model.Command>) Session [ Service_CommandHistoryList ];
      }

      if ( Session [ SESSION_LastPageCommand ] != null )
      {
        this._PageCommand = (Evado.UniForm.Model.Command) Session [ SESSION_LastPageCommand ];
      }

      if ( Session [ SESSION_ICON_URL_LIST ] != null )
      {
        this._IconList = (List<KeyValuePair>) Session [ SESSION_ICON_URL_LIST ];
      }

      Global.LogDebug ( "ApplicationData.Id: " + this._AppData.Id );
      Global.LogDebug ( "SessionId: " + this._Server_SessionId );
      Global.LogDebug ( "UserNetworkId: " + this._UserNetworkId );
      Global.LogDebug ( "A1: " + this._A1 );
      Global.LogDebug ( "PageCommand: " + this._PageCommand.getAsString ( false, false ) );
      Global.LogDebug ( "Command History length: " + this._CommandHistoryList.Count );
      Global.LogDebug ( "Icon list length: " + this._IconList.Count );

      Global.LogDebugMethodEnd ( "loadSessionVariables" );

    }//END loadSessionVariables method

    // ==================================================================================
    /// <summary>
    /// This method send the Command back to the server objects.
    /// </summary>
    // ---------------------------------------------------------------------------------
    private void sendPageCommand ( )
    {
      Global.LogDebugMethod ( "sendPageCommand" );
      Global.LogDebug ( "Sessionid: " + this._Server_SessionId );
      Global.LogDebug ( "User NetworkId: " + this._UserNetworkId );
      Global.LogDebug ( "AppDate Url: " + this._AppData.Url );
      Global.LogDebug ( "Global.ClientVersion: " + Global.ClientVersion );

      //
      // Display a serialised instance of the object.
      //
      string serialisedText = String.Empty;
      string stWebServiceUrl = Global.WebServiceUrl;
      HttpWebRequest request;
      Newtonsoft.Json.JsonSerializerSettings jsonSettings = new Newtonsoft.Json.JsonSerializerSettings
      {
        NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore
      };

      //
      // Set the default application if non are set.
      //
      if ( this._PageCommand.ApplicationId == String.Empty )
      {
        this._PageCommand.ApplicationId = "Default";
      }

      //
      // Replace the default URI with the services URL if provided.
      //
      if ( this._AppData.Url != String.Empty )
      {
        stWebServiceUrl = this._AppData.Url;
      }

      //
      // Create the web service Url
      //
      stWebServiceUrl += Global.RelativeWcfRestURL + Global.ClientVersion
        + "?command=command&session=" + this._Server_SessionId;

      Global.LogDebug ( "stWebServiceUrl: " + stWebServiceUrl );

      // Request.UserHostName
      // Add the header data
      //

      this._PageCommand.AddHeader (
        Evado.UniForm.Model.CommandHeaderElements.UserId,
        this._UserNetworkId );

      this._PageCommand.AddHeader (
        Evado.UniForm.Model.CommandHeaderElements.DeviceId,
        Evado.UniForm.Model.EuStatics.CONST_WEB_CLIENT );

      this._PageCommand.AddHeader (
        Evado.UniForm.Model.CommandHeaderElements.DeviceName,
        Evado.UniForm.Model.EuStatics.CONST_WEB_CLIENT );

      this._PageCommand.AddHeader (
        Evado.UniForm.Model.CommandHeaderElements.OSVersion,
        Evado.UniForm.Model.EuStatics.CONST_WEB_NET_VERSION );

      this._PageCommand.AddHeader (
        Evado.UniForm.Model.CommandHeaderElements.User_Url,
        Request.UserHostName );

      this._PageCommand.AddHeader (
        Evado.UniForm.Model.CommandHeaderElements.DateTime,
        DateTime.Now.ToString ( "dd MMM yyyy HH:mm:ss" ) );

      Global.LogEvent ( "SENT: PageCommand: " + this._PageCommand.getAsString ( false, false ) );
      //
      // serialise the Command prior to sending to the web service.
      //
      Global.LogDebug ( "Serialising the PageComment object" );

      serialisedText = Newtonsoft.Json.JsonConvert.SerializeObject ( this._PageCommand );

      //Global.LogDebugValue ( "JSON Command: " + serialisedText );

      //
      // Initialise the web request.
      //
      Global.LogDebug ( "Creating the WebRequest." );

      try
      {
        request = (HttpWebRequest) WebRequest.Create ( stWebServiceUrl );
        request.Method = "POST";
        request.KeepAlive = true;
        request.CookieContainer = this._CookieContainer;
        request.AuthenticationLevel = System.Net.Security.AuthenticationLevel.None;

        this.SetBody ( request, serialisedText );

        // 
        // Get the web service response
        //
        Global.LogDebug ( "Sending the the WebRequest." );

        HttpWebResponse response = (HttpWebResponse) request.GetResponse ( );

        //
        // Extract the cookie collection from the response.
        //
        this._CookieContainer.Add ( response.Cookies );

        //
        // Store the cookie collection to session 
        //
        Session [ SESSION_CookieContainer ] = this._CookieContainer;

        //
        // Convert teh response in to a content string.
        //
        serialisedText = this.ConvertResponseToString ( response );

        Global.LogDebug ( "JSON Serialised text length: " + serialisedText.Length );

        if ( Global.DebugLogOn == true )
        {
          // 
          // Open the stream to the file.
          // 
          using ( StreamWriter sw = new StreamWriter ( Global.TempPath + @"json-data.txt" ) )
          {
            sw.Write ( serialisedText );

          }// End StreamWriter.
        }

        //this.writeDebug = ": " + serialisedText;
        //
        // deserialise the application data 
        //
        this._AppData = new Evado.UniForm.Model.AppData ( );

        Global.LogDebug ( "Deserialising JSON to Evado.UniForm.Model.AppData object." );

        this._AppData = Newtonsoft.Json.JsonConvert.DeserializeObject<Evado.UniForm.Model.AppData> ( serialisedText );

        Global.LogDebug ( "Application object: " + this._AppData.getAtString ( ) );
        Global.LogDebug ( "Page Command count: " + this._AppData.Page.CommandList.Count );

        //
        // Set the anonymouse page access mode.
        // True enables anonymous access mode hiding:
        // - Exit Command
        // - History commands
        // - Page Commands
        //

        Global.LogDebug ( "ExitCommand: " + this._AppData.Page.Exit.getAsString ( false, false ) );
        //
        // Add the exit Command to the history.
        //
        this.addHistoryCommand ( this._AppData.Page.Exit );

        //
        // Reset the panel display group index for the new page data object.
        //
        this._PanelDisplayGroupIndex = -1;

        //
        // Update the user session id
        //
        this._Server_SessionId = this._AppData.SessionId;

        Session [ SESSION_ServerSesionId ] = this._Server_SessionId;

        Global.LogDebug ( "Server_SessionId: " + this._Server_SessionId );
      }
      catch ( Exception Ex )
      {
        this.litErrorMessage.Text = "Web Service Error. " + Evado.Model.EvStatics.getExceptionAsHtml ( Ex );

        Global.LogDebug ( "Web Service Error. " + Evado.Model.EvStatics.getException ( Ex ) ); ;

        EvEventLog.LogPageError ( this, Evado.Model.EvStatics.getException ( Ex ) );

        this._AppData = new Evado.UniForm.Model.AppData ( );
        this._AppData.Id = Guid.NewGuid ( );
        this._AppData.Page.Id = this._AppData.Id;
        this._AppData.Page.Title = "Service Access Error.";
        Evado.UniForm.Model.Group group = this._AppData.Page.AddGroup (
          "Service Access Error Report", Evado.UniForm.Model.EditAccess.Disabled );

        if ( Global.DebugLogOn == true )
        {
          group.Description = "Web Service URL: " + stWebServiceUrl
          + "\r\nWeb Service Error. " + Evado.Model.EvStatics.getException ( Ex );
        }
        else
        {
          group.Description = "Error Occured Accessing the Web Service - contact your administrator.";
        }
      }

      //
      // Store it in session.
      //
      Session [ SESSION_ApplictionDataObject ] = this._AppData;
      Session [ SESSION_LastPageCommand ] = this._PageCommand;

      Global.LogDebugMethodEnd( "sendPageCommand" );

    }//END sendPageCommand method

    // ==================================================================================
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request">HttpWebRequest object</param>
    /// <param name="requestBody">String: text body.</param>
    // ---------------------------------------------------------------------------------
    void SetBody (
      HttpWebRequest request,
      String requestBody )
    {
      if ( requestBody.Length > 0 )
      {
        using ( Stream requestStream = request.GetRequestStream ( ) )
        {
          using ( StreamWriter writer = new StreamWriter ( requestStream ) )
          {
            writer.Write ( requestBody );
          }
        }
      }
    }

    // ==================================================================================
    /// <summary>
    /// This method convers the returned repsonse into a string.
    /// </summary>
    /// <param name="response">HttpWebResponse object containing the web service respoinse</param>
    /// <returns>String containing the reponse content.</returns>
    // ---------------------------------------------------------------------------------
    String ConvertResponseToString (
      HttpWebResponse response )
    {
      Global.LogDebugMethod ( "ConvertResponseToString" );
      //
      // Extract the header for debug.
      //
      Global.LogDebug ( "Status code: " + (int) response.StatusCode + " " + response.StatusCode );

      foreach ( string key in response.Headers.Keys )
      {
        Global.LogDebug ( String.Format ( "{0}: {1}", key, response.Headers [ key ] ) );
      }

      string result = new StreamReader ( response.GetResponseStream ( ) ).ReadToEnd ( );

      Global.LogDebug ( "ConvertResponseToString method FINISHED." );

      return result;
    }

    // ==================================================================================

    /// <summary>
    /// This method searches through the page group fields to find a matching field..
    /// </summary>
    /// <param name="Field">Field: The field object.</param>
    /// <returns>Field object.</returns>
    // ---------------------------------------------------------------------------------
    private void getImagePageField (
      Evado.UniForm.Model.Field Field )
    {
      Global.LogDebugMethod ( "getImagePageField" );

      if ( Field.Type != Evado.Model.EvDataTypes.Image )
      {
        return;
      }
      if ( Global.RelativeBinaryUploadURL == String.Empty
        || Field.FieldId == String.Empty )
      {
        return;
      }

      String stFileName = Global.TempPath + Field.FieldId;

      Evado.UniForm.Model.EuStatics.HttpUploadFileStatusCodes uploadStatus = Evado.UniForm.Model.EuStatics.HttpUploadFile (
        Global.WebServiceUrl = Global.RelativeBinaryUploadURL, stFileName, "file", "image/jpeg" );

      if ( uploadStatus != Evado.UniForm.Model.EuStatics.HttpUploadFileStatusCodes.Completed )
      {
        Global.LogDebug ( "Image " + Field.FieldId + " upload failed. Error Messge: " + uploadStatus );
      }

    }//END getImagePageField method

    // ==================================================================================
    /// <summary>
    /// This method outputs the serialised data values.
    /// </summary>
    // ---------------------------------------------------------------------------------
    private void outputSerialisedData ( )
    {
      Global.LogDebugMethod ( "outSerialisedData" );
      Global.LogDebug ( " Global.ApplicationPath: '" + Global.ApplicationPath + "' " );

      if ( Global.DisplaySerialisation == false )
      {
        Global.LogDebug ( "serialisation is false" );

        Global.LogDebugMethodEnd ( "outSerialisedData " );
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
      Evado.UniForm.Model.Group serialisationGroup = new Evado.UniForm.Model.Group ( "Serialisation", String.Empty, Evado.UniForm.Model.EditAccess.Disabled );
      serialisationGroup.Layout = Evado.UniForm.Model.GroupLayouts.Full_Width;

      serialisedText = Evado.Model.EvStatics.SerialiseXmlObject<Evado.UniForm.Model.AppData> ( this._AppData );

      /// 
      /// Open the stream to the file.
      /// 
      using ( StreamWriter sw = new StreamWriter ( Global.ApplicationPath + @"temp\serialised_application_data.xml" ) )
      {
        sw.Write ( serialisedText );

      }/// End StreamWrite


      Evado.UniForm.Model.Field groupField = serialisationGroup.createHtmlLinkField ( "lnkxmllad", "XML Serialised Application Data Object", "temp/serialised_application_data.xml" );

      serialisedText = Evado.Model.EvStatics.SerialiseXmlObject<Evado.UniForm.Model.Command> ( this._PageCommand );

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
        this._AppData,
        Newtonsoft.Json.Formatting.Indented,
        jsonSettings );


      /// 
      /// Open the stream to the file.
      /// 
      using ( StreamWriter sw = new StreamWriter ( Global.ApplicationPath + @"temp\json_serialised_application_data.txt" ) )
      {
        sw.Write ( serialisedText );

      }/// End StreamWrite

      groupField = serialisationGroup.createHtmlLinkField ( "lnkjsonad", "JSON Serialised Application Data Object", "temp/json_serialised_application_data.txt" );

      ///
      ///  JSON Command 
      ///
      serialisedText = Newtonsoft.Json.JsonConvert.SerializeObject ( this._PageCommand );

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


      Global.LogDebug ( "Serialiation field count:" + serialisationGroup.FieldList.Count );

      if ( serialisationGroup.FieldList.Count > 0 )
      {
        ///
        /// set the field set attributes.
        ///
        StringBuilder sbHtml = new StringBuilder ( );

        this.generateGroupHeader ( sbHtml, serialisationGroup, false );

        foreach ( Evado.UniForm.Model.Field pageField in serialisationGroup.FieldList )
        {
          if ( pageField.Type == Evado.Model.EvDataTypes.Http_Link )
          {
            String stUrl = pageField.Value;
            String stFieldRowStyling = "class='group-row field layout-normal cf " + this.fieldBackgroundColorClass ( pageField ) + "' ";
            String stFieldTitleStyling = "style='' class='cell title cell-link-value cf'";

            Global.LogDebug ( "Field URL: " + stUrl );

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

        stHtml = stHtml.Replace ( Global.RelativeBinaryDownloadURL, "euws/" );

        this.litSerialisedLinks.Text = stHtml;
      }

      Global.LogDebugMethodEnd ( "outSerialisedData " );


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
      foreach ( KeyValuePair valuePair in this._IconList )
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
    public void getPageCommand ( )
    {
      Global.LogDebugMethod ( "getPageCommand" );

      //
      // read in the posted back Command id
      //
      this.readinCommandId ( );

      //
      // If the object is empty or reset is sent then refresh the object from the server.
      //
      if ( this._CommandGuid == Guid.Empty )
      {
        Global.LogDebug ( "Send empty command to the server. " );

        this._CommandGuid = this.LoginCommandId;
      }
      else
      {
        if ( this._CommandGuid != this._PageCommand.Id )
        {
          Global.LogDebug ( "Get the new command object." );

          this._PageCommand = this.getCommandObject ( this._CommandGuid );
        }
        else
        {
          Global.LogDebug ( "Current and previous CommandId match." );
        }
      }
      Global.LogDebug ( "PageCommand: " + this._PageCommand.getAsString ( false, true ) );

      Global.LogMethodEnd ( "getPageCommand" );

    }//END getPageCommand method

    //==================================================================================	
    /// <summary>
    /// this method reads in the external command parameters.
    /// </summary>
    /// <returns>Bool: true = external command found.</returns>
    // --------------------------------------------------------------------------------
    private bool ReadUrlParameters ( )
    {
      Global.LogDebugMethod ( "ReadUrlParameters" );
      // 
      // Extract the URL parameters and instantiate the local variables.
      // 
      int loop1;
      string Key, Value;

      if ( Global.AuthenticationMode != System.Web.Configuration.AuthenticationMode.Windows )
      {
        Global.LogDebug ( "NOT Windows Authentication" );

        this._PageCommand = new Evado.UniForm.Model.Command ( );
        this._UserNetworkId = String.Empty;
        this._A1 = String.Empty;
      }

      // 
      // Load SpecialisationValueCollection object.
      // 
      NameValueCollection coll = Request.QueryString;

      Global.LogDebug ( "Parameter Collection count: " + coll.Count );
      // 
      // Get names of all keys into a string array.
      // 
      String [ ] aKeys = coll.AllKeys;

      if ( aKeys.Length == 0 )
      {
        Global.LogDebug ( "No query string parameters." );
        Global.LogDebugMethodEnd ( "ReadUrlParameters" );
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

        string parameter = Key.ToLower ( );

        Global.LogDebug ( string.Format ( "Parameter: {0} ", Key ) );

        if ( Global.ExternalCommands.ContainsKey ( parameter ) == true )
        {
          Evado.UniForm.Model.Command command = Global.ExternalCommands [ parameter ];
          Guid guid = Evado.Model.EvStatics.getGuid ( Value );

          Global.LogDebug ( string.Format ( "Guid: {0} ", guid ) );

          if ( guid != Guid.Empty )
          {
            command.SetGuid ( guid );
            this._PageCommand = command;
            this._UserNetworkId = this.Session.SessionID;
            Session [ Global.SESSION_USER_ID ] = this._UserNetworkId;
            Session [ SESSION_ExternalCommand ] = this._PageCommand;

            Global.LogDebug ( this._PageCommand.getAsString ( false, true ) );

            Global.LogDebugMethodEnd ( "ReadUrlParameters" );
            return true;

          }//END guid found.

        }//END external commands found
        else
        {
          if ( Key.ToLower ( ) == "id" )
          {
            Guid commandId = Evado.Model.EvStatics.getGuid ( Value );

            this._PageCommand = this.getCommandObject ( commandId );

            Global.LogDebug ( this._PageCommand.getAsString ( false, true ) );

            Global.LogDebugMethodEnd ( "ReadUrlParameters" );
            return true;
          }
        }

      }//END paraemter iteration loop

      Global.LogDebugMethodEnd ( "ReadUrlParameters" );
      return false;

    }//END ReadUrlParameters method.

    // ==================================================================================
    /// <summary>
    /// This method updates the web application with the form field values.
    /// </summary>
    // ---------------------------------------------------------------------------------
    private void readinCommandId ( )
    {
      Global.LogDebugMethod ( "readinCommandId" );

      if ( this.__CommandId.Value.ToLower ( ) == "login" )
      {
        Global.LogDebug ( "Request Login command" );
        this._RequestLogin = true;
        this.requestLogout ( );

        Global.LogDebugMethodEnd ( "readinCommandId" );
        return;
      }

      try
      {
        if ( this.__CommandId.Value.Length == 36 )
        {
          Global.LogDebug ( "Command value: " + this.__CommandId.Value );
          this._CommandGuid = new Guid ( this.__CommandId.Value );
        }
      }
      catch
      {
        Global.LogDebug ( "Command Id not a guid" );
        this._CommandGuid = Guid.Empty;
      }

      Global.LogDebug ( "Post back CommandId: " + this._CommandGuid );
      Global.LogDebugMethodEnd ( "readinCommandId" );

    }//END readinCommandId method

    // ==================================================================================

    /// <summary>
    /// This method returns the page Command matching the Command Id passed to the 
    /// method from the Application data object.
    /// </summary>
    /// <param name="CommandId">GUID: Id for the Command to be retrieved</param>
    /// <returns>CliemtPageCommand object.</returns>
    // ---------------------------------------------------------------------------------
    private Evado.UniForm.Model.Command getCommandObject ( Guid CommandId )
    {
      Global.LogDebugMethod ( "getCommandObject" );
      Global.LogDebug ( "CommandId: " + CommandId );
      try
      {

        if ( CommandId == this.LoginCommandId )
        {
          Global.LogDebug ( "Commandid = LoginCommandId return empty command." );

          Global.LogDebugMethodEnd ( "getCommandObject" );
          return new Evado.UniForm.Model.Command ( );
        }

        //
        // Look for a history Command.
        //
        Evado.UniForm.Model.Command historyCommand = this.getHistoryCommand ( CommandId );

        if ( historyCommand.Id != Guid.Empty
          && historyCommand.Id != this.LoginCommandId )
        {
          Global.LogDebug ( "Return history command: " + historyCommand.Title );
          Global.LogDebugMethodEnd ( "getCommandObject" );
          return historyCommand;
        }

        //
        // if the exit Command then return the exit Command object.
        //
        if ( this._AppData.Page.Exit != null )
        {
          if ( this._AppData.Page.Exit.Id == CommandId )
          {
            Global.LogDebug ( "Returning page exit command: " + this._AppData.Page.Exit.Title );

            Global.LogDebugMethodEnd ( "getCommandObject" );
            return this._AppData.Page.Exit;
          }
        }

        //
        // Iterate through the list to find the correct Command.
        //
        foreach ( Evado.UniForm.Model.Command command in this._AppData.Page.CommandList )
        {

          if ( command.Id == CommandId )
          {
            Global.LogDebug ( "Returning page` command: " + command.Title );
            Global.LogDebugMethodEnd ( "getCommandObject" );
            return command;
          }
        }

        //
        // Iterate through the list group to find the correct Command.
        //
        foreach ( Evado.UniForm.Model.Group group in this._AppData.Page.GroupList )
        {
          //
          // Iterate through the list to find the correct Command.
          //
          foreach ( Evado.UniForm.Model.Command command in group.CommandList )
          {
            Global.LogDebug ( "Group {0}, command: id {1} - {2}", group.Title, command.Id, command.Title );

            if ( command.Id == CommandId )
            {
              Global.LogDebug ( "Returning page group " + group.Title
                + " command: " + command.Title );

              Global.LogDebugMethodEnd ( "getCommandObject" );
              return command;
            }
          }//END iteration loop

        }//END iteration loop
      }
      catch ( Exception Ex )
      {
        Global.LogDebug ( Evado.Model.EvStatics.getException ( Ex ) );
      }
      Global.LogDebug ( "No command found." );
      Global.LogDebugMethodEnd ( "getCommandObject" );

      return new Evado.UniForm.Model.Command ( );

    }//END getCommand method

    ///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region  update page methods

    // ==================================================================================
    /// <summary>
    /// This method updates the  application data with the form field values.
    /// </summary>
    // ---------------------------------------------------------------------------------
    private void getPageCommandParameters ( )
    {
      Global.LogDebugMethod ( "getPageCommandParameters" );
      //
      // If the Command method is to upate the page then update the data object with 
      // Page field values.
      //
      if ( this._PageCommand.Method != Evado.UniForm.Model.ApplicationMethods.Save_Object
        && this._PageCommand.Method != Evado.UniForm.Model.ApplicationMethods.Delete_Object
        && this._PageCommand.Method != Evado.UniForm.Model.ApplicationMethods.Custom_Method )
      {
        return;
      }

      Global.LogDebug ( "Updating command parameters. " );

      //
      // Upload the page images.
      //
      this.UploadPageImages ( );

      //
      // Get the data from the returned page fields.
      //
      this.getPageDataValues ( );

      //
      // Get the data from the returned page fields.
      //
      this.updateFieldAnnotations ( );

      //
      // Update the Command parmaters witih the page values.
      //
      this.updateWebPageCommandObject ( );

    }//END updateApplicationDataObject method

    // ==================================================================================
    /// <summary>
    /// This method updates the web application with the form field values.
    /// </summary>
    // ---------------------------------------------------------------------------------
    private void getPageDataValues ( )
    {
      Global.LogDebugMethod ( "getPageDataValues" );
      //
      // Get the field collection.
      //
      NameValueCollection ReturnedFormFields = Request.Form;

      // 
      // Get names of all keys into a string array.
      // 
      String [ ] aKeys = ReturnedFormFields.AllKeys;

      Global.LogDebug ( "Key length: " + aKeys.Length );

      // 
      // Iterate the keys to find the value for the selected formDataId
      // 
      for ( int loop1 = 0; loop1 < aKeys.Length; loop1++ )
      {
        Global.LogDebug ( aKeys [ loop1 ] + " >> " + ReturnedFormFields.Get ( aKeys [ loop1 ] ) );
      }

      // 
      // Iterate through the test fields updating the fields that have changed.
      // 
      foreach ( Evado.UniForm.Model.Group group in this._AppData.Page.GroupList )
      {
        //
        // Set the group edit access for inherited edit access.
        //
        if ( this._AppData.Page.EditAccess == Evado.UniForm.Model.EditAccess.Enabled
          && group.EditAccess == Evado.UniForm.Model.EditAccess.Inherited )
        {
          group.EditAccess = Evado.UniForm.Model.EditAccess.Enabled;
        }

        for ( int count = 0; count < group.FieldList.Count; count++ )
        {
          group.FieldList [ count ] = this.updateFormField (
            group.FieldList [ count ],
            ReturnedFormFields,
            group.EditAccess );

          Global.LogDebug ( group.FieldList [ count ].FieldId
            + " > " + group.FieldList [ count ].Title
            + " >> " + group.FieldList [ count ].Type
            + " >>> " + group.FieldList [ count ].Value );

        }//END test field iteration.

      }//END the iteration loop.

      Global.LogDebugMethodEnd ( "getPageDataValues" );

    }//END getPageDataValues method

    // ==================================================================================

    /// <summary>
    /// This method updates the new field annotations.
    /// </summary>
    // ---------------------------------------------------------------------------------
    private void updateFieldAnnotations ( )
    {
      Global.LogDebugMethod ( "updateFieldAnnotations" );
      ///
      /// Get the field collection.
      ///
      NameValueCollection ReturnedFormFields = Request.Form;

      /// 
      /// Get names of all keys into a string array.
      /// 
      String [ ] aKeys = ReturnedFormFields.AllKeys;

      Global.LogDebug ( "Key length: " + aKeys.Length );

      /// 
      /// Iterate the keys to find the value for the selected formDataId
      /// 
      for ( int loop1 = 0; loop1 < aKeys.Length; loop1++ )
      {
        KeyValuePair keyPair = new KeyValuePair ( );
        ///
        /// Skip all non annotation and returned field values.
        ///
        if ( aKeys [ loop1 ].Contains ( Evado.UniForm.Model.Field.CONST_FIELD_QUERY_SUFFIX ) == false
          && aKeys [ loop1 ].Contains ( Evado.UniForm.Model.Field.CONST_FIELD_ANNOTATION_SUFFIX ) == false )
        {
          continue;
        }

        Global.LogDebug ( "" + aKeys [ loop1 ] + " >> " + ReturnedFormFields.Get ( aKeys [ loop1 ] ) );

        int inAnnotationKey = this.getAnnotationIndex ( aKeys [ loop1 ] );

        Global.LogDebug ( " inAnnotationKey: " + inAnnotationKey );
        ///
        /// Get the data value.
        ///
        if ( inAnnotationKey < 0 )
        {
          Global.LogDebug ( " >> New Item" );
          ///
          /// Set the object value and add it to the field annotation list.
          ///
          keyPair.Key = aKeys [ loop1 ];
          keyPair.Value = ReturnedFormFields.Get ( aKeys [ loop1 ] );

          Global.LogDebug ( " Key: " + keyPair.Key + " value: " + keyPair.Value );

          this._FieldAnnotationList.Add ( keyPair );

        }
        else
        {
          ///
          /// Update the annotated value.
          ///
          keyPair = this._FieldAnnotationList [ inAnnotationKey ];
          keyPair.Value = ReturnedFormFields.Get ( aKeys [ loop1 ] );
        }

      }//END return field values.

      for ( int count = 0; count < this._FieldAnnotationList.Count; count++ )
      {
        KeyValuePair keyPair = this._FieldAnnotationList [ count ];

        Global.LogDebug ( "Key: " + keyPair.Key + " >> " + keyPair.Value );
      }

      Global.LogDebug ( "FieldAnnotationList: " + this._FieldAnnotationList.Count );

    }//END updateFieldAnnotations method

    // =============================================================================== 
    /// <summary>
    /// This method returns the annotation's list index.
    /// </summary>
    /// <param name="Key">The page annotation field id</param>
    /// <returns>Int: the annotation list index.</returns>
    // ---------------------------------------------------------------------------------
    private int getAnnotationIndex ( String Key )
    {
      ///this.writeDebug = "<hr/>Evado.UniForm.WebClient.DefaultPage.getAnnotationIndex method.  Key: " + Key
      ///  + " AnnotationList count: " + this._FieldAnnotationList.Count ;
      ///
      /// Iterate through the annotation list to find a matching element
      ///
      for ( int i = 0; i < this._FieldAnnotationList.Count; i++ )
      {
        ///
        /// Get annotation.
        ///
        KeyValuePair annotation = this._FieldAnnotationList [ i ];
        /// this.writeDebug = "[0]" + annotation [ 0 ];

        ///
        /// Return the matching value.
        ///
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
    private Evado.UniForm.Model.Field updateFormField (
      Evado.UniForm.Model.Field FormField,
      NameValueCollection ReturnedFormFields,
      Evado.UniForm.Model.EditAccess GroupStatus )
    {
      Global.LogDebugMethod ( "updateFormField" );
      Global.LogDebug ( "FormField.DataId: " + FormField.FieldId );
      Global.LogDebug ( "FormField.DataType: " + FormField.Type );
      Global.LogDebug ( "FormField.Status: " + FormField.EditAccess );
      Global.LogDebug ( "GroupStatus: " + GroupStatus );

      // 
      // Initialise methods variables and objects.
      // 
      string stValue = String.Empty;
      string stAnnotation = String.Empty;
      string stQuery = String.Empty;
      string stAssessmentStatus = String.Empty;

      if ( GroupStatus == Evado.UniForm.Model.EditAccess.Enabled
        && FormField.EditAccess == Evado.UniForm.Model.EditAccess.Inherited )
      {
        FormField.EditAccess = Evado.UniForm.Model.EditAccess.Enabled;
      }

      //
      // If a binary or image file return it without processing.
      //
      if ( FormField.Type == Evado.Model.EvDataTypes.Binary_File
        || FormField.Type == Evado.Model.EvDataTypes.Image )
      {
        Global.LogDebug ( "Binary or Image field found but not processed" );
        return FormField;
      }

      /**********************************************************************************/
      // 
      // If the test is in EDIT mode update the fields values.
      // 
      if ( FormField.EditAccess == Evado.UniForm.Model.EditAccess.Enabled )
      {
        //
        // If field type is a single value update it 
        //
        switch ( FormField.Type )
        {
          case Evado.Model.EvDataTypes.Check_Box_List:
            {
              FormField.Value = this.getCheckButtonListFieldValue (
                ReturnedFormFields,
                FormField.FieldId,
                FormField.Value,
                FormField.OptionList.Count );
              break;
            }
          case Evado.Model.EvDataTypes.Address:
            {
              FormField.Value = this.getAddressFieldValue (
                ReturnedFormFields,
                FormField.FieldId );
              break;
            }

          case Evado.Model.EvDataTypes.Name:
            {
              FormField.Value = this.getNameFieldValue (
                ReturnedFormFields,
                FormField.FieldId );
              break;
            }

          case Evado.Model.EvDataTypes.Streamed_Video:
            {
              // 
              // Iterate through the option list to compare values.
              // 
              string videoUrl = this.getReturnedFormFieldValue ( ReturnedFormFields, FormField.FieldId );

              Global.LogDebug ( "videoUrl:" + videoUrl );

              FormField.Value = videoUrl;
              break;
            }
          case Evado.Model.EvDataTypes.Http_Link:
            {
              // 
              // Iterate through the option list to compare values.
              // 
              string httpUrl = this.getReturnedFormFieldValue ( ReturnedFormFields, FormField.FieldId + Field.CONST_HTTP_URL_FIELD_SUFFIX );
              string httpTitle = this.getReturnedFormFieldValue ( ReturnedFormFields, FormField.FieldId + Field.CONST_HTTP_TITLE_FIELD_SUFFIX );

              Global.LogDebug ( "httpUrl:" + httpUrl + " httpTitle:" + httpTitle );

              FormField.Value = httpUrl + "^" + httpTitle;
              break;
            }

          case Evado.Model.EvDataTypes.Integer_Range:
          case Evado.Model.EvDataTypes.Float_Range:
          case Evado.Model.EvDataTypes.Double_Range:
          case Evado.Model.EvDataTypes.Date_Range:
            {
              FormField.Value = this.getRangeFieldValue (
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
              FormField = this.updateFormTableFields (
                           FormField,
                           ReturnedFormFields );
              break;
            }
          default:
            {
              stValue = this.getReturnedFormFieldValue ( ReturnedFormFields, FormField.FieldId );

              Global.LogDebug ( "Field stValue: " + stValue );
              // 
              // Does the returned field value exist
              // 
              if ( stValue != null )
              {
                if ( FormField.Value != stValue )
                {
                  if ( FormField.Type == Evado.Model.EvDataTypes.Numeric )
                  {
                    Global.LogDebug ( "Numeric Field Change: Id: '" + FormField.FieldId
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
      Global.LogMethod ( "getSignatureFieldValue" );
      Global.LogClient ( "htmlDataId: " + htmlDataId );
      // 
      // Initialise methods variables and objects.
      // 
      string signatureValueFieldId = htmlDataId + "_sig";
      string signatureNameFieldId = htmlDataId + "_name";

      String stSignature = this.getReturnedFormFieldValue ( ReturnedFormFields, signatureValueFieldId );
      string stName = this.getReturnedFormFieldValue ( ReturnedFormFields, signatureNameFieldId );

      Global.LogClient ( "stSignature: " + stSignature );
      Global.LogClient ( "stName: " + stName );

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

      Global.LogClient ( "Converting signature to signatureBlock object." );
      Evado.Model.EvSignatureBlock signatureBlock = new Evado.Model.EvSignatureBlock ( );
      signatureBlock.Signature = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Evado.Model.EvSegement>> ( stSignature );
      signatureBlock.Name = stName;
      signatureBlock.DateStamp = DateTime.Now;

      string stSignatureBlock = Newtonsoft.Json.JsonConvert.SerializeObject ( signatureBlock );
      Global.LogDebug ( "stSignatureBlock:" + stSignatureBlock );

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
    private string getCheckButtonListFieldValue (
      NameValueCollection ReturnedFormFields,
      string htmlDataId,
      string CurrentValue,
      int OptionListCount )
    {
      Global.LogMethod ( "getCheckButtonListFieldValue" );
      Global.LogClient ( "htmlDataId: " + htmlDataId );
      Global.LogClient ( "OptionList: " + OptionListCount );
      // 
      // Initialise methods variables and objects.
      // 
      string [ ] arrValues = new String [ 0 ];
      string stThisValue = String.Empty;

      arrValues = this.getReturnedFormFieldValueArray ( ReturnedFormFields, htmlDataId );

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
      Global.LogDebug ( "stThisValue:" + stThisValue );

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
    private string getNameFieldValue (
      NameValueCollection ReturnedFormFields,
      string htmlDataId )
    {
      Global.LogMethod ( "getNameFieldValue" );
      Global.LogClient ( "htmlDataId: " + htmlDataId );
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
      stTitle = this.getReturnedFormFieldValue ( ReturnedFormFields, htmlDataId + "_Title" );
      stFirstName = this.getReturnedFormFieldValue ( ReturnedFormFields, htmlDataId + "_FirstName" );
      stMiddleName = this.getReturnedFormFieldValue ( ReturnedFormFields, htmlDataId + "_MiddleName" );
      stFamilyName = this.getReturnedFormFieldValue ( ReturnedFormFields, htmlDataId + "_FamilyName" );

      Global.LogDebug ( "stFirstName:" + stFirstName + " stMiddleName:" + stMiddleName + " stFamilyName:" + stFamilyName + "\r\n" );

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
    private string getRangeFieldValue (
      NameValueCollection ReturnedFormFields,
      string htmlDataId )
    {
      Global.LogMethod ( "getRangeFieldValue" );
      Global.LogClient ( "htmlDataId: " + htmlDataId );
      // 
      // Initialise methods variables and objects.
      // 
      string stLowerValue = String.Empty;
      string stUpperValue = String.Empty;

      // 
      // Iterate through the option list to compare values.
      // 
      stLowerValue = this.getReturnedFormFieldValue ( ReturnedFormFields, htmlDataId + DefaultPage.stField_LowerSuffix );
      stUpperValue = this.getReturnedFormFieldValue ( ReturnedFormFields, htmlDataId + DefaultPage.stField_UpperSuffix );

      Global.LogDebug ( "stLowerValue:" + stLowerValue + " stUpperValue:" + stUpperValue );

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
    private string getAddressFieldValue (
      NameValueCollection ReturnedFormFields,
      string htmlDataId )
    {
      Global.LogMethod ( "getNameFieldValue" );
      Global.LogClient ( "htmlDataId: " + htmlDataId );
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
      stAddress1 = this.getReturnedFormFieldValue ( ReturnedFormFields, htmlDataId + "_Address1" );
      stAddress2 = this.getReturnedFormFieldValue ( ReturnedFormFields, htmlDataId + "_Address2" );
      stSuburb = this.getReturnedFormFieldValue ( ReturnedFormFields, htmlDataId + "_Suburb" );
      stState = this.getReturnedFormFieldValue ( ReturnedFormFields, htmlDataId + "_State" );
      stPostCode = this.getReturnedFormFieldValue ( ReturnedFormFields, htmlDataId + "_PostCode" );
      stCountry = this.getReturnedFormFieldValue ( ReturnedFormFields, htmlDataId + "_Country" );

      Global.LogDebug ( "\r\n stAddress1:" + stAddress1
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
    /// updateFormFieldTable method.
    /// 
    /// Description:
    ///   This method updates the test table field values.
    /// 
    /// </summary>
    /// <param name="FormField">EvFormField object containing test field data.</param>
    /// <param name="ReturnedFormFields">Containing the returned formfield values.</param>
    /// <returns>Returns a EvFormField object.</returns>
    // ---------------------------------------------------------------------------------
    private Evado.UniForm.Model.Field updateFormTableFields (
      Evado.UniForm.Model.Field FormField,
      NameValueCollection ReturnedFormFields )
    {
      Global.LogMethod ( "updateFormTableFields method." );
      Global.LogClient ( " FieldId: " + FormField.FieldId );
      // 
      // Iterate through the rows and columns of the table filling the 
      // data object with the test values.
      // 
      for ( int row = 0; row < FormField.Table.Rows.Count; row++ )
      {
        for ( int Col = 0; Col < FormField.Table.ColumnCount; Col++ )
        {
          // 
          // construct the test table field name.
          // 
          string tableFieldId = FormField.FieldId + "_" + ( row + 1 ) + "_" + ( Col + 1 );
          Global.LogDebug ( "\r\n form fieldId: " + tableFieldId );

          // 
          // Get the table field and update the test field object.
          // 
          string value = this.getReturnedFormFieldValue ( ReturnedFormFields, tableFieldId );

          // 
          // Does the returned field value exist
          // 
          if ( value != null )
          {
            Global.LogDebug ( " value: " + value
               + " TypeId: " + FormField.Table.Header [ Col ].TypeId );

            //
            // If NA is entered set to numeric null.
            //
            if ( FormField.Table.Header [ Col ].TypeId == Evado.Model.EvDataTypes.Numeric )
            {
              if ( value.ToLower ( ) == Evado.Model.EvStatics.CONST_NUMERIC_NOT_AVAILABLE.ToLower ( ) )
              {
                value = Evado.Model.EvStatics.CONST_NUMERIC_NULL.ToString ( );
              }
            }

            FormField.Table.Rows [ row ].Column [ Col ] = value;


          }//END value exists.

        }//END column interation loop

      }//END row interation loop

      return FormField;

    }//END updateFormFieldTable method

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
    private string getReturnedFormFieldValue (
      NameValueCollection ReturnedFormFields,
      String FormDataId )
    {
      Global.LogDebugMethod ( "getReturnedFormFieldValue method" );
      Global.LogDebug ( "FormDataId: " + FormDataId );
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
          Global.LogDebug ( "Index: " + index + ", key: " + key
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
    private string [ ] getReturnedFormFieldValueArray (
      NameValueCollection ReturnedFormFields,
      String FormDataId )
    {
      Global.LogDebugMethod ( "getReturnedFormFieldValueArray method" );
      Global.LogDebug ( "FormDataId: " + FormDataId );
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
        Global.LogDebug ( "aValues: " + str );
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
      Global.LogDebugMethod ( "UploadPageImages method" );
      Global.LogDebug ( "Global.ImageFilePath: " + Global.BinaryFilePath );
      Global.LogDebug ( "Number of files: " + Context.Request.Files.Count );
      //try
      //{
      // 
      // Initialise the methods variables.
      // 
      string stExtension = String.Empty;

      // 
      // Exit the method of not files are included in the post back.
      // 
      if ( Context.Request.Files.Count == 0 )
      {
        Global.LogDebug ( " No images to upload. Exit method." );

        return;
      }

      //
      // Iterate through the uploaded files.
      //
      foreach ( String requestFieldName in Context.Request.Files.AllKeys )
      {
        Global.LogDebug ( "requestFieldName: " + requestFieldName );

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
        Global.LogDebug ( "Uploaded file name: " + fileName );
        Global.LogDebug ( "length: " + uploadedFileObject.ContentLength );

        //
        // Retrieve the UniFORM field id.
        // 
        String stFieldId = requestFieldName;
        int index = stFieldId.LastIndexOf ( Evado.UniForm.Model.Field.CONST_IMAGE_FIELD_SUFFIX );
        stFieldId = stFieldId.Substring ( 0, index );
        Global.LogDebug ( "UniFORM FieldId: {0} Value: {1}", stFieldId, fileName );

        //
        // Update the image field value with the uploaded filename.
        //
        this._AppData.SetFieldValue ( stFieldId, fileName );

        Global.LogDebug ( "UniFORM FieldId: " + stFieldId );

        string stFilePath = Global.BinaryFilePath + fileName;

        Global.LogDebug ( "Image file path: " + stFilePath );

        //
        // Save the file to disk.
        //
        uploadedFileObject.SaveAs ( stFilePath );

        //
        // set the image to the image service.
        //
        this.sendBinaryFileToImageService ( stFilePath, uploadedFileObject.ContentType );

        string stEventContent = "Uploaded Image " + uploadedFileObject.FileName + " saved to "
          + stFilePath + " at " + DateTime.Now.ToString ( "dd-MMM-yyyy HH:mm:ss" );

        EventLog.WriteEntry ( Global.EventLogSource, stEventContent, EventLogEntryType.Information );


      }//END upload file iteration loop

      /*}  // End Try
      catch ( Exception Ex )
      {
        EventLog.WriteEntry ( Global.EventLogSource,
          Evado.Model.EvStatics.getHtmlAsString ( this._DebugLog.ToString ( ) ) + "\r\n"
          + Evado.Model.EvStatics.getException ( Ex ),
          EventLogEntryType.Error );

        Global.WriteDebugLogLine ( "<p>Exception Event:<br>" + Evado.Model.EvStatics.getExceptionAsHtml ( Ex ) + "</p>" );

      }*/
      // End catch.

      ///
      /// write out the debug log.
      ///
      Global.OutputtDebugLog ( );

    }//END UploadPageImages method

    // ==================================================================================

    /// <summary>
    /// This method sends an image's content to the image upload service.
    /// </summary>
    /// <param name="ImageFilePath">Field: The field object.</param>
    // ---------------------------------------------------------------------------------
    private void sendBinaryFileToImageService ( String ImageFilePath, String MimeType )
    {
      Global.LogDebugMethod ( "sendBinaryFileToImageService" );
      Global.LogDebug ( "ImageUploadServiceUrl: " + Global.RelativeBinaryUploadURL );
      Global.LogDebug ( "FileName: " + ImageFilePath );

      string stUploadUrl = Global.RelativeBinaryUploadURL;

      // 
      // Validate that there are valid upload URLs.
      // 
      if ( Global.WebServiceUrl + stUploadUrl == String.Empty
        || ImageFilePath == String.Empty )
      {
        Global.LogDebug ( "Service Url or data id are null. " );
        Global.LogDebugMethodEnd ( "sendBinaryFileToImageService" );
        return;
      }

      if ( stUploadUrl.Contains ( "http://" ) == false
        && stUploadUrl.Contains ( "https://" ) == false )
      {
        stUploadUrl = Global.WebServiceUrl + stUploadUrl;
      }

      Global.LogDebug ( "Upload Url: " + stUploadUrl );

      try
      {
        Evado.UniForm.Model.EuStatics.HttpUploadFileStatusCodes uploadStatus = Evado.UniForm.Model.EuStatics.HttpUploadFile (
          stUploadUrl,
          ImageFilePath, "file", MimeType );

        if ( uploadStatus != Evado.UniForm.Model.EuStatics.HttpUploadFileStatusCodes.Completed )
        {
          Global.LogDebug ( "Image " + ImageFilePath + " upload failed. Error Messge: " + uploadStatus );

          EventLog.WriteEntry ( Global.EventLogSource,
            "Image " + ImageFilePath + " upload failed. Error Messge: " + uploadStatus,
            EventLogEntryType.Error );
        }
      }
      catch ( Exception Ex )
      {
        Global.LogEvent ( Evado.Model.EvStatics.getException ( Ex ) );
      }
      Global.LogDebugMethodEnd ( "sendBinaryFileToImageService" );

    }//END getImagePageField method

    // ==================================================================================

    /// <summary>
    /// This method searches through the page group fields to find a matching field..
    /// </summary>
    /// <param name="DataId">String: The html field Id.</param>
    /// <returns>Field object.</returns>
    // ---------------------------------------------------------------------------------
    private Evado.UniForm.Model.Field getField ( String DataId )
    {
      //
      // Iterate through the page groups and fields to find the matching field.
      //
      foreach ( Evado.UniForm.Model.Group group in this._AppData.Page.GroupList )
      {
        foreach ( Evado.UniForm.Model.Field field in group.FieldList )
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
    private void updateWebPageCommandObject ( )
    {
      Global.LogDebugMethod ( "updateWebPageCommandObject method. "
        + " Page.EditAccess: " + this._AppData.Page.EditAccess
        + " FieldAnnotationList.Count: " + this._FieldAnnotationList.Count );
      //
      // Initialise the methods variables and objects.
      //
      Evado.UniForm.Model.EditAccess fieldStatus = Evado.UniForm.Model.EditAccess.Disabled;
      Evado.UniForm.Model.EditAccess groupStatus = Evado.UniForm.Model.EditAccess.Disabled;

      //
      // Iterate through the page groups and fields to find the matching field.
      //
      foreach ( Evado.UniForm.Model.Group group in this._AppData.Page.GroupList )
      {
        //
        // Set the edit access.
        //
        groupStatus = group.EditAccess;

        if ( group.EditAccess == Evado.UniForm.Model.EditAccess.Inherited )
        {
          groupStatus = this._AppData.Page.EditAccess;
        }

        //
        // Iterat through the group fields.
        //
        foreach ( Evado.UniForm.Model.Field field in group.FieldList )
        {
          //
          // Set the edit access.
          //
          fieldStatus = field.EditAccess;

          if ( field.EditAccess == Evado.UniForm.Model.EditAccess.Inherited )
          {
            fieldStatus = groupStatus;
          }

          Global.LogDebug ( "Group: " + group.Title
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
            Global.LogDebug ( " >> FIELD SKIPPED" );
            continue;
          }

          Global.LogDebug ( "Group: " + group.Title
            + ", FieldId: " + field.FieldId
            + " - " + field.Title
            + " - " + field.Value
            + " >> METHOD PARAMETER UPDATED " );

          if ( field.Type != Evado.Model.EvDataTypes.Table )
          {
            this._PageCommand.AddParameter ( field.FieldId, field.Value );
          }
          else
          {
            this.updateWebPageCommandTableObject ( field );
          }

        }//END Group Field list iteration.

      }//END page group list iteration.

      Global.LogDebug ( "Command parameter count: " + this._PageCommand.Parameters.Count );

      //
      // Add annotation fields
      //
      for ( int count = 0; count < this._FieldAnnotationList.Count; count++ )
      {
        KeyValuePair arrAnnotation = this._FieldAnnotationList [ count ];

        Global.LogDebug ( "Annotation Field: " + arrAnnotation.Key
          + ", Value: " + arrAnnotation.Value );

        this._PageCommand.AddParameter ( arrAnnotation.Key, arrAnnotation.Value );
      }

      Global.LogDebug ( "Page command: " + this._PageCommand.getAsString ( true, true ) );

    }//END updateWebPageCommandObject method

    // ==================================================================================

    /// <summary>
    /// This method updates the Command parameters with field values.
    /// </summary>
    // ---------------------------------------------------------------------------------
    private void updateWebPageCommandTableObject ( Evado.UniForm.Model.Field field )
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
            this._PageCommand.AddParameter ( stName, field.Table.Rows [ iRow ].Column [ iCol ] );

          }//END has a value.

        }//END column iteration loop.

      }//END row iteration loop.

    }//END updateWebPageCommandTableObject method


    ///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region History management

    // ==================================================================================
    /// <summary>
    /// this method returns the initialises the Command history list
    /// </summary>
    // ---------------------------------------------------------------------------------
    public void initialiseHistory ( )
    {
      Global.LogDebugMethod ( "InitialiseHistory" );
      //
      // Initialise the home page Command.
      //
      Evado.UniForm.Model.Command homePageCommand = new Evado.UniForm.Model.Command ( );
      this._CommandHistoryList = new List<Evado.UniForm.Model.Command> ( );

      //
      // Update the session variable.
      //
      Session [ Service_CommandHistoryList ] = this._CommandHistoryList;

    }//END initialiseCommandHistoryList method.

    // ==================================================================================
    /// <summary>
    /// add the current page Command to the previous page list
    /// </summary>
    /// <param name="PageCommand">ClientPageCommand object</param>
    // ---------------------------------------------------------------------------------
    public void addHistoryCommand (
      Evado.UniForm.Model.Command PageCommand )
    {
      Global.LogDebugMethod ( "addHistoryCommand" );

      //
      // If the Command identifier is empty then exit.
      //
      if ( PageCommand == null )
      {
        Global.LogDebug ( "The command is null." );

        return;
      }

      this.formatCommandTitle ( PageCommand );

      //
      // If the anonoyous access mode exit.
      //
      if ( this._AppData.Page.GetAnonymousPageAccess ( ) == true )
      {
        Global.LogDebug ( "Anonyous_Page_Access = true" );

        return;
      }

      Global.LogDebug ( " Command:" + PageCommand.getAsString ( false, false ) );
      //
      // If the Command identifier is empty then exit.
      //
      if ( PageCommand.Id == Guid.Empty
        || PageCommand.Id == this.LoginCommandId )
      {
        Global.LogDebug ( "The command identifier is null or login." );

        return;
      }

      //
      // If the method is to update a value then we need to undertake process processing after 
      // the method has been processed to return the user to the exit page.
      //
      if ( PageCommand.Method != Evado.UniForm.Model.ApplicationMethods.Null
        && PageCommand.Method != Evado.UniForm.Model.ApplicationMethods.Get_Object
        && PageCommand.Method != Evado.UniForm.Model.ApplicationMethods.List_of_Objects )
      {
        Global.LogDebug ( "No commands added to the list" );

        return;
      }

      if ( PageCommand.Type == Evado.UniForm.Model.CommandTypes.Login_Command
        || PageCommand.Type == Evado.UniForm.Model.CommandTypes.Logout_Command
        || PageCommand.Type == Evado.UniForm.Model.CommandTypes.Meeting_Command
        || PageCommand.Type == Evado.UniForm.Model.CommandTypes.Anonymous_Command
        || PageCommand.Type == Evado.UniForm.Model.CommandTypes.Register_Device_Client
        || PageCommand.Type == Evado.UniForm.Model.CommandTypes.Offline_Command
        || PageCommand.Type == Evado.UniForm.Model.CommandTypes.Synchronise_Add
        || PageCommand.Type == Evado.UniForm.Model.CommandTypes.Synchronise_Save )
      {
        Global.LogDebug ( "Not a command that has a history. i.e." + PageCommand.Type );

        return;
      }

      //
      // if the Command is in the list exit.
      //
      if ( this.deleteHistoryCommand ( PageCommand.Id ) == true )
      {
        Global.LogDebug ( "The command exists in the list and has been deleted." );
      }

      Global.LogDebug ( "ADDING: Command : " + PageCommand.Title + " to history." );

      //
      // Shorten the PageCommand title if it is greater then 20 characters
      //        
      if ( PageCommand.Title.Length > 20 )
      {
        PageCommand.Title = PageCommand.Title.Substring ( 0, 20 ) + "...";
      }

      //
      // Empty the header values as they are set by the client.
      //
      PageCommand.Header = new List<Evado.UniForm.Model.Parameter> ( );

      //
      // If they do not match add the new previous page Command to the list.
      //  This is to stop consequetive duplicates.
      //
      this._CommandHistoryList.Add ( PageCommand );


      Global.LogDebug ( "Saving history to Session. list count: " + this._CommandHistoryList.Count );
      //
      // Update the session variable.
      //
      Session [ Service_CommandHistoryList ] = this._CommandHistoryList;

    }//END addServerPageCommandObject method

    // ================================================================================
    /// <summary>
    /// This method get the default exit groupCommand.
    /// </summary>
    /// <param name="PageCommand">ClientPageCommand object: containing the groupCommand that 
    /// is called on web service</param>
    // ----------------------------------------------------------------------------------
    private void formatCommandTitle ( Evado.UniForm.Model.Command PageCommand )
    {
      Global.LogDebugMethod ( "formCommandTitle" );
      Global.LogDebug ( "PageCommand.Title:" + PageCommand.Title );

      if ( PageCommand == null )
      {
        return;
      }

      if ( PageCommand.Title.Length < 3 )
      {
        return;
      }

      string str = PageCommand.Title.Substring ( 0, 2 );
      Global.LogDebug ( "str:'" + str + "'" );
      if ( str.Contains ( "-" ) == true )
      {
        PageCommand.Title = PageCommand.Title.Substring ( 2 );
        PageCommand.Title.Trim ( );
      }
      Global.LogDebug ( "Formatted PageCommand.Title:" + PageCommand.Title );


    }

    // ==================================================================================
    /// <summary>
    /// This method deletes the page Command and all others in the last.
    /// </summary>
    /// <param name="Command">Guid Command identifer</param>
    // ---------------------------------------------------------------------------------
    private bool hasHistoryCommand ( Guid CommandId )
    {
      //
      // Iterate through the Command list to find a matching Command
      //
      for ( int count = 0; count < this._CommandHistoryList.Count; count++ )
      {
        if ( ( this._CommandHistoryList [ count ].Id == CommandId ) )
        {
          return true;
        }
      }

      return false;
    }

    // ==================================================================================
    /// <summary>
    /// Gets the last previous Command
    /// </summary>
    /// <param name="Command">Guid Command identifer</param>
    /// <returns>ClientPageCommand</returns>
    // ---------------------------------------------------------------------------------
    public Evado.UniForm.Model.Command getHistoryCommand (
      Guid CommandId )
    {
      Global.LogDebugMethod ( "getHistoryCommand" );
      Global.LogDebug ( "CommandId: " + CommandId );
      Evado.UniForm.Model.Command command = new Evado.UniForm.Model.Command ( );

      //
      // Iterate through the list of Command history.
      //
      for ( int count = 0; count < this._CommandHistoryList.Count; count++ )
      {
        //
        // does the Command id match
        //
        if ( this._CommandHistoryList [ count ].Id == CommandId )
        {
          Global.LogDebug ( "Found Command: " + this._CommandHistoryList [ count ].Title );

          command = this._CommandHistoryList [ count ].copyObject ( );

          //
          // Delete all object after the returned comment
          //
          for ( int delete = count; delete < this._CommandHistoryList.Count; delete++ )
          {
            Global.LogDebug ( "Deleting: " + this._CommandHistoryList [ delete ].Title ); ;
            //
            // Delete all of the commands after the Command has been found )
            //
            this._CommandHistoryList.RemoveAt ( count );
            delete--;
          }

          return command;
        }

      }//END of the iteration loop.


      Global.LogDebug ( "History count: " + this._CommandHistoryList.Count );

      Global.LogDebugMethodEnd ( "getHistoryCommand" );
      return command;

    }//END getCommandObject method.

    // ==================================================================================
    /// <summary>
    /// Gets the last previous Command
    /// </summary>
    /// <param name="Command">Guid Command identifer</param>
    /// <returns>ClientPageCommand</returns>
    // ---------------------------------------------------------------------------------
    public bool deleteHistoryCommand (
      Guid CommandId )
    {
      Global.LogDebugMethod ( "deleteHistoryCommand method. Identifier: " + CommandId );
      Evado.UniForm.Model.Command command = new Evado.UniForm.Model.Command ( );

      //
      // Iterate through the list of Command history.
      //
      for ( int count = 0; count < this._CommandHistoryList.Count; count++ )
      {
        //
        // does the Command id match
        //
        if ( this._CommandHistoryList [ count ].Id == CommandId )
        {
          Global.LogDebug ( "Found Command: " + this._CommandHistoryList [ count ].Title );

          //
          // Delete all object after the returned comment
          //
          for ( int delete = count; delete < this._CommandHistoryList.Count; delete++ )
          {
            Global.LogDebug ( "Deleting: " + this._CommandHistoryList [ delete ].Title ); ;
            //
            // Delete all of the commands after the Command has been found )
            //
            this._CommandHistoryList.RemoveAt ( count );
            delete--;
          }

          return true;
        }

      }//END of the iteration loop.


      Global.LogDebug ( "History count: " + this._CommandHistoryList.Count );

      return false;

    }//END getCommandObject method.

    //*********************************************************************************
    #endregion.

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
      Global.LogDebugMethod ( "btnPageLeft_OnClick event method" );
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
      Global.LogDebugMethod ( "btnPageRight_OnClick event method" );
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
      Global.LogDebugMethod ( "RequestLogin method" );
      //
      // Initialise the methods variables and object.s
      //
      this.initialiseHistory ( );
      this._AppData.Title = EuLabels.User_Login_Title;

      this.fsLoginBox.Visible = true;
      this.litExitCommand.Visible = false;
      this.litCommandContent.Visible = false;
      this.litPageContent.Visible = false;
      this.litHistory.Visible = false;
      this.litPageMenu.Visible = false;

      this._AppData.Page.Exit = new Evado.UniForm.Model.Command ( );

      this.litExitCommand.Text = String.Empty;
      this.__CommandId.Value = this.LoginCommandId.ToString ( );

      //
      // display the logo if one is defined.
      //
      this.pLogo.Visible = false;
      if ( this._AppData.LogoFilename != String.Empty )
      {
        this._AppData.LogoFilename = this._AppData.LogoFilename.ToLower ( );

        if ( this._AppData.LogoFilename.Contains ( "http:" ) == true
          || this._AppData.LogoFilename.Contains ( "https:" ) == true )
        {
          this.imgLogo.Src = this._AppData.LogoFilename;
        }
        else
        {
          this.imgLogo.Src = Global.RelativeBinaryDownloadURL + this._AppData.LogoFilename;
        }
        this.pLogo.Visible = true;
      }

      Global.LogDebugMethodEnd ( "RequestLogin" );
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
      Global.LogDebugMethod ( "btnLogin_OnClick event method" );
      Global.LogDebug ( "UserId: " + this.fldUserId.Value );
      Global.LogDebug ( "Password: " + this.fldPassword.Value );
      //
      // Initialise the methods variables and object.s
      //
      this.litLoginError.Text = String.Empty;
      this.litLoginError.Visible = false;

      //
      // If the external command exists then load it as the last command.
      //
      if ( Session [ SESSION_ExternalCommand ] != null )
      {
        this._PageCommand = (Evado.UniForm.Model.Command) Session [ SESSION_ExternalCommand ];
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

      this._UserNetworkId = this.fldUserId.Value;
      this._A1 = this.fldPassword.Value;

      Session [ Global.SESSION_USER_ID ] = this._UserNetworkId;
      Session [ Global.SESSION_A1 ] = this._A1;

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

      Global.LogDebug ( "AppData: " + this._AppData.getAtString ( ) );
      if ( this._AppData.Page.Exit != null )
      {
        Global.LogDebug ( "AppData.Page.Exit: " + this._AppData.Page.Exit.getAsString ( false, false ) );
      }

      //
      // Generate the page layout.
      //
      this.generatePage ( );

      Global.LogDebug ( "Sessionid: " + this._Server_SessionId );
      Global.LogDebug ( "User NetworkId: " + this._UserNetworkId );

      this.outputSerialisedData ( );

      Global.OutputtDebugLog ( );

      Global.LogDebugMethodEnd ( "btnLogin_OnClick" );

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
    protected void sendWindowsLoginCommand ( )
    {
      Global.LogDebugMethod ( "sendWindowsLoginCommand event method" );


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
      this._PageCommand = new Evado.UniForm.Model.Command ( "Login",
        Evado.UniForm.Model.CommandTypes.Network_Login_Command,
        "Default",
        "Default",
        Evado.UniForm.Model.ApplicationMethods.Null );

      this._PageCommand.AddParameter ( Evado.UniForm.Model.EuStatics.PARAMETER_LOGIN_USER_ID, this._UserNetworkId );
      this._PageCommand.AddParameter ( Evado.UniForm.Model.EuStatics.PARAMETER_NETWORK_ROLES, roles );


      Global.LogDebug ( "Login PageCommand: " + this._PageCommand.getAsString ( false, false ) );

      //
      // get a Command object from the server.
      //
      this.sendPageCommand ( );

      Global.LogDebug ( "Status: " + this._AppData.Status );

      //
      // Generate the page layout.
      //
      this.generatePage ( );

      this.outputSerialisedData ( );

      Global.OutputtDebugLog ( );

      Global.LogDebugMethodEnd ( "sendWindowsLoginCommand" );

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
      Global.LogDebugMethod ( "SendLoginCommand method" );
      Global.LogDebug ( "PageCommand: " + this._PageCommand.getAsString ( false, false ) );

      //
      // Create login command if it has not already been loaded.
      //
      if ( this._PageCommand.Type != Evado.UniForm.Model.CommandTypes.Login_Command )
      {
        this._PageCommand = new Evado.UniForm.Model.Command ( "Login",
          Evado.UniForm.Model.CommandTypes.Login_Command,
          "Default",
          "Default",
          Evado.UniForm.Model.ApplicationMethods.Null );
      }

      this._PageCommand.AddParameter ( Evado.UniForm.Model.EuStatics.PARAMETER_LOGIN_USER_ID, UserId );
      this._PageCommand.AddParameter ( Evado.UniForm.Model.EuStatics.PARAMETER_LOGIN_PASSWORD, Password );

      Global.LogDebug ( "Login PageCommand: " + this._PageCommand.getAsString ( false, true ) );

      //
      // get a Command object from the server.
      //
      this.sendPageCommand ( );

      Global.LogDebug ( "Status: " + this._AppData.Status );

      //
      // If the login is validated then display the home page.
      //
      if ( this._AppData.Status == Evado.UniForm.Model.AppData.StatusCodes.Login_Failed )
      {
        this.litLoginError.Text = "<p>" + this._AppData.Message + "</p>";
        this.litLoginError.Visible = true;
        this._A1 = String.Empty;

        this.RequestLogin ( );

        return;
      }
      else
        if ( this._AppData.Status == Evado.UniForm.Model.AppData.StatusCodes.Login_Count_Exceeded )
        {
          this.litLoginError.Text = "<p>" + this._AppData.Message + "</p>";
          this.litLoginError.Visible = true;

          return;
        }


      Global.LogDebugMethodEnd ( "SendLoginCommand" );
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
      Global.LogDebugMethod ( "requestLoogout method" );
      this.fldPassword.Value = String.Empty;
      //
      // Create a page object.
      //
      this._PageCommand = new Evado.UniForm.Model.Command ( "Logout",
        Evado.UniForm.Model.CommandTypes.Logout_Command,
        "Default",
        "Default",
        Evado.UniForm.Model.ApplicationMethods.Null );

      //
      // get a Command object from the server.
      //
      this.sendPageCommand ( );

      //
      // display the login panel.
      //
      this.RequestLogin ( );

    }//END  btnLogout_Click event method

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

  public class KeyValuePair
  {
    private String _Key = String.Empty;

    public String Key
    {
      get { return _Key; }
      set { _Key = value; }
    }
    private String _Value = String.Empty;

    public String Value
    {
      get { return _Value; }
      set { _Value = value; }
    }
  }
}
