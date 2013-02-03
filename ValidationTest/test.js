function AddValidationRules(formId) {
    $("#" + formId).validate({
        rules: {
            name: {
                required: true
            },
            email: {
                required: true,
                email: true
            }
        }
    });
}

$(function () {
    AddValidationRules("new_appointment");
});s