/***************************************************************************************
 * <copyright file="Evado.UniForm.Model\EditCodes.cs" company="EVADO HOLDING PTY. LTD.">
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
using System.Collections.Generic;

namespace Evado.UniForm.Model
{
  ///<summary>
  /// This enumeration contains edit status. These page objects are rendered 
  /// into page fields on the client device.
  /// </summary>

  public enum EuEditAccess
  {
    /// <summary>
    /// This enumeration defines non selected or null entry.
    /// </summary>
    Null = 0,

    /// <summary>
    /// This enumeration defines the edit access is inherited from the parent object.
    /// </summary>
    Inherited = 1,

    /// <summary>
    /// This enumeration defines the editing is enabled for this user.
    /// </summary>
    Enabled = 2,

    /// <summary>
    /// This enumeration defines that editing is disabled from this user.
    /// </summary>
    Disabled = 3,

  }//END Enumeration

}//END namespace