/***************************************************************************************
 * <copyright file="webclinical\tm\DownloadMgt.cs" company="EVADO HOLDING PTY. LTD.">
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
 ****************************************************************************************/

using System;
using System.IO;
using System.Collections.Generic;


namespace Evado.Model
{
  /// <summary>
  /// This class provides statics enumeration for use across the application.
  /// </summary>
  public partial class EvStatics
  {
    /// <summary>
    /// The Site Properties object contains static initialisation properties used by 
    /// the web site.
    /// </summary>
    [Serializable]
    public class Files
    {
      #region Properties
      /// <summary>
      /// Status field 
      /// </summary>
      private static string static_DebugLog = String.Empty;

      /// <summary>
      /// Class status property
      /// </summary>
      public static string Log
      {
        get
        {
          return static_DebugLog;
        }
      }

      private static Evado.Model.EvEventCodes _ReturnedEventCode = Evado.Model.EvEventCodes.Ok;
      /// <summary>
      /// This property contains the returned event code.
      /// </summary>
      public static Evado.Model.EvEventCodes ReturnedEventCode
      {
        get { return _ReturnedEventCode; }
      }

      //***********************************************************************************
      #endregion

      #region File methods.

      // ==================================================================================
      /// <summary>
      /// This method saves a text file to the temporary directory.
      /// </summary>
      /// <param name="AppplicationDirectoryPath">String: application directory path</param>
      /// <param name="ExternalDirectoryPath">String: external directory path to be updated.</param>
      /// <param name="ConfigurationDirectoryPath">String: new directory path.</param>
      /// <returns>String: Directory path.</returns>
      //  ---------------------------------------------------------------------------------
      public static String updateDirectoryPath (
        String AppplicationDirectoryPath,
        String ExternalDirectoryPath,
        String ConfigurationDirectoryPath )
      {
        static_DebugLog = "EvStatics.Files.updateDirectoryPath method."
          + "\r\n- Appplication directory path: " + AppplicationDirectoryPath
          + "\r\n- External directory path: " + ExternalDirectoryPath
          + "\r\n- Configuration directory path: " + ConfigurationDirectoryPath;

        if ( ConfigurationDirectoryPath != String.Empty )
        {
          ConfigurationDirectoryPath = ConfigurationDirectoryPath.ToLower ( );
          ConfigurationDirectoryPath = ConfigurationDirectoryPath.Replace ( @"..\", @".\" );
          string header = ConfigurationDirectoryPath.Substring ( 0, 2 );

          //
          // strip un necessary elements if the path is relative.
          //
          if ( header.Contains ( @".\" ) == true )
          {
            ConfigurationDirectoryPath = ConfigurationDirectoryPath.Substring ( 2 );
            header = String.Empty;
          }

          //
          // IF the path is relative append it to the application path.
          //
          if ( ( header.Contains ( ":" ) == false )
            && ( header.Contains ( @"\\" ) == false ) )
          {
            ExternalDirectoryPath = AppplicationDirectoryPath + ConfigurationDirectoryPath;
          }
          else
          {
            //
            // Set the absolute binary path value.
            //
            ExternalDirectoryPath = ConfigurationDirectoryPath;

          }//END seting an absolute binary path

        }//END stRepositoryFilePath is not empty

        static_DebugLog += "\r\n- Set Directory path: " + ExternalDirectoryPath;

        return ExternalDirectoryPath;

      }//END updateDirectoryPath method.


      // ==================================================================================
      /// <summary>
      /// This method saves a text file to the temporary directory.
      /// </summary>
      /// <param name="FileDirectory">String Temporary Directory name</param>
      /// <param name="FileName">String FileName</param>
      //  ---------------------------------------------------------------------------------
      public static String readFile (
        String FileDirectory,
        String FileName )
      {
        static_DebugLog = "EvStatics.Files.readFile method."
          + "\r\n- FileDirectory: " + FileDirectory
          + "\r\n- FileName: " + FileName;

        //
        // Initialise methods variables and objects.
        //
        String stFileContent = String.Empty;

        if ( FileDirectory == String.Empty )
        {
          Files._ReturnedEventCode = Evado.Model.EvEventCodes.File_Directory_Path_Empty;
          return String.Empty;
        }
        if ( FileName == String.Empty )
        {
          Files._ReturnedEventCode = Evado.Model.EvEventCodes.File_File_Name_Empty;
          return String.Empty;
        }

        if ( Files.hasDirectory ( FileDirectory ) == false )
        {
          Files._ReturnedEventCode = Evado.Model.EvEventCodes.File_Directory_Error;
          return String.Empty;
        }

        try
        {
          String TempFileName = FileDirectory + FileName;
          // 
          // Open the stream to the file.
          // 
          using ( StreamReader sr = new StreamReader ( TempFileName ) )
          {
            stFileContent = sr.ReadToEnd ( );

          }// End StreamWriter.

        }
        catch
        {
          static_DebugLog = "\r\nFiles content save failed.";

          Files._ReturnedEventCode = Evado.Model.EvEventCodes.File_Save_Error;
        }

        return stFileContent;
      }

      // ==================================================================================
      /// <summary>
      /// This method saves a text file to the temporary directory.
      /// </summary>
      /// <param name="FileDirectory">String Temporary Directory name</param>
      /// <param name="FileName">String FileName</param>
      //  ---------------------------------------------------------------------------------
      public static List<String> readFileAsList (
        String FileDirectory,
        String FileName )
      {
        static_DebugLog = "EvStatics.Files.readFile method.\r\n"
          + "- FileDirectory: " + FileDirectory + "\r\n"
          + "- FileName: " + FileName + "\r\n";

        //
        // Initialise methods variables and objects.
        //
        List<String> fileContentList = new List<String> ( );

        if ( FileDirectory == String.Empty )
        {
          Files._ReturnedEventCode = Evado.Model.EvEventCodes.File_Directory_Path_Empty;
          return fileContentList;
        }
        if ( FileName == String.Empty )
        {
          Files._ReturnedEventCode = Evado.Model.EvEventCodes.File_File_Name_Empty;
          return fileContentList;
        }

        if ( Files.hasDirectory ( FileDirectory ) == false )
        {
          Files._ReturnedEventCode = Evado.Model.EvEventCodes.File_Directory_Error;
          return fileContentList;
        }

        try
        {
          String TempFileName = FileDirectory + FileName;
          // 
          // Open the stream to the file.
          // 
          using ( StreamReader sr = new StreamReader ( TempFileName ) )
          {
            while ( sr.EndOfStream == false )
            {
              string data = sr.ReadLine ( );

              //static_DebugLog += data + "\r\n";

              if ( data != String.Empty )
              {
                fileContentList.Add ( data );
              }
            }
          }// End StreamWriter.

        }
        catch
        {
          static_DebugLog += "Files content read failed.\r\n";

          Files._ReturnedEventCode = Evado.Model.EvEventCodes.File_Save_Error;
        }

        return fileContentList;

      }//END read file

      // ==================================================================================
      /// <summary>
      /// This method saves a text file to the temporary directory.
      /// </summary>
      /// <param name="FileDirectory">String Temporary Directory name</param>
      /// <param name="FileName">String FileName</param>
      //  ---------------------------------------------------------------------------------
      public static T readXmlFile<T> (
        String FileDirectory,
        String FileName )
      {
        static_DebugLog = "EvStatics.Files.readXmlFile method."
          + "\r\n- FileDirectory: " + FileDirectory
          + "\r\n- FileName: " + FileName;

        //
        // Initialise methods variables and objects.
        //
        String stFileContent = String.Empty;
        T t_object = default ( T );

        if ( FileDirectory == String.Empty )
        {
          Files._ReturnedEventCode = Evado.Model.EvEventCodes.File_Directory_Path_Empty;
          return t_object;
        }
        if ( FileName == String.Empty )
        {
          Files._ReturnedEventCode = Evado.Model.EvEventCodes.File_File_Name_Empty;
          return t_object;
        }

        if ( Files.hasDirectory ( FileDirectory ) == false )
        {
          Files._ReturnedEventCode = Evado.Model.EvEventCodes.File_Directory_Error;
          return t_object;
        }

        try
        {
          String TempFileName = FileDirectory + FileName;
          // 
          // Open the stream to the file.
          // 
          using ( StreamReader sr = new StreamReader ( TempFileName ) )
          {
            stFileContent = sr.ReadToEnd ( );

          }// End StreamWriter.

          t_object = DeserialiseXmlObject<T> ( stFileContent );
        }
        catch(Exception Ex )
        {
          static_DebugLog = "\r\n" + EvStatics.getException( Ex );

          Files._ReturnedEventCode = Evado.Model.EvEventCodes.File_Read_Error;
        }

        return t_object;
      }

      // ==================================================================================
      /// <summary>
      /// This method saves a text file to the temporary directory.
      /// </summary>
      /// <param name="FileDirectory">String Temporary Directory name</param>
      /// <param name="FileName">String FileName</param>
      //  ---------------------------------------------------------------------------------
      public static T readJsonFile<T> (
        String FileDirectory,
        String FileName )
      {
        static_DebugLog = "EvStatics.Files.readJsonFile method."
          + "\r\n- FileDirectory: " + FileDirectory
          + "\r\n- FileName: " + FileName;

        //
        // Initialise methods variables and objects.
        //
        String stFileContent = String.Empty;
        T t_object = default ( T );

        if ( FileDirectory == String.Empty )
        {
          Files._ReturnedEventCode = Evado.Model.EvEventCodes.File_Directory_Path_Empty;
          return t_object;
        }
        if ( FileName == String.Empty )
        {
          Files._ReturnedEventCode = Evado.Model.EvEventCodes.File_File_Name_Empty;
          return t_object;
        }

        if ( Files.hasDirectory ( FileDirectory ) == false )
        {
          Files._ReturnedEventCode = Evado.Model.EvEventCodes.File_Directory_Error;
          return t_object;
        }

        try
        {
          String TempFileName = FileDirectory + FileName;
          // 
          // Open the stream to the file.
          // 
          using ( StreamReader sr = new StreamReader ( TempFileName ) )
          {
            stFileContent = sr.ReadToEnd ( );

          }// End StreamWriter.

          t_object = Newtonsoft.Json.JsonConvert.DeserializeObject<T> ( stFileContent );
        }
        catch ( Exception Ex )
        {
          static_DebugLog = "\r\n" + EvStatics.getException ( Ex );

          Files._ReturnedEventCode = Evado.Model.EvEventCodes.File_Read_Error;
        }

        return t_object;
      }
      // ==================================================================================
      /// <summary>
      /// This method saves a text file to the temporary directory.
      /// </summary>
      /// <param name="FileDirectory">String Temporary Directory name</param>
      /// <param name="FileName">String FileName</param>
      /// <param name="FileContent">String: file content</param>
      //  ---------------------------------------------------------------------------------
      public static bool saveFile (
        String FileDirectory,
        String FileName,
        String FileContent )
      {
        static_DebugLog = "EvStatics.Files.saveFile method."
          + "\r\n - FileDirectory: " + FileDirectory
          + "\r\n - FileName: " + FileName
          + "\r\n - Content lenth: " + FileContent.Length;

        //
        // Validate the parameter values.
        //
        if ( FileDirectory == String.Empty )
        {
          Files._ReturnedEventCode = Evado.Model.EvEventCodes.File_Directory_Path_Empty;
          return false;
        }
        if ( FileName == String.Empty )
        {
          Files._ReturnedEventCode = Evado.Model.EvEventCodes.File_File_Name_Empty;
          return false;
        }
        if ( FileContent == String.Empty )
        {
          Files._ReturnedEventCode = Evado.Model.EvEventCodes.File_File_Content_Empty;
          return false;
        }

        //
        // Create the directory if it does not exist attempt to created it.
        //
        if ( Files.createDirectory ( FileDirectory ) == false )
        {
          return false;
        }

        try
        {
          String TempFileName = FileDirectory + FileName;
          // 
          // Open the stream to the file.
          // 
          using ( StreamWriter sw = new StreamWriter ( TempFileName ) )
          {
            sw.Write ( FileContent );

          }// End StreamWriter.

          return true;
        }
        catch ( Exception ex )
        {
          static_DebugLog += "\r\n" + Evado.Model.EvStatics.getException ( ex );

          Files._ReturnedEventCode = Evado.Model.EvEventCodes.File_Save_Error;
        }
        return false;
      }

      // ==================================================================================
      /// <summary>
      /// This method saves a text file to the temporary directory.
      /// </summary>
      /// <param name="FileDirectory">String Temporary Directory name</param>
      /// <param name="FileName">String FileName</param>
      /// <param name="FileContent">String: file content</param>
      //  ---------------------------------------------------------------------------------
      public static bool saveFileAppend (
        String FileDirectory,
        String FileName,
        String FileContent )
      {
        static_DebugLog = "EvStatics.Files.saveFile method."
          + "\r\n - FileDirectory: " + FileDirectory
          + "\r\n - FileName: " + FileName
          + "\r\n - Content lenth: " + FileContent.Length;

        //
        // Validate the parameter values.
        //
        if ( FileDirectory == String.Empty )
        {
          Files._ReturnedEventCode = Evado.Model.EvEventCodes.File_Directory_Path_Empty;
          return false;
        }
        if ( FileName == String.Empty )
        {
          Files._ReturnedEventCode = Evado.Model.EvEventCodes.File_File_Name_Empty;
          return false;
        }
        if ( FileContent == String.Empty )
        {
          Files._ReturnedEventCode = Evado.Model.EvEventCodes.File_File_Content_Empty;
          return false;
        }

        //
        // Create the directory if it does not exist attempt to created it.
        //
        if ( Files.createDirectory ( FileDirectory ) == false )
        {
          return false;
        }

        try
        {
          String TempFileName = FileDirectory + FileName;
          // 
          // Open the stream to the file.
          // 
          using ( StreamWriter sw = new StreamWriter ( TempFileName, true ) )
          {
            sw.Write ( FileContent );

          }// End StreamWriter.

          return true;
        }
        catch ( Exception ex )
        {
          static_DebugLog += "\r\n" + Evado.Model.EvStatics.getException ( ex );

          Files._ReturnedEventCode = Evado.Model.EvEventCodes.File_Save_Error;
        }
        return false;
      }

      // ==================================================================================
      /// <summary>
      /// This method saves a text file to the temporary directory.
      /// </summary>
      /// <param name="FileDirectory">String Temporary Directory name</param>
      /// <param name="FileName">String FileName</param>
      /// <param name="FileContent">String: file content</param>
      //  ---------------------------------------------------------------------------------
      public static bool saveXmlFile<T> (
        String FileDirectory,
        String FileName,
        T FileContent )
      {
        static_DebugLog = "EvStatics.Files.saveXmlFile method."
          + "\r\n - FileDirectory: " + FileDirectory
          + "\r\n - FileName: " + FileName;
        String xmlFileContent = String.Empty;
        //
        // Validate the parameter values.
        //
        if ( FileDirectory == String.Empty )
        {
          Files._ReturnedEventCode = Evado.Model.EvEventCodes.File_Directory_Path_Empty;
          return false;
        }
        if ( FileName == String.Empty )
        {
          Files._ReturnedEventCode = Evado.Model.EvEventCodes.File_File_Name_Empty;
          return false;
        }
        if ( FileContent == null )
        {
          Files._ReturnedEventCode = Evado.Model.EvEventCodes.File_File_Content_Empty;
          return false;
        }

        xmlFileContent = Evado.Model.EvStatics.SerialiseXmlObject<T> ( FileContent );

        static_DebugLog += "\r\nContent.Length: " + xmlFileContent.Length;

        //
        // Create the directory if it does not exist attempt to created it.
        //
        if ( Files.createDirectory ( FileDirectory ) == false )
        {
          return false;
        }

        try
        {
          String TempFileName = FileDirectory + FileName;
          // 
          // Open the stream to the file.
          // 
          using ( StreamWriter sw = new StreamWriter ( TempFileName ) )
          {
            sw.Write ( xmlFileContent );

          }// End StreamWriter.

          return true;
        }
        catch ( Exception ex )
        {
          static_DebugLog += "\r\n" + Evado.Model.EvStatics.getException ( ex );

          Files._ReturnedEventCode = Evado.Model.EvEventCodes.File_Save_Error;
        }
        return false;
      }

      // ==================================================================================
      /// <summary>
      /// This method saves a text file to the temporary directory.
      /// </summary>
      /// <param name="FileDirectory">String Temporary Directory name</param>
      /// <param name="FileName">String FileName</param>
      /// <param name="FileContent">String: file content</param>
      //  ---------------------------------------------------------------------------------
      public static bool saveJsonFile<T> (
        String FileDirectory,
        String FileName,
        T FileContent )
      {
        static_DebugLog = "EvStatics.Files.saveJsonFile method."
          + "\r\n - FileDirectory: " + FileDirectory
          + "\r\n - FileName: " + FileName;
        String jsonFileContent = String.Empty;
        Newtonsoft.Json.JsonSerializerSettings jsonSettings = new Newtonsoft.Json.JsonSerializerSettings
        {
          NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
          Formatting = Newtonsoft.Json.Formatting.Indented,
          StringEscapeHandling = Newtonsoft.Json.StringEscapeHandling.Default
        };

        //
        // Validate the parameter values.
        //
        if ( FileDirectory == String.Empty )
        {
          Files._ReturnedEventCode = Evado.Model.EvEventCodes.File_Directory_Path_Empty;
          return false;
        }
        if ( FileName == String.Empty )
        {
          Files._ReturnedEventCode = Evado.Model.EvEventCodes.File_File_Name_Empty;
          return false;
        }
        if ( FileContent == null )
        {
          Files._ReturnedEventCode = Evado.Model.EvEventCodes.File_File_Content_Empty;
          return false;
        }

        jsonFileContent = Newtonsoft.Json.JsonConvert.SerializeObject ( FileContent, jsonSettings );

        static_DebugLog += "\r\nContent.Length: " + jsonFileContent.Length;

        //
        // Create the directory if it does not exist attempt to created it.
        //
        if ( Files.createDirectory ( FileDirectory ) == false )
        {
          return false;
        }

        try
        {
          String TempFileName = FileDirectory + FileName;
          // 
          // Open the stream to the file.
          // 
          using ( StreamWriter sw = new StreamWriter ( TempFileName ) )
          {
            sw.Write ( jsonFileContent );

          }// End StreamWriter.

          return true;
        }
        catch ( Exception ex )
        {
          static_DebugLog += "\r\n" + Evado.Model.EvStatics.getException ( ex );

          Files._ReturnedEventCode = Evado.Model.EvEventCodes.File_Save_Error;
        }
        return false;
      }

      // ==================================================================================
      /// <summary>
      /// Creates the directory if it does not exist.
      /// </summary>
      /// <param name="DirectoryPath">String: Directory path to be created.</param>
      /// <returns>True: Directory existed or was created.</returns>
      //  ---------------------------------------------------------------------------------
      public static bool hasDirectory (
        String DirectoryPath )
      {
        static_DebugLog = "EvStatics.Files.hasDirectory method.\r\n"
          + "- DirectoryPath: " + DirectoryPath + "\r\n";
        try
        {
          //
          // Check that the directory path string is not empty.
          //
          if ( DirectoryPath == String.Empty )
          {
            Files._ReturnedEventCode = Evado.Model.EvEventCodes.File_Directory_Path_Empty;
            return false;
          }

          //
          // Initialise methods variables and objects.
          //
          DirectoryInfo di = new DirectoryInfo ( DirectoryPath );

          //
          // Determine whether the directory exists.
          //
          if ( di.Exists )
          {
            return true;
          }

          return false;
        }
        catch
        {
          static_DebugLog = "Failed to create " + DirectoryPath + " directory.\r\n";

          Files._ReturnedEventCode = Evado.Model.EvEventCodes.File_General_Access_Error;
        }
        return false;

      }//END CreateDirectory method

      // ==================================================================================
      /// <summary>
      /// Creates the directory if it does not exist.
      /// </summary>
      /// <param name="DirectoryPath">String: Directory path to be created.</param>
      /// <returns>True: Directory existed or was created.</returns>
      //  ---------------------------------------------------------------------------------
      public static bool createDirectory (
        String DirectoryPath )
      {
        static_DebugLog += "EvStatics.Files.createDirectory method."
          + "\r\n -DirectoryPath: " + DirectoryPath;
        try
        {
          //
          // Check that the directory path string is not empty.
          //
          if ( DirectoryPath == String.Empty )
          {
            Files._ReturnedEventCode = Evado.Model.EvEventCodes.File_Directory_Path_Empty;
            return false;
          }

          //
          // Initialise methods variables and objects.
          //
          DirectoryInfo di = new DirectoryInfo ( DirectoryPath );

          //
          // Determine whether the directory exists.
          //
          if ( di.Exists )
          {
            return true;
          }

          // 
          // Try to create the directory.
          // 
          di.Create ( );

          return true;
        }
        catch
        {
          static_DebugLog = "\r\nFailed to create " + DirectoryPath + " directory.";

          Files._ReturnedEventCode = Evado.Model.EvEventCodes.File_General_Access_Error;
        }
        return false;

      }//END CreateDirectory method

      // ==================================================================================
      /// <summary>
      /// Creates the directory if it does not exist.
      /// </summary>
      /// <param name="DirectoryPath">String: directory path to files to be delted.</param>
      /// <param name="NumberOfDaysAgo">int: number of days </param>
      //  ---------------------------------------------------------------------------------
      public static List<String> getDirectoryFileList ( string DirectoryPath, String Extension )
      {
        static_DebugLog = "EvStatics.Files.getDirectoryFileList method."
          + "\r\n-DirectoryPath: " + DirectoryPath
          + "\r\n-Extension: " + Extension;
        //
        // Initialise the directory object.
        //
        DirectoryInfo di = new DirectoryInfo ( DirectoryPath );
        List<String> fileNames = new List<String> ( );
        System.IO.FileInfo [ ] files = di.GetFiles ( );

        static_DebugLog += "\r\n-File count: " + files.Length;

        for ( int i = 0; i < files.Length; i++ )
        {
          if ( files [ i ].Name.Contains ( Extension ) != true
            && Extension != String.Empty )
          {
            continue;
          }

          fileNames.Add ( files [ i ].Name );
        }

        static_DebugLog += "\r\n-FileName count: " + fileNames.Count;

        return fileNames;

      }//END DownLoadManagement class


      // ==================================================================================
      /// <summary>
      /// Creates the directory if it does not exist.
      /// </summary>
      /// <param name="DirectoryPath">String: directory path to files to be delted.</param>
      /// <param name="NumberOfDaysAgo">int: number of days </param>
      //  ---------------------------------------------------------------------------------
      public static bool deleteOldFiles ( string DirectoryPath, int NumberOfDaysAgo )
      {
        static_DebugLog = "EvStatics.Files.deleteOldFiles method."
          + "\r\n- DirectoryPath: " + DirectoryPath
          + "\r\n- daysOld: " + NumberOfDaysAgo;
        //
        // Initialise the directory object.
        //
        DirectoryInfo di = new DirectoryInfo ( DirectoryPath );
        int inNumberOfDaysAgo = 14; // default is 14 days.

        try
        {
          // 
          // Determine whether the directory exists.
          // 
          if ( di.Exists == false )
          {
            // 
            // Try to create the directory.
            // 
            di.Create ( );

            return true;
          }

          static_DebugLog += "\r\nDeleting files older than "
            + DateTime.Now.AddDays ( -inNumberOfDaysAgo ).ToString ( "dd MMM yyyy" )
            + "\r\nFiles:";

          //
          // update the number of days ago.
          //
          if ( NumberOfDaysAgo > 0 )
          {
            inNumberOfDaysAgo = NumberOfDaysAgo;
          }

          // 
          // Get a reference to each file in that directory.
          // 
          FileInfo [ ] fiArr = di.GetFiles ( );

          // 
          // Display the names of the files.
          // 
          foreach ( FileInfo fi in fiArr )
          {
            static_DebugLog += "\r\n- " + fi.Name
              + " " + fi.CreationTime.ToString ( );
            // 
            // If the file is older than 28 days delete it.
            //
            if ( fi.CreationTime < DateTime.Now.AddDays ( -inNumberOfDaysAgo ) )
            {
              fi.Delete ( );
              static_DebugLog += " >> DELETED";
            }
          }

          return true;
        }
        catch
        {
          static_DebugLog = "\r\nFailed to delete " + DirectoryPath + " files.";

          Files._ReturnedEventCode = Evado.Model.EvEventCodes.File_General_Access_Error;
        }
        return false;

      }//END DownLoadManagement class

      // ==================================================================================
      /// <summary>
      /// Creates the directory if it does not exist.
      /// </summary>
      /// <param name="DirectoryPath">String: directory path to files to be deleted.</param>
      /// <param name="NumberOfDaysAgo">int: number of days </param>
      /// <param name="IgnoreFileList">string: ignore file list</param>
      //  ---------------------------------------------------------------------------------
      public static int deleteOldFiles ( string DirectoryPath, int NumberOfDaysAgo, String [ ] IgnoreFileList )
      {
        static_DebugLog = "EvStatics.Files.deleteOldFiles method."
          + "\r\n- DirectoryPath: " + DirectoryPath
          + "\r\n- NumberOfDaysAgo: " + NumberOfDaysAgo
          + "\r\n- IgnoreList.count: " + IgnoreFileList;
        //
        // Initialise the directory object.
        //
        DirectoryInfo di = new DirectoryInfo ( DirectoryPath );
        int inNumberOfDaysAgo = 14; // default is 14 days.
        int deletedFiled = 0;
        try
        {
          // 
          // Determine whether the directory exists.
          // 
          if ( di.Exists == false )
          {
            // 
            // Try to create the directory.
            // 
            di.Create ( );

            return deletedFiled;
          }

          static_DebugLog += "\r\nDeleting files older than "
            + DateTime.Now.AddDays ( -inNumberOfDaysAgo ).ToString ( "dd MMM yyyy" )
            + "\r\nFiles:";

          //
          // update the number of days ago.
          //
          if ( NumberOfDaysAgo > 0 )
          {
            inNumberOfDaysAgo = NumberOfDaysAgo;
          }

          // 
          // Get a reference to each file in that directory.
          // 
          FileInfo [ ] fiArr = di.GetFiles ( );

          // 
          // Display the names of the files.
          // 
          foreach ( FileInfo fi in fiArr )
          {
            static_DebugLog += "\r\n- " + fi.Name
              + " - " + fi.CreationTime.ToString ( );

            //
            // skip files on the ignore list.
            //
            if ( ignoreFile ( fi.Name, IgnoreFileList ) == true )
            {
              static_DebugLog += " >> IGNORED";
              continue;
            }

            // 
            // If the file is older than 28 days delete it.
            //
            if ( fi.CreationTime < DateTime.Now.AddDays ( -inNumberOfDaysAgo ) )
            {
              fi.Delete ( );
              static_DebugLog += " >> DELETED";
              deletedFiled++;
            }
          }

          return deletedFiled;
        }
        catch
        {
          static_DebugLog = "\r\nFailed to delete " + DirectoryPath + " files.";

          Files._ReturnedEventCode = Evado.Model.EvEventCodes.File_General_Access_Error;
        }
        return deletedFiled;

      }//END DownLoadManagement class

      //================================================================================
      /// <summary>
      /// This method tests to see if the file exists in the ignore list.
      /// </summary>
      /// <param name="FileName">String:  the file name to be tested.</param>
      /// <param name="ignoreFileList">String array of files to be ignored.</param>
      /// <returns>True: if file found on the ignore list.</returns>
      //---------------------------------------------------------------------------------
      private static bool ignoreFile ( String FileName, String [ ] ignoreFileList )
      {
        //
        // Set the filename to lower case for comparision.
        //
        FileName = FileName.ToLower ( );

        //
        // iterate through the array of ignore files and
        // return true if the file is found.
        //
        foreach ( String str in ignoreFileList )
        {
          if ( FileName.Contains ( str ) == true )
          {
            return true;
          }
        }
        return false;

      }//END ignoreFile method.

      //***********************************************************************************
      #endregion

      #region DeleteUserPageStateFiles Directory methods.

      // ==================================================================================
      /// <summary>
      ///  This method deletes the session state files from the server.
      /// </summary>
      /// <param name="DirectoryPath">String path to the files to be dleted.</param>
      //  ---------------------------------------------------------------------------------
      public static bool DeleteUserPageStateFiles ( String DirectoryPath )
      {
        static_DebugLog = "EvStatics.Files.DeleteUserPageStateFiles method. "
        + "\r\n- Directory path: " + DirectoryPath
        + "\r\nFiles:";

        /*
         * Initialise the directory object.
         */
        DirectoryInfo di = new DirectoryInfo ( DirectoryPath );

        try
        {
          if ( di.Exists == false )
          {
            // 
            // Try to create the directory.
            // 
            di.Create ( );

            return true;
          }

          // 
          // Get a reference to each file in that directory.
          // 
          FileInfo [ ] fiArr = di.GetFiles ( );

          // 
          // Display the names of the files.
          // 
          foreach ( FileInfo fi in fiArr )
          {
            static_DebugLog += "\r\n-" + fi.Name
              + " " + fi.CreationTime.ToString ( );
            // 
            // If the file is older than 1 days delete it.
            //
            if ( fi.CreationTime < DateTime.Now.AddDays ( -1 ) )
            {
              fi.Delete ( );
              static_DebugLog += " >> DELETED";
            }
          }

          return true;
        }
        catch
        {
          static_DebugLog = "\r\nFailed to delete " + DirectoryPath + " files.";

          Files._ReturnedEventCode = Evado.Model.EvEventCodes.File_General_Access_Error;
        }
        return false;


      }//END DeletePageStateFiles class

      //***********************************************************************************
      #endregion

    }//END Files class
  }//END EvStatics class

} //END namespace Evado.Model 