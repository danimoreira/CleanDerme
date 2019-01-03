using GestaoClinicaEstetica.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoClinicaEstetica.Context.Mapping
{
    public class ClinicaMapping : PessoaJuridicaMapping<Clinica>
    {
        public ClinicaMapping()
        {
            ToTable("DADOS_CLINICA");

            Property(x => x.CorEventoAniversariantes)
                .HasColumnName("COR_EVENTO_ANIVERSARIANTE");

            Property(x => x.CorEventoRecebimentos)
                .HasColumnName("COR_EVENTO_RECEBIMENTOS");            
        }
    }
}
