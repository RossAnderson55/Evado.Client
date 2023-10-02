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
namespace Evado.UniForm.Model
{
  /// <summary>
  /// This enumerate list contains the field paremeter options.
  /// </summary>
  [Serializable]
  public enum EuFieldParameters
  {
    /// <summary>
    /// This enumeration defines the null or empty field.
    /// </summary>
    Null = 0,

    /// <summary>
    /// This enumeration defines the size of the field.
    /// </summary>
    Width = 2,

    /// <summary>
    /// This enumeration defines the number rows in the field.
    /// </summary>
    Height = 3,

    /// <summary>
    /// This enumeration defines the target for the html field.
    /// </summary>
    Target = 4,

    /// <summary>
    /// This enumeration defines the status of the field.
    /// </summary>
    Status = 5,

    /// <summary>
    /// This enumeration defines the field annotation content.
    /// </summary>
    Annotation = 6,

    /// <summary>
    /// This enumeration defines the format of a text field value.
    /// </summary>
    Format = 7,

    /// <summary>
    /// This enumeration defines the minimum field value.
    /// </summary>
    Min_Value = 8,

    /// <summary>
    /// This enumeration defines the maximum field value.
    /// </summary>
    Max_Value = 9,

    /// <summary>
    /// This enumeration defines the minumum alert field value.
    /// </summary>
    Min_Alert = 10,

    /// <summary>
    /// This enumeration defines the maximum alert field value.
    /// </summary>
    Max_Alert = 11,

    /// <summary>
    /// This enumeration defines the minumum alert field value.
    /// </summary>
    Min_Normal = 12,

    /// <summary>
    /// This enumeration defines the maximum alert field value.
    /// </summary>
    Max_Normal = 13,

    /// <summary>
    /// This enumeration defines the minumim label for 
    /// analogue and horizontal radio button fields.
    /// </summary>
    Min_Label = 14,

    /// <summary>
    /// This enumeration defines the maxumim lavel for 
    /// analogue and horizontal radio button fields.
    /// </summary>
    Max_Label = 15,

    /// <summary>
    /// This enumeration defines the field category i.e. a group of fields.
    /// </summary>
    Category = 117,

    /// <summary>
    /// This enumeration defines field's unit as html markup.
    /// </summary>
    Unit = 17,

    /// <summary>
    /// This enumeration defines field's increment for analogue scale as.
    /// </summary>
    Increment = 18,

    /// <summary>
    /// This enumeration defines the field id to be used to determine if this 
    /// field mandatory setting can be changed by another field’s value.  
    /// When the field value in the matching enumeration Mandatory_Field_If_Value
    /// the field is to be set to mandatory.
    /// </summary>
    Mandatory_If_Field_Id = 19,

    /// <summary>
    /// this enumeration defines the field value to be used to determine if this 
    /// field mandatory on the client.  When the field identified by 
    /// Mandatory_Field_If_Field_Id has a value that matches the value if 
    /// Mandatory_Field_If_Value the field is to be mandatory.
    /// </summary>
    Mandatory_If_Value = 20,

    /// <summary>
    /// This enumeration defines that the first custom method EuCommand in 
    /// the group is to be sent to the service when this field changes its value. 
    /// A change of value can be a text change or a selection change.
    /// 
    /// If the parameter value: True: indicates enabled, False: indicated disabled.
    /// </summary>
    Snd_Cmd_On_Change = 21,

    /// <summary>
    /// This enumerated value defines the page text color.
    /// </summary>
    Fld_Font_Color,

    /// <summary>
    /// This enumerated value defines the page default font.
    /// </summary>
    Fld_Font,

    /// <summary>
    /// This enumerated value defines the Page font size.
    /// </summary>
    Fld_Font_Size,

    /// <summary>
    /// This enumeration defines the default background colour.
    /// </summary>
    BG_Default = 22,

    /// <summary>
    /// This enumeration defines the default background colour if the field is mandatory does not have a value.
    /// </summary>
    BG_Mandatory = 23,

    /// <summary>
    /// This enumeration defines the selected background colour if the field value is outside validation range.
    /// </summary>
    BG_Validation = 24,

    /// <summary>
    /// This enumeration defines the selected background colour if the field value is outside alert range.
    /// </summary>
    BG_Alert = 25,

    /// <summary>
    /// This enumeration defines the selected background colour if the field value is outside normal range.
    /// </summary>
    BG_Normal = 26,

    /// <summary>
    /// This enumeration contains the MD5 has sum of the field value, typically 
    /// used for images and video files when working offline.
    /// </summary>
    MD5_Hash = 27,

    /// <summary>
    /// This enumeration contains the Field type for offline operation.
    /// </summary>
    Field_Type = 28,

    /// <summary>
    /// This enumeration contains the Field type for quiz value
    /// </summary>
    Quiz_Value = 29,

    /// <summary>
    /// This enumeration contains the Field type for quiz answer
    /// </summary>
    Quiz_Answer = 30,

    /// <summary>
    /// This enumeration contains the Field type for value label.
    /// </summary>
    Value_Label = 31,

    /// <summary>
    /// This enumeration defines the field input width as a percentage
    /// </summary>
    Field_Value_Column_Width = 32,

    /// <summary>
    /// This enumeration defines the field input width as a percentage
    /// </summary>
    Field_Value_Legend = 33,

    /// <summary>
    /// This enumeration contains the Field type for value label.
    /// </summary>
    Computed_Formula = 34,

  }//END CLASS

}//END namespace