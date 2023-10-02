/***************************************************************************************
 * <copyright file="Evado.UniForm.Model\AbstractedPage.cs" company="EVADO HOLDING PTY. LTD.">
 *     
 *      Copyright (c) 2013 - 2021 EVADO HOLDING PTY. LTD.  All rights reserved.
 *     
 *      The use and distribution terms for this software are contained in the file
 *      FieldIdd \license.txt, which can be found in the root of this distribution.
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
using System.Collections.Generic;

namespace Evado.UniForm.Model
{
  /// <summary>
  /// This class defines the method parameter object structure.
  /// </summary>
  [Serializable]
  public class EuBinaryReference
  {
    #region class initialisation methods

    //  =================================================================================
    /// <summary>
    /// This method initialiseas the class with parameter and FileName.
    /// </summary>
    //  ---------------------------------------------------------------------------------
    public EuBinaryReference( )
    {
    }

    //  =================================================================================
    /// <summary>
    /// This method initialiseas the class with parameter and FileName.
    /// </summary>
    /// <param name="FieldId">Parameter FieldId</param>
    /// <param name="FileName">Parameter FileName</param>
    //  ---------------------------------------------------------------------------------
    public EuBinaryReference( Guid FieldId, String FileName )
    {
      this._FieldId = FieldId;
      this._FileName = FileName;
    }

    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region Class PropertyList

    private Guid _FieldId = Guid.Empty;

    /// <summary>
    /// This property contains an global unique identifier for the field the file is associated with.
    /// </summary>
    public Guid FieldId
    {
      get { return this._FieldId; }
      set { this._FieldId = value; }
    }

    private String _FileName = String.Empty;

    /// <summary>
    /// This property contains the filename. 
    /// </summary>
    public String FileName
    {
      get { return this._FileName; }
      set { this._FileName = value; }
    }

    private bool _Updated = false;
    /// <summary>
    /// This property contains update status.
    /// </summary>
    public bool Updated
    {
      get { return this._Updated; }
      set { this._Updated = value; }
    }

    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion
  }
}//END FieldIdspace