using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Globalization;
using System.Reflection;
using PopulatingTable_Using_AdoNet_Reflection.CustomAttributes;
using PopulatingTable_Using_AdoNet_Reflection.DbSchemaBuilder;

namespace PopulatingTable_Using_AdoNet_Reflection.DbManager
{
    public class DatabaseManager:IDisposable
    {
        private readonly SqlConnection _sqlConnection;
        private readonly SqlCommand _sqlCommand;
        public DatabaseManager(SqlConnection sqlConnection)
        {
            _sqlConnection = sqlConnection ??
                             throw new ArgumentNullException(nameof(sqlConnection), "SqlConnection is null");
            _sqlCommand = new SqlCommand();
            _sqlCommand.Connection = _sqlConnection;
        }

        public void CreateTableSchema<T>() where T : new()
        {
            var schemaStr = CreateTable((new T()).GetType());

            _sqlCommand.CommandText = schemaStr;

            _sqlCommand.ExecuteNonQuery();   
          
        }

        private string CreateTable(Type typeOfObj)
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

        private string CreateColumn(PropertyInfo propertyInfo)
        {
            var propertyName = propertyInfo.Name.ToLower(new CultureInfo("es-ES", false));
            var propertyType = propertyInfo.PropertyType.Name;


            var columnBuilder = new ColumnBuilder();

            columnBuilder.SetColumnName(propertyName);

            SetColumnType(propertyInfo, propertyType, columnBuilder);

            var customAttributes = propertyInfo.GetCustomAttributes();

            SetColumnConstraints(customAttributes, columnBuilder, propertyType);

            return columnBuilder.Build();
        }

        private static void SetColumnConstraints(IEnumerable<Attribute> customAttributes, ColumnBuilder columnBuilder, string propertyType)
        {
            foreach (var customAttribute in customAttributes)
            {
                switch (customAttribute.GetType().Name)
                {
                    case "KeyAttribute":
                        columnBuilder.SetPrimaryKey();
                        break;
                    case "DatabaseGeneratedAttribute":
                    {
                        var option =
                            (DatabaseGeneratedOption) customAttribute.GetType().GetProperty("Option").GetValue(customAttribute);


                        if (option == DatabaseGeneratedOption.Identity)
                        {
                            if (propertyType != "Int32" && propertyType != "Int16" && propertyType != "Byte")
                                throw new InvalidEnumArgumentException("Non-numeric values can not be identity column!");

                            columnBuilder.SetIdentity();
                        }

                        break;
                    }
                    case "RequiredAttribute":
                        columnBuilder.SetNotNull();
                        break;
                    case "ExcludeAttribute":
                        throw new NotImplementedException();
                        break;
                }
            }
        }

        private static void SetColumnType(PropertyInfo propertyInfo, string propertyType, ColumnBuilder columnBuilder)
        {
            switch (propertyType)
            {
                case "Int32":
                    columnBuilder.SetColumnType(DataType.Int);
                    break;
                case "String":
                {
                    var length = -1;
                    var stringAttribute = propertyInfo.GetCustomAttribute((new MaxLengthAttribute()).GetType());

                    if (stringAttribute != null)
                    {
                        length = (int) stringAttribute.GetType().GetProperty("Length").GetValue(stringAttribute);
                    }

                    columnBuilder.SetColumnType(DataType.Varchar, length);
                    break;
                }
                case "Datetime":
                    columnBuilder.SetColumnType(DataType.Datetime);
                    break;
                case "Decimal":
                    columnBuilder.SetColumnType(DataType.Decimal);
                    break;
                case "Single":
                    columnBuilder.SetColumnType(DataType.Float);
                    break;
                default:
                    throw new InvalidOperationException("Unknown data type.");
            }
        }

        public void Dispose()
        {
            _sqlConnection?.Dispose();
            _sqlCommand?.Dispose();
        }
    }
}