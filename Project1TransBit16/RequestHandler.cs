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

        public RequestHandler(Statistics stat)
        {
            this.stat = stat;
        }

        public void provinceCityHandler()
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



        public void largestCityHandler()
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



        public void smallestCityHandler()
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

        public void compareCityHandler()
        {

        }

        public void cityonMap()
        {

        }

        public void DistanceHandler()
        {

        }

        public void ProvincePopHandler()
        {

        }

        public void RankProvincePopHandler()
        {

        }

        public void RankProvByCityHandler()
        {

        }

        public void provinceCapHandler()
        {

        }









    }
}
