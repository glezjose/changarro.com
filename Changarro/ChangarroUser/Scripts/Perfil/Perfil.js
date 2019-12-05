$(document).ready(function () {
    ObtenerVistas("Perfil/MisDatos");
});

function ObtenerVistas(cUrl) {
    $.ajax({
        type: "POST",
        url: ruta + cUrl,
        dataType: "html",
        success: function (response) {
            MostrarVistas(cElementoID,response)
        }
    });
}

function MostrarVistas(cElementoID, cVista) {
    $("#v-pills-datos").html(cVista);    
}