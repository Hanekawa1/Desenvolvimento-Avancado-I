using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIOrientacao.Api.Request;
using APIOrientacao.Api.Response;
using APIOrientacao.Data.Context;
using Microsoft.AspNetCore.Mvc;

namespace APIOrientacao.Controllers
{
    [Route("api/[controller]")]
    public class PessoaController : Controller
    {
        private readonly Contexto contexto;

        public PessoaController(Contexto contexto)
        {
            this.contexto = contexto;
        }

        [HttpPost] //Tipo de operação
        [ProducesResponseType(typeof(PessoaResponse), 200)] //Tipo de resposta com o Status Code
        [ProducesResponseType(400)] //Em caso de erro
        public IActionResult Post([FromBody] PessoaRequest pessoaRequest)
        {
            PessoaResponse response = new PessoaResponse();
            return StatusCode(200, response);
        }
    }
}
