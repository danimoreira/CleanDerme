$(document).ready(function () {
    agenda = new Agenda();
    modalConsulta = new ModalConsulta();

    agenda.inicializarCalendar();
})

var Agenda = function () {

    this.inicializarCalendar = function () {
        $('#agenda').fullCalendar({
            defaultView: 'agendaWeek',
            businessHours: {
                start: '07:00',
                end: '22:00'
            },
            selectable: true,
            height: 1000,
            nowIndicator: true,
            dayClick: function (date, jsEvent, view) {
                agenda.marcarConsultaClickDay(date);
            },
            eventClick: function (event, jsEvent, view) {
                if (event.tipoEvento == 0) {
                    modalConsulta.visualizarEvento(event.id);
                }
                else if (event.tipoEvento == 1) {
                    modalConsulta.visualizarAniversariante(event.codigoCliente);
                }
                else if (event.tipoEvento == 2) {
                    modalServico.receberServico(event.codigoRecebimento);
                }
                
            },
            events: function (start, end, timezone, callback) {
                $.ajax({
                    type: "GET",
                    url: "Agenda/RecuperarEventosAgenda",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        var event = [];
                        $.each(data, function (index, linha) {
                            event.push({
                                id: linha.Id,
                                title: linha.Title,
                                start: linha.Start,
                                end: linha.End,
                                allDay: linha.AllDay,
                                backgroundColor: linha.BackgroundColor,
                                Icon: linha.Icone,
                                tipoEvento: linha.TipoEvento,
                                codigoCliente: linha.CodigoCliente,
                                codigoRecebimento: linha.CodigoRecebimento
                            });

                        });
                        callback(event);
                    }
                });
            },
            eventRender: function (event, element) {
                if (event.icon) {
                    element.find(".fc-title").prepend("<i class='fa fa-" + event.icon + "'></i>");
                }
            },
            customButtons: {
                myCustomButton: {
                    text: 'Marcar Consulta',
                    click: agenda.marcarConsulta
                }
            },
            header: {
                right: 'myCustomButton prev,next today',
                center: 'title',
                left: 'month,agendaWeek,agendaDay'
            }
        })
    }

    this.marcarConsulta = function () {
        modalConsulta.NovoEvento();
        $("#modalConsulta").modal("show");
    }

    this.marcarConsultaClickDay = function (dateTimeClick) {
        modalConsulta.NovoEvento();
        $("#modalConsulta").modal("show");
        $("#DataConsulta").val(dateTimeClick.format("DD/MM/YYYY"));
        $("#HoraInicioConsulta").val(dateTimeClick.format("hh:mm"));
    }

    this.recuperarEventos = function () {
        $.ajax({
            type: "GET",
            url: "Agenda/RecuperarEventosAgenda",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                var event = [];
                $.each(data, function (index, linha) {
                    event.push({
                        id: linha.Id,
                        title: linha.Title,
                        start: moment(linha.Start),
                        end: moment(linha.End),
                        allDay: linha.AllDay,
                        backgroundColor: linha.BackgroundColor,
                        Icon: linha.Icone,
                        tipoEvento: linha.TipoEvento
                    });

                });

                $("#agenda").fullCalendar('removeEvents');
                $("#agenda").fullCalendar('addEventSource', event);
                $("#agenda").fullCalendar('rerenderEvents');
            }
        });
    }    
}


