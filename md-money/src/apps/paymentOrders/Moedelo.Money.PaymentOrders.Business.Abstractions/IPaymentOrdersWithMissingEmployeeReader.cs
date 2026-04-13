using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Money.PaymentOrders.Domain.Models;

namespace Moedelo.Money.PaymentOrders.Business.Abstractions
{
    public interface IPaymentOrdersWithMissingEmployeeReader
    {
        Task<IReadOnlyCollection<PaymentOrderWithMissingEmployeeResponse>> GetAsync();
    }
}