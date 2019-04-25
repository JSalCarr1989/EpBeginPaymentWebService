using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPBeginPaymentWebService.Interfaces
{
    interface IDbLoggerErrorRepository
    {
        void LogValidateIfPaymentInfoHasResponseError(string error,BeginPayment beginPayment , string FileName, string MethodName);
        void LogCreateBeginPaymentError(string error,BeginPayment beginPayment, string FileName, string MethodName);
        void LogUpdateBeginPaymentError(string error, int beginPaymentId, string FileName, string MethodName);
        void LogGetBeginPaymentError(string error, BeginPayment beginPayment, string FileName, string MethodName);
    }
}
