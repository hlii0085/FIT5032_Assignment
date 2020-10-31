function generateCalendar(events) {
    //console.log(events);
    $('#calendar').fullCalendar({
        defaultView: 'month',
        contentHeight: 600,
        businessHours: true,
        header: {
            left: 'month, agendaWeek, today',
            center: 'title',
            right: 'prevYear, prev, next, nextYear'
        },
        timeFormat: "h(:mm)a",
        events: events,
        eventClick: function (event) {
            $("#name").text(event.title);
            $("#desc").text(event.description);
            $("#time").text("Time: " + moment(event.start).format("DD-MM-YYYY HH:mm"));
            if (event.start < $.now()) {
                $('#book').text("OUT OF DATE");
                $('#book').attr("onclick", "return false");
            } else {
                $('#book').text("Booking");
                var newUrl = "/BookEvents/Book/" + event.eventId;
                $('#book').attr("href", newUrl);
            }
        }
    });
}