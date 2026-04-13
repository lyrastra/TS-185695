using Moedelo.AccountingV2.Dto.PaymentOrder;
using Moedelo.AccountingV2.Dto.Payments;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.AccountingV2.Client.PaymentOrder
{
    public interface IPaymentOrderApiClient
    {
        Task<PaymentOrderForServiceResponseDto> CreatePaymentForServiceAsync(PaymentOrderForServiceRequestDto dto);

        Task<SendPaymentOrderResponseDto> SendPaymentOrderAsync(SendPaymentOrderRequestDto dto);

        [Obsolete("Use Moedelo.Money.Client.PaymentOrders.IPaymentOrdersApiClient.ProvideAsync")]
        Task ProvideAsync(int firmId, int userId, long baseIds);

        [Obsolete("Use Moedelo.Money.Client.PaymentOrders.IPaymentOrdersApiClient.ProvideAsync")]
        Task ProvideAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds);

        /// <summary>
        /// Пересчитывает суммы УСН (если надо) + перепроводит.
        /// Актуально, например, при смене СНО.
        /// Подумай, прежде чем использовать этот метод - это может быть тяжело.
        /// </summary>
        Task InvalidateSinceAsync(int firmId, int userId, DateTime sinceDate);

        Task<byte[]> GetFileAsync(int firmId, int userId, GetFileRequestDto request);

        Task<long?> CreatePaymentAsync(int firmId, int userId, CreatePaymentRequestDto request);

        [Obsolete("Use Moedelo.Money.Client.IMoneyOperationsClient.DeleteAsync")]
        Task DeleteAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds);

        Task DeleteNotPaidAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds);

        Task<List<TransportTaxAdvancePaymentOrderDto>> Get(GetTransportTaxAdvancePaymentOrders request);

        Task<bool> IsExistAsync(int firmId, int userId, long id);

        Task<decimal> FindNextNumberByYearAsync(int firmId, int userId, DateTime dateTime);

        Task<long> Create(int firmId, int userId, CreatePaymentDto dto);
    }
}
