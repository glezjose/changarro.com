/**
 * Método para obtener el id del domicilio
 * */
function AbrirModalDomicilio() {

    IniciarBotonNuevaDireccion();

    $(".btnEditarDireccion").on("click", function () {

        const iIdDireciccion = $(this).closest("div.opcionDireccion").find("input[name='iIdDireccion']").val();

        AbrirModal("Perfil/FormularioDireccion?iIdDireccion=" + iIdDireciccion, IniciarBotonesGuardarDireccion)
    });

    $(".btnEliminarDireccion").on("click", function () {

        const iIdDireciccion = $(this).closest("div.opcionDireccion").find("input[name='iIdDireccion']").val();

        AbrirModal("Perfil/DesactivarDomicilio?iIdDireccion=" + iIdDireciccion, IniciarBotonEliminarDomicilio)
    });

}

/**
 * Método que inicializa el botón para registrar direcciones
 * */
function IniciarBotonNuevaDireccion() {

    $("#btnAgregarDireccion").click(function (e) {

        e.preventDefault();

        AbrirModal("Perfil/RegistroDireccion", IniciarBotonesGuardarDireccion)

    });

}

/**
 * Método que inicializa el botón para eliminar direcciones
 * */
function IniciarBotonEliminarDomicilio() {

    $("#btnEliminarDomicilio").click(function (e) {

        e.preventDefault();

        const iIdDireccion = $("#iIdDireccionEliminar").val();

        GuardarCambios("Perfil/DesactivarDireccion?iIdDireccion=" + iIdDireccion, null, null, LlamarVistaDomiciliosEliminar)

    });
}

/**
 * Método para iniciar el botón para guardar cambios
 * */
function IniciarBotonesGuardarDireccion() {

    $("#btnGuardarCambiosDireccion").click(function (e) {

        e.preventDefault();

        HabilitarFormularioDomicilio();

    });
}

/**
 * Método para habilitar el formulario de edición de datos del usuario
 * */
function HabilitarFormularioDomicilio() {

    if ($("#btnGuardarCambiosDireccion").hasClass("editar")) {

        $("#btnGuardarCambiosDireccion").html("Guardar Cambios")

        $("#campoDomicilioForm").removeAttr("disabled");

        $("#btnGuardarCambiosDireccion").removeClass("editar");

    } else {
        GuardarDatosDomicilio();
    }
}

/**
 * Método para validar y guardar los datos del formulario
 * */
function GuardarDatosDomicilio() {

    ValidarFormularioDomicilio();

    if ($('#formDomicilio').valid() === true) {

        GuardarCambios("Perfil/ActualizarDomicilio", { oDomicilio: JSON.stringify(ObtenerDatosDomicilio()) }, null, LlamarVistaDomicilios);
    }
}

/**
 * Método para obtener lo datos del formulario
 * */
function ObtenerDatosDomicilio() {
    const oDomicilio = {
        iIdDireccion: $.trim($("#iIdDireccionInput").val()),
        cNombre: $.trim($("#cNombreDireccion").val()),
        cCalle: $.trim($("#cCalle").val()),
        cNumeroExterior: $.trim($("#cNumeroExterior").val()),
        cNumeroInterior: $.trim($("#cNumeroInterior").val()),
        cColonia: $.trim($("#cColonia").val()),
        iCodigoPostal: $.trim($("#iCodigoPostal").val()),
        cMunicipio: $.trim($("#cMunicipio").val()),
        iIdEstado: $.trim($("#iIdEstado").val()),
        cDescripcion: $.trim($("#cDescripcion").val()),
    }

    return oDomicilio;
}

/**
 * Método para validar el formulario de domicilio
 * */
function ValidarFormularioDomicilio() {

    $('#formDomicilio').validate({
        rules: {
            cNombreDireccion: {
                required: true,
                minlength: 2,
                maxlength: 50                
            },
            cCalle: {
                required: true,
                minlength: 2,
                maxlength: 50                
            },
            cNumeroExterior: {                
                minlength: 2,
                maxlength: 10   
            },
            cNumeroInterior: {
                minlength: 2,
                maxlength: 10
            },
            cColonia: {
                minlength: 2,
                maxlength: 50
            },
            iCodigoPostal: {
                required: true,
                number: true,
                max: 99999,
                min: 10000
            },
            cMunicipio: {
                required: true,
                minlength: 2,
                maxlength: 30
            },
            cDescripcion: {                
                minlength: 2,
                maxlength: 255
            }
        },
        messages: {
            cNombreDireccion: {
                required: "Por favor ingrese un nombre",
                minlength: "El nombre debe contener mínimo 2 caracteres",
                maxlength: "El máximo permitido es de 50 caracteres"
            },
            cCalle: {
                required: "Por favor ingrese una calle",
                minlength: "La calle debe contener mínimo 2 caracteres",
                maxlength: "La calle debe contener máximo de 10 caracteres"
            },
            cNumeroExterior: {                
                minlength: "El número debe contener mínimo 2 caracteres",
                maxlength: "El número debe contener máximo de 10 caracteres"
            },
            cNumeroInterior: {
                minlength: "El número debe contener mínimo 2 caracteres",
                maxlength: "El número debe contener máximo de 10 caracteres"
            },
            cColonia: {
                minlength: "La colonia debe contener mínimo 2 caracteres",
                maxlength: "La colonia debe contener máximo de 50 caracteres"
            },           
            iCodigoPostal: {
                required: "Por favor ingrese un código postal",
                number: "Por favor ingrese un código postal válido",
                max: "Por favor ingrese un código válido",
                min: "Por favor ingrese un código válido"
            },
            cMunicipio: {
                required: "Por favor ingrese un municipio",
                minlength: "El municipio debe contener mínimo 2 caracteres",
                maxlength: "El municipio debe contener máximo de 30 caracteres"
            },
            cDescripcion: {
                minlength: "La descripción debe contener mínimo 2 caracteres",
                maxlength: "La descripción debe contener máximo de 255 caracteres"
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
 * Método para llamar la vista parcial de direcciones con los nuevos datos
 * */
function LlamarVistaDomicilios() {

    Toast.fire({
        icon: 'success',
        title: '¡Datos actializados Actualizados!'
    })

    $('#modalGeneral').modal('hide')

    ObtenerVistas("Perfil/MisDirecciones", "v-pills-direcciones", AbrirModalDomicilio);
}

/**
 * Método para llamar la vista parcial de direcciones con los nuevos datos después de eliminar una dirección
 * */
function LlamarVistaDomiciliosEliminar() {

    Toast.fire({
        icon: 'success',
        title: '¡Dirección eliminada con éxito!'
    })

    $('#modalGeneral').modal('hide')

    ObtenerVistas("Perfil/MisDirecciones", "v-pills-direcciones", AbrirModalDomicilio);
}