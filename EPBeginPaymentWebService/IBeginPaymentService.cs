using System.Runtime.Serialization;
using System.ServiceModel;

namespace EPBeginPaymentWebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IBeginPaymentService" in both code and config file together.
    [ServiceContract]
    public interface IBeginPaymentService
    {
        [OperationContract]
        string CreateBeginPayment(BeginPayment bpo);
    }

    [DataContract]
    public class BeginPayment
    {
        [DataMember]
        public string BillingAccount { get; set; } = string.Empty;

        [DataMember]
        public string ServiceRequest { get; set; } = string.Empty;

        [DataMember]
        public string PaymentReference { get; set; } = string.Empty;

        [DataMember]
        public string CreateToken { get; set; } = string.Empty;



    }
}
