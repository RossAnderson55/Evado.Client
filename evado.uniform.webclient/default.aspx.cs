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
        this.LogDebug ( "UserHostAddress: " + Request.UserHostAddress );
        this.LogDebug ( "UserHostName: " + Request.UserHostName );

        //
        // Process post back events.
        //
        if ( this.IsPostBack == false )
        {
          //
          // Process non post back events.
          //
          this.GetUrlParameters ( );
        }
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
      this.litVersion.Text = "Version: " + Global.AssemblyAttributes.FullVersion + Global.DevStage;

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

    #region  private
    //==================================================================================	
    /// <summary>
    /// this method reads in the external command parameters.
    /// </summary>
    /// <returns>Bool: true = external command found.</returns>
    // --------------------------------------------------------------------------------
    private bool GetUrlParameters ( )
    {
      this.LogMethod ( "ReadUrlParameters" );
      // 
      // Extract the URL parameters and instantiate the local variables.
      // 
      int loop1;
      string Key, Value;

      this.LogDebug ( "Request.Url: " + Request.Url );
      this.LogDebug ( "Request.Url.AbsoluteUri: " + Request.Url.AbsoluteUri );
      this.LogDebug ( "Request.Url.Query: " + Request.Url.Query );

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

        string parameter = Key.ToLower ( );


      }//END paraemter iteration loop

      this.LogMethodEnd ( "ReadUrlParameters" );
      return false;

    }//END ReadUrlParameters method.

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
      string logValue = DateTime.Now.ToString ( "dd-MM-yy hh:mm:ss" ) + ":"
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
      string logValue = DateTime.Now.ToString ( "dd-MM-yy hh:mm:ss" ) + ":"
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
