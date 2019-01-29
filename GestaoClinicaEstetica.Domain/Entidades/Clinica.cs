using GestaoClinicaEstetica.Domain.Entidades.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoClinicaEstetica.Domain.Entidades
{
    public class Clinica : PessoaJuridicaBase
    {
        public string CorEventoAniversariantes { get; set; }
        public string CorEventoRecebimentos { get; set; }
    }
}
