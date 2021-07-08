using System.Collections.Generic;
using System.Data.SqlClient;

namespace PopulatingTable_Using_AdoNet_Reflection
{
    public class DataPopulate
    {
        SqlConnection _sqlConnection;
        public DataPopulate(SqlConnection sqlConnection)
        {
            _sqlConnection = sqlConnection;
        }


        public bool PopulateTable<T>(List<T> collection) where T : new()
        {
            var bulkDataCreator = new BulkDataCreator();


            var query = bulkDataCreator.CreateBulkDataQuery<T>(collection);

            var sqlCommand = new SqlCommand();

            sqlCommand.CommandText = query;
            sqlCommand.Connection = _sqlConnection;

            return sqlCommand.ExecuteNonQuery() > 0;
        }
    }
}