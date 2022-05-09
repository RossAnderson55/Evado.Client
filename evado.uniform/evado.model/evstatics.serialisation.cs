/***************************************************************************************
 * <copyright file="CommonMethods.cs" company="EVADO HOLDING PTY. LTD.">
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
 *  This class contains the CommonMethods data object.
 *
 ****************************************************************************************/

using System;
using System.Collections; 
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.XPath;
using System.Xml.Serialization;
using System.IO;
using Newtonsoft.Json;

namespace Evado.Model
{
  public partial class EvStatics
  {
    
    // =====================================================================================
    /// <summary>
    /// BusinessCity:
    ///  Serialises the generic type object into an Xml String object.
    /// 
    /// </summary>
    // -------------------------------------------------------------------------------------
    public static string SerialiseXmlObject<T>( T FormField )
    {
      // 
      // Initialise the methods variables and objects.
      // 
      string xmlDataObject = String.Empty;

      // 
      // Serialise the trial object into a Xml text object.
      // 
      XmlSerializer serializer = new XmlSerializer( typeof( T ) );
      StringWriter writer = new StringWriter( );
      try
      {
        // 
        // Write out the site class to the organisation writer.
        // 
        serializer.Serialize( writer, FormField );

        // 
        // Output the xml object into a text xml dataObject. 
        // 
        xmlDataObject = writer.ToString( );

        // 
        // Close the organisation stream.
        // 
        writer.Close( );

      }
      catch( Exception Ex )
      {
        xmlDataObject = Evado.Model.EvStatics.getException ( Ex );
      }

      writer.Close( );
      // 
      // Return the xml text object
      // 
      return xmlDataObject;

    }//END SerialiseObject method.

    // =====================================================================================
    /// <summary>
    /// DeserialiseObject method
    /// 
    /// BusinessCity:
    /// Deserialises an Xml object into a generic type.
    /// 
    /// </summary>
    /// <param name="XmlObject">Validation rules as a Xml object</param>
    // -------------------------------------------------------------------------------------
    public static T DeserialiseXmlObject<T>( string XmlObject )
    {
      // 
      // Initialise the methods variables and objects.
      // 
      T dataObject = default( T );

      // 
      // If a empty field then return an empty object.
      // 
      if ( XmlObject == String.Empty )
      {
        return dataObject;
      }

      // 
      // Deserialise the xml data into a local data object
      // 
      XmlSerializer serializer = new XmlSerializer( typeof( T ) );
      TextReader textReader = new StringReader( XmlObject );
      try
      {
        // 
        // Deserialise the xml object into the type object.
        // 
        dataObject = (T)serializer.Deserialize( textReader );

        // 
        // Close the organisation stream.
        // 
        textReader.Close( );
      }
      catch
      {
        textReader.Close( );
        throw;
      }

      // 
      // Return the validation object.
      // 
      return dataObject;

    }//END DeserialiseObject method.


    // =====================================================================================
    /// <summary>
    /// BusinessCity:
    ///  Serialises the generic type object into an Xml String object.
    /// 
    /// </summary>
    // -------------------------------------------------------------------------------------
    public static string SerialiseJsaonObject<T> ( T FormField )
    {
      // 
      // Initialise the methods variables and objects.
      // 
      string jsonDataObject = String.Empty;
      JsonSerializerSettings jsonSettings = new JsonSerializerSettings
      {
        NullValueHandling = NullValueHandling.Ignore
      };

      try
      {
        // 
        // Output the xml object into a text xml dataObject. 
        // 
        jsonDataObject = JsonConvert.SerializeObject ( FormField, Newtonsoft.Json.Formatting.Indented, jsonSettings );

      }
      catch
      {
        throw;
      }

      // 
      // Return the xml text object
      // 
      return jsonDataObject;

    }//END SerialiseJsonObject method.

    // =====================================================================================
    /// <summary>
    /// DeserialiseObject method
    /// 
    /// BusinessCity:
    /// Deserialises an Xml object into a generic type.
    /// 
    /// </summary>
    /// <param name="JsonObject">Validation rules as a Xml object</param>
    // -------------------------------------------------------------------------------------
    public static T DeserialiseJsonObject<T> ( string JsonObject )
    {
      // 
      // Initialise the methods variables and objects.
      // 
      T dataObject = default ( T );

      // 
      // If a empty field then return an empty object.
      // 
      if ( JsonObject == String.Empty )
      {
        return dataObject;
      }

      // 
      // Deserialise the xml data into a local data object
      // 
      try
      {
        // 
        // Deserialise the xml object into the type object.
        // 
        dataObject = (T) JsonConvert.DeserializeObject<T> ( JsonObject );
      }
      catch
      {
        throw;
      }

      // 
      // Return the validation object.
      // 
      return dataObject;

    }//END DeserialiseObject method.

  }//END CommonMethods class

}//END Model namespace
