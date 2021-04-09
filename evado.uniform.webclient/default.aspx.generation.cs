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

namespace Evado.UniForm.WebClient
{
  /// <summary>
  /// This is the code behind class for the home page.
  /// </summary>
  public partial class DefaultPage : EvPersistentPageState
  {
    private int _TabIndex = 0;

    // ==================================================================================
    /// <summary>
    /// This method uses the global ApplicationData object to generates the page layout.
    /// </summary>
    // ---------------------------------------------------------------------------------
    private void generatePage ( )
    {
      Global.LogClientMethod ( "generatePage method. " );
      Global.LogDebug ( "PageStatus: " + this._AppData.Page.EditAccess );
      Global.LogDebug ( "Page command list count: " + this._AppData.Page.CommandList.Count );
      //
      // initialise method variables and objects.
      //
      this.litPageContent.Visible = true;
      this.litPageMenu.Visible = true;
      this.litExitCommand.Visible = true;
      this.litCommandContent.Visible = true;
      this.litHeaderTitle.Visible = true;
      StringBuilder sbMainBody = new StringBuilder ( );
      StringBuilder sbLeftBody = new StringBuilder ( );
      StringBuilder sbCentreBody = new StringBuilder ( );
      StringBuilder sbRightBody = new StringBuilder ( );
      StringBuilder sbPageMenuPills = new StringBuilder ( );
      int leftColumnPercentage = 0;
      int rightColumnPercentage = 0;
      this.Title = Global.TitlePrefix + this._AppData.Title;
      this.litCommandContent.Visible = true;
      bool displayGroupsAsPanels = this._AppData.Page.GetDisplayGroupsAsPanels ( );

      //
      // Groups are displayed a panels enable and initialise the page objects.
      //
      if ( displayGroupsAsPanels == true )
      {
        this.PagedGroups.Visible = true;

        if ( this._PanelDisplayGroupIndex == -1 )
        {
          this._PanelDisplayGroupIndex = 0;
        }
      }

      //
      // load the java script libraries
      //
      this.loadJavaScriptLibraries ( );

      //
      // If the anonymous access mode exit.
      //

      Global.LogDebug ( "Anonymous_Page_Access: " + this._Anonymous_Page_Access );

      if ( this._Anonymous_Page_Access == false )
      {
        Global.LogDebug ( "Anonyous_Page_Access = false" );

        //
        // generate the page commands.
        //
        this.generatePageCommands ( );

        //
        // Generate the Page History menu
        //
        if ( this._CommandHistoryList.Count > 0 )
        {
          sbPageMenuPills.Append ( "<ol class='breadcrumb'>" );

          //
          // Iterate through the Command history to create the breadcrumbs.
          //
          foreach ( Evado.Model.UniForm.Command command in this._CommandHistoryList )
          {
            this.generateHistoryMenuPills ( sbPageMenuPills, command );
          }

          sbPageMenuPills.Append ( "</ol>" );
        }
      }//END Anonymous_Page_Access control.

      //
      // If an error message has been generated display it at the top of the 
      // page.
      //
      if ( this._AppData.Message != String.Empty )
      {
        this.generateErrorMessge ( sbMainBody );
      }


      string leftColumn = this._AppData.Page.GetParameter ( Model.UniForm.PageParameterList.Left_Column_Width );
      string rightColumn = this._AppData.Page.GetParameter ( Model.UniForm.PageParameterList.Right_Column_Width );

      if ( int.TryParse ( leftColumn, out leftColumnPercentage ) == false )
      {
        leftColumnPercentage = 0;
      }
      if ( int.TryParse ( rightColumn, out rightColumnPercentage ) == false )
      {
        rightColumnPercentage = 0;
      }
      int centreWidthPercentage = 100 - leftColumnPercentage - rightColumnPercentage;

      Global.LogDebug ( "leftColumnPercentage: " + leftColumnPercentage );
      Global.LogDebug ( "rightColumnPercentage: " + rightColumnPercentage );
      Global.LogDebug ( "centreWidthPercentage: " + centreWidthPercentage );

      //
      // Generate the group menu.
      //
      sbPageMenuPills.Append ( "<ul class='nav nav-pills'>" );
      sbPageMenuPills.Append ( "<li class='active'><a href='#'>All</a></li>" );

      //
      // Generate the html for each group in the page object.
      //
      if ( this._AppData.Page.GroupList.Count > 0 )
      {
        for ( int count = 0; count < this._AppData.Page.GroupList.Count; count++ )
        {
          Evado.Model.UniForm.Group group = this._AppData.Page.GroupList [ count ];

          //
          // skip null objects.
          //
          if ( group == null )
          {
            continue;
          }

          String pageColumn = group.GetParameter ( Model.UniForm.GroupParameterList.Page_Column );

          Global.LogDebug ( group.Title + " in column: " + pageColumn );

          //
          // Header fields are always at the top of the page.
          //
          if ( group.Layout == Model.UniForm.GroupLayouts.Page_Header
            || ( leftColumnPercentage == 0
              && rightColumnPercentage == 0 ) )
          {
            Global.LogDebug ( "ADD: " + group.Title + " to main body" );
            this.generateGroup ( sbMainBody, count, false, false );

            this.generatePageMenuPills ( sbPageMenuPills, group );

            continue;
          }

          //
          // if the left column exists and the group is allocated to the left column
          // place the group html in the left body.
          //
          if ( pageColumn == Evado.Model.UniForm.PageColumnCodes.Left.ToString ( )
            && leftColumnPercentage > 0 )
          {
            Global.LogDebug ( "ADD: " + group.Title + " to left column" );

            this._AppData.Page.GroupList [ count ].Layout = Evado.Model.UniForm.GroupLayouts.Full_Width;

            this.generateGroup ( sbLeftBody, count, true, false );

            this.generatePageMenuPills ( sbPageMenuPills, group );
            continue;
          }//END left column only

          //
          // if the right column exists and the group is allocated to the right column
          // place the group html in the right body.
          //
          if ( pageColumn == Evado.Model.UniForm.PageColumnCodes.Right.ToString ( )
            && rightColumnPercentage > 0 )
          {
            Global.LogDebug ( "ADD: " + group.Title + " to right column" );

            this._AppData.Page.GroupList [ count ].Layout = Evado.Model.UniForm.GroupLayouts.Full_Width;

            this.generateGroup ( sbRightBody, count, false, false );

            this.generatePageMenuPills ( sbPageMenuPills, group );

            continue;
          }//END 

          //
          // else place is in the central body.
          //
          Global.LogDebug ( "ADD: " + group.Title + " to center column" );
          this.generateGroup ( sbCentreBody, count, true, displayGroupsAsPanels );

          this.generatePageMenuPills ( sbPageMenuPills, group );

        }//END Group interation loop

      }//END multiple groups.

      sbPageMenuPills.Append ( "</ul>" );

      if ( sbLeftBody.Length > 0
        || sbCentreBody.Length > 0
        || sbRightBody.Length > 0 )
      {
        Global.LogDebug ( "Columns exist." );
        Global.LogDebug ( "Left column length: " + sbLeftBody.Length );
        Global.LogDebug ( "Right column length: " + sbRightBody.Length );

        sbMainBody.AppendLine ( "<!-- OPENING BODY COLUMNS -->" );
        sbMainBody.AppendLine ( "<div style='display:inline-block; width:98%; margin:0; padding:0;'>" );

        if ( sbLeftBody.Length == 0
          && sbCentreBody.Length > 0
          && sbRightBody.Length == 0 )
        {
          Global.LogDebug ( "Side columns empty." );

          sbMainBody.AppendLine ( sbCentreBody.ToString ( ) );
        }//END only centre body.
        else
        {
          Global.LogDebug ( "Side columns have content." );

          if ( sbLeftBody.Length > 0
            && sbCentreBody.Length > 0
            && sbRightBody.Length == 0 )
          {
            Global.LogDebug ( "Add Left column to body (no left column)" );

            sbMainBody.AppendLine ( "<!-- OPENING LEFT BODY COLUMN -->" );
            sbMainBody.AppendLine ( "<div style='width:" + leftColumnPercentage + "%;  float: left;'>" );

            sbMainBody.AppendLine ( sbLeftBody.ToString ( ) );

            sbMainBody.AppendLine ( "<!-- CLOSING LEFT BODY COLUMN -->" );
            sbMainBody.AppendLine ( "</div>" );

            Global.LogDebug ( "Add center column to body" );

            sbMainBody.AppendLine ( "<!-- CENTER CENTER BODY COLUMN -->" );
            sbMainBody.AppendLine ( "<div style='margin-left:" + ( leftColumnPercentage + 1 ) + "%;width: " + ( centreWidthPercentage - 1 ) + "%' >" );

            sbMainBody.AppendLine ( sbCentreBody.ToString ( ) );

            sbMainBody.AppendLine ( "<!-- CLOSING LEFT BODY COLUMN -->" );
            sbMainBody.AppendLine ( "</div>" );
          }
          else
          {
            if ( sbLeftBody.Length == 0
              && sbCentreBody.Length > 0
              && sbRightBody.Length > 0 )
            {
              Global.LogDebug ( "Add right column to body (no left column)" );

              sbMainBody.AppendLine ( "<!-- OPENING LEFT BODY COLUMN -->" );
              sbMainBody.AppendLine ( "<div style='width:" + rightColumnPercentage + "%; '>" );

              sbMainBody.AppendLine ( sbRightBody.ToString ( ) );

              sbMainBody.AppendLine ( "<!-- CLOSING LEFT BODY COLUMN -->" );
              sbMainBody.AppendLine ( "</div>" );

              Global.LogDebug ( "Add center column to body" );

              sbMainBody.AppendLine ( "<!-- CENTER CENTER BODY COLUMN -->" );
              sbMainBody.AppendLine ( "<div style='margin-left:0; width:" + ( centreWidthPercentage - 2 ) + "%' >" );

              sbMainBody.AppendLine ( sbCentreBody.ToString ( ) );

              sbMainBody.AppendLine ( "<!-- CLOSING CENTER BODY COLUMN -->" );
              sbMainBody.AppendLine ( "</div>" );
            }
            else
            {
              Global.LogDebug ( "Add left column to body" );

              sbMainBody.AppendLine ( "<!-- OPENING LEFT BODY COLUMN -->" );
              sbMainBody.AppendLine ( "<div style='width:" + leftColumnPercentage + "%; float:left'>" );

              sbMainBody.AppendLine ( sbLeftBody.ToString ( ) );

              sbMainBody.AppendLine ( "</div>" );
              sbMainBody.AppendLine ( "<!-- CLOSING LEFT BODY COLUMN -->" );

              Global.LogDebug ( "Add right column to body" );

              sbMainBody.AppendLine ( "<!-- OPENING RIGHT BODY COLUMN -->" );
              sbMainBody.AppendLine ( "<div style='width:" + rightColumnPercentage + "%; float:right'>" );

              sbMainBody.AppendLine ( sbRightBody.ToString ( ) );

              sbMainBody.AppendLine ( "</div>" );
              sbMainBody.AppendLine ( "<!-- CLOSING RIGHT BODY COLUMN -->" );

              Global.LogDebug ( "Add center column to body" );

              sbMainBody.AppendLine ( "<!-- CENTER CENTER BODY COLUMN -->" );
              sbMainBody.AppendLine ( "<div style='margin-left:" + ( leftColumnPercentage + 1 ) + "%; width:" + ( centreWidthPercentage - 2 ) + "%' >" );

              sbMainBody.AppendLine ( sbCentreBody.ToString ( ) );

              sbMainBody.AppendLine ( "</div>" );
              sbMainBody.AppendLine ( "<!-- CLOSING CENTER BODY COLUMN -->" );
            }
          }
          sbMainBody.AppendLine ( "</div>" );
          sbMainBody.AppendLine ( "<!-- CLOSING BODY COLUMNS -->" );
        }//END only centre body.
      }
      this.litPageContent.Text = sbMainBody.ToString ( );

      this.litPageMenu.Text = sbPageMenuPills.ToString ( );

      Global.LogDebugMethodEnd ( "generatePage" );
    }//END generatePage method

