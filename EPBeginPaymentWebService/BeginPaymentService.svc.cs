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

namespace EPBeginPaymentWebService
{

    public class BeginPaymentService : IBeginPaymentService
    {

        private IDbConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BeginPaymentDevConnection"].ConnectionString);

        public void CreateBeginPayment(BeginPayment bpo)
        {
            try
            {
                using (IDbConnection conn = Connection)
                {
                    var parameters = new DynamicParameters();

                    parameters.Add("@BILLING_ACCOUNT", bpo.BillingAccount);
                    parameters.Add("@SERVICE_REQUEST", bpo.ServiceRequest);
                    parameters.Add("@PAYMENT_REFERENCE", bpo.PaymentReference);
                    parameters.Add("@CREATE_TOKEN", bpo.CreateToken);
                    parameters.Add("@BEGIN_PAYMENT_ID", dbType: DbType.Int32, direction: ParameterDirection.Output);

                    conn.Open();
                    conn.QueryAsync("SP_CREATE_BEGIN_ENTERPRISE_PAYMENT", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch
            {

            }
        }
    }
}
