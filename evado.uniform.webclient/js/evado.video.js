var Evado = Evado || {};

$(function () {
  //
  // the client frame resize function.
  //
  var displayMeeting = function () {
    console.log("meeting: _____________________________________________________________ ");
    console.log("meeting: displayMeeting STARTED");

    var windowWidth = window.innerWidth;
    var windowHeight = window.innerHeight;
    var meetingDisplayName = "" + document.getElementById("meetingDisplayName").value;
    var meetingUrl = "" + document.getElementById("meetingUrl").value;
    var meetingParameters = "" + document.getElementById("meetingParameters").value;
    var meetingDiv = document.getElementById("meeting");
    var meetingFrame = document.getElementById("meetingFrame");
    var clientDiv = document.getElementById("client");
    var clientFrame = document.getElementById("clientFrame");
    var footerHeight = $("#formFooter").height();
    var frameMargin = 10;
    var divMargin = 15;
    var rightColumnWidthRatio = 0.33;
    var leftColumnWidthRatio = 0.66;
    var clientWidth = windowWidth;
    var clientHeight = windowHeight - footerHeight - divMargin;

    $("#formFooter").css({ width: (windowWidth-5) });
    console.log("meeting: windowWidth: " + windowWidth + ", windowHeight: " + windowHeight);
    console.log("meeting: footerHeight: " + footerHeight);
    console.log("meeting: clientWidth: " + clientWidth + ", clientHeight: " + clientHeight);

    console.log("meeting: meetingUrl: " + meetingUrl);
    console.log("meeting: meetingDisplayName: " + meetingDisplayName);
    console.log("meeting: meetingParameters: " + meetingParameters);
    //
    // Initialise the layout.
    //
    console.log("meeting: INITIALISE THE LAYOUT");
    $("#meeting").css({ display: "none" })
    //$("#meeting").css({ width: "0" })
    //$("#client").css({ width: clientWidth })
    clientFrame.setAttribute("width", clientWidth);
    clientFrame.setAttribute("height", clientHeight);

    //
    // if the video url is empty then close the iframe and disply
    // the full page.
    //
    if (meetingUrl == "") {
      console.log("meeting: Video URL is empty no meeting.");
      console.log("meeting: displayMeeting FINISH");
      return;
    } //END no meeting
    //
    // Set video in landscape
    //
    console.log("meeting: SET CLIENT WIDTH");

    clientWidth = windowWidth * leftColumnWidthRatio;
    console.log("meeting: windowWidth: " + windowWidth + ", clientWidth: " + clientWidth);

    //$("#client").css({ width: clientWidth })
    clientFrame.setAttribute("width", clientWidth);
    clientFrame.setAttribute("height", clientHeight);

    console.log("meeting: clientFrame width: " + clientFrame.getAttribute("width") + ", height: " + clientFrame.getAttribute("height"));

    console.log("meeting: INITIALISE MEETING");

    var meetingWidth = (windowWidth * rightColumnWidthRatio);
    console.log("meeting: meetingWidth: " + meetingWidth);
    console.log("meeting: total parent div width: " + (meetingWidth + clientWidth));

    //$("#meeting").css({ width: meetingWidth })
    //$("#meeting").css({ height: clientHeight })

    meetingDisplayName = meetingDisplayName.replace(" ", "&nbsp;");
    //
    // set the video frame attributes.
    //
    var scrPameters = "?";

    if (meetingParameters != null) {
      scrPameters += meetingParameters;
    }
    if (meetingDisplayName != null) {
      if (scrPameters.length > 1) {
        scrPameters += "&";
      }
      scrPameters += "displayName=" + meetingDisplayName;
    }

    var meetingSrc = meetingUrl + scrPameters;
    console.log("meeting: meetingSrc: " + meetingSrc);

    //meetingFrame.setAttribute("src", meetingSrc);
    $("#meeting").css({ display: "block" })


    console.log("meeting: meetingFrame width: " + meetingFrame.getAttribute("width") + ", height: " + meetingFrame.getAttribute("height"));

    var meetingFrameWidth = meetingWidth - (frameMargin * 2);
    var meetingFrameHeight = clientHeight - (frameMargin * 2);

    meetingFrame.setAttribute("width", meetingFrameWidth);
    meetingFrame.setAttribute("height", meetingFrameHeight);

    console.log("meeting: meetingFrame width: " + meetingFrame.getAttribute("width") + ", height: " + meetingFrame.getAttribute("height"));

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