    // ==================================================================================
    /// <summary>
    /// This method loads the page's javascript libraries
    /// </summary>
    // ---------------------------------------------------------------------------------
    private void loadJavaScriptLibraries ( )
    {
      Global.LogDebugMethod ( "loadJavaScriptLibraries method. " );
      Global.LogDebug ( "RelativeBinaryDownloadURL: " + Global.RelativeBinaryDownloadURL );

      //
      // Exit if there is not Js Library
      if ( this._AppData.Page.JsLibrary == String.Empty )
      {
        return;
      }

      //
      // load the page id for the java scripts.
      //
      this.pageId.Value = this._AppData.Page.PageId;

      //
      // Get an array of library file references.
      // it is assumed these files are residing in the image or binary file URL.
      //
      if ( this._AppData.Page.JsLibrary != null )
      {
        string [ ] arJsLibraries = this._AppData.Page.JsLibrary.Split ( ';' );

        Global.LogDebug ( "Libary count: " + arJsLibraries.Length );

        //
        // reset the JS library value.
        //
        this.litJsLibrary.Text = String.Empty;

        //
        // Iterate through teh library references adding them to the store.
        //
        for ( int i = 0; i < arJsLibraries.Length; i++ )
        {
          string stJsLibaryUrl = Global.RelativeBinaryDownloadURL + arJsLibraries [ i ].Trim ( );

          this.litJsLibrary.Text += "<script type=\"text/javascript\" src=\"" + stJsLibaryUrl + "\"></script>\r\n";
        }
      }

    }//END loadJavaScriptLibraries method

    // ==================================================================================
    /// <summary>
    /// This mehod generates the HTMl for a page group.
    /// </summary>
    /// <param name="command">Evado.Model.UniForm.Command command object</param>
    /// <param name="cssClass">String: Css classes</param>
    /// <returns>Html string</returns>
    // ---------------------------------------------------------------------------------
    private String encodeMarkDown (
      String MarkDownText )
    {
      Global.LogDebugMethod ( "encodeMarkDown method. " );
      //Global.LogDebugValue ( "Text length: " + MarkDownText.Length );
      //
      // Initialise the methods variables and objects.
      //
      String stMarkDown = String.Empty;

      //
      // Process the text to remove spaces.
      //
      MarkDownText = MarkDownText.Replace ( "\r\n", "~" );
      MarkDownText = MarkDownText.Replace ( "\r", "~" );
      MarkDownText = MarkDownText.Replace ( "\n", "~" );
      MarkDownText = MarkDownText.Replace ( "~~", "~" );

      string [ ] arrMarkDownText = MarkDownText.Split ( '~' );
      foreach ( String str in arrMarkDownText )
      {
        stMarkDown += str.Trim ( ) + "\r\n";
      }
      //Global.LogDebugValue ( stMarkDown );
      //Global.LogDebugValue ( "Processed test length: " + stMarkDown.Length );

      //
      // Initialise the markdown options object.
      //
      MarkdownSharp.MarkdownOptions markDownOptions = new MarkdownSharp.MarkdownOptions ( );
      markDownOptions.AutoHyperlink = true;
      markDownOptions.AutoNewlines = true;
      markDownOptions.EmptyElementSuffix = "/>";
      markDownOptions.EncodeProblemUrlCharacters = true;
      markDownOptions.LinkEmails = true;
      markDownOptions.StrictBoldItalic = true;

      //
      // Initialise the markdown object.
      //
      MarkdownSharp.Markdown markDown = new MarkdownSharp.Markdown ( markDownOptions );

      string stHtml = markDown.Transform ( stMarkDown );

      //
      // convert old markup to html.
      //
      stHtml = stHtml.Replace ( "[i]", "<i>" );
      stHtml = stHtml.Replace ( "[/i]", "</i>" );
      stHtml = stHtml.Replace ( "[b]", "<b>" );
      stHtml = stHtml.Replace ( "[/b]", "</b>" );
      stHtml = stHtml.Replace ( "[u]", "<u>" );
      stHtml = stHtml.Replace ( "[/u]", "</u>" );
      stHtml = stHtml.Replace ( "<blockquote>", "" );
      stHtml = stHtml.Replace ( "</blockquote>", "" );

      //Global.LogDebugValue ( "MarkDown HTML: " + stHtml );

      return stHtml;

    }//END encodeMarkDown method.

