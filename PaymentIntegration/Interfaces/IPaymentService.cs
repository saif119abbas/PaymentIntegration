using PaymentIntegration.Dtos;
using PaymentIntegration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentIntegration.Interfaces
{
    public interface IPaymentService
    {
        Task<CreateInvoiceResponse> CreateInvoiceAsync(CreateInvoiceDto dto);
        Task<string> GetPaymentStatusAsync(string paymentId, string keyType = "PaymentId");
    }
}
