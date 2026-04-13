using Moedelo.AccountingV2.Client.Cash;
using Moedelo.AccountingV2.Dto.Cash;
using Moedelo.Common.Enums.Enums.Finances.Money;
using IntegrationPartners = Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums.IntegrationPartners;
using Moedelo.Common.Enums.Enums.SettlementAccounts;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.Finances.Business.Helpers.Money;
using Moedelo.Finances.Domain.Interfaces.Business.Money;
using Moedelo.Finances.Domain.Interfaces.Business.Money.Balances;
using Moedelo.Finances.Domain.Interfaces.Business.Money.SourceReader;
using Moedelo.Finances.Domain.Models.Money;
using Moedelo.Finances.Domain.Models.Money.SourceReader;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.KontragentsV2.Client.Kontragents;
using Moedelo.KontragentsV2.Dto;
using Moedelo.RequisitesV2.Client.Purses;
using Moedelo.RequisitesV2.Client.SettlementAccounts;
using Moedelo.RequisitesV2.Dto.Purses;
using Moedelo.RequisitesV2.Dto.SettlementAccounts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Threading;

namespace Moedelo.Finances.Business.Services.Money.Sources
{
    [InjectAsSingleton]
    public class MoneySourceReader : IMoneySourceReader
    {
        private readonly ISettlementAccountClient settlementAccountClient;
        private readonly ICashApiClient cashApiClient;
        private readonly IPurseClient purseClient;
        private readonly IKontragentsClient kontragentsApiClient;
        private readonly IMoneyBalancesReader moneyBalancesReader;
        private readonly IBankIntegrationDataReader integrationDataReader;
        private readonly IReconciliationDataReader reconciliationDataReader;
        private readonly IBankDataReader bankDataReader;
        private readonly IYandexKassaIntegrationDataReader yandexKassaIntegrationDataReader;

        public MoneySourceReader(
            ISettlementAccountClient settlementAccountClient,
            ICashApiClient cashApiClient,
            IPurseClient purseClient,
            IKontragentsClient kontragentsApiClient,
            IMoneyBalancesReader moneyBalancesReader,
            IBankIntegrationDataReader integrationDataReader,
            IReconciliationDataReader reconciliationDataReader,
            IBankDataReader bankDataReader,
            IYandexKassaIntegrationDataReader yandexKassaIntegrationDataReader)
        {
            this.settlementAccountClient = settlementAccountClient;
            this.cashApiClient = cashApiClient;
            this.purseClient = purseClient;
            this.kontragentsApiClient = kontragentsApiClient;
            this.moneyBalancesReader = moneyBalancesReader;
            this.integrationDataReader = integrationDataReader;
            this.reconciliationDataReader = reconciliationDataReader;
            this.bankDataReader = bankDataReader;
            this.yandexKassaIntegrationDataReader = yandexKassaIntegrationDataReader;
        }