    // ==================================================================================
    /// <summary>
    /// Write Debug comments property.
    /// </summary>
    // ---------------------------------------------------------------------------------
    private void generateErrorMessge ( StringBuilder sbHtml )
    {
      Global.LogDebugMethod ( "generateErrorMessge method. " );

      //
      // Define the error group.
      //
      Evado.Model.UniForm.Group errorGroup = new Evado.Model.UniForm.Group (
        "Message",
        Evado.Model.EvStatics.getStringAsHtml ( this._AppData.Message ),
        Evado.Model.UniForm.EditAccess.Disabled );
      errorGroup.Layout = Evado.Model.UniForm.GroupLayouts.Full_Width;

      if ( this._AppData.Message.ToLower ( ).Contains ( "error" ) == true )
      {
        errorGroup.AddParameter ( Model.UniForm.GroupParameterList.BG_Default, Model.UniForm.Background_Colours.Red );
      }

      //
      // set the field set attributes.
      //
      this.generateGroupHeader ( sbHtml, errorGroup, false );

      //
      // Close the field set tag.
      //
      this.generateGroupFooter ( sbHtml );

    }

    // ==================================================================================
    /// <summary>
    /// This method generated the page commands.  
    /// As postback is needed to collect page field values, page Command buttons are used to 
    /// initiate the Command rather than links.  These buttons are displayed approprately
    /// when the relevant Command is required.
    /// </summary>
    // ---------------------------------------------------------------------------------
    private void generatePageCommands ( )
    {
      Global.LogDebugMethod ( "generateCommands method. " );
      Global.LogDebug ( "CommandList.Count: " + this._AppData.Page.CommandList.Count );

      //
      // Initialise the methods variables and objects.
      //
      int linkIndex = 0;
      StringBuilder stHtml = new StringBuilder ( );

      //
      // Set page header title.
      //
      this.litHeaderTitle.Text = this._AppData.Page.Title;

      //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
      //
      // IF the exit command object is null then no exit page or logout page is to be 
      // displayed.
      //
      if ( this._AppData.Page.Exit == null )
      {
        this.litExitCommand.Text = String.Empty;
      }
      else
      {
        //
        // Add a link to previous page.
        //
        if ( this._AppData.Page.Exit.Id != Guid.Empty
          || this._AppData.Page.Exit.Title != String.Empty )
        {
          if ( this._AppData.Page.Exit.Type != Evado.Model.UniForm.CommandTypes.Logout_Command )
          {
            this.litExitCommand.Text = this.createCommandLink ( this._AppData.Page.Exit );
            this.litExitCommand.Visible = true;
            linkIndex++;
          }
          else
          {
            if ( Global.AuthenticationMode != System.Web.Configuration.AuthenticationMode.Windows )
            {
              this.litExitCommand.Text = "<a "
                + "class='btn btn-danger' "
                + "href=\"javascript:onPostBack('Login')\" > Logout </a>\r\n";

              linkIndex++;
            }
          }
        }
        else
        {
          if ( Global.AuthenticationMode != System.Web.Configuration.AuthenticationMode.Windows
            && this._AppData.Status == Evado.Model.UniForm.AppData.StatusCodes.Login_Authenticated
            && this._AppData.Page.Exit.Title == String.Empty )
          {
            this.litExitCommand.Text = "<a "
              + "class='btn btn-danger' "
              + "href=\"javascript:onPostBack('Login')\" > Logout </a>\r\n";

            linkIndex++;
          }
          else
          {
            Global.LogDebug ( "Exit Command is EMPTY" );
          }
        }
      }

      //
      // Edit if there are not page commands.
      //
      if ( this._AppData.Page.CommandList.Count == 0 )
      {
        return;
      }

      stHtml.AppendLine ( "<ul id='Main_Menu'>" );
      //stHtml.AppendLine ( "\t<li id='Command' ><a class='btn' style='text-align:right;'><img src='./css/Command-Arrow.png' alt='>>'/></a>" );
      stHtml.AppendLine ( "\t<li id='Command' ><a class='btn' style='text-align:right;'><img src='./css/Menu-icon.png' alt='>>'/></a>" );
      stHtml.AppendLine ( "\t\t<ul>" );


      //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
      //
      // Iterate through the page Command list.
      //
      foreach ( Evado.Model.UniForm.Command command in this._AppData.Page.CommandList )
      {
        //
        // skip null commands
        //
        if ( command == null )
        {
          Global.LogDebug ( "Command is null." );
          continue;
        }

        Global.LogDebug ( "Command: " + command.Id + " >> " + command.Title );

        //stHtml.AppendLine ( this.createPageCommand ( Command ) );
        stHtml.AppendLine ( "\t\t\t<li>" + this.createPageCommandLink ( command ) + "</li>" );
        linkIndex++;

      }//END iteration loop.
      stHtml.AppendLine ( "\t\t</ul>" );
      stHtml.AppendLine ( "\t</li>" );
      stHtml.AppendLine ( "</ul>" );

      this.litCommandContent.Text = stHtml.ToString ( );

    }//END generatePageCommands method

    // ==================================================================================
    /// <summary>
    /// This mehod generates the HTMl for a page group.
    /// </summary>
    // ---------------------------------------------------------------------------------
    private String createPageCommandLink (
      Evado.Model.UniForm.Command command )
    {
      //Global.LogDebugMethod ( "createPageCommandLink method. " );
      //
      // Initialise methods variables and objects.
      //
      string html = String.Empty;
      if ( command.getEnableForMandatoryFields ( ) )
      {
        html = "<a "
          + "class='btn btn-danger cmd-button' style='width: 175px;' "
          + "href=\"javascript:onPostBack('" + command.Id + "')\" "
          + " data-enable-for-mandatory-fields "
          + ">" + this.renderCommandTitle ( command ) + "</a>\r\n";
      }
      else
      {
        if ( command.Type == Model.UniForm.CommandTypes.Html_Link )
        {
          string Link_Url = command.GetParameter ( Model.UniForm.CommandParameters.Link_Url );

          Global.LogDebug ( "Link_Url: " + Link_Url );

          html = "<a "
            + "class='btn btn-danger cmd-button' style='width: 175px;' "
            + "href=\"" + Link_Url + "\" target=\"_blank\" "
            + ">" + this.renderCommandTitle ( command ) + "</a>\r\n";
        }
        else
        {
          html = "<a "
            + "class='btn btn-danger cmd-button' style='width: 175px;' "
            + "href=\"javascript:onPostBack('" + command.Id + "')\" "
            + ">" + this.renderCommandTitle ( command ) + "</a>\r\n";
        }
      }

      return html;
    }

