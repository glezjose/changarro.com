/**
 * Esta función obtiene datos de las categorías y productos mediante la petición.
 * @param {any} cTipo GET/POST
 * @param {any} cUrl Dirección del método a ejecutar.
 * @param {any} funcion Nombre de la función a ejecutar.
 */
function ObtenerCategorias(cTipo, cUrl, funcion) {

    $.ajax({
        type: cTipo,
        url: cUrl,
        dataType: "json",
        success: function (data) {

            if (data._cMensaje == null) {

                funcion(data.lstLista)
            }
            else {

                $("#CategoriasProductos").removeClass("amchartdiv");
             
                $("#CategoriasProductos").addClass("cambiarTamaño");
            
                $("#MensajeError").html(data._cMensaje);
            }
     
        },
        error: function () {

            $("#CategoriasProductos").removeClass("amchartdiv");
            $("#CategoriasProductos").addClass("cambiarTamaño");
            

            $("#MensajeError").removeAttr("hidden");
            $("#MensajeError").html("Ha ocurrido un error al tratar de cargar los datos del reporte.");
        }
    });
}

/**
 * Esta función obtiene datos de los clientes con más compras.
 * @param {any} cTipo GET/POST
 * @param {any} cUrl Dirección del método a ejecutar.
 * @param {any} funcion Nombre de la función a ejecutar.
 */
function ObtenerUsuarios(cTipo, cUrl, funcion) {

    $.ajax({
        type: cTipo,
        url: cUrl,
        dataType: "json",
        success: function (data) {

            if (data._cMensaje == null) {

                funcion(data._lstClientes)
            }
            else {

                $("#ClientesCompras").removeClass("amchartdiv2");

                $("#ClientesCompras").addClass("cambiarTamaño");

                $("#MensajeError").html(data._cMensaje);
            }

        },
        error: function () {

            $("#ClientesCompras").removeClass("amchartdiv2");
            $("#ClientesCompras").addClass("cambiarTamaño");


            $("#MensajeError").removeAttr("hidden");
            $("#MensajeError").html("Ha ocurrido un error al tratar de cargar los datos del reporte.");
        }
    });
}

