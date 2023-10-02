/***************************************************************************************
 * <copyright file="EvEmailAddres.cs" company="EVADO HOLDING PTY. LTD.">
 *     
 *      Copyright (c) 2002 - 2023 EVADO HOLDING PTY. LTD.  All rights reserved.
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
 *  This class contains the Evado.Model.EvEmailAddres data object.
 *
 ****************************************************************************************/

using System;
using Newtonsoft.Json;

namespace Evado.Model
{
  /// <summary>
  /// This class defines a selection list option.
  /// </summary>
  [Serializable]
  public class EvEmailAddress
  {
    #region Public methods
    // ==================================================================================
    /// <summary>
    /// Constructor with specified initial values
    /// </summary>
    // ----------------------------------------------------------------------------------
    public EvEmailAddress ( )
    {
      this.Address = String.Empty;
      this.DisplayName = String.Empty;
    }

    // ==================================================================================
    /// <summary>
    /// Constructor with specified initial values
    /// </summary>
    /// <param name="Address">string: the value of the option</param>
    /// <param name="DisplayName">string: the description of the option</param>
    // ----------------------------------------------------------------------------------
    public EvEmailAddress ( String Address, string DisplayName )
    {
      this.Address = Address.ToString ( );
      this.DisplayName = DisplayName;
    }

    // ==================================================================================
    /// <summary>
    /// Constructor with specified initial values
    /// </summary>
    /// <param name="Value">string: the value of the option</param>
    // ----------------------------------------------------------------------------------
    public EvEmailAddress ( string EmailAddress )
    {
      this.Address = EmailAddress;
      this.DisplayName = EmailAddress;

      if ( EmailAddress.Contains ( ":" ) == true )
      {
        string [ ] arEmailAddress = EmailAddress.Split ( ':' );
        this.Address = arEmailAddress [ 0 ];
        this.DisplayName = arEmailAddress [ 1 ];

        return;
      }

      if ( EmailAddress.Contains ( "(" ) == false
        && EmailAddress.Contains ( "<" ) == false )
      {
        return;
      }

      String address = EmailAddress.Replace ( "<", "(" );
      address = address.Replace ( ")", String.Empty );
      address = address.Replace ( ">", String.Empty );
      //
      // Update the value with the delimited text values..
      //
      string [ ] arrAddress = address.Split ( '(' );
      this.DisplayName = arrAddress [ 0 ].Trim ( );
      this.Address = arrAddress [ 1 ].Trim ( );

    }//END class initisation method.


    #endregion

    #region Property list
    /// <summary>
    /// This property contains the option selection value
    /// </summary>
    /// 
    [JsonProperty ( "ea" )]
    public string Address { get; set; }

    /// <summary>
    /// This property contains the option description.
    /// </summary>
    [JsonProperty ( "dn" )]
    public string DisplayName { get; set; }

    /// <summary>
    /// This property contains the email and display name as ':' delimited text.
    /// </summary>
    [JsonIgnore]
    public string DelmitedEmailAddress
    {
      get
      {
        //
        // exit if empty
        //
        if ( this.Address == String.Empty )
        {
          return String.Empty;
        }

        if ( this.DisplayName == String.Empty )
        {
          this.DisplayName = this.Address.Trim();
        }

        //
        // define the local variable to store the attendlist text
        //
        return this.Address.Trim ( ) + ":" + this.DisplayName.Trim ( );
      }
      set
      {
        this.Address = value;
        this.DisplayName = value;

        if ( value.Contains ( ":" ) == false )
        {
          return;
        }

        //
        // Update the value with the delimited text values..
        //
        string [ ] address = value.Split ( ':' );
        this.Address = address [ 0 ].Trim ( );
        this.DisplayName = address [ 1 ].Trim ( );
      }
    }

    /// <summary>
    /// This property contains the email and display name as ':' delimited text.
    /// </summary>
    [JsonIgnore]
    public string TextEmailAddress
    {
      get
      {
        //
        // exit if empty
        //
        if ( this.Address == String.Empty )
        {
          return String.Empty;
        }

        String emailAddress = this.Address;

        if ( this.DisplayName != String.Empty )
        {
          emailAddress = this.DisplayName.Trim ( ) + " (" + this.Address.Trim ( ) + ")";
        }

        //
        // define the local variable to store the attendlist text
        //
        return emailAddress;
      }
      set
      {
        this.Address = value.Trim ( );
        this.DisplayName = value.Trim ( );

        if ( value.Contains ( "(" ) == false
          && value.Contains ( "<" ) == false )
        {
          return;
        }

        String address = value.Replace ( "<", "(" );
        address = address.Replace ( ")", String.Empty );
        address = address.Replace ( ">", String.Empty );
        //
        // Update the value with the delimited text values..
        //
        string [ ] arrAddress = address.Split ( '(' );
        this.DisplayName = arrAddress [ 0 ].Trim ( );
        this.Address = arrAddress [ 1 ].Trim ( );
      }
    }

    /// <summary>
    /// This property outputs the email address as a System.Net.Mail.MailAddress object.
    /// </summary>
    [JsonIgnore]
    public MimeKit.MailboxAddress MailBoxAddress
    {
      get
      {
        //
        // exit if empty
        //
        if ( this.Address == String.Empty )
        {
          return null;
        }

        MimeKit.MailboxAddress msMailAddress = new MimeKit.MailboxAddress( this.Address, this.Address);

        if ( this.DisplayName != String.Empty
          || this.Address == this.DisplayName )
        {
          msMailAddress = new MimeKit.MailboxAddress( this.DisplayName, this.Address );
        }

        //
        // define the local variable to store the attendlist text
        //
        return msMailAddress;
      }
    }
    
    /// <summary>
    /// This property returns tru if email address exists.
    /// </summary>
    [JsonIgnore]
    public bool hasAddress
    {
      get
      {
        if ( this.Address != String.Empty )
        {
          return true;
        }

        return false;
      }
    }
    #endregion

    /// <summary>
    /// This method compare the value with the option value.
    /// </summary>
    /// <param name="Value">delimited ';' list of values.</param>
    /// <returns></returns>
    public bool hasEmailAddress ( String EmailAddress )
    {
      if ( this.Address == EmailAddress )
      {
        return true;
      }

      return false;
    }

  }//END Evado.Model.EvEmailAddres method

}//END namespace Evado.Model
