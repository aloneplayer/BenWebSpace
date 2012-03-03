/*
KNOWN ISSUES :
- Scrollbars can no be set visible at runtime in NS - dialog_enableScrollBars().
- Dialogs can only be made bigger with the resize resizeDlg() function.
This means that dialogs should be created with 
doDialog(url, '0px', '0px');
or dialog width and height size should be set to 0 (zero), with dlgSetSizeX(0)
and dlgSetSizeY(0) or dlgSetSizeXY(0,0), before resizing.
- Resizing where dialog size is changed to fit content, only works with ONE frame
*/
/*
function mrWebDialog() {
// public
this.Open		= doDialog;
	
// protected
	
// resize the dialog slowly
this.Resize 	= resizeDlg;
	
// functions that just sets the size as
// specified in the parameter
this.SetSizeX 	= dlgSetSizeX;
this.SetSizeY 	= dlgSetSizeY;
this.SetSizeXY 	= dlgSetSizeXY;
}
*/
//****************************************************************************************
// PUBLIC FUNCTIONS
// The functions below, can be used by anyone. These are the interface functions 
// for opening dialogs.
//****************************************************************************************
// Public function
// example : doDialog('dialogPage.aspx', '400px', '500px');
function doDialog(URL, height, width)
{
    if (height == null) height = '0px';
    if (width == null) width = '0px';

    return dialog_openDialog(URL, height, width);
}

function doScrollableDialog(URL, height, width)
{
    if (height == null) height = '0px';
    if (width == null) width = '0px';

    return dialog_openDialog(URL, height, width, "yes");
}

function initReturnValue(retVal)
{
    if (retVal != null)
    {
        try
        {
            top.returnValue = retVal;
        }
        catch (e) { }
    }
}

// public function
// Check if this function is executed in a dialog
// Only working in IE
function isDialog()
{
    return window.dialogTop != null ? true : false;
}


//****************************************************************************************
// PROTECTED FUNCTIONS
// The functions below, should only be used by the public functions and by the
// dialogs.
//****************************************************************************************
// protected function
function closeDialog(retVal)
{
    if (retVal != null && retVal != undefined)
    {
        top.returnValue = retVal;
    }
    top.close();
}

// protected function
// Resize the dialog to fit the content of the dialog.
// Max resize to nMaxWidth x nMaxHeigth pixels.
// Size should be 0 x 0 pixels 'dlgSetSizeXY(0,0)' before calling this.
function resizeDlg(nSpeed, nMaxWidth, nMaxHeigth, center)
{
    var nChange = 1;
    var nMaxW, nMaxH;

    if (nSpeed != null && nSpeed > 0)
        nChange = nSpeed;

    if (nMaxWidth != null && nMaxWidth > 0)
        nMaxW = nMaxWidth;
    else
        nMaxW = screen.availWidth;

    if (nMaxHeigth != null && nMaxHeigth > 0)
        nMaxH = nMaxHeigth;
    else
        nMaxH = screen.availHeight;

    if (center == null) center = true;

    var nDocWidth;
    var nDocHeight;
    if (top.dialogWidth != null)
    {
        //for IE
        var nScrollbarSize = 16;

        nDocWidth = Math.min(document.body.scrollWidth, nMaxW);
        nDocHeight = Math.min(document.body.scrollHeight, nMaxH);

        // make room for scrollbars
        if (document.body.scrollWidth > nDocWidth)
            nDocHeight = Math.min(nDocHeight + nScrollbarSize, nMaxH);
        if (document.body.scrollHeight > nDocHeight)
            nDocWidth = Math.min(nDocWidth + nScrollbarSize, nMaxW);

        resizeX(nChange, nDocWidth);
        resizeY(nChange, nDocHeight);

        if (center) dialog_centerDialog(nDocWidth, nDocHeight);
    }
    else if (top.innerWidth != null)
    {
        // Firefox
        var nScrollbarSize = 15;

        nChange *= 2; // netscape is slower than IE
        var marginTop = 0;
        var marginBottom = 0;
        var marginLeft = 0;

        try { marginTop = parseInt(document.getElementsByTagName("BODY").item(0).style.marginTop); } catch (e) { }
        try { marginBottom = parseInt(document.getElementsByTagName("BODY").item(0).style.marginBottom); } catch (e) { }
        try { marginLeft = parseInt(document.getElementsByTagName("BODY").item(0).style.marginLeft); } catch (e) { }
        marginTop = isNaN(marginTop) ? 0 : marginTop;
        marginBottom = isNaN(marginBottom) ? 0 : marginBottom;
        marginLeft = isNaN(marginLeft) ? 0 : marginLeft;

        var nWantedW = dialog_getThisDocumentWidthNS() - marginLeft; // don't know why left margin is there - but it is working..
        var nWantedH = document.height + marginTop + marginBottom;

        nDocWidth = Math.min(nWantedW, nMaxW);
        nDocHeight = Math.min(nWantedH, nMaxH);

        // make room for scrollbars
        if (nWantedH > nDocHeight)
            nDocWidth = Math.min(nDocWidth + nScrollbarSize, nMaxW);
        if (nWantedW > nDocWidth)
            nDocHeight = Math.min(nDocHeight + nScrollbarSize, nMaxH);

        dialog_centerDialog(nDocWidth, nDocHeight);

        // the scrollbar size is because we need to make the window 
        // big enough for the scrollbar to disappear :o(
        resizeX(nChange, nDocWidth + nScrollbarSize);
        resizeY(nChange, nDocHeight + nScrollbarSize);

        top.innerWidth -= nScrollbarSize;
        top.innerHeight -= nScrollbarSize;
    }
}

