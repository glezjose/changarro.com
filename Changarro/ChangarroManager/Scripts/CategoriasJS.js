let iIdCategoria;
let tablaCategoria;
let lEstatus;



/* toma los valores de el formulario y los guarda*/
function cargarTabla() {
    tablaCategoria = $('#tblCategoria').DataTable({
        "ajax": {
            "url": "../Categoria/ListarCat",
            "type": "GET",
            "datatype": "json",

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
            { "data": "cNombre" },
            { "data": "lEstatus" }
        ],
        "select": true,

        "oLanguage": {
            "sProcessing": "Procesando...",
            "sLengthMenu": "Mostrar _MENU_ registros",
            "sZeroRecords": "No se encontraron resultados",
            "sEmptyTable": "Ningún dato disponible en esta tabla",
            "sInfo": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
            "sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
            "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
            "sInfoPostFix": "",
            "sSearch": "Buscar:",
            "sUrl": "",
            "sInfoThousands": ",",
            "sLoadingRecords": "Cargando...",
            "oPaginate": {
                "sFirst": "Primero",
                "sLast": "Último",
                "sNext": "Sig.",
                "sPrevious": "Ant."
            }
        }
    });

}

/**inicia las funciones para categorias */
function IniciarCategoria() {
    cargarTabla();
    ObtenerEstatus();
    Obtenerid();


    $("#btnGuardarCat").click(function (e) {
        e.preventDefault();
        GuardarCategoria();
        tablaCategoria.ajax.reload();
    });
    $("#btnEstatus").click(function (e) {
        e.preventDefault();
        FbotonOn();

    })
}

/*Guarda una nueva categoria*/
function GuardarCategoria() {
    var Data = $("#forcat").serialize();
    
    LlamarMetodo("POST", "../Categoria/AgregarCategoria", Data, false);
}

/**Editar una categoria */
function GuardarCategoria() {
    var Data = $("#forcat").serialize();

    LlamarMetodo("POST", "../Categoria/EditarCategoria", Data, false);
}

function ObtenerEstatus() {
    tablaCategoria.on('select', function () {
        lEstatus = (tablaCategoria.rows({ selected: true }).data()[0]['lEstatus']);
        var uno = document.getElementById('btnEstatus');

        if (lEstatus == true) {

            uno.innerText = "Desactivar";

        }
        else {
            uno.innerText = "Activar"
        }
        console.log(lEstatus)
    });
}
function Obtenerid() {
    tablaCategoria.on('select', function () {
        iIdCategoria = (tablaCategoria.rows({ selected: true }).data()[0]['iIdCategoria']);
        var uno = document.getElementById('btnGuardarCat');
        uno.innerText = "Editar";
        $("#btnGuardarCat").attr("id", "btnEditar");///cambia el id del boton *-*  $("#btnGuardarCat").attr("id", "btnEditar");/
    });
    tablaCategoria.on('click', 'tr', function () {
        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');
            iIdCategoria = 0;
            var uno = document.getElementById('btnEditar');
            $("#btnEditar").attr("id", "btnGuardarCat");
            uno.innerText = "Guardar";

            console.log(iIdCategoria)
        }
    });
}