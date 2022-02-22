/*
GroupName:TransBit
@authors: Sagar Thapa, Gordon Reaman
ProgramName: CityPopulationChangeEvent.cs
Date: 2022-02-22
 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1TransBit16
{
    public class PopulationChangeEvent
    {
        //getters and setters
        public double CurrentPopulation { get; set; }
        public double NewPopulation { get; set; }

        //Constructor
        public PopulationChangeEvent(double currentpop, double newpop)
        {
            this.CurrentPopulation = currentpop;
            this.NewPopulation = newpop;
        }

        //Notify method notifies the end User when the Population change event occurs
        public static void NotifyUser(Object s, PopulationChangeEvent p1)
        {
            Console.WriteLine($"\nThe population has been updated from {p1.CurrentPopulation} to {p1.NewPopulation}.\n");
        }
    }
}
