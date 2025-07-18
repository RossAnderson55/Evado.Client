using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

///Evado. namespace references.

using Evado.UniForm.Web;
using Evado.UniForm.Model;
using Evado.Model;
using System.Runtime.Remoting.Messaging;
using System.Data.Common;
using Newtonsoft.Json.Linq;
using System.Web.UI.WebControls;
using Microsoft.SqlServer.Server;
using System.IO;
using System.Drawing;

namespace Evado.UniForm.AdminClient
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
    /// <param name="sbHtml">StringBuilder containing the page html.</param>
    /// <param name="PageField">Evado.UniForm.Model.EuField object</param>
    //-----------------------------------------------------------------------------------
    private void addMandatoryIfAttribute(
      StringBuilder sbHtml,
      Evado.UniForm.Model.EuField PageField )
    {
      this.LogMethod ( "addMandatoryIfAttribute" );
      //
      // initialise method variables and objects.
      //
      string stMandatoryIfFieldId = PageField.GetParameter ( Evado.UniForm.Model.EuFieldParameters.Mandatory_If_Field_Id );
      string stMandatoryIfFieldValue = PageField.GetParameter ( Evado.UniForm.Model.EuFieldParameters.Mandatory_If_Value );

      if ( stMandatoryIfFieldId.Length > 0 )
      {
        sbHtml.Append ( " data-mandatory-if-field-id=\"" + stMandatoryIfFieldId + "\"" );
      }

      if ( stMandatoryIfFieldValue.Length > 0 )
      {
        sbHtml.Append ( " data-mandatory-if-field-value=\"" + stMandatoryIfFieldValue + "\"" );
      }
    }

    //=================================================================================
    /// <summary>
    /// This method sets the fields background colour.
    /// </summary>
    /// <param name="PageField">Evado.UniForm.Model.EuField: object</param>
    /// <returns>String: css colour class name</returns>
    //-----------------------------------------------------------------------------------
    private string fieldBackgroundColorClass(
      Evado.UniForm.Model.EuField PageField )
    {
      this.LogMethod ( "fieldBackgroundColorClass" );
      //
      // initialise method variables and objects.
      //
      string cssBackgroundColorClass = "";

      Evado.UniForm.Model.EuBackgroundColours background = Evado.UniForm.Model.EuBackgroundColours.Default;

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
        this.LogDebug ( "Field is mandatory." );

        background = PageField.getMandatoryBackGroundColor ( background );
      }

      this.LogDebug ( "background: " + background );

      switch ( background )
      {
        case Evado.UniForm.Model.EuBackgroundColours.Red:
        {
          cssBackgroundColorClass = "Red";
          break;
        }
        case Evado.UniForm.Model.EuBackgroundColours.Dark_Red:
        {
          cssBackgroundColorClass = "Dark_Red";
          break;
        }
        case Evado.UniForm.Model.EuBackgroundColours.Orange:
        {
          cssBackgroundColorClass = "Orange";
          break;
        }
        case Evado.UniForm.Model.EuBackgroundColours.Yellow:
        {
          cssBackgroundColorClass = "Yellow";
          break;
        }
        case Evado.UniForm.Model.EuBackgroundColours.Green:
        {
          cssBackgroundColorClass = "Green";
          break;
        }
        case Evado.UniForm.Model.EuBackgroundColours.Blue:
        {
          cssBackgroundColorClass = "Blue";
          break;
        }
        case Evado.UniForm.Model.EuBackgroundColours.Purple:
        {
          cssBackgroundColorClass = "Purple";
          break;
        }
      }
      this.LogDebug ( "cssBackgroundColorClass: " + cssBackgroundColorClass );
      this.LogMethodEnd ( "fieldBackgroundColorClass" );

      return cssBackgroundColorClass;
    }

    // ===================================================================================
    /// <summary>
    /// This method creates a read only field markup
    /// </summary>
    /// <param name="sbHtml">StringBuilder object containing the page html.</param>
    /// <param name="TabIndex">int: the current tab index.</param>
    /// <param name="PageField">Field object.</param>
    // ----------------------------------------------------------------------------------
    private void createFieldHeader(
      StringBuilder sbHtml,
      Evado.UniForm.Model.EuField PageField,
      int TitleWidth,
      bool TitleFullWidth )
    {
      this.LogMethod ( "createFieldHeader" );
      this.LogDebug ( "PageField.FieldId:{0}.", PageField.FieldId );
      this.LogDebug ( "PageField.Title: {0}.", PageField.Title );
      this.LogDebug ( "PageField.Type: {0}.", PageField.Type );
      this.LogDebug ( "CurrentGroupType:{0}.", this.UserSession.CurrentGroup.GroupType );
      //this.LogDebug ( "ImagesUrl: {0}.", Global.StaticImageUrl );
      //
      // initialise method variables and objects.
      //
      String stLayout = String.Empty;
      String stFieldRowStyling = String.Empty;
      String stFieldTitleStyling = String.Empty;
      String stField_Suffix = String.Empty;
      String stDescription = String.Empty;
      String stAnnotation = PageField.GetParameter ( Evado.UniForm.Model.EuFieldParameters.Annotation );

      stFieldRowStyling = "class='group-row field " + stLayout + " cf " + this.fieldBackgroundColorClass ( PageField ) + "' ";
      stFieldTitleStyling = "style='width:" + TitleWidth + "%; ' class='cell title cell-display-text-title'";

      //
      // Format the description value from mark down to html.
      //
      if ( String.IsNullOrEmpty ( PageField.Description ) == false )
      {
        //this.LogDebug ( "JSON: PageField.Description : {0}.", PageField.Description );

        stDescription = Evado.Model.EvStatics.EncodeMarkDown ( PageField.Description );

        if ( stDescription.Contains ( "/]" ) == true )
        {
          stDescription = stDescription.Replace ( "{images}", Global.StaticImageUrl );
          stDescription = stDescription.Replace ( "[", "<" );
          stDescription = stDescription.Replace ( "]", ">" );
        }
      }
      //this.LogDebug ( "HTML: stDescription: {0}.", stDescription );

      //
      // Format the description value from mark down to html.
      //
      if ( stAnnotation != String.Empty )
      {
        stAnnotation = Evado.Model.EvStatics.EncodeMarkDown ( stAnnotation );
      }
      this.LogDebug ( "stDescription: {0}.", stDescription );


      if ( PageField.Layout == EuFieldLayoutCodes.Column_Layout )
      {
        stFieldTitleStyling = "style='width:100%; ' class='cell title cell-display-text-title'";
      }

      //
      // Set the field layout style classes.
      //
      switch ( PageField.Layout )
      {
        case Evado.UniForm.Model.EuFieldLayoutCodes.Default:
        {
          stLayout = "layout-normal";
          break;
        }
        case Evado.UniForm.Model.EuFieldLayoutCodes.Center_Justified:
        {
          stLayout = "layout-center-justified";
          break;
        }
        case Evado.UniForm.Model.EuFieldLayoutCodes.Column_Layout:
        {
          stLayout = "layout-column";
          stFieldTitleStyling = "style='width: 98%; ' class='cell title cell-display-text-title'";
          break;
        }
        case Evado.UniForm.Model.EuFieldLayoutCodes.Left_Justified:
        {
          stLayout = "layout-left-justified";
          break;
        }
      }


      if ( TitleFullWidth == true
        || PageField.Type == Evado.Model.EvDataTypes.Table
        || PageField.Type == Evado.Model.EvDataTypes.Special_Matrix )
      {
        stFieldTitleStyling = "style='width: 98%; ' class='cell title cell-display-text-title'";

        if ( PageField.Layout != Evado.UniForm.Model.EuFieldLayoutCodes.Column_Layout )
        {
          stFieldTitleStyling = "style='width:" + TitleWidth + "%; ' class='cell title cell-display-text-title'";
        }
      }

      // always use column layout for tables
      if ( PageField.Type == Evado.Model.EvDataTypes.Table )
      {
        stLayout = "layout-column";
      }

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
          stField_Suffix = EuField.CONST_IMAGE_FIELD_SUFFIX; break;
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

      sbHtml.AppendLine ( "<!-- -------------------------------------------------------------------------\r\n"
        + "        FIELD HEADER = " + PageField.FieldId + " DATA TYPE = " + PageField.Type +
        " LAYOUT = " + PageField.Layout + "     \r\n -->" );

      sbHtml.AppendLine ( "<div id='" + PageField.Id + "-row' " + stFieldRowStyling + " >" );

      this.LogDebug ( "Title: " + PageField.Title );
      //
      // Error message
      //
      this.LogDebug ( "Formattted title: " + PageField.Title );

      sbHtml.AppendLine ( "<div " + stFieldTitleStyling + "> " );

      if ( PageField.Title != String.Empty )
      {
        sbHtml.AppendLine ( "<label>" + PageField.Title );

        if ( PageField.Mandatory == true && PageField.EditAccess != false )
        {
          sbHtml.Append ( "<span class='required'> * </span>" );
        }

        sbHtml.Append ( "</label>\r\n " );

        if ( PageField.IsEnabled == true )
        {
          sbHtml.AppendLine ( "<div class='error-container ' style='display: none'>" ); // style='display: none'
          sbHtml.AppendLine ( "<div id='" + PageField.Id + "-err-row' class='cell cell-error-value'>" );
          sbHtml.AppendLine ( "<span id='sp" + PageField.Id + "-err'></span>" );
          sbHtml.AppendLine ( "</div></div>\r\n" );
        }
      }

      if ( stDescription != String.Empty )
      {
        sbHtml.AppendLine ( "<div class='description'>" + stDescription + "</div>" );
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
        if ( this.UserSession.CurrentGroup.GroupType != Evado.UniForm.Model.EuGroupTypes.Annotated_Fields
          && this.UserSession.CurrentGroup.GroupType != Evado.UniForm.Model.EuGroupTypes.Review_Fields
          && stAnnotation != String.Empty )
        {
          sbHtml.Append ( "<div class='description'>" + stAnnotation + "</div>" );
        }

        //
        // Display the annotation field
        //
        else if ( this.UserSession.CurrentGroup.GroupType == Evado.UniForm.Model.EuGroupTypes.Annotated_Fields )
        {
          sbHtml.Append ( "<div class='description'>" + stAnnotation
           + "<input type='text' "
           + "id='" + PageField.FieldId + Evado.UniForm.Model.EuField.CONST_FIELD_ANNOTATION_SUFFIX + "' "
           + "name='" + PageField.FieldId + Evado.UniForm.Model.EuField.CONST_FIELD_ANNOTATION_SUFFIX + "' "
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
        else if ( this.UserSession.CurrentGroup.GroupType == Evado.UniForm.Model.EuGroupTypes.Review_Fields )
        {
          sbHtml.Append ( "<div class='description'>" + stAnnotation
           + "<br/> Query: "
           + "<input type='checkbox' "
           + "id='" + PageField.FieldId + Evado.UniForm.Model.EuField.CONST_FIELD_QUERY_SUFFIX + "' "
           + "name='" + PageField.FieldId + Evado.UniForm.Model.EuField.CONST_FIELD_QUERY_SUFFIX + "' "
           + "tabindex = '" + this._TabIndex + "' />\r\n" );

          this._TabIndex++;

          sbHtml.Append ( "<input type='text' "
         + "id='" + PageField.FieldId + Evado.UniForm.Model.EuField.CONST_FIELD_ANNOTATION_SUFFIX + "' "
         + "name='" + PageField.FieldId + Evado.UniForm.Model.EuField.CONST_FIELD_ANNOTATION_SUFFIX + "' "
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
      sbHtml.Append ( "</div>" );

      this.LogMethodEnd ( "createFieldHeader" );

    }//END createFieldHeader method

    // ===================================================================================
    /// <summary>
    /// This method creates a read only field markup
    /// </summary>
    /// <param name="PageField">Field object.</param>
    /// <returns>String html</returns>
    // ----------------------------------------------------------------------------------
    private void createFieldFooter(
      StringBuilder sbHtml,
      Evado.UniForm.Model.EuField PageField )
    {
      sbHtml.Append ( "</div>" );

      sbHtml.AppendLine ( "<!--      FIELD FOOTER = " + PageField.FieldId + " DATA TYPE = " + PageField.Type +
        "     \r\n------------------------------------------------------------------------- -->" );
    }

    // ===================================================================================
    /// <summary>
    /// This method creates a read only field markup
    /// </summary>
    /// <param name="sbHtml">StringBuilder object.</param>
    /// <param name="PageField">Field object.</param>
    // ----------------------------------------------------------------------------------
    private void createReadOnlyField( StringBuilder sbHtml, EuField PageField )
    {
      this.LogMethod ( "createReadOnlyField" );
      //
      // Initialise the methods variables and objects.
      //
      int valueColumnWidth = this.UserSession.GroupFieldWidth;
      int titleColumnWidth = 100 - valueColumnWidth;
      int stWidth = 20;
      int stHeight = 1;

      if ( PageField.hasParameter ( EuFieldParameters.Field_Value_Column_Width ) == true )
      {
        Evado.UniForm.Model.EuFieldValueWidths widthValue = PageField.ValueColumnWidth;
        valueColumnWidth = ( int ) widthValue;
        titleColumnWidth = 100 - valueColumnWidth;
      }

      if ( PageField.hasParameter ( Evado.UniForm.Model.EuFieldParameters.Width ) == true )
      {
        stWidth = PageField.GetParameterInt ( Evado.UniForm.Model.EuFieldParameters.Width );
      }
      if ( PageField.hasParameter ( Evado.UniForm.Model.EuFieldParameters.Height ) == true )
      {
        stHeight = PageField.GetParameterInt ( Evado.UniForm.Model.EuFieldParameters.Height );
      }

      String stFieldValueStyling = String.Format( "style='width:{0}%; padding-top:10px; ' class='cell value cell-display-text-title' ", valueColumnWidth );
      bool fullWidth = false;

      if ( PageField.Layout == EuFieldLayoutCodes.Column_Layout )
      {
        fullWidth = true;
        stFieldValueStyling = "style='width:100%' class='cell cell-display-text-value cf' ";
      }

      //
      // Ineert the field header
      //
      this.createFieldHeader ( sbHtml, PageField, titleColumnWidth, fullWidth );

      String value = PageField.Value;
      //
      // Encode the readlonly text value.
      //
      if ( value != String.Empty )
      {
        sbHtml.AppendLine ( "<div " + stFieldValueStyling + " > " );
        //
        // process html content.
        //
        if ( value.Contains ( "[/" ) == true )
        {
          this.LogDebug ( "No HTML Markup processing" );

          this.LogDebug ( "HTML: encoded value: " + value );

          String html = this.decodeHtmlText ( value );

          this.LogDebug ( "HTML: decoded value" + html );
          sbHtml.AppendLine ( html );
        }
        else
        {
          this.LogDebug ( "Processing markup" );
          String html = Evado.Model.EvStatics.EncodeMarkDown ( value );
          this.LogDebug ( "HTML: decoded value" + html );

          sbHtml.AppendLine ( html );
        }
        sbHtml.AppendLine ( "</div> " );
      }

      //
      // Insert the field footer elemements
      //
      this.createFieldFooter ( sbHtml, PageField );

      this.LogMethodEnd ( "createReadOnlyField" );
    }

    // =====================================================================================
    /// <summary>
    ///   This method decodes a encoded html string into a html marked up string.
    ///   Where "[" = "less than" = "]" = "greater than".
    /// </summary>
    /// <param name="EncodedHtmlString">string: (Mandatory) encoded html string.</param>
    /// <returns>String: Html markup as text.</returns>
    /// <remarks>
    /// This method consists of the following step:
    /// 
    /// 1. Remplace a "[" character with a "less than" character
    /// 
    /// 2. Remplace a "]" character with a "greater than" character
    /// </remarks>
    // -------------------------------------------------------------------------------------
    public String decodeHtmlText( string EncodedHtmlString )
    {
      EncodedHtmlString = EncodedHtmlString.Replace ( "[CR]", "<br/>" );
      EncodedHtmlString = EncodedHtmlString.Replace ( "[[CR]]", "<br/>" );
      EncodedHtmlString = EncodedHtmlString.Replace ( "[[", "<" );
      EncodedHtmlString = EncodedHtmlString.Replace ( "]]", ">" );
      EncodedHtmlString = EncodedHtmlString.Replace ( "[", "<" );
      EncodedHtmlString = EncodedHtmlString.Replace ( "]", ">" );
      return EncodedHtmlString;

    }//END decodeHtmlText method

    // =====================================================================================
    /// <summary>
    ///   This method encodes a html markup test as encoded version.
    ///   Where "[" = "less than" = "]" = "greater than".
    /// </summary>
    /// <param name="HtmlMarkupText">(Mandatory) Html marked up text.</param>
    /// <returns>encoded html markup</returns>
    /// <remarks>
    /// This method consists of the following step:
    /// 
    /// 1. Remplace a "less than" character with a "[" character
    /// 
    /// 2. Remplace a "greater than" character with a "]" character
    /// </remarks>
    // -------------------------------------------------------------------------------------
    public String encodeHtmlText( string HtmlMarkupText )
    {
      HtmlMarkupText = HtmlMarkupText.Replace ( "\r\n", "[CR]" );
      HtmlMarkupText = HtmlMarkupText.Replace ( "<", "[" );
      HtmlMarkupText = HtmlMarkupText.Replace ( ">", "]" );
      return HtmlMarkupText;

    }//END encodeHtmlText method

    // ===================================================================================
    /// <summary>
    /// This method creates a read only field markup
    /// </summary>
    /// <param name="sbHtml">StringBuilder object.</param>
    /// <param name="PageField">Field object.</param>
    // ----------------------------------------------------------------------------------
    private void createHtmlField( StringBuilder sbHtml, EuField PageField )
    {
      this.LogMethod ( "createHtmlField" );
      //
      // Initialise the methods variables and objects.
      //
      int valueColumnWidth = this.UserSession.GroupFieldWidth;
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
          this.LogValue ( "No Markup processing" );

          this.LogDebug ( "HTML: encoded value: " + PageField.Value );

          String html = this.decodeHtmlText ( PageField.Value );

          this.LogDebug ( "HTML: decoded value" + html );
          sbHtml.AppendLine ( "<div " + stFieldValueStyling + " > " );
          sbHtml.AppendLine ( "<div class='description'>" + html + "</div>" );
          sbHtml.AppendLine ( "</div>" );
        }
        else
        {
          String value = PageField.Value;
          value = Evado.Model.EvStatics.EncodeMarkDown ( value );
          value = value.Replace ( "\r\n", "<br/>" );

          sbHtml.AppendLine ( "<div " + stFieldValueStyling + " > " );
          sbHtml.AppendLine ( "<div class='description'>" + value + "</div>" );
          sbHtml.AppendLine ( "</div>" );
        }
      }

    }//END method

    // ===================================================================================
    /// <summary>
    /// This method creates a test field html markup
    /// </summary>
    /// <param name="sbHtml">StringBuilder object.</param>
    /// <param name="PageField">Field object.</param>
    // ----------------------------------------------------------------------------------
    private void createImageField( StringBuilder sbHtml, EuField PageField )
    {
      this.LogMethod ( "createImageField" );
      this.LogDebug ( "TempUrl: " + Global.TempUrl );
      this.LogDebug ( "PageField.FieldId: " + PageField.FieldId );
      this.LogDebug ( "PageField.Layout: " + PageField.Layout );
      this.LogDebug ( "PageField.Value: " + PageField.Value );
      //
      // Initialise the methods variables and objects.
      //
      int valueColumnWidth = this.UserSession.GroupFieldWidth;
      int titleColumnWidth = 100 - valueColumnWidth;
      string stImageUrl = PageField.Value;
      int iWidth = 400;
      this.TestFileUpload.Visible = true;
      bool fullWidth = false;

      if ( PageField.hasParameter ( Evado.UniForm.Model.EuFieldParameters.Width ) == true )
      {
        iWidth = PageField.GetParameterInt ( Evado.UniForm.Model.EuFieldParameters.Width );
      }

      // 
      // If the url does not include a http statement add the default image url 
      // 
      if ( PageField.hasParameter ( EuFieldParameters.Image_Url_Assignment ) == true )
      {
        stImageUrl = Global.concatinateHttpUrl ( Global.StaticImageUrl, stImageUrl );
      }
      else
      {
        stImageUrl = Global.concatinateHttpUrl ( Global.TempUrl, stImageUrl );
      }
      this.LogDebug ( "stImageUrl:{0}.", stImageUrl );

      string format = PageField.GetParameter ( EuFieldParameters.Format ).ToLower ( );
      this.LogDebug ( "format: {0}.", format );

      string title = PageField.GetParameter ( EuFieldParameters.Image_Title ).ToLower ( );
      this.LogDebug ( "title: {0}.", title );

      String stFieldValueStyling = "style='width:" + valueColumnWidth + "%' class='cell value cell-image-value cf' "; // class='cell value cell-image-value cf' ";

      if ( PageField.Layout == EuFieldLayoutCodes.Column_Layout )
      {
        fullWidth = true;
        stFieldValueStyling = "style='width:100%' class='cell cell-image-value cf' ";
      }
      //cell-input-text-value
      // Ineert the field header
      //
      this.createFieldHeader ( sbHtml, PageField, titleColumnWidth, fullWidth );

      sbHtml.AppendLine ( "<div " + stFieldValueStyling + " >" );
      sbHtml.AppendLine ( "<div id='sp" + PageField.Id + "' >" );
      //
      // Insert the field elements
      //
      if ( PageField.Value != String.Empty )
      {
        this.LogValue ( "Image file exists " + PageField.Value );

        sbHtml.AppendFormat ( "<a href='{0}' target='_blank' > \r\n", stImageUrl );

        sbHtml.AppendFormat ( "<img alt='Image {1}' src='{0}' width='{2}'/></a>",
          stImageUrl, PageField.Value, iWidth );
      }

      if ( PageField.EditAccess == true )
      {
        sbHtml.AppendFormat ( "<input name='{0}{1}' id='{0}{1}' type='file' size='80' /> \r\n",
          PageField.FieldId, EuField.CONST_IMAGE_FIELD_SUFFIX );

        if ( format.Contains ( "title" ) == true )
        {
          sbHtml.AppendFormat ( "{4}<input type='text' id='{0}{1}' name='{0}{1}' value='{2}' "
            + " tabindex = '{3}' maxlength='100' size='100' class='form-control' />\r\n",
            PageField.FieldId, EuField.CONST_IMAGE_FIELD_TITLE_SUFFIX, title,
            this._TabIndex, EuLabels.Image_Title_Field_Title );
        }

        if ( format.Contains ( "delete" ) == true )
        {
          sbHtml.AppendFormat ( "{3}<input "
           + "type='checkbox' "
           + "id='{0}{1}' "
           + "name='{0}{1}' "
           + "tabindex = '{2}'"
           + "value=\"Yes\" />\r\n",
           PageField.FieldId, EuField.CONST_IMAGE_FIELD_DELETE_SUFFIX,
           this._TabIndex, EuLabels.Image_Delete_Field_Title );
        }
      }

      sbHtml.AppendLine ( "<input type='hidden' "
           + "id='" + PageField.FieldId + "' "
           + "name='" + PageField.FieldId + "' "
           + "value='" + PageField.Value + "' /> " );
      sbHtml.AppendLine ( "</div>" );
      sbHtml.AppendLine ( "</div>" );

      //
      // Insert the field footer
      //
      this.createFieldFooter ( sbHtml, PageField );

      this.LogValue ( "END createImageField\r\n" );
    }//END Field Method

    // ===================================================================================
    /// <summary>
    /// This method creates a text field html markup
    /// </summary>
    /// <param name="sbHtml">StringBuilder object.</param>
    /// <param name="PageField">Field object.</param>
    // ----------------------------------------------------------------------------------
    private void createTextField( StringBuilder sbHtml, EuField PageField )
    {
      this.LogMethod ( "createTextField" );
      //
      // Initialise the methods variables and objects.
      //
      int valueColumnWidth = this.UserSession.GroupFieldWidth;
      int titleColumnWidth = 100 - valueColumnWidth;
      int maxLength = 20;

      if ( PageField.hasParameter ( Evado.UniForm.Model.EuFieldParameters.Width ) == true )
      {
        maxLength = PageField.GetParameterInt ( Evado.UniForm.Model.EuFieldParameters.Width );
      }

      //
      // set the min and max lenght if not set.
      //
      if ( maxLength == 0 )
      {
        maxLength = 20;
      }

      int size = maxLength;
      if ( size > 80 )
      {
        size = 80;
      }

      //
      // Set the normal validation parameters.
      //
      string stValidationMethod = " onchange=\"Evado.Form.onTextChange( this, this.value )\" ";

      if ( PageField.hasParameter ( EuFieldParameters.Field_Value_Column_Width ) == true )
      {
        Evado.UniForm.Model.EuFieldValueWidths widthValue = PageField.ValueColumnWidth;
        valueColumnWidth = ( int ) widthValue;
        titleColumnWidth = 100 - valueColumnWidth;
      }
      String stFieldValueStyling = "style='width:" + valueColumnWidth + "%' class='cell value cell-input-text-value cf' ";

      if ( PageField.Layout == EuFieldLayoutCodes.Column_Layout )
      {
        stFieldValueStyling = "style='width:100%' class='cell value cell-input-text-value cf' ";
      }

      //
      // Incert the field header
      //
      this.createFieldHeader ( sbHtml, PageField, titleColumnWidth, false );

      //
      // Insert the field data control
      //
      sbHtml.AppendLine ( "<div " + stFieldValueStyling + " > " );
      sbHtml.AppendLine ( "<div id='sp" + PageField.Id + "'  >" );
      sbHtml.AppendLine ( "<input type='text' "
        + "id='" + PageField.FieldId + "' "
        + "name='" + PageField.FieldId + "' "
        + "value='" + PageField.Value + "' "
        + "tabindex = '" + _TabIndex + "' "
        + "maxlength='" + maxLength + "' "
        + "size='" + size + "' "
        + "data-fieldid='" + PageField.FieldId + "' "
        + "class='form-control'  "
        + stValidationMethod + " data-parsley-trigger=\"change\" " );

      if ( PageField.Mandatory == true
        && PageField.EditAccess != false )
      {
        //sbHtml.Append ( " required " );
      }

      //this.addMandatoryIfAttribute ( sbHtml, PageField );

      if ( PageField.EditAccess == false )
      {
        sbHtml.Append ( " disabled='disabled' " );
      }

      sbHtml.AppendLine ( "/>" );
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
    /// This method creates a text field html markup
    /// </summary>
    /// <param name="sbHtml">StringBuilder object.</param>
    /// <param name="PageField">Field object.</param>
    // ----------------------------------------------------------------------------------
    private void createComputedField( StringBuilder sbHtml, EuField PageField )
    {
      this.LogMethod ( "createComputedField" );
      //
      // Initialise the methods variables and objects.
      //
      int valueColumnWidth = this.UserSession.GroupFieldWidth;
      int titleColumnWidth = 100 - valueColumnWidth;
      String stWidth = PageField.GetParameter ( Evado.UniForm.Model.EuFieldParameters.Width );
      String stRows = PageField.GetParameter ( Evado.UniForm.Model.EuFieldParameters.Height );
      String stFieldValueStyling = "style='width:" + valueColumnWidth + "%' class='cell value cell-input-text-value cf' ";
      PageField.EditAccess = false;
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
      this.createFieldHeader ( sbHtml, PageField, titleColumnWidth, false );

      //
      // Insert the field data control
      //
      sbHtml.AppendLine ( "<div " + stFieldValueStyling + " >" );
      sbHtml.AppendLine ( "<span id='sp" + PageField.Id + "'>" );
      sbHtml.AppendLine ( "<input type='text' "
         + "id='" + PageField.FieldId + "' "
         + "name='" + PageField.FieldId + "' "
         + "value='" + PageField.Value + "' "
         + "tabindex = '" + _TabIndex + "' "
         + "maxlength='" + stWidth + "' "
         + "size='" + stWidth + "' "
         + "class='form-control' " );

      if ( PageField.EditAccess == false )
      {
        sbHtml.Append ( " readonly='readonly' " );
      }

      sbHtml.AppendLine ( "/>" );
      sbHtml.AppendLine ( "</span>" );
      sbHtml.AppendLine ( "</div>" );

      this._TabIndex += 2;

      //
      // Insert the field footer elemements
      //
      this.createFieldFooter ( sbHtml, PageField );

    }//END Field Method

    // ===================================================================================
    /// <summary>
    /// This method creates a free test field html markup
    /// </summary>
    /// <param name="sbHtml">StringBuilder object.</param>
    /// <param name="PageField">Field object.</param>
    // ----------------------------------------------------------------------------------
    private void createFreeTextField( StringBuilder sbHtml, EuField PageField )
    {
      this.LogMethod ( "createFreeTextField" );
      //
      // Initialise the methods variables and objects.
      //
      String fieldMarginStyle = String.Empty;
      int valueColumnWidth = this.UserSession.GroupFieldWidth;
      int titleColumnWidth = 100 - valueColumnWidth;
      String stFieldValueStyling = "style='width:" + valueColumnWidth + "%' class='cell value cell-textarea-value cf' ";
      String stFieldReadonlyStyling = String.Format ( "style='width:{0}%; padding-top:10px; ' class='cell value cell-display-text-title' ", valueColumnWidth );
      string stValidationMethod = " onchange=\"Evado.Form.onTextChange( this, this.value )\" ";


      if ( PageField.Layout == EuFieldLayoutCodes.Column_Layout )
      {
        stFieldValueStyling = "style='width:100%' class='cell value cell-input-text-value cf' ";
        stFieldReadonlyStyling = "style='width:100%' class='cell cell-display-text-value cf' ";
        fieldMarginStyle = "style='margin-left:auto; margin-right:auto;'";
      }

      int width = 40;
      int height = 5;

      if ( PageField.hasParameter ( Evado.UniForm.Model.EuFieldParameters.Width ) == true )
      {
        width = PageField.GetParameterInt ( Evado.UniForm.Model.EuFieldParameters.Width );
      }
      if ( PageField.hasParameter ( Evado.UniForm.Model.EuFieldParameters.Height ) == true )
      {
        height = PageField.GetParameterInt ( Evado.UniForm.Model.EuFieldParameters.Height );
      }

      int maxLength = ( int ) ( width * height * 2 );

      if ( PageField.hasParameter ( Evado.UniForm.Model.EuFieldParameters.Max_Value ) == true )
      {
        maxLength = PageField.GetParameterInt ( Evado.UniForm.Model.EuFieldParameters.Max_Value );
      }

      //
      // Ineert the field header
      //
      this.createFieldHeader ( sbHtml, PageField, titleColumnWidth, false );

      //
      // Insert the field elements
      //

      if ( PageField.EditAccess == true )
      {
        sbHtml.AppendFormat ( "<div {0} > ", stFieldValueStyling );
        sbHtml.AppendFormat ( "<div id='sp{0}'>", PageField.Id );
        sbHtml.AppendLine ( "<textarea "
          + "id='" + PageField.FieldId + "' "
          + "name='" + PageField.FieldId + "' "
          + "tabindex = '" + _TabIndex + "' "
          + "rows='" + height + "' "
          + "cols='" + width + "' "
          + "maxlength='" + maxLength + "' "
          + "class='form-control' " + fieldMarginStyle + "  "
          + stValidationMethod + " data-parsley-trigger=\"change\" " );

        if ( PageField.EditAccess == false
          && PageField.hasParameter ( EuFieldParameters.FreeText_MarkDown ) == true )
        {
          sbHtml.Append ( " disabled='disabled' " );
        }

        sbHtml.AppendLine ( ">"
        + PageField.Value
        + "</textarea>" );
        sbHtml.AppendLine ( "</div>" );
        sbHtml.AppendLine ( "</div>" );
      }
      else
      {
        sbHtml.AppendFormat ( "<div {0} > ", stFieldReadonlyStyling );
        this.LogDebug ( "Processing markup" );
        String html = Evado.Model.EvStatics.EncodeMarkDown ( PageField.Value );
        this.LogDebug ( "HTML: decoded value" + html );

        sbHtml.AppendLine ( html );
        sbHtml.AppendLine ( "</div>" );
      }

      this._TabIndex += 2;

      //
      // Insert the field footer elemements
      //
      this.createFieldFooter ( sbHtml, PageField );

    }//END Field Method

    // ===================================================================================
    /// <summary>
    /// This method creates a numeric field html markup
    /// </summary>
    /// <param name="sbHtml">StringBuilder object.</param>
    /// <param name="PageField">Field object.</param>
    // ----------------------------------------------------------------------------------
    private void createNumericField(
      StringBuilder sbHtml, EuField PageField )
    {
      this.LogMethod ( "createNumericField" );
      //
      // Initialise the methods variables and objects.
      //
      int valueColumnWidth = this.UserSession.GroupFieldWidth;
      int titleColumnWidth = 100 - valueColumnWidth;

      String stFieldValueStyling = "style='width:" + valueColumnWidth + "%' class='cell value cell-number-value cf' ";
      String stSize = PageField.GetParameter ( Evado.UniForm.Model.EuFieldParameters.Width );
      String stUnit = PageField.GetParameter ( Evado.UniForm.Model.EuFieldParameters.Unit );
      String stMinValue = PageField.GetParameter ( Evado.UniForm.Model.EuFieldParameters.Min_Value );
      String stMaxValue = PageField.GetParameter ( Evado.UniForm.Model.EuFieldParameters.Max_Value );
      String stMinAlert = PageField.GetParameter ( Evado.UniForm.Model.EuFieldParameters.Min_Alert );
      String stMaxAlert = PageField.GetParameter ( Evado.UniForm.Model.EuFieldParameters.Max_Alert );
      String stCssValid = PageField.GetParameter ( Evado.UniForm.Model.EuFieldParameters.BG_Validation );
      String stCssAlert = PageField.GetParameter ( Evado.UniForm.Model.EuFieldParameters.BG_Alert );
      String stCssNormal = PageField.GetParameter ( Evado.UniForm.Model.EuFieldParameters.BG_Normal );

      if ( stMinAlert != String.Empty
        && stMaxAlert == String.Empty )
      {
        stMinAlert = stMaxValue;
      }

      //
      // Set the normal validation parameters.
      //
      string stValidationMethod = " onchange=\"Evado.Form.onRangeValidation( this, this.value )\" ";

      if ( PageField.Type == Evado.Model.EvDataTypes.Integer )
      {
        stValidationMethod = " data-parsley-integerna data-parsley-trigger=\"change\" ";
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
      this.createFieldHeader ( sbHtml, PageField, titleColumnWidth, false );

      //
      // Convert numeric null (-1E+38F) to text null (NA)
      //
      PageField.Value = Evado.Model.EvStatics.convertNumNullToTextNull ( PageField.Value );

      //
      // Insert the field elements
      //
      sbHtml.AppendLine ( "<div " + stFieldValueStyling + " > " );
      sbHtml.AppendLine ( "<span id='sp" + PageField.Id + "' >" );
      sbHtml.AppendLine ( "<input type='text' "
        + "id='" + PageField.FieldId + "' "
        + "name='" + PageField.FieldId + "' "
        + "value='" + PageField.Value + "' "
        + "maxlength='" + stSize + "' "
        + "tabindex = '" + _TabIndex + "' "
        + "size='" + stSize + "' " ); // class='form-control'

      sbHtml.AppendLine ( "data-fieldid='" + PageField.FieldId + "' " );
      if ( stMinValue != String.Empty )
      {
        sbHtml.Append ( " data-min-value='" + stMinValue + "' "
          + " data-max-value='" + stMaxValue + "' " );
      }
      if ( stMinAlert != String.Empty )
      {
        sbHtml.Append ( " data-min-alert='" + stMinAlert + "' "
          + " data-max-alert='" + stMaxAlert + "' " );
      }
      if ( stCssValid != String.Empty )
      {
        sbHtml.Append ( " data-css-valid='" + stCssValid + "' " );
      }
      if ( stCssAlert != String.Empty )
      {
        sbHtml.Append ( " data-css-alert='" + stCssAlert + "' " );
      }
      if ( stCssNormal != String.Empty )
      {
        sbHtml.Append ( " data-css-norm='" + stCssNormal + "' \r\n" );
      }
      sbHtml.Append ( " " + stValidationMethod + " data-parsley-trigger=\"change\" " );

      if ( PageField.Mandatory == true && PageField.EditAccess != false )
      {
        //sbHtml.Append ( " required " );
      }

      //this.addMandatoryIfAttribute ( sbHtml, PageField );

      if ( PageField.EditAccess == false )
      {
        sbHtml.Append ( " disabled='disabled' " );
      }

      sbHtml.Append ( "  " );
      sbHtml.AppendLine ( "/>" );
      sbHtml.AppendLine ( "</span>" );
      sbHtml.AppendLine ( stUnit );

      sbHtml.AppendLine ( "</div>" );

      this._TabIndex += 2;

      //
      // Insert the field footer elemements
      //
      this.createFieldFooter ( sbHtml, PageField );

    }//END Field Method

    // ===================================================================================
    /// <summary>
    /// This method creates a Name field html markup
    /// </summary>
    /// <param name="sbHtml">StringBuilder object.</param>
    /// <param name="PageField">Field object.</param>
    // ----------------------------------------------------------------------------------
    private void createNumericRangeField( StringBuilder sbHtml, EuField PageField )
    {
      this.LogMethod ( "createNumericRangeField" );
      this.LogDebug ( "Field.Type: " + PageField.Type );
      //
      // Initialise the methods variables and objects.
      //
      int valueColumnWidth = this.UserSession.GroupFieldWidth;
      int titleColumnWidth = 100 - valueColumnWidth;
      String stSize = "15"; ;
      String stFieldValueStyling = "style='width:" + valueColumnWidth + "%' class='cell value cell-number-value' "; //
      String [ ] arrValue = PageField.Value.Split ( ';' );
      String stLowerValue = String.Empty;
      String stUpperValue = String.Empty;
      String value = String.Empty;

      #region numeric range values
      String stUnit = PageField.GetParameter ( Evado.UniForm.Model.EuFieldParameters.Unit );
      String stMinValue = "-1000000";
      String stMaxValue = "1000000";
      String stMinAlert = "-1000000";
      String stMaxAlert = "1000000";
      String stMinNorm = "-1000000";
      String stMaxNorm = "1000000";
      String stCssValid = PageField.GetParameter ( Evado.UniForm.Model.EuFieldParameters.BG_Validation );
      String stCssAlert = PageField.GetParameter ( Evado.UniForm.Model.EuFieldParameters.BG_Alert );
      String stCssNormal = PageField.GetParameter ( Evado.UniForm.Model.EuFieldParameters.BG_Normal );


      value = PageField.GetParameter ( Evado.UniForm.Model.EuFieldParameters.Min_Value );
      if ( value != String.Empty )
      {
        stMinValue = value;
      }
      value = PageField.GetParameter ( Evado.UniForm.Model.EuFieldParameters.Max_Value );
      if ( value != String.Empty )
      {
        stMaxValue = value;
      }
      value = PageField.GetParameter ( Evado.UniForm.Model.EuFieldParameters.Min_Alert );
      if ( value != String.Empty )
      {
        stMinAlert = value;
      }
      value = PageField.GetParameter ( Evado.UniForm.Model.EuFieldParameters.Max_Alert );
      if ( value != String.Empty )
      {
        stMaxAlert = value;
      }
      value = PageField.GetParameter ( Evado.UniForm.Model.EuFieldParameters.Min_Normal );
      if ( value != String.Empty )
      {
        stMinNorm = value;
      }

      value = PageField.GetParameter ( Evado.UniForm.Model.EuFieldParameters.Max_Normal );
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
      #endregion
      //
      // Set the normal validation parameters.
      ///
      string stValidationMethod = " onchange=\"Evado.Form.onRangeValidation( this, this.value )\" ";

      if ( PageField.Type == Evado.Model.EvDataTypes.Integer )
      {
        stValidationMethod = " data-parsley-integerna \" ";
      }

      this.LogMethod ( "Unit: " + stUnit );

      if ( stUnit.Contains ( "10^" ) == true
        && stUnit.Contains ( "10^0" ) == false )
      {
        stUnit = Regex.Replace ( stUnit, @"10\^([-0-9])(.*)", "10<span class='sup'>$1</span><span>$2</span>" );
      }

      if ( stUnit != String.Empty )
      {
        stUnit = "<div class='form-unit' >&nbsp;" + stUnit + "</div>";
      }

      //
      // Ineert the field header
      //
      this.createFieldHeader ( sbHtml, PageField, titleColumnWidth, false );

      //
      // Insert the field elements
      //
      sbHtml.AppendLine ( "<div " + stFieldValueStyling + " > " );
      sbHtml.AppendLine ( "<span id='sp1-" + PageField.Id + "' >" );

      sbHtml.AppendLine ( "<input type='text' "
        + "id='" + PageField.FieldId + DefaultPage.CONST_FIELD_LOWER_SUFFIX + "' "
        + "name='" + PageField.FieldId + DefaultPage.CONST_FIELD_LOWER_SUFFIX + "' "
        + "value='" + stLowerValue + "' "
        + "tabindex = '" + _TabIndex + "' "
        + "maxlength='" + stSize + "' "
        + "size='" + stSize + "' " );

      sbHtml.AppendLine ( "\r\n data-fieldid='" + PageField.FieldId + "' "
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

      if ( PageField.Mandatory == true && PageField.EditAccess != false )
      {
        sbHtml.AppendLine ( " required " );
      }

      //this.addMandatoryIfAttribute ( sbHtml, PageField );

      if ( PageField.EditAccess == false )
      {
        sbHtml.AppendLine ( " disabled='disabled' " );
      }

      sbHtml.AppendLine ( "/>" );
      sbHtml.AppendLine ( "</span>" );

      this._TabIndex++;

      sbHtml.AppendLine ( "<span>&nbsp;-&nbsp;</span>" );

      sbHtml.AppendLine ( "<span id='sp2-" + PageField.Id + "' >" );
      sbHtml.AppendLine ( "<input type='text' "
        + "id='" + PageField.FieldId + DefaultPage.CONST_FIELD_UPPER_SUFFIX + "' "
        + "name='" + PageField.FieldId + DefaultPage.CONST_FIELD_UPPER_SUFFIX + "' "
        + "value='" + stUpperValue + "' "
        + "tabindex = '" + _TabIndex + "' "
        + "maxlength='" + stSize + "' "
        + "size='" + stSize + "' " );

      sbHtml.AppendLine ( "\r\n data-fieldid='" + PageField.FieldId + "' "
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

      if ( PageField.Mandatory == true && PageField.EditAccess != false )
      {
        //sbHtml.Append ( " required " );
      }

      //this.addMandatoryIfAttribute ( sbHtml, PageField );

      if ( PageField.EditAccess == false )
      {
        sbHtml.Append ( " disabled='disabled' " );
      }

      sbHtml.AppendLine ( "/>" );
      sbHtml.AppendLine ( "</span>" );
      sbHtml.AppendLine ( stUnit );
      sbHtml.Append ( "</div> \r\n" );

      this._TabIndex++;
      this._TabIndex++;

      //
      // Insert the field footer elemements
      //
      this.createFieldFooter ( sbHtml, PageField );

    }//END Field Method

    // ===================================================================================
    /// <summary>
    /// This method creates a date field html markup
    /// </summary>
    /// <param name="sbHtml">StringBuilder object.</param>
    /// <param name="PageField">Field object.</param>
    // ----------------------------------------------------------------------------------
    private void createDateField( StringBuilder sbHtml, EuField PageField )
    {
      this.LogMethod ( "createDateField" );
      //
      // Initialise the methods variables and objects.
      //
      int valueColumnWidth = this.UserSession.GroupFieldWidth;
      int titleColumnWidth = 100 - valueColumnWidth;

      String stFieldValueStyling = "style='width:" + valueColumnWidth + "%' class='cell value cell-date-value ' "; //cf
      int minYear = PageField.GetParameterInt ( Evado.UniForm.Model.EuFieldParameters.Min_Value );
      int maxYear = PageField.GetParameterInt ( Evado.UniForm.Model.EuFieldParameters.Max_Value );
      String stCssValid = PageField.GetParameter ( Evado.UniForm.Model.EuFieldParameters.BG_Validation );
      String stCssAlert = PageField.GetParameter ( Evado.UniForm.Model.EuFieldParameters.BG_Alert );

      String stdateSelection = String.Empty;
      String stDate = PageField.Value;
      String stDay = String.Empty;
      String stMonth = String.Empty;
      String stYear = String.Empty;
      if ( maxYear == 0 )
      {
        maxYear = DateTime.Now.Year + 4;
      }
      if ( minYear == 0 )
      {
        minYear = DateTime.Now.Year - 100;
      }

      this.LogValue ( "minYear: " + minYear + " maxYear: " + maxYear );

      String stFormat = "dd - MMM - yyyy";

      //
      // set the time format structure.
      //

      if ( PageField.Type == Evado.Model.EvDataTypes.Year )
      {
        stFormat = "yyyy";
      }
      else
      {
        if ( PageField.hasParameter ( Evado.UniForm.Model.EuFieldParameters.Format ) )
        {
          string value = PageField.GetParameter ( Evado.UniForm.Model.EuFieldParameters.Format );
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
              if ( format != String.Empty )
              {
                format += " - ";
              }
              format += " MMM";
            }
            if ( value.Contains ( "yy" ) == true
              || value.Contains ( "yyyy" ) == true )
            {
              if ( format != String.Empty )
              {
                format += " - ";
              }
              format += "yyyy";
            }
            stFormat = format;
          }
        }
      }

      //
      // Set the normal validation parameters.
      //
      string stValidationMethod = " onchange=\"Evado.Form.onDateSelectChange( this, '" + PageField.FieldId + "' )\" ";

      //
      // Ineert the field header
      //
      this.createFieldHeader ( sbHtml, PageField, titleColumnWidth, false );

      stDate = PageField.Value;
      stDate = stDate.Replace ( " ", "-" );

      String [ ] arDate = stDate.Split ( '-' );
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

      //
      // Insert the field elements
      //
      sbHtml.AppendLine ( "<div " + stFieldValueStyling + " > " );
      sbHtml.AppendLine ( "<span id='sp" + PageField.Id + "' class='form-field-container-inline' >" );

      if ( PageField.EditAccess == true )
      {
        //
        // get the date components
        //
        sbHtml.AppendLine ( this.GetDateField (
          PageField.FieldId,
          PageField.Value,
          PageField.Mandatory, stFormat, minYear, maxYear ) );
      }
      else
      {
        if ( PageField.Value.Contains ( "1900" ) == false
          && PageField.Value.Contains ( "1901" ) == false )
        {
          sbHtml.AppendLine ( "<input type='text' "
            + "id='" + PageField.FieldId + "' "
            + "name='" + PageField.FieldId + "' "
            + "value='" + PageField.Value + "' "
            + "disabled='disabled' />" );
        }

        sbHtml.AppendLine ( "</span>" );
      }

      sbHtml.AppendLine ( "</div>" );

      //
      // Insert the field footer elemements
      //
      this.createFieldFooter ( sbHtml, PageField );

      this.LogMethodEnd ( "createDateField" );
    }//END Field Method

    // ===================================================================================
    /// <summary>
    /// This method creates a text field html markup
    /// </summary>
    /// <param name="sbHtml">StringBuilder object.</param>
    /// <param name="PageField">Field object.</param>
    // ----------------------------------------------------------------------------------
    private void createDefaultDateField( StringBuilder sbHtml, EuField PageField )
    {
      this.LogMethod ( "createDefaultDateField" );
      //
      // Initialise the methods variables and objects.
      //
      int valueColumnWidth = this.UserSession.GroupFieldWidth;
      int titleColumnWidth = 100 - valueColumnWidth;
      int maxLength = 20;

      if ( PageField.hasParameter ( Evado.UniForm.Model.EuFieldParameters.Width ) == true )
      {
        maxLength = PageField.GetParameterInt ( Evado.UniForm.Model.EuFieldParameters.Width );
      }

      //
      // set the min and max lenght if not set.
      //
      if ( maxLength == 0 )
      {
        maxLength = 20;
      }

      int size = maxLength;
      if ( size > 80 )
      {
        size = 80;
      }

      //
      // correct the date formatting for the default browser format.
      //
      DateTime date = EvStatics.getDateTime ( PageField.Value );

      string dateValue = date.ToString ( "yyy-MM-dd" );
      this.LogDebug ( "Field Date: {0}.", dateValue );

      //
      // Set the normal validation parameters.
      //
      string stValidationMethod = " onchange=\"Evado.Form.onTextChange( this, this.value )\" ";

      if ( PageField.hasParameter ( EuFieldParameters.Field_Value_Column_Width ) == true )
      {
        Evado.UniForm.Model.EuFieldValueWidths widthValue = PageField.ValueColumnWidth;
        valueColumnWidth = ( int ) widthValue;
        titleColumnWidth = 100 - valueColumnWidth;
      }
      String stFieldValueStyling = "style='width:" + valueColumnWidth + "%' class='cell value cell-input-text-value cf' ";

      if ( PageField.Layout == EuFieldLayoutCodes.Column_Layout )
      {
        stFieldValueStyling = "style='width:100%' class='cell value cell-input-text-value cf' ";
      }

      //
      // Incert the field header
      //
      this.createFieldHeader ( sbHtml, PageField, titleColumnWidth, false );

      //
      // Insert the field data control
      //
      sbHtml.AppendLine ( "<div " + stFieldValueStyling + " > " );
      sbHtml.AppendLine ( "<div id='sp" + PageField.Id + "'  >" );
      sbHtml.AppendLine ( "<input type='date' "
        + "id='" + PageField.FieldId + "' "
        + "name='" + PageField.FieldId + "' "
        + "value='" + dateValue + "' "
        + "tabindex = '" + _TabIndex + "' "
        + "maxlength='" + maxLength + "' "
        + "size='" + size + "' "
        + "data-fieldid='" + PageField.FieldId + "' "
        + "class='form-control'  "
        + stValidationMethod + " data-parsley-trigger=\"change\" " );

      if ( PageField.Mandatory == true
        && PageField.EditAccess != false )
      {
        //sbHtml.Append ( " required " );
      }

      //this.addMandatoryIfAttribute ( sbHtml, PageField );

      if ( PageField.EditAccess == false )
      {
        sbHtml.Append ( " disabled='disabled' " );
      }

      sbHtml.AppendLine ( "/>" );
      sbHtml.AppendLine ( "</div>" );
      sbHtml.AppendLine ( "</div>" );

      this._TabIndex += 2;

      //
      // Insert the field footer elemements
      //
      this.createFieldFooter ( sbHtml, PageField );

    }//END createDefaultDateField Field Method

    // ===================================================================================
    /// <summary>
    /// This method creates a Name field html markup
    /// </summary>
    /// <param name="sbHtml">StringBuilder object.</param>
    /// <param name="PageField">Field object.</param>
    // ----------------------------------------------------------------------------------
    private void createDateRangeField( StringBuilder sbHtml, EuField PageField )
    {
      this.LogMethod ( "createDateRangeField" );
      this.LogDebug ( "Field.Type: " + PageField.Type );
      //
      // Initialise the methods variables and objects.
      //
      int valueColumnWidth = this.UserSession.GroupFieldWidth;
      int titleColumnWidth = 100 - valueColumnWidth;
      String stFieldValueStyling = "style='width:" + valueColumnWidth + "%' class='cell value cell-date-value' "; //
      String [ ] arrValue = PageField.Value.Split ( ';' );
      String stLowerValue = String.Empty;
      String stUpperValue = String.Empty;
      String value = String.Empty;

      String stFormat = "dd - MMM - yyyy";

      int minYear = PageField.GetParameterInt ( Evado.UniForm.Model.EuFieldParameters.Min_Value );
      int maxYear = PageField.GetParameterInt ( Evado.UniForm.Model.EuFieldParameters.Max_Value );

      if ( PageField.Type == Evado.Model.EvDataTypes.Year )
      {
        stFormat = "yyyy";
      }
      else
      {
        if ( PageField.hasParameter ( Evado.UniForm.Model.EuFieldParameters.Format ) )
        {
          string parmmeter = PageField.GetParameter ( Evado.UniForm.Model.EuFieldParameters.Format );
          parmmeter = parmmeter.ToLower ( );

          if ( parmmeter.Length <= 8 )
          {
            String format = String.Empty;

            if ( parmmeter.Contains ( "dd" ) == true )
            {
              format = "dd";
            }
            if ( parmmeter.Contains ( "mmm" ) == true )
            {
              if ( format != String.Empty )
              {
                format += " - ";
              }
              format += " MMM";
            }
            if ( parmmeter.Contains ( "yy" ) == true
              || parmmeter.Contains ( "yyyy" ) == true )
            {
              if ( format != String.Empty )
              {
                format += " - ";
              }
              format += "yyyy";
            }
            stFormat = format;
          }
        }
      }


      if ( arrValue.Length < 2 )
      {
        stLowerValue = PageField.Value;
        stUpperValue = PageField.Value;
      }
      else
      {
        stLowerValue = arrValue [ 0 ];
        stUpperValue = arrValue [ 1 ];
      }
      this.LogDebug ( "stLowerValue: {0}, stUpperValue: {1} ", stLowerValue, stUpperValue );


      //
      // Ineert the field header
      //
      this.createFieldHeader ( sbHtml, PageField, titleColumnWidth, false );

      //
      // Insert the field elements
      //
      sbHtml.AppendLine ( "<div " + stFieldValueStyling + " > " );
      sbHtml.AppendLine ( "<span id='sp1-" + PageField.Id + "' >" );  //DefaultPage.CONST_FIELD_LOWER_SUFFIX

      //
      // get the date components
      //
      sbHtml.AppendLine ( this.GetDateField (
        PageField.FieldId + DefaultPage.CONST_FIELD_LOWER_SUFFIX,
        stLowerValue,
        PageField.Mandatory, stFormat, minYear, maxYear ) );
      sbHtml.AppendLine ( "</span>" );

      this._TabIndex++;

      sbHtml.AppendLine ( "<span style=\"margin: 5px;\">&nbsp;>>>&nbsp;</span>" );

      sbHtml.AppendLine ( "<span id='sp2-" + PageField.Id + "' >" );

      sbHtml.AppendLine ( this.GetDateField ( PageField.FieldId + DefaultPage.CONST_FIELD_UPPER_SUFFIX,
        stUpperValue,
        PageField.Mandatory, stFormat, minYear, maxYear ) );
      sbHtml.AppendLine ( "</span>" );

      sbHtml.Append ( "</div> \r\n" );

      this._TabIndex++;
      this._TabIndex++;

      //
      // Insert the field footer elemements
      //
      this.createFieldFooter ( sbHtml, PageField );

      this.LogMethodEnd ( "createDateRangeField" );
    }//END Field Method

    // ===================================================================================
    /// <summary>
    /// This method create the date selection list objects.
    /// </summary>
    /// <param name="Fieldid">String Field identifier.</param>
    /// <param name="DateValue">String object.</param>
    /// <param name="Mandatory"> bool: true = mandatory</param>
    /// <param name="Format"> String  the data format to be displayed.</param>
    /// <param name="MinYear">int min year of date range integer.</param>
    /// <param name="MaxYear">int max date range as integer.</param>
    // ----------------------------------------------------------------------------------
    private String GetDateField( String FieldId, String DateValue, bool Mandatory, String Format, int MinYear, int MaxYear )
    {
      this.LogMethod ( "GetDateField" );
      this.LogDebug ( "FieldId : {0}, DateValue: {1}, Mandatory: {2}, Format: {3}, MinYear: {4}, MaxYear: {5}",
        FieldId, DateValue, Mandatory, Format, MinYear, MaxYear );
      //
      // Initialise the methods variables and objects.
      //
      StringBuilder sbHtml = new StringBuilder ( );
      String stDate = DateValue;
      String stDay = String.Empty;
      String stMonth = String.Empty;
      String stYear = String.Empty;

      stDate = stDate.Replace ( " ", "-" );

      String [ ] arDate = stDate.Split ( '-' );
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
        stYear = DateValue;
      }

      List<Evado.Model.EvItem> dayList = new List<Evado.Model.EvItem> ( );
      dayList.Add ( new Evado.Model.EvItem ( ) );

      for ( int day = 1 ; day <= 31 ; day++ )
      {
        dayList.Add ( new Evado.Model.EvItem ( day.ToString ( "00" ) ) );
      }

      List<Evado.Model.EvItem> monthList = Evado.Model.EvStatics.getStringAsOptionList (
        ":;JAN:" + EuLabels.Month_JAN + ";FEB:" + EuLabels.Month_FEB + ";MAR:" + EuLabels.Month_MAR
        + ";APR:" + EuLabels.Month_APR + ";MAY:" + EuLabels.Month_MAY + ";JUN:" + EuLabels.Month_JUN
        + ";JUL:" + EuLabels.Month_JUL + ";AUG:" + EuLabels.Month_AUG + ";SEP:" + EuLabels.Month_SEP
        + ";OCT:" + EuLabels.Month_OCT + ";NOV:" + EuLabels.Month_NOV + ";DEC:" + EuLabels.Month_DEC, true );

      List<Evado.Model.EvItem> yearList = new List<Evado.Model.EvItem> ( );
      yearList.Add ( new Evado.Model.EvItem ( ) );

      for ( int yr = MaxYear ; yr >= MinYear ; yr-- )
      {
        yearList.Add ( new Evado.Model.EvItem ( yr.ToString ( "0000" ) ) );
      }

      //
      // Set the normal validation parameters.
      //
      string stValidationMethod = " onchange=\"Evado.Form.onDateSelectChange( this, '" + FieldId + "' )\" ";
      //
      // day selection list.
      //
      if ( Format.Contains ( "dd" ) == true )
      {
        sbHtml.Append ( "<select "
          + "id='" + FieldId + "_DAY' "
          + "name='" + FieldId + "_DAY' "
          + "tabindex = '" + this._TabIndex + "' "
          + "value='" + stDay + "' "
          + "class='form-field-inline' "
          + "data-parsley-trigger=\"change\" " + stValidationMethod );

        if ( Mandatory == true )
        {
          //sbHtml.Append ( " required " );
        }

        sbHtml.AppendLine ( ">" );

        foreach ( Evado.Model.EvItem option in dayList )
        {
          sbHtml.Append ( " <option value=\"" + option.Value + "\" " );
          if ( option.Value == stDay )
          {
            sbHtml.Append ( " selected='selected' " );
          }
          sbHtml.AppendLine ( ">" + option.Description + " </option>\r\n" );
        }
        sbHtml.Append ( " </select>\r\n" );

        sbHtml.AppendLine ( "<span style=\"margin: 2px;\"></span>" );
        this._TabIndex++;
      }

      //
      // Month selection list.
      //
      if ( Format.Contains ( "MMM" ) == true )
      {

        sbHtml.Append ( "<select "
          + "id='" + FieldId + "_MTH' "
          + "name='" + FieldId + "_MTH' "
          + "value='" + stMonth + "' "
          + "tabindex = '" + this._TabIndex + "' "
          + "class='form-field-inline' "
          + "data-parsley-trigger=\"change\" " + stValidationMethod );

        if ( Mandatory == true )
        {
          //sbHtml.Append ( " required " );
        }

        sbHtml.AppendLine ( ">" );
        this._TabIndex++;

        foreach ( Evado.Model.EvItem option in monthList )
        {
          sbHtml.Append ( " <option value=\"" + option.Value + "\" " );
          if ( option.Value == stMonth )
          {
            sbHtml.Append ( " selected='selected' " );
          }
          sbHtml.AppendLine ( ">" + option.Description + " </option>\r\n" );
        }
        sbHtml.Append ( " </select>\r\n" );

        sbHtml.AppendLine ( "<span style=\"margin: 2px;\"></span>" );
        this._TabIndex++;
      }

      //
      // Year selection list.
      //
      if ( Format.Contains ( "yyyy" ) == true )
      {

        sbHtml.Append ( "<select "
          + "id='" + FieldId + "_YR' "
          + "name='" + FieldId + "_YR' "
          + "tabindex = '" + this._TabIndex + "' "
          + "value='" + stYear + "' "
          + "class='form-field-inline' "
          + "data-parsley-trigger=\"change\" " + stValidationMethod );

        if ( Mandatory == true )
        {
          //sbHtml.Append ( " required " );
        }

        sbHtml.AppendLine ( ">" );

        foreach ( Evado.Model.EvItem option in yearList )
        {
          sbHtml.Append ( " <option value=\"" + option.Value + "\" " );
          if ( option.Value == stYear )
          {
            sbHtml.Append ( " selected='selected' " );
          }
          sbHtml.AppendLine ( ">" + option.Description + " </option>\r\n" );
        }
        sbHtml.AppendLine ( " </select>\r\n" );

        this._TabIndex++;
      }
      /*
        sbHtml.AppendLine ( "<br/><span style='margin:10pt;'>" + Format + "</span>" );
      */
      sbHtml.AppendLine ( "<input type='hidden' "
        + "id='" + FieldId + "' "
        + "name='" + FieldId + "' "
        + "value='" + DateValue + "' />" );

      this.LogMethodEnd ( "GetDateField" );
      return sbHtml.ToString ( );
    }

    // ===================================================================================
    /// <summary>
    /// This method creates a time field html markup
    /// </summary>
    /// <param name="sbHtml">StringBuilder object.</param>
    /// <param name="PageField">Field object.</param>
    // ----------------------------------------------------------------------------------
    private void createTimeField( StringBuilder sbHtml, EuField PageField )
    {
      this.LogMethod ( "createTimeField" );
      //
      // Initialise the methods variables and objects.
      //
      int valueColumnWidth = this.UserSession.GroupFieldWidth;
      int titleColumnWidth = 100 - valueColumnWidth;
      String stSize = PageField.GetParameter ( Evado.UniForm.Model.EuFieldParameters.Width );
      String stFieldValueStyling = "style='width:" + valueColumnWidth + "%' class='cell value cell-date-value ' "; //cf
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

      List<Evado.Model.EvItem> hourList = new List<Evado.Model.EvItem> ( );
      hourList.Add ( new Evado.Model.EvItem ( ) );

      for ( int hr = 0 ; hr < 24 ; hr++ )
      {
        hourList.Add ( new Evado.Model.EvItem ( hr.ToString ( "00" ) ) );
      }


      List<Evado.Model.EvItem> minuteList = new List<Evado.Model.EvItem> ( );
      minuteList.Add ( new Evado.Model.EvItem ( ) );

      //
      // set the time stamp to 5 minute intervals.
      //
      if ( PageField.Type == EvDataTypes.Time_Stamp )
      {
        int iMinutes = EvStatics.getInteger ( stMinutes );

        int quotent = iMinutes / 5;
        stMinutes = ( quotent * 5 ).ToString ( "00" );

        for ( int increment = 0 ; increment < 12 ; increment++ )
        {
          int min = increment * 5;
          minuteList.Add ( new Evado.Model.EvItem ( min.ToString ( "00" ) ) );
        }
      }
      else
      {
        for ( int min = 0 ; min < 60 ; min++ )
        {
          minuteList.Add ( new Evado.Model.EvItem ( min.ToString ( "00" ) ) );
        }
      }

      //
      // Set the normal validation parameters.
      //
      string stValidationMethod = " onchange=\"Evado.Form.onTimeSelectChange( this, '" + PageField.FieldId + "' )\" ";

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
      this.createFieldHeader ( sbHtml, PageField, titleColumnWidth, false );

      sbHtml.Append ( "<div " + stFieldValueStyling + " > " );

      if ( PageField.EditAccess == true )
      {
        //
        // Insert the field elements
        //
        sbHtml.Append ( "<span id='sp" + PageField.Id + "' class='form-field-container-inline' >" );

        sbHtml.Append ( "<select "
          + "id='" + PageField.FieldId + "_HR' "
          + "name='" + PageField.FieldId + "_HR' "
          + "tabindex = '" + this._TabIndex + "' "
          + "value='" + stHour + "' "
          + "class='form-field-inline' "
          + "data-parsley-trigger=\"change\" "
          + stValidationMethod );

        if ( PageField.Mandatory == true && PageField.EditAccess != false )
        {
          //sbHtml.Append ( " required " );
        }

        if ( PageField.EditAccess == false )
        {
          sbHtml.Append ( "disabled='disabled' " );
        }

        sbHtml.AppendLine ( ">" );

        foreach ( Evado.Model.EvItem option in hourList )
        {
          sbHtml.Append ( " <option value=\"" + option.Value + "\" " );
          if ( option.Value == stHour )
          {
            sbHtml.Append ( " selected='selected' " );
          }
          sbHtml.AppendLine ( ">" + option.Description + " </option>\r\n" );
        }
        sbHtml.Append ( " </select>\r\n" );

        sbHtml.AppendLine ( ": " );

        this._TabIndex++;

        sbHtml.Append ( "<select "
          + "id='" + PageField.FieldId + "_MIN' "
          + "name='" + PageField.FieldId + "_MIN' "
          + "tabindex = '" + this._TabIndex + "' "
          + "value='" + stMinutes + "' "
          + "class='form-field-inline' "
          + "data-parsley-trigger=\"change\" "
          + stValidationMethod );

        if ( PageField.Mandatory == true && PageField.EditAccess != false )
        {
          //sbHtml.Append ( " required " );
        }

        if ( PageField.EditAccess == false )
        {
          sbHtml.Append ( "disabled='disabled' " );
        }

        sbHtml.AppendLine ( ">\r\n" );

        foreach ( Evado.Model.EvItem option in minuteList )
        {
          sbHtml.Append ( " <option value=\"" + option.Value + "\" " );
          if ( option.Value == stMinutes )
          {
            sbHtml.Append ( " selected='selected' " );
          }
          sbHtml.AppendLine ( ">" + option.Description + " </option>\r\n" );
        }
        sbHtml.AppendLine ( " </select>\r\n" );

        this._TabIndex++;

        if ( stFormat.Contains ( "SS" ) == true || stFormat.Contains ( "ss" ) == true )
        {
          sbHtml.Append ( ": " );

          sbHtml.Append ( "<select "
            + "id='" + PageField.FieldId + "_SEC' "
            + "name='" + PageField.FieldId + "_SEC' "
            + "tabindex = '" + this._TabIndex + "' "
            + "value='" + stSeconds + "' "
            + "class='form-field-inline' "
            + "data-parsley-trigger=\"change\" "
            + stValidationMethod );

          if ( PageField.Mandatory == true && PageField.EditAccess != false )
          {
            //sbHtml.Append ( " required " );
          }

          if ( PageField.EditAccess == false )
          {
            sbHtml.Append ( "disabled='disabled' " );
          }

          sbHtml.AppendLine ( ">\r\n" );

          foreach ( Evado.Model.EvItem option in minuteList )
          {
            sbHtml.Append ( " <option value=\"" + option.Value + "\" " );
            if ( option.Value == stSeconds )
            {
              sbHtml.Append ( " selected='selected' " );
            }
            sbHtml.AppendLine ( ">" + option.Description + " </option>\r\n" );
          }
          sbHtml.AppendLine ( " </select>\r\n" );

          this._TabIndex++;

        }
        /*
        if ( PageField.EditAccess == true )
        {
          sbHtml.AppendLine ( "<br/><span style='margin:10pt;'>" + stFormat + "</span>" );
        }
        */

        sbHtml.AppendLine ( "<input type='hidden' "
          + "id='" + PageField.FieldId + "' "
          + "name='" + PageField.FieldId + "' "
          + "value='" + PageField.Value + "' />" );

        sbHtml.AppendLine ( "</span>" );
      }
      else
      {
        sbHtml.Append ( "<input type='text' "
          + "id='" + PageField.FieldId + "' "
          + "name='" + PageField.FieldId + "' "
          + "value='" + PageField.Value + "' disabled='disabled' />" );
      }

      sbHtml.AppendLine ( "</div>" );
      //
      // Insert the field footer elemements
      //
      this.createFieldFooter ( sbHtml, PageField );

    }//END Field Method

    // ===================================================================================
    /// <summary>
    /// This method creates a radio button field html markup
    /// </summary>
    /// <param name="sbHtml">StringBuilder object.</param>
    /// <param name="PageField">Field object.</param>
    // ----------------------------------------------------------------------------------
    private void createRadioButtonField( StringBuilder sbHtml, EuField PageField )
    {
      this.LogMethod ( "createRadioButtonField" );
      this.LogDebug ( "PageField.Value: " + PageField.Value );
      //
      // Initialise the methods variables and objects.
      //
      Evado.UniForm.Model.EuFieldValueWidths widthValue = EuFieldValueWidths.Default;
      int valueColumnWidth = this.UserSession.GroupFieldWidth;
      int titleColumnWidth = 100 - valueColumnWidth;
      String stValueLegend = PageField.GetParameter ( Evado.UniForm.Model.EuFieldParameters.Field_Value_Legend );

      if ( PageField.hasParameter ( EuFieldParameters.Field_Value_Column_Width ) == true )
      {
        widthValue = PageField.ValueColumnWidth;
        valueColumnWidth = ( int ) widthValue;
        titleColumnWidth = 100 - valueColumnWidth;
      }
      this.LogDebug ( "valueColumnWidth: " + valueColumnWidth );

      String stFieldValueStyling = "style='width:" + valueColumnWidth + "%' class='cell value cell-check-value cf' ";

      if ( PageField.Layout == EuFieldLayoutCodes.Column_Layout )
      {
        stFieldValueStyling = "style='width:100%' class='cell value cell-check-value cf' ";
      }
      if ( widthValue == EuFieldValueWidths.Forty_Percent )
      {
        stFieldValueStyling = "style='width:30%' class='cell value cell-check-value cf' ";
      }

      //
      // Set the normal validation parameters.
      //
      string stValidationMethod = " onclick=\"Evado.Form.onSelectionValidation( this, this.value )\" ";


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
      for ( int i = 0 ; i < PageField.OptionList.Count ; i++ )
      {
        Evado.Model.EvItem option = PageField.OptionList [ i ];
        if ( option.Value == String.Empty )
        {
          continue;
        }

        sbHtml.AppendLine ( "<div class='radio'>" );


        if ( ( PageField.EditAccess == false )
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

        if ( PageField.SendCmdOnChange == true )
        {
          sbHtml.Append ( this.createOnChangeEvent ( ) );
        }
        else
        {
          sbHtml.Append ( "\r\n " + stValidationMethod );
        }

        if ( PageField.Mandatory == true
          && PageField.EditAccess != false )
        {
          //sbHtml.Append ( " required " );
        }

        //this.addMandatoryIfAttribute ( sbHtml, PageField );

        if ( option.containsValue ( PageField.Value ) == true )
        {
          sbHtml.Append ( " checked='checked' " );
        }

        if ( PageField.EditAccess == false )
        {
          sbHtml.Append ( " disabled='disabled' " );
        }

        sbHtml.AppendLine ( "/>" );
        if ( valueColumnWidth < 40 )
        {
          sbHtml.AppendLine ( "<span class='label-20' >" + option.Description + "</span>" );
        }
        else
        {
          sbHtml.AppendLine ( "<span class='label' >" + option.Description + "</span>" );
        }
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

      if ( PageField.SendCmdOnChange == true )
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

      if ( PageField.EditAccess == false )
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
    // ----------------------------------------------------------------------------------
    private void createQuizRadioButtonField( StringBuilder sbHtml, EuField PageField )
    {
      this.LogMethod ( "createQuizRadioButtonField" );
      this.LogDebug ( "PageField.Value: " + PageField.Value );
      try
      {
        //
        // Initialise the methods variables and objects.
        //
        int valueColumnWidth = this.UserSession.GroupFieldWidth;
        int titleColumnWidth = 100 - valueColumnWidth;
        String stFieldValueStyling = "style='width:" + valueColumnWidth + "%' class='cell value cell-radio-value cf' ";
        String stQuizValue = PageField.GetParameter ( Evado.UniForm.Model.EuFieldParameters.Quiz_Value );
        String stQuizAnswer = PageField.GetParameter ( Evado.UniForm.Model.EuFieldParameters.Quiz_Answer );

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
          for ( int i = 0 ; i < PageField.OptionList.Count ; i++ )
          {
            Evado.Model.EvItem option = PageField.OptionList [ i ];
            if ( option.Value == String.Empty )
            {
              continue;
            }

            sbHtml.AppendLine ( "<div class='radio'>" );


            if ( ( PageField.EditAccess == false )
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
              && PageField.EditAccess != false )
            {
              //sbHtml.Append ( " required " );
            }

            if ( option.containsValue ( PageField.Value ) == true )
            {
              sbHtml.Append ( " checked='checked' " );
            }

            if ( PageField.EditAccess == false )
            {
              sbHtml.Append ( " disabled='disabled' " );
            }

            sbHtml.AppendLine ( "/>" );
            if ( valueColumnWidth < 40 )
            {
              sbHtml.AppendLine ( "<span class='label-20' >" + option.Description + "</span>" );
            }
            else
            {
              sbHtml.AppendLine ( "<span class='label' >" + option.Description + "</span>" );
            }
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

          if ( PageField.EditAccess == false )
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
        this.LogValue ( Evado.Model.EvStatics.getException ( Ex ) );
      }
    }//END Field Method

    // ===================================================================================
    /// <summary>
    /// This method creates a radio1 button field html markup
    /// </summary>
    /// <param name="sbHtml">StringBuilder:  containing html string content</param>
    /// <param name="PageField">Field object.</param>
    /// <returns>String html</returns>
    // ----------------------------------------------------------------------------------
    private void createHorizontalRadioButtonField( StringBuilder sbHtml, EuField PageField )
    {
      this.LogMethod ( "createHorizontalRadioButtonField" );
      //
      // Initialise the methods variables and objects.
      //
      int valueColumnWidth = this.UserSession.GroupFieldWidth;
      int titleColumnWidth = 100 - valueColumnWidth;
      String stFieldValueStyling = "style='width:" + valueColumnWidth + "%;' class='cell value cell-radio-value' ";

      this.LogDebug ( "valueColumnWidth {0}.", valueColumnWidth );

      string options = Evado.Model.EvStatics.getOptionListAsString ( PageField.OptionList, false );
      this.LogDebug ( "options.Length {0}, options {1}.", options.Length, options );

      if ( options.Length > 30 )
      {
        stFieldValueStyling = "style='width:98%;' class='cell value cell-radio-value' ";
      }
      this.LogDebug ( "FieldValueStyle {0}.", stFieldValueStyling );

      //
      // Set the normal validation parameters.
      //
      string stValidationMethod = " onchange=\"Evado.Form.onSelectionValidation( this, this.value )\" ";

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
      for ( int i = 0 ; i < PageField.OptionList.Count ; i++ )
      {
        Evado.Model.EvItem option = PageField.OptionList [ i ];
        if ( option.Value == String.Empty
          || option.Description == String.Empty )
        {
          continue;
        }

        sbHtml.AppendLine ( "<div class='radio-inline' style='text-align:center'>" );
        sbHtml.AppendLine ( "<label  >" );
        sbHtml.AppendLine ( "<input "
         + "type='radio' "
         + "id='" + PageField.FieldId + "_" + ( i + 1 ) + "' "
         + "name='" + PageField.FieldId + "' "
         + "value=\"" + option.Value + "\" "
         + "tabindex = '" + _TabIndex + "' "
         + "data-parsley-trigger=\"change\" " );

        if ( PageField.SendCmdOnChange == true )
        {
          sbHtml.Append ( this.createOnChangeEvent ( ) );
        }
        else
        {
          sbHtml.Append ( "\r\n " + stValidationMethod );
        }

        if ( PageField.Mandatory == true && PageField.EditAccess != false )
        {
          sbHtml.Append ( " required " );
        }

        //this.addMandatoryIfAttribute ( sbHtml, PageField );

        if ( option.containsValue ( PageField.Value ) == true )
        {
          sbHtml.Append ( " checked='checked' " );
        }

        if ( PageField.EditAccess == false )
        {
          sbHtml.Append ( " disabled='disabled' " );
        }

        sbHtml.AppendLine ( "/>" );

        sbHtml.AppendLine ( "<span class='label' > " + option.Description + "</span>" );
        sbHtml.AppendLine ( "</label>" );
        sbHtml.AppendLine ( "</div>" );

      }//END end option iteration loop.

      #region not selected

      if ( PageField.Mandatory == true )
      {
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

        if ( PageField.Mandatory == true && PageField.EditAccess != false )
        {
          sbHtml.Append ( " required " );
        }

        //this.addMandatoryIfAttribute ( sbHtml, PageField );

        if ( PageField.Value == "" )
        {
          sbHtml.Append ( " checked='checked' " );
        }

        if ( PageField.EditAccess == false )
        {
          sbHtml.Append ( " disabled='disabled' " );
        }

        sbHtml.AppendLine ( "/>" );

        sbHtml.AppendLine ( "<span class='label'>" + EuLabels.Radio_Button_Not_Selected + "</span>" );
        sbHtml.AppendLine ( "</label>" );
        sbHtml.AppendLine ( "</div>" );
      }
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
    /// <param name="sbHtml">StringBuilder:  containing html string content</param>
    /// <param name="PageField">Field object.</param>
    // ----------------------------------------------------------------------------------
    private void createYesNoField( StringBuilder sbHtml, EuField PageField )
    {
      this.LogMethod ( "createYesNoField" );
      //
      // Initialise the methods variables and objects.
      //
      int valueColumnWidth = this.UserSession.GroupFieldWidth;
      int titleColumnWidth = 100 - valueColumnWidth;
      String stValueLegend = PageField.GetParameter ( Evado.UniForm.Model.EuFieldParameters.Field_Value_Legend );

      //
      // Set the normal validation parameters.
      //
      string stValidationMethod = " onclick=\"Evado.Form.onSelectionValidation( this, this.value )\" ";

      if ( PageField.hasParameter ( EuFieldParameters.Field_Value_Column_Width ) == true )
      {
        Evado.UniForm.Model.EuFieldValueWidths widthValue = PageField.ValueColumnWidth;
        valueColumnWidth = ( int ) widthValue;
        titleColumnWidth = 100 - valueColumnWidth;
      }
      this.LogDebug ( "valueColumnWidth: " + valueColumnWidth );

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
      if ( PageField.Mandatory == true &&  PageField.EditAccess != Evado.UniForm.Model.EditCodes.Edit_Disabled )
      {
        sbHtml.Append ( " required " );
      }
      */
      //this.addMandatoryIfAttribute ( sbHtml, PageField );

      if ( PageField.Value == "Yes" )
      {
        sbHtml.Append ( " checked='checked' " );
      }

      if ( PageField.EditAccess == false )
      {
        sbHtml.Append ( " disabled='disabled' " );
      }

      if ( PageField.SendCmdOnChange == true )
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
      if ( PageField.Mandatory == true &&  PageField.EditAccess != Evado.UniForm.Model.EditCodes.Edit_Disabled )
      {
        sbHtml.Append ( " required " );
      }
      */
      //this.addMandatoryIfAttribute ( sbHtml, PageField );


      if ( PageField.Value == "No" )
      {
        sbHtml.Append ( " checked='checked' " );
      }


      if ( PageField.EditAccess == false )
      {
        sbHtml.Append ( " disabled='disabled' " );
      }

      if ( PageField.SendCmdOnChange == true )
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
    /// <summary>
    /// This method creates a boolean field html markup
    /// </summary>
    /// <param name="sbHtml">StringBuilder:  containing html string content</param>
    /// <param name="PageField">Field object.</param>
    // ----------------------------------------------------------------------------------
    private void createBooleanField( StringBuilder sbHtml, EuField PageField )
    {
      this.LogMethod ( "createBooleanField" );
      //
      // Initialise the methods variables and objects.
      //
      int valueColumnWidth = this.UserSession.GroupFieldWidth;
      int titleColumnWidth = 100 - valueColumnWidth;
      String stValueLegend = PageField.GetParameter ( Evado.UniForm.Model.EuFieldParameters.Field_Value_Legend );

      //
      // Set the normal validation parameters.
      //
      string stValidationMethod = " onclick=\"Evado.Form.onSelectionValidation( this, this.value )\" ";

      if ( PageField.hasParameter ( EuFieldParameters.Field_Value_Column_Width ) == true )
      {
        Evado.UniForm.Model.EuFieldValueWidths widthValue = PageField.ValueColumnWidth;
        valueColumnWidth = ( int ) widthValue;
        titleColumnWidth = 100 - valueColumnWidth;
      }
      this.LogDebug ( "valueColumnWidth: " + valueColumnWidth );

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
      // yes input control
      //
      sbHtml.AppendLine ( "<label>" );
      sbHtml.Append ( "<input "
       + "type='checkbox' "
       + "id='" + PageField.FieldId + "_Y' "
       + "name='" + PageField.FieldId + "' "
       + "tabindex = '" + _TabIndex + "' "
       + "value=\"Yes\" "
       + "data-parsley-trigger=\"change\" " );

      if ( PageField.Value.ToLower ( ) == "yes"
        || PageField.Value.ToLower ( ) == "1"
        || PageField.Value.ToLower ( ) == "true" )
      {
        sbHtml.Append ( " checked='checked' " );
      }

      if ( PageField.EditAccess == false )
      {
        sbHtml.Append ( " disabled='disabled' " );
      }

      if ( PageField.SendCmdOnChange == true )
      {
        sbHtml.Append ( this.createOnChangeEvent ( ) );
      }
      else
      {
        sbHtml.Append ( "\r\n " + stValidationMethod );
      }

      sbHtml.AppendLine ( "/>" );
      sbHtml.AppendLine ( "</label>" );

      sbHtml.AppendLine ( "</div>" );

      this._TabIndex++;

      //
      // Insert the field footer elemements
      //
      this.createFieldFooter ( sbHtml, PageField );

      sbHtml.AppendLine ( "<!-- END BOOLEAND FIELD -->" );

    }//END Field Method

    // ===================================================================================
    /// <summary>
    /// This method creates a checkbox list field html markup
    /// </summary>
    /// <param name="sbHtml">StringBuilder:  containing html string content</param>
    /// <param name="PageField">Field object.</param>
    // ----------------------------------------------------------------------------------
    private void createCheckboxField( StringBuilder sbHtml, EuField PageField )
    {
      this.LogMethod ( "createCheckboxField" );
      this.LogDebug ( "PageField.Value: {0}.", PageField.Value );
      //
      // Initialise the methods variables and objects.
      //
      Evado.UniForm.Model.EuFieldValueWidths widthValue = EuFieldValueWidths.Default;
      int valueColumnWidth = this.UserSession.GroupFieldWidth;
      int titleColumnWidth = 100 - valueColumnWidth;
      String stValueLegend = PageField.GetParameter ( Evado.UniForm.Model.EuFieldParameters.Field_Value_Legend );


      if ( PageField.hasParameter ( EuFieldParameters.Field_Value_Column_Width ) == true )
      {
        widthValue = PageField.ValueColumnWidth;
        valueColumnWidth = ( int ) widthValue;
        titleColumnWidth = 100 - valueColumnWidth;
      }
      this.LogDebug ( "valueColumnWidth: " + valueColumnWidth );
      this.LogDebug ( "stValueLegend: " + stValueLegend );

      String stFieldValueStyling = "style='width:" + valueColumnWidth + "%' class='cell value cell-check-value cf' ";

      if ( PageField.Layout == EuFieldLayoutCodes.Column_Layout )
      {
        stFieldValueStyling = "style='width:100%' class='cell value cell-check-value cf' ";
      }

      //
      // Set the normal validation parameters.
      //
      string stValidationMethod = " onclick=\"Evado.Form.onSelectionValidation( this, this.value )\" ";

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
      for ( int i = 0 ; i < PageField.OptionList.Count ; i++ )
      {
        Evado.Model.EvItem option = PageField.OptionList [ i ];

        this.LogDebug ( "V: {0}, D {1}, {2}.", option.Value, option.Description, option.containsValue ( PageField.Value ) );

        int count = i + 1;

        sbHtml.AppendLine ( "<div class='checkbox'>" );
        sbHtml.AppendLine ( "<label>" );

        sbHtml.AppendLine ( "<input "
         + "type='checkbox' "
         + "id='" + PageField.FieldId + "_" + count + "' "
         + "name='" + PageField.FieldId + "' "
         + "tabindex = '" + _TabIndex + "' "
         + "value='" + option.Value + "' " ); // + "style='visibility: hidden;' " );


        if ( option.containsValue ( PageField.Value ) == true )
        {
          sbHtml.Append ( " checked='checked' " );
        }

        if ( PageField.EditAccess == false )
        {
          sbHtml.Append ( " disabled='disabled' " );
        }

        if ( PageField.SendCmdOnChange == true )
        {
          sbHtml.Append ( this.createOnChangeEvent ( ) );
        }
        else
        {
          sbHtml.Append ( "\r\n " + stValidationMethod );
        }

        sbHtml.AppendLine ( "/>" );
        if ( valueColumnWidth < 40 )
        {
          sbHtml.AppendLine ( "<span class='label-20' >" + option.Description + "</span>" );
        }
        else
        {
          sbHtml.AppendLine ( "<span class='label' >" + option.Description + "</span>" );
        }
        sbHtml.AppendLine ( "</label>" );
        sbHtml.AppendLine ( "</div>" );

      }//End option iteration loop.

      sbHtml.AppendLine ( "<div style='font-size:8pt;text-align:center;'><span >Multiple Selections</span></div>" );
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
    /// <param name="sbHtml">StringBuilder:  containing html string content</param>
    /// <param name="PageField">Field object.</param>
    // ----------------------------------------------------------------------------------
    private void createSelectionListField(
      StringBuilder sbHtml,
      Evado.UniForm.Model.EuField PageField )
    {
      this.LogMethod ( "createSelectionListField" );
      this.LogDebug ( "PageField: Title: " + PageField.Title );
      this.LogDebug ( "PageField: Value: " + PageField.Value );
      //
      // Initialise the methods variables and objects.
      //
      int valueColumnWidth = this.UserSession.GroupFieldWidth;
      int titleColumnWidth = 100 - valueColumnWidth;

      if ( PageField.hasParameter ( EuFieldParameters.Field_Value_Column_Width ) == true )
      {
        Evado.UniForm.Model.EuFieldValueWidths widthValue = PageField.ValueColumnWidth;
        valueColumnWidth = ( int ) widthValue;
        titleColumnWidth = 100 - valueColumnWidth;
      }
      String stFieldValueStyling = "style='width:" + valueColumnWidth + "%' class='cell value cell-select-value cf' ";



      if ( PageField.Layout == EuFieldLayoutCodes.Column_Layout )
      {
        stFieldValueStyling = "style='width:100%' class='cell value cell-check-value cf' ";
      }

      //
      // Set the normal validation parameters.
      //
      string stValidationMethod = " onclick=\"Evado.Form.onSelectionValidation( this, this.value )\" ";

      //
      // Ineert the field header
      //
      this.createFieldHeader ( sbHtml, PageField, titleColumnWidth, false );

      //
      // Insert the field elements
      //
      sbHtml.Append ( "<div " + stFieldValueStyling + " > "
        + "<select "
        + "id='" + PageField.FieldId + "' "
        + "name='" + PageField.FieldId + "' "
        + "tabindex = '" + _TabIndex + "' "
        + "value='" + PageField.Value
        + "' class='form-control' data-parsley-trigger=\"change\" " );

      if ( PageField.Mandatory == true && PageField.EditAccess != false )
      {
        //sbHtml.Append ( " required " );
      }

      //this.addMandatoryIfAttribute ( sbHtml, PageField );

      if ( PageField.SendCmdOnChange == true )
      {
        sbHtml.Append ( this.createOnChangeEvent ( ) );
      }
      else
      {
        sbHtml.Append ( "\r\n " + stValidationMethod );
      }

      if ( PageField.EditAccess == false )
      {
        sbHtml.Append ( "disabled='disabled' " );
      }

      sbHtml.Append ( ">\r\n" );

      // 
      // Iterate through the stOptions.
      //
      for ( int i = 0 ; i < PageField.OptionList.Count ; i++ )
      {
        Evado.Model.EvItem option = PageField.OptionList [ i ];
        /*
         * Generate the option html
         */
        sbHtml.Append ( " <option value=\"" + option.Value + "\" " );

        if ( option.containsValue ( PageField.Value ) == true )
        {
          sbHtml.Append ( " selected='selected' " );
        }
        sbHtml.Append ( ">" + option.Description + "</option>\r\n" );
      }
      sbHtml.Append ( " </select>\r\n" );

      sbHtml.Append ( "</div>\r\n" );

      this._TabIndex += 2;

      //
      // Insert the field footer elemements
      //
      this.createFieldFooter ( sbHtml, PageField );

    }//END Field Method

    // ==================================================================================
    /// <summary>
    /// This mehod generates the HTMl for a page group.
    /// </summary>
    // ---------------------------------------------------------------------------------
    private String createOnChangeEvent( )
    {
      this.LogMethod ( "createOnChangeEvent" );
      //
      // Exit if there are not commands in the group.
      //
      if ( this.UserSession.CurrentGroup.CommandList == null )
      {
        return String.Empty;
      }

      //
      // Return the onChange event attribute for the first command.
      //
      if ( this.UserSession.CurrentGroup.CommandList.Count > 0 )
      {
        Evado.UniForm.Model.EuCommand command = this.UserSession.CurrentGroup.CommandList [ 0 ];

        return "onchange=\"javascript:onPostBack('" + command.Id + "')\"";
      }

      return String.Empty;
    }

    // ===================================================================================
    /// <summary>
    /// This method creates a table field html markup
    /// </summary>
    /// <param name="sbHtml">StringBuilder:  containing html string content</param>
    /// <param name="PageField">Field object.</param>
    // ----------------------------------------------------------------------------------
    private void createTableField(
      StringBuilder sbHtml,
      Evado.UniForm.Model.EuField PageField )
    {
      this.LogMethod ( "createTableField" );
      this.LogDebug ( "PageField.Layout: {0}, Edit Access: {1}.",
        PageField.Layout, PageField.EditAccess );
      this.LogDebug ( "Table Columns: {0}, Rows: {1}.",
        PageField.Table.ColumnCount, PageField.Table.Rows.Count );

      //
      // Initialise the methods variables and objects.
      //
      bool fullWidth = false;
      int valueColumnWidth = this.UserSession.GroupFieldWidth;
      int titleColumnWidth = 100 - valueColumnWidth;
      if ( PageField.Layout == EuFieldLayoutCodes.Column_Layout )
      {
        valueColumnWidth = 100;
        titleColumnWidth = 100;
        fullWidth = true;
      }
      String stFieldValueStyling = "style='width:" + valueColumnWidth + "%' class='cell value cell-table-value cf' ";

      //
      // Ineert the field header
      //
      this.createFieldHeader ( sbHtml, PageField, titleColumnWidth, fullWidth );

      //
      // Insert the field elements
      //
      sbHtml.Append ( "<div " + stFieldValueStyling + " > " );

      /********************************************************************************** 
       * The table is generated by inserting a table structure into the html
       * the table header is the first row of the table and displays the table content.
       * 
       * Then each row of the table will have a new table row inserted into the html 
       * output text.
       * 
       **********************************************************************************/

      sbHtml.Append ( "<table class='table table-striped'>" );

      this.getTableFieldHeader (
        sbHtml,
        PageField );

      this.LogDebug ( "Table Colum Length: {0}, ColumnCount: {1}.", PageField.Table.Header.Length, PageField.Table.ColumnCount );
      // 
      // Iterate through the rows in the table.
      // 
      for ( int row = 0 ; row < PageField.Table.Rows.Count ; row++ )
      {
        this.getTableFieldDataRow (
        sbHtml,
        PageField,
        row );
      }

      sbHtml.Append ( "</table>\r\n</div>\r\n" );

      //
      // Insert the field footer elemements
      //
      this.createFieldFooter ( sbHtml, PageField );

    }//END createTableField Method

    // =================================================================================
    /// <summary>
    ///   This method generates a table form field header as html markup.
    /// </summary>
    /// <param name="sbHtml">StringBuilder:  containing html string content</param>
    /// <param name="PageField">Field object.</param>
    // --------------------------------------------------------------------------------
    private void getTableFieldHeader(
      StringBuilder sbHtml,
      Evado.UniForm.Model.EuField PageField )
    {
      this.LogMethod ( "getFormFieldTableHeader" );
      // 
      // Initialise local variables.
      // 
      bool dateStampRows = PageField.GetParameterBoolean ( EuFieldParameters.Date_Stamp_Table_Rows );
      this.LogDebug ( "dateStampRows: {0}.", dateStampRows );

      sbHtml.Append ( "<tr>" );
      // 
      // Iterate through the field table header items
      // 
      foreach ( Evado.Model.EvTableHeader header in PageField.Table.Header )
      {
        this.LogDebug ( "No: {0} - {1}, Type: {2}.", header.No, header.Text, header.DataType );
        // 
        // Skip rows that have not header text or set to hidden.
        //
        if ( header.Text == String.Empty
          || header.HideColumn == true )
        {
          continue;
        }


        sbHtml.AppendFormat ( "<td style='width:{0}%;text-align:center;' >\r\n", header.Width );

        sbHtml.AppendFormat ( "<strong>{0}</strong> \r\n", header.Text );

        switch ( header.DataType )
        {
          case Evado.Model.EvDataTypes.Date:
          {
            sbHtml.Append ( "<br/><span class='Smaller_Italics'>(DD MMM YYYY)</span>" );
            break;
          }

          case Evado.Model.EvDataTypes.Numeric:
          {
            if ( header.OptionsOrUnit == String.Empty )
            {
              sbHtml.Append ( "<br/><span class='Smaller_Italics'>(23.5678)</span>" );
            }
            else
            {
              sbHtml.AppendFormat ( "<br/><span class='Smaller_Italics'>(23.5678 {0})</span>\r\n", header.OptionsOrUnit );
            }
            break;
          }
          case Evado.Model.EvDataTypes.Integer:
          {
            if ( header.OptionsOrUnit == String.Empty )
            {
              sbHtml.Append ( "<br/><span class='Smaller_Italics'>(23)</span>" );
            }
            else
            {
              sbHtml.AppendFormat ( "<br/><span class='Smaller_Italics'>(103 {0})</span>\r\n", header.OptionsOrUnit );
            }
            break;
          }

          case EvDataTypes.Check_Box_List:
          case EvDataTypes.Radio_Button_List:
          {
            if ( header.OptionList.Count == 0 && header.OptionsOrUnit != String.Empty )
            {
              header.OptionList = EvStatics.getStringAsOptionList ( header.OptionsOrUnit, false );
            }
            break;
          }
          case EvDataTypes.Selection_List:
          {
            if ( header.OptionList.Count == 0 && header.OptionsOrUnit != String.Empty )
            {
              header.OptionList = EvStatics.getStringAsOptionList ( header.OptionsOrUnit, true );
            }
            break;
          }
        }

        sbHtml.AppendLine ( "</td>" );

      }//END table header iteration loop.

      //
      // Add date stamp column.
      //
      if ( dateStampRows == true )
      {
        this.LogDebug ( "Adding Date Stamp Columns" );
        sbHtml.Append ( "<td style='width:5%;text-align:center;'>" );
        sbHtml.AppendFormat ( "<strong>{0}</strong> \r\n", EuLabels.Table_Row_DateStamp );
        sbHtml.AppendLine ( "</td>" );
      }

      sbHtml.AppendLine ( "</tr>" );

    }//END getTableFieldHeader method

    // =================================================================================
    /// <summary>
    ///   This method generates the table form field's row data as html markup.
    /// </summary>
    /// <param name="sbHtml">StringBuilder object.</param>
    /// <param name="PageField">Field object.</param>
    /// <param name="Row">Integer: table row.</param>
    // --------------------------------------------------------------------------------
    private void getTableFieldDataRow(
      StringBuilder sbHtml,
      Evado.UniForm.Model.EuField PageField,
      int Row )
    {
      this.LogMethod ( "getTableFieldDataRow" );
      this.LogDebug ( "Row: {0}. ColumnCount: {1}.", Row, PageField.Table.ColumnCount );
      this.LogDebug ( "EditAccess: {0}.", PageField.EditAccess );

      EvTableRow row = PageField.Table.Rows [ Row ];

      if ( row.Hide == true )
      {
        return;
      }

      //
      // initialise the methods variables and objects.
      //
      bool dateStampRows = PageField.GetParameterBoolean ( EuFieldParameters.Date_Stamp_Table_Rows );

      // 
      // Open the fieldtable data cells
      // 
      sbHtml.Append ( "<tr>" );

      for ( int column = 0 ; column < PageField.Table.ColumnCount ; column++ )
      {
        Evado.Model.EvTableHeader header = PageField.Table.Header [ column ];
        EvDataTypes cellDataType = header.DataType;

        bool rowReadOnly = false;
        if ( PageField.EditAccess == false
          || row.ReadOnly == true )
        {
          rowReadOnly = true;
        }

        this.LogDebug ( "column: {0}, Text: {1}, DataType: {2}.", column, header.Text, cellDataType );
        try
        {
          // 
          // Skip rows that have not header text
          // 
          if ( header.Text == String.Empty
            || header.HideColumn == true )
          {
            continue;
          }

          //
          // initialise iteration loop variables and objects. 
          //
          string cellId = PageField.FieldId + "_" + ( Row + 1 ) + "_" + ( column + 1 );
          string colValue = row.Column [ column ].Trim ( );

          switch ( cellDataType )
          {
            case Evado.Model.EvDataTypes.Read_Only_Text:
            {
              sbHtml.Append ( "<td class='data' style='text-align:left;'>" );
              sbHtml.Append ( colValue );
              sbHtml.AppendLine ( "</td>" );
              break;
            }//END Text Data Type.

            case Evado.Model.EvDataTypes.Text:
            case Evado.Model.EvDataTypes.Multi_Text_Values:
            {
              if ( rowReadOnly == true )
              {
                sbHtml.Append ( "<td class='data' style='text-align:left;'>" );
                sbHtml.Append ( colValue );
              }
              else
              {
                sbHtml.Append ( "<td class='data'>" );
                sbHtml.AppendLine ( "<input "
                  + "id='" + cellId + "' "
                  + "name='" + cellId + "' "
                  + "maxlength='" + header.Width * 2 + "' "
                  + "size='" + header.Width + "' "
                  + "tabindex = '" + _TabIndex + "' "
                  + "type='text'"
                  + "value='" + colValue + "' "
                  + "class='form-control' "
                  + "/>" );
              }

              this._TabIndex++;

              sbHtml.AppendLine ( "</td>" );

              break;
            }//END Text Data Type.

            case Evado.Model.EvDataTypes.Free_Text:
            {

              if ( rowReadOnly == true )
              {
                sbHtml.Append ( "<td class='data' style='text-align:left;'>" );
                sbHtml.Append ( colValue );
              }
              else
              {
                sbHtml.Append ( "<td class='data'>" );
                sbHtml.AppendLine ( "<textarea "
                  + "id='" + cellId + "' "
                  + "name='" + cellId + "' "
                + "tabindex = '" + _TabIndex + "' "
                + "rows='2' "
                + "cols='" + header.Width + "' "
                + "maxlength='500' "
                + "class='form-control' "
                + ">"
                + colValue
                + "</textarea>" );
              }

              this._TabIndex++;

              sbHtml.AppendLine ( "</td>" );

              break;
            }//END Free Text Data Type.

            case Evado.Model.EvDataTypes.Numeric:
            {

              if ( rowReadOnly == true )
              {
                sbHtml.Append ( "<td class='data' style='text-align:center;'>" );
                sbHtml.Append ( colValue );
              }
              else
              {
                sbHtml.Append ( "<td class='data'>" );
                //
                // Set the field value.
                //
                try
                {
                  if ( colValue != String.Empty )
                  {
                    colValue = Evado.Model.EvStatics.decodeFieldNumeric ( colValue );
                  }
                }
                catch { }

                sbHtml.AppendLine ( "<input "
                    + "id='" + cellId + "' "
                    + "name='" + cellId + "' "
                    + "tabindex = '" + _TabIndex + "' "
                    + "maxlength='10' "
                    + "size='10' "
                    + "type='text' "
                    + "value='" + colValue + "' "
                    + "onchange=\"Evado.Form.onRangeValidation( this, this.value )\" "
                    + " class='form-control' "
                    + "/>" );
              }

              this._TabIndex++;

              sbHtml.AppendLine ( "</td>" );

              break;
            }//END Numeric  Data Type.

            case Evado.Model.EvDataTypes.Integer:
            {

              if ( rowReadOnly == true )
              {
                sbHtml.Append ( "<td class='data' style='text-align:center;'>" );
                sbHtml.Append ( colValue );
              }
              else
              {
                sbHtml.Append ( "<td class='data'>" );
                //
                // Set the field value.
                //
                try
                {
                  if ( colValue != String.Empty )
                  {
                    colValue = Evado.Model.EvStatics.decodeFieldNumeric ( colValue );
                  }
                }
                catch { }

                sbHtml.AppendLine ( "<input "
                    + "id='" + cellId + "' "
                    + "name='" + cellId + "' "
                    + "tabindex = '" + _TabIndex + "' "
                    + "maxlength='10' "
                    + "size='5' "
                    + "type='text' "
                    + "value='" + colValue + "' "
                    + "onchange=\"Evado.Form.onRangeValidation( this, this.value )\" "
                    + " class='form-control' "
                    + "/>" );
              }

              this._TabIndex++;

              sbHtml.AppendLine ( "</td>" );

              break;
            }//END Intenger  Data Type.

            case Evado.Model.EvDataTypes.Date:
            {

              if ( rowReadOnly == true )
              {
                sbHtml.Append ( "<td class='data' style='text-align:center;'>" );
                sbHtml.Append ( colValue );
              }
              else
              {
                sbHtml.Append ( "<td class='data'>" );
                sbHtml.AppendLine ( "<input "
                  + "id='" + cellId + "' "
                  + "name='" + cellId + "' "
                  + "tabindex = '" + _TabIndex + "' "
                  + "maxlength='12' "
                  + "size='12' "
                  + "type='date' "
                  + "value='" + colValue + "' "
                  + "onchange=\"Evado.Form.onDateValidation( this, this.value  )\" "
                  + "  class='form-control' data-behaviour='datepicker' "
                  + "/>" );
              }

              this._TabIndex++;

              sbHtml.AppendLine ( "</td>" );

              break;
            }//END Date case.

            case Evado.Model.EvDataTypes.Computed_Field:
            {
              sbHtml.Append ( "<td class='data'>" );
              //
              // Set the field value.
              //
              try
              {
                if ( colValue != String.Empty )
                {
                  colValue = Evado.Model.EvStatics.decodeFieldNumeric ( colValue );
                }
              }
              catch { }

              sbHtml.AppendLine ( "<input "
                  + "id='" + cellId + "' "
                  + "name='" + cellId + "' "
                  + "tabindex = '" + _TabIndex + "' "
                  + "maxlength='10' "
                  + "size='10' "
                  + "type='text' "
                  + "value='" + colValue + "' "
                  + " class='form-control' "
                  + " readonly='readonly' " +
                  "/>" );

              this._TabIndex++;

              sbHtml.AppendLine ( "</td>" );

              break;
            }//END Computed Data Type.

            case Evado.Model.EvDataTypes.Boolean:
            {
              colValue = colValue.ToLower ( );
              string buttonValue = EuLabels.Boolean_Yes_Label;

              if ( header.OptionsOrUnit != String.Empty )
              {
                buttonValue = header.OptionsOrUnit;
              }

              //this.LogDebug ( "Row ReadOnly: {0}, Boolean (checkbox), cellId: {1}, buttonValue: {2}, colValue: {3}.",
              //rowReadOnly, cellId, buttonValue, colValue );

              if ( rowReadOnly == true )
              {
                //this.LogDebug ( "EditAccess = Disabled." );
                sbHtml.Append ( "<td class='data' style='text-align:center;'>" );

                var bValue = EvStatics.getBool ( colValue );

                if ( header.OptionsOrUnit == String.Empty )
                {
                  if ( bValue == true )
                  {
                    colValue = "Yes";
                  }
                  else
                  {
                    colValue = "";
                  }
                }
                else
                {
                  if ( bValue == true )
                  {
                    colValue = header.OptionsOrUnit;
                  }
                  else
                  {
                    colValue = String.Empty;
                  }
                }
                sbHtml.Append ( colValue );

                sbHtml.AppendLine ( "</td>" );

                break;
              }

              //this.LogDebug ( "EditAccess = Enabled." );
              sbHtml.AppendLine ( "<td class='data'>" );
              //
              // display the boolean value if there is a value 'true' or 'false'.
              //
              sbHtml.AppendLine ( "<div class='checkbox-table'>" );
              sbHtml.AppendLine ( "<label >" );
              sbHtml.Append ( "<input "
               + "type='checkbox' "
               + "id='" + cellId + "' "
               + "name='" + cellId + "' "
               + "tabindex = '" + _TabIndex + "' "
               + "value='true' " );

              if ( colValue == "true" )
              {
                sbHtml.Append ( " checked='checked' " );
              }

              sbHtml.AppendLine ( "/>" );

              sbHtml.AppendLine ( "<span class='label' >" + buttonValue + "</span>" );
              sbHtml.AppendLine ( "</label>" );
              sbHtml.AppendLine ( "</div>" );

              this._TabIndex++;

              sbHtml.AppendLine ( "</td>" );

              break;
            }//END Boolean  Case.

            case Evado.Model.EvDataTypes.Yes_No:
            {
              if ( rowReadOnly == true )
              {
                sbHtml.Append ( "<td class='data' style='text-align:center;'>" );
                sbHtml.Append ( colValue );
              }
              else
              {
                /*
                 * Create the selectionlist HTML
                 */
                sbHtml.Append ( "<td class='data'>" );
                sbHtml.Append ( "<select "
                    + "id='" + cellId + "' "
                    + "name='" + cellId + "' "
                    + "tabindex = '" + _TabIndex + "' "
                    + "value='" + colValue + "' "
                    + " class='column-control' style= width: 60%;' onchange=\"Evado.Form.onSelectionValidation( this, this.value  )\" " );

                if ( PageField.EditAccess == false
                  || row.ReadOnly == true )
                {
                  sbHtml.Append ( "disabled='disabled' " );
                }

                sbHtml.Append ( ">\r\n" );

                if ( colValue.ToLower ( ) == "yes"
                  || colValue.ToLower ( ) == "true" )
                {
                  sbHtml.Append ( "<option value='Yes' selected='selected' >Yes</option>" );
                }
                else
                {
                  sbHtml.Append ( "<option value='Yes' >Yes</option>" );
                }

                if ( colValue.ToLower ( ) == "no"
                  || colValue.ToLower ( ) == "false"
                  || colValue == "" )
                {
                  sbHtml.Append ( "<option value='No' selected='selected' >No</option>" );
                }
                else
                {
                  sbHtml.Append ( "<option value='No' >No</option>" );
                }

                sbHtml.Append ( " </select>" );

                this._TabIndex++;
              }

              sbHtml.AppendLine ( "</td>" );

              break;
            }//END Yes No  Case.

            case Evado.Model.EvDataTypes.Radio_Button_List:
            {
              if ( rowReadOnly == true )
              {
                sbHtml.Append ( "<td class='data' style='text-align:center;'>" );
                sbHtml.Append ( colValue );
              }
              else
              {
                sbHtml.Append ( "<td class='data'>" );
                List<Evado.Model.EvItem> optionList = PageField.Table.Header [ column ].OptionList;

                // 
                // Iterate through the stOptions.
                // 
                for ( int i = 0 ; i < optionList.Count ; i++ )
                {
                  //
                  // Create a button if the option exist.
                  //
                  if ( optionList [ i ].Description != String.Empty )
                  {
                    sbHtml.Append ( "<div class='radio'><label>\r\n"
                       + "<input "
                       + "type='radio' "
                       + "id='" + cellId + "_" + ( i + 1 ) + "' "
                       + "name='" + cellId + "' "
                       + "tabindex = '" + _TabIndex + "' "
                       + "value='" + optionList [ i ].Value + "' "
                       + "onclick=\"onSelectionValidation( this, this.value  )\" " );

                    if ( colValue == optionList [ i ].Value )
                    {
                      sbHtml.Append ( " checked='checked' " );
                    }

                    if ( PageField.EditAccess == false
                  || row.ReadOnly == true )
                    {
                      sbHtml.Append ( " disabled='disabled' " );
                    }

                    sbHtml.Append ( "/>\r\n" );

                    //
                    // Bold the selected item when in display mode as the button may not
                    // be obvious in some browsers.
                    //
                    if ( ( PageField.EditAccess == false )
                      && ( colValue == optionList [ i ].Value ) )
                    {
                      sbHtml.Append ( "<strong>" + optionList [ i ].Description + "<strong>" );
                    }
                    else
                    {
                      sbHtml.Append ( optionList [ i ].Description );
                    }
                    sbHtml.AppendLine ( "</label></div>" );

                    this._TabIndex++;

                    sbHtml.AppendLine ( "</td>" );

                  }//END option exists.

                }//End option iteration loop.

                sbHtml.Append ( "<div class='radio'><label>\r\n"
                   + "<input "
                   + "type='radio' "
                   + "id='" + cellId + "_" + ( optionList.Count + 1 ) + "' "
                   + "name='" + cellId + "' "
                   + "tabindex = '" + _TabIndex + "' "
                   + "value='' " );

                if ( row.Column [ column ] == String.Empty )
                {
                  sbHtml.Append ( "checked='checked' " );
                }

                if ( PageField.EditAccess == false )
                {
                  sbHtml.Append ( "disabled='disabled' " );
                }

                sbHtml.AppendLine ( "/>\r\n"
                    + "Not Selected\r\n"
                    + "</label></div>" );

                this._TabIndex++;
              }
              sbHtml.AppendLine ( "</td>" );

              break;

            }//END Radio Button  Case.

            case Evado.Model.EvDataTypes.Selection_List:
            {
              if ( PageField.EditAccess == false
                || row.ReadOnly == true )
              {
                sbHtml.Append ( "<td class='data' style='text-align:center;'>" );
                sbHtml.Append ( colValue );
              }
              else
              {
                sbHtml.Append ( "<td class='data'>" );
                List<Evado.Model.EvItem> optionList = PageField.Table.Header [ column ].OptionList;

                /*
                 * Create the selectionlist HTML
                 */
                sbHtml.Append ( "<select "
                    + "id='" + cellId + "' "
                    + "name='" + cellId + "' "
                    + "tabindex = '" + _TabIndex + "' "
                    + "value='" + colValue + "' "
                    + " class='column-control' style= width: 90%;' onchange=\"Evado.Form.onSelectionValidation( this, this.value  )\" " );

                if ( PageField.EditAccess == false
                  || row.ReadOnly == true )
                {
                  sbHtml.Append ( "disabled='disabled' " );
                }

                sbHtml.Append ( ">\r\n" );

                if ( colValue == String.Empty )
                {
                  sbHtml.Append ( "<option value='' selected='selected' ></option>" );
                }
                else
                {
                  sbHtml.Append ( "<option value='' ></option>" );
                }

                // 
                // Iterate through the stOptions.
                // 
                for ( int i = 0 ; i < optionList.Count ; i++ )
                {

                  //
                  // Add the option if it exists.
                  //
                  if ( optionList [ i ].Description != String.Empty )
                  {
                    //
                    // Generate the option html
                    //
                    sbHtml.AppendFormat ( " <option value=\"{0}\" ", optionList [ i ].Value );

                    if ( optionList [ i ].Value == colValue )
                    {
                      sbHtml.Append ( " selected='selected' " );
                    }
                    sbHtml.Append ( ">" + optionList [ i ].Description + "</option>" );

                  }//END option exists.

                }//End option iteration loop.
                sbHtml.Append ( " </select>" );

                this._TabIndex++;
              }
              sbHtml.AppendLine ( "</td>" );

              break;
            }//END Selection List  Case.

            default:
            {
              //   this.LogDebug ( "DataType {0}, was not displayed.", header.DataType );
              break;
            }

          }//END Switch statement

        }
        catch ( Exception Ex )
        {
          this.LogValue ( "Row: {0}, {1} Exception: \r\n{2}", Row, column, Evado.Model.EvStatics.getException ( Ex ) );
        }

      }//END column iteration loop,

      if ( dateStampRows == true )
      {
        sbHtml.Append ( "<td class='data'>" );
        sbHtml.Append ( row.DateStamp );
        sbHtml.AppendLine ( "</td>" );
      }

      sbHtml.Append ( "</tr>" );


    }//END getTableFieldDataRow method

    // ===================================================================================
    /// <summary>
    /// This method creates a test field html markup
    /// </summary>
    /// <param name="sbHtml">StringBuilder object.</param>
    /// <param name="PageField">Field object.</param>
    // ----------------------------------------------------------------------------------
    private void createBinaryField( StringBuilder sbHtml, EuField PageField )
    {
      this.LogMethod ( "createBinaryField" );
      this.LogDebug ( "TempUrl: " + Global.TempUrl );
      this.LogDebug ( "PageField.FieldId: " + PageField.FieldId );
      this.LogDebug ( "PageField.Value: " + PageField.Value );
      //
      // Initialise the methods variables and objects.
      //
      int valueColumnWidth = this.UserSession.GroupFieldWidth;
      int titleColumnWidth = 100 - valueColumnWidth;
      string stBinaryUrl = Global.TempUrl + PageField.Value;
      this.TestFileUpload.Visible = true;

      // 
      // If the url does not include a http statement add the default image url 
      // 
      stBinaryUrl = stBinaryUrl.ToLower ( );
      stBinaryUrl = Global.concatinateHttpUrl ( Global.TempUrl, PageField.Value );

      string stValue = PageField.Value;
      string stLinkUrl = stBinaryUrl;
      string stLinkTitle = PageField.Value;

      int index = stLinkUrl.LastIndexOf ( '/' );
      if ( index > 0 )
      {
        stLinkTitle = stLinkUrl.Substring ( index + 1 );
      }

      if ( stValue.Contains ( "^" ) == true )
      {
        string [ ] arrValue = stValue.Split ( '^' );
        stLinkUrl = arrValue [ 0 ];
        stLinkTitle = arrValue [ 1 ];
      }

      this.LogDebug ( "stImageUrl: " + stBinaryUrl );

      String stFieldValueStyling = "style='width:" + valueColumnWidth + "%' class='cell value cell-input-text-value cf' ";

      //
      // Ineert the field header
      //
      this.createFieldHeader ( sbHtml, PageField, titleColumnWidth, false );

      //
      // Insert the field elements
      //
      sbHtml.AppendLine ( "<div " + stFieldValueStyling + " >" );

      sbHtml.AppendLine ( "<span>" );
      sbHtml.AppendLine ( "<strong>" );
      sbHtml.AppendLine ( "<a href='" + stLinkUrl + "' target='_blank' tabindex = '" + this._TabIndex + "' >" + stLinkTitle + "</a>" );
      sbHtml.AppendLine ( "</strong>" );
      sbHtml.AppendLine ( "</span>" );
      if ( PageField.EditAccess == true )
      {

        sbHtml.AppendLine ( "<input "
          + "name='" + PageField.FieldId + EuField.CONST_IMAGE_FIELD_SUFFIX + "' "
          + "id='" + PageField.FieldId + EuField.CONST_IMAGE_FIELD_SUFFIX + "' "
          + "type='file' "
          + "class='form-control' "
          + "size='100' />" );
      }

      sbHtml.AppendLine ( "<input type='hidden' "
           + "id='" + PageField.FieldId + "' "
           + "name='" + PageField.FieldId + "' "
           + "value='" + PageField.Value + "' /> " );
      sbHtml.AppendLine ( "</div>" );

      //
      // Insert the field footer
      //
      this.createFieldFooter ( sbHtml, PageField );

    }//END Field Method

    // ===================================================================================
    /// <summary>
    /// This method creates a sound field HTML markup
    /// </summary>
    /// <param name="sbHtml">StringBuilder object.</param>
    /// <param name="PageField">Field object.</param>
    // ----------------------------------------------------------------------------------
    private void createSoundField(
      StringBuilder sbHtml,
      Evado.UniForm.Model.EuField PageField )
    {
      this.LogMethod ( "createSoundField" );
      //
      // Initialise the methods variables and objects.
      //
      int valueColumnWidth = this.UserSession.GroupFieldWidth;
      int titleColumnWidth = 100 - valueColumnWidth;
      String stFieldValueStyling = "style='width:" + valueColumnWidth + "%' class='cell value cell-sound-value cf' ";

      if ( Global.DebugLogOn == true )
      {
        //
        // Ineert the field header
        //
        this.createFieldHeader ( sbHtml, PageField, titleColumnWidth, false );

        //
        // Insert the field elements
        //
        sbHtml.Append ( "<div " + stFieldValueStyling + " > "
          + "Sound Field - Note Supported in the web client."
            + "</div>\r\n" );

        //
        // Insert the field footer
        //
        this.createFieldFooter ( sbHtml, PageField );
      }

    }//END Field Method

    // ===================================================================================
    /// <summary>
    /// This method creates a currency field html markup
    /// </summary>
    /// <param name="sbHtml">StringBuilder object.</param>
    /// <param name="PageField">Field object.</param>
    // ----------------------------------------------------------------------------------
    private void createCurrencyField(
      StringBuilder sbHtml,
      Evado.UniForm.Model.EuField PageField )
    {
      this.LogMethod ( "createCurrencyField" );
      //
      // Initialise the methods variables and objects.
      //
      int valueColumnWidth = this.UserSession.GroupFieldWidth;
      int titleColumnWidth = 100 - valueColumnWidth;
      String stSize = PageField.GetParameter ( Evado.UniForm.Model.EuFieldParameters.Width );
      String stFieldValueStyling = "style='width:" + valueColumnWidth + "%' class='cell value cell-currency-value cf' ";

      if ( stSize == String.Empty )
      {
        stSize = "12";
      }
      //
      // Ineert the field header
      //
      this.createFieldHeader ( sbHtml, PageField, titleColumnWidth, false );

      //
      // Insert the field elements
      //
      sbHtml.Append ( "<div " + stFieldValueStyling + " > "
        + "<span id='sp" + PageField.Id + "'>"
        + "<input type='text' "
        + "id='" + PageField.FieldId + "' "
        + "name='" + PageField.FieldId + "' "
        + "tabindex = '" + _TabIndex + "' "
        + "value='" + PageField.Value + "' "

        + "maxlength='" + stSize + "' "
        + "tabindex = '" + _TabIndex + "' "
        + "size='" + stSize + "' class='form-control' data-parsley-trigger=\"change\" " );

      if ( PageField.Mandatory == true && PageField.EditAccess != false )
      {
        //sbHtml.Append ( " required " );
      }

      //this.addMandatoryIfAttribute ( sbHtml, PageField );

      if ( PageField.EditAccess == false )
      {
        sbHtml.Append ( " disabled='disabled' " );
      }

      sbHtml.Append ( "/></span></div>\r\n" );

      this._TabIndex += 2;

      //
      // Insert the field footer elemements
      //
      this.createFieldFooter ( sbHtml, PageField );

    }//END Field Method

    // ===================================================================================
    /// <summary>
    /// This method creates a EmailAddress field html markup
    /// </summary>
    /// <param name="sbHtml">StringBuilder object.</param>
    /// <param name="PageField">Field object.</param>
    // ----------------------------------------------------------------------------------
    private void createEmailAddressField(
      StringBuilder sbHtml,
      Evado.UniForm.Model.EuField PageField )
    {
      this.LogMethod ( "createEmailAddressField" );
      //
      // Initialise the methods variables and objects.
      //
      int valueColumnWidth = this.UserSession.GroupFieldWidth;
      int titleColumnWidth = 100 - valueColumnWidth;
      String stSize = PageField.GetParameter ( Evado.UniForm.Model.EuFieldParameters.Width );
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
      this.createFieldHeader ( sbHtml, PageField, titleColumnWidth, false );

      //
      // Insert the field elements
      //
      sbHtml.Append ( "<div " + stFieldValueStyling + " > "
        + "<span id='sp" + PageField.Id + "'>"
        + "<input type='email' "
        + "id='" + PageField.FieldId + "' "
        + "name='" + PageField.FieldId + "' "
        + "tabindex = '" + _TabIndex + "' "
        + "value='" + PageField.Value + "' "
        + "maxlength='" + stSize + "' "
        + "tabindex = '" + _TabIndex + "' "
        + "size='" + stSize + "' class='form-control' data-parsley-trigger=\"change\" " );

      if ( PageField.Mandatory == true && PageField.EditAccess != false )
      {
        //sbHtml.Append ( " required " );
      }

      //this.addMandatoryIfAttribute ( sbHtml, PageField );

      if ( PageField.EditAccess == false )
      {
        sbHtml.Append ( " disabled='disabled' " );
      }

      sbHtml.AppendLine ( "/></span></div>" );

      this._TabIndex += 2;

      //
      // Insert the field footer elemements
      //
      this.createFieldFooter ( sbHtml, PageField );

    }//END Field Method

    // ===================================================================================
    /// <summary>
    /// This method creates a Telephone Number field html markup
    /// </summary>
    /// <param name="sbHtml">StringBuilder object.</param>
    /// <param name="PageField">Field object.</param>
    // ----------------------------------------------------------------------------------
    private void createTelephoneNumberField(
      StringBuilder sbHtml,
      Evado.UniForm.Model.EuField PageField )
    {
      this.LogMethod ( "createTelephoneNumberField" );
      //
      // Initialise the methods variables and objects.
      //
      int valueColumnWidth = this.UserSession.GroupFieldWidth;
      int titleColumnWidth = 100 - valueColumnWidth;
      String stSize = PageField.GetParameter ( Evado.UniForm.Model.EuFieldParameters.Width );
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
      this.createFieldHeader ( sbHtml, PageField, titleColumnWidth, false );

      //
      // Insert the field elements
      //
      sbHtml.Append ( "<div " + stFieldValueStyling + " > "
        + "<span id='sp" + PageField.Id + "'>"
        + "<input type='tel' "
        + "id='" + PageField.FieldId + "' "
        + "name='" + PageField.FieldId + "' "
        + "tabindex = '" + _TabIndex + "' "
        + "value='" + PageField.Value + "' "

        + "maxlength='" + stSize + "' "
        + "tabindex = '" + _TabIndex + "' "
        + "size='" + stSize + "' class='form-control' data-parsley-trigger=\"change\" " );

      if ( PageField.Mandatory == true && PageField.EditAccess != false )
      {
        //sbHtml.Append ( " required " );
      }

      //this.addMandatoryIfAttribute ( sbHtml, PageField );

      if ( PageField.EditAccess == false )
      {
        sbHtml.Append ( " disabled='disabled' " );
      }

      sbHtml.AppendLine ( "/></span></div>" );

      this._TabIndex += 2;

      //
      // Insert the field footer elemements
      //
      this.createFieldFooter ( sbHtml, PageField );

    }//END Field Method

    // ===================================================================================
    /// <summary>
    /// This method creates a Telephone Number field html markup
    /// </summary>
    /// <param name="sbHtml">StringBuilder object.</param>
    /// <param name="PageField">Field object.</param>
    // ----------------------------------------------------------------------------------
    private void createAnalogueField(
      StringBuilder sbHtml,
      Evado.UniForm.Model.EuField PageField )
    {
      this.LogMethod ( "createAnalogueField" );
      //
      // Initialise the methods variables and objects.
      //
      int valueColumnWidth = this.UserSession.GroupFieldWidth;
      int titleColumnWidth = 100 - valueColumnWidth;
      String stFieldValueStyling = "style='width:100%;' class='cell value cell-input-telephones-value cf' ";
      string minLabel = PageField.GetParameter ( EuFieldParameters.Min_Label );
      string maxLabel = PageField.GetParameter ( EuFieldParameters.Max_Label );
      float increment = PageField.GetParameterflt ( EuFieldParameters.Increment );

      if ( increment == 0
        || increment == Evado.Model.EvStatics.CONST_NUMERIC_NULL
        || increment == Evado.Model.EvStatics.CONST_NUMERIC_ERROR )
      {
        this.LogDebug ( "Set Default Increment" );
        increment = 2.5F;
      }
      this.LogDebug ( "Increment {0}.", increment );

      //
      // Set the column layout to display the analogue scale below the field title and instructions.
      //
      PageField.Layout = Evado.UniForm.Model.EuFieldLayoutCodes.Column_Layout;

      //
      // Ineert the field header
      //
      this.createFieldHeader ( sbHtml, PageField, titleColumnWidth, true );

      //
      // Insert the field elements
      //
      sbHtml.AppendLine ( "<div " + stFieldValueStyling + " > " );
      sbHtml.AppendLine ( "<span id='sp" + PageField.Id + "'>" );

      sbHtml.Append ( "<input type='range' " );
      sbHtml.Append ( "id='" + PageField.FieldId + "' " );
      sbHtml.Append ( "name='" + PageField.FieldId + "' " );
      sbHtml.Append ( "value='" + PageField.Value + "' " );
      sbHtml.Append ( "tabindex = '" + _TabIndex + "' " );
      sbHtml.Append ( "min='0' " );
      sbHtml.Append ( "max='100' " );
      sbHtml.Append ( "step='" + increment + "' " );
      sbHtml.Append ( "tabindex = '" + _TabIndex + "' " );
      sbHtml.Append ( "class='form-control-analogue' " );
      sbHtml.Append ( "data-parsley-trigger=\"change\" " );

      if ( PageField.Mandatory == true && PageField.EditAccess != false )
      {
        //sbHtml.Append ( " required " );
      }

      //this.addMandatoryIfAttribute ( sbHtml, PageField );

      if ( PageField.EditAccess == false )
      {
        sbHtml.Append ( " disabled='disabled' " );
      }

      sbHtml.AppendLine ( "/>" );
      sbHtml.AppendLine ( "</span>" );

      sbHtml.AppendLine ( "<table style='width:100%; ' >" );
      sbHtml.AppendLine ( "<tr>" );
      sbHtml.Append ( "<td style='text-align: left; width:2.5%;'> " );
      sbHtml.Append ( "|<br/>" );
      sbHtml.Append ( "0" );
      sbHtml.AppendLine ( "</td>" );
      for ( int i = 1 ; i < 20 ; i++ )
      {
        sbHtml.Append ( "<td style='text-align: center; width:5%;'> " );
        int value = i * 5;
        sbHtml.Append ( "|<br/>" );
        sbHtml.Append ( value.ToString ( "0#" ) );
        sbHtml.AppendLine ( "</td>" );
      }
      sbHtml.Append ( "<td style='text-align: right; width:2.5%;'> " );
      sbHtml.Append ( "|<br/>" );
      sbHtml.Append ( "100" );
      sbHtml.AppendLine ( "</td>" );
      sbHtml.AppendLine ( "</tr>" );
      sbHtml.AppendLine ( "</table>" );

      if ( minLabel != String.Empty
        && maxLabel != String.Empty )
      {
        sbHtml.AppendLine ( "<table style='width:100%; ' >" );
        sbHtml.AppendLine ( "<tr>" );
        sbHtml.Append ( "<td style='text-align: left; width:50%;'> " );
        sbHtml.Append ( minLabel );
        sbHtml.AppendLine ( "</td>" );
        sbHtml.Append ( "<td style='text-align: right; width:50%;'> " );
        sbHtml.Append ( maxLabel );
        sbHtml.AppendLine ( "</td>" );
        sbHtml.AppendLine ( "</tr>" );
        sbHtml.AppendLine ( "</table>" );
      }
      sbHtml.AppendLine ( "</div>" );

      this._TabIndex += 2;

      _TabIndex++;

      //
      // Insert the field footer elemements
      //
      this.createFieldFooter ( sbHtml, PageField );

    }//END Field Method

    // ===================================================================================
    /// <summary>
    /// This method creates a Name field html markup
    /// </summary>
    /// <param name="sbHtml">StringBuilder object.</param>
    /// <param name="PageField">Field object.</param>
    // ----------------------------------------------------------------------------------
    private void createNameField(
      StringBuilder sbHtml,
      Evado.UniForm.Model.EuField PageField )
    {
      this.LogMethod ( "createNameField" );
      //
      // Initialise the methods variables and objects.
      //
      int valueColumnWidth = this.UserSession.GroupFieldWidth;
      int titleColumnWidth = 100 - valueColumnWidth;
      String stFormat = PageField.GetParameter ( Evado.UniForm.Model.EuFieldParameters.Format );
      String stFieldValueStyling = "style='width:" + valueColumnWidth + "%' class='cell value cell-input-name-value cf' ";
      EvName name = new EvName ( PageField.Value );

      //
      // Ineert the field header
      //
      this.createFieldHeader ( sbHtml, PageField, titleColumnWidth, false );

      //
      // Insert the field elements
      //
      sbHtml.AppendLine ( "<div " + stFieldValueStyling + " > " );
      sbHtml.AppendLine ( "<div id='sp" + PageField.Id + "' >" );


      if ( stFormat.Contains ( EvName.NAME_FORMAT_PREFIX ) == true )
      {
        sbHtml.AppendLine ( "<div style='display: inline-block;'>" );
        sbHtml.AppendLine ( "<input type='text' "
         + "id='" + PageField.FieldId + EvName.NAME_PREFIX_FIELD_SUFFIX + "' "
         + "name='" + PageField.FieldId + EvName.NAME_PREFIX_FIELD_SUFFIX + "' "
         + "value='" + name.Prefix + "' "
        + "tabindex = '" + _TabIndex + "' "
         + "tabindex = '" + _TabIndex + "' "
         + "size='" + EvName.Prefix_Value_Width + "' "
         + "class='form-control' data-parsley-trigger=\"change\" " );

        if ( PageField.Mandatory == true && PageField.EditAccess != false )
        {
          //sbHtml.Append ( " required " );
        }

        //this.addMandatoryIfAttribute ( sbHtml, PageField );

        if ( PageField.EditAccess == false )
        {
          sbHtml.Append ( " disabled='disabled' " );
        }

        sbHtml.Append ( "/></div>\r\n" );

        this._TabIndex++;
      }
      sbHtml.AppendLine ( "<div style='display: inline-block;'>" );
      sbHtml.AppendLine ( "<input type='text' "
       + "id='" + PageField.FieldId + EvName.NAME_GIVEN_FIELD_SUFFIX + "' "
       + "name='" + PageField.FieldId + EvName.NAME_GIVEN_FIELD_SUFFIX + "' "
       + "value='" + name.GivenName + "' "
      + "tabindex = '" + _TabIndex + "' "
       + "tabindex = '" + _TabIndex + "' "
       + "size='" + EvName.GivenName_Value_Width + "' class='form-control' data-parsley-trigger=\"change\" " );

      if ( PageField.Mandatory == true && PageField.EditAccess != false )
      {
        //sbHtml.Append ( " required " );
      }

      //this.addMandatoryIfAttribute ( sbHtml, PageField );

      if ( PageField.EditAccess == false )
      {
        sbHtml.Append ( " disabled='disabled' " );
      }

      sbHtml.Append ( "/></div>\r\n" );

      this._TabIndex++;

      if ( stFormat.Contains ( EvName.NAME_FORMAT_MIDDLE_NAME ) == true )
      {
        sbHtml.AppendLine ( "<div style='display: inline-block;'>" );
        sbHtml.AppendLine ( "<input type='text' "
       + "id='" + PageField.FieldId + EvName.NAME_MIDDLE_FIELD_SUFFIX + "' "
       + "name='" + PageField.FieldId + EvName.NAME_MIDDLE_FIELD_SUFFIX + "' "
         + "value='" + name.MiddleName + "' "
        + "tabindex = '" + _TabIndex + "' "
         + "tabindex = '" + _TabIndex + "' "
         + "size='" + EvName.MiddleName_Value_Width + "' class='form-control' data-parsley-trigger=\"change\" " );

        if ( PageField.Mandatory == true && PageField.EditAccess != false )
        {
          //sbHtml.Append ( " required " );
        }

        //this.addMandatoryIfAttribute ( sbHtml, PageField );

        if ( PageField.EditAccess == false )
        {
          sbHtml.Append ( " disabled='disabled' " );
        }

        sbHtml.Append ( "/></div>\r\n" );

        this._TabIndex++;
      }

      //
      // Family Name field
      //
      sbHtml.Append ( "<div style='display: inline-block;'><input type='text' "
       + "id='" + PageField.FieldId + EvName.NAME_FAMILY_FIELD_SUFFIX + "' "
       + "name='" + PageField.FieldId + EvName.NAME_FAMILY_FIELD_SUFFIX + "' "
       + "value='" + name.FamilyName + "' "
       + "tabindex = '" + _TabIndex + "' "
       + "size='" + EvName.FamilyName_Value_Width + "' class='form-control' data-parsley-trigger=\"change\" " );

      if ( PageField.Mandatory == true && PageField.EditAccess != false )
      {
        //sbHtml.Append ( " required " );
      }

      //this.addMandatoryIfAttribute ( sbHtml, PageField );

      if ( PageField.EditAccess == false )
      {
        sbHtml.Append ( " disabled='disabled' " );
      }

      sbHtml.Append ( "/></div>\r\n" );

      this._TabIndex++;

      sbHtml.Append ( "</div></div>\r\n" );

      //
      // Insert the field footer elemements
      //
      this.createFieldFooter ( sbHtml, PageField );

    }//END Field Method

    // ===================================================================================
    /// <summary>
    /// This method creates a Name field html markup
    /// </summary>
    /// <param name="sbHtml">StringBuilder object.</param>
    /// <param name="PageField">Field object.</param>
    // ----------------------------------------------------------------------------------
    private void createAddressField(
      StringBuilder sbHtml,
      Evado.UniForm.Model.EuField PageField )
    {
      this.LogMethod ( "createAddressField" );
      //
      // Initialise the methods variables and objects.
      //
      int valueColumnWidth = this.UserSession.GroupFieldWidth;
      int titleColumnWidth = 100 - valueColumnWidth;
      bool displayPrompts = true;
      EvAddress address = new EvAddress ( PageField.Value );
      String stFieldValueStyling = "style='width:" + valueColumnWidth + "%' class='cell value cell-input-address-value cf' ";

      if ( PageField.hasParameter ( Evado.UniForm.Model.EuFieldParameters.Format ) == true )
      {
        displayPrompts = PageField.GetParameterBoolean ( Evado.UniForm.Model.EuFieldParameters.Format );
      }
      //
      // Ineert the field header
      //
      this.createFieldHeader ( sbHtml, PageField, titleColumnWidth, false );

      //
      // Insert the field elements
      //
      sbHtml.Append ( "<div " + stFieldValueStyling + " > "
        + "<span id='sp" + PageField.Id + "'>" );

      //
      // Address 1 field
      //
      sbHtml.AppendLine ( "<div class='first' style='display: inline-block;'>" );
      if ( displayPrompts == true )
      {
        sbHtml.AppendLine ( "<span style='width:100px'>" + EuLabels.Address_Field_Address_1_Label + "</span>" );
      }

      sbHtml.AppendLine ( "<input type='text' "
       + "id='" + PageField.FieldId + "_Address1' "
       + "name='" + PageField.FieldId + "_Address1' "
       + "tabindex='" + _TabIndex + "' "
       + "value='" + address.Street + "' "
       + "size='40' "
       + "maxlength='50' "
       + "class='form-control' "
       + "sstyle='width:200px;inline-block;' data-parsley-trigger=\"change\" " );

      if ( PageField.Mandatory == true && PageField.EditAccess != false )
      {
        //sbHtml.Append ( " required " );
      }

      //this.addMandatoryIfAttribute ( sbHtml, PageField );

      if ( PageField.EditAccess == false )
      {
        sbHtml.Append ( " disabled='disabled' " );
      }

      sbHtml.Append ( "/></div>\r\n" );

      this._TabIndex++;

      //
      // Address 2 field
      //
      sbHtml.Append ( "<div style='display: inline-block;'>" );
      if ( displayPrompts == true )
      {
        sbHtml.AppendLine ( "<span style='width:100px'>" + EuLabels.Address_Field_Address_2_Label + "</span> " );
      }
      sbHtml.Append ( "<input type='text' id='" + PageField.FieldId + "_Address2' "
       + "name='" + PageField.FieldId + "_Address2' "
       + "tabindex='" + _TabIndex + "' "
       + "value='" + address.Unit + "' "
       + "size='40' "
       + "maxlength='50' "
       + "class='form-control' "
       + "style='width:200px;inline-block;' data-parsley-trigger=\"change\" " );

      if ( PageField.Mandatory == true && PageField.EditAccess != false )
      {
        //sbHtml.Append ( " required " );
      }

      //this.addMandatoryIfAttribute ( sbHtml, PageField );

      if ( PageField.EditAccess == false )
      {
        sbHtml.Append ( " disabled='disabled' " );
      }

      sbHtml.Append ( "/></div>\r\n" );

      this._TabIndex++;

      sbHtml.AppendLine ( "</br>" );
      //
      // Suburb field
      //
      sbHtml.AppendLine ( "<div style='display: inline-block;'> " );
      if ( displayPrompts == true )
      {
        sbHtml.AppendLine ( "<span style='width:100px'>" + EuLabels.Address_Field_City_Label + "</span>" );
      }
      sbHtml.AppendLine ( "<input type='text' "
       + "id='" + PageField.FieldId + "_Suburb' "
       + "name='" + PageField.FieldId + "_Suburb' "
       + "tabindex='" + _TabIndex + "' "
       + "value='" + address.City + "' "
       + "size='20' "
       + "maxlength='40' "
       + "class='form-control' "
       + "style='width:200px;inline-block;' data-parsley-trigger=\"change\" " );

      if ( PageField.Mandatory == true && PageField.EditAccess != false )
      {
        //sbHtml.Append ( " required " );
      }

      //this.addMandatoryIfAttribute ( sbHtml, PageField );

      if ( PageField.EditAccess == false )
      {
        sbHtml.Append ( " disabled='disabled' " );
      }

      sbHtml.Append ( "/></div>\r\n" );

      this._TabIndex++;

      //
      // State field
      //
      sbHtml.AppendLine ( "<div style='display: inline-block;'>" );
      if ( displayPrompts == true )
      {
        sbHtml.AppendLine ( "<span style='width:100px'>" + EuLabels.Address_Field_State_Label + "</span>" );
      }
      sbHtml.AppendLine ( "<input type='text' "
       + "id='" + PageField.FieldId + "_State' "
       + "name='" + PageField.FieldId + "_State' "
       + "tabindex='" + _TabIndex + "' "
       + "value='" + address.State + "' "
       + "size='8' "
       + "maxlength='8' "
       + "class='form-control' "
       + "style='width:100px;inline-block;' data-parsley-trigger=\"change\" " );

      if ( PageField.Mandatory == true && PageField.EditAccess != false )
      {
        //sbHtml.Append ( " required " );
      }

      //this.addMandatoryIfAttribute ( sbHtml, PageField );

      if ( PageField.EditAccess == false )
      {
        sbHtml.Append ( " disabled='disabled' " );
      }

      sbHtml.Append ( "/></div>\r\n" );

      this._TabIndex++;

      //
      //_PostCode field
      //
      sbHtml.AppendLine ( "<div style='display: inline-block;'>" );
      if ( displayPrompts == true )
      {
        sbHtml.AppendLine ( "<span style='width:100px'>" + EuLabels.Address_Field_Post_Code_Label + "</span>" );
      }
      sbHtml.AppendLine ( "<input type='text' "
       + "id='" + PageField.FieldId + "_PostCode' "
       + "name='" + PageField.FieldId + "_PostCode' "
       + "tabindex='" + _TabIndex + "' "
       + "value='" + address.PostCode + "' "
       + "size='8' maxlength='8' class='form-control' style='width:100px;inline-block;' data-parsley-trigger=\"change\" " );

      if ( PageField.Mandatory == true && PageField.EditAccess != false )
      {
        //sbHtml.Append ( " required " );
      }

      //this.addMandatoryIfAttribute ( sbHtml, PageField );

      if ( PageField.EditAccess == false )
      {
        sbHtml.Append ( " disabled='disabled' " );
      }

      sbHtml.Append ( "/></div>\r\n" );

      this._TabIndex++;

      sbHtml.AppendLine ( "</br>" );
      //
      // Country field
      //
      sbHtml.AppendLine ( "<div class='last' style='display: inline-block;' >" );
      if ( displayPrompts == true )
      {
        sbHtml.AppendLine ( "<span style='width:100px'>" + EuLabels.Address_Field_Country_Label + "</span>" );
      }
      sbHtml.AppendLine ( "<input type='text' "
         + "id='" + PageField.FieldId + "_Country' "
         + "name='" + PageField.FieldId + "_Country' "
         + "tabindex='" + _TabIndex + "' "
         + "value='" + address.Country + "' "
         + "size='20' "
         + "maxlength='20' "
         + "class='form-control' "
         + "style='width:200px;inline-block;' data-parsley-trigger=\"change\" " );

      if ( PageField.EditAccess == false )
      {
        sbHtml.Append ( " disabled='disabled' " );
      }

      sbHtml.Append ( "/></div>\r\n" );

      this._TabIndex++;

      sbHtml.Append ( "</span></div>\r\n" );

      this._TabIndex++;

      //
      // Insert the field footer elemements
      //
      this.createFieldFooter ( sbHtml, PageField );

    }//END Field Method

    // ===================================================================================
    /// <summary>
    /// This method creates a signature field html markup
    /// </summary>
    /// <param name="sbHtml">StringBuilder object.</param>
    /// <param name="PageField">Field object.</param>
    // ----------------------------------------------------------------------------------
    private void createSignatureField(
      StringBuilder sbHtml,
      Evado.UniForm.Model.EuField PageField )
    {
      this.LogMethod ( "createSignatureField" );
      this.LogValue ( "Field.Status: " + PageField.EditAccess );
      //
      // Initialise the methods variables and objects.
      //
      int valueColumnWidth = this.UserSession.GroupFieldWidth;
      int titleColumnWidth = 100 - valueColumnWidth;
      Evado.Model.EvRasterImage rasterImage = new Evado.Model.EvRasterImage ( );
      String image = String.Empty;
      String canvasWidth = "650";
      String canvasHeight = "225";
      bool fullWidth = false;
      String stFieldValueStyling = "style='width:" + valueColumnWidth + "%' class='cell value cell-input-email-value cf' ";

      PageField.Layout = EuFieldLayoutCodes.Left_Justified;

      this.LogValue ( "Set Field.Status: " + PageField.EditAccess );

      //
      // Ineert the field header
      //
      this.createFieldHeader ( sbHtml, PageField, titleColumnWidth, fullWidth );

      //
      // Set the canvas width and height
      //
      if ( PageField.hasParameter ( Evado.UniForm.Model.EuFieldParameters.Width ) == true )
      {
        canvasWidth = PageField.GetParameter ( Evado.UniForm.Model.EuFieldParameters.Width );
      }

      if ( PageField.hasParameter ( Evado.UniForm.Model.EuFieldParameters.Height ) == true )
      {
        canvasHeight = PageField.GetParameter ( Evado.UniForm.Model.EuFieldParameters.Height );
      }

      this.LogValue ( "Field Value: " + PageField.Value );

      if ( PageField.Value != String.Empty )
      {
        try
        {
          rasterImage = Newtonsoft.Json.JsonConvert.DeserializeObject<Evado.Model.EvRasterImage> ( PageField.Value );

          image = Newtonsoft.Json.JsonConvert.SerializeObject ( rasterImage.Image );
        }
        catch
        {
          image = String.Empty;
        }
      }

      this.LogValue ( "Raster Image: " + image );

      //
      // Insert the field elements
      //
      sbHtml.AppendLine ( "<div " + stFieldValueStyling + " > " );
      sbHtml.AppendLine ( "<div id='sp" + PageField.Id + "' class='sigPad' >" );

      sbHtml.AppendLine ( "<div id='sr" + PageField.Id + "' class='sigWrapper' > " );

      sbHtml.Append ( "<canvas id='cav" + PageField.Id + "' " );
      sbHtml.Append ( " class='pad'" );
      sbHtml.Append ( " width='" + canvasWidth + "px'" );
      sbHtml.Append ( " height='" + canvasHeight + "px' >" );
      sbHtml.AppendLine ( "</canvas>" );
      /*
      sbHtml.AppendLine ( "<canvas id='cav" + PageField.Id + "' class='pad'"
        + " width='100%' "
        + " height='50%' ></canvas>" );
      */
      if ( Global.DebugDisplayOn == true )
      {
        sbHtml.AppendLine ( "<input " );
        sbHtml.Append ( " type='text' " );
        sbHtml.Append ( " id='" + PageField.FieldId + "_sig' " );
        sbHtml.Append ( " name='" + PageField.FieldId + "_sig' " );
        sbHtml.Append ( "tabindex='" + _TabIndex + "' " );
        sbHtml.Append ( " class='output' " );
        sbHtml.AppendLine ( " size='10' /> " );
      }
      else
      {
        sbHtml.AppendLine ( "<input " );
        sbHtml.Append ( " type='hidden' " );
        sbHtml.Append ( " id='" + PageField.FieldId + "_sig' " );
        sbHtml.Append ( " name='" + PageField.FieldId + "_sig' " );
        sbHtml.Append ( " tabindex='" + _TabIndex + "' " );
        sbHtml.AppendLine ( " class='output' /> " );
      }

      if ( PageField.EditAccess == true )
      {
        sbHtml.AppendLine ( "<div class='sigNav menu links'>" );
        sbHtml.AppendLine ( "<span class='clearButton'>" );
        sbHtml.AppendLine ( "<a href='#clear' "
          + " class='btn btn-danger cmd-button'>" + EuLabels.Signature_Clear + "</a>" );
        sbHtml.AppendLine ( "</span>" );
        sbHtml.AppendLine ( "</div>" );
      }
      sbHtml.Append ( "</div>" );
      sbHtml.Append ( "</div>" );

      this._TabIndex += 2;
      /*
      sbHtml.AppendLine ( "<script type=\"text/javascript\">" );
      sbHtml.AppendLine ( "$(document).ready(function() { " );

      sbHtml.AppendLine ( "var width = document.getElementById('sr" + PageField.Id + "').scrollWidth;" );
      sbHtml.AppendLine ( "var width = width-20;" );
      sbHtml.AppendLine ( "var height = width/3;" );
     // sbHtml.AppendLine ( "alert( \"width: \" + width +  \"height: \" +height);" );

      sbHtml.AppendLine ( "var canv = document.getElementById('cav" + PageField.Id + "');" );
      sbHtml.AppendLine ( " canv.width = width;" );
      sbHtml.AppendLine ( " canv.height = height ;" );
      sbHtml.AppendLine ( "</script>" );
      */

      if ( PageField.EditAccess == false )
      {
        this.LogValue ( "Setting the signature for display only" );

        sbHtml.AppendLine ( "<script type=\"text/javascript\">" );
        sbHtml.AppendLine ( "$(document).ready(function() { " );
        sbHtml.AppendLine ( "var sig = document.getElementById('" + PageField.FieldId + "_input').value;" );
        //sbHtml.AppendLine ( "alert( \"value: \" + sig );" );
        sbHtml.AppendLine ( "console.log( \"Enabling the signature pad\" ); " );
        sbHtml.AppendLine ( "if (sig != \"\"){ " );
        sbHtml.AppendLine ( "var api = $('#sp" + PageField.Id + "').signaturePad({ displayOnly: true });" );
        sbHtml.AppendLine ( "api.regenerate(sig);" );
        sbHtml.AppendLine ( " } " );
        sbHtml.AppendLine ( " }); " );
        sbHtml.AppendLine ( "</script>" );

        if ( Global.DebugDisplayOn == true )
        {
          sbHtml.AppendLine ( "<input " );
          sbHtml.Append ( " type='text' " );
          sbHtml.Append ( " id='" + PageField.FieldId + "_input' " );
          sbHtml.Append ( " name='" + PageField.FieldId + "_input' " );
          sbHtml.Append ( " tabindex='" + _TabIndex + "' " );
          sbHtml.AppendLine ( " value='" + image + "' /> " );

          this._TabIndex += 2;
        }
        else
        {
          sbHtml.Append ( "<input " );
          sbHtml.Append ( " type='hidden' " );
          sbHtml.Append ( " id='" + PageField.FieldId + "_input' " );
          sbHtml.Append ( " name='" + PageField.FieldId + "_input' " );
          sbHtml.Append ( " tabindex='" + _TabIndex + "' " );
          sbHtml.AppendLine ( " value='" + image + "' /> " );

          this._TabIndex += 2;
        }
      }
      else
      {
        this.LogValue ( "Setting the raster image for draw an image." );

        sbHtml.AppendLine ( "<script type=\"text/javascript\">" );
        sbHtml.AppendLine ( "$(document).ready(function() { " );
        sbHtml.AppendLine ( "console.log( \"Enabling the raster image pad\" ); " );
        sbHtml.AppendLine ( "$('#sp" + PageField.Id + "').signaturePad({ drawOnly: true, validateFields: false,  lineTop: " + canvasHeight + " });" );
        sbHtml.AppendLine ( " }); " );
        sbHtml.AppendLine ( "</script>" );
      }

      //
      // Insert the field footer elemements
      //
      this.createFieldFooter ( sbHtml, PageField );

    }//END Field Method

    // ===================================================================================
    /// <summary>
    /// This method creates a rastor graphic field html markup
    /// </summary>
    /// <param name="sbHtml">StringBuilder object containing html markup.</param>
    /// <param name="PageField">Field object.</param>
    // ----------------------------------------------------------------------------------
    private void createRastorGraphicField(
      StringBuilder sbHtml,
      Evado.UniForm.Model.EuField PageField )
    {
      this.LogMethod ( "createRastorGraphicField" );
      this.LogValue ( "Field.Status: " + PageField.EditAccess );
      //
      // Initialise the methods variables and objects.
      //
      int valueColumnWidth = this.UserSession.GroupFieldWidth;
      int titleColumnWidth = 100 - valueColumnWidth;
      Evado.Model.EvRasterImage rasterImage = new Evado.Model.EvRasterImage ( );
      String Image = String.Empty;
      int canvasWidth = 660;
      int canvasHeight = 440;
      String backgroundImageURL = String.Empty;
      bool fullWidth = false;
      String stFieldValueStyling = "style='width:" + valueColumnWidth + "%' class='cell value cell-input-email-value cf' ";

      PageField.Layout = EuFieldLayoutCodes.Left_Justified;

      this.LogValue ( "Set Field.Status: " + PageField.EditAccess );

      //
      // Ineert the field header
      //
      this.createFieldHeader ( sbHtml, PageField, titleColumnWidth, fullWidth );

      //
      // Set the canvas width and height
      //
      if ( PageField.hasParameter ( Evado.UniForm.Model.EuFieldParameters.Width ) == true )
      {
        canvasWidth = PageField.GetParameterInt ( Evado.UniForm.Model.EuFieldParameters.Width );
      }

      if ( PageField.hasParameter ( Evado.UniForm.Model.EuFieldParameters.Height ) == true )
      {
        canvasHeight = PageField.GetParameterInt ( Evado.UniForm.Model.EuFieldParameters.Height );
      }

      if ( PageField.hasParameter ( Evado.UniForm.Model.EuFieldParameters.Background_Image_URL ) == true )
      {
        backgroundImageURL = PageField.GetParameter ( Evado.UniForm.Model.EuFieldParameters.Background_Image_URL );
      }

      this.LogValue ( "Parameters:  Width: {0}, Height: {1}, Backgroun URL {2}.",
        canvasWidth, canvasHeight, backgroundImageURL );

      this.LogValue ( "Field Value: " + PageField.Value );

      if ( PageField.Value != String.Empty )
      {
        try
        {
          rasterImage = Newtonsoft.Json.JsonConvert.DeserializeObject<Evado.Model.EvRasterImage> ( PageField.Value );

          Image = Newtonsoft.Json.JsonConvert.SerializeObject ( rasterImage.Image );
        }
        catch
        {
          Image = String.Empty;
        }
      }

      this.LogValue ( "Raster Signature: " + Image );

      //
      // Insert the field elements
      //
      sbHtml.AppendLine ( "<div " + stFieldValueStyling + " > " );
      sbHtml.AppendLine ( "<div id='sp" + PageField.Id + "' class='sigPad' >" );

      sbHtml.AppendLine ( "<div id='sr" + PageField.Id + "' class='sigWrapper' > " );

      sbHtml.Append ( "<canvas id='cav" + PageField.Id + "' " );
      sbHtml.Append ( " class='pad'" );
      sbHtml.Append ( " width='" + canvasWidth + "px'" );
      sbHtml.Append ( " height='" + canvasHeight + "px' >" );
      sbHtml.AppendLine ( "</canvas>" );
      /*
      sbHtml.AppendLine ( "<canvas id='cav" + PageField.Id + "' class='pad'"
        + " width='100%' "
        + " height='50%' ></canvas>" );
      */
      if ( Global.DebugDisplayOn == true )
      {
        sbHtml.AppendLine ( "<input " );
        sbHtml.Append ( " type='text' " );
        sbHtml.Append ( " id='" + PageField.FieldId + "_sig' " );
        sbHtml.Append ( " name='" + PageField.FieldId + "_sig' " );
        sbHtml.Append ( "tabindex='" + _TabIndex + "' " );
        sbHtml.Append ( " class='output' " );
        sbHtml.AppendLine ( " size='10' /> " );
      }
      else
      {
        sbHtml.AppendLine ( "<input " );
        sbHtml.Append ( " type='hidden' " );
        sbHtml.Append ( " id='" + PageField.FieldId + "_sig' " );
        sbHtml.Append ( " name='" + PageField.FieldId + "_sig' " );
        sbHtml.Append ( " tabindex='" + _TabIndex + "' " );
        sbHtml.AppendLine ( " class='output' /> " );
      }

      this._TabIndex += 2;

      sbHtml.AppendLine ( "</div>" );

      if ( PageField.EditAccess == true )
      {
        sbHtml.AppendLine ( "<div class='sigNav menu links'>" );
        sbHtml.AppendLine ( "<span class='clearButton'>" );
        sbHtml.AppendLine ( "<a href='#clear' "
          + " class='btn btn-danger cmd-button'>" + EuLabels.Image_Clear + "</a>" );
        sbHtml.AppendLine ( "</span>" );
        sbHtml.AppendLine ( "</div>" );
      }
      sbHtml.Append ( "</div>" );
      sbHtml.Append ( "</div>" );

      this._TabIndex += 2;
      /*
      sbHtml.AppendLine ( "<script type=\"text/javascript\">" );
      sbHtml.AppendLine ( "$(document).ready(function() { " );

      sbHtml.AppendLine ( "var width = document.getElementById('sr" + PageField.Id + "').scrollWidth;" );
      sbHtml.AppendLine ( "var width = width-20;" );
      sbHtml.AppendLine ( "var height = width/3;" );
     // sbHtml.AppendLine ( "alert( \"width: \" + width +  \"height: \" +height);" );

      sbHtml.AppendLine ( "var canv = document.getElementById('cav" + PageField.Id + "');" );
      sbHtml.AppendLine ( " canv.width = width;" );
      sbHtml.AppendLine ( " canv.height = height ;" );
      sbHtml.AppendLine ( "</script>" );
      */

      if ( PageField.EditAccess == false )
      {
        this.LogValue ( "Setting the signature for display only" );

        sbHtml.AppendLine ( "<script type=\"text/javascript\">" );
        sbHtml.AppendLine ( "$(document).ready(function() { " );
        sbHtml.AppendLine ( "var sig = document.getElementById('" + PageField.FieldId + "_input').value;" );
        //sbHtml.AppendLine ( "alert( \"value: \" + sig );" );
        sbHtml.AppendLine ( "console.log( \"Enabling the signature pad\" ); " );
        sbHtml.AppendLine ( "if (sig != \"\"){ " );
        sbHtml.AppendLine ( "var api = $('#sp" + PageField.Id + "').signaturePad({ displayOnly: true });" );
        sbHtml.AppendLine ( "api.regenerate(sig);" );
        sbHtml.AppendLine ( " } " );
        sbHtml.AppendLine ( " }); " );
        sbHtml.AppendLine ( "</script>" );

        if ( Global.DebugDisplayOn == true )
        {
          sbHtml.AppendLine ( "<input " );
          sbHtml.Append ( " type='text' " );
          sbHtml.Append ( " id='" + PageField.FieldId + "_input' " );
          sbHtml.Append ( " name='" + PageField.FieldId + "_input' " );
          sbHtml.Append ( " tabindex='" + _TabIndex + "' " );
          sbHtml.AppendLine ( " value='" + Image + "' /> " );

          this._TabIndex += 2;
        }
        else
        {
          sbHtml.Append ( "<input " );
          sbHtml.Append ( " type='hidden' " );
          sbHtml.Append ( " id='" + PageField.FieldId + "_input' " );
          sbHtml.Append ( " name='" + PageField.FieldId + "_input' " );
          sbHtml.Append ( " tabindex='" + _TabIndex + "' " );
          sbHtml.AppendLine ( " value='" + Image + "' /> " );

          this._TabIndex += 2;
        }
      }
      else
      {
        this.LogValue ( "Setting the signature for draw a signature" );

        sbHtml.AppendLine ( "<script type=\"text/javascript\">" );
        sbHtml.AppendLine ( "$(document).ready(function() { " );
        sbHtml.AppendLine ( "console.log( \"Enabling the signature pad\" ); " );
        sbHtml.AppendLine ( "$('#sp" + PageField.Id + "').signaturePad({ drawOnly: true, validateFields: false,  lineTop: " + canvasHeight + " });" );
        sbHtml.AppendLine ( " }); " );
        sbHtml.AppendLine ( "</script>" );
      }

      //
      // Insert the field footer elemements
      //
      this.createFieldFooter ( sbHtml, PageField );

      this.LogMethodEnd ( "createRastorGraphicField" );

    }//END Field Method

    // ===================================================================================
    /// <summary>
    /// This method creates a text field html markup
    /// </summary>
    /// <param name="sbHtml">StringBuilder containing the page html</param>
    /// <param name="PageField">Field object.</param
    // ----------------------------------------------------------------------------------
    private void createPasswordField(
      StringBuilder sbHtml,
      Evado.UniForm.Model.EuField PageField )
    {
      this.LogMethod ( "createPasswordField" );
      //
      // Initialise the methods variables and objects.
      //
      int valueColumnWidth = this.UserSession.GroupFieldWidth;
      int titleColumnWidth = 100 - valueColumnWidth;
      String stSize = PageField.GetParameter ( Evado.UniForm.Model.EuFieldParameters.Width );
      String stRows = PageField.GetParameter ( Evado.UniForm.Model.EuFieldParameters.Height );
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
      this.createFieldHeader ( sbHtml, PageField, titleColumnWidth, false );

      //
      // Insert the field data control
      //
      sbHtml.Append ( "<div " + stFieldValueStyling + " > "
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

      if ( PageField.Mandatory == true && PageField.EditAccess != false )
      {
        //sbHtml.Append ( " required " );
      }

      //this.addMandatoryIfAttribute ( sbHtml, PageField );

      if ( PageField.EditAccess == false )
      {
        sbHtml.Append ( " disabled='disabled' " );
      }

      sbHtml.Append ( "/></span></div>\r\n" );

      this._TabIndex++;
      this._TabIndex++;

      //
      // Insert the field footer elemements
      //
      this.createFieldFooter ( sbHtml, PageField );

    }//END Field Method

    // ===================================================================================
    /// <summary>
    /// This method creates a text field html markup
    /// </summary>
    /// <param name="sbHtml">StringBuilder containing the page html</param>
    /// <param name="PageField">Field object.</param
    // ----------------------------------------------------------------------------------
    private void createHttpLinkField(
      StringBuilder sbHtml,
      Evado.UniForm.Model.EuField PageField )
    {
      this.LogMethod ( "createHttpLinkField" );
      //
      // Initialise the methods variables and objects.
      //
      int valueColumnWidth = this.UserSession.GroupFieldWidth;
      int titleColumnWidth = 100 - valueColumnWidth;
      int stWidth = 98;

      if ( PageField.hasParameter ( EuFieldParameters.Field_Value_Column_Width ) == true )
      {
        Evado.UniForm.Model.EuFieldValueWidths widthValue = PageField.ValueColumnWidth;
        valueColumnWidth = ( int ) widthValue;
        titleColumnWidth = 100 - valueColumnWidth;
      }

      string stValue = PageField.Value;
      string stLinkUrl = PageField.Value;
      string stLinkTitle = PageField.Value;

      int index = stLinkUrl.LastIndexOf ( '/' );
      if ( index > 0 )
      {
        stLinkTitle = stLinkUrl.Substring ( index + 1 );
      }

      if ( stValue.Contains ( "^" ) == true )
      {
        string [ ] arrValue = stValue.Split ( '^' );
        stLinkUrl = arrValue [ 0 ];
        stLinkTitle = arrValue [ 1 ];
      }

      String stFieldValueStyling = "style='width:" + valueColumnWidth + "%' class='cell value cell-input-text-value cf' ";

      //
      // Ineert the field header
      //
      this.createFieldHeader ( sbHtml, PageField, titleColumnWidth, false );


      sbHtml.AppendLine ( "<div " + stFieldValueStyling + " > " );

      if ( stLinkUrl != String.Empty )
      {
        //
        // If in display model display the http link.
        //
        stLinkUrl = Global.concatinateHttpUrl ( Global.TempUrl, stLinkUrl );

        this.LogValue ( "Final URL: " + stLinkUrl );

        sbHtml.AppendLine ( "<div style=\"width:" + stWidth + "%;\" > " );

        sbHtml.AppendLine ( "<span>" );
        sbHtml.AppendLine ( "<strong>" );
        sbHtml.AppendLine ( "<a href='" + stLinkUrl + "' target='_blank' tabindex = '" + this._TabIndex + "' >" + stLinkTitle + "</a>" );
        sbHtml.AppendLine ( "</strong>" );
        sbHtml.AppendLine ( "</span>" );

        sbHtml.AppendLine ( "</div>" );
      }

      //
      // If in edit mode display the data enty fields.
      //
      if ( PageField.EditAccess == true )
      {
        //
        // Insert the field data control
        //
        sbHtml.AppendLine ( "<table style='width:98%'><tr>" );

        sbHtml.AppendLine ( "<td style='width:10%; text-align:right;'>" );
        sbHtml.AppendLine ( EuLabels.Html_Url_Field_title );
        sbHtml.AppendLine ( "</td>" );
        sbHtml.AppendLine ( "<td>" );
        sbHtml.AppendLine ( "<input type='text' "
           + "id='" + PageField.FieldId + Evado.UniForm.Model.EuField.CONST_HTTP_URL_FIELD_SUFFIX + "' "
           + "name='" + PageField.FieldId + Evado.UniForm.Model.EuField.CONST_HTTP_URL_FIELD_SUFFIX + "' "
           + "value='" + stLinkUrl + "' "
           + "tabindex = '" + _TabIndex + "' "
           + "maxlength='100' "
           + "size='50' "
           + "/>" );
        sbHtml.AppendLine ( "</td></tr>" );

        sbHtml.AppendLine ( "<tr><td style='text-align:right;'>" );
        sbHtml.AppendLine ( EuLabels.Html_Url_Title_Field_Title );
        sbHtml.AppendLine ( "</td>" );
        sbHtml.AppendLine ( "<td>" );
        sbHtml.AppendLine ( "<input type='text' "
           + "id='" + PageField.FieldId + Evado.UniForm.Model.EuField.CONST_HTTP_TITLE_FIELD_SUFFIX + "' "
           + "name='" + PageField.FieldId + Evado.UniForm.Model.EuField.CONST_HTTP_TITLE_FIELD_SUFFIX + "' "
           + "value='" + stLinkTitle + "' "
           + "tabindex = '" + _TabIndex + "' "
           + "maxlength='100' "
           + "size='50' " );

        sbHtml.AppendLine ( "/>" );

        sbHtml.AppendLine ( "</td>" );
        sbHtml.AppendLine ( "</tr></table>" );
      }


      sbHtml.AppendLine ( "<input type='hidden' "
           + "id='" + PageField.FieldId + "' "
           + "name='" + PageField.FieldId + "' "
           + "value='" + PageField.Value + "' /> " );
      this._TabIndex += 2;
      sbHtml.AppendLine ( "</div>" );

      //
      // Insert the field footer elemements
      //
      this.createFieldFooter ( sbHtml, PageField );

      this.LogMethodEnd ( "createHttpLinkField" );

    }//END Field Method

    // ===================================================================================
    /// <summary>
    /// This method creates a free test field html markup
    /// </summary>
    /// <param name="sbHtml">StringBuilder containing the page html</param>
    /// <param name="PageField">Field object.</param
    // ----------------------------------------------------------------------------------
    private void createStreamedVideoField(
      StringBuilder sbHtml,
      Evado.UniForm.Model.EuField PageField )
    {
      this.LogMethod ( "createStreamedVideoField" );
      //
      // Initialise the methods variables and objects.
      //
      string value = PageField.Value.ToLower ( );
      int valueColumnWidth = this.UserSession.GroupFieldWidth;
      int titleColumnWidth = 100 - valueColumnWidth;
      int width = PageField.GetParameterInt ( Evado.UniForm.Model.EuFieldParameters.Width );
      int height = PageField.GetParameterInt ( Evado.UniForm.Model.EuFieldParameters.Height );
      String videoTitle = PageField.GetParameter ( Evado.UniForm.Model.EuFieldParameters.Value_Label );
      String stFieldValueStyling = "style='width:" + valueColumnWidth + "%' class='cell cell-display-text-value cf' ";
      String stVideoStreamParameters = String.Empty;
      String stVideoSource = String.Empty;
      bool fullWidth = false;


      if ( PageField.Layout == EuFieldLayoutCodes.Column_Layout )
      {
        fullWidth = true;
        stFieldValueStyling = "style='width:100%' class='cell cell-display-text-value cf' ";
      }

      //
      // Ineert the field header
      //
      createFieldHeader ( sbHtml, PageField, titleColumnWidth, fullWidth );


      sbHtml.AppendLine ( "<div " + stFieldValueStyling + " >" );
      //
      // get the video iFrame.
      //
      sbHtml.AppendLine ( this.getVideoIFrame ( PageField ) );

      //
      // the page is edit enabled display a field to collect the Video Url and title.
      //

      if ( PageField.EditAccess == true )
      {
        //
        // Insert the field data control
        //
        sbHtml.AppendLine ( "<table style='width:98%'><tr>" );

        sbHtml.AppendLine ( "<td style='width:10%; text-align:right;'>" );
        sbHtml.AppendLine ( "<span>" + EuLabels.Video_Url_Field_Title + "</span>" );
        sbHtml.AppendLine ( "</td>" );
        sbHtml.AppendLine ( "<td>" );
        sbHtml.AppendLine ( "<input type='text' "
           + "id='" + PageField.FieldId + "' "
           + "name='" + PageField.FieldId + "' "
           + "value='" + PageField.Value + "' "
           + "tabindex = '" + _TabIndex + "' "
           + "maxlength='100' "
           + "size='50' "
           + "/>" );
        sbHtml.AppendLine ( "</td>" );
        sbHtml.AppendLine ( "</tr></table>" );
      }
      else
      {
        sbHtml.AppendLine ( "<input type='hidden' "
           + "id='" + PageField.FieldId + "' "
           + "name='" + PageField.FieldId + "' "
           + "value='" + PageField.Value + "' "
           + "/>" );
      }
      sbHtml.AppendLine ( "</div>" );

      this._TabIndex += 2;

      //
      // Insert the field footer elemements
      //
      this.createFieldFooter ( sbHtml, PageField );

    }//END createStreamedvideoField Method

    // ===================================================================================
    /// <summary>
    /// This method creates a iframe containing a streamed video object
    /// </summary>
    /// <param name="PageField">Field object.</param>
    /// <returns>String html</returns>
    // ----------------------------------------------------------------------------------
    private String getVideoIFrame(
      Evado.UniForm.Model.EuField PageField )
    {
      this.LogMethod ( "getVideoIFrame" );
      //
      // Initialise the methods variables and objects.
      //
      StringBuilder sbHtml = new StringBuilder ( );
      string value = PageField.Value.ToLower ( );
      int width = PageField.GetParameterInt ( Evado.UniForm.Model.EuFieldParameters.Width );
      int height = PageField.GetParameterInt ( Evado.UniForm.Model.EuFieldParameters.Height );
      String videoTitle = PageField.GetParameter ( Evado.UniForm.Model.EuFieldParameters.Value_Label );
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

      this.LogValue ( "Video ID: " + value );
      this.LogValue ( "VideoSource: " + stVideoSource );

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
    /// <param name="sbHtml">StringBuilder containing the page html</param>
    /// <param name="PageField">Field object.</param
    // ----------------------------------------------------------------------------------
    private void createExternalImageField(
      StringBuilder sbHtml,
      Evado.UniForm.Model.EuField PageField )
    {
      this.LogMethod ( "createExternalImageField" );
      //
      // Initialise the methods variables and objects.
      //
      int valueColumnWidth = this.UserSession.GroupFieldWidth;
      int titleColumnWidth = 100 - valueColumnWidth;
      string sWidth = PageField.GetParameter ( Evado.UniForm.Model.EuFieldParameters.Width );
      string height = PageField.GetParameter ( Evado.UniForm.Model.EuFieldParameters.Height );
      String stRatio = PageField.GetParameter ( Evado.UniForm.Model.EuFieldParameters.Height_Width_ratio );
      String stFieldValueStyling = "style='width:" + valueColumnWidth + "%' class='cell cell-display-text-value cf' ";
      String stVideoStreamParameters = String.Empty;
      String stImageUrl = PageField.Value.ToLower ( );
      bool fullWidth = false;

      if ( PageField.Layout == EuFieldLayoutCodes.Column_Layout )
      {
        fullWidth = true;
        stFieldValueStyling = "style='width:100%' class='cell cell-display-text-value cf' ";
      }

      stImageUrl = Global.concatinateHttpUrl ( Global.StaticImageUrl, PageField.Value );

      this.LogDebug ( "stImageUrl: " + stImageUrl );

      if ( sWidth == String.Empty )
      {
        sWidth = "80%";
      }
      //
      // Ineert the field header
      //
      this.createFieldHeader ( sbHtml, PageField, titleColumnWidth, fullWidth );

      //
      // Insert the field elements
      //
      sbHtml.AppendLine ( "<div " + stFieldValueStyling + " style='position: relative; ' > " );

      sbHtml.Append ( "<img  " );
      sbHtml.Append ( "id='" + PageField.FieldId + "' " );
      sbHtml.Append ( "name='" + PageField.FieldId + "' " );
      sbHtml.Append ( "src='" + stImageUrl + "' " );
      sbHtml.Append ( "width='" + sWidth + "' " );
      if ( height != String.Empty )
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
    /// <param name="sbHtml">StringBuilder containing the page html</param>
    /// <param name="PageField">Field object.</param
    // ----------------------------------------------------------------------------------
    private void createChartField(
      StringBuilder sbHtml,
      EuField PageField )
    {
      this.LogMethod ( "createPlotChartField" );
      this.LogDebug ( "bodyWidthPixels: {0}.", this.bodyWidthPixels );

      if ( PageField.Chart == null )
      {
        this.LogDebug ( "EXIT: Chart object null " );
        this.LogMethodEnd ( "createPlotChartField" );
        return;
      }
      //
      // Initialise the methods variables and objects.
      //
      int valueColumnWidth = this.UserSession.GroupFieldWidth;
      int titleColumnWidth = 100 - valueColumnWidth;
      string placeHolder = PageField.FieldId.ToLower ( );
      string chartJs = PageField.Chart.GetJsData ( );
      String stWidth = PageField.GetParameter ( Evado.UniForm.Model.EuFieldParameters.Width );
      String stRatio = PageField.GetParameter ( Evado.UniForm.Model.EuFieldParameters.Height_Width_ratio );
      if ( PageField.Layout == EuFieldLayoutCodes.Column_Layout )
      {
        valueColumnWidth = 98;
        titleColumnWidth = 98;
      }
      this.LogDebug ( " stWidth: {0}, stRatio: {1}.", stWidth, stRatio );
      this.LogDebug ( "PageField.Chart.chartJs: \r\n{0}", chartJs );

      String stFieldValueStyling = "style='width:" + valueColumnWidth + "%' class='cell value cell-textarea-value cf' ";

      int iWidth = Convert.ToInt32 ( valueColumnWidth * this.bodyWidthPixels / 100 );
      float fRatio = 0.5F;
      //
      // Set default width
      //
      if ( stWidth != String.Empty )
      {
        iWidth = Evado.Model.EvStatics.getInteger ( stWidth );
      }
      if ( stRatio != String.Empty )
      {
        fRatio = Evado.Model.EvStatics.getFloat ( stRatio );
      }
      float fHeight = fRatio * iWidth;
      int iHeight = Convert.ToInt32 ( fHeight );

      this.LogDebug ( " iWidth: {0},fHeight: {1}, iHeight: {2}.", iWidth, fHeight, iHeight );

      switch ( PageField.Type )
      {
        case Evado.Model.EvDataTypes.Donut_Chart:
        case Evado.Model.EvDataTypes.Pie_Chart:
        {
          iHeight = iWidth;
          break;
        }
      }

      this.LogDebug ( "iWidth: {0}, iHeight: {1}. ", iWidth, iHeight );
      //
      // Ineert the field header
      //
      this.createFieldHeader ( sbHtml, PageField, titleColumnWidth, false );



      //
      // Insert the field elements
      //
      sbHtml.AppendLine ( "<div " + stFieldValueStyling + " > " );
      sbHtml.AppendLine ( "<div id='sp" + PageField.Id + "'>" );

      sbHtml.AppendFormat ( "<div class=\"{0}-container\" style=\"width: 98%;\">", placeHolder ); // height: {1}px;
      sbHtml.AppendFormat ( "<canvas id=\"{0}\" style=\"margin: auto;\" width=\"800\" height=\"450\" ></ canvas >", placeHolder, iWidth, iHeight );
      sbHtml.AppendLine ( "</div>" ); ;

      sbHtml.AppendFormat ( "<script> new Chart ( \"{0}\",{1} )</script > ", placeHolder, chartJs );


      sbHtml.AppendLine ( "</div>" );
      //sbHtml.AppendFormat ( "<textarea id='{0}-T' rows='20' cols='100' disabled='disabled' >{1}</textarea>", placeHolder, chartJs );
      sbHtml.AppendLine ( "</div>" );

      this._TabIndex += 2;

      //
      // Insert the field footer elemements
      //
      this.createFieldFooter ( sbHtml, PageField );

      this.LogMethodEnd ( "createPlotChartField" );

    }//END createPlotChartField Method

  }//END CLASS

}//END namespace
