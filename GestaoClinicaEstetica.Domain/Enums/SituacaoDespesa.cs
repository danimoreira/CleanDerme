using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoClinicaEstetica.Domain.Enums
{
    public enum SituacaoDespesa
    {
        [Description("Em aberto")]
        EmAberto = 0,
        [Description("Liquidado")]
        Liquidado = 1
    }
}
