/***************************************************************************************
 * <copyright file="Evado.UniForm.Model\AbstractedPage.cs" company="EVADO HOLDING PTY. LTD.">
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
  /// This class defines the client page EuCommand object structure.
  /// </summary>
  [Serializable]
  public partial class EuCommand
  {
    #region Class initialisation methods

    /// <summary>
    /// This method to initialise the class.
    /// </summary>
    public EuCommand ( )
    {
    }

    /// <summary>
    /// This method to initialise the class.
    /// </summary>
    /// <param name="Title">String: EuCommand title</param>
    /// <param name="CommandType">CommandType: enumerated value</param>
    /// <param name="ApplicationId">String: application identifier</param>
    /// <param name="ApplicationObject">String: Application object identifier</param>
    /// <param name="ApplicationMethod">EuMethods: method enumerated value</param>
    public EuCommand ( String Title, EuCommandTypes EuCommandType, String ApplicationId, String ApplicationObject, EuMethods ApplicationMethod )
    {
      this._Id = Guid.NewGuid ( );
      this._Title = Title;
      this._Type = EuCommandType;
      this._ApplicationId = ApplicationId;
      this._Object = ApplicationObject;
      this._Method = ApplicationMethod;
    }

    /// <summary>
    /// this method to initialise the class.
    /// </summary> 
    /// <param name="Title">String: EuCommand title</param>
    /// <param name="ApplicationId">String: application identifier</param>
    /// <param name="ApplicationObject">String: Application object identifier</param>
    /// <param name="ApplicationMethod">String method emerated value</param>
    public EuCommand ( String Title, String ApplicationId, object ApplicationObject, EuMethods ApplicationMethod )
    {
      this._Id = Guid.NewGuid ( );
      this._Title = Title;
      this._Type = EuCommandTypes.Normal_Command;
      this._ApplicationId = ApplicationId;
      this._Object = ApplicationObject.ToString ( );
      this._Method = ApplicationMethod;
    }
    #endregion

    #region Class property list

    private Guid _Id = Guid.Empty;
    /// <summary>
    ///  This property contains an identifier for the EuCommand object.
    /// </summary>
    public Guid Id
    {
      get { return this._Id; }
      set { this._Id = value; }
    }

    /// <summary>
    /// This property contains the method parameter that will be used to call  the hosted application.
    /// </summary>
    [JsonProperty ( "h" )]
    public List<EuParameter> Header { get; set; }

    private String _Title = String.Empty;

    /// <summary>
    /// This property contains the title of the EuCommand.
    /// </summary>
    public String Title
    {
      get { return _Title; }
      set { _Title = value.Trim ( ); }
    }

    private EuCommandTypes _Type = EuCommandTypes.Null;
    /// <summary>
    /// This property contains the EuCommand type for this EuCommand object.
    /// </summary>
    [JsonProperty ( "t" )]
    public EuCommandTypes Type
    {
      get { return _Type; }
      set { _Type = value; }
    }

    private String _ApplicationId = String.Empty;

    /// <summary>
    /// This property contains the application the EuCommand is call.
    /// </summary>
    [JsonProperty ( "a" )]
    public String ApplicationId
    {
      get { return _ApplicationId; }
      set { _ApplicationId = value.Trim ( ); }
    }

    private String _Object = String.Empty;

    /// <summary>
    /// This property contains the application object the EuCommand is call.
    /// </summary>
    [JsonProperty ( "o" )]
    public String Object
    {
      get { return _Object; }
      set { _Object = value.Trim ( ); }
    }

    private EuMethods _Method = EuMethods.Null;

    /// <summary>
    /// This property contains the application method parameter that will be called.
    /// </summary>
    [JsonProperty ( "m" )]
    public EuMethods Method
    {
      get { return this._Method; }
      set { this._Method = value; }
    }

    private List<EuParameter> _ParameterList = new List<EuParameter> ( );
    /// <summary>
    /// This property contains a list of Parameter object.
    /// </summary>
    [JsonProperty ( "prm" )]
    public List<EuParameter> Parameters
    {
      get
      {
        return _ParameterList;

      }//End get statement.

      set
      {
        _ParameterList = value;

      }//END set statement

    }//END property.

    /// <summary>
    /// This property contains the commands page identifier.
    /// </summary>
    [JsonIgnore]
    public object PageId
    {
      get
      {
        return this.GetParameter ( EuCommandParameters.Page_Id );
      }
      set
      {
        this.AddParameter ( EuCommandParameters.Page_Id, value );
      }
    }

    /// <summary>
    /// This property contains the command objects Guid identifier.
    /// </summary>
    [JsonIgnore]
    public Guid OBjectGuid
    {
      get
      {
        String value = this.GetParameter ( EuCommandParameters.Guid );

        if ( value != String.Empty )
        {
          return new Guid ( value );
        }

        //
        // Else return empty Guid 
        //
        return Guid.Empty;
      }
      set
      {
        this.AddParameter ( EuCommandParameters.Guid, value );
      }
    }

    /// <summary>
    /// This property is used to indicate that this a new user authentication.
    /// </summary>
    [JsonIgnore]
    public bool IsNewUser { get; set; }


    // ==================================================================================
    /// <summary>
    /// This method sets the custom EuCommand parameter value and and EuMethod enumeration.
    /// </summary>
    // ---------------------------------------------------------------------------------
    [JsonIgnore]
    public EuMethods CustomMethod
    {
      get
      {
        String value = this.GetParameter ( EuCommandParameters.Custom_Method );

        if ( value != String.Empty )
        {
          return Evado.Model.EvStatics.parseEnumValue<EuMethods> ( value );
        }

        //
        // Else return not selected state.
        //
        return EuMethods.Null;

      }
      set
      {
        //
        // get the string value of the parameter list.
        //
        String stValue = value.ToString ( );

        this.AddParameter ( EuCommandParameters.Custom_Method, stValue );
      }
    }

    // ==================================================================================
    /// <summary>
    /// This property get the device identifier as a string
    /// </summary>
    // ---------------------------------------------------------------------------------
    [JsonIgnore]
    public String DeviceId
    {
      get
      {
        return this.GetHeaderValue ( Evado.UniForm.Model.EuCommandHeaderParameters.DeviceId );
      }
      set
      {
        this.SetHeaderValue ( Evado.UniForm.Model.EuCommandHeaderParameters.DeviceId, value );
      }
    }

    // ==================================================================================
    /// <summary>
    /// This property get the device name as a string
    /// </summary>
    /// <returns>String: device name</returns>
    // ---------------------------------------------------------------------------------
    [JsonIgnore]
    public String DeviceName
    {
      get
      {
        return this.GetHeaderValue ( Evado.UniForm.Model.EuCommandHeaderParameters.DeviceName );
      }
      set
      {
        this.SetHeaderValue ( Evado.UniForm.Model.EuCommandHeaderParameters.DeviceName, value );
      }
    }

    // ==================================================================================
    /// <summary>
    /// This property get the device operation system as a string
    /// </summary>
    /// <returns>String: device name</returns>
    // ---------------------------------------------------------------------------------
    [JsonIgnore]
    public String OSVersion
    {
      get
      {
        return this.GetHeaderValue ( Evado.UniForm.Model.EuCommandHeaderParameters.OSVersion );
      }
      set
      {
        this.SetHeaderValue ( Evado.UniForm.Model.EuCommandHeaderParameters.OSVersion, value );
      }
    }

    // ==================================================================================
    /// <summary>
    /// This property get the device client URl system as a string
    /// </summary>
    /// <returns>String: device name</returns>
    // ---------------------------------------------------------------------------------
    [JsonIgnore]
    public String ClientUrl
    {
      get
      {
        return this.GetHeaderValue ( Evado.UniForm.Model.EuCommandHeaderParameters.Client_Url );
      }
      set
      {
        this.SetHeaderValue ( Evado.UniForm.Model.EuCommandHeaderParameters.Client_Url, value );
      }
    }
    // ==================================================================================
    /// <summary>
    /// This property get the user URl system as a string
    /// </summary>
    /// <returns>String: device name</returns>
    // ---------------------------------------------------------------------------------
    [JsonIgnore]
    public String UserUrl
    {
      get
      {
        return this.GetHeaderValue ( Evado.UniForm.Model.EuCommandHeaderParameters.User_Url );
      }
      set
      {
        this.SetHeaderValue ( Evado.UniForm.Model.EuCommandHeaderParameters.User_Url, value );
      }
    }

    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region Class compare methods

    // ==================================================================================
    /// <summary>
    /// This method compares the existing EuCommand object with a passed EuCommand object.  
    /// To determine if they are the same EuCommand.
    /// </summary>
    /// <remarks> 
    /// This method consists of following steps. 
    /// 
    /// 1. EuCommand Id's match return true
    /// 
    /// 2. EuCommand AppId, Object, Method and Titles match return true.
    /// 
    /// 3. return false
    /// 
    /// </remarks>
    /// <param name="PageCommand">Evado.UniForm.Model.EuCommand object</param>
    /// <returns>Bool: true = matching EuCommand</returns>
    // ---------------------------------------------------------------------------------
    public bool isCommand ( EuCommand PageCommand )
    {
      //
      // Initialise the methods variables and objects.
      //
      String pageId = PageCommand.GetPageId ( );
      String currentPageId = this.GetPageId ( );
      Guid dataGuid = PageCommand.GetGuid ( );
      Guid currentDataGuid = this.GetGuid ( );

      //
      // if the EuCommand identifiers match same EuCommand return true;
      //
      if ( this._Id == PageCommand.Id )
      {
        return true;
      }

      if ( PageCommand.Method == EuMethods.Custom_Method )
      {
        EuMethods method = PageCommand.getCustomMethod ( );

        //
        // If the AppId, Object, method and title match then same EuCommand return true.
        //
        if ( this._ApplicationId == PageCommand.ApplicationId
          && this._Object == PageCommand.Object
          && this._Method == method
          && pageId == currentPageId )
        {
          return true;
        }

        return false;
      }

      //
      // If the AppId, Object, method and title match then same EuCommand return true.
      //
      if ( this._ApplicationId == PageCommand.ApplicationId
        && this._Object == PageCommand.Object
        && this._Method == PageCommand.Method
        && pageId == currentPageId )
      {
        return true;
      }

      return false;

    }//END isCommand method.

    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region Class header methods

    // ==================================================================================
    /// <summary>
    /// This method add a parameter to the EuCommand's header list.
    /// </summary>
    /// <param name="Name">EuCommandHeaderParameters: the name enumerated value.</param>
    /// <param name="Value">String: the value of the parameter.</param>
    /// <remarks> 
    /// This method consists of following steps. 
    /// 
    /// 1. Iterate throgh parameter list
    /// 
    /// 2. If parameter Name is equal to Name update parameter value
    /// 
    /// 3. Else do nothing 
    /// 
    /// </remarks>
    // ---------------------------------------------------------------------------------
    public void SetHeaderValue ( EuCommandHeaderParameters Name, String Value )
    {
      if ( this.Header == null )
      {
        this.Header = new List<EuParameter> ( );
      }

      //
      // Search the parmeters for existing parameters.
      // and exit if update the value.
      // 
      foreach ( EuParameter parameter in this.Header )
      {
        if ( parameter.Name == Name.ToString ( ) )
        {
          parameter.Value = Value;

          return;
        }
      }

      this.Header.Add ( new EuParameter ( Name.ToString ( ), Value ) );

    }//END AddParameter method

    // ==================================================================================
    /// <summary>
    /// This method add a parameter to the EuCommand's parameter list..
    /// </summary>
    /// <param name="Name">CommandHeaderElements: the name of the parameter.</param>
    /// <returns> String value of the header element</returns>
    /// <remarks>
    /// This method consists of following steps. 
    /// 
    /// 1. Iterate through parameter list then Search the parmeters for existing parameters
    ///     and exit if update the value.
    ///     
    /// 2. If parameter Name is equal to Name then return parameter Value.
    /// 
    /// 3.  Else return empty string 
    /// 
    /// </remarks>
    // ---------------------------------------------------------------------------------
    public String GetHeaderValue ( EuCommandHeaderParameters Name )
    {
      if ( this.Header == null )
      {
        return String.Empty;
      }

      //
      // Search the parmeters for existing parameters.
      // and exit if update the value.
      // 
      foreach ( EuParameter parameter in this.Header )
      {

        //
        // If parameter Name is equal to Name then return parameter Value.
        //
        if ( parameter.Name.ToLower ( ) == Name.ToString ( ).ToLower ( ) )
        {
          return parameter.Value;
        }
      }//END parameter iteration

      //
      // Else return empty string 
      //
      return String.Empty;

    }//END AddHeader method

    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region Class parameter methods

    // ==================================================================================
    /// <summary>
    /// This method get the client host string
    /// </summary>
    /// <returns> String: containing the host url.</returns>
    //  ---------------------------------------------------------------------------------
    public String GetClientHostUrl ( )
    {
      //
      // if the header exists and there are parameters return the host URL
      //
      if ( this.Header != null )
      {
        if ( this.Header.Count > 0 )
        {
          foreach ( EuParameter prm in this.Header )
          {
            if ( prm.Name == EuCommandHeaderParameters.Client_Url.ToString ( ) )
            {
              return prm.Value;
            }
          }//END prm iteration 
        }
      }

      return String.Empty;
    }//END AddParameter method

    // ==================================================================================
    /// <summary>
    /// This method add a page type parameter to the EuCommand's parameter list..
    /// </summary>
    /// <param name="Value">String: the value of the parameter.</param>
    /// <remarks>
    /// This method consists of following steps.
    /// 
    /// 1. Iterate through a parameter list then search the parmeters for existing parameters.
    ///    and exit if update the value.
    ///    
    /// 2. Iterate through the existing parameters and update the parameter matching the 
    ///    standrd parameter.
    /// 
    /// </remarks>
    // ---------------------------------------------------------------------------------
    public void SetPageId ( object Value )
    {
      //
      // get the string value of the parameter list.
      //
      String value = Value.ToString ( );

      this.AddParameter ( EuCommandParameters.Page_Id, value );

    }//END AddPageType method

    // ==================================================================================
    /// <summary>
    /// This method add a parameter to the EuCommand's page type if it exists.
    /// </summary>
    /// <returns> String value of the header element</returns>
    /// <remarks>
    /// This method consists of following steps. 
    /// 
    /// 1. Iterate throgh a list parameter then search the parameters for existing parameters
    /// and exit if update the value.
    /// 
    /// 2. If parameter Name is equal to Name then return parameter Value. 
    /// 
    /// 3. Else return empty string. 
    /// 
    /// </remarks>
    /// 
    // ---------------------------------------------------------------------------------
    public String GetPageId ( )
    {
      return this.GetParameter ( EuCommandParameters.Page_Id );

    }//END GetPageType method.

    // ==================================================================================
    /// <summary>
    /// This method conmpares page identifiers and returns true if they match..
    /// </summary>
    /// <param name="PageId">Enumerated value representing a page identifier.</param>
    /// <returns> Bool: ture for a match</returns>
    // ---------------------------------------------------------------------------------
    public bool HasPageId ( Object PageId )
    {
      string pageId = this.GetParameter ( EuCommandParameters.Page_Id );
      if ( pageId == PageId.ToString ( ) )
      {
        return true;
      }
      return false;

    }//END GetPageType method.

    // ==================================================================================
    /// <summary>
    /// This method add a parameter to the EuCommand's page type if it exists.
    /// </summary>
    /// <returns> String value of the header element</returns>
    /// <remarks>
    /// This method consists of following steps. 
    /// 
    /// 1. Iterate throgh a list parameter then search the parameters for existing parameters
    /// and exit if update the value.
    /// 
    /// 2. If parameter Name is equal to Name then return parameter Value. 
    /// 
    /// 3. Else return empty string. 
    /// 
    /// </remarks>
    /// 
    // ---------------------------------------------------------------------------------
    public EnumeratedList GetPageId<EnumeratedList> ( )
    {
      String value = this.GetParameter ( EuCommandParameters.Page_Id );

      return Evado.Model.EvStatics.parseEnumValue<EnumeratedList> ( value );
    }//END GetPageType method.


    // ==================================================================================
    /// <summary>
    /// This method add a parameter to the EuCommand's parameter list..
    /// </summary>
    /// <param name="Value">Guid: the value of the parameter.</param>
    // ---------------------------------------------------------------------------------
    public void SetGuid ( Guid Value )
    {
      this.OBjectGuid = Value;

    }//END AddParameterGuid method

    // ==================================================================================
    /// <summary>
    /// This method gets the Guid parameter value.
    /// </summary>
    /// <returns>GUID value</returns>
    // ---------------------------------------------------------------------------------
    public Guid GetGuid ( )
    {
      return this.OBjectGuid;

    }//END getGuidParameter method 

    // ==================================================================================
    /// <summary>
    /// This method adds a parameter to the EuCommand's parameter list..
    /// </summary>
    /// <param name="Name">String: The name of the parameter.</param>
    /// <param name="Value">String: The value of the parameter.</param>
    //  ---------------------------------------------------------------------------------
    public void AddParameter ( String Name, String Value )
    {
      if ( Name == null
        || Value == null )
      {
        return;
      }
      //
      // get the string value of the parameter list.
      //
      String name = Name.ToString ( );
      name = name.Trim ( );
      String value = Value.ToString ( );

      foreach ( EuParameter parameter in this._ParameterList )
      {
        if ( parameter.Name == name )
        {
          parameter.Value = value;

          return;
        }
      }

      this._ParameterList.Add ( new EuParameter ( name, value ) );

    }//END AddParameter method

    // ==================================================================================
    /// <summary>
    /// This method adds a parameter to the EuCommand's parameter list..
    /// </summary>
    /// <param name="Name">String: The name of the parameter.</param>
    /// <param name="Value">String: The value of the parameter.</param>
    //  ---------------------------------------------------------------------------------
    public void AddParameter ( object Name, object Value )
    {
      //
      // get the string value of the parameter list.
      //
      String name = Name.ToString ( );
      name = name.Trim ( );
      String value = Value.ToString ( );

      foreach ( EuParameter parameter in this._ParameterList )
      {
        if ( parameter.Name == name )
        {
          parameter.Value = value;

          return;
        }
      }

      this._ParameterList.Add ( new EuParameter ( name, value ) );

    }//END AddParameter method

    // ==================================================================================
    /// <summary>
    /// This method adds a parameter to the EuCommand's parameter list..
    /// </summary>
    /// <param name="Name">String: The name of the parameter.</param>
    /// <param name="Value">String: The value of the parameter.</param>
    //  ---------------------------------------------------------------------------------
    public void AddParameter ( String Name, object Value )
    {
      //
      // get the string value of the parameter list.
      //
      String name = Name.ToString ( );
      name = name.Trim ( );
      String value = Value.ToString ( );

      foreach ( EuParameter parameter in this._ParameterList )
      {
        if ( parameter.Name == name )
        {
          parameter.Value = value;

          return;
        }
      }

      this._ParameterList.Add ( new EuParameter ( name, value ) );

    }//END AddParameter method

    // ==================================================================================
    /// <summary>
    /// This method adds a parameter to the EuCommand's parameter list..
    /// </summary>
    /// <param name="Name">String: The name of the parameter.</param>
    /// <param name="Value">String: The value of the parameter.</param>
    //  ---------------------------------------------------------------------------------
    public void AddParameter ( EuCommandParameters Name, object Value )
    {
      //
      // get the string value of the parameter list.
      //
      String name = Name.ToString ( );
      name = name.Trim ( );
      String value = Value.ToString ( );

      foreach ( EuParameter parameter in this._ParameterList )
      {
        if ( parameter.Name == name )
        {
          parameter.Value = value;

          return;
        }
      }

      this._ParameterList.Add ( new EuParameter ( name, value ) );

    }//END AddParameter method

    // ==================================================================================
    /// <summary>
    /// This method test to see if the parameter is in the list.
    /// </summary>
    /// <param name="Name">String: the name of the parameter.</param>
    /// <returns>True: parameter exists</returns>
    // ---------------------------------------------------------------------------------
    public bool hasParameter ( EuCommandParameters Name )
    {
      //
      // get the string value of the parameter list.
      //
      String name = Name.ToString ( );
      name = name.Trim ( );

      //
      // Iterate through the parameters to get the selectev value.
      //
      foreach ( EuParameter parameter in this._ParameterList )
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

    }//END hasParameter method

    // ==================================================================================
    /// <summary>
    /// This method test to see if the parameter is in the list.
    /// </summary>
    /// <param name="Name">String: the name of the parameter.</param>
    /// <returns>True: parameter exists</returns>
    // ---------------------------------------------------------------------------------
    public bool hasParameter ( String Name )
    {
      //
      // get the string value of the parameter list.
      //
      String name = Name.ToString ( );
      name = name.Trim ( );

      //
      // Iterate through the parameters to get the selectev value.
      //
      foreach ( EuParameter parameter in this._ParameterList )
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

    }//END hasParameter method

    // ==================================================================================
    /// <summary>
    /// This method test to see if the parameter is in the list.
    /// </summary>
    /// <param name="Name">String: the name of the parameter.</param>
    /// <returns>True: parameter exists</returns>
    // ---------------------------------------------------------------------------------
    public bool hasParameter ( object Name )
    {
      //
      // get the string value of the parameter list.
      //
      String name = Name.ToString ( );
      name = name.Trim ( );

      //
      // Iterate through the parameters to get the selectev value.
      //
      foreach ( EuParameter parameter in this._ParameterList )
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

    }//END hasParameter method

    // ==================================================================================
    /// <summary>
    /// This method adds a parameter to the EuCommand's parameter list..
    /// </summary>
    /// <param name="Name">String: The name of the parameter.</param>
    //  ---------------------------------------------------------------------------------
    public void DeleteParameter ( EuFieldParameters Name )
    {
      //
      // get the string value of the parameter list.
      //
      String name = Name.ToString ( );
      name = name.Trim ( );

      for ( int count = 0; count < this._ParameterList.Count; count++ )
      {
        EuParameter parameter = this._ParameterList [ count ];

        if ( parameter.Name == name )
        {
          this._ParameterList.RemoveAt ( count );
          count--;
        }
      }
    }

    // ==================================================================================
    /// <summary>
    /// This method adds a parameter to the EuCommand's parameter list..
    /// </summary>
    /// <param name="Name">String: The name of the parameter.</param>
    //  ---------------------------------------------------------------------------------
    public void DeleteParameter ( String Name )
    {
      //
      // get the string value of the parameter list.
      //
      String name = Name.ToString ( );
      name = name.Trim ( );

      for ( int count = 0; count < this._ParameterList.Count; count++ )
      {
        EuParameter parameter = this._ParameterList [ count ];

        if ( parameter.Name == name )
        {
          this._ParameterList.RemoveAt ( count );
          count--;
        }
      }
    }

    // ==================================================================================
    /// <summary>
    /// This method gets a parameter value.
    /// </summary>
    /// <param name="Name">String: the name of the parameter.</param>
    /// <returns> String value of the header element</returns>
    // ---------------------------------------------------------------------------------
    public String GetParameter ( EuCommandParameters Name )
    {
      //
      // get the string value of the parameter list.
      //
      String name = Name.ToString ( );
      name = name.Trim ( );

      //
      // Iterate through the parameters to get the selectev value.
      //
      foreach ( EuParameter parameter in this._ParameterList )
      {
        if ( parameter.Name == name )
        {
          return parameter.Value;
        }
      }

      return string.Empty;

    }//END GetParameter method

    // ==================================================================================
    /// <summary>
    /// This method gets a parameter value.
    /// </summary>
    /// <param name="Name">String: the name of the parameter.</param>
    /// <returns> String value of the header element</returns>
    // ---------------------------------------------------------------------------------
    public String GetParameter ( object Name )
    {
      //
      // get the string value of the parameter list.
      //
      String name = Name.ToString ( );
      name = name.Trim ( );

      //
      // Iterate through the parameters to get the selectev value.
      //
      foreach ( EuParameter parameter in this._ParameterList )
      {
        if ( parameter.Name == name )
        {
          return parameter.Value;
        }
      }

      return String.Empty;

    }//END GetParameter method

    // ==================================================================================
    /// <summary>
    /// This method gets a parameter value.
    /// </summary>
    /// <param name="Name">String: the name of the parameter.</param>
    /// <returns> String value of the header element</returns>
    // ---------------------------------------------------------------------------------
    public Guid GetParameterAsGuid ( object Name )
    {
      //
      // get the string value of the parameter list.
      //
      String name = Name.ToString ( );
      name = name.Trim ( );

      //
      // Iterate through the parameters to get the selectev value.
      //
      foreach ( EuParameter parameter in this._ParameterList )
      {
        if ( parameter.Name == name )
        {
          return Evado.Model.EvStatics.getGuid ( parameter.Value );
        }
      }

      return Guid.Empty;

    }//END GetParameter method

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
      // Iterate through the parameters to get the selectev value.
      //
      foreach ( EuParameter parameter in this._ParameterList )
      {
        if ( parameter.Name.Trim ( ) == Name.Trim ( ) )
        {
          return parameter.Value;
        }
      }

      return string.Empty;

    }//END AddParameter method

    // ==================================================================================
    /// <summary>
    /// This method add a parameter to the EuCommand's parameter list..
    /// </summary>
    /// <param name="Name">GroupParameterList: the name of the parameter.</param>
    /// <returns >String value</returns>
    //  ---------------------------------------------------------------------------------
    public EnumeratedList GetParameter<EnumeratedList> ( object Name )
    {
      //
      // get the string value of the parameter list.
      //
      String name = Name.ToString ( );
      name = name.Trim ( );

      //
      // get the value
      //
      string value = this.GetParameter ( name );

      try
      {
        return Evado.Model.EvStatics.parseEnumValue<EnumeratedList> ( value );
      }
      catch
      {
        // Try and return enumeration 'Null' value
        try
        {
          return Evado.Model.EvStatics.parseEnumValue<EnumeratedList> ( "Null" );
        }
        catch
        {
          return default ( EnumeratedList );
        }
      }

    }//END GetParameter method

    // ==================================================================================
    /// <summary>
    /// This method sets the custom EuCommand parameter value.
    /// </summary>
    /// <param name="Value"> EuMethods enumeration
    /// of application methods
    /// </param>
    // ---------------------------------------------------------------------------------
    public void setCustomMethod ( EuMethods Value )
    {
      //
      // get the string value of the parameter list.
      //
      String value = Value.ToString ( );

      this.AddParameter ( EuCommandParameters.Custom_Method, value );
    }

    // ==================================================================================
    /// <summary>
    /// This method gets the custom EuCommand parameter value.
    /// </summary>
    /// <returns>EuMethods enumeration </returns>
    /// <remarks>
    /// This method cosists of following steps. 
    /// 
    /// 1. Iterating through the parameter list and returning the method value for the custom EuCommand parameter.
    /// 
    /// 2. Else return not selected state.
    /// 
    /// </remarks>
    // ---------------------------------------------------------------------------------
    public EuMethods getCustomMethod ( )
    {
      String value = this.GetParameter ( EuCommandParameters.Custom_Method );

      if ( value != String.Empty )
      {
        return Evado.Model.EvStatics.parseEnumValue<EuMethods> ( value );
      }

      //
      // Else return not selected state.
      //
      return EuMethods.Null;
    }

    // ==================================================================================
    /// <summary>
    /// This method sets the short title EuCommand parameter.
    /// </summary>
    /// <param name="Value">The short title content.</param>
    // ---------------------------------------------------------------------------------
    public void setShortTitleParameter ( String Value )
    {
      //
      // get the string value of the parameter list.
      //
      String value = Value.ToString ( );

      this.AddParameter ( EuCommandParameters.Short_Title, value );
    }

    // ==================================================================================
    /// <summary>
    /// This method sets the EuCommand title to the short title value.
    /// </summary>
    // ---------------------------------------------------------------------------------
    public void setShortTitle ( )
    {
      String value = this.GetParameter ( EuCommandParameters.Short_Title );

      //
      // If the title is less than 20 characters and there is not short title 
      // then do not shorten the title.
      //
      if ( this._Title.Length < 20
        || value == String.Empty )
      {
        return;
      }

      if ( value == String.Empty )
      {
        //
        // If short title is not available use the default title shorting approach.
        //
        value = this._Object.Replace ( "_", " " );
        if ( this._Method == EuMethods.List_of_Objects )
        {
          value += EuLabels.Label_Command_List;
        }
      }

      this._Title = value;
    }

    // ==================================================================================
    /// <summary>
    /// This method enables the mandatory fields in the ciient.
    /// </summary>
    // ---------------------------------------------------------------------------------
    public void setEnableForMandatoryFields ( )
    {
      this.AddParameter ( EuCommandParameters.Enable_Mandatory_Fields, "1" );
    }

    // ==================================================================================
    /// <summary>
    /// This method indicates if the mandatory fields are enabled in the client.
    /// </summary>
    /// <returns>Boolean value</returns>
    // ---------------------------------------------------------------------------------
    public bool getEnableForMandatoryFields ( )
    {
      String value = this.GetParameter ( EuCommandParameters.Enable_Mandatory_Fields );

      return Evado.Model.EvStatics.getBool ( value );

    }//END getEnableForMandatoryFields method 

    // ==================================================================================
    /// <summary>
    /// This method indicates if a home page navigation EuCommand is to be inserted into 
    /// the history list.
    /// </summary>
    /// <returns>Boolean: True = insert the home page EuCommand.</returns>
    // ---------------------------------------------------------------------------------
    public bool getInsertHomePageCommand ( )
    {
      String value = this.GetParameter ( EuCommandParameters.Insert_Home_Page_Command );

      return Evado.Model.EvStatics.getBool ( value );

    }//END getInsertHomePageCommand method 

    // ==================================================================================
    /// <summary>
    /// This method add a parameter to for the background colour
    /// </summary>
    /// <param name="Value">EuBackgroundColours: the selected colour's enumerated value.</param>
    /// <remarks>
    /// This method consists of following steps.
    /// 
    /// 1. Iterate through the list paramater to determine of the parameter already exists and update it.
    /// 
    /// 2. If parameter Name is equal to EuCommandParameters name, return
    /// 
    /// 3. Add a new parameter to the list.
    /// 
    /// </remarks>
    //  ---------------------------------------------------------------------------------
    public void SetBackgroundDefaultColour ( EuBackgroundColours Value )
    {
      //
      // Set the EuCommand parameter.
      //
      EuCommandParameters Name = EuCommandParameters.BG_Default;
      //
      // get the string value of the parameter list.
      //
      String value = Value.ToString ( );

      this.AddParameter ( Name, value );

    }//END AddParameter method

    // ==================================================================================
    /// <summary>
    /// This method add a parameter to for the background colour
    /// </summary>
    /// <param name="Value">EuBackgroundColours: the selected colour's enumerated value.</param>
    /// <remarks>
    /// This method consists of following steps.
    /// 
    /// 1. Iterate through the list paramater to determine of the parameter already exists and update it.
    /// 
    /// 2. If parameter Name is equal to EuCommandParameters name, return
    /// 
    /// 3. Add a new parameter to the list.
    /// 
    /// </remarks>
    //  ---------------------------------------------------------------------------------
    public void SetBackgroundAlternativeColour ( EuBackgroundColours Value )
    {
      //
      // Set the EuCommand parameter.
      //
      EuCommandParameters Name = EuCommandParameters.BG_Alternative;
      //
      // get the string value of the parameter list.
      //
      String value = Value.ToString ( );

      this.AddParameter ( Name, value );

    }//END AddParameter method

    // ==================================================================================
    /// <summary>
    /// This method add a parameter to for the background colour
    /// </summary>
    /// <param name="Value">EuBackgroundColours: the selected colour's enumerated value.</param>
    /// <remarks>
    /// This method consists of following steps.
    /// 
    /// 1. Iterate through the list paramater to determine of the parameter already exists and update it.
    /// 
    /// 2. If parameter Name is equal to EuCommandParameters name, return
    /// 
    /// 3. Add a new parameter to the list.
    /// 
    /// </remarks>
    //  ---------------------------------------------------------------------------------
    public void SetBackgroundHighlightedColour ( EuBackgroundColours Value )
    {
      //
      // Set the EuCommand parameter.
      //
      EuCommandParameters Name = EuCommandParameters.BG_Highlighted;
      //
      // get the string value of the parameter list.
      //
      String value = Value.ToString ( );

      this.AddParameter ( Name, value );

    }//END AddParameter method

    // ==================================================================================
    /// <summary>
    /// This method add a parameter to for the background colour
    /// </summary>
    /// <param name="Name">CommandParameters: The name of the parameter.</param>
    /// <param name="Value">EuBackgroundColours: the selected colour's enumerated value.</param>
    /// <remarks>
    /// This method consists of following steps.
    /// 
    /// 1. Iterate through the list paramater to determine of the parameter already exists and update it.
    /// 
    /// 2. If parameter Name is equal to EuCommandParameters name, return
    /// 
    /// 3. Add a new parameter to the list.
    /// 
    /// </remarks>
    //  ---------------------------------------------------------------------------------
    public void SetBackgroundColour ( EuCommandParameters Name, EuBackgroundColours Value )
    {
      //
      // get the string value of the parameter list.
      //
      String value = Value.ToString ( );

      this.AddParameter ( Name, value );

    }//END AddParameter method

    // ==================================================================================
    /// <summary>
    /// This method gets the background colour selection.
    ///   DEPRECIATED.
    /// </summary>
    /// <param name="Name">String: the name of the parameter.</param>
    /// <returns >String value</returns>
    /// <remarks>
    /// This method consists of following steps. 
    /// 
    /// 1. Iterate through prm list of _Parameters.
    /// 
    /// 2. If prm Name is equal to EuCommandParameters Name, return prm Value.
    /// 
    /// 3. Return an empty string  
    /// </remarks>

    //  ---------------------------------------------------------------------------------
    public EuBackgroundColours GetBackBroundColor ( EuCommandParameters Name )
    {
      //
      // Exit if the parameter is not a background colour enumeration.
      //
      if ( Name != EuCommandParameters.BG_Default
        && Name != EuCommandParameters.BG_Alternative
        && Name != EuCommandParameters.BG_Highlighted )
      {
        return EuBackgroundColours.Null;
      }
      //
      // get the string value of the parameter list.
      //
      String name = EuCommandParameters.Enable_Mandatory_Fields.ToString ( );
      name = name.Trim ( );

      String value = this.GetParameter ( Name );

      //
      // Return an empty string 
      //
      return Evado.Model.EvStatics.parseEnumValue<EuBackgroundColours> ( value );

    }//END GetParameter method

    // ==================================================================================
    /// <summary>
    /// This method add a parameter to the EuCommand's parameter list..
    /// </summary>
    /// <returns> PageReference value of the EuCommand</returns>
    /// <remarks>
    /// This method consists of following steps. 
    /// 
    /// 1. Get the page data guid as a string.
    /// 
    /// 2. If stPageDateGuid is not empty convert it to a guid.
    /// 
    /// </remarks>
    // ---------------------------------------------------------------------------------
    public EuPageReference GetPageReference ( )
    {
      try
      {
        //
        // Get the page data guid as a string.
        //
        String stPageDateGuid = this.GetParameter ( EuCommandParameters.Page_Data_Guid );

        //
        // if not empty convert it to a guid.
        //
        if ( stPageDateGuid != String.Empty )
        {
          Guid pageDateGuid = new Guid ( stPageDateGuid );

          return new EuPageReference ( this._Id, pageDateGuid );
        }
      }
      catch { }

      return null;

    }//END PageReference method

    // ==================================================================================
    /// <summary>
    /// This method returns the contents of the page EuCommand.
    /// </summary>
    /// <param name="IncludeHeader">Bool: Flag for include header  </param>
    /// <param name="includeParameters">Bool: Flag for include parameter.</param>
    /// <returns>The contents of the page EuCommand.</returns>
    /// <remarks> 
    /// This method consists of following steps. 
    /// 
    /// 1. If _Header has value and Include header flage is true then add prm Name and Value to 
    ///    formatted string stOutput.
    ///    
    /// 2. If include parameter flag is true and _Parameters list has values then add prm Name 
    ///    and prm Value to formatted string stOutput.
    ///    
    /// 3. Else add parameter count to formatted string stOutput.
    /// 
    /// 4. Return string stOutput
    /// 
    /// </remarks>
    // ---------------------------------------------------------------------------------
    public String getAsString (
      bool IncludeHeader,
      bool includeParameters )
    {
      String stOutput = "Title= '" + this._Title
       + "', Typ='" + this._Type
       + "', App='" + this._ApplicationId
       + "', Obj='" + this._Object
       + "', Mth='" + this._Method + "'";

      //
      // If _Header has value and Include header flage is true then add prm Name and Value to 
      // formatted string stOutput.
      //
      if ( this.Header != null )
      {
        if ( this.Header.Count > 0
          && IncludeHeader == true )
        {
          stOutput = "ID:" + this._Id + ", " + stOutput;
          stOutput += "\r\nHeader Parameters:";
          foreach ( EuParameter prm in this.Header )
          {
            stOutput += "\r\nName: " + prm.Name + " = '" + prm.Value + "'";
          }//END prm iteration 
        }
      }

      //
      // If include parameter flag is true and _Parameters list has values then add prm Name 
      // and prm Value to formatted string stOutput.
      //
      if ( includeParameters == true )
      {
        if ( this.Parameters != null )
        {
          if ( this.Parameters.Count > 0 )
          {
            stOutput += "\r\nCommand Parameters:";
            foreach ( EuParameter prm in this.Parameters )
            {
              stOutput += "\r\nName: " + prm.Name + " = '" + prm.Value + "'";
            }//END prm iteration
          }
        }
        //
        // Else add parameter count to formatted string stOutput.
        //
        else
        {
          stOutput += "\r\nNo Parameters";
        }
      }

      //
      // Return string stOutput
      //
      return stOutput;
    }//END getAsString method 

    // ==================================================================================
    /// <summary>
    /// This method returns the contents of the page EuCommand.
    /// </summary>
    /// <returns>The contents of the page EuCommand.</returns>
    /// <remarks>
    /// This method consists of following steps.
    /// 
    /// 1. Iterate throgh a list parameter then
    ///    Add Name and Value to Parameters list.
    /// 
    /// 2. Return a EuCommand object.
    /// 
    /// </remarks>
    // ---------------------------------------------------------------------------------
    public EuCommand copyObject ( )
    {
      EuCommand EuCommand = new EuCommand ( );
      EuCommand.Id = this._Id;
      EuCommand.Title = this._Title;
      EuCommand.Type = this._Type;
      EuCommand.ApplicationId = this._ApplicationId;
      EuCommand.Object = this._Object;
      EuCommand.Method = this._Method;

      //
      // Iterate throgh a list parameter then  add Name and Value to Parameters list.
      //
      foreach ( EuParameter parameter in this.Parameters )
      {
        EuCommand.Parameters.Add ( new EuParameter ( parameter.Name, parameter.Value ) );
      }//END parameter iteration 

      //
      // Return a EuCommand object. 
      //
      return EuCommand;
    }//END copyObject method 


    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region static default EuCommand methods

    // ==================================================================================
    /// <summary>
    /// This method returns a logout EuCommand.
    /// </summary>
    /// <returns>Evado.UniForm.Model.EuCommand object</returns>
    // ----------------------------------------------------------------------------------
    public static Evado.UniForm.Model.EuCommand getLogoutCommand ( )
    {
      EuCommand pageCommand = new EuCommand (
        EuLabels.Default_Logout_Command_Title,
        EuStatics.CONST_DEFAULT,
        EuStatics.CONST_DEFAULT,
        EuMethods.Get_Object );
      pageCommand.Id = Evado.UniForm.Model.EuStatics.LogoutCommandId;
      pageCommand.Type = EuCommandTypes.Logout_Command;

      return pageCommand;
    }//END getLogooutCommand method.

    // ==================================================================================
    /// <summary>
    /// This method returns a default home page EuCommand.
    /// </summary>
    /// <returns>Evado.UniForm.Model.EuCommand object</returns>
    // ----------------------------------------------------------------------------------
    public static Evado.UniForm.Model.EuCommand getDefaultCommand ( )
    {
      EuCommand pageCommand = new EuCommand (
        EuLabels.Default_Home_Page_Command_Title,
        EuStatics.CONST_DEFAULT,
        EuStatics.CONST_DEFAULT,
        EuMethods.Get_Object );
      pageCommand.Id = Evado.UniForm.Model.EuStatics.CONST_HOME_COMMAND_ID;
      pageCommand.SetPageId ( EuStatics.CONST_HOME_PAGE_ID );

      return pageCommand;
    }//END getDefaultCommand method.

    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion
  }

}//END namespace