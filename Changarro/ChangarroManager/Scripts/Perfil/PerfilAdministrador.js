$(document).ready(function () {
    LlenarForm();
});

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

function LlenarForm(items) {
    $("#nombrecliente").val(items.cNombre);
    $("#apellidocliente").val(items.cApellido);
    $("#correocliente").val(items.cTelefono);
    $("#telefonocliente").val(items.cCorreo);
    $("#imagencliente").val(items.cImagen);
}

