using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Device.Location;
using System.IO;

namespace Project1TransBit16
{
    /* Enables the user to retrieve all information about the stored cities in the Dictionary generic type.*/
    public class Statistics
    {
        //Dictionary that holds the cities information returned from the DataModeler class.
        public Dictionary<string, CityInfo> CityCatalogue; //ISSUE: citycatalogue is missing a few cities. 248 in catalogue, 252 actual
        public List<CityInfo> resultCityList =null;
        public static string Api= "AIzaSyDREvoNKPF3ZEvaIdxavFqU2emfxdP9CSM";
        public static string keyValue = $"https://maps.googleapis.com/maps/api/directions/json?origin=Toronto&destination=Montreal&key={Api}";

        private static readonly HttpClient client = new HttpClient();


        public Statistics(string filename, string fileType)
        {
            DataModeler < Dictionary<string, CityInfo> >DataModeller= new DataModeler<Dictionary<string, CityInfo>>();
            CityCatalogue=DataModeller.ParseFile(Directory.GetCurrentDirectory() + "/data/" + filename+fileType);
        }

//--City Methods--//
        public CityInfo DisplayCityInformation()
        {
            string data = "";
            bool Isvalid = false;
            List<string> loadData = new List<string>();
            CityInfo city = null;
            do
            {
                Console.WriteLine("Enter the city and province name separated by space ( )");
                data = Console.ReadLine();
                loadData = data.Split(" ").ToList();

                foreach (var c in CityCatalogue)
                {

                    if (loadData.Count == 2)
                    {
                        if (c.Key.Equals(loadData[0]) && c.Value.admin_name.Equals(loadData[1]))
                        {
                            return city = c.Value;
                        }
                    }
                }
                Isvalid = false;
                Console.WriteLine("Error !: The city or province does not exist");

            } while (!Isvalid);

            return city;
        }
        public CityInfo DisplayLargestPopulationCity(string provinceName)
        {
            CityInfo Populouscity = new CityInfo();

            foreach (var city in CityCatalogue)
            {

                if (Populouscity.population < city.Value.population && city.Value.admin_name.Equals(provinceName))
                {
                    Populouscity = city.Value;
                }
            }
            return Populouscity;
        }
        public void CompareCitiesPopulation(CityInfo city1, CityInfo city2)
        {
            if (city1.population > city2.population)
            {
                Console.WriteLine(city1.ToString());
            }
            else
            {
                Console.WriteLine(city2.ToString());
            }
            Console.WriteLine($"City1: {city1.city} population: {city1.population}");
            Console.WriteLine($"City2: {city1.city} population: {city2.population}");
        }

        //Use the name of the city and province to mark a city on the map.
        public void ShowCityOnMap()
        {

            CityInfo city = DisplayCityInformation();
            Console.WriteLine($"Link to view city on Map: https://www.latlong.net/c/?lat={city.lat}&long={city.lng}");

        }
        public void CalculateDistanceBetweenCities(CityInfo city1, CityInfo city2)
        {
            var sCoord = new GeoCoordinate(city1.lat, city1.lng);
            var eCoord = new GeoCoordinate(city2.lat, city2.lng);
            const double metertokm = 0.001;

            Console.WriteLine($"{Math.Round(sCoord.GetDistanceTo(eCoord) * metertokm)} km");
        }
        public CityInfo DisplaySmallestPopulationCity(string provinceName)
        {
            CityInfo smallPopcity = null;
            double population = Double.MaxValue;
                
            foreach (var city in CityCatalogue)
            {
                if (population > city.Value.population && city.Value.admin_name.Equals(provinceName))
                {
                    smallPopcity = city.Value;
                    population = city.Value.population;
                }

            }

            return smallPopcity;
        }


//--Province Methods--//
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

        public CityInfo GetCapital (string provinceName)
        {
            //!string.IsNullOrEmpty(city.Value.capital) 
            var capital =          from city in CityCatalogue
                                   where city.Value.admin_name.ToLower().Equals(provinceName)
                                            && city.Value.capital.Equals("admin")
                                   select city.Value;

            return capital.FirstOrDefault();
        }





    }
}
