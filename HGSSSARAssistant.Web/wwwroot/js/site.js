$(document).ready(function () {

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

function initMap() {
    initAddressMap();
}
