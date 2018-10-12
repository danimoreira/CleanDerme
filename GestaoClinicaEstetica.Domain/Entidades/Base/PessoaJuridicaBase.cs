using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoClinicaEstetica.Domain.Entidades.Base
{
    public class PessoaJuridicaBase : PessoaBase
    {
        public string Cnpj { get; set; }
        public DateTime DataFundacao { get; set; }
    }
}
