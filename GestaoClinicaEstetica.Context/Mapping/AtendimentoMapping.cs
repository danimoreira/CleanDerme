using GestaoClinicaEstetica.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoClinicaEstetica.Context.Mapping
{
    public class AtendimentoMapping : BaseMapping<Atendimento>
    {
        public AtendimentoMapping()
        {
            ToTable("ATENDIMENTO");

            Property(x => x.CodigoAgenda)
                .HasColumnName("COD_AGENDA");

            Property(x => x.Observacao)
                .HasColumnName("OBSERVACAO");

            HasRequired<Agenda>(s => s.Agenda)
                .WithMany(g => g.Atendimento)
                .HasForeignKey<int>(x => x.CodigoAgenda)
                .WillCascadeOnDelete(true);
        }
    }
}
