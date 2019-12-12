Dropzone.autoDiscover = false;
$(document).ready(function () {
    ObtenerVistas("Perfil/MisDatos", "v-pills-datos", HabilitarFormularioDatosPerfil); 
    BarraHerramientasPerfil();
    if (Dropzone.instances.length > 0) Dropzone.instances.forEach(dz => dz.destroy())
    //SubirImagen();
});

/**
 * Método para inicializar los tabs de navegación del perfil
 * */
function BarraHerramientasPerfil() {

    $("#v-pills-direcciones-tab").click(function (e) {

        e.preventDefault();

        AligerarCargaVistas("v-pills-direcciones-tab", "Perfil/MisDirecciones", "v-pills-direcciones", AbrirModalDomicilio);

    });

    $("#v-pills-tarjetas-tab").click(function (e) {

        e.preventDefault();

        AligerarCargaVistas("v-pills-tarjetas-tab", "Perfil/MisTarjetas", "v-pills-tarjetas", AbrirModalTarjetas);

    });

    $("#v-pills-historial-tab").click(function (e) {

        e.preventDefault();

        AligerarCargaVistas("v-pills-historial-tab", "Perfil/HistorialCompras", "v-pills-historial", null);

    });
}

/**
 * Método para evitar cargar las vistas parciales mas de una vez al seleccionar un tab
 * @param {any} cIdTab Contiene el id del tab seleccionado
 * @param {any} cUrlVista Contiene la url de la vista de la página parcial
 * @param {any} cIdElemento Contiene el id del elemento que contendrá la vista parcial 
 * @param {any} MiFuncion Función a llamar después de cargar la vista parcial
 */
function AligerarCargaVistas(cIdTab, cUrlVista, cIdElemento, MiFuncion) {

    if ($("#" + cIdTab).hasClass("carga")) {

        ObtenerVistas(cUrlVista, cIdElemento, MiFuncion);

        $("#" + cIdTab).removeClass("carga");

    }
}

/**
 * Método para obtener vistas parciales
 * @param {any} cUrl Dirección del método que devuelve la vista
 */
function ObtenerVistas(cUrl, cIdElemento, MiFuncion) {
    $.ajax({
        type: "POST",
        url: ruta + cUrl,
        dataType: "html",
        success: function (response) {
            MostrarVistas(cIdElemento, response, MiFuncion)
        }
    });
}

/**
 * Método para mostrar la vista html obtenida
 * @param {any} cElementoID Contiene el ID del elemento html que contendrá la vista
 * @param {any} cVista Contiene el html de la vista parcial
 */
function MostrarVistas(cElementoID, cVista, MiFuncion) {
    $("#" + cElementoID).html(cVista);

    MiFuncion();
}

/**
 * Método para guardar cambios realizados.
 * @param {any} cUrl Contiene la url del método a usar en el controlador
 * @param {any} data Contiene los datos a guardar
 */
function GuardarCambios(cUrl, data, cUrlVistas, MiFuncion) {
    $.ajax({
        type: "POST",
        url: ruta + cUrl,
        data: data,
        dataType: "json",
        success: function (response) {
            if (response.lStatus === true) {

                MiFuncion(response._oDatos, cUrlVistas);

            } else {
                swalWithBootstrapButtons.fire({
                    title: ':(',
                    text: 'Ha ocurrido un error al realizar la operación, por favor intente mas tarde',
                    icon: 'error',
                    confirmButtonText: 'Aceptar',
                })
            }
        }
    });
}

