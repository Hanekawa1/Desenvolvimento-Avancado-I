using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIOrientacao.Api.Response
{
    public class OrientacaoResponse
    {
        public int IdProjeto { get; set; }
        public int IdPessoa { get; set; }
        public int IdTipoOrientacao { get; set; }
        public DateTime DataRegistro { get; set; }
        public string NomeProfessor { get; set; }
    }
}
