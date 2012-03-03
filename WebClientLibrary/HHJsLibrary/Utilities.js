var clientContext =
{
    //Check brower is IE or not
    isIE: navigator.appVersion.indexOf("MSIE") != -1 ? true : false,

    readCookie: function (O)
    {
        var o = "", l = O + "=";
        if (document.cookie.length > 0)
        {
            var i = document.cookie.indexOf(l);
            if (i != -1)
            {
                i += l.length;
                var I = document.cookie.indexOf(";", i);
                if (I == -1)
                    I = document.cookie.length;
                o = unescape(document.cookie.substring(i, I))
            }
        }
        ;
        return o
    },
    writeCookie: function (i, l, o, c)
    {
        var O = "", I = "";
        if (o != null)
        {
            O = new Date((new Date).getTime() + o * 3600000);
            O = "; expires=" + O.toGMTString()
        }
        ;
        if (c != null)
        {
            I = ";domain=" + c
        }
        ;
        document.cookie = i + "=" + escape(l) + O + I
    }
}