/**
 * Función para cargar la gráfica productos más vendidos en la página de inicio.
 * @param {any} oUsuariosCompras Objeto que contiene los datos necesarios para pintar la gráfica.
 */
function cargarGraficaUsuarios(oUsuariosCompras) {

    am4core.useTheme(am4themes_frozen); // Temas
    am4core.unuseTheme(am4themes_animated);
  
    var chart = am4core.create("ClientesCompras", am4charts.XYChart); //Crear instancia del gráfico.
    chart.hiddenState.properties.opacity = 0; 

    chart.data = oUsuariosCompras; //Agregar datos a partir del objeto.
    
    var categoryAxis = chart.xAxes.push(new am4charts.CategoryAxis()); //Crear eje X
    categoryAxis.dataFields.category = "cNombre";
    categoryAxis.renderer.grid.template.disabled = true;
    categoryAxis.renderer.minGridDistance = 30;
    categoryAxis.renderer.inside = true;
    categoryAxis.renderer.labels.template.fill = am4core.color("#fff");
    categoryAxis.renderer.labels.template.fontSize = 15;

    var valueAxis = chart.yAxes.push(new am4charts.ValueAxis()); //Crear eje Y
    valueAxis.renderer.grid.template.strokeDasharray = "4,4";
    valueAxis.renderer.labels.template.disabled = true;
    valueAxis.min = 0;

   
    chart.maskBullets = false;
    chart.paddingBottom = 0; // Eliminar el relleno


    var series = chart.series.push(new am4charts.ColumnSeries());  // Crear series.
    series.dataFields.valueY = "iTotalCompras";
    series.dataFields.categoryX = "cNombre";
    series.columns.template.column.cornerRadiusTopLeft = 15;
    series.columns.template.column.cornerRadiusTopRight = 15;
    series.columns.template.tooltipText = "{categoryX}: [bold]{valueY}[/b]";


 
    var bullet = series.bullets.push(new am4charts.Bullet());    // Agregar viñetas.
    var image = bullet.createChild(am4core.Image);
    image.horizontalCenter = "middle";
    image.verticalCenter = "bottom";
    image.dy = 15;
    image.y = am4core.percent(100);
    image.propertyFields.href = "cImagen";
    image.tooltipText = series.columns.template.tooltipText;
    image.filters.push(new am4core.DropShadowFilter());

    series.columns.template.adapter.add("fill", function (fill, target) {
        return chart.colors.getIndex(target.dataItem.index);
    });
}