var iIdProducto = 0;

$(document).ready(function () {
    var Datatables = $('#tblProducto').DataTable({
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
        "select": true
    });

    Datatables.on('select', function () {
        iIdProducto = (Datatables.rows({ selected: true }).data()[0]['iIdProducto']);
        console.log(iIdProducto)

    });

    $("#addproducto").click(function () {

        MostrarModal('../Producto/AddProducto', 'GET', true);

    });

    $("#verproducto").click(function () {
        if (iIdProducto > 0) {
            var cUrl = '../Producto/VisualizarProducto?id=' + iIdProducto;
            MostrarModal(cUrl, 'GET', true);
        } else {
            alert(iIdProducto)
        }

    });

    $("#editarproducto").click(function () {
        if (iIdProducto > 0) {
            var cUrl = '../Producto/EditarProducto?id=' + iIdProducto;
            MostrarModal(cUrl, 'GET', true);
        } else {
            alert("Seleccione un registro");
        }
    });

    MenuDesplegable();
});
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
                alert("Ocurrió un error... T.T")
            }
        }

    });
}

function MenuDesplegable() {
    //Para modal -- id="modalGeneral"

    $("#descargarPlantilla").click(function () {
        let lExisteSesion = true;
        if (lExisteSesion) {
            window.location.replace("../Producto/DescargarPlantilla");
        }
    });

    $("#importarProductos").click(function () {
        MostrarModal("../Producto/ImportarProducto", InicializarModalImportar);
    });
}

function InicializarModalImportar() {
    console.log("Implementar funcion Inicializar Modal Importar");
}
