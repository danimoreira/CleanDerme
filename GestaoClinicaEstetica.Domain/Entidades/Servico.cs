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
    public class Servico : EntidadeBase
    {
        [Required(ErrorMessage = "Favor informar a descrição.", AllowEmptyStrings = false)]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
        [Required(ErrorMessage = "Favor informar o valor do serviço.", AllowEmptyStrings = false)]
        [Display(Name = "Valor do Serviço")]
        public Decimal ValorServico { get; set; }
        [Required(ErrorMessage = "Favor informar a periodicidade.", AllowEmptyStrings = false)]
        [Display(Name = "Periodicidade")]
        public Periodicidade Periodicidade { get; set; }
        [Required(ErrorMessage = "Favor informar a especialidade.", AllowEmptyStrings = false)]
        [Display(Name = "Especialidade")]
        public int CodigoEspecialidade { get; set; }
        [Required(ErrorMessage = "Favor informar a quantidade de sessões.", AllowEmptyStrings = false)]
        [Display(Name = "Quantidade de sessões")]
        public int QuantidadeSessoes { get; set; }

        public virtual Especialidade Especialidade { get; set; }
        public virtual ICollection<Agenda> Agenda { get; set; }
        public virtual ICollection<RecebimentoServicoPorCliente> Recebimentos { get; set; }

    }
}
