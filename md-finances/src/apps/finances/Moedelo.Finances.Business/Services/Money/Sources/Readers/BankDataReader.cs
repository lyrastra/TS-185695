using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.CatalogV2.Client.Banks;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.Finances.Business.Services.Money.Mappers;
using Moedelo.Finances.Domain.Interfaces.Business.Money.SourceReader;
using Moedelo.Finances.Domain.Models.Money;
using Moedelo.Finances.Domain.Models.Money.SourceReader;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.Logging;

namespace Moedelo.Finances.Business.Services.Money.Sources.Readers
{
    [InjectAsSingleton(typeof(IBankDataReader))]
    public class BankDataReader: IBankDataReader
    {
        private const string Tag = nameof(BankDataReader);

        private readonly ILogger logger;
        private readonly IBankIconApiClient bankIconApiClient;
        private readonly IBanksApiClient banksApiClient;

        public BankDataReader(
            IBanksApiClient banksApiClient, 
            ILogger logger,
            IBankIconApiClient bankIconApiClient)
        {
            this.banksApiClient = banksApiClient;
            this.logger = logger;
            this.bankIconApiClient = bankIconApiClient;
        }

        public async Task<Dictionary<int, SourceBankData>> GetBanksBySourcesAsync(IUserContext userContext,
            IReadOnlyCollection<MoneySource> sources, CancellationToken cancellationToken)
        {
            var result = new Dictionary<int, SourceBankData>();
            var bankIds = sources
                .Where(x => x.BankId.HasValue)
                .Select(x => x.BankId.Value)
                .Distinct()
                .ToList();

            try
            {
                result = (await banksApiClient.GetByIdsAsync(bankIds, cancellationToken).ConfigureAwait(false))
                    .ToDictionary(x => x.Id, x => x.Map());
            }
            catch (Exception ex)
            {
                logger.Error(Tag, "GetBanksAsync error", ex, userContext.GetAuditContext());
            }

            var iconUrls = await bankIconApiClient.GetByBankIdsAsync(bankIds).ConfigureAwait(false);
            foreach(var bank in result)
            {
                if (iconUrls.TryGetValue(bank.Key, out string iconUrl))
                {
                    bank.Value.IconUrl = iconUrl;
                }
            }

            return result;
        }
    }
}
