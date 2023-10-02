/***************************************************************************************
 * <copyright file="Evado.Model\TableColumnHeader.cs" company="EVADO HOLDING PTY. LTD.">
 *     
 *      Copyright (c) 2013 - 2021 EVADO HOLDING PTY. LTD.  All rights reserved.
 *     
 *      The use and distribution terms for this software are contained in the file
 *      named \license.txt, which can be found in the root of this distribution.S
 *      By using this software in any fashion, you are agreeing to be bound by the
 *      terms of this license.
 *     
 *      You must not remove this notice, or any other, from this software.
 *     
 * </copyright>
 * 
 * Description: 
 *  This class contains the ClientPageTableColHeader data object.
 *
 ****************************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;

using Evado.Model;

using Newtonsoft.Json;

namespace Evado.Model
{
  /// <summary>
  /// The Column Header Class definition.
  /// </summary>
  [Serializable]
  public class EvTableHeader
  {
    #region Class Constants
    /************************************************************************************
     * These constrants are used by the web clients.
     ************************************************************************************/
    /// <summary>
    /// This contant defines an item type which is read only type.
    /// </summary>
    public const string ItemTypeReadOnly = "RO";
    /// <summary>
    /// This constant defines an item type which is yes no type.
    /// </summary>
    public const string ItemTypeYesNo = "YN";
    /// <summary>
    /// This constant defines an item type which is text type. 
    /// </summary>
    public const string ItemTypeText = "TXT";
    /// <summary>
    /// This constant defines an item type which is numeric type.
    /// </summary>
    public const string ItemTypeNumeric = "NUM";
    /// <summary>
    /// This constant defines an item type which is date type.
    /// </summary>
    public const string ItemTypeDate = "DT";
    /// <summary>
    /// This constant defines an item type which is radio button type. 
    /// </summary>
    public const string ItemTypeRadioButton = "RBL";
    /// <summary>
    /// This constant defines an item which is selection list type. 
    /// </summary>
    public const string ItemTypeSelectionList = "SL";
    #endregion

    #region Class Properties

    /// <summary>
    /// This property contains the value 1.
    /// </summary>
    public int No { get; set; } = 1;

    private string _ColumnId = String.Empty;
    /// <summary>
    /// This property contains the header column text of table.
    /// </summary>
    public string ColumnId
    {
      get
      {
        //
        // if column id is empty the use the colunn text as the identifier.
        //
        if ( this._ColumnId == String.Empty )
        {
          this._ColumnId = this.No.ToString ( "00" );
        }

        return _ColumnId;
      }
      set { _ColumnId = value; }
    }
    /// <summary>
    /// This property contains a text value for Table Col Header object.
    /// </summary>
    public string Text { get; set; } = String.Empty;

    /// <summary>
    /// This property contains the percentage width of the table header column 
    /// </summary>
    public Int16 Width { get; set; } = 50;

    /// <summary>
    /// This property contains a TypeId value for Table Col Header object.
    /// </summary>
    public EvDataTypes DataType { get; set; } = Evado.Model.EvDataTypes.Text;

    /// <summary>
    /// This property contains a options or an unit value for Table Col Header Object.
    /// </summary>
    public string OptionsOrUnit { get; set; } = String.Empty;

    /// <summary>
    /// This property contains the minumum validation range for numbers as an integer.
    /// </summary>
    public int MinimumValue { get; set; } = (int) EvStatics.CONST_NUMERIC_MINIMUM;

    /// <summary>
    /// This property contains the maximum validation range for numbers as an integer.
    /// </summary>
    public int MaximumValue { get; set; } = ( int ) EvStatics.CONST_NUMERIC_MAXIMUM;

    /// <summary>
    /// This property contains a selection list that is displayed on the device client.
    /// </summary>
    public List<Evado.Model.EvOption> OptionList { get; set; } = new List<Evado.Model.EvOption> ( );

    /// <summary>
    /// This property contains cDash metadata values. 
    /// </summary>
    [JsonIgnore]
    public string Metadata { get; set; } = String.Empty;

    /// <summary>
    /// DEPRECATED: This property contains header column data type identifier of table.
    /// </summary>
    [JsonIgnore]
    public String TypeId { get; set; }

    /// <summary>
    /// DEPRECATED: This property contains header column data type identifier of table.
    /// </summary>
    [JsonIgnore]
    public String Type
    {
      get { return this.TypeId; }
      set { this.TypeId = value; }
    }

    #endregion

    #region Public methods
    // =====================================================================================
    /// <summary>
    /// This method returns the Option list for the item.
    ///  
    /// Written by: Ross Anderson
    /// Date: 24/08/2005
    /// </summary>
    /// <returns>List of Evado.Model.EvOptions</returns>
    /// <remarks>
    /// This method consists of the following steps:
    /// 
    /// 1. Initialize a return list
    /// 
    /// 2. Add a null option as first item for a selection list.
    /// 
    /// 3. Add items from option object to the return list.
    /// </remarks>
    // -------------------------------------------------------------------------------------
    public static List<Evado.Model.EvOption> getTypeList ( Evado.Model.EvDataTypes Datatype )
    {
      //
      // Initialize a return list
      //
      List<Evado.Model.EvOption> List = new List<Evado.Model.EvOption> ( );
      // 
      // Add a null option as first item for a selection list.
      // 
      Evado.Model.EvOption Option = new Evado.Model.EvOption ( );
      List.Add ( Option );

      //
      // Add items from option object to the return list. 
      //
      Option = new Evado.Model.EvOption ( EvDataTypes.Yes_No, "YesNo Column" );
      List.Add ( Option );

      Option = new Evado.Model.EvOption ( EvDataTypes.Text, "Text Column" );
      List.Add ( Option );

      Option = new Evado.Model.EvOption ( EvDataTypes.Numeric, "Numeric Column" );
      List.Add ( Option );

      Option = new Evado.Model.EvOption ( EvDataTypes.Date, "Date Column" );
      List.Add ( Option );

      Option = new Evado.Model.EvOption ( EvDataTypes.Radio_Button_List, "Radio Button Column" );
      List.Add ( Option );

      Option = new Evado.Model.EvOption ( EvDataTypes.Selection_List, "Selection List Column" );
      List.Add ( Option );

      Option = new Evado.Model.EvOption ( EvDataTypes.Read_Only_Text, "Read Only Column" );
      List.Add ( Option );

      // 
      //Return the completed Array List.
      //
      return List;

    }//END getTypeList method

    // =====================================================================================
    /// <summary>
    /// This class returns the Option list for the item.
    ///  
    /// Written by: Ross Anderson
    /// Date: 24/08/2005
    /// </summary>
    /// <returns>ArrayList: a list of options</returns>
    /// <remarks>
    /// This method consists of the following steps:
    /// 
    /// 1. Initialize a return list
    /// 
    /// 2. Add a null option as first item for a selection list.
    /// 
    /// 3. Loop ten rounds to add the width to the return list. 
    /// </remarks>
    // -------------------------------------------------------------------------------------
    public static List<Evado.Model.EvOption> getWidthList ( )
    {
      //
      // Initialize a return list.
      //
      List<Evado.Model.EvOption> List = new List<Evado.Model.EvOption> ( );
      // 
      // Add a null option as first item for a selection list.
      // 
      Evado.Model.EvOption Option = new Evado.Model.EvOption ( "0", String.Empty );
      List.Add ( Option );

      //
      // Loop ten rounds to add the width to the return list. 
      //
      for ( int Count = 0; Count < 10; Count++ )
      {
        string sWidth = ( Count * 5 + 5 ).ToString ( );
        Option = new Evado.Model.EvOption ( sWidth, sWidth );
        List.Add ( Option );
      }
      // 
      //Return the completed Array List.
      //
      return List;
    }//END getWidthList method

    // =====================================================================================
    /// <summary>
    /// This class returns the Option list for the item.
    ///  
    /// Written by: Ross Anderson
    /// Date: 24/08/2005
    /// </summary>
    /// <returns>ArrayList: a list of options</returns>
    /// <remarks>
    /// This method consists of the following steps:
    /// 
    /// 1. Initialize a return list
    /// 
    /// 2. Add a null option as first item for a selection list.
    /// 
    /// 3. Loop ten rounds to add the width to the return list. 
    /// </remarks>
    // -------------------------------------------------------------------------------------
    public static List<Evado.Model.EvOption> getRowLengthList ( )
    {
      //
      // Initialize a return list.
      //
      List<Evado.Model.EvOption> List = new List<Evado.Model.EvOption> ( );
      Evado.Model.EvOption Option = new Evado.Model.EvOption ( );

      //
      // Loop five rounds to add the width to the return list. 
      //
      for ( int count = 0; count < 10; count++ )
      {
        string stRow = count.ToString ( );
        Option = new Evado.Model.EvOption ( stRow, stRow );
        List.Add ( Option );
      }

      //
      // Loop ten rounds to add the width to the return list. 
      //
      for ( int Count = 0; Count < 20; Count++ )
      {
        string sWidth = ( Count * 2 + 10 ).ToString ( );
        Option = new Evado.Model.EvOption ( sWidth, sWidth );
        List.Add ( Option );
      }
      // 
      //Return the completed Array List.
      //
      return List;
    }//END getWidthList method
    #endregion
  }
}