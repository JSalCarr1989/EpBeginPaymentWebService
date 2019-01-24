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

        public static string LogUpdateBeginPaymentMessageTemplate => @"The BeginPayment with BeginPaymentId:{@beginPaymentId}
                                                                           was updated with the following values: CreateToken:{@createToken} ResponseCode:{@responseCode}";

        public static string LogCreateBeginPaymentTemplateMessage => @"Inserted BeginPayment with the following data: 
                                         BillingAccount: {@BillingAccount} 
                                         ServiceRequest: {@ServiceRequest} 
                                         PaymentReference:{@PaymentReference} 
                                         CreateToken:{@CreateToken} 
                                         BeginPaymentId: {@BeginPaymentId} 
                                         PaymentStage:{@LogPaymentStage} 
                                         ComunicationStep:{@LogComunicationStep} 
                                         Application:{@Application}
                                         ResponseMessage:{@responseCode}";

        public static string PaymentStage => "BeginPayment";

        public static string ComunicationStep => "FROM SIEBEL TO .NET";

        public static string Application => "WCF WEB SERVICE";


        public void LogCreateBeginPayment(BeginPayment beginPayment, int beginPaymentId, string responseCode)
        {
            _logger.Information(
                LogCreateBeginPaymentTemplateMessage,
                beginPayment.BillingAccount,
                beginPayment.ServiceRequest,
                beginPayment.PaymentReference,
                beginPayment.CreateToken,
                beginPaymentId,
                PaymentStage,
                ComunicationStep,
                Application,
                responseCode
                );
        }

        public void LogUpdateBeginPayment(int beginPaymentId, string createToken,string responseCode)
        {
            _logger.Information(
                LogUpdateBeginPaymentMessageTemplate,
                beginPaymentId,
                createToken,
                responseCode
                );
        }
    }
}