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

    /// <summary>
    /// This constant defines the file segment length
    /// </summary>
    private const int CONST_FILE_SEGMENT_LENGTH = 40000;

    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region class public data client methods
    // ==================================================================================
    /// <summary>
    /// This method sends a page command to the client service.
    /// </summary>
    /// <param name="PageCommand">EuCommand</param>
    /// <returns>EvEventCodes enumerated value indicating the success of the transaction.</returns>
    // ---------------------------------------------------------------------------------
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

       ServiceUrl = String.Format ( EuLabels.Client_Service_Url_Template, 
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

    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region class public file methods
    // ==================================================================================
    /// <summary>
    /// This method sends a request to the file service.
    /// </summary>
    /// <param name="filename">String: file name of the file to be up loaded.</param>
    /// <param name="MimeType">String: the mime type for the file.</param>
    // ---------------------------------------------------------------------------------
    public EvEventCodes SendFileRequest( String filename, String MimeType )
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

        filePath = this.TempPath + fileObject.FileName;

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
          /*
          */
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
    private Evado.Model.EvEventCodes SendFileSegment( String SegementData, int Segment )
    {
      this.LogMethod ( "SendFileSegment" );
      this.LogDebug ( "Segement {0}, Data: {1}", Segment, SegementData );

      //
      // Display a serialised instance of the object.
      //
      string responseText = String.Empty;
      string WebServiceUrl = String.Format ( EuLabels.File_Service_Url_Template,
        this.WebServiceUrl,
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

    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region class private methods

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
