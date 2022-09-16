<%@ Page Language="c#" Debug="true" ValidateRequest="false" EnableEventValidation="false"
  Inherits="Evado.UniForm.WebClient.MeetingPage" CodeBehind="meeting.aspx.cs" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<!-- COPYRIGHT (C) EVADO HOLDING PTY. LTD.	 2011 - 2020 -->
<head id="Head1" runat="server">
  <title>UniFORM Web Client </title>
  <link rel="icon" type="image/png" href="./favicon.png" />
  <style type="text/css" media="screen, print, projection">
    @import "./css/bootstrap.css";
  </style>
  <style type="text/css" media="screen, print, projection">
    body
    {
      font-size: 10pt;
    }
    #formFooter 
    {
      font-size: 8pt;
      width: 100%;
    }

    #formFooter, .left
    { 
      padding: 5px;
      text-align: left ;
      width:33%;
    }
    #formFooter, .right
    { 
      padding: 5px;
      text-align: right ;
      width:33%;
    }
    #formFooter, .center
    { 
      padding: 5px;
      text-align: center ;
    }

    #videoDiv
    {
      background-color: #EAEAEA;
      vertical-align: middle;
      text-align: center;
      padding: 10px;
    }
    
    #videoFrame
    {
    }
    
    #messageDiv
    {
      margin-top: 300px;
      background-color: #EAEAEA;
      vertical-align: middle;
      text-align: center;
    }
    #messageDiv h1
    {
      margin: 20px;
      padding: 100px;
      text-align: center;
      vertical-align: middle;
    }
  </style>
  <script type="text/javascript" src="./js/jquery-1.11.0.min.js"></script>

</head>
<body>
  <form id="pageForm" runat="server">
  <div>
    <div id="videoDiv" runat="server">
      <iframe id="videoFrame" name="videoFrame" src="" width="900" height="800" allow="camera; microphone; speaker" runat="server">
      </iframe>
    </div>
    <div id="messageDiv" runat="server">
      <h1>
        Evado Virtual Meeting</h1>
    </div>
    <asp:Literal ID="litErrorMessage" Visible="false" runat="Server" />
  </div>
  <table id="form-footer" style="font-size: 8pt;">
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
