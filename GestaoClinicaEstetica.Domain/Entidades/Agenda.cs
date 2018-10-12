using GestaoClinicaEstetica.Domain.Entidades.Base;
using GestaoClinicaEstetica.Domain.Enums;
using System;
using System.Collections.Generic;

namespace GestaoClinicaEstetica.Domain.Entidades
{
    public class Agenda : EntidadeBase
    {
        public int CodigoProfissional { get; set; }
        public int CodigoCliente { get; set; }
        public int CodigoEspecialidade { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public TipoConsulta TipoConsulta { get; set; }
        public string Procedimento { get; set; }

        public virtual Profissional Profissional { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual Especialidade Especialidade { get; set; }
        public virtual Presenca Presenca { get; set; }
        public virtual ICollection<Atendimento> Atendimento { get; set; }

    }
}
