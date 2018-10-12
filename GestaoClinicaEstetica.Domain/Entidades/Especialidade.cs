using GestaoClinicaEstetica.Domain.Entidades.Base;
using GestaoClinicaEstetica.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoClinicaEstetica.Domain.Entidades
{
    public class Especialidade : EntidadeBase
    {
        public string Descricao { get; set; }
        public TipoAtendimento TipoAtendimento { get; set; }
        public TimeSpan TempoAtendimentoPadrao { get; set; }

        public virtual ICollection<Agenda> Compromissos { get; set; }
        public virtual ICollection<EspecialidadePorServico> EspecialidadesPorServico { get; set; }
        public virtual ICollection<EspecialidadesPorServicoPorCliente> EspecialidadesPorServicoPorCliente { get; set; }
    }
}
