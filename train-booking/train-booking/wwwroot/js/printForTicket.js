$(document).ready(function () {
    $('.datatable-table').DataTable({
        dom: 'Bfrtip',
        responsive: false,
        searching: false,
        scrollY: false,
        info: false,
        paging: false,
        ordering: false,
        buttons: [
            'csv', 'excel', 'pdf', 'print'
        ],
        "language": {
            "url": "https://cdn.datatables.net/plug-ins/1.10.20/i18n/Ukrainian.json"
        },
        buttons: {
            buttons: [
                {
                    extend: 'pdf',
                    className: 'btn btn-sm btn btn-outline-primary btn-rounded btn-icon',
                    exportOptions: {
                        columns: ['.visible']
                    }
                },
                {
                    extend: 'print',
                    className: 'btn btn-sm btn btn-outline-success btn-rounded btn-icon',
                    exportOptions: {
                        columns: ['.visible']
                    }
                },
            ]
        }
    });
});