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
    public class OrientacaoController : Controller
    {
        private readonly Contexto contexto;

        public OrientacaoController(Contexto contexto)
        {
            this.contexto = contexto;
        }

        [HttpPost]
        [ProducesResponseType(typeof(OrientacaoResponse), 200)]
        [ProducesResponseType(400)]
        public IActionResult Post([FromBody] OrientacaoRequest orientacaoRequest)
        {
            var orientacao = new Orientacao
            {
                IdPessoa = orientacaoRequest.IdPessoa,
                IdProjeto = orientacaoRequest.IdProjeto,
                IdTipoOrientacao = orientacaoRequest.IdTipoOrientacao,
                DataRegistro = orientacaoRequest.DataRegistro
            };

            contexto.Orientacao.Add(orientacao);
            contexto.SaveChanges();

            var orientacaoRetorno = contexto.Orientacao.Where(x => x.IdProjeto == orientacaoRequest.IdProjeto && x.IdPessoa == orientacaoRequest.IdPessoa)
                .Include(i => i.Professor).ThenInclude(i => i.Pessoa)
                .FirstOrDefault();

            OrientacaoResponse response = new OrientacaoResponse();

            if (orientacaoRetorno != null)
            {
                response.IdProjeto = orientacaoRetorno.IdProjeto;
                response.IdPessoa = orientacaoRetorno.IdPessoa;
                response.IdTipoOrientacao = orientacaoRetorno.IdTipoOrientacao;
                response.NomeProfessor = orientacaoRetorno.Professor.Pessoa.Nome;
            }

            return StatusCode(200, response);
        }

        [HttpGet("{idProjeto}/{idPessoa}")]
        [ProducesResponseType(typeof(OrientacaoResponse), 200)]
        [ProducesResponseType(400)]
        public IActionResult Get(int idProjeto, int idPessoa)
        {
            var orientacao = contexto.Orientacao.Where(x => x.IdProjeto == idProjeto && x.IdPessoa == idPessoa)
                .Include(i => i.Professor).ThenInclude(i => i.Pessoa)
                .FirstOrDefault();

            return StatusCode(orientacao == null ? 404 : 200, new OrientacaoResponse {
                IdPessoa = orientacao == null ? -1 : orientacao.IdPessoa,
                IdProjeto = orientacao == null ? -1 : orientacao.IdProjeto,
                IdTipoOrientacao = orientacao == null ? -1 : orientacao.IdTipoOrientacao,
                DataRegistro = orientacao == null ? new DateTime() : orientacao.DataRegistro,
                NomeProfessor = orientacao == null ? "Orientador não encontrado" : orientacao.Professor.Pessoa.Nome
            });

        }

        [HttpPut("{idProjeto}/{idPessoa}")]
        [ProducesResponseType(typeof(OrientacaoResponse), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Put(int idProjeto, int idPessoa, [FromBody] OrientacaoRequest orientacaoRequest)
        {
            try
            {
                var orientacao = contexto.Orientacao.Where(x => x.IdProjeto == idProjeto && x.IdPessoa == idPessoa)
                    .Include(i => i.Professor).ThenInclude(i => i.Pessoa)
                    .FirstOrDefault();

                if(orientacao != null)
                {
                    orientacao.IdPessoa = orientacaoRequest.IdPessoa;
                    orientacao.IdProjeto = orientacaoRequest.IdProjeto;
                    orientacao.IdTipoOrientacao = orientacaoRequest.IdTipoOrientacao;
                    orientacao.DataRegistro = orientacaoRequest.DataRegistro;
                    contexto.SaveChanges();
                }

                contexto.Entry(orientacao).State = EntityState.Modified;
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.InnerException.Message.FirstOrDefault());
            }

            var orientacaoRetorno = contexto.Orientacao.Where(x => x.IdProjeto == idProjeto && x.IdPessoa == idPessoa)
                    .Include(i => i.Professor).ThenInclude(i => i.Pessoa)
                    .FirstOrDefault();

            return StatusCode(200, new OrientacaoResponse() {
                IdPessoa = orientacaoRetorno.IdPessoa,
                IdProjeto = orientacaoRetorno.IdProjeto,
                IdTipoOrientacao = orientacaoRetorno.IdTipoOrientacao,
                DataRegistro = orientacaoRetorno.DataRegistro,
                NomeProfessor = orientacaoRetorno.Professor.Pessoa.Nome
            });
        }

        [HttpDelete("{idProjeto}/{idPessoa}")]
        [ProducesResponseType(400)]
        public IActionResult Delete(int idProjeto, int idPessoa)
        {
            try
            {
                var orientacao = contexto.Orientacao.Where(x => x.IdProjeto == idProjeto && x.IdPessoa == idPessoa)
                 .FirstOrDefault();

                if (orientacao != null)
                {
                    contexto.Orientacao.Remove(orientacao);
                    contexto.SaveChanges();
                }

                return StatusCode(200, "Orientação excluída com sucesso!");
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.InnerException.Message.FirstOrDefault());
            }
        }
    }
}