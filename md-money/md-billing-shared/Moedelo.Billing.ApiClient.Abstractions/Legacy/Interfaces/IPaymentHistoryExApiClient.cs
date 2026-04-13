using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Billing.Abstractions.Legacy.Dto;

namespace Moedelo.Billing.Abstractions.Legacy.Interfaces;

public interface IPaymentHistoryExApiClient
{
    Task<PaymentHistoryExDto> GetAsync(int paymentId);
    Task<List<PaymentHistoryExDto>> GetAsync(IReadOnlyCollection<int> paymentIds);
    Task<List<PaymentHistoryExDto>> GetAsync(PaymentHistoryExRequestDto criteria);
    Task<PaymentHistoryExBillDataDto> GetPaymentHistoryExBillDataAsync(int paymentId);

    Task<IReadOnlyCollection<PaymentHistoryExBillDataDto>> GetPaymentsHistoryExBillDataAsync(
        IReadOnlyCollection<int> paymentIds);
}