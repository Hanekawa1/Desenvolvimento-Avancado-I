using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIOrientacao.Api.Request;
using APIOrientacao.Api.Response;
using APIOrientacao.Data;
using APIOrientacao.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIOrientacao.Controllers
{
    [Route("api/[controller]")]
    public class SituacaoController : Controller
    {
        private readonly Contexto contexto;

        public SituacaoController(Contexto contexto)
        {
            this.contexto = contexto;
        }

        [HttpPost]
        [ProducesResponseType(typeof(SituacaoResponse), 200)]
        [ProducesResponseType(400)]
        public IActionResult Post([FromBody] SituacaoRequest situacaoRequest)
        {
            var situacao = new Situacao
            {
                Descricao = situacaoRequest.Descricao
            };

            contexto.Situacao.Add(situacao);
            contexto.SaveChanges();

            var situacaoRetorno = contexto.Situacao.Where(x => x.IdSituacao == situacao.IdSituacao).FirstOrDefault();

            SituacaoResponse response = new SituacaoResponse();

            if(situacaoRetorno != null)
            {
                response.IdSituacao = situacaoRetorno.IdSituacao;
                response.Descricao = situacaoRetorno.Descricao;
            }

            return StatusCode(200, response);
        }

        [HttpGet("{idSituacao}")]
        [ProducesResponseType(typeof(SituacaoResponse), 200)]
        [ProducesResponseType(400)]
        public IActionResult Get(int idSituacao)
        {
            var situacao = contexto.Situacao.FirstOrDefault(x => x.IdSituacao == idSituacao);

            return StatusCode(situacao == null ? 404 : 200, new SituacaoResponse {
                IdSituacao = situacao == null ? -1 : situacao.IdSituacao,
                Descricao = situacao == null ? "Situação não encontrada" : situacao.Descricao
            });
        }

        [HttpPut("{idSituacao}")]
        [ProducesResponseType(typeof(SituacaoResponse), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Put(int idSituacao, [FromBody] SituacaoRequest situacaoRequest)
        {
            try
            {
                var situacao = contexto.Situacao.Where(x => x.IdSituacao == idSituacao).FirstOrDefault();

                if (situacao != null)
                {
                    situacao.Descricao = situacaoRequest.Descricao;
                    contexto.SaveChanges();
                }

                contexto.Entry(situacao).State = EntityState.Modified;
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.InnerException.Message.FirstOrDefault());
            }

            var situacaoRetorno = contexto.Situacao.FirstOrDefault(x => x.IdSituacao == idSituacao);

            return StatusCode(200, new SituacaoResponse()
            {
                Descricao = situacaoRetorno.Descricao,
                IdSituacao = situacaoRetorno.IdSituacao
            });
        }

        [HttpDelete("{idSituacao}")]
        [ProducesResponseType(400)]
        public IActionResult Delete(int idSituacao)
        {
            try
            {
                var situacao = contexto.Situacao.FirstOrDefault(x => x.IdSituacao == idSituacao);

                if (situacao != null)
                {
                    contexto.Situacao.Remove(situacao);
                    contexto.SaveChanges();
                }

                return StatusCode(200, "Curso excluído com sucesso!");
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.InnerException.Message.FirstOrDefault());
            }
        }
    }
}