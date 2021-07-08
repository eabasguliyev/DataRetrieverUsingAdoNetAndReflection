using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace PopulatingTable_Using_AdoNet_Reflection
{
    public class BulkDataCreator
    {

        public string CreateBulkDataQuery<T>(List<T> collection) where T:new()
        {
            var typeOfObj = (new T()).GetType();

            var properties = typeOfObj.GetProperties();

            var builder = new StringBuilder("INSERT INTO ");

            var tableName = typeOfObj.Name.ToLower() + "s";

            builder.Append(tableName + "(");


            for (int i = 0, length = properties.Length; i < length; i++)
            {
                builder.Append(properties[i].Name.ToLower(new CultureInfo("es-ES", false)));

                if (i != length - 1)
                    builder.Append(", ");
            }

            builder.Append(") VALUES");


            builder.Append(GetBulkData(collection));
            builder.Append(";");

            Console.WriteLine(builder.ToString());

            return builder.ToString();
        }

        private string GetBulkData<T>(List<T> collection) where T : new()
        {
            var builder = new StringBuilder();

            for (int i = 0, length = collection.Count; i < length; i++)
            {
                builder.Append("( ");

                var typeOfObj = (collection[i]).GetType();

                var properties = typeOfObj.GetProperties();

                for (int j = 0, length2 = properties.Length; j < length2; j++)
                {
                    var propertyType = properties[j].PropertyType.Name.ToString();


                    var value = properties[j].GetValue(collection[i]);


                    if (propertyType == "String")
                    {
                        builder.Append($"'{value}'");
                    }
                    else
                        builder.Append(value);

                    if (j != length2 - 1)
                        builder.Append(", ");
                }

                builder.Append(")");

                if (i != length - 1)
                    builder.Append(", ");
            }
            return builder.ToString();
        }
    }
}