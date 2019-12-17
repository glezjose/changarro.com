$(document).ready(function () {
    Continuar();
    CargarBotonesInicio();
});

/**
 * En este método se valida si el cvv cumple con lo requerido.*/
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

/**
 * En este método se lleva a la parte del dom, donde se encuentra el CVV vacio.
 * */
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

/**
 * En este método se lleva a cabo toda la funcionalidad de la compra donde se realiza la compra
 * */
function Continuar() {
    var counter = 1;
    $("#btnPago").click(function (event) {
        counter++;
        if (counter == 2) {
            event.preventDefault();


            $("#collapseTwo").slideDown();

            $("#btnPago").text("Revisar compra");

            $('html, body').animate({
                scrollTop: $("#salto-1").offset().top
            }, 1000);

            HabilitarCVV();

            AgregarNuevaTarjeta();

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

/**
 * En este método se realiza el habilitar los términos y condiciones.
 * */
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

/**
 * En este método se habilita el CVV
 * */
function HabilitarCVV() {

    $(".radio-card").click(function (e) {
        $(".radio-card").removeAttr("checked");
        this.checked = true;
        $(this).attr("checked", true);

        $(".cvv").attr("hidden", true);
        $(".cvv").val("");
        $(this).parent().siblings(".cvv-input").find(".cvv").removeAttr("hidden");
        $(this).parent().siblings(".cvv-input").find(".formCVV").attr("visible", true);
    });
}

/**
 * En este método se cargan los botones de inicio
 * */
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

/**
 * En este método se hace una petición ajax para realizar la compra.
 * */
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

            console.log(data.cMensaje);
            setTimeout(function () {
                window.location.href = "/Changarro";
            }, 2000);
        },
        error: function () {
            alert("error");
        }
    });
}

/**
 * En este método se obtienen las ID de la dirección y tarjeta para realizar compra.
 * */
function ObtenerCompra() {
    let _oCompra = {
        iIdDireccion: $(".radio-address:checked").siblings("#iIdDomicilio").val(),
        iIdTarjeta: $(".radio-card:checked").siblings("#iIdTarjeta").val()
    }
    return _oCompra;
}

/**
 * En este método se valida si se habilita para agregar nuevo domicilio.
 * */
function AgregarNuevoDomicilio() {
    if ($(".row-domicilio").length != 4) {
        IniciarBotonNuevoDomicilio();
    } else {
        $("#btnAgregarDomicilio").hide();
    }
}

/**
 * En este método se inicializa el botón de Agregar Domicilio.
 * */
function IniciarBotonNuevoDomicilio() {
    $("#btnAgregarDomicilio").click(function (e) {
        AbrirModal("Perfil/RegistroDireccion", GuardarDomicilio);
    });
}

function GuardarDomicilio() {
    $("#btnGuardarCambiosDireccion").click(function (e) {
        e.preventDefault();

        ValidarFormularioDomicilio();

        if ($('#formDomicilio').valid() === true) {

            GuardarCambios("Compra/AgregarDomicilio", { oDomicilio: JSON.stringify(ObtenerDatosDomicilio()) }, AgregarFilaDomicilio);
        }
    });
}

function AgregarNuevaTarjeta() {
    if ($(".row-tarjeta").length != 4) {
        IniciarBotonNuevaTarjeta();
    } else {
        $("#btnAgregarTarjeta").hide();
    }
}

function IniciarBotonNuevaTarjeta() {
    $("#btnAgregarTarjeta").click(function (e) {
        AbrirModal("Perfil/RegistroTarjeta", GuardarTarjeta);
    });
}

function GuardarTarjeta() {
    $("#cNumeroTarjeta").keypress(function (e) {
        var iTecla = e.keyCode;

        if (!RangoCodigosTeclas(iTecla, 48, 57)) {
            e.preventDefault();
        }
    });

    $("#btnGuardarTarjeta").click(function (e) {

        e.preventDefault();

        ValidarFormularioTarjeta();

        if ($('#formTarjeta').valid() === true) {

            GuardarCambios("Compra/AgregarTarjeta", { oTarjeta: JSON.stringify(ObtenerDatosTarjeta()) }, AgregarFilaTarjeta);
        }

    });
}

/**
 * Método genérico de petición de ajax para guardar los registros a la BD.
 * @param {any} cUrl
 * @param {any} data
 * @param {any} MiFuncion
 */
function GuardarCambios(cUrl, data, MiFuncion) {
    $.ajax({
        type: "POST",
        url: ruta + cUrl,
        data: data,
        dataType: "json",
        success: function (response) {

            MiFuncion(response);
        }
    });
}

/**
 * Método genérico de petición de ajax para agregar la fila de dirección o tarjeta.
 * @param {any} cUrl La url para hacer petición.
 * @param {any} Selector El selector jquery donde se pintara la vista.
 * @param {any} cMensaje El mensaje de alerta.
 * @param {any} MiFuncion La función a ejecutar.
 */
function AgregarFila(cUrl, Selector, cMensaje, MiFuncion) {
    $.ajax({
        type: "POST",
        url: ruta + cUrl,
        async: false,
        dataType: "html",
        success: function (response) {
            Toast.fire({
                icon: "success",
                title: cMensaje
            });

            $('#modalGeneral').modal('hide');

            Selector.append(response);

            MiFuncion();
        },
        error: function (response) {
            alert("Error en fila nueva.");
        }
    });
}

/**
 * Método que se utiliza para agregar la fila de domicilio.
 * @param {any} oDatos El objeto de datos.
 */
function AgregarFilaDomicilio(oDatos) {
    let iIdDomicilio = oDatos.iIdDomicilio;
    let cUrlVistas = "Compra/FilaDomicilio?iIdDomicilio=" + iIdDomicilio;

    AgregarFila(cUrlVistas, $("#body-domicilio"), "Se ha agregado un nuevo domicilio.", AgregarNuevoDomicilio);
}

/**
 * Método que se utiliza para agregar la fila de tarjeta.
 * @param {any} oDatos El objeto de datos.
 */
function AgregarFilaTarjeta(oDatos) {
    let iIdTarjeta = oDatos.iIdTarjeta;
    let cUrlVistas = "Compra/FilaTarjeta?iIdTarjeta=" + iIdTarjeta;

    $(".cvv").attr("hidden", true);
    $(".cvv").val("");

    AgregarFila(cUrlVistas, $("#body-tarjeta"), "Se ha agregado una nueva tarjeta.", AgregarNuevaTarjeta);

    HabilitarCVV();
}