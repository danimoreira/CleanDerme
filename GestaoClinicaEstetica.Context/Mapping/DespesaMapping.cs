using GestaoClinicaEstetica.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoClinicaEstetica.Context.Mapping
{
    public class DespesaMapping : BaseMapping<Despesa>
    {
        public DespesaMapping()
        {
            ToTable("DESPESAS");

            Property(x => x.Descricao)
                .HasColumnName("DSC_DESPESA");

            Property(x => x.Fornecedor)
                .HasColumnName("NOM_FORNECEDOR");

            Property(x => x.ValorDespesa)
                .HasColumnName("VAL_DESPESA");

            Property(x => x.DataVencimento)
                .HasColumnName("DAT_VENCIMENTO");

            Property(x => x.Situacao)
                .HasColumnName("SIT_DESPESA");

            Property(x => x.ValorPagamento)
                .HasColumnName("VAL_PAGAMENTO");

            Property(x => x.DataPagamento)
                .HasColumnName("DAT_PAGAMENTO");

            Property(x => x.TipoPagamento)
                .HasColumnName("TIP_PAGAMENTO");

            Property(x => x.UsuarioPagamento)
                .HasColumnName("USU_PAGAMENTO");            

            Property(x => x.Observacao)
                .HasColumnName("DSC_OBSERVACAO");
    }
    }
}