    // ==================================================================================
    /// <summary>
    /// This mehod generates the HTMl for a page group.
    /// </summary>
    /// <param name="command">Evado.Model.UniForm.Command command object</param>
    /// <param name="cssClass">String: Css classes</param>
    /// <returns>Html string</returns>
    // ---------------------------------------------------------------------------------
    private String createCommandLink (
      Evado.Model.UniForm.Command command,
      string cssClass = "btn btn-danger cmd-button" )
    {
      //Global.LogDebugMethod ( "createCommandLink method. " );
      //
      // Initialise methods variables and objects.
      //
      string html = String.Empty;
      html = "<a "
           + "class='" + cssClass + "' "
           + "href=\"javascript:onPostBack('" + command.Id + "')\" "
           + ( command.getEnableForMandatoryFields ( ) ? " data-enable-for-mandatory-fields " : "" )
           + ">" + this.renderCommandTitleNoImage ( command ) + "</a>\r\n";

      return html;
    }

    // ==================================================================================
    /// <summary>
    /// This method generates the HTMl for a menu pills.
    /// </summary>
    // ---------------------------------------------------------------------------------
    private void generateHistoryMenuPills (
      StringBuilder stHtml,
      Evado.Model.UniForm.Command command )
    {
      stHtml.Append ( "<li><a href=\"javascript:onPostBack('" + command.Id + "')\">" + command.Title + "</a></li>" );
    }

    // ==================================================================================
    /// <summary>
    /// This method generates the HTMl for a menu pills.
    /// </summary>
    // ---------------------------------------------------------------------------------
    private void generatePageMenuPills ( StringBuilder sbHtml, Evado.Model.UniForm.Group group )
    {
      Global.LogDebugMethod ( "generatePageMenuPills method. " );

      sbHtml.Append ( "<li><a href='#" + group.Id + "-grp'>" + group.Title + "</a></li>" );

      Global.LogDebugMethodEnd ( "generatePageMenuPills" );
    }

    // ==================================================================================
    /// <summary>
    /// This method generates the HTMl for a page group.
    /// </summary>
    // ---------------------------------------------------------------------------------
    private void generateGroup (
      StringBuilder sbHtml,
      int Index,
      bool EnableBodyColumns,
      bool EnablePanelDisplay )
    {
      Global.LogClientMethod ( "generateGroup method. " );
      Global.LogDebug ( "Index: " + Index );
      //
      // Initialise the methods variables and objects.
      //

      //
      // Extract the group object from the list.
      //
      this._CurrentGroup = this._AppData.Page.GroupList [ Index ];

      Global.LogDebug ( "Title: " + this._CurrentGroup.Title );
      Global.LogDebug ( "Group.Status: " + this._CurrentGroup.EditAccess );

      if ( this._CurrentGroup.EditAccess == Model.UniForm.EditAccess.Inherited )
      {
        this._CurrentGroup.EditAccess = this._AppData.Page.EditAccess;
      }
      Global.LogDebug ( "Update Group.Status: " + this._CurrentGroup.EditAccess );

      if ( this._CurrentGroup.FieldList.Count == 0
        && this._CurrentGroup.CommandList.Count == 0
        && this._CurrentGroup.Description == null )
      {
        Global.LogDebug ( "EXIT METHOD: Empty Group" );
        return;
      }

      this._GroupValueColumWidth = 60;
      Model.UniForm.FieldValueWidths widthValue = this._CurrentGroup.getValueColumnWidth ( );
      this._GroupValueColumWidth = (int) widthValue;

      //
      // Set the edit access.
      //
      Evado.Model.UniForm.EditAccess groupStatus = this._CurrentGroup.EditAccess;

      if ( this._CurrentGroup.EditAccess == Evado.Model.UniForm.EditAccess.Inherited )
      {
        groupStatus = this._AppData.Page.EditAccess;
      }

      //
      // set the field set attributes.
      //
      this.generateGroupHeader ( sbHtml, this._CurrentGroup, EnableBodyColumns );

      //
      // Generate the groupd fields.
      //
      this.generateGroupFields ( sbHtml, Index, this._CurrentGroup );

      //
      // Generate the groups commands.
      //
      this.generateGroupCommands ( sbHtml, this._CurrentGroup );

      //
      // Close the field set tag.
      //
      this.generateGroupFooter ( sbHtml );

    }//END generateGroup method

    // ===================================================================================
    /// <summary>
    /// This method creates a test field html markup
    /// </summary>
    /// <param name="PageGroup">The group obbject</param>
    /// <returns>String html</returns>
    // ----------------------------------------------------------------------------------
    private void generateGroupHeader (
      StringBuilder sbHtml,
      Evado.Model.UniForm.Group PageGroup,
      bool EnableBodyColumns )
    {
      Global.LogClientMethod ( "generateGroupHeader method." );
      Global.LogDebug ( "Group: " + PageGroup.Title );
      Global.LogDebug ( "Group.Layout: " + PageGroup.Layout );
      Global.LogDebug ( "EnableBodyColumns: " + EnableBodyColumns );
      //
      // Initialise the methods variables and objects.
      //
      int inPixelWidth = PageGroup.GetParameterInt ( Evado.Model.UniForm.GroupParameterList.Pixel_Width );
      int inPixelHeight = PageGroup.GetParameterInt ( Evado.Model.UniForm.GroupParameterList.Pixel_Height );
      string stFieldName = PageGroup.GetParameter ( Evado.Model.UniForm.GroupParameterList.Hide_Group_If_Field_Id );
      string stFieldValue = PageGroup.GetParameter ( Evado.Model.UniForm.GroupParameterList.Hide_Group_If_Field_Value );

      Global.LogDebug ( "stFieldName: " + stFieldName );

      if ( stFieldName != String.Empty )
      {
        Global.LogDebug ( "Group is dynamically displayed. " );
        Guid fieldGuid = this.getField_ID ( stFieldName );

        String stJavaScript = "<script type=\"text/javascript\">\r\n"
          + @"//This script displays and hides groups: " + PageGroup.Title + "\r\n"
          + "document.getElementById(\"" + PageGroup.Id + "-grp\").style.visibility = \"hidden\"; \r\n"
          + "\r\n"
          + "var value = document.getElementById(\"" + fieldGuid + "\").value; \r\n"
          + "alert(  \"Field \" +  " + stFieldName + ", \" value\" +  value )\r\n"
          + "if ( value == \"" + stFieldValue + "\" )\r\n"
          + " { document.getElementById(\"" + PageGroup.Id + "-grp\").style.visibility = \"visible\";}\r\n "
          + "\r\n</script>";

        this.litJsLibrary.Text += stJavaScript;
      }
      //
      // Group the page header divs together
      //
      if ( PageGroup.Layout == Evado.Model.UniForm.GroupLayouts.Page_Header
        && PageGroup.GetParameterInt ( Evado.Model.UniForm.GroupParameterList.Pixel_Width ) == 0 )
      {
        PageGroup.Layout = Evado.Model.UniForm.GroupLayouts.Full_Width;
      }

      //
      // Define if the group is to be full width.
      //
      string divFieldContainerStyle = " style='";
      string divFieldGroupStyle = " style='";

      if ( PageGroup.Layout == Evado.Model.UniForm.GroupLayouts.Page_Header )
      {
        divFieldGroupStyle += "width:98%; ";
      }
      else
        if ( PageGroup.Layout == Evado.Model.UniForm.GroupLayouts.Full_Width )
        {
          if ( EnableBodyColumns == false )
          {
            divFieldGroupStyle += "width:98%; ";
          }
          else
          {
            divFieldGroupStyle += "width:100%; ";
          }
        }
        else
          if ( inPixelWidth > 0 )
          {
            divFieldGroupStyle += "width:" + inPixelWidth + "px; ";
          }


      if ( inPixelHeight > 0 )
      {
        divFieldGroupStyle += " height:" + inPixelHeight + "px; ";
      }

      divFieldGroupStyle += "'";
      divFieldContainerStyle += "'";




      sbHtml.AppendLine ( "<!--- OPENING GROUP --->" );
      if ( stFieldName != String.Empty
        && stFieldValue != String.Empty )
      {
        sbHtml.AppendLine ( "<div id='" + PageGroup.Id + "-grp' "
          + "class='" + Css_Class_Field_Group + "' " + divFieldGroupStyle );
        sbHtml.Append ( " data-hide-group-if-field-id=\"" + stFieldName + "\"" );
        sbHtml.Append ( " data-hide-group-if-field-value=\"" + stFieldValue + "\"" + "> " );
      }
      else
      {
        sbHtml.AppendLine ( "<div id='" + PageGroup.Id + "-grp' "
          + "class='" + Css_Class_Field_Group + "' " + divFieldGroupStyle + "> " );
      }


      sbHtml.AppendLine ( "<span class='" + Css_Class_Group_Title + "'> " + PageGroup.Title + "</span>" );
      sbHtml.AppendLine ( "<div class='" + Css_Class_Field_Group_Container + "' " + divFieldContainerStyle + " >\r\n " );

      if ( Global.DebugDisplayOn == true )
      {
        sbHtml.AppendLine ( "<div class='debug-info'>"
          + " L: " + PageGroup.Layout
          + " W: " + inPixelWidth
          + " S: " + PageGroup.EditAccess
          + " GT: " + PageGroup.GroupType
          + "</div>\r\n" );
      }

      //
      // Add the gorups description if it exists.
      //
      String description = String.Empty;
      if ( PageGroup.Description != null )
      {
        description = PageGroup.Description;
      }
      if ( description != String.Empty )
      {
        Evado.Model.UniForm.GroupDescriptionAlignments alignment = PageGroup.DescriptionAlignment;

        string textAlignment = alignment.ToString ( ).Replace ( "_", "-" );
        textAlignment = textAlignment.ToLower ( );

        sbHtml.AppendLine ( "<!--- OPENNING DESCRIPTION --->" );
        sbHtml.AppendLine ( "<div class='description cf " + textAlignment + " '>" );

        if ( description.Contains ( "</" ) == true
          || description.Contains ( "/>" ) == true
          || description.Contains ( "[[/" ) == true
          || description.Contains ( "/]]" ) == true )
        {
          description = Evado.Model.EvStatics.decodeHtmlText ( description );
          sbHtml.Append ( description );
        }
        else
        {
          sbHtml.AppendLine ( this.encodeMarkDown ( description ) );
        }
        sbHtml.AppendLine ( "</div>" );
        sbHtml.AppendLine ( "<!-- CLOSING DESCRIPTION -->" );
      }
    }

