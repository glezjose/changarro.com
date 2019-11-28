$(document).ready(function () {
    ValidarCampos()
});

const Toast = Swal.mixin({
    toast: true,
    position: 'top-end',
    showConfirmButton: false,
    timer: 3000,
    timerProgressBar: true,
    onOpen: (toast) => {
        toast.addEventListener('mouseenter', Swal.stopTimer)
        toast.addEventListener('mouseleave', Swal.resumeTimer)
    }
})

$("#btnLogin").click(function (e) {

    e.preventDefault();

    if ($('#registroForm').valid() === true) {

        const oLogin = {
            cCorreo: $("#cCorreo").val(),
            cContrasenia: $("#cContrasenia").val()
        }

        const cUrl = "IniciarSesion";
        ManejarSesionCliente(oLogin, cUrl);
    }
})

$("#btnRegistro").click(function (e) {

    e.preventDefault();

    if ($('#registroForm').valid() === true) { 

        const oNuevoCliente = {
            cNombre: $("#cNombre").val(),
            cApellido: $("#cApellido").val(),
            cCorreo: $("#cCorreo").val(),
            cContrasenia: $("#cContrasenia").val()
        }

        const cUrl = "ManejarSesionCliente";
        ManejarSesionCliente(oNuevoCliente, cUrl);
    }
})

/**
 * /Función para registrar un nuevo cliente
 * @param {any} oSesionCliente Objeto con los datos del nuevo cliente
 * @param {any} cUrl Cadena con la url del método para registrar clientes
 */
function ManejarSesionCliente(oSesionCliente, cUrl) {
    $.ajax({
        type: "POST",
        url: cUrl,
        data: { oCliente: JSON.stringify(oSesionCliente) },
        async: false,
        success: function (data) {
            if (data._cMensajeError === null) {
                const oMensajesError = data._oUsuario;
                console.log(oMensajesError.cCorreo);
                console.log(oMensajesError.cContrasenia);
            }
            else {
                Toast.fire({
                    icon: 'error',
                    title: data._cMensajeError
                })
            }   
        }
    });
}
