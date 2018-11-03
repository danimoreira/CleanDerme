using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoClinicaEstetica.Domain.Enums
{
    public enum SituacaoPagamento
    {
        [Description("Pendente")]
        Pendente = 0,
        [Description("Recebido")]
        Recebido = 1   
    }
}
