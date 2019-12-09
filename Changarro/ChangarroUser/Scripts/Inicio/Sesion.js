$(document).ready(function () {    
    BarraHerramientas();
    Login();
    Registro();
});

/**
 * Inicializa los botones para mostrar un modal con un formulario
 * */
function BarraHerramientas() {
    $("#btnIniciarSesion").click(function (e) {

        e.preventDefault();

        AbrirModal("Inicio/LoginParcial", Login)
    })

    $("#btnRegistrar").click(function (e) {

        e.preventDefault();

        AbrirModal("Inicio/RegistroParcial", Registro)
    })

    $("#btnCerrarSesion").click(function (e) {

        e.preventDefault();

        AbrirModal("Inicio/LogOut", CerrarSesion);
    })
}

/**
 * Método para iniciar sesión
 * */
function Login() {
    $("#registro-tab").click(function (e) {

        e.preventDefault();

        AbrirModal("Inicio/RegistroParcial", Registro)
    });

    ValidarLogin();

    $("#btnLogin").click(function (e) {

        e.preventDefault();

        if ($('#loginForm').valid() === true) {

            const oLogin = {
                cCorreo: $("#cCorreo").val().toLowerCase(),
                cContrasenia: $("#cContrasenia").val()
            }

            ManejarSesionCliente("POST", "Inicio/IniciarSesion", { oCliente: JSON.stringify(oLogin) }, ValidarInicioSesion);
        }        
    });
}

/**Método para registrar nuevo usuario
 */
function Registro() {
    $("#login-tab").click(function (e) {

        e.preventDefault();

        AbrirModal("Inicio/LoginParcial", Login)
    });

    ValidarRegistro();

    $("#btnRegistro").click(function (e) {

        e.preventDefault();

        if ($('#registroForm').valid() === true) {

            const oNuevoCliente = {
                cNombre: $.trim($("#cNombre").val()),
                cApellido: $.trim($("#cApellido").val()),
                cCorreo: $.trim($("#cCorreo").val().toLowerCase()),
                cContrasenia: $("#cContrasenia").val()
            }

            ManejarSesionCliente("POST", "Inicio/RegistrarCliente", { oCliente: JSON.stringify(oNuevoCliente) }, ValidarRegistroCliente);
        }
    });
}

/**
 * Método para serrar cesión
 */
function CerrarSesion() {
    $("#btnLogOut").click(function (e) {

        e.preventDefault();

        $.post(ruta + "Inicio/CerrarSesion");
        location.reload(true);
    });
}

/**
 * Método para llamar funciones de sesión desde el controlador por medio de ajax
 * @param {any} cTipo Tipo de acceso del controlador
 * @param {any} cUrl Url del método del controlador
 * @param {any} data Datos a mandar al método del controlador
 * @param {any} funcion Función a llamar luego de la respuesta del servidor
 */
function ManejarSesionCliente(cTipo, cUrl, data, funcion) {
    $.ajax({
        type: cTipo,
        url: ruta + cUrl,
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

/**
 * Valida el correcto registro del cliente
 * @param {any} oUsuario Objeto del usuario con los campos inválidos
 */
function ValidarRegistroCliente(oUsuario) {
    if (oUsuario === null) {
        window.location.href = '/changarro';
    } else {
        console.log(oUsuario);        
        ValidarRegistro(oUsuario.cNombre, oUsuario.cApellido, oUsuario.cCorreo);
        $('#registroForm').valid();
    }
}

/**
 * Valida el correcto inicio de sesión del cliente
 * @param {any} oUsuario Objeto del usuario con los campos inválidos
 */
function ValidarInicioSesion(oUsuario) {
    if (oUsuario === null) {
        window.location.href = '/changarro';
    } else {
        ValidarLogin(oUsuario.cCorreo, oUsuario.cContrasenia);
        $('#loginForm').valid();
    }
}
