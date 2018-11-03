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
        public int CodigoCliente { get; set; }
        public int CodigoServico { get; set; }
        public int CodigoEspecialidade { get; set; }
        public int CodigoProfissional { get; set; }

        public DateTime DataAquisicao { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime DataPagamento { get; set; }
        public Decimal ValorDevido { get; set; }
        public Decimal ValorRecebido { get; set; }
        public SituacaoPagamento SituacaoPagamento { get; set; }
        public string UsuarioRecebimento { get; set; }
        public TipoPagamento TipoPagamento { get; set; }

        public virtual Cliente Cliente { get; set; }
        public virtual Servico Servico { get; set; }
        public virtual Profissional Profissional { get; set; }
        public virtual Especialidade Especialidade { get; set; }
    }
}
