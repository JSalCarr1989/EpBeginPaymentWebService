using EPBeginPaymentWebService.Interfaces;
using Serilog;

namespace EPBeginPaymentWebService.Models
{
    public class DbLoggerErrorRepository : IDbLoggerErrorRepository
    {

        private readonly ILogger _logger;
        private readonly IDbConnectionRepository _dbConnectionRepository;
        private readonly string _environmentConnectionString;

        public DbLoggerErrorRepository()
        {
            _dbConnectionRepository = new DbConnectionRepository();

            _environmentConnectionString = _dbConnectionRepository.GetConnectionString();

            _logger = new LoggerConfiguration()
             .MinimumLevel.Information()
             .WriteTo.MSSqlServer(_environmentConnectionString, "EpLogError")
             .CreateLogger();
        }

        public static string LogValidateIfPaymentInfoHasResponseTemplateMessage => @"An error:{@error} has ocurred in the function ValidateIfPaymentInfoHasResponse
                                                                                              for the ServiceRequest:{@ServiceRequest} and PaymentReference:{@PaymentReference}";

        public static string LogCreateBeginPaymentErrorTemplateMessage => @"An error:{@error} has ocurred in the function InsertBeginPayment
                                                                                  for the ServiceRequest:{@ServiceRequest} and PaymentReference:{@PaymentReference}";

        public static string LogUpdateBeginPaymentErrorTemplateMessage => @"An error:{@error} has ocurred in the function UpdateBeginPayment
                                                                                  for the BeginPaymentId:{@beginPaymentId}";

        public static string LogGetBeginPaymentErrorTemplateMessage => @"An error:{@error} has ocurred in the function GetBeginPayment
                                                                                  for the ServiceRequest:{@ServiceRequest} and PaymentReference:{@PaymentReference}";



        public void LogCreateBeginPaymentError(string error,BeginPayment beginPayment)
        {
            _logger.Error(LogCreateBeginPaymentErrorTemplateMessage,
                          error,
                          beginPayment.ServiceRequest,
                          beginPayment.PaymentReference);
        }

        public void LogGetBeginPaymentError(string error, BeginPayment beginPayment)
        {
            _logger.Error(LogGetBeginPaymentErrorTemplateMessage,
                         error,
                         beginPayment.ServiceRequest,
                         beginPayment.PaymentReference);
        }

        public void LogUpdateBeginPaymentError(string error, int beginPaymentId)
        {
            _logger.Error(LogUpdateBeginPaymentErrorTemplateMessage,
                          error,
                          beginPaymentId);
        }

        public void LogValidateIfPaymentInfoHasResponseError(string error,BeginPayment beginPayment)
        {
            _logger.Error(LogValidateIfPaymentInfoHasResponseTemplateMessage,
                          error,
                          beginPayment.ServiceRequest,
                          beginPayment.PaymentReference);
        }
    }
}