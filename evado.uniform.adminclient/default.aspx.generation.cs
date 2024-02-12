using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.MobileControls;

///Evado. namespace references.

using Evado.UniForm.Web;

namespace Evado.UniForm.AdminClient
{
  /// <summary>
  /// This is the code behind class for the home page.
  /// </summary>
  public partial class DefaultPage : EvPersistentPageState
  {
    private int _TabIndex = 0;
    //
    // The following margins are margins within the column widths.
    //
    private const float Left_Col_Left_Margin = 0.5F; // %
    private const float Left_Col_Right_Margin = 0.5F; // %
    private const float Center_Col_Left_Margin = 0.5F; // %
    private const float Center_Col_Right_Margin = 2F; // %
    private const float Right_Col_Left_Margin = 1F; // %
    private const float Right_Col_Right_Margin = 0.5F; // %

    // ==================================================================================
    /// <summary>
    /// This method uses the global ApplicationData object to generates the page layout.
    /// </summary>
    // ---------------------------------------------------------------------------------
    private void GeneratePage( )
    {
      this.LogMethod ( "GeneratePage" );
      this.LogDebug ( "PageStatus: " + this.UserSession.AppData.Page.EditAccess );
      this.LogDebug ( "AppData.Page.PageId: {0}.", this.UserSession.AppData.Page.PageId );
      this.LogDebug ( "Page command list count: " + this.UserSession.AppData.Page.CommandList.Count );
      this.LogDebug ( "ImagesUrl: {0}.", Global.StaticImageUrl );
      //
      // initialise method variables and objects.
      //
      this.litPageContent.Visible = true;

      if ( Global.EnablePageMenu == true )
      {
        this.litPageMenu.Visible = true;
      }

      if ( Global.EnablePageHistory == true )
      {
        this.litHistory.Visible = true;
      }

      //
      // initialise the methods variables and objects.
      //
      this.litExitCommand.Visible = true;
      this.litCommandContent.Visible = true;
      this.litHeaderTitle.Visible = true;
      StringBuilder sbMainBody = new StringBuilder ( );
      StringBuilder sbLeftBody = new StringBuilder ( );
      StringBuilder sbCentreBody = new StringBuilder ( );
      StringBuilder sbRightBody = new StringBuilder ( );
      StringBuilder sbPageMenuPills = new StringBuilder ( );
      StringBuilder sbPageHistoryPills = new StringBuilder ( );
      int leftColumnPercentage = 0;
      int rightColumnPercentage = 0;
      this.Title = Global.TitlePrefix + " - " + this.UserSession.AppData.Title;
      this.litCommandContent.Visible = true;

      //
      // Reinitialise the history each time the home page is displayed.
      //

      if ( this.UserSession.AppData.Page.Id == Evado.Model.EvStatics.CONST_HOME_COMMAND_ID
        || this.UserSession.AppData.Page.Id == Evado.Model.EvStatics.CONST_HOME_COMMAND_2_ID
        || this.UserSession.AppData.Page.Id == Evado.Model.EvStatics.CONST_HOME_COMMAND_3_ID
        || this.UserSession.AppData.Page.Id == Evado.Model.EvStatics.CONST_HOME_COMMAND_4_ID
        || this.UserSession.AppData.Page.Id == Evado.Model.EvStatics.CONST_HOME_COMMAND_5_ID
        || this.UserSession.AppData.Page.PageId == Evado.Model.EvStatics.CONST_HOME_PAGE_ID )
      {
        this.LogDebug ( "Home Page encountered." );
        this.initialiseHistory ( );
      }


      //
      // If the anonymous access mode exit.
      //

      this.LogDebug ( "Anonymous_Page_Access: " + this.UserSession.AppData.Page.AnonymousPageAccess );

      if ( this.UserSession.AppData.Page.AnonymousPageAccess == false )
      {
        this.LogDebug ( "Anonyous_Page_Access = false" );

        //
        // generate the page commands.
        //
        this.generatePageCommands ( );

        //
        // Generate the Page History menu
        //
        this.LogDebug ( "CommandHistoryList.Count {0}. ", this.UserSession.CommandHistoryList.Count );
        if ( this.UserSession.CommandHistoryList.Count > 0 )
        {
          sbPageHistoryPills.Append ( "<ol class='breadcrumb'>" );

          //
          // Iterate through the Command history to create the breadcrumbs.
          //
          foreach ( Evado.UniForm.Model.EuCommand command in this.UserSession.CommandHistoryList )
          {
            this.generateHistoryMenuPills ( sbPageHistoryPills, command );
          }

          sbPageHistoryPills.Append ( "</ol>" );
        }
      }//END Anonymous_Page_Access control.

      //
      // If an error message has been generated display it at the top of the 
      // page.
      //
      if ( this.UserSession.AppData.Message != String.Empty )
      {
        this.generateErrorMessge ( sbMainBody );
      }

      leftColumnPercentage = this.UserSession.AppData.Page.LeftColumnWidth;
      rightColumnPercentage = this.UserSession.AppData.Page.RightColumnWidth;
      int centerColumPercentage = 100 - leftColumnPercentage - rightColumnPercentage;
      bool enableBodyColumns = false;

      this.LogDebug ( "Percentage: LeftColumn: {0}, CentreColumn: {1}, rightColumn: {2} ",
        leftColumnPercentage, centerColumPercentage, rightColumnPercentage );

      if ( leftColumnPercentage > 0
        || rightColumnPercentage > 0 )
      {
        enableBodyColumns = true;
      }

      this.LogDebug ( "enableBodyColumns: {0}", enableBodyColumns );
      //
      // Generate the group menu.
      //
      sbPageMenuPills.Append ( "<ul class='nav nav-pills'>" );
      sbPageMenuPills.Append ( "<li class='active'><a href='#'>All</a></li>" );

      //
      // Generate the html for each group in the page object.
      //
      if ( this.UserSession.AppData.Page.GroupList.Count > 0 )
      {
        for ( int count = 0 ; count < this.UserSession.AppData.Page.GroupList.Count ; count++ )
        {
          Evado.UniForm.Model.EuGroup group = this.UserSession.AppData.Page.GroupList [ count ];

          if ( group == null )
          {
            continue;
          }

          this.LogDebug ( "{0}, Layout: {1}, Column: {2}", group.Title, group.Layout, group.PageColumn );

          //
          // Header fields are always at the top of the page.
          //
          if ( group.Layout == Model.EuGroupLayouts.Page_Header
             || enableBodyColumns == false )
          {
            this.LogDebug ( "NO BODY COLUMNS OR HEADER PAGE. ADD: {0}. ", group.Title );
            this.generateGroup ( sbMainBody, count, false );

            this.generatePageMenuPills ( sbPageMenuPills, group );

            continue;
          }

          switch ( group.PageColumn )
          {
            //
            // if the left column exists and the group is allocated to the left column
            // place the group html in the left body.
            //
            case Model.EuPageColumns.Left:
            {
              if ( leftColumnPercentage > 0 )
              {
                this.LogDebug ( "ADD: " + group.Title + " to left column" );

                this.UserSession.AppData.Page.GroupList [ count ].Layout = Evado.UniForm.Model.EuGroupLayouts.Full_Width;

                this.generateGroup ( sbLeftBody, count, enableBodyColumns );

                this.generatePageMenuPills ( sbPageMenuPills, group );
              }
              continue;
            }//END left column only

            //
            // if the right column exists and the group is allocated to the right column
            // place the group html in the right body.
            //
            case Model.EuPageColumns.Right:
            {
              if ( rightColumnPercentage > 0 )
              {
                this.LogDebug ( "ADD: " + group.Title + " to right column" );

                this.UserSession.AppData.Page.GroupList [ count ].Layout = Evado.UniForm.Model.EuGroupLayouts.Full_Width;

                this.generateGroup ( sbRightBody, count, enableBodyColumns );

                this.generatePageMenuPills ( sbPageMenuPills, group );
              }
              continue;
            }
            default:
            {
              //
              // else place is in the central body.
              //
              this.LogDebug ( "ADD: " + group.Title + " to center column" );

              this.generateGroup ( sbCentreBody, count, enableBodyColumns );

              this.generatePageMenuPills ( sbPageMenuPills, group );
              continue;
            }

          }
        }//END Group interation loop
      }//END multiple groups.

      this.LogDebug ( "sbLeftBody.Length: " + sbLeftBody.Length );
      this.LogDebug ( "sbCentreBody.Length: " + sbCentreBody.Length );
      this.LogDebug ( "sbRightBody.Length: " + sbRightBody.Length );

      sbPageMenuPills.Append ( "</ul>" );

      if ( sbLeftBody.Length > 0
        || sbCentreBody.Length > 0
        || sbRightBody.Length > 0 )
      {
        this.LogDebug ( "Columns exist." );

        sbMainBody.AppendLine ( "<!-- OPENING BODY COLUMNS -->" );
        sbMainBody.AppendLine ( "<div class='body-column-header'>" );

        if ( sbLeftBody.Length == 0
          && sbCentreBody.Length > 0
          && sbRightBody.Length == 0 )
        {
          this.LogDebug ( "Side columns empty." );

          sbMainBody.AppendLine ( "<!-- CENTER CENTER BODY COLUMN -->" );
          sbMainBody.AppendLine ( sbCentreBody.ToString ( ) );
          sbMainBody.AppendLine ( "<!-- CLOSING LEFT BODY COLUMN -->" );
        }//END only centre body.
        else
        {
          this.LogDebug ( "left columns has content." );

          if ( sbLeftBody.Length > 0
            && sbCentreBody.Length > 0
            && sbRightBody.Length == 0 )
          {
            this.LogDebug ( "Add Left column to body (no right column)" );

            float leftWidth = leftColumnPercentage - Left_Col_Right_Margin - Left_Col_Right_Margin;
            this.LogDebug ( " left margin: {0}, leftColumnPercentage: {1}$.",
             Left_Col_Left_Margin, leftColumnPercentage );

            sbMainBody.AppendLine ( "<!-- OPENING LEFT BODY COLUMN -->" );
            sbMainBody.AppendFormat ( "<div style='margin-left:{0}%; width:{1}%;float:left;'>\r\n",
              Left_Col_Left_Margin, leftColumnPercentage ); 

            sbMainBody.AppendLine ( sbLeftBody.ToString ( ) );

            sbMainBody.AppendLine ( "<!-- CLOSING LEFT BODY COLUMN -->" );
            sbMainBody.AppendLine ( "</div>" );

            this.LogDebug ( "Add center column to body" );

            float centerWidth = centerColumPercentage - Center_Col_Left_Margin- Center_Col_Right_Margin;
            this.LogDebug ( "leftColumnPercentage: {0}, width: {1}",
              ( Center_Col_Left_Margin ), centerWidth );

            sbMainBody.AppendLine ( "<!-- CENTER CENTER BODY COLUMN -->" );

            sbMainBody.AppendFormat ( "<div style='margin-left:{0}%;width: {1}%;float:right'  >\r\n",
              (Center_Col_Left_Margin ), centerWidth );

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
              this.LogDebug ( "Add right column to body (no left column)" );

              sbMainBody.AppendLine ( "<!-- OPENING RIGH BODY COLUMN -->" );
              sbMainBody.AppendFormat ( "<div style='width:{0}%;  float: right;'>\r\n", rightColumnPercentage );

              sbMainBody.AppendLine ( sbRightBody.ToString ( ) );

              sbMainBody.AppendLine ( "<!-- CLOSING LEFT BODY COLUMN -->" );
              sbMainBody.AppendLine ( "</div>" );

              this.LogDebug ( "Add center column to body" );

              sbMainBody.AppendLine ( "<!-- CENTER CENTER BODY COLUMN -->" );
              sbMainBody.AppendFormat ( "<div style='margin-left:0; width:{0}%; ;'>\r\n",
                 ( centerColumPercentage - Right_Col_Left_Margin ) );

              sbMainBody.AppendLine ( sbCentreBody.ToString ( ) );

              sbMainBody.AppendLine ( "<!-- CLOSING CENTER BODY COLUMN -->" );
              sbMainBody.AppendLine ( "</div>" );
            }
            else
            {
              this.LogDebug ( "Add left column to body" );

              sbMainBody.AppendLine ( "<!-- OPENING LEFT BODY COLUMN -->" );
              sbMainBody.AppendFormat ( "<div style='width:{0}%; float:left;  padding-right: 10px;'>\r\n", leftColumnPercentage );

              sbMainBody.AppendLine ( sbLeftBody.ToString ( ) );

              sbMainBody.AppendLine ( "</div>" );
              sbMainBody.AppendLine ( "<!-- CLOSING LEFT BODY COLUMN -->" );

              this.LogDebug ( "Add right column to body" );

              sbMainBody.AppendLine ( "<!-- OPENING RIGHT BODY COLUMN -->" );
              sbMainBody.AppendFormat ( "<div style='width:{0}%; float:right'>\r\n", rightColumnPercentage );

              sbMainBody.AppendLine ( sbRightBody.ToString ( ) );

              sbMainBody.AppendLine ( "</div>" );
              sbMainBody.AppendLine ( "<!-- CLOSING RIGHT BODY COLUMN -->" );

              this.LogDebug ( "Add center column to body" );

              sbMainBody.AppendLine ( "<!-- CENTER CENTER BODY COLUMN -->" );
              sbMainBody.AppendFormat ( "<div style='margin-left:{0}%; width:{1}%' >\r\n",
                ( leftColumnPercentage + 1 ), ( centerColumPercentage - 2 ) );

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

      this.litHistory.Text = sbPageHistoryPills.ToString ( );
      this.litPageMenu.Text = sbPageMenuPills.ToString ( );

      //
      // hide the history and exit command if anonymous access is enabled.
      //
      if ( this.UserSession.AppData.Status == Model.EuAppData.StatusCodes.Anonymous_Edit_Access )
      {
        this.litExitCommand.Visible = false;
        this.litHistory.Visible = false;
        this.litPageMenu.Visible = false;
      }

      this.LogMethodEnd ( "generatePage" );
    }//END generatePage method

    // ==================================================================================
    /// <summary>
    /// Write Debug comments property.
    /// </summary>
    // ---------------------------------------------------------------------------------
    private void generateErrorMessge( StringBuilder sbHtml )
    {
      this.LogMethod ( "generateErrorMessge" );
      String message = this.UserSession.AppData.Message.ToLower ( );
      //
      // Define the error group.
      //
      Evado.UniForm.Model.EuGroup errorGroup = new Evado.UniForm.Model.EuGroup (
       EuLabels.Page_Message_Group_Title,
        Evado.Model.EvStatics.getStringAsHtml ( this.UserSession.AppData.Message ),
        Evado.UniForm.Model.EuEditAccess.Disabled );
      errorGroup.Layout = Evado.UniForm.Model.EuGroupLayouts.Full_Width;

      if ( message.Contains ( "error" ) == true )
      {
        errorGroup.Title = EuLabels.Page_Message_Error_Group_Title;
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
    private void generatePageCommands( )
    {
      this.LogMethod ( "generateCommands" );
      this.LogDebug ( "CommandList.Count: " + this.UserSession.AppData.Page.CommandList.Count );

      //
      // Initialise the methods variables and objects.
      //
      int linkIndex = 0;
      StringBuilder stHtml = new StringBuilder ( );

      //
      // Set page header title.
      //
      this.litHeaderTitle.Text = this.UserSession.AppData.Page.Title;

      //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
      //
      // IF the exit command object is null then no exit page or logout page is to be 
      // displayed.
      //
      if ( this.UserSession.AppData.Page.Exit == null )
      {
        this.litExitCommand.Text = String.Empty;
      }
      else
      {
        //
        // Add a link to previous page.
        //
        if ( this.UserSession.AppData.Page.Exit.Title != String.Empty )
        {
          if ( this.UserSession.AppData.Page.Id != Evado.Model.EvStatics.CONST_DEFAULT_HOME_PAGE_ID
            && this.UserSession.AppData.Page.Exit.Type != Evado.UniForm.Model.EuCommandTypes.Logout_Command )
          {
            this.litExitCommand.Text = this.createCommandLink ( this.UserSession.AppData.Page.Exit );
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
            && this.UserSession.AppData.Status == Evado.UniForm.Model.EuAppData.StatusCodes.Login_Authenticated
            && this.UserSession.AppData.Page.Exit.Title == String.Empty )
          {
            this.litExitCommand.Text = "<a "
              + "class='btn btn-danger' "
              + "href=\"javascript:onPostBack('Login')\" > Logout </a>\r\n";

            linkIndex++;
          }
          else
          {
            this.LogDebug ( "Exit Command is EMPTY" );
          }
        }
      }

      //
      // Edit if there are not page commands.
      //
      if ( this.UserSession.AppData.Page.CommandList.Count == 0 )
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
      foreach ( Evado.UniForm.Model.EuCommand command in this.UserSession.AppData.Page.CommandList )
      {
        //
        // skip null commands
        //
        if ( command == null )
        {
          this.LogDebug ( "Command is null." );
          continue;
        }

        this.LogDebug ( "Command: " + command.Id + " >> " + command.Title );

        //stHtml.AppendLine ( this.createPageCommand ( Command ) );
        stHtml.AppendLine ( "\t\t\t<li>" + this.createPageCommandLink ( command ) + "</li>" );
        linkIndex++;

      }//END iteration loop.
      stHtml.AppendLine ( "\t\t</ul>" );
      stHtml.AppendLine ( "\t</li>" );
      stHtml.AppendLine ( "</ul>" );

      this.litCommandContent.Text = stHtml.ToString ( );

      this.LogMethodEnd ( "generateCommands" );
    }//END generatePageCommands method

    // ==================================================================================
    /// <summary>
    /// This mehod generates the HTMl for a page group.
    /// </summary>
    // ---------------------------------------------------------------------------------
    private String createPageCommandLink(
      Evado.UniForm.Model.EuCommand command )
    {
      //this.LogMethod ( "createPageCommandLink" );
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
        if ( command.Type == Evado.UniForm.Model.EuCommandTypes.Http_Link )
        {
          string Link_Url = command.GetParameter ( Evado.UniForm.Model.EuCommandParameters.Link_Url );

          this.LogDebug ( "Link_Url: " + Link_Url );

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
    /// <param name="command">Evado.UniForm.Model.EuCommand command object</param>
    /// <param name="cssClass">String: Css classes</param>
    /// <returns>Html string</returns>
    // ---------------------------------------------------------------------------------
    private String createCommandLink(
      Evado.UniForm.Model.EuCommand command,
      string cssClass = "btn btn-danger cmd-button" )
    {
      //this.LogMethod ( "createCommandLink" );
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
    /// This mehod generates the HTMl for a page group.
    /// </summary>
    /// <param name="Command">Evado.UniForm.Model.EuCommand command object</param>
    /// <param name="cssClass">String: Css classes</param>
    /// <returns>Html string</returns>
    // ---------------------------------------------------------------------------------
    private String createHttpCommandLink(
      Evado.UniForm.Model.EuCommand Command,
      string cssClass = "btn btn-danger cmd-button" )
    {
      //this.LogMethod ( "createCommandLink" );
      //
      // Initialise methods variables and objects.
      //
      string html = String.Empty;
      string linkUrl = Command.GetParameter ( Evado.UniForm.Model.EuCommandParameters.Link_Url );

      if ( linkUrl != String.Empty )
      {
        html = "<a "
             + "class='" + cssClass + "' "
             + "href=\"" + linkUrl + "\" target=\"_blank\" "
             + ">" + Command.Title + "</a>\r\n";
      }
      return html;
    }

    // ==================================================================================
    /// <summary>
    /// This method generates the HTMl for a menu pills.
    /// </summary>
    // ---------------------------------------------------------------------------------
    private void generateHistoryMenuPills(
      StringBuilder stHtml,
      Evado.UniForm.Model.EuCommand command )
    {
      stHtml.Append ( "<li><a href=\"javascript:onPostBack('" + command.Id + "')\">" + command.Title + "</a></li>" );
    }

    // ==================================================================================
    /// <summary>
    /// This method generates the HTMl for a menu pills.
    /// </summary>
    // ---------------------------------------------------------------------------------
    private void generatePageMenuPills( StringBuilder sbHtml, Evado.UniForm.Model.EuGroup group )
    {
      this.LogMethod ( "generatePageMenuPills" );

      sbHtml.Append ( "<li><a href='#" + group.Id + "-grp'>" + group.Title + "</a></li>" );

      this.LogMethodEnd ( "generatePageMenuPills" );
    }

    // ==================================================================================
    /// <summary>
    /// This method generates the HTMl for a page group.
    /// </summary>
    // ---------------------------------------------------------------------------------
    private void generateGroup(
      StringBuilder sbHtml,
      int Index,
      bool EnableBodyColumns )
    {
      this.LogMethod ( "generateGroup" );
      this.LogDebug ( "Index: {0}, EnableBodyColumns: {1}.", Index, EnableBodyColumns );
      //
      // Initialise the methods variables and objects.
      //

      //
      // Extract the group object from the list.
      //
      this.UserSession.CurrentGroup = this.UserSession.AppData.Page.GroupList [ Index ];

      this.LogDebug ( "Title: " + this.UserSession.CurrentGroup.Title );
      this.LogDebug ( "Group.Status: " + this.UserSession.CurrentGroup.EditAccess );

      if ( this.UserSession.CurrentGroup.FieldList.Count == 0
        && this.UserSession.CurrentGroup.CommandList.Count == 0
        && this.UserSession.CurrentGroup.Description == null )
      {
        this.LogDebug ( "EXIT METHOD: Empty Group" );
        return;
      }

      this.UserSession.GroupFieldWidth = 60;
      switch ( this.UserSession.CurrentGroup.FieldValueColumnWidth )
      {
        case Model.EuFieldValueWidths.Twenty_Percent:
        {
          this.UserSession.GroupFieldWidth = 20;
          break;
        }
        case Model.EuFieldValueWidths.Forty_Percent:
        {
          this.UserSession.GroupFieldWidth = 40;
          break;
        }
        default:
        {
          break;
        }
      }

      //
      // Set the edit access.
      //
      Evado.UniForm.Model.EuEditAccess groupStatus = this.UserSession.CurrentGroup.EditAccess;

      //
      // set the field set attributes.
      //
      this.generateGroupHeader ( sbHtml, this.UserSession.CurrentGroup, EnableBodyColumns );

      //
      // Generate the groupd fields.
      //
      this.generateGroupFields ( sbHtml, Index, this.UserSession.CurrentGroup );

      //
      // Generate the groups commands.
      //
      this.generateGroupCommands ( sbHtml, this.UserSession.CurrentGroup );

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
    private void generateGroupHeader(
      StringBuilder sbHtml,
      Evado.UniForm.Model.EuGroup PageGroup,
      bool EnableBodyColumns )
    {
      this.LogMethod ( "generateGroupHeader" );
      this.LogDebug ( "Group: " + PageGroup.Title );
      this.LogDebug ( "Group.Layout: " + PageGroup.Layout );
      this.LogDebug ( "EnableBodyColumns: " + EnableBodyColumns );
      //
      // Initialise the methods variables and objects.
      //
      int inPercentWidth = PageGroup.GetParameterInt ( Evado.UniForm.Model.EuGroupParameters.Percent_Width );
      int inPixelHeight = PageGroup.GetParameterInt ( Evado.UniForm.Model.EuGroupParameters.Pixel_Height );
      string stFieldName = PageGroup.GetParameter ( Evado.UniForm.Model.EuGroupParameters.Hide_Group_If_Field_Id );
      string stFieldValue = PageGroup.GetParameter ( Evado.UniForm.Model.EuGroupParameters.Hide_Group_If_Field_Value );

      this.LogDebug ( "stFieldName: " + stFieldName );
      //
      // Group the page header divs together
      //
      if ( PageGroup.Layout == Evado.UniForm.Model.EuGroupLayouts.Page_Header )
      {
        PageGroup.Layout = Evado.UniForm.Model.EuGroupLayouts.Full_Width;
      }

      //
      // Define if the group is to be full width.
      //
      string divFieldContainerStyle = " style='";
      string divFieldGroupStyle = " style='";

      if ( PageGroup.Layout == Evado.UniForm.Model.EuGroupLayouts.Page_Header
        || PageGroup.Layout == Evado.UniForm.Model.EuGroupLayouts.Full_Width )
      {
        divFieldGroupStyle += "width:98%; ";
        /*
      }
      else
        if ( PageGroup.Layout == Evado.UniForm.Model.EuGroupLayouts.Full_Width )
      {
        if ( EnableBodyColumns == false )
        {
          divFieldGroupStyle += "width:98%; ";
        }
        else
        {
          divFieldGroupStyle += "width:100%; ";
        }
        
        */
      }
      else
            if ( inPercentWidth > 0 )
      {
        divFieldGroupStyle += "width:" + inPercentWidth + "%; ";
      }


      if ( inPixelHeight > 0 )
      {
        divFieldGroupStyle += " height:" + inPixelHeight + "px; ";
      }

      divFieldGroupStyle += "'";
      divFieldContainerStyle += "'";




      sbHtml.AppendLine ( "<!---  GROUP HEADER --->" );
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

      if ( PageGroup.Title == String.Empty )
      {
        PageGroup.Title = "&nbsp;";
      }
      sbHtml.AppendLine ( "<span class='" + Css_Class_Group_Title + "'> " + PageGroup.Title + "</span>" );
      sbHtml.AppendLine ( "<div class='" + Css_Class_Field_Group_Container + "' " + divFieldContainerStyle + " >\r\n " );

      if ( Global.DebugDisplayOn == true )
      {
        sbHtml.AppendLine ( "<div class='debug-info'>"
          + " L: " + PageGroup.Layout
          + " W: " + inPercentWidth
          + " S: " + PageGroup.EditAccess
          + " GT: " + PageGroup.GroupType
          + "</div>\r\n" );
      }

      //
      // Add the gorups description if it exists.
      //
      string descriptionBackground = String.Empty;
      String description = String.Empty;

      if ( PageGroup.Title == EuLabels.Page_Message_Error_Group_Title )
      {
        descriptionBackground = " Red ";
      }

      if ( PageGroup.Description != null )
      {
        description = PageGroup.Description;
      }
      if ( description != String.Empty )
      {
        Evado.UniForm.Model.EuGroupDescriptionAlignments alignment = PageGroup.DescriptionAlignment;

        string textAlignment = alignment.ToString ( ).Replace ( "_", "-" );
        textAlignment = textAlignment.ToLower ( );

        sbHtml.AppendLine ( "<!--- OPENNING DESCRIPTION --->" );
        sbHtml.AppendLine ( "<div>" );
        sbHtml.AppendLine ( "<div class='grp-description cf " + textAlignment + descriptionBackground + " '>" );

        if ( description.Contains ( "</" ) == true
          || description.Contains ( "/>" ) == true
          || description.Contains ( "[[/" ) == true
          || description.Contains ( "/]]" ) == true
          || description.Contains ( "[/" ) == true
          || description.Contains ( "/]" ) == true )
        {
          description = this.decodeHtmlText ( description );
          sbHtml.AppendLine ( description );
        }
        else
        {
          sbHtml.AppendLine ( Evado.Model.EvStatics.EncodeMarkDown ( description ) );
        }
        sbHtml.AppendLine ( "</div>" );
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
    private void generateGroupFooter(
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
    private Guid getField_ID( String FieldId )
    {
      //
      // Iterate through group fields to find the field's Id 
      //
      foreach ( Evado.UniForm.Model.EuGroup group in this.UserSession.AppData.Page.GroupList )
      {
        foreach ( Evado.UniForm.Model.EuField field in group.FieldList )
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
    private void generateGroupFields(
      StringBuilder sbHtml,
      int GroupIndex,
      Evado.UniForm.Model.EuGroup PageGroup )
    {
      //
      // Initialise the methods variables and objects.
      //
      this.LogMethod ( "generateGroupFields" );
      this.LogDebug ( "GroupIndex: {0}.", GroupIndex );
      this.LogDebug ( "PageGroup.Title: {0}.", PageGroup.Title );
      this.LogDebug ( "PageGroup.GroupType: {0}.", PageGroup.GroupType );
      this.LogDebug ( "PageGroup.Status: {0}.", PageGroup.EditAccess );
      this.LogDebug ( "PageGroup.CmdLayout: {0}.", PageGroup.CmdLayout );
      this.LogDebug ( "PageGroup.Status: {0}.", PageGroup.EditAccess );
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
      for ( int count = 0 ; count < PageGroup.FieldList.Count ; count++ )
      {
        Evado.UniForm.Model.EuField groupField = PageGroup.FieldList [ count ];

        //
        // continue for null field objects.
        //
        if ( groupField == null )
        {
          this.LogDebug ( "SKIP: Field is null" );
          continue;
        }

        this.LogDebug ( "field.Title: " + groupField.Title
          + ", field.FieldId: " + groupField.FieldId
          + ", field.Type: " + groupField.Type
          + ", Status: " + groupField.EditAccess
          + ", Layout: " + groupField.Layout );

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
            this.createTextField ( sbHtml, groupField );
            break;
          }
          case Evado.Model.EvDataTypes.Password:
          {
            this.createPasswordField ( sbHtml, groupField );
            break;
          }
          case Evado.Model.EvDataTypes.Http_Link:
          {
            this.createHttpLinkField ( sbHtml, groupField );
            break;
          }
          case Evado.Model.EvDataTypes.Free_Text:
          {
            this.createFreeTextField ( sbHtml, groupField );
            break;
          }

          case Evado.Model.EvDataTypes.Html_Content:
          {
            this.createHtmlField ( sbHtml, groupField );
            break;
          }
          case Evado.Model.EvDataTypes.Boolean:
          {
            this.createBooleanField ( sbHtml, groupField );
            break;
          }
          case Evado.Model.EvDataTypes.Yes_No:
          {
            this.createYesNoField ( sbHtml, groupField );
            break;
          }
          case Evado.Model.EvDataTypes.Date:
          case Evado.Model.EvDataTypes.Year:
          {
            this.createDateField ( sbHtml, groupField );
            break;
          }
          case Evado.Model.EvDataTypes.Time:
          {
            this.createTimeField ( sbHtml, groupField );
            break;
          }
          case Evado.Model.EvDataTypes.Numeric:
          case Evado.Model.EvDataTypes.Integer:
          {
            this.createNumericField ( sbHtml, groupField );
            break;
          }
          case Evado.Model.EvDataTypes.Streamed_Video:
          {
            this.createStreamedVideoField ( sbHtml, groupField ); // 
            break;
          }
          case Evado.Model.EvDataTypes.External_Image:
          {
            this.createExternalImageField ( sbHtml, groupField );
            break;
          }
          case Evado.Model.EvDataTypes.Image:
          {
            this.createImageField ( sbHtml, groupField );
            break;
          }
          case Evado.Model.EvDataTypes.Binary_File:
          {
            this.createBinaryField ( sbHtml, groupField );
            break;
          }
          case Evado.Model.EvDataTypes.Sound:
          {
            this.createSoundField ( sbHtml, groupField );
            break;
          }
          case Evado.Model.EvDataTypes.Bar_Code:
          {
            this.createTextField ( sbHtml, groupField );
            break;
          }
          case Evado.Model.EvDataTypes.Radio_Button_List:
          {
            this.createRadioButtonField ( sbHtml, groupField );
            break;
          }
          case Evado.Model.EvDataTypes.Special_Quiz_Radio_Buttons:
          {
            this.createQuizRadioButtonField ( sbHtml, groupField );
            break;
          }
          case Evado.Model.EvDataTypes.Horizontal_Radio_Buttons:
          {
            this.createHorizontalRadioButtonField ( sbHtml, groupField );
            break;
          }
          case Evado.Model.EvDataTypes.Selection_List:
          {
            this.createSelectionListField ( sbHtml, groupField );
            break;
          }
          case Evado.Model.EvDataTypes.Check_Box_List:
          {
            this.createCheckboxField ( sbHtml, groupField );
            break;
          }
          case Evado.Model.EvDataTypes.Table:
          {
            this.createTableField ( sbHtml, groupField );
            break;
          }
          case Evado.Model.EvDataTypes.Currency:
          {
            this.createCurrencyField ( sbHtml, groupField );
            break;
          }
          case Evado.Model.EvDataTypes.Email_Address:
          {
            this.createEmailAddressField ( sbHtml, groupField );
            break;
          }
          case Evado.Model.EvDataTypes.Telephone_Number:
          {
            this.createTelephoneNumberField ( sbHtml, groupField );
            break;
          }
          case Evado.Model.EvDataTypes.Analogue_Scale:
          {
            this.createAnalogueField ( sbHtml, groupField );
            break;
          }
          case Evado.Model.EvDataTypes.Name:
          {
            this.createNameField ( sbHtml, groupField );
            break;
          }
          case Evado.Model.EvDataTypes.Address:
          {
            this.createAddressField ( sbHtml, groupField );
            break;
          }
          case Evado.Model.EvDataTypes.Signature:
          {
            this.createSignatureField ( sbHtml, groupField );
            break;
          }
          case Evado.Model.EvDataTypes.Integer_Range:
          case Evado.Model.EvDataTypes.Float_Range:
          case Evado.Model.EvDataTypes.Double_Range:
          {
            this.LogDebug ( "calling createNumericRangeField" );

            this.createNumericRangeField ( sbHtml, groupField );
            break;
          }
          case Evado.Model.EvDataTypes.Date_Range:
          {
            this.LogDebug ( "calling createDateRangeField" );

            this.createDateRangeField ( sbHtml, groupField );
            break;
          }
          case Evado.Model.EvDataTypes.Computed_Field:
          {
            this.createComputedField ( sbHtml, groupField );
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

      this.LogMethodEnd ( "generateGroupFields" );
    }

    //===================================================================================
    /// <summary>
    /// This mehtoid renders a Command title to include icons values in the title
    /// </summary>
    /// <param name="GroupCommand">Evado.UniForm.Model.EuCommand object</param>
    /// <returns>String Title with embedded html</returns>
    //-----------------------------------------------------------------------------------
    private String renderCommandTitleNoImage(
      Evado.UniForm.Model.EuCommand GroupCommand )
    {
      //this.LogMethod ( "renderCommandTitle" );
      //this.LogDebugValue ( "Command.Title: " + Command.Title );
      //
      // Initialise the methods variables and objects.
      //
      List<Evado.UniForm.Model.EuParameter> parameters = this.UserSession.AppData.Page.Parameters;

      string title = GroupCommand.Title;

      return title;
    }
    //===================================================================================
    /// <summary>
    /// This mehtoid renders a Command title to include icons values in the title
    /// </summary>
    /// <param name="GroupCommand">Evado.UniForm.Model.EuCommand object</param>
    /// <returns>String Title with embedded html</returns>
    //-----------------------------------------------------------------------------------
    private String renderCommandTitle(
      Evado.UniForm.Model.EuCommand GroupCommand )
    {
      //this.LogMethod ( "renderCommandTitle" );
      //this.LogDebugValue ( "Command.Title: " + Command.Title );
      //
      // Initialise the methods variables and objects.
      //
      List<Evado.UniForm.Model.EuParameter> parameters = this.UserSession.AppData.Page.Parameters;
      string iconImage = GroupCommand.GetParameter ( Evado.UniForm.Model.EuCommandParameters.Image_Url );

      string title = GroupCommand.Title;

      if ( iconImage != String.Empty )
      {
        string iconImagePath = Global.StaticImageUrl + iconImage;

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
    private void generateGroupCommands(
      StringBuilder sbHtml,
      Evado.UniForm.Model.EuGroup PageGroup )
    {
      //
      // Initialise the methods variables and objects.
      //
      this.LogMethod ( "generateGroupCommands" );
      this.LogDebug ( "Evado.UniForm.Model.EuGroup.Title: " + PageGroup.Title );
      this.LogDebug ( "Evado.UniForm.Model.EuGroup.CmdLayout: " + PageGroup.CmdLayout );

      //
      // If the page group is null exit .
      //
      if ( PageGroup.CommandList == null )
      {
        this.LogDebug ( "Command list null." );
        this.LogMethodEnd ( "generateGroupCommands" );
        return;
      }

      //
      // If the page group does not have commands exit.
      //
      if ( PageGroup.CommandList.Count == 0 )
      {
        this.LogDebug ( "Command list empty." );
        this.LogMethodEnd ( "generateGroupCommands" );
        return;
      }

      //
      // Display the Command group according to layout.
      //
      switch ( PageGroup.CmdLayout )
      {
        case Evado.UniForm.Model.EuGroupCommandListLayouts.Vertical_Orientation:
        {
          this.generateVerticalCommandGroup ( sbHtml, PageGroup );
          break;
        }
        case Evado.UniForm.Model.EuGroupCommandListLayouts.Tiled_Commands:
        {
          this.generateTiledCommandGroup ( sbHtml, PageGroup );
          break;
        }
        case Evado.UniForm.Model.EuGroupCommandListLayouts.Tabular_Commands:
        {
          this.generateTabularCommandGroup ( sbHtml, PageGroup );
          break;
        }
        default:
        {
          this.generateDefaultCommandGroup ( sbHtml, PageGroup );
          break;
        }
      }

      this.LogMethodEnd ( "generateGroupCommands" );
    }

    //===================================================================================
    /// <summary>
    /// This method generates the virtual groupCommand group html content.
    /// </summary>
    /// <param name="sbHtml">StringBuilding containing the html </param>
    /// <param name="PageGroup">PageGroup object</param>
    //-----------------------------------------------------------------------------------
    private void generateVerticalCommandGroup(
      StringBuilder sbHtml,
      Evado.UniForm.Model.EuGroup PageGroup )
    {
      this.LogMethod ( "generateGroupCommandsVertical" );
      this.LogDebug ( "Group.Title: " + PageGroup.Title );
      //
      // Initialise the methods variables and objects.
      //
      int GroupCommandIndex = 0;
      bool bEventRow = false;

      //
      // Setting the default command bacground colours.
      //
      string background_Default = PageGroup.CommandBackground.ToString ( );
      string background_Alternative = PageGroup.AlternativeCommandBackground.ToString ( );
      string background_Highlighted = PageGroup.HighlightedCommandBackground.ToString ( );

      this.LogDebug ( "background_Default: " + background_Default );
      this.LogDebug ( "background_Alternative: " + background_Alternative );
      this.LogDebug ( "background_Highlighted: " + background_Highlighted );

      //
      // Define the table header.
      //
      sbHtml.Append ( "<table  class='NavigationTable'>" );

      foreach ( Evado.UniForm.Model.EuCommand command in PageGroup.CommandList )
      {
        //
        // skip null commands
        //
        if ( command == null )
        {
          this.LogDebug ( "Command is null." );
          continue;
        }

        this.LogDebug ( "Command:" + command.Title );

        if ( command.Type != Evado.UniForm.Model.EuCommandTypes.Null )
        {
          String background = command.GetParameter ( Evado.UniForm.Model.EuCommandParameters.BG_Default );
          String alternative = command.GetParameter ( Evado.UniForm.Model.EuCommandParameters.BG_Alternative );
          String highlighted = command.GetParameter ( Evado.UniForm.Model.EuCommandParameters.BG_Highlighted );

          if ( alternative == "" ) alternative = background;
          if ( background == "" ) background = background_Default;
          if ( alternative == "" ) alternative = background_Alternative;
          if ( highlighted == "" ) highlighted = background_Highlighted;

          this.LogDebug ( "3 background: " + background );
          this.LogDebug ( "3 alternative: " + alternative );
          this.LogDebug ( "3 highlighted: " + highlighted );

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
    /// This method generates the tabular group Command group html content.
    /// </summary>
    /// <param name="sbHtml">StringBuilding containing the html </param>
    /// <param name="PageGroup">PageGroup object</param>
    //-----------------------------------------------------------------------------------
    private void generateTabularCommandGroup(
      StringBuilder sbHtml,
      Evado.UniForm.Model.EuGroup PageGroup )
    {
      this.LogMethod ( "generateTabularCommandGroup" );
      this.LogDebug ( "Group.Title: " + PageGroup.Title );
      //
      // Initialise the methods variables and objects.
      //
      int GroupCommandIndex = 0;
      bool bEventRow = false;

      //
      // Setting the default command bacground colours.
      //
      string background_Default = PageGroup.CommandBackground.ToString ( );
      string background_Alternative = PageGroup.AlternativeCommandBackground.ToString ( );
      string background_Highlighted = PageGroup.HighlightedCommandBackground.ToString ( );

      this.LogDebug ( "background_Default: " + background_Default );
      this.LogDebug ( "background_Alternative: " + background_Alternative );
      this.LogDebug ( "background_Highlighted: " + background_Highlighted );

      var tableHeader = PageGroup.GetParameter ( Model.EuGroupParameters.Command_Tabular_Header );

      String [ ] arTableHeader = tableHeader.Split ( '|' );

      if ( arTableHeader.Length <= 1 )
      {
        this.LogDebug ( "Only one column so user virtual command structure" );
        this.generateTabularCommandGroup ( sbHtml, PageGroup );
        this.LogMethodEnd ( "generateTabularCommandGroup" );
        return;
      }

      //
      // Define the table header.
      //
      sbHtml.Append ( "<table  class='NavigationTable'>" );

      sbHtml.AppendLine ( "<tr> " );
      foreach ( String header in arTableHeader )
      {
        sbHtml.AppendLine ( "<td class=\"" + background_Default + "\"> "
          + header
          + "</td>" );
      }
      sbHtml.AppendLine ( "</tr> " );

      foreach ( Evado.UniForm.Model.EuCommand command in PageGroup.CommandList )
      {
        //
        // skip null commands
        //
        if ( command == null )
        {
          this.LogDebug ( "Command is null." );
          continue;
        }

        this.LogDebug ( "Command:" + command.Title );

        string [ ] arTitle = command.Title.Split ( '|' );

        this.LogDebug ( "3 background: " + background_Default );
        this.LogDebug ( "3 alternative: " + background_Alternative );
        this.LogDebug ( "3 highlighted: " + background_Highlighted );

        sbHtml.AppendLine ( "<tr> " );

        for ( int index = 0 ; index < arTitle.Length && index < arTableHeader.Length ; index++ )
        {
          if ( bEventRow == false )
          {
            if ( index == 0 )
            {
              sbHtml.AppendLine ( "<td class=\"" + background_Default + "\" "
                  + "onmouseover=\"this.className='" + background_Highlighted + "'\" "
                  + "onmouseout=\"this.className='" + background_Default + "'\" "
                  + "onclick=\"javascript:onPostBack('" + command.Id + "')\"> "
                  + arTitle [ index ]
                  + "</td>" );
            }
            else
            {
              sbHtml.AppendLine ( "<td class=\"" + background_Default + "\" > "
                  + arTitle [ index ]
                  + "</td>" );
            }
            bEventRow = true;
          }
          else
          {
            if ( index == 0 )
            {
              sbHtml.AppendLine ( "<td class=\"" + background_Alternative + "\" "
              + "onmouseover=\"this.className='" + background_Highlighted + "'\" "
              + "onmouseout=\"this.className='" + background_Alternative + "'\" "
              + "onclick=\"javascript:onPostBack('" + command.Id + "')\"> "
                  + arTitle [ index ]
              + "</td>" );
            }
            else
            {
              sbHtml.AppendLine ( "<td class=\"" + background_Alternative + "\" > "
                  + arTitle [ index ]
                  + "</td>" );
            }
            bEventRow = false;
          }
        }
        sbHtml.AppendLine ( "</tr>" );

        GroupCommandIndex++;


      }//END page object iteration loop

      sbHtml.Append ( "</table>" );
      this.LogMethodEnd ( "generateTabularCommandGroup" );
    }//END generateTabularCommandGroup method

    //===================================================================================
    /// <summary>
    /// This method generates the titled groupCommand group html content.
    /// </summary>
    /// <param name="sbHtml">StringBuilding containing the html </param>
    /// <param name="PageGroup">PageGroup object</param>
    //-----------------------------------------------------------------------------------
    private void generateTiledCommandGroup(
      StringBuilder sbHtml,
      Evado.UniForm.Model.EuGroup PageGroup )
    {
      this.LogMethod ( "generateTiledCommandGroup" );
      this.LogDebug ( "Group.Title: " + PageGroup.Title );
      //
      // Initialise the methods variables and objects.
      //
      string columnHeaders = PageGroup.GetParameter ( Evado.UniForm.Model.EuGroupParameters.Tiled_Column_Header );
      string [ ] headers;
      int columnPercentage = 0;
      int columnCount = 0;
      String defaultColor = Evado.UniForm.Model.EuBackgroundColours.Default.ToString ( );

      if ( PageGroup.hasParameter ( Evado.UniForm.Model.EuGroupParameters.BG_Default ) == true )
      {
        defaultColor = PageGroup.GetParameter ( Evado.UniForm.Model.EuGroupParameters.BG_Default );
      }
      this.LogDebug ( "Default" + defaultColor );

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
          List<Evado.UniForm.Model.EuCommand> [ ] columns = new List<Evado.UniForm.Model.EuCommand> [ columnCount ];

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

          foreach ( Evado.UniForm.Model.EuCommand command in PageGroup.CommandList )
          {
            //
            // skip null commands
            //
            if ( command == null )
            {
              this.LogDebug ( "Command is null." );
              continue;
            }

            string tiledColumn = command.GetParameter ( Evado.UniForm.Model.EuCommandParameters.Tiled_Column );

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
              columns [ columnPos ] = new List<Evado.UniForm.Model.EuCommand> ( );
            }

            columns [ columnPos ].Add ( command );

            if ( columns [ columnPos ].Count > rowCount )
            {
              rowCount = columns [ columnPos ].Count;
            }

            count += 1;
          }

          for ( int i = 0 ; i < rowCount ; i++ )
          {
            for ( int j = 0 ; j < columnCount ; j++ )
            {
              Evado.UniForm.Model.EuCommand command = null;

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
        tileWidth = PageGroup.GetParameter ( Evado.UniForm.Model.EuGroupParameters.Command_Width );
        this.LogDebug ( "tileWidth" + tileWidth );

        foreach ( Evado.UniForm.Model.EuCommand command in PageGroup.CommandList )
        {
          //
          // skip null commands
          //
          if ( command == null )
          {
            this.LogDebug ( "Command is null." );
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
    /// <param name="groupCommand">Evado.UniForm.Model.EuCommand object</param>
    /// <param name="tileWidth">String: title width </param>
    /// <param name="defaultColor">String the default colour</param>
    //-----------------------------------------------------------------------------------
    private void generateCommandTile(
      StringBuilder sbHtml,
      Evado.UniForm.Model.EuCommand groupCommand,
      string tileWidth,
       String defaultColor )
    {
      this.LogMethod ( "generateCommandTile" );
      this.LogDebug ( "Command.Title: " + groupCommand.Title );
      this.LogDebug ( "tileWidth: " + tileWidth );
      this.LogDebug ( "defaultColor: " + defaultColor );

      //
      // If command is null then exit.
      //
      if ( groupCommand.Type == Evado.UniForm.Model.EuCommandTypes.Null )
      {
        return;
      }

      //
      // Initialise the methods variables and objects.
      //
      string iconImage = groupCommand.GetParameter ( Evado.UniForm.Model.EuCommandParameters.Image_Url );
      string iconImagePath = Global.StaticImageUrl + iconImage;
      string color = groupCommand.GetParameter ( Evado.UniForm.Model.EuCommandParameters.BG_Default );

      this.LogDebug ( "Command iconImage: " + iconImage );
      this.LogDebug ( "Command BG Color: " + color );

      if ( color == "" ) { color = defaultColor; }
      if ( color == "" ) { color = Evado.UniForm.Model.EuBackgroundColours.Default.ToString ( ); }

      this.LogDebug ( "BG Color: " + color );

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
    private void generateDefaultCommandGroup(
      StringBuilder sbHtml,
      Evado.UniForm.Model.EuGroup PageGroup )
    {
      this.LogMethod ( "generateDefaultCommandGroup" );
      this.LogDebug ( "Group.Title: " + PageGroup.Title );
      //
      // Initialise the methods variables and objects.
      //
      int GroupCommandIndex = 0;

      if ( PageGroup.CommandList == null )
      {
        this.LogDebug ( "Command List is null" );
        return;
      }

      this.LogDebug ( "Group.Command count: " + PageGroup.CommandList.Count );

      sbHtml.Append ( "<div class='menu links'>" );

      foreach ( Evado.UniForm.Model.EuCommand command in PageGroup.CommandList )
      {
        if ( command == null )
        {
          this.LogDebug ( "Command is null" );
          continue;
        }
        if ( command.Type == Evado.UniForm.Model.EuCommandTypes.Http_Link )
        {
          sbHtml.Append ( this.createHttpCommandLink ( command ) );
          GroupCommandIndex++;
        }
        else if ( command.Type != Evado.UniForm.Model.EuCommandTypes.Null )
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
