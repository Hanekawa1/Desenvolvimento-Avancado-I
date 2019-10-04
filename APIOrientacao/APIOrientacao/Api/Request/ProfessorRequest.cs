using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APIOrientacao.Api.Request
{
    public class ProfessorRequest
    {
        [Required(ErrorMessage ="O id da pessoa é obrigatório")]
        public int IdPessoa { get; set; }

        [Required(ErrorMessage = "O registro é obrigatório")]
        public bool RegistroAtivo { get; set; }

        public string Nome { get; set; }
    }
}
