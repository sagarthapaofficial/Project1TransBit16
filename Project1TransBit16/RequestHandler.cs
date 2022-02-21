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

        //Delegate and event handler

        public delegate void Del(Object source, PopulationChangeEvent ev);
        //event handler
        public event Del PopHandler;

        //Instantiation of the cityPopulationChangeEvent
        PopulationChangeEvent popEvent = null;


        public RequestHandler(Statistics stat)
        {
            this.stat = stat;
        }

        //finds the largestcity in the province
        public void largestCityHandler()
        {
            CityInfo largestCity = null;
 
            bool validProvince = false;
            while (!validProvince)
            {
                Console.WriteLine("Enter a province name to view largest city (ex. ontario | quebec):");
                string input = Console.ReadLine();

                if (input == "quebec" || input == "Quebec")
                    input = "Québec";

                var citiesForProvince = from city in stat.CityCatalogue
                                        where city.Value.admin_name.ToLower() == input.ToLower()
                                        select city;

                if (citiesForProvince.Any())
                {
                    largestCity = stat.DisplayLargestPopulationCity(input.ToLower());
                    Console.WriteLine(largestCity.ToString());
                    validProvince = true;
                }
                else
                {
                    Console.WriteLine($"No cities found for province {input}, please check your spelling and try again.");
                }
            }

        }


        //Froms and validates entered province and finds the smallest city in the province
        public void smallestCityHandler()
        {
            CityInfo smallestCity = null;

            bool validProvince = false;
            while (!validProvince)
            {
                Console.WriteLine("Enter a province name to view smallest city (ex. ontario | quebec):");
                string input = Console.ReadLine();

                if (input == "quebec" || input == "Quebec")
                    input = "Québec";

                var citiesForProvince = from city in stat.CityCatalogue
                                        where city.Value.admin_name.ToLower() == input.ToLower()
                                        select city;

                if (citiesForProvince.Any())
                {
                    smallestCity = stat.DisplaySmallestPopulationCity(input.ToLower());
                    Console.WriteLine(smallestCity.ToString());
                    validProvince = true;
                }
                else
                {
                    Console.WriteLine($"No cities found for province {input}, please check your spelling and try again.");
                }
            }

        }

        //compares the population of the 2 distinct cities.
        public void compareCityHandler()
        {
            CityInfo city1 = null, city2 = null;
            Console.WriteLine("First City: ");
            city1 = stat.DisplayCityInformation();
            Console.WriteLine("Second City: ");
            city2 = stat.DisplayCityInformation();
            stat.CompareCitiesPopulation(city1, city2);
        }

        //finds the distance between two cities
        public void DistanceHandler()
        {
            CityInfo city1 = null, city2 = null;
            Console.WriteLine("First City: ");
            city1 = stat.DisplayCityInformation();
            Console.WriteLine("Second City: ");
            city2 = stat.DisplayCityInformation();
            stat.CalculateDistanceBetweenCities(city1, city2);

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
            List<CityInfo> cityList = new List<CityInfo>();

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
                    cityList=stat.DisplayProvinceCities(input.ToLower());
                    validProvince = true;
                }
                else
                {
                    Console.WriteLine($"No cities found for province {input}, please check your spelling and try again.");
                }
            }

            foreach(var c in cityList)
            {
                Console.WriteLine(c.ToString());
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


        public void UpdatePopulationForCityHandler()
        {
            //Assigning the subscriber method to the publisher.
            PopHandler += PopulationChangeEvent.NotifyUser;

            //get file type for write out
            int filetype = 0;
            bool validFiletype = false;
            while (!validFiletype)
            {
                Console.WriteLine("Which of the following file extensions would you like to write to?");
                Console.WriteLine("1) .CSV");
                Console.WriteLine("2) .JSON");
                Console.WriteLine("3) .XML");
                filetype = 0;
                string input = Console.ReadLine();

                if (int.TryParse(input, out filetype))
                    if (filetype >= 1 && filetype <= 3)
                        break;

                Console.WriteLine("Invalid input. Please try again");
            }

            //get city to overwrite
            CityInfo cityToChange = null;
            cityToChange=stat.DisplayCityInformation();

            //get new pop
            double newPopulation = 0;
            bool validPop = false;
            while (!validPop)
            {
                Console.WriteLine($"{cityToChange.city} current population: {cityToChange.population}\nEnter new population:");
                string input = Console.ReadLine();
                newPopulation = 0;

                if (double.TryParse(input, out newPopulation) && newPopulation >= 0)
                {
                    validPop = true;
                }
                else
                {
                    Console.WriteLine("New population entered was negative or not a number. Please try again.");
                }
            }

            //update pop in list. changing this object affects the list
            //cityToChange.population = newPopulation;
            popEvent = new PopulationChangeEvent(cityToChange.population, newPopulation);


            //write out new city to file, do event thing
            switch (filetype)
            {
                case 1:
                    stat.WriteToCSV();
                    break;
                case 2:
                    stat.WriteToJSON();
                    break;
                case 3:
                    stat.WriteToXML();
                    break;

            }

            //fires the event
            eventFired(popEvent);
            //update pop in list. changing this object affects the list
            cityToChange.population = newPopulation;

        }

        //Passes to the event handler
        protected virtual void eventFired(PopulationChangeEvent data)
        {
            if (PopHandler != null) PopHandler.Invoke(this, data);
        }
    }
}
