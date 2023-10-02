/***************************************************************************************
 * <copyright file="Evado.Model\EvHasSetValue.cs" company="EVADO HOLDING PTY. LTD.">
 *     
 *      Copyright (c) 2002 - 2023 EVADO HOLDING PTY. LTD.  All rights reserved.
 *     
 *      The use and distribution terms for this software are contained in the file
 *      named license.txt, which can be found in the root of this distribution.
 *      By using this software in any fashion, you are agreeing to be bound by the
 *      terms of this license.
 *     
 *      You must not remove this notice, or any other, from this software.
 *     
 * </copyright>
 * 
 * Description: 
 *  This class contains the EvCaseReportForms business object.
 *
 ****************************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace Evado.Model
{

  //  ===========================================================
  /// <summary>
  /// Represents objects who have the set value method implemented.
  /// This is useful for setting the values of the object programatially.
  /// </summary>
  //  -----------------------------------------------------------
  public interface EvHasSetValue < ModelClassFiledNames >
  {

    //  =================================================================================
    /// <summary>
    /// Sets the value on the object. All of the proper validations should be done here.
    /// </summary>
    /// <param name="fieldName"> field of the object to be updated</param>
    /// <param name="value"> value to be setted</param>
    /// <returns>true if the operation passes all of the validations.</returns>
    //  ---------------------------------------------------------------------------------
    Evado.Model.EvEventCodes setValue ( ModelClassFiledNames fieldName, string value );

  }
}
