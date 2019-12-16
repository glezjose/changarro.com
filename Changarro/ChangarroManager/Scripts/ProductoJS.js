let iIdProducto;
let TablaProducto;

$(document).ready(function () {
    Botones();
    ObtenerListaProductos();
    ObtenerId();
    ObtenerEstatusProducto();

});



/**Función que muestra los datos que obtiene en la tabla 
 *No recibe ningún parámetro
 * No retorna ningún valor*/
function ObtenerListaProductos() {
    TablaProducto = $('#tblProducto').DataTable({
        "ajax": {
            "url": "../Producto/Listar",
            "type": "POST",
            "datatype": "json"
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
                    return newDate.format("mm/dd/yyyy");
                }
            },
            {
                "data": "dtFechaAlta",
                'render': function (jsonDate) {
                    var newDate = new Date(parseInt(jsonDate.substr(6)));
                    return newDate.format("mm/dd/yyyy");
                }
            },
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
                "sNext": "Siguiente",
                "sPrevious": "Anterior"
            }
        },
        "info": false
    });
}

function Botones() {
    $("#btnAgregarProducto").click(function (e) {
        e.preventDefault();
        MostrarModal('GET', '../Producto/AddProducto', null, funcion);
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

    $("#btnEditarProducto").click(function (e) {
        e.preventDefault();
        if (iIdProducto > 0) {
            MostrarModal('POST', '../Producto/EditarProducto', { iIdProducto: iIdProducto }, funcion);
        } else {
            MsjseleccioneRegistro();
        }
    });

    $("#btnEstatusProducto").click(function (e) {
        e.preventDefault();
        if (iIdProducto > 0) {
            DesactivarProducto();
        } else {
            MsjseleccioneRegistro();
        }
    });

    MenuDesplegable();
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

    $("#exportarProductosXlsx").click(function () {
        window.location = "../Producto/ExportarRegistros";
    });

    $("#exportarProductosPdf").click(function () {
        window.location.replace("../Producto/ExportarRegistrosPdf");
    });

    $("#btncategoria").click(function (e) {
        e.preventDefault();
        MostrarModal("POST", "../Categoria/Categoria", null, IniciarCategoria);
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
        url: ruta + "/Producto/SubirArchivo",
        maxfiles: 1,
        maxFilesize: 5,
        acceptedFiles: ".xls, .xlsx",
        clickable: true
    });
    //console.log(myDrop);
    $("#btnImportarPorductos").click(function () {
        console.log(myDrop);

        $.ajax({
            type: "POST",
            url: ruta + "/Producto/ImportarRegistros",
            data: {
                _cNombreArchivo: myDrop.files[0].name.trim()
            },
            dataType: "json",
            success: function (data) {
                console.log(data.message);
                TablaProducto.ajax.reload();

            },
            error: function () {
                console.log("error importar");
            }
        });
        $("#modalGeneral").modal("hide");

    });
}


/**Función para obtener el Id de la fila seleccionada  
 *  No recibe ningún parámetro
 * No retorna ningún valor
 * */
function ObtenerId() {
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

/**Función para agregar un producto */
function AgregaProducto() {

    var data = CKEDITOR.instances.editor.getData();

    var Data = {};
    var Producto = {
        cNombre: $("#cNombre").val(),
        iIdCategoria: $("#iIdCategoria").val(),
        dPrecio: $("#dPrecio").val(),
        iCantidad: $("#iCantidad").val(),
        cDescripcion: $("#cDescripcion").val()
    };
    Data['Producto'] = JSON.stringify(Producto);

    LlamarMetodo("POST", "../Producto/AgregarProducto", Data, false);
    TablaProducto.ajax.reload();
}

/**
 * Método para cargar la imagen de perfil utilizando Dropzone
 * */
function SubirImagen() {

    let oImagen;

    $("#imgProductoDropzone").dropzone({
        acceptedFiles: 'image/*',
        dictDefaultMessage: "Deposite su imagen aqui",
        dictInvalidFileType: "No puedes subir archivos de ese tipo",
        dictFileTooBig: "Archivo demasiado grande ({{filesize}}MiB). Tamaño máximo: {{maxFilesize}}MiB",
        maxFilesize: 5,
        maxFiles: 1,
        init: function () {
            this.on("maxfilesexceeded", function (file) {
                this.removeAllFiles();
                this.addFile(file);
            });
        },
        url: ruta + 'Perfil/SubirImagen',
        success: function (file, response) {
            MensajeErrorImagen(response)
        },
        transformFile: function (file, done) {

            var myDropZone = this;

            // Crear editor de imagen
            var editor = document.createElement('div');
            editor.style.position = 'fixed';
            editor.style.left = 0;
            editor.style.right = 0;
            editor.style.top = 0;
            editor.style.bottom = 0;
            editor.style.zIndex = 9999;
            editor.style.backgroundColor = '#000';
            document.body.appendChild(editor);

            // Crear botón de confirmación para recortar imagen
            var buttonConfirm = document.createElement('button');
            buttonConfirm.style.position = 'absolute';
            buttonConfirm.style.left = '10px';
            buttonConfirm.style.top = '10px';
            buttonConfirm.style.zIndex = 9999;
            buttonConfirm.textContent = 'Aceptar';
            editor.appendChild(buttonConfirm);
            buttonConfirm.addEventListener('click', function () {

                // Get the canvas with image data from Cropper.js
                var canvas = cropper.getCroppedCanvas({
                    width: 120,
                    height: 120
                });

                canvas.toBlob(function (blob) {

                    // Crear thumbnail del archivo Dropzone      
                    myDropZone.createThumbnail(
                        blob,
                        myDropZone.options.thumbnailWidth,
                        myDropZone.options.thumbnailHeight,
                        myDropZone.options.thumbnailMethod,
                        false,
                        function (dataURL) {

                            // Actualizar el thumbnail del archivo                            
                            myDropZone.emit('thumbnail', file, dataURL);                            

                            oImagen = blob;
                            //
                        });
                });

                // Remover el editor de la vista
                document.body.removeChild(editor);
            });

            // Crear un nodo de imagen para Cropper.js
            var image = new Image();
            image.src = URL.createObjectURL(file);
            editor.appendChild(image);

            // Crear objeto Cropper.js
            var cropper = new Cropper(image, { aspectRatio: 1 });
        }
    });

    return oImagen;
}

/**función para editar un producto 
 * No recibe ningún parámetro
 * No retorna ningún valor
 * */
function EditarProducto() {
    var Data = {};
    var Producto = {
        iIdProducto: iIdProducto,
        cNombre: $("#cNombre").val(),
        iIdCategoria: $("#iIdCategoria").val(),
        dPrecio: $("#dPrecio").val(),
        iCantidad: $("#iCantidad").val(),
        cDescripcion: $("#cDescripcion").val()
    };
    Data['Producto'] = JSON.stringify(Producto);

    LlamarMetodo("POST", "../Producto/ActualizaProducto", Data, false);
    iIdProducto = 0;
    TablaProducto.ajax.reload();
}

/**Obtiene el estatus de la fila seleccionada y cambia el boton segun sea el estatus 
 * No recibe ningún parámetro
 * No retorna ningún valor
 * */
function ObtenerEstatusProducto() {
    TablaProducto.on('select', function () {
        lEstatus = (TablaProducto.rows({ selected: true }).data()[0]['lEstatus']);

        if (lEstatus == true) {
            $("#btnEstatusProducto").removeClass("btn-success");
            $("#btnEstatusProducto").addClass("btn-danger");
            $("#btnEstatusProducto").html('<i class="ti-close icon"></i>');

        }
        else {
            $("#btnEstatusProducto").removeClass("btn-danger");
            $("#btnEstatusProducto").addClass("btn-success");
            $("#btnEstatusProducto").html('<i class="ti-check icon"></i>');
        }
        console.log(lEstatus)
    });
}

/**función que cambia el estatus de un producto
 * No recibe ningún parámetro
 * No retorna ningún valor
 * */
function DesactivarProducto() {
    var Data = {};
    var EstatusProducto = { iIdProducto: iIdProducto };

    Data['EstatusProducto'] = JSON.stringify(EstatusProducto);
    swal.fire({
        title: "¿Desea cambiar el estatus del producto?",
        text: "No se podrá revertir el cambio",
        icon: "warning",
        buttons: true,
        buttons: ["Cancelar", "Aceptar"],
        dangerMode: true
    }).then((respuesta) => {

        if (respuesta) {
            LlamarMetodo("POST", "../Producto/CambiarEstatusProducto", Data, false);
            TablaProducto.ajax.reload();
        }
    });
}
