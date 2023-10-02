/***************************************************************************************
 * <copyright file="model\EvActivity.cs" company="EVADO HOLDING PTY. LTD.">
 *     
 *      Copyright (c) 2002 - 2023 EVADO HOLDING PTY. LTD.  All rights reserved.
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
 *  This class contains the EvActivity data object.
 *
 ****************************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;

namespace Evado.Model
{

  /// <summary>
  /// Business entity used to model accounts
  /// </summary>
  [Serializable]
  public class EvDataBaseUpdate
  {
    #region class enumeartion
    /// <summary>
    /// This enumeration list contains the order state of database update
    /// </summary>
    public enum UpdateOrderBy
    {
      /// <summary>
      /// This enumeration value defines database update order by update number
      /// </summary>
      UpdateNo,

      /// <summary>
      /// This enumeration value defines database update order by date
      /// </summary>
      Date,

      /// <summary>
      /// This enumeration value defines database update order by version
      /// </summary>
      Version
    }

    /// <summary>
    /// This emumeration class defines the versions for the database update.
    /// </summary>
    public enum UpdateVersionList
    {
      /// <summary>
      /// This enumeration indicates that all versions are selected.
      /// </summary>
      Null,

      /// <summary>
      /// This enumeration indicates that versin 2 updates are selected.
      /// </summary>
      Version_2,

      /// <summary>
      /// This enumeration indicates that versin 3 updates are selected.
      /// </summary>
      Version_3,

      /// <summary>
      /// This enumeration indicates that versin 4 updates are selected.
      /// </summary>
      Version_4,

      /// <summary>
      /// This enumeration indicates that versin 4.1 updates are selected.
      /// </summary>
      Version_4_1,

      /// <summary>
      /// This enumeration indicates that versin 4.2 updates are selected.
      /// </summary>
      Version_4_2,

      /// <summary>
      /// This enumeration indicates that versin 4.3 updates are selected.
      /// </summary>
      Version_4_3,

      /// <summary>
      /// This enumeration indicates that versin 4.4 updates are selected.
      /// </summary>
      Version_4_4,

      /// <summary>
      /// This enumeration indicates that versin 4.5 updates are selected.
      /// </summary>
      Version_4_5,

      /// <summary>
      /// This enumeration indicates that versin 4.6 updates are selected.
      /// </summary>
      Version_4_6,

      /// <summary>
      /// This enumeration indicates that versin 4.6 updates are selected.
      /// </summary>
      Version_4_7,

      /// <summary>
      /// This enumeration indicates that versin 4.6 updates are selected.
      /// </summary>
      Version_4_8,

      /// <summary>
      /// This enumeration indicates that versin 5 updates are selected.
      /// </summary>
      Version_5,

      /// <summary>
      /// This enumeration indicates that versin 5.1 updates are selected.
      /// </summary>
      Version_5_1,

      /// <summary>
      /// This enumeration indicates that versin 5.2 updates are selected.
      /// </summary>
      Version_5_2,

      /// <summary>
      /// This enumeration indicates that versin 5.3 updates are selected.
      /// </summary>
      Version_5_3,

      /// <summary>
      /// This enumeration indicates that versin 5.4 updates are selected.
      /// </summary>
      Version_5_4,

      /// <summary>
      /// This enumeration indicates that versin 5.5 updates are selected.
      /// </summary>
      Version_5_5,

      /// <summary>
      /// This enumeration indicates that versin 5.6 updates are selected.
      /// </summary>
      Version_5_6,

      /// <summary>
      /// This enumeration indicates that versin 5.7 updates are selected.
      /// </summary>
      Version_5_7,

      /// <summary>
      /// This enumeration indicates that versin 6 updates are selected.
      /// </summary>
      Version_6,

      /// <summary>
      /// This enumeration indicates that versin 6 updates are selected.
      /// </summary>
      Version_7,
    }

    #endregion

    #region Properties
    /// <summary>
    /// This property contains the Guid of the DataBase Update
    /// </summary>
    public Guid Guid { get; set; } = Guid.Empty;

    /// <summary>
    /// This property defines the update number of the DataBase Update
    /// </summary>
    public int UpdateNo { get; set; } = 0;

    /// <summary>
    /// This property defines the date time stamp of the DataBase Update
    /// </summary>
    public DateTime UpdateDate { get; set; } = Evado.Model.EvStatics.CONST_DATE_NULL;

    /// <summary>
    /// This property contains the date of the DataBase Update
    /// </summary>
    public String stUpdateDate
    {
      get
      {
        return this.UpdateDate.ToString ( "dd MMM yyyy HH:mm" );
      }
    }
    /// <summary>
    /// This property defines the object of the DataBase Update
    /// </summary>
    public string Objects { get; set; } = String.Empty;

    /// <summary>
    /// This property contains version of the DataBase Update
    /// </summary>
    public string Version { get; set; } = String.Empty;

    /// <summary>
    /// This propery contains the description of the DataBase update
    /// </summary>
    public string Description { get; set; } = String.Empty;
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region static methods.

    /// <summary>
    /// This method returns the SQL version of the version enumeration.
    /// </summary>
    public static string SqlVersion ( EvDataBaseUpdate.UpdateVersionList Version )
    {
      String version = Version.ToString ( );
      version = version.Replace ( "Version_", String.Empty );
      version = version.Replace ( "_", "." );

      return version;
    }

    #endregion

  }//END EvDataBaseUpdate method

}//END namespace Evado.Model
