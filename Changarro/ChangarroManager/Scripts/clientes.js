$(document).ready(function () {
    $('#example').DataTable({
        "columnDefs": [
            {
                "targets": [0],
                "visible": false,
                "searchable": false
            },

        ],
        "scrollX": false,
        "language": {
            "lengthMenu": "Mostrar _MENU_ clientes por página",
            "info": "Mostrando página _PAGE_ de _PAGES_",
            "search": "Buscar:",
        }
    });
});