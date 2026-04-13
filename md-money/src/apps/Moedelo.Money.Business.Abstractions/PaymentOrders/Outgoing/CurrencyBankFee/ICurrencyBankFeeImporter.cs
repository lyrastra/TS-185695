using Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencyBankFee;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.CurrencyBankFee
{
    public interface ICurrencyBankFeeImporter
    {
        Task ImportAsync(CurrencyBankFeeImportRequest request);
    }
}