/***************************************************************************************
 * <copyright file="model\EvUserProfile.cs" company="EVADO HOLDING PTY. LTD.">
 *     
 *      Copyright (c) 2002 - 2022 EVADO HOLDING PTY. LTD.  All rights reserved.
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
 *  This class contains the EvUserProfile data object.
 *
 ****************************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Evado.Model
{
  /// <summary>
  /// This class provide user profile for using across the application
  /// </summary>
  [Serializable]
  public class EusTokenUserProfile
  {
    #region Class enumerators

    /// <summary>
    /// This enumerated list defines the user status for the Nakoudu Evado Digital service.
    /// </summary>
    public enum UserStatusCodes
    {
      /// <summary>
      /// This enumerated state indicates that the user status is not set.
      /// Passing this status will be considered the same as an 'Invalid_User'
      /// </summary>
      Null = 0,

      /// <summary>
      /// This enumerated state indicates that the user is new subscriber.
      /// </summary>
      New_User = 1,

      /// <summary>
      /// This enumerated state indicates that the user is a subscriber.
      /// </summary>
      Subscribed_User = 2,

      /// <summary>
      /// This enumerated state indicates that the user is Invalid.
      /// </summary>
      Invalid_User = 3,

      /// <summary>
      /// This enumerated state indicates that the user has been terminated i.e. no longer a subscriber.
      /// </summary>
      Terminated_User = 4,

      /// <summary>
      /// This enumerated state indicates that the user is a demonstration user with limit access and duration.
      /// </summary>
      Demonstration_User = 5,
    }

    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region Class Properties

    /// <summary>
    /// This property contains the guid user token from an external system.
    /// 
    /// The Application Identifier must have the value 'Evado_Digital'.
    /// </summary>
    [JsonProperty ( "aid" )]
    public String ApplicationID = "Evado_Digital";

    /// <summary>
    /// This property contains the guid user token from an external system.
    /// 
    /// The token value will become the Guid identifier for the user proifle within Evado Digital.
    /// </summary>
    [JsonProperty ( "tid" )]
    public Guid Token { get; set; }

    /// <summary>
    /// This property defines the user identifier
    /// </summary>
    [JsonProperty ( "u" )]
    public string UserId { get; set; }

    /// <summary>
    /// This property contains user's given name
    /// </summary>
    [JsonProperty ( "gn" )]
    public string GivenName { get; set; }

    /// <summary>
    /// This property contains user family name
    /// </summary>
    [JsonProperty ( "fn" )]
    public string FamilyName { get; set; }

    /// <summary>
    /// This property contains user's email address
    /// </summary>
    [JsonProperty ( "ue" )]
    public string EmailAddress { get; set; }

    /// <summary>
    /// This property contains user type identifier
    /// </summary>
    /// <remarks>
    /// The list of user types:
    ///   Value   Description
    ///   CI01  =	Corporate Innovators  (Premium Subscription)
    ///   DP01  =	Data Provider (Premium Subscription)
    ///   NA01  =	Network Alumni (Standard Subscription)
    ///   SU02  =	Scaleup - Small business more than $1m revenue (Standard Subscription)
    ///   SS01  =	Sector Supporter  (Standard Subscription)
    ///   SC01  =	Smart City  (Premium Subscription)
    ///   SU01  =	Startup- Small business less than $1m revenue  (Standard Subscription)
    ///   TT01  =	Tech Transfer - university to newco (Standard Subscription)
    ///   TB01  =	Transforming business  (Premium Subscription)
    /// </remarks>
    [JsonProperty ( "ut" )]
    public string UserType { get; set; }

    /// <summary>
    /// This property contains user type
    /// </summary>
    [JsonProperty ( "us" )]
    public UserStatusCodes UserStatus { get; set; }

    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion


  }//END EvTokenUserProfile method

} //END namespace Evado.Model