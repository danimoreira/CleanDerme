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

        return InitDataTable("AtendimentosCliente", definicao);
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

                var tbAtendimento = atendimento.iniciaDataTable();
                tbAtendimento.clear().draw();

                if (data.length != 0) {
                    $.each(data, function (index, linha) {
                        tbAtendimento.row.add([
                            linha.NomeProfissional,
                            linha.DescricaoEspecialidade,
                            moment(linha.DataInicioEvento).format("DD/MM/YYYY HH:mm"),
                            moment(linha.DataFimEvento).format("DD/MM/YYYY HH:mm"),
                            linha.Atendimento,
                            "<a class='btn-link icon-acao icon-acao-editar' onclick='modalAtendimento.indicarAtendimento(" + linha.IdAgenda + ")' data-toggle='tooltip' data-placement='right' title='Informar atendimento'><i class='fa fa-id-card'></i></a>",
                            linha.IdAgenda
                        ]);
                    });
                    tbAtendimento.draw();
                }

                tbAtendimento.column(6).visible(false);

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
        $("#ClienteAtendimento").html("");
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

                $("#ClienteAtendimento").html(data.NomeCliente);
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