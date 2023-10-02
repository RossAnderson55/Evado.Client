/***************************************************************************************
 * <copyright file="Evado.UniForm.Model\statics.cs" company="EVADO HOLDING PTY. LTD.">
 *     
 *      Copyright (c) 2013 - 2021 EVADO HOLDING PTY. LTD.  All rights reserved.
 *     
 *      The use and distribution terms for this software are contained in the file
 *      named \license.txt, which can be found in the root of this distribution.
 *      By using this software in any fashion, you are agreeing to be bound by the
 *      terms of this license.
 *     
 *      You must not remove this notice, or any other, from this software.
 *     
 * </copyright>
 * 
 * Description: 
 *  This class contains the static data objects.
 *
 ****************************************************************************************/

using System;
using System.IO;
using System.Xml.Serialization;

namespace Evado.UniForm.Model
{
  /// 
  /// Business entity used to model EvFormField
  /// 
  public class EuStatics : Evado.Model.EvStatics
  {
    //===================================================================================

    #region static enumerations
    /// <summary>
    /// This 
    /// </summary>
    public enum UpdateResultCodes
    {
      /// <summary>
      /// Value not test
      /// </summary>
      Null,

      /// <summary>
      /// Update object value validation failed.
      /// </summary>
      Validation_Failed,

      /// <summary>
      /// Update object value duplicate identifier
      /// </summary>
      Duplicate_ID_Error,

      /// <summary>
      /// Database update action failed.
      /// </summary>
      Save_Action_Failed,

      /// <summary>
      /// Database update action completed.
      /// </summary>
      Save_Completed,
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region Static Constants

    /// <summary>
    /// This constant defeins the base service url.
    /// </summary>
    public const string APPLICATION_SERVICE_BASE = "euws/";

    /// <summary>
    /// This constance defines the Uniform client data service relative URL.
    /// </summary>
    public const string APPLICATION_SERVICE_CLIENT_RELATIVE_URL = "euws/client/";

    /// <summary>
    /// This constant defines the uniforma file service  relative URL.
    /// </summary>
    public const string APPLICATION_SERVICE_FILE_RELATIVE_URL = "euws/file";

    /// <summary>
    /// This constant defines the uniform temp relative URL.
    /// </summary>
    public const string APPLICATION_TEMP_RELATIVE_URL = "euws/temp/";

    /// <summary>
    /// This constant defines the uniform image relative URL.
    /// </summary>
    public const string APPLICATION_IMAGES_RELATIVE_URL = "euws/images/";

    public const String CONST_EXTERNAL_COMMAND_EXTENSION = ".COMMAND.JSON";

    /// <summary>
    ///  This constant defines a customer identifier
    /// </summary>
    public const string CONST_CUSTOMER_GUID = "CUSTOMER_GUID";

    /// <summary>
    /// This constant defines the user identifier session key
    /// </summary>
    public const String SESSION_USER_ID = "EUWC_USER_ID";

    /// <summary>
    /// This constant defines the user's network roles
    /// </summary>
    public const String SESSION_ROLES = "EUWC_ROLES";

    /// <summary>
    /// This constant defines the iFrame client base URL
    /// </summary>
    public const String CONST_CLIENT_BASE_URL = "./client.aspx";

    public const String CONFIG_PAGE_DEEFAULT_LOGO = "PDLOGO";

    /// <summary>
    /// This constant defines the vimeo base url
    /// </summary>
    public const String CONFIG_VIMEO_EMBED_URL = "VIMEO_EMBED_URL";

    /// <summary>
    /// This constant defines the Yourtube based URL
    /// </summary>
    public const String CONFIG_YOU_TUBE_EMBED_URL = "YOU_TUBE_EMBED_URL";

    /// <summary>
    /// This constant defines the Event Source
    /// </summary>
    public const String CONFIG_EVENT_LOG_SOURCE_KEY = "EventLogSource";

    /// <summary>
    /// This constant defines the Enable Detail logging config key
    /// </summary>
    public const String CONFIG_ENABLE_DETAILED_LOGGING = "ENABLE_DETAILED_LOGGING";

    /// <summary>
    /// This constant defines the enables page menu config key 
    /// </summary>
    public const String CONFIG_ENABLE_PAGE_MENU_KEY = "ENABLE_MENU";

    /// <summary>
    /// This constant defines the enable history config key
    /// </summary>
    public const String CONFIG_ENABLE_PAGE_HISTORY_KEY = "ENABLE_HISTORY";

