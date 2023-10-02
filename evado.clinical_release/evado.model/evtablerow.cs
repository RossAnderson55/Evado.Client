/***************************************************************************************
 * <copyright file="Evado.Model\TableRow.cs" company="EVADO HOLDING PTY. LTD.">
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
 *  This class contains the ClientPageTableRow data object.
 *
 ****************************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;

namespace Evado.Model
{
  /// 
  /// Business entity used to model ClientPageField
  /// 
  [Serializable]
  public class EvTableRow
  {
    #region Class Intialisation Methods


    /// ==================================================================================
    /// <summary>
    /// This constructor intialises an empty string to _Column array. 
    /// </summary>
    
    // ----------------------------------------------------------------------------------
    public EvTableRow( )
    {
      for ( int i = 0; i < Column.Length; i++ )
      {
        this.Column [ i ] = String.Empty;
      }//END _Column.Length iteration
    }//END TableRow method

    /// ==================================================================================
    /// <summary>
    /// This constructor intialises an empty string to _Column array. 
    /// </summary>
    /// <param name="Columns">Int: column count</param>
    // ----------------------------------------------------------------------------------
    public EvTableRow ( int Columns )
    {
      this.Column = new string [ Columns ];

      for ( int i = 0; i < Column.Length; i++ )
      {
        this.Column [ i ] = String.Empty;
      }//END _Column.Length iteration
    }//END TableRow method
    #endregion

    #region Class Constants
    private const int MaxRows = 50;

    #endregion

    #region Class PropertyList

    /// <summary>
    /// This property contains the row number of the table.
    /// </summary>
    public int No { get; set; } = 0;

    /// <summary>
    /// This property contains an array of column data.
    /// </summary>
    public string [ ] Column= new String [ EvTable.DefaultColumns ];

    #endregion
  }//END ClientPageTableRow Class

} // Close namespace  Evado.UniForm.Model
