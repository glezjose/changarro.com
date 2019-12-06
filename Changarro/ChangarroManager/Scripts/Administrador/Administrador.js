//*El documento carga las funciones*/
$(document).ready(function () {
    ObtenerDatosAdministrador();
});

function ObtenerDatos() {
    $.ajax({
        type: 'POST',
        url: '/ChangarroManager/Perfil/ObtenerDatosAdministrador',
        dataType: 'json',
        async: false,
        success: function (data) {
        }
    });
}