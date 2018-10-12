using GestaoClinicaEstetica.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoClinicaEstetica.Context.Mapping
{
    public class ServicoPorClienteMapping : BaseMapping<ServicoPorCliente>
    {
        public ServicoPorClienteMapping()
        {
            ToTable("SERVICO_POR_CLIENTE");

            Property(x => x.CodigoCliente)
                .HasColumnName("COD_CLIENTE");

            Property(x => x.CodigoServico)
                .HasColumnName("COD_SERVICO");

            Property(x => x.DataAquisicao)
                .HasColumnName("DAT_AQUISICAO");

            Property(x => x.Situacao)
                .HasColumnName("SIT_SERVICO");

            HasRequired<Cliente>(x => x.Cliente)
               .WithMany(y => y.ServicosPorCliente)
               .HasForeignKey<int>(g => g.CodigoCliente)
               .WillCascadeOnDelete(true);

            HasRequired<Servico>(x => x.Servico)
               .WithMany(y => y.ServicosPorCliente)
               .HasForeignKey<int>(g => g.CodigoServico)
               .WillCascadeOnDelete(true);            
        }
    }
}
