using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1TransBit16
{
    public class RequestHandler
    {
        Statistics stat = null;
        CityInfo city = null;

        public RequestHandler(Statistics stat)
        {
            this.stat = stat;
        }

        public void provinceCityHandler()
        {
            List<CityInfo> cities = null;
            string provinceName = "";
            Console.WriteLine("Enter the province name:");
            provinceName = Console.ReadLine();

            cities = stat.DisplayProvinceCities($"{char.ToUpper(provinceName[0])}{provinceName.Substring(1)}");
            if (cities.Count > 0)
            {
                foreach (var city in cities)
                {
                    Console.WriteLine(city.ToString());
                }
                return;
            }

            if (cities.Count == 0) Console.WriteLine("The province name is Invalid.");
        }

        public void largestCityHandler()
        {
            CityInfo city = null;
            string provinceName = "";
            bool invalid = false;
            do
            {
                Console.WriteLine("Enter the province name:");
                provinceName = Console.ReadLine();

                city = stat.DisplayLargestPopulationCity($"{char.ToUpper(provinceName[0])}{provinceName.Substring(1)}");
                if (city != null)
                {
                    Console.WriteLine(city.ToString());
                    return;
                }

                Console.WriteLine("The province name is Invalid.");
                invalid = true;

            } while (invalid);
        }



        public void smallestCityHandler()
        {
            CityInfo city = null;
            string provinceName = "";
            bool invalid = false;
            do
            {
                Console.WriteLine("Enter the province name:");
                provinceName = Console.ReadLine();

                city = stat.DisplaySmallestPopulationCity($"{char.ToUpper(provinceName[0])}{provinceName.Substring(1)}");
                if (city != null)
                {
                    Console.WriteLine(city.ToString());
                    return;
                }

                Console.WriteLine("The province name is Invalid.");
                invalid = true;

            } while (invalid);
        }

        public void compareCityHandler()
        {
            CityInfo city1 = null;
            CityInfo city2 = null;
            string word = "";
            bool invalid = false;
            do
            {
                Console.WriteLine("Enter two cities to compare population separated by space ( ):");
                word = Console.ReadLine();
                string[] cities = word.Split(" ");

                if (stat.CityCatalogue.ContainsKey(cities[0]) && stat.CityCatalogue.ContainsKey(cities[1]))
                {
                    foreach (var c in stat.CityCatalogue)
                    {
                        if (c.Key.Equals(cities[0]))
                        {
                            city1 = c.Value;
                        }
                        if (c.Key.Equals(cities[1])) { city2 = c.Value; }

                    }

                    stat.CompareCitiesPopulation(city1, city2);
                    invalid = false;
                }
                else
                {

                    Console.WriteLine("Error: Please Enter valid cities");
                    invalid = true;
                }
            } while (invalid);

        }

        public void DistanceHandler()
        {
            string word = "";
            bool invalid = true;
            Console.WriteLine("Enter two cities to find distance between them with space between ( )");
            word = Console.ReadLine();
            CityInfo city1 = null, city2 = null;

            string[] cities = word.Split(" ");

            do
            { 
            if (stat.CityCatalogue.ContainsKey(cities[0]) && stat.CityCatalogue.ContainsKey(cities[1]))
            {
                foreach (var c in stat.CityCatalogue)
                {
                    if (c.Key.Equals(cities[0]))
                    {
                        city1 = c.Value;
                    }
                    if (c.Key.Equals(cities[1])) { city2 = c.Value; }

                }

                stat.CalculateDistanceBetweenCities(city1, city2);
                invalid = false;
            }
            else
            {

                Console.WriteLine("Error: Please Enter valid cities");
                invalid = true;
            }
        } while (invalid);


        }

        //Done by gord

        public void ProvincePopHandler()
        {

        }

        public void RankProvincePopHandler()
        {

        }

        public void RankProvByCityHandler()
        {

        }

        public void provinceCapHandler()
        {

        }









    }
}
