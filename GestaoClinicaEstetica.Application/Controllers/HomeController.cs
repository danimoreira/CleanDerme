using GestaoClinicaEstetica.Application.Controllers.Base;
using GestaoClinicaEstetica.Domain.Enums;
using GestaoClinicaEstetica.Domain.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestaoClinicaEstetica.Application.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IClienteService _clienteService;
        private readonly IEspecialidadeService _especialidadeService;
        private readonly IProfissionalService _profissionalService;
        private readonly IServicoService _servicoService;
        private readonly IAgendaService _agendaService;
        private readonly IRecebimentoServicoPorClienteService _recebimentoService;

        public HomeController(IClienteService clienteService, IEspecialidadeService especialidadeService, IProfissionalService profissionalService, IServicoService servicoService, IAgendaService agendaService, IRecebimentoServicoPorClienteService recebimentoService)
        {
            _clienteService = clienteService;
            _especialidadeService = especialidadeService;
            _profissionalService = profissionalService;
            _servicoService = servicoService;
            _agendaService = agendaService;
            _recebimentoService = recebimentoService;
        }
        public ActionResult Index()
        {
            this.UpdateBag();
            if (string.IsNullOrEmpty(ViewBag.UsuarioLogin))
                return RedirectToAction("Index", "Seguranca");
            
            return View();
        }

        public override void UpdateBag()
        {
            base.UpdateBag();

            ViewBag.QtdeClientesAtivos = _clienteService.List().Count().ToString();
            ViewBag.QtdeConsultasHoje = _agendaService.List().Where(x => x.DataInicio.Date.Equals(DateTime.Now.Date)).Count().ToString();
            ViewBag.VlrReceberHoje = _recebimentoService.List().Where(x => x.DataVencimento.Date.Equals(DateTime.Now.Date)).Sum(y => y.ValorDevido).ToString();

            ViewBag.ListaAgendaHoje = _agendaService.List().Where(x => x.DataInicio.Date.Equals(DateTime.Now.Date) && x.SituacaoPresenca.Equals(SituacaoPresenca.Pendente)).ToList();
            ViewBag.ListaRecebimentoHoje = _recebimentoService.List().Where(x => x.DataVencimento.Date.Equals(DateTime.Now.Date) && x.SituacaoPagamento.Equals(SituacaoPagamento.Pendente)).ToList();

            ViewBag.ListaClientes = _clienteService.List().Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Nome
            });

            ViewBag.ListaEspecialidades = _especialidadeService.List().Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Descricao
            });

            ViewBag.ListaServicos = _servicoService.List().Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Descricao
            });

            ViewBag.ListaProfissionais = _profissionalService.List().Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Nome
            });
        }
    }
}