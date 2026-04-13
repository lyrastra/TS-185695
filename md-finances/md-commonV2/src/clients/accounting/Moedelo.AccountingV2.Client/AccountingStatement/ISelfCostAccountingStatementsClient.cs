using Moedelo.AccountingV2.Dto.AccountingStatement;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.AccountingV2.Client.AccountingStatement
{
    public interface ISelfCostAccountingStatementsClient
    {
        /// <summary>
        /// Получить бухсправки ФИФО (с проводками НУ) за указанный период
        /// </summary>
        Task<IReadOnlyCollection<SelfCostTaxGetResponseDto>> GetAsync(int firmId, int userId, SelfCostTaxGetByPeriodDto request);
    }
}