var ModalConsulta = function () {

    this.inicializar = function () {
        InitializeSelect2("#CodClienteConsulta");
        InitializeSelect2("#CodEspecialidadeConsulta");
        InitializeSelect2("#CodProfissionalConsulta");
        InitializeSelect2("#CodServicoConsulta");
    }

    this.recuperarCombosDependentes = function () {
        $('#CodServicoConsulta').val("");
        $('#CodProfissionalConsulta').val("");
        this.recuperarProfissional();
        this.recuperarServicos();
    }

    this.recuperarProfissional = function () {
        var codEspecialidade = $("#CodEspecialidadeConsulta").val();

        if (codEspecialidade != undefined) {
            $.ajax({
                type: "GET",
                url: "Agenda/RecuperarProfissionalPorEspecialidade?codEspecialidade=" + codEspecialidade,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    $("#CodProfissionalConsulta").prop("disabled", false);
                    $('#CodProfissionalConsulta').find('option').remove();
                    var options = $("#CodProfissionalConsulta");
                    options.append($("<option />").val("").text(""));
                    $.each(data, function () {
                        options.append($("<option />").val(this.Value).text(this.Text));
                    });
                }
            });
        }
    }

    this.recuperarServicos = function () {
        var codEspecialidade = $("#CodEspecialidadeConsulta").val();

        if (codEspecialidade != undefined) {
            $.ajax({
                type: "GET",
                url: "Agenda/RecuperarServicosPorEspecialidade?codEspecialidade=" + codEspecialidade,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    $("#CodServicoConsulta").prop("disabled", false);
                    $('#CodServicoConsulta').find('option').remove();
                    var options = $("#CodServicoConsulta");
                    options.append($("<option />").val("").text(""));
                    $.each(data, function () {
                        options.append($("<option />").val(this.Value).text(this.Text));
                    });
                }
            });
        }
    }
    
    this.visualizarEvento = function (idEvento) {

        $.ajax({
            type: "GET",
            url: "Agenda/RecuperarEventoAgenda?id=" + idEvento,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {

                $("#idEventoVisualizar").val(data.Id);
                $("#paciente-evento").html(data.NomeCliente);
                $("#profissional-evento").html(data.NomeProfissional);
                $("#especialidade-evento").html(data.DescricaoEspecialidade);
                $("#data-evento").html(moment(data.DataInicioEvento).format("DD/MM/YYYY"));
                $("#hora-inicio-evento").html(moment(data.DataInicioEvento).format("h:mm"));
                $("#hora-fim-evento").html(moment(data.DataFimEvento).format("h:mm"));
                $("#observacao-evento").html(data.Procedimento);
                $("#modalVisualizar").modal("show");
            }
        });
    }

    this.NovoEvento = function () {
        $("[name=IdConsulta]").val("");
        $("#CodClienteConsulta").val("");
        $("#CodEspecialidadeConsulta").val("");
        $("#CodProfissionalConsulta").val("");
        $("#CodServicoConsulta").val("");
        $("#DataConsulta").val("");
        $("#HoraInicioConsulta").val("");
        $("#HoraFimConsulta").val("");
        $("#ObservacaoConsulta").val("");
    }

    this.editarEvento = function () {

        var idEvento = $("#idEventoVisualizar").val();

        $.ajax({
            type: "GET",
            url: "Agenda/RecuperarEventoAgenda?id=" + idEvento,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                $("#modalVisualizar").modal("hide");
                $("[name=IdConsulta]").val(data.Id);
                $("#CodClienteConsulta").val(data.CodigoCliente);
                $("#CodEspecialidadeConsulta").val(data.CodigoEspecialidade);
                modalConsulta.recuperarProfissional();
                modalConsulta.recuperarServicos();
                $("#CodProfissionalConsulta").val(data.CodigoProfissional);
                $("#CodServicoConsulta").val("");
                $("#DataConsulta").val(moment(data.DataInicioEvento).format("YYYY-MM-DD"));
                $("#HoraInicioConsulta").val(moment(data.DataInicioEvento).format("hh:mm"));
                $("#HoraFimConsulta").val(moment(data.DataFimEvento).format("hh:mm"));
                $("#ObservacaoConsulta").val(data.Procedimento);
                $("#modalConsulta").modal("show");
            }
        });
    }

    this.excluirEvento = function () {

        var idEvento = $("[name=IdConsulta]").val();

        $.ajax({
            type: "POST",
            url: "/Agenda/ExcluirEvento",
            data: JSON.stringify({ idEvento: idEvento }),
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                $("#modalConsulta").modal("hide");
                agenda.recuperarEventos();
            }
        });
    }

    this.realizarAgendamento = function () {

        var errorValidation = false;

        $('input,textarea,select').filter('[required]:visible').each(function () {
            if ($(this).val() == "") {
                toastr.error("Favor preencher o campo: " + $(this).attr("data-LabelError"));
                errorValidation = true;
                return false;
            }
        });

        if (errorValidation) {
            return false;
        }

        debugger;

        var evento = {
            Id: $("[name=IdConsulta]").val(),
            CodigoCliente: $("#CodClienteConsulta").val(),
            CodigoEspecialidade: $("#CodEspecialidadeConsulta").val(),
            CodigoProfissional: $("#CodProfissionalConsulta").val(),
            CodigoServico: $("#CodServicoConsulta").val(),
            DataInicio: moment(moment($("#DataConsulta").val()).format("YYYY-MM-DD") + " " + moment($("#HoraInicioConsulta").val(), "hh:mm").format("hh:mm")).format("YYYY-MM-DD hh:mm"),
            DataFim: moment(moment($("#DataConsulta").val()).format("YYYY-MM-DD") + " " + moment($("#HoraFimConsulta").val(), "hh:mm").format("hh:mm")).format("YYYY-MM-DD hh:mm"),
            Procedimento: $("#ObservacaoConsulta").val()
        }

        $.ajax({
            type: "POST",
            url: "/Agenda/RealizarAgendamento",
            data: JSON.stringify({ objeto: evento }),
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                $("#modalConsulta").modal("hide");
                agenda.recuperarEventos();
            }
        });

    }

    this.indicarPresenca = function () {
        
        var codAgenda = $("#idEventoVisualizar").val();

        modalPresenca.indicarPresenca(codAgenda);
    }

    this.informarAtendimento = function () {

        var codAgenda = $("#idEventoVisualizar").val();

        modalAtendimento.indicarAtendimento(codAgenda);
    }

    this.visualizarAniversariante = function (idAniversariante) {
        $.ajax({
            type: "GET",
            url: "Agenda/RecuperarDadosAniversariante?codigoCliente=" + idAniversariante,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                debugger;
                $("#idCliente").val(data.Id);
                $("#paciente-aniversariante").html(data.Nome);
                $("#data-aniversario").html(moment(data.DataAniversario).format("DD/MM"));
                $("#tel-fixo-aniversario").html(data.TelefoneFixo);
                $("#tel-celular-aniversario").html(data.TelefoneCelular);

                $("#modalAniversariante").modal('show');
            }
        });
    }
}


