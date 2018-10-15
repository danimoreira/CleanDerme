$(document).ready(function () {
    InitDataTable("ClientesLista");
})

Cliente = function () {
    this.salvar = function () {

        var cliente = {
            nome: $("[name=Nome]").val(),
            cpf: $("[name=Cpf]").val(),
            dataNascimento: $("[name=DataNascimento]").val(),
            endereco: $("[name=Endereco]").val(),
            bairro: $("[name=Bairro]").val(),
            cidade: $("[name=Cidade]").val(),
            uf: $("[name=Uf]").val(),
            cep: $("[name=Cep]").val(),
            telefoneFixo: $("[name=TelefoneFixo]").val(),
            telefoneCelular: $("[name=TelefoneCelular]").val(),
            email: $("[name=Email]").val(),
            id: $("[name=]").val(),
            dataCadastro: $("[name=DataCadastro]").val(),
            usuarioCadastro: $("[name=UsuarioCadastro]").val(),
            dataAlteracao: $("[name=DataAlteracao]").val(),
            usuarioAlteracao: $("[name=UsuarioAlteracao]").val()
        }

        $.ajax({
            type: "POST",
            data: cliente,
            success: success,
            dataType: dataType
        });
    }
}