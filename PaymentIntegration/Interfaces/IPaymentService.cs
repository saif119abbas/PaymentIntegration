using PaymentIntegration.Dtos;
using PaymentIntegration.Models;

namespace PaymentIntegration.Interfaces
{
    public interface IPaymentService
    {
        Task<CreateInvoiceResponse>CreateInvoiceAsync(CreateInvoiceDto dto);
        Task<string> InitiateSession(InitiateSessionDto initiateSession);
        Task<string> ExecutePayment(ExecutePaymentDto executePaymentDto);
        Task<string> GetPaymentStatusAsync(string paymentId, string keyType = "PaymentId");
    }
}
