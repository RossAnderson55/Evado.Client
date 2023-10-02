/***************************************************************************************
 * <copyright file="Evado.UniForm.Model\PlorSeries.cs" company="EVADO HOLDING PTY. LTD.">
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
 *  This class contains the plot series data object.
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
  public class EuPlot
  {
    #region class initialisation methods

    //  =================================================================================
    /// <summary>
    /// This method initialises the class with parameters and values.
    /// </summary>
    //  ---------------------------------------------------------------------------------
    public EuPlot ( )
    {
    }


    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    /// <summary>
    /// the enumeration defines the legend location.
    /// </summary>
    public enum LegendLocations
    {
      /// <summary>
      /// This value defines the south west location (default)
      /// </summary>
      sw,

      /// <summary>
      /// This value defines the south east location
      /// </summary>
      se,

      /// <summary>
      /// This value defines the north east location
      /// </summary>
      ne,

      /// <summary>
      /// This value defines the north west location
      /// </summary>
      nw
    }

    #region Class private objects and variables

    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region Class private objects and variables

    private List<EuPlotData> _Data = new List<EuPlotData> ( );

    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region Class Properties
    /// <summary>
    /// This property contains a list of x, y coordinates.
    /// </summary>
    public List<EuPlotData> Data
    {
      get
      {
        return this._Data;
      }
      set
      {
        this._Data = value;
      }
    }

    bool _DisplayLegend = false;
    /// <summary>
    /// this property displays the legend
    /// </summary>
    public bool DisplayLegend
    {
      get { return this._DisplayLegend; }
      set { this._DisplayLegend = value; }
    }

    EuPlot.LegendLocations _location = EuPlot.LegendLocations.sw;
    /// <summary>
    /// this property sets the legend location
    /// </summary>
    public LegendLocations LegendLocation
    {
      get { return this._location; }
      set { this._location = value; }
    }

    bool _OverrideXAxisDefinition = false;
    /// <summary>
    /// this property indicated if the x axis configuration is to be overriden.
    /// </summary>
    public bool OverrideXAxisDefinition
    {
      get
      {
        return this._OverrideXAxisDefinition;
      }
      set
      {
        this._OverrideXAxisDefinition = value;

        this.setXAxisProperties ( );
      }
    }

    private System.Text.StringBuilder _X_Axis = new System.Text.StringBuilder ( );
    /// <summary>
    /// this property contains the x Axis properties
    /// </summary>
    public String X_Axis
    {
      get { return this._X_Axis.ToString ( ); }
    }


    private String _X_Color = "000000";
    /// <summary>
    /// this property contains the x color settings.
    /// </summary>
    public String X_Color
    {
      get { return this._X_Color; }
      set
      {
        this._X_Color = value;

        this._X_Font = "{"
        + "size: 10,"
        + "lineHeight: 12,"
        + "style: \"normal\","
        + "weight: \"bold\","
        + "family: \"sans-serif\","
        + "variant: \normal\","
        + "color: \"#" + this._X_Color + "\" " + "}";

        _Y_Font = this._X_Font;
      }
    }
    /*
     "{"
        + "size: 10,"
        + "lineHeight: 12,"
        + "style: \"normal\","
        + "weight: \"bold\","
        + "family: \"sans-serif\","
        + "variant: \normal\","
        + "color: \"#000000\" " + "}"
     */
    private String _X_Font = String.Empty;

    /// <summary>
    /// This property contains the x axis font configuration.
    /// </summary>
    public String X_Font
    {
      get { return _X_Font; }
      set { _X_Font = value; }
    }

    private String [ ] _X_Ticks = new String [ 0 ];
    /// <summary>
    /// this property contains the x ticks settings.
    /// where each value has the format "[ axis value, string value]" 
    /// E.g. "[1,value a]"
    /// </summary>
    public String [ ] X_Ticks
    {
      get { return this._X_Ticks; }
      set { this._X_Ticks = value; }
    }

    bool _OverrideYAxisDefinition = false;
    /// <summary>
    /// this property indicated if the x axis configuration is to be overriden.
    /// </summary>
    public bool OverrideYAxisDefinition
    {
      get { return this._OverrideYAxisDefinition; }
      set
      {
        this._OverrideYAxisDefinition = value;

        if ( this._OverrideXAxisDefinition == false )
        {
          this.setXAxisProperties ( );
        }
      }
    }
    private String _Y_Axis = String.Empty;
    /// <summary>
    /// this property contains the x Axis properties
    /// </summary>
    public String Y_Axis
    {
      get
      {
        this.setXAxisProperties ( );

        return this._Y_Axis;
      }
    }

    private String _Y_Font = "{"
        + "size: 10,"
        + "lineHeight: 12,"
        + "style: \"normal\","
        + "weight: \"bold\","
        + "family: \"sans-serif\","
        + "variant: \normal\","
        + "color: \"#000000\" "
        + "}";

    /// <summary>
    /// This property contains the y axis font configuration.
    /// </summary>
    public String Y_Font
    {
      get { return _Y_Font; }
      set { _Y_Font = value; }
    }

    private String [ ] _Y_Ticks = new String [ 0 ];
    /// <summary>
    /// this property contains the y ticks settings.
    /// </summary>
    public String [ ] Y_Ticks
    {
      get { return this._Y_Ticks; }
      set { this._Y_Ticks = value; }
    }



    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region Class methods

    //====================================================================================
    /// <summary>
    /// This method generates the X axis properties.
    /// </summary>
    //------------------------------------------------------------------------------------
    private void setXAxisProperties ( )
    {
      //this._X_Axis = new System.Text.StringBuilder ( );
      this._X_Axis.AppendLine ( " xaxes: [ {" );
      this._X_Axis.AppendLine ( "show: true, " );
      this._X_Axis.AppendLine ( "position: \"bottom\", " );
      this._X_Axis.AppendLine ( "color: " + this._X_Color + ", " );

      if ( this._X_Font != String.Empty )
      {
        this._X_Axis.Append ( "font: " + this._X_Font );
      }

      if ( this._X_Ticks != null
        && this._X_Ticks.Length > 0 )
      {
        this._X_Axis.Append ( "ticks: [ " );
        for ( int i = 0; i < this._X_Ticks.Length; i++ )
        {
          if ( i > 0 )
          {
            this._X_Axis.Append ( ", " );
          }
          this._X_Axis.Append ( this._X_Ticks );
        }
        this._X_Axis.Append ( "] , " );
      }//END ticks exist.

      this._X_Axis.AppendLine ( "} ] " );

      /* The Axis configuration components.
        xaxis, yaxis: {
        show: null or true/false
        position: "bottom" or "top" or "left" or "right"
        mode: null or "time" ("time" requires jquery.flot.time.js plugin)
         timezone: null, "browser" or timezone (only makes sense for mode: "time")
        color: null or color spec
        tickColor: null or color spec
        font: null or font spec object
        min: null or number
        max: null or number
        autoscaleMargin: null or number
        transform: null or fn: number -> number
        inverseTransform: null or fn: number -> number
        ticks: null or number or ticks array or (fn: axis -> ticks array)
        tickSize: number or array
        minTickSize: number or array
        tickFormatter: (fn: number, object -> string) or string
        tickDecimals: null or number
        labelWidth: null or number
        labelHeight: null or number
        reserveSpace: null or true
        tickLength: null or number
        alignTicksWithAxis: null or number
       }
       */
    }

    //====================================================================================
    /// <summary>
    /// This method sets the X axis font configuration.
    /// </summary>
    /// <param name="Size">int font size</param>
    /// <param name="LineHeight">int lineheight in pixels</param>
    /// <param name="FontFamily">String describing the font families to be used</param>
    /// <param name="SmallCaps">Bool: True display using Small Caps</param>
    /// <param name="Color">String: containing the hexadecimal string defining font color.</param>
    //------------------------------------------------------------------------------------
    public void Set_X_Font ( int Size, int LineHeight, String FontFamily, bool SmallCaps, String Color )
    {
      int size = 10;
      int lineHeight = 12;
      string fontFamily = "sans-serif";
      string variant = "normal";
      String color = "000000";

      this._X_Font = "{"
        + "size: 10,"
        + "lineHeight: 12,"
        + "style: \"normal\","
        + "weight: \"bold\","
        + "family: \"sans-serif\","
        + "variant: \normal\","
        + "color: \"#000000\" "
        + "}";
      if ( Size > 0 )
      {
        size = Size;
      }

      if ( LineHeight > 0 )
      {
        lineHeight = LineHeight;
      }

      if ( FontFamily != String.Empty )
      {
        fontFamily = FontFamily;
      }

      if ( SmallCaps == true )
      {
        variant = "small-caps";
      }
      if ( Color != String.Empty )
      {
        color = Color;
      }

      this._X_Font = "{"
        + "size: " + size + ","
        + "lineHeight: " + lineHeight + ","
        + "style: \"normal\","
        + "weight: \"bold\","
        + "family: \"" + fontFamily + "\","
        + "variant: \"" + variant + "\","
        + "color: \"#" + color + "\" "
        + "}";
    }

    //====================================================================================
    /// <summary>
    /// This method sets the Y axis font configuration.
    /// </summary>
    /// <param name="Size">int font size</param>
    /// <param name="LineHeight">int lineheight in pixels</param>
    /// <param name="FontFamily">String describing the font families to be used</param>
    /// <param name="SmallCaps">Bool: True display using Small Caps</param>
    /// <param name="Color">String: containing the hexadecimal string defining font color.</param>
    //------------------------------------------------------------------------------------
    public void Set_Y_Font ( int Size, int LineHeight, String FontFamily, bool SmallCaps, String Color )
    {
      int size = 10;
      int lineHeight = 12;
      string fontFamily = "sans-serif";
      string variant = "normal";
      String color = "000000";

      this._X_Font = "{"
        + "size: 10,"
        + "lineHeight: 12,"
        + "style: \"normal\","
        + "weight: \"bold\","
        + "family: \"sans-serif\","
        + "variant: \normal\","
        + "color: \"#000000\" "
        + "}";
      if ( Size > 0 )
      {
        size = Size;
      }

      if ( LineHeight > 0 )
      {
        lineHeight = LineHeight;
      }

      if ( FontFamily != String.Empty )
      {
        fontFamily = FontFamily;
      }

      if ( SmallCaps == true )
      {
        variant = "small-caps";
      }
      if ( Color != String.Empty )
      {
        color = Color;
      }

      this._Y_Font = "{"
        + "size: " + size + ","
        + "lineHeight: " + lineHeight + ","
        + "style: \"normal\","
        + "weight: \"bold\","
        + "family: \"" + fontFamily + "\","
        + "variant: \"" + variant + "\","
        + "color: \"#" + color + "\" "
        + "}";
    }

    //====================================================================================
    /// <summary>
    /// This method add a value to the plot data list.
    /// Using only the first 2 values of the array of float.
    /// </summary>
    /// <param name="Data">PlotData object</param>
    //-----------------------------------------------------------------------------------
    public void AddData ( EuPlotData Data )
    {
      this._Data.Add ( Data );

    }
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
    public String GetData ( )
    {
      System.Text.StringBuilder output = new System.Text.StringBuilder ( );

      if ( this._Data.Count == 0 )
      {
        return String.Empty;
      }

      if ( this._Data.Count == 1 )
      {
        output.Append ( "[ " );
        output.Append ( this._Data [ 0 ].GetData ( ) );
        output.AppendLine ( "] " );
      }
      else
      {
        //
        // get each array of data 
        //
        output.Append ( "[ " );
        for ( int i = 0; i < this._Data.Count; i++ )
        {
          if ( i > 0 )
          {
            output.AppendLine ( "," );
          }
          EuPlotData data = this._Data [ i ];
          output.Append ( data.GetData ( ) );
        }
        output.AppendLine ( "] " );
      }

      return output.ToString ( );
    }


    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion
  }
}//END namespace