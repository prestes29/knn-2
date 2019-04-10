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
        public static double obterDistanciaEuclidiana(Balance balance1, Balance balance2)
        {
            double soma = Math.Pow((balance1.leftWeight - balance2.leftWeight), 2)
                + Math.Pow((balance1.leftDistance - balance2.leftDistance), 2)
                + Math.Pow((balance1.rightWeight - balance2.rightWeight), 2)
                + Math.Pow((balance1.rightDistance - balance2.rightDistance), 2);
            return Math.Sqrt(soma);
        }

        public static string[] ClassificadorDeAmostras(List<Balance> Lista1, List<Balance> Lista2, int k)
        {
            var tamanho = Lista1.Count();
            string[] classesLista1 = new string[tamanho];

            int posicao = 0;

            foreach(var elemento1 in Lista1 )
            {
                var listaDistancia = new List<KeyValuePair<double, Balance>>();
                foreach(var elemento2 in Lista2)
                {
                    double distancia = obterDistanciaEuclidiana(elemento1, elemento2);
                    listaDistancia.Add(new KeyValuePair<double, Balance>(distancia, elemento2)); 
                }
                listaDistancia.Sort((x, y) => x.Key.CompareTo(y.Key));

                int contadorL = 0, contadorB = 0, contadorR = 0;

                for ( int i = 0; i<k; i++)
                {
                    string classe = listaDistancia[i].Value.classe;

                    if (classe == "L")
                        contadorL++;
                    if (classe == "R")
                        contadorR++;
                    if (classe == "B")
                        contadorB++;
                }
                string resultadoComparacao;
                if (contadorL > contadorR && contadorL > contadorB)
                    resultadoComparacao = "L";
                else if (contadorR > contadorL && contadorR > contadorB)
                    resultadoComparacao = "R";
                else
                    resultadoComparacao = "B";

                classesLista1[posicao] = resultadoComparacao;
                posicao++;
                listaDistancia = null;
            }
            return classesLista1;
        }

    }
}
