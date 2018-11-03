using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoClinicaEstetica.Domain.Enums
{
    public enum TipoPagamento
    {
        [Description("Dinheiro")]
        Dinheiro = 0,
        [Description("Cartão de Débito")]
        Debito = 1,
        [Description("Cartão de Crédito")]
        Credito = 2,
        [Description("Cheque")]
        Cheque = 3
    }
}
