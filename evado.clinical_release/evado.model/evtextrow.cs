/***************************************************************************************
 * <copyright file="model\EvActivity.cs" company="EVADO HOLDING PTY. LTD.">
 *     
 *      Copyright (c) 2002 - 2023 EVADO HOLDING PTY. LTD..  All rights reserved.
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
 *  This class contains the EvActivity data object.
 *
 ****************************************************************************************/

using Newtonsoft.Json;

using System;
using System.Collections.Generic;

namespace Evado.Model
{

  /// <summary>
  /// This class defines the data model for  trial or registry schedule milestone activities. 
  /// </summary>
  [Serializable]
  public class EvTextRow
  {
    // ==================================================================================
    /// <summary>
    /// This is the class initialisation method.
    /// </summary>
    // ----------------------------------------------------------------------------------
    public EvTextRow ( )
    { }

    // ==================================================================================
    /// <summary>
    /// THis class initialisation method also set the column length.
    /// </summary>
    /// <param name="ColumnCount"></param>
    // ----------------------------------------------------------------------------------
    public EvTextRow ( int ColumnCount )
    {
      for ( int i = 0; i < ColumnCount; i++ )
      {
        this.Values.Add ( "" );
      }
    }

    /// <summary>
    /// This property contains a text values.
    /// </summary>
    public List<String> Values { get; set; } = new List<string> ( );

    // ==================================================================================
    /// <summary>
    /// Thie method add a value to the value list.
    /// </summary>
    /// <param name="Value"></param>
    // ----------------------------------------------------------------------------------
    public void AddValue ( object Value )
    {
      Values.Add ( Value.ToString ( ) );
    }

    // ==================================================================================
    /// <summary>
    /// Thie method add a value to the value list.
    /// </summary>
    /// <param name="Index">Int: Column index</param>
    /// <returns>String columne value</returns>
    // ----------------------------------------------------------------------------------
    public String GetValue ( int Index )
    {
      if ( Index >= 0
        && Index < Values.Count )
      {
        return Values [ Index ];
      }
      return String.Empty;
    }

  } //END EvTextRow class

} //END namespace Evado.Model
