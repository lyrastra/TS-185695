using System.Threading.Tasks;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Models;

namespace Moedelo.Money.PaymentOrders.Business.Abstractions.Outgoing.PaymentToNaturalPersons
{
    public interface IPaymentToNaturalPersonsUpdater
    {
        Task UpdateAsync(PaymentOrderSaveRequest request);
    }
}