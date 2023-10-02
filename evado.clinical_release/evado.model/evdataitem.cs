/***************************************************************************************
 * <copyright file="EvDataItem.cs" company="EVADO HOLDING PTY. LTD.">
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
 *  This class contains the EvDataItem data object.
 *
 ****************************************************************************************/

using System;

namespace Evado.Model
{
  /// <summary>
  /// data  entity used to model accounts
  /// </summary>
  [Serializable]
  public class EvDataItem
  {

    #region Internal member variables

    private string _SubjectId = String.Empty;
    private string _FieldId = String.Empty;
    private string _Subject = String.Empty;
    private string _DataPoint = String.Empty;
    private float _Value = 0;
    private string _Unit = String.Empty;

    #endregion

    #region Properties
    /// <summary>
    /// This property contains a subject identifier of the data item
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
    /// This property contains a field identifier of the data item
    /// </summary>
    public string FieldId
    {
      get
      {
        return this._FieldId;
      }
      set
      {
        this._FieldId = value;
      }
    }

    /// <summary>
    /// This property contains a subject of the data item
    /// </summary>
    public string Subject
    {
      get
      {
        return this._Subject;
      }
      set
      {
        this._Subject = value;
      }
    }

    /// <summary>
    /// This property contains a data point of the data item
    /// </summary>
    public string DataPoint
    {
      get
      {
        return this._DataPoint;
      }
      set
      {
        this._DataPoint = value;
      }
    }

    /// <summary>
    /// This property contains value of the data item
    /// </summary>
    public float Value
    {
      get
      {
        return this._Value;
      }
      set
      {
        this._Value = value;
      }
    }

    /// <summary>
    /// This property contains a unit of the data item
    /// </summary>
    public string Unit
    {
      get
      {
        return this._Unit;
      }
      set
      {
        this._Unit = value;
      }
    }
    #endregion

  }//END class TestDataPointIndex

}//END Namespace Evado.Model
