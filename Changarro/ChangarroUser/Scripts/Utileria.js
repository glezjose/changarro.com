/**
 * Abre el modal y pega la vista que se menciona.
 * @param {any} cUrl Es la url de la vista que se pondrá en el modal.
 */
function AbrirModal(cUrl, funcion) {
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

            funcion();
        },
        error: function () {
            alert('falle');
        }
    });
}

function PersisteDatos(cUrl, funcion) {
    $.ajax({
        type: "POST",
        url: ruta + cUrl,
        dataType: "json",
        success: function (response) {
            $('#modalGeneral').modal('hide');

            Toast.fire({
                icon: response.cIcono,
                title: response.cMensaje
            });

            funcion();
        },
        error: function (response) {
            alert('error');
        }
    });
}

const Toast = Swal.mixin({
    toast: true,
    position: 'bottom-right',
    showConfirmButton: false,
    timer: 1500,
    onOpen: (toast) => {
        toast.addEventListener('mouseenter', Swal.stopTimer)
        toast.addEventListener('mouseleave', Swal.resumeTimer)
    }
});

/**
 * Carga la funcionalidad de los botones que se presentan con cada producto.
 * */
function CargarBotonesProducto() {
    $('.btnVerDetalles').click(function (e) {
        e.preventDefault();

        const iIdProducto = $(this).siblings('#iIdProducto').val();
        const cUrl = '/Producto/VerDetalles?iIdProducto=' + iIdProducto;

        AbrirModal(cUrl, CargarBotonesProducto);
    });

    $('.btnAgregarCarrito').click(function (e) {
        e.preventDefault();
        e.stopPropagation();
        e.stopImmediatePropagation();

        const iIdProducto = $(this).siblings('#iIdProducto').val();
        const cUrl = '/Carrito/AgregarProductoCarrito?iIdProducto=' + iIdProducto;

        PersisteDatos(cUrl, AgregarAcarrito);
    });
}

function AgregarAcarrito() {
    let iTotalProductos = +$('#cart-products').text() + 1;
    $('#cart-products').text(iTotalProductos);
}
