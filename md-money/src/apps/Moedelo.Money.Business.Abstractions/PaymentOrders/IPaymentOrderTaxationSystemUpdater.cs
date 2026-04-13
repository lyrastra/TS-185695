using Moedelo.Money.Enums;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders
{
    public interface IPaymentOrderTaxationSystemUpdater
    {
        Task UpdateAsync(long documentBaseId, TaxationSystemType taxationSystemType, Guid guid);
    }
}
