using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoClinicaEstetica.Domain.Dto
{
    public class SalvarPresencaDto
    {
        public int CodigoAgenda { get; set; }
        public int SituacaoPresenca { get; set; }
        public string Justificativa { get; set; }
    }
}
