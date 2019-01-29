using GestaoClinicaEstetica.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestaoClinicaEstetica.Application.Models
{
    public class AdicionarDespesaModel
    {
        public string Descricao { get; set; }
        public string Fornecedor { get; set; }

        public string Periodicidade { get; set; }
        public int QtdeMeses { get; set; }
        public int DiaVencimento { get; set; }
        public DateTime DataPrimeiroVencto { get; set; }

        public decimal ValorDespesa { get; set; }
        
        public TipoPagamento TipoPagamento { get; set; }
        public string Observacao { get; set; }
    }
}