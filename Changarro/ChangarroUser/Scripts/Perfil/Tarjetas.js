/**
 * Método para obtener el id del domicilio para abrir el modal con su formulario de datos
 * */
function AbrirModalTarjetas() {

    IniciarBotonNuevaTarjeta();

    $(".btnEliminarTarjeta").on("click", function () {

        const iIdTarjeta = $(this).closest("div.opcionTarjeta").find("input[name='iIdTarjeta']").val();

        AbrirModal("Perfil/DesactivacionTarjeta?iIdTarjeta=" + iIdTarjeta, IniciarBotonEliminarTarjeta)
    });

}

/**
 * Método que inicializa el botón para agregar una nueva tarjeta
 * */
function IniciarBotonNuevaTarjeta() {

    $("#btnAgregarTarjeta").click(function (e) {

        e.preventDefault();

        AbrirModal("Perfil/RegistroTarjeta", IniciarBotonGuardarTarjeta)

    });
}

/**
 * Método que inicializa el botón para eliminar tarjeta
 * */
function IniciarBotonEliminarTarjeta() {

    $("#btnEliminarTarjeta").click(function (e) {

        e.preventDefault();

        const iIdTarjeta = $("#iIdTarjetaEliminar").val();

        GuardarCambios("Perfil/DesactivarTarjeta?iIdTarjeta=" + iIdTarjeta, null, null, LlamarVistaTarjetaEliminar)

    });
}

/**
 * Método para iniciar el botón para guardar cambios
 * */
function IniciarBotonGuardarTarjeta() {

    $("#cNumeroTarjeta").keypress(function (e) {
        var iTecla = e.keyCode;

        if (!RangoCodigosTeclas(iTecla, 48, 57)) {
            e.preventDefault();
        }
    });

    $("#btnGuardarTarjeta").click(function (e) {

        e.preventDefault();

        GuardarDatosTarjeta();

    });
}

/**
 * Método para validar y guardar los datos del formulario
 * */
function GuardarDatosTarjeta() {

    ValidarFormularioTarjeta();

    if ($('#formTarjeta').valid() === true) {

        GuardarCambios("Perfil/AgregarTarjeta", { oTarjeta: JSON.stringify(ObtenerDatosTarjeta()) }, "Perfil/MisTarjetas", VerificarCambiosTargeta);
    }
}

/**
 * Método para obtener lo datos del formulario
 * */
function ObtenerDatosTarjeta() {
    const oTarjeta = {

        cNombre: $.trim($("#cNombreTarjeta").val()),
        cTitular: $.trim($("#cTitular").val()),
        cNumeroTarjeta: $.trim($("#cNumeroTarjeta").val()),
        iMesExpiracion: $.trim($("#iMesExpiracion").val()),
        iAnioExpiracion: $.trim($("#iAnioExpiracion").val())
    }
    return oTarjeta;
}

/**
 * Método para validar el formulario de domicilio
 * */
function ValidarFormularioTarjeta(cNumeroTarjeta) {
    jQuery.validator.addMethod("tarjetaRepetida", function (value, element) {
        if (value == cNumeroTarjeta) {
            return false;
        } else {
            return true;
        };
    }, "Este número de tarjeta ya ha sido registrado");

    $('#formTarjeta').validate({
        rules: {
            cNombreTarjeta: {
                required: true,
                minlength: 2,
                maxlength: 50
            },
            cTitular: {
                required: true,
                minlength: 2,
                maxlength: 50
            },
            iAnioExpiracion: {
                required: true
            },
            iMesExpiracion: {
                required: true
            },
            cNumeroTarjeta: {
                tarjetaRepetida: true,
                required: true,
                number: true,
                maxlength: 16,
                minlength: 15,
                min: 0
            }
        },
        messages: {
            cNombreTarjeta: {
                required: "Por favor ingrese un nombre",
                minlength: "El nombre debe contener mínimo 2 caracteres",
                maxlength: "El máximo permitido es de 50 caracteres"
            },
            cTitular: {
                required: "Por favor ingrese el nombre del titular",
                minlength: "La calle debe contener mínimo 2 caracteres",
                maxlength: "La calle debe contener máximo de 50 caracteres"
            },
            cNumeroTarjeta: {
                required: "Por favor ingrese el número de tarjeta",
                number: "Por favor ingrese un número de válido",
                min: "Por favor ingrese un número de válido",
                minlength: "El numero de tarjeta debe contener mínimo 15 caracteres",
                maxlength: "El numero de tarjeta debe contener mínimo 16 caracteres"
            },
            iAnioExpiracion: {
                required: "Por favor seleccione el año"
            },
            iMesExpiracion: {
                required: "Por favor seleccione el mes"
            },
        },
        errorElement: "em",
        errorPlacement: function (error, element) {
            // Agrega la clase 'invalid-feedback' al elemento de error

            error.addClass("invalid-feedback");

            if (element.prop("type") === "checkbox") {
                error.insertAfter(element.next("label"));
            } else {
                error.insertAfter(element);
            }
        },
        highlight: function (element, errorClass, validClass) {
            $(element).addClass("is-invalid").removeClass("is-valid");
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).addClass("is-valid").removeClass("is-invalid");
        }
    });
}

/**
 * Método para comprobar el resultado del guardado de los cambios
 * @param {any} oDatos Objeto con los datos de la tarjeta
 * @param {any} cUrlVistas Cadena con la Url de la vista parcial
 */
function VerificarCambiosTargeta(oDatos, cUrlVistas) {

    if (oDatos === null) {

        ObtenerVistas(cUrlVistas, "v-pills-tarjetas", AbrirModalTarjetas);

        Toast.fire({
            icon: 'success',
            title: '¡Tarjeta agregada con éxito!'
        })

        $('#modalGeneral').modal('hide')
    } else {

        ValidarFormularioTarjeta(oDatos);
        $('#formTarjeta').valid();
    }
}

/**
 * Método para llamar la vista parcial de tarjetas con los nuevos datos después de eliminar alguna tarjeta
 * */
function LlamarVistaTarjetaEliminar() {

    Toast.fire({
        icon: 'success',
        title: '¡Tarjeta eliminada con éxito!'
    })

    $('#modalGeneral').modal('hide');

    ObtenerVistas("Perfil/MisTarjetas", "v-pills-tarjetas", AbrirModalTarjetas);
}

