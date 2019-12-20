$(document).ready(function () {
    LlenarForm();
});

/**
 * Función para cargar los datos del administrador
 * */
function CargarDatosAdministrador() {
    $.ajax({
        type: 'POST',
        url: '/ChangarroManager/Perfil/PerfilAdministrador/CargarDatosAdministrador',
        dataType: 'json',
        async: false,
        data: { "iIdCliente": iIdCliente },
        success: function (data) {
            LlenarForm(data);
        }
    });
}

/**
 * Función para llenar el formulario del administrador
 * @param {any} items Objeto con los datos del administrador
 */
function LlenarForm(items) {
    $("#nombrecliente").val(items.cNombre);
    $("#apellidocliente").val(items.cApellido);
    $("#correocliente").val(items.cTelefono);
    $("#telefonocliente").val(items.cCorreo);
    $("#imagencliente").val(items.cImagen);
}

