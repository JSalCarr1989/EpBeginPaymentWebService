using EPBeginPaymentWebService.Models;

namespace EPBeginPaymentWebService.Interfaces
{
    public interface IBeginPaymentRepository
    {
        string InsertBeginPayment(BeginPayment beginPayment);
        BeginPaymentDTO GetBeginPayment(BeginPayment beginPayment);
        string UpdateBeginPayment(int beginPaymentId,string createToken);
    }
}