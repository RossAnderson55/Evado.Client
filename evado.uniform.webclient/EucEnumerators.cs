/***************************************************************************************
 * <copyright file="webclinical\EVADO HOLDING PTY. LTD.EventLog.cs" company="EVADO HOLDING PTY. LTD.">
 *     
 *      Copyright (c) 2011 - 2025 EVADO HOLDING PTY. LTD.  All rights reserved.
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
 ****************************************************************************************/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Configuration;
using System.Web;
using System.Web.SessionState;
using System.Web.Security;
using System.Net;

//Evado. namespace references.

namespace Evado.UniForm.WebClient
{

  public enum EucAuthenticationStates
  {
    /// <summary>
    /// This enumeratied value indicates that the user had not been authenticated.
    /// Forcing the page to display the login panel.
    /// </summary>
    Un_Authenticated,

    /// <summary>
    /// This enumerated value indicates that the user has been authenticated by the servers.
    /// </summary>
    Authenticated,

    /// <summary>
    /// This enumerated value indicates that the user has been authenticated by the client.
    /// </summary>
    Network_Authenticated,

    /// <summary>
    /// This enumerated value indicates that the user has a anonymous authentication.
    /// </summary>
    AnonymousAuthentication,
  }



}//END NameSpace
