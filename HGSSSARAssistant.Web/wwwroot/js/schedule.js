﻿var marker;
var locationPromptModal = function (initialModalData) {
    var $modal = $('.js-modal');
    var $latitudeInput = $modal.find('.js-map-lat');
    var $longitudeInput = $modal.find('.js-map-lng');
    var $addressInput = $modal.find('.js-map-address');
    var $descriptionInput = $modal.find('.js-map-description');

    $modal.modal('show');

    if (initialModalData) {
        $latitudeInput.val(initialModalData.location.lat);
        $longitudeInput.val(initialModalData.location.lng);
        $addressInput.val(initialModalData.name);

        var map = window.map;
        if (marker) {
            marker.setMap(null);
        }
        marker = new google.maps.Marker({
            position: initialModalData.location,
            map: map,
            draggable: true
        });
        marker.addListener('dragend', function (event) {
        });
        marker.setMap(map);
    }

    return new Promise(function (resolve, reject) {  
        $modal.one('hide.bs.modal', function (event) {
            var lat = $latitudeInput.val();
            var lng = $longitudeInput.val();
            var description = $descriptionInput.val().trim();
            var address = $addressInput.val().trim();

            if (lat && lng && description && address) {
                resolve({
                    lat: lat,
                    lng: lng,
                    address: address,
                    description: description
                });
            } else {
                reject('Incomplete data');
            }
        });

    });
};

var initCalendar = function (eventData) {
    var $calendar = $('#calendar');

    if (!$calendar.length) {
        return;
    }

    var schedule = {};

    $calendar.fullCalendar({
        // put your options and callbacks here
        //show closest monday
        defaultDate: moment().startOf('isoweek'),
        header: {
            left: '',
            center: '',
            right: ''
        },
        timeFormat: 'H:mm',
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
                minTime: '00:00:00'
            }
        },
        selectConstraint: {
            start: '00:00',
            end: '24:00',
        },
        defaultView: 'dayOfWeekView',
        eventOverlap: false,
        selectable: true,
        selectHelper: true,
        select: function (start, end) {
            locationPromptModal().then(function (result) {
                eventData = {
                    start: start,
                    end: end,
                    title: result.description,
                    location: {
                        latitude: result.lat,
                        longitude: result.lng,
                        name: result.address,
                        description: result.description
                    }
                };

                $('#calendar').fullCalendar('renderEvent', eventData, true); // stick? = true
            }).catch(function (err) {
                console.log(err);
                $('#calendar').fullCalendar('unselect');
            });
        },
        editable: true,
        eventClick: function (calEvent, jsEvent, view) {
            locationPromptModal({
                title: calEvent.title,
                location: calEvent.location
            }).then(function (result) {
                calEvent.title = result.description;
                calEvent.location = {
                    latitude: result.lat,
                    longitude: result.lng,
                    name: result.address,
                    description: result.description
                }

                $('#calendar').fullCalendar('updateEvent', calEvent);
            }).catch(function (err) {
                console.log(err);
                $('#calendar').fullCalendar('unselect');
            });
        },
        events: eventData || []
    });
}


fetch('/Users/1/Availabilities', {
    method: 'GET',
    credentials: 'include',
    headers: {
        "Content-Type": 'application/json'
    }
}).then(function (result) {
        return result.json();
    }).then(initCalendar)
    .catch(function (err) {
        initCalendar();
});