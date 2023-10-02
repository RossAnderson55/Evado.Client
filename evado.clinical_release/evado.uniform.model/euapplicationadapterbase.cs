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
  public class EuApplicationAdapterBase
  {

    #region Class Public Methods

    /// <summary>
    /// This property contains a hash table for global objects.
    /// </summary>
    public Hashtable GlobalObjectList { get; set; } = new Hashtable ( );

    /// <summary>
    /// This property contains an application path for the class.
    /// </summary>
    public String ApplicationPath { get; set; } = String.Empty;

    /// <summary>
    /// This property contains a binary file path  for the class.
    /// </summary>
    public String BinaryFilePath { get; set; } = String.Empty;

    /// <summary>
    /// This property contains a service binary Url for the class.
    /// </summary>
    public String BinaryFileUrl { get; set; } = String.Empty;

    /// <summary>
    /// This property contains a user profile.
    /// </summary>
    public Evado.Model.EvUserProfileBase ServiceUserProfile { get; set; } = new EvUserProfileBase ( );


    /// <summary>
    /// This is the base class name space for the adapter class.
    /// </summary>
    protected String ClassNameSpace = "Evado.UniForm.Model.ApplicationServiceBase.";

    /// <summary>
    /// This constant defines session home page identifier.
    /// </summary>
    public const String SESSION_HOME_PAGE_IDENTIFIER = "HomePageIdentifier";
    /// <summary>
    /// This field contains the home page default idenifier.
    /// </summary>
    public Guid HomePageIdentifier = Guid.Empty;

    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region Class Public Methods

    /// <summary>
    /// This field contains the adapter log string builder.
    /// </summary>
    protected StringBuilder _AdapterLog = new StringBuilder ( );

    /// <summary>
    ///  This property contains the debug log entries.
    /// </summary>
    public String AdapterLog
    {
      get { return this._AdapterLog.ToString ( ); }
    }

    /// <summary>
    /// This property defines the log setting.
    /// </summary>
    public EvStatics.LoggingTypes LogSetting { get; set; } = EvStatics.LoggingTypes.Standard;

    // ==================================================================================
    /// <summary>
    /// This property defines if debug logging is enabled.
    /// </summary>
    // ----------------------------------------------------------------------------------
    public bool DebugOn
    {
      get
      {
        if ( this.LogSetting == EvStatics.LoggingTypes.Debug )
        {
          return true;
        }
        return false;
      }
    }

    // ==================================================================================
    /// <summary>
    /// This method gets the application object from the list.
    /// </summary>
    /// <param name="PageCommand">Command: ClientPateCommand object</param>
    /// <returns>Evado.UniForm.Model.EuAppData</returns>
    // ----------------------------------------------------------------------------------
    public virtual EuAppData getPageObject ( EuCommand PageCommand )
    {
      return new EuAppData ( );

    }//END getPageObject method

    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region Debug methods.

    // ==================================================================================
    /// <summary>
    /// This method appendes the debuglog string to the debug log for the class and adds
    /// a new line at the end of the text.
    /// </summary>
    // ----------------------------------------------------------------------------------
    protected void resetApplicationLog ( )
    {
      this._AdapterLog = new StringBuilder ( );
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
      this._AdapterLog.AppendLine ( Evado.Model.EvStatics.CONST_METHOD_START
      + DateTime.Now.ToString ( "dd-MM-yy hh:mm:ss" ) + ": "
      + this.ClassNameSpace + "." + Value + "" );
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
        this._AdapterLog.AppendLine ( Evado.Model.EvStatics.CONST_METHOD_START
        + DateTime.Now.ToString ( "dd-MM-yy hh:mm:ss" ) + ": "
        + this.ClassNameSpace + "." + Value + "" );
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

        this._AdapterLog.AppendLine ( value );
      }
    }

    // ==================================================================================
    /// <summary>
    /// This method appendes debuglog string to the debug log for the class and adds
    /// a new line at the end of the text.
    /// </summary>
    /// <param name="Value">String:  debug text.</param>
    // ----------------------------------------------------------------------------------
    protected void LogValue ( String Value )
    {
      this._AdapterLog.AppendLine ( DateTime.Now.ToString ( "dd-MM-yy hh:mm:ss" ) + ": " + Value );
    }

    // ==================================================================================
    /// <summary>
    /// This method appendes log string to the debug log for the class and adds
    /// a new line at the end of the text.
    /// </summary>
    /// <param name="Format">String: format text.</param>
    /// <param name="args">Array of objects as parameters.</param>
    // ----------------------------------------------------------------------------------
    protected void LogValue ( String Format, params object [ ] args )
    {
      this._AdapterLog.AppendLine ( DateTime.Now.ToString ( "dd-MM-yy hh:mm:ss" ) + ": " +
        String.Format ( Format, args ) );
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
      this._AdapterLog.Append ( Value );
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
        this._AdapterLog.AppendLine ( DateTime.Now.ToString ( "dd-MM-yy hh:mm:ss" ) + ": " + Value );
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
        this._AdapterLog.AppendLine ( DateTime.Now.ToString ( "dd-MM-yy hh:mm:ss" ) + ": " +
          String.Format ( Format, args ) );
      }
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
      this._AdapterLog.Append ( Value );
    }//END AddApplicationEvent class



    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

  }//END Service class

}//END NAMESPACE
