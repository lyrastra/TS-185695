using System;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Common.Types;

namespace Moedelo.Accounting.ApiClient.Abstractions.TaxProfit
{
    public interface ITaxProfitFromWizardClient
    {
        /// <summary>
        /// рассчитывает Доходы и Расходы НУ в рамках МЗМ
        /// </summary>
        Task<TaxPostingsValue> GetTaxPostingsProfitAsync(FirmId firmId, UserId userId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken);
    }
}