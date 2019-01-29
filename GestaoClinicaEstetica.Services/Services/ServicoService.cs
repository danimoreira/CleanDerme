using GestaoClinicaEstetica.Domain.Entidades;
using GestaoClinicaEstetica.Domain.Interfaces.Repository;
using GestaoClinicaEstetica.Domain.Interfaces.Service;
using GestaoClinicaEstetica.Services.Services.Base;

namespace GestaoClinicaEstetica.Services
{
    public class ServicoService : ServiceBase<Servico>, IServicoService
    {
        private IServicoRepository _repository;
        public ServicoService(IServicoRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
