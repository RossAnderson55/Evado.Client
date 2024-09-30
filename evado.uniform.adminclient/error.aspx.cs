using System;
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

    protected void Page_Load( object sender, EventArgs e )
    {
      String stContent =
        String.Format ( "Evado.UniForm.AdminClient.Error.Page_Load Method." );
      Global.WriteToEventLog ( this.User.Identity.Name, stContent,
       System.Diagnostics.EventLogEntryType.Information );

      Global.LogValue ( "Evado.UniForm.AdminClient.Error.Load_Page Event Method." );

      Response.Redirect ( "./default.aspx" );

      Global.OutputtDebugLog ( );
    }

    /// <summary>
    /// This event method redirects to the default page.
    /// </summary>
    /// <param name="source">Object</param>
    /// <param name="e">ElapsedEventArgs arguments</param>
    private void OnTimedEvent( Object source, ElapsedEventArgs e )
    {
      Global.LogValue ( "Evado.UniForm.AdminClient.Error.OnTimedEvent Event Method." );

      Console.WriteLine ( "The Elapsed event was raised at {0:HH:mm:ss.fff}",
                        e.SignalTime );

      Global.LogValue ( "Redirecting to default.asp page." );

      Global.OutputtDebugLog ( ); 

      Response.Redirect ( "./default.aspx", true );
    }
  }
}