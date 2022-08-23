<%@ Page Language="c#" Debug="true" ValidateRequest="false" EnableEventValidation="false"
  Inherits="Evado.UniForm.WebClient.ClientPage" CodeBehind="client.aspx.cs" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<!-- COPYRIGHT (C) EVADO HOLDING PTY. LTD.	 2011 - 2020 -->
<head id="Head1" runat="server">
  <title>UniFORM Web Client </title>
  <link rel="icon" type="image/png" href="./favicon.png">
  <style type="text/css" media="screen, print, projection">
    @import "./css/bootstrap.css";
    @import "./css/bootstrap-theme.css";
    @import "./css/bootstrap-datetimepicker.min.css";
    @import "./css/client.main.css";
    @import "./css/menu.css";
    @import "./css/report.css";
  </style>
  <link href="./css/flot.css" rel="stylesheet" type="text/css">
  <script type="text/javascript" src="./js/jquery-1.11.0.min.js"></script>
  <script type="text/javascript" src="./js/parsley.js"></script>
  <script type="text/javascript" src="./js/bootstrap.js"></script>
  <script type="text/javascript" src="./js/underscore.js"></script>
  <script type="text/javascript" src="./js/moment.min.js"></script>
  <script type="text/javascript" src="./js/client.main.js"></script>
  <script type="text/javascript" src="./js/Evado.Form.js"></script>
  <script type="text/javascript" src="./js/jquery.signaturepad.js"></script>
  <script type="text/javascript" src="./css/menu.js"></script>
  <script type="text/javascript">

    // addEventListener support for IE8
    function bindEvent(element, eventName, eventHandler) {
      if (element.addEventListener) {
        element.addEventListener(eventName, eventHandler, false);
      } else if (element.attachEvent) {
        element.attachEvent('on' + eventName, eventHandler);
      }
    }

    /*
    * This is the page load event.
    */
    function pageLoad() {
      console.log("Client: OnPageLoad Event START");

      var lastMeetingStatus = document.getElementById('lastMeetingStatus').value;
      var meetingUrl = document.getElementById('meetingUrl').value;
      var meetingDisplayName = document.getElementById('meetingDisplayName').value;
      var meetingParameters = document.getElementById('meetingParameters').value;
      var meetingStatus = document.getElementById('meetingStatus').value;

      console.log("Client: meetingStatus: " + meetingStatus);
      console.log("Client: lastMeetingStatus: " + lastMeetingStatus);
      console.log("Client: meetingUrl: " + meetingUrl);
      console.log("Client: meetingDisplayName: " + meetingDisplayName);
      console.log("Client: meetingParameters: " + meetingParameters);

      var message = meetingUrl + ";" + meetingDisplayName + ";" + meetingParameters + ";" + meetingStatus;

      console.log("Client: postMessage: " + message);

      window.parent.postMessage(message, '*');

      console.log("Client: OnPageLoad Event FINISH");
    }
  </script>
