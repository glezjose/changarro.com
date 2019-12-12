function ValidarFormulario() {
    $('#Datos').validate({
        rules: {
            cNombre: {
                required: true,
                minlength: 2,
                maxlength: 50,
                alfanumOespacio: true
            },
            cApellido: {
                required: true,
                minlength: 2,
                maxlength: 50,
                alfanumOespacio: true
            },
            cCorreo: {
                required: true,
                email: true,
            },
            cTelefono: {
                required: true,
                number: true,
                max: 9999999999,
                min: 1000000000,
            }
        },
        messages: {
            cNombre: {
                required: "Por favor ingrese su nombre",
                minlength: "El nombre debe contener mínimo 2 caracteres",
                maxlength: "El máximo permitido es de 50 caracteres"
            },
            cApellido: {
                required: "Por favor ingrese su apellido",
                minlength: "El apellido debe contener mínimo 2 caracteres",
                maxlength: "El máximo permitido es de 50 caracteres"
            },
            cCorreo: {
                required: "Por favor ingrese su correo",
                email: "Por favor ingrese un correo válido"
            },
            cTelefono: {
                required: "Por favor ingrese su número de teléfono",
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

/*Validacion solo letras*/
$.validator.addMethod("alfanumOespacio", function (value, element) {
    return /^[ a-záéíóúüñ]*$/i.test(value);
}, "Ingrese sólo letras");

/*Mensaje Sweetalert2 */
const Toast = Swal.mixin({
    toast: true,
    position: 'top-end',
    showConfirmButton: false,
    timer: 3000,
    timerProgressBar: true,
    onOpen: (toast) => {
        toast.addEventListener('mouseenter', Swal.stopTimer)
        toast.addEventListener('mouseleave', Swal.resumeTimer)
    }
})

