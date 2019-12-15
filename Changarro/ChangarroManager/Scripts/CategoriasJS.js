let iIdCategoria;
let tablaCategoria;
let lEstatus;
let cNombre;
let DatosCategoria;

/* toma los valores de el formulario y los guarda
 * No recibe ningún parámetro
 * No retorna ningún valor
 * */
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
            {
                "data": "lEstatus",
                'render': function (data) {
                    if (data == true) {
                        const activo = '<span id= "activo" class="badge badge-success">Activo</span>';
                        return activo;
                    }
                    else {
                        const inactivo = '<span id="activo" class="badge badge-danger">Inactivo</span>';
                        return inactivo;
                    }
                }
            }
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
    $("#tblCategoria tbody").on('click', 'tr', function () {
        Fila = tablaCategoria.row(this).index();
        DatosCategoria = (tablaCategoria.row(Fila).data());
    });
}


/**inicia las funciones para categorías
 * No recibe ningún parámetro
 * No retorna ningún valor
 * */
function IniciarCategoria() {
    cargarTabla();
    ObtenerEstatus();
    Obtenerid();


    $("#btnGuarda").click(function (e) {
        e.preventDefault();
        let validacion = $("#forcat").valid();
        if (iIdCategoria > 0) {

            if (validacion) {
                EditarCategoria();
            }
        }
        else {
            if (validacion) {
                GuardarCategoria();
            }

        }
        iIdCategoria = 0;
    });
    $("#btnEstatusCat").click(function (e) {
        e.preventDefault();
        if (iIdCategoria > 0) {
            CambiarEstatusCategoria();
        }
        else {
            MsjseleccioneRegistro();
        }

    });
}


/**Extrae el nombre de las categorías
 * No recibe ningún parámetro
 * No retorna ningún valor
 * */
function obtenerNombreCategoria() {
    $("#cNombre").val(DatosCategoria.cNombre);
}


/*Guarda una nueva categoría
 * No recibe ningún parámetro
 * No retorna ningún valor
 * */
function GuardarCategoria() {
    var Data = {};

    var Categoria = {
        cNombre: $("#cNombre").val()
    };

    Data['Categoria'] = JSON.stringify(Categoria);

    LlamarMetodo("POST", "../Categoria/AgregarCategoria", Data, false);
    LimpiarFormularioCategoria();
}


/**Editar una categoría
 * No recibe ningún parámetro
 * No retorna ningún valor
 * */
function EditarCategoria() {
    var Data = {};
    var Categoria = {
        iIdCategoria: iIdCategoria,
        cNombre: $("#cNombre").val()
    };
    Data['Categoria'] = JSON.stringify(Categoria);

    LlamarMetodo("POST", "../Categoria/EditarCategoria", Data, false);
    LimpiarFormularioCategoria();
}


/**obtiene el estatus de la fila seleccionada
 * No recibe ningún parámetro
 * No retorna ningún valor
 * */
function ObtenerEstatus() {
    tablaCategoria.on('select', function () {
        lEstatus = (tablaCategoria.rows({ selected: true }).data()[0]['lEstatus']);

        if (lEstatus == true) {

            $("#btnEstatusCat").removeClass("btn-success");
            $("#btnEstatusCat").addClass("btn-danger");
            $("#btnEstatusCat").html('<i class="ti-close icon"></i>');

        }
        else {

            $("#btnEstatusCat").removeClass("btn-danger");
            $("#btnEstatusCat").addClass("btn-success");
            $("#btnEstatusCat").html('<i class="ti-check icon"></i>');
        }
        console.log(lEstatus);
    });
}


/**obtiene el id de la fila seleccionada
 * No recibe ningún parámetro
 * No retorna ningún valor
 * */
function Obtenerid() {
    tablaCategoria.on('select', function () {

        iIdCategoria = (tablaCategoria.rows({ selected: true }).data()[0]['iIdCategoria']);
        obtenerNombreCategoria();
        $("#btnGuarda").html('<i class="ti-pencil icon"></i>');
        $("#btnGuarda").attr("id", "btnEditar");
    });

    tablaCategoria.on('click', 'tr', function () {
        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');
            iIdCategoria = 0;
            $("#cNombre").val("");

            $("#btnEditar").attr("id", "btnGuarda");
            $("#btnGuarda").html('<i class="ti-plus icon"></i>');
        }
    });
}


/**función para cambiar el estatus de la fila seleccionada
 * No recibe ningún parámetro
 * No retorna ningún valor
 * */
function CambiarEstatusCategoria() {
    var Data = {};
    var EstatusCat = { iIdCategoria: iIdCategoria };

    Data['EstatusCat'] = JSON.stringify(EstatusCat);
    swal.fire({
        title: "¿Desea cambiar el estatus del producto?",
        text: "No se podrá revertir el cambio",
        icon: "warning",
        buttons: true,
        buttons: ["Cancelar", "Aceptar"],
        dangerMode: true
    }).then((respuesta) => {

        if (respuesta) {
            LlamarMetodo("POST", "../Categoria/CambiarEstatusCategoria", Data, false);
            iIdCategoria = 0;
            tablaCategoria.ajax.reload();
        }
    });
    LimpiarFormularioCategoria();
}


/*Limpia el formulario de categoría
 * No recibe ningún parámetro
 * No retorna ningún valor
 * */
function LimpiarFormularioCategoria() {
    iIdCategoria = 0;
    tablaCategoria.ajax.reload();
    $("#cNombre").val("");
    $("#btnEditar").html('<i class="ti-plus icon"></i>');
}

