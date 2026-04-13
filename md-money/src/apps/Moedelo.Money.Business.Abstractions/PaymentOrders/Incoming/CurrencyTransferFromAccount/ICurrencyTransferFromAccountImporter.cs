using Moedelo.Money.Domain.PaymentOrders.Incoming.CurrencyTransferFromAccount;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.CurrencyTransferFromAccount
{
    public interface ICurrencyTransferFromAccountImporter
    {
        Task ImportAsync(CurrencyTransferFromAccountImportRequest request);
    }
}