    /// <summary>
    /// This constant defines the device validated session key value.
    /// </summary>
    public const string SESSION_DEVICE_VALIDATED = "WCF_DEVICE_VALIDATED";

    /// <summary>
    /// This constant defines the binary filed path key.
    /// </summary>
    public const string CONFIG_BINARY_FILE_PATH_KEY = "BINARY_FILE_PATH";

    /// <summary>
    /// This constant defines the binary filed path key.
    /// </summary>
    public const string CONFIG_BINARY_FILE_URL_KEY = "BINARY_FILE_URL";



    /// <summary>
    /// This constant defines the user validated session key value.
    /// </summary>
    public const string SESSION_USER_VALIDATED = "WCF_USER_VALIDATED";

    /// <summary>
    /// This constant defines the user session identifier, session key value.
    /// </summary>
    public const string SESSION_SESSION_ID = "WCF_SESSION_ID";

    /// <summary>
    /// This constant defines the client version session key value.
    /// </summary>
    public const string SESSION_CLIENT_VERSION = "WCF_CLIENT_VERSION";

    /// <summary>
    /// This constant defines the user login count session key value.
    /// </summary>
    public const string SESSION_AD_USER_GROUPS = "USER_GROUPS";

    /// <summary>
    /// This constant defines the user login count session key value.
    /// </summary>
    public const string SESSION_LOGIN_COUNT = "LOGIN_COUNT";

    /// <summary>
    /// This constant defines the service user EuCommand history session key value.
    /// </summary>
    public const string SESSION_SERVICE_COMMAND_HISTORY_LIST = "SERVICE_COMMAND_HISTORY_LIST";

    /// <summary>
    /// This constant defines the UniFORM user profile session key value.
    /// </summary>
    public const string SESSION_UNIFORM_USER_PROFILE = "UNIFORM_USER_PROFILE";

    /// <summary>
    /// This constant defines the user identifier parameter key value.
    /// </summary>
    public const string PARAMETER_LOGIN_USER_TOKEN = "USER_TOKEN";

    /// <summary>
    /// This constant defines the user identifier parameter key value.
    /// </summary>
    public const string PARAMETER_LOGIN_USER_ID = "USER_ID";

    /// <summary>
    /// This constant defines the user password parameter key value.
    /// </summary>
    public const string PARAMETER_LOGIN_PASSWORD = "PASSWORD";

    /// <summary>
    /// This constant defines the user password parameter key value.
    /// </summary>
    public const string PARAMETER_NETWORK_ROLES = "ROLES";

    /// <summary>
    /// This constant defines the default text value.
    /// </summary>
    public const string CONST_WEB_CLIENT = "Web Client";

    /// <summary>
    /// This constant defines the default text value.
    /// </summary>
    public const string CONST_WEB_NET_VERSION = ".NET 4.8";

    /// <summary>
    /// This constant defines the field query suffx value.
    /// </summary>
    public const string CONST_FIELD_FIELD_QUERY_SUFFIX = "_Query";

    /// <summary>
    /// This constant defines the field annotation suffix value.
    /// </summary>
    public const string CONST_FIELD_ANNOTATION_SUFFIX = "_FAnnotation";

    /// <summary>
    /// This constant defines the field version 1.1 annocation suffix value.
    /// </summary>
    public const string CONST_FIELD_V11_ANNOTATION_SUFFIX = "_V11Annotation";

    /// <summary>
    /// This constant defines the field version 1.1 to 1.1 annocation suffix value.
    /// </summary>
    public const string CONST_FIELD_V11_ANNOTATION_SUFFIX_11 = "_V11Annotation_1_1";

    /// <summary>
    /// This constant defines the field version 1.1 to 1.2 annocation suffix value.
    /// </summary>
    public const string CONST_FIELD_V11_ANNOTATION_SUFFIX_12 = "_V11Annotation_1_2";

    /// <summary>
    /// This constant defines the  application object null value.
    /// </summary>
    public const string CONST_IMAGES_SUBSTITUTION = "{images}";

    /// <summary>
    /// This constant defines the application global object hash table key value.
    /// </summary>
    public const string GLOBAL_CLINICAL_OBJECT = "CLINICAL_GLOBAL_OBJECT";

    /// <summary>
    /// This constant defines the application global object hash table key value for meetings.
    /// </summary>
    public const string GLOBAL_MEETING_OBJECT = "MEETING_GLOBAL_OBJECT";

