using Moedelo.CommissionAgents.ApiClient.Abstractions;
using Moedelo.CommissionAgents.ApiClient.Abstractions.Dto;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.IncomeFromCommissionAgent.Autocomplete;
using Moedelo.Money.Business.Kontragents;
using Moedelo.Money.Domain;
using Moedelo.Money.Domain.PaymentOrders.Incoming.IncomeFromCommissionAgent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.IncomeFromCommissionAgent.Autocomplete
{
    [InjectAsSingleton(typeof(ICommissionAgentAutocompleteReader))]
    internal sealed class CommissionAgentAutocompleteReader : ICommissionAgentAutocompleteReader
    {
        private readonly IKontragentsReader kontragentsReader;
        private readonly ICommissionAgentsApiClient commissionAgentsApiClient;

        public CommissionAgentAutocompleteReader(
            IKontragentsReader kontragentsReader,
            ICommissionAgentsApiClient commissionAgentsApiClient)
        {
            this.kontragentsReader = kontragentsReader;
            this.commissionAgentsApiClient = commissionAgentsApiClient;
        }

        public async Task<IReadOnlyCollection<CommissionAgentWithRequisites>> GetAsync(DateTime? date, string query, int count)
        {
            var commissionAgents = await commissionAgentsApiClient.GetAutocompleteAsync(query, count);
            if (commissionAgents.Count == 0)
            {
                return Array.Empty<CommissionAgentWithRequisites>();
            }

            var kontragentIds = commissionAgents.Select(x => x.KontragentId).ToArray();
            var kontragents = await kontragentsReader.GetWithRequisitesByIdsAsync(date ?? DateTime.Today, kontragentIds);

            return commissionAgents
                .Select(x => Map(x, kontragents))
                .Where(x => !x.IsArchive)
                .ToArray();
        }

        private static CommissionAgentWithRequisites Map(CommissionAgentDto commissionAgent, IReadOnlyCollection<KontragentWithRequisites> kontragents)
        {
            var kontragent = kontragents.FirstOrDefault(x => x.Id == commissionAgent.KontragentId);
            return new CommissionAgentWithRequisites
            {
                Id = commissionAgent.Id,
                Inn = commissionAgent.Inn,
                Kpp = kontragent?.Kpp,
                Name = commissionAgent.Name,
                KontragentId = commissionAgent.KontragentId,
                SettlementAccount = kontragent?.SettlementAccount,
                BankBik = kontragent?.BankBik,
                BankName = kontragent?.BankName,
                IsArchive = kontragent?.IsArchive ?? false
            };
        }
    }
}
