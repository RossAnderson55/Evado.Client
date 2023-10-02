/***************************************************************************************
 * <copyright file="Evado.UniForm.Model\AbstractedPage.cs" company="EVADO HOLDING PTY. LTD.">
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
 *  This class contains the AbstractedPage data object.
 *
 ****************************************************************************************/
using System;

namespace Evado.UniForm.Model
{
  /// <summary>
  /// This class defines the method parameter object structure.
  /// </summary>
  [Serializable]
  public class EuDataObj
  {
    #region class initialisation methods

    //  =================================================================================
    /// <summary>
    /// This method initialises the class with parameter and value.
    /// </summary>
    //  ---------------------------------------------------------------------------------
    public EuDataObj( )
    {
    }

    //  =================================================================================
    /// <summary>
    /// This method initialises the class with parameter and value.
    /// </summary>
    /// <param name="FieldId">String:Parameter name</param>
    /// <param name="Value">String:Parameter value</param>
    /// 
    //  ---------------------------------------------------------------------------------
    public EuDataObj( String FieldId, String Value )
    {
      this._FieldId = FieldId;
      this._Value = Value;
      this._Col = null;
      this._Row = null;
    }

    //  =================================================================================
    /// <summary>
    /// This method initialises the class with parameter and value.
    /// </summary>
    /// <param name="FieldId">String:Parameter name</param>
    /// <param name="Value">String:Parameter value</param>
    /// <param name="Parameters">String: Parameter </param>
    //  ---------------------------------------------------------------------------------
    public EuDataObj( String FieldId, String Value, String Parameters )
    {
      this._FieldId = FieldId;
      this._Value = Value;
      this._Parameters = Parameters;
      this._Col = null;
      this._Row = null;
    }

    //  =================================================================================
    /// <summary>
    /// This method initialises the class with parameter and value.
    /// </summary>
    /// <param name="FieldId">String:Parameter name</param>
    /// <param name="Value">String:Parameter value</param>
    /// <param name="Parameters">String:Parameter</param>
    /// <param name="Row">Int: Row value </param>
    //  ---------------------------------------------------------------------------------
    public EuDataObj( String FieldId, String Value, String Parameters, int Row )
    {
      this._FieldId = FieldId;
      this._Value = Value;
      this._Parameters = Parameters;
      this._Row = Row.ToString( );
      this._Col = null;
    }

    //  =================================================================================
    /// <summary>
    /// This method initialises the class with parameter and value.
    /// </summary>
    /// <param name="FieldId">String:Parameter name</param>
    /// <param name="Value">String:Parameter value</param>
    /// <param name="Row">Int: Row value </param>
    /// <param name="Col">Int: Column value </param>
    //  ---------------------------------------------------------------------------------
    public EuDataObj( String FieldId, String Value, int Row, int Col )
    {
      this._FieldId = FieldId;
      this._Value = Value;
      this._Row = Row.ToString( );
      this._Col = Col.ToString( );
    }

    //  =================================================================================
    /// <summary>
    /// This method initialises the class with parameter and value.
    /// </summary>
    /// <param name="FieldId">String:Parameter name</param>
    /// <param name="Value">String: Parameter value</param>
    /// <param name="Parameters">String: Field parameter as a JSON structure</param>
    /// <param name="Row">Int: Row value </param>
    /// <param name="Col">Int: Column value </param>
    //  ---------------------------------------------------------------------------------
    public EuDataObj( String FieldId, String Value, String Parameters, int Row, int Col )
    {
      this._FieldId = FieldId;
      this._Value = Value;
      this._Parameters = Parameters;
      this._Row = Row.ToString( );
      this._Col = Col.ToString( );
    }

    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region Class global values and constants

    /// <summary>
    /// This contant defines the field parameter new annotation value.
    /// </summary>
    public const string CONST_FIELD_ANNOTATION_NEW_SUFFIX = "_new";

    /// <summary>
    /// This contant defines the field parameter existing annotation value.
    /// </summary>
    public const string CONST_FIELD_PARAMETER_ANNOTATION_EXISTING_JSON = "{\"Ann\":\"Existing\"}";

    //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region Class PropertyList

    private String _FieldId = String.Empty;

    /// <summary>
    /// This property contains the Parameter of the EuCommand.
    /// </summary>
    public String FieldId
    {
      get { return _FieldId; }
      set { _FieldId = value; }
    }

    private String _Value = String.Empty;

    /// <summary>
    /// This property contains the EuCommand parameter that will be used to identify this 
    /// EuCommand when it is recieved by backend and processing the EuCommand.
    /// </summary>
    public String Value
    {
      get { return _Value; }
      set { _Value = value; }
    }

    private String _Parameters = null;

    /// <summary>
    /// This property contains the EuCommand parameter that will be used to identify this 
    /// EuCommand when it is recieved by backend when processing the EuCommand.
    /// </summary>
    public String Parameters
    {
      get { return _Parameters; }
      set { _Parameters = value; }
    }

    String _Row = null;
    /// <summary>
    /// This property contains the row the data value belongs to.  
    /// Default is 0.
    /// </summary>

    public String Row
    {
      get { return _Row; }
      set { _Row = value; }
    }

    String _Col = null;
    /// <summary>
    /// This Property defines the column the data value belongs to. 
    /// Default is 0.
    /// </summary>
    public String Col
    {
      get { return _Col; }
      set { _Col = value; }
    }

    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion
  } 
}//END namespace