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

namespace Evado.Model
{

  /// <summary>
  /// This class defines the data model for  trial or registry schedule milestone activities. 
  /// </summary>
  [Serializable]
  public class EvDataValue
  {
    /// <summary>
    /// This property contains a text values.
    /// </summary>
    [JsonProperty ( "val" )]
    public String Value { get; set; } = String.Empty;

    /// <summary>
    /// This propety contains a numeric (float) value.
    /// </summary>
    [JsonIgnore]
    public float Numeric
    {
      get
      {
        return EvStatics.getFloat ( this.Value );
      }
      set
      {
        this.Value = value.ToString ( );
      }
    }

    /// <summary>
    /// This propety contains a Date  (DateTime) value.
    /// </summary>
    [JsonIgnore]
    public DateTime Date
    {
      get
      {
        return EvStatics.getDateTime ( this.Value );
      }
      set
      {
        this.Value = EvStatics.getDateTimeAsIsoString ( value );
      }
    }

  } //END EvDataValue class

} //END namespace Evado.Model
