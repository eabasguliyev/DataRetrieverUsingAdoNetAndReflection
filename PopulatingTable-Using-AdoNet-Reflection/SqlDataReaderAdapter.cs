using System.Data.SqlClient;

namespace PopulatingTable_Using_AdoNet_Reflection
{
    public class SqlDataReaderAdapter:IDataReader
    {
        private SqlDataReader _dataReader;
        public SqlDataReaderAdapter(SqlDataReader dataReader)
        {
            _dataReader = dataReader;
        }

        public object this[string k] => _dataReader[k];
        public bool Read()
        {
            return _dataReader.Read();
        }

        public object GetData(string key)
        {
            return _dataReader[key];
        }

        public void Dispose()
        {
            _dataReader?.Dispose();
        }

        public void UpdateDataReader(SqlDataReader dataReader)
        {
            _dataReader = dataReader;
        }
    }
}