using EPBeginPaymentWebService.Interfaces;
using EPBeginPaymentWebService.Models;

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

            var paymentHasResponse = _logPaymentRepository.ValidateIfPaymentInfoHashResponse(bpo);

            //si no tiene respuesta de pago previa
            if (paymentHasResponse)
            {
                result = $"La Service Request que se trata de procesar ya fue procesada anteriormente";
            }else
            {
                BeginPaymentDTO _beginPayment = _beginPaymentRepository.GetBeginPayment(bpo);

                //si existe en beginpayment
                if (_beginPayment != null)
                {
                    //hacemos update 
                    result = _beginPaymentRepository.UpdateBeginPayment(_beginPayment.BeginPaymentId, bpo.CreateToken);
                }
                else // si no existe en beginpayment
                {
                    //creamos un nuevo registro
                    result = _beginPaymentRepository.InsertBeginPayment(bpo);
                }
            }

            

            

            return result;

        }
    }
}
