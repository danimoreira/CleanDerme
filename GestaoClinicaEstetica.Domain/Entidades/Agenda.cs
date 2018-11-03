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
        public int? CodigoServico { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public string Procedimento { get; set; }

        public SituacaoPresenca SituacaoPresenca { get; set; }
        public string ObservacaoPresenca { get; set; }

        public string ObsAtendimento { get; set; }

        public virtual Profissional Profissional { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual Servico Servico { get; set; }
        public virtual Especialidade Especialidade { get; set; }

    }
}
