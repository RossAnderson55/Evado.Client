/***************************************************************************************
 * <copyright file="webclinical\default.aspx.cs" company="EVADO HOLDING PTY. LTD.">
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
 * Description: 
 *  This class contains the code behind functions for the default clinical web site
 *
 ****************************************************************************************/

using System;
using System.Collections.Generic;

///Evado. namespace references.

using Evado.UniForm.Web;
using Evado.UniForm.Model;

namespace Evado.UniForm.AdminClient
{
  /// <summary>
  /// This is the code behind class for the home page.
  /// </summary>
  public partial class DefaultPage : EvPersistentPageState
  {
    #region History management

    // ==================================================================================
    /// <summary>
    /// this method returns the initialises the Command history list
    /// </summary>
    // ---------------------------------------------------------------------------------
    public void initialiseHistory ( )
    {
      this.LogMethod ( "InitialiseHistory" );
      //
      // Initialise the home page Command.
      //
      Evado.UniForm.Model.EuCommand homePageCommand = new Evado.UniForm.Model.EuCommand ( );
      this.UserSession.CommandHistoryList = new List<Evado.UniForm.Model.EuCommand> ( );

    }//END initialiseCommandHistoryList method.

    // ==================================================================================
    /// <summary>
    /// add the current page Command to the previous page list
    /// </summary>
    /// <param name="PageCommand">DefaultPageCommand object</param>
    // ---------------------------------------------------------------------------------
    public void addHistoryCommand (
      Evado.UniForm.Model.EuCommand PageCommand )
    {
      this.LogMethod ( "addHistoryCommand" );

      //
      // If the Command identifier is empty then exit.
      //
      if ( PageCommand == null )
      {
        this.LogDebug ( "The command is null." );
        this.LogMethodEnd ( "addHistoryCommand" );
        return;
      }

      this.formatCommandTitle ( PageCommand );

      //
      // If the anonoyous access mode exit.
      //
      if ( this.UserSession.AppData.Page.GetAnonymousPageAccess ( ) == true )
      {
        this.LogDebug ( "Anonyous_Page_Access = true" );
        this.LogMethodEnd ( "addHistoryCommand" );
        return;
      }

      this.LogDebug ( "Command:" + PageCommand.getAsString ( false, false ) );
      //
      // If the Command identifier is empty then exit.
      //
      if ( PageCommand.Id == Guid.Empty
        || PageCommand.Id == EuStatics.CONST_LOGIN_COMMAND_ID )
      {
        this.initialiseHistory ( );
        this.LogDebug ( "The command identifier is null or login." );
        this.LogMethodEnd ( "addHistoryCommand" );
        return;
      }

      if ( PageCommand.Type == Evado.UniForm.Model.EuCommandTypes.Login_Command
        || PageCommand.Type == Evado.UniForm.Model.EuCommandTypes.Logout_Command
        || PageCommand.Type == Evado.UniForm.Model.EuCommandTypes.Meeting_Command
        || PageCommand.Type == Evado.UniForm.Model.EuCommandTypes.Anonymous_Command
        || PageCommand.Type == Evado.UniForm.Model.EuCommandTypes.Register_Device_Client
        || PageCommand.Type == Evado.UniForm.Model.EuCommandTypes.Offline_Command
        || PageCommand.Type == Evado.UniForm.Model.EuCommandTypes.Synchronise_Add
        || PageCommand.Type == Evado.UniForm.Model.EuCommandTypes.Synchronise_Save )
      {
        this.LogDebug ( "Not a command that has a history. i.e." + PageCommand.Type );
        this.LogMethodEnd ( "addHistoryCommand" );
        return;
      }

      this.LogDebug ( "INITIAL.CommandHistoryList.Count: {0}.", this.UserSession.CommandHistoryList.Count );

      this.LogDebug ( "PageCommand: {0}.", PageCommand.getAsString ( false, true ) );
      //
      // Look for the command in the history list delete after that command.
      //
      var command = this.getHistoryCommand ( PageCommand );

      this.LogDebug ( "Found Command: {0}.", command.getAsString ( false, true ) );
      //
      // if the command exists then pass through the parameters and delete the commands after the page command.
      //
      if ( command.Id != Guid.Empty )
      {
        this.LogDebug ( "Command in history, Title {0}.", command.Title );

        this.LogDebug ( "this.UserSession.CommandHistoryList.Count: {0}.", this.UserSession.CommandHistoryList.Count );

        this.LogMethodEnd ( "addHistoryCommand" );
        return;
      }

      //
      // If the method is to update a value then we need to undertake process processing after 
      // the method has been processed to return the user to the exit page.
      //
      if ( PageCommand.Method != Evado.UniForm.Model.EuMethods.Null
        && PageCommand.Method != Evado.UniForm.Model.EuMethods.Get_Object
        && PageCommand.Method != Evado.UniForm.Model.EuMethods.List_of_Objects )
      {
        this.LogDebug ( "No commands added to the list" );
        this.LogMethodEnd ( "addHistoryCommand" );
        return;
      }

      this.LogDebug ( "ADDING: Command : {0}  to history.", PageCommand.Title );
      PageCommand.Title = PageCommand.Title;

      this.LogDebug ( "1: title: {0}.", PageCommand.Title );
      int rnIndex = PageCommand.Title.IndexOf ( '\n' );

      if ( rnIndex >= 0 )
      {
        PageCommand.Title = PageCommand.Title.Substring ( 0, rnIndex );
      }
      this.LogDebug ( "2: title: {0}.", PageCommand.Title );
      //
      // Shorten the PageCommand title if it is greater then 20 characters
      //        
      if ( PageCommand.Title.Length > 20 )
      {
        PageCommand.Title = PageCommand.Title.Substring ( 0, 20 ) + "...";
      }

      this.LogDebug ( "3: title: {0}.", PageCommand.Title );

      //
      // Empty the header values as they are set by the client.
      //
      PageCommand.Header = new List<Evado.UniForm.Model.EuParameter> ( );

      //
      // If they do not match add the new previous page Command to the list.
      //  This is to stop consequetive duplicates.
      //
      this.UserSession.CommandHistoryList.Add ( PageCommand );

      this.LogDebug ( "this.UserSession.CommandHistoryList.Count: {0}.", this.UserSession.CommandHistoryList.Count );

      this.LogMethodEnd ( "addHistoryCommand" );
    }//END addHistoryCommand method

