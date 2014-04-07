$(document).ready(function () {

	var checkall = function () {
		var id = $(this).attr("data-colid");
		if ($(this).is(':checked')) {
			$('input.colid_' + id).attr('checked', true);
			$('input.colid_' + id).attr('disabled', 'disabled');
			$(this).removeAttr('disabled');
		}
		else {
			$('input.colid_' + id).removeAttr('disabled');
		}
	}

	$('input[type="checkbox"].anonymous').click(checkall);

	var send = function () {
		$('input[type = "checkbox"]').removeAttr('disabled');
	}

	$('button[type="submit"].btn.btn-success').click(send);

	$items = $('input[type="checkbox"].anonymous');
	for (var i = 0; i < $items.length; i++) {
		if ($items.eq(i).attr('checked')) {
			var id = $items.eq(i).attr("colid");
			$('input.colid_' + id).attr('disabled', 'disabled');
			$items.eq(i).removeAttr('disabled');
		}
	}
});