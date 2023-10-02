/***************************************************************************************
 * <copyright file="EvDataChange.cs" company="EVADO HOLDING PTY. LTD.">
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
 *  This class contains the EvDataChange data object.
 *
 ****************************************************************************************/


using System;
using System.Collections;
using System.Collections.Generic; 

namespace Evado.Model
{
  /// <summary>
  /// Business entity used to model accounts
  /// </summary>
  [Serializable]
  public class EvDataChange
  {
    #region Class enumerators

    /// <summary>
    /// This enumeration list defines the table names of data change
    /// </summary>
    public enum DataChangeTableNames
    {
 
      /// <summary>
      /// This enumeration value defines Data Change to be null 
      /// </summary>
      Null,

      /// <summary>
      /// This enumeration value defines Data Change on ApplicationSettings table
      /// </summary>
      EdApplicationSettings,

      /// <summary>
      /// This enumeration value defines Data Change on ancillary records table
      /// </summary>
      EvAncilliaryRecords,

      /// <summary>
      /// This enumeration value defines Data Change on Activities table
      /// </summary>
      EvActivities,

      /// <summary>
      /// This enumeration value defines Data Change on Alert table
      /// </summary>
      EvAlerts,

      /// <summary>
      /// This enumeration value defines Data Change on budtget table
      /// </summary>
      EvBudget,

      /// <summary>
      /// This enumeration value defines Data Change on budget item table
      /// </summary>
      EvBudgetItem,

      /// <summary>
      /// This enumeration value defines Data Change on budget item organisation table
      /// </summary>
      EvBudgetItemOrganization,

      /// <summary>
      /// This enumeration value defines Data Change on billing record table
      /// </summary>
      EvBillingRecord,

      /// <summary>
      /// This enumeration value defines Data Change on billing record item table
      /// </summary>
      EvBillingRecordItem,

      /// <summary>
      /// This enumeration value defines Data Change on common form table
      /// </summary>
      EvCommonForms,

      /// <summary>
      /// This enumeration value defines Data Change on common form fields table
      /// </summary>
      EvCommonFormFields,

      /// <summary>
      /// This enumeration value defines Data Change on common records table
      /// </summary>
      EvCommonRecords,

      /// <summary>
      /// This enumeration value defines Data Change on Data dictionary table
      /// </summary>
      EvDataDictionary,

      /// <summary>
      /// This enumeration value defines Data Change on ethic tracking table
      /// </summary>
      EvEthicsTracking,

      /// <summary>
      /// This enumeration value defines Data Change on form field selection table
      /// </summary>
      EvFormFieldSelectionLists,

      /// <summary>
      /// This emumeration value defines Data Change on form table
      /// </summary>
      EvForms,

      /// <summary>
      /// This enumeration value defines Data Change on form fields table
      /// </summary>
      EvFormFields,

      /// <summary>
      /// This enumeration value defines Data Change on subject groups table
      /// </summary>
      EvGroups,

      /// <summary>
      /// This enumeration value defines Data Change on milestones table
      /// </summary>
      EvMilestones,

      /// <summary>
      /// This enumeration value defines Data Change on organistions stable
      /// </summary>
      EvOrganisations,

      /// <summary>
      /// This enumeration value defines Data Change on particpants table
      /// </summary>
      EvPatients,

      /// <summary>
      /// This enumeration value defines Data Change on roles table
      /// </summary>
      EvRoles,

      /// <summary>
      /// This enumeration value defines Data Change on records table
      /// </summary>
      EvRecords,

      /// <summary>
      /// This enumeration value defines Data Change on report templates table
      /// </summary>
      EvReportTemplates,

      /// <summary>
      /// This enumeration value defines Data Change on report templates table
      /// </summary>
      EaAnalyticsTemplates,

      /// <summary>
      /// This enumeration value defines Data Change on Serious Adverse Event status table
      /// </summary>
      EvSaeStatus,

      /// <summary>
      /// This enumeration value defines Data Change on schedules table
      /// </summary>
      EvSchedules,

