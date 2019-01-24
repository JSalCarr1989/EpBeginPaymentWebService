using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPBeginPaymentWebService.Interfaces
{
    interface IDbLoggerErrorRepository
    {
        void LogValidateIfPaymentInfoHasResponseError(string error,BeginPayment beginPayment);
        void LogCreateBeginPaymentError(string error,BeginPayment beginPayment);
        void LogUpdateBeginPaymentError(string error, int beginPaymentId);
        void LogGetBeginPaymentError(string error, BeginPayment beginPayment);
    }
}
