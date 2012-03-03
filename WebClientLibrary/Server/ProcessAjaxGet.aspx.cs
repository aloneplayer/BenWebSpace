using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Server_ProcessAjaxGet : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        string name = Request["name"];
        string age = Request["age"];

        //required to keep the page from being cached on the client's browser
        Response.Expires = -1;
      
        Response.ContentType = "text/plain";
        Response.Write(DateTime.Now.ToString() + name + age);
        Response.End();

    }
}