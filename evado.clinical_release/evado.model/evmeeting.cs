/***************************************************************************************
 * <copyright file="Evado.UniForm.Model\AbstractedPage.cs" company="EVADO HOLDING PTY. LTD.">
 *     
 *      Copyright (c) 2013 - 2021 EVADO HOLDING PTY. LTD.  All rights reserved.
 *     
 *      The use and distribution terms for this software are contained in the file
 *      FieldIdd \license.txt, which can be found in the root of this distribution.
 *      By using this software in any fashion, you are agreeing to be bound by the
 *      terms of this license.
 *     
 *      You must not remove this notice, or any other, from this software.
 *     
 * </copyright>
 * 
 * Description: 
 *  This class contains the AbstractedPage data object.
 *
 ****************************************************************************************/
using System;
using System.Collections.Generic;

using static System.Collections.Specialized.BitVector32;

namespace Evado.Model
{
  /// <summary>
  /// This class defines the method parameter object structure.
  /// </summary>
  [Serializable]
  public class EvMeeting
  {
    #region Class constants

    /// <summary>
    /// This const define the meeting status field identifier.
    /// </summary>
    public const String CONST_MEETING_ACTION = "Meeting_Action";
    public const String CONST_MEETING_STATUS = "Meeting_Status";
    public const String CONST_MEETING_GUID = "Meeting_Guid";
    public const String CONST_VISIT_GUID = "Visit_Guid";

    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region Class Enumerations
    /// <summary>
    /// This enumeration list defines the meeting statuses.
    /// </summary>
    public enum States
    {
      /// <summary>
      /// This enumeration defines not selected state or null value.
      /// </summary>
      Null = 0,

      /// <summary>
      /// This enumeration value defines a meeting confirmed but not started status
      /// </summary>
      Meeting_Scheduled,

      /// <summary>
      /// This enumeration value defines a meeting has commenced status.
      /// </summary>
      Meeting_Commenced,

      /// <summary>
      /// This enumeration value defines a meeting has closed status.
      /// </summary>
      Meeting_Closed,

      /// <summary>
      /// This enumeration value defines a meeting has cancelled status.
      /// </summary>
      Meeting_Cancelled,
    }

    /// <summary>
    /// This enumeration list defines the meeting actions.
    /// </summary>
    public enum Actions
    {
      /// <summary>
      /// This enumeration defines not selected state or null value.
      /// </summary>
      Null = 0,

      /// <summary>
      /// This enumeration value defines the meeting request action.
      /// </summary>
      Create_Meeting = 1,

      /// <summary>
      /// This enumeration value defines the confirms the meeting request action.
      /// </summary>
      Schedule_Meeting = 2,

      /// <summary>
      /// This enumeration value defines commence the meeting action.
      /// </summary>
      Commence_Meeting = 3,

      /// <summary>
      /// This enumeration value defines close the meeting action.
      /// </summary>
      Close_Meeting = 4,

      /// <summary>
      /// This enumeration value defines cancel meeting action
      /// </summary>
      Cancel_Meeting = 5,

      /// <summary>
      /// This action saves the meeting.
      /// </summary>
      Save_Meeting = 6,
    }

    /// <summary>
    /// This enumeration list defines the types of meeting
    /// </summary>
    public enum Types
    {
      /// <summary>
      /// This enumeration defines null value or not selection state.
      /// </summary>
      Null,

      /// <summary>
      /// This enumeration defines a account holder meeting
      /// </summary>
      Virtual_Consultation,

      /// <summary>
      /// This enumeration defines a account holder meeting
      /// </summary>
      Site_Meeting,

      /// <summary>
      /// This enumeration defines a trial manager meeting
      /// </summary>
      Team_Meeting,
    }

    /// <summary>
    /// This enumeration list defines the field names of meeting
    /// </summary>
    public enum FieldNames
    {
      /// <summary>
      /// This enumeration defines null value or non selection state
      /// </summary>
      Null,

      /// <summary>
      /// This enumeration defines a trial identifier field name of an meeting
      /// </summary>
      TrialId,

      /// <summary>
      /// This enumeration defines an meeting identifier field name of an meeting
      /// </summary>
      OrgId,

      /// <summary>
      /// This enumeration defines the title of the meeting 
      /// </summary>
      Subject,

      /// <summary>
      /// This enumeration defines the meeting type
      /// </summary>
      Type,

