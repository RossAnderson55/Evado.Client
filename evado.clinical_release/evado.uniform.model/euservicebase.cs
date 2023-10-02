using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

using Evado.Model;


namespace Evado.UniForm.Model
{
  /// <summary>
  /// This 
  /// </summary>
  public class EuServiceBase
  {
    #region Class Global Objects


    /// <summary>
    /// This global variable contains the service identifier
    /// </summary>
    public string LogPrefix = "service";
     
    /// <summary>
    /// This property contains a user profile.
    /// </summary>
    public Evado.Model.EvUserProfileBase ServiceUserProfile { get; set; } = new EvUserProfileBase ( );

    /// <summary>
    /// This property defines the log file path.
    /// </summary>
    public String LogFilePath { get; set; } = String.Empty;

    public String LogFileName { get; set; } = @"-app-log-";

    /// <summary>
    /// This field contains the adapter log string builder.
    /// </summary>
    protected static StringBuilder ApplicationLog = new StringBuilder ( );

    /// <summary>
    /// This property defines the setting.
    /// </summary>
    public EvStatics.LoggingTypes LogSetting { get; set; } = EvStatics.LoggingTypes.Standard;


    /// <summary>
    /// This is the base class name space for the adapter class.
    /// </summary>
    protected String ClassNameSpace = "Evado.UniForm.Model.ServiceBase.";

    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region Class properties and values

    // ==================================================================================
    /// <summary>
    /// This method appendes the debuglog string to the debug log for the class and adds
    /// a new line at the end of the text.
    /// </summary>
    // ----------------------------------------------------------------------------------
    protected void resetLog ( )
    {
      ApplicationLog = new StringBuilder ( );
    }

    // ==================================================================================
    /// <summary>
    /// This method appendes the debuglog string to the debug log for the class and adds
    /// a new line at the end of the text.
    /// </summary>
    /// <param name="Value">String:  debug text.</param>
    // ----------------------------------------------------------------------------------
    protected void LogPublicMethod ( String Value )
    {
      ApplicationLog.AppendLine ( Evado.Model.EvStatics.CONST_METHOD_START
      + DateTime.Now.ToString ( "dd-MM-yy hh:mm:ss" ) + ": "
      + this.ClassNameSpace + "." + Value );
    }

    // ==================================================================================
    /// <summary>
    /// This method appendes the debuglog string to the debug log for the class and adds
    /// a new line at the end of the text.
    /// </summary>
    /// <param name="Value">String:  debug text.</param>
    // ----------------------------------------------------------------------------------
    protected void LogValue ( String Value )
    {
      ApplicationLog.AppendLine ( DateTime.Now.ToString ( "dd-MM-yy hh:mm:ss" ) + ": " + Value );
    }

    // ==================================================================================
    /// <summary>
    /// This method appendes debuglog string to the debug log for the class and adds
    /// a new line at the end of the text.
    /// </summary>
    /// <param name="Format">String: format text.</param>
    /// <param name="Values">Array of objects as parameters.</param>
    // ----------------------------------------------------------------------------------
    protected void LogValue ( String Format, params object [ ] Values )
    {
      ApplicationLog.AppendLine ( DateTime.Now.ToString ( "dd-MM-yy hh:mm:ss" ) + ": " +
        String.Format ( Format, Values ) );
    }

    // ==================================================================================
    /// <summary>
    /// This method appendes the debuglog string to the debug log for the class and adds
    /// a new line at the end of the text.
    /// </summary>
    /// <param name="Value">String:  debug text.</param>
    // ----------------------------------------------------------------------------------
    protected void LogClass ( String Value )
    {
      ApplicationLog.Append ( Value );
    }

    // ==================================================================================
    /// <summary>
    /// This method appendes the debuglog string to the debug log for the class and adds
    /// a new line at the end of the text.
    /// </summary>
    /// <param name="Value">String:  debug text.</param>
    // ----------------------------------------------------------------------------------
    protected void LogMethod ( String Value )
    {
      if ( this.LogSetting == EvStatics.LoggingTypes.Debug )
      {
        ApplicationLog.AppendLine ( Evado.Model.EvStatics.CONST_METHOD_START
        + DateTime.Now.ToString ( "dd-MM-yy hh:mm:ss" ) + ": "
        + this.ClassNameSpace + Value + "" );
      }
    }

    // ==================================================================================
    /// <summary>
    /// This method appendes the debuglog string to the debug log for the class and adds
    /// a new line at the end of the text.
    /// </summary>
    /// <param name="MethodName">String:  debug text.</param>
    // ----------------------------------------------------------------------------------
    protected void LogMethodEnd ( String MethodName )
    {
      if ( this.LogSetting == EvStatics.LoggingTypes.Debug )
      {
        String value = Evado.Model.EvStatics.CONST_METHOD_END;

        value = value.Replace ( " END OF METHOD ", " END OF " + MethodName + " METHOD " );

        ApplicationLog.AppendLine ( value );
      }
    }

