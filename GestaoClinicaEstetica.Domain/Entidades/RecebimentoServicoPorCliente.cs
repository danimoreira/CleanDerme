using GestaoClinicaEstetica.Domain.Entidades.Base;
using GestaoClinicaEstetica.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoClinicaEstetica.Domain.Entidades
{
    public class RecebimentoServicoPorCliente : EntidadeBase
    {
        public int CodigoServicoPorCliente { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime DataPagamento { get; set; }
        public Decimal ValorDevido { get; set; }
        public Decimal ValorRecebido { get; set; }
        public SituacaoPagamento SituacaoPagamento { get; set; }
        public string UsuarioRecebimento { get; set; }
        public TipoPagamento TipoPagamento { get; set; }

        public virtual ServicoPorCliente ServicosPorCliente { get; set; }
    }
}
