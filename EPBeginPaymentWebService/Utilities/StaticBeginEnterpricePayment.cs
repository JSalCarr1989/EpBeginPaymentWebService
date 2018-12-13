using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPBeginPaymentWebService.Utilities
{
    public static class StaticBeginEnterpricePayment
    {

        public static string BillingAccount { get; set; }
        public static string ServiceRequest { get; set; }
        public static string PaymentReference { get; set; }
        public static string CreateToken { get; set; }
        public static int BeginPaymentGeneratedId { get; set; }



        public static string BILLING_ACCOUNT
        {
            get
            {
                return "@BILLING_ACCOUNT";
            }
        }

        public static string SERVICE_REQUEST
        {
            get
            {
                return "@SERVICE_REQUEST";
            }
        }

        public static string PAYMENT_REFERENCE
        {
            get
            {
                return "@PAYMENT_REFERENCE";
            }
        }

        public static string CREATE_TOKEN
        {
            get
            {
                return "@CREATE_TOKEN";
            }
        }

        public static string BEGIN_PAYMENT_ID
        {
            get
            {
                return "@BEGIN_PAYMENT_ID";
            }
        }

        public static string BEGIN_PAYMENT_ID_OUTPUT_SEARCH
        {
            get
            {
                return "BEGIN_PAYMENT_ID";
            }
        }


        public static string LogTemplateBeforeInsert
        {

            get
            {
                return "Before Insert Begin Payment with the following data:" +
                                        "BillingAccount: {@BillingAccount} " +
                                        "ServiceRequest: {@ServiceRequest} " +
                                        "PaymentReference:{@PaymentReference} " +
                                        "CreateToken:{@CreateToken} " +
                                        "PaymentStage:{@LogPaymentStage} " +
                                        "ComunicationStep:{@LogComunicationStep} "+
                                        "Application:{@Application}";

            }
        }

        public static string PaymentStage
        {
            get
            {
                return "BeginPayment";
            }
        }

        public static string ComunicationStep
        {
            get
            {
                return "FROM SIEBEL TO .NET";
            }
        }

        public static string Application
        {
            get
            {
                return "WCF WEB SERVICE";
            }
        }

        public static string SP_CREATE_BEGIN_ENTERPRISE_PAYMENT
        {
            get
            {
                return "SP_CREATE_BEGIN_ENTERPRISE_PAYMENT";
            }
        }



        public static string LogTemplateAfterInsert
        {
            get
            {

                return "After Insert Begin Payment with the following data:" +
                        "Generated Id for Begin Payment {@BeginPaymentGeneratedId}" +
                        "from the ServiceRequest: {@ServiceRequest}" +
                        "and BillingAccount: {@BillingAccount}"+
                        "PaymentStage:{@PaymentStage}"+
                        "ComunicationStep:{@ComunicationStep}"+
                        "Application:{@Application}";
            }
        }
    }
}