using System.Data;

namespace EPBeginPaymentWebService.Interfaces
{
    public interface IDbConnectionRepository
    {
        IDbConnection CreateDbConnection();
        string GetConnectionString();
    }
}