      /// <summary>
      /// This enumeration defines the meeting attendee list
      /// </summary>
      AttendeeList,

      /// <summary>
      /// This enumeration defines the patient identifier reference
      /// </summary>
      PatientId,

      /// <summary>
      /// This enumeration defines the subject identifier reference
      /// </summary>
      SubjectId,

      /// <summary>
      /// This enumeration defines the visit identifier reference
      /// </summary>
      VisitId,

      /// <summary>
      /// This enumeration defines a country field name of an meeting
      /// </summary>
      Description,

      /// <summary>
      /// This enumeration defines a telephone field name of an meeting
      /// </summary>
      Telephone,

      /// <summary>
      /// This enumeration defines a fax phone field name of an meeting
      /// </summary>
      Fax_Phone,

      /// <summary>
      /// This enumeration defines an email field name of an meeting
      /// </summary>
      HostEmailAddress,

      /// <summary>
      /// This enumeration defines a state field name of an meeting
      /// </summary>
      State,

      /// <summary>
      /// This enumeration defines the meeting datetime value
      /// </summary>
      DateTime,

      /// <summary>
      /// This enumeration defines the meeting date value
      /// </summary>
      MeetingDate,

      /// <summary>
      /// This enumeration defines the meeting time value
      /// </summary>
      MeetingTime,
    }

    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region Class Class Property List


    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region public properties

    /// <summary>
    /// This property contains a global unique identifier of an meeting
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// This property contains an customer global unique identifier of an meeting
    /// This foreign key links the organisation to the customer object.
    /// </summary>
    public Guid CustomerGuid { get; set; }

    /// <summary>
    /// This property contains a trial identifier of an meeting
    /// </summary>
    public string TrialId { get; set; } = String.Empty;

    /// <summary>
    /// This property contains an organisatin identifier of an meeting
    /// </summary>
    public string OrgId { get; set; } = String.Empty;

    /// <summary>
    /// This property contains the meeting date
    /// </summary>
    public DateTime DateTime { get; set; } = EvStatics.CONST_DATE_NULL;

    public bool DateChanges = false;

    String date = String.Empty;
    /// <summary>
    /// This property returns the date string.
    /// </summary>
    public String Date
    {
      get
      {
        date = DateTime.ToString ( "dd-MMM-yyyy" );
        return date;
      }
      set
      {
        date = value;

        this.DateTime = EvStatics.getDateTime ( date + " " + time );
      }
    }

    String time = String.Empty;
    /// <summary>
    /// This property returns the time string.
    /// </summary>
    public String Time
    {
      get
      {
        time = DateTime.ToString ( "HH:mm" );
        return time;
      }
      set
      {
        time = value;

        this.DateTime = EvStatics.getDateTime ( date + " " + time );
      }
    }

    /// <summary>
    /// This property contains a name of an meeting
    /// </summary>
    public string Subject { get; set; } = String.Empty;

    /// <summary>
    /// This property contains the meeting type value.
    /// </summary>
    public EvMeeting.Types Type { get; set; } = Types.Null;  

List<EvEmailAddress> _AttendeeEmailAddressList = new List<EvEmailAddress> ( );
    /// <summary>
    /// This property contains a list of the attendees Email addresses
    /// </summary>
    public List<EvEmailAddress> AttendeeEmailAddressList
    {
      get
      {
        return this._AttendeeEmailAddressList;
      }

      set
      {
        this._AttendeeEmailAddressList = value;

        this.Attendees = new String [ this._AttendeeEmailAddressList.Count ];

        for ( int i = 0; i < this._AttendeeEmailAddressList.Count; i++ )
        {
          this.Attendees [ i ] = this._AttendeeEmailAddressList [ i ].DisplayName;
        }
      }
    }

    /// <summary>
    /// This property contains a list of the attendees Email addresses 
    /// as a delimited string.
    /// </summary>
    public String AttendeeEmailAddresses
    {
      get
      {
        string emailaddresses = String.Empty;

        if ( this.AttendeeEmailAddressList.Count == 0 )
        {
          return String.Empty;
        }

        foreach ( EvEmailAddress address in this.AttendeeEmailAddressList )
        {
          if ( emailaddresses != String.Empty )
          {
            emailaddresses += ";";
          }
          emailaddresses += address.DelmitedEmailAddress;
        }
        return emailaddresses;
      }
      set
      {
        this.AttendeeEmailAddressList = new List<EvEmailAddress> ( );
        if ( value == String.Empty )
        {
          return;
        }

        string [ ] arrValue = value.Split ( ';' );

        foreach ( String str in arrValue )
        {
          this.AttendeeEmailAddressList.Add ( new EvEmailAddress ( str ) );
        }
      }
    }

