/***************************************************************************************
 * <copyright file="Evado.UniForm.Model\Page.cs" company="EVADO HOLDING PTY. LTD.">
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
  /// This enumerated lisd defines the page column code value.s
  /// </summary>
  public enum EuPageColumnCodes
  {
    /// <summary>
    /// This is the default enumeration, single column for all groups.
    /// </summary>
    Body = 0,

    /// <summary>
    /// This enumeration defines a two column page with a left gutter and main body.
    /// </summary>
    Left = 1,

    /// <summary>
    /// This enumeration defines a two column page with a main body and right gutter.
    /// </summary>
    Right = 2,

  }

  //  =================================================================================
  /// <summary>
  /// This enumeration list defines the parameter that can be passed the client.
  /// </summary>
  //  ---------------------------------------------------------------------------------
  public enum EuPageParameters
  {
    /// <summary>
    /// This enumeration defines the null or empty field.
    /// </summary>
    Null,

    /// <summary>
    /// This enumeratieon defines the left column width parameter value. The default width is 0px.
    /// </summary>
    Left_Column_Width,

    /// <summary>
    /// This enumeratieon defines the page column setting. The default is a single column.
    /// </summary>
    Right_Column_Width,

    /// <summary>
    /// This enumeration defines will display the page groups at tabs for all groups that are not in the header or columns.
    /// </summary>
    Display_Groups_As_Panels,

    /// <summary>
    /// This enumeration defines the anonymouse page access mode, True indicates active False indicates inactive, the default is False.
    /// When anonymous access is active, the Exit EuCommand is hidden, the Page History is hidden and Page EuCommands are hidden.
    /// </summary>
    Anonyous_Page_Access,

    /// <summary>
    /// This enumerated value defines the page background color.
    /// </summary>
    Page_Background,

    /// <summary>
    /// This enumerated value defines the page text color.
    /// </summary>
    Page_Color,

    /// <summary>
    /// This enumerated value defines the page default font.
    /// </summary>
    Page_Font,

    /// <summary>
    /// This enumerated value defines the Page font size.
    /// </summary>
    Page_Font_Size,

    /// <summary>
    /// This enumerated value defines the page groups access as separate panels.
    /// </summary>
    Group_Panel_Access,

    /// <summary>
    /// This enumerated value defines the page video meeting url parameter.
    /// </summary>
    Video_Meeting_Url,

    /// <summary>
    /// This enumerated value defines the page video meeting Status parameter.
    /// </summary>
    Video_Meeting_Status,
  }
  //  =================================================================================
  /// <summary>
  /// This enumeration list defines the parameter that can be passed the client.
  /// </summary>
  //  ---------------------------------------------------------------------------------
  public enum EuPageImageUrls
  {
    /// <summary>
    /// This enumeration defines the null or empty field.
    /// </summary>
    Null,

    /// <summary>
    /// This enumeration defines the URL to the image to be displayed as part of the EuCommands title link.
    /// This parameter is normally used with tiled groups and can be defined at a page or group level.
    /// </summary>
    Image0_Url,

    /// <summary>
    /// This enumeration defines the URL to the image to be displayed as part of the EuCommands title link.
    /// This parameter is normally used with tiled groups and can be defined at a page or group level.
    /// </summary>
    Image1_Url,

    /// <summary>
    /// This enumeration defines the URL to the image to be displayed as part of the EuCommands title link.
    /// This parameter is normally used with tiled groups and can be defined at a page or group level.
    /// </summary>
    Image2_Url,

    /// <summary>
    /// This enumeration defines the URL to the image to be displayed as part of the EuCommands title link.
    /// This parameter is normally used with tiled groups and can be defined at a page or group level.
    /// </summary>
    Image3_Url,

    /// <summary>
    /// This enumeration defines the URL to the image to be displayed as part of the EuCommands title link.
    /// This parameter is normally used with tiled groups and can be defined at a page or group level.
    /// </summary>
    Image4_Url,

    /// <summary>
    /// This enumeration defines the URL to the image to be displayed as part of the EuCommands title link.
    /// This parameter is normally used with tiled groups and can be defined at a page or group level.
    /// </summary>
    Image5_Url,

    /// <summary>
    /// This enumeration defines the URL to the image to be displayed as part of the EuCommands title link.
    /// This parameter is normally used with tiled groups and can be defined at a page or group level.
    /// </summary>
    Image6_Url,

    /// <summary>
    /// This enumeration defines the URL to the image to be displayed as part of the EuCommands title link.
    /// This parameter is normally used with tiled groups and can be defined at a page or group level.
    /// </summary>
    Image7_Url,

    /// <summary>
    /// This enumeration defines the URL to the image to be displayed as part of the EuCommands title link.
    /// This parameter is normally used with tiled groups and can be defined at a page or group level.
    /// </summary>
    Image8_Url,

    /// <summary>
    /// This enumeration defines the URL to the image to be displayed as part of the EuCommands title link.
    /// This parameter is normally used with tiled groups and can be defined at a page or group level.
    /// </summary>
    Image9_Url,
  }

}//END namespace