using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APIOrientacao.Api.Request
{
    public class SituacaoRequest
    {
        [Required(ErrorMessage = "O campo descrição é obrigatório")]
        public string Descricao { get; set; }
    }
}
