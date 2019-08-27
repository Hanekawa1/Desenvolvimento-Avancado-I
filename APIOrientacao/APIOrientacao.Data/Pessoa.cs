using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace APIOrientacao.Data
{
    public class Pessoa
    {
        public int IdPessoa { get; set; }
        [Required(ErrorMessage = "O nome é obrigatório!")]
        public string Nome { get; set; }
    }
}
