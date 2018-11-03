$(document).ready(function () {
    InitializeSelect2("#CodigoClienteRecebimento");

    financeiro = new Financeiro();
    modalServico = new ModalServico();
    modalAgendarPagto = new ModalAgendarPagto();

    financeiro.inicializar();
    modalServico.inicializar();
    modalAgendarPagto.inicializar();
})

var Financeiro = function () {

    this.inicializar = function () {
        financeiro.inicializarDataTable();
    }

    this.inicializarDataTable = function () {

        var definicao = [
            { "targets": 0, "name": "Servico" },
            { "targets": 1, "name": "DtVenc" },
            { "targets": 2, "name": "VlrDevido" },
            { "targets": 3, "name": "FormaPagto" },
            { "targets": 4, "name": "Situacao" },
            { "targets": 5, "name": "BotoesAcao" },
            { "targets": 6, "name": "IdRecebimento", "bVisible": false, "class": "hidden" }
        ]

        InitDataTable("RecebimentoCliente", definicao);
    }

    this.recuperarDadosRecebimento = function () {
        $("#groupParcelasReceber").addClass("hidden");

        var idCliente = $("#CodigoClienteRecebimento").val();

        $.ajax({
            type: "GET",
            url: "/Financeiro/RecuperarDadosRecebimentoCliente?codigoCliente=" + idCliente,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {

                var recebimento = [];

                var tableRecebimento = $('#RecebimentoCliente').DataTable();

                tableRecebimento.clear().draw();

                if (data.length != 0) {
                    $.each(data, function (index, linha) {

                        tableRecebimento.row.add([
                            linha.DescricaoServico,
                            moment(linha.DataVencimento).format("DD/MM/YYYY"),
                            "R$ " + FormatDecimal(linha.ValorDevido),
                            linha.TipoPagamento,
                            linha.SituacaoPagamento,
                            "<a onclick='modalServico.receberServico(" + linha.IdRecebimento + ")'>Receber</a>",
                            linha.IdRecebimento
                        ]);

                    });

                    tableRecebimento.draw();
                }

                $("#groupParcelasReceber").removeClass("hidden");
            }
        });
    }

}

