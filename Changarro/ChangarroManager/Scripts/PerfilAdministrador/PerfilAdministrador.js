$(document).ready(function () {
});

function MostrarVistas(cElementoID, cVista) {
    $("#" + cElementoID).html(cVista); // Inserta el código html de la vista obtenida dentro del elemedo cElementID

    $("#btnEditarDatos").click(function (e) {

        e.preventDefault();

        if ($("#btnEditarDatos").hasClass("editar")) {  //si el botón con el id btnEditarDatos tiene la clase "editar"

            $("#btnEditarDatos").html("Guardar Cambios")   //cambia el texto de "Editar" a "Guardar Cambios"

            $("#campoDatosForm").removeAttr("disabled");  //Remueve el atributo "disabled" del fieldset "campoDatosForm" para poder insertar datos

            $("#btnEditarDatos").removeClass("editar");  //Remueve la clase "editar" del boton

        } else {  // si el botón con el id btnEditarDatos no tiene la clase "editar"
            VerificarMisDatos(); //Se llama a este método
        }
    });
}

function GuardarCambios(cUrl, data, cUrlVistas) {
    $.ajax({
        type: "POST",
        url: ruta + cUrl,
        data: data,
        dataType: "json",
        success: function (response) {
            if (response.lStatus === true) { //si el estatus es true significa que no hubo errores
                ObtenerVistas(cUrlVistas); // Vuelve a cargar la vista parcial con los nuevos datos
                Toast.fire({
                    icon: 'success',
                    title: '¡Datos Actualizados!'
                })

            } else {
                alert("no");
            }
        }
    });
}

function LlenarObjetoCliente() {
    const oAdministrador =
    {
        cNombre: $("#cNombre").val(),
        cApellido: $("#cApellido").val(),
        cTelefono: $("#cTelefono").val(),
        cCorreo: $("#cCorreo").val()
    }

    return oAdministrador;
}