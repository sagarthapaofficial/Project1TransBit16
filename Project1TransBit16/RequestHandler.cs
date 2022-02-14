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

        }

        public void DistanceHandler(Statistics stat)
        {

        }

        public void ProvincePopHandler(Statistics stat)
        {

        }

        public void RankProvincePopHandler(Statistics stat)
        {

        }

        public void RankProvByCityHandler(Statistics stat)
        {

        }

        public void provinceCapHandler(Statistics stat)
        {

        }









    }
}
