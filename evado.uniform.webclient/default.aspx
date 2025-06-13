<%@ Page Language="c#" Debug="true" ValidateRequest="false" EnableEventValidation="false"
  Inherits="Evado.UniForm.WebClient.DefaultPage" CodeBehind="default.aspx.cs" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<!-- COPYRIGHT (C) EVADO HOLDING PTY. LTD.	 2011 - 2025 -->
<head id="Head1" runat="server">
  <title>UniFORM Web Client </title>
  <link rel="icon" type="image/png" href="./favicon.png" />
  <style type="text/css" media="screen, print, projection">
    @import "./css/bootstrap.css";
    @import "./css/client.video.css";
  </style>
  <!-- 
    <style>
    whereby-embed {
      height: 700px;
    }
    </style>
    -->
  <script type="module" src="https://cdn.srv.whereby.com/embed/v1.js"></script>
  <script type="text/javascript" src="./js/jquery-1.11.0.min.js"></script>
  <script type="text/javascript" src="./js/evado.video.js"></script>
  <script type="text/javascript">
    // addEventListener support for IE8
    function bindEvent(element, eventName, eventHandler) {
      if (element.addEventListener) {
        element.addEventListener(eventName, eventHandler, false);
      } else if (element.attachEvent) {
        element.attachEvent('on' + eventName, eventHandler);
      }
    }

    // Listen to message from child window
    bindEvent(window, 'message', function (e) {
      console.log("Default: _____________________________________________________________ ");
      console.log("Default: MESSAGE EVENT STARTED. ");

      if (e.data == null) {
        console.log("Default:EXIT message data empty. ");
        return;
      }
      console.log("Default: Message data: " + e.data);

      var meetingStatus = document.getElementById('meetingStatus').value;
      console.log("Default: current meetingStatus: " + meetingStatus);

      var value = e.data + "";
      var values = value.split(";");

      if (values.length > 3) {
        console.log("Default: Parameter meetingStatus: " + values[3]);
        document.getElementById('meetingUrl').value = values[0];
        document.getElementById('meetingDisplayName').value = values[1];
        document.getElementById('meetingParameters').value = values[2];
        document.getElementById('meetingStatus').value = values[3];

        console.log("Default: meetingStatus: " + document.getElementById('meetingStatus').value);
        console.log("Default: meetingUrl: " + document.getElementById('meetingUrl').value);
        console.log("Default: meetingDisplayName: " + document.getElementById('meetingDisplayName').value);
        console.log("Default: meetingParameters: " + document.getElementById('meetingParameters').value);

        Evado.Video.displayMeeting();

        Evado.Video.setMeetingUrl();
      }
      console.log("Default: MESSAGE EVENT FINISH. ");
    });


    /*
    * This is the page load event.
    */
    function pageLoad() {

      Evado.Video.displayMeeting();
    }
  </script>
</head>
<body onload="pageLoad()">
  <form id="pageForm" runat="server">
    <div id="meeting">
      <whereby-embed id="whereby" name="whereby" room="" minimal background="on" chat="off" people="off' leaveButton="on" />
    </div>
    <div id="client">
      <iframe id="clientFrame" name="clientFrame" src="./client.aspx" width="200" height="100"
        runat="server"></iframe>
    </div>
    <table id="formFooter">
      <tr>
        <td colspan="3">
          <input id="meetingUrl" type="hidden" runat="server" />
          <input id="meetingDisplayName" type="hidden" runat="server" />
          <input id="meetingParameters" type="hidden" runat="server" />
          <input id="meetingStatus" type="hidden" value="Null" runat="server" />
        </td>
      </tr>
      <tr>
        <td class="left">
          <asp:Literal ID="litCopyright" runat="Server" />
        </td>
        <td class="center">
          <asp:Literal ID="litFooterText" runat="Server" />
        </td>
        <td class="right">
          <asp:Literal ID="litVersion" runat="Server" />
        </td>
      </tr>
    </table>
    <!-- COPYRIGHT (C) EVADO HOLDING PTY. LTD.	 2011 - 2025 -->
  </form>
</body>
</html>
