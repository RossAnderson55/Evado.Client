using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.ComponentModel;
using System.Diagnostics;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Security;
using System.Net;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

///Evado. namespace references.

using Evado.UniForm.Web;
using Evado.Model.UniForm;

namespace Evado.UniForm.WebClient
{
  /// <summary>
  /// This is the code behind class for the home page.
  /// </summary>
  public partial class DefaultPage : EvPersistentPageState
  {
    //===================================================================================
    /// <summary>
    /// This method add the manadatory field if value attributes to teh field element.
    /// </summary>
    /// <param name="stHtml">StringBuilder containing the page html.</param>
    /// <param name="PageField">Evado.Model.UniForm.Field object</param>
    //-----------------------------------------------------------------------------------
    private void addMandatoryIfAttribute (
      StringBuilder stHtml,
      Evado.Model.UniForm.Field PageField )
    {
      Global.LogDebugMethod ( "addMandatoryIfAttribute method. " );
      //
      // initialise method variables and objects.
      //
      string stMandatoryIfFieldId = PageField.GetParameter ( Evado.Model.UniForm.FieldParameterList.Mandatory_If_Field_Id );
      string stMandatoryIfFieldValue = PageField.GetParameter ( Evado.Model.UniForm.FieldParameterList.Mandatory_If_Value );

      if ( stMandatoryIfFieldId.Length > 0 )
      {
        stHtml.Append ( " data-mandatory-if-field-id=\"" + stMandatoryIfFieldId + "\"" );
      }

      if ( stMandatoryIfFieldValue.Length > 0 )
      {
        stHtml.Append ( " data-mandatory-if-field-value=\"" + stMandatoryIfFieldValue + "\"" );
      }
    }

    //=================================================================================
    /// <summary>
    /// This method sets the fields background colour.
    /// </summary>
    /// <param name="PageField">Evado.Model.UniForm.Field: object</param>
    /// <returns>String: css colour class name</returns>
    //-----------------------------------------------------------------------------------
    private string fieldBackgroundColorClass (
      Evado.Model.UniForm.Field PageField )
    {
      Global.LogDebugMethod ( "fieldBackgroundColorClass method. " );
      //
      // initialise method variables and objects.
      //
      string cssBackgroundColorClass = "";

      Evado.Model.UniForm.Background_Colours background = Model.UniForm.Background_Colours.Default;

      //
      // Set the default background colour if it is set.
      //
      background = PageField.getDefaultBackgroundColor ( );

      //
      // Set the mandatory background colour if the value is empty.
      //
      if ( PageField.Mandatory == true
        && PageField.isEmpty == true )
      {
        Global.LogDebug ( "Field is mandatory." );

        background = PageField.getMandatoryBackGroundColor ( background );
      }

      Global.LogDebug ( "background: " + background );

      switch ( background )
      {
        case Model.UniForm.Background_Colours.Red:
          {
            cssBackgroundColorClass = "Red";
            break;
          }
        case Model.UniForm.Background_Colours.Dark_Red:
          {
            cssBackgroundColorClass = "Dark_Red";
            break;
          }
        case Model.UniForm.Background_Colours.Orange:
          {
            cssBackgroundColorClass = "Orange";
            break;
          }
        case Model.UniForm.Background_Colours.Yellow:
          {
            cssBackgroundColorClass = "Yellow";
            break;
          }
        case Model.UniForm.Background_Colours.Green:
          {
            cssBackgroundColorClass = "Green";
            break;
          }
        case Model.UniForm.Background_Colours.Blue:
          {
            cssBackgroundColorClass = "Blue";
            break;
          }
        case Model.UniForm.Background_Colours.Purple:
          {
            cssBackgroundColorClass = "Purple";
            break;
          }
      }
      Global.LogDebug ( "cssBackgroundColorClass: " + cssBackgroundColorClass );
      Global.LogDebugMethodEnd ( "fieldBackgroundColorClass" );

      return cssBackgroundColorClass;
    }

    // ===================================================================================
    /// <summary>
    /// This method creates a read only field markup
    /// </summary>
    /// <param name="stHtml">StringBuilder object containing the page html.</param>
    /// <param name="TabIndex">int: the current tab index.</param>
    /// <param name="PageField">Field object.</param>
    // ----------------------------------------------------------------------------------
    private void createFieldHeader (
      StringBuilder stHtml,
      Evado.Model.UniForm.Field PageField,
      int TitleWidth,
      bool TitleFullWidth )
    {
      Global.LogClientMethod ( "createFieldHeader method. " );
      Global.LogDebug ( "PageField.FieldId: " + PageField.FieldId );
      Global.LogDebug ( "PageField.Title: " + PageField.Title );
      Global.LogDebug ( "PageField.Type: " + PageField.Type );
      Global.LogDebug ( "CurrentGroupType: " + this._CurrentGroup.GroupType );
      //
      // initialise method variables and objects.
      //
      String stLayout = String.Empty;
      String stField_Suffix = String.Empty;
      String stDescription = String.Empty;
      String stAnnotation = PageField.GetParameter ( Evado.Model.UniForm.FieldParameterList.Annotation );

      if ( PageField.Description != null )
      {
        stDescription = PageField.Description;
      }

      if ( stDescription != String.Empty )
      {
        //stDescription = stDescription.Replace ( "\r\n", "<br/>" );
        stDescription = this.encodeMarkDown ( stDescription );
        stDescription = stDescription.Replace ( "\r\n", "<br/>" );
      }

      if ( stAnnotation != String.Empty )
      {
        //stAnnotation = stAnnotation.Replace ( "\r\n", "<br/>" );
        stAnnotation = this.encodeMarkDown ( stAnnotation );
        stAnnotation = stAnnotation.Replace ( "\r\n", "<br/>" );
      }

      //
      // Set the field layout style classes.
      //
      switch ( PageField.Layout )
      {
        case Evado.Model.UniForm.FieldLayoutCodes.Default:
          {
            stLayout = "layout-normal";
            break;
          }
        case Evado.Model.UniForm.FieldLayoutCodes.Center_Justified:
          {
            stLayout = "layout-center-justified";
            break;
          }
        case Evado.Model.UniForm.FieldLayoutCodes.Column_Layout:
          {
            stLayout = "layout-column";
            break;
          }
        case Evado.Model.UniForm.FieldLayoutCodes.Left_Justified:
          {
            stLayout = "layout-left-justified";
            break;
          }
      }
      String stFieldRowStyling = "class='group-row field " + stLayout + " cf " + this.fieldBackgroundColorClass ( PageField ) + "' ";
      String stFieldTitleStyling = "style='width:" + TitleWidth + "%; ' class='cell title cell-display-text-title'";

      if ( TitleFullWidth == true
        || PageField.Type == Model.EvDataTypes.Table
        || PageField.Type == Model.EvDataTypes.Special_Matrix )
      {
        stFieldTitleStyling = "style='width: 98%; ' class='cell title cell-display-text-title'";

        if ( PageField.Layout != Evado.Model.UniForm.FieldLayoutCodes.Column_Layout )
        {
          stFieldTitleStyling = "style='width:" + TitleWidth + "%; ' class='cell title cell-display-text-title'";
        }
      }

      // always use column layout for tables
      if ( PageField.Type == Evado.Model.EvDataTypes.Table )
        stLayout = "layout-column";

      //
      // Set the field suffix based on the data types.
      //
      switch ( PageField.Type )
      {
        case Evado.Model.EvDataTypes.Address:
          {
            stField_Suffix = "_Address"; break;
          }
        case Evado.Model.EvDataTypes.Boolean:
          {
            stField_Suffix = "_Y"; break;
          }
        case Evado.Model.EvDataTypes.Check_Box_List:
          {
            stField_Suffix = "_1"; break;
          }
        case Evado.Model.EvDataTypes.Image:
          {
            stField_Suffix = Field.CONST_IMAGE_FIELD_SUFFIX; break;
          }
        case Evado.Model.EvDataTypes.Name:
          {
            stField_Suffix = "_FirstName"; break;
          }
        case Evado.Model.EvDataTypes.Radio_Button_List:
          {
            stField_Suffix = "_1"; break;
          }
      }
      stHtml.AppendLine ( "<!-- FIELD HEADER -->" );

      stHtml.AppendLine ( "<div id='" + PageField.Id + "-row' " + stFieldRowStyling + " >" );


      Global.LogDebug ( "Title: " + PageField.Title );
      //
      // Error message
      //
      Global.LogDebug ( "Formattted title: " + PageField.Title );

      stHtml.AppendLine ( "<div " + stFieldTitleStyling + "> " );

      if ( PageField.Title != String.Empty )
      {
        stHtml.AppendLine ( "<label>" + PageField.Title );

        if ( PageField.Mandatory == true && PageField.EditAccess != Evado.Model.UniForm.EditAccess.Disabled )
        {
          stHtml.Append ( "<span class='required'> * </span>" );
        }

        stHtml.Append ( "</label>\r\n " );

        if ( PageField.IsEnabled == true )
        {
          stHtml.AppendLine ( "<div class='error-container '>" );
          stHtml.AppendLine ( "<div id='" + PageField.Id + "-err-row' class='cell cell-error-value'>" );
          stHtml.AppendLine ( "<span id='sp" + PageField.Id + "-err'></span>" );
          stHtml.AppendLine ( "</div></div>\r\n" );
        }
      }

      if ( stDescription != String.Empty )
      {
        stHtml.AppendLine ( "<div class='description'>" + stDescription + "</div>" );
      }

      if ( PageField.Type != Evado.Model.EvDataTypes.Read_Only_Text
        && PageField.Type != Evado.Model.EvDataTypes.Computed_Field
        && PageField.Type != Evado.Model.EvDataTypes.Video
        && PageField.Type != Evado.Model.EvDataTypes.Sound
        && PageField.Type != Evado.Model.EvDataTypes.Html_Content
        && PageField.Type != Evado.Model.EvDataTypes.Http_Link
        && PageField.Type != Evado.Model.EvDataTypes.Streamed_Video
        && PageField.Type != Evado.Model.EvDataTypes.External_Image
        && PageField.Type != Evado.Model.EvDataTypes.Email_Address
        && PageField.Type != Evado.Model.EvDataTypes.Binary_File
        && PageField.Type != Evado.Model.EvDataTypes.Signature
        && PageField.Type != Evado.Model.EvDataTypes.Password )
      {
        //
        // Display the annotatin if it exists.
        //
        if ( this._CurrentGroup.GroupType != Evado.Model.UniForm.GroupTypes.Annotated_Fields
          && this._CurrentGroup.GroupType != Evado.Model.UniForm.GroupTypes.Review_Fields
          && stAnnotation != String.Empty )
        {
          stHtml.Append ( "<div class='description'>" + stAnnotation + "</div>" );
        }

        //
        // Display the annotation field
        //
        else if ( this._CurrentGroup.GroupType == Evado.Model.UniForm.GroupTypes.Annotated_Fields )
        {
          stHtml.Append ( "<div class='description'>" + stAnnotation
           + "<input type='text' "
           + "id='" + PageField.FieldId + Evado.Model.UniForm.Field.CONST_FIELD_ANNOTATION_SUFFIX + "' "
           + "name='" + PageField.FieldId + Evado.Model.UniForm.Field.CONST_FIELD_ANNOTATION_SUFFIX + "' "
           + "tabindex = '" + this._TabIndex + "' "
           + "maxlength='200' "
           + "size='70' "
           + "class='form-control' />\r\n"
           + "</div>" );

          this._TabIndex++;
        }

        //
        // Display the review fields.
        //
        else if ( this._CurrentGroup.GroupType == Evado.Model.UniForm.GroupTypes.Review_Fields )
        {
          stHtml.Append ( "<div class='description'>" + stAnnotation
           + "<br/> Query: "
           + "<input type='checkbox' "
           + "id='" + PageField.FieldId + Evado.Model.UniForm.Field.CONST_FIELD_QUERY_SUFFIX + "' "
           + "name='" + PageField.FieldId + Evado.Model.UniForm.Field.CONST_FIELD_QUERY_SUFFIX + "' "
           + "tabindex = '" + this._TabIndex + "' />\r\n" );

          this._TabIndex++;

          stHtml.Append ( "<input type='text' "
         + "id='" + PageField.FieldId + Evado.Model.UniForm.Field.CONST_FIELD_ANNOTATION_SUFFIX + "' "
         + "name='" + PageField.FieldId + Evado.Model.UniForm.Field.CONST_FIELD_ANNOTATION_SUFFIX + "' "
         + "tabindex = '" + this._TabIndex + "' "
         + "maxlength='200' "
         + "size='70' class='form-control' />\r\n"
         + "</div>" );

          this._TabIndex++;
        }

      }// Editable field.

      //
      // Close field header tag
      //
      stHtml.Append ( "</div>" );
    }//END createFieldHeader method

    // ===================================================================================
    /// <summary>
    /// This method creates a read only field markup
    /// </summary>
    /// <param name="PageField">Field object.</param>
    /// <returns>String html</returns>
    // ----------------------------------------------------------------------------------
    private void createFieldFooter (
      StringBuilder stHtml,
      Evado.Model.UniForm.Field PageField )
    {
      stHtml.Append ( "</div>" );
    }

    // ===================================================================================
    /// <summary>
    /// This method creates a read only field markup
    /// </summary>
    /// <param name="PageField">Field object.</param>
    /// <returns>String html</returns>
    // ----------------------------------------------------------------------------------
    private void createReadOnlyField (
      StringBuilder stHtml,
      Evado.Model.UniForm.Field PageField )
    {
      Global.LogClientMethod ( "createReadOnlyField method. " );
      //
      // Initialise the methods variables and objects.
      //
      int valueColumnWidth = this._GroupValueColumWidth;
      int titleColumnWidth = 100 - valueColumnWidth;
      int stWidth = 20;
      int stHeight = 1;

      if ( PageField.hasParameter ( FieldParameterList.Field_Value_Column_Width ) == true )
      {
        Model.UniForm.FieldValueWidths widthValue = PageField.getValueColumnWidth ( );
        valueColumnWidth = (int) widthValue;
        titleColumnWidth = 100 - valueColumnWidth;
      }

      if ( PageField.hasParameter ( Evado.Model.UniForm.FieldParameterList.Width ) == true )
      {
        stWidth = PageField.GetParameterInt ( Evado.Model.UniForm.FieldParameterList.Width );
      }
      if ( PageField.hasParameter ( Evado.Model.UniForm.FieldParameterList.Height ) == true )
      {
        stHeight = PageField.GetParameterInt ( Evado.Model.UniForm.FieldParameterList.Height );
      }

      String stFieldValueStyling = "style='width:" + valueColumnWidth + "%; padding-top=10px; ' class='cell value cell-display-text-title' ";
      bool fullWidth = false;

      if ( PageField.Layout == FieldLayoutCodes.Column_Layout )
      {
        fullWidth = true;
        stFieldValueStyling = "style='width:98%' class='cell cell-display-text-value cf' ";
      }

      //
      // Ineert the field header
      //
      createFieldHeader ( stHtml, PageField, titleColumnWidth, fullWidth );

      //
      // Encode the readlonly text value.
      //
      if ( PageField.Value != String.Empty )
      {
        stHtml.AppendLine ( "<div " + stFieldValueStyling + " > " );
        //
        // process html content.
        //
        if ( PageField.Value.Contains ( "[/" ) == true )
        {
          Global.LogClient ( "No Markup processing" );

          Global.LogDebug ( "HTML: encoded value: " + PageField.Value );

          String html = Evado.Model.EvStatics.decodeHtmlText ( PageField.Value );

          Global.LogDebug ( "HTML: decoded value" + html );
          stHtml.AppendLine ( html );
        }
        else
        {
          String value = PageField.Value;

          if ( value.Contains ( "\r" ) == true )
          {
            value = this.encodeMarkDown ( value );
            value = value.Replace ( "\r\n", "<br/>" );
          }

          stHtml.AppendLine ( value );
        }
        stHtml.AppendLine ( "</div> " );
      }

      //
      // Insert the field footer elemements
      //
      this.createFieldFooter ( stHtml, PageField );

    }
    // ===================================================================================
    /// <summary>
    /// This method creates a read only field markup
    /// </summary>
    /// <param name="PageField">Field object.</param>
    /// <returns>String html</returns>
    // ----------------------------------------------------------------------------------
    private void createHtmlField (
      StringBuilder sbHtml,
      Evado.Model.UniForm.Field PageField )
    {
      Global.LogClientMethod ( "createHtmlField method. " );
      //
      // Initialise the methods variables and objects.
      //
      int valueColumnWidth = this._GroupValueColumWidth;
      int titleColumnWidth = 100 - valueColumnWidth;
      String stContent = String.Empty;

      String stFieldValueStyling = "style='width:98%' class='cell cell-display-text-value cf' ";

      //
      // Encode the readlonly text value.
      //
      if ( PageField.Value != String.Empty )
      {
        if ( PageField.Value.Contains ( "[/" ) == true )
        {
          Global.LogClient ( "No Markup processing" );

          Global.LogDebug ( "HTML: encoded value: " + PageField.Value );

          String html = Evado.Model.EvStatics.decodeHtmlText ( PageField.Value );

          Global.LogDebug ( "HTML: decoded value" + html );
          sbHtml.AppendLine ( "<div class='description'>" + html + "</div>" );
        }
        else
        {
          String value = PageField.Value;
          value = this.encodeMarkDown ( value );
          value = value.Replace ( "\r\n", "<br/>" );

          sbHtml.AppendLine ( "<div class='description'>" + value + "</div>" );
        }
      }

    }//END method

    // ===================================================================================
    /// <summary>
    /// This method creates a test field html markup
    /// </summary>
    /// <param name="PageField">Field object.</pa
    /// <param name="stHtml">StringBuilder object.</param>
    /// <param name="PageField">Field object.</param>
    /// <param name="TabIndex">Integer: table position.</param>
    /// <param name="Status">ClientFieldEditCodes enumerated status.</param>
    // ----------------------------------------------------------------------------------
    private void createImageField (
      StringBuilder stHtml,
      Evado.Model.UniForm.Field PageField,
      Evado.Model.UniForm.EditAccess EditAccess )
    {
      Global.LogClientMethod ( "createImageField method" );
      Global.LogDebug ( "RelativeBinaryDownloadURL: " + Global.RelativeBinaryDownloadURL );
      Global.LogDebug ( "PageField.FieldId: " + PageField.FieldId );
      Global.LogDebug ( "PageField.Layout: " + PageField.Layout );
      Global.LogDebug ( "PageField.Value: " + PageField.Value );
      //
      // Initialise the methods variables and objects.
      //
      int valueColumnWidth = this._GroupValueColumWidth;
      int titleColumnWidth = 100 - valueColumnWidth;
      string stImageUrl = Global.RelativeBinaryDownloadURL + PageField.Value;
      String stSize = "400";
      this.TestFileUpload.Visible = true;
      bool fullWidth = false;

      if ( PageField.hasParameter ( Evado.Model.UniForm.FieldParameterList.Width ) == true )
      {
        stSize = PageField.GetParameter ( Evado.Model.UniForm.FieldParameterList.Width );
      }

      // 
      // If the url does not include a http statement add the default image url 
      // 
      stImageUrl = stImageUrl.ToLower ( );
      stImageUrl = Global.concatinateHttpUrl ( Global.RelativeBinaryDownloadURL, PageField.Value );

      Global.LogClient ( "stImageUrl: " + stImageUrl );

      String stFieldValueStyling = "style='width:" + valueColumnWidth + "%' class='cell value cell-image-value cf' "; // class='cell value cell-image-value cf' ";

      if ( PageField.Layout == FieldLayoutCodes.Column_Layout )
      {
        fullWidth = true;
        stFieldValueStyling = "style='width:98%' class='cell cell-image-value cf' ";
      }
      //cell-input-text-value
      // Ineert the field header
      //
      this.createFieldHeader ( stHtml, PageField, titleColumnWidth, fullWidth );

      stHtml.AppendLine ( "<div " + stFieldValueStyling + " >" );
      stHtml.AppendLine ( "<div id='sp" + PageField.Id + "' >" );
      //
      // Insert the field elements
      //
      if ( PageField.Value != String.Empty )
      {
        Global.LogClient ( "Image file exists " + PageField.Value );

        stHtml.AppendLine ( "<a href='" + stImageUrl + "' target='_blank' > "
          + "<img alt='Image " + PageField.Value + "' " + "src='" + stImageUrl + "' width='" + stSize + "'/></a>" );
      }

      if ( EditAccess == Model.UniForm.EditAccess.Inherited
        || EditAccess == Evado.Model.UniForm.EditAccess.Enabled )
      {
        stHtml.AppendLine ( "<input name='" + PageField.FieldId + Field.CONST_IMAGE_FIELD_SUFFIX + "' "
          + "type='file' id='" + PageField.FieldId + Field.CONST_IMAGE_FIELD_SUFFIX + "' "
          + "size='80' />" );
      }
      stHtml.AppendLine ( "<input type='hidden' "
           + "id='" + PageField.FieldId + "' "
           + "name='" + PageField.FieldId + "' "
           + "value='" + PageField.Value + "' /> " );
      stHtml.AppendLine ( "</div>" );
      stHtml.AppendLine ( "</div>" );

      //
      // Insert the field footer
      //
      this.createFieldFooter ( stHtml, PageField );

      Global.LogClient ( "END createImageField\r\n" );
    }//END Field Method

    // ===================================================================================
    /// <summary>
    /// This method creates a text field html markup
    /// </summary>
    /// <param name="PageField">Field object.</param>
    /// <param name="TabIndex">THe field index on the page</param>
    /// <param name="Status">ClientFieldEditCodes enumerated status.</param>
    /// <returns>String html</returns>
    // ----------------------------------------------------------------------------------
    private void createTextField (
      StringBuilder stHtml,
      Evado.Model.UniForm.Field PageField,
      Evado.Model.UniForm.EditAccess Status )
    {
      Global.LogClientMethod ( "createTextField method. " );
      //
      // Initialise the methods variables and objects.
      //
      int valueColumnWidth = this._GroupValueColumWidth;
      int titleColumnWidth = 100 - valueColumnWidth;
      String stSize = PageField.GetParameter ( Evado.Model.UniForm.FieldParameterList.Width );
      String stRows = PageField.GetParameter ( Evado.Model.UniForm.FieldParameterList.Height );

      //
      // Set the normal validation parameters.
      //
      string stValidationMethod = " onchange=\"Evado.Form.onTextChange( this, this.value )\" ";

      if ( PageField.hasParameter ( FieldParameterList.Field_Value_Column_Width ) == true )
      {
        Model.UniForm.FieldValueWidths widthValue = PageField.getValueColumnWidth ( );
        valueColumnWidth = (int) widthValue;
        titleColumnWidth = 100 - valueColumnWidth;
      }

      String stFieldValueStyling = "style='width:" + valueColumnWidth + "%' class='cell value cell-input-text-value cf' ";

      //
      // Set default width
      //
      if ( stSize == String.Empty )
      {
        stSize = "20";
      }

      //
      // Ineert the field header
      //
      this.createFieldHeader ( stHtml, PageField, titleColumnWidth, false );

      //
      // Insert the field data control
      //
      stHtml.AppendLine ( "<div " + stFieldValueStyling + " > " );
      stHtml.AppendLine ( "<div id='sp" + PageField.Id + "' >" );
      stHtml.AppendLine ( "<input type='text' "
         + "id='" + PageField.FieldId + "' "
         + "name='" + PageField.FieldId + "' "
         + "value='" + PageField.Value + "' "
         + "tabindex = '" + _TabIndex + "' "
         + "maxlength='" + stSize + "' "
         + "size='" + stSize + "' " );
      stHtml.Append ( "data-fieldid='" + PageField.FieldId + "' "
         + "class='form-control' "
        + stValidationMethod + " data-parsley-trigger=\"change\" " );

      if ( PageField.Mandatory == true && Status != Model.UniForm.EditAccess.Disabled )
      {
        //stHtml.Append ( " required " );
      }

      //this.addMandatoryIfAttribute ( stHtml, PageField );

      if ( Status == Evado.Model.UniForm.EditAccess.Disabled )
      {
        stHtml.Append ( " disabled='disabled' " );
      }

      stHtml.AppendLine ( "/>" );
      stHtml.AppendLine ( "</div>" );
      stHtml.AppendLine ( "</div>" );

      this._TabIndex += 2;

      //
      // Insert the field footer elemements
      //
      this.createFieldFooter ( stHtml, PageField );

    }//END Field Method

