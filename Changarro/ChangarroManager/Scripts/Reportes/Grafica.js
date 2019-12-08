$(document).ready(function () {
    productosPorCategoria();
    clientesConMasCompras();
    porductosMasVendidos();
});

/** Función que obtiene el número de productos que
 * hay en cada categoría.
 * */
function productosPorCategoria() {

    const oElemento = {
        cId: "CategoriasProductos",
        cClase: "amchartdiv"
    }

    ObtenerDatosGraficas("POST", "Inicio/ListaProductosPorCategoria", oElemento, CargarGraficaCategorias)
    
}

/** Función que obtiene los clientes con más compras realizadas.
 */
function clientesConMasCompras() {

    const oElemento = {
        cId: "ClientesCompras",
        cClase: "amchartdiv2"
    }

    ObtenerDatosGraficas("POST", "Inicio/ListaClientesConMasCompras", oElemento, CargarGraficaUsuarios)
}

function porductosMasVendidos() {

    const oElemento = {
        cId: "productosVendidos",
        cClase: "amchartdiv"
    }

    ObtenerDatosGraficas("POST", "Inicio/ListaPorductosMasVendidos", oElemento, CargarGraficaProductos)
}