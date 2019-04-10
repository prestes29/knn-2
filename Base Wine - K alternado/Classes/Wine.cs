using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Classes
{
    public class Wine
    {
        public float classe { get; set; }
        public float alcool { get; set; }
        public float acidoMalico { get; set; }
        public float cinza { get; set; }
        public float alcalinidadeDaCinza { get; set; }
        public float magnesio { get; set; }
        public float totalDeFenois { get; set; }
        public float flavanoids { get; set; }
        public float fenoisNãoFlavanoides { get; set; }
        public float proantocianinas { get; set; }
        public float intensidadeDeCor { get; set; }
        public float matriz { get; set; }
        public float od280_od315VinhosDiluidos { get; set; }
        public float prolina { get; set; }
        public bool trocado { get; set; }
        public bool usado { get; set; }
        public bool errado { get; set; }
    }
}
