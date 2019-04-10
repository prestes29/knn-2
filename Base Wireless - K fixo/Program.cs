using ConsoleApp1.Classes;
using ConsoleApp1.Funções;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base_Wireless___K_fixo
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Wine> wines = new List<Wine>();
            int k = 3;
            List<Indicadores> Resultados = new List<Indicadores>(); //guarda os resultados dos testes de todas as 30 rodadas para calcular a media e o desvio padrao


            // LEITURA DE ARQUIVOS

            Console.WriteLine("Iniciando base WINE");
            using (StreamReader reader = new StreamReader("C:\\Users\\Prestes-Noot\\Desktop\\Computação Avançada\\base_wine2.txt"))
            {
                while (!reader.EndOfStream)
                {
                    string linha = reader.ReadLine();

                    Wine wine = new Wine();

                    string[] valores = linha.Split(',');

                    wine.classe = Convert.ToSingle(valores[0]);
                    wine.alcool = Convert.ToSingle(valores[1]);
                    wine.acidoMalico = Convert.ToSingle(valores[2]);
                    wine.cinza = Convert.ToSingle(valores[3]);
                    wine.alcalinidadeDaCinza = Convert.ToSingle(valores[4]);
                    wine.magnesio = Convert.ToSingle(valores[5]);
                    wine.totalDeFenois = Convert.ToSingle(valores[6]);
                    wine.flavanoids = Convert.ToSingle(valores[7]);
                    wine.fenoisNãoFlavanoides = Convert.ToSingle(valores[8]);
                    wine.proantocianinas = Convert.ToSingle(valores[9]);
                    wine.intensidadeDeCor = Convert.ToSingle(valores[10]);
                    wine.matriz = Convert.ToSingle(valores[11]);
                    wine.od280_od315VinhosDiluidos = Convert.ToSingle(valores[12]);
                    wine.prolina = Convert.ToSingle(valores[13]);



                    wines.Add(wine);
                }
            }
            /*
            int i = 0;
            for(i=0;i<flores.Count;i++)
            {
                Console.WriteLine(flores[]);
            }
            */


            for(int contador = 1; contador <= 30; contador++)
            {
                List<Wine> tipo1 = new List<Wine>();
                List<Wine> tipo2 = new List<Wine>();
                List<Wine> tipo3 = new List<Wine>();
                List<Wine> z1 = new List<Wine>();
                List<Wine> z2 = new List<Wine>();
                List<Wine> z3 = new List<Wine>();

                foreach (var divisao in wines)
            {
                if (divisao.classe == 1)
                {
                    tipo1.Add(divisao);
                    continue;
                }
                if (divisao.classe == 2)
                {
                    tipo2.Add(divisao);
                    continue;
                }
                if (divisao.classe == 3)
                {
                    tipo3.Add(divisao);
                }
            }

            Random randNum = new Random();
            Wine wine;

            while (z1.Count() < 15)
            {
                wine = tipo1.ElementAt(randNum.Next(tipo1.Count() - 1));
                if (!wine.usado)
                {
                    wine.usado = true;
                    z1.Add(wine);
                }
            }
            while (z2.Count() < 15)
            {
                wine = tipo1.ElementAt(randNum.Next(tipo1.Count() - 1));
                if (!wine.usado)
                {
                    wine.usado = true;
                    z2.Add(wine);
                }
            }
            while (z3.Count() < 29)
            {
                wine = tipo1.Where(c => c.usado == false).First();
                wine.usado = true;
                z3.Add(wine);
                
            }
            while (z1.Count() < 30)
            {
                wine = tipo2.ElementAt(randNum.Next(tipo2.Count() - 1));
                if (!wine.usado)
                {
                    wine.usado = true;
                    z1.Add(wine);
                }
            }

                while (z2.Count() < 30)
            {
                    wine = tipo2.Where(c => c.usado == false).First();
                    wine.usado = true;
                    z2.Add(wine);
            }

            while (z3.Count() < 58)
            {
                wine = tipo2.Where(c => c.usado == false).First();
                wine.usado = true;
                z3.Add(wine);
            }
            while (z1.Count() < 42)
            {
                wine = tipo3.ElementAt(randNum.Next(tipo3.Count() - 1));
                if (!wine.usado)
                {
                    wine.usado = true;
                    z1.Add(wine);
                }
            }
            while (z2.Count() < 42)
            {
                wine = tipo3.Where(c => c.usado == false).First();
                wine.usado = true;
                z2.Add(wine);
            }
            while (z3.Count() < 82)
            {
                wine = tipo3.Where(c => c.usado == false).First();
                wine.usado = true;
                z3.Add(wine);
            }

            // percorre todos os elementos e compara se a classe que o classificador retornou realmente está certa
            // se a classe estiver errada, ele marca como errada
            float[] classeObtida = Functions.ClassificadorDeAmostras(z1, z2, k);
            int posicao = 0;
            Wine auxTroca1 = null, auxTroca2 = null;

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

            Console.WriteLine("Rodada" + contador + "...\n" + "Taxa de Acerto: " + taxaDeAcertos + "%\n");
                foreach(var limpezaWine in wines)
                {
                    limpezaWine.usado = false;
                }
                tipo1 = null;
                tipo3 = null;
                tipo2 = null;
                z1 = null;
                z2 = null;
                z3 = null;
                // aqui viria a alteração para o k ficar alternando
                // adicionar um k++
        }
            double soma = 0, media, desvioPadrao;
            foreach( var baseParaCalculo in Resultados)
            {
                soma += baseParaCalculo.taxaAcertos;
            }
            media = soma / Resultados.Count();
            soma = 0;

            foreach( var baseParaCalculo in Resultados)
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
