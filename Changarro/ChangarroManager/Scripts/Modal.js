function MostrarModal(cUrl, funcion) {
    console.log("ejecutando");
    $.ajax(
        {
            url: cUrl,
            type: 'POST',
            async: false,
            datatype: "HTML",
            success: function (res) {
                //console.log(res);
                $('#modalGeneral').html(res); //se inserta la vista del controlador que haya obtenido de la peticion

                $('#modalGeneral').modal({
                    show: true
                }); // se activa el modal
                funcion();
            },
            error: function (res) {
                alert('error');
            }
        }
    )
}
