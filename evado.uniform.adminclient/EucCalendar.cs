/***************************************************************************************
 * <copyright file="webclinical\EVADO HOLDING PTY. LTD.EventLog.cs" company="EVADO HOLDING PTY. LTD.">
 *     
 *      Copyright (c) 2011 - 2025 EVADO HOLDING PTY. LTD.  All rights reserved.
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
 ****************************************************************************************/

using Evado.UniForm.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

//Evado. namespace references.

namespace Evado.UniForm.AdminClient
{
  /// <summary>
  /// This class generates the calendar field layout
  /// </summary>
  public class EucCalendar
  {
    #region initialiseation methods.
    // ===================================================================================
    /// <summary>
    /// This initialisation method generates the calendar html structures.
    /// </summary>
    // ----------------------------------------------------------------------------------
    public EucCalendar ( )
    { }

    // ===================================================================================
    /// <summary>
    /// This initialisation method generates the calendar html structures.
    /// </summary>
    /// <param name="sbHtml"></param>
    /// <param name="PageField"></param>
    // ----------------------------------------------------------------------------------
    public EucCalendar ( EuClientSession UserSession, StringBuilder sbHtml, Evado.UniForm.Model.EuField PageField )
    {
      this.LogMethod ( "EucCalendar" );

      this.UserSession = UserSession;
      this.Html = sbHtml; 
      this.PageField = PageField;

      this.GeneratePageField ( );


      this.LogMethodEnd ( "EucCalendar" );
    }

    // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region Properties

    /// <summary>
    /// this property is a string builder containing the HTML markup text.
    /// </summary>
    public StringBuilder Html { get; set; } = new StringBuilder ();

    /// <summary>
    /// This property contains the page field defining the calendar content.
    /// </summary>
    public  Evado.UniForm.Model.EuField PageField { get; set; }

    /// <summary>
    /// This field contains the current group object.
    /// </summary>
    public EuClientSession UserSession = new EuClientSession ( );

    // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region public methods.

    // ===================================================================================
    /// <summary>
    /// This method generates the calendar components.
    /// </summary>
    // ----------------------------------------------------------------------------------
    public void GeneratePageField()
    {
      this.LogMethodEnd ( "GeneratePageField" );



      this.LogMethodEnd ( "GeneratePageField" );
    }

    // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region private methods.

