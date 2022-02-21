using System;
using System.Collections.Generic;
using System.Linq;
<<<<<<< Updated upstream
using System.Text;
using System.Threading.Tasks;
=======
using System.Net.Http;
using System.Text.Json;
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;
>>>>>>> Stashed changes

namespace Project1TransBit16
{
    /* Enables the user to retrieve all information about the stored cities in the Dictionary generic type.*/
    public class Statistics
    {
        //Dictionary that holds the cities information returned from the DataModeler class.
        public Dictionary<string, CityInfo> CityCatalogue;
        public List<CityInfo> resultCityList =null;
<<<<<<< Updated upstream
=======
        public static string Api= "5b3ce3597851110001cf624842d0804e6a864305a35699c60b2ede2d";


        private static readonly HttpClient client = new HttpClient();

>>>>>>> Stashed changes

        public Statistics(string filename, string fileType)
        {
            DataModeler < Dictionary<string, CityInfo> >DataModeller= new DataModeler<Dictionary<string, CityInfo>>();
            CityCatalogue=DataModeller.ParseFile(filename+fileType);
        }

        public static bool validateData(string data, ref List<string>loadData)
        {
            loadData=data.Split(" ").ToList();
            if (loadData.Count != 2) return false;

            return true;
        }
        public void DisplayCityInformation()
        {
            //returns all the city stored information in the cityCatalogue.
            //or could ask the user to select a particular city

            string data = "";
            bool Isvalid=false;
            List<string> loadData = new List<string>();
            do
            {
                Console.WriteLine("Enter the city and province name separated by space ( )");
                data = Console.ReadLine();
                Isvalid = validateData(data, ref loadData);

                if (Isvalid)
                {
<<<<<<< Updated upstream
                    foreach (var city in CityCatalogue)
=======
                    if (loadData.Count == 2)
>>>>>>> Stashed changes
                    {
                        if (city.Key.Equals(loadData[0]) && city.Value.admin_name.Equals(loadData[1]))
                        {
<<<<<<< Updated upstream
                            Isvalid = true;
                            Console.WriteLine(city.Value.ToString());
                            return;
=======
                            return city = c.Value;
                        }
>>>>>>> Stashed changes

                        }
                    }
                    Isvalid = false;
                    Console.WriteLine("Error !: The city or province does not exist");
                }
            } while (!Isvalid);


        }

        public CityInfo cityById()

        public CityInfo DisplayLargestPopulationCity(string provinceName)
        {
            CityInfo Populouscity= new CityInfo();
           
            foreach (var city in CityCatalogue)
            {

                if (Populouscity.population < city.Value.population && city.Value.admin_name.Equals(provinceName))
                {
                    Populouscity = city.Value; 
                }
            }

            return Populouscity;

        }

        public CityInfo DisplaySmallestPopulationCity(string provinceName)
        {
            CityInfo smallPopcity = new CityInfo();
            smallPopcity.population = Double.MaxValue;
          
            foreach (var city in CityCatalogue)
            {
                if (smallPopcity.population > city.Value.population && city.Value.admin_name.Equals(provinceName))
                {
                    smallPopcity = city.Value;
                }

            }

            return smallPopcity;
        }

        public CityInfo CompareCitiesPopulation(CityInfo city1, CityInfo city2)
        {
<<<<<<< Updated upstream
            return new CityInfo();
=======
            if (city1.population > city2.population)
            {
                    Console.WriteLine( city1.ToString());     
            }else
            {
                Console.WriteLine(city2.ToString());
            }
            Console.WriteLine($"City1: {city1.city_ascii} population: {city1.population}");
            Console.WriteLine($"City2: {city1.city_ascii} population: {city2.population}");

>>>>>>> Stashed changes
        }

        //Use the name of the city and province to mark a city on the map.
        public void ShowCityOnMap()
        {

        }

        public int CalculateDistanceBetweenCities(CityInfo city1, CityInfo city2)
        {
<<<<<<< Updated upstream
            // use google api to find the distance between two country.
            return 0;
        }

=======

