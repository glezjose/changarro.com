/**
 * Función para cargar la gráfica de los usuarios con más compras.
 * @param {any} oProductos Objeto que contiene los datos para pintar la gráfica.
 */
function cargarGraficaProductos(oProductos) {

 
    am4core.useTheme(am4themes_material); //Tema
    am4core.unuseTheme(am4themes_animated);
   
    var chart = am4core.create("ProductosVendidos", am4charts.XYChart);  // Crear instancia del gráfico.
  
    chart.data = oProductos;   // Agregar Datos.
   
  
    var categoryAxis = chart.xAxes.push(new am4charts.CategoryAxis());  //Crear eje X
    categoryAxis.dataFields.category = "cNombre";
    categoryAxis.renderer.grid.template.location = 0;
    categoryAxis.renderer.minGridDistance = 30;
   
    categoryAxis.renderer.labels.template.dy = 5;
    categoryAxis.tooltip.disabled = true;
    categoryAxis.renderer.minHeight = 110;

    var label = categoryAxis.renderer.labels.template; //Cortar letras de las etiquetas.
    label.truncate = true;
    label.maxWidth = 150;

    var valueAxis = chart.yAxes.push(new am4charts.ValueAxis());  //Crear eje Y
    valueAxis.renderer.minWidth = 50;

  
    var series = chart.series.push(new am4charts.ColumnSeries()); // Crear series
    series.sequencedInterpolation = true;
    series.dataFields.valueY = "iCantidad";
    series.dataFields.categoryX = "cNombre";
    series.tooltipText = "[{categoryX}: bold]{valueY}[/]";
    series.columns.template.strokeWidth = 0;

    series.tooltip.pointerOrientation = "down";

    series.columns.template.column.cornerRadiusTopLeft = 10;
    series.columns.template.column.cornerRadiusTopRight = 10;
    series.columns.template.column.fillOpacity = 0.8;

    series.columns.template.adapter.add("fill", function (fill, target) {
        return chart.colors.getIndex(target.dataItem.index);
    });

    
    chart.cursor = new am4charts.XYCursor(); // Cursor

}

