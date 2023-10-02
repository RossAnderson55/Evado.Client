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

using System;
using System.Collections;
using System.Collections.Generic;

namespace Evado.UniForm.Model
{
  /// <summary>
  /// Business entity used to model accounts
  /// </summary>
  [Serializable]
  public class EuDevice
  {

    private Guid _Identifier = Guid.Empty;
    /// <summary>
    /// This property contains the GUID device identifier.
    /// </summary>
    public Guid Identifier
    {
      get { return this._Identifier; }
      set { this._Identifier = value; }
    }

    private string _UserId = String.Empty;
    /// <summary>
    /// this property contains the user identifier that registered the device.
    /// </summary>
    public string UserId
    {
      get { return this._UserId; }
      set { this._UserId = value; }
    }

    private string _ServiceId = String.Empty;
    /// <summary>
    /// This property contains the service identifier the device is registered for.
    /// </summary>
    public string ServiceId
    {
      get { return this._ServiceId; }
      set { this._ServiceId = value; }
    }

    private string _DeviceId = String.Empty;
    /// <summary>
    /// This property defines the mobil client device identifier.
    /// </summary>
    public string DeviceId
    {
      get { return this._DeviceId; }
      set { this._DeviceId = value; }
    }

    private string _DeviceName = String.Empty;
    /// <summary>
    /// This property contains the device name.
    /// </summary>
    public string DeviceName
    {
      get { return this._DeviceName; }
      set { this._DeviceName = value; }
    }

    private string _DeviceOs = String.Empty;
    /// <summary>
    /// This property contains the device operation system reference.
    /// </summary>
    public string DeviceOs
    {
      get { return this._DeviceOs; }
      set { this._DeviceOs = value; }
    }

    private DateTime _RegistrationDate =  Evado.Model.EvStatics.CONST_DATE_NULL;
    /// <summary>
    /// This property contains the device registration date.
    /// </summary>
    public DateTime RegistrationDate
    {
      get { return this._RegistrationDate; }
      set { this._RegistrationDate = value; }
    }

  } // Close class User

} // Close namespace Evado.Model