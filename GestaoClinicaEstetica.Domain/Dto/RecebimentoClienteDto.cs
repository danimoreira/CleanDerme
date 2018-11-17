using GestaoClinicaEstetica.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoClinicaEstetica.Domain.Dto
{
    public class RecebimentoClienteDto
    {
        public int IdRecebimento { get; set; }
        public int CodigoCliente { get; set; }
        public int CodigoServico { get; set; }
        public int CodigoEspecialidade { get; set; }
        public int CodigoProfissional { get; set; }
        public string DescricaoServico { get; set; }
        public DateTime DataAquisicao { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime? DataPagamento { get; set; }
        public Decimal ValorDevido { get; set; }
        public Decimal? ValorRecebido { get; set; }
        public string SituacaoPagamento { get; set; }
        public string UsuarioRecebimento { get; set; }
        public int CodTipoPagamento { get; set; }
        public string TipoPagamento { get; set; }
    }
}