    // ===================================================================================
    /// <summary>
    /// This method creates a text field html markup
    /// </summary>
    /// <param name="PageField">Field object.</param>
    /// <param name="TabIndex">THe field index on the page</param>
    /// <param name="Status">ClientFieldEditCodes enumerated status.</param>
    /// <returns>String html</returns>
    // ----------------------------------------------------------------------------------
    private void createComputedField (
      StringBuilder stHtml,
      Evado.Model.UniForm.Field PageField,
      Evado.Model.UniForm.EditAccess Status )
    {
      Global.LogClientMethod ( "createComputedField method. " );
      //
      // Initialise the methods variables and objects.
      //
      int valueColumnWidth = this._GroupValueColumWidth;
      int titleColumnWidth = 100 - valueColumnWidth;
      String stWidth = PageField.GetParameter ( Evado.Model.UniForm.FieldParameterList.Width );
      String stRows = PageField.GetParameter ( Evado.Model.UniForm.FieldParameterList.Height );
      String stFieldValueStyling = "style='width:" + valueColumnWidth + "%' class='cell value cell-input-text-value cf' ";

      //
      // Set default width
      //
      if ( stWidth == String.Empty )
      {
        stWidth = "20";
      }

      //
      // Ineert the field header
      //
      this.createFieldHeader ( stHtml, PageField, titleColumnWidth, false );

      //
      // Insert the field data control
      //
      stHtml.AppendLine ( "<div " + stFieldValueStyling + " >" );
      stHtml.AppendLine ( "<span id='sp" + PageField.Id + "'>" );
      stHtml.AppendLine ( "<input type='text' "
         + "id='" + PageField.FieldId + "' "
         + "name='" + PageField.FieldId + "' "
         + "value='" + PageField.Value + "' "
         + "tabindex = '" + _TabIndex + "' "
         + "maxlength='" + stWidth + "' "
         + "size='" + stWidth + "' "
         + "class='form-control' " );

      if ( Status == Evado.Model.UniForm.EditAccess.Disabled )
      {
        stHtml.Append ( " readonly='readonly' " );
      }

      stHtml.AppendLine ( "/>" );
      stHtml.AppendLine ( "</span>" );
      stHtml.AppendLine ( "</div>" );

      this._TabIndex += 2;

      //
      // Insert the field footer elemements
      //
      this.createFieldFooter ( stHtml, PageField );

    }//END Field Method

    // ===================================================================================
    /// <summary>
    /// This method creates a free test field html markup
    /// </summary>
    /// <param name="PageField">Field object.</param>
    /// <param name="TabIndex">THe field index on the page</param>
    /// <param name="Status">ClientFieldEditCodes enumerated status.</param>
    /// <returns>String html</returns>
    // ----------------------------------------------------------------------------------
    private void createFreeTextField (
      StringBuilder stHtml,
      Evado.Model.UniForm.Field PageField,
      Evado.Model.UniForm.EditAccess Status )
    {
      Global.LogClientMethod ( "createFreeTextField method. " );
      //
      // Initialise the methods variables and objects.
      //
      int valueColumnWidth = this._GroupValueColumWidth;
      int titleColumnWidth = 100 - valueColumnWidth;
      String stFieldValueStyling = "style='width:" + valueColumnWidth + "%' class='cell value cell-textarea-value cf' ";
      string stValidationMethod = " onchange=\"Evado.Form.onTextChange( this, this.value )\" ";

      int width = 40;
      int height = 5;

      if ( PageField.hasParameter ( Evado.Model.UniForm.FieldParameterList.Width ) == true )
      {
        width = PageField.GetParameterInt ( Evado.Model.UniForm.FieldParameterList.Width );
      }
      if ( PageField.hasParameter ( Evado.Model.UniForm.FieldParameterList.Height ) == true )
      {
        height = PageField.GetParameterInt ( Evado.Model.UniForm.FieldParameterList.Height );
      }
      int maxLength = (int) ( width * height * 2 );

      //
      // Ineert the field header
      //
      this.createFieldHeader ( stHtml, PageField, titleColumnWidth, false );

      //
      // Insert the field elements
      //
      stHtml.AppendLine ( "<div " + stFieldValueStyling + " > " );
      stHtml.AppendLine ( "<div id='sp" + PageField.Id + "'>" );
      stHtml.AppendLine ( "<textarea "
        + "id='" + PageField.FieldId + "' "
        + "name='" + PageField.FieldId + "' "
        + "tabindex = '" + _TabIndex + "' "
        + "rows='" + height + "' "
        + "cols='" + width + "' "
        + "maxlength='" + maxLength + "' "
        + "class='form-control' "
        + stValidationMethod + " data-parsley-trigger=\"change\" " );

      if ( Status == Evado.Model.UniForm.EditAccess.Disabled )
      {
        stHtml.Append ( " disabled='disabled' " );
      }

      stHtml.AppendLine ( ">"
      + PageField.Value
      + "</textarea>" );
      stHtml.AppendLine ( "</div>" );
      stHtml.AppendLine ( "</div>" );

      this._TabIndex += 2;

      //
      // Insert the field footer elemements
      //
      this.createFieldFooter ( stHtml, PageField );

    }//END Field Method

    // ===================================================================================
    /// <summary>
    /// This method creates a numeric field html markup
    /// </summary>
    /// <param name="PageField">Field object.</param>
    /// <param name="TabIndex">THe field index on the page</param>
    /// <param name="Status">ClientFieldEditCodes enumerated status.</param>
    /// <returns>String html</returns>
    // ----------------------------------------------------------------------------------
    private void createNumericField (
      StringBuilder stHtml,
      Evado.Model.UniForm.Field PageField,
      Evado.Model.UniForm.EditAccess Status )
    {
      Global.LogClientMethod ( "createNumericField method. " );
      //
      // Initialise the methods variables and objects.
      //
      int valueColumnWidth = this._GroupValueColumWidth;
      int titleColumnWidth = 100 - valueColumnWidth;

      String stFieldValueStyling = "style='width:" + valueColumnWidth + "%' class='cell value cell-number-value cf' ";
      String stSize = PageField.GetParameter ( Evado.Model.UniForm.FieldParameterList.Width );
      String stUnit = PageField.GetParameter ( Evado.Model.UniForm.FieldParameterList.Unit );
      String stMinValue = PageField.GetParameter ( Evado.Model.UniForm.FieldParameterList.Min_Value );
      String stMaxValue = PageField.GetParameter ( Evado.Model.UniForm.FieldParameterList.Max_Value );
      String stMinAlert = PageField.GetParameter ( Evado.Model.UniForm.FieldParameterList.Min_Alert );
      String stMaxAlert = PageField.GetParameter ( Evado.Model.UniForm.FieldParameterList.Max_Alert );
      String stCustomValidation = PageField.GetParameter ( Model.UniForm.FieldParameterList.Validation_Callback );
      String stCssValid = PageField.GetParameter ( Evado.Model.UniForm.FieldParameterList.BG_Validation );
      String stCssAlert = PageField.GetParameter ( Evado.Model.UniForm.FieldParameterList.BG_Alert );
      String stCssNormal = PageField.GetParameter ( Evado.Model.UniForm.FieldParameterList.BG_Normal );

      if ( stMinAlert != String.Empty
        && stMaxAlert == String.Empty )
      {
        stMinAlert = stMaxValue;
      }

      //
      // Set the normal validation parameters.
      //
      string stValidationMethod = " onchange=\"Evado.Form.onRangeValidation( this, this.value )\" ";

      if ( PageField.Type == Model.EvDataTypes.Integer )
      {
        stValidationMethod = " data-parsley-integerna data-parsley-trigger=\"change\" ";
      }

      if ( stCustomValidation != String.Empty )
      {
        stValidationMethod = " onchange=\"Evado.Form.onCustomValidation('" + stCustomValidation + "', this, this.value )\" ";
      }

      if ( stUnit.Contains ( "10^" ) == true )
      {
        stUnit = Regex.Replace ( stUnit, @"10\^([-0-9])(.*)", "10<span class='SuperScript'>$1</span>$2" );
      }

      if ( stUnit != String.Empty )
      {
        stUnit = "<span class='form-unit' >" + stUnit + "</span>";
      }

      //
      // Set default width
      //
      if ( stSize == String.Empty )
      {
        stSize = "12";
      }

      //
      // Ineert the field header
      //
      this.createFieldHeader ( stHtml, PageField, titleColumnWidth, false );

      //
      // Convert numeric null (-1E+38F) to text null (NA)
      //
      PageField.Value = Evado.Model.EvStatics.convertNumNullToTextNull ( PageField.Value );

      //
      // Insert the field elements
      //
      stHtml.AppendLine ( "<div " + stFieldValueStyling + " > " );
      stHtml.AppendLine ( "<span id='sp" + PageField.Id + "' >" );
      stHtml.AppendLine ( "<input type='text' "
        + "id='" + PageField.FieldId + "' "
        + "name='" + PageField.FieldId + "' "
        + "value='" + PageField.Value + "' "
        + "maxlength='" + stSize + "' "
        + "tabindex = '" + _TabIndex + "' "
        + "size='" + stSize + "' " ); // class='form-control'

      stHtml.AppendLine ( "data-fieldid='" + PageField.FieldId + "' " );
      if ( stMinValue != String.Empty )
      {
        stHtml.Append ( " data-min-value='" + stMinValue + "' "
          + " data-max-value='" + stMaxValue + "' " );
      }
      if ( stMinAlert != String.Empty )
      {
        stHtml.Append ( " data-min-alert='" + stMinAlert + "' "
          + " data-max-alert='" + stMaxAlert + "' " );
      }
      if ( stCssValid != String.Empty )
      {
        stHtml.Append ( " data-css-valid='" + stCssValid + "' " );
      }
      if ( stCssAlert != String.Empty )
      {
        stHtml.Append ( " data-css-alert='" + stCssAlert + "' " );
      }
      if ( stCssNormal != String.Empty )
      {
        stHtml.Append ( " data-css-norm='" + stCssNormal + "' \r\n" );
      }
      stHtml.Append ( " " + stValidationMethod + " data-parsley-trigger=\"change\" " );

      if ( PageField.Mandatory == true && Status != Model.UniForm.EditAccess.Disabled )
      {
        //stHtml.Append ( " required " );
      }

      //this.addMandatoryIfAttribute ( stHtml, PageField );

      if ( Status == Evado.Model.UniForm.EditAccess.Disabled )
      {
        stHtml.Append ( " disabled='disabled' " );
      }

      stHtml.Append ( "  " );
      stHtml.AppendLine ( "/>" );
      stHtml.AppendLine ( "</span>" );
      stHtml.AppendLine ( stUnit );

      stHtml.AppendLine ( "</div>" );

      this._TabIndex += 2;

      //
      // Insert the field footer elemements
      //
      this.createFieldFooter ( stHtml, PageField );

    }//END Field Method

    // ===================================================================================
    /// <summary>
    /// This method creates a date field html markup
    /// </summary>
    /// <param name="PageField">Field object.</param>
    /// <param name="TabIndex">Integer: table position.</param>
    /// <param name="Status">ClientFieldEditCodes enumerated status.</param>
    /// <returns>String html</returns>
    // ----------------------------------------------------------------------------------
    private void createDateField (
      StringBuilder stHtml,
      Evado.Model.UniForm.Field PageField,
      Evado.Model.UniForm.EditAccess Status )
    {
      Global.LogDebugMethod ( "createDateField2 Method" );
      //
      // Initialise the methods variables and objects.
      //
      int valueColumnWidth = this._GroupValueColumWidth;
      int titleColumnWidth = 100 - valueColumnWidth;

      String stFieldValueStyling = "style='width:" + valueColumnWidth + "%' class='cell value cell-date-value ' "; //cf
      String stMin_Value = PageField.GetParameter ( Evado.Model.UniForm.FieldParameterList.Min_Value );
      String stMax_Value = PageField.GetParameter ( Evado.Model.UniForm.FieldParameterList.Max_Value );
      String stMinAlert = PageField.GetParameter ( Evado.Model.UniForm.FieldParameterList.Min_Alert );
      String stMaxAlert = PageField.GetParameter ( Evado.Model.UniForm.FieldParameterList.Max_Alert );
      String stCssValid = PageField.GetParameter ( Evado.Model.UniForm.FieldParameterList.BG_Validation );
      String stCssAlert = PageField.GetParameter ( Evado.Model.UniForm.FieldParameterList.BG_Alert );
      String stCustomValidation = PageField.GetParameter ( Evado.Model.UniForm.FieldParameterList.Validation_Callback );
      String stdateSelection = String.Empty;
      String stDate = PageField.Value;
      String stDay = String.Empty;
      String stMonth = String.Empty;
      String stYear = String.Empty;
      int maxYear = DateTime.Now.Year + 4;
      int minYear = DateTime.Now.Year - 100;
      int iYear = 0;
      String stFormat = "dd - MMM - yyyy";

      Global.LogClient ( "Min_Value: " + stMin_Value + ", Max_Value: " + stMax_Value );

      //
      // set the time format structure.
      //
      if ( PageField.hasParameter ( Model.UniForm.FieldParameterList.Format ) )
      {
        string value = PageField.GetParameter ( Model.UniForm.FieldParameterList.Format );
        value = value.ToLower ( );
        if ( value.Length <= 8 )
        {
          String format = String.Empty;

          if ( value.Contains ( "dd" ) == true )
          {
            format = "dd";
          }
          if ( value.Contains ( "mmm" ) == true )
          {
            format = " - MMM";
          }
          if ( value.Contains ( "yy" ) == true
            || value.Contains ( "yyyy" ) == true )
          {
            format = " - yyyy";
          }
          stFormat = format;
        }
      }
      //
      // get the maximum year.
      //
      stMax_Value = stMax_Value.Replace ( " ", "-" );
      String [ ] arDate = stMax_Value.Split ( '-' );
      if ( arDate.Length > 2 )
      {
        stYear = arDate [ 2 ];
      }
      else
        if ( arDate.Length > 1 )
        {
          stYear = arDate [ 1 ];
        }
        else
        {
          stYear = PageField.Value;
        }

      if ( stYear != String.Empty )
      {
        if ( int.TryParse ( stYear, out iYear ) == true )
        {
          maxYear = iYear;
        }
      }

      //
      // get the maximum year.
      //
      stYear = String.Empty;
      stMin_Value = stMin_Value.Replace ( " ", "-" );
      arDate = stMin_Value.Split ( '-' );
      if ( arDate.Length > 2 )
      {
        stYear = arDate [ 2 ];
      }
      else
        if ( arDate.Length > 1 )
        {
          stYear = arDate [ 1 ];
        }
        else
        {
          stYear = PageField.Value;
        }

      if ( stYear != String.Empty )
      {
        if ( int.TryParse ( stYear, out iYear ) == true )
        {
          minYear = iYear;
        }
      }

      Global.LogClient ( "minYear: " + minYear + " maxyear: " + maxYear );

      //
      // Set the normal validation parameters.
      //
      string stValidationMethod = " onchange=\"Evado.Form.onDateSelectChange( this, '" + PageField.FieldId + "' )\" ";

      //
      // Ineert the field header
      //
      this.createFieldHeader ( stHtml, PageField, titleColumnWidth, false );

      stDate = PageField.Value;
      stDate = stDate.Replace ( " ", "-" );

      arDate = stDate.Split ( '-' );
      if ( arDate.Length > 2 )
      {
        stDay = arDate [ 0 ];
        stMonth = arDate [ 1 ].ToUpper ( );
        stYear = arDate [ 2 ];
      }
      else
        if ( arDate.Length > 1 )
        {
          stDay = String.Empty;
          stMonth = arDate [ 0 ].ToUpper ( );
          stYear = arDate [ 1 ];
        }
        else
        {
          stDay = String.Empty;
          stMonth = String.Empty;
          stYear = PageField.Value;
        }

      List<Evado.Model.EvOption> dayList = new List<Model.EvOption> ( );
      dayList.Add ( new Model.EvOption ( ) );

      for ( int day = 1; day <= 31; day++ )
      {
        dayList.Add ( new Model.EvOption ( day.ToString ( "00" ) ) );
      }

      List<Evado.Model.EvOption> monthList = Evado.Model.EvStatics.getStringAsOptionList (
        ":;JAN:" + EuLabels.Month_JAN + ";FEB:" + EuLabels.Month_FEB + ";MAR:" + EuLabels.Month_MAR
        + ";APR:" + EuLabels.Month_APR + ";MAY:" + EuLabels.Month_MAY + ";JUN:" + EuLabels.Month_JUN
        + ";JUL:" + EuLabels.Month_JUL + ";AUG:" + EuLabels.Month_AUG + ";SEP:" + EuLabels.Month_SEP
        + ";OCT:" + EuLabels.Month_OCT + ";NOV:" + EuLabels.Month_NOV + ";DEC:" + EuLabels.Month_DEC );

      List<Evado.Model.EvOption> yearList = new List<Model.EvOption> ( );
      yearList.Add ( new Model.EvOption ( ) );

      for ( int yr = maxYear; yr > minYear; yr-- )
      {
        yearList.Add ( new Model.EvOption ( yr.ToString ( "0000" ) ) );
      }

      //
      // Insert the field elements
      //
      stHtml.AppendLine ( "<div " + stFieldValueStyling + " > " );
      stHtml.AppendLine ( "<span id='sp" + PageField.Id + "' class='form-field-container-inline' >" );


      if ( Status == Evado.Model.UniForm.EditAccess.Enabled )
      {
        if ( stFormat.Contains ( "dd" ) == true )
        {
          stHtml.Append ( "<select "
            + "id='" + PageField.FieldId + "_DAY' "
            + "name='" + PageField.FieldId + "_DAY' "
            + "tabindex = '" + this._TabIndex + "' "
            + "value='" + stDay + "' "
            + "class='form-field-inline' "
            + "data-parsley-trigger=\"change\" "
            + stValidationMethod );

          if ( PageField.Mandatory == true && Status != Evado.Model.UniForm.EditAccess.Disabled )
          {
            //stHtml.Append ( " required " );
          }

          stHtml.AppendLine ( ">" );

          foreach ( Evado.Model.EvOption option in dayList )
          {
            stHtml.Append ( " <option value=\"" + option.Value + "\" " );
            if ( option.Value == stDay )
            {
              stHtml.Append ( " selected='selected' " );
            }
            stHtml.AppendLine ( ">" + option.Description + " </option>\r\n" );
          }
          stHtml.Append ( " </select>\r\n" );

          stHtml.AppendLine ( "- " );

          this._TabIndex++;
        }

        if ( stFormat.Contains ( "MMM" ) == true )
        {

          stHtml.Append ( "<select "
            + "id='" + PageField.FieldId + "_MTH' "
            + "name='" + PageField.FieldId + "_MTH' "
            + "value='" + stMonth + "' "
            + "tabindex = '" + this._TabIndex + "' "
            + "class='form-field-inline' "
            + "data-parsley-trigger=\"change\" "
            + stValidationMethod );

          if ( PageField.Mandatory == true && Status != Evado.Model.UniForm.EditAccess.Disabled )
          {
            //stHtml.Append ( " required " );
          }

          stHtml.AppendLine ( ">" );
          this._TabIndex++;

          foreach ( Evado.Model.EvOption option in monthList )
          {
            stHtml.Append ( " <option value=\"" + option.Value + "\" " );
            if ( option.Value == stMonth )
            {
              stHtml.Append ( " selected='selected' " );
            }
            stHtml.AppendLine ( ">" + option.Description + " </option>\r\n" );
          }
          stHtml.Append ( " </select>\r\n" );

          stHtml.AppendLine ( "- " );

          this._TabIndex++;
        }


        if ( stFormat.Contains ( "yyyy" ) == true )
        {

          stHtml.Append ( "<select "
            + "id='" + PageField.FieldId + "_YR' "
            + "name='" + PageField.FieldId + "_YR' "
            + "tabindex = '" + this._TabIndex + "' "
            + "value='" + stYear + "' "
            + "class='form-field-inline' "
            + "data-parsley-trigger=\"change\" "
            + stValidationMethod );

          if ( PageField.Mandatory == true && Status != Evado.Model.UniForm.EditAccess.Disabled )
          {
            //stHtml.Append ( " required " );
          }

          stHtml.AppendLine ( ">" );

          foreach ( Evado.Model.EvOption option in yearList )
          {
            stHtml.Append ( " <option value=\"" + option.Value + "\" " );
            if ( option.Value == stYear )
            {
              stHtml.Append ( " selected='selected' " );
            }
            stHtml.AppendLine ( ">" + option.Description + " </option>\r\n" );
          }
          stHtml.AppendLine ( " </select>\r\n" );

          stHtml.AppendLine ( "</span>" );

          this._TabIndex++;
        }
        stHtml.Append ( "<br/><span style='margin:10pt;'>" + stFormat + "</span>" );

        stHtml.Append ( "<input type='hidden' "
          + "id='" + PageField.FieldId + "' "
          + "name='" + PageField.FieldId + "' "
          + "value='" + PageField.Value + "' />" );
      }
      else
      {
        stHtml.Append ( "<input type='text' "
          + "id='" + PageField.FieldId + "' "
          + "name='" + PageField.FieldId + "' "
          + "value='" + PageField.Value + "' disabled='disabled' />" );
      }

      stHtml.AppendLine ( "</div>" );

      //
      // Insert the field footer elemements
      //
      this.createFieldFooter ( stHtml, PageField );

    }//END Field Method

