using APIOrientacao.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIOrientacao.Api.Response
{
    public class ProfessorResponse
    {
        public int IdPessoa { get; set; }
        public bool RegistroAtivo { get; set; }
        public string Nome { get; set; }
    }
}
