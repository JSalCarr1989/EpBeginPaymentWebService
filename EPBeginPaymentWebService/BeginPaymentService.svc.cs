using EPBeginPaymentWebService.Interfaces;
using EPBeginPaymentWebService.Models;

namespace EPBeginPaymentWebService
{

    public class BeginPaymentService : IBeginPaymentService
    {

        private readonly IBeginPaymentRepository _beginPaymentRepository;

        public BeginPaymentService()
        {

            _beginPaymentRepository = new BeginPaymentRepository();

        }

        

        public string CreateBeginPayment(BeginPayment bpo)
        {

            var result = _beginPaymentRepository.InsertBeginPayment(bpo);

            return result;

        }
    }
}
