$(document).ready(function () {
    // page is now ready, initialize the calendar...
    var $calendar = $('#calendar');
    if (!$calendar.length) {
        return;
    }

    $calendar.fullCalendar({
        // put your options and callbacks here
        //show closest monday
        defaultDate: moment().startOf("isoweek"),
        header: {
            left: '',
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
                slotLabelFormat: 'H:mm',
                allDaySlot: false,
                slotDuration: '00:30:00',
                minTime: "00:00:00"
            }
        },
        defaultView: 'dayOfWeekView',
        selectable: true,
        selectHelper: true,
        select: function (start, end) {
            var title = prompt('Event Title:');
            var eventData;
            if (title) {
                eventData = {
                    title: title,
                    start: start,
                    end: end
                };
                $('.js-modal').modal('show');
                $('#calendar').fullCalendar('renderEvent', eventData, true); // stick? = true
            }
            $('#calendar').fullCalendar('unselect');
        },
        editable: true,
        eventClick: function (calEvent, jsEvent, view) {
            var title = prompt('Event Title:');
            if (title) {
                calEvent.title = title;

                $('.js-modal').modal('show');
                $('#calendar').fullCalendar('updateEvent', calEvent);
            }
            $('#calendar').fullCalendar('unselect');
        }
    });

    $("#save").click(function () {
        var eventsFromCalendar = $('#calendar').fullCalendar('clientEvents');
        alert(eventsFromCalendar);
        $.ajax(
            {

                url: '@Url.Action("/Users")',
                type: 'POST',
                traditional: true,
                data: { eventsJson: JSON.stringify(eventsFromCalendar) },
                dataType: "json",
                success: function (response) {
                    alert(response);
                },
                error: function (xhr) {
                    debugger;
                    alert(xhr);
                }

            });
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
