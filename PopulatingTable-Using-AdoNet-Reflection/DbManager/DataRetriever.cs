using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace PopulatingTable_Using_AdoNet_Reflection.DbManager
{
    public class DataRetriever
    {
        private readonly SqlConnection _sqlConnection;

        public DataRetriever(SqlConnection sqlConnection)
        {
            _sqlConnection = sqlConnection ??
                             throw new ArgumentNullException(nameof(sqlConnection), "SqlConnection is null");
        }

        public IEnumerable<T> GetAllData<T>() where T : new()
        {
            var str = GetSelectionQuery((new T().GetType()));

            using var sqlCommand = new SqlCommand(str, _sqlConnection);

            var dataReader = sqlCommand.ExecuteReader();

            List<T> collection = new List<T>();

            while (dataReader.Read())
            {
                T obj = new T();

                var propertiesOfCar = obj.GetType().GetProperties();

                foreach (var propertyInfo in propertiesOfCar)
                {
                    if (propertyInfo.PropertyType.Name == "Single")
                        propertyInfo.SetValue(obj, Convert.ToSingle(dataReader[propertyInfo.Name.ToLower()]));
                    else
                        propertyInfo.SetValue(obj, dataReader[propertyInfo.Name.ToLower()]);
                }

                collection.Add(obj);
            }

            return collection;
        }

        private string GetSelectionQuery(Type typeOfObj)
        {
            return $"SELECT * FROM {typeOfObj.Name.ToLower() + "s"};";
        }
    }
}