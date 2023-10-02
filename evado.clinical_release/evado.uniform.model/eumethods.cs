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
  /// This is an enumeration of application methods.  
  /// The methods to be called in an application object.
  /// </summary>
  [Serializable]
  public enum EuMethods
  {
    /// <summary>
    /// This enumeration defines not selected state or null value.
    /// </summary>
    Null = 0,  // json enumeration: 0

    /// <summary>
    /// This enumeration defines that the selected method is to generate a list of objects.
    /// </summary>
    List_of_Objects = 1, // json enumeration: 1 

    /// <summary>
    /// This enumeration defines that the selected method is to retrieve an object.
    /// </summary>
    Get_Object = 2, // json enumeration: 2

    /// <summary>
    /// This enumeration defines that the selected method is to create an object.
    /// </summary>
    Create_Object = 3, // json enumeration: 3

    /// <summary>
    /// This enumeration defines that the selected method is to save an object.
    /// </summary>
    Save_Object = 4, // json enumeration: 4

    /// <summary>
    /// This enumeration defines that the selected method is to delete an object.
    /// </summary>
    Delete_Object = 5, // json enumeration: 5

    /// <summary>
    /// This enumeration defines that the selected method is a custom method.
    /// </summary>
    Custom_Method = 6, // json enumeration: 6
  }

}//END namespace