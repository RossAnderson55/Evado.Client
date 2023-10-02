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
  /// This enumeration list defines the EuCommand header elements.
  /// </summary>
  public enum EuCommandHeaderParameters
  {
    /// <summary>
    /// This enumeration defines not selected state or null value.
    /// </summary>
    Null = 0,

    /// <summary>
    /// This enumeration value defines a header value is a user identifier.
    /// </summary>
    UserId = 1,

    /// <summary>
    /// This enumeration value defines header value is a location latitude
    /// </summary>
    Latitude = 2,

    /// <summary>
    /// This enumeration value defines a header value is a location longitude
    /// </summary>
    Longitude = 3,

    /// <summary>
    /// This enumeration value defines header value is a device identifier
    /// </summary>
    DeviceId = 4,

    /// <summary>
    /// This enumeration value defines header value is a device name
    /// </summary>
    DeviceName = 5,

    /// <summary>
    /// This enumeration value defines header value is a client version
    /// </summary>
    ClientVersion = 6,

    /// <summary>
    /// This enumeration value defines header value is a client OS version
    /// </summary>
    OSVersion = 7,

    /// <summary>
    /// This enumeration value defines header value is a User Url
    /// </summary>
    Client_Url = 8,

    /// <summary>
    /// This enumeration value defines header value is Date time the transaction was created.
    /// </summary>
    DateTime = 9,

    /// <summary>
    /// This enumeration value defines header value is device name.
    /// </summary>
    Device_Model = 10,

    /// <summary>
    /// This enumeration value defines header value is devices application platform.
    /// </summary>
    Platform = 11,

    /// <summary>
    /// This enumeration value defines header value is version of the application platoform.
    /// </summary>
    Framework_Version = 12,

    /// <summary>
    /// This enumeration value defines header value is device client width.
    /// </summary>
    Device_Width = 13,

    /// <summary>
    /// This enumeration value defines header value is device client height.
    /// </summary>
    Device_Height = 14,

    /// <summary>
    /// This enumeration value defines header value is a device idion
    /// </summary>
    DeviceIdiom = 15,

    /// <summary>
    /// This enumeration value defines header value is a device client
    /// </summary>
    User_Url = 16,

  }

}//END namespace