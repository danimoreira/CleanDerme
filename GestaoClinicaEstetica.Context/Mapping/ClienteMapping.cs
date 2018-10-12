using GestaoClinicaEstetica.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoClinicaEstetica.Context.Mapping
{
    public class ClienteMapping : PessoaFisicaMapping<Cliente>
    {
        public ClienteMapping()
        {
            ToTable("CLIENTE");
        }
    }
}
