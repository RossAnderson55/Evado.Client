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
{    /// <summary>
  /// This enumeration defines the page EuCommand types.
  /// </summary>
  [Serializable]
  public enum EuAnonoymousAccessStates
  {
    /// <summary>
    /// This enumeration defines the Normal Access state.
    /// </summary>
    Normal_Access = 0, 

    /// <summary>
    /// This enumeration defines the anonymous page edit access state.
    /// </summary>
    Anonymous_Edit_Access = 1, 

    /// <summary>
    /// This enumeration defines the re-authentication access state
    /// </summary>
    Re_Authentication_Access = 2,

  }//END Enumeration

}//END namespace