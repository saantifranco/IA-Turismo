using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Backpropagation.Nodes
{
    public class Patron
    {
        public double[] input { get; set; }
        public int idPatron { get; set; }
        public string valorEsperado { get; set; }

       public Patron(double[] input, int idPatron, string valorEsperado)
        {
            this.input = input;
            this.idPatron = idPatron;
            this.valorEsperado = valorEsperado;
        }
    }
}
