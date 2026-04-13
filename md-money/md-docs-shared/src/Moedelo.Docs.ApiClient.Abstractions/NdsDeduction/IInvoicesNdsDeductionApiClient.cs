using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Docs.ApiClient.Abstractions.NdsDeduction.Models;

namespace Moedelo.Docs.ApiClient.Abstractions.NdsDeduction
{
    public interface IInvoicesNdsDeductionApiClient
    {
        /// <summary>
        /// Возвращает принятый НДС и общую сумму НДС
        /// </summary>
        Task<List<DeductionAcceptedDto>> GetAcceptedAsync(IReadOnlyCollection<long> documentBaseIds);

        /// <summary>
        /// Возвращает сумму доступного НДС к вычету в заданном квартале
        /// </summary>
        Task<decimal> GetAvailableAsync(NdsDeductionRequestDto requestDto);
        
        /// <summary>
        /// Возвращает счёт-фактуры с суммой к уплате
        /// </summary>
        Task<IReadOnlyList<DeductibleInvoiceDto>> GetPayableInvoicesAsync(DeductibleDocumentsRequestDto requestDto);

        /// <summary>
        /// Возвращает счёт-фактуры с суммой к возмещению
        /// </summary>
        Task<IReadOnlyList<DeductibleInvoiceDto>> GetRefundableInvoicesAsync(DeductibleDocumentsRequestDto requestDto);
    }
}