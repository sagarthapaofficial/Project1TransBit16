﻿using System;
using System.Collections.Generic;

namespace Project1TransBit16
{
    class Program
    {
        const string JSON_FILENAME= "Canadacities-JSON";
        const string XML_FILENAME = "Canadacities-XML";
        const string CSV_FILENAME = "Canadacities";

        public static void MainMenu()
        {
            Console.WriteLine("---------------------");
            Console.WriteLine("Program functionality");
            Console.WriteLine("---------------------");

            Console.WriteLine("-To exit program, enter 'exit' at any prompt");
            Console.WriteLine("-To start program from the begining enter 'restart' at any prompt");
            Console.WriteLine("-You will be presented with a numbered list of options. Please enter a value, when prompted,");
            Console.WriteLine(" to a corresponding file name, file type or data querying routine.");
            Console.WriteLine("Fetching list of available file names to be processed and queried...");

            Console.WriteLine("1) Candiancities-CSV");
            Console.WriteLine("2) Candiancities-JSON");
            Console.WriteLine("3) Candiancities-XML");
            Console.WriteLine("Select an option from the list above (e.g. 1, 2");
        }


        public static void subMenu(string fileName)
        {
            Console.WriteLine($"A city catalogue has now been populated from the {fileName} file.");
            Console.WriteLine($"Fetching list of available data querying routines that can be run on the {fileName} file.");
            Console.WriteLine("1)  Display City Information");
            Console.WriteLine("2)  Display Province Cities");
            Console.WriteLine("3)  Display Largest Population City");
            Console.WriteLine("4)  Display Smallest Population City");
            Console.WriteLine("5)  Compare Cities Population");
            Console.WriteLine("6)  Show City on Map");
            Console.WriteLine("7)  Calculate Distance Between Cities");
            Console.WriteLine("8)  Display Province Population");
            Console.WriteLine("9)  Rank Provinces By Population");
            Console.WriteLine("10)  Rank Provinces By Cities");
            Console.WriteLine("11) Get Capital of Province");
            Console.WriteLine("12) Restart Program And Choose Another File or File Type To Query");

        }

   
        public static void DisplayCountryInfo(ref Statistics stat, ref string fileName)
        {
          
            bool invalid = false;
            string input = "";
            RequestHandler req = new RequestHandler();
            subMenu(fileName);
            do
            {
                input = Console.ReadLine();
                if (input == "1") { stat.DisplayCityInformation(); return; }
                if (input == "2") { req.provinceCityHandler(stat);return; }
                if (input == "3") { req.largestCityHandler(stat); return; }
                if (input == "4") { req.smallestCityHandler(stat);return; }
                if (input == "5") { req.compareCityHandler(stat);return; }
                if (input == "6") { req.cityonMap(stat); return; }
                if (input == "7") { req.DistanceHandler(stat); return; }
                if (input == "8") { req.ProvincePopHandler(stat); return; }
                if (input == "9") { req.RankProvincePopHandler(stat); return; }
                if (input == "10") { req.RankProvByCityHandler(stat); return; }
                if (input == "11") { req.provinceCapHandler(stat); return; }
                if (input == "12") { Main(new string[0]); return; }
                if (input == "exit") { System.Environment.Exit(0); }
                invalid = true;
                Console.WriteLine("Error! : Enter a valid option.");

            } while (invalid);
          

        }
        public static Statistics GetStatistics(ref string fileName)
        {
            MainMenu();
            bool invalid = false;
            string input = "";
            do
            {
                input = Console.ReadLine();
                if (input == "1") return new Statistics(CSV_FILENAME, ".csv");
                if (input == "2") return new Statistics(JSON_FILENAME, ".json");
                if (input == "3") return new Statistics(XML_FILENAME, ".xml");
                if (input == "exit") System.Environment.Exit(0);
                

                invalid = true;
                Console.WriteLine("Error! : Enter a valid option.");

            } while (invalid);

            return null;
        }

      
        static void Main(string[] args)
        {
            Statistics stat = null;
            string fileName = "";
            stat = GetStatistics(ref fileName);
            
            while (true)
            {
                DisplayCountryInfo(ref stat, ref fileName);
                Console.WriteLine("Hit Enter key if you want to continue quering.");
                Console.ReadLine();
            }
        }
    }
}
