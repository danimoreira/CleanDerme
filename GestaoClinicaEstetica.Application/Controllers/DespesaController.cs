using GestaoClinicaEstetica.Application.Controllers.Base;
using GestaoClinicaEstetica.Application.Models;
using GestaoClinicaEstetica.Domain.Entidades;
using GestaoClinicaEstetica.Domain.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestaoClinicaEstetica.Application.Controllers
{
    public class DespesaController : BaseController
    {
        private readonly IDespesaService _despesaService;

        public DespesaController(IDespesaService despesaService)
        {
            _despesaService = despesaService;
        }

        // GET: Despesa
        public ActionResult Index()
        {
            UpdateBag();
            return View();
        }

        public override void UpdateBag()
        {
            base.UpdateBag();
        }

        public JsonResult RecuperarDespesas()
        {
            List<Despesa> listaDespesas = new List<Despesa>();
            listaDespesas = _despesaService.List().OrderBy(x => x.DataVencimento).ToList();
            return Json(listaDespesas, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RecuperarDadosDespesa(int idDespesa)
        {
            Despesa dadosDespesa = _despesaService.GetById(idDespesa);
            return Json(dadosDespesa, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void AdicionarDespesas(AdicionarDespesaModel dadosDespesa)
        {
            this.UpdateBag();

            Despesa objDespesa = new Despesa();

            int contDespesa = dadosDespesa.Periodicidade.Equals("U") ? 1 : dadosDespesa.QtdeMeses;

            DateTime proxVenc = DateTime.Today;

            for (int i = 0; i < contDespesa; i++)
            {
                if (i.Equals(0))
                    proxVenc = dadosDespesa.DataPrimeiroVencto;
                else
                    proxVenc = Convert.ToDateTime(dadosDespesa.DataPrimeiroVencto.AddMonths(i).Year + "-" + dadosDespesa.DataPrimeiroVencto.AddMonths(i).Month + "-" + dadosDespesa.DiaVencimento.ToString());

                objDespesa = new Despesa
                {
                    Descricao = dadosDespesa.Descricao,
                    Fornecedor = dadosDespesa.Fornecedor,
                    Observacao = dadosDespesa.Observacao,
                    TipoPagamento = dadosDespesa.TipoPagamento,
                    ValorDespesa = dadosDespesa.ValorDespesa,

                    Situacao = Domain.Enums.SituacaoDespesa.EmAberto,
                    UsuarioCadastro = ViewBag.UsuarioLogin,
                    DataCadastro = DateTime.Now,

                    DataVencimento = proxVenc
                };

                _despesaService.Add(objDespesa);
            }
        }

        [HttpPost]
        public void PagarDespesa(Despesa objDespesa)
        {
            this.UpdateBag();

            Despesa obj = _despesaService.GetById(objDespesa.Id);

            obj.ValorPagamento = objDespesa.ValorPagamento;
            obj.DataPagamento = objDespesa.DataPagamento;
            obj.Observacao = objDespesa.Observacao;
            obj.DataAlteracao = DateTime.Now;
            obj.UsuarioAlteracao = ViewBag.UsuarioLogin;
            obj.Situacao = Domain.Enums.SituacaoDespesa.Liquidado;

            _despesaService.Update(obj);
        }

        [HttpPost]
        public void ExcluirDespesa(int idDespesa)
        {
            if (idDespesa.Equals(0))
                return;

            Despesa obj = _despesaService.GetById(idDespesa);

            _despesaService.Delete(obj);
        }
    }
}