    // ===================================================================================
    /// <summary>
    /// This method creates a test field html markup
    /// </summary>
    /// <param name="PageGroup">The group obbject</param>
    /// <returns>String html</returns>
    // ----------------------------------------------------------------------------------
    private void generateGroupFooter (
      StringBuilder sbHtml )
    {
      sbHtml.AppendLine ( "</div>" );
      sbHtml.AppendLine ( "</div>" );
      sbHtml.AppendLine ( "<!-- GROUP FOOTER -->\r\n" );
    }

    // ===================================================================================
    /// <summary>
    /// This method creates a test field html markup
    /// </summary>
    /// <param name="PageGroup">The group obbject</param>
    /// <returns>String html</returns>
    // ----------------------------------------------------------------------------------
    private Guid getField_ID ( String FieldId )
    {
      //
      // Iterate through group fields to find the field's Id 
      //
      foreach ( Evado.Model.UniForm.Group group in this._AppData.Page.GroupList )
      {
        foreach ( Evado.Model.UniForm.Field field in group.FieldList )
        {
          if ( field.FieldId == FieldId )
          {
            return field.Id;
          }
        }
      }

      return Guid.Empty;
    }

    // ===================================================================================
    /// <summary>
    /// This method creates a group's field markup
    /// </summary>
    /// <param name="GroupIndex">Index to the group in the page</param>
    /// <param name="PageGroup">The group obbject</param>
    /// <param name="GroupStatus">The groups edit status</param>
    /// <returns>String html</returns>
    // ----------------------------------------------------------------------------------
    private void generateGroupFields (
      StringBuilder sbHtml,
      int GroupIndex,
      Evado.Model.UniForm.Group PageGroup )
    {
      //
      // Initialise the methods variables and objects.
      //
      Global.LogClientMethod ( "generateGroupFields method. " );
      Global.LogDebug ( "PageGroup.Title: " + PageGroup.Title );
      Global.LogDebug ( "PageGroup.GroupType: " + PageGroup.GroupType );
      Global.LogDebug ( "PageGroup.Status: " + PageGroup.EditAccess );
      Global.LogDebug ( "PageGroup.CmdLayout: " + PageGroup.CmdLayout );
      Global.LogDebug ( "PageGroup.Status: " + PageGroup.EditAccess );

      String stCssDefault = PageGroup.GetParameter ( Evado.Model.UniForm.GroupParameterList.BG_Default );
      String stCssValid = PageGroup.GetParameter ( Evado.Model.UniForm.GroupParameterList.BG_Validation );
      String stCssAlert = PageGroup.GetParameter ( Evado.Model.UniForm.GroupParameterList.BG_Alert );
      String stCssNormal = PageGroup.GetParameter ( Evado.Model.UniForm.GroupParameterList.BG_Normal );

      //
      // if no field exit method.
      //
      if ( PageGroup.FieldList.Count == 0 )
      {
        return;
      }

      //
      // Open the group's field and Command table.
      //
      sbHtml.Append ( "<div class='fields'>" );

      this._TabIndex = ( GroupIndex + 1 ) * 100;
      //
      // iterate throught the field list
      //
      for ( int count = 0; count < PageGroup.FieldList.Count; count++ )
      {
        Evado.Model.UniForm.Field groupField = PageGroup.FieldList [ count ];

        //
        // continue for null field objects.
        //
        if ( groupField == null )
        {
          Global.LogDebug ( "SKIP: Field is null" );
          continue;
        }

        //
        // Set the edit access.
        //
        if ( groupField.EditAccess == Evado.Model.UniForm.EditAccess.Inherited )
        {
          groupField.EditAccess = PageGroup.EditAccess;
        }

        Global.LogDebug ( "field.Title: " + groupField.Title
          + ", field.FieldId: " + groupField.FieldId
          + ", field.Type: " + groupField.Type
          + ", Status: " + groupField.EditAccess );

        this._TabIndex++;

        //
        // Insert the field elements
        //
        switch ( groupField.Type )
        {
          case Evado.Model.EvDataTypes.Read_Only_Text:
            {
              this.createReadOnlyField ( sbHtml, groupField );
              break;
            }
          case Evado.Model.EvDataTypes.Text:
            {
              this.createTextField ( sbHtml, groupField, groupField.EditAccess );
              break;
            }
          case Evado.Model.EvDataTypes.Password:
            {
              this.createPasswordField ( sbHtml, groupField, groupField.EditAccess );
              break;
            }
          case Evado.Model.EvDataTypes.Html_Link:
            {
              this.createHttpLinkField ( sbHtml, groupField, groupField.EditAccess );
              break;
            }
          case Evado.Model.EvDataTypes.Free_Text:
            {
              this.createFreeTextField ( sbHtml, groupField, groupField.EditAccess );
              break;
            }

          case Evado.Model.EvDataTypes.Html_Content:
            {
              this.createHtmlField ( sbHtml, groupField );
              break;
            }
          case Evado.Model.EvDataTypes.Boolean:
          case Evado.Model.EvDataTypes.Yes_No:
            {
              this.createYesNoField ( sbHtml, groupField, groupField.EditAccess );
              break;
            }
          case Evado.Model.EvDataTypes.Date:
            {
              this.createDateField ( sbHtml, groupField, groupField.EditAccess );
              break;
            }
          case Evado.Model.EvDataTypes.Time:
            {
              this.createTimeField ( sbHtml, groupField, groupField.EditAccess );
              break;
            }
          case Evado.Model.EvDataTypes.Numeric:
          case Evado.Model.EvDataTypes.Integer:
            {
              this.createNumericField ( sbHtml, groupField, groupField.EditAccess );
              break;
            }
          case Evado.Model.EvDataTypes.Streamed_Video:
            {
              this.createStreamedVideoField ( sbHtml, groupField, groupField.EditAccess );
              break;
            }
          case Evado.Model.EvDataTypes.External_Image:
            {
              this.createExternalImageField ( sbHtml, groupField, groupField.EditAccess );
              break;
            }
          case Evado.Model.EvDataTypes.Image:
            {
              this.createImageField ( sbHtml, groupField, groupField.EditAccess );
              break;
            }
          case Evado.Model.EvDataTypes.Binary_File:
            {
              this.createBinaryField ( sbHtml, groupField, groupField.EditAccess );
              break;
            }
          case Evado.Model.EvDataTypes.Sound:
            {
              this.createSoundField ( sbHtml, groupField, groupField.EditAccess );
              break;
            }
          case Evado.Model.EvDataTypes.Bar_Code:
            {
              this.createTextField ( sbHtml, groupField,
                Evado.Model.UniForm.EditAccess.Enabled );
              break;
            }
          case Evado.Model.EvDataTypes.Radio_Button_List:
            {
              this.createRadioButtonField ( sbHtml, groupField, groupField.EditAccess );
              break;
            }
          case Evado.Model.EvDataTypes.Special_Quiz_Radio_Buttons:
            {
              this.createQuizRadioButtonField ( sbHtml, groupField, groupField.EditAccess );
              break;
            }
          case Evado.Model.EvDataTypes.Horizontal_Radio_Buttons:
            {
              this.createHorizontalRadioButtonField ( sbHtml, groupField, groupField.EditAccess );
              break;
            }
          case Evado.Model.EvDataTypes.Selection_List:
            {
              this.createSelectionListField ( sbHtml, groupField, groupField.EditAccess );
              break;
            }
          case Evado.Model.EvDataTypes.Check_Box_List:
            {
              this.createCheckboxField ( sbHtml, groupField, groupField.EditAccess );
              break;
            }
          case Evado.Model.EvDataTypes.Table:
            {
              this.createTableField ( sbHtml, groupField, groupField.EditAccess );
              break;
            }
          case Evado.Model.EvDataTypes.Currency:
            {
              this.createCurrencyField ( sbHtml, groupField, groupField.EditAccess );
              break;
            }
          case Evado.Model.EvDataTypes.Email_Address:
            {
              this.createEmailAddressField ( sbHtml, groupField, groupField.EditAccess );
              break;
            }
          case Evado.Model.EvDataTypes.Telephone_Number:
            {
              this.createTelephoneNumberField ( sbHtml, groupField, groupField.EditAccess );
              break;
            }
          case Evado.Model.EvDataTypes.Analogue_Scale:
            {
              this.createAnalogueField ( sbHtml, groupField, groupField.EditAccess );
              break;
            }
          case Evado.Model.EvDataTypes.Name:
            {
              this.createNameField ( sbHtml, groupField, groupField.EditAccess );
              break;
            }
          case Evado.Model.EvDataTypes.Address:
            {
              this.createAddressField ( sbHtml, groupField, groupField.EditAccess );
              break;
            }
          case Evado.Model.EvDataTypes.Signature:
            {
              this.createSignatureField ( sbHtml, groupField, groupField.EditAccess );
              break;
            }
          case Evado.Model.EvDataTypes.Integer_Range:
          case Evado.Model.EvDataTypes.Float_Range:
          case Evado.Model.EvDataTypes.Double_Range:
            {
              Global.LogDebug ( "calling createNumericRangeField method." );

              this.createNumericRangeField ( sbHtml, groupField, groupField.EditAccess );
              break;
            }
          case Evado.Model.EvDataTypes.Date_Range:
            {
              Global.LogDebug ( "calling createDateRangeField method." );

              this.createDateRangeField ( sbHtml, groupField, groupField.EditAccess );
              break;
            }
          case Evado.Model.EvDataTypes.Computed_Field:
            {
              this.createComputedField ( sbHtml, groupField, Evado.Model.UniForm.EditAccess.Disabled );
              break;
            }
          case Evado.Model.EvDataTypes.Donut_Chart:
          case Evado.Model.EvDataTypes.Line_Chart:
          case Evado.Model.EvDataTypes.Bar_Chart:
          case Evado.Model.EvDataTypes.Pie_Chart:
          case Evado.Model.EvDataTypes.Stacked_Bar_Chart:
            {
              this.createPlotChartField ( sbHtml, groupField );
              break;
            }
          default:
            {
              break;
            }
        }//END Case Statement

      }//END page object iteration loop

      //
      // Close the field table tag.
      //
      sbHtml.Append ( "</div>\r\n " );

      Global.LogDebugMethodEnd ( "generateGroupFields" );
    }