    /// <summary>
    /// This constant defines the application file repository path key value.
    /// </summary>
    public const string GLOBAL_FILE_REPOSITORY_PATH = "FILE_REPOSITORY_PATH";

    /// <summary>
    /// This constant defines the user session clinical object key value.
    /// </summary>
    public const string GLOBAL_DATE_STAMP = "_DATE_STAMP";

    /// <summary>
    /// This constant defines the web config site ID key value
    /// </summary>
    public const string CONFIG_UNIFORM_SERVICE_ID_KEY = "ServiceId";

    /// <summary>
    /// This constant defines the web config debug key value
    /// </summary>
    public const string CONFIG_PAGE_HEADER_KEY = "HomePageHeader";

    /// <summary>
    /// This constant defines the web config UniFORM binary URL key value
    /// </summary>
    public const string CONFIG_WHEREBY_BASE_URL_KEY = "WHEREBY_BASE_URL";

    /// <summary>
    /// This constant defines the web config UniFORM binary URL key value
    /// </summary>
    public const string CONFIG_WHEREBY_TOKEN_KEY = "WHEREBY_TOKEN";

    /// <summary>
    /// This constant defines the web config UniFORM binary URL key value
    /// </summary>
    public const string CONFIG_WHEREBY_SERVICE_URL = "WHEREBY_SERVICE_URL";

    /// <summary>
    /// This constant defines the web config UniFORM binary URL key value
    /// </summary>
    public const string CONFIG_WHEREBY_PARAMETERS = "WHEREBY_PARAMETERS";

    /// <summary>
    /// This constant defines the web config Db connection string setting key value
    /// </summary>
    public const string CONFIG_DELETE_HR_PERIOD_KEY = "DELETE_PERIOD_HRS";

    /// <summary>
    /// This constant defines the user session clinical object key value.
    /// </summary>
    public const string CONFIG_DELETE_USER_OBJECT_KEY = "DELETE_USER_GLOBAL_OBJECT";

    /// <summary>
    /// This constant defines the user session clinical object key value.
    /// </summary>
    public const string GLOBAL_SESSION_OBJECT = "_SESSION_OBJECT";

    /// <summary>
    /// This constant defines the user session clinical object key value.
    /// </summary>
    public const string GLOBAL_SESSION_CLIENT_DATA_OBJECT = "_CDO";

    /// <summary>
    /// This constant defines the user session clinical object key value.
    /// </summary>
    public const string GLOBAL_COMMAND_HISTORY = "_HISTORY_OBJECT";

    /// <summary>
    /// This constant defines the web config debug validation on key value
    /// </summary>
    public const string CONFIG_DELETE_SESSION_ON_EXIT_KEY = "DELETE_USER_GLOBAL_OBJECT";

    /// <summary>
    /// This constant defines the  application object null value.
    /// </summary>
    public const string APPLICATION_OBJECT_NULL = "Null";

    /// <summary>
    /// This property defines the home page guid value.
    /// </summary>
    public static Guid HomePageCommandId
    {
      get
      {
        return new Guid ( "5d79cd52-3c2b-407c-96d9-111111111111" );
      }
    }
    /// <summary>
    /// This property defines the home page guid value.
    /// </summary>
    public static Guid LogoutCommandId
    {
      get
      {
        return new Guid ( "5d79cd52-3c2b-407c-96d9-222222222222" );
      }
    }
    //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region Deserialization method
    // =====================================================================================
    /// <summary>
    /// This method deserialises an Xml object into a generic type.
    /// </summary>
    /// <param name="FileDirectoryPath">String: File directory path</param>
    /// <param name="FileName">String: File name</param>
    /// <returns>AppData: Object EuAppData </returns>
    /// <remarks>
    /// This method consists of following steps. 
    /// 
    /// 1. Initialise the methods variables and objects.
    /// 
    /// 2. If a empty field then return an empty object.
    /// 
    /// 3. Deserialise the xml data into a local data object.
    /// 
    /// 4. Close the organisation stream.
    /// 
    /// 5. Return the validation object.
    /// 
    /// </remarks>
    // -------------------------------------------------------------------------------------
    public static EuAppData DeserialiseApplicationData( String FileDirectoryPath, String FileName )
    {
      // 
      // Initialise the methods variables and objects.
      // 
      EuAppData dataObject = new EuAppData( );
      string stApplicationDataObjectPath = FileDirectoryPath + FileName + ".UniForm.xml";

      // 
      // If a empty field then return an empty object.
      // 
      if ( stApplicationDataObjectPath == String.Empty )
      {
        return dataObject;
      }

      // 
      // Deserialise the xml data into a local data object
      // 
      XmlSerializer serializer = new XmlSerializer( typeof( EuAppData ) );
      TextReader textReader = new StringReader( stApplicationDataObjectPath );
      try
      {
        // 
        // Deserialise the xml object into the type object.
        // 
        dataObject = (EuAppData) serializer.Deserialize( textReader );

        // 
        // Close the organisation stream.
        // 
        textReader.Close( );
      }
      catch
      {
        textReader.Close( );
        throw;
      }

      // 
      // Return the validation object.
      // 
      return dataObject;

    }//END DeserialiseApplicationData method.

    // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region Static Minor Version encoding methods.

    // =====================================================================================
    /// <summary>
    /// This method deserialises an Xml object into a generic type.
    /// </summary>
    /// <param name="MinorVersion">Float: minor version</param>
    /// <returns>String: Minor version encoded as a string.</returns>
    /// <remarks>
    /// This method consists of following steps. 
    /// 
    /// 1. Initialise the methods variables and objects.
    /// 
    /// 2. convert value
    /// 
    /// 3. Return the minor version as a string.
    /// 
    /// </remarks>
    // -------------------------------------------------------------------------------------
    public static String decodeMinorVersion ( float MinorVersion )
    {
      // 
      // Initialise the methods variables and objects.
      // 
      String stMinorVersion = MinorVersion.ToString ( "00.00" );
      stMinorVersion = stMinorVersion.Replace ( ".", "_" );

      // 
      // Return the minor version at a floating number.
      // 
      return stMinorVersion;

    }//END decodeMinorVersion method.

    // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region Static File Management methods.

    // =====================================================================================
    /// <summary>
    ///  This method reads in a application data file.
    /// </summary>
    /// <param name="ApplicationPath">String: The application path name.</param>
    /// <param name="ApplicationName">String: The filename.</param>
    /// <param name="ApplicationData">AppData: The data filed.</param>
    /// <returns>String with bit value.</returns>
    /// <remarks>
    /// This method consists of following steps. 
    /// 
    /// 1. Initialise the methods variables and objects.
    /// 
    /// 2. If ApplicationPath is empty, add string stStatus with string 'ApplicationPath.Length is zero'.
    /// 
    /// 3. If ApplicationName is empty, add string stStatus with string 'ApplicationName.Length is zero'.
    /// 
    /// 4. Open the text reader with supplied file.
    /// 
    /// 5. Encode html markup.
    /// 
    /// </remarks>
    // -------------------------------------------------------------------------------------
    public static string readApplicationDataPage( String ApplicationPath, String ApplicationName, out EuAppData ApplicationData )
    {
      //
      // Initialise the methods variables and objects.
      //
      ApplicationData = new EuAppData( );
      String stFileData = String.Empty;
      String stHomePagePathname = ApplicationPath + ApplicationName;
      TextReader reader;

      string stStatus = Evado.Model.EvStatics.CONST_METHOD_START + "Evado.UniForm.Model.Statics.readApplicationHomePage static method. "
        + "\r\n stHomePagePathname: " + stHomePagePathname;

      //
      // If ApplicationPath is empty, add string stStatus with string 'ApplicationPath.Length is zero'.
      //
      if ( ApplicationPath == String.Empty )
      {
        stStatus += "\r\n- ApplicationPath.Length is zero";

        return stStatus;
      }

      //
      // If ApplicationName is empty, add string stStatus with string 'ApplicationName.Length is zero'.
      //
      if ( ApplicationName == String.Empty )
      {
        stStatus += "\r\n- ApplicationName.Length is zero";

        return stStatus;
      }

      try
      {

        // 
        // Open the text reader with supplied file
        // 
        using ( reader = File.OpenText( stHomePagePathname ) )
        {
          stFileData = reader.ReadToEnd( );
        }

        if ( stFileData.Length == 0 )
        {
          stStatus += "\r\n- FileStream.Length is zero";
        }


        stFileData = stFileData.Replace ( "<Status>", "<EditAccess>" );
        stFileData = stFileData.Replace ( "</Status>", "</EditAccess>" );
        stFileData = stFileData.Replace ( "<p>", "[[p]]" );
        stFileData = stFileData.Replace ( "</p>", "[[/p]]" );
        stFileData = stFileData.Replace ( "<br/>", "[[br/]]" );
        stFileData = stFileData.Replace ( "<strong>", "[[strong]]" );
        stFileData = stFileData.Replace ( "</strong>", "[[/strong]]" );

        ApplicationData = Evado.Model.EvStatics.DeserialiseXmlObject<EuAppData>( stFileData );

        // 
        // Encode html markup.
        //
        ApplicationData = EuStatics.EncodeHtml( ApplicationData );

      }
      catch ( Exception Ex )
      {
        stStatus += "\r\n Evado.UniForm.Model.Statics.readApplicationHomePage static method."
          + "\r\n exception." + Evado.Model.EvStatics.getException( Ex );
      }

      return stStatus;

    }//END readApplicationDataPage method

