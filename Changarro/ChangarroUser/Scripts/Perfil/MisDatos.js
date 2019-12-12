/**
 * Método para habilitar el formulario de edición de datos del usuario
 * */
function HabilitarFormularioDatosPerfil() {

    $("#btnEditarDatos").click(function (e) {

        e.preventDefault();

        if ($("#btnEditarDatos").hasClass("editar")) {

            $("#btnEditarDatos").html("Guardar Cambios")

            $("#campoDatosForm").removeAttr("disabled");

            $("#btnEditarDatos").removeClass("editar");

        } else {
            VerificarMisDatosUsuario();
        }
    });

}

/**
 * Método para validar los datos antes de guardar
 * */
function VerificarMisDatosUsuario() {

    ValidarFormularioMisDatos();

    if ($('#misDatosForm').valid() === true) {

        GuardarCambios("Perfil/ActualizarDatosCliente", { oCliente: JSON.stringify(LlenarObjetoCliente()) }, "Perfil/MisDatos", VerificarCambiosRealizados);
    }
}

/**
 * Método para comprobar el resultado del guardado de los cambios
 * @param {any} oDatos
 * @param {any} cUrlVistas
 */
function VerificarCambiosRealizados(oDatos, cUrlVistas) {

    if (oDatos.iIdCliente > 0) {

        ObtenerVistas(cUrlVistas, "v-pills-datos", HabilitarFormularioDatosPerfil);

        Toast.fire({
            icon: 'success',
            title: '¡Cambios realizados con éxito!'
        })

    } else {

        ValidarFormularioMisDatos(oDatos.cNombre, oDatos.cApellido, oDatos.cCorreo, oDatos.cTelefono);
        $('#misDatosForm').valid();
    }
}

/**
 * Método para obtener los datos del formulario
 * */
function LlenarObjetoCliente() {
    const oCliente =
    {
        cNombre: $.trim($("#cNombre").val()),
        cApellido: $.trim($("#cApellido").val()),
        cTelefono: $.trim($("#cTelefono").val()),
        cCorreo: $.trim($("#cCorreo").val().toLocaleLowerCase())
    }

    return oCliente;
}

/**
 * Método para validar el formulario de datos del usuario
 * @param {any} cNombre Cadena con el nombre del usuario
 * @param {any} cApellido Cadena con el apellido del usuario
 * @param {any} cCorreo Cadena con el Correo del usuario
 * @param {any} cTelefono Cadena con número de teléfono del usuario
 */
function ValidarFormularioMisDatos(cNombre, cApellido, cCorreo, cTelefono) {
    jQuery.validator.addMethod("telefonoRepetido", function (value, element) {
        if (value == cTelefono) {
            return false;
        } else {
            return true;
        };
    }, "Este número de teléfono ya ha sido registrado");

    jQuery.validator.addMethod("emailRepetido", function (value, element) {
        if (value.toLowerCase() == cCorreo) {
            return false;
        } else {
            return true;
        };
    }, "Este correo electrónico ya ha sido registrado");

    jQuery.validator.addMethod("nombreRepetido", function (value, element) {
        if (value.toLowerCase() == cNombre) {
            return false;
        } else {
            return true;
        };
    }, "Ya existe un usuario registrado con este nombre");

    jQuery.validator.addMethod("apellidoRepetido", function (value, element) {
        if (value.toLowerCase() == cApellido) {
            return false;
        } else {
            return true;
        };
    }, "Ya existe un usuario registrado con este apellido");

    $('#misDatosForm').validate({
        rules: {
            cNombre: {
                required: true,
                minlength: 2,
                maxlength: 50,
                nombreRepetido: true
            },
            cApellido: {
                required: true,
                minlength: 2,
                maxlength: 50,
                apellidoRepetido: true
            },
            cCorreo: {
                required: true,
                email: true,
                emailRepetido: true
            },
            cTelefono: {
                required: true,
                number: true,
                max: 9999999999,
                min: 1000000000,
                telefonoRepetido: true
            }
        },
        messages: {
            cNombre: {
                required: "Por favor ingrese un nombre",
                minlength: "El nombre debe contener mínimo 2 caracteres",
                maxlength: "El máximo permitido es de 50 caracteres"
            },
            cApellido: {
                required: "Por favor ingrese un apellido",
                minlength: "El apellido debe contener mínimo 2 caracteres",
                maxlength: "El máximo permitido es de 50 caracteres"
            },
            cCorreo: {
                required: "Por favor ingrese un correo",
                email: "Por favor ingrese un correo válido"
            },
            cTelefono: {
                required: "Por favor ingrese un teléfono",
                number: "Por favor ingrese un número de teléfono válido",
                max: "Por favor ingrese un número de teléfono válido",
                min: "Por favor ingrese un número de teléfono válido"
            }
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