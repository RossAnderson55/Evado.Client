/***************************************************************************************
 * <copyright file="Evado.UniForm.Model\Option.cs" company="EVADO HOLDING PTY. LTD.">
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
 *  This class contains the Evado.Model.EvOption data object.
 *
 ****************************************************************************************/

using System;

namespace Evado.Model 
{
	/// <summary>
	/// Business entity used to model accounts
	/// </summary>
	[Serializable]
	public class EvSegement
	{
    int _lx = 0;
    /// <summary>
    /// This property defines the lower x value of a segment.
    /// </summary>
    public int lx
    {
      get { return _lx; }
      set { _lx = value; }
    }

    int _mx = 0;
    /// <summary>
    /// This property defines the maximum x value of a segment.
    /// </summary>
    public int mx
    {
      get { return _mx; }
      set { _mx = value; }
    }

    int _ly = 0;
    /// <summary>
    /// This property defines the lower y value of a segment.
    /// </summary>
    public int ly
    {
      get { return _ly; }
      set { _ly = value; }
    }

    int _my = 0;
    /// <summary>
    /// This property defines the lower x value of a segment.
    /// </summary>
    public int my
    {
      get { return _my; }
      set { _my = value; }
    }

  } // Close class Segement

} // Close namespace Evado.UniForm.Model
