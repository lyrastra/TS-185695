using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Docs.Dto;
using Moedelo.Docs.Dto.PurchaseInfo;
using Moedelo.Docs.Dto.Upd;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;

namespace Moedelo.Docs.Client.Upd
{
    /// <summary>
    /// CRUD операции над УПД
    /// </summary>
    public interface IUpdApiClient : IDI
    {
        /// <summary>
        /// Возвращает УПД по DocumentBaseId
        /// </summary>
        Task<UpdDto> GetByBaseIdAsync(int firmId, int userId, long baseId);

        /// <summary>
        /// Возвращает НУ-проводки для УПД по связанному платежу
        /// </summary>
        Task<List<TaxPostingDto>> GeneratePostingsByLinkedPaymentAsync(int firmId, int userId, LinkedPaymentDto dto);

        /// <summary>
        /// Возвращает УПД по DocumentBaseId
        /// </summary>
        Task<List<UpdWithItemsDto>> GetByBaseIdsAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Удаляет УПД по DocumentBaseId
        /// </summary>
        Task DeleteByBaseIdAsync(int firmId, int userId, long baseId);

        /// <summary>
        /// Удаляет УПД по списку DocumentBaseId
        /// </summary>
        Task DeleteByBaseIdsAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds);

        /// <summary>
        /// Возвращает список УПД за период
        /// </summary>
        Task<List<UpdWithItemsDto>> GetByPeriodAsync(int firmId, int userId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);

        /// <summary>
        /// Возвращает информацию по последнему УПД
        /// </summary>
        Task<List<PurchaseInfoDto>> GetLastPurchaseInfoAsync(int firmId, int userId, List<PurchaseInfoRequestDto> purchaseInfoRequestDto);

        /// <summary>
        /// Возвращает список УПД по критериям
        /// </summary>
        Task<List<UpdDto>> GetByCriterionAsync(int firmId, int userId, UpdRequestDto request);

        /// <summary>
        /// Возвращает суммы, покрытые платежными документами
        /// </summary>
        Task<List<PaidSumDto>> GetPaidSumByBaseIdsAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds);

        /// <summary>
        /// Возвращает сообщение, почему отсутствуют налоговые проводки
        /// </summary>
        Task<string> GetNoTaxMessageAsync(int firmId, int userId, long baseId);
    }
}