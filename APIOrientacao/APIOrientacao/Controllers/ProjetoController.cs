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
    public class ProjetoController : Controller
    {
        private readonly Contexto contexto;

        public ProjetoController(Contexto contexto)
        {
            this.contexto = contexto;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ProjetoResponse), 200)]
        [ProducesResponseType(400)]
        public IActionResult Post([FromBody] ProjetoRequest projetoRequest)
        {
            var projeto = new Projeto
            {
                Nome = projetoRequest.Nome,
                Encerrado = projetoRequest.Encerrado,
                IdPessoa = projetoRequest.IdPessoa,
                Nota = projetoRequest.Nota
            };

            contexto.Projeto.Add(projeto);
            contexto.SaveChanges();

            var projetoRetorno = contexto.Projeto.Where(x => x.IdProjeto == projeto.IdProjeto)
                .Include(i => i.Aluno).ThenInclude(i => i.Pessoa)
                .FirstOrDefault();

            ProjetoResponse response = new ProjetoResponse();

            if (projetoRetorno != null)
            {
                response.Nome = projetoRetorno.Nome;
                response.Encerrado = projetoRetorno.Encerrado;
                response.Nota = projetoRetorno.Nota;
                response.NomeAluno = projetoRetorno.Aluno.Pessoa.Nome;
            }

            return StatusCode(200, response);
        }

        [HttpGet("{idProjeto}")]
        [ProducesResponseType(typeof(ProjetoResponse), 200)]
        [ProducesResponseType(400)]
        public IActionResult Get(int idProjeto)
        {
            var projeto = contexto.Projeto.Where(x => x.IdProjeto == idProjeto)
                .Include(i => i.Aluno).ThenInclude(i => i.Pessoa)
                .FirstOrDefault();

            return StatusCode(projeto == null ? 404 : 200, new ProjetoResponse
            {
                IdProjeto = projeto == null ? -1 : projeto.IdProjeto,
                Nome = projeto == null ? "Projeto não encontrado" : projeto.Nome,
                Encerrado = projeto == null ? false : projeto.Encerrado,
                IdPessoa = projeto == null ? -1 : projeto.IdPessoa,
                Nota = projeto == null ? 0 : projeto.Nota,
                NomeAluno = projeto == null ? "Não há aluno vinculado" : projeto.Aluno.Pessoa.Nome
            });
        }

        [HttpPut("{idProjeto}")]
        [ProducesResponseType(typeof(ProjetoResponse), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Put(int idProjeto, [FromBody] ProjetoRequest projetoRequest)
        {
            try
            {
                var projeto = contexto.Projeto.Where(x => x.IdProjeto == idProjeto)
                    .Include(i => i.Aluno).ThenInclude(i => i.Pessoa)
                    .FirstOrDefault();

                if (projeto != null)
                {
                    projeto.Nome = projetoRequest.Nome;
                    projeto.Encerrado = projetoRequest.Encerrado;
                    projeto.IdPessoa = projetoRequest.IdPessoa;
                    projeto.Nota = projetoRequest.Nota;
                    contexto.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.InnerException.Message.FirstOrDefault());
            }

            var projetoRetorno = contexto.Projeto.Where(x => x.IdProjeto == idProjeto)
                .Include(i => i.Aluno).ThenInclude(i => i.Pessoa)
                .FirstOrDefault();

            return StatusCode(200, new ProjetoResponse()
            {
                IdProjeto = projetoRetorno.IdProjeto,
                Nome = projetoRetorno.Nome,
                Encerrado = projetoRetorno.Encerrado,
                IdPessoa = projetoRetorno.IdPessoa,
                Nota = projetoRetorno.Nota,
                NomeAluno = projetoRetorno.Aluno.Pessoa.Nome
            });
        }

        [HttpDelete("{idProjeto}")]
        [ProducesResponseType(400)]
        public IActionResult Delete(int idProjeto)
        {
            try
            {
                var projeto = contexto.Projeto.FirstOrDefault(x => x.IdProjeto == idProjeto);

                if (projeto != null)
                {
                    contexto.Projeto.Remove(projeto);
                    contexto.SaveChanges();
                }

                return StatusCode(200, "Projeto excluído com sucesso!");
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.InnerException.Message.FirstOrDefault());
            }
        }

        [HttpGet("{Nome}")]
        [ProducesResponseType(typeof(List<ProjetoResponse>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetByNome(string nome)
        {
            var projetos = contexto.Projeto.Where(x => x.Nome == nome)
                .Include(i => i.Aluno).ThenInclude(i => i.Pessoa)
                .ToList();

            return StatusCode(projetos == null ? 404 : 200, projetos.Select( x => new ProjetoResponse {
                IdProjeto = projetos == null ? -1 : x.IdProjeto,
                Nome = projetos == null ? "Projeto não encontrado" : x.Nome,
                Encerrado = projetos == null ? false : x.Encerrado,
                IdPessoa = projetos == null ? -1 : x.IdPessoa,
                Nota = projetos == null ? 0 : x.Nota,
                NomeAluno = projetos == null ? "Não há aluno vinculado" : x.Aluno.Pessoa.Nome
            }));
        }

        [HttpGet("{Encerrado}")]
        [ProducesResponseType(typeof(List<ProjetoResponse>), 200)]
        [ProducesResponseType(400)]
        public IActionResult GetByEncerrado(int encerrado)
        {
            var encerradoBool = encerrado == 1 ? true : false;

            var projetos = contexto.Projeto.Where(x => x.Encerrado == encerradoBool)
                .Include(i => i.Aluno).ThenInclude(i => i.Pessoa)
                .ToList();

            return StatusCode(projetos == null ? 404 : 200, projetos.Select(x => new ProjetoResponse
            {
                IdProjeto = projetos == null ? -1 : x.IdProjeto,
                Nome = projetos == null ? "Projeto não encontrado" : x.Nome,
                Encerrado = projetos == null ? false : x.Encerrado,
                IdPessoa = projetos == null ? -1 : x.IdPessoa,
                Nota = projetos == null ? 0 : x.Nota,
                NomeAluno = projetos == null ? "Não há aluno vinculado" : x.Aluno.Pessoa.Nome
            }));
        }
    }
}