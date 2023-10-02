using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evado.Model
{
  public class WbMeetingRequest
  {
    #region Constants
    /// <summary>
    /// This const defines the view mode
    /// </summary>
    public const string templateType_ViewMode = "viewerMode";

    /// <summary>
    /// this const defines the room patent as uuid (GUID);
    /// </summary>
    public const string roomNamePattern_uuid = "uuid";

    /// <summary>
    /// this const defines the room patent as human short;
    /// </summary>
    public const string roomNamePattern_human = "human-short";

    /// <summary>
    /// this const defines the fields hostRoomUrl string
    /// </summary>
    public const string fields_hostRoomUrl = "hostRoomUrl";

    /// <summary>
    /// this const defines the fields viewerRoomUrl string
    /// </summary>
    public const string fields_roomUrl = "roomUrl";

    /// <summary>
    /// this const defines the fields viewerRoomUrl string
    /// </summary>
    public const string fields_viewerRoomUrl = "viewerRoomUrl";

    /// <summary>
    /// this const defines the fields_endDate string
    /// </summary>
    public const string fields_endDate = "endDate";

    /// <summary>
    /// this const defines the room mode normal
    /// </summary>
    public const string roomMode_normal = "normal";

    /// <summary>
    /// this const defines the room mode group
    /// </summary>
    public const string roomMode_group = "group";

    //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region object fields
    /// <summary>
    /// this const defines the templateType value
    /// </summary>
    public String templateType = templateType_ViewMode;

    /// <summary>
    /// this const indicates if the room is locked
    /// </summary>
    public bool isLocked = true;

    /// <summary>
    /// this const defines the room name prefix value.
    /// </summary>
    public string roomNamePrefix = String.Empty;

    /// <summary>
    /// this const defines the room name prefix value.
    /// </summary>
    public string roomNamePattern = roomNamePattern_human;

    /// <summary>
    /// this const defines the room mode value.
    /// </summary>
    public string roomMode = roomMode_normal;

    /// <summary>
    /// this const defines the start date value.
    /// </summary>
    public DateTime startDate = DateTime.Now;

    /// <summary>
    /// this const defines the end date value value.
    /// </summary>
    public DateTime endDate = DateTime.Now;

    /// <summary>
    /// this const defines array of fields.
    /// </summary>
    public String [ ] fields = { fields_hostRoomUrl };

    
    //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion
  }
}
