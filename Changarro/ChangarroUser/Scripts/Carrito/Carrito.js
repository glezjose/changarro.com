$(document).ready(function () {
    ValidarCantidades();
    CargarBotonesEliminar();
    CargarBotonesCantidad();
});

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

function ActualizarCarrito(cUrl, funcion) {
    $.ajax({
        type: "POST",
        async: false,
        url: ruta + cUrl,
        dataType: "json",
        success: function (data) {
            if (funcion != null) {
                funcion()
            }
            ActualizarPrecio(data.dSubTotalPrecio, data.dTotalPrecio)
        }
    });
}

function AgregarCantidad() {
    let iTotalProductos = +$('#total-products').text() + 1;
    $('#total-products').text(iTotalProductos);
    $('#cart-products').text(iTotalProductos);
}

function DisminuirCantidad() {
    let iTotalProductos = +$('#total-products').text() - 1;
    $('#total-products').text(iTotalProductos);
    $('#cart-products').text(iTotalProductos);
}

function ActualizarPrecio(dSubTotal, dTotal) {
    $('#subtotal-price').text(dSubTotal + '.00');
    $('#total-price').text(dTotal + '.00');
}

function ValidarCarritoVacio() {
    let longi = $('.card').length;
    if ($('.card').length == 0) {
        $.ajax({
            type: "GET",
            async: false,
            url: ruta + "/Carrito/CarritoVacio",
            dataType: "HTML",
            success: function (data) {
                $('.cart_inner').html(data);
            }
        });
        alert("hola");
    }
}

function CargarBotonesEliminar() {
    $('.close-btn').click(function (e) {
        e.preventDefault();


        let iIdProducto = $(this).siblings('#iIdProducto').val();
        let cUrl = "/Carrito/EliminarProducto?iIdProducto=" + iIdProducto;

        $(this).closest('.card').slideUp(500, function () {
            $(this).remove();
            ActualizarCarrito(cUrl, ValidarCarritoVacio);
        });

    });
}