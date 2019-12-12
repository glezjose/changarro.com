$("#btnCambiarImagen").click(function (e) {

    SubirImagen()

    e.preventDefault();

    AbrirModal("Perfil/ImagenPerfil", SubirImagen)

});

/**
 * Método para cargar la imagen de perfil utilizando Dropzone
 * */
function SubirImagen() {
  
    $("#imagenPerfilDropzone").dropzone({
        acceptedFiles: 'image/*',        
        dictDefaultMessage: "Deposite su imagen aqui",
        maxFilesize: 2,
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

                            // Remover el archivo de Dropzone
                            $("#btnSubirImagen").click(function (e) {

                                e.preventDefault();

                                done(blob);
                            });
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
}

/**
 * Método para mostrar mensajes de error al guardar la imagen
 * @param {any} oDatos
 */
function MensajeErrorImagen(oDatos) {

    $('#modalGeneral').modal('hide');

    if (oDatos._lStatus) {
        Toast.fire({
            icon: 'success',
            title: '¡Imagen actualizada con éxito!'
        });

        $("#btnCambiarImagen").attr("src", oDatos._cNuevaImagen);
    }
    swalWithBootstrapButtons.fire({
        title: ':(',
        text: oDatos._cNuevaImagen,
        icon: 'error',
        confirmButtonText: 'Aceptar',
    });
}