// protected function
// just set the width of the dialog to X pixels
function dlgSetSizeX(X)
{
    var minX = 100; // const
    var maxX = screen.availWidth;

    if (top.dialogWidth != null)
        top.dialogWidth = Math.max(Math.min(X, maxX), minX) + 'px';
    else if (top.innerWidth != null)
        top.innerWidth = Math.max(Math.min(X, maxX), minX);
}

// protected function
// just set the height of the dialog to Y pixels
function dlgSetSizeY(Y)
{
    var minY = 100; // const
    var maxY = screen.availHeight;
    if (top.dialogWidth != null)
        top.dialogHeight = Math.max(Math.min(Y, maxY), minY) + 'px';
    else if (top.innerHeight != null)
        top.innerHeight = Math.max(Math.min(Y, maxY), minY);
}

// protected function
function dlgSetSizeXY(X, Y)
{
    dlgSetSizeX(X);
    dlgSetSizeY(Y);
}

// protected function
function resizeX(nSpeed, finalWidth)
{
    var minX = 100; // const

    if (finalWidth > screen.availWidth) finalWidth = screen.availWidth;

    if (finalWidth <= minX) return;
    if (top.dialogWidth != null)
    {
        top.dialogWidth = parseInt(document.body.clientWidth) + 'px';
        var nPrevW = parseInt(document.body.clientWidth);
        var win = top;
        do
        {
            nPrevW = parseInt(document.body.clientWidth);
            win.dialogWidth = (parseInt(top.dialogWidth) + 1) + 'px';
        } while (nPrevW == parseInt(document.body.clientWidth));
        top.dialogWidth = (parseInt(top.dialogWidth) - 1) + 'px';

        var nXPixels = finalWidth - parseInt(document.body.clientWidth);
        var nXIterations = Math.floor(nXPixels / nSpeed);
        nXPixels = isNaN(nXPixels % nSpeed) ? nXPixels : nXPixels % nSpeed;
        for (var x = 0; x < nXIterations; x++)
        {
            top.dialogWidth = (parseInt(top.dialogWidth) + nSpeed) + 'px';
        }
        top.dialogWidth = (parseInt(top.dialogWidth) + nXPixels) + 'px';
    }
    else if (top.innerWidth != null)
    {
        var nXPixels = finalWidth - top.innerWidth;
        var nXIterations = Math.floor(nXPixels / nSpeed);
        nXPixels = isNaN(nXPixels % nSpeed) ? nXPixels : nXPixels % nSpeed;
        for (var x = 0; x < nXIterations; x++)
        {
            top.innerWidth = top.innerWidth + nSpeed;
        }
        top.innerWidth = top.innerWidth + nXPixels;
    }
}

