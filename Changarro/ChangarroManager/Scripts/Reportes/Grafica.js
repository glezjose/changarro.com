$(document).ready(function () {
    productosPorCategoria();
    clientesConMasCompras();
    productosMasVendidos();
});

/** Función que obtiene los datos de los productos más vendidos.
 */
function productosMasVendidos() {

    const _oElemento =
    {
        _ciId: "ProductosVendidos",
        _cmensajeError: "Error",
        _cmensajeAdvertencia: "Advertencia",
        _cclaseGrafica: "chartdiv"
    }

    obtenerDatosGraficas("POST", "Inicio/ListaProductosMasVendidos", _oElemento, cargarGraficaProductos)
}

/** Función que obtiene datos de los clientes con más compras realizadas.
 */
function clientesConMasCompras() {

    const _oElemento =
    {
        _ciId: "ClientesCompras",
        _cmensajeError: "Error2",
        _cmensajeAdvertencia:"Advertencia2",
        _cclaseGrafica: "amchartdiv2"
    }

    obtenerDatosGraficas("POST", "Inicio/ListaClientesConMasCompras", _oElemento, cargarGraficaUsuarios)
}

/** Función que obtiene el número de productos que hay en cada categoría.
 */
function productosPorCategoria() {

    const _oElemento =
    {
        _ciId: "CategoriasProductos",
        _cmensajeError: "Error3",
        _cmensajeAdvertencia: "Advertencia3",
        _cclaseGrafica: "amchartdiv"
    }

    obtenerDatosGraficas("POST", "Inicio/ListaProductosPorCategoria", _oElemento, cargarGraficaCategorias)

}

