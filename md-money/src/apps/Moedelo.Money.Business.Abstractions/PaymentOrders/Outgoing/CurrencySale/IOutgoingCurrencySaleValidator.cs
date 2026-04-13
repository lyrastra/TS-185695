using System.Threading.Tasks;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencySale;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.CurrencySale
{
    public interface IOutgoingCurrencySaleValidator
    {
        Task ValidateAsync(OutgoingCurrencySaleSaveRequest saveRequest);
    }
}