using Moedelo.Money.Domain.PaymentOrders.Outgoing.WithdrawalOfProfit;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.WithdrawalOfProfit
{
    public interface IWithdrawalOfProfitImporter
    {
        Task ImportAsync(WithdrawalOfProfitImportRequest request);
    }
}