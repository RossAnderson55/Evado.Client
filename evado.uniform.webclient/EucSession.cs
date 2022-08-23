/***************************************************************************************
 * <copyright file="webclinical\EVADO HOLDING PTY. LTD.EventLog.cs" company="EVADO HOLDING PTY. LTD.">
 *     
 *      Copyright (c) 2011 - 2022 EVADO HOLDING PTY. LTD.  All rights reserved.
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
  /// <summary>
  /// This class contains the user session data for the web client.
  /// </summary>
  public class EucSession
  {
    /// <summary>
    /// This field defines the user's current authentication state.
    /// </summary>
    public EucAuthenticationStates UserState = EucAuthenticationStates.Un_Authenticated;

    /// <summary>
    /// This field contains the current application data object.
    /// </summary>
    public Evado.UniForm.Model.EuAppData AppData = new Evado.UniForm.Model.EuAppData ( );

    public Evado.UniForm.Model.EuAppData.StatusCodes ServerStatus
    {
      get
      {
        return AppData.Status;
      }
    }

    /// <summary>
    /// this field contains the current command object.
    /// </summary>
    public Evado.UniForm.Model.EuCommand PageCommand = new Evado.UniForm.Model.EuCommand ( );

    /// <summary>
    /// This field conaints the retrieved exteral command object.
    /// </summary>
    public Evado.UniForm.Model.EuCommand ExternalCommand = null;

    /// <summary>
    /// This field contains the list of command history for this user.
    /// </summary>
    public List<Evado.UniForm.Model.EuCommand> CommandHistoryList = new List<Evado.UniForm.Model.EuCommand> ( );

    /// <summary>
    /// This field contains the user server cookie container
    /// </summary>
    public CookieContainer CookieContainer = new CookieContainer ( );

    /// <summary>
    /// This field contains the page URL used for external commands.
    /// </summary>
    public String PageUrl = String.Empty;

    /// <summary>
    /// This field contains the servers session identifier for the user.
    /// </summary>
    public String ServerSessionId = String.Empty;

    /// <summary>
    /// This field contains the current command Guid 
    /// </summary>
    public Guid CommandGuid = Guid.Empty;

    /// <summary>
    /// This field contains the current group object.
    /// </summary>
    public Evado.UniForm.Model.EuGroup CurrentGroup = new Evado.UniForm.Model.EuGroup ( );

    /// <summary>
    /// This field defines the group field width as a percentage of the page width.
    /// </summary>
    public int GroupFieldWidth = 60;

    /// <summary>
    /// This field contains the panel display group index.
    /// </summary>
    public int PanelDisplayGroupIndex = -1;

    /// <summary>
    /// This field contains the field annotation list for the current field .
    /// </summary>
    public List<EucKeyValuePair> FieldAnnotationList = new List<EucKeyValuePair> ( );

    /// <summary>
    /// This field contains a list of the icons for the current page command list.
    /// </summary>
    public List<EucKeyValuePair> IconList = new List<EucKeyValuePair> ( );

    /// <summary>
    /// This field contains the user identifier.
    /// </summary>
    public String UserId = String.Empty;

    /// <summary>
    /// This field contains the user's password.
    /// </summary>
    public string Password = String.Empty;

    /// <summary>
    /// This field indicates that an request login.
    /// </summary>
    public bool RequestLogin = false;

    /// <summary>
    /// This field indicates that an external command has been received.
    /// </summary>
    public bool IsExternalCommand = false;

    /// <summary>
    /// This field indicates that the plot script has been loaded from the field data.
    /// </summary>
    public bool PlotScriptLoaded = false;


  }//END Class

}//END NameSpace