    // ================================================================================
    /// <summary>
    /// This method get the default exit groupCommand.
    /// </summary>
    /// <param name="PageCommand">DefaultPageCommand object: containing the groupCommand that 
    /// is called on web service</param>
    // ----------------------------------------------------------------------------------
    private void formatCommandTitle ( Evado.UniForm.Model.EuCommand PageCommand )
    {
      this.LogMethod ( "formCommandTitle" );
      this.LogDebug ( "PageCommand.Title:" + PageCommand.Title );

      if ( PageCommand == null )
      {
        return;
      }

      if ( PageCommand.Title.Length < 3 )
      {
        return;
      }

      string str = PageCommand.Title.Substring ( 0, 2 );
      this.LogDebug ( "str:'" + str + "'" );
      if ( str.Contains ( "-" ) == true )
      {
        PageCommand.Title = PageCommand.Title.Substring ( 2 );
        PageCommand.Title.Trim ( );
      }
      this.LogDebug ( "Formatted PageCommand.Title:" + PageCommand.Title );


    }

    // ==================================================================================
    /// <summary>
    /// This method deletes the page Command and all others in the last.
    /// </summary>
    /// <param name="Command">Guid Command identifer</param>
    // ---------------------------------------------------------------------------------
    private bool hasHistoryCommand ( Guid CommandId )
    {
      //
      // Iterate through the Command list to find a matching Command
      //
      for ( int count = 0; count < this.UserSession.CommandHistoryList.Count; count++ )
      {
        if ( ( this.UserSession.CommandHistoryList [ count ].Id == CommandId ) )
        {
          return true;
        }
      }

      return false;
    }

