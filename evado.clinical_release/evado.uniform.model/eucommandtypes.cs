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
  public enum EuCommandTypes
  {
    /// <summary>
    /// This enumeration defines the EuCommand as not selected or null entry.
    /// </summary>
    Null = 0,  // json enumeration: 0

    /// <summary>
    /// This enumeration defines the EuCommand as a normal internal EuCommand.
    /// </summary>
    Normal_Command = 1, // json enumeration: 1

    /// <summary>
    /// This enumeration defines the EuCommand as an html link that will call an external data object.
    /// </summary>
    Http_Link = 2, // json enumeration: 2

    /// <summary>
    /// This enumeration defines the EuCommand as a login EuCommand.
    /// </summary>
    Login_Command = 3,// json enumeration: 3

    /// <summary>
    /// This enumeration defines the EuCommand as a logout EuCommand.
    /// </summary>
    Logout_Command = 4,// json enumeration: 4

    /// <summary>
    /// This enumeration defines the EuCommand as a device application registration EuCommand.
    /// </summary>
    Register_Device_Client = 5,// json enumeration: 5

    /// <summary>
    /// This enumeration defines the EuCommand commences offline operation
    /// </summary>
    Offline_Command = 6,// json enumeration: 6

    /// <summary>
    /// This enumeration defines the EuCommand uploaded data to be synchronised with the online environment.
    /// </summary>
    Synchronise_Save = 7,// json enumeration: 7

    /// <summary>
    /// This enumeration value defines the EuCommand uploaded data to be added to the online environment.
    /// </summary>
    Synchronise_Add = 8,// json enumeration: 8

    /// <summary>
    /// This enumeration value defines a direct external EuCommand, this user is not validated prior to executing the EuCommand.  
    /// This EuCommand must have a Guid identifier that will open the relevant application object and all history navigation is disabled.  
    /// This means that the user can only access the page the Guid references in the application. 
    /// The application must handle all page EuCommands to ensure that the user is isolated from the remainder of the application.
    /// </summary>
    Anonymous_Command = 9,// json enumeration: 9

    /// <summary>
    /// This enumeration value a login event where the user is authenticated by the network they are using, 
    /// e.g. Basic web authentication or Windows Integrated Authenciation.
    /// For this type of authentication the user's network identity is passed to the UniFORM service.
    /// </summary>
    Network_Login_Command = 10,// json enumeration: 10

    /// <summary>
    /// This enumeration value identifies a re-authentication EuCommand and forces a re-authentication of the user's
    /// credentials.  
    /// - Passing the authentication continues the execution. 
    /// - Failing the authentication forces a fresh user authantication.
    /// </summary>
    Re_Authentication_Command = 11,// json enumeration: 11

    /// <summary>
    /// This enumeration value a login event where the user is authenticated considered already authenticated and 
    /// User identifier token is provided to retrieve the users profile.
    /// </summary>
    //Token_Login_Command = 12,// json enumeration: 12

    /// <summary>
    /// This enumeration value a login event where the user is authenticated considered already authenticated and 
    /// User identifier token is provided to retrieve the users profile.
    /// </summary>
    Meeting_Command = 13,// json enumeration: 12

  }//END Enumeration

}//END namespace