var Evado = Evado || {};

$(function () {
  //
  // the client frame resize function.
  //
  var displayMeeting = function () {
    console.log("meeting: displayMeeting STARTED");

    var windowWidth = window.innerWidth;
    var windowHeight = window.innerHeight;
    var meetingUserName = "" + document.getElementById("meetingUserName").value;
    var meetingUrl = "" + document.getElementById("meetingUrl").value;
    var meetingParameters = "" + document.getElementById("meetingParameters").value;
    var meetingDiv = document.getElementById("meeting");
    var meetingFrame = document.getElementById("meeting-frame");
    var clientDiv = document.getElementById("client");
    var clientFrame = document.getElementById("client-frame");
    var footerHeight = $("#form-footer").height();
    var videoMargin = 10;
    var rightColumnWidthRatio = 0.3;

    console.log("meeting: windowWidth: " + windowWidth);
    console.log("meeting: windowHeight: " + windowHeight);
    console.log("meeting: footerHeight: " + footerHeight);
    console.log("meeting: meetingUserName: " + meetingUserName);
    console.log("meeting: meetingUrl: " + meetingUrl);
    console.log("meeting: meetingParameters: " + meetingParameters);

    //
    // Initialise the layout.
    //
    $("#meeting").css({ display: "none" })
    $("#client").css({ width: "100%" })
    var clientHeight = windowHeight - footerHeight;
    console.log("meeting: clientHeight: " + clientHeight);
    clientFrame.setAttribute("height", clientHeight);
    clientFrame.setAttribute("width", windowWidth);

    //
    // if the video url is empty then close the iframe and disply
    // the full page.
    //
    if (meetingUrl == "") {
      console.log("meeting: Video URL is empty no meeting.");
      console.log("meeting: client-frame width: " + meetingFrame.getAttribute("width"));
      console.log("meeting: client-frame height: " + meetingFrame.getAttribute("height"));
      console.log("meeting: displayMeeting FINISH");
      return;
    } //END no meeting

    var windowRatio = window.innerWidth / window.innerHeight;
    console.log("meeting: windowRatio: " + windowRatio);

    meetingUserName = meetingUserName.replace(" ", "&nbsp;");
    //
    // set the video frame attributes.
    //
    var scrPameters = "?";

    if (meetingParameters != null) {
      scrPameters += meetingParameters;
    }
    if (meetingUserName != null) {
      if (scrPameters.length > 1) {
        scrPameters += "&";
      }
      scrPameters += "displayName=" + meetingUserName;
    }

    var meetingSrc = meetingUrl + scrPameters;
    console.log("meeting: meetingSrc: " + meetingSrc);

    meetingFrame.setAttribute("src", meetingSrc);
    $("#meeting").css({ display: "block" })

    //
    // Set video in landscape
    //
    if (windowRatio > 1.5) {
      console.log("meeting: Landscape layout");
      document.getElementById("meeting").setAttribute('class', 'video-right');

      var clientHeight = windowHeight - footerHeight;
      var meetingWidth = windowWidth * rightColumnWidthRatio - 20;
      var meetingHeight = clientHeight - 20;

      meetingFrame.setAttribute("width", meetingWidth);
      console.log("meeting: meeting-frame: " + meetingFrame.getAttribute("width"));

      meetingFrame.setAttribute("height", meetingHeight);
      console.log("meeting: meeting-frame: " + meetingFrame.getAttribute("height"));

      meetingHeight = $("#meeting-frame").height();
      console.log("meeting: meetingHeight: " + meetingHeight);

      //
      // set the client frame attributes.
      //    
      clientColumnWidth = windowWidth * (1 - rightColumnWidthRatio);
      console.log("meeting: clientColumnWidth: " + clientColumnWidth);

      $("#client").css({ marginTop: "0" });
      clientFrame.setAttribute("width", clientColumnWidth);
      clientFrame.setAttribute("height", clientHeight);
      console.log("meeting: client-frame: " + clientFrame.getAttribute("width"));
      console.log("meeting: client-frame: " + clientFrame.getAttribute("height"));
    }
    //
    // set video in portature
    //
    else {
      console.log("meeting: Portrature layout");
      var meetingHeight = 200;
      var meetingWidth = windowWidth - 20;
      document.getElementById("meeting").setAttribute('class', 'video-top');

      $("#client").css({ marginTop: meetingHeight });

      var meetingHeight = meetingHeight + 20;
      meetingFrame.setAttribute("Height", meetingHeight);
      meetingFrame.setAttribute("Width", meetingWidth);

      meetingHeight = $("#meeting").height();
      console.log("meeting: meetingHeight: " + meetingHeight);

      var clientHeight = windowHeight - meetingHeight - footerHeight;
      var clientWidth = "100%";

      //
      // set the client frame attributes.
      //    
      $("#client").css({ marginTop: meetingHeight });
      clientFrame.setAttribute("width", clientWidth);
      clientFrame.setAttribute("height", clientHeight);

      console.log("meeting: client-frame: " + clientFrame.getAttribute("width"));
      console.log("meeting: client-frame: " + clientFrame.getAttribute("height"));
    }

    console.log("meeting: displayMeeting FINISH");
  };

  $(window).resize(displayMeeting);

  displayMeeting();


  /*
  * Export any functions which need to be made publically available
  */
  Evado.Video = {
    displayMeeting: displayMeeting
  };
});
