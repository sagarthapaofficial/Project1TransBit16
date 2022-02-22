/*
GroupName:TransBit
@authors: Sagar Thapa, Gordon Reaman
ProgramName: Statistics.cs
Date: 2022-02-22
 
 */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
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

        //Dictionary that holds the cities information returned from the DataModeler class.
        public Dictionary<string, CityInfo> CityCatalogue; 
        public List<CityInfo> resultCityList =null;
        //readonly APIkey
        public readonly string ApiKey= "5b3ce3597851110001cf624842d0804e6a864305a35699c60b2ede2d";
        private string filename;


        //Constructor
        public Statistics(string filename, string fileType)
        {
            try
            {
                this.filename = filename;
                DataModeler DataModeller = new DataModeler();
                CityCatalogue = DataModeller.ParseFile(Directory.GetCurrentDirectory() + "\\data\\" + filename + fileType);
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        /// / Retunrs the information of the city queried by the user
        /// @returns CityInfo object
        public CityInfo DisplayCityInformation(string cityName, string province)
        {
            CityInfo city = null;
            try
            {
                foreach (var c in CityCatalogue)
                {
                    //checks for the french accent word
                    if (province == "quebec" || province == "Quebec")
                        province = "Québec";

                    if (c.Value.city_ascii.ToLower().Equals(cityName.ToLower()) && c.Value.admin_name.ToLower().Equals(province.ToLower()))
                    {

                        return city = c.Value;
                    }

                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return city;
           
        }

        /// Returns the largest population city in the given province
        /// <param name="provinceName"></param>
        /// <returns>CityInfo</returns>
        public CityInfo DisplayLargestPopulationCity(string provinceName)
        {
            CityInfo Populouscity = null;
            try
            {
                Populouscity = new CityInfo();

                foreach (var city in CityCatalogue)
                {

                    if (Populouscity.population < city.Value.population && city.Value.admin_name.ToLower().Equals(provinceName))
                    {
                        Populouscity = city.Value;
                    }
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            //returns the populous city
            return Populouscity;
        }



        ///Compares given two cities and return the country with larger population
        /// <param name="city1"></param>
        /// <param name="city2"></param>
        /// //returns CityInfo
        public CityInfo CompareCitiesPopulation(CityInfo city1, CityInfo city2)
        {
            try
            {
                if (city1.population > city2.population)
                {
                    return city1;
                }
                else if (city1.population < city2.population)
                {
                    return city2;
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }


 
        /// </summary>
        //Use the name of the city and province to mark a city on the map.
        public void ShowCityOnMap(CityInfo city)
        {

            //uses the sytem.diagonostics process tool to execute the map of the city
            System.Diagnostics.Process.Start(
            new ProcessStartInfo
            {
                FileName = $"https://www.latlong.net/c/?lat={city.lat}&long={city.lng}",
                UseShellExecute = true
            });
            Console.WriteLine($"Link to view city on Map: https://www.latlong.net/c/?lat={city.lat}&long={city.lng}\n");

        }


        //gets the data from the server
        /// <param name="city1"></param>
        /// <param name="city2"></param>
        private string getData(string url)
        {
            string checkResult = "";
            try
            {
                HttpClient client = new HttpClient();
                Task<string> t = client.GetStringAsync(url);
                checkResult = t.Result;
                client.Dispose();
                return checkResult;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return checkResult;

        }

        //finds the distance between two cities
        /// <param name="city1"></param>
        /// <param name="city2"></param>
        public void CalculateDistanceBetweenCities(CityInfo city1, CityInfo city2)
        {
            //Api version
            string url = $"https://api.openrouteservice.org/v2/directions/driving-car?api_key={ApiKey}&start={city1.lng},{city1.lat}&end={city2.lng},{city2.lat}";
            const double meterToKm = 0.001;
            try
            {
                var responseBody = getData(url);
                //converts the string to json object
                JObject obj = JObject.Parse(responseBody);

                //NOw get the distance value from the JObject
                double distance = (double)(obj["features"][0]["properties"]["summary"]["distance"]);
                Console.WriteLine($"\nDistance Between {city1.city_ascii}, {city1.admin_name} to {city2.city_ascii}, {city2.admin_name} is {Math.Round(distance * meterToKm)} km.\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        //Returns the city with the smallest population
        /// <param name="provinceName"></param>
        /// <returns>CityInfo</returns>
        public CityInfo DisplaySmallestPopulationCity(string provinceName)
        {
            CityInfo smallPopcity = null;

            try
            {
                double population = Double.MaxValue;

                foreach (var city in CityCatalogue)
                {
                    if (population > city.Value.population && city.Value.admin_name.ToLower().Equals(provinceName))
                    {
                        smallPopcity = city.Value;
                        population = city.Value.population;
                    }
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return smallPopcity;
        }


        //--Province Methods--//

        ///Displays the population of the given province
        /// <param name="provinceName"></param>
        /// <returns>population</returns>
        public double DisplayProvincePopulation(string provinceName)
        {

            double population = 0;

            try
            {
                var citiesInProvince = from city in CityCatalogue
                                       where city.Value.admin_name.ToLower().Equals(provinceName.ToLower())
                                       select city;

                foreach (var city in citiesInProvince)
                {
                    population += city.Value.population;
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            //returns the population
            return population;
        }


        ///Returns the list of cities in the given province
        /// <param name="provinceName"></param>
        /// <returns>List<CityInfo></returns>
        public List<CityInfo> DisplayProvinceCities(string provinceName)
        {
            List<CityInfo> list = null;
            try
            {
                //citycatalogue is missing 3 or 4 cities
                var citiesInProvince = from city in CityCatalogue
                                       where city.Value.admin_name.ToLower().Equals(provinceName)
                                       select city;

                list = new List<CityInfo>();
                foreach (var city in citiesInProvince)
                {
                    list.Add(city.Value);
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //returns the list
            return list;
        }

    
         /// Ranks provinces by population   
        public void RankProvincesByPopulation()
        {
            try
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
                String data = String.Format("{0,-5} {1,-25} {2,-10}\n", "No", "City", "Population");
                foreach (var province in provinceAndPop.OrderByDescending(key => key.Value))
                {
                    data += String.Format("{0,-5} {1,-25} {2, -10}\n",
                   i++, province.Key, province.Value);
                }
                Console.WriteLine($"{data}");
           
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }


        
        /// Ranks Provinces by number of cities it has
        public void RankProvincesByCities()
        {
            //get all distinct provinces from citycatalogue. result should be an enumerable of string
            var distinctProvinces = (from city in CityCatalogue
                                    select city.Value.admin_name).Distinct();

            Dictionary<string, int> provinceAndNumCities = new Dictionary<string, int>();

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

        /// Gets the capital of the given province
        /// <param name="provinceName"></param>
        /// <returns>CityInfo</returns>
        public CityInfo GetCapital (string provinceName)
        {
                //!string.IsNullOrEmpty(city.Value.capital) 
               var  capital = from city in CityCatalogue
                              where city.Value.admin_name.ToLower().Equals(provinceName.ToLower())
                                       && city.Value.capital.Equals("admin")
                              select city.Value;

            return capital.FirstOrDefault();
        }


        //--Write Out--//
        //Writes changes to the CSV file
        public void WriteToCSV()
        {
            try
            {
                //FileStream fs = new FileStream(Directory.GetCurrentDirectory() + "\data\test.csv", FileMode.OpenOrCreate);
                FileStream fs = new FileStream(Directory.GetCurrentDirectory() + "\\data\\" + filename + ".csv", FileMode.OpenOrCreate);
                var writer = new StreamWriter(fs, Encoding.UTF8);
                var csv = new CsvWriter(writer, System.Globalization.CultureInfo.InvariantCulture);

                csv.Context.RegisterClassMap<CityInfoMap>();
                csv.WriteRecords(CityCatalogue.Values.ToList());

                fs.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Environment.Exit(1);
            }
        }

        //Writes changes to the json file
        public void WriteToJSON()
        {
            try
            {
                //have not tested reading these files back in
                string payload = JsonConvert.SerializeObject(CityCatalogue.Values);
                //StreamWriter file = File.CreateText(Directory.GetCurrentDirectory() + "\\data\\test.json");
                StreamWriter file = File.CreateText(Directory.GetCurrentDirectory() + "\\data\\Canadacities-JSON.json");
                file.Write(payload);
                file.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Environment.Exit(1);
            }
        }

        //Writes changes to the Xml file
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
                FileStream file = File.Create(Directory.GetCurrentDirectory() + "\\data\\CanadaCities-XML.xml");
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
