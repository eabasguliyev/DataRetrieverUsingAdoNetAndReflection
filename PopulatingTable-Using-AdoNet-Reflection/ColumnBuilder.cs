using System.Text;

namespace PopulatingTable_Using_AdoNet_Reflection
{
    public class ColumnBuilder
    {
        private readonly StringBuilder _str;

        public ColumnBuilder()
        {
            _str = new StringBuilder();
        }

        public ColumnBuilder SetColumnName(string columnName)
        {
            _str.Append(columnName);

            return this;
        }

        public ColumnBuilder SetColumnType(DataType dataType, int length = 0)
        {
            _str.Append($" {dataType.ToString().ToUpper()}");

            if (dataType == DataType.Varchar)
                _str.Append($"({length})");
            else if (dataType == DataType.Decimal)
                _str.Append($"(18, 5)");
            return this;
        }

        public ColumnBuilder SetNotNull()
        {
            _str.Append($" NOT NULL");

            return this;
        }

        public ColumnBuilder SetIdentity(int startIndex = 1, int seedValue = 1)
        {
            _str.Append($" IDENTITY({startIndex},{seedValue})");

            return this;
        }

        public ColumnBuilder SetPrimaryKey()
        {
            _str.Append(" PRIMARY KEY");

            return this;
        }

        public string Build()
        {
            return _str.ToString();
        }
    }
}