using GestaoClinicaEstetica.Domain.Entidades.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoClinicaEstetica.Context.Mapping
{
    public class PessoaJuridicaMapping<TEntity> : PessoaMapping<TEntity> where TEntity : PessoaJuridicaBase
    {
        public PessoaJuridicaMapping()
        {
            Property(x => x.Cnpj)
                .HasColumnName("CNPJ");

            Property(x => x.DataFundacao)
                .HasColumnName("DAT_FUNDACAO");
        }
    }
}
