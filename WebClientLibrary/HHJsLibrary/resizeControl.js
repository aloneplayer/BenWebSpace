// Updates the tree control size when the browser or frame is resized
function resizeTree()
{
    var theWidth, theHeight;
    var treeWidthOffset = 21;
    var treeHeightOffset = 70;

    if (window.innerWidth)
    {
        theWidth = window.innerWidth;
    }
    else if (document.documentElement && document.documentElement.clientWidth)
    {
        theWidth = document.documentElement.clientWidth;
    }
    else if (document.body)
    {
        theWidth = document.body.clientWidth;
    }

    if (window.innerHeight)
    {
        theHeight = window.innerHeight;
    }
    else if (document.documentElement && document.documentElement.clientHeight)
    {
        theHeight = document.documentElement.clientHeight;
    }
    else if (document.body)
    {
        theHeight = document.body.clientHeight;
    }
    var treeDiv = igtree_getTreeById("webTreeProjects").Element;

    theWidth = theWidth - treeWidthOffset;
    theHeight = theHeight - treeHeightOffset;
    if (theWidth < 0) theWidth = 0;
    if (theHeight < 0) theHeight = 0;
    treeDiv.style.width = theWidth + "px";
    treeDiv.style.height = theHeight + "px";
}