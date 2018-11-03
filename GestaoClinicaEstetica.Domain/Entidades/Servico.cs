using GestaoClinicaEstetica.Domain.Entidades.Base;
using GestaoClinicaEstetica.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoClinicaEstetica.Domain.Entidades
{
    public class Servico : EntidadeBase
    {
        public string Descricao { get; set; }
        public Decimal ValorServico { get; set; }
        public Periodicidade Periodicidade { get; set; }
        public int CodigoEspecialidade { get; set; }
        public int QuantidadeSessoes { get; set; }

        public virtual Especialidade Especialidade { get; set; }
        public virtual ICollection<Agenda> Agenda { get; set; }
        public virtual ICollection<RecebimentoServicoPorCliente> Recebimentos { get; set; }

    }
}
