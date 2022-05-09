/***************************************************************************************
 * <copyright file="statics.cs" company="EVADO HOLDING PTY. LTD.">
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
 *  This class contains the static data objects.
 *
 ****************************************************************************************/

using System;
using System.Collections;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Collections.Generic;

namespace Evado.Model
{

  /// <summary>
  /// This class provides statics enumeration for use across the application.
  /// </summary>
  public partial class EvStatics
  {

    #region Enumeration Methods


      //  =============================================================
      /// <summary>
      /// This class generates a list with the enumeration elements.
      /// </summary>
      /// <typeparam name="T">Type of the enumeration</typeparam>
      /// <returns>list: a list of enumeration elements</returns>
      /// <remarks>
      /// This method consists of the following steps:
      /// 
      /// 1. Create the return list of Enumeration Elements
      /// 
      /// 2. Initialize a list of Enumeration Type Name
      /// 
      /// 3. Loop through the list of Enumeration Type Name and add the 
      /// name to the return element list
      /// </remarks>
      //  --------------------------------------------------------------
      public static List<T> getElementList<T>( )
      {
        //
        // Create a return list of enumeration elements
        //
        List<T> elements = new List<T>( );

        //
        // Initialize a name list of enumeration type
        //
        String[ ] names = Enum.GetNames( typeof( T ) );

        //
        // Loop through the string array and add the string value to the list 
        // as well as replace an empty string with "_"
        //
        foreach ( String name in names )
        {
          elements.Add( parseEnumValue<T>( name ) );
        }

        //
        // return a list of enumeration elements
        //
        return elements;
      }//END getElementList method


      //  =============================================================
      /// <summary>
      /// This class generated a list with the enumeration elements.
      /// </summary>
      /// <typeparam name="T">Type of the enumeration</typeparam>
      /// <returns>List: a string list of enumeration elements</returns>
      /// <remarks>
      /// This method consists of the following steps:
      /// 
      /// 1. Create a return string list of enumeration elements
      /// 
      /// 2. Initialize a name list of enumeration type
      /// 
      /// 3. Loop through the name list and add name to the return 
      /// string list of enumeration elements
      /// </remarks>
      //  --------------------------------------------------------------
      public static List<String> getElementAsStringList<T> ( )
      {
        //
        // Create a return list of enumeration elements
        //
        List<String> elements = new List<String>( );

        //
        // Initialize a name list of enumeration type
        //
        String [ ] names = Enum.GetNames( typeof( T ) );

        //
        // Loop through the string array and add the name to the element list
        //
        foreach ( String name in names )
        {
          elements.Add(  name );
        }

        //
        // Return a string list of enumeration elements
        //
        return elements;
      }// END getElementAsStringList method

      //  =========================================================================================
      /// <summary>
      /// This class returns the representation of the value inside an Enumeration.
      /// If the string contains blank spaces, it replaces it with a _ character.
      /// 
      /// Author: Andres Castano
      /// Date: 7 Dic 2009
      /// </summary>
      /// <typeparam name="T">Type of the enumeration.</typeparam>
      /// <param name="value">String: String representation of the enumeration item.</param>
      /// <returns>Type of the enumeration</returns>
      /// <remarks>
      /// This class consists of the following steps:
      /// 
      /// 1. Validate the value string: if value is empty get returned "Null"
      /// 
      /// 2. Replace a blank character in value string with a "_" character
      /// 
      /// 3. Convert the value string into an enumeration type object
      /// </remarks>
      //  ------------------------------------------------------------------------------------------
      public static T parseEnumValue<T>( String value )
      {
        //
        // Set the string value to "null" if it does not exist
        //
        if ( value.Trim( ) == String.Empty )
        {
          value = "Null";
        }

        value = value.Trim( ).Replace( " ", "_" );

        //
        // Return Type of the enumeration
        //
        try
        {
          return (T) Enum.Parse ( typeof ( T ), value );
        }
        catch {
          return default ( T );
        }

      }//END parseEnumValue method
      /*
            //  =========================================================================================
            /// <summary>
            /// This class returns the representation of the integer value inside an Enumeration.
            /// 
            /// Author: Ross Anderson
            /// Date: 7 Dec 2009
            /// </summary>
            /// <typeparam name="T">Type of the enumeration.</typeparam>
            /// <param name="value">int: an integer value inside an enumeration</param>
            /// <returns>String: a string of integer value inside an enumeration</returns>
            /// <remarks>
            /// This class consists of the following steps:
            /// 
            /// 1. Create a name list of enumValue elements
            /// 
            /// 2. Loop through the list name
            /// 
            /// 3. If value exists, return the names of value
            /// 
            /// 4. If not get returned empty string.
            /// </remarks>
            //  ------------------------------------------------------------------------------------------
            public static T parseEnumValue<T> ( int value )
            {
              //
              // Create a name list of EnumValue elements
              //
              Array values = Enum.GetValues ( typeof ( T ) );
              //
              // Loop through the list and return a string value if the value exists
              //
              for ( int index = 0; index < values.Length; index++ )
              {
                if ( index == value )
                {
                  return Evado.Model.EvStatics.parseEnumValue<T> ( names [ index ] );
                }
              }

              return Evado.Model.EvStatics.parseEnumValue<T> ( names [ 0 ] );
            }//END getEnumValue method
       * 
            //  =========================================================================================
            /// <summary>
            /// This class returns the representation of the integer value inside an Enumeration.
            /// 
            /// Author: Ross Anderson
            /// Date: 7 Dec 2009
            /// </summary>
            /// <typeparam name="T">Type of the enumeration.</typeparam>
            /// <param name="value">int: an integer value inside an enumeration</param>
            /// <returns>String: a string of integer value inside an enumeration</returns>
            /// <remarks>
            /// This class consists of the following steps:
            /// 
            /// 1. Create a name list of enumValue elements
            /// 
            /// 2. Loop through the list name
            /// 
            /// 3. If value exists, return the names of value
            /// 
            /// 4. If not get returned empty string.
            /// </remarks>
            //  ------------------------------------------------------------------------------------------
            public static String getStringOfEnumValue<T>( int value )
            {
              //
              // Create a name list of EnumValue elements
              //
              String[ ] names = Enum.GetNames( typeof( T ) );

              //
              // Loop through the list and return a string value if the value exists
              //
              for ( int index = 0; index < names.Length; index++ )
              {
                if ( index == value )
                {
                  return names[ index ];
                }

              }
              return String.Empty;

            }//END getEnumValue method

      */

