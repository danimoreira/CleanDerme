using GestaoClinicaEstetica.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoClinicaEstetica.Domain.Dto
{
    public class ServicoDto
    {
        public string Descricao { get; set; }
        public Decimal ValorServico { get; set; }
        public Periodicidade Periodicidade { get; set; }
        public string DescricaoPeriodicidade { get; set; }
        public int CodigoEspecialidade { get; set; }
        public int QuantidadeSessoes { get; set; }
    }
}
