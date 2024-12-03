using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft;
using Evado.Model;
using Evado.UniForm.Model;
using System.Net.Http;
using Evado.UniForm;

namespace Evado.ServiceClients
{
  public partial class EuServiceClients
  {
    #region class initialisation
    public EuServiceClients( )
    {

    }

    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region class properties.

    /// <summary>
    /// This is the http web client for the Uniform service.
    /// </summary>
    public HttpClient HttpClient { get; set; }

    /// <summary>
    /// This property contains the web service URL
    /// </summary>
    public string WebServiceUrl { get; set; } = String.Empty;

    /// <summary>
    /// This property contains the feild service URL
    /// </summary>
    public string FileServiceUrl { get; set; } = String.Empty;

    /// <summary>
    /// This property contains the image service URL
    /// </summary>
    public string StaticImageUrl { get; set; } = String.Empty;

    //
    // this property contains the serice url
    //
    public String ServiceUrl { get; private set; } = String.Empty; 
    /// <summary>
    /// This property contains the temporary file URL
    /// </summary>
    public string TempUrl { get; set; } = String.Empty;

    /// <summary>
    /// This property contains the temporary file directory
    /// </summary>
    public string TempPath { get; set; } = String.Empty;

    /// <summary>
    /// This property cotnains the client user session object.
    /// </summary>
    public EuClientSession UserSession { get; set; } = new EuClientSession ( );

    /// <summary>
    /// This field contains last event code.
    /// </summary>
    private Evado.Model.EvEventCodes LastEventCode = Evado.Model.EvEventCodes.Ok;

    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion


