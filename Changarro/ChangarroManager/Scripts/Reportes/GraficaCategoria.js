/**
 * Función para cargar la gráfica de los productos por categoría, 
 * en el inicio de la página.
 * @param {any} oCategorias Objeto que contiene los datos necesarios para pintar la gráfica.
 */
function cargarGraficaCategorias(oCategorias) {
   
    am4core.useTheme(am4themes_animated); //tema.


    var chart = am4core.create("CategoriasProductos", am4charts.PieChart); //Crea una instancia del gráfico.
    chart.hiddenState.properties.opacity = 0; // esto crea un desvanecimiento inicial.

    chart.data = oCategorias; // Agregar datos.

    chart.legend = new am4charts.Legend(); //Crear leyenda.

    var series = chart.series.push(new am4charts.PieSeries()); //Crear una instancia de la serie de la gráfica de pastel y obtiene sus valores.
    series.dataFields.value = "iCantidad"; // Representa el valor del campo.
    series.dataFields.category = "cNombre"; //Representa el titulo del campo.

    series.colors.list = [        //Lista de colores.
        am4core.color("#845EC2"),
        am4core.color("#D65DB1"),
        am4core.color("#FF6F91"),
        am4core.color("#FF9671"),
        am4core.color("#FFC75F"),
        am4core.color("#F9F871"),
    ];
}


