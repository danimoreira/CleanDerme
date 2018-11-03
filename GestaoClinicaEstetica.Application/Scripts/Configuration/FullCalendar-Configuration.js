function InitCalendar(idObjeto, fnCallBackCustom, fnCallBackClickDay) {
    $('#' + idObjeto).fullCalendar({
        defaultView: 'agendaWeek',
        businessHours: {
            start: '07:00',
            end: '22:00'
        },
        height: 1000,
        nowIndicator: true,
        dayClick: function (date, jsEvent, view) {
            fnCallBackClickDay(date);
        },
        customButtons: {
            myCustomButton: {
                text: 'Marcar Consulta',
                click: fnCallBackCustom
            }
        },
        header: {
            right: 'myCustomButton prev,next today',
            center: 'title',
            left: 'month,agendaWeek,agendaDay'
        }
    })
}