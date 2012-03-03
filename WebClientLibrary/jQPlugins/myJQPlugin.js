// Usage:
//   $.add(3.4);
$.extend({
    add: function (a, b) { return a + b; }
});


// Usage:  
//   $("#input1").alertWhileClick(); 
$.fn.extend({
    alertWhileClick: function ()
    {
        $(this).click(function ()
        {

            alert($(this).val());
        });
    }
});    
       
 

 