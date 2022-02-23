
/*
GroupName:TransBit
@authors: Sagar Thapa, Gordon Reaman
ProgramName: DataModeler.cs
Date: 2022-02-22
Description: Parses XML, JSON, or CSV files for Canadian city information. Serializes resulting data into CityInfo objects
 */

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
    public class DataModeler
    {
        //holds the city
        private static List<CityInfo> list;
        //holds the cities in the dictionary id as the key of it
        private static Dictionary<string, CityInfo> Citydictionary;

        /// <summary>
        /// Parse the file type choosen by the end user
        /// <param name="fileName"></param>
        /// <returns>Dictionary<string, CityInfo></string></returns>
        public Dictionary<string, CityInfo> ParseFile(string fileName)
        {
            try
            {
                string fileType = Path.GetExtension(fileName);
                if (fileType == ".json")
                {
                    return ParseJSON(fileName);
                }
                else if (fileType == ".xml")
                {
                    return ParseXML(fileName);
                }
                else if (fileType == ".csv")
                {
                    return ParseCSV(fileName);
                }
                else
                {
                    Console.WriteLine("Error: Invalid file type!");
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //returns the Dictionary
            return new Dictionary<string, CityInfo>();
        }

        /// <summary>
        /// Parse the XMl file type choosen by the end user
        /// <param name="fileName"></param>
        /// <returns>Dictionary<string, CityInfo></string></returns>
        public static Dictionary<string, CityInfo> ParseXML(string fileName)
        {
            try
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

                    if (!Citydictionary.ContainsKey(getlist.id))
                    {
                        Citydictionary.Add(getlist.id, getlist);
                    }
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                System.Environment.Exit(0);
            }

            return Citydictionary;
        }

        /// <summary>
        /// Parse the Json file type choosen by the end user
        /// <param name="fileName"></param>
        /// <returns>Dictionary<string, CityInfo></string></returns>
        public static Dictionary<string, CityInfo> ParseJSON(string fileName)
        {
            try
            {
                list = new List<CityInfo>();
                Citydictionary = new Dictionary<string, CityInfo>();
                //deserealize the json txt on the list
                list = JsonConvert.DeserializeObject<List<CityInfo>>(File.ReadAllText(fileName));

                foreach (var getlist in list)
                {
                    if (!Citydictionary.ContainsKey(getlist.id) && getlist.id != "")
                    {
                        Citydictionary.Add(getlist.id, getlist);
                    }
                }
                

            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                System.Environment.Exit(0);
            }
            return Citydictionary;
        }

        /// <summary>
        /// Parse the CSV file choosen by the end user
        /// <param name="fileName"></param>
        /// <returns>Dictionary<string, CityInfo></string></returns>
        public static Dictionary<string, CityInfo> ParseCSV(string fileName)
        {

            try
            {
                String[] csvData = File.ReadAllLines(fileName, Encoding.GetEncoding("iso-8859-1"));
                Citydictionary = new Dictionary<string, CityInfo>();
                //Split 
                var cityInfo = new List<CityInfo>();
                for (int i = 1; i < csvData.Length; i++)
                {
                    if (string.IsNullOrEmpty(csvData[i]) || csvData[i].Split(',').Length < 9)
                        continue;
                    CityInfo info = new CityInfo(csvData[i]);
                    cityInfo.Add(info);
                }

                foreach (var getlist in cityInfo)
                {
                    if (!Citydictionary.ContainsKey(getlist.id))
                    {
                        Citydictionary.Add(getlist.id, getlist);
                    }
                }

            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                System.Environment.Exit(0);
            }
            return Citydictionary;

        }

    }
}
