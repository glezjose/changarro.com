$(document).ready(function () {
    productosPorCategoria();
    clientesConMasCompras();
    productosMasVendidos();
});

/** Función que obtiene los datos de los productos más vendidos.
 */
function productosMasVendidos() {

    const oElemento =
    {
        cId: "ProductosVendidos",
        cMensajeE: "MensajeError",
        cMensajeA: "MensajeAdvertencia",
        cClase: "chartdiv"
    }

    obtenerDatosGraficas("POST", "Inicio/ListaProductosMasVendidos", oElemento, cargarGraficaProductos)
}

/** Función que obtiene datos de los clientes con más compras realizadas.
 */
function clientesConMasCompras() {

    const oElemento =
    {
        cId: "ClientesCompras",
        cMensajeE: "MensajeError2",
        cMensajeA: "MensajeAdvertencia2",
        cClase: "amchartdiv2"
    }

    obtenerDatosGraficas("POST", "Inicio/ListaClientesConMasCompras", oElemento, cargarGraficaUsuarios)
}

/** Función que obtiene el número de productos que hay en cada categoría.
 */
function productosPorCategoria() {

    const oElemento =
    {
        cId: "CategoriasProductos",
        cMensajeE: "MensajeError3",
        cMensajeA: "MensajeAdvertencia3",
        cClase: "amchartdiv"
    }

    obtenerDatosGraficas("POST", "Inicio/ListaProductosPorCategoria", oElemento, cargarGraficaCategorias)

}

