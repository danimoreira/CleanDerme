using GestaoClinicaEstetica.Domain.Entidades;
using GestaoClinicaEstetica.Domain.Interfaces.Repository;
using GestaoClinicaEstetica.Domain.Interfaces.Service;
using GestaoClinicaEstetica.Services.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoClinicaEstetica.Services
{
    public class AgendaService : ServiceBase<Agenda>, IAgendaService
    {
        private IAgendaRepository _repository { get; set; }

        public AgendaService(IAgendaRepository repository) : base (repository)
        {
            _repository = repository;
        }
    }
}