</head>
<body onload="pageLoad()">
  <form id="form1" runat="server">
  <div>
    <asp:HiddenField ID="__CommandId" runat="server" Value="" />
  </div>
  <script type="text/javascript">
    <!--
    var theForm = document.forms['form1'];
    var postSent = false;
    var computedScript = false;

    if (!theForm) {
      theForm = document.form1;
    }

    function submitForm(form) {
      console.log("submitForm function");
      if (postSent == true) {
        //console.log("Post has been sent.");
        return;
      }

      console.log("window.width" + window.innerWidth);
      console.log("window.height" + window.innerHeight);

      document.getElementById("windowWidth").value = window.innerWidth;
      document.getElementById("windowHeight").value = window.innerHeight;
      postSent = true;
      //get the form element's document to create the input control with
      //(this way will work across windows in IE8)
      var button = form.ownerDocument.createElement('input');
      //make sure it can't be seen/disrupts layout (even momentarily)
      button.style.display = 'none';
      //make it such that it will invoke submit if clicked
      button.type = 'submit';
      //append it and click it
      form.appendChild(button).click();
      //if it was prevented, make sure we don't get a build up of buttons
      form.removeChild(button);


    }

    function onPostBack(commandIdentifier) {
      console.log("onPostBack function");

      console.log(" commandIdentifier" + commandIdentifier);

      if (!theForm.onsubmit || (theForm.onsubmit() != false)) {
        theForm.__CommandId.value = commandIdentifier;
        submitForm(theForm);
      }
    }
    // -->
  </script>
  <asp:Literal ID="litJsLibrary" runat="server" />
  <input id="windowWidth" type="hidden" runat="server" />
  <input id="windowHeight" type="hidden" runat="server" />
  <!--  EVADO FORM PAGE -->
  <div id="page">
    <!-- HEADER -->
    <div id="page-header-section">
      <div id="form-header-buttons-section">
        <div id="form-header-left-buttons">
          <asp:Literal ID="litExitCommand" runat="server" />
        </div>
        <div id="form-header-right-buttons ">
          <asp:Literal ID="litCommandContent" runat="server" />
        </div>
        <div class="header">
          <asp:Literal ID="litHeaderTitle" runat="server" />&nbsp;
        </div>
      </div>
      <div class="form-heading e-menu-bar" id="pageMenu">
        <!-- TODO: breadcrumbs should be generated by litPageMenu -->
        <!-- breadcrumbs for history navigation: 
        <ol class="breadcrumb">
          <li><a href="#">Home</a></li>
          <li><a href="#">Library</a></li>
          <li class="active">Data</li>
        </ol>
        -->
        <asp:Literal ID="litHistory" runat="server" />
        <asp:Literal ID="litPageMenu" runat="server" />
      </div>
    </div>
    <!-- PAGE BODY -->
    <div id="page-body">
      <!-- 
      Page body section built dynamically 
      -->
      <div id="form-body-dynamic-section" class="cf">
        <asp:Literal ID="litPageContent" runat="server" />
        <div id="PagedGroups" visible="false" runat="server">
          <br />
          <table style='width: 100%;'>
            <tr>
              <td style='width: 50%;'>
                <asp:Button ID="btnPageLeft" Text=" << Previous " CssClass="LinkBackground btn btn-danger"
                  Style="width: 100px" OnClick="btnPageLeft_OnClick" runat="server" />
              </td>
              <td style='width: 50%;'>
                <asp:Button ID="btnPageRight" Text=" Next >> " CssClass="LinkBackground btn btn-danger"
                  Style="width: 100px; float: right;" OnClick="btnPageRight_OnClick" runat="server" />
              </td>
            </tr>
          </table>
        </div>
        <asp:Literal ID="litErrorMessage" Visible="false" runat="Server" />
        <!-- 
        Login dialog box
        -->
        <div id="fsLoginBox" class="Fields cf field-group-container" visible="false" style="text-align: center;"
          runat="server">
          <p id="pLogo" runat="server">
            <img id="imgLogo" runat="server" /></p>
          <h2>
            Login</h2>
          <asp:Literal ID="litLoginError" Visible="false" runat="Server" />
          <table id="login-form">
            <tr>
              <td class="Prompt" style="width: 40%;">
                <label for="fldUserId" style="text-align: right; font-size: 10pt;" class="control-label">
                  UserId:
                </label>
              </td>
              <td>
                <input id="fldUserId" type="text" runat="server" size="50" class="form-control" />
              </td>
            </tr>
            <tr>
              <td class="Prompt">
                <label for="fldPassword" style="text-align: right; font-size: 10pt;" class="control-label">
                  Password:</label>
              </td>
              <td>
                <input id="fldPassword" type="password" runat="server" size="50" class="form-control" />
              </td>
            </tr>
            <tr>
              <td>
              </td>
              <td class="submit">
                <asp:Button ID="btnLogin" Text=" Login " CssClass="LinkBackground btn btn-danger"
                  Style="width: 100%" OnClick="btnLogin_OnClick" runat="server" />
              </td>
            </tr>
          </table>
        </div>
        <!-- 
       Object serialisation
       -->
        <asp:Literal ID="litSerialisedLinks" runat="server" />
        <!--
        <asp:fileupload id="TestFileUpload" runat="server" Visible="false" />
        -->
        
  <table id="formFooter">
    <tr>
      <td colspan="3">
      <input id="meetingUrl" type="text" size="500" runat="server" />
      <input id="meetingDisplayName" type="hidden"  runat="server" />
      <input id="meetingParameters" type="hidden"  runat="server" />
      <input id="meetingStatus" type="hidden" runat="server" />
      <input id="lastMeetingStatus" type="hidden" runat="server" />
      <input id="groupNo" type="hidden" runat="server" />
      <input id="pageId" type="hidden" runat="server" />
      </td>
    </tr>
  </table>
      </div>
    </div>
  </div>
  <!-- COPYRIGHT (C) EVADO HOLDING PTY. LTD.	 2011 - 2022 -->
  </form>
</body>
</html>
