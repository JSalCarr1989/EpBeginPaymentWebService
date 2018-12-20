namespace EPBeginPaymentWebService.Interfaces
{
    public interface IBeginPaymentRepository
    {
        string InsertBeginPayment(BeginPayment beginPayment);
    }
}