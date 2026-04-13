using Moedelo.Money.Enums;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.CashOrders
{
    public interface ICashOrderTaxationSystemUpdater
    {
        Task UpdateAsync(long documentBaseId, TaxationSystemType taxationSystemType, Guid guid);
    }
}