    //===================================================================================
    /// <summary>
    /// This mehtoid renders a Command title to include icons values in the title
    /// </summary>
    /// <param name="GroupCommand">Evado.Model.UniForm.Command object</param>
    /// <returns>String Title with embedded html</returns>
    //-----------------------------------------------------------------------------------
    private String renderCommandTitleNoImage (
      Evado.Model.UniForm.Command GroupCommand )
    {
      //Global.LogDebugMethod ( "renderCommandTitle method. " );
      //Global.LogDebugValue ( "Command.Title: " + Command.Title );
      //
      // Initialise the methods variables and objects.
      //
      List<Model.UniForm.Parameter> parameters = this._AppData.Page.Parameters;
      string iconImage = GroupCommand.GetParameter ( Model.UniForm.CommandParameters.Image_Url );

      string title = GroupCommand.Title;

      return title;
    }
    //===================================================================================
    /// <summary>
    /// This mehtoid renders a Command title to include icons values in the title
    /// </summary>
    /// <param name="GroupCommand">Evado.Model.UniForm.Command object</param>
    /// <returns>String Title with embedded html</returns>
    //-----------------------------------------------------------------------------------
    private String renderCommandTitle (
      Evado.Model.UniForm.Command GroupCommand )
    {
      //Global.LogDebugMethod ( "renderCommandTitle method. " );
      //Global.LogDebugValue ( "Command.Title: " + Command.Title );
      //
      // Initialise the methods variables and objects.
      //
      List<Model.UniForm.Parameter> parameters = this._AppData.Page.Parameters;
      string iconImage = GroupCommand.GetParameter ( Model.UniForm.CommandParameters.Image_Url );

      string title = GroupCommand.Title;

      if ( iconImage != String.Empty )
      {
        string iconImagePath = Global.RelativeBinaryDownloadURL + iconImage;

        title = "<img class=\"command-icon\" src=\"" + iconImagePath + "\" width='125px' />&nbsp;" + title;
      }

      return title;
    }

