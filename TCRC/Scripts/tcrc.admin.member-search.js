$.fn.exists = function () {
	return this.length !== 0;
};

(function ($) {
	$(document).ready(function () {
		$('#tbl-search-results').DataTable({
			'ordering': false
		});
	});
}(jQuery));