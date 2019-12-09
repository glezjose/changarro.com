$(document).ready(function () {
    iniciarBoton();
    validacion();
});

/**
 * Inicializa el botón del envío de inicio de sesión.
 * */
function iniciarBoton() {

    $("#btnAdminLogin").click(function (e) {
        e.preventDefault();

        inicioSesion();
    });
}

/**
 * Método para enviar los datos y comprobar la validación de los datos ingresados.
 * */
function inicioSesion() {

    const validar = $("#Formulario").valid()

    if (validar === true) {

        const oDatos =
        {

            cCorreo: $("#cCorreo").val().toLowerCase(),
            cContrasenia: $("#cContrasenia").val()
        }

        iniciarSesion("POST", "IniciarSesion", { oAdmin: JSON.stringify(oDatos) }, validarInicioSesion);
    }
}

/**
 * Método para llamar funciones de sesión desde el controlador por medio de ajax.
 * @param {any} cTipo GET/POST.
 * @param {any} cUrl Dirección del método a ejecutar.
 * @param {any} data Datos a mandar al método del controlador.
 * @param {any} funcion Función a llamar luego de la respuesta del servidor.
 */
function iniciarSesion(cTipo, cUrl, data, funcion) {

    $.ajax({
        type: cTipo,
        url: cUrl,
        async: false,
        data: data,
        dataType: "json",
        success: function (respuesta) {

            if (respuesta._cMensaje === null) {
                funcion(respuesta._oAdministrador);
            }
            else {
                Toast.fire({
                    icon: 'error',
                    title: respuesta._cMensaje
                })
            }
        }
    });
}

/**
 * Valida el correcto inicio de sesión del administrador.
 * @param {any} oAdmin Objeto con los campos inválidos.
 */
function validarInicioSesion(oAdmin) {
    if (oAdmin === null) {
        window.location.href = '/ChangarroManager';
    } else {
        validacion(oAdmin.cCorreo, oAdmin.cContrasenia);
        $('#Formulario').valid();
    }
}
