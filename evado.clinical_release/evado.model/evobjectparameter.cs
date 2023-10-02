/***************************************************************************************
 * <copyright file="EvSiteProfile.cs" company="EVADO HOLDING PTY. LTD.">
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
 *  This class contains the EvSiteProfile data object.
 *
 ****************************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;

namespace Evado.Model
{

  /// <summary>
  /// Business entity used to model SiteProperties
  /// </summary>
  [Serializable]
  public class EvObjectParameter
  {
    /// <summary>
    /// This method initialises the object.
    /// </summary>
    public EvObjectParameter ( )
    { }

    //===================================================================================
    /// <summary>
    /// THis initialises the object setting the order name and value.
    /// </summary>
    /// <param name="Name">String parameter name</param>
    /// <param name="Value">String parameter value</param>
    //-----------------------------------------------------------------------------------
    public EvObjectParameter ( String Name, String Value )
    {
      this.Order = 0;
      this.Name = Name;
      this.DataType = Evado.Model.EvDataTypes.Text;
      this.Value = Value;
      this.Options = String.Empty;
    }

    //===================================================================================
    /// <summary>
    /// THis initialises the object setting the name and value.
    /// </summary>
    /// <param name="Name">String parameter name</param>
    /// <param name="Value">String parameter value</param>
    //-----------------------------------------------------------------------------------
    public EvObjectParameter ( object Name, object Value )
    {
      this.Order = 0;
      this.Name = Name.ToString ( );
      this.DataType = Evado.Model.EvDataTypes.Text;
      this.Value = Value.ToString ( );
      this.Options = String.Empty;
    }


    //===================================================================================
    /// <summary>
    /// THis initialises the object setting the name, value and options.
    /// </summary>
    /// <param name="Name">String parameter name</param>
    /// <param name="DataType">EvDataTypes enumerated value</param>
    /// <param name="Value">String parameter value</param>
    //-----------------------------------------------------------------------------------
    public EvObjectParameter ( object Name, EvDataTypes DataType, object Value )
    {
      this.Order = 0;
      this.Name = Name.ToString ( );
      this.DataType = DataType;
      this.Value = Value.ToString();
      this.Options = String.Empty;
    }

    //===================================================================================
    /// <summary>
    /// THis initialises the object setting the name, value and options.
    /// </summary>
    /// <param name="Name">String: the name of the parameter</param>
    /// <param name="Value">String: the value of the paramter</param>
    /// <param name="Options">String: option values for the parameter.</param>
    //-----------------------------------------------------------------------------------
    public EvObjectParameter ( object Name, String Value, String Options )
    {
      this.Order = 0;
      this.Name = Name.ToString ( );
      this.DataType = Evado.Model.EvDataTypes.Text;
      this.Value = Value.ToString ( );
      this.Options = Options;
    }

    /// <summary>
    /// This property contains a global unique identifier of site profile
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// This property contains parameter order
    /// </summary>
    public int Order { get; set; }

    /// <summary>
    /// This property contains parameter name
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// This property contains the parameter value
    /// </summary>
    public EvDataTypes DataType { get; set; }

    /// <summary>
    /// This property contains the parameter value
    /// </summary>
    public string Value { get; set; }

    /// <summary>
    /// This property contains string encoded list of parmeter options 
    /// </summary>
    public string Options { get; set; }

    /// <summary>
    /// This property returns a list of options objects
    /// </summary>
    public List<Evado.Model.EvOption> OptionList
    {
      get
      {
        List<Evado.Model.EvOption> optionList = new List<Evado.Model.EvOption> ( );
        if ( Options == null )
        {
          return optionList;
        }
        String [ ] options = Options.Split ( ';' );

        for ( int i = 0; i < options.Length; i++ )
        {
          string stOption = options [ i ];
          if ( stOption.Contains ( ":" ) == true )
          {
            string [ ] values = stOption.Split ( ':' );

            optionList.Add ( new Evado.Model.EvOption ( values [ 0 ].Trim ( ), values [ 1 ].Trim ( ) ) );
          }
          else
          {
            optionList.Add ( new Evado.Model.EvOption ( stOption.Trim ( ) ) );
          }
        }

        return optionList;
      }
    }


  }//END SiteProperties class

}//END namespace Evado.Clinical.Model
