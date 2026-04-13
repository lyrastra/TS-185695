using Moedelo.Money.Domain.PaymentOrders.Outgoing.PaymentToNaturalPersons;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.PaymentToNaturalPersons
{
    public interface IChargePaymentDetector
    {
        Task<List<ChargePayment>> DetectAsync(string paymentDescription, decimal paymentSum, int? employeeId);
    }
}