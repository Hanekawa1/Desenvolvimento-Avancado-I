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
    public class AlunoController : Controller
    {
        private readonly Contexto contexto;

        public AlunoController(Contexto contexto)
        {
            this.contexto = contexto;
        }

        [HttpPost]
        [ProducesResponseType(typeof(AlunoResponse), 200)]
        [ProducesResponseType(400)]
        public IActionResult Post([FromBody] AlunoRequest alunoRequest)
        {
            var aluno = new Aluno
            {
                IdPessoa = alunoRequest.IdPessoa,
                RegistroAtivo = alunoRequest.RegistroAtivo,
                Matricula = alunoRequest.Matricula,
                IdCurso = alunoRequest.IdCurso
            };

            contexto.Aluno.Add(aluno);
            contexto.SaveChanges();

            var alunoRetorno = contexto.Aluno.Where(x => x.IdPessoa == aluno.IdPessoa).Include(i => i.Pessoa).FirstOrDefault();

            AlunoResponse response = new AlunoResponse();

            if(alunoRetorno != null)
            {
                response.IdPessoa = alunoRetorno.IdPessoa;
                response.RegistroAtivo = alunoRetorno.RegistroAtivo;
                response.Matricula = alunoRetorno.Matricula;
                response.IdCurso = alunoRetorno.IdCurso;
                response.Nome = alunoRetorno.Pessoa.Nome;
            }

            return StatusCode(200, response);
        }

        [HttpGet("{idPessoa}")]
        [ProducesResponseType(typeof(AlunoResponse), 200)]
        [ProducesResponseType(400)]
        public IActionResult Get(int idPessoa)
        {
            var aluno = contexto.Aluno.Where(x => x.IdPessoa == idPessoa)
                .Include(i => i.Pessoa)
                .Include(i => i.Curso)
                .FirstOrDefault();

            return StatusCode(aluno == null ? 404 : 200, new AlunoResponse
            {
                IdPessoa = aluno == null ? -1 : aluno.IdPessoa,
                RegistroAtivo = aluno == null ? false : true,
                Matricula = aluno == null ? "Matrícula não encontrada" : aluno.Matricula,
                IdCurso = aluno == null ? -1 : aluno.IdCurso,
                Nome = aluno == null ? "Aluno não encontrado" : aluno.Pessoa.Nome,
                NomeCurso = aluno == null ? "Curso não encontrado" : aluno.Curso.Nome
            });
        }

        [HttpPut]
        [ProducesResponseType(typeof(AlunoResponse), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Put(int idPessoa, [FromBody] AlunoRequest alunoRequest)
        {
            try
            {
                var aluno = contexto.Aluno.Where(x => x.IdPessoa == idPessoa).Include(i => i.Pessoa).FirstOrDefault();

                if(aluno != null)
                {
                    aluno.RegistroAtivo = alunoRequest.RegistroAtivo;
                    aluno.Matricula = alunoRequest.Matricula;
                    aluno.IdCurso = alunoRequest.IdCurso;
                    aluno.Pessoa.Nome = alunoRequest.Nome;
                }

                contexto.Entry(aluno).State = EntityState.Modified;
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.InnerException.Message.FirstOrDefault());
            }

            var alunoRetorno = contexto.Aluno.FirstOrDefault(x => x.IdPessoa == idPessoa);

            return StatusCode(200, new AlunoResponse()
            {
                IdPessoa = alunoRetorno.IdPessoa,
                RegistroAtivo = alunoRetorno.RegistroAtivo,
                Matricula = alunoRetorno.Matricula,
                IdCurso = alunoRetorno.IdCurso,
                Nome = alunoRetorno.Pessoa.Nome
            });
        }

        [HttpDelete("{idPessoa}")]
        [ProducesResponseType(400)]
        public IActionResult Delete(int idPessoa)
        {
            try
            {
                var aluno = contexto.Aluno.FirstOrDefault(x => x.IdPessoa == idPessoa);

                if(aluno != null)
                {
                    contexto.Remove(aluno);
                    contexto.SaveChanges();
                }

                return StatusCode(200, "Aluno excluído com sucesso!");
            }
            catch(Exception ex)
            {
                return StatusCode(400, ex.InnerException.Message.FirstOrDefault());
            }
        }

        [HttpDelete("{matricula}")]
        [ProducesResponseType(400)]
        public IActionResult DeleteByMatricula(string matricula)
        {
            try
            {
                var aluno = contexto.Aluno.FirstOrDefault(x => x.Matricula == matricula);

                if (aluno != null)
                {
                    contexto.Remove(aluno);
                    contexto.SaveChanges();
                }

                return StatusCode(200, "Aluno excluído com sucesso!");
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.InnerException.Message.FirstOrDefault());
            }
        }

        [HttpGet("{matricula}")]
        [ProducesResponseType(typeof(AlunoResponse), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetByMatricula(string matricula)
        {
            var aluno = contexto.Aluno.Where(x => x.Matricula == matricula)
                .Include(i => i.Pessoa).FirstOrDefault();

            return StatusCode(aluno == null ? 404 : 200, new AlunoResponse
            {
                IdPessoa = aluno == null ? -1 : aluno.IdPessoa,
                RegistroAtivo = aluno == null ? false : true,
                Matricula = aluno == null ? "Matrícula não encontrada" : aluno.Matricula,
                IdCurso = aluno == null ? -1 : aluno.IdCurso,
                Nome = aluno == null ? "Aluno não encontrado" : aluno.Pessoa.Nome,
                NomeCurso = aluno == null ? "Curso não encontrado" : aluno.Curso.Nome
            });
        }

        [HttpGet("{registroAtivo}")]
        [ProducesResponseType(typeof(List<AlunoResponse>), 200)]
        [ProducesResponseType(400)]
        public IActionResult GetByRegistroAtivo(int registroAtivo)
        {
            var ativo = registroAtivo == 1 ? true : false;

            var alunos = contexto.Aluno.Where(x => x.RegistroAtivo == ativo)
                .Include(i => i.Pessoa).ToList();

            return StatusCode(alunos == null ? 404 : 200, alunos.Select(x => new AlunoResponse
            {
                IdPessoa = alunos == null ? -1 : x.IdPessoa,
                RegistroAtivo = alunos == null ? false : true,
                Matricula = alunos == null ? "Matrícula não encontrada" : x.Matricula,
                IdCurso = alunos == null ? -1 : x.IdCurso,
                Nome = alunos == null ? "Aluno não encontrado" : x.Pessoa.Nome,
                NomeCurso = alunos == null ? "Curso não encontrado" : x.Curso.Nome
            }));
        }

        [HttpGet("{nomeCurso}")]
        [ProducesResponseType(typeof(List<AlunoResponse>), 200)]
        [ProducesResponseType(400)]
        public IActionResult Get(string nomeCurso)
        {

            var alunos = contexto.Aluno.Where(x => x.Curso.Nome == nomeCurso)
                .Include(i => i.Pessoa).ToList();

            return StatusCode(alunos == null ? 404 : 200, alunos.Select(x => new AlunoResponse
            {
                IdPessoa = alunos == null ? -1 : x.IdPessoa,
                RegistroAtivo = alunos == null ? false : true,
                Matricula = alunos == null ? "Matrícula não encontrada" : x.Matricula,
                IdCurso = alunos == null ? -1 : x.IdCurso,
                Nome = alunos == null ? "Aluno não encontrado" : x.Pessoa.Nome,
                NomeCurso = alunos == null ? "Curso não encontrado" : x.Curso.Nome
            }));
        }
    }
}