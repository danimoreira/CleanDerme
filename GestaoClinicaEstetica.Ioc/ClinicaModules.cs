using GestaoClinicaEstetica.Domain.Interfaces.Repository;
using GestaoClinicaEstetica.Domain.Interfaces.Service;
using GestaoClinicaEstetica.Repository.Repository;
using GestaoClinicaEstetica.Services.Services;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Common;

namespace GestaoClinicaEstetica.Ioc
{
    public class ClinicaModules : NinjectModule
    {        
        public override void Load()
        {
            Bind<IAgendaRepository>().To<AgendaRepository>();
            Bind<IClienteRepository>().To<ClienteRepository>();
            Bind<IClinicaRepository>().To<ClinicaRepository>();
            Bind<IEspecialidadeRepository>().To<EspecialidadeRepository>();
            Bind<IProfissionalRepository>().To<ProfissionalRepository>();
            Bind<IRecebimentoServicoPorClienteRepository>().To<RecebimentoServicoPorClienteRepository>();
            Bind<IServicoRepository>().To<ServicoRepository>();
            Bind<IUsuarioRepository>().To<UsuarioRepository>();
            Bind<IEspecialidadePorProfissionalRepository>().To<EspecialidadePorProfissionalRepository>();

            Bind<IAgendaService>().To<AgendaService>();
            Bind<IClienteService>().To<ClienteService>();
            Bind<IClinicaService>().To<ClinicaService>();
            Bind<IEspecialidadeService>().To<EspecialidadeService>();
            Bind<IProfissionalService>().To<ProfissionalService>();
            Bind<IRecebimentoServicoPorClienteService>().To<RecebimentoServicoPorClienteService>();
            Bind<IServicoService>().To<ServicoService>();
            Bind<IUsuarioService>().To<UsuarioService>();
            Bind<IEspecialidadePorProfissionalService>().To<EspecialidadePorProfissionalService>();
        }        
    }
}
