using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Exemplo
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");

            var lista = new List<string>();
            lista.Add("Olá!");
            lista.Add("Meu nome é");
            lista.Add("Wender");

            // Imprime o tipo do objeto;
            Console.WriteLine(lista);

            // Manipulação por iteração
            // Cria uma variável para iterar sobre a lista
            foreach(var item in lista)
            {
                Console.WriteLine(item);
            }

            // Inicializa uma variável para concatenação
            string mensagem = "";

            // Manipulação com concatenação
            foreach (var elemento in lista)
            {
                mensagem += elemento + " ";
            }

            Console.WriteLine(mensagem);

            // Mescla as listas iteradas
            var arrayTexto = new string[] { "Opala", "Gol GTS" , "Wender"};

            lista.AddRange(arrayTexto.ToList());


            string mensagem2 = "";

            foreach (var elemento in lista)
            {
                mensagem2 += elemento + " ";
            }

            Console.WriteLine(mensagem2);

            // Pega o primeiro elemento da lista
            Console.WriteLine("Primeiro item da lista: " + lista.First());

            // Pega o último item da lista
            Console.WriteLine("Último item da lista: " + lista.Last());

            // Compara as listas e retorna as exceções
            var excecao = lista.Except(arrayTexto);

            foreach(var ex in excecao)
            {
                Console.WriteLine("Exceção: " + ex);
            }

            // Procura elementos da lista que contém o parâmetro
            Console.WriteLine("Consulta por referência: " + lista.Contains("Wender"));

            // Inicia um dicionário (chave => valor)
            Dictionary<int, string> dicionario = new Dictionary<int, string>();

            for (int i = 1; i <= lista.Count(); i++)
            {
                dicionario.Add(i, lista[i-1]);
            }

            foreach(var elemento in dicionario)
            {
                Console.WriteLine("Key: " + elemento.Key + " | Value: " + elemento.Value);
            }

            // Lambda (manipulação de objetos por programação funcional)

            var retornoLista = dicionario.Where(x => x.Key == 2).Select(y => y.Value).FirstOrDefault();

            Console.WriteLine("Resultado da busca: " + retornoLista.ToString());

            var dataHoraAtual = DateTime.Now;
            var dataAtual = DateTime.Now.Date;
            var dia = DateTime.Now.Date.Day;
            var mes = DateTime.Now.Date.Month;
            var ano = DateTime.Now.Date.Year;
            var diaSemana = DateTime.Now.DayOfWeek;

            CultureInfo regiao = new CultureInfo("pt-BR");

            Console.WriteLine("Dia da semana: " + diaSemana);

            DateTimeFormatInfo diaSemanaPtBR = regiao.DateTimeFormat;
            Console.WriteLine("Dia da semana: " + diaSemanaPtBR.GetDayName(diaSemana));

            Console.ReadKey();
        }
    }
}