      /// <summary>
      /// This enumeration value defines Data Change on site profile table
      /// </summary>
      EvSiteProfile,

      /// <summary>
      /// This enumeration value defines Data Change on subject milestones table
      /// </summary>
      EvSubjectMilestones,

      /// <summary>
      /// This enumeration value defines Data Change on subject records table
      /// </summary>
      EvSubjectRecords,
      
      /// <summary>
      /// This enumeration value defines Data Change on subjects table
      /// </summary>
      EvSubjects,

      /// <summary>
      /// This enumeration value defines Data Change on trial organisations table
      /// </summary>
      EvTrialOrganisations,

      /// <summary>
      /// This enumeration value defines Data Change on trial roles table
      /// </summary>
      EvTrialRoles,

      /// <summary>
      /// This enumeration value defines Data Change on trials table
      /// </summary>
      EvTrials,

      /// <summary>
      /// This enumeration value defines Data Change on userprofiles table
      /// </summary>
      EvUserProfiles,

      /// <summary>
      /// This enumerated value defines the data change for activie directory services.
      /// </summary>
      ActiveDirectoryServices,

      EdSelectionList,

      /// <summary>
      /// This enumeration value defines Data Change on site profile table
      /// </summary>
      EdAdapterSettings,

      /// <summary>
      /// This enumeration value definesPage Layouts on form selection fields table
      /// </summary>
      EdPageLayouts,

      /// <summary>
      /// This enumeration value defines the Meetings fields table.
      /// </summary>
      EvMeetings
    }

    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region Class constants

    /// <summary>
    /// This constant defines the names of the common table
    /// </summary>
    public const string CommonTableNames = "AdverseEvents;ConcomitantMedications;SeriousAdverseEvents";

    /// <summary>
    /// This constrant defines the common record
    /// </summary>
    public const string EvCommonRecord = "EvCommonRecord";

    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region private members

    // Internal member variables
    private Guid _Guid = Guid.Empty;
    private Guid _RecordGuid = Guid.Empty;
    private long _Uid = 0;
    private long _RecordUid = 0;
    private DataChangeTableNames _TableName = DataChangeTableNames.Null;
    private string _TrialId = String.Empty;
    private string _SubjectId = String.Empty;
    private string _RecordId = String.Empty;
    private string _UserId = String.Empty;
    private DateTime _DateStamp = Evado.Model.EvStatics.CONST_DATE_NULL;

    private List<EvDataChangeItem> _Items = new List<EvDataChangeItem>();

    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region public properties

    /// <summary>
    /// This property defines Unique global identifier of Data Change object
    /// </summary>
    public Guid Guid
    {
      get
      {
        return _Guid;
      }
      set
      {
        _Guid = value;
      }
    }

    /// <summary>
    /// The property defines Unique global identifier record of Data Change object
    /// </summary>
    public Guid RecordGuid
    {
      get
      {
        return this._RecordGuid;
      }
      set
      {
        this._RecordGuid = value;
      }
    }

    /// <summary>
    /// This property defines unique integer identification of Data Change
    /// </summary>
    public long Uid
    {
      get
      {
        return _Uid;
      }
      set
      {
        _Uid = value;
      }
    }

    /// <summary>
    /// This property defines record identifier for the Data Change object
    /// </summary>
    public long RecordUid
    {
      get
      {
        return this._RecordUid;
      }
      set
      {
        this._RecordUid = value;
      }
    }

    /// <summary>
    /// This property contains trial identifier of Data Change
    /// </summary>
    public string TrialId
    {
      get
      {
        return this._TrialId;
      }
      set
      {
        this._TrialId = value;
      }
    }

    /// <summary>
    /// This property contains subject identifier of Data Change
    /// </summary>
    public string SubjectId
    {
      get
      {
        return this._SubjectId;
      }
      set
      {
        this._SubjectId = value;
      }
    }

    /// <summary>
    /// This property contains string record identifier of Data Change
    /// </summary>
    public string RecordId
    {
      get
      {
        return this._RecordId;
      }
      set
      {
        this._RecordId = value;
      }
    }

