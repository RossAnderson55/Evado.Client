/***************************************************************************************
 * <copyright file="Evado.Model.UniForm\BLL\EvApplicationEvents.cs" company="EVADO HOLDING PTY. LTD.">
 *     
 *      Copyright (c) 2001 - 2012 EVADO HOLDING PTY. LTD..  All rights reserved.
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
using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;

namespace Evado.Model.UniForm
{
  /// <summary>
  /// A business to manage EvApplicationEvents. This class uses EvApplicationEvent data object for its content.
  /// </summary>
  public class EvApplicationEvents
  {
    #region Initialise class objects and variables
    /// <summary>
    /// Create instantiate the DAL class 
    /// </summary>
    public static string _eventLogSource = ConfigurationManager.AppSettings [ "EventLogSource" ];
    private static string Site = ConfigurationManager.AppSettings [ "WebSite" ];

    private string _Status = String.Empty;
    /// <summary>
    /// This proprty contains the a status of an application event.
    /// </summary>
    public string Status
    {
      get
      {
        return _Status;
      }
    }
    public string HtmlStatus
    {
      get { return _Status.Replace( "\r\n", "<br/>" ); }
    }

    //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region Add Evado.Model.EvEventCodes methods

    // =====================================================================================
    /// <summary>
    /// This method adds EvApplicationEvent data object in the database.
    /// </summary>
    /// <param name="Category">String: Event category</param>
    /// <param name="Description">String: Description</param>
    /// <param name="EventId">EvEventCodes: Object Evado.Model.EvEventCodes </param>
    /// <param name="UserName">String: UserName of a person</param>
    /// <returns>A boolean value</returns>
    /// <remarks>
    /// This method consists of following steps. 
    /// 
    /// 1. Write to the event log.
    /// 
    /// 2. Return a boolean value. 
    /// 
    /// </remarks>
    // -------------------------------------------------------------------------------------
    public static bool LogInformation(
      string Description, Evado.Model.EvEventCodes EventId, string Category, string UserName )
    {
      //
      // Create the event comment content
      // 
      string sEventContent = "EventId: " + EventId.ToString( )
        + "\r\nEvent Category: " + Category
        + "\r\nUserId:  " + UserName
        + "\r\n\r\nDescription:\r\n" + Description;

      // 
      // Write the event comment to the event log.
      // 
      WriteLog( sEventContent, EventLogEntryType.Information );

      //
      // Return a boolean value
      //
      return true;

    } //END LogInformation method

    // =====================================================================================
    /// <summary>
    /// This method adds EvApplicationEvent data object in the database.
    /// </summary>
    /// <param name="Category">String: Event category</param>
    /// <param name="Description">String: Description</param>
    /// <param name="Url">String: Page Url</param>
    /// <param name="UserName">String: UserName of a person</param>
    /// <returns>A boolean value </returns>
    /// <remarks>
    /// This method consists of following steps. 
    /// 
    /// 1. Write to the event log.
    /// 
    /// 2. Return a boolean value.
    /// 
    /// </remarks>
    // -------------------------------------------------------------------------------------
    public static bool LogInformation(
      string Description, string Category, string Url, string UserName )
    {
      string sEventContent = "Event Category: " + Category
        + "\r\nPageUrl: " + Url
        + "\r\nUserId:  " + UserName
        + "\r\n\r\nDescription:\r\n" + Description;
      // 
      // Write to the event log
      // 
      WriteLog( sEventContent, EventLogEntryType.Information );

      // 
      // return a boolean value
      // 
      return true;

    }//END LogInformation method

    // =====================================================================================
    /// <summary>
    /// This method adds EvApplicationEvent data object in the database.
    /// </summary>
    ///<param name="Category">String: Event category </param>
    ///<param name="Description">String: Description</param>
    ///<param name="Url">String: Page Url</param>
    ///<param name="UserName">String: UserName of a person</param>
    ///<returns> A boolean value</returns>
    ///<remarks>
    /// This method consists of following steps. 
    /// 
    /// 1. Write to the event log. 
    /// 
    /// 2. Return a boolean value.
    /// 
    /// </remarks>
    // -------------------------------------------------------------------------------------
    public static bool LogAction(
      string Description, string Category, string Url, string UserName )
    {
      // 
      // Create the event comment content. 
      // 
      string sEventContent = "Event Category: " + Category
+ "\r\nPageUrl: " + Url
+ "\r\nUserId:  " + UserName
+ "\r\n\r\nDescription:\r\n" + Description;

      // 
      // Write to the event log
      // 
      WriteLog( sEventContent, EventLogEntryType.Information );

      // 
      // Return a boolean value
      // 
      return true;

    }//END LogAction method

    // =====================================================================================
    /// <summary>
    /// This method adds EvApplicationEvent data object in the database.
    /// </summary>
    /// <param name="Category">String: Event category </param>
    /// <param name="Description">String: Description </param>
    /// <param name="EventId">EvEventCodes: Object Evado.Model.EvEventCodes</param>
    /// <param name="UserName">String: Username of a person</param>
    /// <returns>A boolean value </returns>
    /// <remarks>
    /// This method consists of following steps.
    /// 
    /// 1. Write to the event log.
    /// 
    /// 2. Return a boolean value. 
    /// 
    /// </remarks>
    // -------------------------------------------------------------------------------------
    public static bool LogAction(
      string Description, Evado.Model.EvEventCodes EventId, string Category, string UserName )
    {
      // 
      // Create the event comment content. 
      // 
      string sEventContent = "EventId: " + EventId.ToString( )
        + "\r\nEvent Category: " + Category
        + "\r\nUserId: " + UserName
        + "\r\n\r\nDescription:\r\n" + Description;

      // 
      // Write to the event log
      // 
      WriteLog( sEventContent, EventLogEntryType.Information );

      // 
      // Return a boolean value
      // 
      return true;

    }//END LogAction method 
    // =====================================================================================
    /// <summary>
    /// This method adds EvApplicationEvent data object in the database.
    /// </summary>
    /// <param name="Description">String: Description </param>
    /// <returns> A boolean value</returns>
    /// <remarks>
    /// This method consists of following step. 
    /// 
    /// 1. Write to the event log.
    /// 
    /// 2. Return a boolean value.
    /// 
    /// </remarks>
    // -------------------------------------------------------------------------------------
    public static bool LogAction(
      string Description )
    {
      // 
      // Write to the event log
      // 
      WriteLog( Description, EventLogEntryType.Information );

      // 
      // Return a boolean value
      // 
      return true;

    }//END LogAction method

    // =====================================================================================
    /// <summary>
    /// This method adds EvApplicationEvent data object in the database.
    /// </summary>
    /// <param name="Category">String: Event Category</param>
    /// <param name="Description">String: Description</param>
    /// <param name="EventId">EvEventCodes: Object Evado.Model.EvEventCodes </param>
    /// <param name="Url">String: Page Url</param>
    /// <param name="UserName">String: Username of a person </param>
    /// <returns> A boolean value </returns>
    ///<remarks>
    /// This method consists of following steps. 
    /// 
    /// 1. Initialise a string Error to the Category if Category is empty.
    /// 
    /// 2. Write to the event log.
    /// 
    /// 3. Return a boolean value. 
    /// 
    /// </remarks> 

    // -------------------------------------------------------------------------------------
    public static bool LogError(
      Evado.Model.EvEventCodes EventId, string Category, string Description, string Url, string UserName )
    {

      // 
      // Initialise a string Error to the Category if Category is empty.
      // 
      if ( Category == String.Empty )
      {
        Category = "Error";
      }
      
      string sEventContent = "EventId: " + EventId.ToString( )
    + "\r\nEvent Category: " + Category
    + "\r\nPageUrl: " + Url
    + "\r\nUserId:  " + UserName
    + "\r\n\r\nDescription:\r\n" + Description;

      // 
      // Write to the event log
      // 
      WriteLog( sEventContent, EventLogEntryType.Error );
      // 
      // Return a boolean value
      // 
      return true;

    }//END LogError method 

    // =====================================================================================
    /// <summary>
    /// This method adds EvApplicationEvent data object in the database.
    /// </summary>
    /// <param name="Category">String: Event category </param>
    /// <param name="Description">String: Description</param>
    /// <returns> A boolean value </returns>
    /// <remarks>
    /// This method consists of following steps. 
    /// 
    /// 1. Initialise a string Error to the Category if Category is empty.
    /// 
    /// 2. Write to the event log.
    /// 
    /// 3. Return a boolean value. 
    /// 
    /// 4. Return a boolean value. 
    /// 
    /// </remarks>
    // -------------------------------------------------------------------------------------
    public static bool LogError( string Category, string Description )
    {

      Evado.Model.EvEventCodes iReturn = Evado.Model.EvEventCodes.Ok;
      // 
      // Initialise a string value Error to the Category if the Caterogy is empty.
      // 
      // Initialise a string Error to the Category if Category is empty.
      //
      if ( Category == String.Empty )
      {
        Category = "Error";
      }

      //
      // Create the event comment content. 
      // 
      string sEventContent = "Event Category: " + Category
        + "\r\n\r\nDescription:\r\n" + Description;

      // 
      // Write to the event log
      // 
      WriteLog( sEventContent, EventLogEntryType.Error );

      // 
      // Return a boolean value
      // 
      return true;

    }//END LogError method 

    // =====================================================================================
    /// <summary>
    /// This method adds EvApplicationEvent data object in the database.
    /// </summary>
    /// <param name="Description">String: Description</param>
    /// <returns> A boolean value </returns>
    /// <remarks>
    /// This method consists of follwoing steps. 
    /// 
    /// 1. Write to the event log. 
    /// 
    /// 2. Return a boolean value. 
    /// 
    /// </remarks>
    // -------------------------------------------------------------------------------------
    public static bool LogError( string Description )
    {
      // 
      // Write to the event log
      // 
      WriteLog( Description, EventLogEntryType.Error );

      // 
      // Return a boolean value
      // 
      return true;

    }//END LogError method 

    // =====================================================================================
    /// <summary>
    /// This method adds EvApplicationEvent data object in the database.
    /// </summary>  
    /// <param name="Category">String: Event category </param>
    /// <param name="Description">String: Description</param>
    /// <param name="EventId">Int: Event id</param>
    /// <param name="Url">String: Page Url </param>
    /// <param name="UserName"> Username of a person</param>
    /// <returns> A boolean value </returns>
    /// <remarks>
    /// This method consists of follwing steps. 
    /// 
    /// 1. Initialise a string Warning to the Category if Category is empty.
    /// 
    /// 2. Write to the event log.
    /// 
    /// 3. Return a boolean value. 
    /// 
    /// </remarks>
    // -------------------------------------------------------------------------------------
    public static bool LogWarning(
      int EventId, string Category, string Description, string Url, string UserName )
    {

      // 
      // Initialise a string Warning to the Category if Category is empty.
      //
      if ( Category == String.Empty )
      {
        Category = "Warning";
      }
      //
      // Create the event comment content. 
      //
      string sEventContent = "EventId: " + EventId.ToString( )
  + "\r\nEvent Category: " + Category
  + "\r\nPageUrl: " + Url
  + "\r\nUserId:  " + UserName
  + "\r\n\r\nDescription:\r\n" + Description;

      // 
      // Write to the event log
      // 
      WriteLog( sEventContent, EventLogEntryType.Warning );

      //
      // Return a boolean value. 
      //
      return true;

    }//END LogWarning method 

    // =====================================================================================
    /// <summary>
    /// This method logs event information.
    /// </summary>
    /// <param name="Category">String: Event category</param>
    /// <param name="Description">String: Description</param>
    /// <returns> A boolean value</returns>
    /// <remarks>
    /// This method consists of following steps. 
    /// 
    /// 1. Initialise string Error to the category if the Category is empty.
    /// 
    /// 2. Create the event comment content. 
    /// 
    /// 3. Return a boolean value.
    /// 
    /// </remarks>
    // -------------------------------------------------------------------------------------
    public static bool LogWarning( string Category, string Description )
    {
      // 
      // Initialise a string Error to the Category if Category is empty.
      //
      if ( Category == String.Empty )
      {
        Category = "Error";
      }

      //
      // Create the event comment content. 
      //
      string sEventContent = "Event Category: " + Category
        + "\r\n\r\nDescription:\r\n" + Description;

      // 
      // Write to the event log
      // 
      WriteLog( sEventContent, EventLogEntryType.Warning );

      //
      // Return a boolean value. 
      //
      return true;

    }//END LogWarning method

    //  ===========================================================================
    /// <summary>
    /// 
    /// This method writes log in a database
    /// </summary>
    /// <param name="EventContent">String: Event content </param>
    /// <param name="Type">EventLogEntryType: Object EventLogEntryType </param>
    /// <returns>If the event source was created successfully true is returned, otherwise false.</returns>
    /// <remarks>
    /// This method consists of following steps. 
    /// 
    /// 1. If Event content lenght is less than 30,000 writes an event log. 
    /// 
    /// 2. Else split the event content into seperate blocks with each block contains less than 30,000 characters. 
    /// 
    /// 3. Writes event logs in a database.
    /// 
    /// </remarks>
    /// 
    //  ---------------------------------------------------------------------------------
    public static void WriteLog( string EventContent, EventLogEntryType Type )
    {

      //  If length of an event content is less than 30,000 then
      //  Writes an entry with the given message text, application-defined event identifier,
      //  and application-defined category to the event log.
      //
      if ( EventContent.Length < 30000 )
      {
        EventLog.WriteEntry( _eventLogSource, EventContent, Type );

        return;

      }//END less than 30000

      // If length of an event is greater than 30,000 then
      // Writes an entry with the given message text, application defined event identifier, 
      // and application-defined category to the event log in separate blocks of 30,000 lenght.
      //    
      int inLength = 30000;

      for ( int inStartIndex = 0; inStartIndex < EventContent.Length; inStartIndex += 30000 )
      {
        if ( EventContent.Length - inStartIndex < inLength )
        {
          inLength = EventContent.Length - inStartIndex;
        }

        string stContent = EventContent.Substring( inStartIndex, inLength );

        //
        // Writes event logs in a database
        //
        EventLog.WriteEntry( _eventLogSource, stContent, Type );

      }//END EventContent interation loop

    }//END WriteLog method 



    //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    // ===========================================  END CLASS CODE  ==============================

  } // Close EvApplicationEvents Class.


} // Close namespace Evado.Evado.UniForm.Test.Dal 