      //  =================================================================================
      /// <summary>
      /// This class try to parse the enum value.
      /// 
      /// Author: Andres Castano
      /// Date: 7 Dic 2009
      /// </summary>
      /// <typeparam name="T">Type of the enumeration.</typeparam>
      /// <param name="value">String: String representation of the enumeration item.</param>
      /// <param name="t"> Output parameter where the parsing result will be stored.</param>
      /// <returns>true if the parse was successful</returns>
      /// <remarks>
      /// This class consists of the following steps:
      /// 
      /// 1. Try parse value inside the Enumeration and return true
      /// 
      /// 2. If fail, log exception and return false
      /// 
      /// </remarks>
      //  ---------------------------------------------------------------------------------
      public static bool tryParseEnumValue<T>( String value, out T t )
      {
        //
        // Return true if parsing the string value was successful
        // or false if parsing the string value was unsuccessful
        //
        try
        {
          t = parseEnumValue<T>( value );
          return true;
        }
        catch ( Exception e )
        {
          String msg = e.Message;
          t = default( T );
          return false;
        }
      }//END tryParseEnumValue method

      //  ==============================================================================================
      /// <summary>
      /// This class returns the list of the string representing the elements of the enumeration.
      /// If the elements contains a _ it replaces it by an empty space.
      /// 
      /// Author: Andres Castano
      /// Date: 7 Dic 2009
      /// </summary>
      /// <param name="enumType">Type: the type of enumeration</param>
      /// <returns>ArrayList: a list of string representing the elements of the enumeration</returns>
      /// <remarks>
      /// This class consists of the following step:
      /// 
      /// 1. Return a list of string representing the elements of the enumeration
      /// </remarks>
      //  ----------------------------------------------------------------------------------------------
      public static ArrayList getEnumElements( Type enumType )
      {

        return getEnumElements( enumType, false );

      }//END getEnumElements method


      //  ==============================================================================================
      /// <summary>
      /// This class returns the list of the string representing the elements of the enumeration.
      /// If the elements contains a _ it replaces it by an empty space.
      /// 
      /// Author: Andres Castano
      /// Date: 7 Dic 2009
      /// </summary>
      /// <param name="enumType">Type: the type of enumeration</param>
      /// <param name="removeNull">bool: If the enum has null, it does not get returned on the list.</param>
      /// <returns>ArrayList: the list of string representing the elements of the enumeration.</returns>
      /// <remarks>
      /// This class consists of the following steps:
      /// 
      /// 1. Initialize a return list
      /// 
      /// 2. Create a string list of enumType elements
      /// 
      /// 3. Loop through the list of enumeration type
      /// 
      /// 4. If the element contains "null" character and is not Null,
      /// add the return list with empty string
      /// 
      /// 5. if not, replace a "_" character with a blank character 
      /// and add the element into the return list
      /// 
      /// </remarks>
      //  ----------------------------------------------------------------------------------------------
      public static ArrayList getEnumElements( Type enumType, bool removeNull )
      {
        //
        // Initialize a return list
        //
        ArrayList retList = new ArrayList( );
        
        //
        // Create a string list of enumeration type
        //
        String[ ] types = Enum.GetNames( enumType );

        //
        // Loop through the list of enumeration type
        //
        for ( int index = 0; index < types.Length; index++ )
        {
          //
          // Add the element to the list if it contains "null" character and is not null
          //
          if ( types[ index ].ToLower( ).Equals( "null" ) )
          {
            if ( removeNull == false )
            {
              retList.Add( String.Empty );
            }

          }
          else
          {
            //
            // Add element to the list by replacing a blank element with a "_" character 
            // if it does not contain "null"
            //
            retList.Add( types[ index ].Replace( "_", " " ) );
          }

        }

        //
        // Return a list of string representing the elements of the enumeration
        //
        return retList;

      }//END getEnumElements method

