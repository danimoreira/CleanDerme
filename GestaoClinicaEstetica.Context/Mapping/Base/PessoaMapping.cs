using GestaoClinicaEstetica.Domain.Entidades.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoClinicaEstetica.Context.Mapping
{
    public class PessoaMapping<TEntity> : BaseMapping<TEntity> where TEntity : PessoaBase
    {
        public PessoaMapping()
        {
            Property(x => x.Nome)
                .HasColumnName("NOME");

            Property(x => x.Endereco)
                .HasColumnName("ENDERECO");

            Property(x => x.Bairro)
                .HasColumnName("BAIRRO");

            Property(x => x.Cidade)
                .HasColumnName("CIDADE");

            Property(x => x.Uf)
                .HasColumnName("UF");

            Property(x => x.Cep)
                .HasColumnName("CEP");           

            Property(x => x.Email)
                .HasColumnName("EMAIL");           

            Property(x => x.TelefoneFixo)
                .HasColumnName("TEL_FIXO");

            Property(x => x.TelefoneCelular)
                .HasColumnName("TEL_CELULAR");           
            
        }
    }
}
