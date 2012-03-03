using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Server_ProcessFormPost : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        string name = Request["name"];

        string comment = Request["comment"];

        string retVal = name + ": "+comment;
        Response.ContentType = "text/plain";
        Response.Write(retVal);
        Response.End();

    }
}