            string url = $"https://api.openrouteservice.org/v2/directions/driving-car?api_key={Api}&start={city1.lng},{city1.lat}&end={city2.lng},{city2.lat}";

            const double mToKm= 1000;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader sreader = new StreamReader(dataStream);
            string responsereader = sreader.ReadToEnd();
            JObject data = JObject.Parse(responsereader);
            double distance = (double)(data["features"][0]["properties"]["summary"]["distance"]);
            response.Close();

            //we could also use System.Device library.

            Console.WriteLine($"Distance between {city1.city_ascii} to {city2.city_ascii} is {Math.Round(distance/ mToKm)} km.");

        }
        public double DisplayProvincePopulation(string provinceName)
        {

            double population = 0;

            var citiesInProvince = from city in CityCatalogue
                                   where city.Value.admin_name.ToLower().Equals(provinceName.ToLower())
                                   select city;

            foreach (var city in citiesInProvince)
            {
                population += city.Value.population;
            }

            return population;
        }
        public List<CityInfo> DisplayProvinceCities(string provinceName)
        {
            //citycatalogue is missing 3 or 4 cities
            var citiesInProvince = from city in CityCatalogue
                                   where city.Value.admin_name.Equals(provinceName)
                                   select city;

            List<CityInfo> list = new List<CityInfo>();
            foreach (var city in citiesInProvince)
            {
                list.Add(city.Value);
            }

            return list;
        }

        //test me
        public void RankProvincesByPopulation()
        {
            //get all distinct provinces from citycatalogue. result should be an enumerable of string
            var distinctProvinces = (from city in CityCatalogue
                                     select city.Value.admin_name).Distinct();


            Dictionary<string, double> provinceAndPop = new Dictionary<string, double>();

            //for each of the distinct provinces
            foreach (var province in distinctProvinces)
            {
                //get an enumerable of all the cities for that province
                var citiesInProvince = from city in CityCatalogue
                                       where city.Value.admin_name.Equals(province.ToString())
                                       select city;

                double pop = 0;

                //for all the cities in the province, add population together
                foreach (var city in citiesInProvince)
                    pop += city.Value.population;

                //add key value pair of province and its cumulative population
                provinceAndPop.Add(province.ToString(), pop);
            }

            //ideally we'd want to use a sorted set, but that sorts on key only. 
            //SortedDictionary<string, double> sortedProvincesByPop = new SortedDictionary<string, double>(provinceAndPop);
            int i = 1;
            Console.WriteLine("Provinces ranked by population:\n");
            foreach (var province in provinceAndPop.OrderByDescending(key => key.Value))
            {
                Console.WriteLine($"{i++}. {province.Key}:  {province.Value}");
            }
        }

        public void RankProvincesByCities()
        {
            //get all distinct provinces from citycatalogue. result should be an enumerable of string
            var distinctProvinces = (from city in CityCatalogue
                                     select city.Value.admin_name).Distinct();

            Dictionary<string, int> provinceAndNumCities = new Dictionary<string, int>();

            //there is probably a better performing way to do this than querying entire set for every result of our prior query... it's probably really bad time complexity
            foreach (var province in distinctProvinces)
            {
                int citiesCountForProvince = (from city in CityCatalogue
                                              where city.Value.admin_name.Equals(province)
                                              select city).Count();

                provinceAndNumCities.Add(province.ToString(), citiesCountForProvince);
            }


            Console.WriteLine("Provinces ranked by number of cities:\n");
            int i = 1;
            foreach (var provinceNumCombo in provinceAndNumCities.OrderByDescending(key => key.Value))
            {
                Console.WriteLine($"{i++}.    {provinceNumCombo.Key}:   {provinceNumCombo.Value}");
            }
        }

        public CityInfo GetCapital(string provinceName)
        {
            //!string.IsNullOrEmpty(city.Value.capital) 
            var capital = from city in CityCatalogue
                          where city.Value.admin_name.ToLower().Equals(provinceName)
                                   && city.Value.capital.Equals("admin")
                          select city.Value;

            return capital.FirstOrDefault();
        }


>>>>>>> Stashed changes




    }
}
