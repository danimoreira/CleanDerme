using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoClinicaEstetica.Domain.Dto
{
    public class EventosDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public bool AllDay { get; set; }
        public string Url { get; set; }
        public string ClassName { get; set; }
        public string Color { get; set; }
        public string BackgroundColor { get; set; }
        public string BorderColor { get; set; }
        public string TextColor { get; set; }
        public string Icone { get; set; }
        public int TipoEvento { get; set; }
        public int CodigoCliente { get; set; }
        public int CodigoRecebimento { get; set; }
    }
}
