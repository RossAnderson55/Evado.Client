/***************************************************************************************
 * <copyright file="webclinical\EVADO HOLDING PTY. LTD.EventLog.cs" company="EVADO HOLDING PTY. LTD.">
 *     
 *      Copyright (c) 2011 - 2020 EVADO HOLDING PTY. LTD.  All rights reserved.
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
using System.Diagnostics;
using System.Configuration;

//Evado. namespace references.

namespace Evado.UniForm.Web
{
  /// <summary>
  /// Summary description for EventLog.
  /// </summary>
  public class EvEventLog
  {
    public static readonly string EventSource = ConfigurationManager.AppSettings[ "EventLogSource" ];

    //  ===========================================================================
    /// <summary>
    /// LogPageInformation to Log method
    /// 
    /// Description:
    /// Creates the Evado EvEvent Source. This requires administrator privileges
    /// because it needs to write to the registry have.
    /// 
    /// </summary>
    /// <returns>If the event source was created successfully true is returned, otherwise false.</returns>
    //  ---------------------------------------------------------------------------------
    public static void LogIllegalAccess(
      Evado.UniForm.Web.EvPersistentPageState This, 
      String UserCommonName )
    {
      string stEvent = "Illegal access attempt by " + UserCommonName
            + " at " + DateTime.Now.ToString( "dd MMM yyyy HH:mm:ss" );

      // 
      // If no connnection write to the local log.
      // 
      EvEventLog.WriteLog( stEvent, EventLogEntryType.Error );
    }


    //  ===========================================================================
    /// <summary>
    /// LogPageInformation to Log method
    /// 
    /// Description:
    /// Creates the Evado EvEvent Source. This requires administrator privileges
    /// because it needs to write to the registry have.
    /// 
    /// </summary>
    /// <returns>If the event source was created successfully true is returned, otherwise false.</returns>
    //  ---------------------------------------------------------------------------------
    public static void LogPageAction(
      Evado.UniForm.Web.EvPersistentPageState This, string EventContent )
    {
      string sEventContent = "PageUrl: " + This.Request.RawUrl
        + "\r\n\r\nUserId: " + This.User.Identity.Name
        + "\r\n\r\nDescription:\r\n" + EventContent;

      EvEventLog.WriteLog( sEventContent, EventLogEntryType.Error );
    }

    //  ===========================================================================
    /// <summary>
    /// LogPageInformation static method
    /// 
    /// Description:
    /// Creates the Evado EvEvent Source. This requires administrator privileges
    /// because it needs to write to the registry have.
    /// 
    /// </summary>
    /// <returns>If the event source was created successfully true is returned, otherwise false.</returns>
    //  ---------------------------------------------------------------------------------
    public static void LogPageInformation(
      Evado.UniForm.Web.EvPersistentPageState This, int EventId, string EventContent )
    {
      string sEventContent = "PageUrl: " + This.Request.RawUrl
        + "\r\n\r\nUserId: " + This.User.Identity.Name
        + "\r\n\r\nDescription:\r\n" + EventContent;

      EvEventLog.WriteLog( sEventContent, EventLogEntryType.Information );
    }

    //  ===========================================================================
    /// <summary>
    /// LogPageWarning method
    /// 
    /// Description:
    /// Creates the Evado EvEvent Source. This requires administrator privileges
    /// because it needs to write to the registry have.
    /// 
    /// </summary>
    /// <returns>If the event source was created successfully true is returned, otherwise false.</returns>
    //  ---------------------------------------------------------------------------------
    public static void LogPageWarning( Evado.UniForm.Web.EvPersistentPageState This, int EventId, string EventContent )
    {
      string sEventContent = "PageUrl: " + This.Request.RawUrl
        + "\r\n\r\nUserId: " + This.User.Identity.Name
        + "\r\n\r\nDescription:\r\n" + EventContent;

      EvEventLog.WriteLog( sEventContent, EventLogEntryType.Error );

    }//END LogPageWarning method

    //  ===========================================================================
    /// <summary>
    /// LogPageError method
    /// 
    /// Description:
    /// Creates the Evado EvEvent Source. This requires administrator privileges
    /// because it needs to write to the registry have.
    /// 
    /// </summary>
    /// <returns>If the event source was created successfully true is returned, otherwise false.</returns>
    //  ---------------------------------------------------------------------------------
    public static void LogPageError( Evado.UniForm.Web.EvPersistentPageState This, string EventContent )
    {
      string sEventContent = "PageUrl: " + This.Request.RawUrl
        + "\r\n\r\nUserId: " + This.User.Identity.Name
        + "\r\n\r\nDescription:\r\n" + EventContent;

      EvEventLog.WriteLog( sEventContent, EventLogEntryType.Error );

    }//END LogPageError page

    //  ===========================================================================
    /// <summary>
    /// LogEventError method
    /// 
    /// Description:
    /// Creates the Evado EvEvent Source. This requires administrator privileges
    /// because it needs to write to the registry have.
    /// 
    /// </summary>
    /// <returns>If the event source was created successfully true is returned, otherwise false.</returns>
    //  ---------------------------------------------------------------------------------
    public static void LogAction( string EventContent )
    {
      EvEventLog.WriteLog( EventContent, EventLogEntryType.Information );
    }


    //  ===========================================================================
    /// <summary>
    /// LogEventError method
    /// 
    /// Description:
    /// Creates the Evado EvEvent Source. This requires administrator privileges
    /// because it needs to write to the registry have.
    /// 
    /// </summary>
    /// <returns>If the event source was created successfully true is returned, otherwise false.</returns>
    //  ---------------------------------------------------------------------------------
    public static void LogError( Evado.UniForm.Web.EvPersistentPageState This, string EventContent )
    {
      string sEventContent = "PageUrl: " + This.Request.RawUrl
        + "\r\n\r\nUserId: " + This.User.Identity.Name
        + "\r\n\r\nDescription:\r\n" + EventContent;

      EvEventLog.WriteLog( sEventContent, EventLogEntryType.Error );

    }//END LogError method

    //  ===========================================================================
    /// <summary>
    /// LogEventError method
    /// 
    /// Description:
    /// Creates the Evado EvEvent Source. This requires administrator privileges
    /// because it needs to write to the registry have.
    /// 
    /// </summary>
    /// <returns>If the event source was created successfully true is returned, otherwise false.</returns>
    //  ---------------------------------------------------------------------------------
    public static void LogError( string EventContent )
    {
      WriteLog( EventContent, EventLogEntryType.Error );

    }//END LogError method

    //  ===========================================================================
    /// <summary>
    /// LogInformation method
    /// 
    /// Description:
    /// 
    /// </summary>
    /// <returns>If the event source was created successfully true is returned, otherwise false.</returns>
    //  ---------------------------------------------------------------------------------
    public static void LogInformation( string EventContent )
    {
      WriteLog( EventContent, EventLogEntryType.Information );

    }//END LogInformation method

    //  ===========================================================================
    /// <summary>
    /// LogWarning method
    /// 
    /// Description:
    /// 
    /// </summary>
    /// <returns>If the event source was created successfully true is returned, otherwise false.</returns>
    //  ---------------------------------------------------------------------------------
    public static void LogWarning( string EventContent )
    {
      WriteLog( EventContent, EventLogEntryType.Warning );

    }//END LogInformation method

    //  ===========================================================================
    /// <summary>
    /// LogWarning method
    /// 
    /// Description:
    /// 
    /// </summary>
    /// <returns>If the event source was created successfully true is returned, otherwise false.</returns>
    //  ---------------------------------------------------------------------------------
    public static void WriteLog( string EventContent, EventLogEntryType Type )
    {
      if ( EventSource == String.Empty )
      {
        return;
      }

      if ( EventContent.Length < 30000 )
      {
        EventLog.WriteEntry ( EventSource, EventContent, Type );

        return;

      }//END less than 30000

      int inLength = 30000;

      for ( int inStartIndex = 0; inStartIndex < EventContent.Length; inStartIndex += 30000 )
      {
        if ( EventContent.Length - inStartIndex < inLength )
        {
          inLength = EventContent.Length - inStartIndex;
        }
        string stContent = EventContent.Substring( inStartIndex, inLength );

        EventLog.WriteEntry ( EventSource, stContent, Type );

      }//END EventContent interation loop

    }//END WriteLog method 

  }//END Class

}//END NameSpace