    // ===================================================================================
    /// <summary>
    /// This method creates a read only field markup
    /// </summary>
    /// <param name="sbHtml">StringBuilder object containing the page html.</param>
    /// <param name="TabIndex">int: the current tab index.</param>
    /// <param name="PageField">Field object.</param>
    // ----------------------------------------------------------------------------------
    private void createFieldHeader (
      StringBuilder sbHtml,
      Evado.UniForm.Model.EuField PageField,
      int TitleWidth,
      bool TitleFullWidth )
    {
      this.LogMethod ( "createFieldHeader" );
      this.LogDebug ( "PageField.FieldId:{0}.", PageField.FieldId );
      this.LogDebug ( "PageField.Title: {0}.", PageField.Title );
      this.LogDebug ( "PageField.Type: {0}.", PageField.Type );
      this.LogDebug ( "CurrentGroupType:{0}.", this.UserSession.CurrentGroup.GroupType );
      //this.LogDebug ( "ImagesUrl: {0}.", Global.StaticImageUrl );
      //
      // initialise method variables and objects.
      //
      String stLayout = String.Empty;
      String stFieldRowStyling = String.Empty;
      String stFieldTitleStyling = String.Empty;
      String stField_Suffix = String.Empty;
      String stDescription = String.Empty;
      String stAnnotation = PageField.GetParameter ( Evado.UniForm.Model.EuFieldParameters.Annotation );

      stFieldRowStyling = "class='group-row field " + stLayout + " cf " + this.UserSession.FieldBackgroundColorClass ( PageField ) + "' ";
      stFieldTitleStyling = "style='width:" + TitleWidth + "%; ' class='cell title cell-display-text-title'";

      //
      // Format the description value from mark down to html.
      //
      if ( String.IsNullOrEmpty ( PageField.Description ) == false )
      {
        //this.LogDebug ( "JSON: PageField.Description : {0}.", PageField.Description );

        stDescription = Evado.Model.EvStatics.EncodeMarkDown ( PageField.Description );

        if ( stDescription.Contains ( "/]" ) == true )
        {
          stDescription = stDescription.Replace ( "{images}", Global.StaticImageUrl );
          stDescription = stDescription.Replace ( "[", "<" );
          stDescription = stDescription.Replace ( "]", ">" );
        }
      }
      //this.LogDebug ( "HTML: stDescription: {0}.", stDescription );

      //
      // Format the description value from mark down to html.
      //
      if ( stAnnotation != String.Empty )
      {
        stAnnotation = Evado.Model.EvStatics.EncodeMarkDown ( stAnnotation );
      }
      this.LogDebug ( "stDescription: {0}.", stDescription );

            stLayout = "layout-column";
            stFieldTitleStyling = "style='width: 98%; ' class='cell title cell-display-text-title'";



      // always use column layout for tables
      if ( PageField.Type == Evado.Model.EvDataTypes.Table )
      {
        stLayout = "layout-column";
      }


      sbHtml.AppendLine ( "<!-- -------------------------------------------------------------------------\r\n"
        + "        FIELD HEADER = " + PageField.FieldId + " DATA TYPE = " + PageField.Type +
        " LAYOUT = " + PageField.Layout + "     \r\n -->" );

      sbHtml.AppendLine ( "<div id='" + PageField.Id + "-row' " + stFieldRowStyling + " >" );

      this.LogDebug ( "Title: " + PageField.Title );
      //
      // Error message
      //
      this.LogDebug ( "Formattted title: " + PageField.Title );

      sbHtml.AppendLine ( "<div " + stFieldTitleStyling + "> " );

      if ( PageField.Title != String.Empty )
      {
        sbHtml.AppendLine ( "<label>" + PageField.Title );

        if ( PageField.Mandatory == true && PageField.EditAccess != false )
        {
          sbHtml.Append ( "<span class='required'> * </span>" );
        }

        sbHtml.Append ( "</label>\r\n " );

        if ( PageField.IsEnabled == true )
        {
          sbHtml.AppendLine ( "<div class='error-container ' style='display: none'>" ); // style='display: none'
          sbHtml.AppendLine ( "<div id='" + PageField.Id + "-err-row' class='cell cell-error-value'>" );
          sbHtml.AppendLine ( "<span id='sp" + PageField.Id + "-err'></span>" );
          sbHtml.AppendLine ( "</div></div>\r\n" );
        }
      }

      if ( stDescription != String.Empty )
      {
        sbHtml.AppendLine ( "<div class='description'>" + stDescription + "</div>" );
      }


      //
      // Close field header tag
      //
      sbHtml.Append ( "</div>" );

      this.LogMethodEnd ( "createFieldHeader" );

    }//END createFieldHeader method

    // ===================================================================================
    /// <summary>
    /// This method creates a read only field markup
    /// </summary>
    /// <param name="PageField">Field object.</param>
    /// <returns>String html</returns>
    // ----------------------------------------------------------------------------------
    private void createFieldFooter (
      StringBuilder sbHtml,
      Evado.UniForm.Model.EuField PageField )
    {
      sbHtml.Append ( "</div>" );

      sbHtml.AppendLine ( "<!--      FIELD FOOTER = " + PageField.FieldId + " DATA TYPE = " + PageField.Type +
        "     \r\n------------------------------------------------------------------------- -->" );
    }

    // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
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
    public void LogStandard ( String Value )
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
    public void LogStandard ( String Format, params object [ ] args )
    {
      string logValue = DateTime.Now.ToString ( "dd-MM-yy hh:mm:ss" ) + ": "
       + "DefaultPage:" + String.Format ( Format, args );

      Global.LogValue ( logValue );
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

  }//END Class

}//END NameSpace
