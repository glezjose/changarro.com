function MostrarModal(cTipo, cUrl, Data, Funcion) {
    $.ajax({
        type: cTipo,
        url: cUrl,
        async: false,
        data: Data,
        dataType: "HTML",
        success: function (response) {
            //alert(response);
            $("#modalGeneral").html(response);
            $("#modalGeneral").modal({
                show: true,
                backdrop: "static",
            });

            if (Funcion) {
                Funcion();
            }
        }
    });
}
function LlamarMetodo(cTipo, cUrl, Data, Funcion) {
    $.ajax({
        type: cTipo,
        url: cUrl,
        data: Data,
        async: false,
        dataType: "json",
        success: function (response) {
            Swal.fire({
                icon: "success",
                title: response.Estatus,
                text: response.Mensaje,
                showConfirmButton: false,
                timer: 1500
            });
            $("#ModalPrincipal").modal('hide');

            if (Funcion) {
                window[Funcion]();
            }
        }
    });
}
function MsjseleccioneRegistro() {
    Swal.fire({
        icon: 'warning',
        title: '¡Advertencia!',
        text: 'Debe seleccionar un registro antes.'
    });
}

