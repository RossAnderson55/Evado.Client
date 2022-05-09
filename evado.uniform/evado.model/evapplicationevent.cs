/***************************************************************************************
 * <copyright file="ApplicationEvent.cs" company="EVADO HOLDING PTY. LTD.">
 *     
 *      Copyright (c) 2002 - 2022 EVADO HOLDING PTY. LTD.  All rights reserved.
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
 *  This class contains the ApplicationEvent data object.
 *
 ****************************************************************************************/

using System;
using System.Collections.Generic;

namespace Evado.Model
{
  /// <summary>
  /// This data class defines the Application Event object content
  /// </summary>
  [Serializable]
  public class EvApplicationEvent
  {
    #region class initialisation methods

    /// <summary>
    /// Default constructor
    /// </summary>
    public EvApplicationEvent ( )
    {
      this.EventId = -1;
      this.Type = EventType.Null;
      this.Category = String.Empty;
      this.Description = String.Empty;
      this.UserId = String.Empty;
      this.PlatformId = String.Empty;
      this.DateTime = EvStatics.CONST_DATE_NULL;
    }

    //====================================================================================
    /// <summary>
    /// Default constructor and initlaise the object.
    /// </summary>
    /// <param name="Type">EventType enumerated value</param>
    /// <param name="EventId">EvEventCodes enumerated value</param>
    /// <param name="Category">String: the category of the event</param>
    /// <param name="Description">String: the description of the event.</param>
    /// <param name="UserId">String: name of the user that created the event.</param>
    // ----------------------------------------------------------------------------------
    public EvApplicationEvent (
      EventType Type,
      Evado.Model.EvEventCodes EventId,
      String Category,
      String Description,
      String UserId )
    {
      this.EventId = (int) EventId;
      this.Type = Type;
      this.Category = Category;
      this.Description = Description;
      this.UserId = UserId;
    }
    //====================================================================================
    /// <summary>
    /// Default constructor and initlaise the object.
    /// </summary>
    /// <param name="Type">EventType enumerated value</param>
    /// <param name="EventId">EvEventCodes enumerated value</param>
    /// <param name="Category">String: the category of the event</param>
    /// <param name="Description">String: the description of the event.</param>
    /// <param name="UserId">String: name of the user that created the event.</param>
    // ----------------------------------------------------------------------------------
    public EvApplicationEvent (
      EventType Type,
      Evado.Model.EvEventCodes EventId,
      String Category,
      String Description,
      String SubjectId,
      String UserId )
    {
      this.EventId = (int) EventId;
      this.Type = Type;
      this.Category = Category;
      this.Description = Description;
      this.SubjectId = SubjectId;
      this.UserId = UserId;
    }
    #endregion

    #region Class enumerations

    /// <summary>
    /// Class Application Event type enumeration list.
    /// </summary>
    public enum EventType
    {
      /// <summary>
      /// This enumeration value defines event type null value
      /// </summary>
      Null,

      /// <summary>
      /// The enumeration value defines event type action
      /// </summary>
      Action,

      /// <summary>
      /// The enumeration value defines event type information
      /// </summary>
      Information,

      /// <summary>
      /// The enumeration value defines event type warning
      /// </summary>
      Warning,

      /// <summary>
      /// The enumeration value defines event type error
      /// </summary>
      Error,
    }

    #endregion

    #region Class  Properties Section


    /// <summary>
    /// This property defines application event's unique integer Identification
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// This property defines application event's unique integer Identification
    /// </summary>
    public Guid CustomerGuid { get; set; }

    /// <summary>
    /// This property defines application event identifier
    /// </summary>
    public int EventId { get; set; }

    /// <summary>
    /// This property contains date time stamp of the Application Event 
    /// </summary>
    public DateTime DateTime { get; set; }

    /// <summary>
    /// This property contains date of the Application Event
    /// </summary>
    public string Date
    {
      get
      {
        return DateTime.ToString ( "dd MMM yyyy" );
      }
    }

    /// <summary>
    /// This property contains time of the Application Event
    /// </summary>
    public string Time
    {
      get
      {
        return DateTime.ToString ( "HH:mm:ss" );
      }
    }

    /// <summary>
    /// This property contains event type of the Application Event
    /// </summary>
    public EventType Type { get; set; }

    /// <summary>
    /// This property contains category of the Application Event
    /// </summary>
    public string Category { get; set; }

    /// <summary>
    /// This property contains user name of the person that generated the Application Event
    /// </summary>
    public string UserId { get; set; }

    /// <summary>
    /// This property contains subject identifier when recording the Application Event referencing subject data 
    /// or patient data as patient always has a subject record.
    /// </summary>
    public string SubjectId { get; set; }

    /// <summary>
    /// This property contains event description of Application Event
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// This property contains platform of the Application Event
    /// </summary>
    public String PlatformId { get; set; }

    /// <summary>
    /// This property contains customer no of the Application Event
    /// </summary>
    public int CustomerId { get; set; }

    /// <summary>
    /// This property contains the content displayed in a list of ApplicationEvents.
    /// </summary>
    public String LinkText
    {
      get
      {

        String description = this.Description;
        Evado.Model.EvEventCodes code = Evado.Model.EvEventCodes.Ok;

        if ( description.Length > 100 )
        {
          description = this.Description.Substring ( 0, 100 );
        } 

        if ( this.EventId < 0 )
        {
          try
          {
            code = (EvEventCodes) this.EventId;
          }
          catch
          {
            code = Evado.Model.EvEventCodes.Ok;
          }
        }

        String stContent = Evado.Model.EvStatics.enumValueToString ( code )
          + " > "
          + this.DateTime.ToString ( "dd MMM yyyy HH:mm" )
          + " >> "
          + description + " ...";

        if ( this.UserId != String.Empty
          && stContent.Contains ( this.UserId ) == false )
        {
          stContent += Evado.Model.EvmLabels.Space_Open_Bracket
          + this.UserId
          + Evado.Model.EvmLabels.Space_Close_Bracket;
        }

        stContent = stContent.Replace ( "\r\n", " " );

        return stContent;
      }
    }

    #endregion

  } // Close class ApplicationEvent

} // Close namespace Evado.Model
