/***************************************************************************************
 *      <copyright file="model\EvUrl.cs" company="EVADO HOLDING PTY. LTD.">
 *     
 *      Copyright (c) 2002 - 2023 EVADO HOLDING PTY. LTD.  All rights reserved.
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
 * Description: 
 *  This class defines the EvUrl data object. 
 *
 ****************************************************************************************/

using System;

namespace Evado.Model
{
  /// <summary>
  /// Data model for a history url structure.
  /// </summary>
  [Serializable]
  public class EvUrl
  {
    /// <summary>
    /// The class initialisation method.
    /// </summary>
    public EvUrl( )
    {
    }

    /// <summary>
    /// Class initialisation method.
    /// </summary>
    /// <param name="Url">The URL reference</param>
    /// <param name="Title">The Url Title.</param>
    public EvUrl( string Url, string Title )
    {
      this._Url = Url.Trim();
      this._Title = Title.Trim();
    }

    /// <summary>
    /// Class initialisation method.
    /// </summary>
    /// <param name="Url">The URL reference</param>
    /// <param name="Parameters">The Url parameter string.</param>
    /// <param name="Title">The Url Title.</param>
    public EvUrl( string Url, string Parameters, string Title )
    {
      this._Url = Url.Trim( );
      this._Title = Title.Trim( );
      this._Parameters = Parameters.Trim( );
    }


    /// 
    /// Internal member variables
    /// 
    private string _Url = String.Empty;
    private string _Title = String.Empty;
    private string _Parameters = String.Empty;

    #region Properties
    /// <summary>
    /// This property defines the title
    /// </summary>
    public string Title
    {
      get { return this._Title; }
      set { this._Title = value.Trim(); }
    }

    /// <summary>
    /// This property contains the Url
    /// </summary>
    public string Url
    {
      get { return this._Url; }
      set { this._Url = value.Trim(); }
    }

    /// <summary>
    /// This property contains the parameters
    /// </summary>
    public string Parameters
    {
      get { return this._Parameters; }
      set { this._Parameters = value.Trim( ); }
    }
    #endregion

  } //END StaticUrlUrl method

} //END namespace Evado.cms.Model