    /// <summary>
    /// This property contains table name of Data Change
    /// </summary>
    public DataChangeTableNames TableName
    {
      get
      {
        return this._TableName;
      }
      set
      {
        this._TableName = value;
      }
    }

    /// <summary>
    /// This property contains user unique integer identification that changed the table values
    /// </summary>
    public string UserId
    {
      get
      {
        return this._UserId;
      }
      set
      {
        this._UserId = value;
      }
    }

    /// <summary>
    /// This property defines date time stamp of when the change was made
    /// </summary>
    public DateTime DateStamp
    {
      get
      {
        return this._DateStamp;
      }
      set
      {
        this._DateStamp = value;
      }
    }

    /// <summary>
    /// This property contains date stamp of the change in string format 
    /// </summary>
    public string stDateStamp
    {
      get
      {
        if ( this._DateStamp > Evado.Model.EvStatics.CONST_DATE_NULL )
        {
          return this._DateStamp.ToString( "dd MMM yyyy HH:mm:ss" );
        }
        return String.Empty;
      }
      set
      {
        string Null = value;
      }
    }

    /// <summary>
    /// This property contains the list of change value items
    /// </summary>
    public List<EvDataChangeItem> Items
    {
      get
      {
        return this._Items;
      }
      set
      {
        this._Items = value;
      }
    }
    // End properties.

    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region public methods

    //  =================================================================================
    /// <summary>
    ///   This method adds a new data change value to the list of data change objects.
    /// </summary>
    /// <param name="ItemId">String: The item identifier.</param>
    /// <param name="InitialValue">String: the intial item value.</param>
    /// <param name="NewValue">String: the new item value.</param>
    /// <returns>Integer: is the count of data change object in the list.</returns>
    /// <remarks>
    /// This method consists of the following steps:
    /// 
    /// 1. Initilize the data change item object
    /// 
    /// 2. Add the item to the item object
    /// 
    /// 3. Return a number of items on the item list object
    /// </remarks>
    //  ---------------------------------------------------------------------------------
    public int AddItem ( String ItemId, String InitialValue, String NewValue )
    {
      //
      // if nothing has changed then exit.
      //
      if ( InitialValue.Trim ( ) == NewValue.Trim ( ) )
      {
        return this._Items.Count;
      }

      // 
      // Initialise the new data change item
      // 
      EvDataChangeItem item = new EvDataChangeItem( ItemId, InitialValue, NewValue );

      // 
      // Append the item to the item list.
      // 
      this._Items.Add( item );

      //
      // return the item index.
      //
      return this._Items.Count;

    }//END AddItem Method
    //  =================================================================================
    /// <summary>
    ///   This method adds a new data change value to the list of data change objects.
    /// </summary>
    /// <param name="ItemId">String: The item identifier.</param>
    /// <param name="enumInitialValue">Enumerated Object: the intial item value.</param>
    /// <param name="enumNewValue">Enumerated Object: the new item value.</param>
    /// <returns>Integer: is the count of data change object in the list.</returns>
    /// <remarks>
    /// This method consists of the following steps:
    /// 
    /// 1. Initilize the data change item object
    /// 
    /// 2. Add the item to the item object
    /// 
    /// 3. Return a number of items on the item list object
    /// </remarks>
    //  ---------------------------------------------------------------------------------
    public int AddItem ( 
      String ItemId, 
      Object enumInitialValue, 
      Object enumNewValue )
    {
      //
      // Convert the enumerated value to string.
      //
      String InitialValue = Enum.GetName ( enumInitialValue.GetType ( ), enumInitialValue );
      String NewValue = Enum.GetName ( enumNewValue.GetType ( ), enumNewValue );
      
      //
      // if nothing has changed then exit.
      //
      if ( InitialValue.Trim ( ) == NewValue.Trim ( ) )
      {
        return this._Items.Count;
      }

      // 
      // Initialise the new data change item
      // 
      EvDataChangeItem item = new EvDataChangeItem ( ItemId, InitialValue, NewValue );

      // 
      // Append the item to the item list.
      // 
      this._Items.Add ( item );

      //
      // return the item index.
      //
      return this._Items.Count;

    }//END AddItem Metho
    //  =================================================================================
    /// <summary>
    ///   This method adds a new data change value to the list of data change objects.
    /// </summary>
    /// <param name="ItemId">String: The item identifier.</param>
    /// <param name="InitialValue">DateTime: the intial item value.</param>
    /// <param name="NewValue">DateTime: the new item value.</param>
    /// <returns>Integer: is the count of data change object in the list.</returns>
    /// <remarks>
    /// This method consists of the following steps:
    /// 
    /// 1. Initilize the data change item object
    /// 
    /// 2. Add the item to the item object
    /// 
    /// 3. Return a number of items on the item list object
    /// </remarks>
    //  ---------------------------------------------------------------------------------
    public int AddItem ( String ItemId, DateTime InitialValue, DateTime NewValue )
    {
      //
      // if nothing has changed then exit.
      //
      if ( InitialValue == NewValue )
      {
        return this._Items.Count;
      }

      // 
      // Initialise the new data change item
      // 
      EvDataChangeItem item = new EvDataChangeItem ( ItemId, 
        InitialValue.ToString ( "dd-MMM-yyy HH:mm:ss" ),
        NewValue.ToString ( "dd-MMM-yyy HH:mm:ss" ) );

      // 
      // Append the item to the item list.
      // 
      this._Items.Add ( item );

      //
      // return the item index.
      //
      return this._Items.Count;

    }//END AddItem Methodd

