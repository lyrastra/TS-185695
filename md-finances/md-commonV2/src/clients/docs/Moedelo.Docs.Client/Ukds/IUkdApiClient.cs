using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Docs.Dto.Ukd;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;

namespace Moedelo.Docs.Client.Ukds
{
    public interface IUkdApiClient : IDI
    {
        Task<List<UkdDto>> GetByBaseIdsAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds);
        Task<IList<UkdCriteriaTableItemDto>> GetByCriteriaAsync(int firmId, int userId, UkdCriteriaRequestDto request,
            CancellationToken cancellationToken = default);
        Task<HttpFileModel> DownloadPdfFileAsync(int firmId, int userId, long id, bool useStampAndSign);
        Task<List<UkdDto>> GetWithoutRefundPaymentsAsync(int firmId, int userId, DateTime startDate, DateTime endDate);
        Task DeleteByBaseIdsAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds);

        /// <summary>
        /// Получить список продуктов, которые используются в УКД для заданной фирмы. Поиск ведётся среди productIds
        /// </summary>
        Task<List<long>> GetProductsIdsInUKDsAsync(int firmId, int userId, ProductsIdsInUKDRequest request);
    }
}