    // =====================================================================================
    /// <summary>
    ///  This method encodes the html.
    /// </summary>
    /// <param name="ApplicationData">AppData: The data filed.</param>
    /// <returns>String with bit value.</returns>
    /// <remarks>
    /// This method consists of following steps. 
    /// 
    /// 1. Iterate through the page groups to encode the HTML
    /// 
    /// 2. Iterate through the group fields to encode the HTML in readonly fields.
    /// 
    /// 3. Return EuAppData object.
    /// 
    /// </remarks>
    // -------------------------------------------------------------------------------------
    private static EuAppData EncodeHtml( EuAppData ApplicationData )
    {
      //
      // Iterate through the page groups to encode the HTML
      //
      foreach ( Evado.UniForm.Model.EuGroup group in ApplicationData.Page.GroupList )
      {
        String description = group.Description;
        if ( description != null )
        {
          description = description.Replace ( "[[", "<" );
          description = description.Replace ( "]]", ">" );
          group.Description = description;
        }

        //
        // Iterate through the group fields to encode the HTML in readonly fields.
        //
        foreach ( Evado.UniForm.Model.EuField field in group.FieldList )
        {
          if ( field.Type == Evado.Model.EvDataTypes.Read_Only_Text )
          {

            if ( description != null )
            {
              field.Value = field.Value.Replace ( "[[", "<" );
              field.Value = field.Value.Replace ( "]]", ">" );
            }
          }
        }//END field iteration
      }//END group iteration


      return ApplicationData;
    }//END EuAppData method


    // =====================================================================================
    /// <summary>
    ///  This method is writing a  json object for a disk file.
    /// </summary>
    /// <param name="ApplicationPath">String: The application path name.</param>
    /// <param name="FileName">String: The filename</param>
    /// <param name="StringText">String: string text </param>
    /// <returns>String with bit value.</returns>
    /// <remarks>
    /// This method consists of follwoing steps. 
    /// 
    /// 1. Initialise the method variables and objects.
    /// 
    /// 2. If ApplicationPath is empty then add a formatted string 'ApplicationPath.Length is zero' to stStatus and return stStatus. 
    /// 
    /// 3. If filename is empty then add a formatted string 'ApplicationName.Length is zero' to stStatus and return stStatus.  
    /// 
    /// 4. Open the text reader with supplied file.
    /// 
    /// 5. Return a string 
    /// 
    /// </remarks>
    // -------------------------------------------------------------------------------------
    public static string writeJsonFile( String ApplicationPath, String FileName, String StringText )
    {
      //
      // Initialise the method variables and objects.
      //
      String stFileData = String.Empty;
      String stHomePagePathname = ApplicationPath + FileName + ".JSON";
      TextWriter textFile;

      string stStatus = Evado.Model.EvStatics.CONST_METHOD_START + "Evado.UniForm.Model.Statics.writeJsonFile static method. "
        + ", stHomePagePathname: " + stHomePagePathname;

      //
      // If ApplicationPath is empty then add a formatted string 'ApplicationPath.Length is zero' to stStatus and return stStatus. 
      //
      try
      {
        if ( ApplicationPath == String.Empty )
        {
          stStatus += "\r\n\r\n ApplicationPath.Length is zero";

          return stStatus;
        }

        //
        // If filename is empty then add a formatted string 'ApplicationName.Length is zero' to stStatus and return stStatus. 
        //
        if ( FileName == String.Empty )
        {
          stStatus += "\r\n\r\n ApplicationName.Length is zero";

          return stStatus;
        }

        // 
        // Open the text reader with supplied file
        // 
        using ( textFile = File.CreateText( stHomePagePathname ) )
        {
          textFile.Write( StringText );
        }


      }
      catch ( Exception Ex )
      {
        stStatus += "\r\n\r\n Evado.UniForm.Model.Statics.writeJsonFile static method Excpetion."
          + Evado.Model.EvStatics.getException( Ex );
      }

      //
      // Retrun stStatus
      //
      return stStatus;

    }//END writeJsonFile method

