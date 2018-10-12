using GestaoClinicaEstetica.Domain.Entidades.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoClinicaEstetica.Domain.Entidades
{
    public class ServicoPorCliente : EntidadeBase
    {
        public int CodigoServico { get; set; }
        public int CodigoCliente { get; set; }
        public DateTime DataAquisicao { get; set; }
        public string Situacao { get; set; }

        public virtual RecebimentoServicoPorCliente RecebimentoServicoPorCliente { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual Servico Servico { get; set; }
        public virtual ICollection<EspecialidadesPorServicoPorCliente> EspecialidadesPorServicoPorCliente { get; set; }
    }
}
