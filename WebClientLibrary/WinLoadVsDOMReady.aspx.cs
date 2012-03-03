using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;

public partial class WinLoadVsDOMReady : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Thread.Sleep(5000);

        // BAD PRACTICE!
        //this.Response.ContentType = "image/jpeg";
        //this.Response.TransmitFile("~/Images/wdyc_logo.jpg");
    }
}