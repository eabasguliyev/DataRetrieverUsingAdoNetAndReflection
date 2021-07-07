using System;
using System.Collections.Generic;

namespace PopulatingTable_Using_AdoNet_Reflection
{
    public class DataRetriever<T> where T : new()
    {
        private IDataReader _dataReader;

        public DataRetriever(IDataReader dataReader)
        {
            _dataReader = dataReader ?? throw new InvalidOperationException("DataReader is null");
        }

        public IEnumerable<T> GetAllData()
        {
            if (!_dataReader.Read())
                throw new InvalidOperationException("There is no row left, you must be update data reader!");

            List<T> collection = new List<T>();


            while (_dataReader.Read())
            {
                T obj = new T();

                var propertiesOfCar = obj.GetType().GetProperties();

                foreach (var propertyInfo in propertiesOfCar)
                {
                    propertyInfo.SetValue(obj, _dataReader[propertyInfo.Name.ToLower()]);
                }

                collection.Add(obj);
            }

            return collection;
        }
    }
}