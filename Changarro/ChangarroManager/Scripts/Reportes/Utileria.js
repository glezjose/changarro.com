/**
 * Esta función obtiene datos de las categorías y productos mediante la petición.
 * @param {any} cTipo GET/POST
 * @param {any} cUrl Dirección del método a ejecutar.
 * @param {any} funcion Nombre de la función a ejecutar.
 */
function ObtenerDatosGraficas(cTipo, cUrl, oElemento, funcion) {

    $.ajax({
        type: cTipo,
        url: ruta + cUrl,
        dataType: "json",
        success: function (data) {

            if (data._cMensaje == null) {

                funcion(data.lstLista)
            }
            else {

                $("#" + oElemento.cId).removeClass(oElemento.cClase);
             
                $("#" + oElemento.cId).addClass("cambiarTamaño");
            
                $("#MensajeError").html(data._cMensaje);
            }
     
        },
        error: function () {

            $("#" + oElemento.cId).removeClass(oElemento.cClase);
            $("#" + oElemento.cId).addClass("cambiarTamaño");
            

            $("#MensajeError").removeAttr("hidden");
            $("#MensajeError").html("Ha ocurrido un error al tratar de cargar los datos del reporte.");
        }
    });
}


