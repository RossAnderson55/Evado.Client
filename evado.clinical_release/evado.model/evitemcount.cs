/***************************************************************************************
 * <copyright file="EvCount.cs" company="EVADO HOLDING PTY. LTD.">
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
 *  This class contains the EvCount data object.
 *
 ****************************************************************************************/

using System;

namespace Evado.Model
{
  /// <summary>
  /// This class defines item and count property used across the application
  /// </summary>
  [Serializable]
  public class EvItemCount
  {
    /// <summary>
    /// Default constructor
    /// </summary>
    public EvItemCount( )
    {
    }

    /// <summary>
    /// Constructor with specified initial values
    /// </summary>
    /// <param name="Item">string: the item name or identifier</param>
    /// <param name="Count">int: the count of the item</param>
    /// 
    public EvItemCount( string Item, int Count )
    {
      this._Item = Item;
      this._Count = Count;
    }
    //
    // Internal member variables
    //
    private string _Item = String.Empty;
    private int _Count = 0;

    #region Property
    /// <summary>
    /// This property contains the name or identifier of the item that has been counted.
    /// </summary>
    public string Item
    {
      get { return _Item; }
      set { _Item = value; }
    }
    /// <summary>
    /// This property contains the count of the items
    /// </summary>
    public int Count
    {
      get { return _Count; }
      set { _Count = value; }
    }
    #endregion

  }//END EvItemCount method

}//END namespace Evado.Model