var ModalServico = function () {

    this.inicializar = function () {
        if ($("[name=IdRecebimento]").val() != "") {
            modalServico.recuperarCombosDependentes();
        }
    }

    this.receberPagamentoAvulso = function () {
        var codCliente = parseInt($("#CodigoClienteRecebimento").val());
        modalServico.abrirModal(codCliente);
    }

    this.abrirModal = function (idCliente) {

        modalServico.limparCamposReceberPagamento();

        $("[name=CodClienteIncServico]").prop("disabled", false);

        if (idCliente != 0) {
            $("[name=CodClienteIncServico]").val(idCliente);
            $("[name=CodClienteIncServico]").prop("disabled", true);
        }

        $("#modalIncluirServico").modal('show');
    }

    this.limparCamposReceberPagamento = function () {
        $("[name=CodClienteIncServico]").prop("disabled", false);
        $("[name=CodEspecialidadeIncServico]").prop("disabled", false);
        $("[name=CodProfissionalIncServico]").prop("disabled", true);
        $("[name=CodServicoIncServico]").prop("disabled", true);

        $("[name=IdRecebimento]").val("");
        $("[name=DataAquisicao]").val("");
        $("[name=CodClienteIncServico]").val("");
        $("[name=CodEspecialidadeIncServico]").val("");
        $("[name=CodProfissionalIncServico]").val("");
        $("[name=CodServicoIncServico]").val("");
        $("[name=FormaPagamento]").val("");
        $("[name=ValorServico]").val("");
        $("[name=DataVencimento]").val("");
        $("[name=DataPagamento]").val("");
        $("[name=ValorPago]").val("");
    }

    this.incluirServico = function () {
        $("#modalIncluirServico").modal('hide');
    }

    this.recuperarCombosDependentes = function () {        
        this.recuperarProfissional();
        this.recuperarServicos();
    }

    this.receberPagamento = function () {

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

        var parcela = {
            Id: $("[name=IdRecebimento]").val(),
            CodigoCliente: $("[name=CodClienteIncServico]").val(),
            CodigoEspecialidade: $("[name=CodEspecialidadeIncServico]").val(),
            CodigoProfissional: $("[name=CodProfissionalIncServico]").val(),
            CodigoServico: $("[name=CodServicoIncServico]").val(),
            DataAquisicao: $("[name=DataAquisicao]").val(),
            DataVencimento: $("[name=DataVencimento]").val(),
            DataPagamento: $("[name=DataPagamento]").val(),
            ValorDevido: $("[name=ValorServico]").val(),
            ValorRecebido: $("[name=ValorPago]").val(),
            TipoPagamento: $("[name=FormaPagamento]").val(),
            SituacaoPagamento: 1
        }

        $.ajax({
            type: "POST",
            url: "/Financeiro/ReceberPagamento",
            data: JSON.stringify({ parcela: parcela }),
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                financeiro.recuperarDadosRecebimento();

                $("#modalIncluirServico").modal('hide');
            }
        });


    }

    this.recuperarProfissional = function () {
        var codEspecialidade = $("#CodEspecialidade").val();

        if (codEspecialidade != undefined) {
            $.ajax({
                type: "GET",
                url: "Agenda/RecuperarProfissionalPorEspecialidade?codEspecialidade=" + codEspecialidade,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    $("#CodigoProfissional").prop("disabled", false);
                    $('#CodigoProfissional').find('option').remove();
                    var options = $("#CodigoProfissional");
                    options.append($("<option />").val("").text(""));
                    $.each(data, function () {
                        options.append($("<option />").val(this.Value).text(this.Text));
                    });
                }
            });
        }
    }

    this.recuperarServicos = function () {
        var codEspecialidade = $("#CodEspecialidade").val();

        if (codEspecialidade != undefined) {
            $.ajax({
                type: "GET",
                url: "Agenda/RecuperarServicosPorEspecialidade?codEspecialidade=" + codEspecialidade,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    $("#CodServico").prop("disabled", false);
                    $('#CodServico').find('option').remove();
                    var options = $("#CodServico");
                    options.append($("<option />").val("").text(""));
                    $.each(data, function () {
                        options.append($("<option />").val(this.Value).text(this.Text));
                    });
                }
            });
        }
    }

    this.recuperarInfoServico = function () {
        var codigoServico = $("#CodServico").val();

        if (codigoServico != undefined) {
            $.ajax({
                type: "GET",
                url: "Financeiro/RecuperarInfoServico?codigoServico=" + codigoServico,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    $("[name=ValorServico]").val(FormatDecimal(data.ValorServico));
                    $("[name=DataVencimento]").val(moment().format("DD/MM/YYYY"));
                    $("[name=DataPagamento]").val(moment().format("DD/MM/YYYY"));
                    $("[name=ValorPago]").val(FormatDecimal(data.ValorServico));
                }
            });
        }
    }

    this.preencherProfissionais = function (codEspecialidade) {
        $.ajax({
            type: "GET",
            url: "Agenda/RecuperarProfissionalPorEspecialidade?codEspecialidade=" + codEspecialidade,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                $('[name=CodProfissionalIncServico]').find('option').remove();
                var options = $("[name=CodProfissionalIncServico]");
                options.append($("<option />").val("").text(""));
                $.each(data, function () {
                    options.append($("<option />").val(this.Value).text(this.Text));
                });
            }
        });
    }

    this.preencherServicos = function (codEspecialidade) {
        $.ajax({
            type: "GET",
            url: "Agenda/RecuperarServicosPorEspecialidade?codEspecialidade=" + codEspecialidade,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                $("[name=CodServicoIncServico]").find('option').remove();
                var options = $("[name=CodServicoIncServico]");
                options.append($("<option />").val("").text(""));
                $.each(data, function () {
                    options.append($("<option />").val(this.Value).text(this.Text));
                });
            }
        });
    }

    this.receberServico = function (codRecebimento) {       

        $.ajax({
            type: "GET",
            url: "/Financeiro/RecuperarDadosRecebimento?codigoRecebimento=" + codRecebimento,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                //modalServico.limparCamposReceberPagamento();

                debugger;
                $("[name=IdRecebimento]").val(data.IdRecebimento);
                $("[name=DataAquisicao]").val(moment(data.DataAquisicao).format("DD/MM/YYYY"));
                $("[name=CodClienteIncServico]").val(data.CodigoCliente);
                $("[name=CodEspecialidadeIncServico]").val(data.CodigoEspecialidade);                

                //modalServico.preencherServicos(data.CodigoEspecialidade);
                //modalServico.preencherProfissionais(data.CodigoEspecialidade);

                $("[name=CodProfissionalIncServico]").val(data.CodigoProfissional);
                $("[name=CodServicoIncServico]").val(data.CodigoServico);
                $("[name=FormaPagamento]").val(data.CodTipoPagamento);
                $("[name=ValorServico]").val(FormatDecimal(data.ValorDevido));
                $("[name=DataVencimento]").val(moment(data.DataVencimento).format("DD/MM/YYYY"));
                $("[name=DataPagamento]").val(moment().format("DD/MM/YYYY"));
                $("[name=ValorPago]").val(FormatDecimal(data.ValorDevido));

                $("[name=CodClienteIncServico]").prop("disabled", true);
                $("[name=CodEspecialidadeIncServico]").prop("disabled", true);
                $("[name=CodProfissionalIncServico]").prop("disabled", true);
                $("[name=CodServicoIncServico]").prop("disabled", true);

                $("#modalIncluirServico").modal('show');
            }
        });
    }

}

