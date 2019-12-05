var iIdCategoria = 0;

$(document).ready(function () {
    $("#categoria").click(function (e) {
        e.preventDefault();
        MostrarModal('../Categoria/Categoria', cargarTabla);
       
    });
});

/* toma los valores de el formulario y los guarda*/
function prueba() {
    var data = $("#forcat").serialize();
    var cUrl = "../Categoria/AgregarCategoria";

    $.ajax({
        type: "POST",
        url: cUrl,
        data: data,
        datatype: 'JSON',
        success: function (Response) {
            if (Response.result) {
                alert('Se ha insertado una nueva categoría')
            }
            else {
                alert("Ocurrió un error... T.T")
            }
        }

    });
}

function cargarTabla() {
    var Datatables = $('#tblCategoria').DataTable({
        "ajax": {
            "url": "../Categoria/ListarCat",
            "type": "GET",
            "datatype": "json",
            "dataSrc": 'data'
            
        },
        "columnDefs": [
            {
                "targets": [0],
                "visible": false,
                "searchable": false
            }
        ],
        "columns": [
            { "data": "iIdCategoria" },
            { "data": "cNombre" }
        ],
        "select": true
    });

    Datatables.on('select', function () {
        iIdCategoria = (Datatables.rows({ selected: true }).data()[0]['iIdCategoria']);
        console.log(iIdCategoria)

    });

}