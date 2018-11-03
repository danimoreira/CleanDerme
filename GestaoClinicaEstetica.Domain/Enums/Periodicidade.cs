using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoClinicaEstetica.Domain.Enums
{
    public enum Periodicidade
    {
        [Description("Avulso")]
        Avulso = 0,
        [Description("Mensal")]
        Mensal = 1,
        [Description("Bimestral")]
        Bimestral = 2,
        [Description("Trimestral")]
        Trimestral = 3,
        [Description("Semestral")]
        Semestral  = 6,
        [Description("Anual")]
        Anual = 12
    }
}
