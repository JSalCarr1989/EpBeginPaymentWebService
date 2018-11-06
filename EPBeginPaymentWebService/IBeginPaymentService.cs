using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace EPBeginPaymentWebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IBeginPaymentService" in both code and config file together.
    [ServiceContract]
    public interface IBeginPaymentService
    {
        [OperationContract]
        void CreateBeginPayment(BeginPayment bpo);
    }

    [DataContract]
    public class BeginPayment
    {
        string _BillingAccount = string.Empty;
        string _ServiceRequest = string.Empty;
        string _PaymentReference = string.Empty;
        string _CreateToken = string.Empty;
       

        [DataMember]
        public string BillingAccount
        {
            get { return _BillingAccount; }
            set { _BillingAccount = value;} 
        }

        [DataMember]
        public string ServiceRequest
        {
            get { return _ServiceRequest; }
            set { _ServiceRequest = value;}
        }

        [DataMember]
        public string PaymentReference
        {
            get { return _PaymentReference; }
            set { _PaymentReference = value; }
        }

        [DataMember]
        public string CreateToken
        {
            get { return _CreateToken; }
            set { _CreateToken = value;}
        }



    }
}