    // =====================================================================================
    /// <summary>
    ///  This methods saves a text file to a directory.
    /// </summary>
    /// <param name="ApplicationPath">String: The application path </param>
    /// <param name="FileName">String: The filename</param>
    /// <param name="StringText">String: stringTest</param>
    /// <returns>String.</returns>
    /// <remarks>
    /// This method consists of following steps. 
    /// 
    /// 1. Initialise the methods variables and objects.
    /// 
    /// 2. If ApplicationPath is empty then add a string 'ApplicationPath.Length is zero' to stStatus and return stStatus.
    /// 
    /// 3. If FileName is empty then add a formatted string 'ApplicationName.Length is zero' to stStatus and return stStatus.
    /// 
    /// 4. Open the text reader with supplied file.
    /// 
    /// 5. Return stStatus
    /// 
    /// </remarks>
    // -------------------------------------------------------------------------------------
    public static string saveTextFile( String ApplicationPath, String FileName, String StringText )
    {
      //
      // Initialise the methods variables and objects.
      //
      String stFileData = String.Empty;
      String stHomePagePathname = ApplicationPath + FileName;
      TextWriter textFile;

      string stStatus = Evado.Model.EvStatics.CONST_METHOD_START + "Evado.UniForm.Model.Statics.TextFileSave static method. "
        + ", stHomePagePathname: " + stHomePagePathname;

      //
      // If ApplicationPath is empty then add a formatted string ApplicationPath.Length is zero to stStatus and return stStatus.
      //
      try
      {
        if ( ApplicationPath == String.Empty )
        {
          stStatus += "\r\n\r\n ApplicationPath.Length is zero";

          return stStatus;
        }

        //
        // If FileName is empty then add a formatted string ApplicationName.Length is zero to stStatus and return stStatus.
        //
        if ( FileName == String.Empty )
        {
          stStatus += "\r\n\r\n ApplicationName.Length is zero";

          return stStatus;
        }

        // 
        // Open the text reader with supplied file
        // 
        using ( textFile = File.CreateText( stHomePagePathname ) )
        {
          textFile.Write( StringText );
        }


      }
      catch ( Exception Ex )
      {
        stStatus += "\r\n\r\n Evado.UniForm.Model.Statics.TextFileSave static method Excpetion." + Evado.Model.EvStatics.getException( Ex );
      }

      //
      // Return stStatus. 
      //
      return stStatus;

    }//END saveTextFile method

    // =====================================================================================
    /// <summary>
    ///  This method appends string text to a text file.
    /// </summary>
    /// <param name="ApplicationPath">String: The Application path</param>
    /// <param name="FileName">String: The filename </param>
    /// <param name="StringText">String: StringText</param>
    /// <returns>String</returns>
    /// <remarks>
    /// This method consists of following steps. 
    /// 
    /// 1. Initialise the methods variables and objects.
    /// 
    /// 2. If ApplicationPath is empty then add a formatted string 'ApplicationPath.Length is zero' to stStatus and return stStatus. 
    /// 
    /// 3. If FileName is empty then add a formatted string 'ApplicationName.Length is zero' to stStatus and return stStatus.  
    /// 
    /// 4. Open the text reader with supplied file.
    /// 
    /// 5. return stStatus.
    /// 
    /// </remarks>
    // -------------------------------------------------------------------------------------
    public static string appendTextFile( String ApplicationPath, String FileName, String StringText )
    {
      //
      // Initialise the methods variables and objects.
      //
      String stFileData = String.Empty;
      String stHomePagePathname = ApplicationPath + FileName;
      TextWriter textFile;

      string stStatus = Evado.Model.EvStatics.CONST_METHOD_START + "Evado.UniForm.Model.Statics.TextFileAppend static method. "
        + ", stHomePagePathname: " + stHomePagePathname;


      try
      {

        //
        // If ApplicationPath is empty then add a formatted string 'ApplicationPath.Length is zero' to stStatus and return stStatus.  
        //
        if ( ApplicationPath == String.Empty )
        {
          stStatus += "\r\n\r\n ApplicationPath.Length is zero";

          return stStatus;
        }

        //
        // If FileName is empty then add a formatted string 'ApplicationName.Length is zero' to stStatus and return stStatus.  
        //
        if ( FileName == String.Empty )
        {
          stStatus += "\r\n\r\n ApplicationName.Length is zero";

          return stStatus;
        }

        // 
        // Open the text reader with supplied file
        // 
        using ( textFile = File.AppendText( stHomePagePathname ) )
        {
          textFile.Write( StringText );
        }


      }
      catch ( Exception Ex )
      {
        stStatus += "\r\n\r\n Evado.UniForm.Model.Statics.TextFileAppend static method Excpetion." + Evado.Model.EvStatics.getException( Ex );
      }

      //
      // Return stStatus 
      //
      return stStatus;

    }//END appendTestFile method