    //  =================================================================================
    /// <summary>
    ///   This method adds a new data change value to the list of data change objects.
    /// </summary>
    /// <param name="ItemId">String: The item identifier.</param>
    /// <param name="InitialValue">int: the intial item value.</param>
    /// <param name="NewValue">int: the new item value.</param>
    /// <returns>Integer: is the count of data change object in the list.</returns>
    /// <remarks>
    /// This method consists of the following steps:
    /// 
    /// 1. Initilize the data change item object
    /// 
    /// 2. Add the item to the item object
    /// 
    /// 3. Return a number of items on the item list object
    /// </remarks>
    //  ---------------------------------------------------------------------------------
    public int AddItem ( String ItemId, int InitialValue, int NewValue )
    {
      //
      // if nothing has changed then exit.
      //
      if ( InitialValue == NewValue )
      {
        return this._Items.Count;
      }

      // 
      // Initialise the new data change item
      // 
      EvDataChangeItem item = new EvDataChangeItem ( ItemId, InitialValue.ToString ( ), NewValue.ToString ( ) );

      // 
      // Append the item to the item list.
      // 
      this._Items.Add ( item );

      //
      // return the item index.
      //
      return this._Items.Count;

    }//END AddItem Method

    //  =================================================================================
    /// <summary>
    ///   This method adds a new data change value to the list of data change objects.
    /// </summary>
    /// <param name="ItemId">String: The item identifier.</param>
    /// <param name="InitialValue">float: the intial item value.</param>
    /// <param name="NewValue">float: the new item value.</param>
    /// <returns>Integer: is the count of data change object in the list.</returns>
    /// <remarks>
    /// This method consists of the following steps:
    /// 
    /// 1. Initilize the data change item object
    /// 
    /// 2. Add the item to the item object
    /// 
    /// 3. Return a number of items on the item list object
    /// </remarks>
    //  ---------------------------------------------------------------------------------
    public int AddItem ( string ItemId, float InitialValue, float NewValue )
    {
      //
      // if nothing has changed then exit.
      //
      if ( InitialValue == NewValue )
      {
        return this._Items.Count;
      }

      // 
      // Initialise the new data change item
      // 
      EvDataChangeItem item = new EvDataChangeItem ( ItemId, InitialValue.ToString ( ), NewValue.ToString ( ) );

      // 
      // Append the item to the item list.
      // 
      this._Items.Add ( item );

      //
      // return the item index.
      //
      return this._Items.Count;

    }//END AddItem Method

