using GestaoClinicaEstetica.Application.Controllers.Base;
using GestaoClinicaEstetica.Domain.Dto;
using GestaoClinicaEstetica.Domain.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestaoClinicaEstetica.Application.Controllers
{
    public class AtendimentoController : BaseController
    {
        private readonly IClienteService _clienteService;
        private readonly IAgendaService _agendaService;

        public AtendimentoController(IClienteService clienteService, IAgendaService agendaService)
        {
            _clienteService = clienteService;
            _agendaService = agendaService;
        }

        // GET: Atendimento
        public ActionResult Index()
        {
            UpdateBag();
            return View();
        }

        [HttpPost]
        public void SalvarAtendimento(SalvarAtendimentoDto atendimento)
        {
            var agenda = _agendaService.GetById(atendimento.CodigoAgenda);

            agenda.DataAlteracao = DateTime.Now;
            agenda.UsuarioAlteracao = ViewBag.UsuarioLogin;
            
            agenda.ObsAtendimento = atendimento.ObsAtendimento;

            _agendaService.Update(agenda);
        }

        [HttpGet]
        public JsonResult RecuperarAtendimentosPorCliente(int codigoCliente)
        {
            List<DadosAtendimentoDto> presencas = _agendaService
                .List()
                .Where(x => x.CodigoCliente == codigoCliente)
                .Select(y => new DadosAtendimentoDto()
                {
                    CodigoCliente = y.CodigoCliente,
                    CodigoEspecialidade = y.CodigoEspecialidade,
                    CodigoProfissional = y.CodigoProfissional,
                    DataFimEvento = y.DataFim,
                    DataInicioEvento = y.DataInicio,
                    DescricaoEspecialidade = y.Especialidade.Descricao,
                    IdAgenda = y.Id,
                    Atendimento = y.ObsAtendimento,
                    NomeCliente = y.Cliente.Nome,
                    NomeProfissional = y.Profissional.Nome
                }).ToList();

            return Json(presencas, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult RecuperarAtendimentoPorCodAgenda(int codigoAgenda)
        {
            DadosAtendimentoDto presencas = _agendaService
                .List()
                .Where(x => x.Id == codigoAgenda)
                .Select(y => new DadosAtendimentoDto()
                {
                    CodigoCliente = y.CodigoCliente,
                    CodigoEspecialidade = y.CodigoEspecialidade,
                    CodigoProfissional = y.CodigoProfissional,
                    DataFimEvento = y.DataFim,
                    DataInicioEvento = y.DataInicio,
                    DescricaoEspecialidade = y.Especialidade.Descricao,
                    IdAgenda = y.Id,
                    Atendimento = y.ObsAtendimento,
                    NomeCliente = y.Cliente.Nome,
                    NomeProfissional = y.Profissional.Nome
                })
                .FirstOrDefault();

            return Json(presencas, JsonRequestBehavior.AllowGet);
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