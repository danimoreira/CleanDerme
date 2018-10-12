using GestaoClinicaEstetica.Domain.Entidades;
using GestaoClinicaEstetica.Domain.Interfaces.Repository;
using GestaoClinicaEstetica.Domain.Interfaces.Repository.Base;
using GestaoClinicaEstetica.Domain.Interfaces.Service;
using GestaoClinicaEstetica.Services.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoClinicaEstetica.Services.Services
{
    public class EspecialidadePorServicoService : ServiceBase<EspecialidadePorServico>, IEspecialidadePorServicoService
    {
        private IEspecialidadePorServicoRepository _repository;
        public EspecialidadePorServicoService(IEspecialidadePorServicoRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
