$(document).ready(function () {
    $('.datatable-table').DataTable({
        dom: 'Bfrtip',
        responsive: false,
        searching: true,
        scrollY: false,
        info: false,
        paging: false,
        ordering: true,
        "language": {
            "url": "https://cdn.datatables.net/plug-ins/1.10.20/i18n/Ukrainian.json"
        }
    });
});