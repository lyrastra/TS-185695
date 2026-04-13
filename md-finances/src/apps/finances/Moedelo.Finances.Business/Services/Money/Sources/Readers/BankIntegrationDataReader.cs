using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.BankIntegrations.ApiClient.Dto.IntegrationRequests;
using Moedelo.BankIntegrations.ApiClient.Framework.Abstractions.IntegrationRequests;
using Moedelo.BankIntegrations.IntegrationPartnersInfo.Extensions;
using Moedelo.BankIntegrationsV2.Client.DataInformation;
using Moedelo.BankIntegrationsV2.Client.IntegratedUser;
using Moedelo.BankIntegrationsV2.Dto.DataInformation;
using Moedelo.Common.Enums.Enums.Integration;
using Moedelo.Finances.Domain.Interfaces.Business.Money.SourceReader;
using Moedelo.Finances.Domain.Models.Money.SourceReader;
using Moedelo.Finances.Domain.Models.Money;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.Logging;

namespace Moedelo.Finances.Business.Services.Money.Sources.Readers
{
    [InjectAsSingleton]
    public class BankIntegrationDataReader : IBankIntegrationDataReader
    {
        private const string Tag = nameof(BankIntegrationDataReader);
        private readonly IBankIntegrationsDataInformationClient integrationsDataInformationClient;
        private readonly IIntegrationRequestApiClient integrationRequestApiClient;
        private readonly IIntegratedUserClient integratedUserClient;
        private readonly ILogger logger;

        public BankIntegrationDataReader(
            IBankIntegrationsDataInformationClient integrationsDataInformationClient,
            ILogger logger,
            IIntegratedUserClient integratedUserClient, 
            IIntegrationRequestApiClient integrationRequestApiClient)
        {
            this.integrationsDataInformationClient = integrationsDataInformationClient;
            this.logger = logger;
            this.integratedUserClient = integratedUserClient;
            this.integrationRequestApiClient = integrationRequestApiClient;
        }

        public async Task<Dictionary<long, IntegrationData>> GetBankIntegrationDataAsync(int firmId, int userId, IReadOnlyCollection<MoneySource> sources, Dictionary<int, SourceBankData> banks)
        {
            var result = new Dictionary<long, IntegrationData>();
            try
            {
                var sourcesWithBank = sources.Where(x => x.BankId.HasValue).ToList();
                var settlements = GetSettlementsWithBank(sourcesWithBank, banks);
                var intSummary = await integrationsDataInformationClient.GetIntSummaryBySettlementsAsync(new IntSummaryBySettlementsRequestDto
                {
                    FirmId = firmId,
                    UserId = userId,
                    Settlements = settlements
                }).ConfigureAwait(false);
                var hasActiveIntegrations = intSummary?.Result.Any(x => x.Status == SettlementIntegrationStatus.Enabled);

                if (!hasActiveIntegrations.HasValue || !hasActiveIntegrations.Value)
                {
                    return result;
                }

                var partnersInfoTask = integratedUserClient.GetActiveIntegrationsPartnersInfoAsync(firmId);
                var integrationPartnersWithUnprocessedRequestsTask = integrationRequestApiClient.HasUnprocessedRequestMovementListAsync(new HasUnprocessedRequestMovementListDto
                {
                    FirmId = firmId,
                    IsManual = true,
                    DateOfCallAfter = DateTime.Now.AddDays(-2).AddSeconds(-1).ToUniversalTime()
                        .ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'")
                });
                await Task.WhenAll(partnersInfoTask, integrationPartnersWithUnprocessedRequestsTask).ConfigureAwait(false);

                foreach (var intSettlementStatus in intSummary.Result)
                {
                    var source = sources.FirstOrDefault(x =>
                        x.Number == intSettlementStatus.SettlementNumber && x.BankBik == intSettlementStatus.Bik);
                    var partnerInfo = partnersInfoTask.Result.FirstOrDefault(x =>
                        x.IntegratedPartner == intSettlementStatus.IntegrationPartner);
                    if (source == null || partnerInfo == null)
                    {
                        continue;
                    }

                    var hasActiveIntegration = intSettlementStatus.Status == SettlementIntegrationStatus.Enabled;
                    result[source.Id] = new IntegrationData
                    {
                        IntegrationPartner = intSettlementStatus.IntegrationPartner,
                        HasActiveIntegration = hasActiveIntegration,
                        IntegrationImage = hasActiveIntegration ? partnerInfo.ImgEnable : partnerInfo.ImgDisable,
                        CanRequestMovementList = partnerInfo.RequestExtractAvailable,
                        CanSendPaymentOrder = partnerInfo.SendPaymentAvailable,
                        HasUnprocessedRequests = integrationPartnersWithUnprocessedRequestsTask.Result.Contains(intSettlementStatus.IntegrationPartner),
                        CanSendBankInvoice = hasActiveIntegration && partnerInfo.IntegratedPartner.CanSendInvoice()
                    };
                }
            }
            catch (Exception ex)
            {
                logger.Error(Tag, "GetIntegrationDataAsync error", ex, null, new { firmId, userId });
            }

            return result;
        }
        
        public static List<SettlementAccountV2Dto> GetSettlementsWithBank(IReadOnlyCollection<MoneySource> sources, Dictionary<int, SourceBankData> banks)
        {
            var settlements = new List<SettlementAccountV2Dto>();
            foreach (var source in sources)
            {
                if (banks.TryGetValue(source.BankId.Value, out SourceBankData bank) && bank.IsActive)
                {
                    settlements.Add(new SettlementAccountV2Dto
                    {
                        SettlementNumber = source.Number,
                        BankFullName = bank.FullName,
                        Bik = bank.Bik
                    });
                }
            }

            return settlements;
        }
    }
}
