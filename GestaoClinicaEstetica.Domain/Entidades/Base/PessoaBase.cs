using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoClinicaEstetica.Domain.Entidades.Base
{
    public class PessoaBase : EntidadeBase
    {
        [Required(ErrorMessage = "Favor informar o Nome.", AllowEmptyStrings = false)]
        [Display(Name = "Nome")]
        public string Nome { get; set; }
        [Display(Name = "Endereço")]
        public string Endereco { get; set; }
        [Display(Name = "Bairro")]
        public string Bairro { get; set; }
        [Display(Name = "Cidade")]
        public string Cidade { get; set; }        
        [Display(Name = "UF")]
        public string Uf { get; set; }
        [Display(Name = "CEP")]
        public string Cep { get; set; }
        [Display(Name = "Telefone Fixo")]
        public string TelefoneFixo { get; set; }

        [Required(ErrorMessage = "Favor informar o Telefone Celular.", AllowEmptyStrings = false)]
        [Display(Name = "Telefone Celular")]
        public string TelefoneCelular { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