    //  =================================================================================
    /// <summary>
    ///   This method adds a new data change value to the list of data change objects.
    /// </summary>
    /// <param name="ItemId">String: The item identifier.</param>
    /// <param name="InitialValue">bool: the intial item value.</param>
    /// <param name="NewValue">bool: the new item value.</param>
    /// <returns>Integer: is the count of data change object in the list.</returns>
    /// <remarks>
    /// This method consists of the following steps:
    /// 
    /// 1. Initilize the data change item object
    /// 
    /// 2. Add the item to the item object
    /// 
    /// 3. Return a number of items on the item list object
    /// </remarks>
    //  ---------------------------------------------------------------------------------
    public int AddItem ( String ItemId, bool InitialValue, bool NewValue )
    {
      //
      // if nothing has changed then exit.
      //
      if ( InitialValue == NewValue )
      {
        return this._Items.Count;
      }

      // 
      // Initialise the new data change item
      // 
      EvDataChangeItem item = new EvDataChangeItem ( ItemId, InitialValue.ToString ( ), NewValue.ToString ( ) );

      // 
      // Append the item to the item list.
      // 
      this._Items.Add ( item );

      //
      // return the item index.
      //
      return this._Items.Count;

    }//END AddItem Method

    //  =================================================================================
    /// <summary>
    ///   This method adds a new data change value to the list of data change objects.
    /// </summary>
    /// <param name="ItemId">String: The item identifier.</param>
    /// <param name="InitialValue">String: the intial item value.</param>
    /// <param name="NewValue">String: the new item value.</param>
    /// <returns>Integer: is the count of data change object in the list.</returns>
    /// <remarks>
    /// This method consists of the following steps:
    /// 
    /// 1. Initilize the data change item object
    /// 
    /// 2. Add the item to the item object
    /// 
    /// 3. Return a number of items on the item list object
    /// </remarks>
    //  ---------------------------------------------------------------------------------
    public int AddItem ( String ItemId, Guid InitialValue, Guid NewValue )
    {
      //
      // if nothing has changed then exit.
      //
      if ( InitialValue == NewValue )
      {
        return this._Items.Count;
      }

      // 
      // Initialise the new data change item
      // 
      EvDataChangeItem item = new EvDataChangeItem ( ItemId, InitialValue.ToString ( ), NewValue.ToString ( ) );

      // 
      // Append the item to the item list.
      // 
      this._Items.Add ( item );

      //
      // return the item index.
      //
      return this._Items.Count;

    }//END AddItem Method

