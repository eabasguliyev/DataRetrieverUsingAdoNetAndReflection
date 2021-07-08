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
            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";

            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;

            //prepareDatabase();

            using var sqlConnection = new SqlConnection();

            sqlConnection.ConnectionString = Resources.ResourceManager.GetString("ConnectionString");

            sqlConnection.Open();

            Console.WriteLine(sqlConnection.State);

            //using var sqlCommand = new SqlCommand();

            //sqlCommand.Connection = sqlConnection;

            //sqlCommand.CommandText = "SELECT * FROM cars;";


            //var dataReader = sqlCommand.ExecuteReader();

            //var sqlDataReaderAdapter = new SqlDataReaderAdapter(dataReader);

            //DataRetriever<Car> dataRetriever = new DataRetriever<Car>(sqlDataReaderAdapter);


            //printCars(dataRetriever.GetAllData());


            //dataReader.Close();

            //dataReader = sqlCommand.ExecuteReader();

            //sqlDataReaderAdapter.UpdateDataReader(dataReader);

            //printCars(dataRetriever.GetAllData());


            var manager = new DatabaseManager(sqlConnection);

            manager.CreateTableSchema<Car>();

            var cars = new List<Car>()
            {
                new Car()
                {
                    Id = 1,
                    Model = "X5",
                    Vendor = "BMW",
                    Engine = 4.2f,
                    Year = 2020
                },
                new Car()
                {
                    Id = 2,
                    Model = "F10",
                    Vendor = "BMW",
                    Engine = 5f,
                    Year = 2019
                }
            };

            var dataPopulate = new DataPopulate(sqlConnection);

            Console.WriteLine(dataPopulate.PopulateTable<Car>(cars));
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