    // ===================================================================================
    /// <summary>
    /// This method creates a time field html markup
    /// </summary>
    /// <param name="PageField">Field object.</param>
    /// <param name="TabIndex">Integer: table position.</param>
    /// <param name="Status">ClientFieldEditCodes enumerated status.</param>
    /// <returns>String html</returns>
    // ----------------------------------------------------------------------------------
    private void createTimeField (
      StringBuilder stHtml,
      Evado.Model.UniForm.Field PageField,
      Evado.Model.UniForm.EditAccess Status )
    {
      Global.LogClientMethod ( "createTimeField method. " );
      //
      // Initialise the methods variables and objects.
      //
      int valueColumnWidth = this._GroupValueColumWidth;
      int titleColumnWidth = 100 - valueColumnWidth;
      String stSize = PageField.GetParameter ( Evado.Model.UniForm.FieldParameterList.Width );
      String stFieldValueStyling = "style='width:" + valueColumnWidth + "%' class='cell value cell-date-value ' "; //cf
      String stCustomValidation = PageField.GetParameter ( Model.UniForm.FieldParameterList.Validation_Callback );
      String stFormat = "HH : MM : SS";
      String time = PageField.Value;
      int timeElements = 2;
      String stHour = "12";
      String stMinutes = "00";
      String stSeconds = "00";

      if ( time != String.Empty )
      {
        string [ ] artime = time.Split ( ':' );
        timeElements = artime.Length;

        stHour = artime [ 0 ];
        stMinutes = artime [ 1 ];
        if ( timeElements > 2 )
        {
          stSeconds = artime [ 2 ];
        }
      }

      if ( timeElements <= 2 )
      {
        stFormat = "HH : MM";
      }

      List<Evado.Model.EvOption> hourList = new List<Model.EvOption> ( );
      hourList.Add ( new Model.EvOption ( ) );

      for ( int hr = 0; hr < 24; hr++ )
      {
        hourList.Add ( new Model.EvOption ( hr.ToString ( "00" ) ) );
      }


      List<Evado.Model.EvOption> minuteList = new List<Model.EvOption> ( );
      minuteList.Add ( new Model.EvOption ( ) );

      for ( int min = 0; min < 60; min++ )
      {
        minuteList.Add ( new Model.EvOption ( min.ToString ( "00" ) ) );
      }

      //
      // Set the normal validation parameters.
      //
      string stValidationMethod = " onchange=\"Evado.Form.onTimeSelectChange( this, '" + PageField.FieldId + "' )\" ";

      if ( stCustomValidation != String.Empty )
      {
        stValidationMethod = " onchange=\"Evado.Form.onCustomValidation('" + stCustomValidation + "', this, this.value )\" ";
      }

      //
      // Set default width
      //
      if ( stSize == String.Empty )
      {
        stSize = "9";
      }

      //
      // Ineert the field header
      //
      this.createFieldHeader ( stHtml, PageField, titleColumnWidth, false );

      //
      // Insert the field elements
      //
      stHtml.Append ( "<div " + stFieldValueStyling + " > "
        + "<span id='sp" + PageField.Id + "' class='form-field-container-inline' >" );

      stHtml.Append ( "<select "
        + "id='" + PageField.FieldId + "_HR' "
        + "name='" + PageField.FieldId + "_HR' "
        + "tabindex = '" + this._TabIndex + "' "
        + "value='" + stHour + "' "
        + "class='form-field-inline' "
        + "data-parsley-trigger=\"change\" "
        + stValidationMethod );

      if ( PageField.Mandatory == true && Status != Evado.Model.UniForm.EditAccess.Disabled )
      {
        //stHtml.Append ( " required " );
      }

      if ( Status == Evado.Model.UniForm.EditAccess.Disabled )
      {
        stHtml.Append ( "disabled='disabled' " );
      }

      stHtml.AppendLine ( ">" );

      foreach ( Evado.Model.EvOption option in hourList )
      {
        stHtml.Append ( " <option value=\"" + option.Value + "\" " );
        if ( option.Value == stHour )
        {
          stHtml.Append ( " selected='selected' " );
        }
        stHtml.AppendLine ( ">" + option.Description + " </option>\r\n" );
      }
      stHtml.Append ( " </select>\r\n" );

      stHtml.AppendLine ( ": " );

      this._TabIndex++;

      stHtml.Append ( "<select "
        + "id='" + PageField.FieldId + "_MIN' "
        + "name='" + PageField.FieldId + "_MIN' "
        + "tabindex = '" + this._TabIndex + "' "
        + "value='" + stMinutes + "' "
        + "class='form-field-inline' "
        + "data-parsley-trigger=\"change\" "
        + stValidationMethod );

      if ( PageField.Mandatory == true && Status != Evado.Model.UniForm.EditAccess.Disabled )
      {
        //stHtml.Append ( " required " );
      }

      if ( Status == Evado.Model.UniForm.EditAccess.Disabled )
      {
        stHtml.Append ( "disabled='disabled' " );
      }

      stHtml.AppendLine ( ">\r\n" );

      foreach ( Evado.Model.EvOption option in minuteList )
      {
        stHtml.Append ( " <option value=\"" + option.Value + "\" " );
        if ( option.Value == stMinutes )
        {
          stHtml.Append ( " selected='selected' " );
        }
        stHtml.AppendLine ( ">" + option.Description + " </option>\r\n" );
      }
      stHtml.AppendLine ( " </select>\r\n" );

      this._TabIndex++;

      if ( stFormat.Contains ( "SS" ) == true || stFormat.Contains ( "ss" ) == true )
      {
        stHtml.Append ( ": " );

        stHtml.Append ( "<select "
          + "id='" + PageField.FieldId + "_SEC' "
          + "name='" + PageField.FieldId + "_SEC' "
          + "tabindex = '" + this._TabIndex + "' "
          + "value='" + stSeconds + "' "
          + "class='form-field-inline' "
          + "data-parsley-trigger=\"change\" "
          + stValidationMethod );

        if ( PageField.Mandatory == true && Status != Evado.Model.UniForm.EditAccess.Disabled )
        {
          //stHtml.Append ( " required " );
        }

        if ( Status == Evado.Model.UniForm.EditAccess.Disabled )
        {
          stHtml.Append ( "disabled='disabled' " );
        }

        stHtml.AppendLine ( ">\r\n" );

        foreach ( Evado.Model.EvOption option in minuteList )
        {
          stHtml.Append ( " <option value=\"" + option.Value + "\" " );
          if ( option.Value == stSeconds )
          {
            stHtml.Append ( " selected='selected' " );
          }
          stHtml.AppendLine ( ">" + option.Description + " </option>\r\n" );
        }
        stHtml.AppendLine ( " </select>\r\n" );

        this._TabIndex++;

      }

      if ( Status == Evado.Model.UniForm.EditAccess.Enabled )
      {
        stHtml.Append ( "<br/><span style='margin:10pt;'>" + stFormat + "</span>" );
      }

      stHtml.Append ( "<input type='hidden' "
        + "id='" + PageField.FieldId + "' "
        + "name='" + PageField.FieldId + "' "
        + "value='" + PageField.Value + "' />" );

      stHtml.AppendLine ( "</div>" );
      //
      // Insert the field footer elemements
      //
      this.createFieldFooter ( stHtml, PageField );

    }//END Field Method

    // ===================================================================================
    /// <summary>
    /// This method creates a radio button field html markup
    /// </summary>
    /// <param name="sbHtml">StringBuilder:  containing html string content</param>
    /// <param name="PageField">Field object.</param>
    /// <param name="TabIndex">Integer: table position.</param>
    /// <param name="Status">ClientFieldEditCodes enumerated status.</param>
    /// <returns>String html</returns>
    // ----------------------------------------------------------------------------------
    private void createRadioButtonField (
      StringBuilder sbHtml,
      Evado.Model.UniForm.Field PageField,
      Evado.Model.UniForm.EditAccess Status )
    {
      Global.LogClientMethod ( "createRadioButtonField method. " );
      Global.LogDebug ( "PageField.Value: " + PageField.Value );
      //
      // Initialise the methods variables and objects.
      //
      int valueColumnWidth = this._GroupValueColumWidth;
      int titleColumnWidth = 100 - valueColumnWidth;
      String stValueLegend = PageField.GetParameter ( Evado.Model.UniForm.FieldParameterList.Field_Value_Legend );
      String stCustomValidation = PageField.GetParameter ( Model.UniForm.FieldParameterList.Validation_Callback ); ;
      String stCmdOnChange = PageField.GetParameter ( Model.UniForm.FieldParameterList.Snd_Cmd_On_Change );

      if ( PageField.hasParameter ( FieldParameterList.Field_Value_Column_Width ) == true )
      {
        Model.UniForm.FieldValueWidths widthValue = PageField.getValueColumnWidth ( );
        valueColumnWidth = (int) widthValue;
        titleColumnWidth = 100 - valueColumnWidth;
      }
      Global.LogDebug ( "valueColumnWidth: " + valueColumnWidth );

      String stFieldValueStyling = "style='width:" + valueColumnWidth + "%' class='cell value cell-check-value cf' ";

      //
      // Set the normal validation parameters.
      //
      string stValidationMethod = " onclick=\"Evado.Form.onSelectionValidation( this, this.value )\" ";

      if ( stCustomValidation != String.Empty )
      {
        stValidationMethod = " onclick=\"Evado.Form.onCustomValidation('" + stCustomValidation + "', this, this.value )\" ";
      }

      //
      // Ineert the field header
      //
      createFieldHeader ( sbHtml, PageField, titleColumnWidth, false );

      //
      // Insert the field elements
      //
      //
      // Insert the field elements
      //
      sbHtml.AppendLine ( "<div " + stFieldValueStyling + " >" );

      //
      // Insert the field elements
      //
      if ( stValueLegend != String.Empty )
      {
        sbHtml.AppendLine ( "<span>" + stValueLegend + "<span>" );
      }

      sbHtml.AppendLine ( "<div>" ); // id='sp" + PageField.Id + "'

      // 
      // Iterate through the Options.
      // 
      for ( int i = 0; i < PageField.OptionList.Count; i++ )
      {
        Evado.Model.EvOption option = PageField.OptionList [ i ];
        if ( option.Value == String.Empty )
        {
          continue;
        }

        sbHtml.AppendLine ( "<div class='radio'>" );


        if ( ( Status == Evado.Model.UniForm.EditAccess.Disabled )
          && ( PageField.Value == option.Description ) )
        {
          sbHtml.AppendLine ( "<label class='bold'>" );
        }
        else
        {
          sbHtml.AppendLine ( "<label>" );
        }

        sbHtml.Append ( "<input "
         + "type='radio' "
         + "id='" + PageField.FieldId + "_" + ( i + 1 ) + "' "
         + "name='" + PageField.FieldId + "' "
         + "tabindex = '" + _TabIndex + "' "
         + "value='" + option.Value + "' "
         + "data-parsley-trigger=\"change\" " );

        if ( stCmdOnChange == "1" )
        {
          sbHtml.Append ( this.createOnChangeEvent ( ) );
        }
        else
        {
          sbHtml.Append ( "\r\n " + stValidationMethod );
        }

        if ( PageField.Mandatory == true
          && Status != Model.UniForm.EditAccess.Disabled )
        {
          //sbHtml.Append ( " required " );
        }

        //this.addMandatoryIfAttribute ( sbHtml, PageField );

        if ( PageField.Value == option.Value
          || PageField.Value == option.Description )
        {
          sbHtml.Append ( " checked='checked' " );
        }

        if ( Status == Evado.Model.UniForm.EditAccess.Disabled )
        {
          sbHtml.Append ( " disabled='disabled' " );
        }

        sbHtml.AppendLine ( "/>" );
        sbHtml.AppendLine ( "<span class='label'>" + option.Description + "</span>" );
        sbHtml.AppendLine ( "</label>" );
        sbHtml.AppendLine ( "</div>" );

      }//END end option iteration loop.


      sbHtml.AppendLine ( "<div class='radio'>" );
      sbHtml.AppendLine ( "<label>" );

      sbHtml.AppendLine ( "<input "
       + "type='radio' "
       + "id='" + PageField.FieldId + "_" + ( PageField.OptionList.Count + 1 ) + "' "
       + "name='" + PageField.FieldId + "' "
       + "tabindex = '" + _TabIndex + "' "
       + "value='' "
         + "data-parsley-trigger=\"change\" " );

      if ( stCmdOnChange == "1" )
      {
        sbHtml.Append ( this.createOnChangeEvent ( ) );
      }
      else
      {
        sbHtml.Append ( "\r\n " + stValidationMethod );
      }

      if ( PageField.Value == String.Empty )
      {
        sbHtml.Append ( " checked='checked' " );
      }

      if ( Status == Evado.Model.UniForm.EditAccess.Disabled )
      {
        sbHtml.Append ( " disabled='disabled' " );
      }

      sbHtml.AppendLine ( "/>" );

      sbHtml.AppendLine ( "<span class='label' style='font-size: 8pt;'>Not Selected</span>" );
      sbHtml.AppendLine ( "</label>" );
      sbHtml.AppendLine ( "</div>" );

      sbHtml.Append ( "<input "
       + "type='hidden' "
       + "id='" + PageField.FieldId + "' "
       + "name='" + PageField.FieldId + "' "
       + "value='" + PageField.Value.Replace ( "Null", "" ) + "' />" );

      sbHtml.AppendLine ( "</div>" );
      sbHtml.AppendLine ( "</div>" );

      this._TabIndex += 2;


      //
      // Insert the field footer elemements
      //
      this.createFieldFooter ( sbHtml, PageField );

    }//END Field Method

    // ===================================================================================
    /// <summary>
    /// This method creates a radio button field html markup
    /// </summary>
    /// <param name="sbHtml">StringBuilder:  containing html string content</param>
    /// <param name="PageField">Field object.</param>
    /// <param name="TabIndex">Integer: table position.</param>
    /// <param name="Status">ClientFieldEditCodes enumerated status.</param>
    /// <returns>String html</returns>
    // ----------------------------------------------------------------------------------
    private void createQuizRadioButtonField (
      StringBuilder sbHtml,
      Evado.Model.UniForm.Field PageField,
      Evado.Model.UniForm.EditAccess Status )
    {
      Global.LogClientMethod ( "createQuizRadioButtonField method. " );
      Global.LogDebug ( "PageField.Value: " + PageField.Value );
      try
      {
        //
        // Initialise the methods variables and objects.
        //
        int valueColumnWidth = this._GroupValueColumWidth;
        int titleColumnWidth = 100 - valueColumnWidth;
        String stFieldValueStyling = "style='width:" + valueColumnWidth + "%' class='cell value cell-radio-value cf' ";
        String stQuizValue = PageField.GetParameter ( Model.UniForm.FieldParameterList.Quiz_Value );
        String stQuizAnswer = PageField.GetParameter ( Model.UniForm.FieldParameterList.Quiz_Answer );

        //
        // Set the normal validation parameters.
        //
        string stValidationMethod = " onchange=\"Evado.Form.onQuizValidation( this, this.value )\" ";

        //
        // Ineert the field header
        //
        createFieldHeader ( sbHtml, PageField, titleColumnWidth, false );

        //
        // Insert the field elements
        //

        //
        // Insert the field elements
        //
        sbHtml.AppendLine ( "<div " + stFieldValueStyling + " >" );
        sbHtml.AppendLine ( "<div id='sp" + PageField.Id + "'>" );

        // 
        // Iterate through the Options.
        // 
        if ( PageField.OptionList.Count > 0 )
        {
          for ( int i = 0; i < PageField.OptionList.Count; i++ )
          {
            Evado.Model.EvOption option = PageField.OptionList [ i ];
            if ( option.Value == String.Empty )
            {
              continue;
            }

            sbHtml.AppendLine ( "<div class='radio'>" );


            if ( ( Status == Evado.Model.UniForm.EditAccess.Disabled )
              && ( PageField.Value == option.Description ) )
            {
              sbHtml.AppendLine ( "<label class='bold'>" );
            }
            else
            {
              sbHtml.AppendLine ( "<label>" );
            }

            sbHtml.Append ( "<input "
             + "type='radio' "
             + "id='" + PageField.FieldId + "_" + ( i + 1 ) + "' "
             + "name='" + PageField.FieldId + "' "
             + "tabindex = '" + _TabIndex + "' "
             + "data-quiz-value='" + stQuizValue + "' "
             + "data-quiz-answer='" + stQuizAnswer + "' "
             + "value='" + option.Value + "' "
             + "data-parsley-trigger=\"change\" " );

            sbHtml.AppendLine ( stValidationMethod );

            if ( PageField.Mandatory == true
              && Status != Model.UniForm.EditAccess.Disabled )
            {
              //sbHtml.Append ( " required " );
            }

            if ( PageField.Value == option.Value
              || PageField.Value == option.Description )
            {
              sbHtml.Append ( " checked='checked' " );
            }

            if ( Status == Evado.Model.UniForm.EditAccess.Disabled )
            {
              sbHtml.Append ( " disabled='disabled' " );
            }

            sbHtml.AppendLine ( "/>" );
            sbHtml.AppendLine ( "<span class='label'>" + option.Description + "</span>" );
            sbHtml.AppendLine ( "</label>" );
            sbHtml.AppendLine ( "</div>" );

            this._TabIndex += 2;

          }//END end option iteration loop.

          sbHtml.AppendLine ( "<div class='radio'>" );
          sbHtml.AppendLine ( "<label>" );

          sbHtml.AppendLine ( "<input "
           + "type='radio' "
           + "id='" + PageField.FieldId + "_" + ( PageField.OptionList.Count + 1 ) + "' "
           + "name='" + PageField.FieldId + "' "
           + "tabindex = '" + _TabIndex + "' "
           + "value='' " );

          if ( PageField.Value == String.Empty )
          {
            sbHtml.Append ( " checked='checked' " );
          }

          if ( Status == Evado.Model.UniForm.EditAccess.Disabled )
          {
            sbHtml.Append ( " disabled='disabled' " );
          }

          sbHtml.AppendLine ( "/>" );

          sbHtml.AppendLine ( "<span class='label' style='font-size: 8pt;'>Not Selected</span>" );
          sbHtml.AppendLine ( "</label>" );
          sbHtml.AppendLine ( "</div>" );
        }

        sbHtml.AppendLine ( "</div>" );

        sbHtml.AppendLine ( "<input "
         + "type='text' "
         + "id='" + PageField.FieldId + "_ANSWERS' "
         + "name='" + PageField.FieldId + "_ANSWERS' "
         + "value='' />" );
        sbHtml.AppendLine ( "</div>" );

        this._TabIndex += 2;

        //
        // Insert the field footer elemements
        //
        this.createFieldFooter ( sbHtml, PageField );
      }
      catch ( Exception Ex )
      {
        Global.LogClient ( Evado.Model.EvStatics.getException ( Ex ) );
      }
    }//END Field Method

    // ===================================================================================
    /// <summary>
    /// This method creates a radio1 button field html markup
    /// </summary>
    /// <param name="sbHtml">StringBuilder:  containing html string content</param>
    /// <param name="PageField">Field object.</param>
    /// <param name="TabIndex">Integer: table position.</param>
    /// <param name="Status">ClientFieldEditCodes enumerated status.</param>
    /// <returns>String html</returns>
    // ----------------------------------------------------------------------------------
    private void createHorizontalRadioButtonField (
      StringBuilder sbHtml,
      Evado.Model.UniForm.Field PageField,
      Evado.Model.UniForm.EditAccess Status )
    {
      Global.LogClientMethod ( "createHorizontalRadioButtonField method. " );
      //
      // Initialise the methods variables and objects.
      //
      int valueColumnWidth = this._GroupValueColumWidth;
      int titleColumnWidth = 100 - valueColumnWidth;
      String stFieldValueStyling = "style='width:98%;' class='cell value cell-radio-value' ";
      String stCustomValidation = PageField.GetParameter ( Evado.Model.UniForm.FieldParameterList.Validation_Callback );
      String stCmdOnChange = PageField.GetParameter ( Model.UniForm.FieldParameterList.Snd_Cmd_On_Change );

      //
      // Set the normal validation parameters.
      //
      string stValidationMethod = " onchange=\"Evado.Form.onSelectionValidation( this, this.value )\" ";

      if ( stCustomValidation != String.Empty )
      {
        stValidationMethod = " onchange=\"Evado.Form.onCustomValidation('" + stCustomValidation + "', this, this.value )\" ";
      }

      //
      // Ineert the field header
      //
      createFieldHeader ( sbHtml, PageField, titleColumnWidth, true );

      //
      // Insert the field elements
      //
      sbHtml.AppendLine ( "<div " + stFieldValueStyling + " > " );
      sbHtml.AppendLine ( "<div> " );
      // cf

      // 
      // Iterate through the Options.
      // 
      for ( int i = 0; i < PageField.OptionList.Count; i++ )
      {
        Evado.Model.EvOption option = PageField.OptionList [ i ];
        if ( option.Value == String.Empty
          || option.Description == String.Empty )
        {
          continue;
        }

        sbHtml.AppendLine ( "<div class='radio-inline'>" );
        sbHtml.AppendLine ( "<label style='text-align:center'>" );

        sbHtml.AppendLine ( "<input "
         + "type='radio' "
         + "id='" + PageField.FieldId + "_" + ( i + 1 ) + "' "
         + "name='" + PageField.FieldId + "' "
         + "value=\"" + option.Value + "\" "
         + "tabindex = '" + _TabIndex + "' "
         + "data-parsley-trigger=\"change\" " );

        if ( stCmdOnChange == "1" )
        {
          sbHtml.Append ( this.createOnChangeEvent ( ) );
        }
        else
        {
          sbHtml.Append ( "\r\n " + stValidationMethod );
        }

        if ( PageField.Mandatory == true && Status != Evado.Model.UniForm.EditAccess.Disabled )
        {
          sbHtml.Append ( " required " );
        }

        //this.addMandatoryIfAttribute ( sbHtml, PageField );

        if ( PageField.Value == option.Value
          || PageField.Value == option.Description )
        {
          sbHtml.Append ( " checked='checked' " );
        }

        if ( Status == Evado.Model.UniForm.EditAccess.Disabled )
        {
          sbHtml.Append ( " disabled='disabled' " );
        }

        sbHtml.AppendLine ( "/>" );

        sbHtml.AppendLine ( "<span class='label'>" + option.Description + "</span>" );
        sbHtml.AppendLine ( "</label>" );
        sbHtml.AppendLine ( "</div>" );

      }//END end option iteration loop.

      #region not selected
      sbHtml.AppendLine ( "<div class='radio-inline'>" );
      sbHtml.AppendLine ( "<label style='text-align:center'>" );

      sbHtml.AppendLine ( "<input "
       + "type='radio' "
       + "id='" + PageField.FieldId + "_" + ( PageField.OptionList.Count + 1 ) + "' "
       + "name='" + PageField.FieldId + "' "
       + "tabindex = '" + _TabIndex + "' "
       + "value=\"\" "
       + "data-parsley-trigger=\"change\" " );

      sbHtml.Append ( stValidationMethod );

      if ( PageField.Mandatory == true && Status != Evado.Model.UniForm.EditAccess.Disabled )
      {
        sbHtml.Append ( " required " );
      }

      //this.addMandatoryIfAttribute ( sbHtml, PageField );

      if ( PageField.Value == "" )
      {
        sbHtml.Append ( " checked='checked' " );
      }

      if ( Status == Evado.Model.UniForm.EditAccess.Disabled )
      {
        sbHtml.Append ( " disabled='disabled' " );
      }

      sbHtml.AppendLine ( "/>" );

      sbHtml.AppendLine ( "<span class='label'>" + EuLabels.Radio_Button_Not_Selected + "</span>" );
      sbHtml.AppendLine ( "</label>" );
      sbHtml.AppendLine ( "</div>" );

      //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
      #endregion

      sbHtml.AppendLine ( "</div>" );
      sbHtml.AppendLine ( "</div>" );

      this._TabIndex += 2;

      //
      // Insert the field footer elemements
      //
      this.createFieldFooter ( sbHtml, PageField );

    }//END Field Method

