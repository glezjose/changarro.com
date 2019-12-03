$(document).ready(function () {
    ValidarRegistro();
    ValidarLogin();
    BarraHerramientas();

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

function BarraHerramientas() {
    $("#btnLogin").click(function (e) {

        e.preventDefault();

        if ($('#loginForm').valid() === true) {

            const oLogin = {
                cCorreo: $("#cCorreo").val().toLowerCase(),
                cContrasenia: $("#cContrasenia").val()
            }

            ManejarSesionCliente("POST", "IniciarSesion", { oCliente: JSON.stringify(oLogin) }, IniciarSesion);
        }
    })

    $("#btnRegistro").click(function (e) {

        e.preventDefault();

        if ($('#registroForm').valid() === true) {

            const oNuevoCliente = {
                cNombre: $("#cNombre").val(),
                cApellido: $("#cApellido").val(),
                cCorreo: $("#cCorreo").val().toLowerCase(),
                cContrasenia: $("#cContrasenia").val()
            }

            ManejarSesionCliente("POST", "RegistrarCliente", { oCliente: JSON.stringify(oNuevoCliente) }, RegistarCliente);
        }
    })
}

function ManejarSesionCliente(cTipo, cUrl, data, funcion) {
    $.ajax({
        type: cTipo,
        url: cUrl,
        async: false,
        data: data,
        dataType: "json",
        success: function (response) {
            if (response._cMensajeError === null) {
                funcion(response._oUsuario);                
            }
            else {
                Toast.fire({
                    icon: 'error',
                    title: response._cMensajeError
                })
            } 
        }
    });
}

function RegistarCliente(oUsuario) {
    if (oUsuario === null) {
        window.location.href = '/changarro/Producto/Inicio';
    } else {
        console.log(oUsuario);        
        ValidarRegistro(oUsuario.cNombre, oUsuario.cApellido, oUsuario.cCorreo);
        $('#registroForm').valid();
    }
}

function IniciarSesion(oUsuario) {
    if (oUsuario === null) {
        window.location.href = '/changarro/Producto/Inicio';
    } else {
        ValidarLogin(oUsuario.cCorreo, oUsuario.cContrasenia);
        $('#loginForm').valid();
    }
}