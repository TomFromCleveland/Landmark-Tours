var locations = [];

function generateMap(mapPins) {

    for (var i = 0; i < mapPins.length; i++) {
        var temp = [mapPins[i].Name, mapPins[i].Latitude, mapPins[i].Longitude, "#lmID" + mapPins[i].ID];
        locations.push(temp);
    }

    map = newMap();


    var infowindow = new google.maps.InfoWindow();

    var marker, i;

    for (i = 0; i < locations.length; i++) {
        marker = new google.maps.Marker({
            position: new google.maps.LatLng(locations[i][1], locations[i][2]),
            title: locations[i][0],
            map: map
        });

        google.maps.event.addListener(marker, 'click', (function (marker, i) {
            return function () {
                infowindow.setContent(locations[i][0]);
                infowindow.open(map, marker);
            }
        })(marker, i));

        console.log(marker.title);
        landmarkLatLng.push(marker.position);

    }


};

function addLandmark() {
    var map = newMap();
    var input = document.getElementById('pac-input');
    var searchBox = new google.maps.places.SearchBox(input);
    map.controls[google.maps.ControlPosition.TOP].push(input);

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
    map = new google.maps.Map(document.getElementById('map'), {
        zoom: 10,
        center: new google.maps.LatLng(41.500473, -81.693750),
        mapTypeId: google.maps.MapTypeId.ROADMAP
    });

    return map;
}


function getDistances() {
    if (navigator.geolocation) {
        var timeoutVal = 10 * 1000 * 1000;
        var options = { enableHighAccuracy: true, timeout: timeoutVal, maximumAge: 0 }
        navigator.geolocation.getCurrentPosition(showLocation, errorHandler, options);




    }
    else {
        // Browser doesn't support Geolocation
        alert("Geolocation is not supported by this browser");
    }
}


function makeCallback(name) {
    return function (response, status) {
        if (status === 'OK') {
            var point = response.routes[0].legs[0];
            $(name).text(point.distance.text);
        }
    };
}


function showLocation(position) {

    var latitude = position.coords.latitude;
    var longitude = position.coords.longitude;
    pos = new google.maps.LatLng(position.coords.latitude, position.coords.longitude),

    map.setCenter(pos);
    for (var i = 0; i < landmarkLatLng.length; i++) {
        var directionService = new google.maps.DirectionsService();
        var request = {
            origin: pos,
            destination: landmarkLatLng[i],
            travelMode: google.maps.DirectionsTravelMode.DRIVING
        };

        directionService.route(request, makeCallback(locations[i][3]));

    }
}

//TO DO: Delete once directions works
//function showStartingLocation(startingLatitude, startingLongitude) {
//    var geocoder = new google.maps.Geocoder;
//    var latlng = {lat: parseFloat(startingLatitude), lng: parseFloat(startingLongitude)};
//    geocoder.geocode({'location': latlng}, function (results, status) {
//        if (status === 'OK') {
//            if (results[0]) {
//                $("#startingAddress").val("Your starting location:" + results[0].formatted_address);
//            }
//        };
//    });
//};



function errorHandler(err) {
    if (err.code == 1) {
        alert("Error: Access is denied!");
    }

    else if (err.code == 2) {
        alert("Error: Position is unavailable!");
    }
}


$(document).ready(function () {

    $(".nav a").on("click", function () {
        $(".nav").find(".active").removeClass("active");
        $(this).parent().addClass("active");
    });

    $("#landmark_submit").click(function () {
        $('#LandmarkName').val(places[0].name);
        $("#Latitude").val(places[0].geometry.location.lat());
        $("#Longitude").val(places[0].geometry.location.lng());
        $('#GooglePlacesID').val(places[0].place_id);
        $('.submission_form').submit();
    });
    $("#landmark_table").DataTable();
    $("#itinerary_submit").click(function () {
        $("#StartingLatitude").val(startingLocation.Lat);
        $("#StartingLongitude").val(startingLocation.Lng);
        $(".NewItineraryForm").submit();
    }); 

    var jsonStr = '{"landmarks":[{}]}';
    var obj = JSON.parse(jsonStr);

    $(".add").on("click", addClick);

    $(".delete").on("click", removeClick);

    $(".checkin").on("click", checkin);
    $(".checkout").on("click", checkout);

});

function addClick() {
    var id = this.parentNode.id;
    var itineraryID=$("#itinerary_id").val();
    var numID = id.replace('lmID', '');
    var service = new landmarkItineraryService(serviceUrl);
    service.addLandmarkToItinerary(numID, itineraryID, function (jsonData) {
        console.log(jsonData)
    });

    $(this).toggleClass("delete");
    $(this).toggleClass("add");
    $(this).text("Remove");
    $(this).off("click");
    $(this).on("click", removeClick);

}

function checkin() {
    $(this).toggleClass("checkout");
    $(this).toggleClass("checkin");
    $(this).text("Checked In");
    $(this).off("click");
    $(this).on("click", checkout);
}

function checkout() {
    $(this).toggleClass("checkin");
    $(this).toggleClass("checkout");
    $(this).text("Check In");
    $(this).off("click");
    $(this).on("click", checkin);
}


function removeClick() {
    var id = this.parentNode.id;
    var itineraryID = $("#itinerary_id").val();
    var numID = id.replace('lmID', '');
    var service = new landmarkItineraryService(serviceUrl);
    service.removeLandmark(numID, itineraryID, function (jsonData) {
        console.log(jsonData)
    });

    $(this).toggleClass("add");
    $(this).toggleClass("delete");
    $(this).text("Add");
    $(this).off("click");
    $(this).on("click", addClick);
}


function redirect() {
    var url = $("#RedirectTo").val();
    window.setTimeout(function () {
        window.location.href = url;
    }, 5000);
}



function giveSuggestions() {
    var input = document.getElementById('itinerary_search');
    var searchBox = new google.maps.places.SearchBox(input);


    searchBox.addListener('places_changed', function () {
        places = searchBox.getPlaces();

        if (places.length == 0) {
            return;
        }

        startingLocation.Lat = places[0].geometry.location.lat();
        startingLocation.Lng = places[0].geometry.location.lng();

        return startingLocation;
    });



}


//function addLandmarks() {
//    $("tr").prop('onclick', null).off('click');
//    $("#distance_header").text("Add to Itinerary");
//    $(".landmark_distance").append("<input class=add type=button value=Add More />")


//}