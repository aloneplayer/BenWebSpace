$(".delete-role-button").click(function() {
var roleId = $(this).attr("meta:id");
    $.post(
		"/UserAdmin/DeleteRole",
		{ id: roleId },
		function(data) {
		    $("#role-" + data.object.id).remove();
		},
		"json"
	);
    return false;
});