using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIOrientacao.Api.Response
{
    public class SituacaoProjetoResponse
    {
        public int IdSituacao { get; set; }
        public int IdProjeto { get; set; }
        public DateTime DataRegistro { get; set; }
    }
}
