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

        public static string LogValidateIfPaymentInfoHasResponseTemplateMessage => @"[Error]:{@error},[File]:{@File},[Function]:{@Function},[Data]:[ServiceRequest:{@ServiceRequest},PaymentReference:{@PaymentReference}]";
        public static string LogCreateBeginPaymentErrorTemplateMessage => @"[Error]:{@error},[File]:{@File},[Function]:{@Function},[Data]:[ServiceRequest:{@ServiceRequest},PaymentReference:{@PaymentReference}]";
        public static string LogUpdateBeginPaymentErrorTemplateMessage => @"[Error]:{@error},[File]:{@File},[Function]:{@Function},[Data]:[BeginPaymentId:{@beginPaymentId}]";
        public static string LogGetBeginPaymentErrorTemplateMessage => @"[Error]:{@error},[File]:{@File},[Function]:{@Function},[Data]:[ServiceRequest:{@ServiceRequest},PaymentReference:{@PaymentReference}]";



        public void LogCreateBeginPaymentError(string error,BeginPayment beginPayment, string FileName, string MethodName)
        {
            _logger.Error(LogCreateBeginPaymentErrorTemplateMessage,
                          FileName,
                          MethodName,
                          error,
                          beginPayment.ServiceRequest,
                          beginPayment.PaymentReference);
        }

        public void LogGetBeginPaymentError(string error, BeginPayment beginPayment, string FileName, string MethodName)
        {
            _logger.Error(LogGetBeginPaymentErrorTemplateMessage,
                         FileName,
                         MethodName,
                         error,
                         beginPayment.ServiceRequest,
                         beginPayment.PaymentReference);
        }

        public void LogUpdateBeginPaymentError(string error, int beginPaymentId, string FileName, string MethodName)
        {
            _logger.Error(LogUpdateBeginPaymentErrorTemplateMessage,
                          FileName,
                          MethodName,
                          error,
                          beginPaymentId);
        }

        public void LogValidateIfPaymentInfoHasResponseError(string error,BeginPayment beginPayment, string FileName, string MethodName)
        {
            _logger.Error(LogValidateIfPaymentInfoHasResponseTemplateMessage,
                          FileName,
                          MethodName,
                          error,
                          beginPayment.ServiceRequest,
                          beginPayment.PaymentReference);
        }
    }
}