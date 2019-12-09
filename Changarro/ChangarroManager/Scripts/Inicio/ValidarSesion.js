/**
 * Función que valida los campos del inicio de sesión.
 * @param {any} cCorreo Correo ingresado para validar. 
 * @param {any} cContrasenia Contraseña ingresada para validar.
 */
function validacion(cCorreo, cContrasenia) {

    $('#Formulario').validate({
        rules:
        {
            cContrasenia: {
                required: true,
                minlength: 5,
                maxlength: 50,
                sinContraseña: true
            },
            cCorreo: {
                required: true,
                email: true,
                correoRegistrado: true
            }
        },
        messages:
        {
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

            error.addClass("invalid-feedback");

            if (element.prop("type") === "checkbox") {

                error.insertAfter(element.next("label"));

            } else {
                error.insertAfter(element);
            }
        },
    });

    /***Valida si el correo ya esta registrado.*/
    $.validator.addMethod("correoRegistrado", function (value) {

        if (value.toLowerCase() == cCorreo) {
            return false;
        } else {
            return true;
        };
    }, "Esta dirección de correo no se encuentra registrada");

    /**Valida si la contraseña ingresada es correcta.*/
    $.validator.addMethod("sinContraseña", function (value) {
        if (value == cContrasenia) {
            return false;
        } else {
            return true;
        };
    }, "Contraseña incorrecta");
}
