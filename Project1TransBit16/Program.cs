﻿/*
GroupName:TransBit
@authors: Sagar Thapa, Gordon Reaman
FileName: Program.cs
Description: Comprises the "client" of the program. Allows user to choose which file extension source to load from (MainMenu()), choose an operation (SubMenu()),
                and then control transfers over to RequestHandler.
Date: 2022-02-22
 
 */


using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Project1TransBit16
{
    class Program
    {
        //constant filenames
        const string JSON_FILENAME= "Canadacities-JSON";
        const string XML_FILENAME = "Canadacities-XML";
        const string CSV_FILENAME = "Canadacities";


       
        /// Main menu 
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

            Console.WriteLine("1) Canadiancities-CSV");
            Console.WriteLine("2) Canadiancities-JSON");
            Console.WriteLine("3) Canadiancities-XML");
            Console.WriteLine("Select an option from the list above (e.g. 1, 2");
        }

        /// Shows the various options to query to the user
        /// <param name="fileName"></param>
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
            Console.WriteLine("10) Rank Provinces By Cities");
            Console.WriteLine("11) Get Capital of Province");
            Console.WriteLine("12) Update the population for a city");
            Console.WriteLine("13) Restart Program And Choose Another File or File Type To Query");
            Console.WriteLine("Enter 'exit' to Exit the program");
        }

   
        /// Handles various options chosen by the user
        /// <param name="stat"></param>
        /// <param name="fileName"></param>
        public static void HandleOptions(ref Statistics stat, ref string fileName)
        {
          
            bool invalid = false;
            string input = "";

            try
            {
                RequestHandler req = new RequestHandler(stat);
                subMenu(fileName);
                do
                {
                    input = Console.ReadLine();
                    if (input == "1") { req.DisplayCityInformationHandler(); return; }
                    if (input == "2") { req.DisplayProvinceCitiesHandler(); return; }
                    if (input == "3") { req.largestCityHandler(); return; }
                    if (input == "4") { req.smallestCityHandler(); return; }
                    if (input == "5") { req.compareCityHandler(); return; }
                    if (input == "6") { req.ShowCityOnMapHandler(); return; }
                    if (input == "7") { req.DistanceHandler(); return; }
                    if (input == "8") { req.DisplayProvincePopulationHandler(); return; }
                    if (input == "9") { req.RankProvincesByPopulationHandler(); return; }
                    if (input == "10") { req.RankProvincesByNumCitiesHandler(); return; }
                    if (input == "11") { req.GetCapitalHandler(); return; }
                    if (input == "12") { req.UpdatePopulationForCityHandler(); return; }
                    if (input == "13") { Main(new string[0]); return; }
                    if (input == "exit") { System.Environment.Exit(0); }
                    invalid = true;
                    Console.WriteLine("Error! : Enter a valid option.");

                } while (invalid);

            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
         
        }

        ///Returns the type of file chosen by the users to query from
        /// <param name="fileName"></param>
        /// <returns>Statistics</returns>
        public static Statistics GetStatistics(ref string fileName)
        {
            try
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
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }


        static void Main(string[] args)
        {
            //Statistic object
            Statistics stat = null;
            string fileName = "";
            stat = GetStatistics(ref fileName);
           
            //Runs until the users wishes to exit
            while (true)
            {
                //calls the handleoptions function
                HandleOptions(ref stat, ref fileName);
                Console.WriteLine("Press any key to query again");
                Console.ReadKey();
            }
        }
    }
}
