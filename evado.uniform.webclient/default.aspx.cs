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

    String UrlParameterString = String.Empty;

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
        this.LogDebug ( "Default:QueryString: " + Request.QueryString.ToString ( ) );

        this.LogDebug ( "Default:1: clientFrame.src: '{0}'.", this.clientFrame.Attributes [ "src" ] );
        //
        // Process post back events.
        //
        if ( this.IsPostBack == false )
        {
          this.LogDebug ( "Default:PostBack = false" );
          this.meetingUrl.Value = String.Empty;
          this.meetingDisplayName.Value = String.Empty;
          this.meetingParameters.Value = String.Empty;
          this.meetingStatus.Value = Evado.Model.EvMeeting.States.Null.ToString();
          String clientUrl = Evado.UniForm.Model.EuStatics.CONST_CLIENT_BASE_URL;
          string queryString = Request.QueryString.ToString ( );
          if ( queryString != String.Empty )
          {
            clientUrl = clientUrl + "?" + Request.QueryString.ToString ( );
          }
          this.LogDebug ( "Default:2: clientUrl: '{0}'.", clientUrl );
          this.clientFrame.Attributes [ "src" ] = clientUrl;

          this.LogDebug ( "Default:2: clientFrame.src: '{0}'.", this.clientFrame.Attributes [ "src" ] );
          this.LogDebug ( "Default:2: clientFrame.width: '{0}'.", this.clientFrame.Attributes [ "width" ] );
          this.LogDebug ( "Default:2: clientFrame.height: '{0}'.", this.clientFrame.Attributes [ "height" ] );
        }

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
      this.litVersion.Text = "Version: " + Global.AssemblyAttributes.FullVersion ;

      this.LogMethodEnd ( "Default:Page_Load" );
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
         + "Evado.Uniform.Webclient.DefaultPage:" + Value + " Method";

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
    public void LogValue ( String Format, params object [ ] args )
    {
      string logValue = DateTime.Now.ToString ( "dd-MM-yy hh:mm:ss" ) + ": "
       + "DefaultPage:" + String.Format ( Format, args );

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
    public void LogDebug ( String Format, params object [ ] args )
    {
      string logValue = DateTime.Now.ToString ( "dd-MM-yy hh:mm:ss" ) + ": "
       + "DefaultPage:" + String.Format ( Format, args );

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
