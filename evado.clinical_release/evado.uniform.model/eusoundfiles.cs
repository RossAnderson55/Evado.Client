/***************************************************************************************
 * <copyright file="Evado.UniForm.Model\SoundFiles.cs" company="EVADO HOLDING PTY. LTD.">
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

  #region Class Enumerators

  /// <summary>
  /// This enumeration defines the the sound file enumeration
  /// </summary>
  [Serializable]
  public enum EuSoundFiles
  {
    /// <summary>
    /// This enumeration defines the EuCommand as not selected or null entry. 
    /// </summary>
    Null,
    /// <summary>
    /// This enumeration defines the edit access is inherited from the parent object.
    /// </summary>
    ECG,
    /// <summary>
    /// This enumeration defines the editing is enabled for this user.
    /// </summary>
    Exhale,
    /// <summary>
    /// This enumeration defines that editing is disabled from this user.
    /// </summary>
    Snoring,

  }//END Enumeration

  #endregion

}//END namespace