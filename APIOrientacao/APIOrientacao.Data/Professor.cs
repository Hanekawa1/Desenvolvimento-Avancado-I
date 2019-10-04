using System;
using System.Collections.Generic;
using System.Text;

namespace APIOrientacao.Data
{
    public class Professor
    {

        public int IdPessoa { get; set; }
        public bool RegistoAtivo { get; set; }

        public Pessoa Pessoa { get; set; }

        public ICollection<Orientacao> Orientacoes { get; set; }
    }
}
