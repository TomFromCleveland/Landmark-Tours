function landmarkItineraryService(baseUrl) {
    
    this.addLandmarkToItinerary = function (landmarkID, itineraryID, successCallback) {

        $.ajax({
            url: baseUrl + "api/add",
            data: {
                LandmarkID: landmarkID,
                ItineraryID: itineraryID
            },
            type: "POST",
            dataType: "json"
        }).done(function (jsonData) {
            successCallback(jsonData);
        }).fail(function (statusCode, xhr, error) {
            console.log(error);
        });
    }

    this.removeLandmark = function (landmarkID, itineraryID, successCallback) {

        $.ajax({
            url: baseUrl + "api/delete",
            data: {
                LandmarkID: landmarkID,
                ItineraryID: itineraryID

            },
            type: "POST",
            dataTYpe: "json"
        }).done(function (jsonData) {
            successCallback(jsondata);
        }).fail(function (statusCode, xhr, error) {
            console.log(error);
        });

    }


}