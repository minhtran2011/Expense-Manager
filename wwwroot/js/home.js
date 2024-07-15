var dataTable;
$(document).ready(function () {
    loadDataTable();
});
function loadDataTable() {
    dataTable = $('#myTable').DataTable({
        "ajax": { url: '/expense/getrecent' },
        "columns": [
            { data: 'category.nameWithIcon', "width": "25%" },
            { data: 'date', "width": "25%" },
            {
                data: 'amount', render: $.fn.dataTable.render.number(','),

                "width": "25%"
            },
            { data: 'description', "width": "25%" }
        ]
    });
}

