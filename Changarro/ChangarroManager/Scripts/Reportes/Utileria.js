/**
 * Función que obtiene datos desde el controlador por medio de ajax.
 * @param {any} cTipo GET/POST.
 * @param {any} cUrl Dirección del método a ejecutar.
 * @param {any} oElemento objeto que contiene los elementos id,clase y alerta del html para los mensajes.
 * @param {any} funcion Nombre de la función a ejecutar.
 */
function obtenerDatosGraficas(cTipo, cUrl, oElemento, funcion) {

    $.ajax({
        type: cTipo,
        url: ruta + cUrl,
        dataType: "json",
        success: function (data) {

            if (data._cMensaje == null) {

                funcion(data._lstLista)
            }
            else {

                $("#" + oElemento._ciId).removeClass(oElemento._cclaseGrafica);
                $("#" + oElemento._ciId).addClass("cambiarTamaño");

                $("#" + oElemento._cmensajeAdvertencia).removeAttr("hidden");
                $("#" + oElemento._cmensajeAdvertencia).html(data._cMensaje);

            }

        },
        error: function () {

            $("#" + oElemento._ciId).removeClass(oElemento._cclaseGrafica);
            $("#" + oElemento._ciId).addClass("cambiarTamaño");


            $("#" + oElemento._cmensajeError).removeAttr("hidden");
            $("#" + oElemento._cmensajeError).html("Ha ocurrido un error al tratar de cargar los datos del reporte.");
           
        }
    });
}


