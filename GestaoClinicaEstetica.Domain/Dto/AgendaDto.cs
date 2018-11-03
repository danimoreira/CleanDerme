using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoClinicaEstetica.Domain.Dto
{
    public class AgendaDto
    {
        public int Id { get; set; }
        public int CodigoProfissional { get; set; }
        public string NomeProfissional { get; set; }
        public int CodigoEspecialidade { get; set; }
        public string DescricaoEspecialidade { get; set; }
        public int CodigoCliente { get; set; }
        public string NomeCliente { get; set; }
        public DateTime DataInicioEvento { get; set; }
        public DateTime DataFimEvento { get; set; }
        public string Procedimento { get; set; }
    }
}
