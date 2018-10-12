using GestaoClinicaEstetica.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoClinicaEstetica.Context.Mapping
{
    public class RecebimentoServicoPorClienteMapping : BaseMapping<RecebimentoServicoPorCliente>
    {
        public RecebimentoServicoPorClienteMapping()
        {
            ToTable("RECEBIMENTOS");

            Property(x => x.CodigoServicoPorCliente)
                .HasColumnName("COD_SERVICO_POR_CLIENTE");

            Property(x => x.DataVencimento)
                .HasColumnName("DAT_VENCIMENTO");

            Property(x => x.DataPagamento)
                .HasColumnName("DAT_PAGAMENTO");

            Property(x => x.TipoPagamento)
                .HasColumnName("TIP_PAGAMENTO");

            Property(x => x.SituacaoPagamento)
                .HasColumnName("SIT_PAGAMENTO");

            Property(x => x.UsuarioRecebimento)
                .HasColumnName("USU_RECEBIMENTO");

            Property(x => x.ValorDevido)
                .HasColumnName("VLR_DEVIDO");

            Property(x => x.ValorRecebido)
                .HasColumnName("VLR_RECEBIDO");

            HasRequired<ServicoPorCliente>(x => x.ServicosPorCliente)
               .WithRequiredPrincipal(y => y.RecebimentoServicoPorCliente)
               .WillCascadeOnDelete(true);            
        }
    }
}
