$(document).ready(function () {
    CargarBotonesCantidad();
    CargarBotonProceder();
});

/**
 * Función para iniciar los botones para actualizar la cantidad
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

        this.parentNode.querySelector('.reduced').disabled = false;

        let iTotalProductos = +$('#total-products').text() + 1;
        $('#total-products').text(iTotalProductos);

        let dSubTotal = +$("#price").text() * iTotalProductos;
        $('#subtotal-price').text(dSubTotal + '.00');
        let dTotal = dSubTotal + 50;
        $('#total-price').text(dTotal + '.00');

    });

    $('.reduced').click(function (e) {
        e.preventDefault();

        this.parentNode.querySelector('.qty').stepDown();

        let cantidad = $(this).siblings('.qty').val();
        let minCantidad = $(this).siblings('.qty').attr('min');
        if (cantidad === minCantidad) {
            this.disabled = true;
        }

        this.parentNode.querySelector('.increase').disabled = false;

        let iTotalProductos = +$('#total-products').text() - 1;
        $('#total-products').text(iTotalProductos);

        let dSubTotal = +$("#price").text() * iTotalProductos;
        $('#subtotal-price').text(dSubTotal + '.00');
        let dTotal = dSubTotal + 50;
        $('#total-price').text(dTotal + '.00');

    });
}

/**
 * Función para iniciar el botón para proceder a comprar*/
function CargarBotonProceder() {
    $("#btnProceder").click(function (e) {
        e.preventDefault();
        window.location.href = "/Changarro/Compra?iCantidad=" + $('#total-products').text();
    });
}