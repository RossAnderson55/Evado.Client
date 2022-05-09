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
using System.Collections.Generic;


namespace Evado.Model 
{
	/// <summary>
	/// Business entity used to model accounts
	/// </summary>
	[Serializable]
  public class EvSignatureBlock
	{
    /// <summary>
    /// This class contains a signature block object 
    /// </summary>
    public EvSignatureBlock ( )
    {
      this.Signature = new List<EvSegement> ( );
      this.Name = String.Empty;
      this.AcceptedBy = String.Empty;
      this.DateStamp = Evado.Model.EvStatics.CONST_DATE_NULL;
    }
    /// <summary>
    /// this propoerty contains the rastergraphic of the signature.
    /// </summary>
    public List<EvSegement> Signature { get; set; }

    /// <summary>
    /// This property contains the name of the peros's signature.
    /// </summary>
    public String Name { get; set; }

    /// <summary>
    /// This property contains the name of the person accepting the signature.
    /// </summary>
    public String AcceptedBy { get; set; }

    /// <summary>
    /// This property contains the name of the date time stamp of the signature the signature.
    /// </summary>
    public DateTime DateStamp { get; set; }

  } // Close class Segement

} // Close namespace Evado.UniForm.Model
