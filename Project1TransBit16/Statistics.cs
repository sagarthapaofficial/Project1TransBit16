using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Device.Location;

namespace Project1TransBit16
{
    /* Enables the user to retrieve all information about the stored cities in the Dictionary generic type.*/
    public class Statistics
    {
        //Dictionary that holds the cities information returned from the DataModeler class.
        public Dictionary<string, CityInfo> CityCatalogue;
        public List<CityInfo> resultCityList =null;
        public static string Api= "AIzaSyDREvoNKPF3ZEvaIdxavFqU2emfxdP9CSM";
        public static string keyValue = $"https://maps.googleapis.com/maps/api/directions/json?origin=Toronto&destination=Montreal&key={Api}";

        private static readonly HttpClient client = new HttpClient();


        public Statistics(string filename, string fileType)
        {
            DataModeler < Dictionary<string, CityInfo> >DataModeller= new DataModeler<Dictionary<string, CityInfo>>();
            CityCatalogue=DataModeller.ParseFile(filename+fileType);
        }

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

        public List<CityInfo> DisplayProvinceCities(string province)
        {
            resultCityList = new List<CityInfo>();
            foreach (var city in CityCatalogue)
            {
                if (city.Value.admin_name.Equals(province))
                {
                    resultCityList.Add(city.Value);
                   
                }
            }

            return resultCityList;

        }


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

        public void CompareCitiesPopulation(CityInfo city1, CityInfo city2)
        {
            if (city1.population > city2.population)
            {
                    Console.WriteLine( city1.ToString());     
            }else
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

             Console.WriteLine($"{Math.Round(sCoord.GetDistanceTo(eCoord)* metertokm)} km");

        }

      




    }
}
