namespace EPBeginPaymentWebService.Interfaces
{
    public interface ILogPaymentRepository
    {
        bool ValidateIfPaymentInfoHashResponse(BeginPayment beginPayment);
    }
}