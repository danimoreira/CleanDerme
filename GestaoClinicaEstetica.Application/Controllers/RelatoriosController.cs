using GestaoClinicaEstetica.Application.Controllers.Base;
using GestaoClinicaEstetica.Domain.Entidades;
using GestaoClinicaEstetica.Domain.Interfaces.Service;
using GestaoClinicaEstetica.Util;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace GestaoClinicaEstetica.Application.Controllers
{
    public class RelatoriosController : BaseController
    {
        private readonly IClienteService _clienteService;
        private readonly IProfissionalService _profissionalService;
        private readonly IRecebimentoServicoPorClienteService _recebimento;
        private readonly IClinicaService _clinicaService;

        public RelatoriosController(IClienteService clienteService, IProfissionalService profissionalService, IRecebimentoServicoPorClienteService recebimento, IClinicaService clinicaService)
        {
            _clienteService = clienteService;
            _profissionalService = profissionalService;
            _recebimento = recebimento;
            _clinicaService = clinicaService;
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
            string txtHtml = string.Empty;

            var empresa = _clinicaService.List().FirstOrDefault();

            var normalFont = FontFactory.GetFont(FontFactory.HELVETICA, 12);
            var boldFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
            var tituloFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14);

            Paragraph paragrafo = new Paragraph();
            paragrafo.Alignment = Element.ALIGN_CENTER;

            paragrafo.Add(new Chunk(empresa.Nome.ToUpper() + "\n", tituloFont));

            if (empresa.Endereco != null)
            {
                paragrafo.Add(new Chunk(empresa.Endereco.ToUpper() + "\n", boldFont));
                paragrafo.Add(new Chunk(empresa.Bairro.ToUpper() + " - " + empresa.Cidade.ToUpper() + "/" + empresa.Uf.ToUpper() + "\n", boldFont));
            }

            if (empresa.Cnpj != null && empresa.Cnpj != "")
                paragrafo.Add(new Chunk("CNPJ: " + empresa.Cnpj.ToUpper() + "\n", boldFont));

            if (empresa.TelefoneFixo != null && empresa.TelefoneFixo != "")
                paragrafo.Add(new Chunk("CNPJ: " + empresa.Cnpj.ToUpper() + "\n", boldFont));

            if (empresa.TelefoneFixo != null && empresa.TelefoneFixo != "")
                paragrafo.Add(new Chunk("Tel: " + empresa.TelefoneFixo.ToUpper() + "\n", boldFont));

            if (empresa.Email != null && empresa.Email != "")
                paragrafo.Add(new Chunk("Email: " + empresa.Email.ToLower() + "\n", boldFont));

            doc.Add(paragrafo);

            Paragraph p = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.LIGHT_GRAY, Element.ALIGN_LEFT, 1)));
            doc.Add(p);

            doc.Add(new Paragraph("\n \n"));

            return doc;
        }

        [HttpGet]
        [Route("Relatorios/GerarHistoricoCliente")]
        public string GerarHistoricoCliente(int codigoCliente)
        {
            string dirArquivo = Server.MapPath("~/Relatorios");
            string nomeArquivo = "RelHistoricoCliente_" + DateTime.Now.ToString("ddMMyyyyhhmss");
            string caminhoCompleto = dirArquivo + "\\" + nomeArquivo + ".pdf";

            var normalFont = FontFactory.GetFont(FontFactory.HELVETICA, 12);
            var boldFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
            var tituloFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14);

            Document doc = InicializarDocumento(caminhoCompleto);

            doc.Open();

            doc = EscreverCabeçalho(doc);

            Cliente dadosCliente = _clienteService.GetById(codigoCliente);

            // Informações do Cliente
            Paragraph paragrafo = new Paragraph();

            paragrafo.Alignment = Element.ALIGN_CENTER;
            paragrafo.Add(new Phrase("HISTÓRICO DO CLIENTE\n", tituloFont));
            paragrafo.Add("\n");
            paragrafo.Add("\n");
            doc.Add(paragrafo);

            paragrafo = new Paragraph();
            paragrafo.Add(new Phrase("Dados Pessoais", tituloFont));
            paragrafo.Add("\n");

            paragrafo.Add(new Phrase("Cliente: ", boldFont));
            paragrafo.Add(new Phrase(dadosCliente.Nome, normalFont));
            paragrafo.Add("\n");

            if (dadosCliente.DataNascimento != null)
            {
                paragrafo.Add(new Phrase("Aniversário: ", boldFont));
                paragrafo.Add(new Phrase(dadosCliente.DataNascimento.Value.ToString("dd/MM"), normalFont));
                paragrafo.Add("\n");
            }            

            if (dadosCliente.Endereco != null)
            {
                paragrafo.Add(new Phrase("Endereço: ", boldFont));
                paragrafo.Add(new Phrase(dadosCliente.Endereco + ", " + dadosCliente.Bairro, normalFont));
                paragrafo.Add("\n");
            }

            if (dadosCliente.Cidade != null)
            {
                paragrafo.Add(new Phrase("Cidade: ", boldFont));
                paragrafo.Add(new Phrase(dadosCliente.Cidade + "/" + dadosCliente.Uf.ToUpper(), normalFont));
                paragrafo.Add("\n");
            }

            paragrafo.Add(new Phrase("Telefone: ", boldFont));
            paragrafo.Add(new Phrase((dadosCliente.TelefoneFixo != null ? dadosCliente.TelefoneFixo + "/" : "") + (dadosCliente.TelefoneCelular ?? "")));
            paragrafo.Add("\n");

            paragrafo.Add(new Phrase("Email: ", boldFont));
            paragrafo.Add(new Phrase((dadosCliente.Email ?? "-")));
            paragrafo.Add("\n");

            paragrafo.Add("\n");

            paragrafo.Add(new Phrase("Consultas realizadas \n", tituloFont));
            paragrafo.Add("\n");

            doc.Add(paragrafo);

            PdfPCell pcell = new PdfPCell();

            PdfPTable table = new PdfPTable(4);
            table.DefaultCell.Border = Rectangle.BOTTOM_BORDER;
            table.WidthPercentage = 100;
            float[] widths = new float[] { 15, 55, 20, 20 };
            table.SetWidths(widths);

            table.AddCell(new Phrase("Data", boldFont));
            table.AddCell(new Phrase("Serviço", boldFont));
            table.AddCell(new Phrase("Profissional", boldFont));
            table.AddCell(new Phrase("Presença", boldFont));
            
            foreach (var item in dadosCliente.Compromissos.OrderBy(x => x.DataInicio).ToList())
            {
                pcell = new PdfPCell();
                pcell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                pcell.AddElement(new Phrase(item.DataInicio.ToString("dd/MM/yyyy"), normalFont));
                table.AddCell(pcell);

                pcell = new PdfPCell();
                pcell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                pcell.AddElement(new Phrase(item.Servico.Descricao, normalFont));
                table.AddCell(pcell);

                pcell = new PdfPCell();
                pcell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                pcell.AddElement(new Phrase(item.Profissional.Nome, normalFont));
                table.AddCell(pcell);

                pcell = new PdfPCell();
                pcell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                pcell.AddElement(new Phrase(FuncoesGerais.GetEnumDescription(item.SituacaoPresenca), normalFont));
                table.AddCell(pcell);                
            }

            doc.Add(table);

            paragrafo = new Paragraph();

            paragrafo.Add("\n");

            paragrafo.Add(new Phrase("Pagamentos realizados \n", tituloFont));
            paragrafo.Add("\n");

            doc.Add(paragrafo);

            PdfPTable tbPagamento = new PdfPTable(4);
            tbPagamento.DefaultCell.Border = Rectangle.BOTTOM_BORDER;
            tbPagamento.WidthPercentage = 100;
            widths = new float[] { 15, 55, 20, 20 };
            tbPagamento.SetWidths(widths);

            tbPagamento.AddCell(new Phrase("Data", boldFont));
            tbPagamento.AddCell(new Phrase("Serviço", boldFont));
            tbPagamento.AddCell(new Phrase("Profissional", boldFont));
            tbPagamento.AddCell(new Phrase("Vlr Pago", boldFont));            

            foreach (var item in dadosCliente.Recebimentos.OrderBy(x => x.DataPagamento).ToList())
            {
                pcell = new PdfPCell();
                pcell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                pcell.AddElement(new Phrase(item.DataPagamento.ToString("dd/MM/yyyy"), normalFont));
                tbPagamento.AddCell(pcell);
                
                pcell = new PdfPCell();
                pcell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                pcell.AddElement(new Phrase(item.Servico.Descricao, normalFont));
                tbPagamento.AddCell(pcell);

                pcell = new PdfPCell();
                pcell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                pcell.AddElement(new Phrase(item.Profissional.Nome, normalFont));
                tbPagamento.AddCell(pcell);

                pcell = new PdfPCell();
                pcell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                pcell.AddElement(new Phrase(item.ValorRecebido.ToString(), normalFont));
                tbPagamento.AddCell(pcell);
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

            var normalFont = FontFactory.GetFont(FontFactory.HELVETICA, 11, BaseColor.GRAY);
            var detailsFont = FontFactory.GetFont(FontFactory.HELVETICA, 9, BaseColor.GRAY);
            var cellFont = FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.GRAY);
            var boldFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
            var tituloFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14);

            string dirArquivo = Server.MapPath("~/Relatorios");
            string nomeArquivo = "RelFechamentoFinanceiro_" + DateTime.Now.ToString("ddMMyyyyhhmss");
            string caminhoCompleto = dirArquivo + "\\" + nomeArquivo + ".pdf";

            Document doc = InicializarDocumento(caminhoCompleto);

            doc.Open();

            doc = EscreverCabeçalho(doc);

            Paragraph paragrafo = new Paragraph();

            paragrafo.Alignment = Element.ALIGN_CENTER;
            paragrafo.Add(new Phrase("FECHAMENTO FINANCEIRO\n", tituloFont));
            paragrafo.Add(new Phrase(dtInicio.ToString("dd/MM/yyyy") + " a " + dtFim.ToString("dd/MM/yyyy"), boldFont));
            paragrafo.Add("\n");
            paragrafo.Add("\n");
            doc.Add(paragrafo);

            var recebimentos = _recebimento.List().Where(x => x.DataPagamento >= dtInicio && x.DataPagamento <= dtFim).ToList();

            PdfPTable table = new PdfPTable(4);
            table.DefaultCell.Border = Rectangle.BOTTOM_BORDER;
            table.WidthPercentage = 100;
            table.HorizontalAlignment = Element.ALIGN_MIDDLE;
            float[] widths = new float[] { 20, 50, 20, 20 };
            table.SetWidths(widths);

            PdfPCell pcell = new PdfPCell();

            pcell = new PdfPCell();
            pcell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            pcell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            pcell.AddElement(new Phrase("Dt Pagamento", boldFont));
            table.AddCell(pcell);

            pcell = new PdfPCell();
            pcell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            pcell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            pcell.AddElement(new Phrase("Descrição", boldFont));
            table.AddCell(pcell);

            pcell = new PdfPCell();
            pcell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            pcell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            pcell.AddElement(new Phrase("Forma Pagto", boldFont));
            table.AddCell(pcell);

            pcell = new PdfPCell();
            pcell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            pcell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            pcell.AddElement(new Phrase("Valor Recebido", boldFont));
            table.AddCell(pcell);            

            foreach (var item in recebimentos.OrderBy(x => x.DataPagamento).OrderBy(y => y.TipoPagamento).ToList())
            {
                pcell = new PdfPCell();
                pcell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                pcell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                pcell.AddElement(new Phrase(item.DataPagamento.ToString("dd/MM/yyyy"), cellFont));
                table.AddCell(pcell);

                pcell = new PdfPCell();
                pcell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                pcell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                pcell.AddElement(new Phrase(item.Servico.Descricao + "\n", cellFont));
                pcell.AddElement(new Phrase(item.Cliente.Nome, detailsFont));
                table.AddCell(pcell);

                pcell = new PdfPCell();
                pcell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                pcell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                pcell.AddElement(new Phrase(FuncoesGerais.GetEnumDescription(item.TipoPagamento), cellFont));
                table.AddCell(pcell);

                pcell = new PdfPCell();
                pcell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                pcell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                pcell.AddElement(new Phrase("R$ " + item.ValorRecebido.ToString(), cellFont));
                table.AddCell(pcell);                
            }

            pcell = new PdfPCell();
            pcell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
            pcell.AddElement(new Phrase("", normalFont));
            pcell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            table.AddCell(pcell);

            pcell = new PdfPCell();
            pcell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
            pcell.AddElement(new Phrase("Total", boldFont));
            pcell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            table.AddCell(pcell);

            pcell = new PdfPCell();
            pcell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
            pcell.AddElement(new Phrase("", normalFont));
            pcell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            table.AddCell(pcell);

            pcell = new PdfPCell();
            pcell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
            pcell.AddElement(new Phrase("R$ " + recebimentos.Sum(x => x.ValorRecebido).ToString(), boldFont));
            table.AddCell(pcell);

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

            var normalFont = FontFactory.GetFont(FontFactory.HELVETICA, 11, BaseColor.GRAY);
            var detailsFont = FontFactory.GetFont(FontFactory.HELVETICA, 9, BaseColor.GRAY);
            var cellFont = FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.GRAY);
            var boldFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
            var tituloFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14);

            string dirArquivo = Server.MapPath("~/Relatorios");
            string nomeArquivo = "RelRepasseProfissional_" + DateTime.Now.ToString("ddMMyyyyhhmss");
            string caminhoCompleto = dirArquivo + "\\" + nomeArquivo + ".pdf";

            Document doc = InicializarDocumento(caminhoCompleto);

            doc.Open();

            doc = EscreverCabeçalho(doc);

            Paragraph paragrafo = new Paragraph();

                     

            var recebimentos = _recebimento.List().Where(x => x.CodigoProfissional == codigoProfissional && x.DataPagamento >= dtInicio && x.DataPagamento <= dtFim).ToList();

            PdfPTable table = new PdfPTable(5);
            table.DefaultCell.Border = Rectangle.BOTTOM_BORDER;
            table.WidthPercentage = 100;
            table.HorizontalAlignment = Element.ALIGN_MIDDLE;
            float[] widths = new float[] { 15, 45, 15, 10, 15 };
            table.SetWidths(widths);

            PdfPCell pcell = new PdfPCell();

            paragrafo.Alignment = Element.ALIGN_CENTER;
            paragrafo.Add(new Phrase("REPASSE AO PROFISSIONAL\n", tituloFont));
            paragrafo.Add(new Phrase("Profissional:" + recebimentos.FirstOrDefault().Profissional.Nome, boldFont));
            paragrafo.Add("\n");
            paragrafo.Add(new Phrase(dtInicio.ToString("dd/MM/yyyy") + " a " + dtFim.ToString("dd/MM/yyyy"), boldFont));
            paragrafo.Add("\n");
            paragrafo.Add("\n");
            doc.Add(paragrafo);

            pcell = new PdfPCell();
            pcell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            pcell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            pcell.AddElement(new Phrase("Data", boldFont));
            table.AddCell(pcell);

            pcell = new PdfPCell();
            pcell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            pcell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            pcell.AddElement(new Phrase("Descrição", boldFont));
            table.AddCell(pcell);
            
            pcell = new PdfPCell();
            pcell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            pcell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            pcell.AddElement(new Phrase("Vlr Devido", boldFont));
            table.AddCell(pcell);

            pcell = new PdfPCell();
            pcell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            pcell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            pcell.AddElement(new Phrase("%", boldFont));
            table.AddCell(pcell);

            pcell = new PdfPCell();
            pcell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            pcell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            pcell.AddElement(new Phrase("Vlr Repasse", boldFont));
            table.AddCell(pcell);
            
            foreach (var item in recebimentos)
            {
                pcell = new PdfPCell();
                pcell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                pcell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                pcell.AddElement(new Phrase(item.DataPagamento.ToString("dd/MM/yyyy"), cellFont));
                table.AddCell(pcell);

                pcell = new PdfPCell();
                pcell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                pcell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                pcell.AddElement(new Phrase(item.Especialidade.Descricao + "\n", cellFont));
                pcell.AddElement(new Phrase(item.Servico.Descricao + "\n", cellFont));
                pcell.AddElement(new Phrase(item.Cliente.Nome, cellFont));
                table.AddCell(pcell);

                pcell = new PdfPCell();
                pcell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                pcell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                pcell.AddElement(new Phrase("R$ " + item.ValorDevido.ToString(), cellFont));
                table.AddCell(pcell);

                pcell = new PdfPCell();
                pcell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                pcell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                decimal percentual = Convert.ToDecimal((item.Especialidade.PercentualRepasse == null ? 0 : item.Especialidade.PercentualRepasse));
                pcell.AddElement(new Phrase(percentual.ToString("0") + "%", cellFont));
                table.AddCell(pcell);

                pcell = new PdfPCell();
                pcell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                pcell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                decimal vlrRepasse = Convert.ToDecimal(item.ValorDevido * (item.Especialidade.PercentualRepasse == null ? 0 : item.Especialidade.PercentualRepasse / 100));
                pcell.AddElement(new Phrase("R$ " + vlrRepasse.ToString("0.00"), cellFont));
                table.AddCell(pcell);                
            }
           
            pcell = new PdfPCell();
            pcell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
            pcell.AddElement(new Phrase("", normalFont));
            pcell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            table.AddCell(pcell);

            pcell = new PdfPCell();
            pcell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
            pcell.AddElement(new Phrase("Total", boldFont));
            pcell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            table.AddCell(pcell);

            pcell = new PdfPCell();
            pcell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
            pcell.AddElement(new Phrase("R$ " + recebimentos.Sum(x => x.ValorDevido).ToString(), boldFont));
            pcell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            table.AddCell(pcell);

            pcell = new PdfPCell();
            pcell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
            pcell.AddElement(new Phrase("", normalFont));
            pcell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            table.AddCell(pcell);

            pcell = new PdfPCell();
            pcell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
            decimal vlrTotalRepasse = Convert.ToDecimal(recebimentos.Sum(x => (x.ValorDevido * (x.Especialidade.PercentualRepasse == null ? 0 : x.Especialidade.PercentualRepasse / 100))));
            pcell.AddElement(new Phrase("R$ " + vlrTotalRepasse.ToString("0.00"), boldFont));
            table.AddCell(pcell);
            
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