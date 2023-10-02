/***************************************************************************************
 * <copyright file="Evado.UniForm.Model\EuEuFieldLayoutCodes.cs" company="EVADO HOLDING PTY. LTD.">
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
  /// This  enumeration defines field justification enumeration class defining a fields property justifications settings.
  /// </summary>
  [Serializable]
  public enum EuFieldLayoutCodes
  { 
    /// <summary>
    /// (Default) This enumeration defines the prompt and field value in two columns, 
    /// This enumeration defines the prompt and field value in two columns, 
    /// where field prompt or title left justified in the first column and the field value
    /// is right justified in the second column.
    /// </summary>
    Default, // json = 0,

    /// <summary>
    /// This enumeration defines the prompt and field value in two columns, 
    /// where field prompt or title is left justified in the first column and the field value
    /// to the left justified of the second column
    /// </summary>
    Left_Justified,  //json = 1

    /// <summary>
    /// This enumeration defines the prompt and field value in two columns, 
    /// where field prompt or title is right justified in the first column and 
    /// the field value is left justified in the second column
    /// </summary>
    Center_Justified, // json = 2

    /// <summary>
    /// This enumeration defines the prompt and field value in one columns 
    /// with two rows, where field prompt or title to the left of the first row 
    /// and the field value to the left of the second row.
    /// </summary>
    Column_Layout // json = 3

  }//END Enumeration
}//END namespace