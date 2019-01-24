namespace EPBeginPaymentWebService.Utilities
{
    public static class StaticBeginEnterpricePayment
    {

        public static string BILLING_ACCOUNT => "@BILLING_ACCOUNT";

        public static string SERVICE_REQUEST => "@SERVICE_REQUEST";

        public static string PAYMENT_REFERENCE => "@PAYMENT_REFERENCE";

        public static string CREATE_TOKEN => "@CREATE_TOKEN";

        public static string BEGIN_PAYMENT_ID => "@BEGIN_PAYMENT_ID";

        public static string BEGIN_PAYMENT_ID_OUTPUT_SEARCH => "BEGIN_PAYMENT_ID";


        public static string LogCreateBeginPaymentTemplateMessage => @"Inserted BeginPayment with the following data: 
                                         BillingAccount: {@BillingAccount} 
                                         ServiceRequest: {@ServiceRequest} 
                                         PaymentReference:{@PaymentReference} 
                                         CreateToken:{@CreateToken} 
                                         BeginPaymentId: {@BeginPaymentId} 
                                         PaymentStage:{@LogPaymentStage} 
                                         ComunicationStep:{@LogComunicationStep} 
                                         Application:{@Application}";

        public static string PaymentStage => "BeginPayment";

        public static string ComunicationStep => "FROM SIEBEL TO .NET";

        public static string Application => "WCF WEB SERVICE";

        public static string SP_CREATE_BEGIN_ENTERPRISE_PAYMENT => "SP_CREATE_BEGIN_ENTERPRISE_PAYMENT";

        public static string SP_EP_GET_BEGINPAYMENT_BY_SERVICEREQUEST => "SP_EP_GET_BEGINPAYMENT_BY_SERVICEREQUEST";

        public static string SP_UPDATE_BEGINPAYMENT_CREATE_TOKEN => "SP_UPDATE_BEGINPAYMENT_CREATE_TOKEN";

        public static string CREATED_RESPONSE_CODE => "SR001";

        public static string UPDATED_RESPONSE_CODE => "SR002";

        public static string ALREADY_PROCESED_RESPONSE_CODE => "SR003";

        public static string ERROR_RESPONSE_CODE => "SR004";

        public static string WITHOUT_RESPONSE => "WITHOUT_RESPONSE";
    }
}