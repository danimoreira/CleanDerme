using GestaoClinicaEstetica.Application.Controllers.Base;
using GestaoClinicaEstetica.Domain.Dto;
using GestaoClinicaEstetica.Domain.Entidades;
using GestaoClinicaEstetica.Domain.Interfaces.Service;
using GestaoClinicaEstetica.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestaoClinicaEstetica.Application.Controllers
{
    public class FinanceiroController : BaseController
    {
        private readonly IClienteService _clienteService;
        private readonly IEspecialidadeService _especialidadeService;
        private readonly IProfissionalService _profissionalService;
        private readonly IServicoService _servicoService;
        private readonly IRecebimentoServicoPorClienteService _recebimentoService;

        public FinanceiroController(IClienteService clienteService, IEspecialidadeService especialidadeService, IProfissionalService profissionalService, IServicoService servicoService, IRecebimentoServicoPorClienteService recebimentoService)
        {
            _clienteService = clienteService;
            _especialidadeService = especialidadeService;
            _profissionalService = profissionalService;
            _servicoService = servicoService;
            _recebimentoService = recebimentoService;
        }
        // GET: Financeiro
        public ActionResult Index()
        {
            UpdateBag();
            return View();
        }

        [HttpPost]
        public void ReceberPagamento(RecebimentoServicoPorCliente parcela)
        {
            parcela.UsuarioRecebimento = ViewBag.UsuarioLogin;
            parcela.DataAlteracao = DateTime.Now;
            parcela.UsuarioAlteracao = ViewBag.UsuarioLogin;

            if (parcela.Id.Equals(0))
            {
                parcela.DataAquisicao = DateTime.Now;
                parcela.DataCadastro = DateTime.Now;
                parcela.UsuarioCadastro = ViewBag.UsuarioLogin;
                _recebimentoService.Add(parcela);
            }
            else
            {
                _recebimentoService.Update(parcela);
            }
        }

        [HttpPost]
        public void RealizarAgendamentoParcelas(AgendarPagamentoDto agendaPagto)
        {
            RecebimentoServicoPorCliente recebimento = new RecebimentoServicoPorCliente
            {
                DataAquisicao = DateTime.Now,
                DataVencimento = agendaPagto.PrimeiroVencimento,
                CodigoEspecialidade = agendaPagto.CodigoEspecialidade,
                CodigoCliente = agendaPagto.CodigoCliente,
                CodigoProfissional = agendaPagto.CodigoProfissional,
                CodigoServico = agendaPagto.CodigoServico,
                ValorDevido = agendaPagto.ValorParcela,
                DataCadastro = DateTime.Now,
                UsuarioCadastro = ViewBag.UsuarioLogin
            };

            _recebimentoService.Add(recebimento);

            if (agendaPagto.QtdeParcelas > 1)
            {
                for (int i = 1; i < agendaPagto.QtdeParcelas; i++)
                {
                    var proxVencto = Convert.ToDateTime(agendaPagto.PrimeiroVencimento.AddMonths(i).Year + "-" + agendaPagto.PrimeiroVencimento.AddMonths(i).Month + "-" + agendaPagto.DiaVencimento.ToString());
                    recebimento = new RecebimentoServicoPorCliente
                    {
                        DataAquisicao = DateTime.Now,
                        DataVencimento = proxVencto,
                        CodigoEspecialidade = agendaPagto.CodigoEspecialidade,
                        CodigoCliente = agendaPagto.CodigoCliente,
                        CodigoProfissional = agendaPagto.CodigoProfissional,
                        CodigoServico = agendaPagto.CodigoServico,
                        ValorDevido = agendaPagto.ValorParcela,
                        DataCadastro = DateTime.Now,
                        UsuarioCadastro = ViewBag.UsuarioLogin
                    };
                    _recebimentoService.Add(recebimento);
                }
            }

        }

        public JsonResult RecuperarDadosRecebimentoCliente(int codigoCliente)
        {
            var recebimentos = _clienteService.GetById(codigoCliente)
                                .Recebimentos
                                .Select(x => new RecebimentoClienteDto
                                {
                                    IdRecebimento = x.Id,
                                    CodigoCliente = x.CodigoCliente,
                                    CodigoServico = x.CodigoServico,
                                    DataAquisicao = x.DataAquisicao,
                                    DataPagamento = x.DataPagamento,
                                    DataVencimento = x.DataVencimento,
                                    DescricaoServico = x.Servico.Descricao,
                                    SituacaoPagamento = FuncoesGerais.GetEnumDescription(x.SituacaoPagamento),
                                    TipoPagamento = FuncoesGerais.GetEnumDescription(x.TipoPagamento),
                                    UsuarioRecebimento = x.UsuarioRecebimento,
                                    ValorDevido = x.ValorDevido,
                                    ValorRecebido = x.ValorRecebido
                                })
                                .OrderByDescending(y => y.DataVencimento)
                                .ToList();

            return Json(recebimentos, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RecuperarDadosRecebimento(int codigoRecebimento)
        {
            var recebimento = _recebimentoService
                .List()
                .Where(y => y.Id.Equals(codigoRecebimento))
                .Select(x => new RecebimentoClienteDto
                {
                    IdRecebimento = x.Id,
                    CodigoCliente = x.CodigoCliente,
                    CodigoServico = x.CodigoServico,
                    CodigoEspecialidade = x.CodigoEspecialidade,
                    CodigoProfissional = x.CodigoProfissional,
                    DataAquisicao = x.DataAquisicao,
                    DataPagamento = x.DataPagamento,
                    DataVencimento = x.DataVencimento,
                    DescricaoServico = x.Servico.Descricao,
                    SituacaoPagamento = FuncoesGerais.GetEnumDescription(x.SituacaoPagamento),
                    TipoPagamento = FuncoesGerais.GetEnumDescription(x.TipoPagamento),
                    CodTipoPagamento = Convert.ToInt32(x.TipoPagamento),
                    UsuarioRecebimento = x.UsuarioRecebimento,
                    ValorDevido = x.ValorDevido,
                    ValorRecebido = x.ValorRecebido
                })
                .FirstOrDefault();

            return Json(recebimento, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RecuperarInfoServico(int codigoServico)
        {
            ServicoDto servicoSelecionado = _servicoService
                                            .List()
                                            .Where(x => x.Id == codigoServico)
                                            .Select(y => new ServicoDto()
                                            {
                                                Descricao = y.Descricao,
                                                DescricaoPeriodicidade = "",
                                                Periodicidade = y.Periodicidade,
                                                QuantidadeSessoes = y.QuantidadeSessoes,
                                                ValorServico = y.ValorServico
                                            }).FirstOrDefault();

            return Json(servicoSelecionado, JsonRequestBehavior.AllowGet);
            ;
        }

        [HttpPost]
        public void ExcluirPagamento(int codPagamento)
        {
            _recebimentoService.Delete(codPagamento);
            return;
        }

        public override void UpdateBag()
        {
            base.UpdateBag();

            ViewBag.ListaClientes = _clienteService.List().Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Nome
            }).OrderBy(y => y.Text).ToList();

            ViewBag.ListaEspecialidades = _especialidadeService.List().Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Descricao
            }).OrderBy(y => y.Text).ToList();

            ViewBag.ListaServicos = _servicoService.List().Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Descricao
            }).OrderBy(y => y.Text).ToList();

            ViewBag.ListaProfissionais = _profissionalService.List().Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Nome
            }).OrderBy(y => y.Text).ToList();
        }
    }
}