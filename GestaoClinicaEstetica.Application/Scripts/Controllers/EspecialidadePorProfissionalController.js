$(document).ready(function () {
    InitializeSelect2("#CodProfissional");

    especialidades = new Especialidades();
})

var Especialidades = function () {

    this.PesquisarEspecialidades = function () {

        $("#ItemEspecialidade").html("");


        var codProfissional = $("[name=CodProfissional]").val();

        $.ajax({
            type: "GET",
            url: "/EspecialidadePorProfissional/RecuperarEspecialidadesDoProfissional?codProfissional=" + codProfissional,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                $("#groupEspecialidades").removeClass("hidden");
                var itemEspecialidade = $("#ItemEspecialidade").html();

                for (item in data)
                {               
                    var selecionado = data[item].Selecionado ? "checked" : "";
                    var strHtml = '<div class="col-xs-3 col-md-3 col-sm-3"> <input type="checkbox" name="chkEspecialidadeSelecionada" data-id="' + data[item].IdObjeto + '" data-idEspecialidade="' + data[item].CodEspecialidade + '" ' + selecionado + ' /><span class="control-label" name="dscEspecialidade" data-idEspecialidade="' + data[item].CodEspecialidade + '">' + data[item].DescricaoEspecialidade + '</span> </div>';                    
                    $("#ItemEspecialidade").append(strHtml);                    
                }                
            }
        });
    }

    this.Salvar = function () {

        var objetoEnvio = [];
        
        $('[name=chkEspecialidadeSelecionada]').each(function (i, obj) {
            var item = {
                IdObjeto: $(obj).data("id"),
                CodProfissional: $("[name=CodProfissional]").val(),
                CodEspecialidade: $(obj).attr("data-idEspecialidade"),
                DescricaoEspecialidade: "",
                Selecionado: $(obj).is(":checked")
            }

            objetoEnvio.push(item);

        });

        $.ajax({
            type: "POST",
            url: "/EspecialidadePorProfissional/SalvarEspecialidades",
            data: JSON.stringify({ objetos: objetoEnvio }),           
            contentType: "application/json; charset=utf-8"
        });

    }

}