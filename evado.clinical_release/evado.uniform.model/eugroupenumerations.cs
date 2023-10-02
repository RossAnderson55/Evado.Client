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

namespace Evado.UniForm.Model
{
  /// <summary>
  /// This enumeration defines a page EuCommand list enumeration.
  /// </summary>
  public enum EuGroupCommandListLayouts
  {
    /// <summary>
    /// This enumeration defines that the EuCommand is displayed vertically across the page.
    /// </summary>
    Vertical_Orientation = 1, // json = 1

    /// <summary>
    /// This enumeration defines that the EuCommand is displayed horizontally down the page.
    /// </summary>
    Horizontal_Orientation = 2, // json = 2

    /// <summary>
    /// This enumeration defines that the EuCommands are layout out in a tyled structure consisting of rows and columns.
    /// </summary>
    Tiled_Commands = 3, //json = 3

    /// <summary>
    /// This enumeration defines that the EuCommands are layout out in a tabular structure where
    /// the first column is the command and the remaining columns display object data.
    /// </summary>
    Tabular_Commands = 4, //json = 4


  }//END EuCommandListLayout enumeration

  ///<summary>
  /// This enumeration defines the group layout settings
  /// </summary>
  public enum EuGroupLayouts
  {
    /// <summary>
    /// This enumeration defines the EuCommand as not selected or null entry.
    /// </summary>
    Null = 0,  //json 0

    /// <summary>
    /// This enumeration defines a dynamic group layout across the page.
    /// </summary>
    Dynamic = 1,  //json 1

    /// <summary>
    /// This enumeration defines the a page header of the group layout. 
    /// </summary>
    Page_Header = 2, // json 2

    /// <summary>
    /// This enumeration defines the group layout is displayed in full width of the page.
    /// </summary>
    Full_Width = 3, // json 3

  }//END GroupLayout enumeration

  ///<summary>
  /// This enumeration defines the group layout settings
  /// </summary>
  public enum EuGroupDescriptionAlignments
  {
    /// <summary>
    /// This enumeration defines description is left aligned.
    /// </summary>
    Left_Align = 0,  //json 0

    /// <summary>
    /// This enumeration defines description is center aligned.
    /// </summary>
    Center_Align = 1,  //json 1

    /// <summary>
    /// This enumeration defines description is right aligned. 
    /// </summary>
    Right_Align = 2, // json 2

  }//END GroupLayout enumeration

  /// <summary>
  /// This enumeration defines a page EuCommand list enumeration.
  /// </summary>
  public enum EuFieldValueWidths
  {
    /// <summary>
    /// This enumeration defines the default value width.
    /// </summary>
    Default = 60, // json = 60

    /// <summary>
    /// This enumeration defines the forty percent value width option.
    /// </summary>
    Forty_Percent = 40, // json = 40

    /// <summary>
    /// This enumeration defines the twenty percent value width option.
    /// </summary>
    Twenty_Percent = 20, //jason = 20

  }//END EuCommandListLayout enumeration

  /// <summary>
  /// This enumeration defines the Group parameter list options.
  /// </summary>
  public enum EuGroupParameters
  {
    /// <summary>
    /// This enumeration defines the group width in percent if total width
    /// </summary>
    Percent_Width,

    /// <summary>
    /// This enumeration defines the group width in pixels
    /// </summary>
    Pixel_Width,

    /// <summary>
    /// This enumeration defines the group height in pixels
    /// </summary>
    Pixel_Height,

    /// <summary>
    /// This enumeration defines the field input width as a percentage
    /// </summary>
    Field_Value_Column_Width,

    /// <summary>
    /// This enumeration defines the group has a list of objects the 
    /// value contains the PageId of the data object that are displayed.
    /// </summary>
    List_of_Objects,

    /// <summary>
    /// This enumeration defines the field id to be used to determine if 
    /// this group is to be hidden on the client.  When the field value 
    /// in the matching enumeration Hide_Group_If_Value the group is to 
    /// be hidden on the client
    /// </summary>
    Hide_Group_If_Field_Id,

    /// <summary>
    /// This enumeration defines the field value to be used to determine 
    /// if this group is to be hidden on the client.  When the field 
    /// identified by Hide_Group_If_FieldId has a value that matches 
    /// the value if Hide_Group_If_Value the field is to be hidden
    /// </summary>
    Hide_Group_If_Field_Value,

    /// <summary>
    /// This enumeration defines if the group is to be hidden when the page is opened.
    /// </summary>
    Hide_Group_on_Open,

    /// <summary>
    /// This enumeration defines the column the group is to be displayed in.
    /// The values are Left, Body, Right.
    /// </summary>
    Page_Column,

    /// <summary>
    /// This enumeration defines the group selection filter used to refresh 
    /// the group EuCommand list of page references when operating offline.
    /// 
    /// This value contains the page field delimited string identifying
    /// the page fields that are to be used to generated the page filter.  
    /// 
    /// The filters value will be dynamically generated from the field values on the page
    /// containing the list of page EuCommands.
    /// </summary>
    Offline_Selection_Filter,

