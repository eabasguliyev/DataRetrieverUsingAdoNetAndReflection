using System;
using System.Data.SqlClient;
using System.Globalization;
using System.Reflection;

namespace PopulatingTable_Using_AdoNet_Reflection
{
    public class DatabaseManager
    {
        SqlConnection _sqlConnection;
        public DatabaseManager(SqlConnection sqlConnection)
        {
            _sqlConnection = sqlConnection;
        }

        public void CreateTableSchema<T>() where T : new()
        {
            var schemaStr = CreateTable((new T()).GetType());

            var sqlCommand = new SqlCommand();

            sqlCommand.Connection = _sqlConnection;

            sqlCommand.CommandText = schemaStr;

            Console.WriteLine(schemaStr);
            sqlCommand.ExecuteNonQuery();   
          
        }
        public string CreateTable(Type typeOfObj)
        {
            var properties = typeOfObj.GetProperties();

            
            var tableBuilder = new TableBuilder();

            var tableName = typeOfObj.Name.ToLower() + 's';

            tableBuilder.SetIfNotExist(tableName).SetTableName(tableName);

            foreach (var property in properties)
            {
              
                tableBuilder.AddColumn(CreateColumn(property));
            }

            
            return tableBuilder.Build();
        }

        public string CreateColumn(PropertyInfo propertyInfo)
        {
            var propertyName = propertyInfo.Name.ToLower(new CultureInfo("es-ES", false));
            var propertyType = propertyInfo.PropertyType.Name;


            var columnBuilder = new ColumnBuilder();

            columnBuilder.SetColumnName(propertyName);

            if (propertyType == "Int32")
            {
                columnBuilder.SetColumnType(DataType.Int);
            }
            else if (propertyType == "String")
            {
                columnBuilder.SetColumnType(DataType.Varchar, 255);
            }
            else if (propertyType == "Datetime")
            {
                columnBuilder.SetColumnType(DataType.Datetime);
            }
            else if (propertyType == "Decimal")
            {
                columnBuilder.SetColumnType(DataType.Decimal);
            }
            else if(propertyType == "Single")
            {
                columnBuilder.SetColumnType(DataType.Float);
            }

            return columnBuilder.Build();
        }

    }
}