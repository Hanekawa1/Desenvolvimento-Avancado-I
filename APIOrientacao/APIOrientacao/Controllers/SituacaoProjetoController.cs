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
    public class SituacaoProjetoController : Controller
    {
        private readonly Contexto contexto;

        public SituacaoProjetoController(Contexto contexto)
        {
            this.contexto = contexto;
        }

        [HttpPost]
        [ProducesResponseType(typeof(SituacaoProjetoResponse), 200)]
        [ProducesResponseType(400)]
        public IActionResult Post([FromBody] SituacaoProjetoRequest situacaoProjetoRequest)
        {
            var situacaoProjeto = new SituacaoProjeto
            {
                IdSituacao = situacaoProjetoRequest.IdSituacao,
                IdProjeto = situacaoProjetoRequest.IdProjeto,
                DataRegistro = situacaoProjetoRequest.DataRegistro
            };

            contexto.SituacaoProjeto.Add(situacaoProjeto);
            contexto.SaveChanges();

            var situacaoProjetoRetorno = contexto.SituacaoProjeto.Where(x => x.IdProjeto == situacaoProjeto.IdProjeto && x.IdSituacao == situacaoProjeto.IdSituacao).FirstOrDefault();

            SituacaoProjeto response = new SituacaoProjeto();

            if (situacaoProjetoRetorno != null)
            {
                response.IdProjeto = situacaoProjetoRetorno.IdProjeto;
                response.IdSituacao = situacaoProjetoRetorno.IdSituacao;
                response.DataRegistro = situacaoProjetoRetorno.DataRegistro;
            }

            return StatusCode(200, response);
        }

        [HttpGet("{idProjeto}/{idSituacao}")]
        [ProducesResponseType(typeof(SituacaoProjetoResponse), 200)]
        [ProducesResponseType(400)]
        public IActionResult Get(int idProjeto, int idSituacao)
        {
            var situacaoProjeto = contexto.SituacaoProjeto.FirstOrDefault(x => x.IdProjeto == idProjeto && x.IdSituacao == idSituacao);

            return StatusCode(situacaoProjeto == null ? 404 : 200, new SituacaoProjeto
            {
                IdProjeto = situacaoProjeto == null ? -1 : situacaoProjeto.IdProjeto,
                IdSituacao = situacaoProjeto == null ? -1 : situacaoProjeto.IdSituacao,
                DataRegistro = situacaoProjeto == null ? new DateTime() : situacaoProjeto.DataRegistro,
            });
        }

        [HttpPut("{idProjeto}/{idSituacao}")]
        [ProducesResponseType(typeof(SituacaoProjetoResponse), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Put(int idProjeto, int idSituacao, [FromBody] SituacaoProjetoRequest situacaoProjetoRequest)
        {
            try
            {
                var situacaoProjeto = contexto.SituacaoProjeto.Where(x => x.IdProjeto == idProjeto && x.IdSituacao == idSituacao).FirstOrDefault();

                if (situacaoProjeto != null)
                {
                    situacaoProjeto.DataRegistro = situacaoProjetoRequest.DataRegistro;
                    contexto.SaveChanges();
                }

                contexto.Entry(situacaoProjeto).State = EntityState.Modified;
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.InnerException.Message.FirstOrDefault());
            }

            var situacaoProjetoRetorno = contexto.SituacaoProjeto.FirstOrDefault(x => x.IdProjeto == idProjeto && x.IdSituacao == idSituacao);

            return StatusCode(200, new SituacaoProjetoResponse()
            {
                IdProjeto = situacaoProjetoRetorno.IdProjeto,
                IdSituacao = situacaoProjetoRetorno.IdSituacao,
                DataRegistro = situacaoProjetoRetorno.DataRegistro
            });
        }

        [HttpDelete("{idProjeto}/{idSituacao}")]
        [ProducesResponseType(400)]
        public IActionResult Delete(int idProjeto, int idSituacao)
        {
            try
            {
                var situacaoProjeto = contexto.SituacaoProjeto.FirstOrDefault(x => x.IdProjeto == idProjeto && x.IdSituacao == idSituacao);

                if (situacaoProjeto != null)
                {
                    contexto.SituacaoProjeto.Remove(situacaoProjeto);
                    contexto.SaveChanges();
                }

                return StatusCode(200, "Situação projeto excluído com sucesso!");
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.InnerException.Message.FirstOrDefault());
            }
        }
    }
}