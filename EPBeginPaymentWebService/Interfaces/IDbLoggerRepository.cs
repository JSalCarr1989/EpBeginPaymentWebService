namespace EPBeginPaymentWebService.Interfaces
{
    public interface IDbLoggerRepository
    {
        void LogCreateBeginPayment(BeginPayment beginPayment, int beginPaymentId,string responseCode);
        void LogUpdateBeginPayment(int beginPaymentId, string createToken, string responseCode);
    }
}