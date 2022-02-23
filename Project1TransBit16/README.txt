Program: Project1TransBit16
Authors: Gordon Reaman, Sagar Thapa
Date: 2022-02-22
Description: 
	This program exposes a means to serialize a fixed set of JSON, XML, and CSV files, which contain information about Canadian cities, their populations, and GPS coordinates.
	The "client" side, represented in Program.cs, allows for the user to choose which data source they wish to load from. DataModeler then performs the serialization process and loads the data into Statistics's member Dictionary. Then, the user will choose which of many operations they'd like to perform on the data. From here, control of the application will hand over to RequestHandler.
	RequestHandler then accepts input from the user based on which type of operation they'd liek to do, validates it, and calls the relevant Statistics method. With some exceptions, the RequestHandler will then print the results to console for the user to see. Then, control will hand back to Program.cs to expect a new operation from the user. 