        public async Task<List<MoneySource>> GetAsync(IUserContext userContext, CancellationToken cancellationToken)
        {
            var sources = await GetSourceItemsAsync(userContext).ConfigureAwait(false);
            var banks = await bankDataReader
                .GetBanksBySourcesAsync(userContext, sources, cancellationToken)
                .ConfigureAwait(false);
            AddBankDataToSources(sources, banks);

            var balancesSumTask = moneyBalancesReader.GetAsync(userContext, sources);
            var banksIntegrationDataTask = integrationDataReader.GetBankIntegrationDataAsync(userContext.FirmId, userContext.UserId, sources, banks);
            var reconciliationDataTask = reconciliationDataReader.GetReconciliationDataAsync(userContext.FirmId, userContext.UserId, sources);
            await Task.WhenAll(balancesSumTask, banksIntegrationDataTask, reconciliationDataTask).ConfigureAwait(false);

            foreach (var source in sources)
            {
                source.Balance = balancesSumTask.Result
                    .Where(x => x.Type == source.Type && x.Id == source.Id)
                    .Sum(x => x.Balance);

                switch (source.Type)
                {
                    case MoneySourceType.SettlementAccount:
                    {
                        if (banksIntegrationDataTask.Result.TryGetValue(source.Id, out var integrationsData))
                        {
                            source.IntegrationPartner = integrationsData.IntegrationPartner;
                            source.HasActiveIntegration = integrationsData.HasActiveIntegration;
                            source.CanRequestMovementList = integrationsData.CanRequestMovementList;
                            source.CanSendPaymentOrder = integrationsData.CanSendPaymentOrder;
                            source.IntegrationImage = integrationsData.IntegrationImage;
                            source.HasUnprocessedRequests = integrationsData.HasUnprocessedRequests;
                            source.CanSendBankInvoice = integrationsData.CanSendBankInvoice;
                        }
                        if (reconciliationDataTask.Result.TryGetValue(source.Id, out var reconciliationData))
                        {
                            source.IsReconciliationProcessing = reconciliationData.IsReconciliationInProcess;
                            source.HasEmployees = reconciliationData.HasEmployees;
                        }

                        break;
                    }
                    case MoneySourceType.Purse when source.IntegrationPartner.HasValue:
                        switch(source.IntegrationPartner)
                        {
                            case IntegrationPartners.YandexKassa:
                            {
                                var yandexKassaIntegrationData = await yandexKassaIntegrationDataReader
                                    .GetData(userContext.FirmId, userContext.UserId)
                                    .ConfigureAwait(false);
                                source.HasActiveIntegration = yandexKassaIntegrationData?.HasActiveIntegration ?? false;
                                source.CanRequestMovementList = yandexKassaIntegrationData?.CanRequestMovementList ?? false;
                                break;
                            }
                            case IntegrationPartners.YMoney:
                            {
                                var yandexKassaIntegrationData = await yandexKassaIntegrationDataReader
                                    .GetData(userContext.FirmId, userContext.UserId)
                                    .ConfigureAwait(false);
                                source.HasActiveIntegration = yandexKassaIntegrationData?.HasActiveIntegration ?? false;
                                source.CanRequestMovementList = yandexKassaIntegrationData?.CanRequestMovementList ?? false;
                                break;
                            }
                        }

                        break;
                }
            }

            AddAllSourcesItemAsync(sources);

            return sources;
        }

        public async Task<List<MoneySource>> GetAsync(IUserContext userContext, IReadOnlyCollection<MoneySourceBase> moneySources)
        {
            var sources = new List<MoneySource>();
            if (moneySources.Count == 0)
            {
                return sources;
            }

            var settlementAccountIds = moneySources.Where(x => x.Type == MoneySourceType.SettlementAccount).Select(x => (int)x.Id).ToArray();
            var settlementAccountsTask = settlementAccountIds.Length > 0
                ? settlementAccountClient.GetByIdsAsync(userContext.FirmId, userContext.UserId, settlementAccountIds)
                : Task.FromResult(new List<SettlementAccountDto>());

            var cashIds = moneySources.Where(x => x.Type == MoneySourceType.Cash).Select(x => x.Id).ToArray();
            var cashListTask = cashIds.Length > 0
                ? cashApiClient.GetByIdsAsync(userContext.FirmId, userContext.UserId, cashIds)
                : Task.FromResult(new List<CashDto>());

            var purseIds = moneySources.Where(x => x.Type == MoneySourceType.Purse).Select(x => (int)x.Id).ToArray();
            var purseSourcesTask = purseIds.Length > 0
                ? GetPurseKontragentsByPurseIdsAsync(userContext.FirmId, userContext.UserId, purseIds)
                : Task.FromResult(new List<MoneySource>());

            await Task.WhenAll(settlementAccountsTask, cashListTask, purseSourcesTask).ConfigureAwait(false);

            sources.AddRange(settlementAccountsTask.Result.Select(MapSettlementAccountToMoneySource));
            sources.AddRange(cashListTask.Result.Select(MapCashToMoneySource));
            sources.AddRange(purseSourcesTask.Result);

            return sources;
        }

        private async Task<List<MoneySource>> GetSourceItemsAsync(IUserContext userContext)
        {
            var sources = new List<MoneySource>();

            var settlementAccountsTask = settlementAccountClient.GetWithDeletedAsync(userContext.FirmId, userContext.UserId);
            var cashListTask = cashApiClient.GetAsync(userContext.FirmId, userContext.UserId);
            var purseSourcesTask = GetPurseKontragentsAsync(userContext.FirmId, userContext.UserId);

            await Task.WhenAll(settlementAccountsTask, cashListTask, purseSourcesTask).ConfigureAwait(false);

            sources.AddRange(settlementAccountsTask.Result.Select(MapSettlementAccountToMoneySource));
            sources.AddRange(cashListTask.Result.Select(MapCashToMoneySource));
            sources.AddRange(purseSourcesTask.Result);

            return sources.OrderByDescending(x => x.IsActive)
                .ThenBy(x => x.Type)
                .ThenByDescending(x => x.IsPrimary)
                .ThenBy(x => x.Id)
                .ToList();
        }

