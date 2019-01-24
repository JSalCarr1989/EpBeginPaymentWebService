using EPBeginPaymentWebService.Interfaces;
using EPBeginPaymentWebService.Utilities;
using Dapper;
using System.Data;
using System;

namespace EPBeginPaymentWebService.Models
{
    public class LogPaymentRepository : ILogPaymentRepository
    {

        private readonly IDbConnectionRepository _dbConnectionRepository;
        private readonly IDbConnection _connection;
        private readonly IDbLoggerErrorRepository _dbLoggerErrorRepository;


        public LogPaymentRepository()
        {
            _dbConnectionRepository = new DbConnectionRepository();
            _connection = _dbConnectionRepository.CreateDbConnection();
            _dbLoggerErrorRepository = new DbLoggerErrorRepository();
        }

        public string ValidateIfPaymentInfoHasResponse(BeginPayment beginPayment)
        {
            string resultMessage = string.Empty;

            try
            {
                int count;
                bool paymentHasResponse;

                

                using (_connection)
                {
                    var parameters = new DynamicParameters();
                    parameters.Add(StaticLogPaymentProperties.SERVICE_REQUEST, beginPayment.ServiceRequest);
                    parameters.Add(StaticLogPaymentProperties.PAYMENT_REFERENCE, beginPayment.PaymentReference);
                    parameters.Add(StaticLogPaymentProperties.HAS_RESPONSE_PAYMENT, dbType: DbType.Int32,
                                    direction: ParameterDirection.Output);
                    _connection.Open();

                    _connection.Query(StaticLogPaymentProperties.SP_VALIDATE_IF_EXISTS_RESPONSEPAYMENT,
                                        parameters,
                                        commandType: CommandType.StoredProcedure);

                    count = parameters.Get<int>(StaticLogPaymentProperties.HAS_RESPONSE_PAYMENT_OUTPUT_SEARCH);

                    paymentHasResponse = (count > 0) ? true : false;

                    resultMessage = (paymentHasResponse) ? StaticLogPaymentProperties.ALREADY_PROCESED_RESPONSE_CODE 
                        : StaticLogPaymentProperties.WITHOUT_RESPONSE;

                    
                }

                
            }
            catch(Exception ex)
            {
                _dbLoggerErrorRepository.LogValidateIfPaymentInfoHasResponseError(ex.ToString(),beginPayment);
                return resultMessage = StaticLogPaymentProperties.ERROR_RESPONSE_CODE;
            }

            return resultMessage;
        }
    }
}