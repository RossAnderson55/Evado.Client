/***************************************************************************************
 * <copyright file="Evado.UniForm.Model\Group.cs" company="EVADO HOLDING PTY. LTD.">
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

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Evado.UniForm.Model
{
  /// <summary>
  /// This class defines the page client structure.
  /// </summary>
  [Serializable]
  public partial class EuGroup
  {
    #region Class Initialisation methods

    //   ================================================================================
    /// <summary>
    /// The initialisation method
    /// </summary>
    //  ---------------------------------------------------------------------------------
    public EuGroup( )
    {
      this.Id = Guid.NewGuid ( );

    }//END Group method

    //   ================================================================================
    /// <summary>
    /// This method initialises class with parameters and value
    /// </summary>
    /// <param name="Title">String: Title of the group. </param>
    //  ---------------------------------------------------------------------------------
    public EuGroup(
      String Title )
    {
      this.Id = Guid.NewGuid ( );
      this.Title = Title;

    }//END Group method

    //   ================================================================================
    /// <summary>
    /// This method initialises class with parameters and value
    /// </summary>
    /// <param name="Title">String: Title of the group. </param>
    /// <param name="Description">String: Group description value.</param>
    /// <param name="Status">EditCodes: The group edit status.</param>
    //  ---------------------------------------------------------------------------------
    public EuGroup(
      String Title,
      String Description,
      EuEditAccess Status )
    {
      this.Id = Guid.NewGuid ( );
      this.Title = Title;
      this.Description = Description;
      this.EditAccess = Status;

    }//END Group method

    //   ================================================================================
    /// <summary>
    /// This method initialises class with parameters and value.
    /// </summary>
    /// <param name="Title"> String: Title of the group. </param>
    /// <param name="EditAccess"> EditCodes: The group edit status.</param>
    //  ---------------------------------------------------------------------------------
    public EuGroup(
      String Title,
      EuEditAccess EditAccess )
    {
      this.Id = Guid.NewGuid ( );
      this.Title = Title;
      this.EditAccess = EditAccess;

    }//END Group method

    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region Property members

    /// <summary>
    ///  This property contains an identifier for the group object.
    /// </summary>
    public Guid Id { get; set; } = Guid.Empty;

    /// <summary>
    /// This property contains the group identifier used by the adapter to identify different groups.
    /// </summary>
    [JsonIgnore]
    public String GroupId { get; set; }

    /// <summary>
    /// This Property contains the title of the group.
    /// </summary>
    public String Title { get; set; } = String.Empty;

    ///
    /// This property contains the group's description stored as a parameter.
    ///
    //[JsonIgnore]
    [JsonProperty ( "d" )]
    public String Description { get; set; }

    //===================================================================================
    /// <summary>
    /// This method set the description alignment parmaeter.
    /// </summary>
    //-----------------------------------------------------------------------------------
    [JsonProperty ( "da" )]
    public EuGroupDescriptionAlignments DescriptionAlignment { get; set; } = EuGroupDescriptionAlignments.Left_Align;

    /// <summary>
    /// This propery contains the GroupLayout enumerated value defining the group layout setting.
    /// </summary>
    [JsonProperty ( "ly" )]
    public EuGroupLayouts Layout { get; set; } = EuGroupLayouts.Dynamic;

    //  =================================================================================
    /// <summary>
    /// This property contains definition that how a page layout can be customized.
    /// </summary>
    //  ---------------------------------------------------------------------------------
    [JsonProperty ( "gt" )]
    public EuGroupTypes GroupType { get; set; } = EuGroupTypes.Default;


    /// <summary>
    /// This property contains the page's EuCommand orientation.
    /// Normally lists of objects should be vertical and page EuCommands horizontally.
    /// </summary>
    [JsonProperty ( "cl" )]
    public EuGroupCommandListLayouts CmdLayout { get; set; } = EuGroupCommandListLayouts.Horizontal_Orientation;

    /// <summary>
    /// This property contains a definition whether a group's fields are editable by the user
    /// when it is displayed in the device client.
    /// </summary>
    [JsonProperty ( "ea" )]
    public EuEditAccess EditAccess { get; set; } = EuEditAccess.Inherited;

    /// <summary>
    /// This property contains a list Field objec.
    /// </summary>
    [JsonProperty ( "fl" )]
    public List<EuField> FieldList { get; set; } = new List<EuField> ( );

    /// <summary>
    /// This property contains a list of Parameter object.
    /// </summary>
    [JsonProperty ( "prm" )]
    public List<EuParameter> Parameters { get; set; } = new List<EuParameter> ( );

    /// <summary>
    /// This property contains a list of EuCommand object.
    /// </summary>
    [JsonProperty ( "cmd" )]
    public List<EuCommand> CommandList { get; set; } = new List<EuCommand> ( );


    /// <summary>
    /// This property contains field jutification property setting.
    /// </summary>
    [JsonIgnore]
    public EuFieldLayoutCodes FieldLayout { get; set; } = EuFieldLayoutCodes.Default;

    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region Class Methods

    // ==================================================================================
    /// <summary>
    /// This method sets the group status to the page statu if is set to inherited.
    /// </summary>
    /// <param name="PageStatus">FieldValueWidth: the value of the parameter.</param>
    //  ---------------------------------------------------------------------------------
    public void setGroupStatus( Evado.UniForm.Model.EuEditAccess PageStatus )
    {
      if ( PageStatus == Evado.UniForm.Model.EuEditAccess.Enabled
        && this.EditAccess == EuEditAccess.Inherited )
      {
        this.EditAccess = PageStatus;
      }
    }//END setGroupStatus method

    // ==================================================================================
    /// <summary>
    /// This method adds a new field to the group.
    /// </summary>
    /// <param name="PageField">Field: new field object.</param>
    /// <remarks>
    /// This method consists of following steps.
    /// 
    /// 1. Add PageFiled to the _FieldList list.
    /// 
    /// </remarks>
    //  ---------------------------------------------------------------------------------
    public void addField( EuField PageField )
    {
      this.FieldList.Add ( PageField );
    }

    // ==================================================================================
    /// <summary>
    /// This method adds a new field to the group.
    /// </summary>
    /// <param name="FieldId">field data identifier</param>
    /// <param name="Title">Field title.</param>
    /// <param name="DataType">Field data type</param>
    /// <param name="Value">String data value</param>
    /// <returns>UniForm.Field object.</returns>
    /// <remarks>
    /// This method consisits of following steps.
    /// 
    /// 1. Define the new field.
    /// 
    /// 2. Add the field to the field list.
    /// 
    /// 3. Return the filed object.
    /// 
    /// </remarks>
    //  ---------------------------------------------------------------------------------
    public EuField addField(
      object FieldId,
      String Title,
      Evado.Model.EvDataTypes DataType,
      String Value )
    {
      //
      // define the new field.
      //
      EuField field = new EuField (
        FieldId.ToString ( ),
        Title,
        DataType,
        Value );
      field.EditAccess = this.EditAccess;

      //
      // Add the field to the field list.
      //
      this.FieldList.Add ( field );

      //
      // Return the field object.
      //
      return field;
    }

    // ==================================================================================
    /// <summary>
    /// This method adds a new EuCommand to the group
    /// </summary>
    /// <param name="PageCommand">Command: new page EuCommand.</param>
    /// <remarks>
    /// This method consists of following steps.
    /// 1. Add PageCommand to _CommandList.
    /// 
    /// </remarks>
    //  ---------------------------------------------------------------------------------
    public void addCommand( EuCommand PageCommand )
    {
      this.CommandList.Add ( PageCommand );
    }

    // ==================================================================================
    /// <summary>
    /// This method adds a new EuCommand to the group
    /// </summary>
    /// <param name="Title">String: EuCommand title</param>
    /// <param name="ApplicationId">String: application identifier</param>
    /// <param name="ApplicationObject">String: Application object identifier</param>
    /// <param name="ApplicationMethod">EuMethods: method enumerated value</param>
    /// <returns>UniForm.Field object.</returns>
    /// <remarks>
    /// This method consists of following steps.
    /// 
    /// 1. Initialise the EuCommand object. 
    /// 
    /// 2. Add the EuCommand to the EuCommand list.
    /// 
    /// 3. Return the EuCommand object. 
    /// 
    /// </remarks>
    //  ---------------------------------------------------------------------------------
    public EuCommand addCommand(
      String Title,
      String ApplicationId,
      String ApplicationObject,
      EuMethods ApplicationMethod )
    {
      //
      // Initialise the EuCommand object.
      //
      EuCommand EuCommand = new EuCommand ( Title, ApplicationId, ApplicationObject, ApplicationMethod );

      //
      // Add the comment to the EuCommand list.
      //
      this.CommandList.Add ( EuCommand );

      // 
      // Return the EuCommand object.
      //
      return EuCommand;
    }

    // ==================================================================================
    /// <summary>
    /// This method adds a new EuCommand to the group
    /// </summary>
    /// <param name="Title">String: EuCommand title</param>
    /// <param name="ApplicationId">String: application identifier</param>
    /// <param name="ApplicationObject">String: Application object identifier</param>
    /// <param name="ApplicationMethod">EuMethods: method enumerated value</param>
    /// <returns>UniForm.Field object.</returns>
    /// <remarks>
    /// This method consists of following steps.
    /// 
    /// 1. Initialise the EuCommand object. 
    /// 
    /// 2. Add the EuCommand to the EuCommand list.
    /// 
    /// 3. Return the EuCommand object. 
    /// 
    /// </remarks>
    //  ---------------------------------------------------------------------------------
    public EuCommand addCommand(
      String Title,
      String ApplicationId,
      object ApplicationObject,
      EuMethods ApplicationMethod )
    {
      //
      // Initialise the EuCommand object.
      //
      EuCommand EuCommand = new EuCommand (
        Title,
        ApplicationId,
        ApplicationObject.ToString ( ),
        ApplicationMethod );

      //
      // Add the comment to the EuCommand list.
      //
      this.CommandList.Add ( EuCommand );

      // 
      // Return the EuCommand object.
      //
      return EuCommand;
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region Class Parameter Methods

    // ==================================================================================
    /// <summary>
    /// This method add a parameter to the EuCommand's parameter list..
    /// </summary>
    /// <param name="Name">GroupParameterList: The name of the parameter.</param>
    /// <param name="Value">int: the value of the parameter.</param>
    //  ---------------------------------------------------------------------------------
    public void AddParameter(
      EuGroupParameters Name,
      object Value )
    {
      //
      // Exit for parameters that cannot be added with this parameter.
      //
      if ( Name == EuGroupParameters.Field_Value_Column_Width
        || Name == EuGroupParameters.Command_Height
        || Name == EuGroupParameters.Page_Column
        || Name == EuGroupParameters.BG_Default
        || Name == EuGroupParameters.BG_Alternative
        || Name == EuGroupParameters.BG_Highlighted
        || Name == EuGroupParameters.BG_Mandatory
        || Name == EuGroupParameters.BG_Validation
        || Name == EuGroupParameters.BG_Alert
        || Name == EuGroupParameters.BG_Normal )
      {
        return;
      }

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
    /// This method add a parameter to the EuCommand's parameter list..
    /// </summary>
    /// <param name="Name">GroupParameterList: The name of the parameter.</param>
    /// <param name="Value">bool: the value of the parameter.</param>
    //  ---------------------------------------------------------------------------------
    public void AddParameter(
      EuGroupParameters Name,
      bool Value )
    {
      //
      // Exit for parameters that cannot be added with this parameter.
      //
      if ( Name == EuGroupParameters.Field_Value_Column_Width
        || Name == EuGroupParameters.Command_Height
        || Name == EuGroupParameters.Page_Column
        || Name == EuGroupParameters.BG_Default
        || Name == EuGroupParameters.BG_Alternative
        || Name == EuGroupParameters.BG_Highlighted
        || Name == EuGroupParameters.BG_Mandatory
        || Name == EuGroupParameters.BG_Validation
        || Name == EuGroupParameters.BG_Alert
        || Name == EuGroupParameters.BG_Normal )
      {
        return;
      }

      //
      // get the string value of the parameter list.
      //
      String name = Name.ToString ( );
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
    /// This method adsd a parameter to the EuCommand's parameter list..
    /// </summary>
    /// <param name="Name">String: The name of the parameter.</param>
    /// <returns> String value of the header element</returns>
    //  ---------------------------------------------------------------------------------
    public int GetParameterInt( EuGroupParameters Name )
    {
      String value = this.GetParameter ( Name );

      return Evado.Model.EvStatics.getInteger ( value );

    }//END AddParameter method

    // ==================================================================================
    /// <summary>
    /// This method add a parameter to the EuCommand's parameter list..
    /// </summary>
    /// <param name="Name">GroupParameterList: the name of the parameter.</param>
    /// <returns >bool value</returns>
    //  ---------------------------------------------------------------------------------
    public bool hasParameter( EuGroupParameters Name )
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
    }

    // ==================================================================================
    /// <summary>
    /// This method adds a parameter to the EuCommand's parameter list..
    /// </summary>
    /// <param name="Name">String: The name of the parameter.</param>
    //  ---------------------------------------------------------------------------------
    public void DeleteParameter( EuGroupParameters Name )
    {
      //
      // get the string value of the parameter list.
      //
      String name = Name.ToString ( );
      name = name.Trim ( );

      for ( int count = 0 ; count < this.Parameters.Count ; count++ )
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
    public String GetParameter( EuGroupParameters Name )
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
    /// This method add a parameter to the EuCommand's parameter list..
    /// </summary>
    /// <param name="Name">GroupParameterList: The name of the parameter.</param>
    /// <param name="Value">int: the value of the parameter.</param>
    //  ---------------------------------------------------------------------------------
    private void SetParameter(
      EuGroupParameters Name,
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

    public EuPageColumnCodes PageColumnCode
    {
      get
      {
        string value = this.GetParameter ( EuGroupParameters.Page_Column );

        if ( value == String.Empty )
        {
          return EuPageColumnCodes.Body;
        }
        return Evado.Model.EvStatics.parseEnumValue<EuPageColumnCodes> ( value );
      }
      set
      {
        String sValue = value.ToString ( );

        this.SetParameter ( EuGroupParameters.Page_Column, sValue );
      }
    }

    // ==================================================================================
    /// <summary>
    /// This method add a parameter to the EuCommand's parameter list..
    /// </summary>
    /// <param name="Value">FieldValueWidth: the value of the parameter.</param>
    //  ---------------------------------------------------------------------------------
    public void SetValueColumnWidth(
      EuFieldValueWidths Value )
    {
      //
      // get the string value of the parameter list.
      //
      String value = Value.ToString ( );

      //
      // Add the parameter to the parameter list.
      //
      this.AddParameter ( EuGroupParameters.Field_Value_Column_Width, value );

    }//END addFieldValueWidth method

    // ==================================================================================
    /// <summary>
    /// This method add a parameter to the EuCommand's parameter list..
    /// </summary>
    /// <param name="Value">percentage value between 0 and 100</param>
    //  ---------------------------------------------------------------------------------
    public void SetCommandWidth( double Value )
    {
      if ( Value < 0 )
      {
        return;
      }
      if ( Value > 100 )
      {
        Value = Value / 10;
      }
      if ( Value > 100 )
      {
        Value = Value / 10;
      }
      //
      // get the string value of the parameter list.
      //
      String value = Value.ToString ( );

      //
      // Add the parameter to the parameter list.
      //
      this.SetParameter ( EuGroupParameters.Command_Width, value );

    }//END addFieldValueWidth method

    // ==================================================================================
    /// <summary>
    /// This method add a parameter to the EuCommand's parameter list..
    /// </summary>
    //  ---------------------------------------------------------------------------------
    public double GetCommandWidth( )
    {
      double width = -1;
      //
      // Add the parameter to the parameter list.
      //
      string value = this.GetParameter ( EuGroupParameters.Command_Width );
      value = value.Replace ( "%", "" );

      if ( value == string.Empty )
      {
        return -1;
      }

      if ( double.TryParse ( value, out width ) == false )
      {
        return -1;
      }
      return width;

    }//END addFieldValueWidth method

    // ==================================================================================
    /// <summary>
    /// This method add a parameter to the EuCommand's parameter list..
    /// </summary>
    /// <param name="Value">pixel value between 0 and 1000</param>
    //  ---------------------------------------------------------------------------------
    public void SetCommandHeight( double Value )
    {
      if ( Value < 0 )
      {
        return;
      }
      if ( Value > 1000 )
      {
        Value = 1000;
      }
      //
      // get the string value of the parameter list.
      //
      String value = Value.ToString ( );

      //
      // Add the parameter to the parameter list.
      //
      this.SetParameter ( EuGroupParameters.Command_Height, value );

    }//END addFieldValueWidth method

    // ==================================================================================
    /// <summary>
    /// This method get the value column width if set.
    /// </summary>
    /// <returns>FieldValueWidths enumeration</returns>
    //  ---------------------------------------------------------------------------------
    public EuFieldValueWidths getValueColumnWidth( )
    {
      //
      // get the string value of the parameter list.
      //
      String value = this.GetParameter ( EuGroupParameters.Field_Value_Column_Width );

      if ( value != String.Empty )
      {
        return Evado.Model.EvStatics.parseEnumValue<EuFieldValueWidths> ( value );
      }

      return EuFieldValueWidths.Default;

    }//END getValueColumnWidth method

    // ==================================================================================
    /// <summary>
    /// This method add a parameter to the EuCommand's parameter list..
    /// </summary>
    //  ---------------------------------------------------------------------------------
    public double GetCommandHeight( )
    {
      double height = -1;
      //
      // Add the parameter to the parameter list.
      //
      string value = this.GetParameter ( EuGroupParameters.Command_Height );

      if ( value == string.Empty )
      {
        return -1;
      }

      if ( double.TryParse ( value, out height ) == false )
      {
        return -1;
      }
      return height;

    }//END addFieldValueWidth method


    // ==================================================================================
    /// <summary>
    /// This method add a parameter to for the background colour for EuCommands
    /// </summary>
    /// <param name="Name">GroupParameterList: The name of the parameter.</param>
    /// <param name="Colour">EuBackgroundColours: the selected colour's enumerated value.</param>
    /// <remarks>
    /// This method consists of following steps.
    /// 
    /// 1. Iterate through the list paramater to determine of the parameter already exists and update it.
    /// 
    /// 2. If parameter Name is equal to GroupParameterList name, return
    /// 
    /// 3. Add a new parameter to the list.
    /// 
    /// </remarks>
    //  ---------------------------------------------------------------------------------
    public void SetCommandBackBroundColor(
      EuGroupParameters Name,
      EuBackgroundColours Colour )
    {
      //
      // Exit if the parameter is not a background colour enumeration.
      //
      if ( Name != EuGroupParameters.BG_Default
        && Name != EuGroupParameters.BG_Alternative
        && Name != EuGroupParameters.BG_Highlighted )
      {
        return;
      }

      //
      // get the string value of the parameter list.
      //
      String value = Colour.ToString ( );

      //
      // Add the parameter to the parameter list.
      //
      this.SetParameter ( Name, value );

    }//END AddCommandBackBroundColor method

    // ==================================================================================
    /// <summary>
    /// This method add a parameter to for the background colour for fields
    /// </summary>
    /// <param name="Name">GroupParameterList: The name of the parameter.</param>
    /// <param name="Colour">EuBackgroundColours: the selected colour's enumerated value.</param>
    /// <remarks>
    /// This method consists of following steps.
    /// 
    /// 1. Iterate through the list paramater to determine of the parameter already exists and update it.
    /// 
    /// 2. If parameter Name is equal to GroupParameterList name, return
    /// 
    /// 3. Add a new parameter to the list.
    /// 
    /// </remarks>
    //  ---------------------------------------------------------------------------------
    public void SetFieldBackBroundColor(
      EuGroupParameters Name,
      EuBackgroundColours Colour )
    {
      //
      // Exit if the parameter is not a background colour enumeration.
      //
      if ( Name != EuGroupParameters.BG_Default
        && Name != EuGroupParameters.BG_Mandatory
        && Name != EuGroupParameters.BG_Validation
        && Name != EuGroupParameters.BG_Alert
        && Name != EuGroupParameters.BG_Normal )
      {
        return;
      }

      //
      // get the string value of the parameter list.
      //
      String value = Colour.ToString ( );

      //
      // Add the parameter to the parameter list.
      //
      this.SetParameter ( Name, value );

    }//END SetFieldBackBroundColor method


    // ==================================================================================
    /// <summary>
    /// This method gets the background colour selection.
    /// </summary>
    /// <param name="Name">String: the name of the parameter.</param>
    /// <returns >String value</returns>
    /// <remarks>
    /// This method consists of following steps. 
    /// 
    /// 1. Iterate through prm list of _Parameters.
    /// 
    /// 2. If prm Name is equal to GroupParameterList Name, return prm Value.
    /// 
    /// 3. Return an empty string  
    /// </remarks>

    //  ---------------------------------------------------------------------------------
    public string GetCssBackBroundColor( EuGroupParameters Name )
    {
      //
      // Exit if the parameter is not a background colour enumeration.
      //
      if ( Name != EuGroupParameters.BG_Default
        && Name != EuGroupParameters.BG_Alternative
        && Name != EuGroupParameters.BG_Highlighted )
      {
        return String.Empty;
      }

      //
      // get the string value of the parameter list.
      //
      EuBackgroundColours BgColour = EuBackgroundColours.White;
      String CssColour = String.Empty;

      //
      // Get the parameter value as a string.
      //
      String value = this.GetParameter ( Name );

      try
      {
        BgColour = Evado.Model.EvStatics.parseEnumValue<EuBackgroundColours> ( value );
        CssColour = BgColour.ToString ( );
        CssColour = CssColour.Replace ( "BG_", String.Empty );
        CssColour = CssColour.ToLower ( );
      }
      catch { return CssColour; }


      //
      // Return an empty string 
      //
      return CssColour;

    }//END GetParameter method

    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region class create methods

    // ==================================================================================
    /// <summary>
    /// THis method creates a text client page field object
    /// </summary>
    /// <returns>Field object</returns>
    /// <remarks>
    /// This method consists of following steps.
    /// 
    /// 1. Add the field to the group list.
    /// 
    /// 2. Return the field object.
    /// 
    /// </remarks>
    ///
    // ----------------------------------------------------------------------------------
    public EuField createField( )
    {
      EuField pageField = new EuField ( );

      pageField.Id = Guid.NewGuid ( );
      pageField.Type = Evado.Model.EvDataTypes.Text;
      pageField.EditAccess = this.EditAccess;

      //
      // Add the field to the group list.
      //
      this.FieldList.Add ( pageField );

      //
      // Return the field object.
      //
      return pageField;
    }//END CreateFieldMethod

    // ==================================================================================
    /// <summary>
    /// This method creates a text client page field object
    /// </summary>
    /// <param name="FieldId">string: the field data identifier</param>
    /// <param name="FieldTitle">String: Field title</param>
    /// <param name="Value">String: text content</param>
    /// <param name="Size">Int: lenght of the field in characters</param>
    /// <returns>Field object</returns>
    /// <remarks>
    /// This method consists of following steps. 
    /// 
    /// 1. Add the field to the group list.
    /// 
    /// 2. Return the field object.
    /// </remarks>
    // ----------------------------------------------------------------------------------
    public EuField createTextField(
      object FieldId,
      String FieldTitle,
      String Value,
      int Size )
    {
      if ( Value == null )
      {
        Value = String.Empty;
      }
      EuField pageField = new EuField ( );
      pageField.Id = Guid.NewGuid ( );
      pageField.Type = Evado.Model.EvDataTypes.Text;
      pageField.FieldId = FieldId.ToString ( );
      pageField.Title = FieldTitle;
      pageField.Value = Value;
      pageField.AddParameter ( EuFieldParameters.Width, Size.ToString ( ) );
      pageField.EditAccess = this.EditAccess;

      //
      // Add the field to the group list.
      //
      this.FieldList.Add ( pageField );

      //
      // Return the field object.
      //
      return pageField;
    }//END createTextField method 

    // ==================================================================================
    /// <summary>
    /// This method creates a text client page field object
    /// </summary>
    /// <param name="FieldId">string: the field data identifier</param>
    /// <param name="FieldTitle">String: Field title</param>
    /// <param name="FieldDescription">String: field description </param>
    /// <param name="Value">String: text content</param>
    /// <param name="Size">Int: lenght of the field in characters</param>
    /// <returns>Field object</returns>
    /// <remarks>
    /// This method consists of following steps. 
    /// 
    /// 1. Add the field to the group list.
    /// 
    /// 2. Return the field object.
    /// 
    /// </remarks>
    // ----------------------------------------------------------------------------------
    public EuField createTextField(
      object FieldId,
      String FieldTitle,
      String FieldDescription,
      String Value,
      int Size )
    {
      if ( Value == null )
      {
        Value = String.Empty;
      }
      EuField pageField = new EuField ( );
      pageField.Id = Guid.NewGuid ( );
      pageField.Type = Evado.Model.EvDataTypes.Text;
      pageField.FieldId = FieldId.ToString ( );
      pageField.Title = FieldTitle;
      pageField.Value = Value;
      if ( FieldDescription != String.Empty )
      {
        pageField.Description = FieldDescription;
      }
      pageField.AddParameter ( EuFieldParameters.Width, Size.ToString ( ) );
      pageField.EditAccess = this.EditAccess;

      //
      // Add the field to the group list.
      //
      this.FieldList.Add ( pageField );

      //
      // Return the field object.
      //
      return pageField;
    }//END createTextField method 

    // ==================================================================================
    /// <summary>
    /// This method creates a text client page field object
    /// </summary>
    /// <param name="FieldTitle">String: Field title</param>
    /// <param name="Value">String: text content</param>
    /// <returns>Field object</returns>
    /// <remarks>
    /// This method consists of following steps.
    /// 
    /// 1. Add the field to the group list.
    /// 
    /// 2. Return the field object.
    /// 
    /// </remarks>
    // ----------------------------------------------------------------------------------
    public EuField createReadOnlyTextField(
      String FieldTitle,
      String Value )
    {
      if ( Value == null )
      {
        Value = String.Empty;
      }
      EuField pageField = new EuField ( );
      pageField.Id = Guid.NewGuid ( );
      pageField.Type = Evado.Model.EvDataTypes.Read_Only_Text;
      pageField.FieldId = String.Empty;
      pageField.Title = FieldTitle;
      pageField.Value = Value;
      pageField.EditAccess = EuEditAccess.Disabled;

      //
      // Add the field to the group list.
      //
      this.FieldList.Add ( pageField );

      //
      // Return the field object.
      //
      return pageField;
    }//END createReadOnlyTextField method 

    // ==================================================================================
    /// <summary>
    /// This method creates a text client page field object
    /// </summary>
    /// <param name="FieldId">string: the field data identifier</param>
    /// <param name="FieldTitle">String: Field title</param>
    /// <param name="Value">String: text content</param>
    /// <returns>Field object</returns>
    /// <remarks>
    /// This method consists of following steps.
    /// 
    /// 1. Add the field to the group list.
    /// 
    /// 2. Return the field object.
    /// 
    /// </remarks>
    // ----------------------------------------------------------------------------------
    public EuField createReadOnlyTextField(
      object FieldId,
      String FieldTitle,
      String Value )
    {
      if ( Value == null )
      {
        Value = String.Empty;
      }
      EuField pageField = new EuField ( );
      pageField.Id = Guid.NewGuid ( );
      pageField.Type = Evado.Model.EvDataTypes.Read_Only_Text;
      pageField.FieldId = FieldId.ToString ( );
      pageField.Title = FieldTitle;
      pageField.Value = Value;
      pageField.EditAccess = EuEditAccess.Disabled;

      //
      // Add the field to the group list.
      //
      this.FieldList.Add ( pageField );

      //
      // Return the field object.
      //
      return pageField;
    }//END createReadOnlyTextField method 

    // ==================================================================================
    /// <summary>
    /// This method creates a text client page field object
    /// </summary>
    /// <param name="FieldId">string: the field data identifier</param>
    /// <param name="FieldTitle">String: Field title</param>
    /// <param name="FieldDescription">String: field description </param>
    /// <param name="Value">String: text content</param>
    /// <returns>Field object</returns>
    /// <remarks>
    /// This method consists of following steps. 
    /// 
    /// 1. Add the field to the group list.
    /// 
    /// 2. Return the field object. 
    /// </remarks>
    // ----------------------------------------------------------------------------------
    public EuField createReadOnlyTextField(
      object FieldId,
      String FieldTitle,
      String FieldDescription,
      String Value )
    {
      if ( Value == null )
      {
        Value = String.Empty;
      }
      EuField pageField = new EuField ( );
      pageField.Id = Guid.NewGuid ( );
      pageField.Type = Evado.Model.EvDataTypes.Read_Only_Text;
      pageField.EditAccess = EuEditAccess.Disabled;
      pageField.FieldId = FieldId.ToString ( );
      pageField.Title = FieldTitle;
      if ( FieldDescription != String.Empty )
      {
        pageField.Description = FieldDescription;
      }
      pageField.Value = Value;

      //
      // Add the field to the group list.
      //
      this.FieldList.Add ( pageField );

      //
      // Return the field object.
      //
      return pageField;
    }//END createReadOnlyTextField method 

    // ==================================================================================
    /// <summary>
    /// THis method creates a text client page field object
    /// </summary>
    /// <param name="FieldId">string: the field data identifier</param>
    /// <param name="FieldTitle">String: Field title</param>
    /// <param name="HtmlValue">String: text content</param>
    /// <returns>Field object</returns>
    /// <remarks>
    /// This method consits of following steps.
    /// 
    /// 1. Add the field to the group list.
    /// 
    /// 2. Return the field object. 
    /// 
    /// </remarks>
    // ----------------------------------------------------------------------------------
    public EuField createHtmlLinkField(
      object FieldId,
      String FieldTitle,
      String HtmlValue )
    {
      if ( HtmlValue == null )
      {
        HtmlValue = String.Empty;
      }
      EuField pageField = new EuField ( );
      pageField.Id = Guid.NewGuid ( );
      pageField.Type = Evado.Model.EvDataTypes.Http_Link;
      pageField.EditAccess = EuEditAccess.Disabled;
      pageField.FieldId = FieldId.ToString ( );
      pageField.Title = FieldTitle;
      pageField.Value = HtmlValue;

      //
      // Add the field to the group list.
      //
      this.FieldList.Add ( pageField );

      //
      // Return the field object.
      //
      return pageField;
    }//END createHtmlField method 

    // ==================================================================================
    /// <summary>
    /// This method creates a free text client page field object
    /// </summary>
    /// <param name="FieldId">string: the field data identifier</param>
    /// <param name="FieldTitle">String: Field title</param>
    /// <param name="Value">String: text content</param>
    /// <param name="Size">Int: length of the field in characters</param>
    /// <param name="Rows">Int: height of the field in characters</param>
    /// <param name="FieldDescription">String: Description of Field </param>
    /// <returns>Field object</returns>
    ///<remarks>
    /// This method consists of following steps. 
    /// 
    /// 1. Add the field to the group list. 
    /// 
    /// 2. Return the field object.  
    /// 
    /// </remarks> 
    // ----------------------------------------------------------------------------------
    public EuField createFreeTextField(
      object FieldId,
      String FieldTitle,
      String FieldDescription,
      String Value,
      int Size,
      int Rows )
    {
      if ( Value == null )
      {
        Value = String.Empty;
      }
      EuField pageField = new EuField ( );
      pageField.Id = Guid.NewGuid ( );
      pageField.Type = Evado.Model.EvDataTypes.Free_Text;
      pageField.FieldId = FieldId.ToString ( );
      pageField.Title = FieldTitle;
      if ( FieldDescription != String.Empty )
      {
        pageField.Description = FieldDescription;
      }
      pageField.Value = Value;
      pageField.AddParameter ( EuFieldParameters.Width, Size.ToString ( ) );
      pageField.AddParameter ( EuFieldParameters.Height, Rows.ToString ( ) );
      pageField.EditAccess = this.EditAccess;

      //
      // Add the field to the group list.
      //
      this.FieldList.Add ( pageField );

      //
      // Return the field object.
      //
      return pageField;
    }//END createFreeTextField method  

    // ==================================================================================
    /// <summary>
    /// This method creates a free text client page field object
    /// </summary>
    /// <param name="FieldId">string: the field data identifier</param>
    /// <param name="FieldTitle">String: Field title</param>
    /// <param name="Value">String: text content</param>
    /// <param name="Width">Int: length of the field in characters</param>
    /// <param name="Rows">Int: height of the field in characters</param>
    /// <returns>Field object</returns>
    /// <remarks>
    /// This method consists of following steps.
    /// 
    /// 1. Add the field to the group list. 
    /// 
    /// 2. Return the field object.   
    /// 
    /// </remarks>
    // ----------------------------------------------------------------------------------
    public EuField createFreeTextField(
      object FieldId,
      String FieldTitle,
      String Value,
      int Width,
      int Rows )
    {
      if ( Value == null )
      {
        Value = String.Empty;
      }
      EuField pageField = new EuField ( );
      pageField.Id = Guid.NewGuid ( );
      pageField.Type = Evado.Model.EvDataTypes.Free_Text;
      pageField.FieldId = FieldId.ToString ( );
      pageField.Title = FieldTitle;
      pageField.Value = Value;
      pageField.AddParameter ( EuFieldParameters.Width, Width.ToString ( ) );
      pageField.AddParameter ( EuFieldParameters.Height, Rows.ToString ( ) );
      pageField.EditAccess = this.EditAccess;

      //
      // Add the field to the group list.
      //
      this.FieldList.Add ( pageField );

      //
      // Return the field object.
      //
      return pageField;
    }//END createFreeTextField method 

    // ==================================================================================
    /// <summary>
    /// This method creates a html markup page field object
    /// </summary>
    /// <param name="FieldId">string: the field data identifier</param>
    /// <param name="FieldTitle">String: Field title</param>
    /// <param name="Value">String: text content</param>
    /// <param name="Size">Int: length of the field in characters</param>
    /// <param name="Rows">Int: height of the field in characters</param>
    /// <param name="FieldDescription">String: Description of Field </param>
    /// <returns>Field object</returns>
    ///<remarks>
    /// This method consists of following steps. 
    /// 
    /// 1. Add the field to the group list. 
    /// 
    /// 2. Return the field object.  
    /// 
    /// </remarks> 
    // ----------------------------------------------------------------------------------
    public EuField createHtmlField(
      object FieldId,
      String FieldTitle,
      String FieldDescription,
      String Value )
    {
      if ( Value == null )
      {
        Value = String.Empty;
      }
      EuField pageField = new EuField ( );
      pageField.Id = Guid.NewGuid ( );
      pageField.Type = Evado.Model.EvDataTypes.Html_Content;
      pageField.FieldId = FieldId.ToString ( );
      pageField.Title = FieldTitle;
      if ( FieldDescription != String.Empty )
      {
        pageField.Description = FieldDescription;
      }
      pageField.Value = Value;
      pageField.EditAccess = Evado.UniForm.Model.EuEditAccess.Disabled;

      //
      // Add the field to the group list.
      //
      this.FieldList.Add ( pageField );

      //
      // Return the field object.
      //
      return pageField;
    }//END createHtmlField method  

    // ==================================================================================
    /// <summary>
    /// This method creates a html markup page field object
    /// </summary>
    /// <param name="FieldId">string: the field data identifier</param>
    /// <param name="FieldTitle">String: Field title</param>
    /// <param name="Value">String: text content</param>
    /// <param name="Size">Int: length of the field in characters</param>
    /// <param name="Rows">Int: height of the field in characters</param>
    /// <param name="FieldDescription">String: Description of Field </param>
    /// <returns>Field object</returns>
    ///<remarks>
    /// This method consists of following steps. 
    /// 
    /// 1. Add the field to the group list. 
    /// 
    /// 2. Return the field object.  
    /// 
    /// </remarks> 
    // ----------------------------------------------------------------------------------
    public EuField createHtmlField(
      object FieldId,
      String FieldTitle,
      String Value )
    {
      if ( Value == null )
      {
        Value = String.Empty;
      }
      EuField pageField = new EuField ( );
      pageField.Id = Guid.NewGuid ( );
      pageField.Type = Evado.Model.EvDataTypes.Html_Content;
      pageField.FieldId = FieldId.ToString ( );
      pageField.Title = FieldTitle;
      pageField.Value = Value;
      pageField.EditAccess = Evado.UniForm.Model.EuEditAccess.Disabled;

      //
      // Add the field to the group list.
      //
      this.FieldList.Add ( pageField );

      //
      // Return the field object.
      //
      return pageField;
    }//END createHtmlField method  

    // ==================================================================================
    /// <summary>
    /// This method creates a boolean client page field object
    /// </summary>
    /// <param name="FieldId">string: the field data identifier</param>
    /// <param name="FieldTitle">String: Field title</param>
    /// <param name="Value">bool: field state</param>
    /// <returns>Field object</returns>
    /// <remarks>
    /// This method consists of following steps. 
    /// 
    /// 1. Add the field to the group list. 
    /// 
    /// 2. Return the field object. 
    /// </remarks>
    // ----------------------------------------------------------------------------------
    public EuField createBooleanField(
      object FieldId,
      String FieldTitle,
      bool Value )
    {
      EuField pageField = new EuField ( );
      String stTextValue = "Yes";

      //
      // If State is false set sttextValue to No.
      //
      if ( Value == false )
      {
        stTextValue = "No";
      }

      pageField.Id = Guid.NewGuid ( );
      pageField.Type = Evado.Model.EvDataTypes.Boolean;
      pageField.FieldId = FieldId.ToString ( );
      pageField.Title = FieldTitle;
      pageField.Value = stTextValue;

      pageField.OptionList = new List<Evado.Model.EvOption> ( );
      pageField.OptionList.Add ( new Evado.Model.EvOption ( "Yes" ) );
      pageField.OptionList.Add ( new Evado.Model.EvOption ( "No" ) );
      pageField.EditAccess = this.EditAccess;

      //
      // Add the field to the group list.
      //
      this.FieldList.Add ( pageField );

      //
      // Return the field object.
      //
      return pageField;
    }//END createBooleanField method 

    // ==================================================================================
    /// <summary>
    /// This method creates a boolean client page field object
    /// </summary>
    /// <param name="FieldId">string: the field data identifier</param>
    /// <param name="FieldTitle">String: Field title</param>
    /// <param name="FieldDescription">String: field description </param>
    /// <param name="Value">bool: field state</param>
    /// <returns>Field object</returns>
    /// <remarks> 
    /// This method consists of following steps. 
    /// 
    /// 1. Add the field to the group list.
    /// 
    /// 2. Return the field to the group list. 
    /// 
    /// </remarks>
    // ----------------------------------------------------------------------------------
    public EuField createBooleanField(
      object FieldId,
      String FieldTitle,
      String FieldDescription,
      bool Value )
    {
      EuField pageField = new EuField ( );
      String stTextValue = "Yes";

      //
      // if State is false set stTextValue No.
      //
      if ( Value == false )
      {
        stTextValue = "No";
      }

      pageField.Id = Guid.NewGuid ( );
      pageField.Type = Evado.Model.EvDataTypes.Boolean;
      pageField.FieldId = FieldId.ToString ( );
      pageField.Title = FieldTitle;
      if ( FieldDescription != String.Empty )
      {
        pageField.Description = FieldDescription;
      }
      pageField.Value = stTextValue;
      pageField.AddParameter ( EuFieldParameters.Width, "5" );

      pageField.EditAccess = this.EditAccess;

      //
      // Add the field to the group list.
      //
      this.FieldList.Add ( pageField );

      //
      // Return the field object.
      //
      return pageField;
    }//END createBooleanField methods  

    // ==================================================================================
    /// <summary>
    /// This method creates a boolean client page field object
    /// </summary>
    /// <param name="FieldId">string: the field data identifier</param>
    /// <param name="FieldTitle">String: Field title</param>
    /// <param name="Value">bool: field state</param>
    /// <returns>Field object</returns>
    /// <remarks>
    /// This method consists of following steps. 
    /// 
    /// 1. Add the field to the group list. 
    /// 
    /// 2. Return the field object. 
    /// </remarks>
    // ----------------------------------------------------------------------------------
    public EuField createYesNoField(
      object FieldId,
      String FieldTitle,
      bool Value )
    {
      EuField pageField = new EuField ( );
      String stTextValue = "Yes";

      //
      // If State is false set sttextValue to No.
      //
      if ( Value == false )
      {
        stTextValue = "No";
      }

      pageField.Id = Guid.NewGuid ( );
      pageField.Type = Evado.Model.EvDataTypes.Yes_No;
      pageField.FieldId = FieldId.ToString ( );
      pageField.Title = FieldTitle;
      pageField.Value = stTextValue;
      pageField.EditAccess = this.EditAccess;

      //
      // Add the field to the group list.
      //
      this.FieldList.Add ( pageField );

      //
      // Return the field object.
      //
      return pageField;
    }//END createBooleanField method 

    // ==================================================================================
    /// <summary>
    /// This method creates a boolean client page field object
    /// </summary>
    /// <param name="FieldId">string: the field data identifier</param>
    /// <param name="FieldTitle">String: Field title</param>
    /// <param name="FieldDescription">String: field description </param>
    /// <param name="Value">bool: field state</param>
    /// <returns>Field object</returns>
    /// <remarks> 
    /// This method consists of following steps. 
    /// 
    /// 1. Add the field to the group list.
    /// 
    /// 2. Return the field to the group list. 
    /// 
    /// </remarks>
    // ----------------------------------------------------------------------------------
    public EuField createYesNoField(
      object FieldId,
      String FieldTitle,
      String FieldDescription,
      bool Value )
    {
      EuField pageField = new EuField ( );
      String stTextValue = "Yes";

      //
      // if State is false set stTextValue No.
      //
      if ( Value == false )
      {
        stTextValue = "No";
      }

      pageField.Id = Guid.NewGuid ( );
      pageField.Type = Evado.Model.EvDataTypes.Yes_No;
      pageField.FieldId = FieldId.ToString ( );
      pageField.Title = FieldTitle;
      if ( FieldDescription != String.Empty )
      {
        pageField.Description = FieldDescription;
      }
      pageField.Value = stTextValue;

      pageField.EditAccess = this.EditAccess;

      //
      // Add the field to the group list.
      //
      this.FieldList.Add ( pageField );

      //
      // Return the field object.
      //
      return pageField;
    }//END createBooleanField methods  

    // ==================================================================================
    /// <summary>
    /// This method creates a boolean client page field object
    /// </summary>
    /// <param name="FieldId">string: the field data identifier</param>
    /// <param name="FieldTitle">String: Field title</param>
    /// <param name="FieldDescription">String: field description </param>
    /// <param name="Value">String: filename</param>
    /// <returns>Field object</returns>
    /// <remarks> 
    /// This method consists of following steps. 
    /// 
    /// 1. Add the field to the group list.
    /// 
    /// 2. Return the field to the group list. 
    /// 
    /// </remarks>
    // ----------------------------------------------------------------------------------
    public EuField createBinaryFileField(
      object FieldId,
      String FieldTitle,
      String FieldDescription,
      String Value )
    {
      if ( Value == null )
      {
        Value = String.Empty;
      }
      EuField pageField = new EuField ( );

      pageField.Id = Guid.NewGuid ( );
      pageField.Type = Evado.Model.EvDataTypes.Binary_File;
      pageField.FieldId = FieldId.ToString ( );
      pageField.Title = FieldTitle;
      if ( FieldDescription != String.Empty )
      {
        pageField.Description = FieldDescription;
      }
      pageField.Value = Value;


      pageField.EditAccess = this.EditAccess;

      //
      // Add the field to the group list.
      //
      this.FieldList.Add ( pageField );

      //
      // Return the field object.
      //
      return pageField;
    }//END createBooleanField methods  

    // ==================================================================================
    /// <summary>
    /// This method creates a boolean client page field object
    /// </summary>
    /// <param name="FieldId">string: the field data identifier</param>
    /// <param name="FieldTitle">String: Field title</param>
    /// <param name="Value">String: filename</param>
    /// <returns>Field object</returns>
    /// <remarks> 
    /// This method consists of following steps. 
    /// 
    /// 1. Add the field to the group list.
    /// 
    /// 2. Return the field to the group list. 
    /// 
    /// </remarks>
    // ----------------------------------------------------------------------------------
    public EuField createBinaryFileField(
      object FieldId,
      String FieldTitle,
      String Value )
    {
      if ( Value == null )
      {
        Value = String.Empty;
      }
      EuField pageField = new EuField ( );
      pageField.Id = Guid.NewGuid ( );
      pageField.Type = Evado.Model.EvDataTypes.Binary_File;
      pageField.FieldId = FieldId.ToString ( );
      pageField.Title = FieldTitle;
      pageField.Value = Value;
      pageField.EditAccess = this.EditAccess;

      //
      // Add the field to the group list.
      //
      this.FieldList.Add ( pageField );

      //
      // Return the field object.
      //
      return pageField;
    }//END createBooleanField methods  

    // ==================================================================================
    /// <summary>
    /// This method creates a text client page field object
    /// </summary>
    /// <param name="FieldId">string: the field data identifier</param>
    /// <param name="FieldTitle">String: Field title</param>
    /// <param name="Value">DateTime: field content</param>
    /// <param name="MinimumYear">DateTime: minimum date</param>
    /// <param name="MaximumYear">DateTime: maximum date</param>
    /// <returns>Field object</returns>
    /// <remarks>
    /// This method consists of following steps. 
    /// 
    /// 1. If MinimumDate is equal to 1 JAN 1900, set MaximumDate 1 Jan 2100.
    /// 
    /// 2. If DateContent is equal to CONST_DATE_NULL(1 JAN 1900) of EvStatics, Set pageField Value empty.
    /// 
    /// 3. Add the field to the group list. 
    /// 
    /// 4. Return the field object. 
    /// 
    /// </remarks>
    // ----------------------------------------------------------------------------------
    public EuField createDateField(
      object FieldId,
      String FieldTitle,
      DateTime Value,
      int MinimumYear )
    {
      if ( Value == null )
      {
        Value = EuStatics.CONST_DATE_NULL;
      }

      int MaximumYear = DateTime.Now.Year + 1;

      if ( MinimumYear >= MaximumYear
        || MinimumYear < 1 )
      {
        MinimumYear = MaximumYear - 100;
      }

      EuField pageField = new EuField ( );
      pageField.Id = Guid.NewGuid ( );
      pageField.Type = Evado.Model.EvDataTypes.Date;
      pageField.FieldId = FieldId.ToString ( );
      pageField.Title = FieldTitle;
      pageField.Value = Value.ToString ( "dd MMM yyyy" );
      pageField.AddParameter ( EuFieldParameters.Width, "12" );
      pageField.AddParameter ( EuFieldParameters.Min_Value, MinimumYear.ToString ( ) );
      pageField.AddParameter ( EuFieldParameters.Max_Value, MaximumYear.ToString ( ) );

      //
      // If DateContent is equal to CONST_DATE_NULL(1 JAN 1900) of EvStatics, Set pageField Value empty.
      //
      if ( Value == EuStatics.CONST_DATE_NULL )
      {
        pageField.Value = String.Empty;
      }

      pageField.EditAccess = this.EditAccess;

      //
      // Add the field to the group list.
      //
      this.FieldList.Add ( pageField );

      //
      // Return the field object.
      //
      return pageField;
    }//END createDateField method 

    // ==================================================================================
    /// <summary>
    /// This method creates a text client page field object
    /// </summary>
    /// <param name="FieldId">string: the field data identifier</param>
    /// <param name="FieldTitle">String: Field title</param>
    /// <param name="Value">DateTime: field content</param>
    /// <param name="MinimumYear">DateTime: minimum date</param>
    /// <param name="MaximumYear">DateTime: maximum date</param>
    /// <returns>Field object</returns>
    /// <remarks>
    /// This method consists of following steps. 
    /// 
    /// 1. If MinimumDate is equal to 1 JAN 1900, set MaximumDate 1 Jan 2100.
    /// 
    /// 2. If DateContent is equal to CONST_DATE_NULL(1 JAN 1900) of EvStatics, Set pageField Value empty.
    /// 
    /// 3. Add the field to the group list. 
    /// 
    /// 4. Return the field object. 
    /// 
    /// </remarks>
    // ----------------------------------------------------------------------------------
    public EuField createDateField(
      object FieldId,
      String FieldTitle,
      DateTime Value,
      int MinimumYear,
      int MaximumYear )
    {
      if ( Value == null )
      {
        Value = EuStatics.CONST_DATE_NULL;
      }

      //
      // If MinimumDate is equal to 1 JAN 1900, set MaximumDate 1 Jan 2100.
      //
      if ( MinimumYear == 0 )
      {
        MaximumYear = 2100;
      }

      EuField pageField = new EuField ( );
      pageField.Id = Guid.NewGuid ( );
      pageField.Type = Evado.Model.EvDataTypes.Date;
      pageField.FieldId = FieldId.ToString ( );
      pageField.Title = FieldTitle;
      pageField.Value = Value.ToString ( "dd MMM yyyy" );
      pageField.AddParameter ( EuFieldParameters.Width, "12" );
      pageField.AddParameter ( EuFieldParameters.Min_Value, MinimumYear.ToString ( ) );
      pageField.AddParameter ( EuFieldParameters.Max_Value, MaximumYear.ToString ( ) );

      //
      // If DateContent is equal to CONST_DATE_NULL(1 JAN 1900) of EvStatics, Set pageField Value empty.
      //
      if ( Value == EuStatics.CONST_DATE_NULL )
      {
        pageField.Value = String.Empty;
      }

      pageField.EditAccess = this.EditAccess;

      //
      // Add the field to the group list.
      //
      this.FieldList.Add ( pageField );

      //
      // Return the field object.
      //
      return pageField;
    }//END createDateField method 

    // ==================================================================================
    /// <summary>
    /// This method creates a text client page field object
    /// </summary>
    /// <param name="FieldId">string: the field data identifier</param>
    /// <param name="FieldTitle">String: Field title</param>
    /// <param name="Value">DateTime: field content</param>
    /// <returns>Field object</returns>
    /// <remarks>
    /// This method consists of following steps. 
    /// 
    /// 1. If FiledDescription not equl to empty, Pass Description and FieldDescription to the method
    ///    AddParameter of th Field object 
    /// 
    /// 2. Add the field to the group list. 
    /// 
    /// 3. Return the field object. 
    /// 
    /// </remarks>
    // ----------------------------------------------------------------------------------
    public EuField createDateField(
      object FieldId,
      String FieldTitle,
      DateTime Value )
    {
      if ( Value == null )
      {
        Value = EuStatics.CONST_DATE_NULL;
      }
      EuField pageField = new EuField ( );
      int MaximumYear = DateTime.Now.Year + 1;
      int MinimumYear = MaximumYear - 100;
      pageField.Id = Guid.NewGuid ( );
      pageField.Type = Evado.Model.EvDataTypes.Date;
      pageField.FieldId = FieldId.ToString ( );
      pageField.Title = FieldTitle;

      pageField.Value = EuStatics.getDateAsString ( Value );
      pageField.AddParameter ( EuFieldParameters.Width, "12" );
      pageField.AddParameter ( EuFieldParameters.Min_Value, MinimumYear.ToString ( ) );
      pageField.AddParameter ( EuFieldParameters.Max_Value, MaximumYear.ToString ( ) );

      pageField.EditAccess = this.EditAccess;

      //
      // Add the field to the group list.
      //
      this.FieldList.Add ( pageField );

      //
      // Return the field object.
      //
      return pageField;
    }//END createDateField method 

    // ==================================================================================
    /// <summary>
    /// This method creates a text client page field object
    /// </summary>
    /// <param name="FieldId">string: the field data identifier</param>
    /// <param name="FieldTitle">String: Field title</param>
    /// <param name="Value">DateTime: field content</param>
    /// <returns>Field object</returns>
    /// <remarks>
    /// This method consits of following steps.
    /// 
    /// 1. Add the field to the group list. 
    /// 
    /// 2. Return the field object. 
    /// 
    /// 
    /// </remarks>
    // ----------------------------------------------------------------------------------
    public EuField createTimeField(
      object FieldId,
      String FieldTitle,
      DateTime Value )
    {
      if ( Value == null )
      {
        Value = EuStatics.CONST_DATE_NULL;
      }
      EuField pageField = new EuField ( );
      pageField.Id = Guid.NewGuid ( );
      pageField.Type = Evado.Model.EvDataTypes.Time;
      pageField.FieldId = FieldId.ToString ( );
      pageField.Title = FieldTitle;
      pageField.Value = Value.ToString ( "HH:mm:ss" );
      pageField.AddParameter ( EuFieldParameters.Width, "5" );
      pageField.EditAccess = this.EditAccess;

      //
      // Add the field to the group list.
      //
      this.FieldList.Add ( pageField );

      //
      // Return the field object.
      //
      return pageField;
    }//END createTimeField method 

    // ==================================================================================
    /// <summary>
    /// This method creates a text client page field object
    /// </summary>
    /// <param name="FieldId">string: the field data identifier</param>
    /// <param name="FieldTitle">String: Field title</param>
    /// <param name="FieldDescription">String: field description </param>
    /// <param name="Value">DateTime: field content</param>
    /// <returns>Field object</returns>
    /// <remarks>
    /// This method consists of following steps.
    /// 
    ///1. If FiledDescription not equl to empty, Pass Description and FieldDescription to the method
    ///    AddParameter of th Field object
    ///    
    ///2. Add the field to the group list.
    ///
    ///3. Return the field object. 
    /// </remarks>
    // ----------------------------------------------------------------------------------
    public EuField createTimeField(
      object FieldId,
      String FieldTitle,
      String FieldDescription,
      DateTime Value )
    {
      if ( Value == null )
      {
        Value = EuStatics.CONST_DATE_NULL;
      }
      EuField pageField = new EuField ( );
      pageField.Id = Guid.NewGuid ( );
      pageField.Type = Evado.Model.EvDataTypes.Time;
      pageField.FieldId = FieldId.ToString ( );
      pageField.Title = FieldTitle;

      //
      // If FiledDescription not equl to empty, Pass Description and FieldDescription to the method
      //AddParameter of th Field object 
      //
      if ( FieldDescription != String.Empty )
      {
        pageField.Description = FieldDescription;
      }
      pageField.Value = Value.ToString ( "HH:mm:ss" );
      pageField.AddParameter ( EuFieldParameters.Width, "5" );
      pageField.EditAccess = this.EditAccess;

      //
      // Add the field to the group list.
      //
      this.FieldList.Add ( pageField );

      //
      // Return the field object.
      //
      return pageField;
    }//END createTimeField method 

    // ==================================================================================
    /// <summary>
    /// This method creates a numeric client page field object
    /// </summary>
    /// <param name="FieldId">string: the field data identifier</param>
    /// <param name="FieldTitle">String: Field title</param>
    /// <param name="Value">float: field content</param>
    /// <param name="MinimumValue">float: minimum value</param>
    /// <param name="MaximumValue">float: maximum value</param>
    /// <returns>Field object</returns>
    /// <remarks>
    /// This method consists of following steps. 
    /// 
    /// 1. Add the field to the group list. 
    /// 
    /// 2. Return the field object. 
    /// 
    /// </remarks>
    // ----------------------------------------------------------------------------------
    public EuField createNumericField(
      object FieldId,
      String FieldTitle,
      float Value,
      float MinimumValue,
      float MaximumValue )
    {
      EuField pageField = new EuField ( );
      String stMinimum = MinimumValue.ToString ( );
      String stMaximum = MaximumValue.ToString ( );

      pageField.Id = Guid.NewGuid ( );
      pageField.Type = Evado.Model.EvDataTypes.Numeric;
      pageField.FieldId = FieldId.ToString ( );
      pageField.Title = FieldTitle;
      pageField.Value = Value.ToString ( );
      pageField.AddParameter ( EuFieldParameters.Width, "12" );
      pageField.AddParameter ( EuFieldParameters.Min_Value, stMinimum );
      pageField.AddParameter ( EuFieldParameters.Max_Value, stMaximum );

      pageField.EditAccess = this.EditAccess;

      //
      // Add the field to the group list.
      //
      this.FieldList.Add ( pageField );

      //
      // Return the field object.
      //
      return pageField;
    }//END createNumericField method 

    // ==================================================================================
    /// <summary>
    /// This method creates a numeric client page field object
    /// </summary>
    /// <param name="FieldId">string: the field data identifier</param>
    /// <param name="FieldTitle">String: Field title</param>
    /// <param name="FieldDescription">String: field description </param>
    /// <param name="Value">float: field content</param>
    /// <param name="MinimumValue">float: minimum value</param>
    /// <param name="MaximumValue">float: maximum value</param>
    /// <returns>Field object</returns>
    /// <remarks>
    /// This method consists of following steps. 
    /// 
    /// 1. Add the field to the group list. 
    /// 
    /// 2. Return the field object. 
    /// 
    /// </remarks>
    // ----------------------------------------------------------------------------------
    public EuField createNumericField(
      object FieldId,
      String FieldTitle,
      String FieldDescription,
      float Value,
      float MinimumValue,
      float MaximumValue )
    {
      EuField pageField = new EuField ( );

      pageField.Id = Guid.NewGuid ( );
      pageField.Type = Evado.Model.EvDataTypes.Numeric;
      pageField.FieldId = FieldId.ToString ( );
      pageField.Title = FieldTitle;
      if ( FieldDescription != String.Empty )
      {
        pageField.Description = FieldDescription;
      }
      pageField.Value = Value.ToString ( );
      pageField.AddParameter ( EuFieldParameters.Min_Value, MinimumValue.ToString ( ) );
      pageField.AddParameter ( EuFieldParameters.Max_Value, MaximumValue.ToString ( ) );
      pageField.AddParameter ( EuFieldParameters.Width, "12" );

      pageField.EditAccess = this.EditAccess;

      //
      // Add the field to the group list.
      //
      this.FieldList.Add ( pageField );

      //
      // Return the field object.
      //
      return pageField;
    }//END createNumericField method 

    // ==================================================================================
    /// <summary>
    /// This method creates a numeric client page field object
    /// </summary>
    /// <param name="FieldId">string: the field data identifier</param>
    /// <param name="FieldTitle">String: Field title</param>
    /// <param name="Value">float: field content</param>
    /// <param name="MinimumValue">float: minimum value</param>
    /// <param name="MaximumValue">float: maximum value</param>
    /// <returns>Field object</returns>
    /// <remarks>
    /// This method consists of following steps. 
    /// 
    /// 1. Add the field to the group list. 
    /// 
    /// 2. Return the field object. 
    /// 
    /// </remarks>
    // ----------------------------------------------------------------------------------
    public EuField createIntegerRangeField(
      object FieldId,
      String FieldTitle,
      String Value,
      float MinimumValue,
      float MaximumValue )
    {
      EuField pageField = new EuField ( );

      pageField.Id = Guid.NewGuid ( );
      pageField.Type = Evado.Model.EvDataTypes.Integer_Range;
      pageField.FieldId = FieldId.ToString ( );
      pageField.Title = FieldTitle;
      pageField.Value = Value.ToString ( );
      pageField.AddParameter ( EuFieldParameters.Width, "10" );
      pageField.AddParameter ( EuFieldParameters.Min_Value, MinimumValue.ToString ( ) );
      pageField.AddParameter ( EuFieldParameters.Max_Value, MaximumValue.ToString ( ) );

      pageField.EditAccess = this.EditAccess;

      //
      // Add the field to the group list.
      //
      this.FieldList.Add ( pageField );

      //
      // Return the field object.
      //
      return pageField;

    }//END createIntegerRangeField method 

    // ==================================================================================
    /// <summary>
    /// This method creates a numeric client page field object
    /// </summary>
    /// <param name="FieldId">string: the field data identifier</param>
    /// <param name="FieldTitle">String: Field title</param>
    /// <param name="FieldDescription">String: field description </param>
    /// <param name="Value">float: field content</param>
    /// <param name="MinimumValue">float: minimum value</param>
    /// <param name="MaximumValue">float: maximum value</param>
    /// <returns>Field object</returns>
    /// <remarks>
    /// This method consists of following steps. 
    /// 
    /// 1. Add the field to the group list. 
    /// 
    /// 2. Return the field object. 
    /// 
    /// </remarks>
    // ----------------------------------------------------------------------------------
    public EuField createIntegerRangeField(
      object FieldId,
      String FieldTitle,
      String FieldDescription,
      String Value,
      float MinimumValue,
      float MaximumValue )
    {
      EuField pageField = new EuField ( );

      pageField.Id = Guid.NewGuid ( );
      pageField.Type = Evado.Model.EvDataTypes.Integer_Range;
      pageField.FieldId = FieldId.ToString ( );
      pageField.Title = FieldTitle;
      if ( FieldDescription != String.Empty )
      {
        pageField.Description = FieldDescription;
      }
      pageField.Value = Value.ToString ( );
      pageField.AddParameter ( EuFieldParameters.Min_Value, MinimumValue.ToString ( ) );
      pageField.AddParameter ( EuFieldParameters.Max_Value, MaximumValue.ToString ( ) );
      pageField.AddParameter ( EuFieldParameters.Width, "10" );

      pageField.EditAccess = this.EditAccess;

      //
      // Add the field to the group list.
      //
      this.FieldList.Add ( pageField );

      //
      // Return the field object.
      //
      return pageField;

    }//END createIntegerRangeField method 

    // ==================================================================================
    /// <summary>
    /// This method creates a numeric client page field object
    /// </summary>
    /// <param name="FieldId">string: the field data identifier</param>
    /// <param name="FieldTitle">String: Field title</param>
    /// <param name="Value">float: field content</param>
    /// <param name="MinimumValue">float: minimum value</param>
    /// <param name="MaximumValue">float: maximum value</param>
    /// <returns>Field object</returns>
    /// <remarks>
    /// This method consists of following steps. 
    /// 
    /// 1. Add the field to the group list. 
    /// 
    /// 2. Return the field object. 
    /// 
    /// </remarks>
    // ----------------------------------------------------------------------------------
    public EuField createFloatRangeField(
      object FieldId,
      String FieldTitle,
      String Value,
      float MinimumValue,
      float MaximumValue )
    {
      EuField pageField = new EuField ( );

      pageField.Id = Guid.NewGuid ( );
      pageField.Type = Evado.Model.EvDataTypes.Float_Range;
      pageField.FieldId = FieldId.ToString ( );
      pageField.Title = FieldTitle;
      pageField.Value = Value.ToString ( );
      pageField.AddParameter ( EuFieldParameters.Width, "12" );
      pageField.AddParameter ( EuFieldParameters.Min_Value, MinimumValue.ToString ( ) );
      pageField.AddParameter ( EuFieldParameters.Max_Value, MaximumValue.ToString ( ) );

      pageField.EditAccess = this.EditAccess;

      //
      // Add the field to the group list.
      //
      this.FieldList.Add ( pageField );

      //
      // Return the field object.
      //
      return pageField;

    }//END createFloatRangeField method 

    // ==================================================================================
    /// <summary>
    /// This method creates a numeric client page field object
    /// </summary>
    /// <param name="FieldId">string: the field data identifier</param>
    /// <param name="FieldTitle">String: Field title</param>
    /// <param name="FieldDescription">String: field description </param>
    /// <param name="Value">float: field content</param>
    /// <param name="MinimumValue">float: minimum value</param>
    /// <param name="MaximumValue">float: maximum value</param>
    /// <returns>Field object</returns>
    /// <remarks>
    /// This method consists of following steps. 
    /// 
    /// 1. Add the field to the group list. 
    /// 
    /// 2. Return the field object. 
    /// 
    /// </remarks>
    // ----------------------------------------------------------------------------------
    public EuField createFloatRangeField(
      object FieldId,
      String FieldTitle,
      String FieldDescription,
      String Value,
      float MinimumValue,
      float MaximumValue )
    {
      EuField pageField = new EuField ( );

      pageField.Id = Guid.NewGuid ( );
      pageField.Type = Evado.Model.EvDataTypes.Float_Range;
      pageField.FieldId = FieldId.ToString ( );
      pageField.Title = FieldTitle;
      if ( FieldDescription != String.Empty )
      {
        pageField.Description = FieldDescription;
      }
      pageField.Value = Value.ToString ( );
      pageField.AddParameter ( EuFieldParameters.Min_Value, MinimumValue.ToString ( ) );
      pageField.AddParameter ( EuFieldParameters.Max_Value, MaximumValue.ToString ( ) );
      pageField.AddParameter ( EuFieldParameters.Width, "12" );

      pageField.EditAccess = this.EditAccess;

      //
      // Add the field to the group list.
      //
      this.FieldList.Add ( pageField );

      //
      // Return the field object.
      //
      return pageField;

    }//END createFloatRangeField method 

    // ==================================================================================
    /// <summary>
    /// This method creates a radiobutton list client page field object
    /// </summary>
    /// <param name="FieldId">string: the field data identifier</param>
    /// <param name="FieldTitle">String: Field title</param>
    /// <param name="Value">String: field content</param>
    /// <param name="OptionList">List of Option: list of option objects</param>
    /// <returns>Field object</returns>
    /// <remarks>
    /// This method consists of following steps. 
    /// 
    /// 1. Find the lenght of the option list largest option item less than 50 characters. 
    /// 
    /// 2. Set the maximum selection width th 50 characters
    /// 
    /// 3. Add the field to the group list. 
    /// 
    /// </remarks>
    // ----------------------------------------------------------------------------------
    public EuField createRadioButtonListField(
      object FieldId,
      String FieldTitle,
      String Value,
      List<Evado.Model.EvOption> OptionList )
    {
      if ( Value == null )
      {
        Value = String.Empty;
      }
      EuField pageField = new EuField ( );
      int inColumns = 50;
      //
      // Find the length of the option list largest option item less than 50 characters.
      //
      foreach ( Evado.Model.EvOption option in OptionList )
      {
        //
        // Set the maximum selection width th 50 characters 
        //
        if ( option.Description.Length > inColumns && inColumns <= 50 )
        {

          inColumns = option.Description.Length;
        }
      }//END option iteration

      pageField.Id = Guid.NewGuid ( );
      pageField.Type = Evado.Model.EvDataTypes.Radio_Button_List;
      pageField.FieldId = FieldId.ToString ( );
      pageField.Title = FieldTitle;
      pageField.Value = Value;
      pageField.AddParameter ( EuFieldParameters.Width, inColumns.ToString ( ) );
      pageField.AddParameter ( EuFieldParameters.Height, OptionList.Count.ToString ( ) );

      pageField.OptionList = OptionList;

      pageField.EditAccess = this.EditAccess;

      //
      // Add the field to the group list.
      //
      this.FieldList.Add ( pageField );

      //
      // Return the field object.
      //
      return pageField;

    }//END createRadioButtonListField method


    // ==================================================================================
    /// <summary>
    /// This method creates a radiobutton list client page field object
    /// </summary>
    /// <param name="FieldId">string: the field data identifier</param>
    /// <param name="FieldTitle">String: Field title</param>
    /// <param name="Value">String: field content</param>
    /// <param name="OptionList">List of Option: list of option objects</param>
    /// <returns>Field object</returns>
    /// <remarks>
    /// This method consists of following steps. 
    /// 
    /// 1. Find the lenght of the option list largest option item less than 50 characters. 
    /// 
    /// 2. Set the maximum selection width th 50 characters
    /// 
    /// 3. Add the field to the group list. 
    /// 
    /// </remarks>
    // ----------------------------------------------------------------------------------
    public EuField createRadioButtonListField(
      object FieldId,
      String FieldTitle,
      object Value,
      List<Evado.Model.EvOption> OptionList )
    {
      if ( Value == null )
      {
        Value = String.Empty;
      }
      EuField pageField = new EuField ( );
      int inColumns = 50;
      //
      // Find the length of the option list largest option item less than 50 characters.
      //
      foreach ( Evado.Model.EvOption option in OptionList )
      {
        //
        // Set the maximum selection width th 50 characters 
        //
        if ( option.Description.Length > inColumns && inColumns <= 50 )
        {

          inColumns = option.Description.Length;
        }
      }//END option iteration

      pageField.Id = Guid.NewGuid ( );
      pageField.Type = Evado.Model.EvDataTypes.Radio_Button_List;
      pageField.FieldId = FieldId.ToString ( );
      pageField.Title = FieldTitle;
      pageField.Value = Value.ToString ( );
      pageField.AddParameter ( EuFieldParameters.Width, inColumns.ToString ( ) );
      pageField.AddParameter ( EuFieldParameters.Height, OptionList.Count.ToString ( ) );

      pageField.OptionList = OptionList;

      pageField.EditAccess = this.EditAccess;

      //
      // Add the field to the group list.
      //
      this.FieldList.Add ( pageField );

      //
      // Return the field object.
      //
      return pageField;

    }//END createRadioButtonListField method

    // ==================================================================================
    /// <summary>
    /// This method creates a radiobutton list client page field object
    /// </summary>
    /// <param name="FieldId">string: the field data identifier</param>
    /// <param name="FieldTitle">String: Field title</param>
    /// <param name="FieldDescription">String: field description </param>
    /// <param name="Value">String: field content</param>
    /// <param name="OptionList">List of Option: list of option objects</param>
    /// <returns>Field object</returns>
    /// <remarks>
    /// This method consists of following steps. 
    /// 
    /// 1. Find the length of the option list largest option item less thatn 50 characters.
    /// 
    /// 2. Set the maximum selection width th 50 characters.
    /// 
    /// 3. Add the field to the group list.
    /// 
    /// 4. Return the field object. 
    ///
    /// </remarks>
    // ----------------------------------------------------------------------------------
    public EuField createRadioButtonListField(
      object FieldId,
      String FieldTitle,
      String FieldDescription,
      object Value,
      List<Evado.Model.EvOption> OptionList )
    {
      if ( Value == null )
      {
        Value = String.Empty;
      }
      EuField pageField = new EuField ( );
      int inColumns = 50;

      //
      // Find the length of the option list largest option item less thatn 50 characters.
      //
      foreach ( Evado.Model.EvOption option in OptionList )
      {
        //
        // Set the maximum selection width th 50 characters
        //
        if ( option.Description.Length > inColumns && inColumns <= 50 )
        {

          inColumns = option.Description.Length;
        }
      }//END option iteration

      pageField.Id = Guid.NewGuid ( );
      pageField.Type = Evado.Model.EvDataTypes.Radio_Button_List;
      pageField.FieldId = FieldId.ToString ( );
      pageField.Title = FieldTitle;
      if ( FieldDescription != String.Empty )
      {
        pageField.Description = FieldDescription;
      }
      pageField.Value = Value.ToString ( ); ;
      pageField.AddParameter ( EuFieldParameters.Width, inColumns.ToString ( ) );
      pageField.AddParameter ( EuFieldParameters.Height, OptionList.Count.ToString ( ) );

      pageField.OptionList = OptionList;

      pageField.EditAccess = this.EditAccess;

      //
      // Add the field to the group list.
      //
      this.FieldList.Add ( pageField );

      //
      // Return the field object.
      //
      return pageField;

    }//END createRadioButtonListField method 

    // ==================================================================================
    /// <summary>
    /// This method creates a radiobutton list client page field object
    /// </summary>
    /// <param name="FieldId">string: the field data identifier</param>
    /// <param name="FieldTitle">String: Field title</param>
    /// <param name="Value">String: field content</param>
    /// <param name="OptionList">List of Option: list of option objects</param>
    /// <returns>Field object</returns>
    /// <remarks>
    /// This method consists of following steps. 
    /// 
    /// 1. Find the length of the option list largest option item less thatn 50 characters.
    /// 
    /// 2. Set the maximum selection width th 50 characters
    /// 
    /// 3. Add the field to the group list.
    /// 
    /// 4. Return the field object. 
    /// 
    /// </remarks>
    // ----------------------------------------------------------------------------------
    public Evado.UniForm.Model.EuField createSelectionListField(
      object FieldId,
      String FieldTitle,
      object Value,
      List<Evado.Model.EvOption> OptionList )
    {
      if ( Value == null )
      {
        Value = String.Empty;
      }
      EuField pageField = new EuField ( );
      int inColumns = 50;

      //
      // Find the length of the option list largest option item less thatn 50 characters.
      //
      foreach ( Evado.Model.EvOption option in OptionList )
      {
        //
        // Set the maximum selection width th 50 characters
        //
        if ( option.Description.Length > inColumns
          && inColumns <= 50 )
        {

          inColumns = option.Description.Length;
        }
      }//END option iteration

      pageField.Id = Guid.NewGuid ( );
      pageField.Type = Evado.Model.EvDataTypes.Selection_List;
      pageField.FieldId = FieldId.ToString ( );
      pageField.Title = FieldTitle;
      pageField.Value = Value.ToString ( );
      pageField.AddParameter ( EuFieldParameters.Width, inColumns.ToString ( ) );
      pageField.AddParameter ( EuFieldParameters.Height, OptionList.Count.ToString ( ) );

      pageField.OptionList = OptionList;

      pageField.EditAccess = this.EditAccess;

      //
      // Add the field to the group list.
      //
      this.FieldList.Add ( pageField );

      //
      // Return the field object.
      //
      return pageField;

    }//END createSelectionListField method 

    // ==================================================================================
    /// <summary>
    /// This method creates a radiobutton list client page field object
    /// </summary>
    /// <param name="FieldId">string: the field data identifier</param>
    /// <param name="FieldTitle">String: Field title</param>
    /// <param name="Value">String: field content</param>
    /// <param name="OptionList">List of Option: list of option objects</param>
    /// <returns>Field object</returns>
    /// <remarks>
    /// This method consists of following steps. 
    /// 
    /// 1. Find the length of the option list largest option item less thatn 50 characters.
    /// 
    /// 2. Set the maximum selection width th 50 characters
    /// 
    /// 3. Add the field to the group list.
    /// 
    /// 4. Return the field object. 
    /// 
    /// </remarks>
    // ----------------------------------------------------------------------------------
    public Evado.UniForm.Model.EuField createSelectionListField(
      object FieldId,
      String FieldTitle,
      String Value,
      List<Evado.Model.EvOption> OptionList )
    {
      EuField pageField = new EuField ( );
      int inColumns = 50;

      //
      // Find the length of the option list largest option item less than 50 characters.
      //
      foreach ( Evado.Model.EvOption option in OptionList )
      {
        //
        // Set the maximum selection width th 50 characters
        //
        if ( option.Description.Length > inColumns
          && inColumns <= 50 )
        {

          inColumns = option.Description.Length;
        }
      }//END option iteration

      pageField.Id = Guid.NewGuid ( );
      pageField.Type = Evado.Model.EvDataTypes.Selection_List;
      pageField.FieldId = FieldId.ToString ( );
      pageField.Title = FieldTitle;
      pageField.Value = Value;
      pageField.AddParameter ( EuFieldParameters.Width, inColumns.ToString ( ) );
      pageField.AddParameter ( EuFieldParameters.Height, OptionList.Count.ToString ( ) );

      pageField.OptionList = OptionList;

      pageField.EditAccess = this.EditAccess;

      //
      // Add the field to the group list.
      //
      this.FieldList.Add ( pageField );

      //
      // Return the field object.
      //
      return pageField;

    }//END createSelectionListField method 



    // ==================================================================================
    /// <summary>
    /// This method creates a radiobutton list client page field object
    /// </summary>
    /// <param name="FieldId">string: the field data identifier</param>
    /// <param name="FieldTitle">String: Field title</param>
    /// <param name="FieldDescription">String: field description </param>
    /// <param name="Value">String: field content</param>
    /// <param name="OptionList">List of Option: list of option objects</param>
    /// <returns>Field object</returns>
    /// <remarks>
    /// This method consists of following steps.
    /// 
    /// 1. Find the length of the option list largest option item less thatn 50 characters.
    /// 
    /// 2. Set the maximum selection width th 50 characters.
    /// 
    /// 3. Add the field to the group list. 
    /// 
    /// 4. Return the field object. 
    /// 
    /// </remarks>
    // ----------------------------------------------------------------------------------
    public EuField createSelectionListField(
      object FieldId,
      String FieldTitle,
      String FieldDescription,
      object Value,
      List<Evado.Model.EvOption> OptionList )
    {
      if ( Value == null )
      {
        Value = String.Empty;
      }
      EuField pageField = new EuField ( );
      int inColumns = 50;

      //
      // Find the length of the option list largest option item less thatn 50 characters.
      //
      foreach ( Evado.Model.EvOption option in OptionList )
      {
        //
        // Set the maximum selection width th 50 characters
        //
        if ( option.Description.Length > inColumns
          && inColumns <= 50 )
        {

          inColumns = option.Description.Length;
        }
      }//END option iteration 

      pageField.Id = Guid.NewGuid ( );
      pageField.Type = Evado.Model.EvDataTypes.Selection_List;
      pageField.FieldId = FieldId.ToString ( );
      pageField.Title = FieldTitle;
      if ( FieldDescription != String.Empty )
      {
        pageField.Description = FieldDescription;
      }
      pageField.Value = Value.ToString ( );
      pageField.AddParameter ( EuFieldParameters.Width, inColumns.ToString ( ) );
      pageField.AddParameter ( EuFieldParameters.Height, OptionList.Count.ToString ( ) );

      pageField.OptionList = OptionList;

      pageField.EditAccess = this.EditAccess;

      //
      // Add the field to the group list.
      //
      this.FieldList.Add ( pageField );

      //
      // Return the field object.
      //
      return pageField;
    }//END createSelectionListField method 

    // ==================================================================================
    /// <summary>
    /// This method creates a checkbox list client page field object
    /// </summary>
    /// <param name="FieldId">string: the field data identifier</param>
    /// <param name="FieldTitle">String: Field title</param>
    /// <param name="Value">String: field content</param>
    /// <param name="OptionList">List of Option: list of option objects</param>
    /// <returns>Field object</returns>
    /// <remarks>
    /// This method consists of following steps.
    /// 
    /// 1. Find the length of the option list largest option item less thatn 50 characters.
    /// 
    /// 2. Set the maximum selection width th 50 characters
    /// 
    /// 3. Add the field to the group list. 
    /// 
    /// 4. Return the group object. 
    ///
    /// </remarks>
    // ----------------------------------------------------------------------------------
    public EuField createCheckBoxListField(
      object FieldId,
      String FieldTitle,
      object Value,
      List<Evado.Model.EvOption> OptionList )
    {
      if ( Value == null )
      {
        Value = String.Empty;
      }
      EuField pageField = new EuField ( );
      int inColumns = 50;

      //
      // Find the length of the option list largest option item less thatn 50 characters.
      //
      foreach ( Evado.Model.EvOption option in OptionList )
      {
        //
        // Set the maximum selection width th 50 characters
        //
        if ( option.Description.Length > inColumns
          && inColumns <= 50 )
        {

          inColumns = option.Description.Length;
        }
      }//END option iteration 

      pageField.Id = Guid.NewGuid ( );
      pageField.Type = Evado.Model.EvDataTypes.Check_Box_List;
      pageField.FieldId = FieldId.ToString ( );
      pageField.Title = FieldTitle;
      pageField.Value = Value.ToString ( );
      pageField.AddParameter ( EuFieldParameters.Width, inColumns.ToString ( ) );
      pageField.AddParameter ( EuFieldParameters.Height, OptionList.Count.ToString ( ) );

      pageField.OptionList = OptionList;
      pageField.EditAccess = this.EditAccess;

      //
      // Add the field to the group list.
      //
      this.FieldList.Add ( pageField );

      //
      // Return the field object.
      //
      return pageField;
    }//END createCheckBoxListField method 

    // ==================================================================================
    /// <summary>
    /// This method creates a checkbox list client page field object
    /// </summary>
    /// <param name="FieldId">String: the field data identifier</param>
    /// <param name="FieldTitle">String: Field title</param>
    /// <param name="FieldDescription">String: field description </param>
    /// <param name="Value">String: field content</param>
    /// <param name="OptionList">List of Option: list of option objects</param>
    /// <returns>Field object</returns>
    /// <remarks>
    /// This method consists of following steps. 
    /// 
    /// 1. Find the length of the option list largest option item less thatn 50 characters.
    /// 
    /// 2. Set the maximum selection width th 50 characters
    /// 
    /// 3. Add the field to the group list. 
    /// 
    /// 4. Return the field object. 
    /// 
    /// </remarks>
    // ----------------------------------------------------------------------------------
    public EuField createCheckBoxListField(
      object FieldId,
      String FieldTitle,
      String FieldDescription,
      object Value,
      List<Evado.Model.EvOption> OptionList )
    {
      if ( Value == null )
      {
        Value = String.Empty;
      }
      EuField pageField = new EuField ( );
      int inColumns = 50;

      //
      // Find the length of the option list largest option item less thatn 50 characters.
      //
      foreach ( Evado.Model.EvOption option in OptionList )
      {
        //
        // Set the maximum selection width th 50 characters
        //
        if ( option.Description.Length > inColumns
                  && inColumns <= 50 )
        {

          inColumns = option.Description.Length;
        }
      }//END option iteration 

      pageField.Id = Guid.NewGuid ( );
      pageField.Type = Evado.Model.EvDataTypes.Check_Box_List;
      pageField.FieldId = FieldId.ToString ( );
      pageField.Title = FieldTitle;
      if ( FieldDescription != String.Empty )
      {
        pageField.Description = FieldDescription;
      }
      pageField.Value = Value.ToString ( );
      pageField.AddParameter ( EuFieldParameters.Width, inColumns.ToString ( ) );
      pageField.AddParameter ( EuFieldParameters.Height, OptionList.Count.ToString ( ) );

      pageField.OptionList = OptionList;
      pageField.EditAccess = this.EditAccess;

      //
      // Add the field to the group list.
      //
      this.FieldList.Add ( pageField );

      //
      // Return the field object.
      //
      return pageField;
    }//END createCheckBoxListField method 

    // ==================================================================================
    /// <summary>
    /// This method creates a image client page field object.
    /// 
    /// The image is directly downloaded from the server so we need to provide 
    /// output it as a file to the temp directory.
    /// 
    /// </summary>
    /// <param name="FieldId">String: the field data identifier</param>
    /// <param name="FieldTitle">String: Field title</param>
    /// <param name="ImageFileName">String: the image filename </param>
    /// <param name="Width">Int: length of the image in pixels (value less than 1 are not added) </param>
    /// <param name="Height">Int: height of the image in pixels (value less than 1 are not added) </param>
    /// <returns>Field object</returns>
    /// <remarks>
    /// This method consists of following steps. 
    /// 
    /// 1. If Width is greater than 0, pass the Size and Width to AddParameter method.
    /// 
    /// 2. If Height is greater than 0, pass Rows and Height to AddParameter method. 
    /// 
    /// 3. Add the field to the group list.
    /// 
    /// 4. Return the field object. 
    /// 
    /// 
    /// </remarks>
    // ----------------------------------------------------------------------------------
    public EuField createImageField(
      object FieldId,
      String FieldTitle,
      String ImageFileName,
      int Width,
      int Height )
    {
      if ( ImageFileName == null )
      {
        ImageFileName = String.Empty;
      }
      EuField pageField = new EuField ( );

      pageField.Id = Guid.NewGuid ( );
      pageField.Type = Evado.Model.EvDataTypes.Image;
      pageField.FieldId = FieldId.ToString ( );
      pageField.Title = FieldTitle;
      pageField.Value = ImageFileName;

      //
      // If Width is greater than 0, pass the Size and Width to AddParameter method. 
      //
      if ( Width > 0 )
      {
        pageField.AddParameter ( EuFieldParameters.Width, Width.ToString ( ) );
      }

      //
      // If Height is greater than 0, pass Rows and Height to AddParameter method. 
      //
      if ( Height > 0 )
      {
        pageField.AddParameter ( EuFieldParameters.Height, Height.ToString ( ) );
      }
      pageField.EditAccess = this.EditAccess;

      //
      // Add the field to the group list.
      //
      this.FieldList.Add ( pageField );

      //
      // Return the field object.
      //
      return pageField;

    }//END createImageField method 

    // ==================================================================================
    /// <summary>
    /// This method creates a image client page field object.
    /// 
    /// The image is directly downloaded from the server so we need to provide 
    /// output it as a file to the temp directory.
    /// 
    /// </summary>
    /// <param name="FielIdId">String: the field data identifier</param>
    /// <param name="FieldTitle">String: Field title</param>
    /// <param name="FieldDescription">String: field description </param>
    /// <param name="ImageFileName">String: the image filename </param>
    /// <param name="Width">Int: length of the image in pixels (value less than 1 are not added) </param>
    /// <param name="Height">Int: height of the image in pixels (value less than 1 are not added) </param>
    /// <returns>Field object</returns>
    /// <remarks>
    /// This method consists of following steps. 
    /// 
    /// 1. If FieldDescription is not empty, pass Description and FieldDescription to AddParameter method.
    /// 
    /// 2. If Width is greater than 0, pass Size and Width to AddParameter method. 
    /// 
    /// 3. If Height is greater than 0, pass Rows and Height to AddParameter method. 
    /// 
    /// 4. Add the field to the group list. 
    /// 
    /// 5. Return the field object. 
    /// 
    /// </remarks>
    // ----------------------------------------------------------------------------------
    public EuField createImageField(
      object FielIdId,
      String FieldTitle,
      String FieldDescription,
      String ImageFileName,
      int Width,
      int Height )
    {
      if ( ImageFileName == null )
      {
        ImageFileName = String.Empty;
      }
      EuField pageField = new EuField ( );

      pageField.Id = Guid.NewGuid ( );
      pageField.Type = Evado.Model.EvDataTypes.Image;
      pageField.FieldId = FielIdId.ToString ( );
      pageField.Title = FieldTitle;

      //
      // If FieldDescription is not empty, pass Description and FieldDescription to AddParameter method.
      //

      if ( FieldDescription != String.Empty )
      {
        pageField.Description = FieldDescription;
      }
      pageField.Value = ImageFileName;

      //
      // If Width is greater than 0, pass Size and Width to AddParameter method. 
      //
      if ( Width > 0 )
      {
        pageField.AddParameter ( EuFieldParameters.Width, Width.ToString ( ) );
      }

      //
      // If Height is greater than 0, pass Rows and Height to AddParameter method. 
      //
      if ( Height > 0 )
      {
        pageField.AddParameter ( EuFieldParameters.Height, Height.ToString ( ) );
      }

      pageField.EditAccess = this.EditAccess;

      //
      // Add the field to the group list.
      //
      this.FieldList.Add ( pageField );

      //
      // Return the field object.
      //
      return pageField;
    }//END createImageField method 

    // ==================================================================================
    /// <summary>
    /// This method creates a bar code client page field object.
    /// 
    /// The image is directly downloaded from the server so we need to provide 
    /// output it as a file to the temp directory.
    /// 
    /// </summary>
    /// <param name="FieldId">String: the field data identifier</param>
    /// <param name="FieldTitle">String: Field title</param>
    /// <param name="Value">String: bar code value </param>
    /// <param name="Size">Int: length of the field in characters</param>
    /// <returns>Field object</returns>
    /// <remarks>
    /// This method consists of following steps. 
    /// 
    /// 1. Add the field to the group list. 
    /// 
    /// 2. Return the field object.
    ///
    /// </remarks>
    // ----------------------------------------------------------------------------------
    public EuField createBarCode(
      object FieldId,
      String FieldTitle,
      String Value,
      int Size )
    {
      if ( Value == null )
      {
        Value = String.Empty;
      }
      EuField pageField = new EuField ( );

      pageField.Id = Guid.NewGuid ( );
      pageField.Type = Evado.Model.EvDataTypes.Bar_Code;
      pageField.FieldId = FieldId.ToString ( );
      pageField.Title = FieldTitle;
      pageField.Value = Value;
      pageField.AddParameter ( EuFieldParameters.Width, Size.ToString ( ) );
      pageField.EditAccess = this.EditAccess;

      //
      // Add the field to the group list.
      //
      this.FieldList.Add ( pageField );

      //
      // Return the field object.
      //
      return pageField;
    }//END createBarCode method 

    // ==================================================================================
    /// <summary>
    /// This method creates a bar code client page field object.
    /// 
    /// The image is directly downloaded from the server so we need to provide 
    /// output it as a file to the temp directory.
    /// 
    /// </summary>
    /// <param name="FieldId">String: the field data identifier</param>
    /// <param name="FieldTitle">String: Field title</param>
    /// <param name="FieldDescription">String: field description </param>
    /// <param name="Value">String: barcode value </param>
    /// <param name="Size">Int: length of the field in characters</param>
    /// <returns>Field object</returns>
    /// <remarks>
    /// This method consists of following steps. 
    /// 
    /// 1. If FieldDescription is not equal to empty, pass Description and FieldDescription to AddParameter method.
    /// 
    /// 2. Add the field to the group list.
    /// 
    /// 3. Return the field object. 
    /// 
    /// </remarks>
    // ----------------------------------------------------------------------------------
    public EuField createBarCode(
      object FieldId,
      String FieldTitle,
      String FieldDescription,
      String Value,
      int Size )
    {
      if ( Value == null )
      {
        Value = String.Empty;
      }
      EuField pageField = new EuField ( );

      pageField.Id = Guid.NewGuid ( );
      pageField.Type = Evado.Model.EvDataTypes.Bar_Code;
      pageField.FieldId = FieldId.ToString ( );
      pageField.Title = FieldTitle;

      //
      // If FieldDescription is not equal to empty, pass Description and FieldDescription to AddParameter method. 
      //
      if ( FieldDescription != String.Empty )
      {
        pageField.Description = FieldDescription;
      }
      pageField.Value = Value;
      pageField.AddParameter ( EuFieldParameters.Width, Size.ToString ( ) );
      pageField.EditAccess = this.EditAccess;

      //
      // Add the field to the group list.
      //
      this.FieldList.Add ( pageField );

      //
      // Return the field object.
      //
      return pageField;
    }//END createBarCode method

    // ==================================================================================
    /// <summary>
    /// This method creates a sound client page field object.
    /// 
    /// The image is directly downloaded from the server so we need to provide 
    /// output it as a file to the temp directory.
    /// 
    /// </summary>
    /// <param name="FieldId">String: the field data identifier</param>
    /// <param name="FieldTitle">String: Field title</param>
    /// <param name="FileName">String: Field FileName</param>
    /// <remarks>
    /// This method consists of following steps. 
    /// 
    /// 1. Add the field to the group list. 
    /// 
    /// 2. Return the field object. 
    /// 
    /// </remarks>
    /// <returns>Field object</returns>
    // ----------------------------------------------------------------------------------
    public EuField createSoundField(
      object FieldId,
      String FieldTitle,
      String FileName )
    {
      if ( FileName == null )
      {
        FileName = String.Empty;
      }
      EuField pageField = new EuField ( );

      pageField.Id = Guid.NewGuid ( );
      pageField.Type = Evado.Model.EvDataTypes.Sound;
      pageField.FieldId = FieldId.ToString ( );
      pageField.Title = FieldTitle;
      pageField.Value = FileName;
      pageField.EditAccess = this.EditAccess;

      //
      // Add the field to the group list.
      //
      this.FieldList.Add ( pageField );

      //
      // Return the field object.
      //
      return pageField;
    }//END createSoundField

    // ==================================================================================
    /// <summary>
    /// This method creates a sound client page field object.
    /// 
    /// The image is directly downloaded from the server so we need to provide 
    /// output it as a file to the temp directory.
    /// 
    /// </summary>
    /// <param name="FieldId">String: the field data identifier</param>
    /// <param name="FieldTitle">String: Field title</param>
    /// <param name="VideoUrl">String: streamed video Url</param>
    /// <param name="Width">int: width is pixels</param>
    /// <param name="Height">int: height is pixels</param>
    /// <remarks>
    /// This method consists of following steps. 
    /// 
    /// 1. Add the field to the group list. 
    /// 
    /// 2. Return the field object. 
    /// 
    /// </remarks>
    /// <returns>Field object</returns>
    // ----------------------------------------------------------------------------------
    public EuField createStreamedVideo(
      String FieldTitle,
      String VideoUrl,
      int Width,
      int Height )
    {
      EuField pageField = new EuField ( );
      float ratio = 9 / 16;

      if ( Width == 0 )
      {
        Width = 600;
        Height = ( int ) ratio * Width;
      }

      if ( Height == 0 )
      {
        Height = ( int ) ratio * Width;
      }

      pageField.Id = Guid.NewGuid ( );
      pageField.Type = Evado.Model.EvDataTypes.Streamed_Video;
      pageField.Title = FieldTitle;
      pageField.Value = VideoUrl;
      pageField.EditAccess = Model.EuEditAccess.Disabled;
      pageField.AddParameter ( Evado.UniForm.Model.EuFieldParameters.Width, Width.ToString ( ) );
      pageField.AddParameter ( Evado.UniForm.Model.EuFieldParameters.Height, Height.ToString ( ) );

      //
      // Add the field to the group list.
      //
      this.FieldList.Add ( pageField );

      //
      // Return the field object.
      //
      return pageField;
    }//END createSoundField

    // ==================================================================================
    /// <summary>
    /// This method creates a sound client page field object.
    /// 
    /// The image is directly downloaded from the server so we need to provide 
    /// output it as a file to the temp directory.
    /// 
    /// </summary>
    /// <param name="FieldId">String: the field data identifier</param>
    /// <param name="FieldTitle">String: Field title</param>
    /// <param name="ImageUrl">String: streamed video Url</param>
    /// <param name="Width">int: width is pixels</param>
    /// <param name="Height">int: height is pixels</param>
    /// <remarks>
    /// This method consists of following steps. 
    /// 
    /// 1. Add the field to the group list. 
    /// 
    /// 2. Return the field object. 
    /// 
    /// </remarks>
    /// <returns>Field object</returns>
    // ----------------------------------------------------------------------------------
    public EuField createExternalImage(
      String FieldTitle,
      String ImageUrl,
      int Width,
      int Height )
    {
      EuField pageField = new EuField ( );

      pageField.Id = Guid.NewGuid ( );
      pageField.Type = Evado.Model.EvDataTypes.External_Image;
      pageField.Title = FieldTitle;
      pageField.Value = ImageUrl;
      pageField.EditAccess = Model.EuEditAccess.Disabled;
      pageField.AddParameter ( Evado.UniForm.Model.EuFieldParameters.Width, Width.ToString ( ) );
      pageField.AddParameter ( Evado.UniForm.Model.EuFieldParameters.Height, Height.ToString ( ) );

      //
      // Add the field to the group list.
      //
      this.FieldList.Add ( pageField );

      //
      // Return the field object.
      //
      return pageField;
    }//END createSoundField

    // ==================================================================================
    /// <summary>
    /// This method creates a sound client page field object.
    /// 
    /// The image is directly downloaded from the server so we need to provide 
    /// output it as a file to the temp directory.
    /// 
    /// </summary>
    /// <param name="FieldId">String: the field data identifier</param>
    /// <param name="FieldTitle">String: Field title</param>
    /// <param name="ImageUrl">String: streamed video Url</param>
    /// <param name="Width">int: width is pixels</param>
    /// <param name="Height">int: height is pixels</param>
    /// <remarks>
    /// This method consists of following steps. 
    /// 
    /// 1. Add the field to the group list. 
    /// 
    /// 2. Return the field object. 
    /// 
    /// </remarks>
    /// <returns>Field object</returns>
    // ----------------------------------------------------------------------------------
    public EuField createExternalImage(
      String FieldTitle,
      String ImageUrl )
    {
      EuField pageField = new EuField ( );

      pageField.Id = Guid.NewGuid ( );
      pageField.Type = Evado.Model.EvDataTypes.External_Image;
      pageField.Title = FieldTitle;
      pageField.Value = ImageUrl;
      pageField.EditAccess = Model.EuEditAccess.Disabled;

      //
      // Add the field to the group list.
      //
      this.FieldList.Add ( pageField );

      //
      // Return the field object.
      //
      return pageField;
    }//END createSoundField

    // ==================================================================================
    /// <summary>
    /// This method creates a hidden page field object.
    /// 
    /// </summary>
    /// <param name="FieldId">String: the field data identifier</param>
    /// <param name="Value">String: Sound file enumerator</param>
    /// <returns>Field object</returns>
    /// <remarks>
    /// This method consists of following steps. 
    /// 
    /// 1. Add the field to the group list. 
    /// 
    /// 2. Retrun the field object.
    /// 
    /// </remarks>
    // ----------------------------------------------------------------------------------
    public EuField createHiddenField(
      object FieldId,
      String Value )
    {
      if ( Value == null )
      {
        Value = String.Empty;
      }
      EuField pageField = new EuField ( );

      pageField.Id = Guid.NewGuid ( );
      pageField.Type = Evado.Model.EvDataTypes.Hidden;
      pageField.FieldId = FieldId.ToString ( );
      pageField.Value = Value;

      //
      // Add the field to the group list.
      //
      this.FieldList.Add ( pageField );

      //
      // Return the field object.
      //
      return pageField;
    }//END createHiddenField method

    // ==================================================================================
    /// <summary>
    /// This method creates a text client page field object
    /// </summary>
    /// <param name="FieldId">string: the field data identifier</param>
    /// <param name="FieldTitle">String: Field title</param>
    /// <param name="ColumnCount">Int: table column count</param>
    /// <returns>Field object</returns>
    /// <remarks>
    /// This method consists of following steps. 
    /// 
    /// 1. Add the field to the group list.
    /// 
    /// 2. Return the field object.
    /// 
    /// </remarks>
    // ----------------------------------------------------------------------------------
    public EuField createTableField(
      object FieldId,
      String FieldTitle,
      int ColumnCount )
    {
      EuField groupField = new EuField ( );
      groupField.Id = Guid.NewGuid ( );
      groupField.Type = Evado.Model.EvDataTypes.Table;
      groupField.FieldId = FieldId.ToString ( );
      groupField.Title = FieldTitle;
      groupField.Table = new Evado.Model.EvTable ( );
      groupField.Layout = Evado.UniForm.Model.EuFieldLayoutCodes.Column_Layout;

      groupField.EditAccess = this.EditAccess;

      //
      // Initialise the table header.
      //
      groupField.Table.setHeader ( ColumnCount );
      //
      // Add the field to the group list.
      //
      this.FieldList.Add ( groupField );

      //
      // Return the field object.
      //
      return groupField;
    }//END createTableField method 

    // ==================================================================================
    /// <summary>
    /// This method creates a text client page field object
    /// </summary>
    /// <param name="FieldId">string: the field data identifier</param>
    /// <param name="FieldTitle">String: Field title</param>
    /// <param name="FieldDescription">String: field description </param>
    /// <param name="ColumnCount">Int: table column count</param>
    /// <returns>Field object</returns>
    /// <remarks>
    /// This method consists of following steps. 
    /// 
    /// 1. Add the field to the group list.
    /// 
    /// 2. Return the field object.
    /// 
    /// </remarks>
    // ----------------------------------------------------------------------------------
    public EuField createTableField(
      object FieldId,
      String FieldTitle,
      String FieldDescription,
      int ColumnCount )
    {
      EuField pageField = new EuField ( );
      pageField.Id = Guid.NewGuid ( );
      pageField.Type = Evado.Model.EvDataTypes.Table;
      pageField.FieldId = FieldId.ToString ( );
      pageField.Title = FieldTitle;
      if ( FieldDescription != String.Empty )
      {
        pageField.Description = FieldDescription;
      }
      pageField.Table = new Evado.Model.EvTable ( );

      pageField.EditAccess = this.EditAccess;

      //
      // Initialise the table header.
      //
      pageField.Table.Header = new Evado.Model.EvTableHeader [ ColumnCount ];

      for ( int i = 0 ; i < ColumnCount ; i++ )
      {
        pageField.Table.Header [ i ] = new Evado.Model.EvTableHeader ( );
      }

      //
      // Add the field to the group list.
      //
      this.FieldList.Add ( pageField );

      //
      // Return the field object.
      //
      return pageField;
    }//END createTableField method 

    // ==================================================================================
    /// <summary>
    /// This method creates a currency page field object.
    /// 
    /// </summary>
    /// <param name="FieldId">String: the field data identifier</param>
    /// <param name="FieldTitle">String: Field title</param>
    /// <param name="Value">String: barcode value </param>
    /// <returns>Field object</returns>
    /// <remarks>
    /// This method consists of following steps.
    /// 
    /// 1. Add the field to the group list. 
    /// 
    /// 2. Retrun the field object. 
    /// 
    /// </remarks>
    // ----------------------------------------------------------------------------------
    public EuField createCurrencyCodeFIeld(
      object FieldId,
      String FieldTitle,
      String Value )
    {
      if ( Value == null )
      {
        Value = String.Empty;
      }
      EuField pageField = new EuField ( );

      pageField.Id = Guid.NewGuid ( );
      pageField.Type = Evado.Model.EvDataTypes.Currency;
      pageField.FieldId = FieldId.ToString ( );
      pageField.Title = FieldTitle;
      pageField.Value = Value;
      pageField.AddParameter ( EuFieldParameters.Width, "12" );
      pageField.EditAccess = this.EditAccess;

      //
      // Add the field to the group list.
      //
      this.FieldList.Add ( pageField );

      //
      // Return the field object.
      //
      return pageField;
    }// END createCurrencyCodeFIeld method


    // ==================================================================================
    /// <summary>
    /// This method creates a currency page field object.
    /// 
    /// </summary>
    /// <param name="FieldId">String: the field data identifier</param>
    /// <param name="FieldTitle">String: Field title</param>
    /// <param name="FieldDescription">String: field description </param>
    /// <param name="Value">String: barcode value </param>
    /// <returns>Field object</returns>
    /// <remarks>
    /// This method consists of following steps. 
    /// 
    /// 1. Add the field to the group list. 
    /// 
    /// 2. Return the field object. 
    /// 
    /// </remarks>
    // ----------------------------------------------------------------------------------
    public EuField createCurrencyCodeField(
      object FieldId,
      String FieldTitle,
      String FieldDescription,
      String Value )
    {
      if ( Value == null )
      {
        Value = String.Empty;
      }
      EuField pageField = new EuField ( );

      pageField.Id = Guid.NewGuid ( );
      pageField.Type = Evado.Model.EvDataTypes.Currency;
      pageField.FieldId = FieldId.ToString ( );
      pageField.Title = FieldTitle;
      if ( FieldDescription != String.Empty )
      {
        pageField.Description = FieldDescription;
      }
      pageField.Value = Value;
      pageField.AddParameter ( EuFieldParameters.Width, "12" );
      pageField.EditAccess = this.EditAccess;

      //
      // Add the field to the group list.
      //
      this.FieldList.Add ( pageField );

      //
      // Return the field object.
      //
      return pageField;
    }//END createCurrencyCodeField method

    // ==================================================================================
    /// <summary>
    /// This method creates a email address page field object.
    ///
    /// </summary>
    /// <param name="FieldId">String: the field data identifier</param>
    /// <param name="FieldTitle">String: Field title</param>
    /// <param name="Value">String: barcode value </param>
    /// <returns>Field object</returns>
    ///<remarks>
    /// This method consists of following steps. 
    /// 
    /// 1. Add the field to the group list.
    /// 
    /// 2. Return the field object. 
    /// 
    /// </remarks> 

    // ----------------------------------------------------------------------------------
    public EuField createEmailAddressField(
      object FieldId,
      String FieldTitle,
      String Value )
    {
      if ( Value == null )
      {
        Value = String.Empty;
      }
      EuField pageField = new EuField ( );

      pageField.Id = Guid.NewGuid ( );
      pageField.Type = Evado.Model.EvDataTypes.Email_Address;
      pageField.FieldId = FieldId.ToString ( );
      pageField.Title = FieldTitle;
      pageField.Value = Value;
      pageField.AddParameter ( EuFieldParameters.Width, "80" );
      pageField.EditAccess = this.EditAccess;

      //
      // Add the field to the group list.
      //
      this.FieldList.Add ( pageField );

      //
      // Return the field object.
      //
      return pageField;
    }//END createEmailAddressField method 

    // ==================================================================================
    /// <summary>
    /// This method creates email address page field object.
    /// 
    /// </summary>
    /// <param name="FieldId">String: the field data identifier</param>
    /// <param name="FieldTitle">String: Field title</param>
    /// <param name="FieldDescription">String: field description </param>
    /// <param name="Value">String: barcode value </param>
    /// <returns>Field object</returns>
    /// <remarks>
    /// This method consists of following steps. 
    /// 
    /// 1. If FieldDescription is not equal to empty, Pass Description and FieldDescription to the AddParameter method
    /// 
    /// 2. Add the field value to the group list. 
    /// 
    /// 3. Return the field object. 
    /// 
    /// </remarks>
    // ----------------------------------------------------------------------------------
    public EuField createEmailAddressField(
      object FieldId,
      String FieldTitle,
      String FieldDescription,
      String Value )
    {
      if ( Value == null )
      {
        Value = String.Empty;
      }
      EuField pageField = new EuField ( );

      pageField.Id = Guid.NewGuid ( );
      pageField.Type = Evado.Model.EvDataTypes.Email_Address;
      pageField.FieldId = FieldId.ToString ( );
      pageField.Title = FieldTitle;

      //
      // If FieldDescription is not equal to empty, Pass Description and FieldDescription to the AddParameter method
      //
      if ( FieldDescription != String.Empty )
      {
        pageField.Description = FieldDescription;
      }
      pageField.Value = Value;
      pageField.AddParameter ( EuFieldParameters.Width, 100 );
      pageField.EditAccess = this.EditAccess;

      //
      // Add the field to the group list.
      //
      this.FieldList.Add ( pageField );

      //
      // Return the field object.
      //
      return pageField;
    }//END createEmailAddressField method 

    // ==================================================================================
    /// <summary>
    /// This method creates a telephone number page field object.
    ///
    /// </summary>
    /// <param name="FieldId">String: the field data identifier</param>
    /// <param name="FieldTitle">String: Field title</param>
    /// <param name="Value">String: barcode value </param>
    /// <returns>Field object</returns>
    /// <remarks>
    /// This method consists of following steps. 
    /// 
    /// 1. Add the field to the group list. 
    /// 
    /// 2. Return the field object. 
    /// 
    /// </remarks>
    // ----------------------------------------------------------------------------------
    public EuField createTelephoneNumberField(
      object FieldId,
      String FieldTitle,
      String Value )
    {
      if ( Value == null )
      {
        Value = String.Empty;
      }
      EuField pageField = new EuField ( );

      pageField.Id = Guid.NewGuid ( );
      pageField.Type = Evado.Model.EvDataTypes.Telephone_Number;
      pageField.FieldId = FieldId.ToString ( );
      pageField.Title = FieldTitle;
      pageField.Value = Value;
      pageField.AddParameter ( EuFieldParameters.Width, "15" );
      pageField.EditAccess = this.EditAccess;

      //
      // Add the field to the group list.
      //
      this.FieldList.Add ( pageField );

      //
      // Return the field object.
      //
      return pageField;
    }//END createTelephoneNumberField method 

    // ==================================================================================
    /// <summary>
    /// This method creates a telephone number page field object.
    /// 
    /// </summary>
    /// <param name="FieldId">String: the field data identifier</param>
    /// <param name="FieldTitle">String: Field title</param>
    /// <param name="FieldDescription">String: field description </param>
    /// <param name="Value">String: barcode value </param>
    /// <returns>Field object</returns>
    /// <remarks>
    /// This method consists of following steps.
    /// 
    /// 1. If FieldDescription is not equal to empty, Pass Description and FieldDescription to the AddParameter method
    /// 
    /// 2. Add the field to the group list. 
    /// 
    /// 3. Return the field object. 
    /// 
    /// </remarks>
    // ----------------------------------------------------------------------------------
    public EuField createTelephoneNumberField(
      object FieldId,
      String FieldTitle,
      String FieldDescription,
      String Value )
    {
      if ( Value == null )
      {
        Value = String.Empty;
      }
      EuField pageField = new EuField ( );

      pageField.Id = Guid.NewGuid ( );
      pageField.Type = Evado.Model.EvDataTypes.Telephone_Number;
      pageField.FieldId = FieldId.ToString ( );
      pageField.Title = FieldTitle;

      //
      // If FieldDescription is not equal to empty, Pass Description and FieldDescription to the AddParameter method
      //
      if ( FieldDescription != String.Empty )
      {
        pageField.Description = FieldDescription;
      }
      pageField.Value = Value;
      pageField.AddParameter ( EuFieldParameters.Width, 15 );
      pageField.EditAccess = this.EditAccess;

      //
      // Add the field to the group list.
      //
      this.FieldList.Add ( pageField );

      //
      // Return the field object.
      //
      return pageField;
    }

    // ==================================================================================
    /// <summary>
    /// This method creates a Name page field object.
    ///
    /// </summary>
    /// <param name="FieldId">String: the field data identifier</param>
    /// <param name="FieldTitle">String: Field title</param>
    /// <param name="Value">String: barcode value </param>
    /// <param name="Size">Int: length of the field in characters</param>
    /// <returns>Field object</returns>
    /// <remarks>
    /// This method consists of following steps. 
    /// 
    /// 1. Add the field to the group list. 
    /// 
    /// 2. Return the field object. 
    /// 
    /// </remarks>
    // ----------------------------------------------------------------------------------
    public EuField createNameField(
      object FieldId,
      String FieldTitle,
      String Value )
    {
      if ( Value == null )
      {
        Value = String.Empty;
      }
      EuField pageField = new EuField ( );

      pageField.Id = Guid.NewGuid ( );
      pageField.Type = Evado.Model.EvDataTypes.Name;
      pageField.FieldId = FieldId.ToString ( );
      pageField.Title = FieldTitle;
      pageField.Value = Value;
      pageField.AddParameter ( EuFieldParameters.Width, "40" );
      pageField.EditAccess = this.EditAccess;

      //
      // Add the field to the group list.
      //
      this.FieldList.Add ( pageField );

      //
      // Return the field object.
      //
      return pageField;
    }//END createNameField method 
    // ==================================================================================
    /// <summary>
    /// This method creates a Name page field object.
    ///
    /// </summary>
    /// <param name="FieldId">String: the field data identifier</param>
    /// <param name="FieldTitle">String: Field title</param>
    /// <param name="Value">String: barcode value </param>
    /// <param name="Size">Int: length of the field in characters</param>
    /// <returns>Field object</returns>
    /// <remarks>
    /// This method consists of following steps. 
    /// 
    /// 1. Add the field to the group list. 
    /// 
    /// 2. Return the field object. 
    /// 
    /// </remarks>
    // ----------------------------------------------------------------------------------
    public EuField createNameField(
      object FieldId,
      String FieldTitle,
      String Value,
      bool Format_Prefix,
      bool Format_Middle )
    {
      if ( Value == null )
      {
        Value = String.Empty;
      }
      EuField pageField = new EuField ( );
      String format = String.Empty;

      pageField.Id = Guid.NewGuid ( );
      pageField.Type = Evado.Model.EvDataTypes.Name;
      pageField.FieldId = FieldId.ToString ( );
      pageField.Title = FieldTitle;
      pageField.Value = Value;
      pageField.AddParameter ( EuFieldParameters.Width, "40" );
      pageField.EditAccess = this.EditAccess;

      if ( Format_Prefix == true )
      {
        format += EuField.CONST_NAME_FORMAT_PREFIX;
      }
      format += EuField.CONST_NAME_FORMAT_GIVEN_NAME;

      if ( Format_Middle == true )
      {
        format += EuField.CONST_NAME_FORMAT_MIDDLE_NAME;
      }
      format += EuField.CONST_NAME_FORMAT_FAMILY_NAME;

      pageField.AddParameter ( EuFieldParameters.Format, format );

      //
      // Add the field to the group list.
      //
      this.FieldList.Add ( pageField );

      //
      // Return the field object.
      //
      return pageField;
    }//END createNameField method 

    // ==================================================================================
    /// <summary>
    /// This method creates a name page field object.
    /// 
    /// </summary>
    /// <param name="FieldId">String: the field data identifier</param>
    /// <param name="FieldTitle">String: Field title</param>
    /// <param name="FieldDescription">String: field description </param>
    /// <param name="Value">String: barcode value </param>
    /// <param name="Size">Int: length of the field in characters</param>
    /// <returns>Field object</returns>
    /// <remarks>
    /// This method consists of following steps.
    /// 
    /// 1. If FieldDescription is not equal to empty, Pass Description and FieldDescription to the AddParameter method
    /// 
    /// 2. Add the field to the group list. 
    /// 
    /// 3. Retrun the field object. 
    /// 
    /// </remarks>
    // ----------------------------------------------------------------------------------
    public EuField createNameField(
      object FieldId,
      String FieldTitle,
      String FieldDescription,
      String Value )
    {
      if ( Value == null )
      {
        Value = String.Empty;
      }
      EuField pageField = new EuField ( );

      pageField.Id = Guid.NewGuid ( );
      pageField.Type = Evado.Model.EvDataTypes.Telephone_Number;
      pageField.FieldId = FieldId.ToString ( );
      pageField.Title = FieldTitle;

      //
      // If FieldDescription is not equal to empty, Pass Description and FieldDescription to the AddParameter method
      //
      if ( FieldDescription != String.Empty )
      {
        pageField.Description = FieldDescription;
      }
      pageField.AddParameter ( EuFieldParameters.Width, 40 );
      pageField.Value = Value;
      pageField.EditAccess = this.EditAccess;

      //
      // Add the field to the group list.
      //
      this.FieldList.Add ( pageField );

      //
      // Return the field object.
      //
      return pageField;
    }//END createNameField method

    // ==================================================================================
    /// <summary>
    /// This method creates a Address page field object.
    ///
    /// </summary>
    /// <param name="FieldId">string: the field data identifier</param>
    /// <param name="FieldTitle">String: Field title</param>
    /// <param name="Value">String: barcode value </param>
    /// <returns>Field object</returns>
    /// <remarks>
    /// This method consists of following steps. 
    /// 
    /// 1. Add the field to the group list. 
    /// 
    /// 2. Return the field object. 
    /// 
    /// </remarks>
    // ----------------------------------------------------------------------------------
    public EuField createAddressField(
      object FieldId,
      String FieldTitle,
      String Value )
    {
      if ( Value == null )
      {
        Value = String.Empty;
      }
      EuField pageField = new EuField ( );

      pageField.Id = Guid.NewGuid ( );
      pageField.Type = Evado.Model.EvDataTypes.Address;
      pageField.FieldId = FieldId.ToString ( );
      pageField.Title = FieldTitle;
      pageField.Value = Value;
      pageField.EditAccess = this.EditAccess;

      //
      // Add the field to the group list.
      //
      this.FieldList.Add ( pageField );

      //
      // Return the field object.
      //
      return pageField;
    }//END createAddressField method 

    // ==================================================================================
    /// <summary>
    /// This method creates a Address page field object.
    ///
    /// </summary>
    /// <param name="FieldId">string: the field data identifier</param>
    /// <param name="FieldTitle">String: Field title</param>
    /// <param name="Address_1">String: address line 1 </param>    
    /// <param name="Address_2">String: address line 2 </param>
    /// <param name="Address_City">String: city </param>
    /// <param name="Address_State">String: state</param>
    /// <param name="Address_PostCode">String: post code</param>
    /// <param name="Addrees_Country">String: country </param>
    /// <returns>Field object</returns>
    /// <remarks>
    /// This method consists of following steps. 
    /// 
    /// 1. Add the field to the group list. 
    /// 
    /// 2. Return the field object. 
    /// 
    /// </remarks>
    // ----------------------------------------------------------------------------------
    public EuField createAddressField(
      object FieldId,
      String FieldTitle,
      String Address_1,
      String Address_2,
      String Address_City,
      String Address_State,
      String Address_PostCode,
      String Addrees_Country )
    {
      EuField pageField = new EuField ( );

      pageField.Id = Guid.NewGuid ( );
      pageField.Type = Evado.Model.EvDataTypes.Address;
      pageField.FieldId = FieldId.ToString ( );
      pageField.Title = FieldTitle;
      pageField.Value = Address_1 + ";"
        + Address_2 + ";"
        + Address_City + ";"
        + Address_State + ";"
        + Address_PostCode + ";"
        + Addrees_Country + ";";
      pageField.EditAccess = this.EditAccess;

      //
      // Add the field to the group list.
      //
      this.FieldList.Add ( pageField );

      //
      // Return the field object.
      //
      return pageField;
    }//END createAddressField method 

    // ==================================================================================
    /// <summary>
    /// This method creates a Address page field object.
    /// 
    /// </summary>
    /// <param name="FieldId">String: the field data identifier</param>
    /// <param name="FieldTitle">String: Field title</param>
    /// <param name="FieldDescription">String: field description </param>
    /// <param name="Value">String: barcode value </param>
    /// <returns>Field object</returns>
    /// <remarks>
    /// This method consists of following steps.
    /// 
    /// 1. If FieldDescription is not equal to empty, Pass Description and FieldDescription to the AddParameter method
    /// 
    /// 2. Add the field to the group list. 
    /// 
    /// 3. Return the field object. 
    /// 
    /// </remarks>
    // ----------------------------------------------------------------------------------
    public EuField createAddressField(
      object FieldId,
      String FieldTitle,
      String FieldDescription,
      String Value )
    {
      if ( Value == null )
      {
        Value = String.Empty;
      }
      EuField pageField = new EuField ( );

      pageField.Id = Guid.NewGuid ( );
      pageField.Type = Evado.Model.EvDataTypes.Address;
      pageField.FieldId = FieldId.ToString ( );
      pageField.Title = FieldTitle;

      //
      // If FieldDescription is not equal to empty, Pass Description and FieldDescription to the AddParameter method
      //

      if ( FieldDescription != String.Empty )
      {
        pageField.Description = FieldDescription;
      }
      pageField.Value = Value;
      pageField.EditAccess = this.EditAccess;

      //
      // Add the field to the group list.
      //
      this.FieldList.Add ( pageField );

      //
      // Return the field object.
      //
      return pageField;

    }//END createAddressField method

    // ==================================================================================
    /// <summary>
    /// This method creates an analogue page field object.
    ///
    /// </summary>
    /// <param name="FieldId">String: the field data identifier</param>
    /// <param name="FieldTitle">String: Field title</param>
    /// <param name="Value">String: barc code value </param>
    /// <param name="AnalogueLegendStart">String: Analogue legend start</param>
    /// <param name="AnalogueLegendFinish">String: Analogue legend finish</param>
    /// <param name="AnalogueMaximum">Int: Maximum Analogue</param>
    /// <param name="AnalogueMinimum">Int: Minimum Analogue</param>
    /// <param name="Increment">Int: Increment</param>
    /// <returns>Field object</returns>
    /// <remarks> 
    /// This method consists of following steps. 
    /// 
    /// 1. If AnalogueLegendStart is not equal to empty, Pass Min_Label and AnalogueLegendStart to AddParameter method.
    /// 
    /// 2. If AnalogueLegendFinish is not equal to empty, Pass Max_Label and AnalogueLegendFinish to AddParameter method
    /// 
    /// 3. Add the field to the group list.
    /// 
    /// 4. Return the field object. 
    /// 
    /// </remarks>
    /// 
    // ----------------------------------------------------------------------------------
    public EuField createAnalogueField(
      object FieldId,
      String FieldTitle,
      String Value,
      String AnalogueLegendStart,
      String AnalogueLegendFinish,
      int AnalogueMinimum,
      int AnalogueMaximum,
      int Increment )
    {
      if ( Value == null )
      {
        Value = String.Empty;
      }
      EuField pageField = new EuField ( );

      //
      // Set the max and min default value.
      //
      if ( AnalogueMinimum == 0
        && AnalogueMaximum == 0 )
      {
        AnalogueMaximum = 100;
      }

      pageField.Id = Guid.NewGuid ( );
      pageField.Type = Evado.Model.EvDataTypes.Analogue_Scale;
      pageField.FieldId = FieldId.ToString ( );
      pageField.Title = FieldTitle;
      pageField.Value = Value;
      pageField.EditAccess = this.EditAccess;

      pageField.AddParameter ( EuFieldParameters.Increment, Increment );
      pageField.AddParameter ( EuFieldParameters.Min_Value, AnalogueMinimum );
      pageField.AddParameter ( EuFieldParameters.Max_Value, AnalogueMaximum );

      //
      // If AnalogueLegendStart is not equal to empty, Pass Min_Label and AnalogueLegendStart to AddParameter method
      //
      if ( AnalogueLegendStart != String.Empty )
      {
        pageField.AddParameter ( EuFieldParameters.Min_Label, AnalogueLegendStart );
      }

      //
      // If AnalogueLegendFinish is not equal to empty, Pass Max_Label and AnalogueLegendFinish to AddParameter method
      //
      if ( AnalogueLegendFinish != String.Empty )
      {
        pageField.AddParameter ( EuFieldParameters.Max_Label, AnalogueLegendFinish );
      }
      //
      // Add the field to the group list.
      //
      this.FieldList.Add ( pageField );

      //
      // Return the field object.
      //
      return pageField;
    }//END createAnalogueField method 

    // ==================================================================================
    /// <summary>
    /// This method creates an analogue page field object. 
    /// </summary>
    /// <param name="FieldId">string: the field data identifier</param>
    /// <param name="FieldTitle">String: Field title</param>
    /// <param name="FieldDescription">String: field description </param>
    /// <param name="Value">String: barcode value </param>
    /// <param name="AnalogueLegendStart">String: Analogue legend start</param>
    /// <param name="AnalogueLegendFinish">String: Analogue legend finish</param>
    /// <param name="AnalogueMaximum">Int: Maximum Analogue</param>
    /// <param name="AnalogueMinimum">Int: Minimum Analogue</param>
    /// <param name="Increment">Int: Increment</param>
    /// <returns>Field object</returns>
    /// <remarks> 
    /// This method consists of following methods. 
    ///
    /// 1. If FieldDescription is not equal to empty, Pass Description and FieldDescription to AddParameter method. 
    /// 
    /// 2. If AnalogueLegendStart is not equal to empty, Pass Min_Label and AnalogueLegendStart to AddParameter method.
    /// 
    /// 3. If AnalogueLegendFinish is not equal to empty, Pass Max_Label and AnalogueLegendFinish to AddParameter method
    /// 
    /// 4. Add the field to the group list.
    /// 
    /// 5. Return the field object. 
    /// </remarks>
    // ----------------------------------------------------------------------------------
    public EuField createAnalogueField(
      object FieldId,
      String FieldTitle,
      String FieldDescription,
      String Value,
      String AnalogueLegendStart,
      String AnalogueLegendFinish,
      int AnalogueMinimum,
      int AnalogueMaximum,
      int Increment )
    {
      if ( Value == null )
      {
        Value = String.Empty;
      }
      EuField pageField = new EuField ( );
      if ( AnalogueMinimum == 0
        && AnalogueMaximum == 0 )
      {
        AnalogueMaximum = 100;
      }

      pageField.Id = Guid.NewGuid ( );
      pageField.Type = Evado.Model.EvDataTypes.Address;
      pageField.FieldId = FieldId.ToString ( );
      pageField.Title = FieldTitle;
      pageField.Value = Value;
      pageField.EditAccess = this.EditAccess;

      //
      // If FieldDescription is not equal to empty, Pass Description and FieldDescription to AddParameter method.
      //
      if ( FieldDescription != String.Empty )
      {
        pageField.Description = FieldDescription;
      }
      pageField.AddParameter ( EuFieldParameters.Increment, Increment );
      pageField.AddParameter ( EuFieldParameters.Min_Value, AnalogueMinimum );
      pageField.AddParameter ( EuFieldParameters.Max_Value, AnalogueMaximum );

      //
      // If AnalogueLegendStart is not equal to empty, Pass Min_Label and AnalogueLegendStart to AddParameter method.
      //
      if ( AnalogueLegendStart != String.Empty )
      {
        pageField.AddParameter ( EuFieldParameters.Min_Label, AnalogueLegendStart );
      }
      //
      // If AnalogueLegendFinish is not equal to empty, Pass Max_Label and AnalogueLegendFinish to AddParameter method
      //
      if ( AnalogueLegendFinish != String.Empty )
      {
        pageField.AddParameter ( EuFieldParameters.Max_Label, AnalogueLegendFinish );
      }

      //
      // Add the field to the group list.
      //
      this.FieldList.Add ( pageField );

      //
      // Return the field object.
      //
      return pageField;
    }//END createAnalogueField method 

    // ==================================================================================
    /// <summary>
    /// This method creates a Signature page field object.
    ///
    /// </summary>
    /// <param name="FieldId">string: the field data identifier</param>
    /// <param name="FieldTitle">String: Field title</param>
    /// <param name="Value">String: barc code value </param>
    /// <param name="Width">Int: witdh of the field in pixels</param>
    /// <param name="Height">Int: height of the field in pixels</param>
    /// <returns>Field object</returns>
    // ----------------------------------------------------------------------------------
    public EuField createSignatureField(
      object FieldId,
      String FieldTitle,
      String Value,
      int Width,
      int Height )
    {
      if ( Value == null )
      {
        Value = String.Empty;
      }
      EuField pageField = new EuField ( );

      pageField.Id = Guid.NewGuid ( );
      pageField.Type = Evado.Model.EvDataTypes.Signature;
      pageField.FieldId = FieldId.ToString ( );
      pageField.Title = FieldTitle;
      pageField.Value = Value;
      pageField.EditAccess = this.EditAccess;
      if ( Width > 0 )
      {
        pageField.AddParameter ( EuFieldParameters.Width, Width );
      }
      if ( Height > 0 )
      {
        pageField.AddParameter ( EuFieldParameters.Height, Height );
      }

      //
      // If pageField Value is not equal to empty, set a formatted string to Value.
      //
      if ( pageField.Value == String.Empty )
      {
        pageField.Value = "{\"signature\":[],\"acceptedBy\":\"\"}";
      }

      //
      // Add the field to the group list.
      //
      this.FieldList.Add ( pageField );

      //
      // Return the field object.
      //
      return pageField;
    }//END createSignatureField method

    // ==================================================================================
    /// <summary>
    /// This method creates a Signature page field object.
    ///
    /// </summary>
    /// <param name="FieldId">string: the field data identifier</param>
    /// <param name="FieldTitle">String: Field title</param>
    /// <param name="Value">String: barc code value </param>
    /// <returns>Field object</returns>
    // ----------------------------------------------------------------------------------
    public EuField createSignatureField(
      object FieldId,
      String FieldTitle,
      String Value )
    {
      if ( Value == null )
      {
        Value = String.Empty;
      }
      EuField pageField = new EuField ( );

      pageField.Id = Guid.NewGuid ( );
      pageField.Type = Evado.Model.EvDataTypes.Signature;
      pageField.FieldId = FieldId.ToString ( );
      pageField.Title = FieldTitle;
      pageField.Value = Value;
      pageField.EditAccess = this.EditAccess;

      //
      // Add the field to the group list.
      //
      this.FieldList.Add ( pageField );

      //
      // Return the field object.
      //
      return pageField;
    }//END createSignatureField method

    // ==================================================================================
    /// <summary>
    /// This method creates a Signature page field object.
    /// 
    /// </summary>
    /// <param name="FieldId">string: the field data identifier</param>
    /// <param name="FieldTitle">String: Field title</param>
    /// <param name="FieldDescription">String: field description </param>
    /// <param name="Value">String: barc code value </param>
    /// <param name="Width">Int: witdh of the field in pixels</param>
    /// <param name="Height">Int: height of the field in pixels</param>
    /// <returns>Field object</returns>
    // ----------------------------------------------------------------------------------
    public EuField createSignatureField(
      object FieldId,
      String FieldTitle,
      String FieldDescription,
      String Value,
      int Width,
      int Height )
    {
      //
      // If pageField Value is not equal to empty, set a formatted string to Value.
      //
      if ( Value == null )
      {
        Value = "{\"Signature\":[],\"Name\":\"\",\"AcceptedBy\":\"\",\"DateStamp\":\"1900-01-01T00:00:00\"}";
      }
      if ( Value == String.Empty || Value == "null" )
      {
        Value = "{\"Signature\":[],\"Name\":\"\",\"AcceptedBy\":\"\",\"DateStamp\":\"1900-01-01T00:00:00\"}";
      }
      EuField pageField = new EuField ( );

      pageField.Id = Guid.NewGuid ( );
      pageField.Type = Evado.Model.EvDataTypes.Signature;
      pageField.FieldId = FieldId.ToString ( );
      pageField.Title = FieldTitle;

      //
      // If FieldDescription is not equal to empty, pass Description and FieldDescription to AddParameter method. 
      //
      if ( FieldDescription != String.Empty )
      {
        pageField.Description = FieldDescription;
      }
      if ( Width > 0 )
      {
        pageField.AddParameter ( EuFieldParameters.Width, Width );
      }
      if ( Height > 0 )
      {
        pageField.AddParameter ( EuFieldParameters.Height, Height );
      }
      pageField.Value = Value;
      pageField.EditAccess = this.EditAccess;

      //
      // Add the field to the group list.
      //
      this.FieldList.Add ( pageField );

      //
      // Return the field object.
      //
      return pageField;
    }//END createSignatureField method

    // ==================================================================================
    /// <summary>
    /// This method creates a Signature page field object.
    /// 
    /// </summary>
    /// <param name="FieldId">string: the field data identifier</param>
    /// <param name="FieldTitle">String: Field title</param>
    /// <param name="FieldDescription">String: field description </param>
    /// <param name="Value">String: barc code value </param>
    /// <returns>Field object</returns>
    // ----------------------------------------------------------------------------------
    public EuField createSignatureField(
      object FieldId,
      String FieldTitle,
      String FieldDescription,
      String Value )
    {
      //
      // If pageField Value is not equal to empty, set a formatted string to Value.
      //
      if ( Value == null )
      {
        Value = "{\"Signature\":[],\"Name\":\"\",\"AcceptedBy\":\"\",\"DateStamp\":\"1900-01-01T00:00:00\"}";
      }
      if ( Value == String.Empty )
      {
        Value = "{\"Signature\":[],\"Name\":\"\",\"AcceptedBy\":\"\",\"DateStamp\":\"1900-01-01T00:00:00\"}";
      }
      EuField pageField = new EuField ( );

      pageField.Id = Guid.NewGuid ( );
      pageField.Type = Evado.Model.EvDataTypes.Signature;
      pageField.FieldId = FieldId.ToString ( );
      pageField.Title = FieldTitle;

      //
      // If FieldDescription is not equal to empty, pass Description and FieldDescription to AddParameter method. 
      //
      if ( FieldDescription != String.Empty )
      {
        pageField.Description = FieldDescription;
      }
      pageField.Value = Value;
      pageField.EditAccess = this.EditAccess;


      //
      // Add the field to the group list.
      //
      this.FieldList.Add ( pageField );

      //
      // Return the field object.
      //
      return pageField;
    }//END createSignatureField method

    // ==================================================================================
    /// <summary>
    /// This method creates a password client page field object
    /// </summary>
    /// <param name="FieldId">string: the field data identifier</param>
    /// <param name="FieldTitle">String: Field title</param>
    /// <returns>Field object</returns>
    /// <remarks>
    /// This method consists of following steps. 
    /// 
    /// 1. Add the field to the group list.
    /// 
    /// 2. Return the field object.
    /// 
    /// </remarks>
    // ----------------------------------------------------------------------------------
    public EuField createPasswordField(
      object FieldId,
      String FieldTitle )
    {
      EuField pageField = new EuField ( );
      pageField.Id = Guid.NewGuid ( );
      pageField.Type = Evado.Model.EvDataTypes.Password;
      pageField.FieldId = FieldId.ToString ( );
      pageField.Title = FieldTitle;
      pageField.Value = String.Empty;
      pageField.AddParameter ( EuFieldParameters.Width, "50" );
      pageField.EditAccess = this.EditAccess;

      //
      // Add the field to the group list.
      //
      this.FieldList.Add ( pageField );

      //
      // Return the field object.
      //
      return pageField;
    }//END createPasswordField method 

    // ==================================================================================
    /// <summary>
    /// This method creates a password client page field object
    /// </summary>
    /// <param name="FieldId">string: the field data identifier</param>
    /// <param name="FieldTitle">String: Field title</param>
    /// <param name="FieldDescription">String: field description </param>
    /// <returns>Field object</returns>
    /// <remarks>
    /// This method consists of following steps. 
    /// 
    /// 1. Add the field to the group list.
    /// 
    /// 2. Return the field object.
    /// 
    /// </remarks>
    // ----------------------------------------------------------------------------------
    public EuField createPasswordField(
      object FieldId,
      String FieldTitle,
      String FieldDescription )
    {
      EuField pageField = new EuField ( );
      pageField.Id = Guid.NewGuid ( );
      pageField.Type = Evado.Model.EvDataTypes.Password;
      pageField.FieldId = FieldId.ToString ( );
      pageField.Title = FieldTitle;
      pageField.AddParameter ( EuFieldParameters.Width, "50" );
      pageField.EditAccess = this.EditAccess;

      //
      // Add the field to the group list.
      //
      this.FieldList.Add ( pageField );

      //
      // Return the field object.
      //
      return pageField;
    }//END createPasswordField method 

    // ==================================================================================
    /// <summary>
    /// This method creates a single line chart field object
    /// </summary>
    /// <param name="FieldId">string: the field data identifier</param>
    /// <param name="FieldTitle">String: Field title</param>
    /// <param name="FieldDescription">String: field description </param>
    /// <param name="Value">Evado.UniForm.Model.EuPlotData: plot data</param>
    /// <param name="DisplayLegend">Bool: true = display legend</param>
    /// <returns>Field object</returns>
    /// <remarks>
    /// This method consists of following steps. 
    /// 
    /// 1. Add the field to the group list.
    /// 
    /// 2. Return the field object. 
    /// </remarks>
    // ----------------------------------------------------------------------------------
    public EuField createSingleLineChartField(
      object FieldId,
      String FieldTitle,
      String FieldDescription,
      Evado.UniForm.Model.EuPlotData Value,
      bool DisplayLegend )
    {
      //
      // exit is the plot data is null 
      //
      if ( Value == null )
      {
        return null;
      }

      //
      // Define the methods variables and objects.
      //
      Evado.UniForm.Model.EuPlot plot = new Evado.UniForm.Model.EuPlot ( );
      Evado.UniForm.Model.EuPlotData plotData = new Evado.UniForm.Model.EuPlotData ( );

      Value.Label = FieldId.ToString ( );
      Value.Type = Evado.UniForm.Model.EuPlotData.PlotType.Lines;
      plot.DisplayLegend = DisplayLegend;

      plot.Data.Add ( Value );

      string testData = Newtonsoft.Json.JsonConvert.SerializeObject ( plot );

      //
      // define the uniform field object.
      //
      EuField pageField = new EuField ( );
      pageField.Id = Guid.NewGuid ( );
      pageField.Type = Evado.Model.EvDataTypes.Line_Chart;
      pageField.EditAccess = EuEditAccess.Disabled;
      pageField.FieldId = FieldId.ToString ( );
      pageField.Title = FieldTitle;
      if ( FieldDescription != String.Empty )
      {
        pageField.Description = FieldDescription;
      }
      pageField.Value = testData;

      //
      // Add the field to the group list.
      //
      this.FieldList.Add ( pageField );

      //
      // Return the field object.
      //
      return pageField;
    }//END createSingleLineChartField method 

    // ==================================================================================
    /// <summary>
    /// This method creates a single line chart field object
    /// </summary>
    /// <param name="FieldId">string: the field data identifier</param>
    /// <param name="FieldTitle">String: Field title</param>
    /// <param name="FieldDescription">String: field description </param>
    /// <param name="Value">Evado.UniForm.Model.EuPlotData: plot data</param>
    /// <param name="DisplayLegend">Bool: true = display legend</param>
    /// <returns>Field object</returns>
    /// <remarks>
    /// This method consists of following steps. 
    /// 
    /// 1. Add the field to the group list.
    /// 
    /// 2. Return the field object. 
    /// </remarks>
    // ----------------------------------------------------------------------------------
    public EuField createBarChartField(
      object FieldId,
      String FieldTitle,
      String FieldDescription,
      Evado.UniForm.Model.EuPlotData Value,
      bool DisplayLegend )
    {
      //
      // exit is the plot data is null 
      //
      if ( Value == null )
      {
        return null;
      }

      //
      // Define the methods variables and objects.
      //
      Evado.UniForm.Model.EuPlot plot = new Evado.UniForm.Model.EuPlot ( );
      plot.DisplayLegend = DisplayLegend;
      plot.OverrideXAxisDefinition = false;

      Value.Label = FieldId.ToString ( );
      Value.Type = Evado.UniForm.Model.EuPlotData.PlotType.Bars;

      plot.Data.Add ( Value );


      string testData = Newtonsoft.Json.JsonConvert.SerializeObject ( plot );

      //
      // define the uniform field object.
      //
      EuField pageField = new EuField ( );
      pageField.Id = Guid.NewGuid ( );
      pageField.Type = Evado.Model.EvDataTypes.Line_Chart;
      pageField.EditAccess = EuEditAccess.Disabled;
      pageField.FieldId = FieldId.ToString ( );
      pageField.Title = FieldTitle;
      if ( FieldDescription != String.Empty )
      {
        pageField.Description = FieldDescription;
      }
      pageField.Value = testData;

      //
      // Add the field to the group list.
      //
      this.FieldList.Add ( pageField );

      //
      // Return the field object.
      //
      return pageField;
    }//END createBarChartField method 

    // ==================================================================================
    /// <summary>
    /// This method creates a pie chart chart field object
    /// </summary>
    /// <param name="FieldId">string: the field data identifier</param>
    /// <param name="FieldTitle">String: Field title</param>
    /// <param name="FieldDescription">String: field description </param>
    /// <param name="Value">String: text content</param>
    /// <param name="DisplayLegend">Bool: true = display legend</param>
    /// <returns>Field object</returns>
    /// <remarks>
    /// This method consists of following steps. 
    /// 
    /// 1. Add the field to the group list.
    /// 
    /// 2. Return the field object. 
    /// </remarks>
    // ----------------------------------------------------------------------------------
    public EuField createPieChartField(
      object FieldId,
      String FieldTitle,
      String FieldDescription,
      List<Evado.UniForm.Model.EuPlotData> Value,
      bool DisplayLegend )
    {
      //
      // exit is the plot data is null 
      //
      if ( Value == null )
      {
        return null;
      }

      //
      // Define the methods variables and objects.
      //
      Evado.UniForm.Model.EuPlot plot = new Evado.UniForm.Model.EuPlot ( );
      plot.DisplayLegend = DisplayLegend;

      plot.Data = Value;

      string testData = Newtonsoft.Json.JsonConvert.SerializeObject ( plot );

      //
      // define the uniform field object.
      //
      EuField pageField = new EuField ( );
      pageField.Id = Guid.NewGuid ( );
      pageField.Type = Evado.Model.EvDataTypes.Pie_Chart;
      pageField.EditAccess = EuEditAccess.Disabled;
      pageField.FieldId = FieldId.ToString ( );
      pageField.Title = FieldTitle;
      if ( FieldDescription != String.Empty )
      {
        pageField.Description = FieldDescription;
      }
      pageField.Value = testData;

      //
      // Add the field to the group list.
      //
      this.FieldList.Add ( pageField );

      //
      // Return the field object.
      //
      return pageField;
    }//END createPieChartField method 

    // ==================================================================================
    /// <summary>
    /// This method creates a pie chart chart field object
    /// </summary>
    /// <param name="FieldId">string: the field data identifier</param>
    /// <param name="FieldTitle">String: Field title</param>
    /// <param name="FieldDescription">String: field description </param>
    /// <param name="Value">String: text content</param>
    /// <param name="DisplayLegend">Bool: true = display legend</param>
    /// <returns>Field object</returns>
    /// <remarks>
    /// This method consists of following steps. 
    /// 
    /// 1. Add the field to the group list.
    /// 
    /// 2. Return the field object. 
    /// </remarks>
    // ----------------------------------------------------------------------------------
    public EuField createDonutChartField(
      object FieldId,
      String FieldTitle,
      String FieldDescription,
      List<Evado.UniForm.Model.EuPlotData> Value,
      bool DisplayLegend )
    {
      //
      // exit is the plot data is null 
      //
      if ( Value == null )
      {
        return null;
      }

      //
      // Define the methods variables and objects.
      //
      Evado.UniForm.Model.EuPlot plot = new Evado.UniForm.Model.EuPlot ( );
      plot.DisplayLegend = DisplayLegend;

      plot.Data = Value;

      string testData = Newtonsoft.Json.JsonConvert.SerializeObject ( plot );

      //
      // define the uniform field object.
      //
      EuField pageField = new EuField ( );
      pageField.Id = Guid.NewGuid ( );
      pageField.Type = Evado.Model.EvDataTypes.Donut_Chart;
      pageField.EditAccess = EuEditAccess.Disabled;
      pageField.FieldId = FieldId.ToString ( );
      pageField.Title = FieldTitle;
      if ( FieldDescription != String.Empty )
      {
        pageField.Description = FieldDescription;
      }
      pageField.Value = testData;

      //
      // Add the field to the group list.
      //
      this.FieldList.Add ( pageField );

      //
      // Return the field object.
      //
      return pageField;
    }//END createDonutChartField method 

    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion
  }
}//END namespace