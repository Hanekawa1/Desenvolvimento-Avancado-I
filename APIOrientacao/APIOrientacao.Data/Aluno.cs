using System;
using System.Collections.Generic;
using System.Text;

namespace APIOrientacao.Data
{
    public class Aluno
    {
        public int IdPessoa { get; set; }
        public string Matricula { get; set; }
        public bool RegistroAtivo { get; set; }
        public int IdCurso { get; set; }

        public Curso Curso { get; set; }
        public Pessoa Pessoa { get; set; }

        //Diferença entre Collection, List, Enumerable
    }
}
