$(document).ready(function () {
    InitializeSelect2("#CodigoClientePesquisaAtendimento");

    atendimento = new Atendimento();
    modalAtendimento = new ModalAtendimento();

    atendimento.inicializar();
})

var Atendimento = function () {

    this.inicializar = function () {

    }

    this.iniciaDataTable = function () {
        var definicao = [
            { "targets": 0, "name": "Profissional" },
            { "targets": 1, "name": "Especialidade" },
            { "targets": 2, "name": "DtInicio" },
            { "targets": 3, "name": "DtFim" },
            { "targets": 4, "name": "Atendimento" },
            { "targets": 5, "name": "BotoesAcao" },
            { "targets": 6, "name": "IdAgenda", "Visible": false, "class": "hidden" }
        ]

        InitDataTable("AtendimentosCliente", definicao);
    }

    this.pesquisarAtendimentos = function () {
        $("#groupAtendimentoCliente").addClass("hidden");
        
        var idCliente = $("#CodigoClientePesquisaAtendimento").val();

        $.ajax({
            type: "GET",
            url: "/Atendimento/RecuperarAtendimentosPorCliente?codigoCliente=" + idCliente,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {

                var tbAtendimento = $('#AtendimentosCliente').DataTable();
                tbAtendimento.clear().draw();

                if (data.length != 0) {
                    $.each(data, function (index, linha) {
                        tbAtendimento.row.add([
                            linha.NomeProfissional,
                            linha.DescricaoEspecialidade,
                            moment(linha.DataInicioEvento).format("DD/MM/YYYY hh:mm"),
                            moment(linha.DataFimEvento).format("DD/MM/YYYY hh:mm"),
                            linha.Atendimento,
                            "<a onclick='modalAtendimento.indicarAtendimento(" + linha.IdAgenda + ")'>Informar Atendimento</a>",
                            linha.IdAgenda
                        ]);
                    });
                    tbAtendimento.draw();
                }

                $("#groupAtendimentoCliente").removeClass("hidden");
            }
        });

    }

}

var ModalAtendimento = function () {
    this.abrirModal = function () {
        $("#modalAtendimento").modal('show');
    }

    this.fecharModal = function () {
        $("#modalAtendimento").modal('hide');
    }

    this.limparCamposModal = function () {
        $("#ProfissionalAtendimento").html("");
        $("#EspecialidadeAtendimento").html("");
        $("#InicioConsultaAtendimento").html("");
        $("#FimConsultaAtendimento").html("");

        $("[name=ObsAtendimento]").val("");
        $("#IdAgendaAtendimento").val("");
    }

    this.reloadAtendimentos = function () {
        atendimento.pesquisarAtendimentos();
    }

    this.preencherModalDadosAtendimento = function (codAgenda) {
        $.ajax({
            type: "GET",
            url: "/Atendimento/RecuperarAtendimentoPorCodAgenda?codigoAgenda=" + codAgenda,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {

                $("#ProfissionalAtendimento").html(data.NomeProfissional);
                $("#EspecialidadeAtendimento").html(data.DescricaoEspecialidade);
                $("#InicioConsultaAtendimento").html(moment(data.DataInicioEvento).format("DD/MM/YYYY hh:mm"));
                $("#FimConsultaAtendimento").html(moment(data.DataFimEvento).format("DD/MM/YYYY hh:mm"));

                $("[name=ObsAtendimento]").val(data.Atendimento);
                $("#IdAgendaAtendimento").val(data.IdAgenda);
            }
        });
    }

    this.indicarAtendimento = function (codAgenda) {
        debugger;

        modalAtendimento.limparCamposModal();

        modalAtendimento.preencherModalDadosAtendimento(codAgenda);

        modalAtendimento.abrirModal();
    }

    this.salvarAtendimento = function () {
        var atendimento = {
            CodigoAgenda: $("#IdAgendaAtendimento").val(),
            ObsAtendimento: $("[name=ObsAtendimento]").val()
        }

        $.ajax({
            type: "POST",
            url: "/Atendimento/SalvarAtendimento",
            data: JSON.stringify({ atendimento: atendimento }),
            contentType: "application/json; charset=utf-8",
            success: function () {
                modalAtendimento.fecharModal();
                modalAtendimento.reloadAtendimentos();
            }
        });
    }
}