using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoClinicaEstetica.Domain.Enums
{
    public enum SituacaoPresenca
    {
        [Description("Não informado")]
        Pendente = 0,
        [Description("Presente")]
        Presente = 1,
        [Description("Falta")]
        Falta = 2
    }
}
