/***************************************************************************************
 * <copyright file="webclinical\default.aspx.cs" company="EVADO HOLDING PTY. LTD.">
 *     
 *      Copyright (c) 2011 - 2025 EVADO HOLDING PTY. LTD.  All rights reserved.
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
using System.Collections.Specialized;
using System.Diagnostics;
using System.Web;
using System.IO;

///Evado. namespace references.

using Evado.UniForm.Web;
using Evado.UniForm.Model;
using Evado.Model;
using Newtonsoft.Json.Linq;

namespace Evado.UniForm.AdminClient
{
  /// <summary>
  /// This is the code behind class for the home page.
  /// </summary>
  public partial class DefaultPage : EvPersistentPageState
  {

    #region  update page methods

    // ==================================================================================
    /// <summary>
    /// This method updates the  application data with the form field values.
    /// </summary>
    // ---------------------------------------------------------------------------------
    private void GetCommandParameters( )
    {
      this.LogMethod ( "GetPageCommandParameters" );
      //
      // If the Command method is to upate the page then update the data object with 
      // Page field values.
      //
      if ( this.UserSession.PageCommand.Method != Evado.UniForm.Model.EuMethods.Save_Object
        && this.UserSession.PageCommand.Method != Evado.UniForm.Model.EuMethods.Delete_Object
        && this.UserSession.PageCommand.Method != Evado.UniForm.Model.EuMethods.Custom_Method )
      {
        this.LogMethodEnd ( "getPageCommandParameters" );
        return;
      }

      //this.LogDebug ( "Updating command parameters. " );

      //
      // Upload the page images.
      //
      this.UploadPageImages ( );

      //
      // set the image deletion parameters.
      //
      this.UpdateImageDeletion ( );

      //
      // Get the data from the returned page fields.
      //
      this.GetPageDataValues ( );

      //
      // Get the data from the returned page fields.
      //
      this.UpdateFieldAnnotations ( );

      //
      // Update the Command parmaters witih the page values.
      //
      this.UpdateWebPageCommandObject ( );

    }//END updateApplicationDataObject method

    // ==================================================================================
    /// <summary>
    /// This method updates the web application with the form field values.
    /// </summary>
    // ---------------------------------------------------------------------------------
    private void GetPageDataValues( )
    {
      this.LogMethod ( "getPageDataValues" );
      //
      // Get the field collection.
      //
      NameValueCollection ReturnedFormFields = Request.Form;

      // 
      // Get names of all keys into a string array.
      // 
      String [ ] aKeys = ReturnedFormFields.AllKeys;

      this.LogDebug ( "Key length: " + aKeys.Length );

      // 
      // Iterate the keys to find the value for the selected formDataId
      // 
      for ( int loop1 = 0 ; loop1 < aKeys.Length ; loop1++ )
      {
        this.LogDebug ( aKeys [ loop1 ] + " >> " + ReturnedFormFields.Get ( aKeys [ loop1 ] ) );
      }

      // 
      // Iterate through the test fields updating the fields that have changed.
      // 
      foreach ( Evado.UniForm.Model.EuGroup group in this.UserSession.AppData.Page.GroupList )
      {
        for ( int count = 0 ; count < group.FieldList.Count ; count++ )
        {
          group.FieldList [ count ] = this.UpdateFormField (
            group.FieldList [ count ],
            ReturnedFormFields,
            group.EditAccess );

          this.LogDebug ( group.FieldList [ count ].FieldId
            + " > " + group.FieldList [ count ].Title
            + " >> " + group.FieldList [ count ].Type
            + " >>> " + group.FieldList [ count ].Value );

        }//END test field iteration.

      }//END the iteration loop.


      EvStatics.Files.saveJsonFile<EuPage> ( Global.TempPath, "UpdatedAppDatPage.json", this.UserSession.AppData.Page );
      this.LogMethodEnd ( "getPageDataValues" );

    }//END getPageDataValues method

    // ==================================================================================

    /// <summary>
    /// This method updates the new field annotations.
    /// </summary>
    // ---------------------------------------------------------------------------------
    private void UpdateFieldAnnotations( )
    {
      this.LogMethod ( "updateFieldAnnotations" );
      //
      // Get the field collection.
      //
      NameValueCollection ReturnedFormFields = Request.Form;

      // 
      // Get names of all keys into a string array.
      // 
      String [ ] aKeys = ReturnedFormFields.AllKeys;

      this.LogDebug ( "Key length: " + aKeys.Length );

      // 
      // Iterate the keys to find the value for the selected formDataId
      // 
      for ( int loop1 = 0 ; loop1 < aKeys.Length ; loop1++ )
      {
        EuKeyValuePair keyPair = new EuKeyValuePair ( );
        //
        // Skip all non annotation and returned field values.
        //
        if ( aKeys [ loop1 ].Contains ( Evado.UniForm.Model.EuField.CONST_FIELD_QUERY_SUFFIX ) == false
          && aKeys [ loop1 ].Contains ( Evado.UniForm.Model.EuField.CONST_FIELD_ANNOTATION_SUFFIX ) == false )
        {
          continue;
        }

        this.LogDebug ( "" + aKeys [ loop1 ] + " >> " + ReturnedFormFields.Get ( aKeys [ loop1 ] ) );

        int inAnnotationKey = this.GetAnnotationIndex ( aKeys [ loop1 ] );

        this.LogDebug ( " inAnnotationKey: " + inAnnotationKey );
        //
        // Get the data value.
        //
        if ( inAnnotationKey < 0 )
        {
          this.LogDebug ( " >> New Item" );
          //
          // Set the object value and add it to the field annotation list.
          //
          keyPair.Key = aKeys [ loop1 ];
          keyPair.Value = ReturnedFormFields.Get ( aKeys [ loop1 ] );

          this.LogDebug ( " Key: " + keyPair.Key + " value: " + keyPair.Value );

          this.UserSession.FieldAnnotationList.Add ( keyPair );

        }
        else
        {
          //
          // Update the annotated value.
          //
          keyPair = this.UserSession.FieldAnnotationList [ inAnnotationKey ];
          keyPair.Value = ReturnedFormFields.Get ( aKeys [ loop1 ] );
        }

      }//END return field values.

      for ( int count = 0 ; count < this.UserSession.FieldAnnotationList.Count ; count++ )
      {
        EuKeyValuePair keyPair = this.UserSession.FieldAnnotationList [ count ];

        this.LogDebug ( "Key: " + keyPair.Key + " >> " + keyPair.Value );
      }

      this.LogDebug ( "FieldAnnotationList: " + this.UserSession.FieldAnnotationList.Count );

    }//END updateFieldAnnotations method

    // =============================================================================== 
    /// <summary>
    /// This method returns the annotation's list index.
    /// </summary>
    /// <param name="Key">The page annotation field id</param>
    /// <returns>Int: the annotation list index.</returns>
    // ---------------------------------------------------------------------------------
    private int GetAnnotationIndex( String Key )
    {
      //this.writeDebug = "<hr/>Evado.UniForm.AdminClient.DefaultPage.getAnnotationIndex method.  Key: " + Key
      //  + " AnnotationList count: " + this.UserSession.FieldAnnotationList.Count ;
      //
      // Iterate through the annotation list to find a matching element
      //
      for ( int i = 0 ; i < this.UserSession.FieldAnnotationList.Count ; i++ )
      {
        //
        // Get annotation.
        //
        EuKeyValuePair annotation = this.UserSession.FieldAnnotationList [ i ];
        // this.writeDebug = "[0]" + annotation [ 0 ];

        //
        // Return the matching value.
        //
        if ( annotation.Key == Key )
        {
          /// this.writeDebug = " >> FOUND ";
          return i;
        }
      }

      ///
      /// for not match return empty string.
      ///
      return -1;

    }//END getAnnotationValue method 

    // =============================================================================== 
    /// <summary>
    /// updateFormField method.
    /// 
    /// Description:
    ///   This method updates a test field object.
    /// 
    /// </summary>
    /// <param name="FormField">Field object containing test field data.</param>
    /// <param name="ReturnedFormFields">Containing the returned formfield values.</param>
    /// <param name="FormState">Current FormRecord state</param>
    /// <returns>Returns a Field object.</returns>
    // ---------------------------------------------------------------------------------
    private Evado.UniForm.Model.EuField UpdateFormField(
       EuField FormField,
      NameValueCollection ReturnedFormFields,
       bool GroupStatus )
    {
      this.LogMethod ( "updateFormField" );
      this.LogDebug ( "FormField.DataId: {0}, FormField.DataType: {1}, FormField.Status:  {2} ", FormField.FieldId, FormField.Type, FormField.EditAccess );

      // 
      // Initialise methods variables and objects.
      // 
      string stValue = String.Empty;
      string stAnnotation = String.Empty;
      string stQuery = String.Empty;
      string stAssessmentStatus = String.Empty;

      //
      // If a binary or image file return it without processing.
      //
      if ( FormField.Type == Evado.Model.EvDataTypes.Binary_File )
      {
        //this.LogDebug ( "Binary field found but not processed" );
        this.LogMethodEnd ( "updateFormField" );
        return FormField;
      }

      /**********************************************************************************/
      // 
      // If the test is in EDIT mode update the fields values.
      // 
      if ( FormField.EditAccess != true
       && FormField.Type != EvDataTypes.Computed_Field )
      {
        //this.LogDebug ( "User does not have edit access." );
        this.LogMethodEnd ( "updateFormField" );
        return FormField;
      }//END updating field

      //
      // If field type is a single value update it 
      //
      switch ( FormField.Type )
      {
        case Evado.Model.EvDataTypes.Check_Box_List:
        {
          FormField.Value = this.GetCheckButtonListFieldValue (
            ReturnedFormFields,
            FormField.FieldId,
            FormField.Value,
            FormField.OptionList.Count );
          break;
        }
        case Evado.Model.EvDataTypes.Address:
        {
          FormField.Value = this.GetAddressFieldValue (
            ReturnedFormFields,
            FormField.FieldId );
          break;
        }

        case Evado.Model.EvDataTypes.Name:
        {
          FormField.Value = this.GetNameFieldValue (
            ReturnedFormFields,
            FormField.FieldId );
          break;
        }

        case Evado.Model.EvDataTypes.Streamed_Video:
        {
          // 
          // Iterate through the option list to compare values.
          // 
          string videoUrl = this.GetReturnedFormFieldValue ( ReturnedFormFields, FormField.FieldId );

          this.LogDebug ( "videoUrl:" + videoUrl );

          FormField.Value = videoUrl;
          break;
        }
        case Evado.Model.EvDataTypes.Http_Link:
        {
          // 
          // Iterate through the option list to compare values.
          // 
          string httpUrl = this.GetReturnedFormFieldValue ( ReturnedFormFields, FormField.FieldId + EuField.CONST_HTTP_URL_FIELD_SUFFIX );
          string httpTitle = this.GetReturnedFormFieldValue ( ReturnedFormFields, FormField.FieldId + EuField.CONST_HTTP_TITLE_FIELD_SUFFIX );

          this.LogDebug ( "httpUrl:" + httpUrl + " httpTitle:" + httpTitle );

          FormField.Value = httpUrl + "^" + httpTitle;
          break;
        }

        case Evado.Model.EvDataTypes.Integer_Range:
        case Evado.Model.EvDataTypes.Float_Range:
        case Evado.Model.EvDataTypes.Double_Range:
        case Evado.Model.EvDataTypes.Date_Range:
        {
          FormField.Value = this.GetRangeFieldValue (
            ReturnedFormFields,
            FormField.FieldId );
          break;
        }

        case Evado.Model.EvDataTypes.Signature:
        {
          FormField.Value = this.getSignatureFieldValue (
            ReturnedFormFields,
            FormField.FieldId );
          break;
        }

        case Evado.Model.EvDataTypes.Table:
        case Evado.Model.EvDataTypes.Record_Table:
        case Evado.Model.EvDataTypes.Special_Matrix:
        {
          FormField = this.UpdateFormTableFields (
                       FormField,
                       ReturnedFormFields );
          break;
        }

        case Evado.Model.EvDataTypes.Computed_Field:
        {
          this.LogDebug ( "Computed Field." );
          FormField.Value = this.updateComputedField ( FormField );

          this.LogDebug ( "Computed_Field: FormField.Value: {0}.", FormField.Value );
          break;
        }

        case Evado.Model.EvDataTypes.Date:
        {
          this.LogDebug ( "Date Field." );
          stValue = this.GetReturnedFormFieldValue ( ReturnedFormFields, FormField.FieldId );

          this.LogDebug ( "Field stValue: {0}.", stValue );

          //
          // correcting the date format into the platform standard format.
          //
          DateTime date = EvStatics.getDateTime ( stValue );

          this.LogDebug ( "Field Date: {0}.", date.ToString ( "dd-MMM-yyyy" ) );

          FormField.Value = stValue;

          break;
        }

        case Evado.Model.EvDataTypes.Boolean:
        {
          this.LogDebug ( "Boolean Field." );
          stValue = this.GetReturnedFormFieldValue ( ReturnedFormFields, FormField.FieldId );

          this.LogDebug ( "Field stValue: " + stValue );
          // 
          // Does the returned field value exist
          // 
          FormField.Value = "false";
          if ( stValue != null )
          {
            FormField.Value = stValue;
          }

          this.LogDebug ( "Computed_Field: FormField.Value: {0}.", FormField.Value );
          break;
        }

        case Evado.Model.EvDataTypes.Image:
        {
          this.LogDebug ( "Image Field." );

          this.LogDebug ( "FieldId: {0}, Value: {1}.", FormField.FieldId, FormField.Value );

          string fieldId = FormField.FieldId + EuField.CONST_IMAGE_FIELD_TITLE_SUFFIX;

          stValue = this.GetReturnedFormFieldValue ( ReturnedFormFields, fieldId );

          this.LogDebug ( "Title stValue: {0}.", stValue );

          if ( String.IsNullOrEmpty ( stValue ) == false )
          {
            FormField.AddParameter ( fieldId, stValue );
          }

          fieldId = FormField.FieldId + EuField.CONST_IMAGE_FIELD_TITLE_SUFFIX;

          stValue = this.GetReturnedFormFieldValue ( ReturnedFormFields, fieldId );

          this.LogDebug ( "Delete stValue: {0}.", stValue );

          if ( String.IsNullOrEmpty ( stValue ) == false )
          {
            FormField.AddParameter ( fieldId, stValue );
          }
          break;
        }
        default:
        {
          stValue = this.GetReturnedFormFieldValue ( ReturnedFormFields, FormField.FieldId );

          this.LogDebug ( "Field stValue: " + stValue );
          // 
          // Does the returned field value exist
          // 
          if ( stValue != null )
          {
            if ( FormField.Value != stValue )
            {
              if ( FormField.Type == Evado.Model.EvDataTypes.Numeric )
              {
                this.LogDebug ( "Numeric Field Change: Id: '" + FormField.FieldId
                 + "' Old: '" + FormField.Value + "' New: '" + stValue + "' " );

                FormField.Value = Evado.Model.EvStatics.convertTextNullToNumNull ( stValue );
              }

              // 
              // Set field value.
              // 
              FormField.Value = stValue;

            }//END Update field value.

          }//END Value exists.

          break;
        }

      }//END Switch


      // 
      // stReturn the test field object.
      // 
      return FormField;

    }//END updateFormField method

    // =============================================================================== 
    /// <summary>
    /// This method updates the Computed form fields.
    /// 
    /// </summary>
    /// <param name="ComputedField">Evado.Uniform.Model.EuField object</param>
    // ---------------------------------------------------------------------------------
    private String updateComputedField( EuField ComputedField )
    {
      this.LogMethod ( "updateComputedField" );
      // 
      // Initialise methods variables and objects.
      //
      String computedFormula = ComputedField.GetParameter ( EuFieldParameters.Computed_Formula );
      float computedFieldValue = 0F;

      if ( computedFormula == String.Empty )
      {
        this.LogDebug ( "EXIT: formula is empty" );
        this.LogMethodEnd ( "updateComputedField" );
        return ComputedField.Value;
      }
      if ( computedFormula.Contains ( "(" ) == false )
      {
        this.LogDebug ( "EXIT: formula incomplete" );
        this.LogMethodEnd ( "updateComputedField" );
        return ComputedField.Value;
      }

      try
      {
        computedFormula = computedFormula.Replace ( "(", "^" );
        computedFormula = computedFormula.Replace ( ")", "^" );

        string [ ] arComputedFormula = computedFormula.Split ( '^' );

        if ( arComputedFormula.Length > 1 )
        {
          this.LogDebug ( "computedFormula: {0}, Formula: {1}, parameter: {2}.",
            computedFormula, arComputedFormula [ 0 ], arComputedFormula [ 1 ] );
        }

        //
        // Set the formula and the field references.
        //
        String formula = arComputedFormula [ 0 ];
        String parameters = arComputedFormula [ 1 ];
        parameters = parameters.Replace ( ",", ";" );

        String [ ] arParameters = parameters.Split ( ';' );
        bool [ ] negativeValue = new bool [ arParameters.Length ];

        //
        // determine if there are negative fields.
        //
        for ( int index = 0 ; index < arParameters.Length ; index++ )
        {
          arParameters [ index ] = arParameters [ index ].Trim ( );

          if ( arParameters [ index ] [ 0 ] == '-' )
          {
            negativeValue [ index ] = true;
            arParameters [ index ] = arParameters [ index ].Substring ( 1 );
          }
          else
          {
            negativeValue [ index ] = false;
          }
        }

        switch ( formula )
        {
          case EuField.COMPUTED_FUNCTION_SUM_FIELDS:
          {
            //
            // Iterate through the field idenifiers retrieving the field value 
            // and if numeric add it to the fieldValue variale.
            //
            for ( int index = 0 ; index < arParameters.Length ; index++ )
            {
              string fielId = arParameters [ index ];
              EuField field = this.UserSession.AppData.Page.getField ( fielId );

              if ( field == null )
              {
                this.LogDebug ( "ERROR: FIELD NULL." );
                continue;
              }

              //this.LogDebug ( "fielId: {0}, field.FieldId: {1}, Value: '{2}'.",
              //   fielId, field.FieldId, field.Value );

              float fValue = Evado.Model.EvStatics.getFloat ( field.Value, 0 );
              if ( fValue == 0 )
              {
                this.LogDebug ( "ERROR: Empty or not a numeric value." );
                continue;
              }
              if ( negativeValue [ index ] == true )
              {
                computedFieldValue -= fValue;
              }
              else
              {
                computedFieldValue += fValue;
              }
            }
            break;
          }
          case EuField.COMPUTED_FUNCTION_SUM_CATEGORY:
          {
            //
            // get the computed fields fied category
            //
            string fieldCategory = arComputedFormula [ 1 ].Trim ( );
            this.LogDebug ( "ComputedField: fieldCategory: {0}.", fieldCategory );

            //
            // Iterate through the page groups 
            //
            foreach ( EuGroup group in this.UserSession.AppData.Page.GroupList )
            {
              //
              // Iterate through the fields in each group. Retrieving the field
              // and if in the computed field category add its value to the fieldValue variale.
              //
              foreach ( EuField field in group.FieldList )
              {
                string category = field.GetParameter ( EuFieldParameters.Category );

                this.LogDebug ( "fieldCategory: {0}, Field.Categrory: {1}.", fieldCategory, category );

                if ( category.ToUpper ( ) != fieldCategory.ToUpper ( ) )
                {
                  continue;
                }
                //this.LogDebug ( "Adding, field.Value: {0}.", field.Value );

                float fValue = Evado.Model.EvStatics.getFloat ( field.Value, 0 );
                if ( fValue == 0 )
                {
                  this.LogDebug ( "ERROR: Empty or not a numeric value." );
                  continue;
                }

                computedFieldValue += fValue;
              }
            }
            break;
          }
          case EuField.COMPUTED_FUNCTION_SUM_COLUMN:
          {
            if ( arParameters.Length == 0 )
            {
              break;
            }

            //
            // Retrieve the tale field identifier and tale column (0-9).
            //
            String tableFieldId = arParameters [ 0 ].Trim ( );
            String columnId = arParameters [ 1 ].Trim ( );
            int columnIndex = Evado.Model.EvStatics.getInteger ( columnId );
            columnIndex--;

            //this.LogDebug ( " tableFieldId: {0}, columnId: {1}, columnIndex: {2}.", tableFieldId, columnId, columnIndex );

            if ( columnIndex < 0 || columnIndex > 9 )
            {
              break;
            }

            //
            // retrieve the tale field
            //
            EuField field = this.UserSession.AppData.Page.getField ( tableFieldId );

            if ( field == null )
            {
              //this.LogDebug ( "ERROR: FIELD NULL." );
              break;
            }
            if ( field.Table == null )
            {
              //this.LogDebug ( "ERROR: FIELD TABLE  NULL." );
              break;
            }

            //
            // iterate through each row adding the column value if a floating number.
            //
            foreach ( Evado.Model.EvTableRow row in field.Table.Rows )
            {
              if ( columnIndex < row.Column.Length )
              {
                //this.LogDebug ( "columnIndex: {0}, row.No: {1}, Value: {2}.",
                //  columnIndex, row.No, row.Column [ columnIndex ] );

                if ( row.Column [ columnIndex ] == String.Empty )
                {
                  this.LogDebug ( "EMPTY: ColumnId: {0}, Index: {1} Value: '{3}', empty.",
                  columnId, columnIndex, row.Column [ columnIndex ] );
                  continue;
                }

                float fValue = Evado.Model.EvStatics.getFloat ( row.Column [ columnIndex ], -1 );
                if ( fValue == -1 )
                {
                  this.LogDebug ( "ERROR: ColumnId: {0}, Index: {1} Value: '{2}', not a numeric value.",
                    columnId, columnIndex, row.Column [ columnIndex ] );
                  continue;
                }

                computedFieldValue += fValue;
              }//field index ok
            }//END row interation loop

            break;
          }
        }//End Switch

        ComputedField.Value = computedFieldValue.ToString ( );

        this.LogDebug ( "ComputedField.Value: '{0}'.", ComputedField.Value );
      }
      catch ( Exception Ex )
      {
        this.LogStandard ( Evado.Model.EvStatics.getException ( Ex ) );
      }

      this.LogMethodEnd ( "updateComputedField" );

      return ComputedField.Value;

    }//END updateComputedField method

    // =============================================================================== 
    /// <summary>
    /// updateChecklistField method.
    /// 
    /// Description:
    ///   This method updates the common TestReport static test fields
    /// 
    /// </summary>
    /// <param name="ReturnedFormFields">List of returned html form fields.</param>
    /// <param name="htmlDataId">The html form field to be udpated.</param>
    /// <param name="CurrentValue">The current form field value.</param>
    /// <param name="OptionList">THe form field option list.</param>
    /// <returns>Returns a string containing the Java Scripts.</returns>
    // ---------------------------------------------------------------------------------
    private string getSignatureFieldValue(
      NameValueCollection ReturnedFormFields,
      string htmlDataId )
    {
      this.LogMethod ( "getSignatureFieldValue" );
      this.LogStandard ( "htmlDataId: " + htmlDataId );
      // 
      // Initialise methods variables and objects.
      // 
      string signatureValueFieldId = htmlDataId + "_sig";
      string signatureNameFieldId = htmlDataId + "_name";

      String stSignature = this.GetReturnedFormFieldValue ( ReturnedFormFields, signatureValueFieldId );
      string stName = this.GetReturnedFormFieldValue ( ReturnedFormFields, signatureNameFieldId );

      this.LogStandard ( "stSignature: " + stSignature );
      this.LogStandard ( "stName: " + stName );

      if ( stSignature == null )
      {
        return String.Empty;
      }
      if ( stSignature == String.Empty )
      {
        return String.Empty;
      }

      if ( stName == null )
      {
        stName = String.Empty;
      }

      this.LogStandard ( "Converting signature to signatureBlock object." );
      Evado.Model.EvSignatureBlock signatureBlock = new Evado.Model.EvSignatureBlock ( );
      signatureBlock.Signature = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Evado.Model.EvSegement>> ( stSignature );
      signatureBlock.Name = stName;
      signatureBlock.DateStamp = DateTime.Now;

      string stSignatureBlock = Newtonsoft.Json.JsonConvert.SerializeObject ( signatureBlock );
      this.LogDebug ( "stSignatureBlock:" + stSignatureBlock );

      return stSignatureBlock;

    }//END getCheckButtonListFieldValue method

    // =============================================================================== 
    /// <summary>
    /// updateChecklistField method.
    /// 
    /// Description:
    ///   This method updates the common TestReport static test fields
    /// 
    /// </summary>
    /// <param name="ReturnedFormFields">List of returned html form fields.</param>
    /// <param name="htmlDataId">The html form field to be udpated.</param>
    /// <param name="CurrentValue">The current form field value.</param>
    /// <param name="OptionList">THe form field option list.</param>
    /// <returns>Returns a string containing the Java Scripts.</returns>
    // ---------------------------------------------------------------------------------
    private string GetCheckButtonListFieldValue(
      NameValueCollection ReturnedFormFields,
      string htmlDataId,
      string CurrentValue,
      int OptionListCount )
    {
      this.LogMethod ( "getCheckButtonListFieldValue" );
      this.LogStandard ( "htmlDataId: " + htmlDataId );
      this.LogStandard ( "OptionList: " + OptionListCount );
      // 
      // Initialise methods variables and objects.
      // 
      string [ ] arrValues = new String [ 0 ];
      string stThisValue = String.Empty;

      arrValues = this.GetReturnedFormFieldValueArray ( ReturnedFormFields, htmlDataId );

      if ( arrValues != null )
      {
        // 
        // Iterate through the option list to compare values.
        // 
        for ( int index = 0 ; index < arrValues.Length ; index++ )
        {
          if ( stThisValue != String.Empty )
          {
            stThisValue += ";";
          }

          stThisValue += arrValues [ index ];

        }//END Value exists.

      }
      this.LogDebug ( "stThisValue:" + stThisValue );

      return stThisValue;

    }//END getCheckButtonListFieldValue method

    // =============================================================================== 
    /// <summary>
    ///   This method updates the common TestReport static test fields
    /// </summary>
    /// <param name="ReturnedFormFields">List of returned html form fields.</param>
    /// <param name="htmlDataId">The html form field to be udpated.</param>
    /// <param name="CurrentValue">The current form field value.</param>
    /// <param name="OptionList">THe form field option list.</param>
    /// <returns>Returns a string containing the Java Scripts.</returns>
    // ---------------------------------------------------------------------------------
    private string GetNameFieldValue(
      NameValueCollection ReturnedFormFields,
      string htmlDataId )
    {
      this.LogMethod ( "getNameFieldValue" );
      this.LogStandard ( "htmlDataId: " + htmlDataId );
      // 
      // Initialise methods variables and objects.
      // 
      string stTitle = String.Empty;
      string stFirstName = String.Empty;
      string stMiddleName = String.Empty;
      string stFamilyName = String.Empty;

      // 
      // Iterate through the option list to compare values.
      // 
      stTitle = this.GetReturnedFormFieldValue ( ReturnedFormFields, htmlDataId + EvName.NAME_PREFIX_FIELD_SUFFIX );
      stFirstName = this.GetReturnedFormFieldValue ( ReturnedFormFields, htmlDataId + EvName.NAME_GIVEN_FIELD_SUFFIX );
      stMiddleName = this.GetReturnedFormFieldValue ( ReturnedFormFields, htmlDataId + EvName.NAME_MIDDLE_FIELD_SUFFIX );
      stFamilyName = this.GetReturnedFormFieldValue ( ReturnedFormFields, htmlDataId + EvName.NAME_FAMILY_FIELD_SUFFIX );

      this.LogDebug ( "stFirstName:" + stFirstName + " stMiddleName:" + stMiddleName + " stFamilyName:" + stFamilyName + "\r\n" );

      return stTitle + ";" + stFirstName + ";" + stMiddleName + ";" + stFamilyName;

    }//END getCheckButtonListFieldValue method

    // =============================================================================== 
    /// <summary>
    ///   This method updates the common TestReport static test fields
    /// 
    /// </summary>
    /// <param name="ReturnedFormFields">List of returned html form fields.</param>
    /// <param name="htmlDataId">The html form field to be udpated.</param>
    /// <param name="CurrentValue">The current form field value.</param>
    /// <param name="OptionList">THe form field option list.</param>
    /// <returns>Returns a string containing the Java Scripts.</returns>
    // ---------------------------------------------------------------------------------
    private string GetRangeFieldValue(
      NameValueCollection ReturnedFormFields,
      string htmlDataId )
    {
      this.LogMethod ( "getRangeFieldValue" );
      this.LogStandard ( "htmlDataId: " + htmlDataId );
      // 
      // Initialise methods variables and objects.
      // 
      string stLowerValue = String.Empty;
      string stUpperValue = String.Empty;

      // 
      // Iterate through the option list to compare values.
      // 
      stLowerValue = this.GetReturnedFormFieldValue ( ReturnedFormFields, htmlDataId + DefaultPage.CONST_FIELD_LOWER_SUFFIX );
      stUpperValue = this.GetReturnedFormFieldValue ( ReturnedFormFields, htmlDataId + DefaultPage.CONST_FIELD_UPPER_SUFFIX );

      this.LogDebug ( "stLowerValue: {0},  stUpperValue: {1} " );

      return stLowerValue + ";" + stUpperValue;

    }//END getCheckButtonListFieldValue method

    // =============================================================================== 
    /// <summary>
    /// updateNameFieldValue method.
    /// 
    /// Description:
    ///   This method updates the common TestReport static test fields
    /// 
    /// </summary>
    /// <param name="ReturnedFormFields">List of returned html form fields.</param>
    /// <param name="htmlDataId">The html form field to be udpated.</param>
    /// <param name="CurrentValue">The current form field value.</param>
    /// <param name="OptionList">THe form field option list.</param>
    /// <returns>Returns a string containing the Java Scripts.</returns>
    // ---------------------------------------------------------------------------------
    private string GetAddressFieldValue(
      NameValueCollection ReturnedFormFields,
      string htmlDataId )
    {
      this.LogMethod ( "getNameFieldValue" );
      this.LogStandard ( "htmlDataId: " + htmlDataId );
      // 
      // Initialise methods variables and objects.
      // 
      string stAddress1 = String.Empty;
      string stAddress2 = String.Empty;
      string stSuburb = String.Empty;
      string stState = String.Empty;
      string stPostCode = String.Empty;
      string stCountry = String.Empty;

      // 
      // Iterate through the option list to compare values.
      // 
      stAddress1 = this.GetReturnedFormFieldValue ( ReturnedFormFields, htmlDataId + "_Address1" );
      stAddress2 = this.GetReturnedFormFieldValue ( ReturnedFormFields, htmlDataId + "_Address2" );
      stSuburb = this.GetReturnedFormFieldValue ( ReturnedFormFields, htmlDataId + "_Suburb" );
      stState = this.GetReturnedFormFieldValue ( ReturnedFormFields, htmlDataId + "_State" );
      stPostCode = this.GetReturnedFormFieldValue ( ReturnedFormFields, htmlDataId + "_PostCode" );
      stCountry = this.GetReturnedFormFieldValue ( ReturnedFormFields, htmlDataId + "_Country" );

      this.LogDebug ( "\r\n stAddress1:" + stAddress1
        + " stAddress2:" + stAddress2
        + " stSuburb:" + stSuburb
        + " stState:" + stState
        + " stPostCode:" + stPostCode
        + " stCountry:" + stCountry
        + "\r\n" );

      return stAddress1 + ";" + stAddress2 + ";" + stSuburb + ";" + stState + ";" + stPostCode + ";" + stCountry;

    }//END getCheckButtonListFieldValue method

    // =============================================================================== 
    /// <summary>
    ///   This method updates the test table field values.
    /// 
    /// </summary>
    /// <param name="FormField">EvFormField object containing test field data.</param>
    /// <param name="ReturnedFormFields">Containing the returned formfield values.</param>
    /// <returns>Returns a EvFormField object.</returns>
    // ---------------------------------------------------------------------------------
    private Evado.UniForm.Model.EuField UpdateFormTableFields(
      EuField FormField,
      NameValueCollection ReturnedFormFields )
    {
      this.LogMethod ( "updateFormTableFields" );
      this.LogDebug ( " FieldId: {0}, EditAccess: {1}.",
        FormField.FieldId, FormField.EditAccess );
      // 
      // Iterate through the rows and columns of the table filling the 
      // data object with the test values.
      // 
      for ( int rowIndex = 0 ; rowIndex < FormField.Table.Rows.Count ; rowIndex++ )
      {
        for ( int colIndex = 0 ; colIndex < FormField.Table.Header.Length ; colIndex++ )
        {
          this.LogDebug ( "" );
          this.LogDebug ( "Row: {0}, Col: {1}, DataType: {2}, Readonly: {3}, Value: {4}.",
            rowIndex, colIndex,
            FormField.Table.Header [ colIndex ].DataType,
            FormField.Table.Rows [ rowIndex ].ReadOnly,
            FormField.Table.Rows [ rowIndex ].Column [ colIndex ] );

          if ( FormField.Table.Header [ colIndex ].Text == String.Empty )
          {
            this.LogDebug ( "SKIP: Column {0} Header text empty.", colIndex );
            continue;
          }

          // 
          // construct the test table field name.
          // 
          string tableFieldId = FormField.FieldId + "_" + ( rowIndex + 1 ) + "_" + ( colIndex + 1 );
          this.LogDebug ( "tableFieldId: {0}, DataType: {1}.",
            tableFieldId, FormField.Table.Header [ colIndex ].DataType );

          // 
          // Get the table field and update the test field object.
          // 
          string value = this.GetReturnedFormFieldValue ( ReturnedFormFields, tableFieldId );

          //
          // If NA is entered set to numeric null.
          //
          switch ( FormField.Table.Header [ colIndex ].DataType )
          {
            case Evado.Model.EvDataTypes.Numeric:
            {
              // 
              // Does the returned field value exist
              // 
              if ( value == null )
              {
                this.LogDebug ( "SKIP: Null value " );
                continue;
              }

              // this.LogDebug ( "DataType: {0}, value: {1}.", FormField.Table.Header [ colIndex ].DataType, value );
              if ( value.ToLower ( ) == Evado.Model.EvStatics.CONST_NUMERIC_NOT_AVAILABLE.ToLower ( ) )
              {
                value = Evado.Model.EvStatics.CONST_NUMERIC_NULL.ToString ( );
              }

              FormField.Table.Rows [ rowIndex ].Column [ colIndex ] = value;
              break;
            }
            case EvDataTypes.Boolean:
            {

              // 
              // Does the returned field value exist
              // 
              if ( FormField.Table.Rows [ rowIndex ].Column [ colIndex ] == String.Empty )
              {
                this.LogDebug ( "Boolean: SKIP: Row: {0}, Col: {1} Boolean Column is empty ", rowIndex, colIndex );
                break;
              }

              this.LogDebug ( "Boolean: Title: {0}, DataType: {1}, value: {2}.",
                FormField.Table.Header [ colIndex ].Text, FormField.Table.Header [ colIndex ].DataType, value );

              var bValue = EvStatics.getBool ( value );

              FormField.Table.Rows [ rowIndex ].Column [ colIndex ] = bValue.ToString ( );

              this.LogDebug ( "Boolean: Table Cell {0}-{1} = {2}.",
                rowIndex, colIndex, FormField.Table.Rows [ rowIndex ].Column [ colIndex ] );

              break;
            }
            case Evado.Model.EvDataTypes.Computed_Field:
            {
              break;
            }
            default:
            {
              // 
              // Does the returned field value exist
              // 
              if ( value == null )
              {
                this.LogDebug ( "SKIP: Null value " );
                continue;
              }

              this.LogDebug ( "Default: DataType: {0}, value: {1}.", FormField.Table.Header [ colIndex ].DataType, value );
              FormField.Table.Rows [ rowIndex ].Column [ colIndex ] = value;
              break;
            }
          }

        }//END column interation loop

      }//END row interation loop

      //
      // execute table computations.
      //
      this.UpdateTableComputedColumn ( FormField.Table );

      return FormField;

    }//END updateFormFieldTable method

    // =============================================================================== 
    /// <summary>
    /// This method computes the table computer column calculation.
    /// </summary>
    /// <param name="Table">fEvado.Model.EvTable object</param>
    /// <param name="ColumnIndex">int column identifier</param>
    /// <returns>String value </returns>
    // ---------------------------------------------------------------------------------
    private void UpdateTableComputedColumn( Evado.Model.EvTable Table )
    {
      this.LogMethod ( "UpdateTableComputedColumn" );

      String value = String.Empty;
      int computedColumnIndex = -1;
      EvTableHeader computedColumnHeader = new EvTableHeader ( );


      //
      // Look for a computed field in the table header.
      //
      for ( int index = 0 ; index < Table.Header.Length ; index++ )
      {
        if ( Table.Header [ index ].Text == String.Empty )
        {
          continue;
        }
        this.LogDebug ( "Header.DataType: {0}, Formula: {1}", Table.Header [ index ].DataType, Table.Header [ index ].OptionsOrUnit );

        if ( Table.Header [ index ].DataType == EvDataTypes.Computed_Field )
        {
          computedColumnHeader = Table.Header [ index ];
          computedColumnIndex = index;
        }
      }

      //
      // exit if a computed field is not found.
      //
      if ( computedColumnIndex == -1 )
      {
        //this.LogDebug ( "No computer field found" );
        this.LogMethodEnd ( "UpdateTableComputedColumn" );
        return;
      }

      if ( computedColumnHeader.OptionsOrUnit == String.Empty )
      {
        //this.LogDebug ( "No formula found" );
        this.LogMethodEnd ( "UpdateTableComputedColumn" );
        return;
      }

      this.LogDebug ( "COMPUTED COLUMN: DataType: {0}, Formula: {1}",
        computedColumnHeader.DataType, computedColumnHeader.OptionsOrUnit );

      string formula = computedColumnHeader.OptionsOrUnit;

      //
      // The computed column formula selection.
      //
      for ( int rowIndex = 0 ; rowIndex < Table.Rows.Count ; rowIndex++ )
      {
        EvTableRow row = Table.Rows [ rowIndex ];
        float fltValue = 0;
        float fltValue1 = 0;
        float fltValue2 = 0;

        //
        // Iterate through the row values.
        //
        for ( int columnIndex = 0 ; columnIndex < row.Column.Length && columnIndex < Table.Header.Length ; columnIndex++ )
        {
          EvTableHeader columnHeader = Table.Header [ columnIndex ];

          if ( columnHeader.Text == String.Empty )
          {
            continue;
          }

          this.LogDebug ( "Header.DataType: {0}", columnHeader.DataType, columnHeader.OptionsOrUnit );

          //
          // create teh column identifier.
          //
          string columnId = ( columnIndex + 1 ).ToString ( "00" );
          float fltColValue = 0;
          this.LogDebug ( "colId: {0}.", columnId );

          //
          // SKIP if not in the selection column list.
          //
          if ( formula.Contains ( columnId ) == false )
          {
            this.LogDebug ( "Continue columnId not found in formula" );
            continue;
          }

          //
          // select the processing by date type
          //
          switch ( columnHeader.DataType )
          {
            default:
            {
              this.LogDebug ( "CONTINUE: {0} - {1}, column data type not compatible.", columnIndex, columnHeader.DataType );
              continue;
            }
            case EvDataTypes.Multi_Text_Values:
            {
              this.LogDebug ( "{0}, value: '{1}'", computedColumnHeader.DataType, row.Column [ columnIndex ] );

              fltColValue = this.SumStringValues ( row.Column [ columnIndex ] );

              row.Column [ columnIndex ] = fltColValue.ToString ( );

              this.LogDebug ( "ColumnValue: {0}.", fltColValue );
              break;
            }
            case EvDataTypes.Numeric:
            case EvDataTypes.Integer:
            {
              fltColValue = EvStatics.getFloat ( row.Column [ columnIndex ], 0 );
              break;
            }
          }//END Data type switch

          //
          // skip of the value is zero, or 'empty'.
          //
          if ( fltColValue == 0 )
          {
            //this.LogDebug ( "fltColValue = 0" );
            continue;
          }

          //
          // Proccess the sum row columns function.
          //
          if ( formula.Contains ( EvTableHeader.COMPUTED_FUNCTION_SUM_ROW_COLUMNS ) == true )
          {
            //this.LogDebug ( "Row Sum row function found" );
            fltValue += fltColValue;
          }

          //
          // Process the multiple column values function.
          //
          else if ( formula.Contains ( EvTableHeader.COMPUTED_FUNCTION_MULTIPLE_ROW_COLUMNS ) == true )
          {
            //this.LogDebug ( "Row multiply function found" );
            string formula1 = formula.Replace ( EvTableHeader.COMPUTED_FUNCTION_MULTIPLE_ROW_COLUMNS, String.Empty );
            formula1 = formula1.Replace ( "(", String.Empty );
            formula1 = formula1.Replace ( ")", String.Empty );
            formula1 = formula1.Replace ( ",", ";" );

            String [ ] parms = formula1.Split ( ';' );

            //this.LogDebug ( "parm array length: {0}.", parms.Length );

            if ( parms.Length < 2 )
            {
              //this.LogDebug ( "Parm length less than 2" );
              continue;
            }

            for ( int i = 0 ; i < parms.Length ; i++ )
            {
              this.LogDebug ( "i: {0}, Value: {1}.", i, parms [ i ] );
            }

            //
            // get the first value.
            //
            if ( parms [ 0 ].Trim ( ) == columnId )
            {
              fltValue1 = fltColValue;
              //this.LogDebug ( "Value1: Row: {0}, Col: {1}, Value1: {2}", rowIndex, columnIndex, fltValue1 );
            }

            //
            // get the first value.
            //
            if ( parms [ 1 ].Trim ( ) == columnId )
            {
              fltValue2 = fltColValue;
              //this.LogDebug ( "Value2: Row: {0}, Col: {1}, Value1: {2}", rowIndex, columnIndex, fltValue2 );
            }
          }
        }//END row column iteration loop

        if ( fltValue1 != 0
            && fltValue2 != 0
            && formula.Contains ( EvTableHeader.COMPUTED_FUNCTION_MULTIPLE_ROW_COLUMNS ) == true )
        {
          fltValue = fltValue1 * fltValue2;
          //this.LogDebug ( "fltValue1: Row: {0}, fltValue2: {1}, fltValue: {2}", fltValue1, fltValue2, fltValue );
        }

        row.Column [ computedColumnIndex ] = fltValue.ToString ( );
        this.LogDebug ( "Computered Column: Row: {0}, Column:{1}, value: {2}.", rowIndex, computedColumnIndex, row.Column [ computedColumnIndex ] );

      }//END row iteration loop

      this.LogMethodEnd ( "UpdateTableComputedColumn" );
    }//END UpdateTableComputedColumn method

    // ==================================================================================
    /// <summary>
    /// This static method sums a string of delimited numeric values.
    /// delimiter are ' ', ';' and ','.
    /// </summary>
    /// <param name="Value">String delimited string</param>
    /// <returns>String: contatinated url.</returns>
    // -----------------------------------------------------------------------------------
    public float SumStringValues( String Value )
    {
      this.LogMethod ( "SumStringValues" );
      this.LogDebug ( "Value: {0}.", Value );
      float returnedValue = 0;

      Value = Value.Trim ( );
      Value = Value.Replace ( ";", "^" );
      Value = Value.Replace ( ",", "^" );
      Value = Value.Replace ( " ", "^" );

      if ( Value.Contains ( "^" ) == false
        && Value.Contains ( @"\" ) == false )
      {
        this.LogMethodEnd ( "SumStringValues" );
        return EvStatics.getFloat ( Value, 0 );
      }

      String [ ] values = Value.Split ( '^' );

      this.LogDebug ( "Value array length: {0}", values.Length );

      foreach ( String svalue in values )
      {
        float columnValue = 0;
        if ( svalue.Contains ( "/" ) == true )
        {
          string [ ] arvalues = svalue.Split ( '/' );
          if ( arvalues [ 0 ] != "0"
            && arvalues [ 1 ] != "0" )
          {
            float numerator = EvStatics.getFloat ( arvalues [ 0 ], 1 );
            float denominator = EvStatics.getFloat ( arvalues [ 0 ], 1 );

            this.LogDebug ( "numerator: {0},denominator: {1}", numerator, denominator );

            columnValue = numerator / denominator;
          }
        }
        else
        {
          columnValue = EvStatics.getFloat ( svalue, 0 );
        }

        this.LogDebug ( "String value: {0}, float value: {1}", svalue, columnValue );

        returnedValue += columnValue;
      }

      this.LogDebug ( "returnedValue: {0}", returnedValue );
      this.LogMethodEnd ( "SumStringValues" );
      return returnedValue;
    }

    // =============================================================================== 
    /// <summary>
    ///   This method generates the Java script object variables for the test.
    /// 
    /// </summary>
    /// <param name="ReturnedFormFields">Name Value Collection</param>
    /// <param name="FormDataId">FormRecord field id to be retrieved.</param>
    /// <returns>Returns a string containing the field value.</returns>
    // ---------------------------------------------------------------------------------
    private string GetReturnedFormFieldValue(
    NameValueCollection ReturnedFormFields,
    String FormDataId )
    {
      //this.LogMethod ( "getReturnedFormFieldValue" );
      //this.LogDebug ( "FormDataId: " + FormDataId );
      // 
      // Initialise the method variables and objects.
      // 
      String [ ] aKeys;
      int index;

      // 
      // Get names of all keys into a string array.
      // 
      aKeys = ReturnedFormFields.AllKeys;

      // 
      // Iterate the keys to find the value for the selected formDataId
      // 
      for ( index = 0 ; index < aKeys.Length ; index++ )
      {
        String key = aKeys [ index ].ToString ( );
        String [ ] aValues = ReturnedFormFields.GetValues ( aKeys [ index ] );

        // 
        // If there is a match then return the value.
        // 
        if ( aKeys [ index ].ToString ( ).ToLower ( ) == FormDataId.ToLower ( ) )
        {
          // this.LogDebug ( "Index: " + index + ", key: " + key
          //  + " Value: " + aValues [ 0 ] );
          //
          // stReturn the first value.
          //
          return aValues [ 0 ].ToString ( ).Trim ( );
        }

      }//END For loop.

      // 
      // If not found return an empty string.
      // 
      return null;

    }//END getReturnedFormFieldValue method

    // =============================================================================== 
    /// <summary>
    /// getReturnedFormFieldValue method.
    /// 
    /// Description:
    ///   This method generates the Java script object variables for the test.
    /// 
    /// </summary>
    /// <param name="ReturnedFormFields">Name Value Collection</param>
    /// <param name="FormDataId">FormRecord field id to be retrieved.</param>
    /// <returns>Returns a string containing the field value.</returns>
    // ---------------------------------------------------------------------------------
    private string [ ] GetReturnedFormFieldValueArray(
      NameValueCollection ReturnedFormFields,
      String FormDataId )
    {
      this.LogMethod ( "getReturnedFormFieldValueArray" );
      this.LogDebug ( "FormDataId: " + FormDataId );
      // 
      // Initialise the method variables and objects.
      // 
      String [ ] aKeys;
      int index;

      // 
      // Get names of all keys into a string array.
      // 
      aKeys = ReturnedFormFields.AllKeys;

      // 
      // Iterate the keys to find the value for the selected formDataId
      // 
      for ( index = 0 ; index < aKeys.Length ; index++ )
      {
        String key = aKeys [ index ].ToString ( );
        String [ ] aValues = ReturnedFormFields.GetValues ( aKeys [ index ] );

        string str = String.Empty;
        foreach ( String st in aValues )
        {
          str += st + " > ";
        }
        this.LogDebug ( "aValues: " + str );
        // 
        // If there is a match then return the value.
        // 
        if ( aKeys [ index ].ToString ( ).ToLower ( ) == FormDataId.ToLower ( ) )
        {
          //
          // stReturn the first value.
          //
          return aValues;
        }

      }//END For loop.

      // 
      // If not found return an empty string.
      // 
      return null;

    }//END getReturnedFormFieldValue method

    // ==================================================================================
    /// <summary>
    /// This method searches through the page group fields to find a matching field..
    /// </summary>
    // ---------------------------------------------------------------------------------
    private void UploadPageImages( )
    {
      this.LogMethod ( "UploadPageImages" );
      // this.LogDebug ( "Global.TempPath: {0}.", Global.TempPath );
      //this.LogDebug ( "Number of files: {0}.", Context.Request.Files.Count );
      try
      {

        // 
        // Initialise the methods variables.
        // 
        string stExtension = String.Empty;

        // 
        // Exit the method of not files are included in the post back.
        // 
        if ( Context.Request.Files.Count == 0 )
        {
          //this.LogDebug ( " No images to upload. Exit" );
          this.LogMethodEnd ( "UploadPageImages" );
          return;
        }


        //
        // Iterate through the uploaded files.
        //
        foreach ( String requestFieldName in Context.Request.Files.AllKeys )
        {
          //this.LogDebug ( "requestFieldName: " + requestFieldName );

          //
          // Skip the dummy test upload.
          //
          if ( requestFieldName == "TestFileUpload" )
          {
            continue;
          }

          // 
          // Get the posted file.
          // 
          HttpPostedFile uploadedFileObject = Context.Request.Files.Get ( requestFieldName );

          //
          // If the file is empty continue to the next file.
          //
          if ( uploadedFileObject.ContentLength == 0 )
          {
            continue;
          }

          string fileName = this.UserSession.UserId + "_" + Path.GetFileName ( uploadedFileObject.FileName );
          fileName = fileName.Replace ( " ", "_" );
          //this.LogDebug ( "Uploaded file name: " + fileName );
          //this.LogDebug ( "length: " + uploadedFileObject.ContentLength );

          //
          // Retrieve the UniFORM field id.
          // 
          String stFieldId = requestFieldName;
          int index = stFieldId.LastIndexOf ( Evado.UniForm.Model.EuField.CONST_IMAGE_FIELD_SUFFIX );
          stFieldId = stFieldId.Substring ( 0, index );
          //this.LogDebug ( "UniFORM FieldId: {0} Value: {1}", stFieldId, fileName );

          //
          // Update the image field value with the uploaded filename.
          //
          this.UserSession.AppData.SetFieldValue ( stFieldId, fileName );

          //this.LogDebug ( "UniFORM FieldId: " + stFieldId );

          string fullFilePath = Global.TempPath + fileName;

          //this.LogDebug ( "Image file path: " + fullFilePath );

          //
          // Save the file to disk.
          //
          uploadedFileObject.SaveAs ( fullFilePath );

          //
          // set the image to the image service.
          //
          this.SendFileRequest ( fileName, uploadedFileObject.ContentType );

          string stEventContent = "Uploaded Image " + uploadedFileObject.FileName + " saved to "
            + fullFilePath + " at " + DateTime.Now.ToString ( "dd-MMM-yyyy HH:mm:ss" );

          this.LogStandard ( stEventContent );
          EventLog.WriteEntry ( Global.EventLogSource, stEventContent, EventLogEntryType.Information );

        }//END upload file iteration loop

      }  // End Try
      catch ( Exception Ex )
      {
        this.LogStandard ( "Exception Event:\r\n" + Evado.Model.EvStatics.getException ( Ex ) );
      }
      // End catch.

      this.LogMethodEnd ( "UploadPageImages" );

      //
      // write out the debug log.
      //
      Global.OutputtDebugLog ( );

    }//END UploadPageImages method

    // ==================================================================================

    /// <summary>
    /// This method searches for image delete fields and set them appropriately.
    /// </summary>
    /// <param name="ReturnedFormFields">Name Value Collection</param>
    // ---------------------------------------------------------------------------------
    private void UpdateImageDeletion( )
    {
      this.LogMethod ( "UpdateImageDeletion" );
      //
      // initialise the methods variables and objects.
      //
      NameValueCollection ReturnedFormFields = Request.Form;
      //this.LogDebug ( "ReturnedFormFields.Count: {0}.", ReturnedFormFields.Count );

      //
      // iterate through all groups and fields looking for image or binary field that have
      // a delete field and pass the value back.
      //
      foreach ( EuGroup group in this.UserSession.AppData.Page.GroupList )
      {
        foreach ( EuField field in group.FieldList )
        {
          if ( field.Type != EvDataTypes.Image
          && field.Type != EvDataTypes.Binary_File )
          {
            continue;
          }

          string deleteFieldid = String.Format ( "{0}{1}", field.FieldId, EuField.CONST_IMAGE_FIELD_DELETE_SUFFIX );

          string deleteValue = this.GetReturnedFormFieldValue ( ReturnedFormFields, deleteFieldid );

          if ( deleteValue != null )
          {
            field.AddParameter ( EuField.DELETE_IMAGE_PARAMETER, deleteValue );
          }
        }
      }
      // 
      // Initialise the methods variables.
      // 
      string stExtension = String.Empty;

      this.LogMethodEnd ( "UpdateImageDeletion" );

    }//END UpdateImageDeletion method

    // ==================================================================================

    /// <summary>
    /// This method searches through the page group fields to find a matching field..
    /// </summary>
    /// <param name="DataId">String: The html field Id.</param>
    /// <returns>Field object.</returns>
    // ---------------------------------------------------------------------------------
    private EuField GetField( String DataId )
    {
      //
      // Iterate through the page groups and fields to find the matching field.
      //
      foreach ( Evado.UniForm.Model.EuGroup group in this.UserSession.AppData.Page.GroupList )
      {
        foreach ( Evado.UniForm.Model.EuField field in group.FieldList )
        {
          String stDataId = field.FieldId;

          if ( stDataId == DataId )
          {
            return field;

          }//END field selection

          if ( stDataId.Contains ( DataId ) == true )
          {
            return field;

          }//END field selection

        }//END Group Field list iteration.

      }//END page group list iteration.

      return null;

    }//END getField method

    // ==================================================================================

    /// <summary>
    /// This method updates the Command parameters with field values.
    /// </summary>
    // ---------------------------------------------------------------------------------
    private void UpdateWebPageCommandObject( )
    {
      this.LogMethod ( "updateWebPageCommandObject" );
      //this.LogDebug ( "Page.EditAccess: " + this.UserSession.AppData.Page.EditAccess );
      //this.LogDebug ( "FieldAnnotationList.Count: " + this.UserSession.FieldAnnotationList.Count );
      //
      // Initialise the methods variables and objects.
      //
      bool fieldStatus = false;
      bool groupStatus = false;

      //
      // Iterate through the page groups and fields to find the matching field.
      //
      foreach ( Evado.UniForm.Model.EuGroup group in this.UserSession.AppData.Page.GroupList )
      {
        //
        // Set the edit access.
        //
        groupStatus = group.EditAccess;

        //
        // Iterat through the group fields.
        //
        foreach ( Evado.UniForm.Model.EuField field in group.FieldList )
        {
          //
          // Set the edit access.
          //
          fieldStatus = field.EditAccess;

          this.LogDebug ( "Group: {0}, field.FieldId: {1}, Status: {2}.",
            group.Title, field.FieldId, fieldStatus );

          if ( field.Type == Evado.Model.EvDataTypes.Read_Only_Text
            || field.Type == Evado.Model.EvDataTypes.External_Image
            || field.Type == Evado.Model.EvDataTypes.Line_Chart
            || field.Type == Evado.Model.EvDataTypes.Null
            || field.Type == Evado.Model.EvDataTypes.Pie_Chart
            || field.Type == Evado.Model.EvDataTypes.Donut_Chart
            || field.Type == Evado.Model.EvDataTypes.Sound
            || field.FieldId == String.Empty )
          {
            this.LogDebug ( " >> FIELD SKIPPED" );
            continue;
          }

          this.LogDebug ( "Group: {0}, FieldId: {1} = {2} = {3}  >> METHOD PARAMETER UPDATED ",
            group.Title, field.FieldId, field.Title, field.Value );

          switch ( field.Type )
          {
            case EvDataTypes.Table:
            case EvDataTypes.Record_Table:
            case Evado.Model.EvDataTypes.Special_Matrix:
            {
              this.updateWebPageCommandTableObject ( field );
              break;
            }
            case EvDataTypes.Image:
            {
              this.UserSession.PageCommand.AddParameter ( field.FieldId, field.Value );

              //
              // extract the image title if present.
              //
              if ( field.hasParameter ( EuField.CONST_IMAGE_FIELD_TITLE_SUFFIX ) == true )
              {
                string fieldId = field.FieldId + EuField.CONST_IMAGE_FIELD_TITLE_SUFFIX;

                string value = field.GetParameter ( fieldId );

                this.UserSession.PageCommand.AddParameter ( fieldId, value );

                this.LogDebug ( "Parameter Name: {0}, Value: {1}", fieldId, value );
              }

              //
              // extract the image delete field if present.
              //
              if ( field.hasParameter ( EuField.DELETE_IMAGE_PARAMETER ) == true )
              {
                string fieldId = field.FieldId + EuField.CONST_IMAGE_FIELD_DELETE_SUFFIX;

                string value = field.GetParameter ( EuField.DELETE_IMAGE_PARAMETER );

                this.UserSession.PageCommand.AddParameter ( fieldId, value );

                this.LogDebug ( "Delete Parameter Name: {0}, Value: {1}", fieldId, value );
              }


              break;
            }
            default:
            {
              this.UserSession.PageCommand.AddParameter ( field.FieldId, field.Value );
              break;
            }
          }
        }//END Group Field list iteration.

      }//END page group list iteration.

      // this.LogDebug ( "Command parameter count: " + this.UserSession.PageCommand.Parameters.Count );

      //
      // Add annotation fields
      //
      for ( int count = 0 ; count < this.UserSession.FieldAnnotationList.Count ; count++ )
      {
        EuKeyValuePair arrAnnotation = this.UserSession.FieldAnnotationList [ count ];

        // this.LogDebug ( "Annotation Field: " + arrAnnotation.Key
        //    + ", Value: " + arrAnnotation.Value );

        this.UserSession.PageCommand.AddParameter ( arrAnnotation.Key, arrAnnotation.Value );
      }

      //this.LogDebug ( "Page command: " + this.UserSession.PageCommand.getAsString ( true, true ) );

      this.LogMethodEnd ( "updateWebPageCommandObject" );
    }//END updateWebPageCommandObject method

    // ==================================================================================

    /// <summary>
    /// This method updates the Command parameters with field values.
    /// </summary>
    // ---------------------------------------------------------------------------------
    private void updateWebPageCommandTableObject(
      Evado.UniForm.Model.EuField field )
    {
      this.LogMethod ( "updateWebPageCommandTableObject" );
      //
      // Iterate through the rows in the table.
      //
      for ( int iRow = 0 ; iRow < field.Table.Rows.Count ; iRow++ )
      {
        string stName = field.FieldId + "_" + ( iRow + 1 ) + "_0";
        string stValue = field.Table.Rows [ iRow ].No.ToString ( );
        this.UserSession.PageCommand.AddParameter ( stName, stValue );

        this.LogDebug ( "NO: Row: {0}, Vaue: {1} ", stName, stValue );

        stName = field.FieldId + "_" + ( iRow + 1 ) + "_ID";
        stValue = field.Table.Rows [ iRow ].RowId;
        this.UserSession.PageCommand.AddParameter ( stName, stValue );

        this.LogDebug ( "ROWID: Row: {0}, Value: {1} ", stName, stValue );

        //
        // Iterate through the columns in the table.
        //
        for ( int iCol = 0 ; iCol < field.Table.ColumnCount ; iCol++ )
        {
          //
          // skip all read only fields as they cannot be updated.
          //
          if ( field.Table.Header [ iCol ].DataType == EvDataTypes.Read_Only_Text )
          {
            this.LogDebug ( "Skip: Col: {0}, Readonly Text ", iCol );
            continue;
          }

          //
          // If the cel is not readonly and has a value then add it to the parameters.
          //
          if ( field.Table.Rows [ iRow ].Column [ iCol ] == String.Empty )
          {
            continue;
          }

          stName = field.FieldId + "_" + ( iRow + 1 ) + "_" + ( iCol + 1 );
          stValue = field.Table.Rows [ iRow ].Column [ iCol ];
          //this.LogDebug ( "Row: {0}, Vaue: {1} ", stName, stValue );

          this.UserSession.PageCommand.AddParameter ( stName, stValue );

        }//END column iteration loop.

      }//END row iteration loop.

      this.LogMethodEnd ( "updateWebPageCommandTableObject" );
    }//END updateWebPageCommandTableObject method

    ///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion
  }
}