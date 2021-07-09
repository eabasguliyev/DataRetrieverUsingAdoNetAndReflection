using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using PopulatingTable_Using_AdoNet_Reflection.DbManager;
using PopulatingTable_Using_AdoNet_Reflection.Entities;
using PopulatingTable_Using_AdoNet_Reflection.Properties;

namespace PopulatingTable_Using_AdoNet_Reflection
{
    class Program
    {
        static void Main(string[] args)
        {
            // change numeric decimal separator from ',' to '.'

            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";

            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;

            //prepareDatabase();

            using var sqlConnection = new SqlConnection
            {
                ConnectionString = Resources.ResourceManager.GetString("ConnectionStringHome")
            };


            sqlConnection.Open();

            Console.WriteLine(sqlConnection.State);

            // Creates table of Car

            using DatabaseManager manager = new DatabaseManager(sqlConnection);

            manager.CreateTableSchema<Car>();
            Console.WriteLine("Schema created or already exists.");

            var cars = new List<Car>()
            {
                new Car()
                {
                    Model = "X5",
                    Vendor = "BMW",
                    Engine = 4.2f,
                    Year = 2020
                },
                new Car()
                {
                    Model = "F10",
                    Vendor = "BMW",
                    Engine = 5f,
                    Year = 2019
                }
            };


            // Populates table of Car

            using DataPopulate dataPopulate = new DataPopulate(sqlConnection);
            
            if(dataPopulate.PopulateTable<Car>(cars))
                Console.WriteLine("Table populated.");


            // Loads Car table to memory and print
            using DataRetriever dataRetriever = new DataRetriever(sqlConnection);

            var cars2 = dataRetriever.GetAllData<Car>();


            printCars(cars2);

            Console.WriteLine(sqlConnection.State);
        }

        private static void printCars(IEnumerable<Car> cars)
        {
            Console.WriteLine($"|{"Vendor",20}|{"Model",20}|{"Engine",20}|{"Year",20}|");
            Console.WriteLine(new string('-', 85));
            foreach (var car in cars)
            {
                Console.WriteLine(car);
            }
        }


        // not used.
        private static void prepareDatabase()
        {
            using var sqlConnection = new SqlConnection();

            sqlConnection.ConnectionString = Resources.ResourceManager.GetString("MasterConnectionString");

            sqlConnection.Open();

            using var sqlCommand = new SqlCommand();

            sqlCommand.Connection = sqlConnection;

            sqlCommand.CommandText = Resources.ResourceManager.GetString("SchemaQuery");

            sqlCommand.ExecuteNonQuery();
        }
    }
}
