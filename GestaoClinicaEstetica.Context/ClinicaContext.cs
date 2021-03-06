﻿using GestaoClinicaEstetica.Context.Mapping;
using GestaoClinicaEstetica.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoClinicaEstetica.Context
{
    //[DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class ClinicaContext : DbContext
    {
        public ClinicaContext() : base("name=ClinicaConnString")
        {
        }

        public virtual DbSet<Agenda> Agenda { get; set; }
        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<Clinica> Clinica { get; set; }
        public virtual DbSet<Especialidade> Especialidade { get; set; }
        public virtual DbSet<Profissional> Profissional { get; set; }
        public virtual DbSet<RecebimentoServicoPorCliente> RecebimentoServicoPorCliente { get; set; }
        public virtual DbSet<Servico> Servico { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual EspecialidadePorProfissional EspecialidadePorProfissionals { get; set; }
        public virtual DbSet<Despesa> Despesas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AgendaMapping());
            modelBuilder.Configurations.Add(new ClienteMapping());
            modelBuilder.Configurations.Add(new ClinicaMapping());
            modelBuilder.Configurations.Add(new EspecialidadeMapping());
            modelBuilder.Configurations.Add(new ProfissionalMapping());
            modelBuilder.Configurations.Add(new RecebimentoServicoPorClienteMapping());
            modelBuilder.Configurations.Add(new ServicoMapping());
            modelBuilder.Configurations.Add(new UsuarioMapping());
            modelBuilder.Configurations.Add(new EspecialidadePorProfissionalMapping());
            modelBuilder.Configurations.Add(new DespesaMapping());

            base.OnModelCreating(modelBuilder);            
        }        
    }
}
