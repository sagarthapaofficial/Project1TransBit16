using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1TransBit16
{
    public class PopulationChangeEvent
    {
        public double CurrentPopulation { get; set; }
        public double NewPopulation { get; set; }

        public PopulationChangeEvent(double currentpop, double newpop)
        {
            this.CurrentPopulation = currentpop;
            this.NewPopulation = newpop;
        }

        //Notify method notifies the prof when the event occurs
        public static void NotifyUser(Object s, PopulationChangeEvent p1)
        {
            Console.WriteLine($"The population has been updated from {p1.CurrentPopulation} to {p1.NewPopulation}.");
        }
    }
}
