﻿using GestaoClinicaEstetica.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoClinicaEstetica.Context.Mapping
{
    public class AgendaMapping : BaseMapping<Agenda>
    {
        public AgendaMapping()
        {
            ToTable("AGENDA");

            Property(x => x.CodigoCliente)
                .HasColumnName("COD_CLIENTE")
                .IsRequired();

            Property(x => x.CodigoEspecialidade)
                .HasColumnName("COD_ESPECIALIDADE")
                .IsRequired();

            Property(x => x.CodigoProfissional)
                .HasColumnName("COD_PROFISSIONAL")
                .IsRequired();

            Property(x => x.DataInicio)
                .HasColumnName("DAT_INICIO");

            Property(x => x.DataFim)
                .HasColumnName("DAT_FIM");

            Property(x => x.Procedimento)
                .HasColumnName("PROCEDIMENTO");

            Property(x => x.CodigoServico)
                .HasColumnName("COD_SERVICO");

            Property(x => x.ObservacaoPresenca)
                .HasColumnName("OBS_PRESENCA");

            Property(x => x.SituacaoPresenca)
                .HasColumnName("SIT_PRESENCA");

            Property(x => x.ObsAtendimento)
                .HasColumnName("OBS_ATENDIMENTO")
                .HasColumnType("VARCHAR")
                .HasMaxLength(5000);

            HasRequired<Cliente>(s => s.Cliente)
                .WithMany(x => x.Compromissos)
                .HasForeignKey<int>(s => s.CodigoCliente)
                .WillCascadeOnDelete(true);

            HasRequired<Especialidade>(s => s.Especialidade)
                .WithMany(x => x.Compromissos)
                .HasForeignKey<int>(s => s.CodigoEspecialidade)
                .WillCascadeOnDelete(true);

            HasRequired<Profissional>(s => s.Profissional)
                .WithMany(x => x.Compromissos)
                .HasForeignKey<int>(s => s.CodigoProfissional)
                .WillCascadeOnDelete(true);

            HasOptional<Servico>(s => s.Servico)
                .WithMany(x => x.Agenda)
                .HasForeignKey<int?>(s => s.CodigoServico)
                .WillCascadeOnDelete(true);

        }
    }
}