    /// <summary>
    /// This enumeration defines that the group is to be hidden when offline.
    /// </summary>
    Offline_Hide_Group,

    /// <summary>
    /// This enumeration defines the delimited list of tiled column headers. 
    /// If defined the number column header text defines the number of tiled columns.
    /// </summary>
    Tiled_Column_Header,

    /// <summary>
    /// This enumeration defines the group width in percentage or pixels
    /// </summary>
    Command_Width,

    /// <summary>
    /// This enumeration defines the group height in pixels
    /// </summary>
    Command_Height,

    /// <summary>
    /// This enumeration defines the tabular command header field value.
    /// The columns are '|' delimited.
    /// </summary>
    Command_Tabular_Header,

    /// <summary>
    /// This enumerated value defines the page text color.
    /// </summary>
    Grp_Font_Color,

    /// <summary>
    /// This enumerated value defines the page default font.
    /// </summary>
    Grp_Font,

    /// <summary>
    /// This enumerated value defines the Page font size.
    /// </summary>
    Grp_Font_Size,

    /// <summary>
    /// This enumeration defines the default background colour.
    /// </summary>
    BG_Header,

    /// <summary>
    /// This enumeration defines the default background colour.
    /// </summary>
    BG_Default,

    /// <summary>
    /// This enumeration defines the default alternate background colour.
    /// </summary>
    BG_Alternative,

    /// <summary>
    /// This enumeration defines the selected background colour if the application object had been selected.
    /// </summary>
    BG_Highlighted,

    /// <summary>
    /// This enumeration defines the selected background colour if the application object had been selected.
    /// </summary>
    BG_Selected,

    /// <summary>
    /// This enumeration defines the default background colour if the field is mandatory does not have a value.
    /// </summary>
    BG_Mandatory,

    /// <summary>
    /// This enumeration defines the selected background colour if the field value is outside validation range.
    /// </summary>
    BG_Validation,

    /// <summary>
    /// This enumeration defines the selected background colour if the field value is outside alert range.
    /// </summary>
    BG_Alert,

    /// <summary>
    /// This enumeration defines the selected background colour if the field value is outside normal range.
    /// </summary>
    BG_Normal,

  }//END GroupParameterList enumeration

  /// <summary>
  /// This enumeration defines how a page layout can be customised.
  /// </summary>
  public enum EuGroupTypes
  {
    /// <summary>
    /// This enumeration defines the default page type which is used for general purpose pages.
    /// </summary>
    Default = 0,  // json=0

    /// <summary>
    /// This enumeration defines the annotated field page type. When a selected page 
    /// will be generated with annotation text boxes are inserted beneath all updatable field prompt.
    /// </summary>
    Annotated_Fields = 1, //json=1

    /// <summary>
    /// This enumeration defines the review field page type.  When a selected page 
    /// will be generated with review check box and annotation fields inserted beneath all updatable field prompt.
    /// </summary>
    Review_Fields = 2, //json=2
  }

  /// <summary>
  /// This enumerated list defines the background colour options that 
  /// can be applied to groups, EuCommand and fields.
  /// </summary>
  public enum EuBackgroundColours
  {
    /// <summary>
    /// This enumeration defines the null or empty field.
    /// </summary>
    Null = 0,

    /// <summary>
    /// This enumeration defines that the group EuCommand or field background is to be white
    /// </summary>
    Default = 1,

    /// <summary>
    /// This enumeration defines that the group EuCommand or field background is to be white
    /// </summary>
    White = 2,

    /// <summary>
    /// This enumeration defines that the group EuCommand or field background is to be gray
    /// </summary>
    Gray = 3,

    /// <summary>
    /// This enumeration defines that the group EuCommand or field background is to be dark red.
    /// (Used as the default highlighted value.
    /// </summary>
    Dark_Red = 4,

    /// <summary>
    /// This enumeration defines that the group EuCommand header background is to be dark gray with dark read foreground.
    /// This value is only valid when a EuCommand type value is set to 'Null' and is used to delineate EuCommand menu's   
    /// </summary>
    Header = 5,

    /// <summary>
    /// This enumeration defines that the group EuCommand or field background is to be red.
    /// </summary>
    Red = 6,

    /// <summary>
    /// This enumeration defines that the group EuCommand or field background is to be orange.
    /// </summary>
    Orange = 7,

    /// <summary>
    /// This enumeration defines that the group EuCommand or field background is to be yellow.
    /// </summary>
    Yellow = 8,

    /// <summary>
    /// This enumeration defines that the group EuCommand or field background is to be green.
    /// </summary>
    Green = 9,

    /// <summary>
    /// This enumeration defines that the group EuCommand or field background is to be blue.
    /// </summary>
    Blue = 10,

    /// <summary>
    /// This enumeration defines that the group EuCommand or field background is to be purple.
    /// </summary>
    Purple = 11,
  }

}//END namespace