    // ===================================================================================
    /// <summary>
    /// This method creates a boolean field html markup
    /// </summary>
    /// <param name="PageField">Field object.</param>
    /// <param name="TabIndex">Integer: table position.</param>
    /// <param name="Status">ClientFieldEditCodes enumerated status.</param>
    /// <returns>String html</returns>
    // ----------------------------------------------------------------------------------
    private void createYesNoField (
      StringBuilder sbHtml,
      Evado.Model.UniForm.Field PageField,
      Evado.Model.UniForm.EditAccess Status )
    {
      Global.LogClientMethod ( "createBooleanField method. " );
      //
      // Initialise the methods variables and objects.
      //
      int valueColumnWidth = this._GroupValueColumWidth;
      int titleColumnWidth = 100 - valueColumnWidth;
      String stValueLegend = PageField.GetParameter ( Evado.Model.UniForm.FieldParameterList.Field_Value_Legend );
      String stCmdOnChange = PageField.GetParameter ( Model.UniForm.FieldParameterList.Snd_Cmd_On_Change );
      //
      // Set the normal validation parameters.
      //
      string stValidationMethod = " onclick=\"Evado.Form.onSelectionValidation( this, this.value )\" ";

      if ( PageField.hasParameter ( FieldParameterList.Field_Value_Column_Width ) == true )
      {
        Model.UniForm.FieldValueWidths widthValue = PageField.getValueColumnWidth ( );
        valueColumnWidth = (int) widthValue;
        titleColumnWidth = 100 - valueColumnWidth;
      }
      Global.LogDebug ( "valueColumnWidth: " + valueColumnWidth );

      String stFieldValueStyling = "style='width:" + valueColumnWidth + "%' class='cell value cell-bool-value cf' ";


      //
      // Ineert the field header
      //
      createFieldHeader ( sbHtml, PageField, titleColumnWidth, false );

      //
      // Insert the field elements
      //
      sbHtml.AppendLine ( "<div " + stFieldValueStyling + " >" );

      //
      // Insert the field elements
      //
      if ( stValueLegend != String.Empty )
      {
        sbHtml.AppendLine ( "<span>" + stValueLegend + "<span>" );
      }

      sbHtml.AppendLine ( "<div>" );

      //
      // yes input control
      //
      sbHtml.AppendLine ( "<div class='radio-inline'>" );
      sbHtml.AppendLine ( "<label>" );
      sbHtml.Append ( "<input "
       + "type='radio' "
       + "id='" + PageField.FieldId + "_Y' "
       + "name='" + PageField.FieldId + "' "
       + "tabindex = '" + _TabIndex + "' "
       + "value=\"Yes\" "
       + "data-parsley-trigger=\"change\" " );

      /*
      if ( PageField.Mandatory == true && Status != Model.UniForm.EditCodes.Edit_Disabled )
      {
        sbHtml.Append ( " required " );
      }
      */
      //this.addMandatoryIfAttribute ( sbHtml, PageField );

      if ( PageField.Value == "Yes" )
      {
        sbHtml.Append ( " checked='checked' " );
      }

      if ( Status == Evado.Model.UniForm.EditAccess.Disabled )
      {
        sbHtml.Append ( " disabled='disabled' " );
      }

      if ( stCmdOnChange == "1" )
      {
        sbHtml.Append ( this.createOnChangeEvent ( ) );
      }
      else
      {
        sbHtml.Append ( "\r\n " + stValidationMethod );
      }

      sbHtml.AppendLine ( "/>" );
      sbHtml.AppendLine ( "<span class='label'>Yes</span>" );
      sbHtml.AppendLine ( "</label>" );
      sbHtml.AppendLine ( "</div>" );

      //
      // No input control
      //
      sbHtml.Append ( "<div class='radio-inline'>" );
      sbHtml.AppendLine ( "<label>" );
      sbHtml.AppendLine ( "<input "
       + "type='radio' "
       + "id='" + PageField.FieldId + "_N' "
       + "name='" + PageField.FieldId + "' "
       + "value=\"No\" "
       + "tabindex = '" + _TabIndex + "' "
       + "data-parsley-trigger=\"change\" " );

      /*
      if ( PageField.Mandatory == true && Status != Model.UniForm.EditCodes.Edit_Disabled )
      {
        sbHtml.Append ( " required " );
      }
      */
      //this.addMandatoryIfAttribute ( sbHtml, PageField );


      if ( PageField.Value == "No" )
      {
        sbHtml.Append ( " checked='checked' " );
      }


      if ( Status == Evado.Model.UniForm.EditAccess.Disabled )
      {
        sbHtml.Append ( " disabled='disabled' " );
      }

      if ( stCmdOnChange == "1" )
      {
        sbHtml.Append ( this.createOnChangeEvent ( ) );
      }
      else
      {
        sbHtml.Append ( "\r\n " + stValidationMethod );
      }

      sbHtml.AppendLine ( "/>" );
      sbHtml.AppendLine ( "<span class='label'>No</span>" );
      sbHtml.AppendLine ( "</label>" );
      sbHtml.AppendLine ( "</div>" );

      sbHtml.AppendLine ( "</div>" );
      sbHtml.AppendLine ( "</div>" );

      this._TabIndex += 2;

      //
      // Insert the field footer elemements
      //
      this.createFieldFooter ( sbHtml, PageField );

    }//END Field Method

    // ===================================================================================
    // <summary>
    // This method creates a checkbox list field html markup
    // </summary>
    // <param name="PageField">Field object.</param>
    // <param name="TabIndex">Integer: table position.</param>
    // <param name="Status">ClientFieldEditCodes enumerated status.</param>
    // <returns>String html</returns>
    // ----------------------------------------------------------------------------------
    private void createCheckboxField (
      StringBuilder sbHtml,
      Evado.Model.UniForm.Field PageField,
      Evado.Model.UniForm.EditAccess Status )
    {
      Global.LogClientMethod ( "createCheckboxField method. " );
      //
      // Initialise the methods variables and objects.
      //
      int valueColumnWidth = this._GroupValueColumWidth;
      int titleColumnWidth = 100 - valueColumnWidth;
      String stValueLegend = PageField.GetParameter ( Evado.Model.UniForm.FieldParameterList.Field_Value_Legend );
      String stCmdOnChange = PageField.GetParameter ( Model.UniForm.FieldParameterList.Snd_Cmd_On_Change );
      String stCustomValidation = PageField.GetParameter ( Model.UniForm.FieldParameterList.Validation_Callback );

      if ( PageField.hasParameter ( FieldParameterList.Field_Value_Column_Width ) == true )
      {
        Model.UniForm.FieldValueWidths widthValue = PageField.getValueColumnWidth ( );
        valueColumnWidth = (int) widthValue;
        titleColumnWidth = 100 - valueColumnWidth;
      }
      Global.LogDebug ( "valueColumnWidth: " + valueColumnWidth );
      Global.LogDebug ( "stValueLegend: " + stValueLegend );

      String stFieldValueStyling = "style='width:" + valueColumnWidth + "%' class='cell value cell-check-value cf' ";

      //
      // Set the normal validation parameters.
      //
      string stValidationMethod = " onclick=\"Evado.Form.onSelectionValidation( this, this.value )\" ";

      if ( stCustomValidation != String.Empty )
      {
        stValidationMethod = " onclick=\"Evado.Form.onCustomValidation('" + stCustomValidation + "', this, this.value )\" ";
      }

      //
      // Ineert the field header
      //
      this.createFieldHeader ( sbHtml, PageField, titleColumnWidth, false );

      //
      // Insert the field elements
      //
      sbHtml.AppendLine ( "<div " + stFieldValueStyling + " >" );

      //
      // Insert the field elements
      //
      if ( stValueLegend != String.Empty )
      {
        sbHtml.AppendLine ( "<span>" + stValueLegend + "</span>" );
      }

      sbHtml.AppendLine ( "<div>" );

      //
      // Generate the html code 
      //
      for ( int i = 0; i < PageField.OptionList.Count; i++ )
      {
        Evado.Model.EvOption option = PageField.OptionList [ i ];

        int count = i + 1;

        sbHtml.Append ( "<div class='checkbox'>\r\n"
         + "<label>\r\n"
         + "<input "
         + "type='checkbox' "
         + "id='" + PageField.FieldId + "_" + count + "' "
         + "name='" + PageField.FieldId + "' "
         + "tabindex = '" + _TabIndex + "' "
         + "value='" + option.Value + "' " );


        if ( PageField.Value.Contains ( option.Value ) == true
          || PageField.Value.Contains ( option.Description ) == true )
        {
          sbHtml.Append ( " checked='checked' " );
        }

        if ( Status == Evado.Model.UniForm.EditAccess.Disabled )
        {
          sbHtml.Append ( " disabled='disabled' " );
        }

        if ( stCmdOnChange == "1" )
        {
          sbHtml.Append ( this.createOnChangeEvent ( ) );
        }
        else
        {
          sbHtml.Append ( "\r\n " + stValidationMethod );
        }

        sbHtml.AppendLine ( "/>" );
        sbHtml.AppendLine ( "<span class='label' style='width:80%'>" + option.Description + "</span>" );
        sbHtml.AppendLine ( "</label>" );
        sbHtml.AppendLine ( "</div>" );

      }//End option iteration loop.

      //
      // the cbeckbox footer.
      //
      sbHtml.Append ( "\t\r\n</div></div>\r\n" );

      this._TabIndex += 2;

      //
      // Insert the field footer
      //
      this.createFieldFooter ( sbHtml, PageField );

    }//END Field Method

    // ===================================================================================
    /// <summary>
    /// This method creates a selection list field html markup
    /// </summary>
    /// <param name="PageField">Field object.</param>
    /// <param name="TabIndex">Integer: table position.</param>
    /// <param name="Status">ClientFieldEditCodes enumerated status.</param>
    /// <returns>String html</returns>
    // ----------------------------------------------------------------------------------
    private void createSelectionListField (
      StringBuilder stHtml,
      Evado.Model.UniForm.Field PageField,
      Evado.Model.UniForm.EditAccess Status )
    {
      Global.LogClientMethod ( "createSelectionListField method. " );
      Global.LogDebug ( "PageField: Title: " + PageField.Title );
      Global.LogDebug ( "PageField: Value: " + PageField.Value );
      //
      // Initialise the methods variables and objects.
      //
      int valueColumnWidth = this._GroupValueColumWidth;
      int titleColumnWidth = 100 - valueColumnWidth;

      if ( PageField.hasParameter ( FieldParameterList.Field_Value_Column_Width ) == true )
      {
        Model.UniForm.FieldValueWidths widthValue = PageField.getValueColumnWidth ( );
        valueColumnWidth = (int) widthValue;
        titleColumnWidth = 100 - valueColumnWidth;
      }
      String stFieldValueStyling = "style='width:" + valueColumnWidth + "%' class='cell value cell-select-value cf' ";
      String stCmdOnChange = PageField.GetParameter ( Model.UniForm.FieldParameterList.Snd_Cmd_On_Change );
      String stCustomValidation = PageField.GetParameter ( Model.UniForm.FieldParameterList.Validation_Callback );

      Global.LogDebug ( "stCmdOnChange: " + stCmdOnChange );
      Global.LogDebug ( "stCustomValidation: " + stCustomValidation );

      //
      // Set the normal validation parameters.
      //
      string stValidationMethod = " onclick=\"Evado.Form.onSelectionValidation( this, this.value )\" ";

      if ( stCustomValidation != String.Empty )
      {
        stValidationMethod = " onclick=\"Evado.Form.onCustomValidation('" + stCustomValidation + "', this, this.value )\" ";
      }

      //
      // Ineert the field header
      //
      this.createFieldHeader ( stHtml, PageField, titleColumnWidth, false );

      //
      // Insert the field elements
      //
      stHtml.Append ( "<div " + stFieldValueStyling + " > "
        + "<select "
        + "id='" + PageField.FieldId + "' "
        + "name='" + PageField.FieldId + "' "
        + "tabindex = '" + _TabIndex + "' "
        + "value='" + PageField.Value
        + "' class='form-control' data-parsley-trigger=\"change\" " );

      if ( PageField.Mandatory == true && Status != Model.UniForm.EditAccess.Disabled )
      {
        //stHtml.Append ( " required " );
      }

      //this.addMandatoryIfAttribute ( stHtml, PageField );

      if ( stCmdOnChange == "1" )
      {
        stHtml.Append ( this.createOnChangeEvent ( ) );
      }
      else
      {
        stHtml.Append ( "\r\n " + stValidationMethod );
      }

      if ( Status == Evado.Model.UniForm.EditAccess.Disabled )
      {
        stHtml.Append ( "disabled='disabled' " );
      }

      stHtml.Append ( ">\r\n" );

      // 
      // Iterate through the stOptions.
      //
      for ( int i = 0; i < PageField.OptionList.Count; i++ )
      {
        Evado.Model.EvOption option = PageField.OptionList [ i ];
        /*
         * Generate the option html
         */
        stHtml.Append ( " <option value=\"" + option.Value + "\" " );
        if ( option.Value == PageField.Value
          || option.Description == PageField.Value )
        {
          stHtml.Append ( " selected='selected' " );
        }
        stHtml.Append ( ">" + option.Description + "</option>\r\n" );
      }
      stHtml.Append ( " </select>\r\n" );

      stHtml.Append ( "</div>\r\n" );

      this._TabIndex += 2;

      //
      // Insert the field footer elemements
      //
      this.createFieldFooter ( stHtml, PageField );

    }//END Field Method

    // ==================================================================================
    /// <summary>
    /// This mehod generates the HTMl for a page group.
    /// </summary>
    // ---------------------------------------------------------------------------------
    private String createOnChangeEvent ( )
    {
      Global.LogClientMethod ( "createOnChangeEvent method. " );
      //
      // Exit if there are not commands in the group.
      //
      if ( this._CurrentGroup.CommandList == null )
      {
        return String.Empty;
      }

      //
      // Return the onChange event attribute for the first command.
      //
      if ( this._CurrentGroup.CommandList.Count > 0 )
      {
        Evado.Model.UniForm.Command command = this._CurrentGroup.CommandList [ 0 ];

        return "onchange=\"javascript:onPostBack('" + command.Id + "')\"";
      }

      return String.Empty;
    }

    // ===================================================================================
    /// <summary>
    /// This method creates a table field html markup
    /// </summary>
    /// <param name="PageField">Field object.</param>
    /// <param name="TabIndex">Integer: table position.</param>
    /// <param name="Status">ClientFieldEditCodes enumerated status.</param>
    /// <returns>String html</returns>
    // ----------------------------------------------------------------------------------
    private void createTableField (
      StringBuilder stHtml,
      Evado.Model.UniForm.Field PageField,

      Evado.Model.UniForm.EditAccess Status )
    {
      Global.LogClientMethod ( "createTableField method. " );
      Global.LogDebug ( "PageField.Layout: " + PageField.Layout );
      Global.LogDebug ( "Table Columns: " + PageField.Table.ColumnCount );
      Global.LogDebug ( "Rows: " + PageField.Table.Rows.Count );

      //
      // Initialise the methods variables and objects.
      //
      bool fullWidth = false;
      int valueColumnWidth = this._GroupValueColumWidth;
      int titleColumnWidth = 100 - valueColumnWidth;
      if ( PageField.Layout == FieldLayoutCodes.Column_Layout )
      {
        valueColumnWidth = 100;
        titleColumnWidth = 100;
        fullWidth = true;
      }
      String stFieldValueStyling = "style='width:" + valueColumnWidth + "%' class='cell value cell-table-value cf' ";

      //
      // Ineert the field header
      //
      this.createFieldHeader ( stHtml, PageField, titleColumnWidth, fullWidth );

      //
      // Insert the field elements
      //
      stHtml.Append ( "<div " + stFieldValueStyling + " > " );

      /********************************************************************************** 
       * The table is generated by inserting a table structure into the html
       * the table header is the first row of the table and displays the table content.
       * 
       * Then each row of the table will have a new table row inserted into the html 
       * output text.
       * 
       **********************************************************************************/

      stHtml.Append ( "<table class='table table-striped'>" );

      this.getTableFieldHeader (
        stHtml,
        PageField );

      // 
      // Iterate through the rows in the table.
      // 
      for ( int row = 0; row < PageField.Table.Rows.Count; row++ )
      {
        this.createTableFieldDataRow (
        stHtml,
        PageField,
        row,
        Status );
      }

      stHtml.Append ( "</table>\r\n</div>\r\n" );

      //
      // Insert the field footer elemements
      //
      this.createFieldFooter ( stHtml, PageField );

    }//END createTableField Method

    // =================================================================================
    /// <summary>
    /// Description:
    ///   This method generates a table form field header as html markup.
    /// 
    /// </summary>
    /// <param name="PageField">EvForm object containing the form to be generated.</param>
    /// <returns>String containing HTML markup for the form.</returns>
    // --------------------------------------------------------------------------------
    private void getTableFieldHeader (
      StringBuilder stHtml,
      Evado.Model.UniForm.Field PageField )
    {
      Global.LogClientMethod ( "getFormFieldTableHeader method." );
      // 
      // Initialise local variables.
      // 
      string stWidth = String.Empty;
      int iWidth = 0;

      stHtml.Append ( "<tr>" );
      //
      // Sum the data widths to compute the column widths.
      //
      foreach ( Evado.Model.UniForm.TableColHeader header in PageField.Table.Header )
      {
        if ( header.Text != String.Empty )
        {
          int i = 0;
          if ( int.TryParse ( header.Width, out i ) == true )
          {
            iWidth += i;
          }
        }
      }//END header iteration loop to sum width.

      // 
      // Iterate through the field table header items
      // 
      foreach ( Evado.Model.UniForm.TableColHeader header in PageField.Table.Header )
      {
        // 
        // Skip rows that have not header text
        //
        if ( header.Text == String.Empty )
        {
          continue;
        }

        int i = 0;
        if ( int.TryParse ( header.Width, out i ) == true )
        {
          float celWidth = 100 * i / iWidth;
          stWidth = "Width:" + celWidth + "%";
        }
        else
        {
          stWidth = "Width:" + header.Width + "%";
        }

        if ( PageField.Table.ColumnCount == 1 )
        {
          stWidth = "Width:100%";
        }
        if ( PageField.Table.ColumnCount == 2 )
        {
          stWidth = "Width:50%";
        }

        stHtml.Append ( "<td style='" + stWidth + ";text-align:center;' >" );

        stHtml.Append ( "<strong>" + header.Text + "</strong> " );

        if ( header.TypeId == Model.EvDataTypes.Date )
        {
          stHtml.Append ( "<br/><span class='Smaller_Italics'>(DD MMM YYYY)</span>" );
        }

        if ( header.TypeId == Model.EvDataTypes.Numeric )
        {
          if ( header.OptionsOrUnit == String.Empty )
          {
            stHtml.Append ( "<br/><span class='Smaller_Italics'>(23.5678)</span>" );
          }
          else
          {
            stHtml.Append ( "<br/><span class='Smaller_Italics'>(23.5678 " + header.OptionsOrUnit + ")</span>" );
          }
        }

        stHtml.Append ( "</td>" );

      }//END table header iteration loop.

      stHtml.Append ( "</tr>" );

    }//END getTableFieldHeader method

