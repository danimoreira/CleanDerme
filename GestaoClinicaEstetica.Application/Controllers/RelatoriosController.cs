using GestaoClinicaEstetica.Application.Controllers.Base;
using GestaoClinicaEstetica.Domain.Entidades;
using GestaoClinicaEstetica.Domain.Interfaces.Service;
using GestaoClinicaEstetica.Util;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace GestaoClinicaEstetica.Application.Controllers
{
    public class RelatoriosController : BaseController
    {
        private readonly IClienteService _clienteService;
        private readonly IProfissionalService _profissionalService;
        private readonly IRecebimentoServicoPorClienteService _recebimento;

        public RelatoriosController(IClienteService clienteService, IProfissionalService profissionalService, IRecebimentoServicoPorClienteService recebimento)
        {
            _clienteService = clienteService;
            _profissionalService = profissionalService;
            _recebimento = recebimento;
        }

        // GET: Relatorios/HistoricoCliente
        public ActionResult HistoricoCliente()
        {
            UpdateBag();
            return View();
        }

        private Document InicializarDocumento(string caminhoCompleto)
        {
            Document doc = new Document(PageSize.A4);
            doc.SetMargins(10, 10, 10, 20);
            doc.AddCreationDate();

            string caminho = @"" + caminhoCompleto;
            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(caminho, FileMode.Create));

            return doc;
        }

        private Document EscreverCabeçalho(Document doc)
        {
            return doc;
        }
        
        [HttpGet]
        [Route("Relatorios/GerarHistoricoCliente")]
        public string GerarHistoricoCliente(int codigoCliente)
        {
            string dirArquivo = Server.MapPath("~/Relatorios");
            string nomeArquivo = "RelHistoricoCliente_" + DateTime.Now.ToString("ddMMyyyyhhmss");
            string caminhoCompleto = dirArquivo + "\\" + nomeArquivo + ".pdf";

            Document doc = InicializarDocumento(caminhoCompleto);
            
            doc.Open();

            doc = EscreverCabeçalho(doc);

            Cliente dadosCliente = _clienteService.GetById(codigoCliente);
            
            // Informações do Cliente
            doc.Add(new Phrase("Cliente:" + dadosCliente.Nome));
            doc.Add(new Phrase("Endereço:" + dadosCliente.Endereco));
            doc.Add(new Phrase("Bairro:" + dadosCliente.Bairro + " - Cidade: " + dadosCliente.Cidade));
            doc.Add(new Phrase("Telefone:" + dadosCliente.TelefoneFixo + " / " + dadosCliente.TelefoneCelular));
            doc.Add(new Phrase("Email:" + dadosCliente.Email));

            doc.Add(new Phrase(""));
            doc.Add(new Phrase("Consultas"));
            doc.Add(new Phrase(""));

            PdfPTable table = new PdfPTable(4);
            table.DefaultCell.Border = Rectangle.NO_BORDER;

            table.AddCell("Data");
            table.AddCell("Serviço");
            table.AddCell("Profissional");
            table.AddCell("Presença");
           
            foreach (var item in dadosCliente.Compromissos)
            {
                table.AddCell(item.DataInicio.ToString("dd/MM/yyyy"));
                table.AddCell(item.Servico.Descricao);
                table.AddCell(item.Profissional.Nome);
                table.AddCell(FuncoesGerais.GetEnumDescription(item.SituacaoPresenca));
            }

            doc.Add(table);

            doc.Add(new Phrase(""));
            doc.Add(new Phrase("Pagamentos"));
            doc.Add(new Phrase(""));

            PdfPTable tbPagamento = new PdfPTable(4);
            tbPagamento.DefaultCell.Border = Rectangle.NO_BORDER;

            tbPagamento.AddCell("Data");
            tbPagamento.AddCell("Serviço");
            tbPagamento.AddCell("Profissional");
            tbPagamento.AddCell("Vlr Pago");

            foreach (var item in dadosCliente.Recebimentos)
            {
                tbPagamento.AddCell(item.DataPagamento.ToString("dd/MM/yyyy"));
                tbPagamento.AddCell(item.Servico.Descricao);
                tbPagamento.AddCell(item.Profissional.Nome);
                tbPagamento.AddCell(item.ValorRecebido.ToString());
            }

            doc.Add(tbPagamento);

            doc.Close();

            return nomeArquivo;
        }

        public ActionResult FechamentoFinanceiro()
        {
            UpdateBag();
            return View();
        }

        [HttpGet]
        [Route("Relatorios/GerarFechamentoFinanceiro")]
        public string GerarFechamentoFinanceiro(DateTime dtInicio, DateTime dtFim)
        {
            PdfPTable tabela = new PdfPTable(3);

            string dirArquivo = Server.MapPath("~/Relatorios");
            string nomeArquivo = "RelFechamentoFinanceiro_" + DateTime.Now.ToString("ddMMyyyyhhmss");
            string caminhoCompleto = dirArquivo + "\\" + nomeArquivo + ".pdf";

            Document doc = InicializarDocumento(caminhoCompleto);
            
            doc.Open();

            doc = EscreverCabeçalho(doc);

            var recebimentos = _recebimento.List().Where(x => x.DataPagamento >= dtInicio && x.DataPagamento <= dtFim).ToList();

            PdfPTable table = new PdfPTable(4);
            table.DefaultCell.Border = Rectangle.NO_BORDER;

            table.AddCell("Dt Pagamento");
            table.AddCell("Serviço");
            table.AddCell("Valor Devido");
            table.AddCell("Valor Recebido");

            foreach (var item in recebimentos)
            {
                table.AddCell(item.DataPagamento.ToString("dd/MM/yyyy"));
                table.AddCell(item.Servico.Descricao);
                table.AddCell(item.ValorDevido.ToString());
                table.AddCell(item.ValorRecebido.ToString());
            }

            table.AddCell("");
            table.AddCell("Total");
            table.AddCell(recebimentos.Sum(x => x.ValorDevido).ToString());
            table.AddCell(recebimentos.Sum(x => x.ValorRecebido).ToString());

            doc.Add(table);

            doc.Close();

            return nomeArquivo;
        }

        public ActionResult RepasseProfissional()
        {
            UpdateBag();
            return View();
        }

        [HttpGet]
        [Route("Relatorios/GerarRepasseProfissional")]
        public string GerarRepasseProfissional(int codigoProfissional, DateTime dtInicio, DateTime dtFim)
        {
            PdfPTable tabela = new PdfPTable(3);

            string dirArquivo = Server.MapPath("~/Relatorios");
            string nomeArquivo = "RelRepasseProfissional_" + DateTime.Now.ToString("ddMMyyyyhhmss");
            string caminhoCompleto = dirArquivo + "\\" + nomeArquivo + ".pdf";

            Document doc = InicializarDocumento(caminhoCompleto);

            doc.Open();

            doc = EscreverCabeçalho(doc);

            var recebimentos = _recebimento.List().Where(x => x.CodigoProfissional == codigoProfissional && x.DataPagamento >= dtInicio && x.DataPagamento <= dtFim).ToList();

            PdfPTable table = new PdfPTable(6);
            table.DefaultCell.Border = Rectangle.NO_BORDER;

            doc.Add(new Phrase("Profissional:" + recebimentos.FirstOrDefault().Profissional.Nome));

            table.AddCell("Dt Pagamento");
            table.AddCell("Especialidade");
            table.AddCell("Serviço");
            table.AddCell("Valor Devido");
            table.AddCell("Percentual");
            table.AddCell("Valor Repasse");

            foreach (var item in recebimentos)
            {
                table.AddCell(item.DataPagamento.ToString("dd/MM/yyyy"));
                table.AddCell(item.Especialidade.Descricao);
                table.AddCell(item.Servico.Descricao);
                table.AddCell(item.ValorDevido.ToString());
                table.AddCell((item.Especialidade.PercentualRepasse == null ? 0 : item.Especialidade.PercentualRepasse).ToString());
                table.AddCell((item.ValorDevido * (item.Especialidade.PercentualRepasse == null ? 0 : item.Especialidade.PercentualRepasse / 100)).ToString());
            }
            table.AddCell("");
            table.AddCell("");
            table.AddCell("Total");
            table.AddCell(recebimentos.Sum(x => x.ValorDevido).ToString());
            table.AddCell("");
            table.AddCell(recebimentos.Sum(x => (x.ValorDevido * (x.Especialidade.PercentualRepasse == null ? 0 : x.Especialidade.PercentualRepasse / 100))).ToString());

            doc.Add(table);

            doc.Close();

            return nomeArquivo;
        }

        public override void UpdateBag()
        {
            base.UpdateBag();

            ViewBag.ListaClientes = _clienteService.List().Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Nome
            });

            ViewBag.ListaProfissionais = _profissionalService.List().Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Nome
            });
        }
    }
}