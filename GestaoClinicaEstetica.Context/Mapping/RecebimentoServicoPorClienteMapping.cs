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

            Property(x => x.CodigoCliente)
                .HasColumnName("COD_CLIENTE");

            Property(x => x.CodigoServico)
                .HasColumnName("COD_SERVICO");

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

            Property(x => x.DataAquisicao)
                .HasColumnName("DAT_AQUISICAO");
        }
    }
}
