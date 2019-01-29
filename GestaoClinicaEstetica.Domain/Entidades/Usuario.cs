using GestaoClinicaEstetica.Domain.Entidades.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoClinicaEstetica.Domain.Entidades
{
    public class Usuario : EntidadeBase
    {
        [Required(ErrorMessage = "Favor informar o nome do usuário.", AllowEmptyStrings = false)]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Favor informar o login.", AllowEmptyStrings = false)]
        [Display(Name = "Login")]        
        public string Login { get; set; }

        [Required(ErrorMessage = "Favor informar a senha.", AllowEmptyStrings = false)]
        [StringLength(15, ErrorMessage = "A senha deve conter mais de 5 caracteres", MinimumLength = 5)]
        [DataType(DataType.Password)]        
        [Display(Name = "Senha")]
        public string Senha { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "As senhas digitadas não são iguais. Confirme novamente a senha.")]
        [DataType(DataType.Password)]
        [Compare("Senha")]
        public string ConfirmaSenha { get; set; }


    }
}