// protected function
function resizeY(nSpeed, finalHeight)
{
    var minY = 75; // const

    if (finalHeight > screen.availHeight) finalHeight = screen.availHeight;

    if (finalHeight <= minY) finalHeight = minY;
    if (top.dialogWidth != null)
    {
        var nYPixels = finalHeight - parseInt(document.body.clientHeight);
        var nYIterations = Math.floor(nYPixels / nSpeed);
        nYPixels = isNaN(nYPixels % nSpeed) ? nYPixels : nYPixels % nSpeed;
        for (var y = 0; y < nYIterations; y++)
        {
            top.dialogHeight = (parseInt(top.dialogHeight) + nSpeed) + 'px';
        }
        top.dialogHeight = (parseInt(top.dialogHeight) + nYPixels) + 'px';
    }
    else if (top.innerHeight != null)
    {
        var nYPixels = finalHeight - top.innerHeight;
        var nYIterations = Math.floor(nYPixels / nSpeed);
        nYPixels = isNaN(nYPixels % nSpeed) ? nYPixels : nYPixels % nSpeed;
        for (var y = 0; y < nYIterations; y++)
        {
            top.innerHeight = top.innerHeight + nSpeed;
        }
        top.innerHeight = top.innerHeight + nYPixels;
    }
}

//****************************************************************************************
// PRIVATE FUNCTIONS
// The functions below, should only be used by the public or protected functions
// above.
//****************************************************************************************
// private function
function dialog_openDialog(URL, height, width, scrollable)
{
    var theHeight = height;
    var theWidth = width;
    var theURL = URL;
    if (navigator.appName == "Microsoft Internet Explorer")
    {
        var args = { opener: window };

        if (arguments.length < 4)
        {
            scrollable = "no";
        }

        options = "resizable:yes;status:no;help:no;scroll:" + scrollable + ";dialogHeight:" + theHeight + ";dialogWidth:" + width + ";";
        try
        {

            return window.showModalDialog(theURL, args, options);
        }
        catch (e)
        {
            alert(GetPopupBlockerWarningMsg());
            return;
        }
    }
    else
    {
        var dlgX = top.screenX + (top.innerWidth / 2) - parseInt(theWidth) / 2;
        var dlgY = top.screenY + (top.innerHeight / 2) - parseInt(theHeight) / 2;

        window.dialog_return_value = "";
        options = "menubar=no,status=no,location=no,dependent=yes,scrollbars=yes,resizable=yes,height=" + theHeight + ",width=" + theWidth + ",screenX=" + dlgX + ",screenY=" + dlgY + ",modal=yes";
        try
        {
            window.open(theURL, "", options);
            return window.dialog_return_value;
        }
        catch (e)
        {
            alert(GetPopupBlockerWarningMsg());
            return;
        }
    }
}

//''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
// RESIZE STUFF

// private function
// places the dialog so that it would be centered if the dialog has the 
// width and height as specified in the parameters.
function dialog_centerDialog(nDocWidth, nDocHeight)
{
    if (top.dialogWidth != null)
    {
        top.dialogLeft = (parseInt(top.dialogLeft) + (document.body.clientWidth - nDocWidth) / 2) + 'px';
        top.dialogTop = (parseInt(top.dialogTop) + (document.body.clientHeight - nDocHeight) / 2) + 'px';
    }
    else if (top.innerWidth != null)
    {
        try
        {
            var dlgX = top.opener.screenX + (top.opener.top.innerWidth - nDocWidth) / 2;
            var dlgY = top.opener.screenY + (top.opener.top.innerHeight - nDocHeight) / 2;
            top.moveTo(dlgX, dlgY);
        } catch (e) { }
    }
}

// private function
// After calling dialog_IsScrollbarsVisible() then 
// dialog_IsScrollbarsVisible.X and dialog_IsScrollbarsVisible.Y
// can be examined to check if any of the scrollbars 
// are visible.
dialog_IsScrollbarsVisible.X = 0; // 1 if X scrollbar is visible
dialog_IsScrollbarsVisible.Y = 0; // 1 if Y scrollbar is visible
function dialog_IsScrollbarsVisible()
{
    dialog_IsScrollbarsVisible.X = 0;
    dialog_IsScrollbarsVisible.Y = 0;

    window.scrollTo(1, 1);
    dialog_IsScrollbarsVisible.X = window.pageXOffset | document.body.scrollLeft;
    dialog_IsScrollbarsVisible.Y = window.pageYOffset | document.body.scrollTop;

    window.scrollTo(0, 0);
}

