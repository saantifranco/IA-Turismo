using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Backpropagation.Nodes
{
    public class Conversor
    {
        public String[] lugares = null;

        public Conversor(String[] lugaresPosibles){
            lugares = lugaresPosibles;
        }

        public String convertir(int placeID){
            return lugares[placeID];
        }
    }
}