    // =================================================================================
    /// <summary>
    /// Description:
    ///   This method generates the table form field's row data as html markup.
    /// 
    /// </summary>
    /// <param name="stHtml">StringBuilder object.</param>
    /// <param name="PageField">Field object.</param>
    /// <param name="TabIndex">Integer: table position.</param>
    /// <param name="Status">ClientFieldEditCodes enumerated status.</param>
    // --------------------------------------------------------------------------------
    private void createTableFieldDataRow (
      StringBuilder stHtml,
      Evado.Model.UniForm.Field PageField,
      int Row,

      Evado.Model.UniForm.EditAccess Status )
    {
      Global.LogClientMethod ( "getTableFieldDataRow method. " );
      Global.LogDebug ( "Row: " + Row );
      // 
      // Initialise local variables.
      // 

      // 
      // Open the fieldtable data cells
      // 
      stHtml.Append ( "<tr>" );

      for ( int column = 0; column < PageField.Table.ColumnCount; column++ )
      {
        Evado.Model.UniForm.TableColHeader header = PageField.Table.Header [ column ];
        try
        {
          string stDataId = PageField.FieldId + "_" + ( Row + 1 ) + "_" + ( column + 1 );
          string stValue = PageField.Table.Rows [ Row ].Column [ column ].Trim ( );

          // 
          // Skip rows that have not header text
          // 
          if ( header.Text == String.Empty )
          {
            continue;
          }

          switch ( header.TypeId )
          {
            case Model.EvDataTypes.Read_Only_Text:
              {
                stHtml.Append ( "<td align='middle'>" );
                stHtml.Append ( PageField.Table.Rows [ Row ].Column [ column ] );

                break;
              }//END Text State.
            case Model.EvDataTypes.Text:
              {
                stHtml.Append ( "<td align='middle'>" );
                stHtml.AppendLine ( "<input "
                    + "id='" + stDataId + "' "
                    + "name='" + stDataId + "' "
                    + "maxlength='" + header.Width + "' "
                    + "size='" + header.Width + "' "
                    + "tabindex = '" + _TabIndex + "' "
                    + "type='text'"
                    + "value='" + stValue + "' "
                    + "onchange=\"Evado.Form.onTextValidation( this"
                    + ", '" + stDataId + "'"
                    + ", '" + stValue + "'"
                    + ", '" + Evado.Model.UniForm.TableColHeader.ItemTypeText + "'"
                    + " )\" class='form-control' " );

                if ( Status == Evado.Model.UniForm.EditAccess.Disabled )
                {
                  stHtml.Append ( " readonly='readonly' " );
                }

                stHtml.Append ( "/>" );

                this._TabIndex++;

                break;
              }//END Text State.

            case Model.EvDataTypes.Numeric:
              {
                stHtml.Append ( "<td align='middle'>" );
                //
                // Set the field value.
                //
                try
                {
                  if ( stValue != String.Empty )
                  {
                    stValue = Evado.Model.EvStatics.decodeFieldNumeric ( stValue );
                  }
                }
                catch { }

                stHtml.AppendLine ( "<input "
                    + "id='" + stDataId + "' "
                    + "name='" + stDataId + "' "
                    + "tabindex = '" + _TabIndex + "' "
                    + "maxlength='10' "
                    + "size='10' "
                    + "type='text' "
                    + "value='" + stValue + "' "
                    + "onchange=\"Evado.Form.onRangeValidation( this, this.value )\" "
                    + " class='form-control' " );

                if ( Status == Evado.Model.UniForm.EditAccess.Disabled )
                {
                  stHtml.Append ( " readonly='readonly' " );
                }

                stHtml.Append ( "/>" );
                if ( header.OptionsOrUnit != String.Empty )
                {
                  stHtml.AppendLine ( " " + header.OptionsOrUnit );
                }

                this._TabIndex++;

                break;
              }//END Numeric case.ase FieldTableColumnHeader.ItemTypeText:

            case Model.EvDataTypes.Date:
              {
                stHtml.Append ( "<td align='middle'>" );
                stHtml.AppendLine ( "<input "
                    + "id='" + stDataId + "' "
                    + "name='" + stDataId + "' "
                    + "tabindex = '" + _TabIndex + "' "
                    + "maxlength='12' "
                    + "size='12' "
                    + "type='text' "
                    + "value='" + stValue + "' "
                    + "onchange=\"Evado.Form.onDateValidation( this, this.value  )\" "
                    + "  class='form-control' data-behaviour='datepicker' " );

                if ( Status == Evado.Model.UniForm.EditAccess.Disabled )
                {
                  stHtml.Append ( " readonly='readonly' " );
                }

                stHtml.Append ( "/>" );

                this._TabIndex++;

                break;
              }//END Date case.

            case Model.EvDataTypes.Yes_No:
              {
                stHtml.Append ( "<td align='left'>" );
                if ( stValue.ToLower ( ) == "true" || stValue == "1" || stValue == "Yes" )
                {
                  stValue = "yes";
                }
                else
                {
                  stValue = "no";
                }

                stHtml.AppendLine ( "<div class='radio'>" );
                stHtml.AppendLine ( "<label>" );
                stHtml.Append ( "<input type='radio' "
                   + "id='" + stDataId + "_1' "
                   + "name='" + stDataId + "' "
                   + "tabindex = '" + _TabIndex + "' "
                   + "value='Yes' "
                   + "onclick=\"Evado.Form.onSelectionValidation( this, this.value  )\" " );

                if ( stValue.ToLower ( ) == "yes" )
                {
                  stHtml.Append ( " checked='checked' " );
                }

                if ( Status == Evado.Model.UniForm.EditAccess.Disabled )
                {
                  stHtml.Append ( " disabled='disabled' " );
                }

                stHtml.AppendLine ( "/>" );

                //
                // Bold the selected item when in display mode as the button may not
                // be obvious in some browsers.
                //
                if ( ( Status == Evado.Model.UniForm.EditAccess.Disabled )
                  && ( stValue.ToLower ( ) == "yes" ) )
                {
                  stHtml.Append ( "<strong>Yes</strong>\r\n" );
                }
                else
                {
                  stHtml.Append ( "Yes\r\n" );
                }

                stHtml.AppendLine ( "</label></div>\r\n" );

                stHtml.Append ( "<div class='radio'><label>\r\n"
                   + "<input type='radio' "
                   + "id='" + stDataId + "_2' "
                   + "name='" + stDataId + "' "
                   + "tabindex = '" + _TabIndex + "' "
                   + "value='No' "
                   + "onclick=\"onSelectionValidation ( this, this.value  )\" " );

                if ( stValue.ToLower ( ) == "no" )
                {
                  stHtml.Append ( "checked=\"checked\" " );
                }

                if ( Status == Evado.Model.UniForm.EditAccess.Disabled )
                {
                  stHtml.Append ( "disabled='disabled' " );
                }

                stHtml.AppendLine ( "/>" );

                this._TabIndex++;

                //
                // Bold the selected item when in display mode as the button may not
                // be obvious in some browsers.
                //
                if ( ( Status == Evado.Model.UniForm.EditAccess.Disabled )
                  && ( stValue.ToLower ( ) == "no" ) )
                {
                  stHtml.Append ( "<strong>No</strong>\r\n" );
                }
                else
                {
                  stHtml.Append ( "No\r\n" );
                }

                stHtml.AppendLine ( "</label>" );

                stHtml.AppendLine ( "</div>" );

                this._TabIndex++;

                break;
              }//END Yes No  Case.

            case Model.EvDataTypes.Radio_Button_List:
              {
                stHtml.Append ( "<td align='left'>" );
                List<Evado.Model.EvOption> optionList = PageField.Table.Header [ column ].OptionList;

                // 
                // Iterate through the stOptions.
                // 
                for ( int i = 0; i < optionList.Count; i++ )
                {
                  //
                  // Create a button if the option exist.
                  //
                  if ( optionList [ i ].Description != String.Empty )
                  {
                    stHtml.Append ( "<div class='radio'><label>\r\n"
                       + "<input "
                       + "type='radio' "
                       + "id='" + stDataId + "_" + ( i + 1 ) + "' "
                       + "name='" + stDataId + "' "
                       + "tabindex = '" + _TabIndex + "' "
                       + "value='" + optionList [ i ].Value + "' "
                       + "onclick=\"onSelectionValidation( this, this.value  )\" " );

                    if ( stValue == optionList [ i ].Value )
                    {
                      stHtml.Append ( " checked='checked' " );
                    }

                    if ( Status == Evado.Model.UniForm.EditAccess.Disabled )
                    {
                      stHtml.Append ( " disabled='disabled' " );
                    }

                    stHtml.Append ( "/>\r\n" );

                    //
                    // Bold the selected item when in display mode as the button may not
                    // be obvious in some browsers.
                    //
                    if ( ( Status == Evado.Model.UniForm.EditAccess.Disabled )
                      && ( stValue == optionList [ i ].Value ) )
                    {
                      stHtml.Append ( "<strong>" + optionList [ i ].Description + "<strong>\r\n" );
                    }
                    else
                    {
                      stHtml.Append ( optionList [ i ].Description + "\r\n" );
                    }
                    stHtml.AppendLine ( "</label></div>" );

                    this._TabIndex++;

                  }//END option exists.

                }//End option iteration loop.

                stHtml.Append ( "<div class='radio'><label>\r\n"
                   + "<input "
                   + "type='radio' "
                   + "id='" + stDataId + "_" + ( optionList.Count + 1 ) + "' "
                   + "name='" + stDataId + "' "
                   + "tabindex = '" + _TabIndex + "' "
                   + "value='' " );

                if ( PageField.Table.Rows [ Row ].Column [ column ] == String.Empty )
                {
                  stHtml.Append ( "checked='checked' " );
                }

                if ( Status == Evado.Model.UniForm.EditAccess.Disabled )
                {
                  stHtml.Append ( "disabled='disabled' " );
                }

                stHtml.AppendLine ( "/>\r\n"
                    + "Not Selected\r\n"
                    + "</label></div>" );

                this._TabIndex++;

                break;

              }//END Radio Button  Case.

            case Model.EvDataTypes.Selection_List:
              {
                stHtml.Append ( "<td align='middle'>" );
                List<Evado.Model.EvOption> optionList = PageField.Table.Header [ column ].OptionList;

                /*
                 * Create the selectionlist HTML
                 */
                stHtml.Append ( "<select "
                    + "id='" + stDataId + "' "
                    + "name='" + stDataId + "' "
                    + "tabindex = '" + _TabIndex + "' "
                    + "value='" + stValue + "' "
                    + " onchange=\"Evado.Form.onSelectionValidation( this, this.value  )\" " );

                if ( Status == Evado.Model.UniForm.EditAccess.Disabled )
                {
                  stHtml.Append ( "disabled='disabled' " );
                }

                stHtml.Append ( ">\r\n" );

                if ( stValue == String.Empty )
                {
                  stHtml.Append ( "<option value='' selected='selected' ></option>" );
                }
                else
                {
                  stHtml.Append ( "<option value='' ></option>" );
                }

                // 
                // Iterate through the stOptions.
                // 
                for ( int i = 0; i < optionList.Count; i++ )
                {

                  //
                  // Add the option if it exists.
                  //
                  if ( optionList [ i ].Description != String.Empty )
                  {
                    //
                    // Generate the option html
                    //
                    stHtml.Append ( " <option value=\"" + optionList [ i ].Value + "\" " );

                    if ( optionList [ i ].Value == stValue )
                    {
                      stHtml.Append ( " selected='selected' " );
                    }
                    stHtml.Append ( ">" + optionList [ i ].Description + "</option>" );

                  }//END option exists.

                }//End option iteration loop.
                stHtml.Append ( " </select>" );

                this._TabIndex++;

                break;
              }//END Selection List  Case.

          }//END Switch statement

          stHtml.Append ( "</td>" );
        }
        catch ( Exception Ex )
        {
          Global.LogClient ( "Row: " + Row + ", Column: " + column + ", " + Evado.Model.EvStatics.getExceptionAsHtml ( Ex ) );
          break;
        }

      }//END column iteration loop,

      stHtml.Append ( "</tr>" );


    }//END getTableFieldDataRow method

    // ===================================================================================
    /// <summary>
    /// This method creates a test field html markup
    /// </summary>
    /// <param name="PageField">Field object.</pa
    /// <param name="stHtml">StringBuilder object.</param>
    /// <param name="PageField">Field object.</param>
    /// <param name="TabIndex">Integer: table position.</param>
    /// <param name="Status">ClientFieldEditCodes enumerated status.</param>
    // ----------------------------------------------------------------------------------
    private void createBinaryField (
      StringBuilder stHtml,
      Evado.Model.UniForm.Field PageField,
      Evado.Model.UniForm.EditAccess Status )
    {
      Global.LogClientMethod ( "createBinaryField method." );
      Global.LogDebug ( "RelativeBinaryDownloadURL: " + Global.RelativeBinaryDownloadURL );
      Global.LogDebug ( "PageField.FieldId: " + PageField.FieldId );
      Global.LogDebug ( "PageField.Value: " + PageField.Value );
      //
      // Initialise the methods variables and objects.
      //
      int valueColumnWidth = this._GroupValueColumWidth;
      int titleColumnWidth = 100 - valueColumnWidth;
      string stBinaryUrl = Global.RelativeBinaryDownloadURL + PageField.Value;
      String stSize = PageField.GetParameter ( Evado.Model.UniForm.FieldParameterList.Width );
      this.TestFileUpload.Visible = true;

      // 
      // If the url does not include a http statement add the default image url 
      // 
      stBinaryUrl = stBinaryUrl.ToLower ( );
      stBinaryUrl = Global.concatinateHttpUrl ( Global.RelativeBinaryDownloadURL, PageField.Value );

      Global.LogDebug ( "stImageUrl: " + stBinaryUrl );

      String stFieldValueStyling = "style='width:" + valueColumnWidth + "%' class='cell value cell-image-value cf' ";

      //
      // Ineert the field header
      //
      this.createFieldHeader ( stHtml, PageField, titleColumnWidth, false );

      //
      // Insert the field elements
      //
      if ( Status == Evado.Model.UniForm.EditAccess.Enabled )
      {
        stHtml.AppendLine ( "<div " + stFieldValueStyling + " >" );

        stHtml.AppendLine ( "<input name='" + PageField.FieldId + Field.CONST_IMAGE_FIELD_SUFFIX + "' "
          + "type='text' id='" + PageField.FieldId + Field.CONST_IMAGE_FIELD_SUFFIX + "' "
          + "size='80' /><br/>" );

        stHtml.AppendLine ( "<input name='" + PageField.FieldId + Field.CONST_IMAGE_FIELD_SUFFIX + "' "
          + "type='file' id='" + PageField.FieldId + Field.CONST_IMAGE_FIELD_SUFFIX + "' "
          + "size='80' />" );
      }
      else
      {
        this.createHttpLinkField (
          stHtml,
          PageField,
          Status );
      }

      stHtml.AppendLine ( "<input type='hidden' "
           + "id='" + PageField.FieldId + "' "
           + "name='" + PageField.FieldId + "' "
           + "value='" + PageField.Value + "' /> " );
      stHtml.AppendLine ( "</div>" );

      //
      // Insert the field footer
      //
      this.createFieldFooter ( stHtml, PageField );

    }//END Field Method

    // ===================================================================================
    /// <summary>
    /// This method creates a sound field HTML markup
    /// </summary>
    /// <param name="stHtml">StringBuilder object.</param>
    /// <param name="PageField">Field object.</param>
    /// <param name="TabIndex">Integer: table position.</param>
    /// <param name="Status">ClientFieldEditCodes enumerated status.</param>
    // ----------------------------------------------------------------------------------
    private void createSoundField (
      StringBuilder stHtml,
      Evado.Model.UniForm.Field PageField,
      Evado.Model.UniForm.EditAccess Status )
    {
      Global.LogClientMethod ( "createSoundField method." );
      //
      // Initialise the methods variables and objects.
      //
      int valueColumnWidth = this._GroupValueColumWidth;
      int titleColumnWidth = 100 - valueColumnWidth;
      String stFieldValueStyling = "style='width:" + valueColumnWidth + "%' class='cell value cell-sound-value cf' ";

      if ( Global.DebugLogOn == true )
      {
        //
        // Ineert the field header
        //
        this.createFieldHeader ( stHtml, PageField, titleColumnWidth, false );

        //
        // Insert the field elements
        //
        stHtml.Append ( "<div " + stFieldValueStyling + " > "
          + "Sound Field - Note Supported in the web client."
            + "</div>\r\n" );

        //
        // Insert the field footer
        //
        this.createFieldFooter ( stHtml, PageField );
      }

    }//END Field Method

    // ===================================================================================
    /// <summary>
    /// This method creates a currency field html markup
    /// </summary>
    /// <param name="stHtml">StringBuilder object.</param>
    /// <param name="PageField">Field object.</param>
    /// <param name="TabIndex">Integer: table position.</param>
    /// <param name="Status">ClientFieldEditCodes enumerated status.</param>
    // ----------------------------------------------------------------------------------
    private void createCurrencyField (
      StringBuilder stHtml,
      Evado.Model.UniForm.Field PageField,
      Evado.Model.UniForm.EditAccess Status )
    {
      Global.LogClientMethod ( "createCurrencyField method." );
      //
      // Initialise the methods variables and objects.
      //
      int valueColumnWidth = this._GroupValueColumWidth;
      int titleColumnWidth = 100 - valueColumnWidth;
      String stSize = PageField.GetParameter ( Evado.Model.UniForm.FieldParameterList.Width );
      String stFieldValueStyling = "style='width:" + valueColumnWidth + "%' class='cell value cell-currency-value cf' ";

      if ( stSize == String.Empty )
      {
        stSize = "12";
      }
      //
      // Ineert the field header
      //
      this.createFieldHeader ( stHtml, PageField, titleColumnWidth, false );

      //
      // Insert the field elements
      //
      stHtml.Append ( "<div " + stFieldValueStyling + " > "
        + "<span id='sp" + PageField.Id + "'>"
        + "<input type='text' "
        + "id='" + PageField.FieldId + "' "
        + "name='" + PageField.FieldId + "' "
        + "tabindex = '" + _TabIndex + "' "
        + "value='" + PageField.Value + "' "

        + "maxlength='" + stSize + "' "
        + "tabindex = '" + _TabIndex + "' "
        + "size='" + stSize + "' class='form-control' data-parsley-trigger=\"change\" " );

      if ( PageField.Mandatory == true && Status != Model.UniForm.EditAccess.Disabled )
      {
        //stHtml.Append ( " required " );
      }

      //this.addMandatoryIfAttribute ( stHtml, PageField );

      if ( Status == Evado.Model.UniForm.EditAccess.Disabled )
      {
        stHtml.Append ( " disabled='disabled' " );
      }

      stHtml.Append ( "/></span></div>\r\n" );

      this._TabIndex += 2;

      //
      // Insert the field footer elemements
      //
      this.createFieldFooter ( stHtml, PageField );

    }//END Field Method

    // ===================================================================================
    /// <summary>
    /// This method creates a EmailAddress field html markup
    /// </summary>
    /// <param name="stHtml">StringBuilder object.</param>
    /// <param name="PageField">Field object.</param>
    /// <param name="TabIndex">Integer: table position.</param>
    /// <param name="Status">ClientFieldEditCodes enumerated status.</param>
    // ----------------------------------------------------------------------------------
    private void createEmailAddressField (
      StringBuilder stHtml,
      Evado.Model.UniForm.Field PageField,
      Evado.Model.UniForm.EditAccess Status )
    {
      Global.LogClientMethod ( "createEmailAddressField method." );
      //
      // Initialise the methods variables and objects.
      //
      int valueColumnWidth = this._GroupValueColumWidth;
      int titleColumnWidth = 100 - valueColumnWidth;
      String stSize = PageField.GetParameter ( Evado.Model.UniForm.FieldParameterList.Width );
      String stFieldValueStyling = "style='width:" + valueColumnWidth + "%' class='cell value cell-input-email-value cf' ";

      //
      // Set default width
      //
      if ( stSize == String.Empty )
      {
        stSize = "20";
      }

      //
      // Ineert the field header
      //
      this.createFieldHeader ( stHtml, PageField, titleColumnWidth, false );

      //
      // Insert the field elements
      //
      stHtml.Append ( "<div " + stFieldValueStyling + " > "
        + "<span id='sp" + PageField.Id + "'>"
        + "<input type='text' "
        + "id='" + PageField.FieldId + "' "
        + "name='" + PageField.FieldId + "' "
        + "tabindex = '" + _TabIndex + "' "
        + "value='" + PageField.Value + "' "
        + "maxlength='" + stSize + "' "
        + "tabindex = '" + _TabIndex + "' "
        + "size='" + stSize + "' class='form-control' data-parsley-trigger=\"change\" " );

      if ( PageField.Mandatory == true && Status != Model.UniForm.EditAccess.Disabled )
      {
        //stHtml.Append ( " required " );
      }

      //this.addMandatoryIfAttribute ( stHtml, PageField );

      if ( Status == Evado.Model.UniForm.EditAccess.Disabled )
      {
        stHtml.Append ( " disabled='disabled' " );
      }

      stHtml.AppendLine ( "/></span></div>" );

      this._TabIndex += 2;

      //
      // Insert the field footer elemements
      //
      this.createFieldFooter ( stHtml, PageField );

    }//END Field Method

    // ===================================================================================
    /// <summary>
    /// This method creates a Telephone Number field html markup
    /// </summary>
    /// <param name="stHtml">StringBuilder object.</param>
    /// <param name="PageField">Field object.</param>
    /// <param name="TabIndex">Integer: table position.</param>
    /// <param name="Status">ClientFieldEditCodes enumerated status.</param>
    // ----------------------------------------------------------------------------------
    private void createTelephoneNumberField (
      StringBuilder stHtml,
      Evado.Model.UniForm.Field PageField,
      Evado.Model.UniForm.EditAccess Status )
    {
      Global.LogClientMethod ( "createTelephoneNumberField method." );
      //
      // Initialise the methods variables and objects.
      //
      int valueColumnWidth = this._GroupValueColumWidth;
      int titleColumnWidth = 100 - valueColumnWidth;
      String stSize = PageField.GetParameter ( Evado.Model.UniForm.FieldParameterList.Width );
      String stFieldValueStyling = "style='width:" + valueColumnWidth + "%' class='cell value cell-input-telephones-value cf' ";

      //
      // Set default width
      //
      if ( stSize == String.Empty )
      {
        stSize = "18";
      }

      //
      // Ineert the field header
      //
      this.createFieldHeader ( stHtml, PageField, titleColumnWidth, false );

      //
      // Insert the field elements
      //
      stHtml.Append ( "<div " + stFieldValueStyling + " > "
        + "<span id='sp" + PageField.Id + "'>"
        + "<input type='text' "
        + "id='" + PageField.FieldId + "' "
        + "name='" + PageField.FieldId + "' "
        + "tabindex = '" + _TabIndex + "' "
        + "value='" + PageField.Value + "' "

        + "maxlength='" + stSize + "' "
        + "tabindex = '" + _TabIndex + "' "
        + "size='" + stSize + "' class='form-control' data-parsley-trigger=\"change\" " );

      if ( PageField.Mandatory == true && Status != Model.UniForm.EditAccess.Disabled )
      {
        //stHtml.Append ( " required " );
      }

      //this.addMandatoryIfAttribute ( stHtml, PageField );

      if ( Status == Evado.Model.UniForm.EditAccess.Disabled )
      {
        stHtml.Append ( " disabled='disabled' " );
      }

      stHtml.AppendLine ( "/></span></div>" );

      this._TabIndex += 2;

      //
      // Insert the field footer elemements
      //
      this.createFieldFooter ( stHtml, PageField );

    }//END Field Method

    // ===================================================================================
    /// <summary>
    /// This method creates a Telephone Number field html markup
    /// </summary>
    /// <param name="stHtml">StringBuilder object.</param>
    /// <param name="PageField">Field object.</param>
    /// <param name="TabIndex">Integer: table position.</param>
    /// <param name="Status">ClientFieldEditCodes enumerated status.</param>
    // ----------------------------------------------------------------------------------
    private void createAnalogueField (
      StringBuilder stHtml,
      Evado.Model.UniForm.Field PageField,
      Evado.Model.UniForm.EditAccess Status )
    {
      Global.LogClientMethod ( "createAnalogueField method." );
      //
      // Initialise the methods variables and objects.
      //
      int valueColumnWidth = this._GroupValueColumWidth;
      int titleColumnWidth = 100 - valueColumnWidth;
      String stFieldValueStyling = "style='width:100%;' class='cell value cell-input-telephones-value cf' ";
      //
      // Set the column layout to display the analogue scale below the field title and instructions.
      //
      PageField.Layout = Model.UniForm.FieldLayoutCodes.Column_Layout;

      //
      // Ineert the field header
      //
      this.createFieldHeader ( stHtml, PageField, titleColumnWidth, true );

      //
      // Insert the field elements
      //
      stHtml.AppendLine ( "<div " + stFieldValueStyling + " > " );
      stHtml.AppendLine ( "<span id='sp" + PageField.Id + "'>" );
      stHtml.Append ( "<input type='range' "
        + "id='" + PageField.FieldId + "' "
        + "name='" + PageField.FieldId + "' "
        + "value='" + PageField.Value + "' "
        + "tabindex = '" + _TabIndex + "' "
        + "min='0' "
        + "min='100' "
        + "step='10' "
        + "tabindex = '" + _TabIndex + "' "
        + "class='form-control-analogue' "
        + "data-parsley-trigger=\"change\" " );

      if ( PageField.Mandatory == true && Status != Evado.Model.UniForm.EditAccess.Disabled )
      {
        //stHtml.Append ( " required " );
      }

      //this.addMandatoryIfAttribute ( stHtml, PageField );

      if ( Status == Evado.Model.UniForm.EditAccess.Disabled )
      {
        stHtml.Append ( " disabled='disabled' " );
      }

      stHtml.AppendLine ( "/>" );
      stHtml.AppendLine ( "</span>" );
      stHtml.AppendLine ( "</div>" );

      this._TabIndex += 2;

      _TabIndex++;

      //
      // Insert the field footer elemements
      //
      this.createFieldFooter ( stHtml, PageField );

    }//END Field Method

