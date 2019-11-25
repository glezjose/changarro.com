﻿let tablaCliente = $('#tblCliente').DataTable({
    language: {
        url: '../Assets/spanish.json'
    },
    responsive: true,
    select: true,
    columns: [
        { data: 'ID' },
        { data: 'Nombre' },
        { data: 'Apellido' },
        { data: 'Telefono' },
        { data: 'Correo' },
        { data: 'Estatus' },
        { data: 'Status' },
        { data: 'FechaAlta' },
        { data: 'FechaBaja' },
        { data: 'FechaModificacion' },
    ],
    "columnDefs": [
        {
            "targets": [0],
            "visible": false,
            "searchable": false
        },
        {
            "targets": [6],
            "visible": false,
            "searchable": true
        },
        {
            "targets": [7],
            "visible": false,
            "searchable": false
        },
        {
            "targets": [8],
            "visible": false,
            "searchable": false
        },
        {
            "targets": [9],
            "visible": false,
            "searchable": false
        }
    ]
});
let iIdCliente;
let iIdFilaCliente;

$(document).ready(function () {
    ObtenerClientes();
    MostrarEstados("false");
});

tablaCliente.on('select', function () {
    iIdCliente = tablaCliente.rows({ selected: true }).data()[0]['ID'];
    iIdFilaCliente = tablaCliente.row({ selected: true }).index();
});

tablaCliente.on('user-select', function (e, dt, type, cell, originalEvent) {
    if ($(cell.node()).parent().hasClass('selected')) {
        e.preventDefault();
    }
});

function ObtenerClientes() {
    $.ajax({
        type: 'POST',
        url: '/ChangarroManager/Cliente/ObtenerClientes',
        dataType: 'json',
        async: false,
        success: function (data) {
            PintarTabla(data);
        }
    });
}

function PintarTabla(data) {
    $.each(data, function (i, items) {
        FormatoTabla(items, true);
    });
}

function FormatoTabla(items, op) {

    let span;
    const activo = '<span id="activo" class="badge badge-success">Activo</span>&nbsp&nbsp&nbsp&nbsp';
    const inactivo = '<span id="activo" class="badge badge-danger">Inactivo</span>&nbsp&nbsp&nbsp&nbsp';

    items.lEstatus === true ? span = activo : span = inactivo;

    const rowCliente = {
        "ID": items.iIdCliente,
        "Nombre": items.cNombre,
        "Apellido": items.cApellido,
        "Telefono": items.cTelefono,
        "Correo": items.cCorreo,
        "Estatus": span,
        "Status": items.lEstatus,
        "FechaAlta": FormatoFecha(items.dtFechaAlta),
        "FechaBaja": FormatoFecha(items.dtFechaBaja),
        "FechaModificacion": FormatoFecha(items.dtFechaModificacion),
    };

    if (op === true) {
        tablaCliente.row.add(rowCliente);
        tablaCliente.draw(false);
    }
    else {
        tablaCliente.row(iIdFilaCliente).data(rowCliente).draw(false);
    }
}

function FormatoFecha(fecha) {
    if (fecha == null || fecha == "") {
        fecha = "N/A"
    } else {
        fecha = moment(fecha);
        fecha = fecha.format("DD/MM/YYYY");
    }
    return fecha;
}

$("#btnVerDetalles").click(function (e) {
    e.preventDefault();

    if (iIdCliente > 0) {
        console.log(iIdCliente);
        const cUrl = '/ChangarroManager/Cliente/VerDetallesCliente';
        CargarModal(cUrl, iIdCliente);
    }
    else {
        alert("Por favor seleccione un cliente");
    } 
})

$("#btnCambiarEstatus").click(function (e) {
    e.preventDefault();

    if (iIdCliente > 0) {
        let lEstatus;
        $(this).hasClass("btn-danger") === true ? (lEstatus = false) : (lEstatus = true);

        const cUrl = '/ChangarroManager/Cliente/CambiarEstatusCliente';
        CambiarEstatus(cUrl, iIdCliente, lEstatus);
    }
    else {
        alert("Por favor seleccione un cliente");
    }
})

$("#swichClientes").click(function (e) {
    MostrarClientesActivos();
})

/**
 * En esta función se hace una petición ajax para obtener los datos html y mostrar el modal con los datos que se requieran
 * @param {any} cUrl La url que intentara solicitar
 */
function CargarModal(cUrl, iIdCliente) {
    $.ajax({
        type: 'POST',
        url: cUrl,
        data: { "iIdCliente": iIdCliente },
        dataType: 'html',
        async: false,
        success: function (response) {
            $('#modalGeneral').html(response);
            $('#modalGeneral').modal({
                show: true,
                backdrop: "static"
            });
            iIdCliente = 0;
        },
        error: function () {
            swal("Error no se encontraron datos");
        }
    });
}

function CambiarEstatus(cUrl, iIdCliente, lEstatus) {
    $.ajax({
        type: 'POST',
        url: cUrl,
        data: { "iIdCliente": iIdCliente, "lEstatus": lEstatus },
        dataType: 'json',
        async: false,
        success: function (response) {
            FormatoTabla(response, false);
            alert("Estatus Cambiado"),
            iIdCliente = 0;
            tablaCliente.rows({ selected: true }).deselect();
        },
        error: function () {
            swal("Error no se encontraron datos");
        }
    });
}

function MostrarClientesActivos() {
    iIdCliente = 0;
    tablaCliente.rows({ selected: true }).deselect();
    if (document.getElementById('swichClientes').checked) {
        MostrarEstados("true");
        $("#btnCambiarEstatus").removeClass("btn-danger");
        $("#btnCambiarEstatus").addClass("btn-success");
        $("#btnCambiarEstatus").html('<i class="ti-check icon"></i>');
        //tablaCliente.column(9).visible(true);
    }
    else {
        MostrarEstados("false");
        $("#btnCambiarEstatus").removeClass("btn-success");
        $("#btnCambiarEstatus").addClass("btn-danger");
        $("#btnCambiarEstatus").html('<i class="ti-close icon"></i>');
        //tablaEmp.column(9).visible(false);
    }
}

function MostrarEstados(lEstatus) {
    tablaCliente
        .columns(6)
        .search('^(?:(?!' + lEstatus + ').)*$\r?\n?', true, false)
        .draw();
}