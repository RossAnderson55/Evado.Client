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
  public class EvDataChangeItem
  {
    #region initialisation methods

    //===================================================================================
    /// <summary>
    /// This method assign new data change item value to existing list of item
    /// </summary>
    //-----------------------------------------------------------------------------------
    public EvDataChangeItem ( )
    {
    }
    //===================================================================================
    /// <summary>
    /// This method assign new data change item value to existing list of item
    /// </summary>
    /// <param name="ItemId">string: The item identifier</param>
    /// <param name="InitialValue">string: the initial item value</param>
    /// <param name="NewValue">string: the new item value</param>
    //-----------------------------------------------------------------------------------
    public EvDataChangeItem ( string ItemId, string InitialValue, string NewValue )
    {
      this._ItemId = ItemId;
      this._InitialValue = InitialValue;
      this._NewValue = NewValue;
    }

    #endregion


    #region properties

    private string _ItemId = String.Empty;
    /// <summary>
    /// This property contains the data change item's identifier
    /// </summary>
    public string ItemId
    {
      get
      {
        return this._ItemId;
      }
      set
      {
        this._ItemId = value;
      }
    }

    private string _InitialValue = String.Empty;
    /// <summary>
    /// This property contains the initial item value
    /// </summary>
    public string InitialValue
    {
      get
      {
        return this._InitialValue;
      }
      set
      {
        this._InitialValue = value.Replace( ";", "; ");
        this._InitialValue = this._InitialValue.Replace( "  ", " " );
      }
    }

    private string _NewValue = String.Empty;
    /// <summary>
    /// This property contains the new item value.
    /// </summary>
    public string NewValue
    {
      get
      {
        return this._NewValue;
      }
      set
      {
        this._NewValue = value.Replace( ";", "; " );
        this._NewValue = this._NewValue.Replace( "  ", " " );
      }
    }
    #endregion

  }//END EvDataChangeItem class

}//END namespace
