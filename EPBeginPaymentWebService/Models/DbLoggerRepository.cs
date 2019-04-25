using EPBeginPaymentWebService.Interfaces;
using EPBeginPaymentWebService.Utilities;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using Serilog.Configuration;
using System.Collections.ObjectModel;
using System.Data;

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



            var columnOptions = new ColumnOptions {



                        AdditionalDataColumns = new Collection<DataColumn>
                        {
                           new DataColumn {DataType = typeof (string), ColumnName = "ServiceRequest"},
                           new DataColumn {DataType = typeof (string), ColumnName = "PaymentReference"},
                        }
                    

            };


            
            

            columnOptions.Store.Remove(StandardColumn.Properties);

            columnOptions.Store.Add(StandardColumn.LogEvent);

            


            _logger = new LoggerConfiguration()
             .MinimumLevel.Information()
             .WriteTo.MSSqlServer(_environmentConnectionString, "EpLog",columnOptions:columnOptions)
             .CreateLogger();
        }

        public static string LogUpdateBeginPaymentMessageTemplate => @"[File]:{@FileName},[Function]:{@MethodName},[Data]:[BeginPaymentId:{@beginPaymentId},CreateToken:{@createToken},ResponseCode:{@responseCode}]";

        public static string LogCreateBeginPaymentTemplateMessage => @"[File]:{@FileName},[Function]:{@MethodName},[Data]:[BillingAccount:{@BillingAccount},ServiceRequest:{@ServiceRequest},PaymentReference:{@PaymentReference},CreateToken:{@CreateToken},BeginPaymentId:{@BeginPaymentId},PaymentStage:{@LogPaymentStage},ComunicationStep:{@LogComunicationStep},Application:{@Application},ResponseMessage:{@responseCode}]";






        public static string PaymentStage => "BeginPayment";

        public static string ComunicationStep => "FROM SIEBEL TO .NET";

        public static string Application => "WCF WEB SERVICE";


        public void LogCreateBeginPayment(BeginPayment beginPayment, int beginPaymentId, string responseCode,string FileName,string MethodName)
        {
            _logger.Information(
                LogCreateBeginPaymentTemplateMessage,
                FileName,
                MethodName,
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

        public void LogUpdateBeginPayment(int beginPaymentId, string createToken,string responseCode,string FileName,string MethodName)
        {
            _logger.Information(
                LogUpdateBeginPaymentMessageTemplate,
                FileName,
                MethodName,
                beginPaymentId,
                createToken,
                responseCode
                );
        }
    }
}