using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace PopulatingTable_Using_AdoNet_Reflection.DbManager
{
    public class DataPopulate:IDisposable
    {
        private readonly SqlConnection _sqlConnection;
        private readonly SqlCommand _sqlCommand;
        public DataPopulate(SqlConnection sqlConnection)
        {
            _sqlConnection = sqlConnection ??
                             throw new ArgumentNullException(nameof(sqlConnection), "SqlConnection is null");

            _sqlCommand = new SqlCommand();
            _sqlCommand.Connection = _sqlConnection;
        }


        public bool PopulateTable<T>(List<T> collection) where T : new()
        {
            var bulkDataCreator = new BulkDataCreator();


            var query = bulkDataCreator.CreateBulkDataQuery<T>(collection);


            _sqlCommand.CommandText = query;

            return _sqlCommand.ExecuteNonQuery() > 0;
        }

        public void Dispose()
        {
            _sqlConnection?.Dispose();
            _sqlCommand?.Dispose();
        }
    }
}