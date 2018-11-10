using GestaoClinicaEstetica.Application.Controllers.Base;
using GestaoClinicaEstetica.Domain.Dto;
using GestaoClinicaEstetica.Domain.Entidades;
using GestaoClinicaEstetica.Domain.Enums;
using GestaoClinicaEstetica.Domain.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestaoClinicaEstetica.Application.Controllers
{
    public class AgendaController : BaseController
    {
        private readonly IClienteService _clienteService;
        private readonly IEspecialidadeService _especialidadeService;
        private readonly IProfissionalService _profissionalService;
        private readonly IServicoService _servicoService;
        private readonly IAgendaService _agendaService;
        private readonly IRecebimentoServicoPorClienteService _recebimentoService;

        public AgendaController(IClienteService clienteService, IEspecialidadeService especialidadeService, IProfissionalService profissionalService, IServicoService servicoService, IAgendaService agendaService, IRecebimentoServicoPorClienteService recebimentoService)
        {
            _clienteService = clienteService;
            _especialidadeService = especialidadeService;
            _profissionalService = profissionalService;
            _servicoService = servicoService;
            _agendaService = agendaService;
            _recebimentoService = recebimentoService;
        }

        // GET: Agenda
        public ActionResult Index()
        {
            UpdateBag();

            return View();
        }

        [HttpGet]
        [Route("Agenda/RecuperarProfissionalPorEspecialidade/{codEspecialidade}")]
        public JsonResult RecuperarProfissionalPorEspecialidade(int codEspecialidade)
        {
            var profissionais = _profissionalService.List().Where(x => x.EspecialidadePorProfissional.Where(y => y.CodEspecialidade == codEspecialidade).Count() > 0).ToList();

            var listaProfissionais = profissionais.Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Nome
            }).Select(y => new { y.Value, y.Text });

            return Json(listaProfissionais, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("Agenda/RecuperarServicosPorEspecialidade/{codEspecialidade}")]
        public JsonResult RecuperarServicosPorEspecialidade(int codEspecialidade)
        {
            var servicos = _servicoService.List().Where(x => x.CodigoEspecialidade == codEspecialidade).ToList();

            var listaServicos = servicos.Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Descricao
            }).Select(y => new { y.Value, y.Text });

            return Json(listaServicos, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void ExcluirEvento(int idEvento)
        {
            if(idEvento != 0)
            {
                _agendaService.Delete(idEvento);
            }
        }

        [HttpPost]
        public void RealizarAgendamento(Agenda objeto)
        {
            if (objeto.Id.Equals(0))
            {
                objeto.DataCadastro = DateTime.Now;
                objeto.UsuarioCadastro = ViewBag.UsuarioLogin;
                objeto.DataAlteracao = DateTime.Now;
                objeto.UsuarioAlteracao = ViewBag.UsuarioLogin;

                _agendaService.Add(objeto);
            }
            else
            {
                var eventoAntigo = _agendaService.GetById(objeto.Id);
                eventoAntigo.DataAlteracao = DateTime.Now;
                eventoAntigo.UsuarioAlteracao = ViewBag.UsuarioLogin;
                eventoAntigo.CodigoCliente = objeto.CodigoCliente;
                eventoAntigo.CodigoEspecialidade = objeto.CodigoEspecialidade;
                eventoAntigo.CodigoProfissional = objeto.CodigoProfissional;
                eventoAntigo.DataInicio = objeto.DataInicio;
                eventoAntigo.DataFim = objeto.DataFim;
                eventoAntigo.Procedimento = objeto.Procedimento;

                _agendaService.Update(eventoAntigo);
            }
        }

        [HttpGet]
        [Route("Agenda/RecuperarEventosAgenda")]
        public JsonResult RecuperarEventosAgenda()
        {
            var eventos = RecuperarEventos();

            return Json(eventos, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult RecuperarEventoAgenda(int id)
        {
            var evento = _agendaService.GetById(id);

            AgendaDto agendaDto = new AgendaDto
            {
                Id = evento.Id,
                CodigoCliente = evento.CodigoCliente,
                CodigoEspecialidade = evento.CodigoEspecialidade,
                CodigoProfissional = evento.CodigoProfissional,
                DataInicioEvento = evento.DataInicio,
                DataFimEvento = evento.DataFim,
                DescricaoEspecialidade = evento.Especialidade.Descricao,
                NomeCliente = evento.Cliente.Nome,
                NomeProfissional = evento.Profissional.Nome,
                Procedimento = evento.Procedimento
            };

            return Json(agendaDto, JsonRequestBehavior.AllowGet);
        }

        public bool VerificarExistenciaCompromisso(int codigoCliente, int codigoProfissional, DateTime dataInicio, DateTime dataFim)
        {
            bool existeCompromisso = _agendaService.List().Where(x => !x.SituacaoPresenca.Equals(SituacaoPresenca.Falta) && x.CodigoCliente.Equals(codigoCliente) && ((x.DataInicio >= dataInicio && x.DataInicio <= dataFim) || (x.DataFim >= dataInicio && x.DataFim <= dataFim))).Count() > 0;

            if (!existeCompromisso)
                existeCompromisso = _agendaService.List().Where(x => !x.SituacaoPresenca.Equals(SituacaoPresenca.Falta) && x.CodigoProfissional.Equals(codigoCliente) && ((x.DataInicio >= dataInicio && x.DataInicio <= dataFim) || (x.DataFim >= dataInicio && x.DataFim <= dataFim))).Count() > 0;

            return existeCompromisso;
        }

        public List<EventosDto> RecuperarEventos()
        {
            List<EventosDto> eventos = new List<EventosDto>();

            eventos.AddRange(_agendaService.List().Where(y => !y.SituacaoPresenca.Equals(SituacaoPresenca.Falta)).Select(x => new EventosDto
            {
                Title = _clienteService.GetById(x.CodigoCliente).Nome + "(" + _especialidadeService.GetById(x.CodigoEspecialidade).Descricao + ")",
                Start = x.DataInicio.ToString("yyyy-MM-dd hh:mm"),
                End = x.DataFim.ToString("yyyy-MM-dd hh:mm"),
                Id = x.Id,
                BackgroundColor = _especialidadeService.GetById(x.CodigoEspecialidade).CorEvento,
                TipoEvento = 0
            })
                                .ToList());

            eventos.AddRange(_clienteService.List().Select(x => new EventosDto
            {
                Title = x.Nome,
                Start = x.DataNascimento.Value.ToString(DateTime.Now.Year + "-MM-dd hh:mm"),
                AllDay = true,
                BackgroundColor = "#ffee05",
                Icone = "birthday-cake",
                TipoEvento = 1,
                CodigoCliente = x.Id
            })
                            .ToList());

            eventos.AddRange(_recebimentoService.List().Where(y => y.SituacaoPagamento.Equals(SituacaoPagamento.Pendente)).Select(x => new EventosDto
            {
                Title = _clienteService.GetById(x.CodigoCliente).Nome + "( R$ " + x.ValorDevido + ")",
                Start = x.DataVencimento.ToString("yyyy-MM-dd hh:mm"),
                AllDay = true,
                BackgroundColor = "#40c42f",
                Icone = "dollar-sign",
                TipoEvento = 2,
                CodigoRecebimento = x.Id
            })
                            .ToList());

            return eventos;
        }

        [HttpGet]
        public JsonResult RecuperarDadosAniversariante(int codigoCliente)
        {
            AniversarianteDto aniversariante = _clienteService.List().Where(x => x.Id.Equals(codigoCliente)).Select(x => new AniversarianteDto
                                                {
                                                    Id = x.Id,
                                                    Nome = x.Nome,
                                                    DataAniversario = x.DataNascimento,
                                                    TelefoneCelular = x.TelefoneCelular,
                                                    TelefoneFixo = x.TelefoneFixo
                                                }).FirstOrDefault();

            return Json(aniversariante, JsonRequestBehavior.AllowGet);
        }

        public override void UpdateBag()
        {
            base.UpdateBag();

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