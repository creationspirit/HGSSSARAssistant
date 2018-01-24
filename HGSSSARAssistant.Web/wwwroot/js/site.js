﻿$(document).ready(function () {

    // page is now ready, initialize the calendar...
    var $calendar = $('#calendar');
    if (!$calendar.length) {
        return;
    }

    $calendar.fullCalendar({
        // put your options and callbacks here
        header: {
            left: 'Calendar',
            center: '',
            right: ''
        },
        views: {
            dayOfWeekView: {
                type: 'agendaWeek',
                duration: {
                    days: 7
                },
                columnFormat: 'dddd', // Format the day to only show like 'Monday'
                slotLabelFormat: 'h(:mm)a',
                allDaySlot: false,
                slotDuration: '00:30:00'
            }
        },
        defaultView: 'dayOfWeekView',
    });
});


var populateLoactionInputs = function(latLng) {
    $('.js-map-lat').val(latLng.lat);
    $('.js-map-lng').val(latLng.lng);
}

var populateAddressName = function(latlng) {
    var geocoder = new google.maps.Geocoder;
    geocoder.geocode({'location': latlng}, function(results, status) {
      if (status === 'OK') {
        if (results[0]) {
          $('.js-map-address').val(results[0].formatted_address);
        }
      }
    });
}

var initMap = function() {
    var mapMountEl = $('.js-map')[0];
    if (!mapMountEl) {
        return;
    }
    var marker;
    var map = new google.maps.Map(mapMountEl, {
      center: {lat: 45.815, lng: 15.982},
      zoom: 10
    });

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
    
};
