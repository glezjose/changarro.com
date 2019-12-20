function funcion() {

    Continuar();
    $('#myTab a[href="#detallesPanel"]').click(function (e) {
        e.preventDefault();
        $('.progress-bar').css('width', '33%');
        $('.progress-bar').html('Paso 1 de 3');
        CambiarBtn(1);
    });

    $('#myTab a[href="#descripcionPanel"]').click(function (e) {
        e.preventDefault();
        $('.progress-bar').css('width', '66%');
        $('.progress-bar').html('Paso 2 de 3');
        CambiarBtn(2);
    });

    $('#myTab a[href="#imagenPanel"]').click(function (e) {
        e.preventDefault();
        $('.progress-bar').css('width', '100%');
        $('.progress-bar').html('Paso 3 de 3');
        $('#myTab a[href="#imagenPanel"]').tab('show');
        CambiarBtn(3);
    });
    CKEDITOR.replace('editor');
}
function Continuar() {
    $('#continuar').click(function (e) {
        e.preventDefault();
        ValidarCampos();
        var validar = $('#form4').valid();
        if (validar) {
            $('.progress-bar').css('width', '66%');
            $('.progress-bar').html('Paso 2 de 3');
            $('#myTab a[href="#descripcionPanel"]').tab('show');
            $('#continuar').attr('id', 'continuar2');
            Continuar2();
        }
    });
}

function Continuar2() {
    $('#continuar2').click(function (e) {
        e.preventDefault();
        $('.progress-bar').css('width', '100%');
        $('.progress-bar').html('Paso 3 de 3');
        $('#myTab a[href="#imagenPanel"]').tab('show');
        $('#continuar2').attr('id', 'guardar');
        $('#guardar').text("Guardar");
        GuardarProducto();
    });
}

function GuardarProducto() {

    let lStatus;

    $('#guardar').click(function (e) {

        $(this).data('clicked', true);

        e.preventDefault();

        console.log("xd");

        if (iIdProducto > 0) {

            EditarProducto();
        }
        else {

            AgregaProducto();
        }

    });

    $("#imgProductoDropzone").dropzone({
        //autoProcessQueue: false
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
        url: ruta + 'Producto/AgregarProducto',
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

                            if (lStatus === true) {
                                console.log(lStatus);
                                done(blob);

                            }
                            
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

}

function CambiarBtn(paso) {
    if (paso == 1) {
        $('#modalProductosFooter').html('<button id="continuar" type="button" class="btn btn-warning">Continuar</button>');
        Continuar();
        console.log('1');
    }
    else if (paso == 2) {
        $('#modalProductosFooter').html('<button id="continuar2" type="button" class="btn btn-warning">Continuar</button>');
        Continuar2();
        console.log('2');
    }
    else if (paso == 3) {
        $('#modalProductosFooter').html('<button id="guardar" type="button" class="btn btn-warning">Guardar</button>');
        GuardarProducto();
        console.log('3');
    }
}

function ValidarCampos() {
    $('#form4').validate({
        rules: {
            cNombre: {
                required: true,
                maxlength: 20,
            },
            dPrecio: {
                number: true,
                required: true
            },
            iCantidad: {
                number: true,
                required: true
            }
        },
        messages: {
            cModelo: {
                required: "Se requiere de este campo",
                minlength: "Requiere de 6 caracteres"
            },
            dPrecio: {
                required: "Ingrese una cantidad",
                number: "Número invalido",
            },
            iCantidad: {
                required: "Ingrese una cantidad",
                number: "Número invalido",
            }
        }

    });
}