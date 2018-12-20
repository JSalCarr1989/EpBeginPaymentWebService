namespace EPBeginPaymentWebService.Interfaces
{
    public interface IDbLoggerRepository
    {
        void LogCreateBeginPayment(BeginPayment beginPayment, int beginPaymentId);
    }
}