      //  ==============================================================================================
      /// <summary>
      /// This class obtains the selection option from the values of the enumeration
      /// </summary>
      /// <param name="enumType">Type: the type of enumeration</param>
      /// <param name="isSelectionList">true if it is a selection list</param>
      /// <returns>List: an option list</returns>
      /// <remarks>
      /// This class consists of the following steps:
      /// 
      /// 1. Create a value list of enumType elements
      /// 
      /// 2. Initialize a return option list
      /// 
      /// 3. If the element is selected, add the element to the option list
      /// 
      /// 4. Loop through the value list
      /// 
      /// 5. if the value does not consist of "null" character, 
      /// replace a "_" character with a blank character 
      /// Add value to the option list
      /// 
      /// </remarks>
      //  ----------------------------------------------------------------------------------------------
      public static List<Evado.Model.EvOption> getOptionValueFromEnum( Type enumType, bool isSelectionList )
      {
        //
        // Create a value list of enumType elements
        //
        Array values = Enum.GetValues( enumType );

        //
        // Initialize a return option list
        //
        List<Evado.Model.EvOption> options = new List<Evado.Model.EvOption>( );

        //
        // Add selection value to option list if it is selected
        //
        if ( isSelectionList == true )
        {
          options.Add( new Evado.Model.EvOption ( ) );
        }

        //
        // Loop through the values array and add value to the option list
        // if the value does not contain "null" character
        //
        foreach ( object value in values )
        {
          string name = value.ToString( );
          int iValue = (int) value;
          if ( name.ToLower( ) != "null" )
          {
            options.Add( new Evado.Model.EvOption ( iValue.ToString( ), name.Replace( "_", " " ) ) );
          }

        }

        //
        // Return a list of selection options
        //
        return options;
      }

      //  ==============================================================================================
      /// <summary>
      /// This class obtains a list of selection options made of the names of the enumeration.
      /// </summary>
      /// <param name="enumType">Type: the type of enumeration</param>
      /// <param name="isSelectionList">Boolean: true if the selection list</param>
      /// <returns>List: a list of selection options</returns>
      /// <remarks>
      /// This class consists of the following steps:
      /// 
      /// 1. Create a list of enumType elements
      /// 
      /// 2. Initialize the return option list
      /// 
      /// 3. Loop through the list of enumType elements
      /// 
      /// 4. If name element is empty and not selected, continue
      /// 
      /// 5. If name contains "all" character, continue
      /// 
      /// 6. Replace a blank character with a "_" character 
      /// 
      /// 7. Add the name element to the return option list
      /// 
      /// </remarks>
      //  ------------------------------------------------------------------------------------------------
      public static List<Evado.Model.EvOption> getOptionsFromEnum( Type enumType, bool isSelectionList )
      {
        //
        // Create a list of enumType elements.
        //
        ArrayList names = getEnumElements( enumType );

        //
        // Initialise the return option list
        //
        List<Evado.Model.EvOption> options = new List<Evado.Model.EvOption>( );

        //
        // If it is not selection list, then dont add the empty name to the list.
        //
        if (  isSelectionList == true )
        {
          options.Add ( new Evado.Model.EvOption ( ) ); 
        }

        //
        // Loop through the name in the array list
        //
        foreach ( String name in names )
        {
          //
          // If it is not selection list, then dont add the empty name to the list.
          //
          if ( name == String.Empty )
          {
            continue;
          }

          //
          // If it is not selection list, then dont add the empty name to the list.
          //
          if ( name.ToLower() == "all" )
          {
            continue;
          }

          //
          // Add the name to the list of selection options
          //
          options.Add( new Evado.Model.EvOption ( name.Replace( " ", "_" ), name ) );
        }

        //
        // Return a list of selection options
        //
        return options;

      }//End getOptionsFromEnum method

      //  ==============================================================================================
      /// <summary>
      /// This class obtains a list of selection options made of the names of the enumeration.
      /// </summary>
      /// <param name="enumObject">Object: an enumeration object</param>
      /// <returns>Object: a selection option object</returns>
      /// <remarks>
      /// This class consists of the following steps:
      /// 
      /// 1. Create a string name of enumObject
      /// 
      /// 2. Initialize an option object with a replacing of "_" character with a blank character
      /// </remarks>
      //  ------------------------------------------------------------------------------------------------
      public static Evado.Model.EvOption getOption( Object enumObject )
      {
        //
        // Create a string name of enumeObject
        //
        String name = enumObject.ToString();

        //
        // Initialize a return option object
        //
        Evado.Model.EvOption option = new Evado.Model.EvOption ( name, name.Replace( "_", " " ) );

        //
        // Return a selection option object.
        //
        return option;

      }//END getEvOption method

    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

  } // Close Statics class

} // Close namespace Evado.Model
