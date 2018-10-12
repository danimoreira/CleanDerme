using GestaoClinicaEstetica.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoClinicaEstetica.Context.Mapping
{
    public class ServicoMapping : BaseMapping<Servico>
    {
        public ServicoMapping()
        {
            ToTable("SERVICO");

            Property(x => x.Descricao)
                .HasColumnName("DESCRICAO");

            Property(x => x.Periodicidade)
                .HasColumnName("PERIDIOCIDADE");

            Property(x => x.ValorServico)
                .HasColumnName("VLR_SERVICO");
    }
    }
}
