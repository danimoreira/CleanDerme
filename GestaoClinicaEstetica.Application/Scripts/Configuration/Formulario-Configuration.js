$(document).ready(function () {
    $('.date').mask('00/00/0000');
    $('.time').mask('00:00');
    $('.date_time').mask('00/00/0000 00:00:00');
    $('.cep').mask('00000-000');
    $('.phone-fixo').mask('(00) 0000-0000');
    $('.phone-cel').mask('(00) 00000-0000');
    $('.cpf').mask('000.000.000-00', { reverse: true });
    $('.cnpj').mask('00.000.000/0000-00', { reverse: true });
    $('.money2').mask("#.##0,00", { reverse: true });
})

function SalvarFormulario() {
    var formulario = $(".formulario-conteudo").find("form");
    formulario.submit();
}

function ConfirmarExclusao() {
    var formulario = $(".formulario-conteudo-exclusao").find("form");
    formulario.submit();
}