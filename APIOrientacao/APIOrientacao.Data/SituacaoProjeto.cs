using System;
using System.Collections.Generic;
using System.Text;

namespace APIOrientacao.Data
{
    public class SituacaoProjeto
    {
        public int IdSituacao { get; set; }
        public int IdProjeto { get; set; }
        public DateTime DataRegistro { get; set; }

        public Projeto Projeto { get; set; }
        public Situacao Situacao { get; set; }
    }
}
