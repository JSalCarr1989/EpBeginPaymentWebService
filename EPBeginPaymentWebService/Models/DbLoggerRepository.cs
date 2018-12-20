using EPBeginPaymentWebService.Interfaces;
using EPBeginPaymentWebService.Utilities;
using Serilog;


namespace EPBeginPaymentWebService.Models
{
    public class DbLoggerRepository : IDbLoggerRepository
    {
        private readonly ILogger _logger;
        private readonly IDbConnectionRepository _dbConnectionRepository;
        private readonly string _environmentConnectionString;

        public DbLoggerRepository()
        {

            _dbConnectionRepository = new DbConnectionRepository();

            _environmentConnectionString = _dbConnectionRepository.GetConnectionString();

            _logger = new LoggerConfiguration()
             .MinimumLevel.Information()
             .WriteTo.MSSqlServer(_environmentConnectionString, "EpLog")
             .CreateLogger();
        }

        public void LogCreateBeginPayment(BeginPayment beginPayment, int beginPaymentId)
        {
            _logger.Information(
                StaticBeginEnterpricePayment.LogCreateBeginPaymentTemplateMessage,
                beginPayment.BillingAccount,
                beginPayment.ServiceRequest,
                beginPayment.PaymentReference,
                beginPayment.CreateToken,
                beginPaymentId,
                StaticBeginEnterpricePayment.PaymentStage,
                StaticBeginEnterpricePayment.ComunicationStep,
                StaticBeginEnterpricePayment.Application
                );
        }
    }
}