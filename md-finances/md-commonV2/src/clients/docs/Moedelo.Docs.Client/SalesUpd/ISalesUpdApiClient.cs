using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums;
using Moedelo.Docs.Dto;
using Moedelo.Docs.Dto.Docs;
using Moedelo.Docs.Dto.SalesUpd;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.Docs.Client.SalesUpd
{
    public interface ISalesUpdApiClient : IDI
    {
        Task<SalesUpdDto> GetByDocumentBaseId(int firmId, int userId, long baseId);

        Task<SalesUpdWithContractDto> GetByDocumentBaseIdWithContractAsync(int firmId, int userId, long baseId);

        Task<List<SalesUpdItemDto>> GetItemsByIdsAsync(int firmId, int userId, IReadOnlyCollection<int> ids, CancellationToken cancellationToken = default);

        Task<bool> HasProductItemsAsync(int firmId, int userId, long documentBaseId, CancellationToken cancellationToken = default);

        Task<List<SalesUpdDto>> GetByDocumentBaseIds(int firmId, int userId, IReadOnlyCollection<long> baseIds);

        Task DeleteAsync(int firmId, int userId, long baseId);

        Task DeleteByBaseIdsAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds);

        Task<List<SalesUpdDto>> GetByPeriodAsync(int firmId, int userId, DateTime startDate, DateTime endDate, bool withForgotten = false, bool useReadOnly = false);

        Task<List<SalesUpdWithItemsDto>> GetWithItemsAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds);
        
        Task<List<SalesUpdWithItemsDto>> GetByPeriodWithItemsAsync(int firmId, int userId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
        
        Task<DocFileDto> GetFileByBaseIdAsync(int firmId, int userId, long baseId, DocumentFormat format,
            bool? useStampAndSign = null);
        
        Task<List<SalesUpdDto>> GetByCriterionAsync(int firmId, int userId, SalesUpdRequestDto request);

        Task<long> GetNextNumberAsync(int firmId, int userId, int year);

        /// <summary>
        /// Возвращает суммы, покрытые платежными документами
        /// </summary>
        Task<List<PaidSumDto>> GetPaidSumByBaseIdsAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds);
        
        /// <summary>
        /// Привязать/отвязать счет
        /// </summary>
        Task UpdateLinkWithBillAsync(int firmId, int userId, SalesUpdBillSaveRequestDto dto);
    }
}