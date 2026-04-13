using System.Threading.Tasks;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Domain.PaymentOrders.Incoming.IncomeFromCommissionAgent;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.IncomeFromCommissionAgent;

public interface IIncomeFromCommissionAgentOutsourceProcessor
{
    Task<OutsourceConfirmResult> ConfirmAsync(IncomeFromCommissionAgentSaveRequest request);
}