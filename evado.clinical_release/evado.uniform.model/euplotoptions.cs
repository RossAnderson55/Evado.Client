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
using System.Collections.Generic;

namespace Evado.UniForm.Model
{
  /// <summary>
  /// This class defines the method parameter object structure.
  /// </summary>
  [Serializable]
  public class EuPlotOptions
  {
    #region class initialisation methods

    //  =================================================================================
    /// <summary>
    /// This method initialises the class with parameters and values.
    /// </summary>
    //  ---------------------------------------------------------------------------------
    public EuPlotOptions ( )
    {
    }

    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region Class public enumerated lists

    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region Class Properties

    /// <summary>
    /// this property contains the JSON pie configuration.
    /// </summary>
    public String Pie { get; set; }

    /// <summary>
    /// this property contains the JSON legend configuration.
    /// </summary>
    public String Legend { get; set; }

    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region Class methods

    //====================================================================================
    /// <summary>
    /// This method add a value to the plot data list.
    /// Using only the first 2 values of the array of float.
    /// </summary>
    /// <returns>String containing the JSON class output</returns>
    //-----------------------------------------------------------------------------------
    public void SetLegend ( bool Show, string position )
    {
      if ( Show == false )
      {
        this.Legend = "legend: { show: false;  }";
        return;
      }

      this.Legend = "legend: { ";
      if ( position != String.Empty )
      {
        switch ( position )
        {
          case "ne":
            {
              this.Legend += "position = \"ne\", ";
              break;
            }
          case "se":
            {
              this.Legend += "position = \"se\", ";
              break;
            }
          case "nw":
            {
              this.Legend += "position = \"nw\", ";
              break;
            }
          case "sw":
            {
              this.Legend += "position = \"sw\", ";
              break;
            }
        }
      }
       this.Legend += " show: true;  }";
    }

    //====================================================================================
    /// <summary>
    /// This method add a value to the plot data list.
    /// Using only the first 2 values of the array of float.
    /// </summary>
    /// <returns>String containing the JSON class output</returns>
    //-----------------------------------------------------------------------------------
    public void SetPie (  )
    {
      this.Pie = " pie: { show: true }";
    }

    //====================================================================================
    /// <summary>
    /// This method add a value to the plot data list.
    /// Using only the first 2 values of the array of float.
    /// </summary>
    /// <returns>String containing the JSON class output</returns>
    //-----------------------------------------------------------------------------------
    public void SetDonut ( )
    {
      this.Pie = " pie: { innerRadius: 0.5, show: true }";
    }

    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion
  }
}//END namespace