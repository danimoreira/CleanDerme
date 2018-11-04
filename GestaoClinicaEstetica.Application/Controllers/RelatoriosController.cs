using GestaoClinicaEstetica.Application.Controllers.Base;
using GestaoClinicaEstetica.Domain.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestaoClinicaEstetica.Application.Controllers
{
    public class RelatoriosController : BaseController
    {
        private readonly IClienteService _clienteService;

        public RelatoriosController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        // GET: Relatorios/HistoricoCliente
        public ActionResult HistoricoCliente()
        {
            UpdateBag();
            return View();
        }

        public ActionResult FechamentoFinanceiro()
        {
            return View();
        }

        public ActionResult RepasseProfissional()
        {
            return View();
        }

        public override void UpdateBag()
        {
            base.UpdateBag();

            ViewBag.ListaClientes = _clienteService.List().Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Nome
            });
        }
    }
}