    // ==================================================================================
    /// <summary>
    /// This method appendes the debuglog string to the debug log for the class and adds
    /// a new line at the end of the text.
    /// </summary>
    /// <param name="Value">String:  debug text.</param>
    // ----------------------------------------------------------------------------------
    protected void LogAdapter ( String Value )
    {
        ApplicationLog.Append ( Value );
    }


    // ==================================================================================
    /// <summary>
    /// This method appendes debuglog string to the debug log for the class and adds
    /// a new line at the end of the text.
    /// </summary>
    /// <param name="Value">String:  debug text.</param>
    // ----------------------------------------------------------------------------------
    protected void LogDebug ( String Value )
    {
      if ( this.LogSetting == EvStatics.LoggingTypes.Debug )
      {
        ApplicationLog.AppendLine ( DateTime.Now.ToString ( "dd-MM-yy hh:mm:ss" ) + ": " + Value );
      }
    }

    // ==================================================================================
    /// <summary>
    /// This method appendes debuglog string to the debug log for the class and adds
    /// a new line at the end of the text.
    /// </summary>
    /// <param name="Format">String: format text.</param>
    /// <param name="args">Array of objects as parameters.</param>
    // ----------------------------------------------------------------------------------
    protected void LogDebug ( String Format, params object [ ] args )
    {
      if ( this.LogSetting == EvStatics.LoggingTypes.Debug )
      {
        ApplicationLog.AppendLine ( DateTime.Now.ToString ( "dd-MM-yy hh:mm:ss" ) + ": " +
          String.Format ( Format, args ) );
      }
    }


    //  =================================================================================
    /// <summary>
    ///   This static method removes a user from the online user list.
    /// 
    /// </summary>
    //   ---------------------------------------------------------------------------------
    public void OutputLog ( )
    {
      //
      // Define the filename
      //
      String LogFileName = LogPrefix
        + this.LogFileName
        + DateTime.Now.ToString ( "yy-MM" ) + ".log";

      LogFileName = LogFileName.Replace ( "/", "-" );

      //
      // if the debug log path is defined output the debug log to the given path.
      //
      if ( this.LogFilePath == String.Empty )
      {
        return;
      }

      //
      // Output the debug log to debug log page.
      //
      String stContent = String.Empty;

      if ( ApplicationLog.Length == 0 )
      {
        stContent = " APPLICATION LOG\r\n"
          + "Saved: " + DateTime.Now.ToString ( "dd MMM yyyy HH:mm:ss" )
          + "\r\nNo Debug Content";
      }
      else
      {
        stContent += " APPLICATION LOG\r\n"
          + "Saved: " + DateTime.Now.ToString ( "dd MMM yyyy HH:mm:ss" )
          + "\r\n"
          + ApplicationLog.ToString ( );
      }

      stContent = Evado.Model.EvStatics.getHtmlAsString ( stContent );

      Evado.Model.EvStatics.Files.saveFile ( this.LogFilePath, LogFileName, stContent );

    }//END writeOutDebugLog method

    //  =================================================================================
    /// <summary>
    ///   This static method removes a user from the online user list.
    /// 
    /// </summary>
    //   ---------------------------------------------------------------------------------
    public void OutputLog_Save ( )
    {
      //
      // Define the filename
      //
      String LogFileName = LogPrefix
        + this.LogFileName
        + DateTime.Now.ToString ( "yy-MM" ) + "-SAVE.log";

      LogFileName = LogFileName.Replace ( "/", "-" );

      //
      // if the debug log path is defined output the debug log to the given path.
      //
      if ( this.LogFilePath == String.Empty )
      {
        return;
      }

      //
      // Output the debug log to debug log page.
      //
      String stContent = String.Empty;

      if ( ApplicationLog.Length == 0 )
      {
        stContent = " APPLICATION LOG\r\n"
          + "Saved: " + DateTime.Now.ToString ( "dd MMM yyyy HH:mm:ss" )
          + "\r\nNo Debug Content";
      }
      else
      {
        stContent += " APPLICATION LOG\r\n"
          + "Saved: " + DateTime.Now.ToString ( "dd MMM yyyy HH:mm:ss" )
          + "\r\n"
          + ApplicationLog.ToString ( );
      }

      stContent = Evado.Model.EvStatics.getHtmlAsString ( stContent );

      Evado.Model.EvStatics.Files.saveFile ( this.LogFilePath, LogFileName, stContent );

    }//END writeOutDebugLog method

    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

  }//END Service class

}//END NAMESPACE
