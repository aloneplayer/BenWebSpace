<%@ WebHandler Language="C#" Class="GetDataHandler" %>

using System;
using System.Web;

public class GetDataHandler : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        context.Response.Write("Hello World");
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}