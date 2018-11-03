using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoClinicaEstetica.Domain.Dto
{
    public class AgendarPagamentoDto
    {
        public int CodigoCliente { get; set; }
        public int CodigoEspecialidade { get; set; }
        public int CodigoProfissional { get; set; }
        public int CodigoServico { get; set; }

        public int QtdeParcelas { get; set; }
        public DateTime PrimeiroVencimento { get; set; }
        public int DiaVencimento { get; set; }
        public decimal ValorParcela { get; set; }
    }
}
