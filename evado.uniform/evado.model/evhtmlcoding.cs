/***************************************************************************************
 * <copyright file="HtmlCoding.cs" company="EVADO HOLDING PTY. LTD.">
 *     
 *      Copyright (c) 2002 - 2022 EVADO HOLDING PTY. LTD.  All rights reserved.
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
 *  This class contains the HtmlCoding  methods.
 *
 ****************************************************************************************/

using System;
using System.Data; 
using System.Collections; 
using System.Collections.Generic;

//Evado. namespace references.
using Evado.Model ;


namespace Evado.Model 
{
  /// <summary>
  /// The Site Properties object contains static initialisation properties used by 
  /// the web site.
  /// </summary>
  public class EvHtmlCoding
  {
   
    // =====================================================================================
    /// <summary>
    /// This class encodes the content from an XML file.
    /// 
    /// </summary>
    /// <param name="HtmlString">string: (Mandatory) The file name.</param>
    /// <returns>string: an encode html string</returns>
    /// <remarks>
    /// This method consists of the following step:
    /// 
    /// 1. Replace some characters of html string with other encoding characters
    /// </remarks>
    // -------------------------------------------------------------------------------------
    public static string Encode( string HtmlString ) 
    {
      //
      // Encoding Function and formating the Html string
      //
      HtmlString = HtmlString.Replace( "<", "&lt;" );
      HtmlString = HtmlString.Replace( ">", "&gt;" );
      HtmlString = HtmlString.Replace( "&", "&amp;" );
      HtmlString = HtmlString.Replace( "'", "&sqt;" );

      // 
      // Return the Site object
      // 
      return HtmlString;

    }//END Encode method.

    // =====================================================================================
    /// <summary>
    /// This class decodes the content from an XML file.
    /// 
    /// </summary>
    /// <param name="String">string: (Mandatory) The file name.</param>
    /// <returns>string: an decode string</returns>
    /// <remarks>
    /// This method consists of the following step:
    /// 
    /// 1. Replace some encoding characters with other understanding characters
    /// </remarks>
    // -------------------------------------------------------------------------------------
    public static string Decode( string String ) 
    {
      // 
      // Decoding function and formating the string
      // 
      string HtmlString = String.Replace( "&amp;", "&" );
      HtmlString = HtmlString.Replace( "&gt;", ">" );
      HtmlString = HtmlString.Replace( "& gt;", ">" );
      HtmlString = HtmlString.Replace( "&lt;", "<" );
      HtmlString = HtmlString.Replace( "& lt;", "<" );
      HtmlString = HtmlString.Replace( "&sqt;", "'"  );

      // 
      // Return the Site object
      // 
      return HtmlString;

    }//END Decode method.
   
    // =====================================================================================
    /// <summary>
    /// This class adds cData to the content from an XML file.
    /// 
    /// </summary>
    /// <param name="HtmlString">string: (Mandatory) The html content.</param>
    /// <returns>string: a site object</returns>
    /// <remarks>
    /// This method consists of the following step:
    /// 
    /// 1. Add cData character into the html string
    /// </remarks>
    // -------------------------------------------------------------------------------------
    public static string cData( string HtmlString ) 
    {
      //
      // Encoding Function
      //
      HtmlString = "<![CDATA[" + HtmlString + "]]>";
      // 
      // Return the Site object
      // 
      return HtmlString;

    }//END cData method.
   
    // =====================================================================================
    /// <summary>
    /// This class validates the encoding of the content from an XML file.
    /// 
    /// </summary>
    /// <param name="HtmlString">string: (Mandatory) The html content.</param>
    /// <returns>boolean: true if the file is encoded</returns>
    /// <remarks>
    /// This method consists of the following steps
    /// 
    /// 1. Create a string array of character for testing
    /// 
    /// 2. If the character in HtmlString consists of the test character,
    /// return true
    /// </remarks>
    // -------------------------------------------------------------------------------------
    public static bool CheckEncoding( string HtmlString ) 
    {
      char [] TestChar = {'<', '>', '&'};
      //
      // Check for values.
      //
      if ( HtmlString.IndexOfAny( TestChar ) > -1 )
      {
        return true ;
      }
      // 
      // Return the false
      // 
      return false;

    }//END CheckEncoding method.

  }//END HtmlCoding class

}//END namespace nvision.BioTech.Bll 