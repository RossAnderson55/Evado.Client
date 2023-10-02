/***************************************************************************************
 * <copyright file="Evado.UniForm.Model\Offline.cs" company="EVADO HOLDING PTY. LTD.">
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
using System.Collections.Generic;


namespace Evado.UniForm.Model
{
  /// <summary>
  /// This class contains the device client page description.
  /// </summary>
  // ----------------------------------------------------------------------------------
  [Serializable]
  public class EuOffline
  {
    #region class PropertyList.

    private Guid _Id = Guid.Empty;

    /// <summary>
    /// This property contains an identifier for the group object.
    /// </summary>
    public Guid Id
    {
      get { return this._Id; }
      set { this._Id = value; }
    }

    private String _SelectionFilter = null;

    /// <summary>
    ///  This property contains selection filter to update a list of page object on a selection page.
    /// </summary>
    public String SelectionFilter
    {
      get { return this._SelectionFilter; }
      set { this._SelectionFilter = value; }
    }

    private List<EuPage> _PageList = new List<EuPage>( );

    /// <summary>
    /// This property contains the list of Page objects.
    /// </summary>

    public List<EuPage> PageList
    {
      get { return this._PageList; }
      set { this._PageList = value; }
    }

    private List<EuPageData> _PageDataList = new List<EuPageData>( );

    /// <summary>
    /// This property contains a list of page data objects.
    /// </summary>
    // ----------------------------------------------------------------------------------
    public List<EuPageData> PageDataList
    {
      get { return this._PageDataList; }
      set { this._PageDataList = value; }
    }

    /*
    private List<BinaryReference> _BinaryObjectList = new List<BinaryReference> ( );
    // ==================================================================================
    /// <summary>
    /// this property contains a list of binary data objects to be downloaded.
    /// </summary>
    // ----------------------------------------------------------------------------------
    public List<BinaryReference> BinaryObjectList
    {
      get { return this._BinaryObjectList; }
      set { this._BinaryObjectList = value; }
    }
    */
    /*
    private List<JavaLibaryReference> _JavaLibraryReferences = new List<JavaLibaryReference> ( );

    // ==================================================================================
    /// <summary>
    /// This property contains the offline java library
    /// </summary>
    // ----------------------------------------------------------------------------------
    public List<JavaLibaryReference> JavaLibraryReferences
    {
      get { return this._JavaLibraryReferences; }
      set { this._JavaLibraryReferences = value; }
    }
     */

    // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region class methods


    // ==================================================================================
    /// <summary>
    /// This method generates appends the page to the page object list..
    /// </summary>
    /// <param name="OffinePage">Evado.UniForm.Model.EuPage: object Evado.UniForm.Model.EuPage</param>
    /// <remarks> 
    /// This method consists of following steps. 
    /// 
    /// 1. Iterate through the offline pages to determine if the page exists and if not add it.
    /// 
    /// </remarks>
    // ----------------------------------------------------------------------------------
    public void AppendPage( Evado.UniForm.Model.EuPage OffinePage )
    {
      //
      // Iterate through the offline pages to determine if the page exists and if not add it.
      //
      foreach ( Evado.UniForm.Model.EuPage page in this._PageList )
      {
        if ( page.PageId == OffinePage.PageId )
        {
          return;
        }
      }

      this._PageList.Add( OffinePage );

    }//END AppendPage method

    // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

  }
}