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

            if (provinceName == "quebec" || provinceName == "Quebec")
                provinceName = "Québec";

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

                if (provinceName == "quebec" || provinceName == "Quebec")
                    provinceName = "Québec";

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
                if (provinceName == "quebec" || provinceName == "Quebec")
                    provinceName = "Québec";

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

                //it would be nice if this input was case-insensitive
                //it also currently does not handle quebec cities (with diacritics etc)  - need to leverage city_ascii value 

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


            //it would be nice if this input was case-insensitive
            //it also currently does not handle quebec cities (with diacritics etc) - need to leverage city_ascii value 

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



        //Forms & validates input and hooks to Statistics.DisplayProvincePopulation
        public void DisplayProvincePopulationHandler()
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

        //Forms & validates input and hooks to Statistics.DisplayProvinceCities
        public void DisplayProvinceCitiesHandler()
        {
            bool validProvince = false;
            while (!validProvince)
            {
                Console.WriteLine("Enter a province name to view all of the cities for that province (ex. ontario | quebec):");
                string input = Console.ReadLine();

                if (input == "quebec" || input == "Quebec")
                    input = "Québec";

                var citiesForProvince = from city in stat.CityCatalogue
                                        where city.Value.admin_name.ToLower() == input.ToLower()
                                        select city;

                if (citiesForProvince.Any())
                {
                    stat.DisplayProvinceCities(input);
                    validProvince = true;
                }
                else
                {
                    Console.WriteLine($"No cities found for province {input}, please check your spelling and try again.");
                }
            }
        }

        //Hooks to Statistics.RankProvincesByPopulation
        public void RankProvincesByPopulationHandler()
        {
            Console.WriteLine("Sorting provinces by population.");
            stat.RankProvincesByPopulation();
        }

        //Hooks to Statistics.RankProvincesByCities
        public void RankProvincesByNumCitiesHandler()
        {
            Console.WriteLine("Sorting provinces by the number of cities in each.");
            stat.RankProvincesByCities();
        }

        //Forms & validates input and hooks to Statistics.GetCapital
        public void GetCapitalHandler()
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
