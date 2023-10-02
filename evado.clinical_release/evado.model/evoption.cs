/***************************************************************************************
 * <copyright file="EvOption.cs" company="EVADO HOLDING PTY. LTD.">
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
 *  This class contains the Evado.Model.EvOption data object.
 *
 ****************************************************************************************/

using System;
using Newtonsoft.Json;

namespace Evado.Model
{
  /// <summary>
  /// This class defines a selection list option.
  /// </summary>
  [Serializable]
  public class EvOption
  {
    #region Public methods
    // ==================================================================================
    /// <summary>
    /// Constructor with specified initial values
    /// </summary>
    // ----------------------------------------------------------------------------------
    public EvOption ( )
    {
    }

    // ==================================================================================
    /// <summary>
    /// Constructor with specified initial values
    /// </summary>
    /// <param name="Value">string: the value of the option</param>
    /// <param name="Description">string: the description of the option</param>
    // ----------------------------------------------------------------------------------
    public EvOption ( object Value, string Description )
    {
      this._Value = Value.ToString ( );
      this._Description = Description;
    }

    // ==================================================================================
    /// <summary>
    /// Constructor with specified initial values
    /// </summary>
    /// <param name="Value">string: the value of the option</param>
    /// <param name="Description">string: the description of the option</param>
    // ----------------------------------------------------------------------------------
    public EvOption ( string Value, string Description )
    {
      this._Value = Value.Trim();
      this._Description = Description.Trim();
    }

    // ==================================================================================
    /// <summary>
    /// Constructor with specified initial values
    /// </summary>
    /// <param name="Value">string: the value of the option</param>
    /// <param name="Description">string: the description of the option</param>
    // ----------------------------------------------------------------------------------
    public EvOption ( int Value, string Description )
    {
      this._Value = Value.ToString ( );
      this._Description = Description;
    }

    // ==================================================================================
    /// <summary>
    /// Constructor with specified initial values
    /// </summary>
    /// <param name="Value">string: the value of the option</param>
    // ----------------------------------------------------------------------------------
    public EvOption ( string Value )
    {
      this._Value = Value;
      this._Description = Value;
    }


    #endregion

    #region Property list
    private string _Value = String.Empty;
    /// <summary>
    /// This property contains the option selection value
    /// </summary>
    /// 
    [JsonProperty ( "v" )]
    public string Value
    {
      get { return _Value; }
      set { _Value = value.Trim ( ); }
    }

    private string _Description = String.Empty;

    /// <summary>
    /// This property contains the option description.
    /// </summary>
    [JsonProperty ( "d" )]
    public string Description
    {
      get { return _Description; }
      set { _Description = value.Trim ( ); }
    }

    #endregion
    /// <summary>
    /// This method compare the value with the option value.
    /// </summary>
    /// <param name="Value">delimited ';' list of values.</param>
    /// <returns></returns>
    public bool hasValue ( String Value )
    {
      if ( this._Value == Value )
      {
        return true;
      }

      string [ ] arValues = Value.Split ( ';' );

      foreach ( String val in arValues )
      {
        if ( this._Value == val )
        {
          return true;
        }
      }

      return false;
    }

  }//END Evado.Model.EvOption method

}//END namespace Evado.Model
