<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WinLoadVsDOMReady.aspx.cs"
    Inherits="WinLoadVsDOMReady" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Window Onload vs DOM Ready</title>
    <script src="jQuery/jquery-1.4.2.js" type="text/javascript"></script>
    <script type="text/javascript">
        window.onload = function ()
        {
            var e = $("#message");
            e.html(e.html() + "window loaded!<br />");
        }

        $(document).ready(function ()
        {
            var e = $("#message");
            e.html(e.html() + "document ready!<br />");
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <img src="/Demo/TimeConsumingRequest" alt="Time" /><br />
    <div id="message" style="font-size: large;">
    </div>
    </form>
</body>
</html>
