/***************************************************************************************
 * <copyright file="Evado.Model\statics.cs" company="EVADO HOLDING PTY. LTD.">
 *     
 *      Copyright (c) 2002 - 2023 EVADO HOLDING PTY. LTD.  All rights reserved.
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
using System.Collections;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace Evado.Model
{
  /// <summary>
  /// This class provides static methods for use across the application.
  /// </summary>
  [Serializable]
  public partial class EvStatics
  {
    #region class enumerations

    /// <summary>
    /// This enumeration list defines SQL query update code for generating 
    /// the SQL statment.
    /// </summary>
    public enum SqlUpdateQueryCode
    {
      /// <summary>
      /// This enumeration defines the null or not selection state.
      /// </summary>
      Null = 0,
      /// <summary>
      /// This enumeratin defines the add object selection state.
      /// </summary>
      Add = 1,
      /// <summary>
      /// This enumeration defines the update object selection state
      /// </summary>
      Update = 2,
      /// <summary>
      /// This enumeration defines the withdraw object selection state.
      /// </summary>
      Withdraw = 3,
      /// <summary>
      /// This enumeration defeins the delete object selection state.
      /// </summary>
      Delete = -1
    }

    /// <summary>
    /// This enumeration list defines sex options
    /// </summary>
    public enum SexOptions
    {
      /// <summary>
      /// This enumeration defines null or not selection state
      /// </summary>
      Null,

      /// <summary>
      /// This enumeration defines male state of sex options
      /// </summary>
      Male,

      /// <summary>
      /// This enumeration defines female state of sex options
      /// </summary>
      Female,

      /// <summary>
      /// This enumeration defines non binary  state of sex options
      /// </summary>
      Non_Binary,

      /// <summary>
      /// This enumeration defines none of the above state of sex options
      /// </summary>
      Not_Stated,
    }

    /// <summary>
    /// This enumeration list defines marital status options 
    /// </summary>
    public enum MartialStatusOptions
    {
      /// <summary>
      /// This enumeration defines null or not selection state
      /// </summary>
      Null,

      /// <summary>
      /// This enumeration defines single state of marital status options
      /// </summary>
      Single,

      /// <summary>
      /// This enumeration defines married state of marital status options
      /// </summary>
      Married,

      /// <summary>
      /// This enumeration defines divorced state of marital status options
      /// </summary>
      Divorced,

      /// <summary>
      /// This enumeration defines separated state of marital status options
      /// </summary>
      Separated,

      /// <summary>
      /// This emeration defines widowed state of marital status options
      /// </summary>
      Widowed,

      /// <summary>
      /// This emeratation defines unknown state of marital status options
      /// </summary>
      Unknown
    }

    /// <summary>
    /// This enumerated list defines the adapter and class logging types.
    /// </summary>
    public enum LoggingTypes
    {
      Standard = 0,

      Debug = 1,
    }

    //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region Static Constants

    /// <summary>
    /// This static readonly value contains the date null value.
    /// </summary>
    public static readonly DateTime CONST_DATE_NULL = DateTime.Parse ( "1 JAN 1900" );

    /// <summary>
    /// This static readonly value contains the date completed value.
    /// </summary>
    public static readonly DateTime CONST_DATE_COMPLETED = DateTime.Parse ( "1 Jan 1901" );

    /// <summary>
    /// This static readonly value contains the date completed value.
    /// </summary>
    public static readonly DateTime CONST_DATE_MIN_RANGE = DateTime.Parse ( "1 Jan 1902" );

    /// <summary>
    /// This static readonly value contains the date null value.
    /// </summary>
    public static readonly DateTime CONST_DATE_MAX_RANGE = DateTime.Parse ( "31 DEC 2099" );

    /// <summary>
    /// This static readonly value contains the empty guid value..
    /// </summary>
    public static readonly Guid CONST_EMPTY_GUID = new Guid ( "00000000-0000-0000-0000-000000000000" );

    /// <summary>
    /// This static readonly value contains the date completed value.
    /// </summary>
    public static readonly Guid CONST_NEW_OBJECT_ID = new Guid ( "6ff565a2-ae55-4092-83a5-ffffffffffff" );

    /// <summary>
    /// This static readonly value contains home page identifier 1.
    /// </summary>
    public static readonly Guid CONST_LOGIN_COMMAND_ID = new Guid ( "6ff565a2-ae55-4092-83a5-000000000000" );

    /// <summary>
    /// This static readonly value contains home page identifier 1.
    /// </summary>
    public static readonly Guid CONST_DEFAULT_HOME_PAGE_ID = new Guid ( "6ff565a2-ae55-4092-83a5-111111111111" );

    /// <summary>
    /// This static readonly value contains home command identifier 1.
    /// </summary>
    public static readonly Guid CONST_HOME_COMMAND_ID = new Guid ( "6ff565a2-ae55-4092-83a5-111111111111" );

    /// <summary>
    /// This static readonly value contains  home command identifier 2
    /// </summary>
    public static readonly Guid CONST_HOME_COMMAND_2_ID = new Guid ( "6ff565a2-ae55-4092-83a5-222222222222" );

    /// <summary>
    /// This static readonly value contains home command identifier 3
    /// </summary>
    public static readonly Guid CONST_HOME_COMMAND_3_ID = new Guid ( "6ff565a2-ae55-4092-83a5-333333333333" );

    /// <summary>
    /// This static readonly value contains home command identifier 4
    /// </summary>
    public static readonly Guid CONST_HOME_COMMAND_4_ID = new Guid ( "6ff565a2-ae55-4092-83a5-444444444444" );
    /// <summary>
    /// This static readonly value contains home command identifier 5
    /// </summary>
    public static readonly Guid CONST_HOME_COMMAND_5_ID = new Guid ( "6ff565a2-ae55-4092-83a5-555555555555" );

    /// <summary>
    /// This constant defines the default maximum numeric value.
    /// </summary>
    public const float CONST_NUMERIC_MINIMUM = ( float ) -1000000;

    /// <summary>
    /// This constant defines the default mimumum numeric value.
    /// </summary>
    public const float CONST_NUMERIC_MAXIMUM = ( float ) 1000000;

    /// <summary>
    /// This contant defines the integer null value.
    /// </summary>
    public const float CONST_NUMERIC_EMPTY = -1E+35F;

    /// <summary>
    /// This contant defines the integer null value.
    /// </summary>
    public const float CONST_NUMERIC_NULL = -1E+38F;

    /// <summary>
    /// This constant defines the integer error value.
    /// </summary>
    public const float CONST_NUMERIC_ERROR = 1E+38F;

    /// <summary>
    /// This contant defines the numeric null value.
    /// </summary>
    public const int CONST_INTEGER_NULL = int.MinValue;

    /// <summary>
    /// This constant defines the numeric error value.
    /// </summary>
    public const int CONST_INTEGER_ERROR = int.MaxValue;

    /// <summary>
    /// This constant defines the numeric not available text value.
    /// </summary>
    public const String CONST_NUMERIC_NOT_AVAILABLE = "NA";

    /// <summary>
    /// This constant defines the string nul value.
    /// </summary>
    public const String CONST_NIL = "Nil";

    /// <summary>
    /// This constant defines the string null value. 
    /// </summary>
    public const String CONST_NULL = "Null";
    /// <summary>
    /// This constant defines the string null value. 
    /// </summary>
    public const String CONST_DEFAULT = "Default";
    /// <summary>
    /// This constant defines the Home page Id value. 
    /// </summary>
    public const String CONST_HOME_PAGE_ID = "Home_Page";

    /// <summary>
    /// This constant defines the lek valuelength as an integer 
    /// </summary>
    public const int CONST_FIELD_VALUE_KEY_LENGTH = 20;

    /// <summary>
    /// This constant defines the string boolean false value.
    /// </summary>
    public const string STRING_BOOLEAN_FALSE = "false";

    /// <summary>
    /// This constant defines the string boolean true value.
    /// </summary>
    public const string STRING_BOOLEAN_TRUE = "true";

    /// <summary>
    /// This constant defines the Guid identifier key value.
    /// </summary>
    public const string CONST_GUID_IDENTIFIER = "Guid";

    /// <summary>
    /// This constant defines the save action key value 
    /// </summary>
    public const string CONST_SAVE_ACTION = "Action";

    /// <summary>
    /// This constant defines the char, default string list separator value. 
    /// </summary>
    public const char CONST_CHAR_SEPARATOR = ';';

    /// <summary>
    /// This constant defines the string, default string list separator value.
    /// </summary>
    public const String CONST_STRING_SEPARATOR = ";";

    /// <summary>
    /// This constant defines the paragraph separator value.
    /// </summary>
    public const char CONST_PARAGRAPH_SEPARATOR = '\r';

    /// <summary>
    /// This constant defines the paragraph line feed separator value.
    /// </summary>
    public const string CONST_CR_LF = "\r\n";

    /// <summary>
    /// This constant defines the string tab value.
    /// </summary>
    public const string CONST_TAB_SEPARATOR = "\t";

    /// <summary>
    /// This constant defines the debug method header value.
    /// </summary>
    public const string CONST_METHOD_START =
      "------------------------------------------------------------"
      + "--------------------------------------------------\r\n";

    /// <summary>
    /// This constant defines the debug method header value.
    /// </summary>
    public const string CONST_METHOD_END =
      "------------------------- END OF METHOD "
      + "-------------------------\r\n";


    //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region Web Config application setting key values.

    /// <summary>
    /// This constant defines the web config event source key value
    /// </summary>
    public const string CONFIG_EVENT_LOG_KEY = "EventLogSource";

    /// <summary>
    /// This constant defines the web config SSL protocol key value
    /// </summary>
    public const string CONFIG_SSL_PROTOCOL_KEY = "SSL_PROTOCOL";

    /// <summary>
    /// This constant defines the web config SMTP server name key value
    /// </summary>
    public const string CONFIG_SMTP_SERVER_KEY = "SmtpServer";

    /// <summary>
    /// This constant defines the web config debug key value
    /// </summary>
    public const string CONFIG_SMTP_SERVER_PORT_KEY = "SmtpServerPort";

    /// <summary>
    /// This constant defines the web config SMTP user ID key value
    /// </summary>
    public const string CONFIG_SMTP_USER_KEY = "SmtpUserId";

    /// <summary>
    /// This constant defines the web config SMTP user password key value
    /// </summary>
    public const string CONFIG_SMTP_USER_PASSWORD_KEY = "SmtpPassword";

    /// <summary>
    /// This constant defines the web config help url key value
    /// </summary>
    public const string CONFIG_STATIC_FILE_PATH_KEY = "STATIC_FILE_PATH";

    /// <summary>
    /// This constant defines the web config debug key value
    /// </summary>
    public const string CONFIG_LOG_SETTING_KEY = "LOG_SETTING";

    /// <summary>
    /// This constant defines the java web config debug key value
    /// </summary>
    public const string CONFIG_JAVA_DEBUG_KEY = "JavaDebug";

    /// <summary>
    /// This constant defines the web config debug validation on key value
    /// </summary>
    public const string CONFIG_DEBUG_VALIDATION_KEY = "DebugValidationOn";

    /// <summary>
    /// This constant defines the LDAP connection string key value.
    /// </summary>
    public const string CONFIG_LDAP_CONNECTION_STRING = "ADConnectionString";

    /// <summary>
    /// This constant defines the server log file path key value.
    /// </summary>
    public const string CONFIG_LOG_FILE_PATH = "LogFilePath";

    /// <summary>
    /// This constant defines the server log file path key value.
    /// </summary>
    public const string CONFIG_EXPORT_FILE_PATH = "ExportFilePath";

    /// <summary>
    /// This constant defines the default logo filename key value.
    /// </summary>
    public const string CONFIG_LOGO_FILE_NAME = "LogoFilename";

    //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region Static general conversion methods.

    // =====================================================================================
    /// <summary>
    /// This method get the julien date.
    /// </summary>
    /// <param name="Date">DateTime Object</param>
    /// <returns>long: julien date.</returns>
    // -------------------------------------------------------------------------------------
    public static long ConvertToJulian ( DateTime Date )
    {
      int Month = Date.Month;
      int Day = Date.Day;
      int Year = Date.Year;

      if ( Month < 3 )
      {
        Month = Month + 12;
        Year = Year - 1;
      }
      long JulianDay = Day + ( 153 * Month - 457 ) / 5 + 365 * Year + ( Year / 4 ) - ( Year / 100 ) + ( Year / 400 ) + 1721119;
      return JulianDay;
    }

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
    public static String encodeMinorVersion ( float MinorVersion )
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


    // =====================================================================================
    /// <summary>
    /// This method deserialises an Xml object into a generic type.
    /// </summary>
    /// <param name="MinorVersion">String: Minor version encoded as a string</param>
    /// <returns>Float: minor version as a floating point number.</returns>
    /// <remarks>
    /// This method consists of following steps. 
    /// 
    /// 1. Initialise the methods variables and objects.
    /// 
    /// 2. Convert string encoding value.
    /// 
    /// 3. Convert the string to a floating point number.
    /// 
    /// 4.Return the minor version as a floating point object.
    /// 
    /// </remarks>
    // -------------------------------------------------------------------------------------
    public static float decodeMinorVersion ( String MinorVersion )
    {
      // 
      // Initialise the methods variables and objects.
      // 
      float minorVersion = 1.0F;
      String stMinorVersion = MinorVersion.ToLower ( );
      stMinorVersion = stMinorVersion.Replace ( "-", "." );
      stMinorVersion = stMinorVersion.Replace ( "_", "." );
      stMinorVersion = stMinorVersion.Replace ( ":", String.Empty );
      stMinorVersion = stMinorVersion.Replace ( "v", String.Empty );

      //
      // convert the string minor version to a floating number.
      //
      if ( stMinorVersion.Contains ( "." ) == true )
      {
        if ( float.TryParse ( stMinorVersion, out minorVersion ) == false )
        {
          minorVersion = float.NaN;
        }
      }

      // 
      // Return the minor version at a floating number.
      // 
      return minorVersion;

    }//END decodeMinorVersion method.

    //  =========================================================================================
    /// <summary>
    /// This class transforms an enum value to a string. It replaces any _ character with a blank space.
    /// </summary>
    /// <param name="enumObject">Object: an enumeration object</param>
    /// <returns>String: a string</returns>
    /// <remarks>
    /// This class consists of the following steps:
    /// 
    /// 1. Create a name string of enumObject
    /// 
    /// 2. if the name string is "null" get returned empty string.
    /// 
    /// 3. If not replace "_" in the name string with a blank character. 
    /// </remarks>
    //  -----------------------------------------------------------------------------------------
    public static String enumValueToString ( Object enumObject )
    {
      //
      // Define an internal string with enumeration object value
      //
      String name = Enum.GetName ( enumObject.GetType ( ), enumObject );

      //
      // Set the string to empty if it does not exist
      //
      if ( name.ToLower ( ) == "null" )
      {
        name = string.Empty;
      }

      //
      // Return a string with a blank character
      //
      return name.Replace ( "_", " " );

    }//END enumValueToString method

    //  =========================================================================================
    /// <summary>
    /// This class transforms a enumerated value into a string value.
    /// </summary>
    /// <param name="enumObject">Object: an enumeration object</param>
    /// <returns>String: a string</returns>
    /// <remarks>
    /// This class consists of the following steps:
    /// 
    /// 1. Create a name string of enumObject
    /// 
    /// 2. if the name string is "null" get returned empty string.
    /// 
    /// </remarks>
    //  -----------------------------------------------------------------------------------------
    public static String getEnumStringValue ( Object enumObject )
    {
      //
      // Define an internal string with enumeration object value
      //
      String name = Enum.GetName ( enumObject.GetType ( ), enumObject );

      //
      // Set the string to empty if it does not exist
      //
      if ( name.ToLower ( ) == "null" )
      {
        name = string.Empty;
      }

      //
      // Return a string with a blank character
      //
      return name;

    }//END getEnumStringValue method

    // =====================================================================================
    /// <summary>
    ///   This method returns an EvEventCode from an external event code.
    /// </summary>
    /// <param name="EventCode">T: Guid</param>
    /// <returns>EvEventCodes enumerated value.</returns>
    // -------------------------------------------------------------------------------------
    public static Evado.Model.EvEventCodes setEvadoEventCode<T> ( T EventCode )
    {
      //
      // Define the methods variables and objects.
      //
      String eventCode = EventCode.ToString ( );
      Evado.Model.EvEventCodes outEventCode = Evado.Model.EvEventCodes.Ok;

      //
      // Try the conversion
      //
      if ( Evado.Model.EvStatics.tryParseEnumValue<EvEventCodes> ( eventCode, out outEventCode ) == true )
      {
        return outEventCode;
      }

      //
      // Return conversion error.
      //
      return Evado.Model.EvEventCodes.Null;

    }//END getEvadoEventCode method

    // =====================================================================================
    /// <summary>
    ///   This method returns an EvEventCode from an external event code.
    /// </summary>
    /// <param name="EventCode">T: Guid</param>
    /// <returns>EvEventCodes enumerated value.</returns>
    // -------------------------------------------------------------------------------------
    public static T getEvadoEventCode<T> ( Evado.Model.EvEventCodes EventCode )
    {
      //
      // Define the methods variables and objects.
      //
      String eventCode = EventCode.ToString ( );
      T outEventCode = default ( T );

      //
      // Try the conversion
      //
      if ( Evado.Model.EvStatics.tryParseEnumValue<T> ( eventCode, out outEventCode ) == true )
      {
        return outEventCode;
      }

      //
      // Return conversion error.
      //
      return outEventCode;

    }//END convertEventCode method



    //  =========================================================================================
    /// <summary>
    /// This class transforms an enum value to a string. It replaces any _ character with a blank space.
    /// </summary>
    /// <param name="enumObject">Object: an enumeration object</param>
    /// <returns>String: a string</returns>
    /// <remarks>
    /// This class consists of the following steps:
    /// 
    /// 1. Create a name string of enumObject
    /// 
    /// 2. if the name string is "null" get returned empty string.
    /// 
    /// 3. If not replace "_" in the name string with a blank character. 
    /// </remarks>
    //  -----------------------------------------------------------------------------------------
    public static String getEnumValueAsString ( Object enumObject )
    {
      //
      // Define an internal string with enumeration object value
      //
      String name = Enum.GetName ( enumObject.GetType ( ), enumObject );

      //
      // Set the string to empty if it does not exist
      //
      if ( name.ToLower ( ) == "null" )
      {
        name = string.Empty;
      }

      //
      // Return a string with a blank character
      //
      return name.Replace ( "_", " " );

    }//END enumValueToString method

    // =====================================================================================
    /// <summary>
    ///   This method returns a Guid of the passed string.
    /// </summary>
    /// <param name="GuidValue">string: Guid</param>
    /// <returns>Guid: Guid value.</returns>
    /// <remarks>
    /// This method consists of the following steps:
    /// 
    /// 1. Try adding the GuidValue
    /// 
    /// 2. If not, return an empty Guid
    /// </remarks>
    // -------------------------------------------------------------------------------------
    public static Guid getGuid ( string GuidValue )
    {
      try
      {
        //
        // Add GuidValue, if it has 36 character
        //
        if ( GuidValue.Length == 36 )
        {
          return new Guid ( GuidValue );
        }
      }
      catch
      {
        return Guid.Empty;
      }

      return Guid.Empty;
    }//END getGuid method

    // =====================================================================================
    /// <summary>
    ///   This method returns a Guid of the passed string.
    /// </summary>
    /// <param name="BoolValue">String: (Mandatory) bollean expression as a string.</param>
    /// <returns>bool: true, if BoolValue contains the valid characters.</returns>
    /// <remarks>
    /// This method consists of the following steps
    /// 
    /// 1. Validate the BoolValue is not empty
    /// 
    /// 2. Formating the BoolValue string
    /// 
    /// 3. Return true, if BoolValue consists of the valid characters 
    /// </remarks>
    // -------------------------------------------------------------------------------------
    public static bool getBool ( string BoolValue )
    {
      //
      // Validate the BoolValue string
      //
      if ( BoolValue == String.Empty )
      {
        return false;
      }

      //
      // Format the BoolValue string
      //
      BoolValue = BoolValue.Replace ( "&nbsp;", String.Empty );
      BoolValue = BoolValue.Trim ( );
      BoolValue = BoolValue.ToLower ( );

      //
      // Return true, if BoolValue is valid
      //
      if ( BoolValue == "yes"
        || BoolValue == "true"
        || BoolValue == "t"
        || BoolValue == "y"
        || BoolValue == "on"
        || BoolValue == "1" )
      {
        return true;
      }//END if

      return false;

    }//END getBool method

    // =====================================================================================
    /// <summary>
    ///   This method returns a Guid of the passed string.
    /// </summary>
    /// <param name="BoolValue">String: (Mandatory) bollean expression as a string.</param>
    /// <returns>bool: true, if BoolValue contains the valid characters.</returns>
    /// <remarks>
    /// This method consists of the following steps
    /// 
    /// 1. Validate the BoolValue is not empty
    /// 
    /// 2. Formating the BoolValue string
    /// 
    /// 3. Return true, if BoolValue consists of the valid characters 
    /// </remarks>
    // -------------------------------------------------------------------------------------
    public static int getBoolInteger ( string BoolValue )
    {
      //
      // Validate the BoolValue string
      //
      if ( BoolValue == String.Empty )
      {
        return 0;
      }

      //
      // Format the BoolValue string
      //
      BoolValue = BoolValue.Replace ( "&nbsp;", String.Empty );
      BoolValue = BoolValue.Trim ( );
      BoolValue = BoolValue.ToLower ( );

      //
      // Return true, if BoolValue is valid
      //
      if ( BoolValue == "yes"
        || BoolValue == "true"
        || BoolValue == "t"
        || BoolValue == "y"
        || BoolValue == "on"
        || BoolValue == "1" )
      {
        return 1;
      }//END if

      return 0;

    }//END getBool method

    // =====================================================================================
    /// <summary>
    ///   This method returns a Guid of the passed string.
    /// </summary>
    /// <param name="BoolValue">String: (Mandatory) bollean expression as a string.</param>
    /// <returns>bool: true, if BoolValue contains the valid characters.</returns>
    /// <remarks>
    /// This method consists of the following steps
    /// 
    /// 1. Validate the BoolValue is not empty
    /// 
    /// 2. Formating the BoolValue string
    /// 
    /// 3. Return true, if BoolValue consists of the valid characters 
    /// </remarks>
    // -------------------------------------------------------------------------------------
    public static String getBoolAsString ( bool BoolValue )
    {
      //
      // Validate the BoolValue string
      //
      if ( BoolValue == true )
      {
        return Evado.Model.EvmLabels.Label_Yes;
      }

      return Evado.Model.EvmLabels.Label_No;

    }//END getBool method

    // =====================================================================================
    /// <summary>
    ///   This method returns a Guid of the passed string
    /// </summary>
    /// <param name="FloatValue">string: (Mandatory) float value represented as a string.</param>
    /// <returns>float: float value.</returns>
    /// <remarks>
    /// This method consists of the following steps:
    /// 
    /// 1. Create a return float value
    /// 
    /// 2. return float value, if it is in float format
    /// 
    /// 3. return 0, if the FloatValue string is not float
    /// 
    /// If the string is empty the returned value is -1E+38F.
    /// If the string is not a number the returned value is 1E+38F.
    /// 
    /// </remarks>
    // -------------------------------------------------------------------------------------
    public static float getFloat ( string FloatValue )
    {
      //
      // Create an internal floatValue
      //
      float floatValue = 0;
      if ( FloatValue == String.Empty
        || FloatValue == Evado.Model.EvStatics.CONST_NUMERIC_NOT_AVAILABLE )
      {
        return Evado.Model.EvStatics.CONST_NUMERIC_NULL;
      }

      //
      // Return Float value, if it is float format
      //
      if ( float.TryParse ( FloatValue, out floatValue ) == true )
      {
        return floatValue;
      }
      return Evado.Model.EvStatics.CONST_NUMERIC_ERROR;

    }//END getFloat method

    // =====================================================================================
    /// <summary>
    ///   This method returns an integer of the passed string.
    /// </summary>
    /// <param name="BoolValue">bool: if the value is an integer.</param>
    /// <returns>int: integer value.</returns>
    /// <remarks>
    /// This method consists of the following steps:
    /// 
    /// 1. Create a return integer value
    /// 
    /// 2. return integer value, if it is in float format
    /// 
    /// 3. return 0, if the IntValue string is not float
    /// </remarks>
    // -------------------------------------------------------------------------------------
    public static int getInteger ( bool BoolValue )
    {
      //
      // Handle bool value converted to 0 or 1.
      //
      if ( BoolValue == true )
      {
        return 1;
      }
      return int.MinValue;

    }//END getInteger method

    // =====================================================================================
    /// <summary>
    ///   This method returns an integer of the passed string.
    /// </summary>
    /// <param name="IntValue">string: (Mandatory) Float represented as a string.</param>
    /// <returns>int: integer value.</returns>
    /// <remarks>
    /// This method consists of the following steps:
    /// 
    /// 1. Create a return integer value
    /// 
    /// 2. return integer value, if it is in float format
    /// 
    /// 3. return 0, if the IntValue string is not float
    /// </remarks>
    // -------------------------------------------------------------------------------------
    public static int getInteger ( string IntValue )
    {
      //
      // Create a return intValue
      //
      int intValue = 0;

      //
      // if a string IntValue is integer, return IntValue
      //
      if ( int.TryParse ( IntValue, out intValue ) == true )
      {
        return intValue;
      }
      return 0;
    }//END getInteger method

    // =====================================================================================
    /// <summary>
    ///   This method returns a short integer of the passed string.
    /// </summary>
    /// <param name="IntValue">string: (Mandatory) Short represented as a string.</param>
    /// <returns>short: get short value.</returns>
    /// <remarks>
    /// This method consists of the following steps:
    /// 
    /// 1. Create a return short value
    /// 
    /// 2. return short value, if it is in short format
    /// 
    /// 3. return 0, if the Value string is not short
    /// </remarks>
    // -------------------------------------------------------------------------------------
    public static short getShort ( string IntValue )
    {
      //
      // Create a return short value
      //
      short intValue = 0;

      //
      // return short value, if intValue string contains short value 
      //
      if ( short.TryParse ( IntValue, out intValue ) == true )
      {
        return intValue;
      }

      return short.MinValue;

    }//END getShort method

    // =====================================================================================
    /// <summary>
    ///   This method returns a string of a integer.
    /// </summary>
    /// <param name="Value">short: numeric.</param>
    /// <returns>Stirng: get short value.</returns>
    // -------------------------------------------------------------------------------------
    public static String getFloatAsString ( float Value )
    {
      return Value.ToString ( "####0.000" );

    }//END getFloatAsString method

    // =====================================================================================
    /// <summary>
    ///   This method returns a string of a integer.
    /// </summary>
    /// <param name="IntValue">short: numeric.</param>
    /// <returns>Stirng: get short value.</returns>
    // -------------------------------------------------------------------------------------
    public static String getIntegerAsString ( int IntValue )
    {
      return IntValue.ToString ( "####0" );

    }//END getIntegerAsString method

    // =====================================================================================
    /// <summary>
    ///   This method returns a string of a short integer.
    /// </summary>
    /// <param name="IntValue">short: numeric.</param>
    /// <returns>Stirng: get short value.</returns>
    // -------------------------------------------------------------------------------------
    public static String getShortAsString ( short IntValue )
    {
      return IntValue.ToString ( "####0" );

    }//END getShortAsString method

    // =====================================================================================
    /// <summary>
    ///   This method returns a Datetime of the passed string.
    /// </summary>
    /// <param name="DateValue">string: (Mandatory) Date stamp value represented as a string.</param>
    /// <returns>DateTime: date time value.</returns>
    /// <remarks>
    /// This method consists of the following steps:
    /// 
    /// 1. Create a return dateValue
    /// 
    /// 2. Return DateValue, if it contains date time value
    /// 
    /// </remarks>
    // -------------------------------------------------------------------------------------
    public static DateTime getDateTime ( string DateValue )
    {
      //
      // Create a return dateValue
      //
      DateTime dateValue = Evado.Model.EvStatics.CONST_DATE_NULL;

      //
      // Return DateValue, if it contains date time value
      //
      if ( DateTime.TryParse ( DateValue, out dateValue ) == true )
      {
        return dateValue;
      }
      return Evado.Model.EvStatics.CONST_DATE_NULL;
    }//END getDateTime method

    //  ===================================================================================
    /// <summary>
    /// Formats a date as a string.
    /// </summary>
    /// <param name="Date">DateTime: date value</param>
    /// <returns>String: get the date in string format</returns>
    /// <remarks>
    /// This method consists of the following step:
    /// 
    /// 1. Convert the Date value into string format, it exists
    /// 
    /// </remarks>
    //  -----------------------------------------------------------------------------------
    public static String getDateAsString ( DateTime Date )
    {
      //
      // if date value exists, return string date value
      //
      if ( Date != CONST_DATE_NULL )
      {
        if ( Date == Evado.Model.EvStatics.CONST_DATE_NULL )
        {
          return String.Empty;
        }
        return Date.ToString ( "dd-MMM-yyyy" );
      }

      return String.Empty;

    }//END getDateAsString method

    //  ===================================================================================
    /// <summary>
    /// Formats a date as a string.
    /// </summary>
    /// <param name="Date">DateTime: date value</param>
    /// <returns>String: get the date in string format</returns>
    /// <remarks>
    /// This method consists of the following step:
    /// 
    /// 1. Convert the Date value into string format, it exists
    /// 
    /// </remarks>
    //  -----------------------------------------------------------------------------------
    public static String getDateAsIsoString ( DateTime Date )
    {
      //
      // if date value exists, return string date value
      //
      if ( Date != CONST_DATE_NULL )
      {
        if ( Date == Evado.Model.EvStatics.CONST_DATE_NULL )
        {
          return String.Empty;
        }
        return Date.ToString ( "yyy-MMM-dd" );
      }

      return String.Empty;

    }//END getDateAsString method

    //  ===================================================================================
    /// <summary>
    /// Formats a date as a string.
    /// </summary>
    /// <param name="Date">DateTime: date value</param>
    /// <returns>String: get the date in string format</returns>
    /// <remarks>
    /// This method consists of the following step:
    /// 
    /// 1. Convert the Date value into string format, it exists
    /// 
    /// </remarks>
    //  -----------------------------------------------------------------------------------
    public static String getDateTimeAsIsoString ( DateTime Date )
    {
      //
      // if date value exists, return string date value
      //
      if ( Date != CONST_DATE_NULL )
      {
        return Date.ToString ( "yyyy-MMM-ddTHH:mm:ss" );
      }

      return String.Empty;

    }//END getDateAsString method

    // =====================================================================================
    /// <summary>
    ///   This method returns an option string of the passed string.
    /// </summary>
    /// <param name="Option">string: (Mandatory) string of ':' separated option values.</param>
    /// <returns>String: get option value.</returns>
    /// <remarks>
    /// This method consists of the following steps:
    /// 
    /// 1. Validate whether Option string contains a ":" character
    /// 
    /// 2. Use ":" to split the Option string
    /// 
    /// 3. Return a front part of the Option string
    /// 
    /// 4. If no ":" exists, return the whole Option string
    /// </remarks>
    // -------------------------------------------------------------------------------------
    public static String getOptionValue ( string Option )
    {
      if ( Option.Contains ( ":" ) == true )
      {
        //
        // Split string option into a string array
        //
        string [ ] arrOption = Option.Split ( ':' );

        //
        // Return the front part of string array as option value
        //
        return arrOption [ 0 ];
      }

      return Option;
    }//END getOptionValue method

    // =====================================================================================
    /// <summary>
    ///   This method returns option's description of the passed string.
    /// </summary>
    /// <param name="Option">string: the description of option</param>
    /// <returns>String: get option's description</returns>
    /// <remarks>
    /// This method consists of the following steps:
    /// 
    /// 1. Validate whether Option string contains a ":" character
    /// 
    /// 2. Use ":" to split the Option string
    /// 
    /// 3. Return a back part of the Option string
    /// 
    /// 4. If no ":" exists, return the whole Option string
    /// </remarks>
    // -------------------------------------------------------------------------------------
    public static String getOptionDescription ( string Option )
    {
      if ( Option.Contains ( ":" ) == true )
      {
        //
        // Split string option into a string array
        //
        string [ ] arrOption = Option.Split ( ':' );
        if ( arrOption.Length > 1 )
        {
          //
          // Return the back part of the Option string
          //
          return arrOption [ 1 ];
        }
      }

      //
      // return the back part of string array as option description 
      //
      return Option;
    }//END getOptionDescription method

    // =====================================================================================
    /// <summary>
    ///   This method returns an option object of the passed string.
    /// </summary>
    /// <param name="Option">string: string of ':' separates option value</param>
    /// <returns>Evado.Model.Evoption: get option value object</returns>
    /// <remarks>
    /// This method consists of the following steps:
    /// 
    /// 1. Create an index that contains a ":" character of the Option string
    /// 
    /// 2. Validate whether the index exists
    /// 
    /// 3. Set Value to the front part and Description to the back part of index
    /// 
    /// </remarks>
    // -------------------------------------------------------------------------------------
    public static Evado.Model.EvOption getOption ( string Option )
    {
      //
      // Create an index of the ":" character of the Option
      //
      int index = Option.IndexOf ( ':' );

      //
      // Validate whether the Option string contains ":" character
      //
      if ( index > 0 )
      {
        //
        // Set the value to the front part of index 
        //
        string value = Option.Substring ( 0, index );

        //
        // Set the description to the back part of index
        //
        string description = Option.Substring ( index );
        {
          return new Evado.Model.EvOption ( value, description );
        }
      }

      //
      // return value and description to new option object
      //
      return new Evado.Model.EvOption ( Option );
    }//END getOption method

    // =====================================================================================
    /// <summary>
    ///   This method returns if the passed string has option value.
    /// </summary>
    /// <param name="Option">Evado.Model.EvOption: (Mandatory) object containing the option.</param>
    /// <param name="OptionIndex">int: (Mandatory) The option index (for code by value ).</param>
    /// <param name="OptionValues">string: (Mandatory) The string option value</param>
    /// <returns>bool: get option value.</returns>
    /// <remarks>
    /// This method consists of the following steps:
    /// 
    /// 1. Initialize array option value and option index
    /// 
    /// 2. Loop through the array option
    /// 
    /// 3. Validate the value of Option with the ones of OptionValue
    /// 
    /// 4. If Validation pass, return true
    /// 
    /// </remarks>
    // -------------------------------------------------------------------------------------
    public static bool hasOptionValue ( Evado.Model.EvOption Option, int OptionIndex, String OptionValues )
    {
      //
      // Initialize arrOptionValue array to contain the list of characters from OptionValues string
      // after spliting a ";" character
      //
      string [ ] arrOptionValue = OptionValues.Split ( ';' );
      int iOptionIndex = OptionIndex + 1;

      //
      // Iterate through the arrOptionValue array to find a match.
      //
      for ( int index = 0; index < arrOptionValue.Length; index++ )
      {
        //
        // Return true, if any item (value, index, description) of Option has the same value
        // with the one in arrOptionValue array
        //
        if ( Option.Value == arrOptionValue [ index ].Trim ( )
          || iOptionIndex.ToString ( ) == arrOptionValue [ index ].Trim ( )
          || Option.Description == arrOptionValue [ index ].Trim ( ) )
        {
          return true;
        }
      }

      //
      // if any option's item (value, description) matches the option's value return true.
      //
      if ( Option.Value == OptionValues.Trim ( )
        || Option.Description == OptionValues.Trim ( ) )
      {
        return true;
      }

      return false;
    }//END hasOptionValue method

    // =====================================================================================
    /// <summary>
    ///  This method returns a list of option object for the string of options.
    ///  Where the selection options are separated by ';' and option values are separated by ':'
    ///  V:D:  'V' is the value and 'D' is the description. 
    /// </summary>
    /// <param name="StringOptions">String: string options with encoded separators</param>
    /// <param name="IsSelectionList">Boolean: true, use value coding for all options</param>
    /// <returns>List: List of Option objects.</returns>
    /// <remarks>
    /// This method consists of the following steps:
    /// 
    /// 1. Initialize a return option list
    /// 
    /// 2. Create an array to split StringOption 
    /// 
    /// 3. Loop through the array
    /// 
    /// 4. Add value and description to the items of array if they exist
    /// 
    /// </remarks>
    // -------------------------------------------------------------------------------------
    public static List<Evado.Model.EvOption> getStringAsOptionList (
      String StringOptions, bool IsSelectionList )
    {
      //
      // Initialise a return option list
      //
      List<Evado.Model.EvOption> list = new List<Evado.Model.EvOption> ( );
      if ( IsSelectionList == true )
      {
        list.Add ( new EvOption ( ) );
      }
      //
      // Crate an arrStringOptions array
      //
      string [ ] arrStringOptions = StringOptions.Split ( ';' );

      //
      // iteration through the array of selection options.
      //
      for ( int i = 0; i < arrStringOptions.Length; i++ )
      {
        //
        // Create a string option to get a value from option array
        //
        string stringOption = arrStringOptions [ i ].Trim ( );

        // 
        // Validate whether the option exists.
        //
        if ( stringOption == String.Empty )
        {
          continue;
        }

        //
        // Get the option value from the string.
        //
        Evado.Model.EvOption option = Evado.Model.EvStatics.getStringAsOption ( stringOption );

        //
        // Add the option to the list of options.
        //
        list.Add ( option );


      }//END option array iteration loop.

      // 
      // Return the option list.
      // 
      return list;

    }//END getStringAsOptionList method

    // =====================================================================================
    /// <summary>
    ///  This method returns an Evado.Model.EvOption object containing a value and description values.
    ///  If the OptionValue contains a ':' the left side of the ':' is considered the option 
    ///  value  and the right side of the ':' is considered the option description 
    /// </summary>
    /// <param name="OptionValue">string: the string option value</param>
    /// <returns>Evado.Model.EvOption: Option object.</returns>
    // -------------------------------------------------------------------------------------
    private static Evado.Model.EvOption getStringAsOption ( string OptionValue )
    {
      //
      // Initialize the return Option object
      //
      Evado.Model.EvOption option = new Evado.Model.EvOption ( );

      //
      // Return Evado.Model.EvOption object with the same value in the Option.Value and Option.Description fields.
      //
      if ( OptionValue.Contains ( ":" ) == false )
      {
        return new Evado.Model.EvOption ( OptionValue );
      }

      //
      // Split the OptionValue string to get the value and description
      //
      string [ ] arrStringOptions = OptionValue.Split ( ':' );

      option = new Evado.Model.EvOption ( arrStringOptions [ 0 ], arrStringOptions [ 1 ] );

      // 
      // Return the option object.
      // 
      return option;

    }//END getStringAsOption method

    //  ===================================================================================
    /// <summary>
    /// This method tests to see if a value is in the option list.
    /// </summary>
    /// <param name="OptionList">List of Evado.Model.EvOption objects</param>
    /// <param name="Value">String: Optoon value.</param>
    /// <returns>bool: True = Value exists</returns>
    //  -----------------------------------------------------------------------------------
    public static bool hasOption ( List<Evado.Model.EvOption> OptionList, object Value )
    {
      String value = Value.ToString ( );

      foreach ( Evado.Model.EvOption option in OptionList )
      {
        if ( option.Value.ToLower ( ) == value.ToLower ( ) )
        {
          return true;
        }
      }

      return false;
    }//END hasOption method.

    // =====================================================================================
    /// <summary>
    /// This method returns an a paragraph string to a html markup.
    /// </summary>
    /// <param name="Text">string: a Html text</param>
    /// <returns>strin: a paragrahp string of html markup</returns>
    /// <remarks>
    /// This method consists of the following steps:
    /// 
    /// 1. Convert the string into html format
    /// </remarks>
    // -------------------------------------------------------------------------------------
    public static string getStringAsHtml ( string Text )
    {
      // 
      // Format the text string into Html
      // 
      string stHtml = "<p>" + Text.Replace ( "\r", "" ) + "</p>";
      stHtml = stHtml.Replace ( "\n", "<br/>" );
      return stHtml;

    }//END getStringAsHtml method

    /// <summary>
    /// This method returns an a paragraph string to a html markup.
    /// </summary>
    /// <param name="Html">string: a Html paragraph</param>
    /// <returns>string: a Html paragraph</returns>
    /// <remarks>
    /// This method consists of the following steps:
    /// 
    /// 1. Convert a html into a string format
    /// </remarks>
    // -------------------------------------------------------------------------------------
    public static string getHtmlAsString ( string Html )
    {
      // 
      // Format the Html into a string
      // 
      string text = Html.Replace ( "<br/>", "\r\n" );
      text = text.Replace ( "<br />", "\r\n" );
      text = text.Replace ( "<hr />", "\r\n__________________________________________________________\r\n" );
      text = text.Replace ( "<hr/>", "\r\n__________________________________________________________\r\n" );
      text = text.Replace ( "<strong>", String.Empty );
      text = text.Replace ( "</strong>", String.Empty );
      text = text.Replace ( "<b>", String.Empty );
      text = text.Replace ( "</b>", String.Empty );
      text = text.Replace ( "<u>", String.Empty );
      text = text.Replace ( "</u>", String.Empty );
      text = text.Replace ( "<p>", "\r\n\r\n" );
      text = text.Replace ( "</p>", "" );
      text = text.Replace ( "<table>", "" );
      text = text.Replace ( "<table >", "" );
      text = text.Replace ( "</table>", "" );
      text = text.Replace ( "<tr>", "" );
      text = text.Replace ( "<tr >", "" );
      text = text.Replace ( "</tr>", "\r\n" );
      text = text.Replace ( "<td>", "\t" );
      text = text.Replace ( "<td >", "\t" );
      text = text.Replace ( "</td>", "" );

      return text;

    }//END getStringAsHtml method

    //  ===================================================================================
    /// <summary>
    /// Formats a string date and returns another string with the specified pattern.
    /// The original string date should be in the format dd MMM yyyy HH:mm:ss which is the 
    /// original format in the data base.
    /// </summary>
    /// <param name="SexValue">EvStatics.SexOptions: The enumeration list of sex options</param>
    /// <returns>String: string HL7 value</returns>
    /// Andres Castano 19 Nov 2009
    /// <remarks>
    /// This method consists of the following steps:
    /// 
    /// 1. Switch to a code of retrieving SexValue
    /// 
    /// </remarks>
    //  -----------------------------------------------------------------------------------
    public static String getHL7SexValue ( Evado.Model.EvStatics.SexOptions SexValue )
    {
      // 
      // Use the switch to get the relevant code for the sex value.
      // 
      switch ( SexValue )
      {
        case Evado.Model.EvStatics.SexOptions.Male:
          return "M";
        case Evado.Model.EvStatics.SexOptions.Female:
          return "F";
        default:
          return "U";
      }//END Switch

    }//END getHL7SexValue method

    //  ===================================================================================
    /// <summary>
    /// Formats a string martial status enumerstion in to an HL7 representation
    /// </summary>
    /// <param name="StatusValue">EvStatics.MartialStatusOptions: The enumeration list of status value</param>
    /// <returns>String: the marital status value based on HL7 code</returns>
    /// Andres Castano 19 Nov 2009
    /// <remarks>
    /// This method consists of the following steps:
    /// 
    /// 1. Switch to the code of retrieving the marital status options
    /// </remarks>
    //  -----------------------------------------------------------------------------------
    public static String getHL7MaritalStatusValue ( Evado.Model.EvStatics.MartialStatusOptions StatusValue )
    {
      // 
      // Use the switch to get the relevant code for the martial status value.
      // 
      switch ( StatusValue )
      {
        case Evado.Model.EvStatics.MartialStatusOptions.Single:
          return "S";
        case Evado.Model.EvStatics.MartialStatusOptions.Married:
          return "M";
        case Evado.Model.EvStatics.MartialStatusOptions.Divorced:
          return "D";
        case Evado.Model.EvStatics.MartialStatusOptions.Separated:
          return "A";
        case Evado.Model.EvStatics.MartialStatusOptions.Widowed:
          return "W";
        default:
          return "U";
      }//END Switch

    }//END getHL7MaritalStatusValue method

    //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region Static field value encoding and decoding methods.

    // =====================================================================================
    /// <summary>
    ///   This method decodes a form field numeric value for display.
    /// </summary>
    /// <param name="TextValue">string: (Mandatory) Float represented as a string.</param>
    /// <returns>string: float value.</returns>
    /// <remarks>
    /// This method consists of the following steps:
    /// 
    /// 1. Create a floatValue and upperBoundary
    /// 
    /// 2. Validate whether the TextValue contains the float value
    /// 
    /// 3. Return the textValue if true and null value if false
    /// </remarks>
    // -------------------------------------------------------------------------------------
    public static String convertNumNullToTextNull ( string TextValue )
    {
      float flValue = Evado.Model.EvStatics.getFloat ( TextValue );
      //
      // Validate whether TextValue contains the float value
      //
      if ( flValue < -1E+37 || flValue > 1E+37 )
      {
        return String.Empty;
      }
      if ( flValue > 1E+35 )
      {
        return Evado.Model.EvStatics.CONST_NUMERIC_NOT_AVAILABLE;
      }

      return TextValue;

    }//END convertNulToNegInfl method
    // =====================================================================================
    /// <summary>
    ///   This method decodes a form field numeric value for display.
    /// </summary>
    /// <param name="TextValue">string: (Mandatory) Float represented as a string.</param>
    /// <returns>string: float value.</returns>
    /// <remarks>
    /// This method consists of the following steps:
    /// 
    /// 1. Create a floatValue and upperBoundary
    /// 
    /// 2. Validate whether the TextValue contains the float value
    /// 
    /// 3. Return the textValue if true and null value if false
    /// </remarks>
    // -------------------------------------------------------------------------------------
    public static String convertTextNullToNumNull ( string TextValue )
    {
      //
      // Validate whether TextValue contains the float value
      //
      if ( TextValue == Evado.Model.EvStatics.CONST_NUMERIC_NOT_AVAILABLE )
      {
        return Evado.Model.EvStatics.CONST_NUMERIC_NULL.ToString ( );
      }

      return TextValue;

    }//END convertNulToNegInfl method

    // =====================================================================================
    /// <summary>
    ///   This method decodes a form field numeric value for display.
    /// </summary>
    /// <param name="TextValue">string: (Mandatory) Float represented as a string.</param>
    /// <returns>Boolean: true, if TextValue contains float value.</returns>
    /// <remarks>
    /// This method consists of the following steps:
    /// 
    /// 1. Create a float value and upper boundary
    /// 
    /// 2. Validate whether the TextValue contains float value
    /// and is smaller than upper boundary
    /// 
    /// 3. Return true, if the validation succeed
    /// </remarks>
    // -------------------------------------------------------------------------------------
    public static bool hasNumericNul ( string TextValue )
    {
      //
      // Create a float value and upper boundary
      //
      float floatValue = 0;
      float upperBoundary = Evado.Model.EvStatics.CONST_NUMERIC_NULL;

      //
      // Validate whether the TextValue has float value and smaller than upper boundary
      //
      if ( float.TryParse ( TextValue, out floatValue ) == true )
      {
        if ( floatValue == upperBoundary )
        {
          return true;
        }
      }
      return false;

    }//END hasNumericNul method

    // =====================================================================================
    /// <summary>
    ///   This method test so see if this TextValue is a double point number
    /// </summary>
    /// <param name="TextValue">string: (Mandatory) Float represented as a string.</param>
    /// <returns>Boolean: true = is a number.</returns>
    // -------------------------------------------------------------------------------------
    public static bool isNumber ( string TextValue )
    {
      //
      // Create a float value and upper boundary
      //
      double floatValue = 0;

      //
      // Validate whether the TextValue has float value and smaller than upper boundary
      //
      if ( double.TryParse ( TextValue, out floatValue ) == true )
      {
        return true;
      }
      return false;

    }//END hasNumericNul method


    // =====================================================================================
    /// <summary>
    ///   This method test whether the site has test site identifeir.
    /// </summary>
    /// <param name="OrgId">string: (Mandatory) organisation identifier.</param>
    /// <returns>Boolean: true = is a number.</returns>
    // -------------------------------------------------------------------------------------
    public static bool isTestSite ( string OrgId )
    {
      //
      // Create a float value and upper boundary
      //
      String orgId = OrgId.ToLower ( );

      //
      // Validate whether the TextValue has float value and smaller than upper boundary
      //
      if ( orgId.Contains( "test" ) == true 
        || orgId.Contains ( "tts" ) == true )
      {
        return true;
      }
      return false;

    }//END isTestSite method

    // =====================================================================================
    /// <summary>
    ///   This method decodes a form field numeric value for display.
    /// </summary>
    /// <param name="FloatValue">string: (Mandatory) Float represented as a string.</param>
    /// <returns>string: a decode field numeric</returns>
    /// <remarks>
    /// This method consists of the following steps:
    /// 
    /// 1. Create a floatvalue and upperboundary
    /// 
    /// 2. Validate whether Floatvalue string is float
    /// 
    /// 3. If FloatValue is smaller than upperBoundary, return "NA"
    /// 
    /// 4. If not return a FloatValue string
    /// </remarks>
    // -------------------------------------------------------------------------------------
    public static String decodeFieldNumeric ( string FloatValue )
    {
      //
      // Create a floatvalue and upperboundary
      //
      float floatValue = 0;
      float upperBoundary = Evado.Model.EvStatics.CONST_NUMERIC_NULL;

      //
      // Validate whether FloatValue string is float
      //
      if ( float.TryParse ( FloatValue, out floatValue ) == true )
      {
        //
        // Return "NA", if float value is smaller than upperBoundary
        //
        if ( floatValue < upperBoundary )
        {
          return "NA";
        }
        return floatValue.ToString ( );
      }
      return String.Empty;

    }//END decodeFieldNumeric method

    // =====================================================================================
    /// <summary>
    ///   This property returns an empty CVS value.
    /// </summary>
    // -------------------------------------------------------------------------------------
    public static String encodeCsvEmpty
    {
      get
      {
        return ",\"\"";
      }
    }

    // =====================================================================================
    /// <summary>
    ///   This method formats a text field for export    
    /// </summary>
    /// <param name="TextValue">string: (Mandatory) Text String.</param>
    /// <returns>String: string export value.</returns>
    /// <remarks>
    /// This method consists of the following steps:
    /// 
    /// 1. Create a return output string and a float value
    /// 
    /// 2. Validate whether the TextValue is float
    /// 
    /// 3. Format the TextValue into an encode Text
    /// </remarks>
    // -------------------------------------------------------------------------------------
    public static String encodeCsvFirstColumn ( string TextValue )
    {
      //
      // Create a return output string and a float value
      //
      string sOutput = String.Empty;
      float fValue = 0.0F;

      //
      // Validate whether the TextValue contains float value
      //
      if ( float.TryParse ( TextValue, out fValue ) == true )
      {
        return TextValue;
      }

      //
      // Format the TextValue
      //
      TextValue = TextValue.Replace ( "\r", String.Empty );
      TextValue = TextValue.Replace ( "\n", " " );
      TextValue = TextValue.Replace ( ",", " " );
      TextValue = TextValue.Replace ( "  ", " " );

      return "\"" + TextValue + "\"";

    }//END encodeTextExport method

    // =====================================================================================
    /// <summary>
    ///   This method formats a text field for export    
    /// </summary>
    /// <param name="TextValue">string: (Mandatory) Text String.</param>
    /// <returns>String: string export value.</returns>
    /// <remarks>
    /// This method consists of the following steps:
    /// 
    /// 1. Create a return output string and a float value
    /// 
    /// 2. Validate whether the TextValue is float
    /// 
    /// 3. Format the TextValue into an encode Text
    /// </remarks>
    // -------------------------------------------------------------------------------------
    public static String encodeCsvText ( string TextValue )
    {
      //
      // Create a return output string and a float value
      //
      string sOutput = String.Empty;
      float fValue = 0.0F;

      //
      // Validate whether the TextValue contains float value
      //
      if ( float.TryParse ( TextValue, out fValue ) == true )
      {
        return "," + TextValue;
      }

      //
      // Format the TextValue
      //
      TextValue = TextValue.Replace ( "\r", String.Empty );
      TextValue = TextValue.Replace ( "\n", " " );
      TextValue = TextValue.Replace ( ",", " " );
      TextValue = TextValue.Replace ( "  ", " " );

      return ",\"" + TextValue + "\"";

    }//END encodeTextExport method

    // =====================================================================================
    /// <summary>
    ///   This method encodes a form field numeric value for database storage.
    /// </summary>
    /// <param name="FieldValue">string: (Mandatory) Float represented as a string.</param>
    /// <returns>String: encoded value string.</returns>
    /// <remarks>
    /// This method consists of the following steps:
    /// 
    /// 1. Create a float Value
    /// 
    /// 2. Encode NA value
    /// 
    /// 3. Validate float value and export
    /// </remarks>
    // -------------------------------------------------------------------------------------
    public static String encodeCsvNumeric ( String FieldValue )
    {
      float floatValue = 0;

      //
      // encode NA value.
      //
      if ( FieldValue.ToLower ( ) == "na" )
      {
        return "," + Evado.Model.EvStatics.CONST_NUMERIC_NULL.ToString ( );
      }

      //
      // Validate numeric and export.
      //
      if ( float.TryParse ( FieldValue, out floatValue ) == true )
      {
        return "," + floatValue.ToString ( );
      }

      return "," + Evado.Model.EvStatics.CONST_NUMERIC_ERROR.ToString ( );

    }//END encodeFieldNumeric method

    // =====================================================================================
    /// <summary>
    ///   This method formats a Yes Not (bool) field for export
    /// </summary>
    /// <param name="YesNotValue">string: (Mandatory) Yes represented as a string 'Yes' or 'No'.</param>
    /// <returns>String: string export value.</returns>
    /// <remarks>
    /// This method consists of the following steps:
    /// 
    /// 1. Create a return output string
    /// 
    /// 2. If yes, return ",1"
    ///
    /// 3. If no, return ",0"
    ///
    /// 4. If not yes and no, return ","
    /// </remarks>
    // -------------------------------------------------------------------------------------
    public static String encodeCsvBoolean ( string YesNotValue )
    {
      //
      // Create a return output string
      //
      string sOutput = String.Empty;

      //
      // If yes, return ",1"
      // If no, return ",0"
      // If not yes and no, return ","
      //
      if ( YesNotValue.ToLower ( ) == "yes"
        || YesNotValue.ToLower ( ) == "y"
        || YesNotValue.ToLower ( ) == "t"
        || YesNotValue.ToLower ( ) == "true"
        || YesNotValue.ToLower ( ) == "1" )
      {
        sOutput += ",1";
      }
      else
      {
        if ( YesNotValue.ToLower ( ) == "no"
        || YesNotValue.ToLower ( ) == "n"
        || YesNotValue.ToLower ( ) == "f"
        || YesNotValue.ToLower ( ) == "false"
        || YesNotValue.ToLower ( ) == "0" )
        {
          sOutput += ",0";
        }
        else
        {
          sOutput += ",";
        }
      }

      return sOutput;

    }//END formatYesNoExport method

    ///=====================================================================================
    /// <summary>
    ///  This calls strips the illegal directory characters. e.g. ':', '.'
    /// 
    /// </summary>
    /// <param name="InPath">string: The array of signoffs</param>
    /// <returns>string: a string of legal directory characters</returns>
    /// <remarks>
    /// This method consists of the following step:
    /// 
    /// 1. Remove all the illegal characters
    /// </remarks>
    //-------------------------------------------------------------------------------------
    public static string StripIllegalDirChars ( string InPath )
    {
      // 
      // remove the illegal characters.
      // 
      string outPath = InPath.Replace ( ":", String.Empty );
      outPath = outPath.Replace ( ".", String.Empty );
      outPath = outPath.Replace ( ",", String.Empty );
      outPath = outPath.Replace ( ":", String.Empty );
      outPath = outPath.Replace ( "'", String.Empty );
      outPath = outPath.Replace ( "-", String.Empty );
      outPath = outPath.Replace ( ";", String.Empty );
      outPath = outPath.Replace ( "=", String.Empty );
      outPath = outPath.Replace ( "*", String.Empty );
      outPath = outPath.Replace ( "^", String.Empty );
      outPath = outPath.Replace ( "%", String.Empty );
      outPath = outPath.Replace ( "$", String.Empty );
      outPath = outPath.Replace ( "#", String.Empty );
      outPath = outPath.Replace ( "@", String.Empty );
      outPath = outPath.Replace ( "!", String.Empty );
      outPath = outPath.Replace ( "~", String.Empty );
      outPath = outPath.Replace ( "?", String.Empty );
      outPath = outPath.Replace ( "|", String.Empty );
      outPath = outPath.Replace ( ">", String.Empty );
      outPath = outPath.Replace ( "<", String.Empty );
      outPath = outPath.Replace ( " ", "_" );
      outPath = outPath.Replace ( "__", "_" );

      return outPath.Trim ( );
    }//END StripIllegalDirChars method

    ///=====================================================================================
    /// <summary>
    ///  Strips the illegal directory characters. e.g. ':', '.'    
    /// </summary>
    /// <param name="InPath">string: The array of signoffs</param>
    /// <returns>string: a title with legal characters</returns>
    /// <remarks>
    /// This method consists of the following steps:
    /// 
    /// 1. Remove all the illegal characters from the title
    /// </remarks>
    //-------------------------------------------------------------------------------------
    public static string StripTitleIllegalChars ( string InPath )
    {
      // 
      // remove the illegal character.
      // 
      string outPath = InPath.Replace ( ":", String.Empty );
      outPath = outPath.Replace ( "*", String.Empty );
      outPath = outPath.Replace ( "^", String.Empty );
      outPath = outPath.Replace ( ":", String.Empty );
      outPath = outPath.Replace ( "'", String.Empty );
      outPath = outPath.Replace ( "*", String.Empty );
      outPath = outPath.Replace ( "^", String.Empty );
      outPath = outPath.Replace ( "&", String.Empty );
      outPath = outPath.Replace ( "%", String.Empty );
      outPath = outPath.Replace ( "$", String.Empty );
      outPath = outPath.Replace ( "#", String.Empty );
      outPath = outPath.Replace ( "@", String.Empty );
      outPath = outPath.Replace ( "!", String.Empty );
      outPath = outPath.Replace ( "~", String.Empty );
      outPath = outPath.Replace ( "?", String.Empty );
      outPath = outPath.Replace ( "|", String.Empty );
      outPath = outPath.Replace ( "/", String.Empty );
      outPath = outPath.Replace ( @"\", String.Empty );
      outPath = outPath.Replace ( ">", String.Empty );
      outPath = outPath.Replace ( "<", String.Empty );
      outPath = outPath.Replace ( "  ", " " );
      outPath = outPath.Replace ( "  ", " " );

      return outPath;
    }//END StripTitleIllegalChars method


    //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region Static array tranformation methods.

    // =====================================================================================
    /// <summary>
    /// This class gets a ArrayList contain the ArrayList of selections of _Trials.    
    /// </summary>
    /// <param name="ObjectList">ArrayList: AN arraylist of objects.</param>
    /// <returns>Array: an array of objects.</returns>
    /// <remarks>
    /// This method consists of the following steps:
    /// 
    /// 1. Initialize the array option and option value
    /// 
    /// 2. if list is not empty, loop through the list 
    /// and add the list items to the array option
    /// </remarks>
    // -------------------------------------------------------------------------------------
    public static List<T> getArrayAsList<T> ( T [ ] ObjectList )
    {
      // 
      // Initialise the methods objects and variables.
      // 
      List<T> options = new List<T> ( );

      // 
      // Check that the list is not empty.
      // 
      if ( ObjectList.Length > 0 )
      {
        // 
        // Pass the values into the array.
        // 
        for ( int count = 0; count < ObjectList.Length; count++ )
        {
          options.Add ( ObjectList [ count ] );
        }

      }//END List not empty.

      // 
      // Return the array option.
      // 
      return options;

    }//END getListAsArray method

    // =====================================================================================
    /// <summary>
    /// This class gets a ArrayList contain the ArrayList of selections of _Trials.    
    /// </summary>
    /// <param name="List">ArrayList: AN arraylist of objects.</param>
    /// <returns>Array: an array of objects.</returns>
    /// <remarks>
    /// This method consists of the following steps:
    /// 
    /// 1. Initialize the array option and option value
    /// 
    /// 2. if list is not empty, loop through the list 
    /// and add the list items to the array option
    /// </remarks>
    // -------------------------------------------------------------------------------------
    public static T [ ] getListAsArray<T> ( List<T> List )
    {
      // 
      // Initialise the methods objects and variables.
      // 
      T [ ] options = new T [ 1 ];
      options [ 0 ] = default ( T );

      // 
      // Check that the list is not empty.
      // 
      if ( List.Count > 0 )
      {
        // 
        // Demension the array
        // 
        options = new T [ List.Count ];

        // 
        // Pass the values into the array.
        // 
        for ( int count = 0; count < List.Count; count++ )
        {
          options [ count ] = ( T ) List [ count ];
        }

      }//END List not empty.

      // 
      // Return the array option.
      // 
      return options;

    }//END getListAsArray method

    // =====================================================================================
    /// <summary>
    /// This class gets a List string and converts them to a delimited ';' string. 
    /// </summary>
    /// <param name="List">List of String.</param>
    /// <returns>String objects.</returns>
    // -------------------------------------------------------------------------------------
    public static String getStringListAsString ( List<String> List )
    {
      // 
      // Initialise the methods objects and variables.
      // 
      String outString = String.Empty;

      // 
      // Check that the list is not empty.
      // 
      if ( List.Count > 0 )
      {
        // 
        // Pass the values into the array.
        // 
        for ( int count = 0; count < List.Count; count++ )
        {
          if ( outString != String.Empty )
          {
            outString = outString + ";";
          }
          outString = outString + List [ count ];
        }

      }//END List not empty.

      // 
      // Return the string.
      // 
      return outString;

    }//END getStringListAsString method

    // =====================================================================================
    /// <summary>
    ///  This method adds a value to a delimited list as a string
    /// </summary>
    ///  <param name="DelimitedList">Delimited ';' list of values
    ///  <param name="Value">Delimited ';' list of value
    // -------------------------------------------------------------------------------------
    public static String addDelimitedListValue (
      string DelimitedList,
      String Value )
    {
      if ( DelimitedList != String.Empty )
      {
        DelimitedList += ";";
      }

      DelimitedList += Value;

      return DelimitedList;

    }//END addValueToStringList method

    // =====================================================================================
    /// <summary>
    ///  This method adds a value to a delimited list as a string
    /// </summary>
    ///  <param name="DelimitedList">Delimited ';' list of values
    ///  <param name="Value">Delimited ';' list of value
    // -------------------------------------------------------------------------------------
    public static String deleteDelimitedListValue (
      string DelimitedList,
      String StringValue )
    {
      DelimitedList.Replace( StringValue, String.Empty );
      DelimitedList.Replace ( ";;", ";" );

      return DelimitedList;

    }//END addValueToStringList method

    // =====================================================================================
    /// <summary>
    ///  This method returns an array of strings as a string.
    ///  <param name="Strings">string array: a string array</param>
    ///  <returns>string: a string of array</returns>
    ///  <remarks>
    /// This method consists of the following step:
    /// 
    /// 1. Call getArrayAsString method to convert array to string
    /// </remarks>
    /// </summary>
    // -------------------------------------------------------------------------------------
    public static string getArrayAsString (
      string [ ] Strings )
    {
      return getArrayAsString ( Strings, ";" );
    }//END getArrayAsString method

    // =====================================================================================
    /// <summary>
    ///  This method returns an array of strings as a string.
    ///  <param name="Strings">string array: a string array</param>
    ///  <param name="Separator">string: a separator string </param>
    ///  <returns>string; a string from an array</returns>
    ///  <remarks>
    /// This method consists of the following steps:
    /// 
    /// 1. Initialize the element string
    /// 
    /// 2. Validate the Strings array is not empty
    /// 
    /// 3. Loop through the Strings array
    /// 
    /// 4. Pass the value from Strings array to the element string separated 
    /// by a separater
    /// </remarks>
    /// </summary>
    // -------------------------------------------------------------------------------------
    public static string getArrayAsString (
      string [ ] Strings,
      string Separator )
    {
      // 
      // Initialise the string element
      // 
      string arElements = String.Empty;

      // 
      // Check that the list is not empty.
      // 
      if ( Strings.Length > 0 )
      {
        // 
        // Pass the values into the array.
        // 
        for ( int count = 0; count < Strings.Length; count++ )
        {
          if ( arElements != String.Empty )
          {
            arElements += Separator;
          }
          arElements += Strings [ count ].Trim ( );
        }

      }//END List not empty.

      // 
      // Return the list.
      // 
      return arElements;

    }//END getListAsString method

    // =====================================================================================
    /// <summary>
    ///  This method returns an array of strings as a string where the separator is ';'
    ///  <param name="Strings">string: a strings</param>
    ///  <returns>string array: a string converted from an array</returns>
    /// </summary>
    // -------------------------------------------------------------------------------------
    public static string [ ] getStringAsArray (
      string Strings )
    {
      return getStringAsArray ( Strings, CONST_CHAR_SEPARATOR );
    }//END getStringAsArray method

    // =====================================================================================
    /// <summary>
    ///  This method returns an array of strings as a string.
    ///  <param name="Strings">string: a strings</param>
    ///  <param name="separator">char: a separator character</param>
    ///  <returns>string array: a string converted from a string array</returns>
    ///  <remarks>
    /// This method consists of the following step:
    /// 
    /// </remarks>
    /// </summary>
    // -------------------------------------------------------------------------------------
    public static string [ ] getStringAsArray (
      string Strings, char separator )
    {
      if ( Strings == String.Empty )
      {
        return null;
      }

      // 
      // Return the list.
      // 
      return Strings.Split ( separator );

    }//END getListAsString method

    // =====================================================================================
    /// <summary>
    ///  This method returns an string of values for a list of options.
    /// </summary>
    /// <param name="OptionList">List: list of options objects.</param>
    /// <param name="byValue">Boolean: True return the option values.</param>
    /// <returns>String: of encoded values (';' as the separator)</returns>
    /// <remarks>
    /// This method consists of the following steps:
    /// 
    /// 1. Create a value string
    /// 
    /// 2. Loop through the OptionList list
    /// 
    /// 3. If byValue is true, add option list value to the value string
    /// 
    /// 4. If not, add option list description to the value string
    /// </remarks>
    // -------------------------------------------------------------------------------------
    public static string getOptionListAsString (
      List<Evado.Model.EvOption> OptionList,
      bool byValue )
    {
      //
      // Create a stValue string
      //
      String stValue = String.Empty;

      //
      // Iterate through the list creating the string of values.
      //
      foreach ( Evado.Model.EvOption option in OptionList )
      {
        if ( stValue != String.Empty )
        {
          stValue += ";";
        }

        //
        // If the byValue is true then add option value to the stValue string.
        // If not, add option description to the stValue string
        //
        if ( byValue == true )
        {
          stValue += option.Value;
        }
        else
        {
          stValue += option.Description;
        }
      }

      //
      // Return the values.
      //
      return stValue;
    }

    // =====================================================================================
    /// <summary>
    ///  This method sorts the option list.
    /// </summary>
    /// <param name="OptionList">List: list of options.</param>
    // -------------------------------------------------------------------------------------
    public static void sortOptionListValues (
      List<Evado.Model.EvOption> OptionList )
    {
      OptionComparer comparer = new OptionComparer ( );
      //
      // Sort using the default comparitor.
      //
      OptionList.Sort ( comparer );
    }

    // =====================================================================================
    /// <summary>
    ///  This method sorts the option list.
    /// </summary>
    /// <param name="OptionList">List: list of options.</param>
    // -------------------------------------------------------------------------------------
    public static void sortOptionListDescriptions (
      List<Evado.Model.EvOption> OptionList )
    {
      OptionDescComparer comparer = new OptionDescComparer ( );
      //
      // Sort using the default comparitor.
      //
      OptionList.Sort ( comparer );
    }

    // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region Static formatting methods.

    // =====================================================================================
    /// <summary>
    ///  This method returns an a string formatted for a filename without the extension.
    /// <param name="Text">string: a text string</param>
    /// <returns>string: a filename</returns>
    /// <remarks>
    /// This method consists of the following step:
    /// 
    /// 1. Remove the illegal characters from the text
    /// </remarks>
    /// </summary>
    // -------------------------------------------------------------------------------------
    public static string formatFilename ( string Text )
    {
      // 
      // Remove the illegal characters.
      //
      Text = Text.Replace ( " ", "_" );
      Text = Text.Replace ( ":", "" );
      Text = Text.Replace ( ";", "" );
      Text = Text.Replace ( ",", "" );
      Text = Text.Replace ( ".", "" );
      Text = Text.Replace ( "!", "" );
      Text = Text.Replace ( "@", "" );
      Text = Text.Replace ( "#", "" );
      Text = Text.Replace ( "$", "" );
      Text = Text.Replace ( "%", "" );
      Text = Text.Replace ( "^", "" );
      Text = Text.Replace ( "&", "" );
      Text = Text.Replace ( "*", "" );
      Text = Text.Replace ( "-", "" );

      // 
      // Return the text.
      // 
      return Text;

    }//END formatFilename method

    // =====================================================================================
    /// <summary>
    ///  This method returns an a string formatted for an Evado identifier.
    ///  <param name="Text">string: text</param>
    ///  <returns>string: a legal Evado Identifier string</returns>
    ///  <remarks>
    /// This method consists of the following steps: 
    /// 
    /// 1. Remove the illegal characters from the Text
    /// </remarks>
    /// </summary>
    // -------------------------------------------------------------------------------------
    public static string formatEvadoIdentifier ( string Text )
    {
      // 
      // Remove the illegal characters.
      //
      Text = Text.Replace ( " ", String.Empty );
      Text = Text.Replace ( "-", String.Empty );
      Text = Text.Replace ( ",", String.Empty );
      Text = Text.Replace ( ".", String.Empty );
      Text = Text.Replace ( "=", String.Empty );
      Text = Text.Replace ( "`", String.Empty );
      Text = Text.Replace ( "!", String.Empty );
      Text = Text.Replace ( "@", String.Empty );
      Text = Text.Replace ( "#", String.Empty );
      Text = Text.Replace ( "$", String.Empty );
      Text = Text.Replace ( "%", String.Empty );
      Text = Text.Replace ( "^", String.Empty );
      Text = Text.Replace ( "&", String.Empty );
      Text = Text.Replace ( "*", String.Empty );
      Text = Text.Replace ( "+", String.Empty );
      Text = Text.Replace ( "(", String.Empty );
      Text = Text.Replace ( ")", String.Empty );
      Text = Text.Replace ( "{", String.Empty );
      Text = Text.Replace ( "}", String.Empty );
      Text = Text.Replace ( "[", String.Empty );
      Text = Text.Replace ( "]", String.Empty );
      Text = Text.Replace ( ":", String.Empty );
      Text = Text.Replace ( "/", String.Empty );
      Text = Text.Replace ( @"\", String.Empty );

      // 
      // Return the text.
      // 
      return Text.Trim ( );

    }//END formatEvadoIdentifier method

    //==================================================================================
    /// <summary>
    /// This method trips the leading and following spaces from mark down text.
    /// </summary>
    /// <param name="Value">String value</param>
    /// <returns>trimmed text.</returns>
    //----------------------------------------------------------------------------------
    public static String trimMarkDown ( String Value )
    {
      string value = String.Empty;

      Value = Value.Replace ( "\r", "^" );
      Value = Value.Replace ( "\n", "^" );
      Value = Value.Replace ( "^^", "^" );


      String [ ] arLines = Value.Split ( '^' );

      for ( int i = 0; i < arLines.Length; i++ )
      {
        if ( value != String.Empty )
        {
          value += " \r\n";
        }
        value += arLines [ i ].TrimStart ( );
      }

      return value;
    }

    // ===================================================================================
    /// <summary>
    /// This method check to see if the text value is a numeric if not then encaptulate the 
    /// field value in quotation marks.
    /// 
    /// </summary>
    /// <param name="Text">String: String value to be processed.</param>
    /// <returns>String: Formated text value</returns>
    /// <remarks>
    /// This method consists of the following steps:
    /// 
    /// 1. Create a float numeric
    /// 
    /// 2. Validate the float value on the text
    /// 
    /// 3. Convert the Text into Csv format
    /// </remarks>
    // -------------------------------------------------------------------------------------
    private String formatCsvTextData ( String Text )
    {
      //
      // Create a float numeric 
      //
      float fltNumeric = 0.0F;

      //
      // Validate whether the Text is float
      // and format the Text to a Csv style
      //
      if ( float.TryParse ( Text, out fltNumeric ) == false )
      {
        Text = "\"" + Text + "\"";
      }

      return Text;
    }

    //  ===================================================================================
    /// <summary>
    /// This method formats a string double and returns another string with the specified pattern.
    /// </summary>
    /// <param name="stDouble">String: Double string</param>
    /// <param name="pattern">String: pattern</param>
    /// <returns>String: a double string</returns>
    /// Andres Castano 19 Nov 2009
    /// <remarks>
    /// This method consists of the following steps:
    /// 
    /// 1. Try convert stDouble string into double type
    /// 
    /// 2. return a double string based on the 'pattern' format
    /// </remarks>
    //  -----------------------------------------------------------------------------------
    public static String formatDoubleString ( String stDouble, String pattern )
    {
      try
      {
        //
        // Convert stDouble string into double type
        //
        double dVal = double.Parse ( stDouble );

        //
        // Return dVal into string based on pattern format
        //
        return dVal.ToString ( pattern );
      }
      catch ( Exception )
      {
        return stDouble;
      }

    }//END formatDoubleString method

    //  ===================================================================================
    /// <summary>
    /// This class formats a string date and returns another string with the specified pattern.
    /// The original string date should be in the format dd MMM yyyy HH:mm:ss which is the 
    /// original format in the data base.
    /// </summary>
    /// <param name="stDate">String: Date string</param>
    /// <param name="pattern">String: formating patten</param>
    /// <returns>String: a date string</returns>
    /// Andres Castano 19 Nov 2009
    /// <remarks>
    /// This method consists of the following steps: 
    /// 
    /// 1. Validate whether the stDate has value
    /// 
    /// 2. Convert the stDate string into DateTime format
    /// 
    /// 3. Return the convert value under a pattern string
    /// </remarks>
    //  -----------------------------------------------------------------------------------
    public static String formatDateString ( String stDate, String pattern )
    {
      if ( stDate == String.Empty )
      {
        return String.Empty;
      }
      DateTime dVal = DateTime.Parse ( stDate );
      return dVal.ToString ( pattern );

    }//END formatDateString method

    // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region Static Comparision methods methods.

    //  ==================================================================================	
    /// <summary>
    ///  This method removes the domain name from a use id.
    /// </summary>
    /// <param name="DomainUserId">String: a domain user identifier</param>
    /// <returns>Strign: A string containing the domain user identifier.</returns>
    /// <remarks>
    /// This method consists of the following steps:
    /// 
    /// 1. Create an array of domain name for the activity.
    /// 
    /// 2. Check to see if the user is an administrator, returns Administrator.
    /// 
    /// 3. If there is no domain exists, return the domain user identifier
    /// </remarks>
    //  ---------------------------------------------------------------------------------
    public static bool CompareDelimtedStrings ( String FullDelimitedString, String SumDelimitedString )
    {
      // 
      // initialise the methods variables and objects.
      // 
      string [ ] arrFullDelimitedString = FullDelimitedString.Split ( ';' );
      string [ ] arrSubDelimitedString = SumDelimitedString.Split ( ';' );

      //
      // Iterate through the full delimited array.
      //
      for ( int i = 0; i < arrFullDelimitedString.Length; i++ )
      {
        var str1 = arrFullDelimitedString [ i ].Trim ( );

        //
        // Iterate through the sub delimited array.
        //
        for ( int j = 0; j < arrSubDelimitedString.Length; j++ )
        {
          var str2 = arrSubDelimitedString [ j ].Trim ( );

          //
          // return true on the first match.
          //
          if ( str1.ToLower ( ) == str2.ToLower ( ) )
          {
            return true;
          }
        }
      }

      //
      // return false result
      //
      return false;
    }

    // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region Static DomainName methods methods.

    //  ==================================================================================	
    /// <summary>
    ///  This method removes the domain name from a use id.
    /// </summary>
    /// <param name="DomainUserId">String: a domain user identifier</param>
    /// <returns>Strign: A string containing the domain user identifier.</returns>
    /// <remarks>
    /// This method consists of the following steps:
    /// 
    /// 1. Create an array of domain name for the activity.
    /// 
    /// 2. Check to see if the user is an administrator, returns Administrator.
    /// 
    /// 3. If there is no domain exists, return the domain user identifier
    /// </remarks>
    //  ---------------------------------------------------------------------------------
    public static String removeDomainName ( string DomainUserId )
    {
      // 
      // Create an array of domain name for the activity.
      // 
      string [ ] arrUserId = DomainUserId.Split ( '\\' );

      // 
      // Check to see if the user is an administrator, returns Administrator.
      // 
      if ( arrUserId.Length > 1 )
      {
        return arrUserId [ 1 ];
      }

      //
      // If there is no domain exists, return the domain user identifier
      //
      return DomainUserId;
    }

    //====================================================================================
    /// <summary>
    /// This method cleans a SamUserId of invalid characters.
    /// </summary>
    /// <param name="strIn">String imput string</param>
    /// <returns>String: cleaned text</returns>
    //------------------------------------------------------------------------------------
    public static string CleanSamUserId ( string strIn )
    {
      // Replace invalid characters with empty strings.
      try
      {
        strIn = strIn.Replace ( "_", "" );
        strIn = removeDomainName ( strIn );

        return Regex.Replace ( strIn, @"[^\w\.]", "", RegexOptions.None );
      }
      // If we timeout when replacing invalid characters, 
      // we should return Empty.
      catch
      {
        return String.Empty;
      }
    }
    // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region Password methods.

    //====================================================================================
    /// <summary>
    /// This method returns a password string.
    /// </summary>
    /// <param name="int">length of password (min 8, max 16)</param>
    /// <returns>String: password string</returns>
    //------------------------------------------------------------------------------------
    public static string CreatePassword ( int Length )
    {
      int length = 8;
      if ( Length > length && Length <= 16 )
      {
        length = Length;
      }
      Guid guid = Guid.NewGuid ( );
      String password = Convert.ToBase64String ( guid.ToByteArray ( ) );

      Random rand = new Random ( );
      int integer = rand.Next ( 9 );

      return password.Substring ( 3, length ) + integer;

    }
    // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region MarkDown formatting.

    // ==================================================================================
    /// <summary>
    /// This method generates HTML markup from markdown text markup.
    /// </summary>
    /// <param name="MarkDownText">String containing MarkDown markup</param>
    /// <returns>Html string</returns>
    // ---------------------------------------------------------------------------------
    public static String EncodeMarkDown (
      String MarkDownText )
    {
      //
      // Initialise the methods variables and objects.
      //
      String stMarkDown = String.Empty;

      //
      // Process the text to remove spaces.
      //
      MarkDownText = MarkDownText.Replace ( "\r\n", "~" );
      MarkDownText = MarkDownText.Replace ( "\r", "~" );
      MarkDownText = MarkDownText.Replace ( "\n", "~" );
      MarkDownText = MarkDownText.Replace ( "~~", "~" );
      MarkDownText = MarkDownText.Replace ( "[b]", "__" );
      MarkDownText = MarkDownText.Replace ( "[/b]", "__" );
      MarkDownText = MarkDownText.Replace ( "[i]", "_" );
      MarkDownText = MarkDownText.Replace ( "[/i]", "_" );

      string [ ] arrMarkDownText = MarkDownText.Split ( '~' );
      foreach ( String str in arrMarkDownText )
      {
        stMarkDown += str.Trim ( ) + "\r\n";
      }

      //
      // Initialise the markdown options object.
      //
      MarkdownSharp.MarkdownOptions markDownOptions = new MarkdownSharp.MarkdownOptions ( );
      markDownOptions.AutoHyperlink = true;
      markDownOptions.AutoNewlines = true;
      markDownOptions.EmptyElementSuffix = "/>";
      markDownOptions.EncodeProblemUrlCharacters = true;
      markDownOptions.LinkEmails = true;
      markDownOptions.StrictBoldItalic = true;

      //
      // Initialise the markdown object.
      //
      MarkdownSharp.Markdown markDown = new MarkdownSharp.Markdown ( markDownOptions );

      string stHtml = markDown.Transform ( stMarkDown );

      //
      // convert old markup to html.
      //
      stHtml = stHtml.Replace ( "<blockquote>", "" );
      stHtml = stHtml.Replace ( "</blockquote>", "" );

      return stHtml;

    }//END EncodeMarkDown method.

    #endregion

    #region Static SQL conversion methods.

    // =====================================================================================
    /// <summary>
    ///  This method converts a boolean to a SQL bit value.
    ///  True: '1', False: '0'
    /// 
    /// </summary>
    /// <param name="BooleanValue">Boolean: Boolean value to be converted</param>
    /// <returns>integer: a bit value.</returns>
    /// <remarks>
    /// This method consists of the following steps:
    /// 
    /// 1. Create a sql bit value
    /// 
    /// 2. Return 1 if the BooleanValue is true
    /// 
    /// 3. Return 0 if not
    /// </remarks>
    // -------------------------------------------------------------------------------------
    public static int getSqlBitValue ( bool BooleanValue )
    {
      //
      // Create a sql bit value
      //
      int stSqlBit = 0;

      //
      // Return 1 if BooleanValue is true
      // and 0 if not. 
      //
      if ( BooleanValue == true )
      {
        stSqlBit = 1;
      }
      return stSqlBit;

    }//END getSqlBitValue method

    // =====================================================================================
    /// <summary>
    ///  This method converts a string to a SQL bit value.
    /// 
    /// </summary>
    /// <param name="BooleanValue">String: Boolean value to be converted</param>
    /// <returns>integer: a bit value.</returns>
    /// <remarks>
    /// This method consists of the following steps:
    /// 
    /// 1. Create a sql bit value
    /// 
    /// 2. Return 1 if BooleanValue is true
    /// 
    /// 3. Return 0 if not
    /// 
    /// </remarks>
    // -------------------------------------------------------------------------------------
    public static int getSqlBitValue ( string BooleanValue )
    {
      //
      // Create a sql bit value
      //
      int stSqlBit = 0;

      //
      // Return 1, if BooleanValue is true
      // and 0, if not
      //
      if ( BooleanValue.ToLower ( ) == "true"
        || BooleanValue.ToLower ( ) == "yes"
        || BooleanValue.ToLower ( ) == "1" )
      {
        stSqlBit = 1;
      }
      return stSqlBit;

    }//END getSqlBitValue method


    // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region Concatination methods

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

      return RelativeUrl.ToLower ( );
    }
    #endregion

    #region Static Event message methods.

    // =====================================================================================
    /// <summary>
    ///  This method returns an exception string 
    /// </summary>
    /// <param name="EventCode">EvEventCodes enumerated list class</param>
    /// <returns>string: an event message</returns>
    // -------------------------------------------------------------------------------------
    public static string getEventMessage ( Evado.Model.EvEventCodes EventCode )
    {
      //
      // Create an exception string
      //
      string stEventMessage = EventCode.ToString ( );

      stEventMessage = stEventMessage.Replace ( "Object_", String.Empty );
      stEventMessage = stEventMessage.Replace ( "Identifier_", String.Empty );

      if ( EventCode == Evado.Model.EvEventCodes.Data_General_Data_Error )
      {
        stEventMessage = stEventMessage.Replace ( "Data_General_Data_Error", "General_Data_Error" );
      }
      else
      {
        stEventMessage = stEventMessage.Replace ( "Data_", String.Empty );
      }
      stEventMessage = stEventMessage.Replace ( "Business_Logic_", String.Empty );
      stEventMessage = stEventMessage.Replace ( "Page_Loading_", String.Empty );
      stEventMessage = stEventMessage.Replace ( "Page_Filling_", String.Empty );
      stEventMessage = stEventMessage.Replace ( "Page_Saving_", "Saving_" );
      stEventMessage = stEventMessage.Replace ( "_", " " );

      return stEventMessage;

    }//END getEventMessage method

    // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region Age Calculations

    //  =================================================================================
    /// <summary>
    ///  BmiCalculator method
    /// 
    /// Description
    ///  This method calculates the Age based on the Birth date
    /// 
    /// </summary>
    /// <param name="DateOfBirth">The date of birth as a valid date string.</param>
    /// <param name="inMonths">boolen: if age is calculated in months.</param>    
    /// <returns>Age and an integer.</returns>
    //  ---------------------------------------------------------------------------------
    public static int CalculateAge ( string DateOfBirth, bool inMonths )
    {
      DateTime dateOfBirth;

      if ( DateTime.TryParse ( DateOfBirth, out dateOfBirth ) == true )
      {
        return CalculateAge ( dateOfBirth, inMonths );
      }

      return 0;

    }//END Calculate method

    //  =================================================================================
    /// <summary>
    ///  This method calculates the ate based on the birth date.
    /// </summary>
    /// <param name="DateOfBirth">The date of birth as a valid datetime object.</param>
    /// <param name="inMonths">true, if age is in months</param>
    /// <returns>Age and an integer.</returns>
    //  ---------------------------------------------------------------------------------
    public static int CalculateAge ( DateTime DateOfBirth, bool inMonths )
    {
      // 
      // IF date is skip calculation.
      // 
      if ( DateOfBirth == Evado.Model.EvStatics.CONST_DATE_NULL )
      {
        return 0;
      }

      // 
      // Initialise the variables.
      // 
      int iYearDiff = DateTime.Now.Year - DateOfBirth.Year;
      int iMonthDiff = DateTime.Now.Month - DateOfBirth.Month;
      int iMonthDayDiff = DateTime.Now.Day - DateOfBirth.Day;

      if ( inMonths == true )
      {
        iYearDiff = iYearDiff * 12 + iMonthDiff;

        // 
        // stReturn the age.
        // 
        return iYearDiff;
      }
      // 
      // If this year's month is later than the birth month
      // the person has had a birth day to add a year.
      // 
      if ( iMonthDiff <= 0 )
      {
        iYearDiff--;
      }

      // 
      // If the birth month equals currentMonth month and the 
      // currentMonth month day is later than the birth month day 
      // the person has had a birth day so add a year.
      // 
      if ( iMonthDiff == 0
        && iMonthDayDiff >= 0 )
      {
        iYearDiff++;
      }

      if ( iYearDiff < 0 )
      {
        iYearDiff = 0;
      }

      // 
      // stReturn the age.
      // 
      return iYearDiff;

    }//END Calculate method

    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region Time Validiation TestReport

    //  =================================================================================
    /// <summary>
    ///  ValidateTimeStructure method
    /// 
    /// Description
    ///  This method validates a time structure.
    /// 
    /// </summary>
    /// <param name="Value">A time string object.</param>
    /// <returns>string error return</returns>
    //  ---------------------------------------------------------------------------------
    public string ValidateTimeStructure ( string Value )
    {
      // 
      // Initialise the methods variables and objects.
      // 
      int hour = 0;
      int minute = 0;

      // 
      // Does the structure have a ':' 
      // 
      if ( Value.Contains ( ":" ) == false )
      {
        return "This is not a valid Time format (HH:MM).";
      }

      // 
      // TestReport that the string can be split
      //
      string [ ] arrTime = Value.Split ( ':' );
      if ( arrTime.Length < 2 )
      {
        return "This is not a valid Time format (HH:MM).";
      }

      // 
      // TestReport that the hour is a valid integer
      //
      if ( Int32.TryParse ( arrTime [ 0 ], out hour ) == false )
      {
        return "This is not a valid Time format (HH:MM).";
      }

      // 
      // TestReport that the minutes is a valid integer
      // 
      if ( Int32.TryParse ( arrTime [ 1 ], out minute ) == false )
      {
        return "This is not a valid Time format (HH:MM).";
      }

      // 
      // TestReport the hour range.
      // 
      if ( hour < 0 || hour > 24 )
      {
        return "The hour value is not in the range of 0 to 24.";
      }

      // 
      // TestReport the minute range.
      // 
      if ( minute < 0 || minute > 59 )
      {
        return "The minute value is not in the range of 0 to 59.";
      }

      // 
      // If there is not error return an empty string.
      // 
      return String.Empty;
    }

    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region BMI methods

    //  =================================================================================
    /// <summary>
    ///  BmiCalculator method
    /// 
    /// Description
    /// This method calculates the BMI based on one of a number of formulas
    /// 
    /// </summary>
    /// <param name="Height"></param>
    /// <param name="Weight"></param>
    /// <returns></returns>
    //  ---------------------------------------------------------------------------------
    public static float CalculateBmi ( int Height, int Weight )
    {
      return StandardFormula ( Height, Weight );

    }//END BmiCalculator method

    //  =================================================================================
    /// <summary>
    ///  IntegerToFloat method
    /// 
    /// Description
    /// This method calculates converts an integer string to a floating point number
    /// 
    /// </summary>
    /// <param name="Value">Integer value as a string.</param>
    /// <returns>floating piont number</returns>
    //  ---------------------------------------------------------------------------------
    public static float IntegerToFloat ( string Value )
    {
      try
      {
        // 
        // Initialise the method variables and objects
        // 
        int iValue = 0;

        // 
        // Check that there is a value.
        // 
        if ( Int32.TryParse ( Value, out iValue ) == true )
        {
          return iValue;
        }
        // 
        // stReturn the converted value.
        // 
        return 0;
      }
      catch
      {
        return 0;
      }
    }

    //  =================================================================================
    /// <summary>
    /// StandardFormulat method
    /// 
    /// Description:
    ///  This method performs the standard BMI calculation and returns
    ///  a floating point number.
    /// </summary>
    /// <param name="Height"></param>
    /// <param name="Weight"></param>
    /// <returns></returns>
    //  ---------------------------------------------------------------------------------
    private static float StandardFormula ( float Height, float Weight )
    {
      // 
      // Initialis the local variables and objects
      // 
      float fltResult = 0;
      // 
      // Validate the input variables
      // 
      if ( Height > 0 && Weight > 0 )
      {
        Height = Height / 100;
        fltResult = Weight / ( Height * Height );

        if ( fltResult < 0 )
        {
          fltResult = 0;
        }

        return fltResult;
      }
      return 0;
    }

    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region Static Exception methods.

    // =====================================================================================
    /// <summary>
    ///  This method returns an exception string 
    /// </summary>
    /// <param name="Ex">Exception: exception</param>
    /// <returns>string: an exception string</returns>
    /// <remarks>
    /// This method consists of the following steps:
    /// 
    /// 1. Create an exception string
    /// 
    /// 2. Add message description to the exception string
    /// </remarks>
    // -------------------------------------------------------------------------------------
    public static string getException ( Exception Ex )
    {
      //
      // Create an exception string
      //
      string stException = "Exception: ";

      //
      // Add message description to the exception string
      //
      stException += "\r\n Message: " + Ex.Message
        + "\r\n Source: " + Ex.Source
        + "\r\n TargetSite: " + Ex.TargetSite
        + "\r\n Inner Exeception: \r\n" + Ex.InnerException
        + "\r\n StackTrace: \r\n" + Ex.StackTrace;

      return stException;
    }//END getExceptionAsString method

    // =====================================================================================
    /// <summary>
    ///  This method returns an exception string in html format
    /// </summary>
    /// <param name="Ex">Exception: exception</param>
    /// <returns>string: an exception html string</returns>
    /// <remarks>
    /// This method consists of the following steps:
    /// 
    /// 1. Create an exception string
    /// 
    /// 2. Add message decription to the exception string based on the html format
    /// 
    /// </remarks>
    // -------------------------------------------------------------------------------------
    public static string getExceptionAsHtml ( Exception Ex )
    {
      string stException = "Exception: ";

      stException += "\r\n Message: " + Ex.Message
        + "\r\n Source: " + Ex.Source
        + "\r\n TargetSite: " + Ex.TargetSite
        + "\r\n Inner Exeception: \r\n" + Ex.InnerException
        + "\r\n StackTrace: \r\n" + Ex.StackTrace;

      return stException.Replace ( "\r\n", "<br/>" );
    }//END getExceptionAsHtml method

    //  ===========================================================================
    /// <summary>
    /// This class writes log to the event log source
    /// </summary>
    /// <param name="EventLogSource">string: The event log source</param>
    /// <param name="EventContent">string: The event content to be written</param>
    /// <param name="LogEntryType">EventLogEntryType: The event log type</param>
    /// <remarks>
    /// This method consists of the following steps:
    /// 
    /// 1. Validate the EventLogSource for not being null and empty
    /// 
    /// 2. Write out even log but not over 30000 blocks
    /// </remarks>
    //  ---------------------------------------------------------------------------------
    public static void WriteToEventLog ( String EventLogSource,
      String EventContent,
      System.Diagnostics.EventLogEntryType LogEntryType )
    {
      //
      // If the event log source is empty or null exit.
      //
      if ( EventLogSource == null
        || EventLogSource == String.Empty )
      {
        return;
      }

      //
      // output short event logs.
      //
      if ( EventContent.Length < 32000 )
      {
        System.Diagnostics.EventLog.WriteEntry ( EventLogSource, EventContent, LogEntryType );

        return;
      }

      //
      // As a long event log outout it in 32000 blocks.
      //
      int increment = 30000;
      for ( int index = 0; index < increment * 10; index += increment )
      {
        //
        // Create a remaining length value
        //
        int remainingLength = EventContent.Length - index;

        //
        // Validate whether the remaining lenth exists
        //
        if ( remainingLength > 0 )
        {
          //
          // If the remaining length is greater than increment
          // Pass index and increment to the stContent string
          // Write Event log
          //
          if ( remainingLength > increment )
          {
            string stContent = EventContent.Substring ( index, increment );
            System.Diagnostics.EventLog.WriteEntry ( EventLogSource, stContent, LogEntryType );
          }
          else
          {
            //
            // If not, pass index to the stContent string
            // Write Event log
            //
            string stContent = EventContent.Substring ( index );
            System.Diagnostics.EventLog.WriteEntry ( EventLogSource, stContent, LogEntryType );

          }//END output remaining.

        }//END Remaining length > 0

      }//END iteration loop.

    }//END static WriteToEventLog method

    // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region Binary file methods.

    // ==================================================================================
    /// <summary>
    /// This method generates a mime type list.
    /// </summary>
    /// <param name="ApplicationDirectory">String: application directory. </param>
    /// <param name="FileName">String: the mime type file name.</param>
    /// <returns>List of EvOptions as a list of mime types.</returns>
    /// <remarks>
    /// This method consists of the following steps:
    /// 
    /// 1. Initialize the line string, line list and Mime list
    /// 
    /// 2. Initialise the system.IO reader
    /// 
    /// 3. Open the text reader
    /// 
    /// 4. Read the remainder of the file into the outputLog array list
    /// 
    /// 5. Loop through the lines list
    /// 
    /// 6. Add items into Mime list
    ///
    /// </remarks>
    // ----------------------------------------------------------------------------------
    public static List<Evado.Model.EvOption> loadMimeTypes (
      String ApplicationDirectory,
      String FileName )
    {
      //
      // Initialize the line string, line list and Mime list
      //
      String line = String.Empty;
      List<String> lines = new List<String> ( );
      List<Evado.Model.EvOption> mimeList = new List<Evado.Model.EvOption> ( );

      try
      {
        if ( FileName != String.Empty )
        {
          String TempFileName = ApplicationDirectory + FileName;

          // 
          // Initialise the system.IO reader
          //
          System.IO.TextReader reader;

          // 
          // Open the text reader with supplied file
          // 
          using ( reader = System.IO.File.OpenText ( TempFileName ) )
          {
            // 
            // Read the remainder of the file into the outputLog array list
            // 
            while ( ( line = reader.ReadLine ( ) ) != null )
            {
              lines.Add ( line );
            }
          }

          //
          // Iterate through the file rows creating Evado.Model.EvOption for each mime type
          // and adding them to the mime list.
          //
          foreach ( String line1 in lines )
          {
            string [ ] arrRow = line1.Split ( ',' );

            if ( arrRow.Length > 1 )
            {
              mimeList.Add ( new Evado.Model.EvOption ( arrRow [ 0 ], arrRow [ 1 ] ) );
            }
          }//END file row iteration loop

        }//END output report 2
      }
      catch
      {
        throw;
      }

      return mimeList;
    }//END loadMimeTypes method

    // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region Order Comparer

    /// <summary>
    /// this class defines the order comparer for actvity form object lists.
    /// </summary>
    public class OptionComparer : IComparer<Evado.Model.EvOption>
    {
      //=================================================================================
      /// <summary>
      /// This method performs the order comparision 
      /// </summary>
      /// <param name="x">EvOption object</param>
      /// <param name="y">EvOption object</param>
      /// <returns>int </returns>
      //---------------------------------------------------------------------------------
      public int Compare ( Evado.Model.EvOption x, Evado.Model.EvOption y )
      {
        return String.Compare ( x.Value, y.Value );
      }//END compare method

    }//END class

    /// <summary>
    /// this class defines the order comparer for actvity form object lists.
    /// </summary>
    public class OptionDescComparer : IComparer<Evado.Model.EvOption>
    {
      //=================================================================================
      /// <summary>
      /// This method performs the order comparision 
      /// </summary>
      /// <param name="x">EvOption object</param>
      /// <param name="y">EvOption object</param>
      /// <returns>int </returns>
      //---------------------------------------------------------------------------------
      public int Compare ( Evado.Model.EvOption x, Evado.Model.EvOption y )
      {
        return String.Compare ( x.Description, y.Description );
      }//END compare method

    }//END class

  } //END Statics class


  #endregion

} //END namespace Evado.Model
