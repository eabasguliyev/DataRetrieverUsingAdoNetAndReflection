using System.Text;

namespace PopulatingTable_Using_AdoNet_Reflection.DbSchemaBuilder
{
    public class TableBuilder
    {
        private readonly StringBuilder _str;

        private bool _firstColumn;
        private bool _firstBuild;
        public TableBuilder()
        {
            _str = new StringBuilder();

            _firstColumn = true;
            _firstBuild = true;
        }

        public TableBuilder SetIfNotExist(string tableName)
        {

            _str.Append($"IF NOT EXISTS(SELECT * FROM sysobjects WHERE name = '{tableName}' AND XTYPE = 'U')");
                return this;
        }
        public TableBuilder SetTableName(string tableName)
        {
            _str.Append($"CREATE TABLE {tableName}");

            return this;
        }

        public TableBuilder AddColumn(string column)
        {
            _str.Append(_firstColumn ? " (" : ", ");

            _str.Append(column);

            _firstColumn = false;
            return this;
        }

        public string Build()
        {
            if(_firstBuild)
            {
                _str.Append(");");
                _firstBuild = false;
            }

            return _str.ToString();
        }
    }
}