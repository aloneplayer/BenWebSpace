$(".delete-user-button").click(function() {
    var userId = $(this).attr("meta:id");
    $.post(
		"/UserAdmin/DeleteUser",
		{ id: userId },
		function(data) {
		    $("#user-" + data.object.id).remove();
		},
		"json"
	);
    return false;
});