    // ===================================================================================
    /// <summary>
    /// This method creates a Name field html markup
    /// </summary>
    /// <param name="stHtml">StringBuilder object.</param>
    /// <param name="PageField">Field object.</param>
    /// <param name="TabIndex">Integer: table position.</param>
    /// <param name="Status">ClientFieldEditCodes enumerated status.</param>
    // ----------------------------------------------------------------------------------
    private void createNameField (
      StringBuilder stHtml,
      Evado.Model.UniForm.Field PageField,
      Evado.Model.UniForm.EditAccess Status )
    {
      Global.LogClientMethod ( "createNameField method." );
      //
      // Initialise the methods variables and objects.
      //
      int valueColumnWidth = this._GroupValueColumWidth;
      int titleColumnWidth = 100 - valueColumnWidth;
      int fieldSize = PageField.GetParameterInt ( Evado.Model.UniForm.FieldParameterList.Width );
      String stFormat = PageField.GetParameter ( Evado.Model.UniForm.FieldParameterList.Format );
      String stFieldValueStyling = "style='width:" + valueColumnWidth + "%' class='cell value cell-input-name-value cf' ";

      string stTitle = String.Empty;
      string stFirstName = String.Empty;
      string stMiddleName = String.Empty;
      string stFamilyName = String.Empty;


      String [ ] arrValue = PageField.Value.Split ( ';' );

      //
      // Fill the field structure
      switch ( arrValue.Length )
      {
        case 1:
          {
            stFamilyName = arrValue [ 0 ];
            break;
          }
        case 2:
          {
            stFirstName = arrValue [ 0 ];
            stFamilyName = arrValue [ 1 ];
            break;
          }
        case 3:
          {
            stTitle = arrValue [ 0 ];
            stFirstName = arrValue [ 1 ];
            stFamilyName = arrValue [ 2 ];
            break;
          }
        case 4:
        default:
          {
            stTitle = arrValue [ 0 ];
            stFirstName = arrValue [ 1 ];
            stMiddleName = arrValue [ 2 ];
            stFamilyName = arrValue [ 3 ];
            break;
          }
      }//END field value switch

      //
      // Set default width
      //
      if ( fieldSize > 30 || fieldSize < 5 )
      {
        fieldSize = 20;
      }

      //
      // Ineert the field header
      //
      this.createFieldHeader ( stHtml, PageField, titleColumnWidth, false );

      //
      // Insert the field elements
      //
      stHtml.AppendLine ( "<div " + stFieldValueStyling + " > " );
      stHtml.AppendLine ( "<div id='sp" + PageField.Id + "' >" );


      if ( stFormat.Contains ( Evado.Model.UniForm.Field.CONST_NAME_FORMAT_PREFIX ) == true )
      {
        stHtml.AppendLine ( "<div style='display: inline-block;'>" );
        stHtml.AppendLine ( "<input type='text' "
         + "id='" + PageField.FieldId + "_Title' "
         + "name='" + PageField.FieldId + "_Title' "
         + "value='" + stTitle + "' "
        + "tabindex = '" + _TabIndex + "' "
         + "tabindex = '" + _TabIndex + "' "
         + "size='3' class='form-control' data-parsley-trigger=\"change\" " );

        if ( PageField.Mandatory == true && Status != Model.UniForm.EditAccess.Disabled )
        {
          //stHtml.Append ( " required " );
        }

        //this.addMandatoryIfAttribute ( stHtml, PageField );

        if ( Status == Evado.Model.UniForm.EditAccess.Disabled )
        {
          stHtml.Append ( " disabled='disabled' " );
        }

        stHtml.Append ( "/></div>\r\n" );

        this._TabIndex++;
      }
      stHtml.AppendLine ( "<div style='display: inline-block;'>" );
      stHtml.AppendLine ( "<input type='text' "
       + "id='" + PageField.FieldId + "_FirstName' "
       + "name='" + PageField.FieldId + "_FirstName' "
       + "value='" + stFirstName + "' "
      + "tabindex = '" + _TabIndex + "' "
       + "tabindex = '" + _TabIndex + "' "
       + "size='" + fieldSize + "' class='form-control' data-parsley-trigger=\"change\" " );

      if ( PageField.Mandatory == true && Status != Model.UniForm.EditAccess.Disabled )
      {
        //stHtml.Append ( " required " );
      }

      //this.addMandatoryIfAttribute ( stHtml, PageField );

      if ( Status == Evado.Model.UniForm.EditAccess.Disabled )
      {
        stHtml.Append ( " disabled='disabled' " );
      }

      stHtml.Append ( "/></div>\r\n" );

      this._TabIndex++;

      if ( stFormat.Contains ( Evado.Model.UniForm.Field.CONST_NAME_FORMAT_MIDDLE_NAME ) == true )
      {
        stHtml.AppendLine ( "<div style='display: inline-block;'>" );
        stHtml.AppendLine ( "<input type='text' "
         + "id='" + PageField.FieldId + "_MiddleName' "
         + "name='" + PageField.FieldId + "_MiddleName' "
         + "value='" + stMiddleName + "' "
        + "tabindex = '" + _TabIndex + "' "
         + "tabindex = '" + _TabIndex + "' "
         + "size='" + fieldSize + "' class='form-control' data-parsley-trigger=\"change\" " );

        if ( PageField.Mandatory == true && Status != Model.UniForm.EditAccess.Disabled )
        {
          //stHtml.Append ( " required " );
        }

        //this.addMandatoryIfAttribute ( stHtml, PageField );

        if ( Status == Evado.Model.UniForm.EditAccess.Disabled )
        {
          stHtml.Append ( " disabled='disabled' " );
        }

        stHtml.Append ( "/></div>\r\n" );

        this._TabIndex++;
      }

      //
      // Family Name field
      //
      stHtml.Append ( "<div style='display: inline-block;'><input type='text' "
       + "id='" + PageField.FieldId + "_FamilyName' "
       + "name='" + PageField.FieldId + "_FamilyName' "
       + "value='" + stFamilyName + "' "
       + "tabindex = '" + _TabIndex + "' "
       + "size='" + fieldSize + "' class='form-control' data-parsley-trigger=\"change\" " );

      if ( PageField.Mandatory == true && Status != Model.UniForm.EditAccess.Disabled )
      {
        //stHtml.Append ( " required " );
      }

      //this.addMandatoryIfAttribute ( stHtml, PageField );

      if ( Status == Evado.Model.UniForm.EditAccess.Disabled )
      {
        stHtml.Append ( " disabled='disabled' " );
      }

      stHtml.Append ( "/></div>\r\n" );

      this._TabIndex++;

      stHtml.Append ( "</div></div>\r\n" );

      //
      // Insert the field footer elemements
      //
      this.createFieldFooter ( stHtml, PageField );

    }//END Field Method

    // ===================================================================================
    /// <summary>
    /// This method creates a Name field html markup
    /// </summary>
    /// <param name="stHtml">StringBuilder object.</param>
    /// <param name="PageField">Field object.</param>
    /// <param name="TabIndex">Integer: table position.</param>
    /// <param name="Status">ClientFieldEditCodes enumerated status.</param>
    // ----------------------------------------------------------------------------------
    private void createAddressField (
      StringBuilder stHtml,
      Evado.Model.UniForm.Field PageField,
      Evado.Model.UniForm.EditAccess Status )
    {
      Global.LogClientMethod ( "createAddressField method." );
      //
      // Initialise the methods variables and objects.
      //
      int valueColumnWidth = this._GroupValueColumWidth;
      int titleColumnWidth = 100 - valueColumnWidth;
      String stSize = PageField.GetParameter ( Evado.Model.UniForm.FieldParameterList.Width );
      String stFieldValueStyling = "style='width:" + valueColumnWidth + "%' class='cell value cell-input-address-value cf' ";
      String [ ] arrValue = PageField.Value.Split ( ';' );

      //
      // Set default width
      //
      if ( stSize == String.Empty )
      {
        stSize = "20";
      }

      //
      // Ineert the field header
      //
      this.createFieldHeader ( stHtml, PageField, titleColumnWidth, false );

      //
      // Insert the field elements
      //
      stHtml.Append ( "<div " + stFieldValueStyling + " > "
        + "<span id='sp" + PageField.Id + "'>" );

      if ( arrValue.Length > 5 )
      {
        //
        // Address 1 field
        //
        stHtml.Append ( "<div class='first' style='display: inline-block;'><span style='width:100px'>" + EuLabels.Address_Field_Address_1_Label + "</span><input type='text' "
         + "id='" + PageField.FieldId + "_Address1' "
         + "name='" + PageField.FieldId + "_Address1' "
         + "tabindex='" + _TabIndex + "' "
         + "value='" + arrValue [ 0 ] + "' "
         + "size='" + stSize + "' class='form-control' style='display: inline-block;' data-parsley-trigger=\"change\" " );

        if ( PageField.Mandatory == true && Status != Model.UniForm.EditAccess.Disabled )
        {
          //stHtml.Append ( " required " );
        }

        //this.addMandatoryIfAttribute ( stHtml, PageField );

        if ( Status == Evado.Model.UniForm.EditAccess.Disabled )
        {
          stHtml.Append ( " disabled='disabled' " );
        }

        stHtml.Append ( "/></div>\r\n" );

        this._TabIndex++;

        //
        // Address 2 field
        //
        stHtml.Append ( "<div style='display: inline-block;'><span style='width:100px'>" + EuLabels.Address_Field_Address_2_Label + "</span><input type='text' "
         + "id='" + PageField.FieldId + "_Address2' "
         + "name='" + PageField.FieldId + "_Address2' "
         + "tabindex='" + _TabIndex + "' "
         + "value='" + arrValue [ 1 ] + "' "
         + "size='" + stSize + "' class='form-control' style='width:200px;inline-block;' data-parsley-trigger=\"change\" " );

        if ( PageField.Mandatory == true && Status != Model.UniForm.EditAccess.Disabled )
        {
          //stHtml.Append ( " required " );
        }

        //this.addMandatoryIfAttribute ( stHtml, PageField );

        if ( Status == Evado.Model.UniForm.EditAccess.Disabled )
        {
          stHtml.Append ( " disabled='disabled' " );
        }

        stHtml.Append ( "/></div>\r\n" );

        this._TabIndex++;

        //
        // Suburb field
        //
        stHtml.Append ( "<div style='display: inline-block;'><span style='width:100px'>" + EuLabels.Address_Field_City_Label + "</span><input type='text' "
         + "id='" + PageField.FieldId + "_Suburb' "
         + "name='" + PageField.FieldId + "_Suburb' "
         + "tabindex='" + _TabIndex + "' "
         + "value='" + arrValue [ 2 ] + "' "
         + "size='" + stSize + "' class='form-control' style='width:200px;inline-block;' data-parsley-trigger=\"change\" " );

        if ( PageField.Mandatory == true && Status != Model.UniForm.EditAccess.Disabled )
        {
          //stHtml.Append ( " required " );
        }

        //this.addMandatoryIfAttribute ( stHtml, PageField );

        if ( Status == Evado.Model.UniForm.EditAccess.Disabled )
        {
          stHtml.Append ( " disabled='disabled' " );
        }

        stHtml.Append ( "/></div>\r\n" );

        this._TabIndex++;

        //
        // State field
        //
        stHtml.Append ( "<div style='display: inline-block;'><span style='width:100px'>" + EuLabels.Address_Field_State_Label + "</span><input type='text' "
         + "id='" + PageField.FieldId + "_State' "
         + "name='" + PageField.FieldId + "_State' "
         + "tabindex='" + _TabIndex + "' "
         + "value='" + arrValue [ 3 ] + "' "
         + "size='" + 5 + "' class='form-control' style='width:200px;inline-block;' data-parsley-trigger=\"change\" " );

        if ( PageField.Mandatory == true && Status != Model.UniForm.EditAccess.Disabled )
        {
          //stHtml.Append ( " required " );
        }

        //this.addMandatoryIfAttribute ( stHtml, PageField );

        if ( Status == Evado.Model.UniForm.EditAccess.Disabled )
        {
          stHtml.Append ( " disabled='disabled' " );
        }

        stHtml.Append ( "/></div>\r\n" );

        this._TabIndex++;

        //
        //_PostCode field
        //
        stHtml.Append ( "<div style='display: inline-block;'><span style='width:100px'>" + EuLabels.Address_Field_Post_Code_Label + "</span><input type='text' "
         + "id='" + PageField.FieldId + "_PostCode' "
         + "name='" + PageField.FieldId + "_PostCode' "
         + "tabindex='" + _TabIndex + "' "
         + "value='" + arrValue [ 4 ] + "' "
         + "size='6' maxlength='6' class='form-control' style='width:200px;inline-block;' data-parsley-trigger=\"change\" " );

        if ( PageField.Mandatory == true && Status != Model.UniForm.EditAccess.Disabled )
        {
          //stHtml.Append ( " required " );
        }

        //this.addMandatoryIfAttribute ( stHtml, PageField );

        if ( Status == Evado.Model.UniForm.EditAccess.Disabled )
        {
          stHtml.Append ( " disabled='disabled' " );
        }

        stHtml.Append ( "/></div>\r\n" );

        this._TabIndex++;

        //
        // Country field
        //
        stHtml.Append ( "<div class='last' style='display: inline-block;' ><span style='width:100px'>" + EuLabels.Address_Field_Country_Label + "</span><input type='text' "
         + "id='" + PageField.FieldId + "_Country' "
         + "name='" + PageField.FieldId + "_Country' "
         + "tabindex='" + _TabIndex + "' "
         + "value='" + arrValue [ 5 ] + "' "
         + "size='" + stSize + "' class='form-control' style='width:200px;inline-block;' data-parsley-trigger=\"change\" " );

        if ( Status == Evado.Model.UniForm.EditAccess.Disabled )
        {
          stHtml.Append ( " disabled='disabled' " );
        }

        stHtml.Append ( "/></div>\r\n" );

        this._TabIndex++;
      }
      else
      {
        //
        // Address 1 field
        //
        stHtml.Append ( "<div class='first' style='display: inline-block;'><span style='width:100px'>" + EuLabels.Address_Field_Address_1_Label + "</span><input type='text' "
         + "id='" + PageField.FieldId + "_Address1' "
         + "name='" + PageField.FieldId + "_Address1' "
         + "tabindex='" + _TabIndex + "' "
         + "value='' "
         + "tabindex = '" + _TabIndex + "' "
         + "size='" + stSize + "' class='form-control'  style='width:200px;inline-block;' data-parsley-trigger=\"change\" " );

        if ( PageField.Mandatory == true && Status != Model.UniForm.EditAccess.Disabled )
        {
          //stHtml.Append ( " required " );
        }

        //this.addMandatoryIfAttribute ( stHtml, PageField );

        if ( Status == Evado.Model.UniForm.EditAccess.Disabled )
        {
          stHtml.Append ( " disabled='disabled' " );
        }

        stHtml.Append ( "/></div>\r\n" );

        this._TabIndex++;

        //
        // Address 2 field
        //
        stHtml.Append ( "<div style='display: inline-block;'><span style='width:100px'>" + EuLabels.Address_Field_Address_2_Label + "</span><input type='text' "
         + "id='" + PageField.FieldId + "_Address2' "
         + "name='" + PageField.FieldId + "_Address2' "
         + "tabindex='" + _TabIndex + "' "
         + "value='' "
         + "size='" + stSize + "' class='form-control'  style='width:200px;inline-block;' data-parsley-trigger=\"change\" " );

        if ( PageField.Mandatory == true && Status != Model.UniForm.EditAccess.Disabled )
        {
          //stHtml.Append ( " required " );
        }

        //this.addMandatoryIfAttribute ( stHtml, PageField );

        if ( Status == Evado.Model.UniForm.EditAccess.Disabled )
        {
          stHtml.Append ( " disabled='disabled' " );
        }

        stHtml.Append ( "/></div>\r\n" );

        this._TabIndex++;

        //
        // Suburb field
        //
        stHtml.Append ( "<div style='display: inline-block;'><span style='width:100px'>" + EuLabels.Address_Field_City_Label + "</span><input type='text' "
         + "id='" + PageField.FieldId + "_Suburb' "
         + "name='" + PageField.FieldId + "_Suburb' "
         + "tabindex='" + _TabIndex + "' "
         + "value='' "
         + "size='" + stSize + "' class='form-control'  style='width:200px;inline-block;' data-parsley-trigger=\"change\" " );

        if ( PageField.Mandatory == true && Status != Model.UniForm.EditAccess.Disabled )
        {
          //stHtml.Append ( " required " );
        }

        //this.addMandatoryIfAttribute ( stHtml, PageField );

        if ( Status == Evado.Model.UniForm.EditAccess.Disabled )
        {
          stHtml.Append ( " disabled='disabled' " );
        }

        stHtml.Append ( "/></div>\r\n" );

        this._TabIndex++;

        //
        // State field
        //
        stHtml.Append ( "<div style='display: inline-block;'><span style='width:100px'>" + EuLabels.Address_Field_State_Label + "</span><input type='text' "
         + "id='" + PageField.FieldId + "_State' "
         + "name='" + PageField.FieldId + "_State' "
         + "tabindex = '" + _TabIndex + "' "
         + "value='' "
         + "maxlength='" + stSize + "' "
         + "size='" + stSize + "' class='form-control'  style='width:200px;inline-block;' data-parsley-trigger=\"change\" " );

        if ( PageField.Mandatory == true && Status != Model.UniForm.EditAccess.Disabled )
        {
          //stHtml.Append ( " required " );
        }

        //this.addMandatoryIfAttribute ( stHtml, PageField );

        if ( Status == Evado.Model.UniForm.EditAccess.Disabled )
        {
          stHtml.Append ( " disabled='disabled' " );
        }

        stHtml.Append ( "/></div>\r\n" );

        this._TabIndex++;

        //
        //_PostCode field
        //
        stHtml.Append ( "<div style='display: inline-block;'><span style='width:100px'>" + EuLabels.Address_Field_Post_Code_Label + "</span><input type='text' "
         + "id='" + PageField.FieldId + "_PostCode' "
         + "name='" + PageField.FieldId + "_PostCode' "
         + "tabindex='" + _TabIndex + "' "
         + "value='' "
         + "class='form-control'  style='width:200px;inline-block;' data-parsley-trigger=\"change\" "
         + "size='6' maxlength='6' " );

        if ( PageField.Mandatory == true && Status != Model.UniForm.EditAccess.Disabled )
        {
          //stHtml.Append ( " required " );
        }

        //this.addMandatoryIfAttribute ( stHtml, PageField );

        if ( Status == Evado.Model.UniForm.EditAccess.Disabled )
        {
          stHtml.Append ( " disabled='disabled' " );
        }

        stHtml.Append ( "/></div>\r\n" );

        this._TabIndex++;

        //
        // Country field
        //
        stHtml.Append ( "<div class='last' style='display: inline-block;'><span style='width:100px'>" + EuLabels.Address_Field_Country_Label + "</span><input type='text' "
         + "id='" + PageField.FieldId + "_Country' "
         + "name='" + PageField.FieldId + "_Country' "
         + "tabindex='" + _TabIndex + "' "
         + "value='' "
         + "size='" + stSize + "' "
         + "class='form-control'  style='width:200px;inline-block;' data-parsley-trigger=\"change\" " );

        if ( Status == Evado.Model.UniForm.EditAccess.Disabled )
        {
          stHtml.Append ( " disabled='disabled' " );
        }

        stHtml.Append ( "/></div>\r\n" );

        this._TabIndex++;

      }

      stHtml.Append ( "</span></div>\r\n" );

      this._TabIndex++;

      //
      // Insert the field footer elemements
      //
      this.createFieldFooter ( stHtml, PageField );

    }//END Field Method

    // ===================================================================================
    /// <summary>
    /// This method creates a signature field html markup
    /// </summary>
    /// <param name="stHtml">StringBuilder object.</param>
    /// <param name="PageField">Field object.</param>
    /// <param name="TabIndex">Integer: table position.</param>
    /// <param name="GroupStatus">ClientFieldEditCodes enumerated status.</param>
    // ----------------------------------------------------------------------------------
    private void createSignatureField (
      StringBuilder stHtml,
      Evado.Model.UniForm.Field PageField,
      Evado.Model.UniForm.EditAccess GroupStatus )
    {
      Global.LogClientMethod ( "createSignatureField method." );
      Global.LogClient ( "Field.Status: " + PageField.EditAccess );
      Global.LogClient ( "GroupStatus: " + GroupStatus );
      //
      // Initialise the methods variables and objects.
      //
      int valueColumnWidth = this._GroupValueColumWidth;
      int titleColumnWidth = 100 - valueColumnWidth;
      String fieldId = PageField.FieldId.ToLower ( );
      Evado.Model.EvSignatureBlock signature = new Evado.Model.EvSignatureBlock ( );
      String RasterSignature = String.Empty;
      int titleWidth = 30;
      int valueWidth = 70;
      String canvasWidth = "650";
      String canvasHeight = "225";
      bool fullWidth = false;
      String stFieldValueStyling = "style='width:" + valueWidth + "%' class='cell value cell-input-email-value cf' ";

      PageField.Layout = FieldLayoutCodes.Left_Justified;

      if ( PageField.EditAccess == Model.UniForm.EditAccess.Inherited )
      {
        PageField.EditAccess = GroupStatus;
      }

      Global.LogClient ( "Set Field.Status: " + PageField.EditAccess );

      //
      // Ineert the field header
      //
      this.createFieldHeader ( stHtml, PageField, titleWidth, fullWidth );

      //
      // Set the canvas width and height
      //
      if ( PageField.hasParameter ( Evado.Model.UniForm.FieldParameterList.Width ) == true )
      {
        canvasWidth = PageField.GetParameter ( Evado.Model.UniForm.FieldParameterList.Width );
      }

      if ( PageField.hasParameter ( Evado.Model.UniForm.FieldParameterList.Height ) == true )
      {
        canvasHeight = PageField.GetParameter ( Evado.Model.UniForm.FieldParameterList.Height );
      }

      Global.LogClient ( "Field Value: " + PageField.Value );

      if ( PageField.Value != String.Empty )
      {
        try
        {
          signature = Newtonsoft.Json.JsonConvert.DeserializeObject<Evado.Model.EvSignatureBlock> ( PageField.Value );

          RasterSignature = Newtonsoft.Json.JsonConvert.SerializeObject ( signature.Signature );
        }
        catch
        {
          RasterSignature = String.Empty;
        }
      }

      Global.LogClient ( "Raster Signature: " + RasterSignature );
      Global.LogClient ( "signature.Name: " + signature.Name );
      Global.LogClient ( "signature.AcceptedBy: " + signature.AcceptedBy );
      Global.LogClient ( "signature.DateStamp: " + signature.DateStamp );

      //
      // Insert the field elements
      //
      stHtml.AppendLine ( "<div " + stFieldValueStyling + " > " );
      stHtml.AppendLine ( "<div id='sp" + PageField.Id + "' class='sigPad' >" );

      stHtml.AppendLine ( "<div id='sr" + PageField.Id + "' class='sigWrapper' > " );

      stHtml.Append ( "<canvas id='cav" + PageField.Id + "' " );
      stHtml.Append ( " class='pad'" );
      stHtml.Append ( " width='" + canvasWidth + "px'" );
      stHtml.Append ( " height='" + canvasHeight + "px' >" );
      stHtml.AppendLine ( "</canvas>" );
      /*
      stHtml.AppendLine ( "<canvas id='cav" + PageField.Id + "' class='pad'"
        + " width='100%' "
        + " height='50%' ></canvas>" );
      */
      if ( Global.DebugDisplayOn == true )
      {
        stHtml.AppendLine ( "<input " );
        stHtml.Append ( " type='text' " );
        stHtml.Append ( " id='" + PageField.FieldId + "_sig' " );
        stHtml.Append ( " name='" + PageField.FieldId + "_sig' " );
        stHtml.Append ( "tabindex='" + _TabIndex + "' " );
        stHtml.Append ( " class='output' " );
        stHtml.AppendLine ( " size='10' /> " );
      }
      else
      {
        stHtml.AppendLine ( "<input " );
        stHtml.Append ( " type='hidden' " );
        stHtml.Append ( " id='" + PageField.FieldId + "_sig' " );
        stHtml.Append ( " name='" + PageField.FieldId + "_sig' " );
        stHtml.Append ( " tabindex='" + _TabIndex + "' " );
        stHtml.AppendLine ( " class='output' /> " );
      }

      this._TabIndex += 2;

      stHtml.AppendLine ( "<input " );
      stHtml.Append ( " type='text' " );
      stHtml.Append ( " id='" + PageField.FieldId + "_name' " );
      stHtml.Append ( " name='" + PageField.FieldId + "_name' " );
      stHtml.Append ( " tabindex='" + _TabIndex + "' " );
      stHtml.Append ( " value='" + signature.Name + "' " );
      stHtml.Append ( " class='sigName' " );
      stHtml.Append ( "style='width: " + canvasWidth + "px; '" );

      if ( GroupStatus == Evado.Model.UniForm.EditAccess.Disabled )
      {
        stHtml.Append ( " disabled='disabled' " );
      }
      stHtml.AppendLine ( "/>" );

      stHtml.AppendLine ( "</div>" );

      if ( PageField.EditAccess == Model.UniForm.EditAccess.Enabled )
      {
        stHtml.AppendLine ( "<div class='sigNav menu links'>" );
        stHtml.AppendLine ( "<span class='clearButton'>" );
        stHtml.AppendLine ( "<a href='#clear' "
          + " class='btn btn-danger cmd-button'>" + EuLabels.Signature_Clear + "</a>" );
        stHtml.AppendLine ( "</span>" );
        stHtml.AppendLine ( "</div>" );
      }
      stHtml.Append ( "</div>" );
      stHtml.Append ( "</div>" );

      this._TabIndex += 2;
      /*
      stHtml.AppendLine ( "<script type=\"text/javascript\">" );
      stHtml.AppendLine ( "$(document).ready(function() { " );

      stHtml.AppendLine ( "var width = document.getElementById('sr" + PageField.Id + "').scrollWidth;" );
      stHtml.AppendLine ( "var width = width-20;" );
      stHtml.AppendLine ( "var height = width/3;" );
     // stHtml.AppendLine ( "alert( \"width: \" + width +  \"height: \" +height);" );

      stHtml.AppendLine ( "var canv = document.getElementById('cav" + PageField.Id + "');" );
      stHtml.AppendLine ( " canv.width = width;" );
      stHtml.AppendLine ( " canv.height = height ;" );
      stHtml.AppendLine ( "</script>" );
      */

      if ( PageField.EditAccess == Model.UniForm.EditAccess.Disabled )
      {
        Global.LogClient ( "Setting the signature for display only" );

        stHtml.AppendLine ( "<script type=\"text/javascript\">" );
        stHtml.AppendLine ( "$(document).ready(function() { " );
        stHtml.AppendLine ( "var sig = document.getElementById('" + PageField.FieldId + "_input').value;" );
        //stHtml.AppendLine ( "alert( \"value: \" + sig );" );
        stHtml.AppendLine ( "console.log( \"Enabling the signature pad\" ); " );
        stHtml.AppendLine ( "if (sig != \"\"){ " );
        stHtml.AppendLine ( "var api = $('#sp" + PageField.Id + "').signaturePad({ displayOnly: true });" );
        stHtml.AppendLine ( "api.regenerate(sig);" );
        stHtml.AppendLine ( " } " );
        stHtml.AppendLine ( " }); " );
        stHtml.AppendLine ( "</script>" );

        if ( Global.DebugDisplayOn == true )
        {
          stHtml.AppendLine ( "<input " );
          stHtml.Append ( " type='text' " );
          stHtml.Append ( " id='" + PageField.FieldId + "_input' " );
          stHtml.Append ( " name='" + PageField.FieldId + "_input' " );
          stHtml.Append ( " tabindex='" + _TabIndex + "' " );
          stHtml.AppendLine ( " value='" + RasterSignature + "' /> " );

          this._TabIndex += 2;
        }
        else
        {
          stHtml.Append ( "<input " );
          stHtml.Append ( " type='hidden' " );
          stHtml.Append ( " id='" + PageField.FieldId + "_input' " );
          stHtml.Append ( " name='" + PageField.FieldId + "_input' " );
          stHtml.Append ( " tabindex='" + _TabIndex + "' " );
          stHtml.AppendLine ( " value='" + RasterSignature + "' /> " );

          this._TabIndex += 2;
        }
      }
      else
      {
        Global.LogClient ( "Setting the signature for draw a signature" );

        stHtml.AppendLine ( "<script type=\"text/javascript\">" );
        stHtml.AppendLine ( "$(document).ready(function() { " );
        stHtml.AppendLine ( "console.log( \"Enabling the signature pad\" ); " );
        stHtml.AppendLine ( "$('#sp" + PageField.Id + "').signaturePad({ drawOnly: true, validateFields: false,  lineTop: " + canvasHeight + " });" );
        stHtml.AppendLine ( " }); " );
        stHtml.AppendLine ( "</script>" );
      }

      //
      // Insert the field footer elemements
      //
      this.createFieldFooter ( stHtml, PageField );

    }//END Field Method

