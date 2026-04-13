using System.Threading.Tasks;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Domain.PaymentOrders.Incoming.MediationFee;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.MediationFee;

public interface IMediationFeeOutsourceProcessor
{
    Task<OutsourceConfirmResult> ConfirmAsync(MediationFeeSaveRequest request);
}