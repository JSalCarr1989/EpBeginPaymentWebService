using EPBeginPaymentWebService.Interfaces;
using EPBeginPaymentWebService.Utilities;
using System.Data;
using Dapper;
using System;

namespace EPBeginPaymentWebService.Models
{
    public class BeginPaymentRepository : IBeginPaymentRepository
    {
        private readonly IDbConnectionRepository _dbConnectionRepository;
        private IDbConnection _connection;
        private readonly IDbLoggerRepository _dbLoggerRepository;
        private readonly IDbLoggerErrorRepository _dbLoggerErrorRepository;
        

        public BeginPaymentRepository()
        {
            _dbConnectionRepository = new DbConnectionRepository();
            _dbLoggerRepository = new DbLoggerRepository();
            _dbLoggerErrorRepository = new DbLoggerErrorRepository();
            
            
        }

        public BeginPaymentDTO GetBeginPayment(BeginPayment beginPayment)
        {
            BeginPaymentDTO result = new BeginPaymentDTO();

            try
            {
                using (_connection = _dbConnectionRepository.CreateDbConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add(StaticBeginEnterpricePayment.SERVICE_REQUEST, beginPayment.ServiceRequest);

                    _connection.Open();

                    result = _connection.QueryFirstOrDefault<BeginPaymentDTO>(
                        StaticBeginEnterpricePayment.SP_EP_GET_BEGINPAYMENT_BY_SERVICEREQUEST,
                        parameters,
                        commandType: CommandType.StoredProcedure
                        );
                }
            }

            catch(Exception ex)
            {
                _dbLoggerErrorRepository.LogGetBeginPaymentError(ex.ToString(), beginPayment);
            }

            return result;
            
        }

        public string InsertBeginPayment(BeginPayment beginPayment)
        {

            string _resultMessage = string.Empty;

            try
            {


                int id;
            
 
                using (_connection = _dbConnectionRepository.CreateDbConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add(StaticBeginEnterpricePayment.BILLING_ACCOUNT, beginPayment.BillingAccount);
                    parameters.Add(StaticBeginEnterpricePayment.SERVICE_REQUEST, beginPayment.ServiceRequest);
                    parameters.Add(StaticBeginEnterpricePayment.PAYMENT_REFERENCE, beginPayment.PaymentReference);
                    parameters.Add(StaticBeginEnterpricePayment.CREATE_TOKEN, beginPayment.CreateToken);
                    parameters.Add(StaticBeginEnterpricePayment.BEGIN_PAYMENT_ID,
                                    dbType: DbType.Int32,
                                    direction: ParameterDirection.Output);

                    _connection.Open();

                    _connection.Query(StaticBeginEnterpricePayment.SP_CREATE_BEGIN_ENTERPRISE_PAYMENT,
                                      parameters,
                                      commandType: CommandType.StoredProcedure);

                    id = parameters.Get<int>(StaticBeginEnterpricePayment.BEGIN_PAYMENT_ID_OUTPUT_SEARCH);

                    

                    _resultMessage = StaticBeginEnterpricePayment.CREATED_RESPONSE_CODE;

                    _dbLoggerRepository.LogCreateBeginPayment(beginPayment, id, _resultMessage);

                }
            }
            catch (Exception ex)
            {
                _resultMessage = StaticBeginEnterpricePayment.ERROR_RESPONSE_CODE;
                _dbLoggerErrorRepository.LogCreateBeginPaymentError(ex.ToString(),beginPayment);
            }

            return _resultMessage;
        }

        public string UpdateBeginPayment(int beginPaymentId, string createToken)
        {
            string result = string.Empty;

            try
            {
                using (_connection = _dbConnectionRepository.CreateDbConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add(StaticBeginEnterpricePayment.BEGIN_PAYMENT_ID,beginPaymentId);
                    parameters.Add(StaticBeginEnterpricePayment.CREATE_TOKEN, createToken);

                    _connection.Open();

                    _connection.Query(
                        StaticBeginEnterpricePayment.SP_UPDATE_BEGINPAYMENT_CREATE_TOKEN,
                        parameters,
                        commandType: CommandType.StoredProcedure);

                    result = StaticBeginEnterpricePayment.UPDATED_RESPONSE_CODE;

                    _dbLoggerRepository.LogUpdateBeginPayment(beginPaymentId, createToken, result);
                }
               
            }
            catch(Exception ex)
            {
                result = StaticBeginEnterpricePayment.ERROR_RESPONSE_CODE;
                _dbLoggerErrorRepository.LogUpdateBeginPaymentError(ex.ToString(), beginPaymentId);
            }

            return result;
            
        }
    }
}