    // ===================================================================================
    /// <summary>
    /// This method creates a groups commands
    /// </summary>
    /// <param name="sbHtml">StringBuilder containing the html content.</param>
    /// <param name="PageGroup">Group object</param>
    // ----------------------------------------------------------------------------------
    private void generateGroupCommands (
      StringBuilder sbHtml,
      Evado.Model.UniForm.Group PageGroup )
    {
      //
      // Initialise the methods variables and objects.
      //
      Global.LogDebugMethod ( "generateGroupCommands method. " );
      Global.LogDebug ( "Evado.Model.UniForm.Group.Title: " + PageGroup.Title );
      Global.LogDebug ( "Evado.Model.UniForm.Group.CmdLayout: " + PageGroup.CmdLayout );

      //
      // If the page group is null exit .
      //
      if ( PageGroup.CommandList == null )
      {
        Global.LogDebug ( "Command list null." );
        Global.LogDebugMethodEnd ( "generateGroupCommands" );
        return;
      }

      //
      // If the page group does not have commands exit.
      //
      if ( PageGroup.CommandList.Count == 0 )
      {
        Global.LogDebug ( "Command list empty." );
        Global.LogDebugMethodEnd ( "generateGroupCommands" );
        return;
      }

      //
      // Display the Command group according to layout.
      //
      switch ( PageGroup.CmdLayout )
      {
        case Evado.Model.UniForm.GroupCommandListLayouts.Vertical_Orientation:
          this.generateVerticalCommandGroup ( sbHtml, PageGroup );
          break;

        case Evado.Model.UniForm.GroupCommandListLayouts.Tiled_Commands:
          this.generateTiledCommandGroup ( sbHtml, PageGroup );
          break;

        default:
          this.generateDefaultCommandGroup ( sbHtml, PageGroup );
          break;
      }

      Global.LogDebugMethodEnd ( "generateGroupCommands" );
    }

    //===================================================================================
    /// <summary>
    /// This method generates the virtual groupCommand group html content.
    /// </summary>
    /// <param name="sbHtml">StringBuilding containing the html </param>
    /// <param name="PageGroup">PageGroup object</param>
    //-----------------------------------------------------------------------------------
    private void generateVerticalCommandGroup (
      StringBuilder sbHtml,
      Evado.Model.UniForm.Group PageGroup )
    {
      Global.LogDebugMethod ( "generateGroupCommandsVertical method. " );
      Global.LogDebug ( "Group.Title: " + PageGroup.Title );
      //
      // Initialise the methods variables and objects.
      //
      int GroupCommandIndex = 0;
      bool bEventRow = false;

      //
      // Setting the default command bacground colours.
      //
      String background_Default = "White";
      String background_Alternative = "Gray";
      String background_Highlighted = "Dark_Red";

      Global.LogDebug ( "1 background_Default: " + background_Default );
      Global.LogDebug ( "1 background_Alternative: " + background_Alternative );
      Global.LogDebug ( "1 background_Highlighted: " + background_Highlighted );

      //
      // Update the colour if it is in the group settings.
      //
      if ( PageGroup.hasParameter ( Model.UniForm.GroupParameterList.BG_Default ) == true )
      {
        background_Default = PageGroup.GetParameter ( Model.UniForm.GroupParameterList.BG_Default );
      }
      if ( PageGroup.hasParameter ( Model.UniForm.GroupParameterList.BG_Alternative ) == true )
      {
        background_Alternative = PageGroup.GetParameter ( Model.UniForm.GroupParameterList.BG_Alternative );
      }
      if ( PageGroup.hasParameter ( Model.UniForm.GroupParameterList.BG_Highlighted ) == true )
      {
        background_Highlighted = PageGroup.GetParameter ( Model.UniForm.GroupParameterList.BG_Highlighted );
      }

      Global.LogDebug ( "2 background_Default: " + background_Default );
      Global.LogDebug ( "2 background_Alternative: " + background_Alternative );
      Global.LogDebug ( "2 background_Highlighted: " + background_Highlighted );
      //
      // Define the table header.
      //
      sbHtml.Append ( "<table  class='NavigationTable'>" );

      foreach ( Evado.Model.UniForm.Command command in PageGroup.CommandList )
      {
        //
        // skip null commands
        //
        if ( command == null )
        {
          Global.LogDebug ( "Command is null." );
          continue;
        }

        Global.LogDebug ( "Command:" + command.Title );

        if ( command.Type != Evado.Model.UniForm.CommandTypes.Null )
        {
          String background = command.GetParameter ( Model.UniForm.CommandParameters.BG_Default );
          String alternative = command.GetParameter ( Model.UniForm.CommandParameters.BG_Alternative );
          String highlighted = command.GetParameter ( Model.UniForm.CommandParameters.BG_Highlighted );

          if ( alternative == "" ) alternative = background;
          if ( background == "" ) background = background_Default;
          if ( alternative == "" ) alternative = background_Alternative;
          if ( highlighted == "" ) highlighted = background_Highlighted;

          Global.LogDebug ( "3 background: " + background );
          Global.LogDebug ( "3 alternative: " + alternative );
          Global.LogDebug ( "3 highlighted: " + highlighted );

          if ( bEventRow == false )
          {
            sbHtml.AppendLine ( "<tr> "
              + "<td class=\"" + background + "\" "
              + "onmouseover=\"this.className='" + highlighted + "'\" "
              + "onmouseout=\"this.className='" + background + "'\" "
              + "onclick=\"javascript:onPostBack('" + command.Id + "')\"> "
              + this.renderCommandTitle ( command )
              + "</td>"
              + "</tr>" );
            bEventRow = true;
          }
          else
          {
            sbHtml.AppendLine ( "<tr> "
              + "<td class=\"" + alternative + "\" "
              + "onmouseover=\"this.className='" + highlighted + "'\" "
              + "onmouseout=\"this.className='" + alternative + "'\" "
              + "onclick=\"javascript:onPostBack('" + command.Id + "')\"> "
              + this.renderCommandTitle ( command )
              + "</td>"
              + "</tr>" );
            bEventRow = false;
            /*
              + "<p class='NavigationPrompt'>"
              + "</p>"
             */
          }

          GroupCommandIndex++;
        }
        else
        {
          sbHtml.Append ( "<tr> "
          + "<td class=\"Header\" >" + command.Title + "</td></tr>" );
        }


      }//END page object iteration loop

      sbHtml.Append ( "</table>" );
    }