var ModalAgendarPagto = function () {

    this.inicializar = function () {

    }

    this.abrirModal = function (idCliente) {

        modalAgendarPagto.limparCamposAgendarPagamento();

        $("[name=CodClienteAgendarPagto]").prop("disabled", false);

        if (idCliente != 0) {
            $("[name=CodClienteAgendarPagto]").val(idCliente);
            $("[name=CodClienteAgendarPagto]").prop("disabled", true);
        }

        $("#modalAgendarPagamento").modal('show');
    }

    this.limparCamposAgendarPagamento = function () {
        $("[name=CodClienteAgendarPagto]").val("");
        $("[name=CodEspecialidadeAgendarPagto]").val("");
        $("[name=CodProfissionalAgendarPagto]").val("");
        $("[name=CodServicoAgendarPagto]").val("");
        $("[name=PrimeiroVenctoAgendarPagto]").val("");
        $("[name=DiaVenctoAgendarPagto]").val("");
        $("[name=ValorPagoAgendarPagto]").val("");
    }

    this.agendarPagamento = function () {
        var codCliente = parseInt($("#CodigoClienteRecebimento").val());
        modalAgendarPagto.abrirModal(codCliente);
    }

    this.recuperarCombosDependentes = function () {
        $("[name=CodServicoAgendarPagto]").val("");
        $("[name=CodProfissionalAgendarPagto]").val("");
        this.recuperarProfissional();
        this.recuperarServicos();
    }

    this.recuperarProfissional = function () {
        var codEspecialidade = $("[name=CodEspecialidadeAgendarPagto]").val();

        if (codEspecialidade != undefined) {
            $.ajax({
                type: "GET",
                url: "Agenda/RecuperarProfissionalPorEspecialidade?codEspecialidade=" + codEspecialidade,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    $("[name=CodProfissionalAgendarPagto]").prop("disabled", false);
                    $('[name=CodProfissionalAgendarPagto]').find('option').remove();
                    var options = $("[name=CodProfissionalAgendarPagto]");
                    options.append($("<option />").val("").text(""));
                    $.each(data, function () {
                        options.append($("<option />").val(this.Value).text(this.Text));
                    });
                }
            });
        }
    }

    this.recuperarServicos = function () {
        var codEspecialidade = $("[name=CodEspecialidadeAgendarPagto]").val();

        if (codEspecialidade != undefined) {
            $.ajax({
                type: "GET",
                url: "Agenda/RecuperarServicosPorEspecialidade?codEspecialidade=" + codEspecialidade,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    $("[name=CodServicoAgendarPagto]").prop("disabled", false);
                    $('[name=CodServicoAgendarPagto]').find('option').remove();
                    var options = $("[name=CodServicoAgendarPagto]");
                    options.append($("<option />").val("").text(""));
                    $.each(data, function () {
                        options.append($("<option />").val(this.Value).text(this.Text));
                    });
                }
            });
        }
    }

    this.recuperarInfoServico = function () {
        var codigoServico = $("[name=CodServicoAgendarPagto]").val();

        if (codigoServico != undefined) {
            $.ajax({
                type: "GET",
                url: "Financeiro/RecuperarInfoServico?codigoServico=" + codigoServico,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    $("[name=ValorPagoAgendarPagto]").val(FormatDecimal(data.ValorServico));
                    $("[name=QtdeMesesAgendarPagto]").val(data.Periodicidade);
                }
            });
        }
    }

    this.realizarAgendamentos = function () {
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

        var agendamento = {
            CodigoCliente: $("[name=CodClienteAgendarPagto]").val(),
            CodigoEspecialidade: $("[name=CodEspecialidadeAgendarPagto]").val(),
            CodigoProfissional: $("[name=CodProfissionalAgendarPagto]").val(),
            CodigoServico: $("[name=CodServicoAgendarPagto]").val(),
            QtdeParcelas: $("[name=QtdeMesesAgendarPagto]").val(),
            PrimeiroVencimento: $("[name=PrimeiroVenctoAgendarPagto]").val(),
            DiaVencimento: $("[name=DiaVenctoAgendarPagto]").val(),
            ValorParcela: $("[name=ValorPagoAgendarPagto]").val()
        }

        $.ajax({
            type: "POST",
            url: "/Financeiro/RealizarAgendamentoParcelas",
            data: JSON.stringify({ agendaPagto: agendamento }),
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                financeiro.recuperarDadosRecebimento();
                $("#modalAgendarPagamento").modal('hide');
            }
        });
    }

}