/**
 * Método para validar el formulario de registro del cliente
 * @param {any} cNombre Cadena con el nombre del cliente
 * @param {any} cApellido Cadena con el apellido del cliente
 * @param {any} cCorreo Cadena con el correo del cliente
 */
function ValidarRegistro(cNombre, cApellido, cCorreo) {

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

    $('#registroForm').validate({
        rules: {
            cNombre: {
                required: true,
                minlength: 2,
                maxlength: 50,
                nombreRepetido : true
            },
            cApellido: {
                required: true,
                minlength: 2,
                maxlength: 50,
                apellidoRepetido : true
            },
            cContrasenia: {
                required: true,
                minlength: 5,
                maxlength: 50
            },
            cConfirmContrasenia: {
                required: true,
                minlength: 5,
                maxlength: 50,
                equalTo: "#cContrasenia"
            },
            cCorreo: {
                required: true,
                email: true,
                emailRepetido: true
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
            cContrasenia: {
                required: "Por favor ingrese una contraseña",
                minlength: "La contraseña debe tener al menos 5 caracteres"
            },
            cConfirmContrasenia: {
                required: "Por favor ingrese una contraseña",
                minlength: "La contraseña debe tener el menos 5 caracteres",
                equalTo: "Por favor inserte la misma contraseña de arriba"
            },
            cCorreo: {
                required: "Por favor ingrese un correo",
                email: "Por favor ingrese un correo válido"
            }            
        },
        errorElement: "em",
        errorPlacement: function (error, element) {
            // Agrega la clase 'invalid-feedback' al elemento de error
            // Add the `invalid-feedback` class to the error element
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
 * Método para validar el formulario de inicio de sesión
 * @param {any} cCorreo Cadena con el correo del cliente
 * @param {any} cContrasenia Cadena con el correo del cliente
 */
function ValidarLogin(cCorreo, cContrasenia) {

    jQuery.validator.addMethod("emailRegistrado", function (value, element) {
        if (value.toLowerCase() == cCorreo) {
            return false;
        } else {
            return true;
        };
    }, "Esta dirección de correo no se encuentra registrada");

    jQuery.validator.addMethod("sinPass", function (value, element) {
        if (value == cContrasenia) {
            return false;
        } else {
            return true;
        };
    }, "Contraseña incorrecta");

    $('#loginForm').validate({
        rules: {
            cContrasenia: {
                required: true,
                minlength: 5,
                maxlength: 50,
                sinPass: true
            },
            cCorreo: {
                required: true,
                email: true,
                emailRegistrado: true
            }
        },
        messages: {            
            cContrasenia: {
                required: "Por favor ingrese una contraseña",
                minlength: "La contraseña debe tener al menos 5 caracteres"
            },            
            cCorreo: {
                required: "Por favor ingrese un correo",
                email: "Por favor ingrese un correo válido"
            }
        },
        errorElement: "em",
        errorPlacement: function (error, element) {
            // Agrega la clase 'invalid-feedback' al elemento de error
            // Add the `invalid-feedback` class to the error element
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
