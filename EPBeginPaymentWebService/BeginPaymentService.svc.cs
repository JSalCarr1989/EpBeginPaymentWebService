using EPBeginPaymentWebService.Interfaces;
using EPBeginPaymentWebService.Models;
using EPBeginPaymentWebService.Utilities;

namespace EPBeginPaymentWebService
{

    public class BeginPaymentService : IBeginPaymentService
    {

        private readonly IBeginPaymentRepository _beginPaymentRepository;
        private readonly ILogPaymentRepository _logPaymentRepository;
        


        public BeginPaymentService()
        {

            _beginPaymentRepository = new BeginPaymentRepository();
            _logPaymentRepository = new LogPaymentRepository();
            

        }

        

        public string CreateBeginPayment(BeginPayment bpo)
        {

            string result = string.Empty;

            var paymentHasResponse = _logPaymentRepository.ValidateIfPaymentInfoHasResponse(bpo);

            if(paymentHasResponse == StaticBeginEnterpricePayment.WITHOUT_RESPONSE)
            {
                BeginPaymentDTO _beginPayment = _beginPaymentRepository.GetBeginPayment(bpo);

                
                if (_beginPayment != null)
                {
                    
                    result = _beginPaymentRepository.UpdateBeginPayment(_beginPayment.BeginPaymentId, bpo.CreateToken);
                }
                else
                {
                    
                    result = _beginPaymentRepository.InsertBeginPayment(bpo);
                }
            }else
            {
                result = paymentHasResponse;
            }            

            return result;

        }
    }
}
