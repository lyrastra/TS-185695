using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.PaymentImport.Dto;

namespace Moedelo.PaymentImport.Client
{
    public interface IOutsourceImportRulesClient : IDI
    {
        /// <summary>
        /// Возвращает применёные массовые правила импорта к ПП из аутсорса
        /// </summary>
        Task<IReadOnlyCollection<OutsourceAppliedImportRuleDto>> GetAppliedRulesAsync(int firmId, int userId, AppliedRulesRequestDto request);
    }
}