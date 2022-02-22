
/*
GroupName:TransBit
@authors: Sagar Thapa, Gordon Reaman
ProgramName: Statistics.cs
Date: 2022-02-22
 
 */

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

        //Constructor
        public RequestHandler(Statistics stat)
        {
            this.stat = stat;
        }

        ///helper function that validates the city and the province and return valid city
        /// <returns>CityInfo</returns>
        public CityInfo vCityProvince()
        {
            string data = "";
            bool Isvalid = false;
            List<string> loadData = new List<string>();
            CityInfo city = null;

            try
            {
                do
                {
                    Console.WriteLine("Enter the city and province name separated by comma (,)");
                    data = Console.ReadLine();
                    loadData = data.Split(",").ToList();
                    if (loadData.Count == 2)
                    {
                        city = stat.DisplayCityInformation(loadData[0], loadData[1]);
                    }
                    Isvalid = (city != null) ? Isvalid = true : Isvalid = false;

                    if (!Isvalid) { Console.WriteLine("Error !: The city or province does not exist"); }


                } while (!Isvalid);



            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return city;
        }


        //Displays valid cityInformation 
        public void DisplayCityInformationHandler()
        {
            CityInfo city = null;

            try
            {
                city = vCityProvince();

                PrintCityHeaders();
                PrintCity(city);
                Console.WriteLine();

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }


        ///Shows a given city on a map by opening a web browser and providing link to it too.
        public void ShowCityOnMapHandler()
        {
            //calls vCityProvince to get the city 
            CityInfo city = vCityProvince();
            //calls the cityonMap on statistics
            stat.ShowCityOnMap(city);

        }
        //finds the largestcity in the province
        public void largestCityHandler()
        {
            CityInfo largestCity = null;

            try
            {
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
                        PrintCityHeaders();
                        PrintCity(largestCity);
                        Console.WriteLine();
                        //Console.WriteLine(largestCity.ToString());
                        validProvince = true;
                    }
                    else
                    {
                        Console.WriteLine($"No cities found for province {input}, please check your spelling and try again.");
                    }
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }


        //Froms and validates entered province and finds the smallest city in the province
        public void smallestCityHandler()
        {
            CityInfo smallestCity = null;

            try
            {
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
                        PrintCityHeaders();
                        PrintCity(smallestCity);
                        Console.WriteLine();
                        validProvince = true;
                    }
                    else
                    {
                        Console.WriteLine($"No cities found for province {input}, please check your spelling and try again.");
                    }
                }

            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        //compares the population of the 2 distinct cities.
        public void compareCityHandler()
        {
            CityInfo city1 = null, city2 = null;
            Console.WriteLine("First City: ");
           
            //calls the vCityProvince method to validate and return valid city
            city1 = vCityProvince();
            Console.WriteLine("Second City: ");
            city2 = vCityProvince();

            //console output
            PrintCityHeaders();
            PrintCity(city1);
            PrintCity(city2);
            Console.WriteLine();

            //Prints out the greater city
            CityInfo greater = stat.CompareCitiesPopulation(city1, city2);
            Console.WriteLine($"The city with the greater population is {greater.city_ascii}.\n");

        }

        //finds the distance between two cities
        public void DistanceHandler()
        {
            CityInfo city1 = null, city2 = null;
            Console.WriteLine("First City: ");
            //calls the vCityProvince method to validate and return valid city
            city1 = vCityProvince();
            Console.WriteLine("Second City: ");
            //calls the vCityProvince method to validate and return valid city
            city2 = vCityProvince();
            stat.CalculateDistanceBetweenCities(city1, city2);

        }
        //Forms & validates input and hooks to Statistics.DisplayProvincePopulation
        public void DisplayProvincePopulationHandler()
        {
            bool validProvince = false;
            try
            {
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
                        Console.WriteLine($"\nPopulation for {input}: {pop}\n");
                        validProvince = true;
                    }
                    else
                    {
                        Console.WriteLine($"No cities found for province {input}, please check your spelling and try again.");
                    }
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //Forms & validates input and hooks to Statistics.DisplayProvinceCities
        public void DisplayProvinceCitiesHandler()
        {
            List<CityInfo> cityList = new List<CityInfo>();
            try
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
                        cityList = stat.DisplayProvinceCities(input.ToLower());
                        validProvince = true;
                    }
                    else
                    {
                        Console.WriteLine($"No cities found for province {input}, please check your spelling and try again.");
                    }
                }

                PrintCityHeaders();

                //display the cities in the list
                foreach (var c in cityList)
                    PrintCity(c);

                Console.WriteLine();

            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
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
            try
            {
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
                        Console.WriteLine($"\nCapital city for {input} is {cap.city}\n");
                        validProvince = true;
                    }
                    else
                    {
                        Console.WriteLine($"No cities found for province {input}, please check your spelling and try again.");
                    }
                }

            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //Updates the population of the given city on the given file types
        public void UpdatePopulationForCityHandler()
        {

            try
            {
                //Assigning the subscriber method to the publisher.
                PopHandler += PopulationChangeEvent.NotifyUser;

                //get file type for write out
                int filetype = 0;
                bool validFiletype = false;
                while (!validFiletype)
                {
                    Console.WriteLine("\nWhich of the following file extensions would you like to write to?");
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
                Console.WriteLine();
                cityToChange = vCityProvince();

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

                //update pop in list. changing this object affects the list
                cityToChange.population = newPopulation;

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

            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        //Helper method to print cityheader titles
        private void PrintCityHeaders()
        {
            String headers = String.Format("\n{0,-17} {1,-17} {2,-10} {3,-10} {4,-8} {5,-15} {6,-12} {7,-10}\n", "City", "City_ASCII", "Latitude", "Longitude", "Country", "Province", "Population", "ID");
            Console.WriteLine(headers);
        }

        //helper method to print the city
        private void PrintCity(CityInfo c)
        {
            String obj = String.Format("{0,-17} {1,-17} {2,-10} {3,-10} {4,-8} {5,-15} {6,-12} {7,-10}", c.city, c.city_ascii, c.lat, c.lng, c.country, c.admin_name, c.population, c.id);
            Console.WriteLine(obj);
        }

        //Passes to the event handler
        protected virtual void eventFired(PopulationChangeEvent data)
        {
            if (PopHandler != null) PopHandler.Invoke(this, data);
        }
    }
}
