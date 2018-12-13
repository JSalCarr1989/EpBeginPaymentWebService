using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using Serilog;
using Serilog.Formatting.Compact;
using EPBeginPaymentWebService.Utilities;

namespace EPBeginPaymentWebService
{

    public class BeginPaymentService : IBeginPaymentService
    {
        private static string environmentConnectionString = Environment.GetEnvironmentVariable("EpPaymentDevConnectionStringEnvironment", EnvironmentVariableTarget.Machine);
        private readonly IDbConnection Connection = new SqlConnection(environmentConnectionString);

        public string CreateBeginPayment(BeginPayment bpo)
        {
            //var logger = new LoggerConfiguration()
            //             .MinimumLevel.Debug()
            //             .WriteTo.RollingFile(new CompactJsonFormatter(), 
            //                                  @"E:\LOG\EnterprisePaymentLog.json",
            //                                  shared:true,
            //                                  retainedFileCountLimit:30
            //                                  )
            //             .CreateLogger();

            var logger = new LoggerConfiguration()
                         .MinimumLevel.Information()
                         .WriteTo.MSSqlServer(environmentConnectionString, "Log")
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
                                        StaticBeginEnterpricePayment.ComunicationStep,
                                        StaticBeginEnterpricePayment.Application);

                    conn.Query(StaticBeginEnterpricePayment.SP_CREATE_BEGIN_ENTERPRISE_PAYMENT, 
                                      parameters, 
                                      commandType: CommandType.StoredProcedure);

                    id = parameters.Get<int>(StaticBeginEnterpricePayment.BEGIN_PAYMENT_ID_OUTPUT_SEARCH);

                    StaticBeginEnterpricePayment.BeginPaymentGeneratedId = id;

                    logger.Information( StaticBeginEnterpricePayment.LogTemplateAfterInsert,
                                        id, 
                                        bpo.ServiceRequest,
                                        bpo.BillingAccount,
                                        StaticBeginEnterpricePayment.PaymentStage,
                                        StaticBeginEnterpricePayment.ComunicationStep,
                                        StaticBeginEnterpricePayment.Application
                                        );

                    return $"Successful Operation for Service Request:{bpo.ServiceRequest} , Billing Account: {bpo.BillingAccount} and Payment Reference: {bpo.PaymentReference}";

                }
            }
            catch(Exception ex)
            {
                logger.Error(ex, $"Unsuccessful Operation for Service Request:{bpo.ServiceRequest} , Billing Account: {bpo.BillingAccount} and Payment Reference: {bpo.PaymentReference}");
                return $"Unsuccessful Operation for Service Request:{bpo.ServiceRequest} , Billing Account: {bpo.BillingAccount} and Payment Reference: {bpo.PaymentReference}";
            }
        }
    }
}
