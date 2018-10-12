using GestaoClinicaEstetica.Domain.Entidades.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoClinicaEstetica.Domain.Entidades
{
    public class EspecialidadesPorServicoPorCliente : EntidadeBase
    {
        public int CodigoServicoPorCliente { get; set; }
        public int CodigoEspecialidade { get; set; }
        public string Referencia { get; set; }
        public int QuantidadeSessoes { get; set; }
        public int QuantidadeSessoesRealizadas { get; set; }

        public virtual ServicoPorCliente ServicoPorCliente { get; set; }
        public virtual Especialidade Especialidade { get; set; }
    }
}
