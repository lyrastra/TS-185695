using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.AccPostings.Dto;
using Moedelo.Common.Enums.Enums.Accounting;
using Moedelo.Common.Enums.Enums.SyntheticAccounts;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;

namespace Moedelo.AccPostings.Client
{
    public interface ISyntheticAccountBalanceClient : IDI
    {     

        Task<List<SyntheticAccountBalanceDto>> GetAccountBalanceOnDateAsync(
            int firmId,
            int userId,
            SyntheticAccountBalanceRequestDto requestDto);

        /// <summary>
        /// Получаем баласн проводок сгруппированные по субконто
        /// </summary>
        Task<List<AccountBalanceDivisionDto>> GetPostingBalanceFor20AccountingAndManufacturingCostItemAsync(
            int firmId,
            int userId,
            DateTime endDate,
            CostItemGroupCode code);
    }
}