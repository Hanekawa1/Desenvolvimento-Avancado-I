using System;
using System.Collections.Generic;
using System.Text;

namespace APIOrientacao.Data
{
    public class Projeto
    {
        public int IdProjeto { get; set; }
        public int IdPessoa { get; set; }
        public string Nome { get; set; }
        public bool Encerrado { get; set; }
        public decimal Nota { get; set; }

        public Aluno Aluno { get; set; }
        public ICollection<SituacaoProjeto> SituacoesProjeto { get; set; }
        public ICollection<Orientacao> Orientacoes { get; set; }
    }
}
