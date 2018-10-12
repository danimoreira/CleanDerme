using GestaoClinicaEstetica.Domain.Entidades.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoClinicaEstetica.Context.Mapping
{
    public class BaseMapping<TEntity> : EntityTypeConfiguration<TEntity> where TEntity : EntidadeBase
    {
        public BaseMapping()
        {
            HasKey(x => x.Id);
            Property(x => x.Id).HasColumnName("ID").IsRequired();
            Property(x => x.DataCadastro).HasColumnName("DATA_CADASTRO");
            Property(x => x.UsuarioCadastro).HasColumnName("USU_CADASTRO");
            Property(x => x.DataAlteracao).HasColumnName("DATA_ALTERACAO");
            Property(x => x.UsuarioAlteracao).HasColumnName("USU_ALTERACAO");
        }
    }
}
