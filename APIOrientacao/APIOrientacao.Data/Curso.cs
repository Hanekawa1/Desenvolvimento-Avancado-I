using System;
using System.Collections.Generic;
using System.Text;

namespace APIOrientacao.Data
{
    public class Curso
    {
        public int IdCurso { get; set; }
        public string Nome { get; set; }

        public ICollection<Aluno> Alunos { get; set; }

        //Não é necessário no momento
        //Auxilia a estruturação e manipulação dos objetos
        public Curso()
        {
            Alunos = new HashSet<Aluno>();
        }
    }
}
