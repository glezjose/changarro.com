$(document).ready(function () {
    Boton();
});

function Boton() {

    $("#btnCancelarCuenta").click(function (e) {

        e.preventDefault();

        AbrirModal("Perfil/CancelarSuscripcion", Desactivar)
    });
}

function Desactivar() {

    $("#btnCancelarSuscripcion").click(function (e) {

        const cContrasenia = $("#cContraseniaCancelar").val();

        DesactivarCuenta(cContrasenia)
    });
}

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

const swalWithBootstrapButtons = Swal.mixin({
    customClass: {
        confirmButton: 'genric-btn primary radius',
 
    },
    buttonsStyling: false
})

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