// private function
// dialog_enableScrollBars() is only working in ie
function dialog_enableScrollBars(bEnable)
{

    if (document.body.scroll != null)
    {
        if (bEnable == null || bEnable)
            document.body.scroll = "auto";
        else
            document.body.scroll = "no";
    }
    else if (window.scrollbars != null)
    {
        // this is not working (TODO ?!?)
        //netscape.security.PrivilegeManager.enablePrivilege("UniversalBrowserWrite");
        if (bEnable == null || bEnable)
            window.scrollbars.visible = true;
        else
            window.scrollbars.visible = false;
    }
}

// private function
// NS way to find exact document width
function dialog_getThisDocumentWidthNS()
{
    try
    {
        var bodyNode = document.getElementsByTagName("body").item(0);
        var tNode = document.createElement("table");
        var trNode = document.createElement("tr");
        var tdNode1 = document.createElement("td");
        var tdNode2 = document.createElement("td");
        var aNode = document.createElement("a");

        tdNode2.appendChild(aNode);
        tdNode2.setAttribute("style", "padding: 0px");
        tdNode1.appendChild(bodyNode.cloneNode(true));
        tdNode1.setAttribute("style", "padding: 0px");
        trNode.appendChild(tdNode1);
        trNode.appendChild(tdNode2);

        tNode.appendChild(trNode);
        tNode.setAttribute("style", "border: 0px");

        bodyNode.appendChild(tNode);

        var nDocWidth = aNode.offsetLeft - 4;

        bodyNode.removeChild(tNode);

        return nDocWidth;
    } catch (e)
    {
        return 0;
    }
}

function GetPopupBlockerWarningMsg()
{
    hWarnMsg = document.getElementById('hPopupBlockerWarning');
    if (hWarnMsg == null)
    {
        return 'Dialog can not be displayed, it may be blocked by Popup Blocker.\nPlease turn off Popup Blocker and try again!';
    }
    else
    {
        return hWarnMsg.value;
    }
}

function openDialog(URL, width, height, args, processReturnValue)
{
    if (height == null) height = 0;
    if (width == null) width = 0;

    if (isIE())
    {
        options = "location:no;resizable:yes;status:no;help:no;scroll:no;dialogWidth:" + width + "px;dialogHeight:" + height + "px";
        try
        {
            var value = window.showModalDialog(URL, args, options);
            return value;
        }
        catch (e)
        {
            alert(GetPopupBlockerWarningMsg());
            return;
        }
    }
    else if (isFirefox3x())
    {
        options = "center:yes;resizable:yes;dialogheight:" + height + ";dialogwidth:" + width;
        try
        {
            var value = window.showModalDialog(URL, args, options);
            return value;
        }
        catch (e)
        {
            alert(GetPopupBlockerWarningMsg());
            return;
        }
    }
    else //firefox 2.x
    {
        options = "center=yes,resizable=yes,height=" + height + ",width=" + width;
        try
        {
            var dialogWin = window.open(URL, "_blank", options);
            window.myAction = this;
            window.myArguments = args;  // Pass args to child

            this.returnAction = function(value)
            {
                if (value != undefined)
                {
                    processReturnValue(value);
                }
            }
            // disable the parent
            window.onclick = function() { dialogWin.focus() };
            //event.cancelBubble = true;  cause exception.
        }
        catch (e)
        {
            alert(GetPopupBlockerWarningMsg());
            return;
        }
    }
}

function dialogOnUnload()
{
    if (isFirefox2x())
    {
        if (window.opener)
        {
            if (window.opener.myAction)
            {
                if (window.opener.myAction.returnAction)
                {
                    window.opener.myAction.returnAction(window.returnValue);
                }
            }
        }
    }
}

function isIE()
{
    var ua = navigator.userAgent.toLowerCase();
    return ua.match(/msie ([\d.]+)/) != null;
}

