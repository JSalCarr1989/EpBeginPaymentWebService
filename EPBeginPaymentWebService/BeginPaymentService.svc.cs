using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Dapper;
using Serilog;
using Serilog.Formatting.Compact;
using Serilog.Formatting.Json;
using EPBeginPaymentWebService.Utilities;

namespace EPBeginPaymentWebService
{

    public class BeginPaymentService : IBeginPaymentService
    {

        private IDbConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BeginPaymentDevConnection"].ConnectionString);

        public void CreateBeginPayment(BeginPayment bpo)
        {
            var logger = new LoggerConfiguration()
                         .MinimumLevel.Debug()
                         .WriteTo.RollingFile(new CompactJsonFormatter(), @"E:\LOG\EnterprisePaymentLog.json")
                         .CreateLogger();

            
            try
            {
           

                int id;
                using (IDbConnection conn = Connection)
                {
                    var parameters = new DynamicParameters();

                    StaticBeginEnterpricePayment.BillingAccount = bpo.BillingAccount;
                    StaticBeginEnterpricePayment.ServiceRequest = bpo.ServiceRequest;
                    StaticBeginEnterpricePayment.PaymentReference = bpo.PaymentReference;
                    StaticBeginEnterpricePayment.CreateToken = bpo.CreateToken;


                    parameters.Add(StaticBeginEnterpricePayment.BILLING_ACCOUNT, bpo.BillingAccount);
                    parameters.Add(StaticBeginEnterpricePayment.SERVICE_REQUEST, bpo.ServiceRequest);
                    parameters.Add(StaticBeginEnterpricePayment.PAYMENT_REFERENCE, bpo.PaymentReference);
                    parameters.Add(StaticBeginEnterpricePayment.CREATE_TOKEN, bpo.CreateToken);
                    parameters.Add(StaticBeginEnterpricePayment.BEGIN_PAYMENT_ID, 
                                    dbType: DbType.Int32, 
                                    direction: ParameterDirection.Output);

                    conn.Open();

                    logger.Information(StaticBeginEnterpricePayment.LogTemplateBeforeInsert, 
                                        bpo.BillingAccount,
                                        bpo.ServiceRequest,
                                        bpo.PaymentReference,
                                        bpo.CreateToken,
                                        StaticBeginEnterpricePayment.PaymentStage,
                                        StaticBeginEnterpricePayment.ComunicationStep);

                    conn.QueryAsync(StaticBeginEnterpricePayment.SP_CREATE_BEGIN_ENTERPRISE_PAYMENT, 
                                      parameters, 
                                      commandType: CommandType.StoredProcedure);

                    id = parameters.Get<int>(StaticBeginEnterpricePayment.BEGIN_PAYMENT_ID_OUTPUT_SEARCH);

                    StaticBeginEnterpricePayment.BeginPaymentGeneratedId = id;

                    logger.Information( StaticBeginEnterpricePayment.LogTemplateAfterInsert,
                                        id, 
                                        bpo.ServiceRequest,
                                        bpo.BillingAccount);

                }
            }
            catch(Exception ex)
            {
                logger.Error(ex, $"Failed Operation for Service Request:{bpo.ServiceRequest} , Billing Account: {bpo.BillingAccount} and Payment Reference: {bpo.PaymentReference}  ");
            }
        }
    }
}
