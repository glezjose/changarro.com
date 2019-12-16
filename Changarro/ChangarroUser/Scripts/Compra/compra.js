$(document).ready(function () {
    Continuar();
    CargarBotonesInicio();
});

function ValidarCVV() {
    $(".radio-card:checked").parent().siblings(".cvv-input").find(".formCVV").validate({
        rules: {
            cCVV: {
                required: true,
                digits: true,
                minlength: 3,
                maxlength: 3
            }
        },
        messages: {
            cCVV: {
                required: "",
                digits: "",
                minlength: "",
                maxlength: ""
            }

        }
    });
}

function LlevarAcvv() {
    $('html, body').animate({
        scrollTop: $("#salto-1").offset().top
    }, 1000);

    Toast.fire({
        icon: "warning",
        title: "Por favor ingrese el CVV de la tarjeta correctamente."
    });

    $(".radio-card:checked").parent().siblings(".cvv-input").find(".cvv").focus();
}
function Continuar() {
    var counter = 1;
    $("#btnPago").click(function (event) {
        counter++;
        if (counter == 2) {
            event.preventDefault();

            AgregarNuevaTarjeta();

            $("#collapseTwo").slideDown();

            $("#btnPago").text("Revisar compra");

            $('html, body').animate({
                scrollTop: $("#salto-1").offset().top
            }, 1000);

            HabilitarCVV();

        } else if (counter == 3) {
            event.preventDefault();

            $("#collapseThree").slideDown();

            $("#btnPago").text("Realizar pago");

            $('html, body').animate({
                scrollTop: $("#salto-2").offset().top
            }, 1000);

        } else if (counter >= 4) {
            ValidarCVV();
            const lValidar = $(".radio-card:checked").parent().siblings(".cvv-input").find(".formCVV").valid();
            if (lValidar) {
                if ($("#checkTerminos").attr("checked")) {
                    RealizarCompra();
                } else {
                    Toast.fire({
                        icon: "warning",
                        title: "Por favor lea los terminos y condiciones."
                    });
                }
            } else {
                LlevarAcvv();
            }
        }
    });
}

function HabilitarTerminosyCondiciones() {
    $("#checkTerminos").click(function (e) {
        if ($(this).attr("checked")) {
            $(this).removeAttr("checked");
        } else {
            this.checked = true;
            $(this).attr("checked", true);
        }
    });
}

function HabilitarCVV() {

    $(".radio-card").click(function (e) {
        $(".radio-card").removeAttr("checked");
        this.checked = true;
        $(this).attr("checked", true);

        $(".cvv").attr("hidden", true);
        $(this).parent().siblings(".cvv-input").find(".cvv").removeAttr("hidden");
        $(this).parent().siblings(".cvv-input").find(".formCVV").attr("visible", true);
    });
}

function CargarBotonesInicio() {
    $(".radio-address").click(function (e) {
        $(".radio-address").removeAttr("checked");
        this.checked = true;
        $(this).attr("checked", true);
    });

    $("#terms").click(function (e) {
        e.preventDefault();

        let cUrl = "/Compra/TerminosYcondiciones";

        AbrirModal(cUrl, null);
    });

    HabilitarTerminosyCondiciones();

    AgregarNuevoDomicilio();
}

function RealizarCompra() {
    let oCompra = ObtenerCompra();
    $.ajax({
        type: "POST",
        url: ruta + "/Compra/RealizarCompra",
        data: { "oCompra": JSON.stringify(oCompra) },
        dataType: "json",
        success: function (data) {
            Toast.fire({
                icon: data.cIcono,
                title: data.cMensaje
            });

            setTimeout(function () {
                window.location.href = "/Changarro";
            }, 2000);
        },
        error: function () {
            alert("error");
        }
    });
}

function ObtenerCompra() {
    let _oCompra = {
        iIdDireccion: $(".radio-address:checked").siblings("#iIdDomicilio").val(),
        iIdTarjeta: $(".radio-card:checked").siblings("#iIdTarjeta").val()
    }
    return _oCompra;
}

function AgregarNuevoDomicilio() {
    if (!$(".row-domicilio").length == 4) {
        IniciarBotonNuevoDomicilio();
    } else {
        $("#btnAgregarDomicilio").hide();
    }
}

function IniciarBotonNuevoDomicilio() {
    $("#btnAgregarDomicilio").click(function (e) {
        AbrirModal("Perfil/RegistroDireccion", null);
    });
}

function AgregarNuevaTarjeta() {
    if (!$(".row-tarjeta").length == 4) {
        IniciarBotonNuevaTarjeta();
    } else {
        $("#btnAgregarTarjeta").hide();
    }
}

function IniciarBotonNuevaTarjeta() {
    $("#btnAgregarTarjeta").click(function (e) {
        AbrirModal("Perfil/RegistroTarjeta", null);
    });
}