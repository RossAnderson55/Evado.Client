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
  /// <summary>
  /// This class contains the user session data for the web client.
  /// </summary>
  public class EucKeyValuePair
  {
    private String _Key = String.Empty;

    public String Key
    {
      get { return _Key; }
      set { _Key = value; }
    }
    private String _Value = String.Empty;

    public String Value
    {
      get { return _Value; }
      set { _Value = value; }
    }
  }

}//END NameSpace
