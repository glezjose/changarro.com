/**
 * Abre el modal y pega la vista que se menciona.
 * @param {any} cUrl Es la url de la vista que se pondra en el modal.
 */
function AbrirModal(cUrl) {
    $.ajax({
        type: 'POST',
        url: ruta + cUrl,
        dataType: 'html',
        async: false,
        success: function (response) {
            $('#modalGeneral').html(response);
            $('#modalGeneral').modal({
                show: true,
                backdrop: "static"
            });
        },
        error: function () {
            alert('falle');
        }
    });
}

/**
 * Carga la funcionalidad de los botones con la clase ".btnVerDetalles".
 * */
function CargarBotonesProducto() {
    $('.btnVerDetalles').click(function (e) {
        e.preventDefault();
        const iIdProducto = $(this).siblings('#iIdProducto').val();
        const cUrl = '/Producto/VerDetalles?iIdProducto=' + iIdProducto;
        console.log(cUrl);
        AbrirModal(cUrl);
    });
}
