var mymap = L.map('mapid').setView([51.505, -0.09], 13);

L.tileLayer('https://api.tiles.mapbox.com/v4/{id}/{z}/{x}/{y}.png?access_token={accessToken}', {
    attribution: 'Map data &copy; <a href="https://www.openstreetmap.org/">OpenStreetMap</a> contributors, <a href="https://creativecommons.org/licenses/by-sa/2.0/">CC-BY-SA</a>, Imagery © <a href="https://www.mapbox.com/">Mapbox</a>',
    maxZoom: 18,
    minZoom: 2,
    id: 'mapbox.streets',
    accessToken: 'pk.eyJ1IjoicGZwaW1lbnRhIiwiYSI6ImNqcDh0a2pvazAzNncza3A2eHhrbzNxd3cifQ.8JlnHEqau7jfCptsqkJ8sQ'
}).addTo(mymap);


var geojsonFeature = {
    "type": "Feature",
    "features": [
        {
            "type": "Feature",
            "properties": { name: "Rome", language: "Italian" },
            "geometry": {
                "type": "Point",
                "coordinates": [
                    12.496365,
                    41.902782
                ]
            }
        },
        {
            "type": "Feature",
            "properties": { name: "Rio de Janeiro", language: "Portuguese" },
            "geometry": {
                "type": "Point",
                "coordinates": [
                    -43.172897,
                    -22.906847
                ]
            }
        },
        {
            "type": "Feature",
            "properties": { name: "Cancun", language: "Spanish" },
            "geometry": {
                "type": "Point",
                "coordinates": [
                    -86.851524,
                    21.161907
                ]
            }
        },
        {
            "type": "Feature",
            "properties": { name: "London", language: "English" },
            "geometry": {
                "type": "Point",
                "coordinates": [
                    -0.127758,
                    51.507351
                ]
            }
        },
        {
            "type": "Feature",
            "properties": { name: "Madrid", language: "Spanish" },
            "geometry": {
                "type": "Point",
                "coordinates": [
                    -0.127758,
                    51.507351
                ]
            }
        }
    ]
};

var popupContent = '<table>';
for (var p in geojsonFeature.properties) {
    popupContent += '<tr><td>' + p + '</td><td>' + geojsonFeature.properties[p] + '</td></tr>';
}
popupContent += '</table>';
//layer.bindPopup(popupContent);


//L.geoJSON(geojsonFeature).addTo(mymap);

var layerGroup = L.geoJSON(geojsonFeature, {
    onEachFeature: function (feature, layer) {
        //layer.bindPopup(layer.feature.properties.name);
        layer.bindPopup('<h1>' + feature.properties.name + '</h1><br/><p>Language: ' + feature.properties.language + '</p>');
    }
}).addTo(mymap);

mymap.setView([10, 0], 2);