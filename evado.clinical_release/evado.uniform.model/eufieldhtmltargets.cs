/***************************************************************************************
 * <copyright file="Evado.UniForm.Model\Field.cs" company="EVADO HOLDING PTY. LTD.">
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
 *
 ****************************************************************************************/
using System;

namespace Evado.UniForm.Model
{
  /// <summary>
  /// This enumeration contains the field target enumeration settings.
  /// </summary>
  [Serializable]
  public enum EuFieldHtmlTargets
    {
      /// <summary>
      /// This enumeration defines that the html link is to be opened within
      /// the UniFORM client.
      /// </summary>
      Internal = 0,

      /// <summary>
      /// This enumeration defines that the html link is to be opened outside
      /// the UniFORM client.
      /// </summary>
      External = 2
  }//END CLASS

}//END namespace