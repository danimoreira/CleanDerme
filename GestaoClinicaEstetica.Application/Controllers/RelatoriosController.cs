using GestaoClinicaEstetica.Application.Controllers.Base;
using GestaoClinicaEstetica.Domain.Entidades;
using GestaoClinicaEstetica.Domain.Enums;
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

            var normalFont = FontFactory.GetFont(FontFactory.HELVETICA, 9, BaseColor.LIGHT_GRAY);
            var boldFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9, BaseColor.LIGHT_GRAY);
            var tituloFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 11, BaseColor.LIGHT_GRAY);

            Paragraph paragrafo = new Paragraph();
            paragrafo.Alignment = Element.ALIGN_CENTER;

            paragrafo.Add(new Chunk(empresa.Nome.ToUpper() + "\n", tituloFont));

            if (empresa.Endereco != null)
            {
                paragrafo.Add(new Chunk(empresa.Endereco.ToUpper() + ", ", boldFont));
                paragrafo.Add(new Chunk(empresa.Bairro.ToUpper() + " - " + empresa.Cidade.ToUpper() + "/" + empresa.Uf.ToUpper() + "\n", boldFont));
            }

            if (empresa.Cnpj != null && empresa.Cnpj != "")
                paragrafo.Add(new Chunk("CNPJ: " + empresa.Cnpj.ToUpper() + "\n", boldFont));

            if (empresa.TelefoneCelular != null && empresa.TelefoneCelular != "")
                paragrafo.Add(new Chunk("Celular: " + empresa.TelefoneCelular + " ", boldFont));

            if (empresa.TelefoneFixo != null && empresa.TelefoneFixo != "")
                paragrafo.Add(new Chunk("Tel: " + empresa.TelefoneFixo.ToUpper() + "\n", boldFont));

            if (empresa.Email != null && empresa.Email != "")
                paragrafo.Add(new Chunk("Email: " + empresa.Email.ToLower() + "\n", boldFont));

            doc.Add(paragrafo);

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

            var normalFont = FontFactory.GetFont(FontFactory.HELVETICA, 11, BaseColor.GRAY);
            var detailsFont = FontFactory.GetFont(FontFactory.HELVETICA, 9, BaseColor.GRAY);
            var cellFont = FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.GRAY);
            var boldFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 11, BaseColor.GRAY);
            var boldTextFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 11, BaseColor.MAGENTA);
            var tituloFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14, BaseColor.MAGENTA);

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
            paragrafo.Alignment = Element.ALIGN_LEFT;
            paragrafo.Add(new Phrase("DADOS PESSOAIS \n", boldTextFont));
            paragrafo.Add("\n");
            doc.Add(paragrafo);

            paragrafo = new Paragraph();
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
            paragrafo.Add(new Phrase((dadosCliente.TelefoneFixo != null ? dadosCliente.TelefoneFixo + "/" : "") + (dadosCliente.TelefoneCelular ?? ""), normalFont));
            paragrafo.Add("\n");

            paragrafo.Add(new Phrase("Email: ", boldFont));
            paragrafo.Add(new Phrase((dadosCliente.Email ?? "-"), normalFont));
            paragrafo.Add("\n");

            doc.Add(paragrafo);

            paragrafo = new Paragraph();
            paragrafo.Add("\n");
            paragrafo.Alignment = Element.ALIGN_LEFT;
            paragrafo.Add(new Phrase("CONSULTAS REALIZADAS \n", boldTextFont));
            paragrafo.Add("\n");
            doc.Add(paragrafo);

            PdfPCell pcell = new PdfPCell();

            PdfPTable table = new PdfPTable(4);
            table.DefaultCell.Border = Rectangle.BOTTOM_BORDER;
            table.WidthPercentage = 100;
            float[] widths = new float[] { 15, 55, 20, 20 };
            table.SetWidths(widths);

            pcell = new PdfPCell();

            pcell = new PdfPCell(new Phrase("Data", boldFont));
            pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            pcell.Border = PdfPCell.BOTTOM_BORDER;
            pcell.BorderColor = BaseColor.GRAY;
            table.AddCell(pcell);

            pcell = new PdfPCell(new Phrase("Serviço", boldFont));
            pcell.HorizontalAlignment = Element.ALIGN_LEFT;
            pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            pcell.Border = PdfPCell.BOTTOM_BORDER;
            pcell.BorderColor = BaseColor.GRAY;
            table.AddCell(pcell);

            pcell = new PdfPCell(new Phrase("Profissional", boldFont));
            pcell.HorizontalAlignment = Element.ALIGN_LEFT;
            pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            pcell.Border = PdfPCell.BOTTOM_BORDER;
            pcell.BorderColor = BaseColor.GRAY;
            table.AddCell(pcell);

            pcell = new PdfPCell(new Phrase("Presença", boldFont));
            pcell.HorizontalAlignment = Element.ALIGN_LEFT;
            pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            pcell.Border = PdfPCell.BOTTOM_BORDER;
            pcell.BorderColor = BaseColor.GRAY;
            table.AddCell(pcell);

            foreach (var item in dadosCliente.Compromissos.OrderBy(x => x.DataInicio).OrderBy(y => y.DataInicio).ToList())
            {
                pcell = new PdfPCell(new Phrase(item.DataInicio.ToString("dd/MM/yyyy"), normalFont));
                pcell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                pcell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                pcell.Border = PdfPCell.BOTTOM_BORDER;
                pcell.BorderColor = BaseColor.LIGHT_GRAY;
                table.AddCell(pcell);

                pcell = new PdfPCell(new Phrase(item.Servico.Descricao, normalFont));
                pcell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                pcell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                pcell.Border = PdfPCell.BOTTOM_BORDER;
                pcell.BorderColor = BaseColor.LIGHT_GRAY;
                table.AddCell(pcell);

                pcell = new PdfPCell(new Phrase(item.Profissional.Nome, normalFont));
                pcell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                pcell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                pcell.Border = PdfPCell.BOTTOM_BORDER;
                pcell.BorderColor = BaseColor.LIGHT_GRAY;
                table.AddCell(pcell);

                pcell = new PdfPCell(new Phrase(FuncoesGerais.GetEnumDescription(item.SituacaoPresenca), normalFont));
                pcell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                pcell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                pcell.Border = PdfPCell.BOTTOM_BORDER;
                pcell.BorderColor = BaseColor.LIGHT_GRAY;
                table.AddCell(pcell);
            }

            doc.Add(table);

            paragrafo = new Paragraph();
            paragrafo.Add("\n");
            paragrafo.Alignment = Element.ALIGN_LEFT;
            paragrafo.Add(new Phrase("PAGAMENTOS REALIZADOS \n", boldTextFont));
            paragrafo.Add("\n");
            doc.Add(paragrafo);

            table = new PdfPTable(4);
            table.DefaultCell.Border = Rectangle.BOTTOM_BORDER;
            table.WidthPercentage = 100;
            widths = new float[] { 15, 55, 20, 20 };
            table.SetWidths(widths);

            pcell = new PdfPCell();

            pcell = new PdfPCell(new Phrase("Data", boldFont));
            pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            pcell.Border = PdfPCell.BOTTOM_BORDER;
            pcell.BorderColor = BaseColor.GRAY;
            table.AddCell(pcell);

            pcell = new PdfPCell(new Phrase("Serviço", boldFont));
            pcell.HorizontalAlignment = Element.ALIGN_LEFT;
            pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            pcell.Border = PdfPCell.BOTTOM_BORDER;
            pcell.BorderColor = BaseColor.GRAY;
            table.AddCell(pcell);

            pcell = new PdfPCell(new Phrase("Profissional", boldFont));
            pcell.HorizontalAlignment = Element.ALIGN_LEFT;
            pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            pcell.Border = PdfPCell.BOTTOM_BORDER;
            pcell.BorderColor = BaseColor.GRAY;
            table.AddCell(pcell);

            pcell = new PdfPCell(new Phrase("Vlr Pago", boldFont));
            pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            pcell.Border = PdfPCell.BOTTOM_BORDER;
            pcell.BorderColor = BaseColor.GRAY;
            table.AddCell(pcell);

            foreach (var item in dadosCliente.Recebimentos.OrderBy(x => x.DataPagamento).OrderBy(y => y.DataPagamento).ToList())
            {
                pcell = new PdfPCell(new Phrase(item.DataPagamento.ToString("dd/MM/yyyy"), normalFont));
                pcell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                pcell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                pcell.Border = PdfPCell.BOTTOM_BORDER;
                pcell.BorderColor = BaseColor.LIGHT_GRAY;
                table.AddCell(pcell);

                pcell = new PdfPCell(new Phrase(item.Servico.Descricao, normalFont));
                pcell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                pcell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                pcell.Border = PdfPCell.BOTTOM_BORDER;
                pcell.BorderColor = BaseColor.LIGHT_GRAY;
                table.AddCell(pcell);

                pcell = new PdfPCell(new Phrase(item.Profissional.Nome, normalFont));
                pcell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                pcell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                pcell.Border = PdfPCell.BOTTOM_BORDER;
                pcell.BorderColor = BaseColor.LIGHT_GRAY;
                table.AddCell(pcell);

                pcell = new PdfPCell(new Phrase("R$ " + item.ValorRecebido.ToString(), normalFont));
                pcell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                pcell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                pcell.Border = PdfPCell.BOTTOM_BORDER;
                pcell.BorderColor = BaseColor.LIGHT_GRAY;
                table.AddCell(pcell);
            }

            doc.Add(table);
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
            var boldFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 11, BaseColor.GRAY);
            var boldTextFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 11, BaseColor.MAGENTA);
            var tituloFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14, BaseColor.MAGENTA);

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

            PdfPTable table = new PdfPTable(4);
            PdfPCell pcell = new PdfPCell();
            float[] widths = new float[] { 20, 50, 20, 20 };
            Paragraph lineP = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.LIGHT_GRAY, Element.ALIGN_LEFT, 1)));

            var recebimentos = _recebimento.List().Where(x => x.DataPagamento >= dtInicio && x.DataPagamento <= dtFim).ToList();

            foreach (var tipoPagto in Enum.GetValues(typeof(TipoPagamento)))
            {
                TipoPagamento tipoPagamentoEnum = (TipoPagamento)tipoPagto;

                var recebimentoPorPagamento = recebimentos.Where(y => y.TipoPagamento.Equals(tipoPagto)).OrderBy(x => x.DataPagamento).ToList();

                if (recebimentoPorPagamento.Count > 0)
                {
                    paragrafo = new Paragraph();

                    paragrafo.Alignment = Element.ALIGN_LEFT;
                    paragrafo.Add(new Phrase("FORMA DE PAGAMENTO: " + FuncoesGerais.GetEnumDescription(tipoPagamentoEnum) + "\n \n", boldTextFont));
                    doc.Add(paragrafo);

                    table = new PdfPTable(4);
                    table.DefaultCell.Border = Rectangle.BOTTOM_BORDER;
                    table.WidthPercentage = 100;
                    table.HorizontalAlignment = Element.ALIGN_MIDDLE;
                    table.SetWidths(widths);

                    pcell = new PdfPCell();

                    pcell = new PdfPCell(new Phrase("Dt Pagamento", boldFont));
                    pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                    pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    pcell.Border = PdfPCell.BOTTOM_BORDER;
                    pcell.BorderColor = BaseColor.GRAY;
                    table.AddCell(pcell);

                    pcell = new PdfPCell(new Phrase("Descrição", boldFont));
                    pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                    pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    pcell.Border = PdfPCell.BOTTOM_BORDER;
                    pcell.BorderColor = BaseColor.GRAY;
                    table.AddCell(pcell);

                    pcell = new PdfPCell(new Phrase("Forma Pagto", boldFont));
                    pcell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    pcell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                    pcell.Border = PdfPCell.BOTTOM_BORDER;
                    pcell.BorderColor = BaseColor.GRAY;
                    table.AddCell(pcell);

                    pcell = new PdfPCell(new Phrase("Valor Recebido", boldFont));
                    pcell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    pcell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                    pcell.Border = PdfPCell.BOTTOM_BORDER;
                    pcell.BorderColor = BaseColor.GRAY;
                    table.AddCell(pcell);

                    foreach (var item in recebimentoPorPagamento)
                    {

                        pcell = new PdfPCell(new Phrase(item.DataPagamento.ToString("dd/MM/yyyy"), cellFont));
                        pcell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        pcell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                        pcell.Border = PdfPCell.BOTTOM_BORDER;
                        pcell.BorderColor = BaseColor.LIGHT_GRAY;
                        table.AddCell(pcell);

                        pcell = new PdfPCell();
                        pcell.AddElement(new Phrase("Serviço: " + item.Servico.Descricao, cellFont));
                        pcell.AddElement(new Phrase(new Phrase("Cliente: " + item.Cliente.Nome, cellFont)));
                        pcell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        pcell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                        pcell.Border = PdfPCell.BOTTOM_BORDER;
                        pcell.BorderColor = BaseColor.LIGHT_GRAY;
                        table.AddCell(pcell);

                        pcell = new PdfPCell(new Phrase(FuncoesGerais.GetEnumDescription(item.TipoPagamento), cellFont));
                        pcell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        pcell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                        pcell.Border = PdfPCell.BOTTOM_BORDER;
                        pcell.BorderColor = BaseColor.LIGHT_GRAY;
                        table.AddCell(pcell);

                        pcell = new PdfPCell(new Phrase("R$ " + item.ValorRecebido.ToString(), cellFont));
                        pcell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                        pcell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                        pcell.Border = PdfPCell.BOTTOM_BORDER;
                        pcell.BorderColor = BaseColor.LIGHT_GRAY;
                        table.AddCell(pcell);
                    }

                    pcell = new PdfPCell(new Phrase("", normalFont));
                    pcell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                    pcell.Border = PdfPCell.NO_BORDER;
                    pcell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                    table.AddCell(pcell);

                    pcell = new PdfPCell(new Phrase("Total", boldFont));
                    pcell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                    pcell.Border = PdfPCell.NO_BORDER;
                    pcell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                    table.AddCell(pcell);

                    pcell = new PdfPCell(new Phrase("", normalFont));
                    pcell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                    pcell.Border = PdfPCell.NO_BORDER;
                    pcell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                    table.AddCell(pcell);

                    pcell = new PdfPCell(new Phrase("R$ " + recebimentoPorPagamento.Sum(x => x.ValorRecebido).ToString(), boldFont));
                    pcell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                    pcell.Border = PdfPCell.NO_BORDER;
                    table.AddCell(pcell);

                    doc.Add(table);

                    paragrafo = new Paragraph();
                    paragrafo.Add("\n");
                    paragrafo.Add("\n");
                    doc.Add(paragrafo);
                }
            }

            table = new PdfPTable(4);
            table.DefaultCell.Border = Rectangle.BOTTOM_BORDER;
            table.WidthPercentage = 100;
            table.HorizontalAlignment = Element.ALIGN_MIDDLE;
            table.SetWidths(widths);

            pcell = new PdfPCell(new Phrase("TOTAL GERAL", boldFont));
            pcell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
            pcell.Border = PdfPCell.TOP_BORDER;
            pcell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            table.AddCell(pcell);

            pcell = new PdfPCell();
            pcell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
            pcell.Border = PdfPCell.TOP_BORDER;
            pcell.AddElement(new Phrase("", normalFont));
            pcell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            table.AddCell(pcell);

            pcell = new PdfPCell();
            pcell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
            pcell.Border = PdfPCell.TOP_BORDER;
            pcell.AddElement(new Phrase("", normalFont));
            pcell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            table.AddCell(pcell);

            pcell = new PdfPCell(new Phrase("R$ " + recebimentos.Sum(x => x.ValorRecebido).ToString(), boldFont));
            pcell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
            pcell.Border = PdfPCell.TOP_BORDER;
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
            var boldFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 11, BaseColor.GRAY);
            var boldTextFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 11, BaseColor.MAGENTA);
            var tituloFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14, BaseColor.MAGENTA);

            string dirArquivo = Server.MapPath("~/Relatorios");
            string nomeArquivo = "RelRepasseProfissional_" + DateTime.Now.ToString("ddMMyyyyhhmss");
            string caminhoCompleto = dirArquivo + "\\" + nomeArquivo + ".pdf";

            Document doc = InicializarDocumento(caminhoCompleto);

            doc.Open();

            doc = EscreverCabeçalho(doc);

            Paragraph paragrafo = new Paragraph();

            var recebimentos = _recebimento.List().Where(x => x.CodigoProfissional == codigoProfissional && x.DataPagamento >= dtInicio && x.DataPagamento <= dtFim).OrderBy(y => y.DataPagamento).ToList();

            PdfPTable table = new PdfPTable(5);
            table.DefaultCell.Border = Rectangle.BOTTOM_BORDER;
            table.WidthPercentage = 100;
            table.HorizontalAlignment = Element.ALIGN_MIDDLE;
            float[] widths = new float[] { 15, 45, 15, 10, 15 };
            table.SetWidths(widths);

            PdfPCell pcell = new PdfPCell();

            paragrafo.Alignment = Element.ALIGN_CENTER;
            paragrafo.Add(new Phrase("REPASSE AO PROFISSIONAL\n", tituloFont));
            paragrafo.Add(new Phrase("Profissional: " + recebimentos.FirstOrDefault().Profissional.Nome, boldFont));
            paragrafo.Add("\n");
            paragrafo.Add(new Phrase(dtInicio.ToString("dd/MM/yyyy") + " a " + dtFim.ToString("dd/MM/yyyy"), boldFont));
            paragrafo.Add("\n");
            paragrafo.Add("\n");
            doc.Add(paragrafo);


            pcell = new PdfPCell(new Phrase("Data", boldFont));
            pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            pcell.Border = PdfPCell.BOTTOM_BORDER;
            pcell.BorderColor = BaseColor.GRAY;
            table.AddCell(pcell);

            pcell = new PdfPCell(new Phrase("Descrição", boldFont));
            pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            pcell.Border = PdfPCell.BOTTOM_BORDER;
            pcell.BorderColor = BaseColor.GRAY;
            table.AddCell(pcell);

            pcell = new PdfPCell(new Phrase("Vlr Serviço", boldFont));
            pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            pcell.Border = PdfPCell.BOTTOM_BORDER;
            pcell.BorderColor = BaseColor.GRAY;
            table.AddCell(pcell);

            pcell = new PdfPCell(new Phrase("%", boldFont));
            pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            pcell.Border = PdfPCell.BOTTOM_BORDER;
            pcell.BorderColor = BaseColor.GRAY;
            table.AddCell(pcell);

            pcell = new PdfPCell(new Phrase("Vlr Repasse", boldFont));
            pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            pcell.Border = PdfPCell.BOTTOM_BORDER;
            pcell.BorderColor = BaseColor.GRAY;
            table.AddCell(pcell);

            foreach (var item in recebimentos)
            {
                pcell = new PdfPCell(new Phrase(item.DataPagamento.ToString("dd/MM/yyyy"), cellFont));
                pcell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                pcell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                pcell.Border = PdfPCell.BOTTOM_BORDER;
                pcell.BorderColor = BaseColor.LIGHT_GRAY;
                table.AddCell(pcell);

                pcell = new PdfPCell();
                pcell.AddElement(new Phrase("Especialidade: " + item.Especialidade.Descricao, cellFont));
                pcell.AddElement(new Phrase("Serviço: " + item.Servico.Descricao, cellFont));
                pcell.AddElement(new Phrase("Cliente: " + item.Cliente.Nome, cellFont));
                pcell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                pcell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                pcell.Border = PdfPCell.BOTTOM_BORDER;
                pcell.BorderColor = BaseColor.LIGHT_GRAY;
                table.AddCell(pcell);

                pcell = new PdfPCell(new Phrase("R$ " + item.ValorDevido.ToString(), cellFont));
                pcell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                pcell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                pcell.Border = PdfPCell.BOTTOM_BORDER;
                pcell.BorderColor = BaseColor.LIGHT_GRAY;
                table.AddCell(pcell);

                decimal percentual = Convert.ToDecimal((item.Especialidade.PercentualRepasse == null ? 0 : item.Especialidade.PercentualRepasse));
                pcell = new PdfPCell(new Phrase(percentual.ToString("0") + "%", cellFont));
                pcell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                pcell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                pcell.Border = PdfPCell.BOTTOM_BORDER;
                pcell.BorderColor = BaseColor.LIGHT_GRAY;
                table.AddCell(pcell);

                decimal vlrRepasse = Convert.ToDecimal(item.ValorDevido * (item.Especialidade.PercentualRepasse == null ? 0 : item.Especialidade.PercentualRepasse / 100));
                pcell = new PdfPCell(new Phrase("R$ " + vlrRepasse.ToString("0.00"), cellFont));
                pcell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                pcell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                pcell.Border = PdfPCell.BOTTOM_BORDER;
                pcell.BorderColor = BaseColor.LIGHT_GRAY;
                table.AddCell(pcell);
            }

            pcell = new PdfPCell(new Phrase("", normalFont));
            pcell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
            pcell.Border = PdfPCell.NO_BORDER;
            pcell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            table.AddCell(pcell);

            pcell = new PdfPCell(new Phrase("Total", boldFont));
            pcell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
            pcell.Border = PdfPCell.NO_BORDER;
            pcell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            table.AddCell(pcell);

            pcell = new PdfPCell(new Phrase("R$ " + recebimentos.Sum(x => x.ValorDevido).ToString(), boldFont));
            pcell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
            pcell.Border = PdfPCell.NO_BORDER;
            pcell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            table.AddCell(pcell);

            pcell = new PdfPCell();
            pcell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
            pcell.AddElement(new Phrase("", normalFont));
            pcell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            pcell.Border = PdfPCell.NO_BORDER;
            table.AddCell(pcell);

            decimal vlrTotalRepasse = Convert.ToDecimal(recebimentos.Sum(x => (x.ValorDevido * (x.Especialidade.PercentualRepasse == null ? 0 : x.Especialidade.PercentualRepasse / 100))));
            pcell = new PdfPCell(new Phrase("R$ " + vlrTotalRepasse.ToString("0.00"), boldFont));
            pcell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
            pcell.Border = PdfPCell.NO_BORDER;
            pcell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
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