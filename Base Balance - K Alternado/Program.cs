using ConsoleApp1.Classes;
using ConsoleApp1.Funções;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base_Balance___K_Alternado
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Balance> balances = new List<Balance>();
            int k = 2;
            List<Indicadores> Resultados = new List<Indicadores>(); //guarda os resultados dos testes de todas as 30 rodadas para calcular a media e o desvio padrao


            // LEITURA DE ARQUIVOS

            Console.WriteLine("Iniciando... <<<<<   base Balance  >>>>>\n\n\n");
            using (StreamReader reader = new StreamReader("C:\\Users\\Prestes-Noot\\Desktop\\Computação Avançada\\base_balance.txt"))
            {
                while (!reader.EndOfStream)
                {
                    string linha = reader.ReadLine();

                    Balance balance = new Balance();

                    string[] valores = linha.Split(',');

                    balance.classe = valores[0];
                    balance.leftWeight = Convert.ToSingle(valores[1]);
                    balance.leftDistance = Convert.ToSingle(valores[2]);
                    balance.rightWeight = Convert.ToSingle(valores[3]);
                    balance.rightDistance = Convert.ToSingle(valores[4]);

                    balances.Add(balance);
                }
            }
            /*
            int i = 0;
            for(i=0;i<flores.Count;i++)
            {
                Console.WriteLine(flores[]);
            }
            */


            for (int contador = 1; contador <= 30; contador++)
            {
                List<Balance> l = new List<Balance>();
                List<Balance> b = new List<Balance>();
                List<Balance> r = new List<Balance>();
                List<Balance> z1 = new List<Balance>();
                List<Balance> z2 = new List<Balance>();
                List<Balance> z3 = new List<Balance>();

                foreach (var divisao in balances)
                {
                    if (divisao.classe == "L")
                    {
                        l.Add(divisao);
                        continue;
                    }
                    if (divisao.classe == "B")
                    {
                        b.Add(divisao);
                        continue;
                    }
                    if (divisao.classe == "R")
                    {
                        r.Add(divisao);
                    }
                }

                Random randNum = new Random();
                Balance balance;

                while (z1.Count() < 72)
                {
                    balance = l.ElementAt(randNum.Next(l.Count() - 1));
                    if (!balance.usado)
                    {
                        balance.usado = true;
                        z1.Add(balance);
                    }
                }
                while (z2.Count() < 72)
                {
                    balance = l.ElementAt(randNum.Next(l.Count() - 1));
                    if (!balance.usado)
                    {
                        balance.usado = true;
                        z2.Add(balance);
                    }
                }
                while (z3.Count() < 144)
                {
                    balance = l.Where(c => c.usado == false).First();
                    balance.usado = true;
                    z3.Add(balance);

                }
                while (z1.Count() < 84)
                {
                    balance = b.ElementAt(randNum.Next(b.Count() - 1));
                    if (!balance.usado)
                    {
                        balance.usado = true;
                        z1.Add(balance);
                    }
                }

                while (z2.Count() < 84)
                {
                    balance = b.Where(c => c.usado == false).First();
                    balance.usado = true;
                    z2.Add(balance);
                }

                while (z3.Count() < 169)
                {
                    balance = b.Where(c => c.usado == false).First();
                    balance.usado = true;
                    z3.Add(balance);
                }
                while (z1.Count() < 156)
                {
                    balance = r.ElementAt(randNum.Next(r.Count() - 1));
                    if (!balance.usado)
                    {
                        balance.usado = true;
                        z1.Add(balance);
                    }
                }
                while (z2.Count() < 156)
                {
                    balance = r.Where(c => c.usado == false).First();
                    balance.usado = true;
                    z2.Add(balance);
                }
                while (z3.Count() < 313)
                {
                    balance = r.Where(c => c.usado == false).First();
                    balance.usado = true;
                    z3.Add(balance);
                }

                // percorre todos os elementos e compara se a classe que o classificador retornou realmente está certa
                // se a classe estiver errada, ele marca como errada
                string[] classeObtida = Functions.ClassificadorDeAmostras(z1, z2, k);
                int posicao = 0;
                Balance auxTroca1 = null, auxTroca2 = null;

                foreach (var verificacaoDosAcertos in z1)
                {
                    if (verificacaoDosAcertos.classe != classeObtida[posicao])
                    {
                        verificacaoDosAcertos.errado = true;
                    }
                    posicao++;
                }

                while (z1.Any(e => e.errado == true))
                {
                    auxTroca1 = z1.Where(e => e.errado == true).First();
                    auxTroca2 = z2.Where(c => c.classe == auxTroca1.classe).First();
                    z1.Remove(auxTroca1);
                    z2.Remove(auxTroca2);
                    auxTroca1.trocado = true;
                    auxTroca1.errado = false;
                    auxTroca2.trocado = true;
                    z1.Add(auxTroca2);
                    z2.Add(auxTroca1);
                }

                foreach (var limparz1 in z1)
                {
                    limparz1.trocado = false;
                    limparz1.usado = false;
                }

                foreach (var limparz2 in z2)
                {
                    limparz2.trocado = false;
                    limparz2.usado = false;
                }

                classeObtida = Functions.ClassificadorDeAmostras(z3, z2, k);
                posicao = 0;
                int acertos = 0;
                double taxaDeAcertos = 0;

                foreach (var classificadorAcertosz3 in z3)
                {
                    if (classificadorAcertosz3.classe == classeObtida[posicao])
                    {
                        acertos++;
                    }
                    posicao++;
                }

                taxaDeAcertos = (acertos * 100) / z3.Count(); //regra de 3 para definir a porcentagem de acertos
                Indicadores indicador = new Indicadores(acertos, taxaDeAcertos);
                Resultados.Add(indicador);

                Console.WriteLine("<<<<<   Rodada" + contador + "   >>>>>" + "...\n" + "Taxa de Acerto: " + taxaDeAcertos + "%" + "\nK: "+ k +"\n");
                foreach (var limpezaFlores in balances)
                {
                    limpezaFlores.usado = false;
                }
                l = null;
                r = null;
                b = null;
                z1 = null;
                z2 = null;
                z3 = null;
                k++;
                // aqui viria a alteração para o k ficar alternando
                // adicionar um k++
            }
            double soma = 0, media, desvioPadrao;
            foreach (var baseParaCalculo in Resultados)
            {
                soma += baseParaCalculo.taxaAcertos;
            }
            media = soma / Resultados.Count();
            soma = 0;

            foreach (var baseParaCalculo in Resultados)
            {
                soma += Math.Pow((baseParaCalculo.taxaAcertos - media), 2);
            }
            desvioPadrao = Math.Sqrt(soma / Resultados.Count());

            Console.WriteLine("\n\n\n<<<<<  Média: " + media + "           >>>>>");
            Console.WriteLine("<<<<<  Desvio padrão: " + desvioPadrao + "   >>>>>");

            Console.ReadKey();
        }
    }
}
