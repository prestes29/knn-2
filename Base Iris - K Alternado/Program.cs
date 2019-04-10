using ConsoleApp1.Classes;
using ConsoleApp1.Funções;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base_Iris___K_Alternado
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Flor> flores = new List<Flor>();
            int k = 1;
            List<Indicadores> Resultados = new List<Indicadores>(); //guarda os resultados dos testes de todas as 30 rodadas para calcular a media e o desvio padrao


            // LEITURA DE ARQUIVOS

            Console.WriteLine("Iniciando base IRIS");
            using (StreamReader reader = new StreamReader("C:\\Users\\Prestes-Noot\\Desktop\\Computação Avançada\\dados.txt"))
            {
                while (!reader.EndOfStream)
                {
                    string linha = reader.ReadLine();

                    Flor flor = new Flor();

                    string[] valores = linha.Split(',');

                    flor.comprimento_sepala = Convert.ToSingle(valores[0]);
                    flor.largura_sepala = Convert.ToSingle(valores[1]);
                    flor.comprimento_petala = Convert.ToSingle(valores[2]);
                    flor.largura_petala = Convert.ToSingle(valores[3]);
                    flor.classe = valores[4];

                    flores.Add(flor);
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
                List<Flor> setosa = new List<Flor>();
                List<Flor> versicolor = new List<Flor>();
                List<Flor> virginica = new List<Flor>();
                List<Flor> z1 = new List<Flor>();
                List<Flor> z2 = new List<Flor>();
                List<Flor> z3 = new List<Flor>();

                foreach (var divisao in flores)
                {
                    if (divisao.classe == "Iris-setosa")
                    {
                        setosa.Add(divisao);
                        continue;
                    }
                    if (divisao.classe == "Iris-versicolor")
                    {
                        versicolor.Add(divisao);
                        continue;
                    }
                    if (divisao.classe == "Iris-virginica")
                    {
                        virginica.Add(divisao);
                    }
                }

                Random randNum = new Random();
                Flor flor;

                while (z1.Count() < 13)
                {
                    flor = setosa.ElementAt(randNum.Next(setosa.Count() - 1));
                    if (!flor.usado)
                    {
                        flor.usado = true;
                        z1.Add(flor);
                    }
                }
                while (z2.Count() < 13)
                {
                    flor = setosa.ElementAt(randNum.Next(setosa.Count() - 1));
                    if (!flor.usado)
                    {
                        flor.usado = true;
                        z2.Add(flor);
                    }
                }
                while (z3.Count() < 24)
                {
                    flor = setosa.Where(c => c.usado == false).First();
                    flor.usado = true;
                    z3.Add(flor);

                }
                while (z1.Count() < 26)
                {
                    flor = versicolor.ElementAt(randNum.Next(versicolor.Count() - 1));
                    if (!flor.usado)
                    {
                        flor.usado = true;
                        z1.Add(flor);
                    }
                }

                while (z2.Count() < 26)
                {
                    flor = versicolor.Where(c => c.usado == false).First();
                    flor.usado = true;
                    z2.Add(flor);
                }

                while (z3.Count() < 48)
                {
                    flor = versicolor.Where(c => c.usado == false).First();
                    flor.usado = true;
                    z3.Add(flor);
                }
                while (z1.Count() < 38)
                {
                    flor = virginica.ElementAt(randNum.Next(virginica.Count() - 1));
                    if (!flor.usado)
                    {
                        flor.usado = true;
                        z1.Add(flor);
                    }
                }
                while (z2.Count() < 38)
                {
                    flor = virginica.Where(c => c.usado == false).First();
                    flor.usado = true;
                    z2.Add(flor);
                }
                while (z3.Count() < 74)
                {
                    flor = virginica.Where(c => c.usado == false).First();
                    flor.usado = true;
                    z3.Add(flor);
                }

                // percorre todos os elementos e compara se a classe que o classificador retornou realmente está certa
                // se a classe estiver errada, ele marca como errada
                string[] classeObtida = Functions.ClassificadorDeAmostras(z1, z2, k);
                int posicao = 0;
                Flor auxTroca1 = null, auxTroca2 = null;

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

                Console.WriteLine("Rodada" + contador + "...\n" + "Taxa de Acerto: " + taxaDeAcertos + "%" + "\nK:"+ k +"\n");
                foreach (var limpezaFlores in flores)
                {
                    limpezaFlores.usado = false;
                }
                setosa = null;
                virginica = null;
                versicolor = null;
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

            Console.WriteLine("Média: " + media);
            Console.WriteLine("Desvio padrão: " + desvioPadrao);

            Console.ReadKey();
        }
    }
    
}
