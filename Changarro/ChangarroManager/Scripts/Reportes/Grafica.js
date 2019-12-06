$(document).ready(function () {
    productosPorCategoria();
    clientesConMasCompras();
});

/** Función que obtiene el número de productos que
 * hay en cada categoría.
 * */
function productosPorCategoria() {

    ObtenerCategorias("POST", "Inicio/ListaProductosPorCategoria", CargarGraficaCategorias)
    
}

/** Función que obtiene los clientes con más compras realizadas.
 */
function clientesConMasCompras() {

    ObtenerUsuarios("POST", "Inicio/ListaClientesConMasCompras", CargarGraficaUsuarios)
}
