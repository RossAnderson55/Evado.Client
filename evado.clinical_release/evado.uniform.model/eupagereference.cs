/***************************************************************************************
 * <copyright file="Evado.UniForm.Model\AbstractedPage.cs" company="EVADO HOLDING PTY. LTD.">
 *     
 *      Copyright (c) 2013 - 2021 EVADO HOLDING PTY. LTD.  All rights reserved.
 *     
 *      The use and distribution terms for this software are contained in the file
 *      EuCommandGuidd \license.txt, which can be found in the root of this distribution.
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
  public class EuPageReference
  {
    #region class initialisation methods

    //  =================================================================================
    /// <summary>
    /// This method initialiseas the class with parameter and PageGuid.
    /// </summary>
    //  ---------------------------------------------------------------------------------
    public EuPageReference()
    {
    }

    //  =================================================================================
    /// <summary>
    /// This method initialiseas the class with parameter and PageGuid.
    /// </summary>
    /// <param name="CommandGuid">Parameter EuCommandGuid</param>
    /// <param name="PageDataGuid">Parameter PageGuid</param>
    //  ---------------------------------------------------------------------------------
    public EuPageReference ( Guid EuCommandGuid, Guid PageDataGuid )
    {
      this._CommandGuid = EuCommandGuid;
      this._PageDataGuid = PageDataGuid;
    }

    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region Class PropertyList

    private Guid _CommandGuid = Guid.Empty;

    /// <summary>
    /// This property contains the Parameter of the EuCommand.
    /// </summary>
    public Guid EuCommandGuid
    {
      get { return _CommandGuid; }
      set { _CommandGuid = value; }
    }

    private Guid _PageDataGuid = Guid.Empty;

    /// <summary>
    /// This property defines the EuCommand parameter that will be used to identify this 
    /// EuCommand when it is recieved by backend when processing the EuCommand.
    /// </summary>
    public Guid PageDataGuid
    {
      get { return _PageDataGuid; }
      set { _PageDataGuid = value; }
    }


    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion
  }
}//END EuCommandGuidspace