    EvEmailAddress _HostEmailAddress = new EvEmailAddress ( );
    /// <summary>
    /// This property contains a list of the host's Email addresses
    /// </summary>
    public EvEmailAddress HostEmailAddress { get; set; } = new EvEmailAddress ();

    /// <summary>
    /// This property contains the patient identifier 
    /// </summary>
    public string PatientId { get; set; } = String.Empty;

    /// <summary>
    /// This property contains the subject identifier
    /// </summary>
    public string SubjectId { get; set; } = String.Empty;

    /// <summary>
    /// This property contains the visit identifier
    /// </summary>
    public string VisitId { get; set; } = String.Empty;

    /// <summary>
    /// This property contains the meeting description
    /// </summary>
    public string Description { get; set; } = String.Empty;

    /// <summary>
    /// This property contains a telephone of an meeting
    /// </summary>
    public string Telephone { get; set; } = String.Empty;

    /// <summary>
    /// This property contains a summary of an meeting
    /// </summary>
    public string LinkText
    {
      get
      {
        String stLinkText = String.Format ( EvmLabels.Meeting_Subject_Link_Text, this.Subject, this.Date, this.Time );

        if ( this.Type != Types.Null )
        {
          stLinkText += String.Format ( EvmLabels.Meeting_Type_Link_Text, this.Type.ToString ( ).Replace ( "_", " " ) );
        }

        if ( this.Status != EvMeeting.States.Null )
        {
          stLinkText += String.Format ( EvmLabels.Meeting_Status_Link_Text, this.Status.ToString ( ).Replace ( "_", " " ) );
        }

        return stLinkText;
      }
    }

    /// <summary>
    /// This property contains an updated string of an meeting
    /// </summary>
    public string UpdatedBy { get; set; } = String.Empty;

    /// <summary>
    /// This property contains the updated date 
    /// </summary>
    public DateTime UpdatedDate { get; set; } = EvStatics.CONST_DATE_NULL;

    /// <summary>
    /// This property contains a user identifier of those who updates an meeting
    /// </summary>
    public string UpdatedByUserId { get; set; } = String.Empty;

    /// <summary>
    /// This property contains a state object of an meeting
    /// </summary>
    public States Status { get; set; } = States.Null;

    /// <summary>
    /// This property contains save action setting for the object.
    /// </summary>

    public Actions Action { get; set; } = Actions.Null;

    #endregion

    #region Class Whereby Property List

    /// <summary>
    /// This property defines the meeting room identifier.
    /// </summary>
    public String MeetingId { get; set; } = String.Empty;

    /// <summary>
    /// This property defines the meeting room identifier.
    /// </summary>
    public String RoomName { get; set; } = String.Empty;

    DateTime _EndDate = EvStatics.CONST_DATE_NULL;
    /// <summary>
    /// This property defines the meeting end date
    /// </summary>
    public DateTime EndDate
    {
      get {
        return this._EndDate;
      }
      set {

        this._EndDate = value;

        if ( this._EndDate < DateTime.Now )
        {
          this._EndDate = DateTime.Now.AddDays ( 1 );
        }

      }
    } 


    
    /// <summary>
    /// This property defines the room URL
    /// </summary>
    public String RoomUrl { get; set; } = String.Empty;

    /// <summary>
    /// This property defines the host URL
    /// </summary>
    public String HostRoomUrl { get; set; } = String.Empty;

    /// <summary>
    /// This property defines the Host Name.
    /// </summary>
    public String HostName
    {
      get
      {
        return this.HostEmailAddress.DisplayName;
      }
    }

    /// <summary>
    /// This property defines if the current name is host.
    /// </summary>
    public bool IsHost { get; set; }

    string [ ] attendees = new String [ 0 ];
    /// <summary>
    /// This property defines the array of attendees
    /// </summary>
    public String [ ] Attendees
    {
      get
      {
        if ( attendees.Length != this._AttendeeEmailAddressList.Count )
        {
          attendees = new String [ this._AttendeeEmailAddressList.Count ];

          for ( int i = 0; i < this._AttendeeEmailAddressList.Count; i++ )
          {
            attendees [ i ] = this._AttendeeEmailAddressList [ i ].DisplayName;
          }
        }

        return attendees;
      }
      set
      {
        attendees = value;
      }
    }

