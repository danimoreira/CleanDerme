using GestaoClinicaEstetica.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoClinicaEstetica.Context.Mapping
{
    public class PresencaMapping : BaseMapping<Presenca>
    {
        public PresencaMapping()
        {
            ToTable("PRESENCA_POR_CLIENTE");

            Property(x => x.CodigoAgenda)
                .HasColumnName("COD_AGENDA");

            Property(x => x.Observacao)
                .HasColumnName("OBSERVACAO");

            Property(x => x.SituacaoPresenca)
                .HasColumnName("SIT_PRESENCA");

            HasRequired<Agenda>(x => x.Agenda)
               .WithRequiredPrincipal(y => y.Presenca)
               .WillCascadeOnDelete(true);            
        }
    }
}
