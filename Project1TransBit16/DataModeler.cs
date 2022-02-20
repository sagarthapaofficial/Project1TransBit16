using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Linq;

namespace Project1TransBit16
{
    public class DataModeler<T>
    {
        private static List<CityInfo> list;
        private static Dictionary<string, CityInfo> Citydictionary;
        public Dictionary<string, CityInfo> ParseFile(string fileName)
        {
            string fileType=Path.GetExtension(fileName);
            if(fileType==".json")
            {
               return ParseJSON(fileName);
            }else if(fileType==".xml")
            {
                return ParseXML(fileName);
            }else if(fileType==".csv")
            {
                return ParseCSV (fileName);
            }else
            {
                Console.WriteLine("Error: Invalid file type!");
            }
            return new Dictionary<string, CityInfo>();
        }
        public static Dictionary<string, CityInfo> ParseXML(string fileName)
        {

            list = new List<CityInfo>();
            Citydictionary = new Dictionary<string, CityInfo>();
         
            //the root element is CanadaCities to we initiliaze in the serializer
            XmlSerializer serializer = new XmlSerializer(typeof(List<CityInfo>), new XmlRootAttribute("CanadaCities"));
            using (Stream reader = new FileStream(fileName, FileMode.Open))
            {
                // Call the Deserialize method to restore the object's state.
                list = (List<CityInfo>)serializer.Deserialize(reader);
                
            }
            foreach (var getlist in list)
            {
                if (!Citydictionary.ContainsKey(getlist.city_ascii))
                {
                    Citydictionary.Add(getlist.city_ascii, getlist);
                }
            }
            return Citydictionary;
        }

        public static Dictionary<string, CityInfo> ParseJSON(string fileName)
        {
            list= new List<CityInfo>();
            Citydictionary = new Dictionary<string, CityInfo>();
            //deserealize the json txt on the list
            list = JsonConvert.DeserializeObject<List<CityInfo>>(File.ReadAllText(fileName));

            foreach (var getlist in list)
            {
                if (!Citydictionary.ContainsKey(getlist.city_ascii))
                {
                    Citydictionary.Add(getlist.city_ascii, getlist);
                }
            }
            return Citydictionary;
        }

        //Parses csv file.
        public static Dictionary<string, CityInfo> ParseCSV(string fileName)
        {
            String[] csvData= File.ReadAllLines(fileName);
            Citydictionary = new Dictionary<string, CityInfo>();
            //Split 
            var cityInfo = new List<CityInfo>();
            for(int i=1;i<csvData.Length;i++)
            {
                CityInfo info = new CityInfo(csvData[i]);
                cityInfo.Add(info);
            }

            foreach (var getlist in cityInfo)
            {
                if (!Citydictionary.ContainsKey(getlist.city_ascii))
                {
                    Citydictionary.Add(getlist.city_ascii, getlist);
                }
            }
            return Citydictionary;

        }

    }
}
