var $latitudeInput = $('.js-map-lat');
var $longitudeInput = $('.js-map-lng');
var $addressInput = $('.js-map-address');

$addressInput.attr('data-autofill', true);
$addressInput.on('input', function(e) {
    $addressInput.attr('data-autofill', $addressInput.val().length === 0);
});

var populateLoactionInputs = function(latLng) {
    $latitudeInput.val(latLng.lat);
    $longitudeInput.val(latLng.lng);
}

var populateAddressName = function(latlng) {
    var geocoder = new google.maps.Geocoder;
    geocoder.geocode({'location': latlng}, function(results, status) {
      if (status === 'OK') {
        if (results[0] && $addressInput.attr('data-autofill') === 'true') {
          $addressInput.val(results[0].formatted_address);
          $addressInput.attr('data-autofill', true);
        }
      }
    });
}


var initAddressMap = function() {
    var $mapMountEl = $('.js-map');
    if (!$mapMountEl.length) {
        return;
    }
    var marker;
    var map = new google.maps.Map($mapMountEl[0], {
      center: {lat: 45.815, lng: 15.982},
      zoom: 12,
      streetViewControl: false
    });


    var initialMarkerLat = $mapMountEl.data('lat');
    var initialMarkerLng = $mapMountEl.data('lng');

    if (initialMarkerLat && initialMarkerLng) {
        marker = new google.maps.Marker({
          position: {
            lat: parseFloat(initialMarkerLat, 10),
            lng: parseFloat(initialMarkerLng, 10)
          },
          map: map,
          draggable: true
        });
    }

    map.addListener('click', function(e) {
        if (marker) {
            marker.setMap(null);
        }
        marker = new google.maps.Marker({
          position: e.latLng,
          map: map,
          draggable: true
        });
        marker.addListener('dragend', function(event) {
            populateAddressName(event.latLng);
            populateLoactionInputs({
                lat: event.latLng.lat(),
                lng: event.latLng.lng()
            });
        });
        populateAddressName(e.latLng);
        populateLoactionInputs({
            lat: e.latLng.lat(),
            lng: e.latLng.lng()
        });
        marker.setMap(map);
    });
}