<%@ Page Language="c#" Debug="true" ValidateRequest="false" EnableEventValidation="false"
  Inherits="Evado.UniForm.WebClient.DefaultPage" CodeBehind="default.aspx.cs" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<!-- COPYRIGHT (C) EVADO HOLDING PTY. LTD.	 2011 - 2020 -->
<head id="Head1" runat="server">
  <title>UniFORM Web Client </title>
  <link rel="icon" type="image/png" href="/favicon.png">
  <style type="text/css" media="screen, print, projection">
    @import "./css/bootstrap.css";
    @import "./css/client.video.css";
  </style>
  <script type="text/javascript" src="./js/jquery-1.11.0.min.js"></script>
  <script type="text/javascript" src="./js/Evado.video.js"></script>
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

      console.log("meeting: Message Event: ");
      if (e.data == null) {
        return;
      }

      console.log("meeting: Message data: " + e.data);

      var value = e.data+"";
      var values = value.split(";");

      if (values.length > 3) {

        document.getElementById('meetingUrl').value = values[0];
        document.getElementById('meetingUserName').value = values[1];
        document.getElementById('meetingParameters').value = values[2];
        document.getElementById('meetingStatus').value = values[3];

        console.log("meeting: meetingStatus: " + document.getElementById('meetingStatus').value);
        console.log("meeting: meetingUrl: " + document.getElementById('meetingUrl').value);
        console.log("meeting: meetingUserName: " + document.getElementById('meetingUserName').value);
        console.log("meeting: meetingParameters: " + document.getElementById('meetingParameters').value);

        Evado.Video.displayMeeting();
      }
    });
  </script>
</head>
<body>
  <form id="pageForm" runat="server">
  <div id="meeting">
    <iframe id='meeting-frame' name='meeting-frame' src='' allow="camera; microphone; fullscreen; speaker; display-capture">
    </iframe>
    <!-- allow="camera; microphone; fullscreen; speaker; display-capture" -->
  </div>
  <div id="client">
    <iframe id='client-frame' name='client-frame' src='./client.aspx' width='100%' height="100%">
    </iframe>
  </div>
  <table id="form-footer">
    <tr>
      <td colspan="3" id="log">
      </td>
    </tr>
    <tr>
      <td colspan="3" style="margin:20px;" >
    <input id="meetingUrl" type="hidden" size="50" runat="server" />
    <input id="meetingUserName" type="hidden" size="50" runat="server" />
    <input id="meetingParameters" type="hidden" size="30" runat="server" />
    <input id="meetingStatus" type="hidden" size="30" runat="server" />
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
  <!-- COPYRIGHT (C) EVADO HOLDING PTY. LTD.	 2011 - 2022 -->
  </form>
</body>
</html>
