using GestaoClinicaEstetica.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoClinicaEstetica.Context.Mapping
{
    public class EspecialidadePorProfissionalMapping : BaseMapping<EspecialidadePorProfissional>
    {
        public EspecialidadePorProfissionalMapping()
        {
            ToTable("ESPECIALIDADE_POR_PROFISSIONAL");

            Property(x => x.CodProfissional)
                .HasColumnName("COD_PROFISSIONAL");

            Property(x => x.CodEspecialidade)
                .HasColumnName("COD_ESPECIALIDADE");

            HasRequired<Profissional>(x => x.Profissional)
               .WithMany(y => y.EspecialidadePorProfissional)
               .HasForeignKey<int>(g => g.CodProfissional)
               .WillCascadeOnDelete(true);

            HasRequired<Especialidade>(x => x.Especialidade)
               .WithMany(y => y.EspecialidadePorProfissional)
               .HasForeignKey<int>(g => g.CodEspecialidade)
               .WillCascadeOnDelete(true);
        }
    }
}
