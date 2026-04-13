using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.AccountingV2.Dto.TaxReductionTip;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.AccountingV2.Client.TaxReductionTip
{
    public interface ITaxReductionTipClient : IDI
    {
        /// <summary>
        /// Данные для отчёта-подсказки по снижению налога
        /// </summary>
        Task<List<TaxReductionTipItemDto>> GetAsync(int firmId, int userId);
    }
}