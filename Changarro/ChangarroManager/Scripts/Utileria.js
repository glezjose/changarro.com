$(document).ready(function () {
    cerrarSesion();
});

/**Método para cerrar sesión del administrador. */
function cerrarSesion() {
    $("#btnCerrarSesion").click(function (e) {
        e.preventDefault();

        $.post(ruta + "Inicio/CerrarSesion");
        location.reload(true);
    });
}
