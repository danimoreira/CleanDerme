using GestaoClinicaEstetica.Domain.Entidades.Base;
using System.Collections.Generic;

namespace GestaoClinicaEstetica.Domain.Entidades
{
    public class EspecialidadePorProfissional : EntidadeBase
    {
        public int CodProfissional { get; set; }
        public int CodEspecialidade { get; set; }

        public virtual Profissional Profissional { get; set; }
        public virtual Especialidade Especialidade { get; set; }
    }
}