    /// <summary>
    /// This property defines client user name
    /// </summary>
    public String DisplayName { get; set; } = String.Empty;


    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region public methods

    // ==================================================================================
    /// <summary>
    /// This method returns the meeting data as text.
    /// </summary>
    /// <returns> String containing the meeting data.</returns>
    // ----------------------------------------------------------------------------------
    public String GetMeetingValues ( )
    {
      // 
      // Create the customer name object
      // 
      System.Text.StringBuilder fieldValue = new System.Text.StringBuilder ( );

      fieldValue.AppendFormat ( "Action '{0}'.\r\n", this.Action );
      fieldValue.AppendFormat ( "Type '{0}'.\r\n", this.Type );
      fieldValue.AppendFormat ( "Status '{0}'.\r\n", this.Status );
      fieldValue.AppendFormat ( "Subject '{0}'.\r\n", this.Subject );
      fieldValue.AppendFormat ( "Date '{0}'.\r\n", this.Date );
      fieldValue.AppendFormat ( "Time '{0}'.\r\n", this.Time );
      fieldValue.AppendFormat ( "DateTime '{0}'.\r\n", this.DateTime );
      fieldValue.AppendFormat ( "EndDate '{0}'.\r\n", this.EndDate );
      string list = String.Empty;
      foreach ( string str in this.Attendees )
      {
        if ( list != String.Empty )
        {
          list += ", ";
        }
        list += str;
      };
      fieldValue.AppendFormat ( "Attendees {0}.\r\n", list );
      fieldValue.AppendFormat ( "MeetingId '{0}'.\r\n", this.MeetingId );
      fieldValue.AppendFormat ( "RoomName '{0}'.\r\n", this.RoomName );
      fieldValue.AppendFormat ( "DisplayName '{0}'.\r\n", this.DisplayName );
      fieldValue.AppendFormat ( "HostRoomUrl '{0}'.\r\n", this.HostRoomUrl );
      fieldValue.AppendFormat ( "RoomUrl '{0}'.\r\n", this.RoomUrl );
      fieldValue.AppendFormat ( "HostEmailAddress.Text '{0}'.\r\n", this.HostEmailAddress.TextEmailAddress );
      fieldValue.AppendFormat ( "HostEmailAddress.FullAddress '{0}'.\r\n", this.HostEmailAddress.DelmitedEmailAddress );
      fieldValue.AppendFormat ( "AttendeeEmailAddresses {0}.\r\n", this.AttendeeEmailAddresses );
      fieldValue.AppendFormat ( "TrialId '{0}'.\r\n", this.TrialId );
      fieldValue.AppendFormat ( "OrgId '{0}'.\r\n", this.OrgId );
      fieldValue.AppendFormat ( "PatientId '{0}'.\r\n", this.PatientId );
      fieldValue.AppendFormat ( "SubjectId '{0}'.\r\n", this.SubjectId );
      fieldValue.AppendFormat ( "VisitId '{0}'.\r\n", this.VisitId );
      fieldValue.AppendFormat ( "Telephone '{0}'.\r\n", this.Telephone );
      fieldValue.AppendFormat ( "LinkText '{0}'.\r\n", this.LinkText );

      return fieldValue.ToString ( );
    }

    // ==================================================================================
    /// <summary>
    /// This method updates the meeting object content.
    /// </summary>
    /// <param name="Meeting">EuMeeting object</param>
    // ----------------------------------------------------------------------------------
    public void updateMeeting ( EvMeeting Meeting )
    {
      this.CustomerGuid = Meeting.CustomerGuid;
      this.MeetingId = Meeting.MeetingId;
      this.RoomUrl = Meeting.RoomUrl;
      this.HostRoomUrl = Meeting.HostRoomUrl;
      this.Attendees = Meeting.Attendees;
      this.Status = Meeting.Status;
    }

