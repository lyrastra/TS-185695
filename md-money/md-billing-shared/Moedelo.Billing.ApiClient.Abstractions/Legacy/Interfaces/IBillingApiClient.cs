using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Billing.Abstractions.Legacy.Dto;
using Moedelo.Common.Types;

namespace Moedelo.Billing.Abstractions.Legacy.Interfaces;

public interface IBillingApiClient
{
    Task<PaymentHistoryDto> GetLastTariffWithTrialAsync(FirmId firmId);
    Task<TariffDto> GetTariffByPriceListIdAsync(int id);
    Task<PriceListDto> GetPriceListByIdAsync(int id);
    Task SwitchOnPaymentAsync(SwitchOnPaymentRequestDto switchOnPaymentRequestDto);
    Task SwitchOffPaymentAsync(SwitchOffPaymentRequestDto requestDto);
    Task<int> SavePaymentHistoryAsync(PaymentHistoryDto paymentHistory);
    Task<IReadOnlyCollection<PositionByPaymentDto>> GetActsByPaymentAsync(int paymentId);
    Task<PaymentHistoryDto> GetFirstUnsuccessfulPaymentForGroupAsync(int anyPaymentIdInGroup, long transactionId);
}