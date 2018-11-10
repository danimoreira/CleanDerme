$(document).ready(function () {
    relatorios = new Relatorios();

})

var Relatorios = function () {

    this.gerarFechamentoFinanceiro = function () {
        
        var dataInicio = moment($("[name=DtInicioRelatorio]").val()).format("YYYY-MM-DD");
        var dataFim = moment($("[name=DtFimRelatorio]").val()).format("YYYY-MM-DD");

        $.ajax({
            type: "GET",
            url: "/Relatorios/GerarFechamentoFinanceiro?dtInicio=" + dataInicio + "&dtFim=" + dataFim,
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                var arquivoPdf = data + ".pdf";
                window.open("/Relatorios/" + arquivoPdf, "_blank");
            }
        });
    }

    this.gerarRepasseProfissional = function ()
    {
            var codProfissional = $("#CodigoProfissionalRelatorio").val();
            var dataInicio = moment($("[name=DtInicioRelatorio]").val()).format("YYYY-MM-DD");
            var dataFim = moment($("[name=DtFimRelatorio]").val()).format("YYYY-MM-DD");

            $.ajax({
                type: "GET",
                url: "/Relatorios/GerarRepasseProfissional?codigoProfissional=" + codProfissional + "&dtInicio=" + dataInicio + "&dtFim=" + dataFim,
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    var arquivoPdf = data + ".pdf";
                    window.open("/Relatorios/" + arquivoPdf, "_blank");
                }
            });
    }

    this.gerarHistoricoCliente = function () {
        var codigoCliente = $("#CodigoClienteRelatorio").val();
        
        $.ajax({
            type: "GET",
            url: "/Relatorios/GerarHistoricoCliente?codigoCliente=" + codigoCliente,
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                var arquivoPdf = data + ".pdf";                
                window.open("/Relatorios/" + arquivoPdf, "_blank");
            }
        });
    }
}