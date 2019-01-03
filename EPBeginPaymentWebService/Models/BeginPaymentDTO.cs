using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPBeginPaymentWebService.Models
{
    public class BeginPaymentDTO
    {
        public int BeginPaymentId { get; set; }
        public string BillingAccount { get; set; }
        public string ServiceRequest { get; set; }
        public string PaymentReference { get; set; }
        public string CreateToken { get; set; }
    }
}