    // =====================================================================================
    /// <summary>
    ///  This method reads a text file
    /// </summary>
    /// <param name="ApplicationPath">String: The application path name.</param>
    /// <param name="ApplicationName">String: The filename.</param>
    /// <param name="StringText">String: The String</param>
    /// <returns>String.</returns>
    /// <remarks> 
    /// This method consists of following steps. 
    /// 
    /// 1. Initialise the methods variables and objects
    /// 
    /// 2. If ApplicationPath is empty then add a formatted string 'ApplicationPath.Length is zero' to stStatus and return stStatus.
    /// 
    /// 3. If ApplicationName is empty then add a formatted string 'ApplicationName.Length is zero' to stStatus and return stStatus.
    /// 
    /// 4. Open the text reader with supplied file.
    /// 
    /// 5. Return stStatus
    /// 
    /// </remarks>
    // -------------------------------------------------------------------------------------
    public static string readTextFile( String ApplicationPath, String ApplicationName, out String StringText )
    {
      //
      // Initialise the methods variables and objects.
      //
      String stFileData = String.Empty;
      String stHomePagePathname = ApplicationPath + ApplicationName;
      TextReader reader;
      StringText = String.Empty;

      string stStatus = Evado.Model.EvStatics.CONST_METHOD_START + "Evado.UniForm.Model.Statics.readJsLibrary static method. "
        + ", stHomePagePathname: " + stHomePagePathname;

      //
      // If ApplicationPath is empty then add a formatted string 'ApplicationPath.Length is zero' to stStatus and return stStatus. 
      //
      if ( ApplicationPath == String.Empty )
      {
        stStatus += "\r\n\r\n ApplicationPath.Length is zero";

        return stStatus;
      }

      //
      // If ApplicationName is empty then add a formatted string 'ApplicationName.Length is zero' to stStatus and return stStatus.
      // 
      if ( ApplicationName == String.Empty )
      {
        stStatus += "\r\n\r\n ApplicationName.Length is zero";

        return stStatus;
      }

      try
      {

        // 
        // Open the text reader with supplied file
        // 
        using ( reader = File.OpenText( stHomePagePathname ) )
        {
          StringText = reader.ReadToEnd( );
        }

        if ( StringText.Length == 0 )
        {
          stStatus += "\r\n\r\n FileStream.Length is zero";
        }

      }
      catch ( Exception Ex )
      {
        stStatus += "\r\n\r\n Evado.UniForm.Model.Statics.readApplicationHomePage static method Excpetion." + Evado.Model.EvStatics.getException( Ex );
      }

      //
      // Return stStatus
      //
      return stStatus;

    }//END readTextFile method


