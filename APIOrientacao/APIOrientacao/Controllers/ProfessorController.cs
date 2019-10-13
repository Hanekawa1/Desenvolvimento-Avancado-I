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
    public class ProfessorController : Controller
    {
        private readonly Contexto contexto;

        public ProfessorController(Contexto contexto)
        {
            this.contexto = contexto;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ProfessorResponse), 200)]
        [ProducesResponseType(400)]
        public IActionResult Post([FromBody] ProfessorRequest professorRequest)
        {
            var professor = new Professor
            {
                IdPessoa = professorRequest.IdPessoa,
                RegistoAtivo = professorRequest.RegistroAtivo
            };

            contexto.Professor.Add(professor);
            contexto.SaveChanges();

            var professorRetorno = contexto.Professor.Where(x => x.IdPessoa == professor.IdPessoa).Include(i => i.Pessoa).FirstOrDefault();

            ProfessorResponse response = new ProfessorResponse();

            if(professorRetorno != null)
            {
                response.IdPessoa = professorRetorno.IdPessoa;
                response.RegistroAtivo = professorRetorno.RegistoAtivo;
                response.Nome = professorRetorno.Pessoa.Nome;
            }

            return StatusCode(200, response);
        }

        [HttpGet("{idPessoa}")]
        [ProducesResponseType(typeof(ProfessorResponse), 200)]
        [ProducesResponseType(400)]
        public IActionResult Get(int idPessoa)
        {
            var professor = contexto.Professor.Where(x => x.IdPessoa == idPessoa).Include(i => i.Pessoa).FirstOrDefault();

            return StatusCode(professor == null ? 404 : 200, new ProfessorResponse
            {
                IdPessoa = professor == null ? -1 : professor.IdPessoa,
                RegistroAtivo = professor == null ? false : true,
                Nome = professor == null ? "Professor não encontrado" : professor.Pessoa.Nome
            });
        }

        [HttpPut("{idPessoa}")]
        [ProducesResponseType(typeof(ProfessorResponse), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Put(int idPessoa, [FromBody] ProfessorRequest professorRequest)
        {
            try
            {
                var professor = contexto.Professor.Where(x => x.IdPessoa == idPessoa)
                    .Include(i => i.Pessoa).FirstOrDefault();

                if(professor != null)
                {
                    professor.RegistoAtivo = professorRequest.RegistroAtivo;
                    professor.Pessoa.Nome = professorRequest.Nome;
                    contexto.SaveChanges();
                }

                contexto.Entry(professor).State = EntityState.Modified;

            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.InnerException.Message.FirstOrDefault());
            }

            var professorRetorno = contexto.Professor.FirstOrDefault(x => x.IdPessoa == idPessoa);

            return StatusCode(200, new ProfessorResponse() {
                IdPessoa = professorRetorno.IdPessoa,
                RegistroAtivo = professorRetorno.RegistoAtivo
            });
        }

        [HttpDelete("{idPessoa}")]
        [ProducesResponseType(400)]
        public IActionResult Delete(int idPessoa)
        {
            try
            {
                var professor = contexto.Professor.FirstOrDefault(x => x.IdPessoa == idPessoa);

                if(professor != null)
                {
                    contexto.Professor.Remove(professor);
                    contexto.SaveChanges();
                }

                return StatusCode(200, "Professor excluído com sucesso!");
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.InnerException.Message.FirstOrDefault());
            }
        }
        //Rotas não funcionais
        /*
        [HttpGet("{nome}")]
        [ProducesResponseType(typeof(List<ProfessorResponse>), 200)]
        [ProducesResponseType(400)]
        public IActionResult GetByNome(string nome)
        {
            var professores = contexto.Professor.Where(x => x.Pessoa.Nome == nome)
                .Include(i => i.Pessoa).ToList();

            return StatusCode(professores == null ? 404 : 200, professores.Select (x => new ProfessorResponse
            {
                IdPessoa = professores == null ? -1 : x.IdPessoa,
                RegistroAtivo = professores == null ? false : true,
                Nome = professores == null ? "Professor não encontrado" : x.Pessoa.Nome
            }));
        }

        [HttpGet("{registroAtivo}")]
        [ProducesResponseType(typeof(List<ProfessorResponse>), 200)]
        [ProducesResponseType(400)]
        public IActionResult GetByRegistroAtivo(int registroAtivo)
        {
            var ativo = registroAtivo == 1 ? true : false;

            var professores = contexto.Professor.Where(x => x.RegistoAtivo == ativo)
                .Include(i => i.Pessoa).ToList();

            return StatusCode(professores == null ? 404 : 200, professores.Select(x => new ProfessorResponse
            {
                IdPessoa = professores == null ? -1 : x.IdPessoa,
                RegistroAtivo = professores == null ? false : true,
                Nome = professores == null ? "Professor não encontrado" : x.Pessoa.Nome
            }));
        }
        */
    }
}