    #region class data client 
    public Evado.Model.EvEventCodes SendPageCommand( EuCommand PageCommand )
    {
      this.LogMethod ( "SendPageCommand" );
      this.LogValue ( "DebugLogOn {0}.", this.DebugLogOn );
      this.LogDebug ( "Sessionid: {0}.", this.UserSession.ServerSessionId );
      this.LogDebug ( "UserNetworkUserId: {0}.", this.UserSession.UserId );
      this.LogDebug ( "WebServiceUrl: {0}.", this.WebServiceUrl );
      this.LogDebug ( "ClientVersion: {0}.", this.UserSession.ClientVersion );
      //
      // Initialise the methods variables and objects.
      //
      string jsonData = String.Empty;
      string baseUrl = this.WebServiceUrl;

      Newtonsoft.Json.JsonSerializerSettings jsonSettings = new Newtonsoft.Json.JsonSerializerSettings
      {
        NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore
      };

      this.LogDebug ( "WebServiceUrl:  {0}.", this.WebServiceUrl );
      this.LogDebug ( "FileServiceUrl:  {0}.", this.FileServiceUrl );
      this.LogDebug ( "ImagesUrl: {0}.", this.StaticImageUrl );
      this.LogDebug ( "TempUrl: {0}.", this.TempUrl );

       ServiceUrl = String.Format ( EuStatics.WEB_SERVICE_URI_FORMAT, 
        this.WebServiceUrl,
        EuStatics.APPLICATION_SERVICE_CLIENT_RELATIVE_URL,
        this.UserSession.ClientVersion , 
        this.UserSession.ServerSessionId ); 

      this.LogDebug ( "webServiceUrl: {0}.", ServiceUrl );
      //
      // Set the default application if non are set.
      //
      if (PageCommand.ApplicationId == String.Empty )
      {
        PageCommand.ApplicationId = "Default";
      }

      this.LogValue ( "SENT: PageCommand: " + PageCommand.getAsString ( false, false ) );
      //
      // serialise the Command prior to sending to the web service.
      //
      this.LogDebug ( "Serialising the PageComment object" );

      jsonData = Newtonsoft.Json.JsonConvert.SerializeObject ( PageCommand );

      Evado.Model.EvStatics.Files.saveFile ( this.TempPath + @"cmd-data.json", jsonData );


      try
      {
        //
        // The post command 
        //
        jsonData = this.SendPost ( ServiceUrl, jsonData );

        if ( jsonData == null )
        {
          this.LogDebug ( "ERROR: POST COMMAND DID NOT WORK" );
        }

        //
        // deserialise the application data 
        //
        this.UserSession.AppData = new Evado.UniForm.Model.EuAppData ( );

        if ( String.IsNullOrEmpty ( jsonData ) == false
          && jsonData.Contains ( "{" ) == true )
        {
          this.LogDebug ( "JSON Serialised text length: {0}", jsonData.Length );

          Evado.Model.EvStatics.Files.saveFile ( this.TempPath + @"app-data.json", jsonData );

          this.LogDebug ( "Deserialising JSON to Evado.UniForm.Model.EuAppData object." );

          this.UserSession.AppData = Newtonsoft.Json.JsonConvert.DeserializeObject<Evado.UniForm.Model.EuAppData> ( jsonData );

          this.LogDebug ( "Application object: {0}.", this.UserSession.AppData.getAtString ( ) );
          this.LogDebug ( "Page Command count: {0}.", this.UserSession.AppData.Page.CommandList.Count );
          this.LogDebug ( "this.UserSession.AppData.SessionId: {0}.", this.UserSession.AppData.SessionId );

          //
          // Set the anonymouse page access mode.
          // True enables anonymous access mode hiding:
          // - Exit Command
          // - History commands
          // - Page Commands
          //
          this.LogDebug ( "ExitCommand: " + this.UserSession.AppData.Page.Exit.getAsString ( false, false ) );
          //
          // Add the exit Command to the history.
          //
          this.UserSession.AddHistoryCommand ( this.UserSession.AppData.Page.Exit );

        }
        else
        {
          this.LogDebug ( "Response Not a JSON object = {0}.", jsonData );

          if ( jsonData == null )
          {
            jsonData = String.Empty;
          }

          this.UserSession.AppData.Page.Title = "Service Access Error.";
          Evado.UniForm.Model.EuGroup group = this.UserSession.AppData.Page.AddGroup (
            "Service Access Error Report", Evado.UniForm.Model.EuEditAccess.Disabled );
          group.Description =
          String.Format ( "Web Service URL: {0}, did not return a JSON object", jsonData );
        }

        //
        // Update the user session id
        //
        this.UserSession.ServerSessionId = this.UserSession.AppData.SessionId;

        this.LogDebug ( "ServerUserSessionId:  {0}.", this.UserSession.ServerSessionId );
      }
      catch ( Exception Ex )
      {
        this.LogDebug ( "Web Service Error. " + Evado.Model.EvStatics.getException ( Ex ) );

        this.LogMethodEnd ( "sendPageCommand" );
        return Evado.Model.EvEventCodes.WebServices_General_Failure_Error;
      }

      this.LogMethodEnd ( "sendPageCommand" );
      return this.LastEventCode;

    }


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
        using ( this.HttpClient = new HttpClient ( handler ) )
        {
          using ( content )
          {
            HttpResponseMessage response = this.HttpClient.PostAsync ( uri, content ).Result;

            this.LogDebug ( "response.StatusCode {0}.", response.StatusCode );
            if ( response.StatusCode != System.Net.HttpStatusCode.OK )
            {
              switch ( response.StatusCode )
              {
                case System.Net.HttpStatusCode.InternalServerError:
                {
                  this.LastEventCode = Evado.Model.EvEventCodes.WebServices_Internal_Server_Error;
                  break;
                }
                case System.Net.HttpStatusCode.NotFound:
                {
                  this.LastEventCode = Evado.Model.EvEventCodes.WebServices_Not_Found_Error;
                  break;
                }
                case System.Net.HttpStatusCode.BadRequest:
                {
                  this.LastEventCode = Evado.Model.EvEventCodes.WebServices_Bad_Request_Error;
                  break;
                }
                case System.Net.HttpStatusCode.Conflict:
                {
                  this.LastEventCode = Evado.Model.EvEventCodes.WebServices_Conflict_Error;
                  break;
                }
                case System.Net.HttpStatusCode.RequestEntityTooLarge:
                {
                   this.LastEventCode = Evado.Model.EvEventCodes.WebServices_Request_Entity_Too_Large_Error;
                  break;
                }
                case System.Net.HttpStatusCode.MethodNotAllowed:
                {
                   this.LastEventCode = Evado.Model.EvEventCodes.WebServices_Method_Not_Allowed_Error;
                  break;
                }
                case System.Net.HttpStatusCode.NoContent:
                {
                   this.LastEventCode = Evado.Model.EvEventCodes.WebServices_No_Content_Error;
                  break;
                }
                default:
                {
                   this.LastEventCode = Evado.Model.EvEventCodes.WebServices_General_Failure_Error;
                  break;
                }
              }//ENd switch statement

              this.LogValue ( "WebService URL {0}, raised error event {1}", WebServiceUrl,  this.LastEventCode );
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
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region Logging methods.

    private System.Text.StringBuilder classLog = new StringBuilder ( );

    /// <summary>
    /// this property turns on debug logging.
    /// </summary>
    public bool DebugLogOn { get; set; } = false;

    /// <summary>
    /// this property returns the class log.
    /// </summary>
    public String Log
    {
      get
      {
        return classLog.ToString ( );
      }
    }

    //  =================================================================================
    /// <summary>
    ///   This static method removes a user from the online user list.
    /// </summary>
    //   ---------------------------------------------------------------------------------
    public void LogMethod( String Value )
    {
      string logValue = Evado.Model.EvStatics.CONST_METHOD_START
         + DateTime.Now.ToString ( "dd-MM-yy hh:mm:ss" ) + ": "
         + "Evado.ServiceClients.EuServiceClients:" + Value + " Method";

      classLog.AppendLine ( logValue );
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

      classLog.AppendLine ( value );
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
       +  Value;

      classLog.AppendLine ( logValue );
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
       +  String.Format ( Format, args );

      classLog.AppendLine ( logValue );
    }

    //  =================================================================================
    /// <summary>
    ///   This method log debug the passed value
    /// </summary>
    /// <param name="Value">String: value.</param>
    //   ---------------------------------------------------------------------------------
    public void LogDebug( String Value )
    {
      if( DebugLogOn == false )
      {
        return;
      }
      string logValue = DateTime.Now.ToString ( "dd-MM-yy hh:mm:ss" ) + ": "
       +  Value;

      classLog.AppendLine ( logValue );
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
      if ( DebugLogOn == false )
      {
        return;
      }
      string logValue = DateTime.Now.ToString ( "dd-MM-yy hh:mm:ss" ) + ": "
       +  String.Format ( Format, args );

      classLog.AppendLine ( logValue );
    }

    ///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

  }//END ServiceClients Class
}
