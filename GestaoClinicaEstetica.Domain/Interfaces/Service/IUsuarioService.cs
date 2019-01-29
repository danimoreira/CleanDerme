using GestaoClinicaEstetica.Domain.Entidades;
using GestaoClinicaEstetica.Domain.Interfaces.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoClinicaEstetica.Domain.Interfaces.Service
{
    public interface IUsuarioService : IServiceBase<Usuario>
    {
        Usuario Logar(Usuario dados);
    }
}
