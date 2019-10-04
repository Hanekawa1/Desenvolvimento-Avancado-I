using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APIOrientacao.Api.Request
{
    public class ProjetoRequest
    {
        public int IdProjeto { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O campo Encerrado é obrigatório")]
        public bool Encerrado { get; set; }
        [Required(ErrorMessage = "O campo IdPessoa é obrigatório")]
        public int IdPessoa { get; set; }
        [Required(ErrorMessage = "O campo Nota é obrigatório")]
        public decimal Nota { get; set; }

        public string NomeAluno { get; set; }
    }
}
