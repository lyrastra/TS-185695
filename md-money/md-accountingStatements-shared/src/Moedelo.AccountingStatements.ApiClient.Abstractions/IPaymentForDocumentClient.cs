using Moedelo.AccountingStatements.ApiClient.Abstractions.Dtos.PaymentForDocuments;
using Moedelo.Infrastructure.Http.Abstractions.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.AccountingStatements.ApiClient.Abstractions
{
    /// <summary>
    /// Клиент для бух. справок по признанию предоплаты оплатой
    /// </summary>
    public interface IPaymentForDocumentClient
    {
        Task<PaymentForDocumentCreateResponseDto> CreateAsync(PaymentForDocumentCreateRequestDto request);

        Task<PaymentForDocumentCreateResponseDto[]> CreateAsync(IReadOnlyCollection<PaymentForDocumentCreateRequestDto> requests);

        /// <summary> Удаление </summary>
        /// <param name="documentBaseIds">список идентификаторов</param>
        /// <param name="deleteLinksImmediately">флаг: удалить связи бухсправок синхронно (сразу)</param>
        Task DeleteAsync(IReadOnlyCollection<long> documentBaseIds, bool deleteLinksImmediately = false, HttpQuerySetting setting = null);
    }
}
