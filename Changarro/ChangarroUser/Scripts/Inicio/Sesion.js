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
        RegistrarCliente(oLogin, cUrl);
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

        const cUrl = "RegistrarCliente";
        RegistrarCliente(oNuevoCliente, cUrl);
    }
})

/**
 * /Función para registrar un nuevo cliente
 * @param {any} oNuevoCliente Objeto con los datos del nuevo cliente
 * @param {any} cUrl Cadena con la url del método para registrar clientes
 */
function RegistrarCliente(oNuevoCliente, cUrl) {
    $.ajax({
        type: "POST",
        url: cUrl,
        data: { oCliente: JSON.stringify(oNuevoCliente) },
        async: false,
        success: function (data) {
            console.log(data._oUsuario);
            Toast.fire({
                icon: 'success',
                title: 'Empleado registrado con éxito'
            })
        }
    });
}