    // ===================================================================================
    /// <summary>
    /// This method creates a text field html markup
    /// </summary>
    /// <param name="stHtml">StringBuilder containing the page html</param>
    /// <param name="PageField">Field object.</param>
    /// <param name="Status">ClientFieldEditCodes enumerated status.</param>
    /// <returns>String html</returns>
    // ----------------------------------------------------------------------------------
    private void createPasswordField (
      StringBuilder stHtml,
      Evado.Model.UniForm.Field PageField,
      Evado.Model.UniForm.EditAccess Status )
    {
      Global.LogClientMethod ( "createPasswordField method." );
      //
      // Initialise the methods variables and objects.
      //
      int valueColumnWidth = this._GroupValueColumWidth;
      int titleColumnWidth = 100 - valueColumnWidth;
      String stSize = PageField.GetParameter ( Evado.Model.UniForm.FieldParameterList.Width );
      String stRows = PageField.GetParameter ( Evado.Model.UniForm.FieldParameterList.Height );
      String stFieldValueStyling = "style='' class='cell value cell-input-text-value cf' ";

      //
      // Set default width
      //
      if ( stSize == String.Empty )
      {
        stSize = "50";
      }

      //
      // Ineert the field header
      //
      this.createFieldHeader ( stHtml, PageField, titleColumnWidth, false );

      //
      // Insert the field data control
      //
      stHtml.Append ( "<div " + stFieldValueStyling + " > "
         + "<span id='sp" + PageField.Id + "'>"
         + "<input type='password' "
         + "id='" + PageField.FieldId + "' "
         + "name='" + PageField.FieldId + "' "
         + "tabindex = '" + _TabIndex + "' "
         + "value='" + PageField.Value + "' "
         + "tabindex = '" + this._TabIndex + "' "
         + "maxlength='" + stSize + "' "
         + "size='" + stSize + "' "
         + "class='form-control' "
         + "data-fieldid='" + PageField.FieldId + "' "
         + "class='form-control' "
         + "data-parsley-trigger=\"change\" " );

      if ( PageField.Mandatory == true && Status != Evado.Model.UniForm.EditAccess.Disabled )
      {
        //stHtml.Append ( " required " );
      }

      //this.addMandatoryIfAttribute ( stHtml, PageField );

      if ( Status == Evado.Model.UniForm.EditAccess.Disabled )
      {
        stHtml.Append ( " disabled='disabled' " );
      }

      stHtml.Append ( "/></span></div>\r\n" );

      this._TabIndex++;
      this._TabIndex++;

      //
      // Insert the field footer elemements
      //
      this.createFieldFooter ( stHtml, PageField );

    }//END Field Method

    // ===================================================================================
    /// <summary>
    /// This method creates a Name field html markup
    /// </summary>
    /// <param name="stHtml">StringBuilder object.</param>
    /// <param name="PageField">Field object.</param>
    /// <param name="Status">ClientFieldEditCodes enumerated status.</param>
    // ----------------------------------------------------------------------------------
    private void createNumericRangeField (
      StringBuilder stHtml,
      Evado.Model.UniForm.Field PageField,
      Evado.Model.UniForm.EditAccess Status )
    {
      Global.LogClientMethod ( "createNumericRangeField method." );
      Global.LogDebug ( "Field.Type: " + PageField.Type );
      //
      // Initialise the methods variables and objects.
      //
      int valueColumnWidth = this._GroupValueColumWidth;
      int titleColumnWidth = 100 - valueColumnWidth;
      String stSize = "15"; ;
      String stFieldValueStyling = "style='' class='cell value cell-input-name-value' "; //
      String [ ] arrValue = PageField.Value.Split ( ';' );
      String stLowerValue = String.Empty;
      String stUpperValue = String.Empty;
      String value = String.Empty;

      String stUnit = PageField.GetParameter ( Evado.Model.UniForm.FieldParameterList.Unit );
      String stMinValue = "-1000000";
      String stMaxValue = "1000000";
      String stMinAlert = "-1000000";
      String stMaxAlert = "1000000";
      String stMinNorm = "-1000000";
      String stMaxNorm = "1000000";
      String stCssValid = PageField.GetParameter ( Evado.Model.UniForm.FieldParameterList.BG_Validation );
      String stCssAlert = PageField.GetParameter ( Evado.Model.UniForm.FieldParameterList.BG_Alert );
      String stCssNormal = PageField.GetParameter ( Evado.Model.UniForm.FieldParameterList.BG_Normal );
      String stCustomValidation = PageField.GetParameter ( Evado.Model.UniForm.FieldParameterList.Validation_Callback );

      value = PageField.GetParameter ( Evado.Model.UniForm.FieldParameterList.Min_Value );
      if ( value != String.Empty )
      {
        stMinValue = value;
      }
      value = PageField.GetParameter ( Evado.Model.UniForm.FieldParameterList.Max_Value );
      if ( value != String.Empty )
      {
        stMaxValue = value;
      }
      value = PageField.GetParameter ( Evado.Model.UniForm.FieldParameterList.Min_Alert );
      if ( value != String.Empty )
      {
        stMinAlert = value;
      }
      value = PageField.GetParameter ( Evado.Model.UniForm.FieldParameterList.Max_Alert );
      if ( value != String.Empty )
      {
        stMaxAlert = value;
      }
      value = PageField.GetParameter ( Evado.Model.UniForm.FieldParameterList.Min_Normal );
      if ( value != String.Empty )
      {
        stMinNorm = value;
      }

      value = PageField.GetParameter ( Evado.Model.UniForm.FieldParameterList.Max_Normal );
      if ( value != String.Empty )
      {
        stMaxNorm = value;
      }

      if ( arrValue.Length < 2 )
      {
        stLowerValue = PageField.Value;
      }
      else
      {
        stLowerValue = arrValue [ 0 ];
        stUpperValue = arrValue [ 1 ];
      }

      //
      // Set the normal validation parameters.
      ///
      string stValidationMethod = " onchange=\"Evado.Form.onRangeValidation( this, this.value )\" ";

      if ( PageField.Type == Evado.Model.EvDataTypes.Integer )
      {
        stValidationMethod = " data-parsley-integerna \" ";
      }

      if ( stCustomValidation != String.Empty )
      {
        stValidationMethod = " onchange=\"Evado.Form.onCustomValidation('" + stCustomValidation + "', this, this.value )\" ";
      }

      Global.LogDebugMethod ( "Unit: " + stUnit );

      if ( stUnit.Contains ( "10^" ) == true
        && stUnit.Contains ( "10^0" ) == false )
      {
        stUnit = Regex.Replace ( stUnit, @"10\^([-0-9])(.*)", "10<span class='sup'>$1</span> $2" );
      }

      if ( stUnit != String.Empty )
      {
        stUnit = "<div class='form-unit' >&nbsp;" + stUnit + "</div>";
      }

      //
      // Ineert the field header
      //
      this.createFieldHeader ( stHtml, PageField, titleColumnWidth, false );

      //
      // Insert the field elements
      //
      stHtml.AppendLine ( "<div " + stFieldValueStyling + " > " );
      stHtml.AppendLine ( "<div id='sp" + PageField.Id + "' class='form-field-container-inline' >" );

      stHtml.AppendLine ( "<input type='text' "
        + "id='" + PageField.FieldId + DefaultPage.stField_LowerSuffix + "' "
        + "name='" + PageField.FieldId + DefaultPage.stField_LowerSuffix + "' "
        + "value='" + stLowerValue + "' "
        + "tabindex = '" + _TabIndex + "' "
        + "maxlength='" + stSize + "' "
        + "tabindex = '" + this._TabIndex + "' "
        + "size='" + stSize + "' "
        + "class='form-control-range ' "
        + "style='width:50pt;'" ); // + "class='form-control-inline'  "

      stHtml.AppendLine ( "\r\n data-fieldid='" + PageField.FieldId + "' "
      + "data-min-value='" + stMinValue + "' "
      + "data-max-value='" + stMaxValue + "' "
      + "data-min-alert='" + stMinAlert + "' "
      + "data-max-alert='" + stMaxAlert + "' "
      + "data-min-norm='" + stMinNorm + "' "
      + "data-max-norm='" + stMaxNorm + "' "
      + "data-css-valid='" + stCssValid + "' "
      + "data-css-alert='" + stCssAlert + "' "
      + "data-css-norm='" + stCssNormal + "' "
      + stValidationMethod
      + "data-parsley-trigger=\"change\" " );

      if ( PageField.Mandatory == true && Status != Evado.Model.UniForm.EditAccess.Disabled )
      {
        stHtml.AppendLine ( " required " );
      }

      //this.addMandatoryIfAttribute ( stHtml, PageField );

      if ( Status == Evado.Model.UniForm.EditAccess.Disabled )
      {
        stHtml.AppendLine ( " disabled='disabled' " );
      }

      stHtml.AppendLine ( "/>" );

      this._TabIndex++;

      stHtml.AppendLine ( " - " );

      stHtml.AppendLine ( "<input type='text' "
        + "id='" + PageField.FieldId + DefaultPage.stField_UpperSuffix + "' "
        + "name='" + PageField.FieldId + DefaultPage.stField_UpperSuffix + "' "
        + "value='" + stUpperValue + "' "
        + "tabindex = '" + _TabIndex + "' "
        + "maxlength='" + stSize + "' "
        + "tabindex = '" + this._TabIndex + "' "
        + "size='" + stSize + "' "
        + "class='form-control-range'"
        + "style='width:50pt;'" );

      stHtml.AppendLine ( "\r\n data-fieldid='" + PageField.FieldId + "' "
      + "data-min-value='" + stMinValue + "' "
      + "data-max-value='" + stMaxValue + "' "
      + "data-min-alert='" + stMinAlert + "' "
      + "data-max-alert='" + stMaxAlert + "' "
      + "data-min-norm='" + stMinNorm + "' "
      + "data-max-norm='" + stMaxNorm + "' "
      + "data-css-valid='" + stCssValid + "' "
      + "data-css-alert='" + stCssAlert + "' "
      + "data-css-norm='" + stCssNormal + "' "
      + stValidationMethod
      + "data-parsley-trigger=\"change\" " );

      if ( PageField.Mandatory == true && Status != Evado.Model.UniForm.EditAccess.Disabled )
      {
        //stHtml.Append ( " required " );
      }

      //this.addMandatoryIfAttribute ( stHtml, PageField );

      if ( Status == Evado.Model.UniForm.EditAccess.Disabled )
      {
        stHtml.Append ( " disabled='disabled' " );
      }

      stHtml.AppendLine ( "/>" );
      stHtml.AppendLine ( "</div>" );
      stHtml.AppendLine ( stUnit );
      stHtml.Append ( "</div> \r\n" );

      this._TabIndex++;
      this._TabIndex++;

      //
      // Insert the field footer elemements
      //
      this.createFieldFooter ( stHtml, PageField );

    }//END Field Method

    // ===================================================================================
    /// <summary>
    /// This method creates a Name field html markup
    /// </summary>
    /// <param name="stHtml">StringBuilder object.</param>
    /// <param name="PageField">Field object.</param>
    /// <param name="Status">ClientFieldEditCodes enumerated status.</param>
    // ----------------------------------------------------------------------------------
    private void createDateRangeField (
      StringBuilder stHtml,
      Evado.Model.UniForm.Field PageField,
      Evado.Model.UniForm.EditAccess Status )
    {
      Global.LogClientMethod ( "createNumericRangeField method." );
      Global.LogDebug ( "Field.Type: " + PageField.Type );
      //
      // Initialise the methods variables and objects.
      //
      int valueColumnWidth = this._GroupValueColumWidth;
      int titleColumnWidth = 100 - valueColumnWidth;
      String stSize = "15"; ;
      String stFieldValueStyling = "style='' class='cell value cell-date-value' "; //
      String [ ] arrValue = PageField.Value.Split ( ';' );
      String stLowerValue = String.Empty;
      String stUpperValue = String.Empty;
      String value = String.Empty;

      String stMinValue = "-1000000";
      String stMaxValue = "1000000";
      String stMinAlert = "-1000000";
      String stMaxAlert = "1000000";
      String stMinNorm = "-1000000";
      String stMaxNorm = "1000000";
      String stCssValid = PageField.GetParameter ( Evado.Model.UniForm.FieldParameterList.BG_Validation );
      String stCssAlert = PageField.GetParameter ( Evado.Model.UniForm.FieldParameterList.BG_Alert );
      String stCssNormal = PageField.GetParameter ( Evado.Model.UniForm.FieldParameterList.BG_Normal );
      String stCustomValidation = PageField.GetParameter ( Evado.Model.UniForm.FieldParameterList.Validation_Callback );

      value = PageField.GetParameter ( Evado.Model.UniForm.FieldParameterList.Min_Value );
      if ( value != String.Empty )
      {
        stMinValue = value;
      }
      value = PageField.GetParameter ( Evado.Model.UniForm.FieldParameterList.Max_Value );
      if ( value != String.Empty )
      {
        stMaxValue = value;
      }
      value = PageField.GetParameter ( Evado.Model.UniForm.FieldParameterList.Min_Alert );
      if ( value != String.Empty )
      {
        stMinAlert = value;
      }
      value = PageField.GetParameter ( Evado.Model.UniForm.FieldParameterList.Max_Alert );
      if ( value != String.Empty )
      {
        stMaxAlert = value;
      }
      value = PageField.GetParameter ( Evado.Model.UniForm.FieldParameterList.Min_Normal );
      if ( value != String.Empty )
      {
        stMinNorm = value;
      }

      value = PageField.GetParameter ( Evado.Model.UniForm.FieldParameterList.Max_Normal );
      if ( value != String.Empty )
      {
        stMaxNorm = value;
      }

      if ( arrValue.Length < 2 )
      {
        stLowerValue = PageField.Value;
      }
      else
      {
        stLowerValue = arrValue [ 0 ];
        stUpperValue = arrValue [ 1 ];
      }

      //
      // Set the normal validation parameters.
      ///
      string stValidationMethod = " onchange=\"Evado.Form.onDateValidation( this, this.value )\" ";

      if ( stCustomValidation != String.Empty )
      {
        stValidationMethod = " onchange=\"Evado.Form.onCustomValidation('" + stCustomValidation + "', this, this.value )\" ";
      }

      //
      // Ineert the field header
      //
      this.createFieldHeader ( stHtml, PageField, titleColumnWidth, false );

      //
      // Insert the field elements
      //
      stHtml.AppendLine ( "<div " + stFieldValueStyling + " > " );
      stHtml.AppendLine ( "<span id='sp" + PageField.Id + "' "
        + " class='fform-field-container-inline' style='width:100%' >" );

      stHtml.Append ( "<input type='text' "
        + "id='" + PageField.FieldId + DefaultPage.stField_LowerSuffix + "' "
        + "name='" + PageField.FieldId + DefaultPage.stField_LowerSuffix + "' "
        + "tabindex = '" + _TabIndex + "' "
        + "value='" + stLowerValue + "' "
        + "maxlength='" + stSize + "' "
        + "tabindex = '" + this._TabIndex + "' "
        + "size='" + stSize + "' "
        + "class='form-control-range'  style='width:90pt;' " ); // + "class='form-control-inline'  "

      stHtml.Append ( "\r\n data-fieldid='" + PageField.FieldId + "' "
      + "data-min-value='" + stMinValue + "' "
      + "data-max-value='" + stMaxValue + "' "
      + "data-min-alert='" + stMinAlert + "' "
      + "data-max-alert='" + stMaxAlert + "' "
      + "data-min-norm='" + stMinNorm + "' "
      + "data-max-norm='" + stMaxNorm + "' "
      + "data-css-valid='" + stCssValid + "' "
      + "data-css-alert='" + stCssAlert + "' "
      + "data-css-norm='" + stCssNormal + "' "
      + stValidationMethod
      + " data-parsley-trigger=\"change\" " );

      if ( PageField.Mandatory == true && Status != Evado.Model.UniForm.EditAccess.Disabled )
      {
        //stHtml.Append ( " required " );
      }

      //this.addMandatoryIfAttribute ( stHtml, PageField );

      if ( Status == Evado.Model.UniForm.EditAccess.Disabled )
      {
        stHtml.Append ( " disabled='disabled' " );
      }

      stHtml.AppendLine ( "/>" );

      this._TabIndex++;

      stHtml.AppendLine ( " - " );

      stHtml.Append ( "<input type='text' "
        + "id='" + PageField.FieldId + DefaultPage.stField_UpperSuffix + "' "
        + "name='" + PageField.FieldId + DefaultPage.stField_UpperSuffix + "' "
        + "tabindex = '" + _TabIndex + "' "
        + "value='" + stUpperValue + "' "
        + "maxlength='" + stSize + "' "
        + "tabindex = '" + this._TabIndex + "' "
        + "size='" + stSize + "' class='form-control-range' style='width:90pt;'" ); // + "class='form-control-inline'  "

      stHtml.Append ( "\r\n data-fieldid='" + PageField.FieldId + "' "
      + " data-min-value='" + stMinValue + "' "
      + " data-max-value='" + stMaxValue + "' "
      + " data-min-alert='" + stMinAlert + "' "
      + " data-max-alert='" + stMaxAlert + "' "
      + " data-min-norm='" + stMinNorm + "' "
      + " data-max-norm='" + stMaxNorm + "' "
      + " data-css-valid='" + stCssValid + "' "
      + " data-css-alert='" + stCssAlert + "' "
      + " data-css-norm='" + stCssNormal + "' \r\n"
      + stValidationMethod
      + " data-parsley-trigger=\"change\" " );

      if ( PageField.Mandatory == true && Status != Evado.Model.UniForm.EditAccess.Disabled )
      {
        //stHtml.Append ( " required " );
      }

      //this.addMandatoryIfAttribute ( stHtml, PageField );

      if ( Status == Evado.Model.UniForm.EditAccess.Disabled )
      {
        stHtml.Append ( " disabled='disabled' " );
      }

      stHtml.AppendLine ( "/>" );
      stHtml.AppendLine ( "</span>" );
      stHtml.Append ( "</div> \r\n" );

      this._TabIndex++;
      this._TabIndex++;

      //
      // Insert the field footer elemements
      //
      this.createFieldFooter ( stHtml, PageField );

    }//END Field Method

