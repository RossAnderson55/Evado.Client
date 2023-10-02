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
  public class EuPlotData
  {
    #region class initialisation methods

    //  =================================================================================
    /// <summary>
    /// This method initialises the class with parameters and values.
    /// </summary>
    //  ---------------------------------------------------------------------------------
    public EuPlotData ( )
    {
    }

    //  =================================================================================
    /// <summary>
    /// This method initialiseas the class with parameters.
    /// </summary>
    /// <param name="Values">List of Float Array</param>
    /// <param name="Label">String: the series label</param>
    //  ---------------------------------------------------------------------------------
    public EuPlotData ( List<float [ ]> Values, String Label )
    {
      this.Label = Label;
      this.Type = PlotType.Lines;

      if ( Values.Count == 0 )
      {
        return;
      }

      //
      // load the data list array for correctly formatted values.
      //
      foreach ( float [ ] value in Values )
      {
        if ( value.Length < 2 )
        {
          continue;
        }
        float [ ] value1 = new float [ 2 ];

        value1 [ 0 ] = value [ 0 ];
        value1 [ 1 ] = value [ 1 ];

        this._Values.Add ( value1 );
      }//END data iteration loop.

    }

    //  =================================================================================
    /// <summary>
    /// This method initialiseas the class with parameters.
    /// </summary>
    /// <param name="Values">List of Float Array</param>
    /// <param name="Label">String: the series label</param>
    /// <param name="Color">String: the color </param>
    //  ---------------------------------------------------------------------------------
    public EuPlotData ( List<float [ ]> Values, String Label, String Color )
    {
      this.Label = Label;
      this.Color = Color;
      this.Type = PlotType.Lines;

      if ( Values.Count == 0 )
      {
        return;
      }

      //
      // load the data list array for correctly formatted values.
      //
      foreach ( float [ ] value in Values )
      {
        if ( value.Length < 2 )
        {
          continue;
        }
        float [ ] value1 = new float [ 2 ];

        value1 [ 0 ] = value [ 0 ];
        value1 [ 1 ] = value [ 1 ];

        this._Values.Add ( value1 );
      }//END data iteration loop.

    }

    //  =================================================================================
    /// <summary>
    /// This method initialiseas the class with parameters.
    /// </summary>
    /// <param name="Values">List of Float Array</param>
    /// <param name="Label">String: the series label</param>
    /// <param name="Color">String: the color </param>
    /// <param name="Type">PlotType: the color </param>
    //  ---------------------------------------------------------------------------------
    public EuPlotData ( List<float [ ]> Values, String Label, String Color, PlotType Type )
    {
      this.Label = Label;
      this.Color = Color;
      this.Type = Type;

      if ( Values.Count == 0 )
      {
        return;
      }

      //
      // load the data list array for correctly formatted values.
      //
      foreach ( float [ ] value in Values )
      {
        if ( value.Length < 2 )
        {
          continue;
        }
        float [ ] value1 = new float [ 2 ];

        value1 [ 0 ] = value [ 0 ];
        value1 [ 1 ] = value [ 1 ];

        this._Values.Add ( value1 );
      }//END data iteration loop.

    }

    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region Class public enumerated lists
    /// <summary>
    /// This enumeration defines the type of plot for the chart series
    /// </summary>
    public enum PlotType
    {
      /// <summary>
      /// this enuerate indicates that the plot type is not defined.
      /// </summary>
      Null,

      /// <summary>
      /// this enumeration defines a line chart.
      /// </summary>
      Lines,

      /// <summary>
      /// this enumeration defines a points on a chart
      /// </summary>
      Points,

      /// <summary>
      /// this enumeration defeines the bars on a chart
      /// </summary>
      Bars,

      /// <summary>
      /// this enumeration defines a line with points chart.
      /// </summary>
      Lines_Points,

      /// <summary>
      /// this enumeration defines a line with fill chart.
      /// </summary>
      Lines_Fill,

      /// <summary>
      /// this enumeration defines a line with steps chart.
      /// </summary>
      Lines_Steps,

      /// <summary>
      /// this enumeration defines a line with steps chart.
      /// </summary>
      Lines_Fill_Steps,

    }

    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region Class private objects and variables

    private List<float [ ]> _Values = new List<float [ ]> ( );

    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region Class Properties
    /// <summary>
    /// This property contains a list of x, y coordinates.
    /// </summary>
    public List<float [ ]> Values
    {
      get
      {
        return this._Values;
      }
      set
      {
        //
        // re-initialise the data list array.
        this._Values = new List<float [ ]> ( );

        //
        // if the input data is empty exit property method.
        //
        if ( value.Count == 0 )
        {
          return;
        }

        //
        // load the data list array for correctly formatted values.
        //
        foreach ( float [ ] value1 in value )
        {
          if ( value1.Length < 2 )
          {
            continue;
          }
          float [ ] value2 = new float [ 2 ];

          value2 [ 0 ] = value1 [ 0 ];
          value2 [ 1 ] = value1 [ 1 ];

          this._Values.Add ( value2 );
        }//END data iteration loop. 
      }
    }

    /// <summary>
    /// This property defines the series label value.
    /// </summary>
    public String Label { get; set; }

    /// <summary>
    /// This property defines the series color value.
    /// </summary>
    public String Color { get; set; }


    private PlotType _Type = PlotType.Null;
    /// <summary>
    /// this property defines the plot type
    /// </summary>
    public PlotType Type
    {
      get { return _Type; }
      set
      {
        this._Type = value;
      }
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
    public String  GetData ( )
    {
      System.Text.StringBuilder output = new System.Text.StringBuilder ( );
      if ( this._Values.Count == 0 )
      {
        return String.Empty;
      }

      int pointcount = this._Values [ 0 ].Length;

      double [ , ] data = new double [ this._Values.Count, pointcount ];

      for ( int i = 0; i < this._Values.Count; i++ )
      {
        if ( pointcount == 1 )
        {
          data [ i, 0 ] = Values [ i ] [ 0 ];
        }
        else
        {
          data [ i, 0 ] = Values [ i ] [ 0 ];
          data [ i, 1 ] = Values [ i ] [ 1 ];
        }
      }

      output.Append ( "{" );
      string stData = "data: " + Newtonsoft.Json.JsonConvert.SerializeObject ( data, Newtonsoft.Json.Formatting.None );
      output.Append  ( stData );

      if ( this.Label != null )
      {
        output.Append ( ", label: \"" + this.Label + "\"" );
      }

      switch ( this._Type )
      {
        case PlotType.Lines:
          {
            output.Append ( ", lines: { show: true }" );
            break;
          }
        case PlotType.Lines_Points:
          {
            output.Append ( ", lines: { show: true }" );
            output.Append ( ", point: { show: true }" );
            break;
          }
        case PlotType.Lines_Fill:
          {
            output.Append ( ", lines: { show: true, fill: true  }" );
            break;
          }
        case PlotType.Lines_Steps:
          {
            output.Append ( ", lines: { show: true, steps: true  }" );
            break;
          }
        case PlotType.Lines_Fill_Steps:
          {
            output.Append ( ", lines: { show: true, fill: true, steps: true  }" );
            break;
          }
        case PlotType.Bars:
          {
            output.Append ( ", bars: { show: true }" );
            break;
          }
        case PlotType.Points:
          {
            output.Append ( ", point: { show: true }" );
            break;
          }
      }//END switch.

      if ( this.Color != null )
      {
        output.Append ( ", color: " + this.Color );
      }
      output.Append ( "}" );

      return output.ToString ( );
    }

    //====================================================================================
    /// <summary>
    /// This method add a value to the plot data list.
    /// Using only the first 2 values of the array of float.
    /// </summary>
    /// <param name="X">float value</param>
    /// <param name="Y">float value</param>
    //-----------------------------------------------------------------------------------
    public void AddValue ( float X, float Y )
    {
      float [ ] value2 = new float [ 2 ];

      value2 [ 0 ] = X;
      value2 [ 1 ] = Y;

      this._Values.Add ( value2 );

    }

    //====================================================================================
    /// <summary>
    /// This method add a value to the plot data list.
    /// Using only the first 2 values of the array of float.
    /// </summary>
    /// <param name="X">double value</param>
    /// <param name="Y">double value</param>
    //-----------------------------------------------------------------------------------
    public void AddValue ( double X, double Y )
    {
      float [ ] value2 = new float [ 2 ];

      value2 [ 0 ] = Convert.ToSingle ( X );
      value2 [ 1 ] = Convert.ToSingle ( Y );

      this._Values.Add ( value2 );

    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion
  }
}//END namespace