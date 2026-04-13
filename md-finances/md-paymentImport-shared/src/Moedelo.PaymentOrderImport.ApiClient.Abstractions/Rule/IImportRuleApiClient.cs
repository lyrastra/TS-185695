using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.PaymentOrderImport.ApiClient.Abstractions.Dto;

namespace Moedelo.PaymentOrderImport.ApiClient.Abstractions.Rule
{
    public interface IImportRuleApiClient
    {
        Task<ImportRuleDto> GetAsync(int ruleId, CancellationToken ct = default);

        /// <summary>
        /// Возвращает правила импорта из ЛК по идентификаторам.
        /// Метод сделан для массовых операций в аутсорсе. Работает сразу по всем фирмам.
        /// </summary>
        Task<IReadOnlyCollection<ImportRuleDto>> GetByIdsAsync(IReadOnlyCollection<int> ids, CancellationToken ct = default);

        /// <summary>
        /// Создание правила импорта
        /// </summary>
        Task<ImportRuleSaveResponseDto> CreateRuleAsync(ImportRuleSaveDto dto, CancellationToken ct = default);
        
        /// <summary>
        /// Получение списка правил импорта
        /// </summary>
        Task<IReadOnlyCollection<ImportRuleListResponseDto>> GetRulesAsync(CancellationToken ct = default);
    }
}
