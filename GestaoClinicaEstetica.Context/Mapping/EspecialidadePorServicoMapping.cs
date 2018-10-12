using GestaoClinicaEstetica.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoClinicaEstetica.Context.Mapping
{
    public class EspecialidadePorServicoMapping : BaseMapping<EspecialidadePorServico>
    {
        public EspecialidadePorServicoMapping()
        {
            ToTable("ESPECIALIDADE_POR_SERVICO");

            Property(x => x.CodigoEspecialidade)
                .HasColumnName("COD_ESPECIALIDADE");

            Property(x => x.CodigoServico)
                .HasColumnName("COD_SERVICO");

            Property(x => x.QuantidadeSessoes)
                .HasColumnName("QTD_SESSOES");

            HasRequired<Servico>(x => x.Servico)
               .WithMany(y => y.EspecialidadesPorServico)
               .HasForeignKey<int>(g => g.CodigoServico)
               .WillCascadeOnDelete(true);

            HasRequired<Especialidade>(x => x.Especialidade)
               .WithMany(y => y.EspecialidadesPorServico)
               .HasForeignKey<int>(g => g.CodigoEspecialidade)
               .WillCascadeOnDelete(true);            
        }
    }
}
