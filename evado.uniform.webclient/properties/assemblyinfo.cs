/***************************************************************************************
 * <copyright file="webclinical\AssemblyInfo.cs" company="Evado UnIFORM">
 *     
 *      Copyright (c) 2011 - 2025 Evado  All rights reserved.
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
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

//
// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly
//
[assembly: AssemblyTitle ( "Evado UniFORM Web Client." )]
[assembly: AssemblyDescription ( "This assembly contains the web client for Evado.UniFORM" )]
[assembly: AssemblyConfiguration ( "" )]
[assembly: AssemblyCompany ( "Evado Holdings Pty. Ltd." )]
[assembly: AssemblyProduct ( "Evado UnIFORM Web Client" )]
[assembly: AssemblyCopyright ( "(c) Evado Holdings Pty. Ltd. 2011 - 2025 All rights reserved" )]
[assembly: AssemblyTrademark( "UniFORM" )]
[assembly: AssemblyCulture ( "en-AU" )]

//
// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Revision and Build Numbers 
// by using the '*' as shown below:
[assembly: AssemblyVersion ( "6.4.4.*" )]
[assembly: AssemblyFileVersion ( "6.4.4" )]

//
// In order to sign your assembly you must specify a key to use. Refer to the 
// Microsoft .NET Framework documentation for more information on assembly signing.
//
// Use the attributes below to control which key is used for signing. 
//
// Notes: 
//   (*) If no key is specified, the assembly is not signed.
//   (*) KeyName refers to a key that has been installed in the Crypto Service
//       Provider (CSP) on your machine. KeyFile refers to a file which contains
//       a key.
//   (*) If the KeyFile and the KeyName values are both specified, the 
//       following processing occurs:
//       (1) If the KeyName can be found in the CSP, that key is used.
//       (2) If the KeyName does not exist and the KeyFile does exist, the key 
//           in the KeyFile is installed into the CSP and used.
//   (*) In order to create a KeyFile, you can use the sn.exe (Strong Name) utility.
//        When specifying the KeyFile, the location of the KeyFile should be
//        relative to the "project output directory". The location of the project output
//        directory is dependent on whether you are working with a local or web project.
//        For local projects, the project output directory is defined as
//       <Project Directory>\obj\<Configuration>. For example, if your KeyFile is
//       located in the project directory, you would specify the AssemblyKeyFile 
//       attribute as [assembly: AssemblyKeyFile("..\\..\\mykey.snk")]
//        For web projects, the project output directory is defined as
//       %HOMEPATH%\VSWebCache\<Machine Name>\<Project Directory>\obj\<Configuration>.
//   (*) Delay Signing is an advanced option - see the Microsoft .NET Framework
//       documentation for more information on this.
//
//[assembly: AssemblyDelaySign(false)]
//[assembly: AssemblyKeyFile( "" )]
//[assembly: AssemblyKeyName( "" )]
[assembly: ComVisibleAttribute(false)]
