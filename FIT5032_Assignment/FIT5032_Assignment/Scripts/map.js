let map;

var xmlHttp = new XMLHttpRequest();
xmlHttp.open("GET", "Hospitals/GetHospitals", false);
xmlHttp.send();
var hospitals = JSON.parse(xmlHttp.responseText);
//console.log(hospitals);

function initMap() {
    map = new google.maps.Map(document.getElementById("map"), {
        center: { lat: -34.397, lng: 150.644 },
        zoom: 8,
    });

    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(
            function (position) {
                var pos = {
                    lat: position.coords.latitude,
                    lng: position.coords.longitude,
                };
                map.setCenter(pos);
            },
            function () {
                handleLocationError(true, infoWindow, map.getCenter());
            }
        );
    } else {
        //browser doesnot support Geolocation
        handleLocationError(false, infoWindow, map.getCenter());
    }

    var geocoder = new google.maps.Geocoder();
    for (var i = 0; i < hospitals.length; i++) {
        geodoceAddress(geocoder, map, hospitals[i]);
    }


}

function handleLocationError(browserHasGeolocation, infoWindow, pos) {
    infoWindow.setPosition(pos);
    infoWindow.setContent(
        browserHasGeolocation
            ? "Error: The Geolocation service failed."
            : "Error: Your browser doesn't support geolocation."
    );
    infoWindow.open(map);
}

function geodoceAddress(geocoder, map, hospital) {

    var content = "<h3>" + hospital.HospitalName + "</h3><h4>" + hospital.Description + "</h4><h4>" + hospital.Address + "</h4>";
    var infowindow = new google.maps.InfoWindow({ content: content });
    console.log(content);

    geocoder.geocode({ address: hospital.Address }, function (results, status) {
        if (status == "OK") {
            var marker = new google.maps.Marker({
                map: map,
                position: results[0].geometry.location
            });

            marker.addListener("click", function () {
                infowindow.open(map, marker);
            })
        }
    })
}