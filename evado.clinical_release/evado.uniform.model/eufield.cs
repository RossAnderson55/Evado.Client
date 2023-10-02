/***************************************************************************************
 * <copyright file="Evado.UniForm.Model\Field.cs" company="EVADO HOLDING PTY. LTD.">
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
 *
 ****************************************************************************************/
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Evado.UniForm.Model
{
  /// <summary>
  /// This class contains defines the client page field object contents.
  /// </summary>
  [Serializable]
  public partial class EuField
  {
    #region Class initialisation methods

    //  =================================================================================
    /// <summary>
    /// This method initialises the class with values.
    /// </summary>
    //  ---------------------------------------------------------------------------------
    public EuField ( )
    {
    }

    //  =================================================================================
    /// <summary>
    /// This method initialises the class with values.
    /// </summary>
    /// <param name="DataId">String: A field data identifier</param>
    /// <param name="Title">String: A field title.</param>
    /// <param name="DataType">EvDataTypes: A field data type object</param>
    //  ---------------------------------------------------------------------------------
    public EuField ( String DataId, String Title, Evado.Model.EvDataTypes DataType )
    {
      this.Id = Guid.NewGuid ( );
      this.FieldId = DataId;
      this.Title = Title;
      this.Type = DataType;

      if ( DataType == Evado.Model.EvDataTypes.Table
        || DataType == Evado.Model.EvDataTypes.Special_Matrix )
      {
        this.Table = new Evado.Model.EvTable ( );
      }
    }

    //  =================================================================================
    /// <summary>
    /// This method initialises the class with values.
    /// </summary>
    /// <param name="DataId">String: A field data identifier</param>
    /// <param name="Title">String: A field title.</param>
    /// <param name="DataType">EvDataTypes: A field data type object</param>
    /// <param name="Value">String: A data value</param>
    //  ---------------------------------------------------------------------------------
    public EuField ( String DataId, String Title, Evado.Model.EvDataTypes DataType, String Value )
    {
      this.Id = Guid.NewGuid ( );
      this.FieldId = DataId;
      this.Title = Title;
      this.Type = DataType;
      this.Value = Value;
    }


    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region Class Enumerations

    /// <summary>
    /// This enumeration contains the field target enumeration settings.
    /// </summary>
    public enum FieldTarget
    {
      /// <summary>
      /// This enumeration defines that the html link is to be opened within
      /// the UniFORM client.
      /// </summary>
      Internal = 0,

      /// <summary>
      /// This enumeration defines that the html link is to be opened outside
      /// the UniFORM client.
      /// </summary>
      External = 2
    }

    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region class constants
    /// <summary>
    /// This constant defines the field queyr suffix
    /// </summary>
    public const string CONST_FIELD_QUERY_SUFFIX = "_Query";
    /// <summary>
    /// This constant defines the Field annotation suffice
    /// </summary>
    public const string CONST_FIELD_ANNOTATION_SUFFIX = "_FAnnotation";

    public const string CONST_NAME_FORMAT_PREFIX = "PRE;";
    public const string CONST_NAME_FORMAT_GIVEN_NAME = "GN;";
    public const string CONST_NAME_FORMAT_MIDDLE_NAME = "MN;";
    public const string CONST_NAME_FORMAT_FAMILY_NAME = "FN;";

    public const String CONST_IMAGE_FIELD_SUFFIX = "_FILE";
    public const String CONST_IMAGE_TITLE_SUFFIX = "_TITLE";


    public const String CONST_HTTP_URL_FIELD_SUFFIX = "_URL";
    public const String CONST_HTTP_TITLE_FIELD_SUFFIX = "_TITLE";

    public const String COMPUTED_FUNCTION_SUM_FIELDS = "=SUMFIELDS";
    public const String COMPUTED_FUNCTION_SUM_CATEGORY = "=SUMCATEGORY";
    public const String COMPUTED_FUNCTION_SUM_COLUMN = "=SUMCOLUMN";
    #endregion 

    #region Class properties.

    /// <summary>
    ///  This property contains an identifier for the Field object.
    /// 
    /// </summary>
    [JsonProperty ( "id" )]
    public Guid Id { get; set; } = Guid.Empty;

    /// <summary>
    ///  This property contains the identifier to cross-reference device data
    ///  with the page object.
    /// </summary>
    [JsonProperty ( "fid" )]
    public String FieldId { get; set; } = String.Empty;

    /// <summary>
    ///  This property contains the page field objects title.
    /// </summary>
    public String Title { get; set; } = String.Empty;

    /// <summary>
    /// This property contains the group's description stored as a parameter.
    /// </summary>
    [JsonProperty ( "d" )]
    public String Description { get; set; }

    /// <summary>
    /// This property contains field jutification property setting.
    /// </summary>
    [JsonProperty ( "ly" )]
    public EuFieldLayoutCodes Layout { get; set; } = EuFieldLayoutCodes.Default;

    /// <summary>
    /// This property contains page objects data type
    /// </summary>
    [JsonProperty ( "t" )]
    public Evado.Model.EvDataTypes Type { get; set; } = Evado.Model.EvDataTypes.Null;

    /// <summary>
    /// This property defines whether the field is mandatory or not.
    /// True: the field is mandatory.
    /// </summary>
    [JsonProperty ( "mad" )]
    public bool Mandatory { get; set; }

    /// <summary>
    /// This property defines whether the field is mandatory or not.
    /// True: the field is mandatory.
    /// </summary>
    [JsonIgnore]
    public bool IsEnabled
    {
      get
      {
        if ( this.EditAccess == EuEditAccess.Disabled )
        {
          return false;
        }

        switch ( this.Type )
        {
          case  Evado.Model.EvDataTypes.Computed_Field:
          case  Evado.Model.EvDataTypes.External_Image:
          case  Evado.Model.EvDataTypes.Html_Content:
          case  Evado.Model.EvDataTypes.Http_Link:
          case  Evado.Model.EvDataTypes.Line_Chart:
          case  Evado.Model.EvDataTypes.Pie_Chart:
          case  Evado.Model.EvDataTypes.Stacked_Bar_Chart:
          case  Evado.Model.EvDataTypes.Read_Only_Text:
          case  Evado.Model.EvDataTypes.Donut_Chart:
          case  Evado.Model.EvDataTypes.Sound:
          case  Evado.Model.EvDataTypes.Streamed_Video:
          case  Evado.Model.EvDataTypes.Video:
            {
              return false;
            }
        }//END switch statment

        return true;

      }
    }

    /// <summary>
    /// This property indicates if the field is empty.
    /// </summary>
    [JsonIgnore]
    public bool isReadOnly
    {
      get
      {
        //
        // select the data type for testing.
        //
        switch ( this.Type )
        {
          case  Evado.Model.EvDataTypes.External_Image:
          case  Evado.Model.EvDataTypes.Html_Content:
          case  Evado.Model.EvDataTypes.Http_Link:
          case  Evado.Model.EvDataTypes.Line_Chart:
          case  Evado.Model.EvDataTypes.Pie_Chart:
          case  Evado.Model.EvDataTypes.Stacked_Bar_Chart:
          case  Evado.Model.EvDataTypes.Read_Only_Text:
          case  Evado.Model.EvDataTypes.Donut_Chart:
          case  Evado.Model.EvDataTypes.Sound:
          case  Evado.Model.EvDataTypes.Streamed_Video:
          case  Evado.Model.EvDataTypes.Video:
            {
              return true;
            }
        }//ENd Switch statement

        return false;
      }
    }

    /// <summary>
    /// This member defines the method parameter that will be used to call  the hosted application.
    /// </summary>
    [JsonProperty ( "prm" )]
    public List<EuParameter> Parameters { get; set; } = new List<EuParameter> ( );

    private String _Value = String.Empty;
    /// <summary>
    /// This property contains the text value for the date data type. 
    /// </summary>
    [JsonProperty ( "v" )]
    public String Value { get; set; } = String.Empty;

    /// <summary>
    /// This properaty defines a table fields structure and contents
    /// </summary>
    [JsonProperty ( "tbl" )]
    public Evado.Model.EvTable Table { get; set; }

    /// <summary>
    /// This property defines whether a field is editable by the user
    /// when displayed in the device client.
    /// </summary>
    [JsonProperty ( "ae" )]
    public EuEditAccess EditAccess { get; set; } = EuEditAccess.Inherited;

    /// <summary>
    /// This property defines a selection list that is displayed on the device client.
    /// </summary>
    [JsonProperty ( "opt" )]
    public List<Evado.Model.EvOption> OptionList { get; set; }

    /// <summary>
    /// This property indicates if the field is empty.
    /// </summary>
    [JsonIgnore]
    public bool isEmpty
    {
      get
      {
        bool isEmpty = true;

        //
        // select the data type for testing.
        //
        switch ( this.Type )
        {
          case  Evado.Model.EvDataTypes.Table:
          case  Evado.Model.EvDataTypes.Special_Matrix:
            {
              if ( Table != null )
              {
                //
                // itereate through table cells looking for non readonly cells with values.
                //
                foreach ( Evado.Model.EvTableRow row in this.Table.Rows )
                {
                  for ( int i = 0; i < row.Column.Length && i < Table.Header.Length; i++ )
                  {
                    if ( row.Column [ i ] != String.Empty
                      && Table.Header [ i ].DataType !=  Evado.Model.EvDataTypes.Read_Only_Text )
                    {
                      isEmpty = false;
                    }
                  }
                }
              }
              break;
            }
          case Evado.Model.EvDataTypes.Radio_Button_List:
            {
              if ( this.Value != "Null"
                && this.Value != String.Empty )
              {
                isEmpty = false;
              }
              break;
            }
          default:
            {
              if ( this.Value != string.Empty )
              {
                isEmpty = false;
              }
              break;
            }

        }
        return isEmpty;
      }
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region Class Methods

    // ==================================================================================
    /// <summary>
    /// This method adds a parameter to the EuCommand's parameter list..
    /// </summary>
    /// <param name="Name">String: The name of the parameter.</param>
    /// <param name="Value">String: The value of the parameter.</param>
    //  ---------------------------------------------------------------------------------
    public void AddParameter ( 
      String Name, 
      String Value )
    {
      //
      // get the string value of the parameter list.
      //
      String name = Name.ToString ( );
      name = name.Trim ( );
      String value = Value.ToString ( );

      foreach ( EuParameter parameter in this.Parameters )
      {
        if ( parameter.Name == name )
        {
          parameter.Value = value;

          return;
        }
      }

      this.Parameters.Add ( new EuParameter ( name, value ) );

    }//END AddParameter method

    // ==================================================================================
    /// <summary>
    /// This method adds a parameter to the EuCommand's parameter list..
    /// </summary>
    /// <param name="Name">String: The name of the parameter.</param>
    /// <param name="Value">String: The value of the parameter.</param>
    //  ---------------------------------------------------------------------------------
    public void AddParameter ( 
      EuFieldParameters Name, 
      object Value )
    {
      //
      // get the string value of the parameter list.
      //
      String name = Name.ToString ( );
      name = name.Trim ( );
      String value = Value.ToString ( );

      foreach ( EuParameter parameter in this.Parameters )
      {
        if ( parameter.Name == name )
        {
          parameter.Value = value;

          return;
        }
      }

      this.Parameters.Add ( new EuParameter ( name, value ) );

    }//END AddParameter method


    // ==================================================================================
    /// <summary>
    /// This method test whether the parameter exists in the field.
    /// </summary>
    /// <param name="Name">String: The name of the parameter.</param>
    //  ---------------------------------------------------------------------------------
    public bool hasParameter ( 
      EuFieldParameters Name )
    {
      //
      // get the string value of the parameter list.
      //
      String name = Name.ToString ( );
      name = name.Trim ( );

      //
      // Iterate through the parameters to get the selectev value.
      //
      foreach ( EuParameter parameter in this.Parameters )
      {
        if ( parameter.Name == name )
        {
          return true;
        }
      }

      //
      // Return result
      //
      return false;

    }//END AddParameter method

    // ==================================================================================
    /// <summary>
    /// This method test to see if the parameter is in the list.
    /// </summary>
    /// <param name="Name">String: the name of the parameter.</param>
    /// <returns>True: parameter exists</returns>
    // ---------------------------------------------------------------------------------
    public bool hasParameter ( 
      String Name )
    {
      //
      // get the string value of the parameter list.
      //
      String name = Name.ToString ( );
      name = name.Trim ( );

      //
      // Iterate through the parameters to get the selectev value.
      //
      foreach ( EuParameter parameter in this.Parameters )
      {
        if ( parameter.Name == name )
        {
          return true;
        }
      }

      //
      // Return result
      //
      return false;

    }//END hasParameter method

    // ==================================================================================
    /// <summary>
    /// This method adds a parameter to the EuCommand's parameter list..
    /// </summary>
    /// <param name="Name">String: The name of the parameter.</param>
    //  ---------------------------------------------------------------------------------
    public void DeleteParameter ( 
      EuFieldParameters Name )
    {
      //
      // get the string value of the parameter list.
      //
      String name = Name.ToString ( );
      name = name.Trim ( );

      for ( int count = 0; count < this.Parameters.Count; count++ )
      {
        EuParameter parameter = this.Parameters [ count ];

        if ( parameter.Name == name )
        {
          this.Parameters.RemoveAt ( count );
          count--;
        }
      }
    }

    // ==================================================================================
    /// <summary>
    /// This method adsd a parameter to the EuCommand's parameter list..
    /// </summary>
    /// <param name="Name">String: The name of the parameter.</param>
    /// <returns> String value of the header element</returns>
    //  ---------------------------------------------------------------------------------
    public String GetParameter ( 
      EuFieldParameters Name )
    {
      //
      // get the string value of the parameter list.
      //
      String name = Name.ToString ( );
      name = name.Trim ( );


      //
      // Iterate through the parameters to get the selectev value.
      //
      foreach ( EuParameter parameter in this.Parameters )
      {
        if ( parameter.Name == name )
        {
          return parameter.Value.Trim();
        }
      }

      return string.Empty;

    }//END AddParameter method

    // ==================================================================================
    /// <summary>
    /// This method adsd a parameter to the EuCommand's parameter list..
    /// </summary>
    /// <param name="Name">String: The name of the parameter.</param>
    /// <returns> String value of the header element</returns>
    //  ---------------------------------------------------------------------------------
    public String GetParameter ( 
      String Name )
    {
      //
      // get the string value of the parameter list.
      //
      String name = Name.ToString ( );
      name = name.Trim ( );

      //
      // Iterate through the parameters to get the selectev value.
      //
      foreach ( EuParameter parameter in this.Parameters )
      {
        if ( parameter.Name == name )
        {
          return parameter.Value;
        }
      }

      return string.Empty;

    }//END AddParameter method

    // ==================================================================================
    /// <summary>
    /// This method adsd a parameter to the EuCommand's parameter list..
    /// </summary>
    /// <param name="Name">String: The name of the parameter.</param>
    /// <returns> String value of the header element</returns>
    //  ---------------------------------------------------------------------------------
    public int GetParameterInt ( 
      EuFieldParameters Name )
    {
      //
      // get the string value of the parameter list.
      //
      string value = this.GetParameter ( Name );

      return Evado.Model.EvStatics.getInteger( value );

    }//END AddParameter method

    // ==================================================================================
    /// <summary>
    /// This method adsd a parameter to the EuCommand's parameter list..
    /// </summary>
    /// <param name="Name">String: The name of the parameter.</param>
    /// <returns> String value of the header element</returns>
    //  ---------------------------------------------------------------------------------
    public float GetParameterflt (
      EuFieldParameters Name )
    {
      //
      // get the string value of the parameter list.
      //
      string value = this.GetParameter ( Name );

      return Evado.Model.EvStatics.getFloat ( value );

    }//END AddParameter method

    // ==================================================================================
    /// <summary>
    /// This method adsd a parameter to the EuCommand's parameter list..
    /// </summary>
    /// <param name="Name">String: The name of the parameter.</param>
    /// <returns> String value of the header element</returns>
    //  ---------------------------------------------------------------------------------
    public DateTime GetParameterDate (
      EuFieldParameters Name )
    {
      //
      // get the string value of the parameter list.
      //
      string value = this.GetParameter ( Name );

      return Evado.Model.EvStatics.getDateTime ( value );

    }//END AddParameter method

    // ==================================================================================
    /// <summary>
    /// This method sets the Send EuCommand on change parameter
    /// </summary>
    // ---------------------------------------------------------------------------------
    public void setSendCommandOnChange ( )
    {
      this.AddParameter ( EuFieldParameters.Snd_Cmd_On_Change, "true" );
    }

    // ==================================================================================
    /// <summary>
    /// This method gets  the Send EuCommand on change parameter
    /// </summary>
    /// <returns>True: EuCommand is set. </returns>
    // ---------------------------------------------------------------------------------
    public bool getSendCommandOnChange ( )
    {
      String value = this.GetParameter ( EuFieldParameters.Snd_Cmd_On_Change );

      return Evado.Model.EvStatics.getBool ( value );
    }

    // ==================================================================================
    /// <summary>
    /// This method sets the background colour value
    /// </summary>
    /// <param name="Name">EuFieldParameters: The name of the parameter.</param>
    /// <param name="Value">EuBackgroundColours: the selected colour's enumerated value.</param>
    //  ---------------------------------------------------------------------------------
    public void setBackgroundColor ( 
      EuFieldParameters Name, 
      EuBackgroundColours Value )
    {
      //
      // Exit if the parameter is not a background colour enumeration.
      //
      if ( Name != EuFieldParameters.BG_Default
        && Name != EuFieldParameters.BG_Mandatory
        && Name != EuFieldParameters.BG_Validation
        && Name != EuFieldParameters.BG_Alert
        && Name != EuFieldParameters.BG_Normal )
      {
        return;
      }
      //
      // get the string value of the parameter list.
      //
      String value = Value.ToString ( );

      this.AddParameter ( Name, value );


    }//END AddParameter method

    // ==================================================================================
    /// <summary>
    /// This method add a parameter to for the background colour
    /// </summary>
    /// <param name="Value">EuBackgroundColours: the selected colour's enumerated value.</param>
    //  ---------------------------------------------------------------------------------
    public void setDefaultBackBroundColor ( 
      EuBackgroundColours Value )
    {
      //
      // Exit if the parameter is not a background colour enumeration.
      //
      String name = EuFieldParameters.BG_Default.ToString();

      //
      // get the string value of the parameter list.
      //
      String value = Value.ToString ( );

      this.AddParameter ( name, value );


    }//END AddParameter method

    // ==================================================================================
    /// <summary>
    /// This method add a parameter to for the background colour
    /// </summary>
    /// <param name="Value">EuBackgroundColours: the selected colour's enumerated value.</param>
    //  ---------------------------------------------------------------------------------
    public void setMandatoryBackgroundColor ( 
      EuBackgroundColours Value )
    {
      //
      // Exit if the parameter is not a background colour enumeration.
      //
      String name = EuFieldParameters.BG_Mandatory.ToString ( );

      //
      // get the string value of the parameter list.
      //
      String value = Value.ToString ( );

      this.AddParameter ( name, value );


    }//END AddParameter method

    // ==================================================================================
    /// <summary>
    /// This method add a parameter to for the background colour
    /// </summary>
    /// <param name="Name">EuFieldParameters: The name of the parameter.</param>
    //  ---------------------------------------------------------------------------------
    public EuBackgroundColours GetBackgroundColor ( 
      EuFieldParameters Name )
    {
      //
      // Exit if the parameter is not a background colour enumeration.
      //
      if ( Name != EuFieldParameters.BG_Default
        && Name != EuFieldParameters.BG_Mandatory
        && Name != EuFieldParameters.BG_Validation
        && Name != EuFieldParameters.BG_Alert
        && Name != EuFieldParameters.BG_Normal )
      {
        return EuBackgroundColours.Default;
      }

      //
      // get the string value of the parameter list.
      //
      String value = this.GetParameter( Name );
      EuBackgroundColours colour = EuBackgroundColours.Default;

      //
      //Iterate through the list paramater to determine of the parameter already exists and update it.
      //
      foreach ( EuParameter parameter in this.Parameters )
      {
        //
        //If parameter Name is equal to GroupParameterList Name, return
        //
        if (value != String.Empty )
        {
          colour = Evado.Model.EvStatics.parseEnumValue<EuBackgroundColours> ( parameter.Value );

          return colour;
        }
      }//END parameter iteration

      //
      // return the found colour
      //
      return colour;

    }//END AddParameter method

    // ==================================================================================
    /// <summary>
    /// This method add a parameter to for the background colour
    /// </summary>
    /// <returns>EuBackgroundColours emunerated value</returns>
    //  ---------------------------------------------------------------------------------
    public EuBackgroundColours getDefaultBackgroundColor ( )
    {
      //
      // get the string value of the parameter list.
      //
      String value = this.GetParameter ( EuFieldParameters.BG_Default );
      EuBackgroundColours colour = EuBackgroundColours.Default;

      //
      //if the value exists reset the colour
      //
      if ( value != String.Empty )
      {
        if ( Evado.Model.EvStatics.tryParseEnumValue<EuBackgroundColours> ( value, out colour ) == true )
        {
          return colour;
        }
      }

      //
      // return the found colour
      //
      return EuBackgroundColours.Default;

    }//END AddParameter method

    // ==================================================================================
    /// <summary>
    /// This method add a parameter to for the background colour
    /// </summary>
    /// <returns>EuBackgroundColours emunerated value</returns>
    //  ---------------------------------------------------------------------------------
    public EuBackgroundColours getMandatoryBackGroundColor (EuBackgroundColours CurentColor )
    {
      //
      // get the string value of the parameter list.
      //
      String value = this.GetParameter ( EuFieldParameters.BG_Mandatory );
      EuBackgroundColours colour = EuBackgroundColours.Red;

      //
      // If the field has a value it should have the default background.
      //
      if ( this.Value != String.Empty )
      {
        return CurentColor;
      }

      //
      //if the value exists reset the colour
      //
      if ( value != String.Empty )
      {
        if ( Evado.Model.EvStatics.tryParseEnumValue<EuBackgroundColours> ( value, out colour ) == true )
        {
          return colour;
        }
      }

      //
      // return the found colour
      //
      return EuBackgroundColours.Red;

    }//END AddParameter method

    // ==================================================================================
    /// <summary>
    /// This method add a parameter to the EuCommand's parameter list..
    /// </summary>
    /// <param name="Value">FieldValueWidth: the value of the parameter.</param>
    //  ---------------------------------------------------------------------------------
    public void SetValueColumnWidth (
      EuFieldValueWidths Value )
    {
      //
      // get the string value of the parameter list.
      //
      String value = Value.ToString ( );

      //
      // Add the parameter to the parameter list.
      //
      this.AddParameter ( EuFieldParameters.Field_Value_Column_Width, value );

    }//END addFieldValueWidth method

    // ==================================================================================
    /// <summary>
    /// This method get the value column width if set.
    /// </summary>
    //  ---------------------------------------------------------------------------------
    public EuFieldValueWidths getValueColumnWidth ( )
    {
      //
      // get the string value of the parameter list.
      //
      String value = this.GetParameter ( EuFieldParameters.Field_Value_Column_Width );

      if ( value != String.Empty )
      {
        return Evado.Model.EvStatics.parseEnumValue<EuFieldValueWidths> ( value );
      }

      return EuFieldValueWidths.Default;

    }//END getValueColumnWidth method

    // ==================================================================================
    /// <summary>
    /// This method fixes the numeric validation parameters.
    /// </summary>
    //  ---------------------------------------------------------------------------------
    public void FixNumericValidation ( )
    {
      //
      // Search the parmeters for existing parameters.
      // and exit if update the value.
      // 
      for ( int count = 0; count < this.Parameters.Count; count++ )
      {
        EuParameter parameter = this.Parameters [ count ];
        if ( parameter.Name == EuFieldParameters.Min_Value.ToString ( ) )
        {
          this.AddParameter ( "MinValue", parameter.Value );
        }
        if ( parameter.Name == EuFieldParameters.Max_Value.ToString ( ) )
        {
          this.AddParameter ( "MaxValue", parameter.Value );
        }
        if ( parameter.Name == EuFieldParameters.Min_Alert.ToString ( ) )
        {
          this.AddParameter ( "MinAlert", parameter.Value );
        }
        if ( parameter.Name == EuFieldParameters.Max_Alert.ToString ( ) )
        {
          this.AddParameter ( "MaxAlert", parameter.Value );
        }
        if ( parameter.Name == EuFieldParameters.Min_Normal.ToString ( ) )
        {
          this.AddParameter ( "MinNormal", parameter.Value );
        }
        if ( parameter.Name == EuFieldParameters.Max_Normal.ToString ( ) )
        {
          this.AddParameter ( "MaxNormal", parameter.Value );
        }
      }

    }//END AddParameter method

    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region Class Static Methods
    //  =================================================================================
    /// <summary>
    /// This method gets a fields edit status based on the edit status hierarchy.
    /// </summary>
    /// <param name="PageStatus">EditsCodes: Contains the page's edit status</param>
    /// <param name="GroupStatus">EditsCodes: Contains the group's edit status</param>
    /// <param name="FieldStatus">EditsCodes: Contains the field's edit status</param>
    /// <returns>EditCodes: Object EditCodes</returns>
    /// <remarks>
    /// This method consists of following steps. 
    /// 
    /// 1. Initialise the method variables. 
    /// 
    /// 2. Set the group status if it is inherited. 
    /// 
    /// 3. Set the field status if it is inherited
    /// 
    /// 4. Return EditCodes object
    /// 
    /// </remarks>
    //  ---------------------------------------------------------------------------------
    public static EuEditAccess getEditStatus (
      EuEditAccess PageStatus,
      EuEditAccess GroupStatus,
      EuEditAccess FieldStatus )
    {
      //
      // Initialise the methods varibles.
      //
      EuEditAccess status = FieldStatus;

      //
      // Set the group status if it is inherited.
      //
      if ( GroupStatus == EuEditAccess.Inherited )
      {
        GroupStatus = PageStatus;
      }

      //
      // Set the field status if it is inherited.
      //
      if ( FieldStatus == EuEditAccess.Inherited )
      {
        status = GroupStatus;
      }

      //
      // Return EditCodes object
      //
      return status;
    }

    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

  }//END CLASS

}//END namespace