$(document).ready(function () {

    // DO NOT REMOVE : GLOBAL FUNCTIONS!
    pageSetUp();







    
    /*
     * FULL CALENDAR JS
     */

    if ($("#calendar").length) {
        var date = new Date();
        var d = date.getDate();
        var m = date.getMonth();
        var y = date.getFullYear();

        var calendar = $('#calendar').fullCalendar({

            editable: true,
            draggable: true,
            selectable: false,
            selectHelper: true,
            unselectAuto: false,
            disableResizing: false,
            height: "auto",

            header: {
                left: 'title', //,today
                center: 'prev, next, today',
                right: 'month, agenDay' //month, agendaDay,
            },

            select: function (start, end, allDay) {
                var title = prompt('Event Title:');
                if (title) {
                    calendar.fullCalendar('renderEvent', {
                        title: title,
                        start: start,
                        end: end,
                        allDay: allDay
                    }, true // make the event "stick"
                    );
                }
                calendar.fullCalendar('unselect');
            },

            events: [{
                title: '$25,500 Settled',
                start: new Date(y, m, 1),
                description: 'long description',
                className: ["event", "bg-color-greenLight"],
                icon: 'fa-check'
            }, {
                title: '2 Chargebacks recieved',
                start: new Date(y, m, d - 5),
                className: ["event", "bg-color-red"],
                icon: 'fa-lock'
            }, {
                id: 999,
                title: '3 Payments Approved',
                start: new Date(y, m, d - 3, 16, 0),
                allDay: false,
                className: ["event", "bg-color-blue"],
                icon: 'fa-clock-o'
            }, {
                id: 999,
                title: '2 New clients registered',
                start: new Date(y, m, d - 3, 16, 0),
                allDay: false,
                className: ["event", "bg-color-blue"],
                icon: 'fa-clock-o'
            }],


            eventRender: function (event, element, icon) {
                if (!event.description === "") {
                    element.find('.fc-title').append("<br/><span class='ultra-light'>" + event.description + "</span>");
                }
                if (!event.icon === "") {
                    element.find('.fc-title').append("<i class='air air-top-right fa " + event.icon + " '></i>");
                }
            }
        });

    };

    /* hide default buttons */
    $('.fc-toolbar .fc-right, .fc-toolbar .fc-center').hide();

    // calendar prev
    $('#calendar-buttons #btn-prev').click(function () {
        $('.fc-prev-button').click();
        return false;
    });

    // calendar next
    $('#calendar-buttons #btn-next').click(function () {
        $('.fc-next-button').click();
        return false;
    });

    // calendar today
    $('#calendar-buttons #btn-today').click(function () {
        $('.fc-button-today').click();
        return false;
    });

    // calendar month
    $('#mt').click(function () {
        $('#calendar').fullCalendar('changeView', 'month');
    });

    // calendar agenda week
    $('#ag').click(function () {
        $('#calendar').fullCalendar('changeView', 'agendaWeek');
    });

    // calendar agenda day
    $('#td').click(function () {
        $('#calendar').fullCalendar('changeView', 'agendaDay');
    });

    



});