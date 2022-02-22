/*
GroupName:TransBit
@authors: Sagar Thapa, Gordon Reaman
ProgramName: CityInfo.cs
Date: 2022-02-22
 
 */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Project1TransBit16
{
    //holds information about the city.
    [XmlType("CanadaCity")]
    public class CityInfo
    {

        //Default constructor
        public CityInfo()
        {
            city = "";
            city_ascii = "";
            lat = 0.0;
            lng = 0.0;
            country = "";
            admin_name = "";
            capital = "";
            population = 0.0;
            id = "";
        }
        //CityInfo takes data
        public CityInfo(string data)
        {
            string []getData= data.Split(',');
            city = getData[0];
            city_ascii = getData[1];
            lat = Convert.ToDouble(getData[2]);
            lng = Convert.ToDouble(getData[3]);
            country = getData[4];
            admin_name = getData[5];
            capital = getData[6];
            population = Convert.ToDouble(getData[7]);
            id = getData[8];

        }

        //getters and setters
        public string city { get; set; }
       
        public  string  city_ascii { get; set; }
       
        public  double lat { get; set; }

        public double lng { get; set; }
      
        public  string capital { get; set; }
        public  string country { get; set; }
       
        public  string admin_name { get; set; }
       
      
        public  double population { get; set; }
       
        public  string id { get; set; }

        //ToString method
        public override string ToString()
        {
            return $"City: {city}, city_Ascii:{city_ascii}, Latitude: {lat}, Longitude: {lng}, Country: {country}, Province: {admin_name}, Population: {population}, Capital: {capital}";
        }

    }
}
