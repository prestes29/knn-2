using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Classes
{
    public class Balance
    {
        public float leftWeight { get; set; }
        public float leftDistance { get; set; }
        public float rightWeight { get; set; }
        public float rightDistance { get; set; }
        public string classe { get; set; }
        public bool trocado { get; set; }
        public bool usado { get; set; }
        public bool errado { get; set; }
    }
}
