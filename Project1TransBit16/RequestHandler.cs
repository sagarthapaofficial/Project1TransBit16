using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1TransBit16
{
    public class RequestHandler
    {

        public void provinceCityHandler(Statistics stat)
        {
            List<CityInfo> cities = new List<CityInfo>();
            string provinceName = "";
            Console.WriteLine("Enter the province name:");
            provinceName = Console.ReadLine();

            cities = stat.DisplayProvinceCities(provinceName);
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



        public void largestCityHandler(Statistics stat)
        {
            CityInfo city = new CityInfo();
            string provinceName = "";
            bool invalid = false;
            do
            {
                Console.WriteLine("Enter the province name:");
                provinceName = Console.ReadLine();

                city = stat.DisplayLargestPopulationCity(provinceName);
                if (city != null)
                {
                   Console.WriteLine(city.ToString());
                   return;
                }

                Console.WriteLine("The province name is Invalid.");
                invalid = true;

            } while (invalid);
        }



        public void smallestCityHandler(Statistics stat)
        {
            CityInfo city = new CityInfo();
            string provinceName = "";
            bool invalid = false;
            do
            {
                Console.WriteLine("Enter the province name:");
                provinceName = Console.ReadLine();

                city = stat.DisplaySmallestPopulationCity(provinceName);
                if (city != null)
                {
                    Console.WriteLine(city.ToString());
                    return;
                }

                Console.WriteLine("The province name is Invalid.");
                invalid = true;

            } while (invalid);
        }

        public void compareCityHandler(Statistics stat)
        {

        }

        public void cityonMap(Statistics stat)
        {
<<<<<<< Updated upstream
=======
            string word = "";
            bool invalid = true;
            CityInfo city1 = null, city2 = null;
          

            do
            {
                Console.WriteLine("Enter two cities to find distance between them with space between ( )");
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
>>>>>>> Stashed changes

        }

        public void DistanceHandler(Statistics stat)
        {

        }

        public void ProvincePopHandler(Statistics stat)
        {
            bool validProvince = false;
            while (!validProvince)
            {
                Console.WriteLine("Enter a province name to view the sum of all populations of its cities (ex. ontario | quebec):");
                string input = Console.ReadLine();

                if (input == "quebec" || input == "Quebec")
                    input = "Québec";

                var citiesForProvince = from city in stat.CityCatalogue
                                        where city.Value.admin_name.ToLower() == input.ToLower()
                                        select city;

                if (citiesForProvince.Any())
                {
                    double pop = stat.DisplayProvincePopulation(input);
                    Console.WriteLine($"Population for {input}: {pop}");
                    validProvince = true;
                }
                else
                {
                    Console.WriteLine($"No cities found for province {input}, please check your spelling and try again.");
                }
            }

        }

        public void RankProvincePopHandler(Statistics stat)
        {
            Console.WriteLine("Sorting provinces by population.");
            stat.RankProvincesByPopulation();
        }

        public void RankProvByCityHandler(Statistics stat)
        {
            Console.WriteLine("Sorting provinces by the number of cities in each.");
            stat.RankProvincesByCities();
        }

<<<<<<< Updated upstream
        public void provinceCapHandler(Statistics stat)
=======

        //Forms & validates input and hooks to Statistics.GetCapital
        public void provinceCapHandler()
>>>>>>> Stashed changes
        {
            bool validProvince = false;
            while (!validProvince)
            {
                Console.WriteLine("Enter a province name to view its capital city (ex. ontario | quebec):");
                string input = Console.ReadLine();

                if (input == "quebec" || input == "Quebec")
                    input = "Québec";

                var citiesForProvince = from city in stat.CityCatalogue
                                        where city.Value.admin_name.ToLower() == input.ToLower()
                                        select city;

                if (citiesForProvince.Any())
                {
                    var cap = stat.GetCapital(input);
                    Console.WriteLine($"Capital city for {input} is {cap.city}");
                    validProvince = true;
                }
                else
                {
                    Console.WriteLine($"No cities found for province {input}, please check your spelling and try again.");
                }
            }
        }





    }

       

    









    
}
