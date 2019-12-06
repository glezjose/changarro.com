/**
 * Carga la gráfica en el inicio de la página.
 * @param {any} oCategorias Recibe el objeto de datos desde el ajax.
 */
function CargarGraficaCategorias(oCategorias) {
    // Themes begin
     am4core.useTheme(am4themes_animated);


    var chart = am4core.create("CategoriasProductos", am4charts.PieChart);
    chart.hiddenState.properties.opacity = 0; // this creates initial fade-in

    chart.data = oCategorias;

    chart.legend = new am4charts.Legend();

    var series = chart.series.push(new am4charts.PieSeries());
    series.dataFields.value = "iCantidad";
    series.dataFields.category = "cNombre";

    series.colors.list = [
        am4core.color("#845EC2"),
        am4core.color("#D65DB1"),
        am4core.color("#FF6F91"),
        am4core.color("#FF9671"),
        am4core.color("#FFC75F"),
        am4core.color("#F9F871"),
    ];
}


