using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Resources;
using PopulatingTable_Using_AdoNet_Reflection.Properties;

namespace PopulatingTable_Using_AdoNet_Reflection
{
    class Program
    {
        static void Main(string[] args)
        {
            prepareDatabase();

            using var sqlConnection = new SqlConnection();

            sqlConnection.ConnectionString = Resources.ResourceManager.GetString("ConnectionString");

            sqlConnection.Open();


            using var sqlCommand = new SqlCommand();

            sqlCommand.Connection = sqlConnection;

            sqlCommand.CommandText = "SELECT * FROM cars;";


            DataRetriever<Car> dataRetriever = new DataRetriever<Car>(new SqlDataReaderAdapter(sqlCommand.ExecuteReader()));


            printCars(dataRetriever.GetAllData());
            //printCars(dataRetriever.GetAllData());
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
