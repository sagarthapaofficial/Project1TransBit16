using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Device.Location;
using System.IO;
using Newtonsoft.Json;
using System.Xml.Serialization;
using CsvHelper;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using CsvHelper.Configuration;

namespace Project1TransBit16
{
    /* Enables the user to retrieve all information about the stored cities in the Dictionary generic type.*/
    public class Statistics
    {
        //ISSUE: citycatalogue is missing a few cities. 252 actual, 248 in csv and xml, 249 in json

        //Dictionary that holds the cities information returned from the DataModeler class.
        public Dictionary<string, CityInfo> CityCatalogue; 
        public List<CityInfo> resultCityList =null;
        public readonly string ApiKey= "5b3ce3597851110001cf624842d0804e6a864305a35699c60b2ede2d";


        private string filename;


        public Statistics(string filename, string fileType)
        {
            this.filename = filename; 
            DataModeler < Dictionary<string, CityInfo> >DataModeller= new DataModeler<Dictionary<string, CityInfo>>();
            CityCatalogue=DataModeller.ParseFile(Directory.GetCurrentDirectory() + "\\data\\" + filename+fileType);
            //CityCatalogue = DataModeller.ParseFile(Directory.GetCurrentDirectory() + "\\data\\test.csv");
        }

        /// <summary>
        /// /
        /// </summary>
        /// <returns></returns>
        public CityInfo DisplayCityInformation()
        {
            string data = "";
            bool Isvalid = false;
            List<string> loadData = new List<string>();
            CityInfo city = null;
            do
            {
                Console.WriteLine("Enter the city and province name separated by comma (,)");
                data = Console.ReadLine();
                loadData = data.Split(",").ToList();


                foreach (var c in CityCatalogue)
                {

                    if (loadData.Count == 2)
                    {
                        if (loadData[1] == "quebec" || loadData[1] == "Quebec")
                            loadData[1] = "Québec";
                        if (c.Value.city_ascii.ToLower().Equals(loadData[0].ToLower()) && c.Value.admin_name.ToLower().Equals(loadData[1].ToLower()))
                        {

                            city = c.Value;
                            return city;
                        }
                    }
                }
                Isvalid = false;
                Console.WriteLine("Error !: The city or province does not exist");

            } while (!Isvalid);

            return city;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="provinceName"></param>
        /// <returns></returns>
        public CityInfo DisplayLargestPopulationCity(string provinceName)
        {
            CityInfo Populouscity = new CityInfo();

            foreach (var city in CityCatalogue)
            {

                if (Populouscity.population < city.Value.population && city.Value.admin_name.ToLower().Equals(provinceName))
                {
                    Populouscity = city.Value;
                }
            }
            return Populouscity;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="city1"></param>
        /// <param name="city2"></param>
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
            Console.WriteLine($"City2: {city2.city} population: {city2.population}");
        }


        /// <summary>
        /// 
        /// </summary>
        //Use the name of the city and province to mark a city on the map.
        public void ShowCityOnMap()
        {

            CityInfo city = DisplayCityInformation();

            //uses the sytem.diagonostics process tool to execute the map of the city
            System.Diagnostics.Process.Start(
            new ProcessStartInfo
            {
                FileName = $"https://www.latlong.net/c/?lat={city.lat}&long={city.lng}",
                UseShellExecute = true
            });
            Console.WriteLine($"Link to view city on Map: https://www.latlong.net/c/?lat={city.lat}&long={city.lng}");

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="city1"></param>
        /// <param name="city2"></param>
        public async void CalculateDistanceBetweenCities(CityInfo city1, CityInfo city2)
        {
            //Api version
            string url=$"https://api.openrouteservice.org/v2/directions/driving-car?api_key={ApiKey}&start={city1.lng},{city1.lat}&end={city2.lng},{city2.lat}";
            const double meterToKm= 0.001;

            HttpClient client = new HttpClient();
            string responseBody = await client.GetStringAsync(url);
            JObject obj = JObject.Parse(responseBody);
           
            //NOw get the distance value from the JObject
            double distance = (double)(obj["features"][0]["properties"]["summary"]["distance"]);
            Console.WriteLine($"Distance Between {city1.city_ascii}, {city1.admin_name} to {city2.city_ascii}, {city2.admin_name} is {Math.Round(distance*meterToKm)} km.");



            /*    var sCoord = new GeoCoordinate(city1.lat, city1.lng);
                var eCoord = new GeoCoordinate(city2.lat, city2.lng);
                const double metertokm = 0.001;*/

            //    Console.WriteLine($"{Math.Round(sCoord.GetDistanceTo(eCoord) * metertokm)} km");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="provinceName"></param>
        /// <returns></returns>
        public CityInfo DisplaySmallestPopulationCity(string provinceName)
        {
            CityInfo smallPopcity = null;
            double population = Double.MaxValue;
                
            foreach (var city in CityCatalogue)
            {
                if (population > city.Value.population && city.Value.admin_name.ToLower().Equals(provinceName))
                {
                    smallPopcity = city.Value;
                    population = city.Value.population;
                }
            }

            return smallPopcity;
        }


//--Province Methods--//
        /// <summary>
        /// 
        /// </summary>
        /// <param name="provinceName"></param>
        /// <returns></returns>
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


        /// <summary>
        /// 
        /// </summary>
        /// <param name="provinceName"></param>
        /// <returns></returns>
        public List<CityInfo> DisplayProvinceCities(string provinceName)
        {
            //citycatalogue is missing 3 or 4 cities
            var citiesInProvince = from city in CityCatalogue
                                   where city.Value.admin_name.ToLower().Equals(provinceName)
                                   select city;

            List<CityInfo> list = new List<CityInfo>();
            foreach (var city in citiesInProvince)
            {
                list.Add(city.Value);
            }

            return list;
        }

        /// <summary>
        /// 
        /// </summary>
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
            String data = String.Format("{0,-5} {1,-25} {2,-10}\n","No", "City", "Population");
            foreach (var province in provinceAndPop.OrderByDescending(key => key.Value))
            {
                data += String.Format("{0,-5} {1,-25} {2, -10}\n",
               i++,province.Key,province.Value);
            }
            Console.WriteLine($"{data}");

        }


        /// <summary>
        /// 
        /// </summary>
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
         
            //String formatting
            String data = String.Format("{0,-5} {1,-25} {2,-10}\n", "No", "Province", "CityNumber");
            foreach (var provinceNumCombo in provinceAndNumCities.OrderByDescending(key => key.Value))
            {
                data += String.Format("{0,-5} {1,-25} {2, -10}\n",
               i++, provinceNumCombo.Key, provinceNumCombo.Value);
            }
            Console.WriteLine($"{data}");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="provinceName"></param>
        /// <returns></returns>
        public CityInfo GetCapital (string provinceName)
        {
            //!string.IsNullOrEmpty(city.Value.capital) 
            var capital =          from city in CityCatalogue
                                   where city.Value.admin_name.ToLower().Equals(provinceName)
                                            && city.Value.capital.Equals("admin")
                                   select city.Value;

            return capital.FirstOrDefault();
        }


        //--Write Out--//
        public void WriteToCSV()
        {

            try
            {
                //FileStream fs = new FileStream(Directory.GetCurrentDirectory() + "\\data\\test.csv", FileMode.OpenOrCreate);
                FileStream fs = new FileStream(Directory.GetCurrentDirectory() + "\\data\\" + filename + ".csv", FileMode.OpenOrCreate);
                using (var writer = new StreamWriter(fs, Encoding.Latin1))
                using (var csv = new CsvWriter(writer, System.Globalization.CultureInfo.InvariantCulture))
                {
                    csv.Context.RegisterClassMap<CityInfoMap>();
                    csv.WriteRecords(CityCatalogue.Values);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Environment.Exit(1);
            }
        }

        public void WriteToJSON()
        {
            try
            {
                //have not tested reading these files back in
                string payload = JsonConvert.SerializeObject(CityCatalogue.Values);
                //StreamWriter file = File.CreateText(Directory.GetCurrentDirectory() + "\\data\\test.json");
                StreamWriter file = File.CreateText(Directory.GetCurrentDirectory() + "\\data\\" + filename + ".json");
                file.Write(payload);
                file.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Environment.Exit(1);
            }
        }

        public void WriteToXML()
        {
            try
            {
                //files written with this method do read back in properly, but they contain information not present in the original file:
                    //<?xml version="1.0"?>
                    //<CanadaCities xmlns:xsi = "http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd = "http://www.w3.org/2001/XMLSchema">
                //we are requested not to edit the three source data files, so this may be a problem?
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<CityInfo>), new XmlRootAttribute("CanadaCities"));
                //FileStream file = File.Create(Directory.GetCurrentDirectory() + "\\data\\test.xml");
                FileStream file = File.Create(Directory.GetCurrentDirectory() + "\\data\\" + filename + ".xml");
                xmlSerializer.Serialize(file, CityCatalogue.Values.ToList());
                file.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Environment.Exit(1);
            }
        }

        //helper map class for Statistics.WriteToCSV, to force the columns to output in the specified order
        private sealed class CityInfoMap : ClassMap<CityInfo>
        {
            public CityInfoMap()
            {
                Map(m => m.city).Name("city");
                Map(m => m.city_ascii).Name("city_ascii");
                Map(m => m.lat).Name("lat");
                Map(m => m.lng).Name("lng");
                Map(m => m.country).Name("country");
                Map(m => m.admin_name).Name("admin_name");
                Map(m => m.capital).Name("capital");
                Map(m => m.population).Name("population");
                Map(m => m.id).Name("id");
            }
        }
    }
}
