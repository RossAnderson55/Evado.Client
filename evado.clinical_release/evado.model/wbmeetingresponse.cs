using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Evado.Model
{
  public class WbMeetingResponse
  {
    /// <summary>
    /// this field defines the meeting identifer value.
    /// </summary>
    public String MeetingId = String.Empty;
    /// <summary>
    /// this field defines the meeting name value.
    /// </summary>
    public String roomName = String.Empty;

    /// <summary>
    /// this field defines the room url value.
    /// </summary>
    public String roomUrl = String.Empty;

    /// <summary>
    /// this field defines the start date value.
    /// </summary>
    public DateTime startDate = DateTime.Now;

    /// <summary>
    /// this field defines the end date value.
    /// </summary>
    public DateTime endDate = DateTime.Now;

    /// <summary>
    /// this field defines the host romm URL value.
    /// </summary>
    public String hostRoomUrl = String.Empty;

    /// <summary>
    /// this field defines viewer room URL value.
    /// </summary>
    public String viewerRoomUrl = String.Empty;

  }
}
