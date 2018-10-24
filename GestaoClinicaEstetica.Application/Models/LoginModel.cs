using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GestaoClinicaEstetica.Application.Models
{
    [NotMapped]
    public class LoginModel
    {
        [NotMapped]
        [DisplayName("Usuário")]
        [Required(ErrorMessage = "Usuário/Senha inválidos!")]
        public string Usuario { get; set; }

        [NotMapped]
        [DisplayName("Senha")]
        [Required(ErrorMessage = "Usuário/Senha inválidos!")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
    }
}