    // ==================================================================================
    /// <summary>
    /// Gets the last previous Command
    /// </summary>
    /// <param name="Command">Guid Command identifer</param>
    /// <returns>DefaultPageCommand</returns>
    // ---------------------------------------------------------------------------------
    public Evado.UniForm.Model.EuCommand getHistoryCommand (
      Guid CommandId )
    {
      this.LogMethod ( "getHistoryCommand" );
      this.LogDebug ( "CommandId: " + CommandId );
      Evado.UniForm.Model.EuCommand command = new Evado.UniForm.Model.EuCommand ( );

      //
      // Iterate through the list of Command history.
      //
      for ( int count = 0; count < this.UserSession.CommandHistoryList.Count; count++ )
      {
        //
        // does the Command id match
        //
        if ( this.UserSession.CommandHistoryList [ count ].Id == CommandId )
        {
          this.LogDebug ( "Found Command: " + this.UserSession.CommandHistoryList [ count ].Title );

          command = this.UserSession.CommandHistoryList [ count ].copyObject ( );

          //
          // Delete all object after the returned comment
          //
          for ( int delete = count; delete < this.UserSession.CommandHistoryList.Count; delete++ )
          {
            this.LogDebug ( "Deleting: " + this.UserSession.CommandHistoryList [ delete ].Title ); ;
            //
            // Delete all of the commands after the Command has been found )
            //
            this.UserSession.CommandHistoryList.RemoveAt ( count );
            delete--;
          }

          return command;
        }

      }//END of the iteration loop.


      this.LogDebug ( "History count: " + this.UserSession.CommandHistoryList.Count );

      this.LogMethodEnd ( "getHistoryCommand" );
      return command;

    }//END getCommandObject method.

    // ==================================================================================
    /// <summary>
    /// Gets the last previous Command
    /// </summary>
    /// <param name="PageCommand">Guid Command identifer</param>
    /// <returns>DefaultPageCommand</returns>
    // ---------------------------------------------------------------------------------
    public Evado.UniForm.Model.EuCommand getHistoryCommand (
      EuCommand PageCommand )
    {
      this.LogMethod ( "getHistoryCommand" );
      this.LogDebug ( "PageCommand: {0}.", PageCommand.getAsString ( false, true ) );

      //
      // Iterate through the list of Command history.
      //
      for ( int count = 0; count < this.UserSession.CommandHistoryList.Count; count++ )
      {
        var command = this.UserSession.CommandHistoryList [ count ];

        this.LogDebug ( "Command: {0}.", command.getAsString ( false, true ) );

        if ( command.isCommand ( PageCommand ) == true )
        {
          this.LogDebug ( "{0} > Found Command: {1}.", count, PageCommand.Title );

          //
          // Update the command parameters.
          //
          command.Parameters = PageCommand.Parameters;

          //
          // Delete all object after the returned comment
          //
          if ( ( count + 1 ) < this.UserSession.CommandHistoryList.Count )
          {
            for ( int delete = count + 1; delete < this.UserSession.CommandHistoryList.Count; delete++ )
            {
              this.LogDebug ( "Deleting: {0}.", this.UserSession.CommandHistoryList [ delete ].Title ); ;
              //
              // Delete all of the commands after the Command has been found )
              //
              this.UserSession.CommandHistoryList.RemoveAt ( delete );
              delete--;
            }
          }

          this.LogMethodEnd ( "getHistoryCommand" );
          return command;
        }

      }//END of the iteration loop.

      this.LogMethodEnd ( "getHistoryCommand" );
      return new EuCommand ( );

    }//END getCommandObject method.

    //*********************************************************************************
    #endregion.
  }
}
