using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APIOrientacao.Api.Request
{
    public class PessoaRequest
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        public string Nome { get; set; }
    }
}