        private void AddBankDataToSources(IReadOnlyCollection<MoneySource> sources, Dictionary<int, SourceBankData> banks)
        {
            foreach (var source in sources)
            {
                if (source.BankId.HasValue && banks.TryGetValue((int)source.BankId, out SourceBankData sourceBank))
                {
                    source.BankName = sourceBank.FullName;
                    source.BankBik = sourceBank.Bik;
                    source.IconUrl = sourceBank.IconUrl;
                }
            }
        }

        private static void AddAllSourcesItemAsync(IList<MoneySource> sources)
        {
            var isMultyCurrency = sources.GroupBy(x => x.Currency).Count() > 1;
            sources.Insert(0, new MoneySource
            {
                Id = 0,
                Name = "Все счета и кассы",
                Type = MoneySourceType.All,
                IsActive = true,
                Balance = !isMultyCurrency ? sources.Sum(x => x.Balance) : 0,
                HideBalance = isMultyCurrency,
                HasActiveIntegration = sources.Any(x => x.HasActiveIntegration),
                HasUnprocessedRequests = sources.Any(x => x.HasUnprocessedRequests)
            });

        }

        private async Task<List<MoneySource>> GetPurseKontragentsByPurseIdsAsync(int firmId, int userId, IReadOnlyCollection<int> purseIds)
        {
            var pursesTask = purseClient.GetByIdsAsync(firmId, userId, purseIds);
            var kontragentsTask = kontragentsApiClient.GetPopulationAndPurseAgentsAsync(firmId, userId);
            await Task.WhenAll(pursesTask, kontragentsTask).ConfigureAwait(false);

            var kontragentPurses = kontragentsTask.Result
                .Join(pursesTask.Result, k => k.PurseId, p => p.Id, (k, p) => k)
                .ToList();

            return kontragentPurses
                .Select(k => MapPurseKontragentToMoneySource(k, pursesTask.Result.FirstOrDefault(p => p.Id == k.PurseId)))
                .ToList();
        }

        private async Task<List<MoneySource>> GetPurseKontragentsAsync(int firmId, int userId)
        {
            var kontragents = await kontragentsApiClient.GetPopulationAndPurseAgentsAsync(firmId, userId).ConfigureAwait(false);
            var purseIds = kontragents.Where(x => x.PurseId.HasValue)
                .Select(x => x.PurseId.Value)
                .Distinct()
                .ToList();
            var purses =
                (await purseClient.GetByIdsAsync(firmId, userId, purseIds).ConfigureAwait(false)).Where(
                    x => !x.IsDelete);

            var kontragentPurses = kontragents.Join(purses, k => k.PurseId, p => p.Id, (k, p) => k).ToList();

            return kontragentPurses
                .Select(k => MapPurseKontragentToMoneySource(k, purses.FirstOrDefault(p => p.Id == k.PurseId)))
                .ToList();
        }

        private static MoneySource MapSettlementAccountToMoneySource(SettlementAccountDto settlementAccount)
        {
            return new MoneySource
            {
                Id = settlementAccount.Id,
                Name = settlementAccount.Name,
                Number = settlementAccount.Number,
                Type = MoneySourceType.SettlementAccount,
                Currency = settlementAccount.Currency,
                BankId = settlementAccount.BankId,
                IsActive = !settlementAccount.IsDeleted,
                IsPrimary = settlementAccount.IsPrimary,
                IsTransit = settlementAccount.Type == SettlementAccountType.Transit,
                SubcontoId = settlementAccount.SubcontoId
            };
        }

        private static MoneySource MapCashToMoneySource(CashDto cash)
        {
            return new MoneySource
            {
                Id = cash.Id,
                Name = cash.Name,
                Type = MoneySourceType.Cash,
                Currency = Currency.RUB,
                IsActive = cash.IsEnable,
                IsPrimary = cash.IsMain,
                SubcontoId = cash.SubcontoId
            };
        }

        private static MoneySource MapPurseKontragentToMoneySource(KontragentDto kontragent, PurseDto purse)
        {
            return new MoneySource
            {
                Id = kontragent.Id,
                Name = kontragent.GetName(),
                Type = MoneySourceType.Purse,
                IsActive = true,
                SubcontoId = kontragent.SubcontoId,
                IntegrationPartner = PurseIntegrationHelper.GetPartnerByPurseName(purse?.Name)
            };
        }
    }
}