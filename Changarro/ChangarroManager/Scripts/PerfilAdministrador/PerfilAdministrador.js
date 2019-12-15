//*Espera a que el documento este cargado, para obetener las funciones.*/
$(document).ready(function () {
    ObtenerVista("PerfilDatosAdministrador/DatosPerfilAdministrador");
});

/**
 * Método para obtener la vista parcial.
 * @param {any} cUrl Dirección que devuelve la vista parcial.
 */
function ObtenerVista(cUrl) {
    $.ajax({
        type: "POST",
        url: ruta + cUrl,
        dataType: "html",
        success: function (response) {
            MostrarVistas("contenedorDatos", response) //Llama ala función para mostrar la vista parcial.
        }
    });
}

/**
 * Método para obtener la vista Html.
 * @param {any} cElementoID Contiene el ID del elemento html.
 * @param {any} cVista Contiene el Html de la vista parcial.
 */
function MostrarVistas(cElementoID, cVista) {
    $("#" + cElementoID).html(cVista); // Inserta el código html de la vista obtenida dentro del elemento cElementID

    $("#btnEditarDatos").click(function (e) {

        e.preventDefault();

        if ($("#btnEditarDatos").hasClass("editar")) {  //Botón con el id btnEditarDatos tiene la clase "editar"

            $("#btnEditarDatos").html("Guardar Cambios")   //Cambia el texto de "Editar" a "Guardar Cambios"

            $("#DatosAdministrador").removeAttr("disabled");  //Remueve el atributo "disabled" del fieldset del "DatosAdministrador" para poder insertar datos.

            $("#btnEditarDatos").removeClass("editar");  //Remueve la clase "editar" del botón

        } else {  // El botón btnEditarDatos no tiene la clase "editar" guarda los cambios.
            VerificarDatos()
        }
    });
}

/*Método para verificar*/
function VerificarDatos() {

    ValidarFormulario();

    if ($('#Datos').valid() === true) {

        GuardarCambios("PerfilDatosAdministrador/ActualizarDatosAdministrador", { oAdministrador: JSON.stringify(ObjetoAdministrador()) }, "PerfilDatosAdministrador/DatosPerfilAdministrador")
    }
}

/**
 * Método para guardar cambios realizados.
 * @param {any} cUrl Contiene la url del método en el controlador para guardar los cambios.
 * @param {any} data Contiene los datos que se van a guardar.
 * @param {any} cUrlVistas Contine la url de la vista parcial.
 */
function GuardarCambios(cUrl, data, cUrlVistas) {
    $.ajax({
        type: "POST",
        url: ruta + cUrl,
        data: data,
        dataType: "json",
        success: function (response) {
            if (response.lStatus === true) { // Si el estatus es true significa que no hubo errores.
                ObtenerVista(cUrlVistas); // Carga la vista parcial con los nuevos datos.
                Toast.fire({
                    icon: 'success',
                    title: 'Datos Actualizados Correctamente'
                })

            } else {
                Swal.fire({
                    icon: 'warning',
                    title: 'Ocurrio un Problema',
                    text: 'No fue posible conectarse con la base de datos.',
                })
            }
        }
    });
}

/*Método para obtener los datos del formulario.*/
function ObjetoAdministrador() {
    const oAdministrador =
    {
        cNombre: $("#cNombre").val(),
        cApellido: $("#cApellido").val(),
        cTelefono: $("#cTelefono").val(),
        cCorreo: $("#cCorreo").val()
    }

    return oAdministrador;
}