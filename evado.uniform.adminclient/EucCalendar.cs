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

using Evado.Model;
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
using System.Web.UI.MobileControls;

//Evado. namespace references.

namespace Evado.UniForm.AdminClient
{
  /// <summary>
  /// This class generates the calendar field layout
  /// </summary>
  public class EucCalendar
  {

    #region Properties

    /// <summary>
    /// this property is a string builder containing the HTML markup text.
    /// </summary>
    public StringBuilder Html { get; set; } = new StringBuilder ( );

    /// <summary>
    /// This property contains the page field defining the calendar content.
    /// </summary>
    public EuField GroupField { get; set; }

    /// <summary>
    /// This property contains the page group containing the calendar field.
    /// </summary>
    public EuGroup PageGroup { get; set; } = new EuGroup ( );

    /// <summary>
    /// This field contains the current group object.
    /// </summary>
    public EuClientSession UserSession = new EuClientSession ( );


    //
    // Setting the default command bacground colours.
    //
    string background_Default = String.Empty ;
    string background_Alternative = String.Empty ;
    string background_Highlighted = String.Empty ;
    bool bEventRow = false;


    // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region public methods.

    // ===================================================================================
    /// <summary>
    /// This method generates the calendar components.
    /// </summary>
    // ----------------------------------------------------------------------------------
    public void GeneratePageField ( )
    {
      this.LogMethodEnd ( "GeneratePageField" );
      //
      // Initialise the methods variables and objects.
      //
      int valueColumnWidth = 100;
      int titleColumnWidth = 100;
      String stFieldValueStyling = String.Format ("style='width:{0}%' class='cell value cell-table-value cf' ",valueColumnWidth );
      //
      // Setting the default command bacground colours.
      //
      this.background_Default = this.PageGroup.CommandBackground.ToString ( );
      this.background_Alternative = this.PageGroup.AlternativeCommandBackground.ToString ( );
      this.background_Highlighted = this.PageGroup.HighlightedCommandBackground.ToString ( );

      this.LogDebug ( "background_Default: " + background_Default );
      this.LogDebug ( "background_Alternative: " + background_Alternative );
      this.LogDebug ( "background_Highlighted: " + background_Highlighted );

      if ( this.GroupField.Calendar == null )
      {
        this.LogMethodEnd ( "GeneratePageField" );
        return;
      }

      //
      // sort the entries into date order.
      // Earliest to latest
      //
      this.GroupField.Calendar.sortEntries ( );

      //
      // Ineert the field header
      //
      this.createFieldHeader ( titleColumnWidth );

      //
      // Insert the field elements
      //
      this.Html.AppendFormat ( "<div {0} > \r\n", stFieldValueStyling );

      switch ( this.GroupField.Calendar.DateRange )
      {
        case Evado.Model.EvmCalendar.DateRanges.Week:
          {
            this.getWeekCalendar ( this.GroupField.Calendar );
            break;
          }
        case Evado.Model.EvmCalendar.DateRanges.Month:
          {
            this.getWeekCalendar ( this.GroupField.Calendar );
            break;
          }
        default:
          {
            this.getDayCalendar ( this.GroupField.Calendar );
            break;
          }
      }

      this.Html.AppendLine ( "</div>" );

      //
      // Insert the field footer elemements
      //
      this.createFieldFooter ( );

      this.LogMethodEnd ( "GeneratePageField" );

    }//END GeneratePageField METHOD 

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
    private void createFieldHeader ( int TitleWidth )
    {
      this.LogMethod ( "createFieldHeader" );
      this.LogDebug ( "PageField.FieldId:{0}.", GroupField.FieldId );
      this.LogDebug ( "PageField.Title: {0}.", GroupField.Title );
      this.LogDebug ( "PageField.Type: {0}.", GroupField.Type );
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
      String stAnnotation = GroupField.GetParameter ( Evado.UniForm.Model.EuFieldParameters.Annotation );

      stFieldRowStyling = "class='group-row field " + stLayout + " cf " + this.UserSession.FieldBackgroundColorClass ( GroupField ) + "' ";
      stFieldTitleStyling = "style='width:" + TitleWidth + "%; ' class='cell title cell-display-text-title'";

      //
      // Format the description value from mark down to html.
      //
      if ( String.IsNullOrEmpty ( GroupField.Description ) == false )
      {
        //this.LogDebug ( "JSON: PageField.Description : {0}.", PageField.Description );

        stDescription = Evado.Model.EvStatics.EncodeMarkDown ( GroupField.Description );

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
      if ( GroupField.Type == Evado.Model.EvDataTypes.Table )
      {
        stLayout = "layout-column";
      }

      this.Html.AppendFormat ( "<!-- -------------------------------------------------------------------------\r\n"
        + "        FIELD HEADER = {0} DATA TYPE = {0} LAYOUT = {2}  -->\r\n", GroupField.FieldId, GroupField.Type, GroupField.Layout );

      this.Html.AppendLine ( "<div id='" + GroupField.Id + "-row' " + stFieldRowStyling + " >" );

      this.LogDebug ( "Title: " + GroupField.Title );
      //
      // Error message
      //
      this.LogDebug ( "Formattted title: " + GroupField.Title );

      this.Html.AppendLine ( "<div " + stFieldTitleStyling + "> " );

      if ( GroupField.Title != String.Empty )
      {
        this.Html.AppendLine ( "<label>" + GroupField.Title );

        if ( GroupField.Mandatory == true && GroupField.EditAccess != false )
        {
          this.Html.Append ( "<span class='required'> * </span>" );
        }

        this.Html.Append ( "</label>\r\n " );

        if ( GroupField.IsEnabled == true )
        {
          this.Html.AppendLine ( "<div class='error-container ' style='display: none'>" ); // style='display: none'
          this.Html.AppendLine ( "<div id='" + GroupField.Id + "-err-row' class='cell cell-error-value'>" );
          this.Html.AppendLine ( "<span id='sp" + GroupField.Id + "-err'></span>" );
          this.Html.AppendLine ( "</div></div>\r\n" );
        }
      }

      if ( stDescription != String.Empty )
      {
        this.Html.AppendLine ( "<div class='description'>" + stDescription + "</div>" );
      }


      //
      // Close field header tag
      //
      this.Html.Append ( "</div>" );

      this.LogMethodEnd ( "createFieldHeader" );

    }//END createFieldHeader method

    // ===================================================================================
    /// <summary>
    /// This method creates a read only field markup
    /// </summary>
    // ----------------------------------------------------------------------------------
    private void createFieldFooter ( )
    {
      this.Html.Append ( "</div>" );

      this.Html.AppendLine ( "<!--      FIELD FOOTER = " + GroupField.FieldId + " DATA TYPE = " + GroupField.Type +
        "     \r\n------------------------------------------------------------------------- -->" );
    }

    // ===================================================================================
    /// <summary>
    /// This method creates a read only field markup
    /// </summary>
    /// <param name="Calendar">EumCalendar object.</param>
    // ----------------------------------------------------------------------------------
    private void getDayCalendar ( EumCalendar Calendar )
    {
      this.LogMethod ( "getDayCalendar" );
      this.LogDebug ( "StartDate: {0}, DateRane: {1}", Calendar.StartDate.ToString ( "dd-MM-yy" ), Calendar.DateRange );
      this.LogDebug ( "TimeStart: {0}, TimeFinish: {1}", Calendar.TimeStartHr, Calendar.TimeFinishHr );
      this.LogDebug ( "No Calendar Entries = {0}.", Calendar.Entries.Count );

      this.Html.AppendLine ( "<table class='table table-striped'>" );

      int lowerTime = 0;
      int upperTime = 0;

      //
      // iterate through the time scale of the calendar.
      //
      for ( int hour = Calendar.TimeStartHr; hour < Calendar.TimeFinishHr; hour++ )
      {
        lowerTime = hour * 100;
        upperTime = ( hour + 1 ) * 100;

        this.LogDebug ( "lowerTime: {0}, upperTime: {1}.", lowerTime, upperTime );

        this.Html.AppendLine ( "<tr>" );
        this.Html.AppendFormat ( "<td>{0}</td>", hour.ToString ( "00" ) );
        this.Html.AppendLine ( "<td>" );

        this.GetCalendaEntry ( Calendar, Calendar.StartDate, lowerTime, upperTime );

        this.Html.AppendLine ( "</td>" );
        this.Html.AppendLine ( "</tr>" );

      }//END hour iteration loop

      this.Html.AppendLine ( "</table>" );

      this.LogMethodEnd ( "getDayCalendar" );

    }//END getDayCalendar Method

    // ===================================================================================
    /// <summary>
    /// This method creates a read only field markup
    /// </summary>
    /// <param name="Calendar">EumCalendar object.</param>
    // ----------------------------------------------------------------------------------
    private void getWeekCalendar ( EumCalendar Calendar )
    {
      this.LogMethod ( "getDayCalendar" );
      this.LogDebug ( "StartDate: {0}, DateRane: {1}", Calendar.StartDate.ToString ( "dd-MM-yy" ), Calendar.DateRange );
      this.LogDebug ( "TimeStart: {0}, TimeFinish: {1}", Calendar.TimeStartHr, Calendar.TimeFinishHr );
      this.LogDebug ( "No Calendar Entries = {0}.", Calendar.Entries.Count );

      this.Html.AppendLine ( "<table class='table table-striped'>" );

      int lowerTime = 0;
      int upperTime = 0;


      //
      // insert the header for the week range.
      //
      this.Html.AppendLine ( "<tr>" );
      this.Html.AppendFormat ( "<td style='width:10%' >&nbsp;</td>" );
      for ( int index = 0; index < 7; index++ )
      {
        var date = Calendar.MondayDate.AddDays ( index );
        string headerText =  date.DayOfWeek.ToString();
        headerText = headerText.Replace ( "Monday", "Mon" );
        headerText = headerText.Replace ( "Tuesday", "Tues" );
        headerText = headerText.Replace ( "Wednesday", "Wed" );
        headerText = headerText.Replace ( "Thursday", "Thur" );
        headerText = headerText.Replace ( "Friday", "Fri" );
        headerText = headerText.Replace ( "Saturday", "Sat" );
        headerText = headerText.Replace ( "Sunday", "Sun" );
        headerText += " " + date.ToString ( "dd" );

        this.Html.AppendFormat ( "<td style='width:12.5%'>{0}</td>", headerText );
      }
      this.Html.AppendLine ( "</tr>" );


      //
      // iterate through the time scale of the calendar.
      //
      for ( int hour = Calendar.TimeStartHr; hour < Calendar.TimeFinishHr; hour++ )
      {
        lowerTime = hour * 100;
        upperTime = ( hour + 1 ) * 100;

        this.LogDebug ( "lowerTime: {0}, upperTime: {1}.", lowerTime, upperTime );

        this.Html.AppendLine ( "<tr>" );
        this.Html.AppendFormat ( "<td>{0}</td>", hour.ToString ( "00" ) );

        //
        // add the date row values.
        //
        for ( int index = 0; index < 7; index++ )
        {
          var date = Calendar.MondayDate.AddDays ( index );

          this.LogDebug ( "date: {0}.", date.ToString ( "dd-MMM-yy" ) );

          this.Html.AppendLine ( "<td>" );
          this.GetCalendaEntry ( Calendar, date, lowerTime, upperTime );
          this.Html.AppendLine ( "</td>" );
        }

        this.Html.AppendLine ( "</tr>" );

      }//END hour iteration loop

      this.Html.AppendLine ( "</table>" );

      this.LogMethodEnd ( "getDayCalendar" );

    }//END getDayCalendar Method

    // ===================================================================================
    /// <summary>
    /// This method renders the calendar entry command for a specific column date and time period.
    /// </summary>
    /// <param name="Entry">EumCalendarEntry data object</param>
    /// <param name="ColumnDate">DateTime: column day's date</param
    /// <param name="LowerTime">int: 24 hour time</param
    /// <param name="UpperTime">int: 24 hour time</param>
    // ----------------------------------------------------------------------------------
    private void GetCalendaEntry ( EumCalendar Calendar, DateTime ColumnDate, int LowerTime, int UpperTime )
    {
      //this.LogMethod ( "GetCalendaEntry" );
      this.LogDebug ( "ColumnDate: {0}.", ColumnDate.ToString ( "dd-MMM-yy" ) );
      //
      // Iterate through the entries adding entries that match the time value.
      //
      foreach ( EumCalendarEntry entry in Calendar.Entries )
      {
        this.LogDebug ( "entry.Date: {0}.", entry.Date.ToString ( "dd-MMM-yy HH:mm" ) );

        if ( entry.Date.Date != ColumnDate )
        {
          this.LogDebug ( "SKIP: dates doesn't match" );
          continue;
        }

        if ( entry.iTime < LowerTime )
        {
          this.LogDebug ( "SKIP: time out of lower range" );
          continue;
        }

        if ( entry.iTime >= UpperTime )
        {
          this.LogDebug ( "SKIP: time out of upper range" );
          continue;
        }

        this.renderCalendarCommand ( entry );

      }//END calendar entry interation loop.

      //this.LogMethodEnd ( "GetCalendaEntry" );
    }//EN GetCalendaEntry Method

    // ===================================================================================
    /// <summary>
    /// This method renders the calendar entry command 
    /// </summary>
    /// <param name="Entry">EumCalendarEntry data object</param>
    /// <param name="bEventRow">Bool true = even row</param>
    // ----------------------------------------------------------------------------------
    void renderCalendarCommand ( EumCalendarEntry Entry )
    {

      if ( Entry.Command.Type != Evado.UniForm.Model.EuCommandTypes.Null )
      {
        String background = Entry.Command.GetParameter ( Evado.UniForm.Model.EuCommandParameters.BG_Default );
        String alternative = Entry.Command.GetParameter ( Evado.UniForm.Model.EuCommandParameters.BG_Alternative );
        String highlighted = Entry.Command.GetParameter ( Evado.UniForm.Model.EuCommandParameters.BG_Highlighted );

        if ( background == "" || background == "Null" ) background = background_Default;
        if ( alternative == "" || alternative == "Null" ) alternative = background_Alternative;
        if ( highlighted == "" || highlighted == "Null" ) highlighted = background_Highlighted;

        string title = Entry.Name.FullName;

        title = title.Replace ( "\r\n", "<br/>" );

        if ( bEventRow == false )
        {
          this.Html.AppendFormat ( "<span class=\"{0}\" "
             + "onmouseover=\"this.className='{1}'\" "
             + "onmouseout=\"this.className='{0}'\" "
             + "onclick=\"javascript:onPostBack('{2}')\">{3}</span>",
             background, highlighted, Entry.Command.Id, title );

          bEventRow = true;
        }
        else
        {
          this.Html.AppendFormat ( "<span class=\"{0}\" "
            + "onmouseover=\"this.className='{1}'\" "
            + "onmouseout=\"this.className='{0}'\" "
            + "onclick=\"javascript:onPostBack('{2}')\">{3}</span>",
            alternative, highlighted, Entry.Command.Id, title );

          bEventRow = false;
        }
      }
      else
      {
        this.Html.Append ( "<tr> "
        + "<td class=\"Header\" >" + Entry.Command.Title + "</span>" );
      }
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
