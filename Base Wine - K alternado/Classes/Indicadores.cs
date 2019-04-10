using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Classes
{
    class Indicadores
    {
        public int acertos { get; set; }
        public double taxaAcertos { get; set; }

        public Indicadores(int a, double taxaA)
        {
            acertos = a;
            taxaAcertos = taxaA;
        }
    }
}
