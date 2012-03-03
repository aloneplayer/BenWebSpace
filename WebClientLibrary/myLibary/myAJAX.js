var g_xmlHttp;

function createXMLHttpRequestObject()
{
    var xmlHttp = null;
    if (window.XMLHttpRequest)   // IE 7, Firefox
    {
        xmlHttp = new XMLHttpRequest();
    }
    else if (window.ActiveXObject)   // IE 6 and 7
    {
        xmlHttp = new ActiveXObject("Microsoft.XMLHTTP");
    }
    return xmlHttp;
}

function getAJAXRequest(url, processer)
{
    g_xmlHttp = createXMLHttpRequestObject();

    if (g_xmlHttp != null)
    {
        g_xmlHttp.onreadystatechange = handleStateChange(processer);
        g_xmlHttp.open("GET", url, true);
        g_xmlHttp.send(null);
    }
}

function postAJAXRequest(url, processer)
{
    g_xmlHttp = createXMLHttpRequestObject();

    if (g_xmlHttp != null)
    {
        g_xmlHttp.onreadystatechange = handleStateChange(processer);
        g_xmlHttp.open("POST", url, true);
        g_xmlHttp.send(null);
    }
}

function handleStateChange(processer)
{
    if (g_xmlHttp.readyState == 4)  // response is complete
    {
        // request succeeded
        if (g_xmlHttp.status == 200)
        {
            var xmlDoc = g_xmlHttp.responseXML.documentElement;
            alert(xmlDoc);
            processer(g_xmlHttp);
        }
        else
        {
            alert("Problem retrieving data, the status code is: " + g_xmlHttp.status);
        }
    }
}

function ajaxGetXML(url)
{
    alert("Get XML");
    getAJAXRequest(url, processXML);
}

function processXML(xmlHttp)
{
    var xmlDoc = xmlHttp.responseXML;
    alert(xmlDoc);
}