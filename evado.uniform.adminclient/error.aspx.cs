﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Evado.UniForm.AdminClient
{
  public partial class Error : System.Web.UI.Page
  {
    private System.Timers.Timer aTimer;

    protected void Page_Load( object sender, EventArgs e )
    {
      String stContent =
        String.Format ( "Evado.UniForm.AdminClient.Error.Page_Load Method\r\nError Encountered." );
      Global.WriteToEventLog ( this.User.Identity.Name, stContent,
       System.Diagnostics.EventLogEntryType.Information );

      Global.LogValue ( "Load_Page Event Method." );

      //
      // Create a timer with a two second interval.
      //
      aTimer = new System.Timers.Timer ( 2000 );
      // Hook up the Elapsed event for the timer. 
      aTimer.Elapsed += OnTimedEvent;
      aTimer.AutoReset = true;
      aTimer.Enabled = true;
    }

    /// <summary>
    /// This event method redirects to the default page.
    /// </summary>
    /// <param name="source">Object</param>
    /// <param name="e">ElapsedEventArgs arguments</param>
    private void OnTimedEvent( Object source, ElapsedEventArgs e )
    {
      Global.LogValue ( "Load_Page Event Method." );
      Console.WriteLine ( "The Elapsed event was raised at {0:HH:mm:ss.fff}",
                        e.SignalTime );

      Global.LogValue ( "Redirecting to default.asp page." );

      Response.Redirect ( "./default.aspx" );
    }
  }
}