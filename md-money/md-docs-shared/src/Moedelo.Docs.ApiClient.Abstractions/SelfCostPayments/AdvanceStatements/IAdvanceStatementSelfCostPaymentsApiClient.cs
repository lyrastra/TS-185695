using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Docs.ApiClient.Abstractions.SelfCostPayments.AdvanceStatements.Models;
using Moedelo.Docs.ApiClient.Abstractions.SelfCostPayments.Common;

namespace Moedelo.Docs.ApiClient.Abstractions.SelfCostSources.AdvanceStatements
{
    public interface IAdvanceStatementSelfCostPaymentsApiClient
    {
        /// <summary>
        /// Возвращает платежи (АО) для расчёта себестоимости
        /// </summary>
        Task<IReadOnlyCollection<AdvanceStatemenSelfCostPaymentDto>> GetOnDateAsync(SelfCostPaymentRequestDto request);
    }
}