    //  =================================================================================
    /// <summary>
    /// This method creates a Html fieldset containing the contents of the indexed data
    /// change object.
    /// </summary>
    /// <param name="ChangeNo">Int: the index to the data change object to be displayed.</param>
    /// <returns>Html as String</returns> 
    /// <remarks>
    /// This method consists of the following steps:
    /// 
    /// 1. Initialize the stHtml string
    /// 
    /// 2. Open the fieldset element and set its legend
    /// 
    /// 3. Add a table row displaying the trail identifier if it exists
    /// 
    /// 4. Add a table row displaying the subject identifier if it exists
    /// 
    /// 5. Add a table row displaying the record identifier if it is exists
    /// 
    /// 6. Output the data change item value in a table 
    /// 
    /// 7. Loop through the items in this change items as a table row.
    /// 
    /// 8. Extract data, set view, set column, format xml, add user identifier and time stamp
    /// </remarks>
    //  ---------------------------------------------------------------------------------
    public string getAsHtml( int ChangeNo )
    {
      //
      // Initialise the stHtlm string
      //
      String stHtml = String.Empty;

      //
      // Open the fieldset element and set its legend
      //
      stHtml += "<fieldset class='Fields'> "
        + "<legend>Change: " + ( ChangeNo + 1 ) + "</legend>"
        + "<table>";

      // 
      // Add a table row displaying the trial identifier if it exists.
      //
      if ( this._TrialId != String.Empty )
      {
        stHtml += "<tr>"
          + "<td class='Prompt Width_20' >TrialId:</td>"
          + "<td>" + this._TrialId + "</td>"
          + "</tr>";
      }

      //
      // Add a table row displaying the subject identifier if it exists
      //
      if ( this._SubjectId != String.Empty )
      {
        stHtml += "<tr>"
          + "<td class='Prompt Width_20' >SubjectId:</td>"
          + "<td>" + this._SubjectId + "</td>"
          + "</tr>";
      }
      
      //
      // Add a table row displaying the record identifier if it is exists
      //
      if ( this._RecordId != String.Empty )
      {
        stHtml += "<tr>"
          + "<td class='Prompt Width_20' >RecordId:</td>"
          + "<td>" + this._RecordId + "</td>"
          + "</tr>";
      }
      stHtml += "</table>\r\n";

       //
       // Output the data change item value in a table 
       //
      stHtml += "<table class='View' style='width:98%' cellspacing='0' border='1' style='border-collapse:collapse;' >"
        + "<tr class='View_Header'>"
        + "<th style='width:40px;' style='text-align:center;' >Item</th>"
        + "<th style='text-align:center;' >ItemId</th>"
        + "<th style='width:425px;'style='text-align:center;' >Initial Value</th>"
        + "<th style='width:425px;'style='text-align:center;' >New Value</th>"
        + "</tr>";

      //
      // Iterate through the items in this change items as a table row.
      //
      for ( int index = 0; index < this._Items.Count; index++ )
      {
        //
        // Extract data change item from the list of items.
        //
        EvDataChangeItem item = this._Items [ index ];

        //
        // Set the view to be "View_Item" for the event order and "View_Alt" for the odd order 
        //
        if ( index % 2 == 0 )
        {
          stHtml += "<tr class='View_Item' >"
            + "<td style='text-align:center;'>"
            + ( index + 1 )
            + "</td>";
        }
        else
        {
          stHtml += "<tr class='View_Alt'>"
            + "<td style='text-align:center;'>"
            + ( index + 1 )
            + "</td>";
        }

        //
        // Set the column to be "Null Data" if item identifier does not exist
        // but if it exists, assign column with item identifier from the list of items
        //
        if ( item.ItemId == String.Empty )
        {
          stHtml += "<td>Null Data</td>";
        }
        else
        {
          stHtml += "<td>" + EvHtmlCoding.Decode( item.ItemId )  + "</td>";
        }

        //
        // Set the column to be "Null Data" if the initial value does not exist
        //
        if ( item.InitialValue == String.Empty )
        {
          stHtml += "<td>Null Data</td>";
        }
        else
        {

          string value = EvHtmlCoding.Decode( item.InitialValue );

          //
          // Formating XML object for display
          //
          if ( value.Contains("< ?xml version" ) == true )
          {
            value = value.Replace( "> ", ">" );
            value = value.Replace( "\n", String.Empty );
            value = value.Replace( "> ", "><br/>" );
          }

          stHtml += "<td>" + value + "</td>";
        }

        //
        // Set the column to be "Null Data" if the new value does not exist
        //
        if ( item.NewValue == String.Empty )
        {
          stHtml += "<td>Null Data</td>";
        }
        else
        {
          string value = EvHtmlCoding.Decode( item.NewValue );
          //
          // Formating XML object for display
          //
          if ( value.Contains( "< ?xml version" ) == true )
          {
            value = value.Replace( "> ", ">" );
            value = value.Replace( "\n", String.Empty );
            value = value.Replace( "> ", "> <br/>" );
          }

          stHtml += "<td>" + value + "</td>";
        }

      }

      //
      // Add user identifier and date time stamp from the list of items to the new table row
      //
      stHtml += "<table>"
        + "<tr>"
        + "<td class='Prompt Width_20' >UserId:</td>"
        + "<td>"
        + this._UserId
        + "</td>"
        + "</tr>"
        + "<tr>"
        + "<td class='Prompt' >DateStamp:</td>"
        + "<td>"
        + this.stDateStamp
        + "</td>"
        + "</tr>"
        + "</table>"
        + "</fieldset>\t\n";

      //
      // Return the html markup as a string
      //
      return stHtml;

    }//END getAsHtml method


    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region static methods

