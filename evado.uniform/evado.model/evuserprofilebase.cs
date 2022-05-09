/***************************************************************************************
 * <copyright file="model\EvUserProfile.cs" company="EVADO HOLDING PTY. LTD.">
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
 *  This class contains the EvUserProfile data object.
 *
 ****************************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Evado.Model
{
  /// <summary>
  /// This class provide user profile for using across the application
  /// </summary>
  [Serializable]
  public class EvUserProfileBase
  {

    #region Class enumerators

    /// <summary>
    /// This enumerated list defines the user authentication states within the web service.
    /// </summary>
    public enum UserAuthenticationStates
    {
      /// <summary>
      /// This enumerated state indicates that the user authenticated state is not set.
      /// </summary>
      Null = 0,

      /// <summary>
      /// This enumerated state indicates that the user is not authenticated or failed authentication.
      /// </summary>
      Not_Authenticated,

      /// <summary>
      /// This enumerated state indicates that the user credntials have been authenticated.
      /// </summary>
      Authenticated,

      /// <summary>
      /// This enumerated state indicates that the user has been provided with anonymous access to the environment.
      /// </summary>
      Anonymous_Access,

      /// <summary>
      /// This enumerated state indicates that the user has been provided with token access to the environment.
      /// </summary>
      Token_Access,

      /// <summary>
      /// This enumerated state indicates that the user has been provided with anonymous access to the environment.
      /// </summary>
      Debug_Authentication,

    }

    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region Class Properties

    private Guid _Guid = Guid.Empty;
    /// <summary>
    /// This property contains the global unique identifier
    /// </summary>
    public Guid Guid
    {
      get { return this._Guid; }
      set { this._Guid = value; }
    }

    /// <summary>
    /// This property contains the guid user token from an external system.
    /// 
    /// The toke value will become the Guid identifier for the user proifle.
    /// </summary>
    public Guid Token { get; set; }

    private string _UserId = String.Empty;
    /// <summary>
    /// This property defines the user identifier
    /// </summary>
    public string UserId
    {
      get { return this._UserId; }
      set { this._UserId = value; }
    }

    private string _Password = String.Empty;
    /// <summary>
    /// This property contains user password and is used with updating the password.
    /// </summary>
    public string Password
    {
      get { return this._Password; }
      set { this._Password = value; }
    }


    private string _ActiveDirectoryUserId = String.Empty;
    /// <summary>
    /// This property contains active directory user identifier
    /// </summary>
    public string ActiveDirectoryUserId
    {
      get { return this._ActiveDirectoryUserId; }
      set { this._ActiveDirectoryUserId = value; }
    }

    private string _Prefix = String.Empty;
    /// <summary>
    /// This property user contains prefix 
    /// </summary>
    public string Prefix
    {
      get { return this._Prefix; }
      set { this._Prefix = value; }
    }

    private string _GivenName = String.Empty;
    /// <summary>
    /// This property contains user's given name
    /// </summary>
    public string GivenName
    {
      get { return this._GivenName; }
      set
      {
        this._GivenName = value;
        this.setNames ( );
      }
    }

    private string _MiddleName = String.Empty;
    /// <summary>
    /// This property contains user's given name
    /// </summary>
    public string MiddleName
    {
      get { return this._MiddleName; }
      set
      {
        this._MiddleName = value;
      }
    }

    private string _FamilyName = String.Empty;
    /// <summary>
    /// This property contains user family name
    /// </summary>
    public string FamilyName
    {
      get { return this._FamilyName; }
      set
      {
        this._FamilyName = value;
        this.setNames ( );
      }
    }
    private string _Suffix = String.Empty;
    /// <summary>
    /// This property user contains suffix 
    /// </summary>
    public string Suffix
    {
      get { return this._Suffix; }
      set { this._Suffix = value; }
    }

    private string _CommonName = String.Empty;
    /// <summary>
    /// This property contains common name in user profile
    /// </summary>
    public string CommonName
    {
      get { return this._CommonName; }
      set
      {
        this._CommonName = value;
      }
    }
    /// <summary>
    /// This method splits the CommonName string in to parts.
    /// </summary>
    protected void setNames ( )
    {
      if ( this._FamilyName == String.Empty
        && this._GivenName == String.Empty )
      {
        return;
      }

      if ( this._Prefix != String.Empty )
      {
        this._CommonName = this._Prefix + " " + this._GivenName + " " + this._FamilyName;
        return;
      }

      this._CommonName = this._GivenName + " " + this._FamilyName;

    }//END method

    private string _EmailAddress = String.Empty;
    /// <summary>
    /// This property contains user's email address
    /// </summary>
    public string EmailAddress
    {
      get { return this._EmailAddress; }
      set { this._EmailAddress = value; }
    }

    private string _Address_1 = String.Empty;

    /// <summary>
    /// This property contains an address of an user
    /// </summary>
    public string Address_1
    {
      get
      {
        return this._Address_1;
      }
      set
      {
        this._Address_1 = value;
      }
    }

    private string _Address_2 = String.Empty;
    /// <summary>
    /// This property contains an address of an user
    /// </summary>
    public string Address_2
    {
      get
      {
        return this._Address_2;
      }
      set
      {
        this._Address_2 = value;
      }
    }

    private string _AddressCity = String.Empty;
    /// <summary>
    /// This property contains an address of an user
    /// </summary>
    public string AddressCity
    {
      get
      {
        return this._AddressCity;
      }
      set
      {
        this._AddressCity = value;
      }
    }

    private string _AddressPostCode = String.Empty;
    /// <summary>
    /// This property contains an address of an user
    /// </summary>
    public string AddressPostCode
    {
      get
      {
        return this._AddressPostCode;
      }
      set
      {
        this._AddressPostCode = value;
      }
    }

    private string _AddressState = String.Empty;
    /// <summary>
    /// This property contains an address of an user
    /// </summary>
    public string AddressState
    {
      get
      {
        return this._AddressState;
      }
      set
      {
        this._AddressState = value;
      }
    }

    private string _AddressCountry = String.Empty;
    /// <summary>
    /// This property contains a country of an user
    /// </summary>
    public string AddressCountry
    {
      get
      {
        return this._AddressCountry;
      }
      set
      {
        this._AddressCountry = value;
      }
    }

    private string _Telephone = String.Empty;
    /// <summary>
    /// This property contains a telephone of an organization
    /// </summary>
    public string Telephone
    {
      get
      {
        return this._Telephone;
      }
      set
      {
        this._Telephone = value;
      }
    }

    private string _MobilePhone = String.Empty;
    /// <summary>
    /// This property contains a mobile telephone of an user
    /// </summary>
    public string MobilePhone
    {
      get
      {
        return this._MobilePhone;
      }
      set
      {
        this._MobilePhone = value;
      }
    }

    private string _Title = String.Empty;
    /// <summary>
    /// This property defines the user's title
    /// </summary>
    public string Title
    {
      get { return this._Title; }
      set { this._Title = value; }
    }

    private DateTime _ExpiryDate = Evado.Model.EvStatics.CONST_DATE_NULL;
    /// <summary>
    /// This property defines the user's account expiry date.
    /// </summary>
    public DateTime ExpiryDate
    {
      get { return this._ExpiryDate; }
      set { this._ExpiryDate = value; }
    }

    private bool _IsAuthenticated = false;

    /// <summary>
    /// This property indicates that the user has been authenticted.
    /// </summary>
    public bool IsAuthenticated
    {
      get { return _IsAuthenticated; }
      set { _IsAuthenticated = value; }
    }

    private bool _NewAuthentication = false;

    /// <summary>
    /// This property indicates that the user has just been authenticated.
    /// </summary>
    public bool NewAuthentication
    {
      get { return _NewAuthentication; }
      set { _NewAuthentication = value; }
    }

    private string _OrgId = String.Empty;
    /// <summary>
    /// This property contains user organisation identifier
    /// </summary>
    public string OrgId
    {
      get { return this._OrgId; }
      set { this._OrgId = value; }
    }

    private string _SessionId = String.Empty;
    /// <summary>
    /// This property defines the user's session identifier
    /// </summary>
    public string SessionId
    {
      get { return this._SessionId; }
      set { this._SessionId = value; }
    }

    private string _SignoffAction = String.Empty;
    /// <summary>
    /// This property contains the string instance of the signoff action
    /// </summary>
    public string SignoffAction
    {
      get { return _SignoffAction; }
      set { _SignoffAction = value; }
    }

    private string _OrganisationName = String.Empty;
    /// <summary>
    /// This property contains user's organization name
    /// </summary>
    public string OrganisationName
    {
      get { return this._OrganisationName; }
      set { this._OrganisationName = value; }
    }

    private UserAuthenticationStates _UserAuthenticationState = UserAuthenticationStates.Null;

    /// <summary>
    /// This property contains the user authentication state.
    /// </summary>
    public UserAuthenticationStates UserAuthenticationState
    {
      get { return _UserAuthenticationState; }
      set { _UserAuthenticationState = value; }
    }

    private int _UserLoginFailureCount = 0;

    /// <summary>
    /// This property contains the user authentication failure attempts.
    /// </summary>
    public int UserLoginFailureCount
    {
      get { return _UserLoginFailureCount; }
      set { _UserLoginFailureCount = value; }
    }



    private string _UpdatedBy = String.Empty;
    /// <summary>
    /// This property contains the record update log
    /// </summary>
    public string UpdatedBy
    {
      get { return this._UpdatedBy; }
      set { this._UpdatedBy = value; }
    }

    private DateTime _UpdatedDate = Evado.Model.EvStatics.CONST_DATE_NULL;
    /// <summary>
    /// The property defines the date time stamp on update log
    /// </summary>
    public DateTime UpdatedDate
    {
      get { return _UpdatedDate; }
      set { _UpdatedDate = value; }
    }

    private string _UpdatedByUserId = String.Empty;
    /// <summary>
    /// This property contains updating user identifier
    /// </summary>
    public string UpdatedByUserId
    {
      get { return this._UpdatedByUserId; }
      set { this._UpdatedByUserId = value; }
    }

    /// <summary>
    /// this property returns a selection option object for the user.
    /// </summary>
    public Evado.Model.EvOption Option
    {
      get
      {
        return new Evado.Model.EvOption ( this.UserId, String.Format ( "{0} - {1} ", this.UserId, this.CommonName ) );
      }
    }

    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region Parameters Methods


    /// <summary>
    /// This property contains a list of application parameters.
    /// </summary>
    public List<EvObjectParameter> Parameters { get; set; }

    // ==============================================================================
    /// <summary>
    /// This method returns the parameter object if exists in the parameter list.
    /// </summary>
    /// <param name="Name">The paramater name</param>
    /// <returns>EvObjectParameter object </returns>
    //  ------------------------------------------------------------------------------
    private EvObjectParameter getParameterObject ( object Name )
    {
      //
      // If the list is null then return null 
      if ( this.Parameters == null )
      {
        this.Parameters = new List<EvObjectParameter> ( );
        return null;
      }

      String name = Name.ToString ( );
      //
      // foreach item in the list return the parameter if the names match.
      //
      foreach ( EvObjectParameter parm in this.Parameters )
      {
        if ( parm.Name.ToLower ( ) == name.ToLower ( ) )
        {
          return parm;
        }
      }

      //
      // return null if the object is not found.
      return null;
    }//END getParameter method

    // ==============================================================================
    /// <summary>
    /// This method updates the parameter list.
    /// </summary>
    /// <param name="Parameter">EvObjectParameter object</param>
    //  ------------------------------------------------------------------------------
    private string setParameter ( EvObjectParameter Parameter )
    {
      //
      // If the object is null then return 
      //
      if ( Parameter == null )
      {
        return "Null object";
      }

      //
      // Initialise the list if null
      //
      if ( this.Parameters == null )
      {
        this.Parameters = new List<EvObjectParameter> ( );
      }

      //
      // Add the item to the empty list.
      //
      if ( this.Parameters.Count == 0 )
      {
        this.Parameters.Add ( Parameter );

        return "Empty parameters: Added parameter";
      }

      //
      // foreach item in the list return the parameter if the names match.
      //
      for ( int i = 0; i < this.Parameters.Count; i++ )
      {
        if ( this.Parameters [ i ].Name == Parameter.Name )
        {
          this.Parameters [ i ] = Parameter;
          return "Updated object Value: " + Parameter.Value;
        }
      }//END iteration loop

      //
      // if the item is not found in the list then add it to the list.
      //
      this.Parameters.Add ( Parameter );

      return "Added Parameter";

    }//END getParameter method

    //=======================================================================================
    /// <summary>
    /// This method set the parameter value.
    /// </summary>
    /// <param name="Name">String: parameter key</param>
    /// <param name="Value">String: parameter value</param>
    //---------------------------------------------------------------------------------------
    public string setParameter ( object Name, object Value )
    {
      EvObjectParameter parm = new EvObjectParameter (
        Name, Value );

      return this.setParameter ( parm );
    }

    //=======================================================================================
    /// <summary>
    /// This method set the parameter value.
    /// </summary>
    /// <param name="Name">String: parameter key</param>
    /// <param name="Value">String: parameter value</param>
    //---------------------------------------------------------------------------------------
    public string setParameter ( object Name, EvDataTypes DataType, object Value )
    {
      EvObjectParameter parm = new EvObjectParameter (
        Name, DataType, Value );

      return this.setParameter ( parm );
    }

    //=======================================================================================
    /// <summary>
    /// This method returens the parameter value.
    /// </summary>
    /// <param name="Name">String: parameter key</param>
    /// <returns>int value</returns>
    //---------------------------------------------------------------------------------------
    public String getParameter ( object Name )
    {
      var parameter = this.getParameterObject ( Name );

      if ( parameter != null )
      {
        return parameter.Value;
      }

      return string.Empty;
    }

    //=======================================================================================
    /// <summary>
    /// This method returens the parameter value.
    /// </summary>
    /// <param name="Name">String: parameter key</param>
    /// <returns>int value</returns>
    //---------------------------------------------------------------------------------------
    public bool getBooleanParameter ( object Name )
    {
      var parameter = this.getParameterObject ( Name );

      if ( parameter.Value == "true" || parameter.Value == "yes" || parameter.Value == "1" )
      {
        return true;
      }

      return false;
    }

    //=======================================================================================
    /// <summary>
    /// This method returens the parameter value.
    /// </summary>
    /// <param name="Name">String: parameter key</param>
    /// <param name="Value">String: parameter value</param>
    //---------------------------------------------------------------------------------------
    public void setBooleanParameter ( object Name, bool Value )
    {
      string value = Value.ToString ( );
      EvObjectParameter parm = new EvObjectParameter (
        Name, value );
      parm.DataType = Evado.Model.EvDataTypes.Boolean;

      this.setParameter ( parm );
    }

    //=======================================================================================
    /// <summary>
    /// This method returens the parameter value.
    /// </summary>
    /// <param name="Name">Object: parameter key</param>
    /// <returns>int value</returns>
    //---------------------------------------------------------------------------------------
    public T getParameter<T> ( object Name )
    {
      var parameter = this.getParameterObject ( Name );
      T iValue = default ( T );

      if ( Evado.Model.EvStatics.tryParseEnumValue<T> ( parameter.Value, out iValue ) == true )
      {
        return iValue;
      }

      return iValue;
    }


    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region Class Domain Group Methods


    private List<String> _DomainGroups = new List<string> ( );

    /// <summary>
    /// This property contains a list of the domain groups.
    /// </summary>
    public List<String> DomainGroups
    {
      get { return _DomainGroups; }
      set
      {
        _DomainGroups = value;
      }
    }

    /// <summary>
    /// This property contains a list of the domain groups.
    /// </summary>
    public String DomainGroupNames
    {
      get
      {
        //
        // Define the local variables.
        //
        string domainGroups = String.Empty;

        //
        // Iterate through the domain group generating the string
        //
        if ( this._DomainGroups.Count > 0 )
        {
          foreach ( string group in this._DomainGroups )
          {
            if ( domainGroups != string.Empty )
            {
              domainGroups += ";";
            }
            domainGroups += group;

          }//END interation loop

        }//END domain group has members.

        //
        // return the string of domain groups.
        //
        return domainGroups;
      }
      set
      {
        string [ ] arrRoles = value.Split ( ';' );

        foreach ( string group in arrRoles )
        {
          this._DomainGroups.Add ( group );

        }//END interation loop
      }
    }


    // =====================================================================================
    /// <summary>
    /// This class gets the study profile for the user.
    /// </summary>
    /// <returns>string: a user profile string</returns>
    // -------------------------------------------------------------------------------------
    public string getUserProfile ( )
    {
      //
      // Initialize a return text
      //
      StringBuilder sbText = new StringBuilder ( );

      //
      // Append user profile elements to the return text
      //
      sbText.AppendLine ( "User Profile for UserId: " + this.UserId );
      if ( this.Password != String.Empty )
      {
        sbText.AppendLine ( "Password: " + this.Password );
      }
      sbText.AppendLine ( "Prefix: " + this.Prefix );
      sbText.AppendLine ( "GivenName: " + this.GivenName );
      sbText.AppendLine ( "FamilyName: " + this.FamilyName );
      sbText.AppendLine ( "Name: " + this.CommonName );
      sbText.AppendLine ( "Title: " + this.Title );
      sbText.AppendLine ( "EmailAddress: " + this.EmailAddress );
      if ( this.DomainGroupNames != String.Empty )
      {
        sbText.AppendLine ( "DomainGroupNames: " + this.DomainGroupNames );
      }

      if ( this.AdsCustomerGroup != String.Empty )
      {
        sbText.AppendLine ( "ADS Customer Group: " + this.AdsCustomerGroup );
      }

      if ( this.AdsRoleGroup != String.Empty )
      {
        sbText.AppendLine ( "ADS Role Group: " + this.AdsRoleGroup );
      }

      // 
      // Return the text.
      // 
      return sbText.ToString ( );
    }//END getUserProfile class

    // ==================================================================================
    /// <summary>
    /// This method returns True if the group name is contains in the user's domain groups
    /// </summary>
    /// <returns>Bool: True if group has been found.</returns>
    // ----------------------------------------------------------------------------------
    public void initialiseDomainGroups ( )
    {
      this._DomainGroups = new List<string> ( );
    }

    // ==================================================================================
    /// <summary>
    /// This method returns True if the group name is contains in the user's domain groups
    /// </summary>
    /// <param name="GroupName">String: name of a group.</param>
    /// <returns>Bool: True if group has been found.</returns>
    // ----------------------------------------------------------------------------------
    public bool hasGroupName ( String GroupName )
    {
      if ( GroupName.Contains ( ";" ) == false )
      {
        foreach ( String groupName in this._DomainGroups )
        {
          if ( groupName.ToUpper ( ) == GroupName.ToUpper ( ) )
          {
            return true;
          }
        }
      }
      else
      {
        //
        // Process a delimited list of domain names.
        //
        foreach ( String groupName in this._DomainGroups )
        {
          String group = GroupName.ToUpper ( );
          if ( group.Contains ( groupName.ToUpper ( ) ) == true )
          {
            return true;
          }
        }
      }

      return false;

    }//END hasGroup

    // ==================================================================================
    /// <summary>
    /// This method adds array of domain groups to the DomainGroups list.
    /// </summary>
    /// <param name="DomainGroups">String array of domain groups.</param>
    // ----------------------------------------------------------------------------------
    public void addDomainGroups ( String [ ] DomainGroups )
    {

      foreach ( String groupName in DomainGroups )
      {
        if ( this.hasGroupName ( groupName ) == false )
        {
          this.addDomainGroup ( groupName );
        }
      }
    }//END addDomainGroups method

    // ==================================================================================
    /// <summary>
    /// This method adds array of domain groups to the DomainGroups list.
    /// </summary>
    /// <param name="GroupName">String: Group name.</param>
    // ----------------------------------------------------------------------------------
    public void addDomainGroup ( String GroupName )
    {
      //
      // if the group does not exist add it.
      //
      if ( this.hasGroupName ( GroupName ) == true )
      {
        return;
      }

      this._DomainGroups.Add ( GroupName );

    }//END addDomainGroups method

    // ==================================================================================
    /// <summary>
    /// This method adds array of domain groups to the DomainGroups list.
    /// </summary>
    /// <param name="GroupName">String: Group name.</param>
    // ----------------------------------------------------------------------------------
    public void deleteDomainGroup ( String GroupName )
    {
      //
      // Iterate through the domain group list to find the matching group and delete it.
      //
      for ( int count = 0; count < this._DomainGroups.Count; count++ )
      {
        if ( this._DomainGroups [ count ].ToLower ( ) == GroupName.ToLower ( ) )
        {
          this._DomainGroups.RemoveAt ( count );
          count--;
        }
      }

    }//END addDomainGroups method


    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region General Methods

    /// <summary>
    /// This property contains customer user group the user belongs to.
    /// </summary>
    public string AdsCustomerGroup
    {
      get
      {
        foreach ( String group in this.DomainGroups )
        {
          if ( group.Contains ( "CU_" ) == true )
          {
            return group;
          }
        }
        return string.Empty;
      }
    }

    /// <summary>
    /// This property contains the domain role group the user belongs to.
    /// </summary>
    public string AdsRoleGroup
    {
      get
      {
        foreach ( String group in this.DomainGroups )
        {
          if ( group.Contains ( "ROL_" ) == true )
          {
            return group;
          }
        }
        return string.Empty;
      }
    }

    //  ==================================================================================	
    /// <summary>
    ///  This method removes the domain name from a use identifier.
    /// </summary>
    /// <param name="DomainUserId">string: String containing the user's domain name</param>
    /// <returns>String: A string containing the user identifier.</returns>
    /// <remarks>
    /// This method consists of the following steps:
    /// 
    /// 1. Create an array of domain values for the activity
    /// 
    /// 2. If domain exits, return administrator domain for user identifier
    /// 
    /// 3. If not, exit the user idenfier
    /// </remarks>
    //  ---------------------------------------------------------------------------------
    public static String removeUserIdDomainName ( string DomainUserId )
    {
      // 
      // Create an array of domain values for the activity.
      // 
      string [ ] arrUserId = DomainUserId.Split ( '\\' );

      // 
      // Check to see if the activity is an administrator.  If so set the activity id to Administrator.
      // 
      if ( arrUserId.Length > 1 )
      {
        return arrUserId [ 1 ];
      }

      //
      // If there is no domain exit the user id
      //
      return DomainUserId;

    }//END removeDomainName method

    /*
    //  ==================================================================================	
    /// <summary>
    /// This class defines value of the user profile
    /// </summary>
    /// <param name="fieldName">UserProfileClassFieldNames: an object of user profile class file names</param>
    /// <param name="value">String: a value for updating into the user profile</param>
    /// <returns>int: return zero number</returns>
    /// <remarks>
    /// This class consists of the following steps:
    /// 
    /// 1. Switch to the retrieving field name of user profile 
    /// 
    /// 2. Update the related field name with value
    /// </remarks>
    //  ---------------------------------------------------------------------------------
    public Evado.Model.EvEventCodes setValue ( UserProfileFieldNames fieldName, String value )
    {
      //
      // Update the value of user profile's field name based retrieving fieldname
      //
      switch ( fieldName )
      {
        case UserProfileFieldNames.UserId:
          this.UserId = value;
          break;
        case UserProfileFieldNames.Password:
          this.Password = value;
          break;
        case UserProfileFieldNames.OrgId:
          this.OrgId = value;
          break;
        case UserProfileFieldNames.ActiveDirectoryUserId:
          this.ActiveDirectoryUserId = value;
          break;
        case UserProfileFieldNames.CommonName:
          this.CommonName = value;
          this.UserCommonName = value;
          break;
        case UserProfileFieldNames.Title:
          this.Title = value;
          break;
        case UserProfileFieldNames.Email_Address:
          this.EmailAddress = value;
          break;
      }//End switch field name
      return 0;
    }//END Evado.Model.EvEventCodes method
    */

    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

  }//END EvUserProfile method

} //END namespace Evado.Clinical.Model