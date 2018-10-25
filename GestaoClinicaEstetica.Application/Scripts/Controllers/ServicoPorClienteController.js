$(document).ready(function () {
    InitializeSelect2("CodCliente");

    InitDataTable("ServicosPorClienteLista");

    servicoPorCliente = new ServicoPorCliente();
    modalService = new ModalService();

    ModalService.inicializa();
})


var ServicoPorCliente = function () {

}

var ModalService = function () {

    this.inicializa = function () {
        $('#ModalServico').modal({
            keyboard: false
        })
    }

    this.openModal = function () {
        $("#ModalServico").modal('show');
    }
}