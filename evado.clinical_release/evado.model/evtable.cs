/***************************************************************************************
 * <copyright file="Evado.Model\Table.cs" company="EVADO HOLDING PTY. LTD.">
 *     
 *      Copyright (c) 2013 - 2021 EVADO HOLDING PTY. LTD.  All rights reserved.
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
 *  This class contains the ClientPageFieldTable data object.
 *
 ****************************************************************************************/

using Newtonsoft.Json;

using System;
using System.Collections;
using System.Collections.Generic;

namespace Evado.Model
{  /// 
  /// Business entity used to model ClientPageField
  /// 
  [Serializable]
  public class EvTable
  {

    #region class Initialisation Methods.

    //  =================================================================================
    /// <summary>
    /// This method initialiseas the header array.
    /// </summary>
    //  ---------------------------------------------------------------------------------

    public EvTable ( )
    {
      // 
      // Initialise the header array.
      // 
      for ( int i = 0; i < 10; i++ )
      {
        this.Header [ i ] = new EvTableHeader ( );
        this.Header [ i ].No = i + 1;
        this.Header [ i ].ColumnId  = this.Header [ i ].No.ToString("00");
      }
    }

    //  =================================================================================
    /// <summary>
    /// This method initialiseas the header array.
    /// </summary>
    /// <param name="ColumnCount">Int: number of table columns</param>
    //  ---------------------------------------------------------------------------------

    public EvTable ( int ColumnCount )
    {
      this.Header = new EvTableHeader [ ColumnCount ];
      // 
      // Initialise the header array.
      // 
      for ( int i = 0; i < ColumnCount; i++ )
      {
        this.Header [ i ] = new EvTableHeader ( );
        this.Header [ i ].No = i + 1;
        this.Header [ i ].ColumnId = this.Header [ i ].No.ToString ( "00" );
      }
    }

    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region Class Constants
    /// <summary>
    /// This contant defines the column of Table is 10.
    /// </summary>
    public const int DefaultColumns = 10;

    #endregion

    #region Class Properties

    /// <summary>
    /// This property contains the  Header of the TableColHeader object.
    /// </summary>
    public EvTableHeader [ ] Header { get; set; } = new EvTableHeader [ 10 ];

    /// <summary>
    /// This property contains the count of the column.
    /// </summary>
    [JsonIgnore]
    public int ColumnCount
    {
      get
      {
        int count = 0;
        if ( Header != null )
        {
          for ( int i = 0; i < Header.Length; i++ )
          {
            if ( this.Header [ i ].Text != String.Empty )
            {
              count++;
            }
          }
        }
        return count;
      }

    }

    /// <summary>
    /// This property contains the list of the TableRow object.
    /// </summary>
    public List<EvTableRow> Rows { get; set; } = new List<EvTableRow> ( );

    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region Class Methods
    //=================================================================================
    /// <summary>
    /// This method adds a new row in the table.
    /// </summary>
    /// <returns>Evado.UniForm.Model.EvTableRow</returns>
    //---------------------------------------------------------------------------------
    public EvTableHeader [ ] setHeader ( int ColumnCount )
    {
      this.Header = new EvTableHeader [ ColumnCount ];

      for( int i=0; i<ColumnCount; i++ )
      {
        this.Header [ i ] = new EvTableHeader ( );
      }
       
      return this.Header;
    }

    //=================================================================================
    /// <summary>
    /// This method adds a new row in the table.
    /// </summary>
    /// <returns>Evado.UniForm.Model.EvTableRow</returns>
    //---------------------------------------------------------------------------------
    public EvTableRow addRow ( )
    {
      EvTableRow row = new EvTableRow (
       this.Header.Length );

      this.Rows.Add ( row );

      return row;
    }

    //=================================================================================
    /// <summary>
    /// This method adds a new row in the table.
    /// </summary>
    /// <returns>Evado.UniForm.Model.EvTableRow</returns>
    //---------------------------------------------------------------------------------
    public EvTableRow addRow ( int  Columns )
    {
      EvTableRow row = new EvTableRow ( );

      this.Rows.Add ( row );

      return row;
    }
    // ================================================================================
    /// <summary>
    /// This method redimensions the table rows.
    /// </summary>
    /// <param name="RowCount"></param>
    // --------------------------------------------------------------------------------
    public void SetRowCount ( int RowCount )
    {
      // 
      // selection matches the currentMonth lengh exit as nothing needs to be done.
      // 
      if ( RowCount <= this.Rows.Count )
      {
        return;
      }

      int setIndex = 0;
      if ( this.Rows.Count > 0 )
      {
        setIndex = this.Rows.Count - 1;
      }

      // 
      // Initialise the new array and fill it with the currentMonth values.
      // 
      for ( int row = setIndex; row < RowCount; row++ )
      {
        EvTableRow newRow = new EvTableRow ( );
        newRow.No = ( row + 1 );
        this.Rows.Add ( newRow );

      }//END row initialise interation

    }//END SetRowCount method. 

    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion


  } // Close ClientPageFieldTable class

} // Close namespace  Evado.UniForm.Model
