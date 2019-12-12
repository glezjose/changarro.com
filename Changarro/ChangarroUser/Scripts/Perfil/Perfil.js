$(document).ready(function () {
    ObtenerVistas("Perfil/MisDatos"); 
    //SubirImagen();
});

/**
 * Método para obtener vistas parciales
 * @param {any} cUrl Dirección del método que devuelve la vista
 */
function ObtenerVistas(cUrl) {
    $.ajax({
        type: "POST",
        url: ruta + cUrl,
        dataType: "html",
        success: function (response) {
            MostrarVistas("v-pills-datos",response)
        }
    });
}

/**
 * Método para mostrar la vista html obtenida
 * @param {any} cElementoID Contiene el ID del elemento html que contendrá la vista
 * @param {any} cVista Contiene el html de la vista parcial
 */
function MostrarVistas(cElementoID, cVista) {
    $("#" + cElementoID).html(cVista);

    $("#btnEditarDatos").click(function (e) {
        
        e.preventDefault();
        
        if ($("#btnEditarDatos").hasClass("editar")) {

            $("#btnEditarDatos").html("Guardar Cambios")

            $("#campoDatosForm").removeAttr("disabled");

            $("#btnEditarDatos").removeClass("editar");

        } else {
            VerificarMisDatos();
        }
    });
}

/**
 * Método para validar los datos antes de guardar
 * */
function VerificarMisDatos() {

    ValidarFormularioMisDatos();

    if ($('#misDatosForm').valid() === true) {

        GuardarCambios("Perfil/ActualizarDatosCliente", { oCliente: JSON.stringify(LlenarObjetoCliente()) }, "Perfil/MisDatos");
    }
}

/**
 * Método para guardar cambios realizados en los da
 * @param {any} cUrl 
 * @param {any} data
 */
function GuardarCambios(cUrl, data, cUrlVistas) {
    $.ajax({
        type: "POST",
        url: ruta + cUrl,
        data: data,
        dataType: "json",
        success: function (response) {
            if (response.lStatus === true) {

                VerificarCambiosRealizados(response._oDatos, cUrlVistas)

            } else {
                alert("no");
            }
        }
    });
}

function VerificarCambiosRealizados(oDatos, cUrlVistas) {

    if (oDatos.iIdCliente > 0) {

        ObtenerVistas(cUrlVistas);

        Toast.fire({
            icon: 'success',
            title: '¡Datos Actualizados!'
        })

    } else {

        ValidarFormularioMisDatos(oDatos.cNombre, oDatos.cApellido, oDatos.cCorreo);
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


function ValidarFormularioMisDatos(cNombre, cApellido, cCorreo) {

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
                min: 1000000000
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