function isIE7Plus()
{
    var ua = navigator.userAgent.toLowerCase();
    s = ua.match(/msie ([\d.]+)/);
    if (s && s[1])
    {
        var version = parseFloat(s[1]);
        return version >= 7;
    }
    return false;
}

function isFirefox()
{
    var ua = navigator.userAgent.toLowerCase();
    return ua.match(/firefox\/([\d.]+)/) != null;
}


function isFirefox3x()
{
    var ua = navigator.userAgent.toLowerCase();
    s = ua.match(/firefox\/([\d.]+)/);
    if (s && s[1])
    {
        var version = parseFloat(s[1]);
        return version >= 3;
    }
    return false;
}

function isFirefox2x()
{
    var ua = navigator.userAgent.toLowerCase();
    s = ua.match(/firefox\/([\d.]+)/)
    if (s && s[1])
    {
        var version = parseFloat(s[1]);
        return version >= 2 && version < 3;
    }
    return false;
}

function adjustDialogSize(borderId)
{
    var maxWidth = screen.availWidth;
    var maxHeight = screen.availHeight;

    var nDocWidth;
    var nDocHeight;
    if (isIE())
    {
        var nScrollbarSize = 16;

        nDocWidth = Math.min(document.body.scrollWidth, maxWidth);
        nDocHeight = Math.min(document.body.scrollHeight, maxHeight);

        // make room for scrollbars
        if (document.body.scrollWidth > nDocWidth)
            nDocHeight = Math.min(nDocHeight + nScrollbarSize, maxHeight);
        if (document.body.scrollHeight > nDocHeight)
            nDocWidth = Math.min(nDocWidth + nScrollbarSize, maxWidth);

        adjustDialogWidth(nDocWidth);
        adjustDialogHeight(nDocHeight);

        centerDialog(nDocWidth, nDocHeight);
    }
    else if (isFirefox())
    {
        var nScrollbarSize = 18;
        var borderTable = document.getElementById(borderId);
        if (borderTable == null)
        {
            return;
        }
        nDocWidth = borderTable.clientWidth;
        nDocHeight = borderTable.clientHeight;

        if (document.body.scrollWidth > nDocWidth)
            nDocHeight = Math.min(nDocHeight + nScrollbarSize, maxHeight);
        if (document.body.scrollHeight > nDocHeight)
            nDocWidth = Math.min(nDocWidth + nScrollbarSize, maxWidth);

        adjustDialogWidth(nDocWidth);
        adjustDialogHeight(nDocHeight);

        centerDialog(nDocWidth, nDocHeight);
    }
}

function adjustDialogWidth(finalWidth)
{
    var minX = 100; // const
    if (finalWidth <= minX)
    {
        finalWidth = minX;
    }

    if (finalWidth > screen.availWidth)
    {
        finalWidth = screen.availWidth;
    }

    if (isIE())
    {
        top.dialogWidth = finalWidth + 'px';
    }
    else if (isFirefox())
    {
        top.innerWidth = finalWidth;
    }
}

function adjustDialogHeight(finalHeight)
{
    var minY = 75; // const
    if (finalHeight <= minY)
    {
        finalHeight = minY;
    }

    if (finalHeight > screen.availHeight)
    {
        finalHeight = screen.availHeight;
    }

    if (isIE())
    {
    	  if(isIE7Plus())
    	  {   
    	  	  // IE7 & IE8 dialogHeight does not contains the title bar, status bar...
    	  	  top.dialogHeight = finalHeight + 'px'	
    	  }
    	  else
    	  {
            top.dialogHeight = finalHeight + 25 + 'px';
      	}
    }
    else if (isFirefox())
    {
        top.innerHeight = finalHeight;
    }
}

function centerDialog(width, height)
{
    if (isIE())
    {
        var x = (window.screen.availWidth - width) / 2;
        var y = (window.screen.availHeight - height) / 2;
        if (top.dialogLeft) //is modal dialog
        {
            top.dialogLeft = x + 'px';
            top.dialogTop = y + 'px';
        }
        else   //window
        {
            top.moveTo(x, y);
        }
    }
    else if (isFirefox())
    {
        window.screenX = (window.screen.availWidth - window.outerWidth) / 2;
        window.screenY = (window.screen.availHeight - window.outerHeight) / 2;
    }
}