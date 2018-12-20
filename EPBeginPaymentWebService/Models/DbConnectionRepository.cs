using System;
using System.Data;
using System.Data.SqlClient;
using EPBeginPaymentWebService.Interfaces;

namespace EPBeginPaymentWebService.Models
{
    public class DbConnectionRepository : IDbConnectionRepository
    {
        private readonly string _environmentConnectionString;

        public DbConnectionRepository()
        {
            _environmentConnectionString = Environment.GetEnvironmentVariable(
                "EpPaymentDevConnectionStringEnvironment", EnvironmentVariableTarget.Machine);
        }

        public IDbConnection CreateDbConnection()
        {
            return new SqlConnection(_environmentConnectionString);
        }

        public string GetConnectionString()
        {
            return _environmentConnectionString;
        }
    }
}