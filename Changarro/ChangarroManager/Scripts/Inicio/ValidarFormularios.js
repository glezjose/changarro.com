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
