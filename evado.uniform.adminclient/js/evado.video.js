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

    console.log("meeting: INITIALISE MEETING");

    var meetingWidth = (windowWidth * rightColumnWidthRatio);
    console.log("meeting: meetingWidth: " + meetingWidth);
    console.log("meeting: total parent div width: " + (meetingWidth + clientWidth));

    $("#meeting").css({ width: meetingWidth })
    $("#meeting").css({ height: clientHeight })

    $("#meeting").css({ display: "block" })

    var wherebyWidth = meetingWidth - (frameMargin * 2);
    var wherebyHeight = clientHeight - (frameMargin * 2);

    console.log("meeting: 2 Whereby CSS width: " + wherebyWidth + ", height: " + wherebyHeight);

    $("#whereby").css({ height: wherebyHeight })
    $("#whereby").css({ width: wherebyWidth })


    console.log("meeting: displayMeeting FINISH");
  };
  //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
  //
  // the client meeting URl function.
  //
  var setMeetingUrl = function () {
    console.log("meeting: _____________________________________________________________ ");
    console.log("meeting: setMeetingUrl STARTED");

    var meetingDisplayName = "" + document.getElementById("meetingDisplayName").value;
    var meetingUrl = "" + document.getElementById("meetingUrl").value;

    //
    // if the video url is empty then close the iframe and disply
    // the full page.
    //
    if (meetingUrl == "") {
      console.log("meeting: Video URL is empty no meeting.");
      console.log("meeting: displayMeeting FINISH");
      return;
    } //END no meeting

    console.log("meeting: INITIALISE MEETING");
    console.log("meeting: meetingUrl: " + meetingUrl);
    console.log("meeting: meetingDisplayName: " + meetingDisplayName);

    var elm = document.querySelector("whereby-embed");

    elm.setAttribute("room", meetingUrl);

    if (meetingDisplayName != null) {
      elm.setAttribute("displayName", meetingDisplayName);
    }

    console.log("meeting: setMeetingUrl FINISH");
  };

  $(window).resize(displayMeeting);

  displayMeeting();

  /*
  * Export any functions which need to be made publically available
  */
  Evado.Video = {
    displayMeeting: displayMeeting,
    setMeetingUrl: setMeetingUrl
  };
});
