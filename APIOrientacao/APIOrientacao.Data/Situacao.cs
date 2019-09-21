using System;
using System.Collections.Generic;
using System.Text;

namespace APIOrientacao.Data
{
    public class Situacao
    {
        public int IdSituacao { get; set; }
        public string Descricao { get; set; }

        public ICollection<SituacaoProjeto> SituacoesProjeto { get; set; }
    }
}
