using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1TransBit16
{
    public class PopulationChangeEvent : EventArgs
    {
        public double CurrentPopulation { get; set; }
        public double NewPopulation { get; set; }

        public PopulationChangeEvent(double currentpop, double newpop)
        {
            this.CurrentPopulation = currentpop;
            this.NewPopulation = newpop;
        }
    }
}
