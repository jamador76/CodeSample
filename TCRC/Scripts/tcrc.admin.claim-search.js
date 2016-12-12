$.fn.exists = function () {
	return this.length !== 0;
};

(function ($) {
	$('.datepicker').each(function () {
		$(this).removeClass('hasDatepicker').datepicker({
			showOn: 'focus',
			showButtonPanel: true,
			changeMonth: true,
			changeYear: true
		});
	});

	$(document).ready(function () {
		$('#tbl-search-results').DataTable({
			'ordering': false
		});
	});
}(jQuery));