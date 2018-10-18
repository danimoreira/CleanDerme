using GestaoClinicaEstetica.Domain.Entidades.Base;
using GestaoClinicaEstetica.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoClinicaEstetica.Domain.Entidades
{
    public class Especialidade : EntidadeBase
    {
        [Required(ErrorMessage = "Favor informar a descrição.", AllowEmptyStrings = false)]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
        [Required(ErrorMessage = "Favor informar o tipo de atendimento.", AllowEmptyStrings = false)]
        [Display(Name = "Tipo de Atendimento")]
        public TipoAtendimento TipoAtendimento { get; set; }
        [Required(ErrorMessage = "Favor informar o tempo de atendimento padrão.", AllowEmptyStrings = false)]
        [Display(Name = "Tempo de Atendimento Padrão")]
        public TimeSpan? TempoAtendimentoPadrao { get; set; }

        public virtual ICollection<Agenda> Compromissos { get; set; }
        public virtual ICollection<Servico> Servicos { get; set; }
        public virtual ICollection<EspecialidadesPorServicoPorCliente> EspecialidadesPorServicoPorCliente { get; set; }
    }
}
