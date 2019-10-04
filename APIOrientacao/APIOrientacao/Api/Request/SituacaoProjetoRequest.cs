using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APIOrientacao.Api.Request
{
    public class SituacaoProjetoRequest
    {
        [Required(ErrorMessage = "O campo IdSituacao é obrigatório")]
        public int IdSituacao { get; set; }
        [Required(ErrorMessage = "O campo IdProjeto é obrigatório")]
        public int IdProjeto { get; set; }

        public DateTime DataRegistro { get; set; }
    }
}
