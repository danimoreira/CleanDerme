using GestaoClinicaEstetica.Application.Controllers.Base;
using GestaoClinicaEstetica.Domain.Dto;
using GestaoClinicaEstetica.Domain.Enums;
using GestaoClinicaEstetica.Domain.Interfaces.Service;
using GestaoClinicaEstetica.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestaoClinicaEstetica.Application.Controllers
{
    public class PresencaController : BaseController
    {
        private readonly IClienteService _clienteService;
        private readonly IAgendaService _agendaService;

        public PresencaController(IClienteService clienteService, IAgendaService agendaService)
        {
            _clienteService = clienteService;
            _agendaService = agendaService;
        }

        // GET: Presenca
        public ActionResult Index()
        {
            UpdateBag();

            return View();
        }

        [HttpPost]
        public void SalvarPresenca(SalvarPresencaDto presenca)
        {
            var agenda = _agendaService.GetById(presenca.CodigoAgenda);

            agenda.DataAlteracao = DateTime.Now;
            agenda.UsuarioAlteracao = ViewBag.UsuarioLogin;

            agenda.SituacaoPresenca = (SituacaoPresenca)presenca.SituacaoPresenca;
            agenda.ObservacaoPresenca = presenca.Justificativa;

            _agendaService.Update(agenda);
        }

        [HttpGet]
        public JsonResult RecuperarPresencasPorCliente(int codigoCliente)
        {
            List<DadosPresencaDto> presencas = _agendaService
                .List()
                .Where(x => x.CodigoCliente == codigoCliente)
                .Select(y => new DadosPresencaDto()
                {
                    CodigoCliente = y.CodigoCliente,
                    CodigoEspecialidade = y.CodigoEspecialidade,
                    CodigoProfissional = y.CodigoProfissional,
                    CodSituacaoPresenca = (Int32)y.SituacaoPresenca,
                    DataFimEvento = y.DataFim,
                    DataInicioEvento = y.DataInicio,
                    DescricaoEspecialidade = y.Especialidade.Descricao,
                    IdAgenda = y.Id,
                    Justificativa = y.ObservacaoPresenca,
                    NomeCliente = y.Cliente.Nome,
                    NomeProfissional = y.Profissional.Nome,
                    SituacaoPresenca = FuncoesGerais.GetEnumDescription(y.SituacaoPresenca)
                }).ToList();

            return Json(presencas, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult RecuperarPresencaPorCodAgenda(int codigoAgenda)
        {
            DadosPresencaDto presencas = _agendaService
                .List()
                .Where(x => x.Id == codigoAgenda)
                .Select(y => new DadosPresencaDto()
                {
                    CodigoCliente = y.CodigoCliente,
                    CodigoEspecialidade = y.CodigoEspecialidade,
                    CodigoProfissional = y.CodigoProfissional,
                    CodSituacaoPresenca = (Int32)y.SituacaoPresenca,
                    DataFimEvento = y.DataFim,
                    DataInicioEvento = y.DataInicio,
                    DescricaoEspecialidade = y.Especialidade.Descricao,
                    IdAgenda = y.Id,
                    Justificativa = y.ObservacaoPresenca,
                    NomeCliente = y.Cliente.Nome,
                    NomeProfissional = y.Profissional.Nome,
                    SituacaoPresenca = FuncoesGerais.GetEnumDescription(y.SituacaoPresenca)
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
            }).OrderBy(y => y.Text).ToList();
        }
    }
}