    // ===================================================================================
    /// <summary>
    /// This method creates a text field html markup
    /// </summary>
    /// <param name="PageField">Field object.</param>
    /// <param name="TabIndex">THe field index on the page</param>
    /// <param name="EditAccess">ClientFieldEditCodes enumerated status.</param>
    /// <returns>String html</returns>
    // ----------------------------------------------------------------------------------
    private void createHttpLinkField (
      StringBuilder stHtml,
      Evado.Model.UniForm.Field PageField,
      Evado.Model.UniForm.EditAccess EditAccess )
    {
      Global.LogClientMethod ( "createHttpLinkField method. " );
      //
      // Initialise the methods variables and objects.
      //
      int valueColumnWidth = this._GroupValueColumWidth;
      int titleColumnWidth = 100 - valueColumnWidth;
      int stWidth = 50;
      int stHeight = 1;

      if ( PageField.hasParameter ( FieldParameterList.Field_Value_Column_Width ) == true )
      {
        Model.UniForm.FieldValueWidths widthValue = PageField.getValueColumnWidth ( );
        valueColumnWidth = (int) widthValue;
        titleColumnWidth = 100 - valueColumnWidth;
      }

      if ( PageField.hasParameter ( Evado.Model.UniForm.FieldParameterList.Width ) == true )
      {
        stWidth = PageField.GetParameterInt ( Evado.Model.UniForm.FieldParameterList.Width );
      }
      if ( PageField.hasParameter ( Evado.Model.UniForm.FieldParameterList.Height ) == true )
      {
        stHeight = PageField.GetParameterInt ( Evado.Model.UniForm.FieldParameterList.Height );
      }

      string stValue = PageField.Value;
      string stUrl = PageField.Value;
      string stLinkTitle = PageField.Title;

      if ( stValue.Contains ( ";" ) == true )
      {
        string [ ] arrValue = stValue.Split ( ';' );
        stUrl = arrValue [ 0 ];
        stLinkTitle = arrValue [ 1 ];
      }

      String stFieldValueStyling = "style='width:" + valueColumnWidth + "%' class='cell value cell-input-text-value cf' ";

      //
      // Ineert the field header
      //
      this.createFieldHeader ( stHtml, PageField, titleColumnWidth, false );


      stHtml.AppendLine ( "<div " + stFieldValueStyling + " > " );

      if ( stUrl != String.Empty )
      {
        //
        // If in display model display the http link.
        //
        stUrl = Global.concatinateHttpUrl ( Global.RelativeBinaryDownloadURL, stUrl );

        Global.LogClient ( "Final URL: " + stUrl );

        stHtml.AppendLine ( "<div " + stFieldValueStyling + " > " );

        stHtml.AppendLine ( "<span>" );
        stHtml.AppendLine ( "<strong>" );
        stHtml.AppendLine ( "<a href='" + stUrl + "' target='_blank' tabindex = '" + this._TabIndex + "' >" + stLinkTitle + "</a>" );
        stHtml.AppendLine ( "</strong>" );
        stHtml.AppendLine ( "</span>" );

        stHtml.AppendLine ( "</div>" );
      }

      //
      // If in edit mode display the data enty fields.
      //
      if ( EditAccess == Model.UniForm.EditAccess.Enabled )
      {
        //
        // Insert the field data control
        //
        stHtml.AppendLine ( "<table style='width:98%'><tr>" );

        stHtml.AppendLine ( "<td style='width:20%; text-align:right;'>" );
        stHtml.AppendLine ( EuLabels.Html_Url_Field_title );
        stHtml.AppendLine ( "</td>" );
        stHtml.AppendLine ( "<td>" );
        stHtml.AppendLine ( "<input type='text' "
           + "id='" + PageField.FieldId + "' "
           + "name='" + PageField.FieldId + Evado.Model.UniForm.Field.CONST_HTTP_URL_FIELD_SUFFIX + "' "
           + "value='" + PageField.Value + "' "
           + "tabindex = '" + _TabIndex + "' "
           + "maxlength='100' "
           + "size='100' "
           + "/>" );
        stHtml.AppendLine ( "</td></tr>" );

        stHtml.AppendLine ( "<tr><td style='width:20%; text-align:right;'>" );
        stHtml.AppendLine ( EuLabels.Html_Url_Title_Field_Title );
        stHtml.AppendLine ( "</td>" );
        stHtml.AppendLine ( "<td>" );
        stHtml.AppendLine ( "<input type='text' "
           + "id='" + PageField.FieldId + "' "
           + "name='" + PageField.FieldId + Evado.Model.UniForm.Field.CONST_HTTP_TITLE_FIELD_SUFFIX + "' "
           + "value='" + PageField.Value + "' "
           + "tabindex = '" + _TabIndex + "' "
           + "maxlength='100' "
           + "size='100' " );

        stHtml.AppendLine ( "/>" );

        stHtml.AppendLine ( "</td>" );
        stHtml.AppendLine ( "</tr></table>" );
      }


      stHtml.AppendLine ( "<input type='hidden' "
           + "id='" + PageField.FieldId + "' "
           + "name='" + PageField.FieldId + "' "
           + "value='" + PageField.Value + "' /> " );
      this._TabIndex += 2;

      //
      // Insert the field footer elemements
      //
      this.createFieldFooter ( stHtml, PageField );

      Global.LogClientMethodEnd ( "createHttpLinkField method. " );

    }//END Field Method

    // ===================================================================================
    /// <summary>
    /// This method creates a free test field html markup
    /// </summary>
    /// <param name="PageField">Field object.</param>
    /// <param name="TabIndex">THe field index on the page</param>
    /// <param name="EditAccess">ClientFieldEditCodes enumerated status.</param>
    /// <returns>String html</returns>
    // ----------------------------------------------------------------------------------
    private void createStreamedVideoField (
      StringBuilder stHtml,
      Evado.Model.UniForm.Field PageField,
      Evado.Model.UniForm.EditAccess EditAccess )
    {
      Global.LogClientMethod ( "createStreamedVideoField method. " );
      //
      // Initialise the methods variables and objects.
      //
      string value = PageField.Value.ToLower ( );
      int valueColumnWidth = this._GroupValueColumWidth;
      int titleColumnWidth = 100 - valueColumnWidth;
      int width = PageField.GetParameterInt ( Evado.Model.UniForm.FieldParameterList.Width );
      int height = PageField.GetParameterInt ( Evado.Model.UniForm.FieldParameterList.Height );
      String videoTitle = PageField.GetParameter ( Evado.Model.UniForm.FieldParameterList.Value_Label );
      String stFieldValueStyling = "style='width:" + valueColumnWidth + "%' class='cell cell-display-text-value cf' ";
      String stVideoStreamParameters = String.Empty;
      String stVideoSource = String.Empty;
      bool fullWidth = false;


      if ( PageField.Layout == FieldLayoutCodes.Column_Layout )
      {
        fullWidth = true;
        stFieldValueStyling = "style='width:98%' class='cell cell-display-text-value cf' ";
      }

      //
      // Ineert the field header
      //
      createFieldHeader ( stHtml, PageField, titleColumnWidth, fullWidth );


      stHtml.AppendLine ( "<div " + stFieldValueStyling + " >" );
      //
      // get the video iFrame.
      //
      stHtml.AppendLine ( this.getVideoIFrame ( PageField ) );

      //
      // the page is edit enabled display a field to collect the Video Url and title.
      //

      if ( EditAccess == Model.UniForm.EditAccess.Inherited
        || EditAccess == Evado.Model.UniForm.EditAccess.Enabled )
      {
        //
        // Insert the field data control
        //
        stHtml.AppendLine ( "<table style='width:98%'><tr>" );

        stHtml.AppendLine ( "<td style='width:20%; text-align:right;'>" );
        stHtml.AppendLine ( "<span>" + EuLabels.Video_Url_Field_Title + "</span>" );
        stHtml.AppendLine ( "</td>" );
        stHtml.AppendLine ( "<td>" );
        stHtml.AppendLine ( "<input type='text' "
           + "id='" + PageField.FieldId + "' "
           + "name='" + PageField.FieldId + "' "
           + "value='" + PageField.Value + "' "
           + "tabindex = '" + _TabIndex + "' "
           + "maxlength='100' "
           + "size='100' "
           + "/>" );
        stHtml.AppendLine ( "</td>" );
        stHtml.AppendLine ( "</tr></table>" );
      }
      else
      {
        stHtml.AppendLine ( "<input type='hidden' "
           + "id='" + PageField.FieldId + "' "
           + "name='" + PageField.FieldId + "' "
           + "value='" + PageField.Value + "' "
           + "/>" );
      }
      stHtml.AppendLine ( "</div>" );

      this._TabIndex += 2;

      //
      // Insert the field footer elemements
      //
      this.createFieldFooter ( stHtml, PageField );

    }//END createStreamedvideoField Method

    // ===================================================================================
    /// <summary>
    /// This method creates a iframe containing a streamed video object
    /// </summary>
    /// <param name="PageField">Field object.</param>
    /// <returns>String html</returns>
    // ----------------------------------------------------------------------------------
    private String getVideoIFrame (
      Evado.Model.UniForm.Field PageField )
    {
      Global.LogClientMethod ( "getVideoIFrame" );
      //
      // Initialise the methods variables and objects.
      //
      StringBuilder sbHtml = new StringBuilder ( );
      string value = PageField.Value.ToLower ( );
      int width = PageField.GetParameterInt ( Evado.Model.UniForm.FieldParameterList.Width );
      int height = PageField.GetParameterInt ( Evado.Model.UniForm.FieldParameterList.Height );
      String videoTitle = PageField.GetParameter ( Evado.Model.UniForm.FieldParameterList.Value_Label );
      String stVideoStreamParameters = String.Empty;
      String stVideoSource = String.Empty;

      if ( value == String.Empty )
      {
        return String.Empty;
      }

      //
      // Set default width
      //
      if ( width == 0
        && height == 0 )
      {
        width = 450;
        height = width * 10 / 17;
      }
      else
      {
        if ( height == 0 )
        {
          height = width * 10 / 17;
        }
        else
        {
          width = height * 17 / 10;
        }
      }


      if ( PageField.Value.Contains ( "vimeo.com" ) == true )
      {
        int index = PageField.Value.LastIndexOf ( '/' );
        value = PageField.Value.Substring ( ( index + 1 ) );

        stVideoSource = Global.VimeoEmbeddedUrl + value;

        stVideoStreamParameters = "frameborder=\"0\" webkitAllowFullScreen mozallowfullscreen allowFullScreen ";
      }

      if ( PageField.Value.Contains ( "youtube" ) == true
        || PageField.Value.Contains ( "youtu.be" ) == true )
      {
        int index = PageField.Value.LastIndexOf ( '/' );

        value = PageField.Value.Substring ( ( index + 1 ) );

        value = value.Replace ( "watch?v=", "" );

        stVideoSource = Global.YouTubeEmbeddedUrl + value;

        stVideoStreamParameters = "frameborder=\"0\" allow=\"accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture\" allowfullscreen";
      }

      Global.LogClient ( "Video ID: " + value );
      Global.LogClient ( "VideoSource: " + stVideoSource );

      sbHtml.AppendLine ( "<iframe "
        + "id='" + PageField.FieldId + DefaultPage.CONST_VIDEO_SUFFIX + "' "
        + "name='" + PageField.FieldId + DefaultPage.CONST_VIDEO_SUFFIX + "' "
        + "src='" + stVideoSource + "' " );

      if ( width > 0 )
      {
        sbHtml.AppendLine ( "width='" + width + "' " );
      }
      if ( height > 0 )
      {
        sbHtml.AppendLine ( "height='" + height + "' " );
      }

      sbHtml.AppendLine ( stVideoStreamParameters
       + " style=' display: block; margin-left: auto; margin-right: auto' >"
       + "</iframe>" );

      return sbHtml.ToString ( );
    }//END getVideoIFrame Method

    // ===================================================================================
    /// <summary>
    /// This method creates a free test field html markup
    /// </summary>
    /// <param name="PageField">Field object.</param>
    /// <param name="TabIndex">THe field index on the page</param>
    /// <param name="Status">ClientFieldEditCodes enumerated status.</param>
    /// <returns>String html</returns>
    // ----------------------------------------------------------------------------------
    private void createExternalImageField (
      StringBuilder sbHtml,
      Evado.Model.UniForm.Field PageField,
      Evado.Model.UniForm.EditAccess Status )
    {
      Global.LogClientMethod ( "createExternalImageField method. " );
      //
      // Initialise the methods variables and objects.
      //
      int valueColumnWidth = this._GroupValueColumWidth;
      int titleColumnWidth = 100 - valueColumnWidth;
      int width = PageField.GetParameterInt ( Evado.Model.UniForm.FieldParameterList.Width );
      int height = PageField.GetParameterInt ( Evado.Model.UniForm.FieldParameterList.Height );
      String stFieldValueStyling = "style='width:" + valueColumnWidth + "%' class='cell cell-display-text-value cf' ";
      String stVideoStreamParameters = String.Empty;
      bool fullWidth = false;

      if ( PageField.Layout == FieldLayoutCodes.Column_Layout )
      {
        fullWidth = true;
        stFieldValueStyling = "style='width:98%' class='cell cell-display-text-value cf' ";
      }

      PageField.Value = PageField.Value.ToLower ( );

      //
      // Set default width
      //
      if ( width == 0 )
      {
        width = 450;
      }
      if ( height == 0 )
      {
        height = width * 10 / 17;
      }

      //
      // Ineert the field header
      //
      createFieldHeader ( sbHtml, PageField, titleColumnWidth, fullWidth );

      //
      // Insert the field elements
      //
      sbHtml.AppendLine ( "<div " + stFieldValueStyling + " style='position: relative; ' > " );

      sbHtml.Append ( "<img  " );
      sbHtml.Append ( "id='" + PageField.FieldId + "' " );
      sbHtml.Append ( "name='" + PageField.FieldId + "' " );
      sbHtml.Append ( "src='" + PageField.Value + "' " );

      if ( width > 0 )
      {
        sbHtml.Append ( "width='" + width + "' " );
      }
      if ( height > 0 )
      {
        sbHtml.Append ( "height='" + height + "' " );
      }

      sbHtml.AppendLine ( " style=' display: block; margin-left: auto; margin-right: auto' />" );
      sbHtml.AppendLine ( "</div>" );

      this._TabIndex += 2;

      //
      // Insert the field footer elemements
      //
      this.createFieldFooter ( sbHtml, PageField );

    }//END createExternalImageField Method

    // ===================================================================================
    /// <summary>
    /// This method creates a chart plot field html markup
    /// </summary>
    /// <param name="PageField">Field object.</param>
    /// <param name="TabIndex">THe field index on the page</param>
    /// <returns>String html</returns>
    // ----------------------------------------------------------------------------------
    private void createPlotChartField (
      StringBuilder stHtml,
      Evado.Model.UniForm.Field PageField )
    {
      Global.LogClientMethod ( "createPlotChartField method. " );
      //
      // Initialise the methods variables and objects.
      //
      int valueColumnWidth = this._GroupValueColumWidth;
      int titleColumnWidth = 100 - valueColumnWidth;
      string placeHolder = PageField.FieldId.ToLower ( );
      String stWidth = PageField.GetParameter ( Evado.Model.UniForm.FieldParameterList.Width );

      String stFieldValueStyling = "style='width:" + valueColumnWidth + "%' class='cell value cell-textarea-value cf' ";

      //
      // Set default width
      //
      if ( stWidth == String.Empty )
      {
        stWidth = "650";
      }
      int width = Evado.Model.EvStatics.getInteger ( stWidth );
      int height = width / 2;
      switch ( PageField.Type )
      {
        case Evado.Model.EvDataTypes.Donut_Chart:
        case Evado.Model.EvDataTypes.Pie_Chart:
          {
            height = width;
            break;
          }
      }

      Global.LogDebug ( "Width: " + width + ", height: " + height );
      //
      // Ineert the field header
      //
      this.createFieldHeader ( stHtml, PageField, titleColumnWidth, false );

      //
      // Insert the field elements
      //
      stHtml.AppendLine ( "<div " + stFieldValueStyling + " > " );
      stHtml.AppendLine ( "<div id='sp" + PageField.Id + "'>" );
      String plotCode = this.generatePlotCode ( placeHolder, PageField );

      //
      // Chart sizing script
      //
      /*
      stHtml.AppendLine ( "<script type=\"text/javascript\">" );
      stHtml.AppendLine ( "$(function () {" );
      stHtml.AppendLine ( "var width = $(this).width()" );
      stHtml.AppendLine ( "alert( \"Width: \" + width) " );
      stHtml.AppendLine ( " });" );
      stHtml.AppendLine ( "</script>" );
      */

      stHtml.AppendLine ( plotCode );

      stHtml.AppendLine ( "<div class=\"plot-container\" style=\"width: " + width + "px; height: " + height + "px;\">" );
      stHtml.AppendLine ( "<div id=\"" + placeHolder + "\" class=\"plot-placeholder\"></div>" );
      stHtml.AppendLine ( "</div>" );
      /*
      stHtml.AppendLine ( "<textarea "
        + "id='" + placeHolder + "' "
        + "rows='5' "
        + "cols='80' "
        + "disabled='disabled' >" );
      stHtml.AppendLine ( PageField.Value );
      stHtml.AppendLine ( "</textarea>" );
      */
      stHtml.AppendLine ( "</div>" );
      stHtml.AppendLine ( "</div>" );

      this._TabIndex += 2;

      //
      // Insert the field footer elemements
      //
      this.createFieldFooter ( stHtml, PageField );

      Global.LogClientMethodEnd ( "createPlotChartField" );

    }//END createPlotChartField Method

    // ===================================================================================
    /// <summary>
    /// This method generates the plot code
    /// </summary>
    /// <param name="PageField">Field object.</param>
    /// <returns>String html</returns>
    // ----------------------------------------------------------------------------------
    private String generatePlotCode (
      String PlaceHolder,
      Evado.Model.UniForm.Field PageField )
    {
      Global.LogClientMethod ( "generatePlotCode method. " );
      StringBuilder code = new StringBuilder ( );
      string legend = String.Empty;

      Plot plotObject = Newtonsoft.Json.JsonConvert.DeserializeObject<Evado.Model.UniForm.Plot> ( PageField.Value );

      if ( plotObject.DisplayLegend == true )
      {
        string location = plotObject.LegendLocation.ToString ( );

        legend = "legend: {	position: \"" + location + "\",	show: true} ";
      }
      Global.LogDebug ( "Display Legend: " + plotObject.DisplayLegend + ", legend: " + legend );

      if ( this._PlotScriptLoaded == false )
      {
        code.AppendLine ( "<script language=\"javascript\" type=\"text/javascript\" src=\"./js/jquery.js\"></script>" );
        code.AppendLine ( "<script language=\"javascript\" type=\"text/javascript\" src=\"./js/jquery.canvaswrapper.js\"></script>" );
        code.AppendLine ( "<script language=\"javascript\" type=\"text/javascript\" src=\"./js/jquery.colorhelpers.js\"></script>" );
        code.AppendLine ( "<script language=\"javascript\" type=\"text/javascript\" src=\"./js/jquery.flot.js\"></script>" );
        code.AppendLine ( "<script language=\"javascript\" type=\"text/javascript\" src=\"./js/jquery.flot.saturated.js\"></script>" );
        code.AppendLine ( "<script language=\"javascript\" type=\"text/javascript\" src=\"./js/jquery.flot.browser.js\"></script>" );
        code.AppendLine ( "<script language=\"javascript\" type=\"text/javascript\" src=\"./js/jquery.flot.drawSeries.js\"></script>" );
        code.AppendLine ( "<script language=\"javascript\" type=\"text/javascript\" src=\"./js/jquery.flot.uiConstants.js\"></script>" );
        code.AppendLine ( "<script language=\"javascript\" type=\"text/javascript\" src=\"./js/jquery.flot.legend.js\"></script>" );
        code.AppendLine ( "<script language=\"javascript\" type=\"text/javascript\" src=\"./js/jquery.flot.pie.js\"></script> " );
        code.AppendLine ( "<script language=\"javascript\" type=\"text/javascript\" src=\"./js/jquery.flot.stack.js\"></script> " );
        this._PlotScriptLoaded = true;
      }

      code.AppendLine ( "<script type=\"text/javascript\">" );

      code.AppendLine ( "$(function () {" );
      code.AppendLine ( "var " + PlaceHolder + " = " + plotObject.GetData ( ) );

      switch ( PageField.Type )
      {
        case Evado.Model.EvDataTypes.Donut_Chart:
          {
            legend = "";

            code.Append ( "$.plot(\"#" + PlaceHolder + "\", " );
            code.Append ( PlaceHolder );
            code.AppendLine ( ", { " );
            code.AppendLine ( " series: { pie: { innerRadius: 0.5, show: true " );
            code.AppendLine ( " } } }" );
            code.AppendLine ( " ) " );
            break;
          }
        case Evado.Model.EvDataTypes.Pie_Chart:
          {
            legend = "";

            code.Append ( "$.plot(\"#" + PlaceHolder + "\", " );
            code.Append ( PlaceHolder );
            code.AppendLine ( ", { " );
            code.AppendLine ( " series: { pie: { show: true " );
            code.AppendLine ( " } } }" );
            code.AppendLine ( " ) " );

            break;
          }
        case Evado.Model.EvDataTypes.Bar_Chart:
          {
            code.Append ( "$.plot(\"#" + PlaceHolder + "\", " );
            code.AppendLine ( PlaceHolder );
            code.AppendLine ( ", { " );
            if ( legend != String.Empty )
            {
              code.Append ( legend + ", " );
            }
            if ( plotObject.X_Axis != String.Empty )
            {
              code.AppendLine ( plotObject.X_Axis );
            }
            else
            {
              code.AppendLine ( " var axes = plot.getXAxes ( ).concat ( plot.getYAxes ( ) ); " );
              code.AppendLine ( "axes.forEach(function(axis) {" );
              code.AppendLine ( "axis.options.showTicks = true; " );
              code.AppendLine ( "axis.options.showMinorTicks = false; " );
              code.AppendLine ( "}" );
            }
            code.AppendLine ( " }" );
            code.AppendLine ( " ) " );
            break;
          }
        case Evado.Model.EvDataTypes.Stacked_Bar_Chart:
          {
            code.AppendLine ( "	var stack = 0,	bars = true,	lines = false,	steps = false;" );

            code.Append ( "\t$.plot(\"#" + PlaceHolder + "\", " + PlaceHolder );
            code.AppendLine ( ", { " );
            if ( legend != String.Empty )
            {
              code.Append ( legend + ", " );
            }
            code.AppendLine ( "series: { stack: stack, lines: { show: lines, fill: true, steps: steps	},	bars: { show: bars,	barWidth: 0.6} }" );

            if ( plotObject.X_Axis != String.Empty )
            {
              code.AppendLine ( ", " + plotObject.X_Axis );
            }

            code.AppendLine ( "} ) " );
            break;
          }
        case Evado.Model.EvDataTypes.Line_Chart:
        default:
          {
            code.Append ( "\t$.plot(\"#" + PlaceHolder + "\", " );
            code.AppendLine ( PlaceHolder );
            if ( legend != String.Empty )
            {
              code.AppendLine ( ", { " );
              code.Append ( legend );
              code.AppendLine ( " }" );
            }
            code.AppendLine ( " ) " );
            break;
          }
      }


      code.AppendLine ( "function labelFormatter(label, series) { " );
      code.Append ( "return \"<div style='font-size:10pt; text-align:center; padding:2px; color:white;'>\"" );
      code.AppendLine ( " + label + \"<br/>\" + Math.round(series.percent) + \"%</div>\"; }" );


      code.AppendLine ( " });" );
      code.AppendLine ( "</script>" );

      Global.LogDebug ( code.ToString ( ) );
      Global.LogClientMethodEnd ( "generatePlotCode" );

      return code.ToString ( );
    }

  }

}
