$(document).ready(function () {
    BarraHerramientas();
});

function BarraHerramientas() {
    $("#btnCerrarSesion").click(function (e) {

        e.preventDefault();

        $.post(ruta + "Inicio/CerrarSesion");
        location.reload(true);
    });
}
