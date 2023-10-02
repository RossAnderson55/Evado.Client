/***************************************************************************************
 * <copyright file="Evado.UniForm.Model\AppData.cs" company="EVADO HOLDING PTY. LTD.">
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

namespace Evado.UniForm.Model
{
  /// <summary>
  /// This class contains the content of an abstracted page.
  /// </summary>
  [Serializable]
  public class EuAppData
  {
    #region Class initialisation

    // ==================================================================================
    /// <summary>
    /// Constructor with specified initial values
    /// </summary>
    // ----------------------------------------------------------------------------------
    public EuAppData ( )
    {
      this._Id = Guid.Empty;
    }

    /// ==================================================================================
    /// <summary>
    /// Constructor with specified initial values. 
    /// </summary>
    /// <param name="ObjectTitle">String: Client Application Data object title.</param>
    /// <param name="DefaultEditAccess">ClientFieldEditsCodes: Default page field edit state</param>
    // ----------------------------------------------------------------------------------
    public EuAppData ( String ObjectTitle, EuEditAccess DefaultEditAccess )
    {
      this._Id = Guid.NewGuid ( );
      this._Page.Id = this._Id;
      this._Title = ObjectTitle;
      this._Page.Title = ObjectTitle;
      this._Page.EditAccess = DefaultEditAccess;
    }
    // ==================================================================================
    /// <summary>
    /// Constructor with specified initaial values.
    /// </summary>
    /// <param name="ObjectTitle">String: Client Application Data object title.</param>
    /// <param name="PageTitle">String: Page title</param>
    /// <param name="DefaultEditAccess">ClientFieldEditsCodes: Default page field edit state</param>
    // ----------------------------------------------------------------------------------
    public EuAppData ( String ObjectTitle, String PageTitle, EuEditAccess DefaultEditAccess )
    {
      this._Id = Guid.NewGuid ( );
      this._Page.Id = Guid.NewGuid ( );
      this._Title = ObjectTitle;
      this._Page.Title = PageTitle;
      this._Page.EditAccess = DefaultEditAccess;
    }


    #endregion

    #region Class Enumerators

    //  =================================================================================
    /// <summary>
    /// This enumeration defines the user loging status codes that are passed to the 
    /// UniFORM app client.
    /// </summary>
    //  ---------------------------------------------------------------------------------
    public enum StatusCodes
    {
      /// <summary>
      /// This enumeration indicates a null value.
      /// </summary>
      Null = 0,  // 0
      /// <summary>
      /// This enumeration indicates that the user's credentials have been validated.
      /// </summary>
      Login_Authenticated = 1,  // 1

      /// <summary>
      /// This enumeration indicates that the user is requested to login.
      /// </summary>
      Login_Request = 2,  // 2

      /// <summary>
      /// THis enumeration indications that the user's credentials have not been validated
      /// </summary>
      Login_Failed = 3,  // 3

      /// <summary>
      /// This enumeration indicates that the user has exceeded the login count.
      /// </summary>
      Login_Count_Exceeded = 4,  // 4

      /// <summary>
      /// This enumeration indicates that the device is not registered in this server.
      /// </summary>
      Device_Not_Registered = 5,  // 5

      /// <summary>
      /// This enumeration indicates that the device is being redirected to a new URI.
      /// </summary>
      Device_Redirection = 6, // 6

      /// <summary>
      /// This enumeration indicates that the server is expecting a synchronised EuCommand.
      /// </summary>
      Device_Off_Line = 7, // 7

      /// <summary>
      /// This enumeration indicates that the server is expecting a synchronised EuCommand.
      /// </summary>
      Syncrhonise_Device = 8, // 8

      /// <summary>
      /// This enumeration indicates that the server is Anonymous_Edit_Access EuCommand.
      /// </summary>
      Anonymous_Edit_Access = 9, // 9
    }

    //  =================================================================================
    /// <summary>
    /// This enumeration list defines the parameter that can be passed the client.
    /// </summary>
    //  ---------------------------------------------------------------------------------
    public enum ParameterList
    {
      /// <summary>
      /// This enumerated value defines the Administration Server Url.
      /// </summary>
      Cfg_AdminUrl,

      /// <summary>
      /// This enumerated value defines the Application Server Url 1.
      /// </summary>
      Cfg_SvrUrl1,

      /// <summary>
      /// This enumerated value defines the Application Server Url 2.
      /// </summary>
      Cfg_SvrUrl2,

      /// <summary>
      /// This enumerated value defines the Application Server Url 3.
      /// </summary>
      Cfg_SvrUrl3,

      /// <summary>
      /// This enumerated value defines the relative Rest Url.
      /// </summary>
      Cfg_RelRestUrl,

      /// <summary>
      /// This enumerated value defines the relative download Url
      /// </summary>
      Cfg_RelDownloadUrl,

      /// <summary>
      /// This enumerated value defines the relative upload Url.
      /// </summary>
      Cfg_RelUploadUrl,

      /// <summary>
      /// This enumerated value defines whether the client is debug mode.
      /// </summary>
      Cfg_ClientDebug,

      /// <summary>
      /// This enumerated value defines the page background color.
      /// </summary>
      Default_Page_Background,

      /// <summary>
      /// This enumerated value defines the page text color.
      /// </summary>
      Default_Page_Color,

      /// <summary>
      /// This enumerated value defines the page default font.
      /// </summary>
      Default_Page_Font,

      /// <summary>
      /// This enumerated value defines the Page font size.
      /// </summary>
      Default_Page_Font_Size,

      /// <summary>
      /// This enumerated value defines the page video meeting url parameter.
      /// </summary>
      Meeting_Url,

      /// <summary>
      /// This enumerated value defines the page video Status name parameter.
      /// </summary>
      Meeting_Status,

      /// <summary>
      /// This enumerated value defines the page video meeting user name parameter.
      /// </summary>
      Meeting_DisplayName,

      /// <summary>
      /// This enumerated value defines the page video meeting Parameters parameter.
      /// </summary>
      Meeting_Parameters,

    }

    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region Class constants

    //  =================================================================================
    /// <summary>
    /// This constant defines client data API version.
    /// </summary>
    //  ---------------------------------------------------------------------------------
    public const float API_Version = 3.0F;


    ///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region Properties

    private Guid _Id = Guid.Empty;
    /// <summary>
    /// This property contains an identifier for the application data object.
    /// </summary>

    public Guid Id
    {
      get { return this._Id; }
      set
      {
        this._Id = value;

        if ( this.Page.Id == Guid.Empty )
        {
          this.Page.Id = this.Id;
        }
      }
    }

    private String _SessionId = String.Empty;

    /// <summary>
    /// This property contains the session id of the Page.
    /// </summary>
    [JsonProperty ( "sid" )]
    public String SessionId
    {
      get { return _SessionId; }
      set { _SessionId = value; }
    }

    private EuAppData.StatusCodes _Status = EuAppData.StatusCodes.Null;

    /// <summary>
    /// This property contains user login status.
    /// </summary>

    [JsonProperty ( "st" )]
    public EuAppData.StatusCodes Status
    {
      get { return _Status; }
      set { _Status = value; }
    }

    private String _Url = String.Empty;

    /// <summary>
    /// This property contains the service Url 
    /// </summary>

    public String Url
    {
      get { return this._Url; }
      set { this._Url = value; }
    }

    private String _Title = String.Empty;

    /// <summary>
    /// This contains the title of the Page.
    /// </summary>

    public String Title
    {
      get { return _Title; }
      set
      {
        _Title = value;
        this._Page.Title = this._Title;
      }
    }

    private String _Message = String.Empty;

    /// <summary>
    /// This property contains the  Error Message of the Page.
    /// </summary>
    [JsonProperty ( "msg" )]
    public String Message
    {
      get { return _Message; }
      set { _Message = value; }
    }

    private String _LogoFilename = String.Empty;

    /// <summary>
    /// The background image URL property
    /// </summary>
    [JsonProperty ( "logo" )]
    public String LogoFilename
    {
      get { return _LogoFilename; }
      set { _LogoFilename = value; }
    }

    private EuPage _Page = new EuPage ( );

    /// <summary>
    /// This property contains the page to be displayed on the client oject.
    /// </summary>

    public EuPage Page
    {
      get { return _Page; }
      set { _Page = value; }
    }

    private EuOffline _Offline = null;

    /// <summary>
    /// This property contains the offline storage objects.
    /// </summary>

    [JsonIgnore]
    public EuOffline Offline
    {
      get { return _Offline; }
      set { _Offline = value; }
    }

    private List<EuParameter> _Parameters = new List<EuParameter> ( );

    /// <summary>
    /// This property contains the method parameter that will be used to call the hosted application.
    /// </summary>
    [JsonProperty ( "prms" )]
    public List<EuParameter> Parameters
    {
      get { return _Parameters; }
      set { _Parameters = value; }
    }

    /// <summary>
    /// This property defines the meeting status
    /// </summary>
    /// 
    [JsonProperty ( "mp" )]
    public Evado.Model.EvMeeting.States MeetingStatus { get; set; }
    //Evado.Model.EvMeeting.States
    #endregion

    #region Class paramter methods

    // ==================================================================================
    /// <summary>
    /// This method add a parameter to the EuCommand's parameter list..
    /// </summary>
    /// <param name="Name">ParameterList: the name of the parameter.</param>
    /// <param name="Value">Object: the value of the parameter.</param>
    /// <remarks>
    /// This method consists of the following steps:
    /// 
    /// 1. Validate the Value for not being empty
    /// 
    /// 2. Loop through the _Parameter list
    /// 
    /// 3. Add Value to Name in _Parameter list 
    /// </remarks>
    //  ---------------------------------------------------------------------------------
    public void SetParameter (
      ParameterList Name,
      object Value )
    {
      string value = Value.ToString ( );
      // Search the parmeters for existing parameters.
      // and exit if update the value.
      // 
      if ( value == String.Empty )
      {
        return;
      }

      foreach ( EuParameter parameter in this._Parameters )
      {
        if ( parameter.Name == Name.ToString ( ) )
        {
          parameter.Value = value;

          return;
        }
      }

      this._Parameters.Add ( new EuParameter ( Name.ToString ( ), value ) );

    }//END AddParameter method

    // ==================================================================================
    /// <summary>
    /// This method sets a parameter from the application parameter list.
    /// </summary>
    /// <param name="Name">ParameterList: the name of the parameter.</param>
    //  ---------------------------------------------------------------------------------
    public void DeleteParameter ( ParameterList Name )
    {
      //
      // Iterate through the parameters.
      //
      for ( int count = 0; count < this.Parameters.Count; count++ )
      {
        if ( this.Parameters [ count ].Name == Name.ToString ( ) )
        {
          this.Parameters.RemoveAt ( count );
          count--;
        }
      }

    }//END DelParameter method

    // ==================================================================================
    /// <summary>
    /// This method add a parameter to the EuCommand's parameter list..
    /// </summary>
    /// <param name="Name">AppData.ParameterList: the name of the parameter.</param>
    /// <returns> String parameter  value</returns>
    /// <remarks>
    /// This method consists of the following steps:
    /// 
    /// 1. Loop through the _Parameter list
    /// 
    /// 2. return found value 
    /// </remarks>
    //  ---------------------------------------------------------------------------------
    public bool HasParameter ( EuAppData.ParameterList Name )
    {
      foreach ( EuParameter parameter in this._Parameters )
      {
        if ( parameter.Name == Name.ToString ( ) )
        {
          return true;
        }
      }

      return false;
    }//END AddParameter method

    // ==================================================================================
    /// <summary>
    /// This method add a parameter to the EuCommand's parameter list..
    /// </summary>
    /// <param name="Name">String: the name of the parameter.</param>
    /// <returns> String parameter  value</returns>
    /// <remarks>
    /// This method consists of the following steps:
    /// 
    /// 1. Loop through the _Parameter list
    /// 
    /// 2. return found value 
    /// </remarks>
    //  ---------------------------------------------------------------------------------
    public string GetParameter (
      ParameterList Name )
    {
      foreach ( EuParameter parameter in this._Parameters )
      {
        if ( parameter.Name == Name.ToString ( ) )
        {
          return parameter.Value;
        }
      }

      return null;
    }//END AddParameter method


    #endregion

    #region Class object methods

    // =====================================================================================
    /// <summary>
    /// This method retrieves a field object using its unique field identifier.
    /// </summary>
    /// <param name="DataId"> String: field Id</param> 
    /// <remarks>
    /// This method consists of following steps
    /// 
    /// 1. Iterate through group list
    /// 
    /// 2. Iterate through field list
    /// 
    /// 3. Set field Value if field equal to data Id.
    /// 
    /// </remarks>
    // -------------------------------------------------------------------------------------

    public Evado.UniForm.Model.EuField GetField (
      String DataId )
    {
      //
      // Loop through group list
      //
      foreach ( Evado.UniForm.Model.EuGroup group in this._Page.GroupList )
      {
        //
        // Loop through field loop 
        //
        foreach ( EuField field in group.FieldList )
        {
          //
          // Making comparision betwewn filed Id and data Id
          //

          if ( field.FieldId == DataId )
          {
            //
            // return the field object
            //
            return field;
          }
        }//END field Iteration
      }//END group iteration
      return null;
    }//END setFieldValue method

    // =====================================================================================
    /// <summary>
    /// This method update the contents of the page field value.
    /// </summary>
    /// <param name="DataId"> String: field Id</param> 
    /// <param name="Value"> String: field value</param>    
    /// <remarks>
    /// This method consists of following steps
    /// 
    /// 1. Iterate through group list
    /// 
    /// 2. Iterate through field list
    /// 
    /// 3. Set field Value if field equal to data Id.
    /// 
    /// </remarks>
    // -------------------------------------------------------------------------------------

    public void SetFieldValue (
      String DataId,
      String Value )
    {
      //
      // Loop through group list
      //
      foreach ( Evado.UniForm.Model.EuGroup group in this._Page.GroupList )
      {

        //
        // Loop through field loop 
        //

        foreach ( EuField field in group.FieldList )
        {
          //
          // Making comparision betwewn filed Id and data Id
          //

          if ( field.FieldId == DataId )
          {
            //
            // Set value if field value is equal to data Id
            //


            field.Value = Value;
          }
        }//END field Iteration
      }//END group iteration
    }//END setFieldValue method

    // =====================================================================================
    /// <summary>
    /// This method update the contents of the page field value.
    /// </summary>
    /// <param name="DataId"> String: field Id</param> 
    /// <returns>String field value</returns>   
    /// <remarks>
    /// This method consists of following steps
    /// 
    /// 1. Iterate through group list
    /// 
    /// 2. Iterate through field list
    /// 
    /// 3. return field value as a string.
    /// 
    /// </remarks>
    // -------------------------------------------------------------------------------------
    public String GetFieldValue (
      String DataId )
    {
      //
      // Loop through group list
      //
      foreach ( Evado.UniForm.Model.EuGroup group in this._Page.GroupList )
      {
        //
        // Loop through field loop 
        //
        foreach ( EuField field in group.FieldList )
        {
          //
          // Making comparision betwewn filed Id and data Id
          //

          if ( field.FieldId == DataId )
          {
            //
            // Set value if field value is equal to data Id
            //


            return field.Value;
          }
        }//END field Iteration
      }//END group iteration

      return null;

    }//END setFieldValue method

    // =====================================================================================
    /// <summary>
    /// This method update the contents of the page field value.
    /// </summary>
    /// <param name="CommandId">Guid: EuCommand identifier</param>
    /// <returns>Evado.UniForm.Model.EuCommand object</returns>   
    /// <remarks>
    /// This method consists of following steps
    /// 
    /// 1. Iterate through page EuCommand list
    /// 
    /// 2. Iterate through group list
    /// 
    /// 3. Iterate through EuCommand list
    /// 
    /// 4. return matching EuCommand object or null.
    /// 
    /// </remarks>
    // -------------------------------------------------------------------------------------
    public EuCommand GetCommand (
      Guid EuCommandId )
    {
      //
      // Loop through group list
      //
      foreach ( Evado.UniForm.Model.EuCommand EuCommand in this._Page.CommandList )
      {
        //
        // If a EuCommand id matches the passed EuCommandId return the EuCommand.
        //
        if ( EuCommand.Id == EuCommandId )
        {
          return EuCommand;
        }
      }//END page EuCommand iteration loop

      //
      // Iterate through the page group list.
      //
      foreach ( Evado.UniForm.Model.EuGroup group in this._Page.GroupList )
      {
        //
        // Iterate through the group EuCommand list
        //
        foreach ( Evado.UniForm.Model.EuCommand EuCommand in group.CommandList )
        {
          //
          // If a EuCommand id matches the passed EuCommandId return the EuCommand.
          //
          if ( EuCommand.Id == EuCommandId )
          {
            return EuCommand;
          }
        }//END field Iteration
      }//END group iteration

      return null;

    }//END GetCommand method

    #endregion

    #region Class output methods

    // =====================================================================================
    /// <summary>
    /// This method returns the contents of the page EuCommand.
    /// </summary>
    /// <returns>The contents of the page EuCommand.</returns>
    /// <remarks>
    /// This method consists of following stpes 
    /// 
    /// 1. Format stOutput String
    /// 
    /// 2. Return stOutput if Page and Page Exit are not equal to null
    /// </remarks>
    // -------------------------------------------------------------------------------------
    public String getAtString ( )
    {
      //
      // Create a stOutput String
      //
      System.Text.StringBuilder stOutput = new System.Text.StringBuilder ( );

      stOutput.AppendLine ( "AppData: Id: " + this._Id );
      stOutput.AppendLine ( "- SessionId: " + this._SessionId );
      stOutput.AppendLine ( "- ServiceUri: " + this._Url );
      stOutput.AppendLine ( "- Status: " + this._Status );
      stOutput.AppendLine ( "- Title: " + this._Title );
      stOutput.AppendLine ( "- MeetingStatus: " + this.MeetingStatus );
      stOutput.AppendLine ( "AppData.Page: Id: " + this.Page.Id );

      //
      // Making comparision between Page and null
      //

      if ( this.Page != null )
      {
        stOutput.AppendLine ( "Page Id: " + this.Page.Id
          + ", Page Title: " + this.Page.Title
          + ", Page Group count: " + this.Page.GroupList.Count ); ;
        //
        // Making comparision between exit property of EuCommand class and null 
        //

        if ( this.Page.Exit != null )
        {
          stOutput.AppendLine ( "Exit EuCommand Title: " + this.Page.Exit.Title );
        }
      }

      if ( this.Parameters != null )
      {
        foreach ( EuParameter prm in this.Parameters )
        {
          stOutput.AppendFormat( "Parm: {0} = {1}.\r\n" , prm.Name,prm.Value );
        }
      }

      return stOutput.ToString();
    }//END getAtString method

    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion
  }
}//END namespace