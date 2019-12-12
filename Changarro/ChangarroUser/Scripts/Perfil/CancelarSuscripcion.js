$(document).ready(function () {
    Boton();
});

/**
 * Método para inicializar el botón para cancelar la suscripción
 * */
function Boton() {

    $("#btnCancelarCuenta").click(function (e) {

        e.preventDefault();

        AbrirModal("Perfil/CancelarSuscripcion", Desactivar)
    });
}

/**
 * Método para Obtener la contraseña y confirmar la solicitud
 * */
function Desactivar() {

    $("#cInputCorreoPerfil").val($("#cCorreoPerfil").text());

    $("#btnCancelarSuscripcion").click(function (e) {

        const cContrasenia = $("#cContraseniaCancelar").val();

        DesactivarCuenta(cContrasenia)
    });
}

/**
 * Método para desactivar la cuenta
 * @param {any} cContrasenia Contiene la contraseña del usuario
 */
function DesactivarCuenta(cContrasenia) {    

    $.ajax({
        type: "POST",
        url: ruta + "Perfil/DesactivarCuenta",
        data: { _cContrasenia: cContrasenia },
        dataType: "json",
        success: function (response) {
            if (response._cMensaje === null) {
                $.post(ruta + "Inicio/CerrarSesion");
                location.reload(true);
            }
            else {
                MensajeError(response._lStatus, response._cMensaje)
            }
        }
    });    
}

/**
 * Método para desplegar mensajes de error
 * @param {any} lStatus Contiene el estatus de la operación de desactivación de la cuenta
 * @param {any} cMensaje Contiene el mensaje de error
 */
function MensajeError(lStatus, cMensaje) {
    if (lStatus === true) {

        Toast.fire({
            icon: 'warning',
            title: cMensaje
        })

    } else {

        swalWithBootstrapButtons.fire({
            title: ':(',
            text: cMensaje,
            icon: 'error',
            confirmButtonText: 'Aceptar',
        })
    }

}

const swalWithBootstrapButtons = Swal.mixin({
    customClass: {
        confirmButton: 'genric-btn primary radius',

    },
    buttonsStyling: false
})