using GestaoClinicaEstetica.Domain.Entidades;
using GestaoClinicaEstetica.Domain.Interfaces.Repository;
using GestaoClinicaEstetica.Domain.Interfaces.Repository.Base;
using GestaoClinicaEstetica.Domain.Interfaces.Service;
using GestaoClinicaEstetica.Services.Services.Base;

namespace GestaoClinicaEstetica.Services.Services
{
    public class ServicoPorClienteService : ServiceBase<ServicoPorCliente>, IServicoPorClienteService
    {
        private IServicoPorClienteRepository _repository;
        public ServicoPorClienteService(IServicoPorClienteRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
