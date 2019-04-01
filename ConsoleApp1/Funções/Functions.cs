using ConsoleApp1.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Funções
{
    class Functions
    {
        public static double obterDistanciaEuclidiana(Flor flor1, Flor flor2)
        {
            double soma = Math.Pow((flor1.comprimento_petala - flor2.comprimento_petala), 2)
                + Math.Pow((flor1.comprimento_sepala - flor2.comprimento_sepala), 2)
                + Math.Pow((flor1.largura_petala - flor2.largura_petala), 2)
                + Math.Pow((flor1.largura_sepala - flor2.largura_sepala), 2);
            return Math.Sqrt(soma);
        }

        public static string[] ClassificadorDeAmostras(List<Flor> Lista1, List<Flor> Lista2, int k)
        {
            var tamanho = Lista1.Count();
            string[] classesLista1 = new string[tamanho];

            int posicao = 0;

            foreach(var elemento1 in Lista1 )
            {
                var listaDistancia = new List<KeyValuePair<double, Flor>>();
                foreach(var elemento2 in Lista2)
                {
                    double distancia = obterDistanciaEuclidiana(elemento1, elemento2);
                    listaDistancia.Add(new KeyValuePair<double, Flor>(distancia, elemento2)); 
                }
                listaDistancia.Sort((x, y) => x.Key.CompareTo(y.Key));

                int contadorSetosa = 0, contadorVirginica = 0, contadorVersicolor = 0;

                for ( int i = 0; i<k; i++)
                {
                    string classe = listaDistancia[i].Value.classe;

                    if (classe == "Iris-setosa")
                        contadorSetosa++;
                    if (classe == "Iris-versicolor")
                        contadorVersicolor++;
                    if (classe == "Iris-virginica")
                        contadorVirginica++;
                }
                string resultadoComparacao;
                if (contadorSetosa > contadorVersicolor && contadorSetosa > contadorVirginica)
                    resultadoComparacao = "Iris-setosa";
                else if (contadorVersicolor > contadorSetosa && contadorVersicolor > contadorVirginica)
                    resultadoComparacao = "Iris-versicolor";
                else
                    resultadoComparacao = "Iris-virginica";

                classesLista1[posicao] = resultadoComparacao;
                posicao++;
                listaDistancia = null;
            }
            return classesLista1;
        }

    }
}
