using Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencyTransferToAccount;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.CurrencyTransferToAccount
{
    public interface ICurrencyTransferToAccountImporter
    {
        Task ImportAsync(CurrencyTransferToAccountImportRequest request);
    }
}