using GestaoClinicaEstetica.Domain.Entidades.Base;
using GestaoClinicaEstetica.Domain.Enums;
using GestaoClinicaEstetica.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoClinicaEstetica.Domain.Entidades
{
    public class Despesa : EntidadeBase
    {
        public string Descricao { get; set; }
        public string Fornecedor { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime? DataPagamento { get; set; }

        public SituacaoDespesa Situacao { get; set; }
        public string UsuarioPagamento { get; set; }

        public decimal ValorDespesa { get; set; }
        public Decimal? ValorPagamento { get; set; }

        public TipoPagamento TipoPagamento { get; set; }

        public string Observacao { get; set; }

        [NotMapped]
        public string DscTipoPagamento
        {
            get
            {
                return FuncoesGerais.GetEnumDescription(TipoPagamento);
            }
        }
    }
}
