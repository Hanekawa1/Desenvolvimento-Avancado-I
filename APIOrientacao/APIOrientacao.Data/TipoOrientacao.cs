using System;
using System.Collections.Generic;
using System.Text;

namespace APIOrientacao.Data
{
    public class TipoOrientacao
    {
        public int IdTipoOrientacao { get; set; }
        public string Descricao { get; set; }

        public ICollection<Orientacao> Orientacoes { get; set; }
    }
}
