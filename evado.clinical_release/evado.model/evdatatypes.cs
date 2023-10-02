/***************************************************************************************
 * <copyright file="Evado.UniForm.Model\AbstractedPage.cs" company="EVADO HOLDING PTY. LTD.">
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
 *  This class contains the AbstractedPage data object.
 *
 ****************************************************************************************/
using System;

namespace Evado.Model
{
  /// <summary>
  /// This class contains the the pain and object. These page objects are rendered 
  /// into page fields on the client device.
  /// </summary>
  [Serializable]
  public enum EvDataTypes
  {
    /// <summary>
    /// This option is used to define non selected or null entry.
    /// </summary>
    Null = 0,  // json enumeration: 0

    /// <summary>
    /// This option defines the  the text page object, this object allows a user to 
    /// enter text content.
    /// </summary>
    Read_Only_Text = 1,  // json enumeration: 1

    /// <summary>
    /// This option defines the  the text page object, this object allows a user to 
    /// enter text content.
    /// </summary>
    Text = 2,  // json enumeration: 2

    /// <summary>
    /// This option defines the  the text page object, this object allows a user to 
    /// enter free text content.
    /// </summary>
    Free_Text = 3,  // json enumeration: 3

    /// <summary>
    /// This options defines the  boolean page object, this object allows the user 
    /// to enter a yes/no tru/false value.
    /// </summary>
    Yes_No = 4,  // json enumeration: 4

    /// <summary>
    /// This options defines the  numeric page object, this object allows the user 
    /// to enter a numeric value, which is validated on entry prior to being passed 
    /// back to the web service.
    /// </summary>
    Numeric = 5,  // json enumeration: 5

    /// <summary>
    /// This options defines the date page object, this object allows the user 
    /// to enter a date value, which is validated on entry prior to being passed 
    /// back to the web service.
    /// </summary>
    Date = 6,  // json enumeration: 6

    /// <summary>
    /// This options defines the  time page object, this object allows the user 
    /// to enter time.
    /// </summary>
    Time = 7,  // json enumeration: 7

    /// <summary>
    /// This options defines the  selection page object, this object allows the user 
    /// to select an option list value.
    /// </summary>
    Selection_List = 8,  // json enumeration: 8

    /// <summary>
    /// This options defines the  radio button list page object, this object allows the user 
    /// to select an option list value.
    /// </summary>
    Radio_Button_List = 9,  // json enumeration: 9

    /// <summary>
    /// This options defines the  Check box list page object, this object allows the user 
    /// to select multiple option list values.
    /// </summary>
    Check_Box_List = 10, // json enumeration: 10

    /// <summary>
    /// This options defines the still photograph page object, this object allows the user
    /// to take a still photograph, which is displayed on the device and can be deleted.
    /// Tthe photograph is passed back to the web service as a bright object (image).
    /// </summary>
    Image = 11,  // json enumeration: 11

    /// <summary>
    /// This options defines the Video page object this object interfaces with the device's 
    ///  camera and enables the user to take a year though image which is then stored in the
    ///  page object. The video can then be saved back to the server via the web service.
    /// </summary>
    Sound = 12,  // json enumeration: 12

    /// <summary>
    /// This options defines the BarCode scanner.
    /// </summary>
    Bar_Code = 13,  // json enumeration: 13

    /// <summary>
    /// This options defines the text field where the text values are hidden fom the user.
    /// </summary>
    Hidden = 14,  // json enumeration: 14

    /// <summary>
    /// This options defines the table field where the data is layed out as a table.
    /// </summary>
    Table = 15,  // json enumeration: 15

    /// <summary>
    /// This options defines a text field where the data is an http link 
    /// </summary>
    Http_Link = 16,  // json enumeration: 16

    /// <summary>
    /// This options defines the audio page object this object interfaces with the device's 
    ///  microphone and enables the user to record sound, which is then stored in the
    ///  page object. The sound can then be saved back to the server via the web service.
    /// </summary>
    Video = 17,  // json enumeration: 17

    /// <summary>
    /// This options defines a currency field as a floating point 
    /// </summary>
    Currency = 18,  // json enumeration: 18

    /// <summary>
    /// This options defines a currency field as a floating point 
    /// </summary>
    Email_Address = 19,  // json enumeration: 19

    /// <summary>
    /// This options defines a telephone number
    /// </summary>
    Telephone_Number = 20,  // json enumeration: 20

    /// <summary>
    /// This options defines a peron's name field 
    /// </summary>
    Name = 21,  // json enumeration: 21

    /// <summary>
    /// This options defines an Address field consisting of the following sub-fields.
    /// Address 1, Address 2, Suburb or City, Region or state, Postcode or zip code 
    /// and Country.
    /// </summary>
    Address = 22,  // json enumeration: 22

    /// <summary>
    /// This options defines a signature field collected as a raster graphic
    /// </summary>
    Signature = 23,  // json enumeration: 23

    /// <summary>
    /// This options defines a computed field that executes a java script
    /// </summary>
    Computed_Field = 24,  // json enumeration: 24

    /// <summary>
    /// This options defines an anologe scale that is displayed horizontally beneath the field prompt and desctiption.
    /// </summary>
    Analogue_Scale = 25,  // json enumeration: 25

    /// <summary>
    /// This options define a radio button list displayed horizontally beneath the field prompt and desctiption.
    /// </summary>
    Horizontal_Radio_Buttons = 26,  // json enumeration: 26

