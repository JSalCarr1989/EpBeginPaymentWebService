namespace EPBeginPaymentWebService.Interfaces
{
    public interface ILogPaymentRepository
    {
        string ValidateIfPaymentInfoHasResponse(BeginPayment beginPayment);
    }
}