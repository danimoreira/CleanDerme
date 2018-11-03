using GestaoClinicaEstetica.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoClinicaEstetica.Context.Mapping
{
    public class EspecialidadeMapping : BaseMapping<Especialidade>
    {
        public EspecialidadeMapping()
        {
            ToTable("ESPECIALIDADE");

            Property(x => x.Descricao)
                .HasColumnName("DESCRICAO");

            Property(x => x.TipoAtendimento)
                .HasColumnName("TIP_ATENDIMENTO");

            Property(x => x.TempoAtendimentoPadrao)
                .HasColumnName("TMP_ATENDIMENTO_PADRAO");

            Property(x => x.CorEvento)
                .HasColumnName("TIP_COR_EVENTO")
                .HasColumnType("VARCHAR")
                .HasMaxLength(10);

            Property(x => x.PercentualRepasse)
                .HasColumnName("PRC_REPASSE");

        }
    }
}
