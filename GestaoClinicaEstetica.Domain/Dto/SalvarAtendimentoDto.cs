using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoClinicaEstetica.Domain.Dto
{
    public class SalvarAtendimentoDto
    {
        public int CodigoAgenda { get; set; }
        public string ObsAtendimento { get; set; }
    }
}
