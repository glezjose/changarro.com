let iIdProducto;
let TablaProducto;

$(document).ready(function () {
    Botones();
    ObtenerListaProductos();

});

function ObtenerListaProductos() {
    TablaProducto = $('#tblProducto').DataTable({
        "ajax": {
            "url": "../Producto/Listar",
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
            { "data": "iIdProducto" },
            { "data": "cNombre" },
            { "data": "NombreCate" },
            { "data": "dPrecio" },
            {
                "data": "dtFechaAlta",
                'render': function (jsonDate) {
                    var newDate = new Date(parseInt(jsonDate.substr(6)));
                    return newDate.format("dd/mm/yyyy");
                }
            },
            {
                "data": "dtFechaAlta",
                'render': function (jsonDate) {
                    var newDate = new Date(parseInt(jsonDate.substr(6)));
                    return newDate.format("dd/mm/yyyy");
                }
            },
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
                "sNext": "Siguiente",
                "sPrevious": "Anterior"
            }
        }
    });

    TablaProducto.on('select', function () {
        iIdProducto = (TablaProducto.rows({ selected: true }).data()[0]['iIdProducto']);
        console.log(iIdProducto)
    });
    TablaProducto.on('click', 'tr', function () {
        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');
            iIdProducto = 0;
            console.log(iIdProducto);
        }
    });
}

function Botones() {
    $("#btnAgregarProducto").click(function (e) {
        e.preventDefault();
        MostrarModal('GET', '../Producto/AddProducto', null, 'IniciarCategoria');
    });
    $("#btnVerProducto").click(function (e) {
        e.preventDefault();

        if (iIdProducto > 0) {
            var cUrl = '../Producto/VisualizarProducto?id=' + iIdProducto;
            MostrarModal('POST', cUrl, { iIdProducto: iIdProducto });
        } else {
            MsjseleccioneRegistro();
        }
    });
    $("#editarproducto").click(function (e) {
        e.preventDefault();
        if (iIdProducto > 0) {
            var cUrl = '../Producto/EditarProducto?id=' + iIdProducto;
            MostrarModal('POST', cUrl, true);
        } else {
            MsjseleccioneRegistro();
        }
    });

    MenuDesplegable();
}
function GuardarProducto() {
    console.log(":D")
    var data = $("#form4").serialize();
    var cUrl = "../Producto/AgregarCategoria";
    console.log("l");
    $.ajax({
        type: "POST",
        url: cUrl,
        data: data,
        datatype: 'JSON',
        success: function (Response) {
            if (Response.result) {
                alert('Se ha insertado un nuevo producto')
            }
            else {
                MsjseleccioneRegistro();
            }
        }

    });
}
/**
 * Función que asigna los eventos de clic a los elementos del menú desplegable "Herramientas"
 * No recibe ningún parámetro
 * No retorna ningún valor
 * */
function MenuDesplegable() {

    $("#descargarPlantilla").click(function () {
        let lExisteSesion = true;
        if (lExisteSesion) {
            window.location.replace("../Producto/DescargarPlantilla");
        }
    });

    $("#importarProductos").click(function () {
        MostrarModal("POST", "../Producto/ImportarProducto", null, InicializarModalImportar);
    });

    $("#exportarProductos").click(function () {
        window.location = "../Producto/ExportarRegistros";
    });
}

/**
 * Función que asigna manejadores de evento al elemento con id = "dropZoneImportar" y al botón "Importar"
 * No recibe ningun parametro
 * No retorna ningun valor
 * */
function InicializarModalImportar() {
    console.log("Implementar función Inicializar Modal Importar");

    var myDrop = new Dropzone("#dropZoneImportar", {
        url: "../Producto/SubirArchivo",
        maxfiles: 1,
        maxFilesize: 5,
        acceptedFiles: ".xls, .xlsx"
    });
    $("#btnImportarPorductos").click(function () {
        $.ajax({
            type: "POST",
            url: "../Producto/ImportarRegistros",
            data: {
                _cNombreArchivo: myDrop.files[0].name.trim()
            },
            dataType: "json",
            success: function(data){
                console.log(data.message);
                TablaProducto.ajax.reload();

            },
            error: function(){
                console.log("error importar");
            }
        });
        $("#modalGeneral").modal("hide");
        
    });
}
