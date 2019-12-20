$(document).ready(function () {
    ValidarCantidades();
    CargarBotonesEliminar();
    CargarBotonesCantidad();
});

/**
 * Función para inicializar el botón de cantidad
 * */
function CargarBotonesCantidad() {
    $('.increase').click(function (e) {
        e.preventDefault();

        this.parentNode.querySelector('.qty').stepUp();

        let cantidad = $(this).siblings('.qty').val();
        let maxCantidad = $(this).siblings('.qty').attr('max');
        if (cantidad === maxCantidad) {
            this.disabled = true;
        }

        let iIdProducto = $(this).siblings('#iIdProducto').val();
        let cUrl = "/Carrito/AumentarCantidad?iIdProducto=" + iIdProducto;
        ActualizarCarrito(cUrl, AgregarCantidad);


        this.parentNode.querySelector('.reduced').disabled = false;
    });

    $('.reduced').click(function (e) {
        e.preventDefault();

        this.parentNode.querySelector('.qty').stepDown();

        let cantidad = $(this).siblings('.qty').val();
        let minCantidad = $(this).siblings('.qty').attr('min');
        if (cantidad === minCantidad) {
            this.disabled = true;
        }

        let iIdProducto = $(this).siblings('#iIdProducto').val();
        let cUrl = "/Carrito/DisminuirCantidad?iIdProducto=" + iIdProducto;
        ActualizarCarrito(cUrl, DisminuirCantidad);

        this.parentNode.querySelector('.increase').disabled = false;
    });
}

/**
 * Función para validar la cantidad de productos existentes
 * */
function ValidarCantidades() {
    $('.increase').each(function () {
        let cantidad = $(this).siblings('.qty').val();
        let maxCantidad = $(this).siblings('.qty').attr('max');
        if (cantidad === maxCantidad || cantidad > maxCantidad) {
            this.disabled = true;
        }
    });

    $('.reduced').each(function () {
        let cantidad = $(this).siblings('.qty').val();
        let minCantidad = $(this).siblings('.qty').attr('min');
        if (cantidad === minCantidad) {
            this.disabled = true;
        }
    });
}

/**
 * Función para actualizar el carrito de compras
 * @param {any} cUrl Url del método en el controlador
 * @param {any} funcion Función a ejecutar
 */
function ActualizarCarrito(cUrl, funcion) {
    $.ajax({
        type: "POST",
        async: false,
        url: ruta + cUrl,
        dataType: "json",
        success: function (data) {
            if (funcion != null) {
                funcion(data)
            }
            ActualizarPrecio(data.dSubTotalPrecio, data.dTotalPrecio)
        }
    });
}

/**
 * Función para aumentar la cantidad de productos
 * */
function AgregarCantidad() {
    let iTotalProductos = +$('#total-products').text() + 1;
    $('#total-products').text(iTotalProductos);
    $('#cart-products').text(iTotalProductos);
}

/**
 * Función para restar la cantidad de productos
 * */
function DisminuirCantidad() {
    let iTotalProductos = +$('#total-products').text() - 1;
    $('#total-products').text(iTotalProductos);
    $('#cart-products').text(iTotalProductos);
}

/**
 * Función para actualizar el precio 
 * @param {any} dSubTotal Cantidad subtotal
 * @param {any} dTotal Cantidad total
 */
function ActualizarPrecio(dSubTotal, dTotal) {
    $('#subtotal-price').text(dSubTotal + '.00');
    $('#total-price').text(dTotal + '.00');
}

/**
 * Función para verificar que el carrito este vacío
 * @param {any} oCarrito Objeto con datos del producto
 */
function ValidarCarritoVacio(oCarrito) {
    CambiarTotalProductos(oCarrito.iTotalProductos);
    if ($('.product-card').length == 0) {
        $.ajax({
            type: "POST",
            async: false,
            url: ruta + "/Carrito/CarritoVacio",
            dataType: "HTML",
            success: function (data) {
                $('.cart_inner').html(data);
            }
        });
    }
}

function CambiarTotalProductos(iTotalProductos) {
    $('#cart-products').text(iTotalProductos);
}

/**
 * Inicializa el botón de eliminar producto del carrito*/
function CargarBotonesEliminar() {
    $('.close-btn').click(function (e) {
        e.preventDefault();


        let iIdProducto = $(this).siblings('#iIdProducto').val();
        let cUrl = "/Carrito/EliminarProducto?iIdProducto=" + iIdProducto;

        $(this).closest('.product-card').slideUp(500, function () {
            $(this).remove();
            ActualizarCarrito(cUrl, ValidarCarritoVacio);
        });

    });
}