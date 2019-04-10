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
        public static double obterDistanciaEuclidiana(Wine wine1, Wine wine2)
        {
            double soma = Math.Pow((wine1.alcool - wine2.alcool), 2)
                + Math.Pow((wine1.acidoMalico - wine2.acidoMalico), 2)
                + Math.Pow((wine1.cinza - wine2.cinza), 2)
                + Math.Pow((wine1.alcalinidadeDaCinza - wine2.alcalinidadeDaCinza), 2)
                +Math.Pow((wine1.magnesio - wine2.magnesio), 2)
                + Math.Pow((wine1.totalDeFenois - wine2.totalDeFenois), 2)
                + Math.Pow((wine1.flavanoids - wine2.flavanoids), 2)
                + Math.Pow((wine1.fenoisNãoFlavanoides - wine2.fenoisNãoFlavanoides), 2)
                + Math.Pow((wine1.proantocianinas - wine2.proantocianinas), 2)
                + Math.Pow((wine1.intensidadeDeCor - wine2.intensidadeDeCor), 2)
                + Math.Pow((wine1.matriz - wine2.matriz), 2)
                + Math.Pow((wine1.od280_od315VinhosDiluidos - wine2.od280_od315VinhosDiluidos), 2)
                + Math.Pow((wine1.prolina - wine2.prolina), 2);

            return Math.Sqrt(soma);
        }

        public static float[] ClassificadorDeAmostras(List<Wine> Lista1, List<Wine> Lista2, int k)
        {
            var tamanho = Lista1.Count();
            float[] classesLista1 = new float[tamanho];

            int posicao = 0;

            foreach(var elemento1 in Lista1 )
            {
                var listaDistancia = new List<KeyValuePair<double, Wine>>();
                foreach(var elemento2 in Lista2)
                {
                    double distancia = obterDistanciaEuclidiana(elemento1, elemento2);
                    listaDistancia.Add(new KeyValuePair<double, Wine>(distancia, elemento2)); 
                }
                listaDistancia.Sort((x, y) => x.Key.CompareTo(y.Key));

                int contadorTipo1 = 0, contadorTipo2 = 0, contadorTipo3 = 0;

                for ( int i = 0; i<k; i++)
                {
                    float classe = listaDistancia[i].Value.classe;

                    if (classe == 1)
                        contadorTipo1++;
                    if (classe == 3)
                        contadorTipo3++;
                    if (classe == 2)
                        contadorTipo2++;
                }
                float resultadoComparacao;
                if (contadorTipo1 > contadorTipo3 && contadorTipo1 > contadorTipo2)
                    resultadoComparacao = 1;
                else if (contadorTipo3 > contadorTipo1 && contadorTipo3 > contadorTipo2)
                    resultadoComparacao = 3;
                else
                    resultadoComparacao = 2;

                classesLista1[posicao] = resultadoComparacao;
                posicao++;
                listaDistancia = null;
            }
            return classesLista1;
        }

    }
}
