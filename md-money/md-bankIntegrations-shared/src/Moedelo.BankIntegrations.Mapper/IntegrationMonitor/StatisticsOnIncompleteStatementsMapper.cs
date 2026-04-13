using Moedelo.BankIntegrations.ApiClient.Dto.IntegrationMonitor;
using Moedelo.BankIntegrations.Models.IntegrationMonitor;
using System.Collections.Generic;
using System.Linq;

namespace Moedelo.BankIntegrations.Mapper.IntegrationMonitor
{
    public static class StatisticsOnIncompleteStatementsMapper
    {
        public static StatisticsOnIncompleteStatementDto Map(this StatisticsOnIncompleteStatement model) 
        {
            return new StatisticsOnIncompleteStatementDto()
            {
                PartnerId = model.PartnerId,
                Qty = model.Qty,
            };
        }

        public static IReadOnlyList<StatisticsOnIncompleteStatementDto> Map(this IReadOnlyList<StatisticsOnIncompleteStatement> models)
        {
            return models.Select(x => x.Map()).ToList();
        }

    }
}
