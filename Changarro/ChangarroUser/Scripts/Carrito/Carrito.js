$(document).ready(function () {
    CargarBotonesCantidad();
});

function CargarBotonesCantidad() {
    $('.increase').click(function (e) {
        e.preventDefault();

        this.parentNode.querySelector('.qty').stepUp()

    });

    $('.reduced').click(function (e) {
        e.preventDefault();

        this.parentNode.querySelector('.qty').stepDown()

    });
}