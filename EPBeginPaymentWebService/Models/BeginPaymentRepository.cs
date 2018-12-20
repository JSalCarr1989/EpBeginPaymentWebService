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
        private readonly IDbConnection _connection;
        private readonly IDbLoggerRepository _dbLoggerRepository;
        private  string _resultMessage;

        public BeginPaymentRepository()
        {
            _dbConnectionRepository = new DbConnectionRepository();
            _dbLoggerRepository = new DbLoggerRepository();
            _connection = _dbConnectionRepository.CreateDbConnection();
        }

        public string InsertBeginPayment(BeginPayment beginPayment)
        {
            try
            {


                int id;
            
 
                using (_connection)
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

                    _resultMessage = $"Successful Operation for Service Request:{beginPayment.ServiceRequest} , Billing Account: {beginPayment.BillingAccount} and Payment Reference: {beginPayment.PaymentReference}";

                    return _resultMessage;

                }
            }
            catch (Exception ex)
            {
                _resultMessage = $"Unsuccessful Operation for Service Request:{beginPayment.ServiceRequest} , Billing Account: {beginPayment.BillingAccount} and Payment Reference: {beginPayment.PaymentReference}";
                return _resultMessage;
            }
        }
    }
}