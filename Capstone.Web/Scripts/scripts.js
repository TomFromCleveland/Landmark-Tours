function generateMap(mapPins) {
    var locations = [];
    for (var i = 0; i < mapPins.length; i++) {
        var temp = [mapPins[i].Name, mapPins[i].Latitude, mapPins[i].Longitude];
        locations.push(temp);
    }

    var map = newMap();
    var infowindow = new google.maps.InfoWindow();

    var marker, i;

    for (i = 0; i < locations.length; i++) {
        marker = new google.maps.Marker({
            position: new google.maps.LatLng(locations[i][1], locations[i][2]),
            map: map
        });

        google.maps.event.addListener(marker, 'click', (function (marker, i) {
            return function () {
                infowindow.setContent(locations[i][0]);
                infowindow.open(map, marker);
            }
        })(marker, i));
    }


};


function addLandmark() {
    var map = newMap();
    var input = document.getElementById('pac-input');
    var searchBox = new google.maps.places.SearchBox(input);
    map.controls[google.maps.ControlPosition.TOP_LEFT].push(input);

    // Bias the SearchBox results towards current map's viewport.
    map.addListener('bounds_changed', function () {
        searchBox.setBounds(map.getBounds());
    });

    var markers = [];
    searchBox.addListener('places_changed', function () {
        places = searchBox.getPlaces();

        if (places.length == 0) {
            return;
        }

        // Clear out the old markers.
        markers.forEach(function (marker) {
            marker.setMap(null);
        });

        markers = [];
        var bounds = new google.maps.LatLngBounds();
        places.forEach(function (place) {
            if (!place.geometry) {
                console.log("Returned place contains no geometry");
                return;
            }
            var icon = {
                url: place.icon,
                size: new google.maps.Size(71, 71),
                origin: new google.maps.Point(0, 0),
                anchor: new google.maps.Point(17, 34),
                scaledSize: new google.maps.Size(25, 25)
            };

            // Create a marker for each place.
            markers.push(new google.maps.Marker({
                map: map,
                icon: icon,
                title: place.name,
                animation: google.maps.Animation.DROP,
                position: place.geometry.location
            }));

            if (place.geometry.viewport) {
                bounds.union(place.geometry.viewport);
            } else {
                bounds.extend(place.geometry.location);
            }

        });
        map.fitBounds(bounds);
        return places;
    });
    
}

function newMap() {
    return new google.maps.Map(document.getElementById('map'), {
        zoom: 10,
        center: new google.maps.LatLng(41.500473, -81.693750),
        mapTypeId: google.maps.MapTypeId.ROADMAP
    });
}



$(document).ready(function () {
      $(".nav a").on("click", function () {
        $(".nav").find(".active").removeClass("active");
        $(this).parent().addClass("active");
    });

    $("#landmark_submit").click(function () {
        $('#LandmarkName').val(places[0].name);
        $('#StreetAddress').val(places[0].address_components[0].long_name + ' ' + places[0].address_components[1].long_name);
        $('#City').val(places[0].address_components[2].long_name);
        $('#State').val(places[0].address_components[4].long_name);
        $('#zip').val(places[0].address_components[6].short_name);
        $('.submission_form').submit();
    });
});




