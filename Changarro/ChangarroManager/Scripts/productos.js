function funcion() {

        Continuar();
        $('#myTab a[href="#detallesPanel"]').click(function (e) {
            e.preventDefault();
            $('.progress-bar').css('width', '33%');
            $('.progress-bar').html('Step 1 of 3');
            CambiarBtn(1);
        });

        $('#myTab a[href="#descripcionPanel"]').click(function (e) {
            e.preventDefault();
            $('.progress-bar').css('width', '66%');
            $('.progress-bar').html('Step 2 of 3');
            CambiarBtn(2);
        });

        $('#myTab a[href="#imagenPanel"]').click(function (e) {
            e.preventDefault();
            $('.progress-bar').css('width', '100%');
            $('.progress-bar').html('Step 3 of 3');
            $('#myTab a[href="#imagenPanel"]').tab('show');
            CambiarBtn(3);
        });
}
function Continuar() {
    $('#continuar').click(function (e) {
        e.preventDefault();
        ValidarCampos();
        var validar = $('#form4').valid();
        if (validar) {
        $('.progress-bar').css('width', '66%');
        $('.progress-bar').html('Step 2 of 3');
        $('#myTab a[href="#descripcionPanel"]').tab('show');
        $('#continuar').attr('id', 'continuar2');
        Continuar2();
        }
    });
}

function Continuar2() {
    $('#continuar2').click(function (e) {
        e.preventDefault();
        $('.progress-bar').css('width', '100%');
        $('.progress-bar').html('Step 3 of 3');
        $('#myTab a[href="#imagenPanel"]').tab('show');
        $('#continuar2').attr('id', 'guardar');
        $('#guardar').text("Guardar");
        GuardarProducto();
    });
}
function GuardarProducto() {
    $('#guardar').click(function (e) {
        e.preventDefault();

        alert("si lo logre!!!");

    });
}

function CambiarBtn(paso) {
    if (paso == 1) {
        $('#modalProductosFooter').html('<button id="continuar" type="button" class="btn btn-warning">Continuar</button>');
        Continuar();
        console.log('1');
    }
    else if (paso == 2) {
        $('#modalProductosFooter').html('<button id="continuar2" type="button" class="btn btn-warning">Continuar</button>');
        Continuar2();
        console.log('2');
    }
    else if (paso == 3) {
        $('#modalProductosFooter').html('<button id="guardar" type="button" class="btn btn-warning">Guardar</button>');
        GuardarProducto();
        console.log('3');
    }
}

function ValidarCampos() {
    $('#form4').validate({
        rules: {
            cNombre: {
                required: true,
                maxlength: 10,
            }
        }
    });
}