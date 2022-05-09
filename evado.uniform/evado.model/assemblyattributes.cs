/***************************************************************************************
 * <copyright file="Model\AssemblyAttributes.cs" company="EVADO HOLDING PTY. LTD.">
 *     
 *      Copyright (c) 2002 - 2022 EVADO HOLDING PTY. LTD.  All rights reserved.
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
 *  This class contains the EvCommonFormFields business object.
 *
 ****************************************************************************************/

using System;
using System.Collections; 
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace Evado.Model
{
	/// <summary>
  /// Summary description for AssemblyAttributes class.
	/// </summary>
	public class AssemblyAttributes
	{
		private string _Version = String.Empty;
    private string _FullVersion = String.Empty;
    private string _Title = String.Empty;
		private string _Copyright = String.Empty;
		private string _Trademark = String.Empty;
		private string _Product = String.Empty;
		private string _Company = String.Empty;
		private string _Description = String.Empty;

    /// <summary>
    /// The class initialisation method.
    /// </summary>
    public AssemblyAttributes ( )
		{
			char SEPARATOR = Convert.ToChar(",");
		
			System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
				
			string [] FullName = assembly.FullName.Split( SEPARATOR );
				
			for ( int i = 0 ; i < FullName.Length; i++ )
			{
				if ( FullName[i].LastIndexOf("Version=") > 0 )
				{
					int length =  FullName[i].LastIndexOf("=");
					string stVersion = FullName[i].Substring( length+1 ) + "\r\n";
					
					Version version = new Version( stVersion );
					
					_Version = version.Major +"." + version.Minor +"." + version.Build ;
          _FullVersion = version.Major + "." + version.Minor + "." + version.Build + "." + version.Revision;
					
				}
			}
				
			object[] customAttributes = assembly.GetCustomAttributes(true);
				
			for ( int j = 0; j < customAttributes.Length; j++ )
			{

        System.Type Type = customAttributes[ j ].GetType();
					
        if ( Type.Name == "AssemblyTitleAttribute" )
        {
          _Title = ((AssemblyTitleAttribute)customAttributes[j]).Title;
        }

        if ( Type.Name == "AssemblyCopyrightAttribute" )
        {
          _Copyright = ((AssemblyCopyrightAttribute)customAttributes[j]).Copyright;
        }

        if ( Type.Name == "AssemblyProductAttribute" )
        {
          _Product = ((AssemblyProductAttribute)customAttributes[j]).Product;
        }
        if ( Type.Name == "AssemblyCompanyAttribute" )
        {
          _Company = ((AssemblyCompanyAttribute)customAttributes[j]).Company;
        }
        if ( Type.Name == "AssemblyDescriptionAttribute" )
        {
          _Description = ((AssemblyDescriptionAttribute)customAttributes[j]).Description;
        }
        if ( Type.Name == "AssemblyTrademarkAttribute" )
        {
          _Trademark = ((AssemblyTrademarkAttribute)customAttributes[j]).Trademark;
        }
			}//END For loop
			
		}// END WebAttributes class constructor;
		
		// Properties
    /// <summary>
    /// This property contains the Company assembly attribute.
    /// </summary>
		public string Company 
		{
			get { return _Company; }
		}

    /// <summary>
    /// This property contains the copyright assembly attribute.
    /// </summary>
		public string Copyright 
		{
			get { return _Copyright; }
		}

    /// <summary>
    /// This property contains the description assembly attribute.
    /// </summary>
		public string Description 
		{
			get { return _Description; }
		}

    /// <summary>
    /// This property contains the product assembly attribute.
    /// </summary>
		public string Product 
		{
			get { return _Product; }
		}

    /// <summary>
    /// This property contains the trademark assembly attribute.
    /// </summary>
		public string Trademark 
		{
			get { return _Trademark; }
		}

    /// <summary>
    /// This property contains the title assembly attribute.
    /// </summary>
		public string Title 
		{
			get { return _Title; }
		}

    /// <summary>
    /// This property contains the version assembly attribute.
    /// </summary>
		public string Version 
		{
			get { return _Version; }
    }

    /// <summary>
    /// This property contains the full version assembly attribute.
    /// </summary>
    public string FullVersion
    {
      get { return _FullVersion; }
    }
    /// <summary>
    /// This method returns the assembly attributes as string text.
    /// </summary>
    /// <returns></returns>
    public string DisplayAttributes ( )
    {
      string strAttributes = "Company:\t" + this._Company + "\r\n"
        + "Product:\t" + this._Product + "</td></tr>"
        + "Title:\t" + this._Title + "\r\n"
        + "Description:\t" + this._Description + "\r\n"
        + "Trademark:\t" + this._Trademark + "\r\n"
        + "Version:\t" + this._Version + "\r\n"
        + "Full Version:\t" + this._FullVersion + "\r\n";

      return strAttributes;
    }

	}//END AssemblyAttributes Class
	
}  //END Version namespace

