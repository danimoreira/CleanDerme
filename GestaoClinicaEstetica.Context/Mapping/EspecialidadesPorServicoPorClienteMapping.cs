using GestaoClinicaEstetica.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoClinicaEstetica.Context.Mapping
{
    public class EspecialidadesPorServicoPorClienteMapping : BaseMapping<EspecialidadesPorServicoPorCliente>
    {
        public EspecialidadesPorServicoPorClienteMapping()
        {
            ToTable("ESPECIALIDADE_POR_SERVICO_POR_CLIENTE");

            Property(x => x.CodigoServicoPorCliente)
                .HasColumnName("COD_SERVICO_POR_CLIENTE");

            Property(x => x.CodigoEspecialidade)
                .HasColumnName("COD_ESPECIALIDADE");

            Property(x => x.Referencia)
                .HasColumnName("REFERENCIA");

            Property(x => x.QuantidadeSessoes)
                .HasColumnName("QTD_SESSOES");

            Property(x => x.QuantidadeSessoesRealizadas)
                .HasColumnName("QTD_SESSOES_REALIZADAS");

            HasRequired<ServicoPorCliente>(x => x.ServicoPorCliente)
               .WithMany(y => y.EspecialidadesPorServicoPorCliente)
               .HasForeignKey<int>(g => g.CodigoServicoPorCliente)
               .WillCascadeOnDelete(true);

            HasRequired<Especialidade>(x => x.Especialidade)
               .WithMany(y => y.EspecialidadesPorServicoPorCliente)
               .HasForeignKey<int>(g => g.CodigoEspecialidade)
               .WillCascadeOnDelete(true);
        }
    }
}
