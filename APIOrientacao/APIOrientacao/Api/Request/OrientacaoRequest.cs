using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APIOrientacao.Api.Request
{
    public class OrientacaoRequest
    {
        [Required(ErrorMessage = "O campo IdProjeto é obrigatório")]
        public int IdProjeto { get; set; }
        [Required(ErrorMessage = "O campo IdPessoa é obrigatório")]
        public int IdPessoa { get; set; }
        [Required(ErrorMessage = "O campo IdTipoOrientacao é obrigatório")]
        public int IdTipoOrientacao { get; set; }
        [Required(ErrorMessage = "O campo DataRegistro é obrigatório")]
        public DateTime DataRegistro { get; set; }

        public string NomeProfessor { get; set; }
    }
}
