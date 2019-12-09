/**
 * Función que obtiene datos desde el controlador por medio de ajax.
 * @param {any} cTipo GET/POST.
 * @param {any} cUrl Dirección del método a ejecutar.
 * @param {any} oElemento objeto que contiene los elementos id,clase y alerta del html.
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

                $("#" + oElemento.cId).removeClass(oElemento.cClase);

                $("#" + oElemento.cId).addClass("cambiarTamaño");

                $("#" + oElemento.cMensaje).html(data._cMensaje);
            }

        },
        error: function () {

            $("#" + oElemento.cId).removeClass(oElemento.cClase);
            $("#" + oElemento.cId).addClass("cambiarTamaño");


            $("#" + oElemento.cMensaje).removeAttr("hidden");
            $("#" + oElemento.cMensaje).html("Ha ocurrido un error al tratar de cargar los datos del reporte.");
           
        }
    });
}


