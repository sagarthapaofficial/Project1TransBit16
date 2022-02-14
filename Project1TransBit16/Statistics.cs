using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1TransBit16
{
    /* Enables the user to retrieve all information about the stored cities in the Dictionary generic type.*/
    public class Statistics
    {
        //Dictionary that holds the cities information returned from the DataModeler class.
        public Dictionary<string, CityInfo> CityCatalogue;
        public List<CityInfo> resultCityList =null;

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
                    foreach (var city in CityCatalogue)
                    {
                        if (city.Key.Equals(loadData[0]) && city.Value.admin_name.Equals(loadData[1]))
                        {
                            Isvalid = true;
                            Console.WriteLine(city.Value.ToString());
                            return;

                        }
                    }
                    Isvalid = false;
                    Console.WriteLine("Error !: The city or province does not exist");
                }
            } while (!Isvalid);


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
            return new CityInfo();
        }

        //Use the name of the city and province to mark a city on the map.
        public void ShowCityOnMap()
        {

        }

        public int CalculateDistanceBetweenCities(CityInfo city1, CityInfo city2)
        {
            // use google api to find the distance between two country.
            return 0;
        }





    }
}
