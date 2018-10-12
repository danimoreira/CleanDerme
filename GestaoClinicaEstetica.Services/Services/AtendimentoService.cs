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
    public class AtendimentoService : ServiceBase<Atendimento>, IAtendimentoService
    {
        private IAtendimentoRepository _repository { get; set; }
        public AtendimentoService(IAtendimentoRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
