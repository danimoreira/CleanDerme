using GestaoClinicaEstetica.Domain.Entidades.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoClinicaEstetica.Domain.Entidades
{
    public class EspecialidadePorServico : EntidadeBase
    {
        public int CodigoServico { get; set; }
        public int CodigoEspecialidade { get; set; }
        public int QuantidadeSessoes { get; set; }

        public virtual Servico Servico { get; set; }
        public virtual Especialidade Especialidade { get; set; }
    }
}
