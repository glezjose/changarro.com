$(document).ready(function () {
    BarraHerramientas();
    ValidarLogin();
});

/**
 * Inicializa los botones para mostrar un modal con un formulario
 * */
function BarraHerramientas() {
    
    $("#btnAdminLogin").click(function (e) {

        e.preventDefault();

        Login();
    });
}

/**
 * Método para iniciar sesión
 * */
function Login() {

    if ($('#loginForm').valid() === true) {

        const oLogin = {
            cCorreo: $("#cCorreo").val().toLowerCase(),
            cContrasenia: $("#cContrasenia").val()
        }

        IniciarSesion("POST", "IniciarSesion", { oAdmin: JSON.stringify(oLogin) }, ValidarInicioSesion);
    }        
}

/**
 * Método para llamar funciones de sesión desde el controlador por medio de ajax
 * @param {any} cTipo Tipo de acceso del controlador
 * @param {any} cUrl Url del método del controlador
 * @param {any} data Datos a mandar al método del controlador
 * @param {any} funcion Función a llamar luego de la respuesta del servidor
 */
function IniciarSesion(cTipo, cUrl, data, funcion) {

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

/**
 * Valida el correcto inicio de sesión del cliente
 * @param {any} oUsuario Objeto del usuario con los campos inválidos
 */
function ValidarInicioSesion(oUsuario) {
    if (oUsuario === null) {
        window.location.href = '/ChangarroManager';
    } else {
        ValidarLogin(oUsuario.cCorreo, oUsuario.cContrasenia);
        $('#loginForm').valid();
    }
}
