function InitDataTable(idTable, columnsDefinition) {
    return $('#' + idTable).DataTable({
        destroy: true,
        language: {
            processing: "Aguarde! Recuperando as informações...",
            search: "Pesquisar",
            lengthMenu: "Exibir _MENU_ por página",
            info: "Exibindo de _START_ a _END_ de _TOTAL_",
            infoEmpty: "",
            infoFiltered: "(Encontrados _MAX_ com o filtro informado",
            infoPostFix: "",
            loadingRecords: "Recuperando os registros",
            zeroRecords: "Não existem registros cadastrados",
            emptyTable: "Não existem registros cadastrados",
            paginate: {
                first: "<i class='fa fa-step-backward fa-2x'></i>",
                previous: "<i class='fa fa-caret-left fa-2x'></i>",
                next: "<i class='fa fa-caret-right fa-2x'></i>",
                last: "<i class='fa fa-step-forward fa-2x'></i>"
            },
            aria: {
                sortAscending: ": activer pour trier la colonne par ordre croissant",
                sortDescending: ": activer pour trier la colonne par ordre décroissant"
            },
            columnDefs: columnsDefinition
        }
    });
}

function PesquisarDataTable(obj) {
    var txtSearch = $(obj).attr("data-nameInput");
    var idTable = $(obj).attr("data-idTable");
    var table = $('#' + idTable).DataTable();
    table.search($("[name=" + txtSearch + "]").val()).draw();
}

function LimparPesquisaDataTable(obj) {
    var txtSearch = $(obj).attr("data-nameInput");
    $("[name=" + txtSearch + "]").val("");
    table.search("").draw();
}