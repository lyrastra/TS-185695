using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto.Balances;
using Moedelo.Accounting.Enums;
using Moedelo.Common.Types;

namespace Moedelo.Accounting.ApiClient.Abstractions.legacy
{
    public interface IBalancesApiClient
    {
        /// <summary>
        /// Получение даты ввода остатков (null - остатки не введены)
        /// </summary>
        Task<DateTime?> GetDateAsync(FirmId firmId, UserId userId, bool useReadOnlyDb = false);

        /// <summary>
        /// Получение даты ввода остатков по списку фирм
        /// </summary>
        Task<IReadOnlyDictionary<int, DateTime>> GetDateByFirmIds(
            IReadOnlyCollection<int> firmIds);

        /// <summary>
        /// Получение бух. остатков по счетам 51.01.00 (Расчетный рублевый), 52.01.01 (Транзитный) и 52.01.02(Валютный)
        /// </summary>
        Task<List<CurrencyRemainsDto>> GetCurrencyRemainsAsync(FirmId firmId, UserId userId, IReadOnlyList<CurrencySyntheticAccountCode> codes);

        /// <summary>
        /// Получение бух. остатков по списку фирм и счетов
        /// </summary>
        Task<IReadOnlyDictionary<int, AccountBalanceDto[]>> GetBalancesByFirmIdsAndAccountCodesAsync(GetBalanceByFirmIdsRequestDto request);

        /// <summary>
        /// Получение документов "Долг сотрудника по выдачам под отчет" (счёт 71.01)
        /// </summary>
        Task<AdvanceStatementBalanceDocumentDto[]> GetAdvanceStatementBalanceDocumentsAsync(FirmId firmId, UserId userId);
    }
}
