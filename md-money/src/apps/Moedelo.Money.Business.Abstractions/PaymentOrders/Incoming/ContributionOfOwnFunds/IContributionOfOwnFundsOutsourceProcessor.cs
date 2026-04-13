using System.Threading.Tasks;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Domain.PaymentOrders.Incoming.ContributionOfOwnFunds;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.ContributionOfOwnFunds;

public interface IContributionOfOwnFundsOutsourceProcessor
{
    Task<OutsourceConfirmResult> ConfirmAsync(ContributionOfOwnFundsSaveRequest request);
}