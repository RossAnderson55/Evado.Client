/***************************************************************************************
 * <copyright file="webclinical\amdin\PersistViewStateToFileSystem.cs" company="EVADO HOLDING PTY. LTD.">
 *     
 *      Copyright (c) 2011 - 2025 EVADO HOLDING PTY. LTD.  All rights reserved.
 *     
 *      The use and distribution terms for this software are contained in the file
 *      named license.txt, which can be found in the root of this distribution.
 *      By using this software in any fashion, you are agreeing to be bound by the
 *      terms of this license.
 *     
 *      You must not remove this notice, or any other, from this software.
 *     
 * </copyright>
 * 
 ****************************************************************************************/

using System;
using System.IO;
using System.Collections.Specialized;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using Evado.Model;

namespace Evado.UniForm.Web
{
  /// <summary>
  /// This class redirects the default viewstate storage to a data file.
  /// </summary>
  public class EvPersistentPageState : System.Web.UI.Page
  {

    // ==================================================================================== 
    /// <summary>
    /// Description:
    ///  This method stores the page's view state to a server object.
    /// </summary>
    /// <param name="viewState"></param>
    // ------------------------------------------------------------------------------------

    protected override void SavePageStateToPersistenceMedium( object viewState )
    {
      try
      {
        // serialize the view state into a base-64 encoded string
        LosFormatter los = new LosFormatter();
        StringWriter writer = new StringWriter();
        los.Serialize( writer, viewState );

        //
        // save the string to disk
        //
        StreamWriter sw = File.CreateText( this.ViewStateFilePath );
        sw.Write( writer.ToString() );
        sw.Close();
      }
      catch ( Exception Ex )
      {
        Evado.UniForm.Model.EvApplicationEvents.LogError( Evado.Model.EvStatics.getException( Ex ) );
      }
    }//END SavePageStateToPersistenceMedium Method

    // ==================================================================================== 
    /// <summary>
    /// Description:
    ///  This method retrieves teh the page's view state from a server object.
    /// </summary>
    // ------------------------------------------------------------------------------------
    protected override object LoadPageStateFromPersistenceMedium( )
    {
      try
      {
        // determine the file to access
        if ( !File.Exists( ViewStateFilePath ) )
          return null;
        else
        {
          // open the file
          StreamReader sr = File.OpenText( ViewStateFilePath );
          string viewStateString = sr.ReadToEnd();
          sr.Close();
          // deserialize the string
          LosFormatter los = new LosFormatter();
          return los.Deserialize( viewStateString );
        }
      }
      catch ( Exception Ex )
      {
        EvEventLog.LogPageError ( this, Evado.Model.EvStatics.getException ( Ex ) );

        return null;
      }
    }//END LoadPageStateFromPersistenceMedium Method

    // ==================================================================================== 
    /// <summary>
    /// Description:
    ///  This method retrieves the the page's view state from a server object.
    /// </summary>
    /// <returns>The file page name asa string</returns>
    // ------------------------------------------------------------------------------------
    public string ViewStateFilePath
    {
      get
      {
        try
        {
          ///
          /// Get names of  all keys into a string array.
          /// 
          string Key, Value;
          NameValueCollection coll = Request.QueryString;
          String[ ] aKeys = coll.AllKeys;
          String stQueryValues = "_";
          for ( int loop1 = 0; loop1 < aKeys.Length; loop1++ )
          {
            Key = Server.HtmlEncode( aKeys[ loop1 ] ).ToString( );
            String[ ] aValues = coll.GetValues( aKeys[ loop1 ] );
            Value = Server.HtmlEncode( aValues[ 0 ] ).ToString( );
            if ( Key.ToLower( ) == "tid"
              || Key.ToLower( ) == "sid"
              || Key.ToLower( ) == "rid"
              || Key.ToLower( ) == "oid"
              || Key.ToLower( ) == "vid"
              || Key.ToLower( ) == "pid"
              || Key.ToLower( ) == "guid"
              || Key.ToLower( ) == "uid" )
            {
              stQueryValues += Value;
            }
          }

          stQueryValues = stQueryValues.Replace( "%2", String.Empty );
          stQueryValues = stQueryValues.Replace( "/", String.Empty );
          stQueryValues = stQueryValues.Replace( "aspx", String.Empty );
          stQueryValues = stQueryValues.Replace( "?", "_" );
          stQueryValues = stQueryValues.Replace( "&", String.Empty );
          stQueryValues = stQueryValues.Replace( "=", String.Empty );
          stQueryValues = stQueryValues.Replace( @".", String.Empty );
          stQueryValues = stQueryValues.Replace( "-", String.Empty );

          if ( stQueryValues.Length > 20 )
          {
            //stQueryValues = stQueryValues.Substring( 0, 20 );
          }
          //stQueryValues = String.Empty;

          string folderName =
            Path.Combine( Request.PhysicalApplicationPath,
            "PersistedViewState" );

          DirectoryInfo di = new DirectoryInfo( folderName );

          if ( di.Exists == false )
          {
            di.Create( );
          }

          // 
          // Try to create the directory.
          // 
          di.Create( );



          string fileName = Session.SessionID + "_" +
            Path.GetFileNameWithoutExtension( Request.Path ) + stQueryValues + ".vs";
          fileName = fileName.Replace( "/", "_" );
          return Path.Combine( folderName, fileName ).ToLower( );

        }
        catch ( Exception Ex )
        {
          EvEventLog.LogError( Ex.ToString( ) );

          throw ( Ex );

        }//END try-catch

      }//END get

    }//END ViewStateFilePath Method

    #region Temporary Directory methods.

    //***********************************************************************************
    #endregion

  }//END class
}//END name space

