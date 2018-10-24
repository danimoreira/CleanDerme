using GestaoClinicaEstetica.Domain.Entidades.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoClinicaEstetica.Domain.Entidades
{
    public class Profissional : PessoaFisicaBase
    {
        public virtual ICollection<Agenda> Compromissos { get; set; }
        public virtual ICollection<EspecialidadePorProfissional> EspecialidadePorProfissional { get; set; }
    }
}
