$(document).ready(function () {
    ObtenerVistas("Perfil/MisDatos");
});

function ObtenerVistas(cUrl) {
    $.ajax({
        type: "POST",
        url: ruta + cUrl,
        dataType: "html",
        success: function (response) {
            MostrarVistas("v-pills-datos",response)
        }
    });
}

function MostrarVistas(cElementoID, cVista) {
    $("#"+cElementoID).html(cVista);    
}

//btnEditarDatos