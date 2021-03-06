﻿$(document).ready(function () {
    InitializeSelect2("#CodigoClientePesquisaPresenca");

    presenca = new Presenca();
    modalPresenca = new ModalPresenca();

    presenca.inicializar();
})

var Presenca = function () {

    this.inicializar = function () {

    }

    this.iniciaDataTable = function () {
        var definicao = [
            { "targets": 0, "name": "Profissional" },
            { "targets": 1, "name": "Especialidade" },
            { "targets": 2, "name": "DtInicio" },
            { "targets": 3, "name": "DtFim" },
            { "targets": 4, "name": "Presenca" },
            { "targets": 5, "name": "BotoesAcao" },
            { "targets": 6, "name": "IdAgenda", "visible": false }
        ]

        return InitDataTable("PresencaCliente", definicao);
    }

    this.pesquisarPresencas = function () {
        $("#groupPresencaCliente").addClass("hidden");

        var idCliente = $("#CodigoClientePesquisaPresenca").val();

        $.ajax({
            type: "GET",
            url: "/Presenca/RecuperarPresencasPorCliente?codigoCliente=" + idCliente,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {

                var tablePresencas = presenca.iniciaDataTable();
                tablePresencas.clear().draw();

                if (data.length != 0) {
                    $.each(data, function (index, linha) {
                        tablePresencas.row.add([
                            linha.NomeProfissional,
                            linha.DescricaoEspecialidade,
                            moment(linha.DataInicioEvento).format("DD/MM/YYYY HH:mm"),
                            moment(linha.DataFimEvento).format("DD/MM/YYYY HH:mm"),
                            linha.SituacaoPresenca,
                            "<a class='btn-link icon-acao icon-acao-editar' onclick='modalPresenca.indicarPresenca(" + linha.IdAgenda + ")' data-toggle='tooltip' data-placement='right' title='Informar presença/ausência'><i class='fa fa-calendar-check-o'></i></a>",
                            linha.IdAgenda
                        ]);
                    });
                    tablePresencas.draw();
                }

                tablePresencas.column(6).visible(false);

                $("#groupPresencaCliente").removeClass("hidden");
            }
        });

    }

}

var ModalPresenca = function () {
    this.abrirModal = function () {
        $("#modalIndicarPresenca").modal('show');
    }

    this.fecharModal = function () {
        $("#modalIndicarPresenca").modal('hide');
    }

    this.limparCamposModal = function () {
        $("#ClientePresenca").html("");
        $("#ProfissionalPresenca").html("");
        $("#EspecialidadePresenca").html("");
        $("#InicioConsultaPresenca").html("");
        $("#FimConsultaPresenca").html("");

        $("input[name=SituacaoPresenca][value=1]").prop('checked', true);        
        $("[name=Justificativa]").val("");
        $("#IdAgendaPresenca").val(""); 
    }

    this.reloadPresencas = function () {
        presenca.pesquisarPresencas();
    }

    this.preencherModalDadosPresenca = function (codAgenda) {
        $.ajax({
            type: "GET",
            url: "/Presenca/RecuperarPresencaPorCodAgenda?codigoAgenda=" + codAgenda,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (!$("#msgPresencaInformada").hasClass("hidden"))
                    $("#msgPresencaInformada").addClass("hidden");

                $("#ClientePresenca").html(data.NomeCliente);
                $("#ProfissionalPresenca").html(data.NomeProfissional);
                $("#EspecialidadePresenca").html(data.DescricaoEspecialidade);
                $("#DataConsultaPresenca").html(moment(data.DataInicioEvento).format("DD/MM/YYYY"));
                $("#InicioConsultaPresenca").html(moment(data.DataInicioEvento).format("HH:mm"));
                $("#FimConsultaPresenca").html(moment(data.DataFimEvento).format("HH:mm"));
                $("[name=Justificativa]").val(data.Justificativa);
                $("#IdAgendaPresenca").val(data.IdAgenda);   

                
                if (data.CodSituacaoPresenca > 0) {
                    $("input[name=SituacaoPresenca][value=" + data.CodSituacaoPresenca + "]").prop('checked', true);
                    $("#msgPresencaInformada").removeClass("hidden");
                } else {
                    $("input[name=SituacaoPresenca]").prop('checked', false);
                }
                    
            }
        });
    }

    this.indicarPresenca = function (codAgenda) {
        modalPresenca.limparCamposModal();

        modalPresenca.preencherModalDadosPresenca(codAgenda);

        modalPresenca.abrirModal();
    }

    this.salvarPresenca = function () {
        var presenca = {
            CodigoAgenda: $("#IdAgendaPresenca").val(),
            SituacaoPresenca: $("input[name=SituacaoPresenca]:checked").prop("value"),
            Justificativa: $("[name=Justificativa]").val()
        }

        if (presenca.SituacaoPresenca == undefined || presenca.SituacaoPresenca == 0) {
            toastr.error("Indique uma situação de presença ou falta para salvar!");
            return;
        }

        $.ajax({
            type: "POST",
            url: "/Presenca/SalvarPresenca",
            data: JSON.stringify({ presenca: presenca }),
            contentType: "application/json; charset=utf-8",
            success: function () {
                modalPresenca.fecharModal();
                modalPresenca.reloadPresencas();
            }
        });
    }
}