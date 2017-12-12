function operateFormatter(value, row, index) {
	return [
			'<div class="table-icons">',
			'<a rel="tooltip" title="Update" class="btn btn-simple btn-warning btn-icon table-action edit" href="javascript:void(0)">',
			'<i class="ti-save"></i>',
        '</a>'
        //,'<a rel="tooltip" title="Delete" class="btn btn-simple btn-danger btn-icon table-action remove" href="javascript:void(0)">',
        //'<i class="ti-close"></i>', '</a>', '</div>', '</a>'

    ].join('');
}

var table = $("#bootstrap-table");

$('[rel="tooltip"]').tooltip();

table.bootstrapTable({
	toolbar : ".toolbar",
	clickToSelect : true,
	showRefresh : true,
	search : true,
	showToggle : true,
	showColumns : true,
	pagination : true,
	searchAlign : 'left',
	pageSize : 8,
	clickToSelect : false,
	pageList : [ 8, 10, 25, 50, 100 ],

	formatShowingRows : function(pageFrom, pageTo, totalRows) {
		// do nothing here, we don't want to show the text "showing x of y
		// from..."
	},
	formatRecordsPerPage : function(pageNumber) {
		return pageNumber + " rows visible";
	},
	icons : {
		refresh : 'fa fa-refresh',
		toggle : 'fa fa-th-list',
		columns : 'fa fa-columns',
		detailOpen : 'fa fa-plus-circle',
		detailClose : 'ti-close'
	}
});



$(window).resize(function() {
	table.bootstrapTable('resetView');
});