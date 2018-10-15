using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoClinicaEstetica.Domain.Entidades.Base
{
    public class EntidadeBase
    {
        [Display(Name = "Id")]
        public int Id { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Data de Cadastro")]
        public DateTime DataCadastro { get; set; }
        [Display(Name = "Usuário de Cadastro")]
        public string UsuarioCadastro { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Data de Alteração")]
        public DateTime DataAlteracao { get; set; }
        [Display(Name = "Usuário de Alteração")]
        public string UsuarioAlteracao { get; set; }
    }
}
