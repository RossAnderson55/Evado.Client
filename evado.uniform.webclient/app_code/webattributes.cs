/***************************************************************************************
 *                 COPYRIGHT (C) EVADO HOLDING PTY. LTD.  2011 - 2022
 *
 *                            ALL RIGHTS RESERVED
 *
 *  Evado. - EVADO HOLDING PTY. LTD. Trial Trial_Manager System - Web Site.
 *
 *
 ****************************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace Evado.UniForm.WebClient
{
  /// <summary>
  /// Summary description for WebAttributes.
  /// </summary>
  public class WebAttributes
  {
    private string _Version = String.Empty;
    private string _FullVersion = String.Empty;
    private string _MinorVersion = String.Empty;
    private string _Title = String.Empty;
    private string _Copyright = String.Empty;
    private string _Trademark = String.Empty;
    private string _Product = String.Empty;
    private string _Company = String.Empty;
    private string _Description = String.Empty;

    public WebAttributes ( )
    {
      char SEPARATOR = Convert.ToChar( "," );

      System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly( );

      string [ ] FullName = assembly.FullName.Split( SEPARATOR );

      for ( int i = 0; i < FullName.Length; i++ )
      {
        if ( FullName [ i ].LastIndexOf( "Version=" ) > 0 )
        {
          int length = FullName [ i ].LastIndexOf( "=" );
          string stVersion = FullName [ i ].Substring( length + 1 ) + "\r\n";

          Version version = new Version( stVersion );

          _MinorVersion = version.Major + "." + version.Minor;
          _Version = version.Major + "." + version.Minor + "." + version.Build;
          _FullVersion = version.Major + "." + version.Minor + "." + version.Build + "." + version.Revision;
        }
      }

      object [ ] customAttributes = assembly.GetCustomAttributes( true );

      for ( int j = 0; j < customAttributes.Length; j++ )
      {

        System.Type Type = customAttributes [ j ].GetType( );

        if ( Type.Name == "AssemblyTitleAttribute" )
        {
          _Title = ( ( AssemblyTitleAttribute ) customAttributes [ j ] ).Title;
        }

        if ( Type.Name == "AssemblyCopyrightAttribute" )
        {
          _Copyright = ( ( AssemblyCopyrightAttribute ) customAttributes [ j ] ).Copyright;
        }

        if ( Type.Name == "AssemblyProductAttribute" )
        {
          _Product = ( ( AssemblyProductAttribute ) customAttributes [ j ] ).Product;
        }
        if ( Type.Name == "AssemblyCompanyAttribute" )
        {
          _Company = ( ( AssemblyCompanyAttribute ) customAttributes [ j ] ).Company;
        }
        if ( Type.Name == "AssemblyDescriptionAttribute" )
        {
          _Description = ( ( AssemblyDescriptionAttribute ) customAttributes [ j ] ).Description;
        }
        if ( Type.Name == "AssemblyTrademarkAttribute" )
        {
          _Trademark = ( ( AssemblyTrademarkAttribute ) customAttributes [ j ] ).Trademark;
        }
      }//END For loop

    }// END WebAttributes class constructor;

    // Properties
    public string Company
    {
      get { return _Company; }
    }
    public string Copyright
    {
      get { return _Copyright; }
    }
    public string Description
    {
      get { return _Description; }
    }
    public string Product
    {
      get { return _Product; }
    }
    public string Trademark
    {
      get { return _Trademark; }
    }
    public string Title
    {
      get { return _Title; }
    }
    public string Version
    {
      get { return _Version; }
    }
    public string MinorVersion
    {
      get { return _MinorVersion; }
    }
    public string FullVersion
    {
      get { return _FullVersion; }
    }

    public string DisplayAttributes ( )
    {
      string strAttributes = "<table valign='top'>"
        + "<tr><td class='Right'>Company:</td>"
        + "<td class='Left'>" + this._Company + "</td></tr>"
        + "<tr><td class='Right'>Product:</td>"
        + "<td class='Left'>" + this._Product + "</td></tr>"
        + "<tr><td class='Right'>Title:</td>"
        + "<td class='Left'>" + this._Title + "</td></tr>"
        + "<tr><td class='Right'>Description:</td>"
        + "<td class='Left'>" + this._Description + "</td></tr>"
        + "<tr><td class='Right'>Trademark:</td>"
        + "<td class='Left'>" + this._Trademark + "</td></tr>"
        + "<tr><td class='Right'>Version:</td>"
        + "<td class='Left'>" + this._Version + "</td></tr>"
        + "<tr><td class='v'>Full Version:</td>"
        + "<td class='Left'>" + this._FullVersion + "</td></tr>"
        + "</table>";


      return strAttributes;
    }

  }//END Web Attributes Class

}  //END Version namespace

