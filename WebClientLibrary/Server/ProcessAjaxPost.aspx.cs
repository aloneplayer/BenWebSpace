using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Server_ProcessAjaxPost : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string retVal = "";

            string userName = Request["userName"];

            string userPwd = Request["userPwd"];

            if (userName == "HHuang" && userPwd == "123")
            {
                retVal = "Login is success";
            }
            else
            {
                retVal = "Login is false";
            }
            Response.Write(retVal);
        }
    }
}