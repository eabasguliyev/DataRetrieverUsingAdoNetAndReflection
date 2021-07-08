using System.Text;

namespace PopulatingTable_Using_AdoNet_Reflection.DbSchemaBuilder
{
    public class DatabaseBuilder
    {
        private readonly StringBuilder _str;

        public DatabaseBuilder()
        {
            _str = new StringBuilder();
        }

        public DatabaseBuilder SetDatabaseName(string dbName)
        {
            _str.Append($"CREATE DATABASE {dbName};");

            return this;
        }

        public string Build()
        {
            return _str.ToString();
        }
    }
}