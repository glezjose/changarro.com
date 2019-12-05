$(document).ready(function () {
    CargarBotonesProducto();

    $('#modalGeneral').on('hidden.bs.modal', function () {
        $('#modal-render').remove();
    });
});
