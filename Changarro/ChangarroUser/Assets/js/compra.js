$(document).ready(function () {
    cambiarFlecha();
    Continuar();
});

function cambiarFlecha() {
    var flecha1 = $("#flecha1");
    flecha1.click(function () {
        if (flecha1.hasClass("ti-angle-down")) {
            flecha1.removeClass("ti-angle-down");
            flecha1.addClass("ti-angle-up");
        }
        else if (flecha1.hasClass("ti-angle-up")) {
            flecha1.removeClass("ti-angle-up");
            flecha1.addClass("ti-angle-down");
        }
    });
    var flecha2 = $("#flecha2");
    flecha2.click(function () {
        if (flecha2.hasClass("ti-angle-down")) {
            flecha2.removeClass("ti-angle-down");
            flecha2.addClass("ti-angle-up");
        }
        else if (flecha2.hasClass("ti-angle-up")) {
            flecha2.removeClass("ti-angle-up");
            flecha2.addClass("ti-angle-down");
        }
    });
    var flecha3 = $("#flecha3");
    flecha3.click(function () {
        if (flecha3.hasClass("ti-angle-down")) {
            flecha3.removeClass("ti-angle-down");
            flecha3.addClass("ti-angle-up");
        }
        else if (flecha3.hasClass("ti-angle-up")) {
            flecha3.removeClass("ti-angle-up");
            flecha3.addClass("ti-angle-down");
        }
    });
}

function Continuar() {
    var counter = 1;
    $("#btnPago").click(function (event) {
        counter++;
        if (counter == 2) {
            if (this.hash !== "") {
                // Prevent default anchor click behavior
                event.preventDefault();

                // Store hash
                var hash = this.hash;

                $("#collapseTwo").slideDown();

                // Using jQuery's animate() method to add smooth page scroll
                // The optional number (800) specifies the number of milliseconds it takes to scroll to the specified area
                $('html, body').animate({
                    scrollTop: $(hash).offset().top
                }, 800, function () {

                    $("#btnPago").text("Revisar compra");

                    // Add hash (#) to URL when done scrolling (default click behavior)
                    window.location.hash = hash;
                });
            }
            $("#btnPago").attr("href", "#jump2");
        } else if (counter == 3) {
            if (this.hash !== "") {
                // Prevent default anchor click behavior
                event.preventDefault();

                // Store hash
                var hash = this.hash;

                $("#collapseThree").slideDown();

                // Using jQuery's animate() method to add smooth page scroll
                // The optional number (800) specifies the number of milliseconds it takes to scroll to the specified area
                $('html, body').animate({
                    scrollTop: $(hash).offset().top
                }, 800, function () {

                    $("#btnPago").text("Realizar Pago");
                    // Add hash (#) to URL when done scrolling (default click behavior)
                    window.location.hash = hash;
                });
            }
        }
    });
}