$(document).ready(function () {

    despesa = new Despesa();
    modalDespesa = new ModalDespesa();
    modalPagarDespesa = new ModalPagarDespesa();

    despesa.inicializar();
    modalDespesa.inicializar();
    modalPagarDespesa.inicializar();
})

var Despesa = function () {

    this.inicializar = function () {
        despesa.inicializarDataTable();

        despesa.recuperarDadosDespesa();
    }

    this.inicializarDataTable = function () {

        var definicao = [
            { "targets": 0, "name": "Descrição" },
            { "targets": 1, "name": "DtVenc" },
            { "targets": 2, "name": "VlrDevido" },
            { "targets": 3, "name": "Situacao" },
            { "targets": 4, "name": "DtPagto" },
            { "targets": 5, "name": "BotoesAcao" },
            { "targets": 6, "name": "IdDespesa", "class": "hidden" }
        ]

        InitDataTable("DespesasClinica", definicao);
    }

    this.recuperarDadosDespesa = function () {
        $.ajax({
            type: "GET",
            url: "/Despesa/RecuperarDespesas",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                var tableDespesa = $('#DespesasClinica').DataTable();

                tableDespesa.clear().draw();

                if (data.length != 0) {
                    $.each(data, function (index, linha) {

                        tableDespesa.row.add([
                            linha.Descricao,
                            moment(linha.DataVencimento).format("DD/MM/YYYY"),
                            "R$ " + FormatDecimal(linha.ValorDespesa),
                            linha.Situacao == "0" ? "Em aberto" : "Pago",
                            linha.Situacao == "0" ? " - " : moment(linha.DataPagamento).format("DD/MM/YYYY"),
                            "<a class='btn-link icon-acao icon-acao-editar' onclick='modalPagarDespesa.recuperarDadosPagarDespesa(" + linha.Id + ")' data-toggle='tooltip' data-placement='right' title='Pagar despesa'><i class='fa fa-credit-card'></i></a> <a class='btn-link icon-acao icon-acao-deletar' onclick='modalPagarDespesa.excluirDespesa(" + linha.Id + ")' data-toggle='tooltip' data-placement='right' title='Excluir despesa'><i class='fa fa-trash'></i></a>",
                            linha.Id
                        ]);

                    });

                    tableDespesa.draw();
                }

                tableDespesa.column(6).visible(false);
            }
        });
    }

}

var ModalDespesa = function () {

    this.inicializar = function () {

        $("input[name=Periodicidade]").on("click", function () {
            var idtPeriodo = $(this).val();

            if (idtPeriodo === 'R') {
                $(".clsPeriodoRecorrente").removeClass("hidden");
            }
            else {
                $(".clsPeriodoRecorrente").addClass("hidden");
            }
        })

    }

    this.abrirModal = function () {
        modalDespesa.limparCampos();

        $("#modalDespesas").modal('show');
    }

    this.limparCampos = function () {
        $("[name=Descricao]").val("");
        $("[name=Fornecedor]").val("");
        $("[name=QtdeMesesAgendarPagto]").val("");
        $("[name=DiaVenctoAgendarPagto]").val("");
        $("[name=PrimeiroVenctoAgendarPagto]").val("");
        $("[name=ValorDespesa]").val("");
        $("[name=TipoPagamento]").val("");
        $("[name=Observacao]").val("");
        $("input[name=Periodicidade][value='U']").prop('checked', true);
        $("input[name=Periodicidade][value='R']").prop('checked', false);

        $(".clsPeriodoRecorrente").addClass("hidden");
    }

    this.adicionarDespesa = function () {

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

        var dadosDespesa = {
            Descricao: $("[name=Descricao]").val(),
            Fornecedor: $("[name=Fornecedor]").val(),
            Periodicidade: $("[name=Periodicidade]:checked").val(),
            QtdeMeses: $("[name=QtdeMesesAgendarPagto]").val(),
            DiaVencimento: $("[name=DiaVenctoAgendarPagto]").val(),
            DataPrimeiroVencto: $("[name=PrimeiroVenctoAgendarPagto]").val(),
            ValorDespesa: $("[name=ValorDespesa]").val(),
            TipoPagamento: $("[name=TipoPagamento]").val(),
            Observacao: $("[name=Observacao]").val()
        }

        $.ajax({
            type: "POST",
            url: "/Despesa/AdicionarDespesas",
            data: JSON.stringify({ dadosDespesa: dadosDespesa }),
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                despesa.recuperarDadosDespesa();
                $("#modalDespesas").modal('hide');
            }
        });
    }
}

var ModalPagarDespesa = function () {

    this.inicializar = function () {

    }

    this.abrirModal = function () {
        $("#modalLiquidarDespesa").modal('show');
    }

    this.limparCampos = function () {
        $("#IdDespesa").val("");
        $("#DespesaDescricao").text("");
        $("#DespesaFornecedor").text("");
        $("#DespesaValor").text("");
        $("#DespesaTipoPagamento").text("");
        $("#DespesaDataVencimento").text("");

        $("[name=DataPagamento]").val("");
        $("[name=ValorPago]").val("");
        $("[name=ObservacaoLiqDespesa]").val("");
    }

    this.recuperarDadosPagarDespesa = function (idDespesa) {

        modalPagarDespesa.limparCampos();

        $.ajax({
            type: "GET",
            url: "/Despesa/RecuperarDadosDespesa?idDespesa=" + idDespesa,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                $("[name=IdDespesa]").val(data.Id);
                $("#DespesaDescricao").text(data.Descricao);
                $("#DespesaFornecedor").text(data.Fornecedor);
                $("#DespesaValor").text("R$ " + FormatDecimal(data.ValorDespesa));
                $("#DespesaDataVencimento").text(moment(data.DataVencimento).format("DD/MM/YYYY"));
                $("#DespesaTipoPagamento").text(data.DscTipoPagamento);

                if (data.DataPagamento == null) {
                    $("[name=DataPagamento]").val(moment().format("DD/MM/YYYY"));
                    $("[name=ValorPago]").val(FormatDecimal(data.ValorDespesa));
                }
                else {
                    $("[name=DataPagamento]").val(moment(data.DataPagamento).format("DD/MM/YYYY"));
                    $("[name=ValorPago]").val(FormatDecimal(data.ValorPagamento));
                }
                
                $("[name=ObservacaoLiqDespesa]").val(data.Observacao);

                $("#modalLiquidarDespesa").modal('show');

            }
        })
    }

    this.excluirDespesa = function (idDespesa) {
        var respostaConfirmacao = confirm("Deseja excluir esta despesa?");

        if (respostaConfirmacao != true)
            return;

        $.ajax({
            type: "POST",
            url: "/Despesa/ExcluirDespesa",
            data: JSON.stringify({ idDespesa: idDespesa }),
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                despesa.recuperarDadosDespesa();

                toastr.success("Despesa excluída com sucesso!");
            }
        });
    }

    this.pagarDespesa = function () {
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
        
        var dadosDespesa = {
            Id: $("#modalLiquidarDespesa [name=IdDespesa]").val(),
            DataPagamento: $("#modalLiquidarDespesa [name=DataPagamento]").val(),
            ValorPagamento: $("#modalLiquidarDespesa [name=ValorPago]").val(),
            Observacao: $("#modalLiquidarDespesa [name=ObservacaoLiqDespesa]").val()
        }

        $.ajax({
            type: "POST",
            url: "/Despesa/PagarDespesa",
            data: JSON.stringify({ objDespesa: dadosDespesa }),
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                despesa.recuperarDadosDespesa();
                $("#modalLiquidarDespesa").modal('hide');
            }
        });
    }

}