    //===================================================================================
    /// <summary>
    /// This method generates the titled groupCommand group html content.
    /// </summary>
    /// <param name="sbHtml">StringBuilding containing the html </param>
    /// <param name="PageGroup">PageGroup object</param>
    //-----------------------------------------------------------------------------------
    private void generateTiledCommandGroup (
      StringBuilder sbHtml,
      Evado.Model.UniForm.Group PageGroup )
    {
      Global.LogDebugMethod ( "generateTiledCommandGroup method. " );
      Global.LogDebug ( "Group.Title: " + PageGroup.Title );
      //
      // Initialise the methods variables and objects.
      //
      string columnHeaders = PageGroup.GetParameter ( Model.UniForm.GroupParameterList.Tiled_Column_Header );
      string [ ] headers;
      int columnPercentage = 0;
      int columnCount = 0;
      String defaultColor = Model.UniForm.Background_Colours.Default.ToString ( );

      if ( PageGroup.hasParameter ( Model.UniForm.GroupParameterList.BG_Default ) == true )
      {
        defaultColor = PageGroup.GetParameter ( Model.UniForm.GroupParameterList.BG_Default );
      }
      Global.LogDebug ( "Default" + defaultColor );

      string tileWidth = "";

      sbHtml.Append ( "<div class='menu links tiled cf'>" );

      //
      // Output group column headers.
      //
      if ( columnHeaders != "" )
      {
        headers = columnHeaders.Split ( ',' );

        columnCount = headers.Length;

        if ( columnCount > 0 )
        {
          List<Evado.Model.UniForm.Command> [ ] columns = new List<Evado.Model.UniForm.Command> [ columnCount ];

          columnPercentage = 100 / columnCount;
          tileWidth = columnPercentage.ToString ( ) + "%";

          foreach ( string heading in headers )
          {
            sbHtml.Append ( "<div class='tile-heading' style='width:" + tileWidth + ";'>" );
            sbHtml.Append ( "<div class='inner'>\r\n " );
            sbHtml.Append ( heading );
            sbHtml.Append ( "</div>\r\n " );
            sbHtml.Append ( "</div>\r\n " );
          }

          int count = 0;
          int columnPos;
          int rowCount = 0;

          foreach ( Evado.Model.UniForm.Command command in PageGroup.CommandList )
          {
            //
            // skip null commands
            //
            if ( command == null )
            {
              Global.LogDebug ( "Command is null." );
              continue;
            }

            string tiledColumn = command.GetParameter ( Model.UniForm.CommandParameters.Tiled_Column );

            if ( tiledColumn != "" )
            {
              columnPos = Convert.ToInt32 ( tiledColumn ) - 1;
            }
            else
            {
              columnPos = count % columnCount;
            }

            if ( columns [ columnPos ] == null )
            {
              columns [ columnPos ] = new List<Evado.Model.UniForm.Command> ( );
            }

            columns [ columnPos ].Add ( command );

            if ( columns [ columnPos ].Count > rowCount )
            {
              rowCount = columns [ columnPos ].Count;
            }

            count += 1;
          }

          for ( int i = 0; i < rowCount; i++ )
          {
            for ( int j = 0; j < columnCount; j++ )
            {
              Evado.Model.UniForm.Command command = null;

              if ( columns [ j ].Count >= ( i + 1 ) )
              {
                command = columns [ j ] [ i ];
              }

              if ( command != null )
              {
                this.generateCommandTile ( sbHtml, command, tileWidth, defaultColor );
              }
              else
              {
                // blank tile for empty space
                sbHtml.Append ( "<div class='tile blank'" );
                if ( tileWidth != "" )
                {
                  sbHtml.Append ( " style='width:" + tileWidth + ";'" );
                }

                sbHtml.Append ( ">" );
                sbHtml.Append ( "</div>" );
              }
            }
          }
        }
      }

      //
      // Output non grouped headers.
      //
      if ( columnHeaders == "" || columnCount == 0 )
      {
        tileWidth = PageGroup.GetParameter ( Model.UniForm.GroupParameterList.Command_Width );
        Global.LogDebug ( "tileWidth" + tileWidth );

        foreach ( Evado.Model.UniForm.Command command in PageGroup.CommandList )
        {
          //
          // skip null commands
          //
          if ( command == null )
          {
            Global.LogDebug ( "Command is null." );
            continue;
          }

          this.generateCommandTile ( sbHtml, command, tileWidth, defaultColor );
        }
      }

      sbHtml.Append ( "</div>\r\n " );
    }

    //===================================================================================
    /// <summary>
    /// This method creates a command title html content.
    /// </summary>
    /// <param name="sbHtml">StringBuilding containing the html contnet.</param>
    /// <param name="groupCommand">Evado.Model.UniForm.Command object</param>
    /// <param name="tileWidth">String: title width </param>
    /// <param name="defaultColor">String the default colour</param>
    //-----------------------------------------------------------------------------------
    private void generateCommandTile (
      StringBuilder sbHtml,
      Evado.Model.UniForm.Command groupCommand,
      string tileWidth,
       String defaultColor )
    {
      Global.LogDebugMethod ( "generateCommandTile method. " );
      Global.LogDebug ( "Command.Title: " + groupCommand.Title );
      Global.LogDebug ( "tileWidth: " + tileWidth );
      Global.LogDebug ( "defaultColor: " + defaultColor );

      //
      // If command is null then exit.
      //
      if ( groupCommand.Type == Evado.Model.UniForm.CommandTypes.Null )
      {
        return;
      }

      //
      // Initialise the methods variables and objects.
      //
      string iconImage = groupCommand.GetParameter ( Model.UniForm.CommandParameters.Image_Url );
      string iconImagePath = Global.RelativeBinaryDownloadURL + iconImage;
      string color = groupCommand.GetParameter ( Model.UniForm.CommandParameters.BG_Default );

      Global.LogDebug ( "Command iconImage: " + iconImage );
      Global.LogDebug ( "Command BG Color: " + color );

      if ( color == "" ) { color = defaultColor; }
      if ( color == "" ) { color = Model.UniForm.Background_Colours.Default.ToString ( ); }

      Global.LogDebug ( "BG Color: " + color );

      sbHtml.Append ( "<div class='tile" );
      sbHtml.Append ( "'" );

      if ( tileWidth != "" )
      {
        sbHtml.Append ( " style='width:" + tileWidth + ";'" );
      }

      string tiledColumn = groupCommand.GetParameter ( "Tiled_Column" );

      if ( tiledColumn != "" )
      {
        sbHtml.Append ( " data-column='" + tiledColumn + "'" );
      }

      sbHtml.Append ( ">" );
      sbHtml.Append ( "<a "
         + "class='btn btn-default" );

      if ( color != "" )
      {
        sbHtml.Append ( " " + color );
      }

      sbHtml.Append ( "' "
         + "href=\"javascript:onPostBack('" + groupCommand.Id + "')\" "
         + ( groupCommand.getEnableForMandatoryFields ( ) ? " data-enable-for-mandatory-fields " : "" )
         + ">" );

      if ( iconImage != "" )
      {
        sbHtml.Append ( "<img src=\"" + iconImagePath + "\" />" );
      }

      sbHtml.Append ( "<div class='title'>" );
      sbHtml.Append ( groupCommand.Title.Replace ( "\r\n", "<br/>" ) );
      sbHtml.Append ( "</div>" );

      sbHtml.Append ( "</a>\r\n" );
      sbHtml.Append ( "</div>" );

    }//END generateCommandTile method

    //==================================================================================
    /// <summary>
    /// This method generates a default command group
    /// </summary>
    /// <param name="sbHtml">StringBuilder containing the html content.</param>
    /// <param name="PageGroup">Group object</param>
    //-----------------------------------------------------------------------------------
    private void generateDefaultCommandGroup (
      StringBuilder sbHtml,
      Evado.Model.UniForm.Group PageGroup )
    {
      Global.LogDebugMethod ( "generateDefaultCommandGroup method. " );
      Global.LogDebug ( "Group.Title: " + PageGroup.Title );
      //
      // Initialise the methods variables and objects.
      //
      int GroupCommandIndex = 0;

      if ( PageGroup.CommandList == null )
      {
        Global.LogDebug ( "Command List is null" );
        return;
      }

      Global.LogDebug ( "Group.Command count: " + PageGroup.CommandList.Count );

      sbHtml.Append ( "<div class='menu links'>" );

      foreach ( Evado.Model.UniForm.Command command in PageGroup.CommandList )
      {
        if ( command == null )
        {
          Global.LogDebug ( "Command is null" );
          continue;
        }
        if ( command.Type != Evado.Model.UniForm.CommandTypes.Null )
        {
          sbHtml.Append ( this.createCommandLink ( command ) );
          GroupCommandIndex++;
        }

      }//END page object iteration loop

      //
      // Close the Command Command tag
      //
      sbHtml.Append ( "</div>\r\n " );
    }


  }//END class

}//END name space
