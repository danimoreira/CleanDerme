using GestaoClinicaEstetica.Domain.Entidades;
using GestaoClinicaEstetica.Domain.Interfaces.Repository;
using GestaoClinicaEstetica.Domain.Interfaces.Repository.Base;
using GestaoClinicaEstetica.Domain.Interfaces.Service;
using GestaoClinicaEstetica.Services.Services.Base;

namespace GestaoClinicaEstetica.Services.Services
{
    public class EspecialidadesPorServicoPorClienteService : ServiceBase<EspecialidadesPorServicoPorCliente>, IEspecialidadesPorServicoPorClienteService
    {
        private IEspecialidadesPorServicoPorClienteRepository _repository;
        public EspecialidadesPorServicoPorClienteService(IEspecialidadesPorServicoPorClienteRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
