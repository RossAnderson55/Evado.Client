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

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace Evado.UniForm.Model
{
  /// <summary>
  /// This class contains the device client page description.
  /// </summary>
  [Serializable]
  public class EuPage
  {
    #region Class constants
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region Properties

    /// <summary>
    ///  This property contains a Guid identifier for the Page object.
    /// </summary>
    public Guid Id { get; set; } = Guid.Empty;
    /*
    /// <summary>
    ///  This Property contains a Guid identifier for the object.
    /// </summary>
    [JsonProperty ( "pdid" )]
    public Guid PageDataGuid { get; set; }
    */

    /// <summary>
    /// This property contains definitions whether a group's fields are editable by the user
    /// when displayed in the device client.  
    /// Default is Edi_Disabled, valid values are Edit_Enabled or Edi_Disabled 
    /// </summary>
    [JsonProperty ( "ea" )]
    public EuEditAccess EditAccess { get; set; } = EuEditAccess.Disabled;


    /// <summary>
    ///  This property contains identification of page hierarchy.
    /// For the hierarchy to operate it is necessary for each identifier to be unique
    ///  within the hierarchy.
    /// </summary>
    [JsonProperty ( "pid" )]
    public String PageId { get; set; } = String.Empty;

    /// <summary>
    /// This property contains defininition of page groups default type and controls; how a page layout is customized for specific
    /// purposes.
    /// </summary>
    [JsonProperty ( "dgt" )]
    public EuGroupTypes DefaultGroupType { get; set; } = EuGroupTypes.Default;

    /// <summary>
    /// This property contains the title of the Page.
    /// </summary>
    public String Title { get; set; } = String.Empty;

    /// <summary>
    /// This property contains exit EuCommand when leaving this page.
    /// </summary>
    public EuCommand Exit { get; set; } = new EuCommand ( );

    /// <summary>
    /// This property contains the list of page EuCommands.
    /// </summary>
    [JsonProperty ( "cl" )]
    public List<EuCommand> CommandList { get; set; } = new List<EuCommand> ( );

    /// <summary>
    /// This property contains a list of page group objects.
    /// </summary>
    [JsonProperty ( "gl" )]
    public List<EuGroup> GroupList { get; set; } = new List<EuGroup> ( );

    /// <summary>
    /// This property contains a list of Parameter object.
    /// </summary>
    [JsonProperty ( "prm" )]
    public List<EuParameter> Parameters { get; set; } = new List<EuParameter> ( );

    /// <summary>
    /// This property contains the names of the Java Script library for this page.
    /// </summary>
    [JsonProperty ( "lib" )]
    public String JsLibrary { get; set; }

    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region Class Methods

    // ==================================================================================
    /// <summary>
    /// This method adds a parameter to the EuCommand's parameter list..
    /// </summary>
    /// <param name="Name">String: The name of the parameter.</param>
    /// <param name="Value">String: The value of the parameter.</param>
    //  ---------------------------------------------------------------------------------
    public void AddParameter ( String Name, String Value )
    {
      //
      // get the string value of the parameter list.
      //
      String name = Name.ToString ( );
      name = name.Trim ( );
      String value = Value.ToString ( );

      foreach ( EuParameter parameter in this.Parameters )
      {
        if ( parameter.Name == name )
        {
          parameter.Value = value;

          return;
        }
      }

      this.Parameters.Add ( new EuParameter ( name, value ) );

    }//END AddParameter method

    // ==================================================================================
    /// <summary>
    /// This method adds a parameter to the EuCommand's parameter list..
    /// </summary>
    /// <param name="Name">String: The name of the parameter.</param>
    /// <param name="Value">String: The value of the parameter.</param>
    //  ---------------------------------------------------------------------------------
    public void AddParameter ( EuPageParameters Name, object Value )
    {
      //
      // get the string value of the parameter list.
      //
      String name = Name.ToString ( );
      name = name.Trim ( );
      String value = Value.ToString ( );

      foreach ( EuParameter parameter in this.Parameters )
      {
        if ( parameter.Name == name )
        {
          parameter.Value = value;

          return;
        }
      }

      this.Parameters.Add ( new EuParameter ( name, value ) );

    }//END AddParameter method

    // ==================================================================================
    /// <summary>
    /// This method test whether the parameter exists in the field.
    /// </summary>
    /// <param name="Name">String: The name of the parameter.</param>
    //  ---------------------------------------------------------------------------------
    public bool hasParameter ( EuPageParameters Name )
    {
      //
      // get the string value of the parameter list.
      //
      String name = Name.ToString ( );
      name = name.Trim ( );

      //
      // Iterate through the parameters to get the selectev value.
      //
      foreach ( EuParameter parameter in this.Parameters )
      {
        if ( parameter.Name == name )
        {
          return true;
        }
      }

      //
      // Return result
      //
      return false;

    }//END AddParameter method

    // ==================================================================================
    /// <summary>
    /// This method adds a parameter to the EuCommand's parameter list..
    /// </summary>
    /// <param name="Name">String: The name of the parameter.</param>
    //  ---------------------------------------------------------------------------------
    public void DeleteParameter ( EuPageParameters Name )
    {
      //
      // get the string value of the parameter list.
      //
      String name = Name.ToString ( );
      name = name.Trim ( );

      for ( int count = 0; count < this.Parameters.Count; count++ )
      {
        EuParameter parameter = this.Parameters [ count ];

        if ( parameter.Name == name )
        {
          this.Parameters.RemoveAt ( count );
          count--;
        }
      }
    }

    // ==================================================================================
    /// <summary>
    /// This method adsd a parameter to the EuCommand's parameter list..
    /// </summary>
    /// <param name="Name">String: The name of the parameter.</param>
    /// <returns> String value of the header element</returns>
    //  ---------------------------------------------------------------------------------
    public String GetParameter ( EuPageParameters Name )
    {
      //
      // get the string value of the parameter list.
      //
      String name = Name.ToString ( );
      name = name.Trim ( );

      //
      // Iterate through the parameters to get the selectev value.
      //
      foreach ( EuParameter parameter in this.Parameters )
      {
        if ( parameter.Name == name )
        {
          return parameter.Value;
        }
      }

      return string.Empty;

    }//END AddParameter method

    // ==================================================================================
    /// <summary>
    /// This method adsd a parameter to the EuCommand's parameter list..
    /// </summary>
    /// <param name="Name">String: The name of the parameter.</param>
    /// <returns> String value of the header element</returns>
    //  ---------------------------------------------------------------------------------
    public String GetParameter ( String Name )
    {
      //
      // get the string value of the parameter list.
      //
      String name = Name.ToString ( );
      name = name.Trim ( );

      //
      // Iterate through the parameters to get the selectev value.
      //
      foreach ( EuParameter parameter in this.Parameters )
      {
        if ( parameter.Name == name )
        {
          return parameter.Value;
        }
      }

      return string.Empty;

    }//END AddParameter method

    // ==================================================================================
    /// <summary>
    /// This method turns on displaying groups and panels.
    /// Note: only the panels that are not in headers and columns will be displayed on 
    /// panels.
    /// </summary>
    // ---------------------------------------------------------------------------------
    [JsonIgnore]
    public bool AnonymousPageAccess
    {
      get
      {
        return Evado.Model.EvStatics.getBool ( this.GetParameter ( EuPageParameters.Anonyous_Page_Access ) );
      }
      set
      {
        this.AddParameter ( EuPageParameters.Anonyous_Page_Access, value );
      }
    }//END SetAnonymousPageAccess method

    // ==================================================================================
    /// <summary>
    /// This property defines the left column width as a percentage of page width
    /// </summary>
    // ---------------------------------------------------------------------------------
    [JsonIgnore]
    public int LeftColumnWidth
    {
      get
      {
        //
        // get the string value of the parameter list.
        //
        int iValue = -1;
        String value = this.GetParameter ( EuPageParameters.Left_Column_Width );

        if ( int.TryParse ( value, out iValue ) == true )
        {
          return iValue;
        }

        return -1;
      }
      set
      {
        if ( value < 0 || value > 50 )
        {
          value = -1;
        }
        //
        // get the string value of the parameter list.
        //
        this.AddParameter ( EuPageParameters.Left_Column_Width, value );
      }

    }//END LeftColumnWidth property

    // ==================================================================================
    /// <summary>
    /// This property defines the right column width as a percentage of page width
    /// </summary>
    // ---------------------------------------------------------------------------------
    [JsonIgnore]
    public int RightColumnWidth
    {
      get
      {
        //
        // get the string value of the parameter list.
        //
        int iValue = -1;
        String value = this.GetParameter ( EuPageParameters.Right_Column_Width );

        if ( int.TryParse ( value, out iValue ) == true )
        {
          return iValue;
        }

        return -1;
      }
      set
      {
        if ( value < 0 || value > 50 )
        {
          value = -1;
        }
        //
        // get the string value of the parameter list.
        //
        this.AddParameter ( EuPageParameters.Right_Column_Width, value );
      }

    }//END LeftColumnWidth method

    // ==================================================================================
    /// <summary>
    /// This method returns the ucon URL value.
    /// </summary>
    //  ---------------------------------------------------------------------------------
    public void setImageUrl ( EuPageImageUrls Parameter, String ImageUrl )
    {
      string name = Parameter.ToString ( );

      this.AddParameter ( name, ImageUrl );

    }//END setGroupStatus method

    // ==================================================================================
    /// <summary>
    /// This method returns the ucon URL value.
    /// </summary>
    //  ---------------------------------------------------------------------------------
    public String getImageUrl ( EuPageImageUrls Parameter )
    {
      string name = Parameter.ToString ( );

      return this.GetParameter ( name );

    }//END setGroupStatus method

    // ==================================================================================
    /// <summary>
    /// This method adds a new EuCommand to the group
    /// </summary>
    /// <param name="PageCommand">Command: new page EuCommand.</param>
    /// <remarks>
    /// This method consists of following steps.
    /// 
    /// 1. Add a new EuCommand to the _CommandList
    /// 
    /// </remarks>
    // ---------------------------------------------------------------------------------
    public EuCommand addCommand (
      EuCommand PageCommand )
    {
      this.CommandList.Add ( PageCommand );

      return PageCommand;

    }//END addCommand method

    // ==================================================================================
    /// <summary>
    /// This method adds a new EuCommand to the group
    /// </summary>
    /// <param name="Title">String: EuCommand title</param>
    /// <param name="ApplicationId">String: application identifier</param>
    /// <param name="ApplicationObject">String: Application object identifier</param>
    /// <param name="ApplicationMethod">EuMethods: method enumerated value</param>
    /// <returns>UniForm.Model.EuField object.</returns>
    ///<remarks>
    /// This method consists of following steps
    /// 
    /// 1. Initialise the EuCommand object.
    /// 
    /// 2. Add the EuCommand to the commmand list.
    /// 
    /// 3. Return the EuCommand object 
    ///
    /// </remarks> 
    // ---------------------------------------------------------------------------------
    public EuCommand addCommand (
      String Title,
      String ApplicationId,
      object ApplicationObject,
      EuMethods ApplicationMethod )
    {
      //
      // Initialise the EuCommand object.
      //
      EuCommand EuCommand = new EuCommand ( Title, ApplicationId, ApplicationObject, ApplicationMethod );

      //
      // Add the EuCommand to the EuCommand list.
      //
      this.CommandList.Add ( EuCommand );

      // 
      // Return the EuCommand object.
      //
      return EuCommand;

    }//END addCommand method

    // ==================================================================================
    /// <summary>
    /// This method adds a field group object to the page.
    /// </summary>
    /// <returns>ClientPageGroup object.</returns>
    /// <remarks>
    /// This method consists of following steps
    /// 
    /// 1. Initialise the new group object.
    /// 
    /// 2. Set the default group type.
    /// 
    /// 3. Add the new group object to the Page object.
    /// 
    /// 4. Return the group object.
    /// 
    /// </remarks>
    /// 
    // ----------------------------------------------------------------------------------
    public EuGroup AddGroup ( )
    {
      //
      // Initialise the new group object
      //
      EuGroup group = new EuGroup ( )
      {
        EditAccess = this.EditAccess,
        GroupType = this.DefaultGroupType
      };

      //
      // Add the new group object to the Page object.
      //
      this.GroupList.Add ( group );

      //
      // Return the group object.
      //
      return group;

    }//END AddGroup method

    // ==================================================================================
    /// <summary>
    /// This method adds a field group object to the page.
    /// </summary>
    /// <param name="Group">Group: the group object.</param>
    /// <remarks>
    /// This method consists of following steps
    /// 
    /// 1. Set the default group type.
    /// 
    /// 2. Add new group object to the Page object.
    /// </remarks>
    // ----------------------------------------------------------------------------------
    public void AddGroup ( EuGroup Group )
    {
      //
      // Set the default group type.
      //
      Group.GroupType = this.DefaultGroupType;

      //
      // Add the new group object to the Page object.
      //
      this.GroupList.Add ( Group );

    }

    // ==================================================================================
    /// <summary>
    /// This method adds a field group object to the page.
    /// </summary>
    /// <param name="Title">String: the title of the field group.</param>
    /// <returns>ClientPageGroup object.</returns>
    /// <remarks>
    /// This method consists of following steps
    /// 
    /// 1. Initialise the new group object.
    /// 
    /// 2. Set the default group type.
    /// 
    /// 3. Add the new group object to the Page object.
    /// 
    /// 4. Return the group object.
    /// 
    /// </remarks>
    /// 
    // ----------------------------------------------------------------------------------
    public EuGroup AddGroup (
      String Title )
    {
      //
      // Initialise the new group object
      //
      EuGroup group = new EuGroup ( )
      {
        Title = Title,
        EditAccess = this.EditAccess,
        GroupType = this.DefaultGroupType
      };


      //
      // Add the new group object to the Page object.
      //
      this.GroupList.Add ( group );

      //
      // Return the group object.
      //
      return group;

    }//END AddGroup method

    // ==================================================================================
    /// <summary>
    /// This method adds a field group object to the page.
    /// </summary>
    /// <param name="Title">String: the title of the field group.</param>
    /// <param name="EditAccess">ClientFieldEditsCodes: the default field edit state for the group.</param>
    /// <returns>ClientPageGroup object.</returns>
    /// <remarks>
    /// This method consists of following steps
    /// 
    /// 1. Initialise the new group object.
    /// 
    /// 2. Set the default group type.
    /// 
    /// 3. Add the new group object to the Page object.
    /// 
    /// 4. Return the group object.
    /// 
    /// </remarks>
    /// 
    // ----------------------------------------------------------------------------------
    public EuGroup AddGroup (
      String Title,
      EuEditAccess EditAccess )
    {
      //
      // Initialise the new group object
      //
      EuGroup group = new EuGroup ( )
      {
        Title = Title,
        EditAccess = EditAccess,
        GroupType = this.DefaultGroupType
      };

      //
      // Set the status if the group is inherited.
      //
      if ( EditAccess == EuEditAccess.Inherited
        || EditAccess == EuEditAccess.Null )
      {
        group.EditAccess = this.EditAccess;
      }

      //
      // Add the new group object to the Page object.
      //
      this.GroupList.Add ( group );

      //
      // Return the group object.
      //
      return group;

    }//END AddGroup method

    // ==================================================================================
    /// <summary>
    /// This method adds a field group object to the page.
    /// </summary>
    /// <param name="Title">String: the title of the field group.</param>
    /// <param name="Description">String: The description of the field group.</param>
    /// <param name="EditStatus">ClientFieldEditsCodes: the default field edit state for the group.</param>
    /// <returns>ClientPageGroup object.</returns>
    /// <remarks>
    /// This method consists of following steps
    /// 
    /// 1. Initialise the new group object.
    /// 
    /// 2. Set the default group type.
    /// 
    /// 3. Add the new group object to the Page object.
    /// 
    /// 4. Return the group object.
    /// 
    /// </remarks>
    /// 
    // ----------------------------------------------------------------------------------
    public EuGroup AddGroup (
      String Title,
      String Description,
      EuEditAccess EditStatus )
    {
      //
      // Initialise the new group object
      //
      EuGroup group = new EuGroup ( )
      {
        Title = Title,
        Description = Description,
        EditAccess = EditAccess,
        GroupType = this.DefaultGroupType
      };

      //
      // Set the status if the group is inherited.
      //
      if ( EditStatus == EuEditAccess.Inherited
        || EditStatus == EuEditAccess.Null )
      {
        group.EditAccess = this.EditAccess;
      }

      //
      // Add the new group object to the Page object.
      //
      this.GroupList.Add ( group );

      //
      // Return the group object.
      //
      return group;

    }//END AddGroup method

    // ==================================================================================
    /// <summary>
    /// This method adds a field group object to the page.
    /// </summary>
    /// <param name="Title">String: the title of the field group.</param>
    /// <param name="Description">String: The description of the field group.</param>
    /// <returns>ClientPageGroup object.</returns>
    /// <remarks>
    /// This method consists of following steps
    /// 
    /// 1. Initialise the new group object.
    /// 
    /// 2. Set the default group type.
    /// 
    /// 3. Add the new group object to the Page object.
    /// 
    /// 4. Return the group object.
    /// 
    /// </remarks>
    /// 
    // ----------------------------------------------------------------------------------
    public EuGroup AddGroup ( String Title, String Description )
    {
      //
      // Initialise the new group object
      //
      EuGroup group = new EuGroup ( )
      {
        Title = Title,
        Description = Description,
        EditAccess = this.EditAccess,
        GroupType = this.DefaultGroupType
        };

      //
      // Add the new group object to the Page object.
      //
      this.GroupList.Add ( group );

      //
      // Return the group object.
      //
      return group;

    }//END AddGroup method

    // ==================================================================================
    /// <summary>
    /// This method retrieves a EuCommand from the page object.  
    /// </summary>
    /// <param name="GroupTitle">String: A EuCommand's title as a string.</param>
    /// <returns>Evado.UniForm.Model.EuCommand object.</returns>
    /// <remarks>
    /// This method consists of following steps
    /// 
    /// 1. Iterate through the page EuCommand list looking for a matching EuCommand title.
    /// 
    /// 2. Iterate through the page group EuCommands looking for a matching EuCommand title.
    /// 
    /// 3. Return null of none are found. 
    /// 
    /// </remarks>
    // ----------------------------------------------------------------------------------
    public bool hasGroup ( String GroupTitle )
    {
      //
      // Iterate through the page EuCommand list looking for a matching 
      // EuCommand title.
      //
      foreach ( EuGroup group in this.GroupList )
      {

        // Compare EuCommand title and variable with the parameter EuCommand title
        // EuCommand is returned if comparision is true

        if ( group.Title.ToLower ( ) == GroupTitle.ToLower ( ) )
        {
          return true;
        }
      }//END page EuCommand iteration loop

      // 
      // Return false if not found.
      // 
      return false;

    }//END getCommand Method

    // ==================================================================================
    /// <summary>
    /// This method retrieves a EuCommand from the page object.  
    /// </summary>
    /// <param name="CommandTitle">String: A EuCommand's title as a string.</param>
    /// <returns>Evado.UniForm.Model.EuCommand object.</returns>
    /// <remarks>
    /// This method consists of following steps
    /// 
    /// 1. Iterate through the page EuCommand list looking for a matching EuCommand title.
    /// 
    /// 2. Iterate through the page group EuCommands looking for a matching EuCommand title.
    /// 
    /// 3. Return null of none are found. 
    /// 
    /// </remarks>
    // ----------------------------------------------------------------------------------
    public EuCommand getCommand ( String EuCommandTitle )
    {
      //
      // If the EuCommand Id is empty retun null indicating nothing found.
      //
      if ( EuCommandTitle == String.Empty )
      {
        return null;
      }

      //
      // Iterate through the page EuCommand list looking for a matching 
      // EuCommand title.
      //
      foreach ( EuCommand EuCommand in this.CommandList )
      {

        // Compare EuCommand title and variable with the parameter EuCommand title
        // EuCommand is returned if comparision is true

        if ( EuCommand.Title.ToLower ( ) == EuCommandTitle.ToLower ( ) )
        {
          return EuCommand;
        }
      }//END page EuCommand iteration loop

      //
      // Iterate through the page group EuCommands looking for a matching 
      // EuCommand title.
      //
      foreach ( EuGroup group in this.GroupList )
      {
        foreach ( EuCommand EuCommand in group.CommandList )
        {
          // Compare EuCommand title and variable with the parameter EuCommand title
          // EuCommand is returned if comparision is true

          if ( EuCommand.Title.ToLower ( ) == EuCommandTitle.ToLower ( ) )
          {
            return EuCommand;
          }
        }//END page EuCommand iteration loop
      }

      // 
      // Return null of none are found.
      // 
      return null;

    }//END getCommand Method

    // ==================================================================================
    /// <summary>
    /// This method retrieves a EuCommand from the page object.  
    /// </summary>
    /// <param name="CommandId">GUID: the EuCommand identifier.</param>
    /// <returns>Evado.UniForm.Model.EuCommand object.</returns>
    /// <remarks>
    /// This method consists of following steps
    /// 
    /// 1. Iterate through the page EuCommand list looking for a matching EuCommand title.
    /// 
    /// 2. Iterate through the page group EuCommands looking for a matching EuCommand title.
    /// 
    /// 3. Return null of none are found. 
    /// 
    /// </remarks>
    // ----------------------------------------------------------------------------------
    public EuCommand getCommand ( Guid EuCommandId )
    {
      //
      // If the EuCommand Id is empty retun null indicating nothing found.
      //
      if ( EuCommandId == Guid.Empty )
      {
        return null;
      }

      //
      // Iterate through the page EuCommand list looking for a matching 
      // EuCommand title.
      //
      foreach ( EuCommand EuCommand in this.CommandList )
      {

        // Compare EuCommand title and variable with the parameter EuCommand title
        // EuCommand is returned if comparision is true

        if ( EuCommand.Id == EuCommandId )
        {
          return EuCommand;
        }
      }//END page EuCommand iteration loop

      //
      // Iterate through the page group EuCommands looking for a matching 
      // EuCommand title.
      //
      foreach ( EuGroup group in this.GroupList )
      {
        foreach ( EuCommand EuCommand in group.CommandList )
        {
          // Compare EuCommand title and variable with the parameter EuCommand title
          // EuCommand is returned if comparision is true

          if ( EuCommand.Id == EuCommandId )
          {
            return EuCommand;
          }
        }//END page EuCommand iteration loop
      }

      // 
      // Return null of none are found.
      // 
      return null;

    }//END getCommand Method

    // ==================================================================================
    /// <summary>
    /// This method retrieves a EuCommand from the page object.  
    /// </summary>
    /// <param name="CommandTitle">String: A EuCommand's title as a string.</param>
    /// <returns>Evado.UniForm.Model.EuCommand object.</returns>
    /// <remarks>
    /// This method consists of following steps
    /// 
    /// 1. Iterate through the page EuCommand list looking for a matching EuCommand title and 
    ///    delete that EuCommand.
    /// 
    /// 2. Iterate through the page group EuCommands looking for a matching EuCommand title and 
    ///    delete that EuCommand.
    /// 
    /// 3. Return false if the EuCommand is not removed. 
    /// </remarks>
    // ----------------------------------------------------------------------------------
    public bool deleteCommand ( String EuCommandTitle )
    {
      //
      // If the EuCommand Id is empty retun null indicating nothing found.
      //
      if ( EuCommandTitle == String.Empty )
      {
        return false;
      }

      //
      // Iterate through the page EuCommand list looking for a matching 
      // EuCommand title and delete that EuCommand.
      //
      for ( int i = 0; i < this.CommandList.Count; i++ )
      {
        EuCommand EuCommand = this.CommandList [ i ];

        // Compare EuCommand title and variable with the parameter EuCommand title
        // EuCommand is returned if comparision is true

        if ( EuCommand.Title.ToLower ( ) == EuCommandTitle.ToLower ( ) )
        {
          this.CommandList.RemoveAt ( i );
          i--;
          return true;
        }
      }//END page EuCommand iteration loop

      //
      // Iterate through the page group EuCommands looking for a matching 
      // EuCommand title and delete that EuCommand.
      //
      foreach ( EuGroup group in this.GroupList )
      {
        for ( int i = 0; i < group.CommandList.Count; i++ )
        {
          EuCommand EuCommand = group.CommandList [ i ];

          // Compare EuCommand title and variable with the parameter EuCommand title
          // EuCommand is returned if comparision is true

          if ( EuCommand.Title.ToLower ( ) == EuCommandTitle.ToLower ( ) )
          {
            this.CommandList.RemoveAt ( i );
            i--;
            return true;
          }

        }//END group EuCommand list iteration loop

      }//END group iteration loop

      // 
      // Return false if the EuCommand is not removed.
      // 
      return false;

    }//END deleteCommand Method

    // ==================================================================================
    /// <summary>
    /// This method deleted a EuCommand from the page object.  
    /// </summary>
    /// <param name="CommandId">String: A EuCommand's title as a string.</param>
    /// <returns>Evado.UniForm.Model.EuCommand object.</returns>
    /// <remarks>
    /// This method consists of following steps
    /// 
    /// 1. Iterate through the page EuCommand list looking for a matching EuCommand title and 
    ///    delete that EuCommand.
    /// 
    /// 2. Iterate through the page group EuCommands looking for a matching EuCommand title and 
    ///    delete that EuCommand.
    /// 
    /// 3. Return false if the EuCommand is not removed. 
    /// </remarks>
    // ----------------------------------------------------------------------------------
    public bool deleteCommand ( Guid EuCommandId )
    {
      //
      // If the EuCommand Id is empty retun null indicating nothing found.
      //
      if ( EuCommandId == Guid.Empty )
      {
        return false;
      }

      //
      // Iterate through the page EuCommand list looking for a matching 
      // EuCommand title and delete that EuCommand.
      //
      for ( int i = 0; i < this.CommandList.Count; i++ )
      {
        EuCommand EuCommand = this.CommandList [ i ];

        // Compare EuCommand title and variable with the parameter EuCommand title
        // EuCommand is returned if comparision is true

        if ( EuCommand.Id == EuCommandId )
        {
          this.CommandList.RemoveAt ( i );
          i--;
          return true;
        }
      }//END page EuCommand iteration loop

      //
      // Iterate through the page group EuCommands looking for a matching 
      // EuCommand title and delete that EuCommand.
      //
      foreach ( EuGroup group in this.GroupList )
      {
        for ( int i = 0; i < group.CommandList.Count; i++ )
        {
          EuCommand EuCommand = group.CommandList [ i ];

          // Compare EuCommand title and variable with the parameter EuCommand title
          // EuCommand is returned if comparision is true

          if ( EuCommand.Id == EuCommandId )
          {
            this.CommandList.RemoveAt ( i );
            i--;
            return true;
          }

        }//END group EuCommand list iteration loop

      }//END group iteration loop

      // 
      // Return false if the EuCommand is not removed.
      // 
      return false;

    }//END getCommand Method

    // ==================================================================================
    /// <summary>
    /// This method retrieves a EuCommand from the page object.  
    /// </summary>
    /// <param name="FieldId">String: A EuCommand's title as a string.</param>
    /// <returns>Evado.UniForm.Model.EuCommand object.</returns>
    /// <remarks>
    /// This method consists of following steps. 
    /// 
    /// 1. Iterate through the page group EuCommands looking for a matching EuCommand title.
    ///
    /// 2. If FieldId is equal to parameter passed, return field object.
    /// 
    /// 3. Return null of none are found.
    /// 
    /// </remarks>
    // ----------------------------------------------------------------------------------
    public EuField getField ( String FieldId )
    {
      //
      // Iterate through the page group EuCommands looking for a matching 
      // EuCommand title.
      //
      foreach ( EuGroup group in this.GroupList )
      {
        foreach ( EuField field in group.FieldList )
        {
          if ( field.FieldId.ToLower ( ) == FieldId.ToLower ( ) )
          {
            return field;
          }
        }//END page EuCommand iteration loop
      }

      // 
      // Return null of none are found.
      // 
      return null;

    }//END getCommand Method

    // =====================================================================================
    /// <summary>
    /// This method returns the contents of the page field.
    /// </summary>
    /// <param name="DataType">EvDataTypes: a data type object.</param>
    /// <param name="DataId"> String: data Id  </param>
    /// <returns>The contents of the page EuCommand.</returns>
    /// <remarks>
    /// This method consists of following steps
    /// 
    /// 1. Loop through group list
    /// 
    /// 2. Loop through field
    /// 
    /// 3. Validate the conditions
    /// 
    /// 4. Return string 
    ///  
    /// </remarks>
    // -------------------------------------------------------------------------------------
    public String getFieldValue (
      Evado.Model.EvDataTypes DataType,
      String DataId )
    {
      //
      // create a stValue string
      //

      String stValue = String.Empty;

      //
      // iterate through the list group
      //


      foreach ( Evado.UniForm.Model.EuGroup group in this.GroupList )
      {

        //
        // iterate throuth the list field
        //

        foreach ( EuField field in group.FieldList )
        {
          //
          // Compare field type to data type and filed type to Null
          //
          if ( field.Type == DataType || field.Type == Evado.Model.EvDataTypes.Null )
          {
            if ( field.FieldId == DataId )
            {
              //
              // Return filed value if filed value is equal to data Id
              //

              return field.Value;
            }
          }
        }
      }

      return stValue;
    }//END getFieldValue method

    // =====================================================================================
    /// <summary>
    /// This method returns the contents of the page field.
    /// </summary>
    /// <param name="FieldId"> String: data Id  </param>
    /// <returns>The contents of the page EuCommand.</returns>
    /// <remarks>
    /// This method consists of following steps
    /// 
    /// 1. Loop through group list
    /// 
    /// 2. Loop through field
    /// 
    /// 3. Validate the conditions
    /// 
    /// 4. Return string 
    ///  
    /// </remarks>
    // -------------------------------------------------------------------------------------
    public String getFieldValue (
      String FieldId )
    {

      //
      // create a stValue string
      //
      String stValue = String.Empty;

      //
      // If field guid return empty string.
      //
      if ( FieldId == String.Empty )
      {
        return stValue;
      }

      //
      // iterate through the list group
      //
      foreach ( Evado.UniForm.Model.EuGroup group in this.GroupList )
      {
        //
        // iterate throuth the list field
        //

        foreach ( EuField field in group.FieldList )
        {
          //
          // Compare field type to data type and filed type to Null
          //
          if ( field.FieldId.ToLower ( ) == FieldId.ToLower ( ) )
          {
            //
            // Return filed value if filed value is equal to data Id
            //

            return field.Value;
          }
        }
      }

      return stValue;

    }//END getFieldValue method

    // =====================================================================================
    /// <summary>
    /// This method returns the contents of the page field.
    /// </summary>
    /// <param name="FieldGuid"> Guid: data Id  </param>
    /// <returns>The contents of the page EuCommand.</returns>
    /// <remarks>
    /// This method consists of following steps
    /// 
    /// 1. Loop through group list
    /// 
    /// 2. Loop through field
    /// 
    /// 3. Validate the conditions
    /// 
    /// 4. Return string 
    ///  
    /// </remarks>
    // -------------------------------------------------------------------------------------
    public String getFieldValue (
      Guid FieldGuid )
    {

      //
      // create a stValue string
      //
      String stValue = String.Empty;

      //
      // If field guid return empty string.
      //
      if ( FieldGuid == Guid.Empty )
      {
        return stValue;
      }

      //
      // iterate through the list group
      //
      foreach ( Evado.UniForm.Model.EuGroup group in this.GroupList )
      {
        //
        // iterate throuth the list field
        //

        foreach ( EuField field in group.FieldList )
        {
          //
          // Compare field type to data type and filed type to Null
          //
          if ( field.Id == FieldGuid )
          {
            //
            // Return filed value if filed value is equal to data Id
            //

            return field.Value;
          }
        }
      }

      return stValue;

    }//END getFieldValue method

    // =====================================================================================
    /// <summary>
    /// This method returns the contents of the page field.
    /// </summary>
    /// <param name="FieldGuid"> Guid: data Id  </param>
    /// <param name="FieldValue">String: field value.</param>
    /// <returns>True: field found and updated.</returns>
    /// <remarks>
    /// This method consists of following steps
    /// 
    /// 1. Iterate through group list
    /// 
    /// 2. Iterate through field
    /// 
    /// 3. Validate the conditions
    /// 
    /// 4. Return result 
    ///  
    /// </remarks>
    // -------------------------------------------------------------------------------------
    public bool setFieldValue (
      Guid FieldGuid,
      String FieldValue )
    {
      //
      // If field guid return empty string.
      //
      if ( FieldGuid == Guid.Empty )
      {
        return false;
      }

      //
      // iterate through the list group
      //
      foreach ( Evado.UniForm.Model.EuGroup group in this.GroupList )
      {
        //
        // iterate throuth the list field
        //
        foreach ( EuField field in group.FieldList )
        {
          //
          // Compare field ID matach update the field value and return true.
          //
          if ( field.Id == FieldGuid )
          {
            field.Value = FieldValue;
            //
            // Return result
            //
            return true;
          }
        }
      }

      return false;

    }//END getFieldValue method

    // =====================================================================================
    /// <summary>
    /// This method returns the contents of the page field.
    /// </summary>
    /// <param name="FieldGuid"> Guid: data Id  </param>
    /// <param name="Row"> int: table row to be updated.</param>
    /// <param name="Column"> int: table column to be updated.</param>
    /// <param name="FieldValue">String: field value.</param>
    /// <returns>True: field found and updated.</returns>
    /// <remarks>
    /// This method consists of following steps
    /// 
    /// 1. Iterate through group list
    /// 
    /// 2. Iterate through field
    /// 
    /// 3. Validate the conditions
    /// 
    /// 4. Return result 
    ///  
    /// </remarks>
    // -------------------------------------------------------------------------------------
    public bool setFieldValue (
      Guid FieldGuid,
      int Row,
      int Column,
      String FieldValue )
    {
      //
      // If field guid return empty string.
      //
      if ( FieldGuid == Guid.Empty )
      {
        return false;
      }

      //
      // iterate through the list group
      //
      foreach ( Evado.UniForm.Model.EuGroup group in this.GroupList )
      {
        //
        // iterate throuth the list field
        //
        foreach ( EuField field in group.FieldList )
        {
          //
          // if the Id does not match continue.
          //
          if ( field.Id != FieldGuid )
          {
            continue;
          }

          //
          // If the matching field is not a table or matrix exit.
          //
          if ( field.Type != Evado.Model.EvDataTypes.Table
            || field.Type != Evado.Model.EvDataTypes.Special_Matrix )
          {
            return false;
          }

          //
          // If the field table object is null exit.
          //
          if ( field.Table == null )
          {
            return false;
          }

          //
          // If the row or column could are outside the table site exit.
          //
          if ( field.Table.Rows.Count <= Row
            || field.Table.ColumnCount <= Column )
          {
            return false;
          }

          //
          // Update the table cell value.
          //
          field.Table.Rows [ Row ].Column [ Column ] = FieldValue;

          //
          // Return result
          //
          return true;
        }
      }

      return false;

    }//END getFieldValue method

    // ==================================================================================
    /// <summary>
    /// This method generates the PageData object for this page..
    /// </summary>
    /// <param name="AppId"> String: A variable to be initialized to page data AppId </param>
    /// <param name="Object"> String: A variable to be initialized to page data Object </param>
    /// <returns>Evado.UniForm.Model.EuPageData object</returns>
    /// <remarks>
    /// This method consists of following steps
    /// 
    /// 1. Initialise the methods variables and objects. 
    /// 
    /// 2. Iterate through the fields.
    /// 
    /// 3. Return a page data object containing the page content. 
    /// </remarks>
    // ----------------------------------------------------------------------------------
    public EuPageData getPageData (
      String AppId,
      String Object )
    {
      //
      // Initialise the methods variables and objects.
      //
      EuPageData pageData = new EuPageData ( );
      pageData.Id = this.Id;
      pageData.PageId = this.PageId;
      pageData.EditAccess = this.EditAccess;
      pageData.AppId = AppId;
      pageData.Object = Object;

      //
      // Iterate through the page fields.
      //
      foreach ( EuGroup group in this.GroupList )
      {
        this.getPageDataFields ( pageData.DataList, group );
      }

      //
      // Return a page data object containing the page content.
      //
      return pageData;

    }//END getPageData method

    // =================================================================================
    /// <summary>
    /// This method generate the field page data parameters.
    /// </summary>
    /// <param name="DataObjectList"> List: A list of Evado.UniForm.Model.DataObject </param>
    /// <param name="Group"> Group: the group object. </param>
    /// <remarks>
    /// This method consists of following steps
    /// 
    /// 1. Initialise the methods variables and objects. 
    /// 
    /// 2. Iterate through the group fields. 
    /// 
    /// 3. Initialise iteration parameters.
    /// 
    /// 4. Prepare the field annotation parameters.
    /// 
    /// 5. Retrieve the annotation value if it exists. 
    /// 
    /// 6. If the field has existing annotations then add the annotation data as an existing annotation content.
    /// 
    /// 7. Add an annotation field object for the user entered annotation.
    /// 
    /// 8. Field type switch to process the different field types approproately.
    /// 
    /// </remarks>
    // ----------------------------------------------------------------------------------
    private void getPageDataFields ( List<EuDataObj> DataObjectList, EuGroup Group )
    {
      //
      // Initialise the methods variables and objects.
      //
      EuParameter parameter = new EuParameter ( );
      EuDataObj data = new EuDataObj ( );

      //
      // Iterate through the group fields.
      //
      foreach ( EuField field in Group.FieldList )
      {
        //
        // Initialise iteration parameters.
        // 
        String stAnnotationParameter = String.Empty;

        // 
        // Prepare the field annotation parameters.
        // 
        if ( Group.GroupType == Evado.UniForm.Model.EuGroupTypes.Annotated_Fields
          && field.FieldId != String.Empty
          && field.Type != Evado.Model.EvDataTypes.Read_Only_Text )
        {
          //
          // Retrieve the annotation value if it exists.
          // 
          string stAnnotation = field.GetParameter ( EuFieldParameters.Annotation );
          stAnnotation = stAnnotation.Replace ( "\r\n", "\\r\\n" );

          // 
          // If the field has existing annotations then add the annotation data as an exising annotation content.
          // 
          if ( stAnnotation != String.Empty )
          {
            stAnnotationParameter +=
                 "\"" + Evado.UniForm.Model.EuFieldParameters.Annotation
               + "\":\"" + stAnnotation + "\",";
          }

          //
          // Add an annotation field object for the user entered annotation.
          // 
          stAnnotationParameter +=
               "\"" + Evado.UniForm.Model.EuFieldParameters.Annotation
               + EuDataObj.CONST_FIELD_ANNOTATION_NEW_SUFFIX
             + "\":\"\"";
        }

        //
        // Field type switch to process the different field types approproately.
        //

        switch ( field.Type )
        {

          //
          //A switch case for Evado.Model.UniFrom.EvDataType.Table
          // 

          case Evado.Model.EvDataTypes.Table:
            {
              this.getPageDataFieldTableData ( field, DataObjectList, stAnnotationParameter );

              break;
            }

          //
          // A switch case for Evado.UniForm.Model.EvDatatype.Image
          //


          case Evado.Model.EvDataTypes.Image:
            {
              string stHash = field.GetParameter ( EuFieldParameters.MD5_Hash );
              string stHashParameters = "\"" + Evado.UniForm.Model.EuFieldParameters.MD5_Hash
               + "\":\"" + stHash + "\""
               + ",\"" + Evado.UniForm.Model.EuFieldParameters.Field_Type
               + "\":" + ( int ) field.Type;

              //
              // Compare stAnnotationParameter to Null
              //

              if ( stAnnotationParameter != String.Empty )
              {
                stAnnotationParameter = "{" + stHashParameters + "," + stAnnotationParameter + "}";
              }

              //
              // Add data object parameters to DataObjectList
              //

              DataObjectList.Add ( new EuDataObj ( field.FieldId, field.Value, stAnnotationParameter ) );

              break;
            }

          //
          // Default switch case
          //


          default:
            {
              if ( stAnnotationParameter != String.Empty )
              {
                stAnnotationParameter = "{" + stAnnotationParameter + "}";
              }
              DataObjectList.Add ( new EuDataObj ( field.FieldId, field.Value, stAnnotationParameter ) );
              break;
            }
        }//END field type switch

      }//END field iteration loop 

    }//END getPageDataFields method

    // =================================================================================
    /// <summary>
    /// This method generate the field page data parameters.
    /// </summary>
    /// <param name="AnnotationParameters"> String: An Annotation Parameter </param>
    /// <param name="Field"> Field : The Field Object </param>
    /// <param name="ParameterList"> List: List of Evado.UniForm.Model.DataObj object </param>
    /// <remarks>
    /// This method consists of following steps
    /// 
    /// 1. Initailise the methods variables and objects.
    /// 
    /// 2. If the array length is greater than 5 add parameters and array indexes in ParameterList 
    /// 
    /// 3. If the field is empty or has length less than 6 fill ParameterList with empty values.
    /// 
    /// 
    /// </remarks>
    // ----------------------------------------------------------------------------------
    public void getPageDataFieldAddressData ( EuField Field,
      List<EuDataObj> ParameterList,
      String AnnotationParameters )
    {
      //
      // Initialise the methods variables and objects.
      //
      string [ ] arrValues = Field.Value.Split ( ',' );
      if ( AnnotationParameters != String.Empty )
      {
        AnnotationParameters = "{" + AnnotationParameters + "}";
      }

      //
      // If the array length is greater than 5 add parameters and array indexes in ParameterList 
      //
      if ( arrValues.Length > 5 )
      {
        ParameterList.Add ( new EuDataObj ( Field.FieldId, arrValues [ 0 ] ) );
        ParameterList.Add ( new EuDataObj ( Field.FieldId, arrValues [ 1 ], AnnotationParameters, 1 ) );
        ParameterList.Add ( new EuDataObj ( Field.FieldId, arrValues [ 2 ], AnnotationParameters, 2 ) );
        ParameterList.Add ( new EuDataObj ( Field.FieldId, arrValues [ 3 ], AnnotationParameters, 3 ) );
        ParameterList.Add ( new EuDataObj ( Field.FieldId, arrValues [ 4 ], AnnotationParameters, 4 ) );
        ParameterList.Add ( new EuDataObj ( Field.FieldId, arrValues [ 5 ], AnnotationParameters, 5 ) );
      }

      //
      //If the field is empty or has length less than 6 fill ParameterList with empty values.
      //

      if ( Field.Value == String.Empty
       || arrValues.Length < 6 )
      {
        ParameterList.Add ( new EuDataObj ( Field.FieldId, String.Empty ) );
        ParameterList.Add ( new EuDataObj ( Field.FieldId, String.Empty, AnnotationParameters, 1 ) );
        ParameterList.Add ( new EuDataObj ( Field.FieldId, String.Empty, AnnotationParameters, 2 ) );
        ParameterList.Add ( new EuDataObj ( Field.FieldId, String.Empty, AnnotationParameters, 3 ) );
        ParameterList.Add ( new EuDataObj ( Field.FieldId, String.Empty, AnnotationParameters, 4 ) );
        ParameterList.Add ( new EuDataObj ( Field.FieldId, String.Empty, AnnotationParameters, 5 ) );
      }

    }//END getPageDataFieldAddressData method

    // =================================================================================
    /// <summary>
    /// This method generate the field page data parameters.
    /// </summary>
    /// <param name="AnnotationParameters"> String: A AnnotationParameter </param>
    /// <param name="DataObjectList"> List: A list of Evado.UniForm.Model.DataObj </param>
    /// <param name="Field"> Field: A Field object </param>
    /// <remarks>
    /// This method consists of following steps
    /// 
    /// 1. Initialise the methods variables and objects.
    /// 
    /// 2. If array length is greater than 1 add parameters and arrayValues to DataObjectList.
    /// 
    /// 3. Else add an empty value to FieldId and annotation parameter value.
    /// 
    /// </remarks>
    // ----------------------------------------------------------------------------------
    public void getPageDataFieldNameData (
      EuField Field,
      List<EuDataObj> DataObjectList,
      String AnnotationParameters )
    {
      //
      // Initialise the methods variables and objects.
      //
      string [ ] arrValues = Field.Value.Split ( ',' );

      if ( AnnotationParameters != String.Empty )
      {
        AnnotationParameters = "{" + AnnotationParameters + "}";
      }

      //
      // If array length is greater than 1 add parameters and arrayValues to DataObjectList
      //
      if ( arrValues.Length > 1 )
      {
        DataObjectList.Add ( new EuDataObj ( Field.FieldId, arrValues [ 0 ] ) );

        DataObjectList.Add ( new EuDataObj ( Field.FieldId, arrValues [ 1 ], AnnotationParameters, 1 ) );
      }

      //
      // Else add an empty value to FieldId and annotation parameter value
      //
      else
      {
        DataObjectList.Add ( new EuDataObj ( Field.FieldId, String.Empty ) );

        DataObjectList.Add ( new EuDataObj ( Field.FieldId, Field.Value, AnnotationParameters, 1 ) );
      }

    }//END getPageDataFieldNameData method

    // =================================================================================
    /// <summary>
    /// This method generate the field page data parameters.
    /// </summary>
    /// <param name="Field">Field object: contain the field to be processes.</param>
    /// <param name="DataObjectList">List DataObj</param>
    /// <param name="AnnotationParameters">String: of json parameters</param>
    /// <remarks>
    /// This method consists of following steps.
    /// 
    /// 1. Initialise the methods variables and objects.
    /// 
    /// 2. Confirm that the field has a table.
    /// 
    /// 3. Iterate through the table rows.
    /// 
    /// 4. Iterate through the coumns.
    /// 
    /// 5. Create a value if the column has a header value.
    /// 
    /// 6. Create the parameter name and value.
    /// 
    /// 7. Add page data parameter.
    /// 
    /// </remarks>
    /// 
    // ----------------------------------------------------------------------------------
    public void getPageDataFieldTableData (
      EuField Field,
      List<EuDataObj> DataObjectList,
      String AnnotationParameters )
    {
      //
      // Initialise the methods variables and objects
      //
      int row = 0;
      int col = 0;
      if ( AnnotationParameters != String.Empty )
      {
        AnnotationParameters = "{" + AnnotationParameters + "}";
      }

      //
      // confirm that the field has a table.
      //
      if ( Field.Table != null )
      {
        //
        // Iterate through the table rows.
        //
        for ( row = 0; row < Field.Table.Rows.Count; row++ )
        {
          //
          // Iterate through the row columns.
          //
          for ( col = 0; col < Field.Table.Header.Length; col++ )
          {
            //
            // Create a value if the column has a header value.
            //
            if ( Field.Table.Header [ col ].Text != String.Empty )
            {
              //
              // create the parameter name and value
              //
              string stValue = Field.Table.Rows [ row ].Column [ col ];

              //
              // Add page data parameter
              //
              DataObjectList.Add ( new EuDataObj ( Field.FieldId, stValue, AnnotationParameters, row, col ) );

            }//END header exist

          }//END column iteration loop 

        }//END row iteration loop

      }//END Table object exists.

    }//END getPageDataFieldTableData method

    // ==================================================================================
    /// <summary>
    /// This method get the group value
    /// </summary>
    /// <param name="Title">String: the title of the field group.</param>
    /// <returns>ClientPageGroup object.</returns>
    /// <remarks>
    /// This method consists of following steps.
    /// 
    /// 1. Iterate through the groups to find the matching group.
    /// 
    /// 2. If group title matches with parameter passed, return a group object
    /// 
    /// </remarks>
    // ----------------------------------------------------------------------------------
    public EuGroup GetGroup ( String Title )
    {
      //
      // Iterate through the groups to find the matching group.
      //
      foreach ( EuGroup group in this.GroupList )
      {
        if ( group.Title.ToLower ( ) == Title.ToLower ( ) )
        {
          return group;
        }
      }// END group iteration loop

      return new EuGroup ( );

    }//END GetGroup method


    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion
  }
}//END namespace