using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace APIOrientacao.Api.Request
{
    public class AlunoRequest
    {
        [Required(ErrorMessage = "O Id do aluno é obrigatório")]
        public int IdPessoa { get; set; }

        [Required(ErrorMessage = "A matrícula é obrigatória")]
        public string Matricula { get; set; }

        [Required(ErrorMessage = "O registro ativo do aluno é obrigatório")]
        public bool RegistroAtivo { get; set; }

        [Required(ErrorMessage = "O curso é obrigatório")]
        public int IdCurso { get; set; }

        public string Nome { get; set; }
        public string NomeCurso { get; set; }
    }
}
