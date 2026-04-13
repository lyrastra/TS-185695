using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.AccountingV2.Dto.Invoice;
using Moedelo.Common.Enums.Enums;

namespace Moedelo.AccountingV2.Client.Invoice
{
    public interface IInvoiceApiClient : IDI
    {
        Task ProvideAsync(int firmId, int userId, long invoiceBaseId);

        Task DeleteByBaseId(int firmId, int userId, List<long> documentBaseIds);

        /// <summary>
        /// Перепроводит документ в БУ (без пересохранения)
        /// </summary>
        Task ProvideAsync(int firmId, int userId, IReadOnlyCollection<long> invoiceBaseIds);

        Task<List<QuarterDto>> GetQuartersWithInvoices(int firmId, int userId);

        Task<byte[]> DownloadFileAsync(int firmId, int userId, long documentBaseId, bool useStampAndSign,
            DocumentFormat format);

        /// <summary>
        /// Следует вызвать в процессе объединения номенклатур для обработки мержа СЧФ
        /// </summary>
        Task MergeProductsAsync(int firmId, int userId, ProductMergeRequestDto data);
    }
}
