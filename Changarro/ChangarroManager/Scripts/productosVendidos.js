am4core.useTheme(am4themes_animated);

var chart = am4core.createFromConfig({
  // Reduce saturation of colors to make them appear as toned down
  "colors": {
    "saturation": 0.4
  },

  // Setting data
  "data": [{
    "country": "Producto 1",
    "visits": 3025
  }, {
    "country": "Producto 2",
    "visits": 1882
  }, {
    "country": "Producto 3",
    "visits": 1809
  }, {
    "country": "Producto 4",
    "visits": 1322
  }, {
    "country": "Producto 5",
    "visits": 1122
  }, {
    "country": "Producto 6",
    "visits": 1114
  }, {
    "country": "Producto 7",
    "visits": 984
  }, {
    "country": "Producto 8",
    "visits": 711
  }, {
    "country": "Producto 9",
    "visits": 665
  }, {
    "country": "Producto 10",
    "visits": 580
  }],

  // Add Y axis
  "yAxes": [{
    "type": "ValueAxis",
    "renderer": {
      "maxLabelPosition": 0.98
    }
  }],

  // Add X axis
  "xAxes": [{
    "type": "CategoryAxis",
    "renderer": {
      "minGridDistance": 20,
      "grid": {
        "location": 0
      }
    },
    "dataFields": {
      "category": "country"
    }
  }],

  // Add series
  "series": [{
    // Set type
    "type": "ColumnSeries",

    // Define data fields
    "dataFields": {
      "categoryX": "country",
      "valueY": "visits"
    },

    // Modify default state
    "defaultState": {
      "transitionDuration": 1000
    },

    // Set animation options
    "sequencedInterpolation": true,
    "sequencedInterpolationDelay": 100,

    // Modify color appearance
    "columns": {
      // Disable outline
      "strokeOpacity": 0,

      // Add adapter to apply different colors for each column
      "adapter": {
        "fill": function (fill, target) {
          return chart.colors.getIndex(target.dataItem.index);
        }
      }
    }
  }],

  // Enable chart cursor
  "cursor": {
    "type": "XYCursor",
    "behavior": "zoomX"
  }
}, "productosVendidos", "XYChart");
