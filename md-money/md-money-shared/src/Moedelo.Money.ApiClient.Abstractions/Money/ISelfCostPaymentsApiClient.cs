using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Money.ApiClient.Abstractions.Money.Dto;

namespace Moedelo.Money.ApiClient.Abstractions.Money
{
    public interface ISelfCostPaymentsApiClient
    {
        /// <summary>
        /// Возвращает исходящие п/п для расчета себестоимости
        /// </summary>
        Task<IReadOnlyCollection<SelfCostPaymentDto>> GetSettlementAccountPaymentsAsync(SelfCostPaymentRequestDto requestDto);

        /// <summary>
        /// Возвращает РКО для расчета себестоимости
        /// </summary>
        Task<IReadOnlyCollection<SelfCostPaymentDto>> GetCashPaymentsAsync(SelfCostPaymentRequestDto requestDto);

        /// <summary>
        /// Возвращает бюджетные платежи по НДС (валюта)
        /// </summary>
        Task<IReadOnlyCollection<SelfCostPaymentDto>> GetCurrencyNdsPaymentsAsync(SelfCostPaymentRequestDto requestDto);
    }
}