    // =====================================================================================
    /// <summary>
    /// This class provides a list of trial types.
    /// 
    /// </summary>
    /// <returns>List: containing selection List for configuration table</returns>
    /// <remarks>
    /// This method consists of the following steps:
    /// 
    /// 1. Initilize a return list of Configuration table name
    /// 
    /// 2. Create an option list
    /// 
    /// 3. Add items from option list to the return list
    /// </remarks>
    //  ------------------------------------------------------------------------------------
    public static List<Evado.Model.EvOption> getConfigurationTablesNameList( )
    {
      //
      // Initilize a return list of Configuration table name object
      //
      List<Evado.Model.EvOption> list = new List<Evado.Model.EvOption>();
      
      //
      // Create an option list
      //
      Evado.Model.EvOption option = new Evado.Model.EvOption();
      list.Add( option );

      //
      // Add items from option list to the return list
      //

      option = Evado.Model.EvStatics.getOption( DataChangeTableNames.EvAlerts );
      list.Add( option );

      option = Evado.Model.EvStatics.getOption( DataChangeTableNames.EvForms );
      list.Add( option );

      option = Evado.Model.EvStatics.getOption( DataChangeTableNames.EdApplicationSettings );
      list.Add( option );

      option = Evado.Model.EvStatics.getOption( DataChangeTableNames.EvUserProfiles );
      list.Add( option );

      // 
      // Return the list.
      // 
      return list;
    }//END getConfigurationTablesNameList method

    // =====================================================================================
    /// <summary>
    /// This class provides a list of trial types.
    /// 
    /// </summary>
    /// <returns>List: containing selection List for record table</returns>
    /// <remarks>
    /// This method consists of the following steps:
    /// 
    /// 1. Initialize a return list of record table name
    /// 
    /// 2. Create an option list
    /// 
    /// 3. Add items from the option list to the return list
    /// </remarks>
    //  ------------------------------------------------------------------------------------
    public static List<Evado.Model.EvOption> getRecordTablesNameList( )
    {
      //
      // Initialize a return list of record table name
      //
      List<Evado.Model.EvOption> list = new List<Evado.Model.EvOption>();

      //
      // Create an option list 
      //
      Evado.Model.EvOption option = new Evado.Model.EvOption();
      list.Add( option );

      // Add items from the option list to the return list

      option = Evado.Model.EvStatics.getOption( DataChangeTableNames.EvRecords );
      list.Add ( option );

      option = Evado.Model.EvStatics.getOption ( DataChangeTableNames.EvCommonRecords);
      list.Add ( option );

      // 
      // Return the list.
      // 
      return list;
    }

    // =====================================================================================
    /// <summary>
    /// This class provides a list of trial types.
    /// 
    /// </summary>
    /// <returns>IList containing selection List for items in record table</returns>
    /// <remarks>
    /// This method consists of the following steps:
    /// 
    /// 1. Initialize a return list of record item table name
    /// 
    /// 2. Create an option list
    /// 
    /// 3. Add items from the option list to the return list
    /// </remarks>
    //  ------------------------------------------------------------------------------------
    public static List<Evado.Model.EvOption> getRecordItemTablesNameList( )
    {
      //
      // Initialize a return list of record item table name
      //
      List<Evado.Model.EvOption> list = new List<Evado.Model.EvOption>();

      //
      // Create an option list
      //
      Evado.Model.EvOption option = new Evado.Model.EvOption();
      list.Add( option );

      //
      // Add items from the option list to the return list
      //
      option = Evado.Model.EvStatics.getOption( DataChangeTableNames.EvFormFields );
      list.Add( option );



      // 
      // Return the list.
      // 
      return list;
    }

    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

  }//END Data Change method

}//END namespace
