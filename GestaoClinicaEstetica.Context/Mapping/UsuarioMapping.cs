using GestaoClinicaEstetica.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoClinicaEstetica.Context.Mapping
{
    public class UsuarioMapping : BaseMapping<Usuario>
    {
        public UsuarioMapping()
        {
            ToTable("USUARIO");

            Property(x => x.Nome)
                .HasColumnName("NOME");

            Property(x => x.Login)
                .HasColumnName("LOGIN");

            Property(x => x.Senha)
                .HasColumnName("SENHA");
        }
    }
}
