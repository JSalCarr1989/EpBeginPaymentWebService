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
        private  string _resultMessage;

        public BeginPaymentRepository()
        {
            _dbConnectionRepository = new DbConnectionRepository();
            _dbLoggerRepository = new DbLoggerRepository();
            
            
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
                Console.WriteLine(ex.ToString());
            }

            return result;
            
        }

        public string InsertBeginPayment(BeginPayment beginPayment)
        {
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

                    _dbLoggerRepository.LogCreateBeginPayment(beginPayment,id);

                    _resultMessage = $"Operacion Satisfactoria para la Service Request:{beginPayment.ServiceRequest} , Billing Account: {beginPayment.BillingAccount} and Payment Reference: {beginPayment.PaymentReference}";

                    return _resultMessage;

                }
            }
            catch (Exception ex)
            {
                _resultMessage = $"Operacion Fallida para la Service Request:{beginPayment.ServiceRequest} , Billing Account: {beginPayment.BillingAccount} and Payment Reference: {beginPayment.PaymentReference}";
                return _resultMessage;
            }
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
                }
               
            }
            catch(Exception ex)
            {

            }
            return result;
            
        }
    }
}