using GestaoClinicaEstetica.Domain.Entidades.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoClinicaEstetica.Context.Mapping
{
    public class PessoaFisicaMapping<TEntity> : PessoaMapping<TEntity> where TEntity : PessoaFisicaBase
    {
        public PessoaFisicaMapping()
        {
            Property(x => x.Cpf)
                .HasColumnName("CPF");

            Property(x => x.DataNascimento)
                .HasColumnName("DAT_NASCIMENTO");
        }
    }
}
