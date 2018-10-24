using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestaoClinicaEstetica.Application.Models
{
    public class ListaEspecialidadeViewModel
    {
        public int IdObjeto { get; set; }
        public int CodProfissional { get; set; }
        public int CodEspecialidade { get; set; }        
        public string DescricaoEspecialidade { get; set; }
        public bool Selecionado { get; set; }
    }
}