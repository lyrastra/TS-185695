using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Money.Dto.PaymentOrders;
using Moedelo.Money.Dto.PaymentOrders.Outgoing.BudgetaryPayment.UnifiedBudgetaryPayment;

namespace Moedelo.Money.Client.PaymentOrders.Outgoing.BudgetaryPayment.UnifiedBudgetaryPayment
{
    public interface IUnifiedBudgetaryPaymentClient
    {
        Task<PaymentOrderSaveResponseDto> CreateAsync(int firmId, int userId, UnifiedBudgetaryPaymentSaveDto dto);

        Task<UnifiedBudgetaryPaymentSaveDto> GetAsync(int firmId, int userId, long documentBaseId);

        Task<PaymentOrderSaveResponseDto> UpdateAsync(int firmId, int userId, UnifiedBudgetaryPaymentSaveDto dto);

        /// <summary>
        /// Бюджетные счета
        /// </summary>
        Task<IReadOnlyCollection<BudgetaryAccountResponseDto>> GetAccountCodesAsync(int firmId, int userId);

        /// <summary>
        /// Автокомплит КБК
        /// </summary>
        Task<IReadOnlyCollection<BudgetaryKbkAutocompleteResponseDto>> KbkAutocompleteAsync(int firmId, int userId, BudgetaryKbkAutocompleteRequestDto requestDto);

        /// <summary>
        /// Формируем описание на основе внутренних подплатежей ЕНП
        /// </summary>
        Task<string> GetDescriptionAsync(int firmId, int userId, IReadOnlyCollection<UnifiedBudgetarySubPaymentDto> subPaymentsDto, DateTime paymentDate);
    }
}