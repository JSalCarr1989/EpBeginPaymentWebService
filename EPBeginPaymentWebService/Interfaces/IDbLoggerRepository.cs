namespace EPBeginPaymentWebService.Interfaces
{
    public interface IDbLoggerRepository
    {
        void LogCreateBeginPayment(BeginPayment beginPayment, int beginPaymentId,string responseCode,string FileName,string MethodName);
        void LogUpdateBeginPayment(int beginPaymentId, string createToken, string responseCode,string FileName,string MethodName);
    }
}