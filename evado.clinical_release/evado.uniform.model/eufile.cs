/***************************************************************************************
 * <copyright file="model\EvUserRegistration.cs" company="EVADO HOLDING PTY. LTD.">
 *     
 *      Copyright (c) 2001 - 2012 EVADO HOLDING PTY. LTD..  All rights reserved.
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
 *  This class contains the EvUserRegistration data object.
 *
 ****************************************************************************************/

using Newtonsoft.Json;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;

namespace Evado.UniForm.Model
{
  /// <summary>
  /// Business entity used to model accounts
  /// </summary>
  [Serializable]
  public class EuFile
  {
    #region class enumerations
    /// <summary>
    /// This enumeration defines the web file service action.
    /// </summary>
    public enum WebAction
    {
      /// <summary>
      /// this enumerated value defines the file upload action.
      /// </summary>
      Upload,

      /// <summary>
      /// This enumerated value defines the file download action.
      /// </summary>
      Download
    }

    // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region class properties

    /// <summary>
    /// This property contains the GUID device identifier.
    /// </summary>
    [JsonProperty ( "id" )]
    public Guid Identifier { get; set; } = Guid.Empty;

    /// <summary>
    /// this property contains the user identifier.
    /// </summary>
    [JsonProperty ( "cs" )]
    public string ClientSession { get; set; } = String.Empty;

    /// <summary>
    /// this property contains the user identifier.
    /// </summary>
    [JsonProperty ( "usr" )]
    public string UserId { get; set; } = String.Empty;

    /// <summary>
    /// This property contains the file name.
    /// </summary>
    [JsonProperty ( "fn" )]
    public string FileName { get; set; } = String.Empty;

    /// <summary>
    /// This property contains file's mimi type.
    /// </summary>
    [JsonProperty ( "mim" )]
    public string MimeType { get; set; } = String.Empty;

    /// <summary>
    /// this property contains the error message.
    /// </summary>
    [JsonProperty ( "em" )]
    public string ErrroMessage { get; set; } = String.Empty;

    /// <summary>
    /// this property defines the file web service action.
    /// </summary>
    [JsonProperty ( "wsa" )]
    public WebAction Action { get; set; } = WebAction.Upload;

    /// <summary>
    /// this property contains the post status;
    /// </summary>
    [JsonProperty ( "fs" )]
    public Evado.Model.EvEventCodes FileStatus { get; set; } = Evado.Model.EvEventCodes.Ok;

    /// <summary>
    /// This property contains the device registration date.
    /// </summary>
    [ JsonProperty ( "fd" )]
    public byte [] FileData { get; set; }
    // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

  } // Close class User

} // Close namespace Evado.Model