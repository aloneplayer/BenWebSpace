$("#clear").click(function () {
    $('#contact_form form')[0].reset();
    return false;
});

$("#send").click(function () {
    //$('#contact_form form').submit();
    var $contactForm = $("#contact_form form");
    if ($contactForm.valid()) {
        $.post("/ContactMessage/Create",
               $contactForm.serialize(),
               function (data) {
                   $("#form_container").html(data);
               },
               'html');
    }
    return false;
});