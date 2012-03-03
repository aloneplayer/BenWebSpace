// Add event handler
$(function ()
{
    $("#Button_alert").click(function ()
    {
        alert("Hello!");
    });
});

//Show JQuery Version
//alert(jQuery.fn.jquery);

//show window dimensions
function showWindowDimensions()
{
    alert('Window height: ' + jQuery(window).height()); // returns the height of the viewport
    alert('Window width: ' + jQuery(window).width()); // returns the width of the viewport
    alert('Document height: ' + jQuery(document).height()); // returns the height of the document
    alert('Document width: ' + jQuery(document).width()); // returns the width of the document
}


//Create new element

// Format user input
// 45.98897 -> $45.99
function formatInput(input)
{
    var amt = parseFloat(input.value);
    $(input).val('$' + amt.toFixed(2));
}

//为页面的某一区域中的每个链接附加一个confirm
$('#external_links a').click(function ()
{
    return confirm('You are going to visit: ' + this.href);
});
//为页面的某一区域中的每个链接附加一个confirm 非JQuery版
var external_links = document.getElementById('external_links');
var links = external_links.getElementsByTagName('a');
for (var i = 0; i < links.length; i++)
{
    var link = links.item(i);
    link.onclick = function ()
    {
        return confirm('You are going to visit: ' + this.href);
    };
}

// Process Table rows
$(document).ready(function ()
{
    $("#table > tbody > tr:odd").addClass("odd");
});
