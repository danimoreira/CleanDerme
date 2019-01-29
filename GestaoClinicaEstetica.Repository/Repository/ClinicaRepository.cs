using GestaoClinicaEstetica.Domain.Entidades;
using GestaoClinicaEstetica.Domain.Interfaces.Repository;
using GestaoClinicaEstetica.Repository.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoClinicaEstetica.Repository
{
    public class ClinicaRepository : RepositoryBase<Clinica>, IClinicaRepository
    {
    }
}