    // ==================================================================================
    /// <summary>
    /// This method creates the WhereBy meeting room request object
    /// </summary>
    /// <returns>WbMeetingRequest object</returns>
    // ----------------------------------------------------------------------------------
    public WbMeetingRequest CreateMeetingRequest ( )
    {
      WbMeetingRequest meetingRequest = new WbMeetingRequest ( );

      meetingRequest.roomNamePrefix = this.CreateRoomPrefix ( );
      meetingRequest.startDate = this.DateTime;
      meetingRequest.endDate = this.EndDate;

      return meetingRequest;
    }
    // ==================================================================================
    /// <summary>
    /// This method save the WhereBy meeting room response object.
    /// </summary>
    /// <param name="MeetingResponse">WbMeetingResponse object</param>
    // ----------------------------------------------------------------------------------
    public void UpdateMeetingResponse ( WbMeetingResponse MeetingResponse )
    {
      this.MeetingId = MeetingResponse.MeetingId;
      this.RoomName = MeetingResponse.roomName;
      this.EndDate = MeetingResponse.endDate;
      this.DateTime = MeetingResponse.startDate;
      this.HostRoomUrl = MeetingResponse.hostRoomUrl;
      this.RoomUrl = MeetingResponse.roomUrl;
    }

    // ==================================================================================
    /// <summary>
    /// This private method creates the meeting room prefix
    /// </summary>
    /// <returns>String object </returns>
    // ----------------------------------------------------------------------------------
    private String CreateRoomPrefix ( )
    {
      String roomPrefix = String.Empty;

      switch ( this.Type )
      {
        case Types.Site_Meeting:
          {
            roomPrefix = "sm-";
            break;
          }
        case Types.Team_Meeting:
          {
            roomPrefix = "tm-";
            break;
          }
        default:
        case Types.Virtual_Consultation:
          {
            roomPrefix = "vc-";
            break;
          }
      }
      return roomPrefix;
    }

    // ==================================================================================
    /// <summary>
    /// This method sets the field value.
    /// </summary>
    /// <param name="FieldName">EvMeeting.OrganisationFieldNames: a field name object</param>
    /// <param name="Value">string: a Value for updating</param>
    /// <remarks>
    /// This method consists of the following steps:
    /// 
    /// 1. Initialize the internal variables
    /// 
    /// 2. Switch the FieldName and update the Value on the meeting field names.
    /// </remarks>
    //  ---------------------------------------------------------------------------------
    public void setValue ( EvMeeting.FieldNames FieldName, string Value )
    {
      //
      // Switch the FieldName and update the Value on the meeting field names.
      //
      switch ( FieldName )
      {
        case EvMeeting.FieldNames.DateTime:
          {
            this.DateTime = EvStatics.getDateTime ( Value );
            return;
          }
        case EvMeeting.FieldNames.MeetingDate:
          {
            this.Date = Value;
            return;
          }
        case EvMeeting.FieldNames.MeetingTime:
          {
            this.Time = Value;
            return;
          }
        case EvMeeting.FieldNames.Subject:
          {
            this.Subject = Value;
            return;
          }
        case EvMeeting.FieldNames.Description:
          {
            this.Description = Value;
            return;
          }
        case EvMeeting.FieldNames.TrialId:
          {
            this.TrialId = Value;
            return;
          }
        case EvMeeting.FieldNames.OrgId:
          {
            this.OrgId = Value;
            return;
          }
        case EvMeeting.FieldNames.PatientId:
          {
            this.PatientId = Value;
            return;
          }
        case EvMeeting.FieldNames.VisitId:
          {
            this.VisitId = Value;
            return;
          }
        case EvMeeting.FieldNames.SubjectId:
          {
            this.SubjectId = Value;
            return;
          }
        case EvMeeting.FieldNames.Telephone:
          {
            this.Telephone = Value;
            return;
          }
        case EvMeeting.FieldNames.HostEmailAddress:
          {
            this.HostEmailAddress = new EvEmailAddress ( Value );
            return;
          }

        case EvMeeting.FieldNames.Type:
          {
            this.Type = Evado.Model.EvStatics.parseEnumValue<Types> ( Value );
            return;
          }

        case EvMeeting.FieldNames.State:
          {
            this.Status = Evado.Model.EvStatics.parseEnumValue<States> ( Value );
            return;
          }

        default:

          return;

      }//END Switch

    }//END setValue method 

    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region Static methods
    /// <summary>
    /// This method returnes a list of options containing organisation types.
    /// </summary>
    /// <param name="CurrentType">Current organisation type</param>
    /// <returns></returns>
    public static List<Evado.Model.EvOption> getTypeList ( )
    {
      return EvStatics.getOptionsFromEnum ( typeof ( EvMeeting.Types ), true );

    }//END static method.

    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion
  }
}//END  Evado.Model namespase