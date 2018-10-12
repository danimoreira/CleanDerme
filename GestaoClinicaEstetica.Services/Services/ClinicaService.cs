using GestaoClinicaEstetica.Domain.Entidades;
using GestaoClinicaEstetica.Domain.Interfaces.Repository;
using GestaoClinicaEstetica.Domain.Interfaces.Service;
using GestaoClinicaEstetica.Services.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoClinicaEstetica.Services.Services
{
    public class ClinicaService : ServiceBase<Clinica>, IClinicaService
    {
        private IClinicaRepository _repository;
        public ClinicaService(IClinicaRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
