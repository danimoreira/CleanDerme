using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoClinicaEstetica.Domain.Entidades.Base
{
    public class PessoaFisicaBase : PessoaBase
    {

        [Required(ErrorMessage = "Favor informar o CPF.", AllowEmptyStrings = false)]
        [Display(Name = "CPF")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "Favor informar a Data de Nascimento.", AllowEmptyStrings = false)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Data de Nascimento")]
        public DateTime? DataNascimento { get; set; }
    }
}