    /// <summary>
    /// This options define a radio button list displayed horizontally beneath the field prompt and desctiption.
    /// </summary>
    Integer = 27,  // json enumeration: 27

    /// <summary>
    /// This options define a binary file field.
    /// </summary>
    Binary_File = 28,  // json enumeration: 28

    /// <summary>
    /// This enumeration define an integer range field. 
    /// </summary>
    Integer_Range = 29,  // json enumeration: 29

    /// <summary>
    /// This enumeration define a date range field. 
    /// </summary>
    Date_Range = 30,  // json enumeration: 30

    /// <summary>
    /// This enumeration define a float range field. 
    /// </summary>
    Float_Range = 31,  // json enumeration: 31

    /// <summary>
    /// This enumeration define a double range field. 
    /// </summary>
    Double_Range = 32,  // json enumeration: 32

    /// <summary>
    /// This enumeration define a HTML content field. 
    /// </summary>
    Html_Content = 33,

    /// <summary>
    /// This enumeration define a Raster graphic data field. 
    /// </summary>
    Raster_Image = 34, 

    /// <summary>
    /// This enumeration define a Bar Chart display field. 
    /// </summary>
    Password = 35,

    /// <summary>
    /// This enumeration define a boolean field. 
    /// </summary>
    Boolean = 36,

    /// <summary>
    /// This enumeration define a Bar Chart display field. 
    /// </summary>
    Bar_Chart = 37,

    /// <summary>
    /// This enumeration define a Line Chart display field. 
    /// </summary>
    Line_Chart = 38,

    /// <summary>
    /// This enumeration define a Pie Chart display field. 
    /// </summary>
    Pie_Chart = 39,

    /// <summary>
    /// This enumeration define a rotary, speedo chart display field. 
    /// </summary>
    Donut_Chart = 40,

    /// <summary>
    /// This enumeration define a stacked bar chart display field. 
    /// </summary>
    Stacked_Bar_Chart = 41,

    /// <summary>
    /// This enumeration define a streamed video field. 
    /// </summary>
    Streamed_Video = 42,

    /// <summary>
    /// This enumeration define external Image display field. 
    /// </summary>
    External_Image = 43,

    /// <summary>
    /// This enumeration define a User endorsement display field. 
    /// </summary>
    User_Endorsement = 45,

    /// <summary>
    /// This enumeration define a radio button list with an external selection source.
    /// </summary>
    External_RadioButton_List = 96,

    /// <summary>
    /// This enumeration define a checkbox list with an external selection source.
    /// </summary>
    External_CheckBox_List = 97,

    /// <summary>
    /// This enumeration define a selection list with an external selection source.
    /// </summary>
    External_Selection_List = 99,

    /// <summary>
    /// This enumeration define a special matrix, a table with readonly fields.
    /// </summary>
    Special_Matrix = 100,
    /// <summary>
    /// This enumeration define a special medication summary data type.
    /// </summary>
    Special_Medication_Summary = 101,

    /// <summary>
    /// This enumeration define a special demographics summary data type.
    /// </summary>
    Special_Subject_Demographics = 102,

    /// <summary>
    /// This enumeration define a special site name data type.
    /// </summary>
    Special_Subsitute_Data = 103,

    /// <summary>
    /// This enumeration define a special checokbox to query the field instruction content for 
    /// consent forms.
    /// </summary>
    Special_Query_Checkbox = 104,

    /// <summary>
    /// This enumeration define a special checokbox to YesNo the field instruction content for 
    /// consent forms.
    /// </summary>
    Special_Query_YesNo = 105,

    /// <summary>
    /// This enumeration define a special checokbox to YesNo the field instruction content for 
    /// consent forms.
    /// </summary>
    Special_Quiz_Radio_Buttons = 106,

    /// <summary>
    /// This enumeration define a special site document data type.
    /// </summary>
    Special_Document = 107,

    /// <summary>
    /// This enumeration define a special trial identifier data type.
    /// </summary>
    Special_Trial_Id = 108,

    /// <summary>
    /// This enumeration define a special trial Title data type.
    /// </summary>
    Special_Trial_Title = 109,

    /// <summary>
    /// This enumeration define a special trial protocol id data type.
    /// </summary>
    Special_Trial_Protocol_Id = 110,

    /// <summary>
    /// This enumeration define a special trial protocol id data type.
    /// </summary>
    Special_Trial_Principal_Investigator = 111,

    /// <summary>
    /// This enumeration define a special trial protocol id data type.
    /// </summary>
    Special_Trial_Sponsor = 112,

    /// <summary>
    /// This enumeration define a special trial site id data type.
    /// </summary>
    Special_Site_Id = 113,

    /// <summary>
    /// This enumeration define a special trial site name data type.
    /// </summary>
    Special_Site_Name = 114,

    /// <summary>
    /// This enumeration define a special trial site investigator data type.
    /// </summary>
    Special_Site_Investigator = 115,


    /// <summary>
    /// This options defines the date page object, this object allows the user 
    /// to enter a date value, which is validated on entry prior to being passed 
    /// back to the web service.
    /// </summary>
    Year = 116,  // json enumeration: 116

    /// <summary>
    /// This enumeration define a special external reference field data type.
    /// </summary>
    External_Field_Reference = 117,


  }//END Enumeration

}//END namespace