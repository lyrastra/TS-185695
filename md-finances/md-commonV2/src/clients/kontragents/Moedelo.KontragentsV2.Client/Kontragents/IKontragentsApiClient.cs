using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.KontragentsV2.Dto;
using Moedelo.KontragentsV2.Dto.Enums;

namespace Moedelo.KontragentsV2.Client.Kontragents
{
    public interface IKontragentsClient : IDI
    {
        Task<KontragentDto> GetByIdAsync(int firmId, int userId, int id);

        Task<List<KontragentDto>> GetByIdsAsync(int firmId, int userId, IReadOnlyCollection<int> ids,
            CancellationToken cancellationToken = default);
        Task<List<BasicKontragentInfoDto>> GetBasicInfoByIdsAsync(IReadOnlyCollection<int> ids);

        /// <summary>
        /// Получить список контрагентов, у которых в имени встречается хотя бы одна строка из заданного списка
        /// </summary>
        /// <param name="firmId">идентификатор фирмы, в контексте которой делается запрос</param>
        /// <param name="userId">идентификатор пользователя, в контексте которого делается запрос</param>
        /// <param name="names"></param>
        /// <param name="cancellationToken">токен отмены операции</param>
        /// <returns></returns>
        Task<List<KontragentDto>> GetByNamesAsync(int firmId, int userId, IReadOnlyCollection<string> names,
            CancellationToken cancellationToken = default);

        Task<List<KontragentDto>> GetAsync(int firmId, int userId);

        Task<int> SaveAsync(int firmId, int userId, KontragentDto kontragent);

        Task<KontragentsPageDto> GetPageAsync(int firmId, int userId, KontragentsPageRequestDto request);

        Task<List<KontragentShortDto>> KontragentsAutocompleteAsync(int firmId, int userId, string query, int count = 100, CancellationToken cancellationToken = default);

        Task<KontragentDto> GetBySubcontoIdAsync(int firmId, int userId, long subcontoId);

        Task<List<KontragentDto>> GetByInnAsync(int firmId, int userId, string inn);

        Task<List<KontragentDto>> GetNonresidentByTaxpayerNumber(int firmId, int userId, string taxpayerNumber);

        /// <summary> Платежные агенты (закреплены за электронными кошельками) и контрагент "Население" </summary>
        Task<List<KontragentDto>> GetPopulationAndPurseAgentsAsync(int firmId, int userId);

        Task<List<KontragentDto>> GetByInnsAsync(int firmId, int userId, IReadOnlyCollection<string> inns);

        /// <summary>
        /// Создать контрагента "Население", если он ещё не создан
        /// </summary>
        /// <param name="firmId"></param>
        /// <param name="userId"></param>
        /// <returns>Id созданного/существующего контрагента</returns>
        Task<int> CreatePopulationKontragentIfNotExistAsync(int firmId, int userId);

        Task<List<DebtorDto>> GetDebtorsAsync(int firmId, int userId, int count);

        Task<List<KontragentWithSettlementAccountDto>> GetWithSettlementAccountAsync(int firmId, int userId, string query, int count);

        Task<List<KontragentEmailDto>> GetEmailKontragentAutocompleteAsync(int firmId, int userId, string query, int count, List<int> withoutIds);

        Task<List<KontragentWithAccountingPaymentOrderDto>> GetWithAccountingPaymentOrderAsync(int firmId, int userId, string query, DateTime paymentOrderDate, int count);

        Task<List<KontragentDto>> GetByNameExceptIdsOrderByNameAsync(int firmId, int userId, string query, int count, bool onlyFounders = false, List<int> exceptIds = null);

        Task ResaveWithSubcontoAsync(int firmId, int userId);

        Task<KontragentDto> GetByPurseIdAsync(int firmId, int userId, int purseId);
        
        Task<bool> IsKontragentsExistAsync(int firmId, int userId);

        Task<KontragentsRequestStatusCode> DeleteAsync(int firmId, int userId, int id);

        Task<List<KontragentDto>> GetForImportAsync(int firmId, int userId);

        Task<List<KontragentDto>> AutocompleteAsync(int firmId, int userId, string query, string description, string inn, int count);
    }
}