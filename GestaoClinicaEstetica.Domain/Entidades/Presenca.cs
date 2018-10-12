using GestaoClinicaEstetica.Domain.Entidades.Base;
using GestaoClinicaEstetica.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoClinicaEstetica.Domain.Entidades
{
    public class Presenca : EntidadeBase
    {
        public int CodigoAgenda { get; set; }
        public SituacaoPresenca SituacaoPresenca { get; set; }
        public string Observacao { get; set; }

        public virtual Agenda Agenda { get; set; }
    }
}
