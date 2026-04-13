using System.Threading.Tasks;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Domain.PaymentOrders.Incoming.FinancialAssistance;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.FinancialAssistance;

public interface IFinancialAssistanceOutsourceProcessor
{
    Task<OutsourceConfirmResult> ConfirmAsync(FinancialAssistanceSaveRequest request);
}