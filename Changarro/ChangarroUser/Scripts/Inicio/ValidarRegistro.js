function ValidarCampos() {
    $('#registroForm').validate({
        rules: {
            cNombre: {
                required: true,
                minlength: 2,
                maxlength: 50,
            },
            cApellido: {
                required: true,
                minlength: 2,
                maxlength: 50,
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
            cCorreo: "Por favor ingrese un correo válido",
            
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