    // =====================================================================================
    /// <summary>
    /// This method reads an image.
    /// </summary>
    /// <param name="ImageFilePath">String: The image path name.</param>
    /// <param name="ImageFileName">String: The image name.</param>
    /// <param name="BinaryObject">Byte[]: The binary filed.</param>
    /// <returns>String</returns>
    /// <remarks>
    /// This method consists of following steps. 
    /// 
    /// 1. Initialise the methods variables and objects.
    /// 
    /// 2. If ImageFileName is empty then add a formatted string 'ImageFileName.Length is zero' to stStatus and return stStatus. 
    /// 
    /// 3. If ImageFilePath is empty then add a formatted string ' FileStream.Length is zero' to stStatus and return stStatus. 
    /// 
    /// 4. Open and read the file.
    /// 
    /// 5. Return stStatus
    /// 
    /// </remarks>
    /// 
    // -------------------------------------------------------------------------------------
    public static string readInImage( String ImageFilePath, String ImageFileName, out Byte [ ] BinaryObject )
    {

      //
      // Initialise the methods variables and objects.
      //
      BinaryObject = new Byte [ 0 ];
      String stImageFilePathname = ImageFilePath + ImageFileName;
      string stStatus = Evado.Model.EvStatics.CONST_METHOD_START + "Evado.UniForm.Model.Statics.readInImage static method. "
        + ", stImageFilePathname: " + stImageFilePathname;
      try
      {
        //
        // If ImageFileName is empty then add a formatted string 'ImageFileName.Length is zero' to stStatus and return stStatus. 
        //
        if ( ImageFileName == String.Empty )
        {
          stStatus += "\r\n\r\n ImageFileName.Length is zero";

          return stStatus;
        }

        //
        // If ImageFilePath is empty then add a formatted string ' FileStream.Length is zero' to stStatus and return stStatus. 
        //
        if ( ImageFilePath == String.Empty )
        {
          stStatus += "\r\n\r\n ImageFilePath.Length is zero";

          return stStatus;
        }

        //
        // Open and read the file.
        //
        using ( FileStream fileStream = new FileStream( stImageFilePathname, FileMode.Open, FileAccess.Read ) )
        {
          if ( fileStream.Length == 0 )
          {
            stStatus += "\r\n\r\n FileStream.Length is zero";

            return stStatus;
          }

          BinaryObject = new Byte [ fileStream.Length ];

          fileStream.Read( BinaryObject, 0, BinaryObject.Length );

          fileStream.Close( );
        }
      }
      catch ( Exception Ex )
      {
        stStatus += "\r\n\r\n Evado.UniForm.Model.Statics.writeOutImage static method Excpetion." + Evado.Model.EvStatics.getException( Ex );
      }

      //
      // Return stStatus
      //
      return stStatus;

    }//END readInImage method

    // =====================================================================================
    /// <summary>
    ///  This method writes an image out to the image path location.
    /// </summary>
    /// <param name="ImageFilePath">String: The path to the image </param>
    /// <param name="ImageFileName">String: The file name of the image.</param>
    /// <param name="ImageObject">Byte[]: The image as an array of bytes</param>
    /// <returns> String. </returns>
    /// <remarks>
    /// This method consists of following steps.
    /// 
    /// 1. Initialise the methods variables and objects.
    /// 
    /// 2. If length of ImageObject is equal to 0 then assign formatted string 'Object length zero' to stStatus and return stStatus. 
    /// 
    /// 3. Open the stream to the file.
    /// 
    /// 4. Iterate throuch b byte array.
    /// 
    /// 5. Writes a byte to the current position in the file stream.
    /// 
    /// 6.  Close the file.
    /// 
    /// 7. Return stStatus. 
    /// 
    /// </remarks>
    // -------------------------------------------------------------------------------------
    public static string writeOutImage( String ImageFilePath, String ImageFileName, Byte [ ] ImageObject )
    {
      //
      // Initialise the methods variables and objects.
      //
      String stImageFilePathname = ImageFilePath + ImageFileName;

      string stStatus = Evado.Model.EvStatics.CONST_METHOD_START + "Evado.UniForm.Model.Statics.writeOutImage static method. "
        + ", stImageFilePathname: " + stImageFilePathname
        + ", ImageSize: " + ImageObject.Length;
      try
      {

        //
        // If length of ImageObject is equal to 0 then assign formatted string 'Object length zero' to stStatus and return stStatus.  
        //
        if ( ImageObject.Length == 0 )
        {
          stStatus = " >> Object length zero ";
          return stStatus;
        }

        // 
        // Open the stream to the file.
        // 
        using ( FileStream fs = new FileStream( stImageFilePathname, FileMode.Create ) )
        {
          // 
          // Iterate throuch b byte array.
          // 
          foreach ( Byte b in ImageObject )
          {
            //
            // Writes a byte to the current position in the file stream.
            //
            fs.WriteByte( b );
          }//END b iteration

          // 
          // Close the file.
          // 
          fs.Close( );

        }// End StreamWriter.
      }
      catch ( Exception Ex )
      {
        throw ( Ex );
      }

      //
      // Return stStatus
      //
      return stStatus;

    }//END writeOutImage method

    #region Class Enumeration
    /// <summary>
    /// This enumerated list contains upload file status codes.
    /// </summary>
    public enum HttpUploadFileStatusCodes
    {

      /// <summary>
      /// This enumeration defines that upload file status completed
      /// </summary>
      Completed,

      /// <summary>
      /// This enumeration defines file lenght is zero.
      /// </summary>
      File_Length_Zero,

      /// <summary>
      /// This enumeration defines upload file status transfer is failed 
      /// </summary>
      Transfer_Failed,

    }

    #endregion

    //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion


  } // Close Statics class

} // Close namespace Evado.UniForm.Model
