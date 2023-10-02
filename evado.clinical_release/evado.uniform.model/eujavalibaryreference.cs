/***************************************************************************************
 * <copyright file="Evado.UniForm.Model\AbstractedPage.cs" company="EVADO HOLDING PTY. LTD.">
 *     
 *      Copyright (c) 2013 - 2021 EVADO HOLDING PTY. LTD.  All rights reserved.
 *     
 *      The use and distribution terms for this software are contained in the file
 *      LibaryNamed \license.txt, which can be found in the root of this distribution.
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
  public class EuJavaLibaryReference
  {
    #region class initialisation methods

    //  =================================================================================
    /// <summary>
    /// This method initialises the class with parameter and FileName.
    /// </summary>
    //  ---------------------------------------------------------------------------------
    public EuJavaLibaryReference( )
    {
    }

    //  =================================================================================
    /// <summary>
    /// This method initialises the class with parameter and FileName.
    /// </summary>
    /// <param name="LibaryName">Guid: Parameter library name</param>
    /// <param name="FileName">String: Parameter file name</param>
    //  ---------------------------------------------------------------------------------
    public EuJavaLibaryReference( Guid LibaryName, String FileName )
    {
      this._LibaryName = LibaryName;
      this._FileName = FileName;
    }

    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region Class PropertyList

    private Guid _LibaryName = Guid.Empty;

    /// <summary>
    /// This property contains an identifier for the JaveLibaryReference object.
    /// </summary>
    public Guid LibaryName
    {
      get { return this._LibaryName; }
      set { this._LibaryName = value; }
    }

    private String _FileName = String.Empty;

    /// <summary>
    /// This property contains the binary file name.
    /// </summary>
    public String FileName
    {
      get { return this._FileName; }
      set { this._FileName = value; }
    }

    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion
  }
}//END LibaryNamespace