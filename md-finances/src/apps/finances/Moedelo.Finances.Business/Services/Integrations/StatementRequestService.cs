using Moedelo.BankIntegrationsV2.Client.BankOperation;
using Moedelo.BankIntegrationsV2.Client.DataInformation;
using Moedelo.BankIntegrationsV2.Client.IntegratedUser;
using Moedelo.BankIntegrationsV2.Client.Validation;
using Moedelo.BankIntegrationsV2.Dto.BankOperation;
using Moedelo.BankIntegrationsV2.Dto.DataInformation;
using Moedelo.BankIntegrationsV2.Dto.Integrations;
using Moedelo.CatalogV2.Client.Banks;
using Moedelo.CatalogV2.Dto.Banks;
using Moedelo.Common.Enums.Enums.Access;
using Moedelo.Common.Enums.Enums.Integration;
using Moedelo.CommonV2.EventBus;
using Moedelo.CommonV2.EventBus.Cash;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.Finances.Business.Helpers.Money;
using Moedelo.Finances.Business.Services.Integrations.Exceptions;
using Moedelo.Finances.Business.Services.Integrations.Models;
using Moedelo.Finances.Domain.Interfaces.Business.Integrations;
using Moedelo.Finances.Domain.Interfaces.Business.Money;
using Moedelo.Finances.Domain.Models.Integrations;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.EventBus;
using Moedelo.InfrastructureV2.Domain.Interfaces.Logging;
using Moedelo.KontragentsV2.Client.Kontragents;
using Moedelo.RequisitesV2.Client.Purses;
using Moedelo.RequisitesV2.Client.SettlementAccounts;
using Moedelo.RequisitesV2.Dto.SettlementAccounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.BankIntegrations.IntegrationPartnersInfo.Extensions;
using Moedelo.Common.Enums.Enums.Finances.Money;
using IntegrationPartners = Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums.IntegrationPartners;

namespace Moedelo.Finances.Business.Services.Integrations
{
    [InjectAsSingleton]
    public class StatementRequestService : IStatementRequestService
    {
        private const string TAG = nameof(StatementRequestService);

        private static readonly IntegrationPartners[] NotBankIntegration =
        {
            IntegrationPartners.SapeRu,
            IntegrationPartners.Robokassa,
            IntegrationPartners.YandexKassa,
            IntegrationPartners.YMoney
        };

        private readonly ILogger logger;
        private readonly IBanksApiClient banksApiClient;
        private readonly ISettlementAccountClient settlementAccountClient;
        private readonly IBankOperationClient bankOperationClient;
        private readonly IIntegratedUserClient integratedUserClient;
        private readonly IMoneyOperationReader moneyOperationReader;
        private readonly ISettlementAccountValidationClient settlementAccountValidationClient;
        private readonly IPublisher<YandexMovementsRequestedEvent> yandexMovementRequestedPublisher;
        private readonly IPurseClient purseClient;
        private readonly IKontragentsClient kontragentsClient;
        private readonly IBankIntegrationsDataInformationClient bankIntegrationsDataInformationClient;

        public StatementRequestService(
            ILogger logger,
            IPublisherFactory publisherFactory,
            IBanksApiClient banksApiClient,
            ISettlementAccountClient settlementAccountClient,
            IBankOperationClient bankOperationClient,
            IIntegratedUserClient integratedUserClient,
            IMoneyOperationReader moneyOperationReader,
            ISettlementAccountValidationClient settlementAccountValidationClient,
            IPurseClient purseClient,
            IKontragentsClient kontragentsClient,
            IBankIntegrationsDataInformationClient bankIntegrationsDataInformationClient)
        {
            this.logger = logger;
            this.banksApiClient = banksApiClient;
            this.settlementAccountClient = settlementAccountClient;
            this.bankOperationClient = bankOperationClient;
            this.integratedUserClient = integratedUserClient;
            this.moneyOperationReader = moneyOperationReader;
            this.settlementAccountValidationClient = settlementAccountValidationClient;
            this.purseClient = purseClient;
            this.kontragentsClient = kontragentsClient;
            yandexMovementRequestedPublisher = publisherFactory.GetForAllClient(EventBusMessages.YandexMovementsRequested);
            this.bankIntegrationsDataInformationClient = bankIntegrationsDataInformationClient;
        }

        public async Task<BankStatementResponse> SendStatementRequestsAsync(IUserContext userContext, BankStatementRequestBySettlementAccounts request)
        {
            var result = await SendStatementRequestsVerboseAsync(
                userContext,
                request).ConfigureAwait(false);
                
            if (!result.Any())
            {
                return new BankStatementResponse
                {
                    IsSuccess = false,
                    Message = "Рaсчетные счета не найдены",
                };
            }

            var integrationEnabledRecords = result.Where(r => r.BlockedReason == null).ToArray();
            if (!integrationEnabledRecords.Any())
            {
                return new BankStatementResponse
                {
                    IsSuccess = false,
                    Message = "Интеграция не подключена",
                };
            }

            return new BankStatementResponse
            {
                IsSuccess = integrationEnabledRecords.All(x => x.IsSuccess),
                MessageList = integrationEnabledRecords
                    .Where(x => !x.IsSuccess)
                    .Select(x => $"{x.SettlementAccountNumber} - {x.Error}")
                    .ToList()
            };
        }
        
        public async Task<List<ResultOfStatementRequest>> SendStatementRequestsVerboseAsync(IUserContext userContext, BankStatementRequestBySettlementAccounts request)
        {
            var result = new List<ResultOfStatementRequest>();

            if (request.StopOnUnprocessedRequest)
            {
                var requestQueueStatus = await bankIntegrationsDataInformationClient.GetRequestQueueStatusAsync(
                    userContext.FirmId);

                if (requestQueueStatus.Where(x => x.HasUnprocessedRequests).Any())
                {
                       result.Add( new ResultOfStatementRequest { BlockedReason = StatementRequestBlockedReason.ExistsUnprocessedRequest, IsSuccess = false });
                    logger.Error(TAG, $"HasUnprocessedRequests {requestQueueStatus.Where(x => x.HasUnprocessedRequests).Any()}");
                    return result;
                }
            }

            var settlementAccounts = await settlementAccountClient.GetAsync(userContext.FirmId, userContext.UserId).ConfigureAwait(false);
            var settlementAccountsStatus = await GetSettlementAccountsStasusForIntegrationAsync(userContext, settlementAccounts).ConfigureAwait(false);
            var validSettlementAccounts = await GetValidSettlementsForRequestMovementsAsync(settlementAccounts, userContext, settlementAccountsStatus).ConfigureAwait(false);
            var bankStatements = validSettlementAccounts.Select(x => new BankStatement(x.Id, x.Number, x.BankId)).ToArray();

            try
            {
                bankStatements = await FillBankStatementsAsync(userContext, bankStatements).ConfigureAwait(false);
                bankStatements = await FillDatesAsync(userContext, request.StartDate, request.EndDate, bankStatements).ConfigureAwait(false);
            }
            catch (Exception ex) when (ex is BankNotFoundException || ex is IntegrationNotFoundException)
            {
                bankStatements = Array.Empty<BankStatement>();
            }
            
            var isAccountingTask = userContext.HasAllRuleAsync(AccessRule.UsnAccountantTariff);
            var contextExtraDataTask = userContext.GetContextExtraDataAsync();
            await Task.WhenAll(isAccountingTask, contextExtraDataTask).ConfigureAwait(false);

            foreach (var settlementAccount in settlementAccounts)
            {
                var resultByRequest = new ResultOfStatementRequest
                {
                    SettlementAccountId = settlementAccount.Id,
                    SettlementAccountNumber = settlementAccount.Number,
                    IsSuccess = false
                };
                result.Add(resultByRequest);

                if (validSettlementAccounts.All(v => v.Id != settlementAccount.Id))
                {
                    resultByRequest.BlockedReason = StatementRequestBlockedReason.SettlementAccountNotValid;
                    continue;
                }

                var bankStatement = bankStatements.FirstOrDefault(x => x.SettlementAccountId == settlementAccount.Id);
                if (bankStatement == null)
                {
                    resultByRequest.BlockedReason = StatementRequestBlockedReason.IntegrationDisabled;
                    continue;
                }

                var requestDto = MapToMovementListRequest(
                    userContext.FirmId,
                    bankStatement.StartDate,
                    bankStatement.EndDate,
                    bankStatement.SettlementAccountNumber,
                    bankStatement.BankBik,
                    bankStatement.IntegrationPartner.Value,
                    contextExtraDataTask.Result.Inn,
                    isAccountingTask.Result);

                var response = await bankOperationClient.RequestMovementListAsync(requestDto).ConfigureAwait(false);
                resultByRequest.IsSuccess = response.IsSuccess;

                if (response.IsSuccess == false)
                {
                    logger.Error(TAG, $"RequestMovementList failed for request {response.RequestId}");
                    resultByRequest.Error = response.Message;
                }

                resultByRequest.RequestId = response.RequestId;
            }
            return result;
        }

        public async Task<BankStatementResponse> SendStatementRequestAsync(IUserContext userContext, BankStatementRequestBySettlementAccount request)
        {
            var settlementAccount = await GetSettlementAccountAsync(userContext, request).ConfigureAwait(false);
            var bank = await GetBankAsync(userContext, settlementAccount).ConfigureAwait(false);
            var isAccountingTask = userContext.HasAllRuleAsync(AccessRule.UsnAccountantTariff);
            var contextExtraDataTask = userContext.GetContextExtraDataAsync();
            var integrationPartnerTask = GetIntegrationPartner(userContext, bank);
            await Task.WhenAll(isAccountingTask, contextExtraDataTask, integrationPartnerTask).ConfigureAwait(false);

            var partner = integrationPartnerTask.Result;
            var startDate = await GetStartDateAsync(userContext, request.StartDate, partner).ConfigureAwait(false);
            var endDate = request.EndDate ?? DateTime.Today;
            var requestDto = MapToMovementListRequest(userContext.FirmId, startDate, endDate, settlementAccount.Number, bank.Bik, partner, contextExtraDataTask.Result.Inn, isAccountingTask.Result);
            var response = await bankOperationClient.RequestMovementListAsync(requestDto).ConfigureAwait(false);
            if (response.IsSuccess == false)
            {
                logger.Error(TAG, $"RequestMovementList failed for request {response.RequestId}");
                return new BankStatementResponse
                {
                    Message = response.Message ?? "Произошла ошибка при попытке запросить выписку",
                };
            }

            return new BankStatementResponse
            {
                Message = response.Message,
                IsSuccess = response.IsSuccess,
                PhoneMask = response.PhoneMask
            };
        }

        public async Task<BankStatementResponse> SendStatementRequestAsync(IUserContext userContext, BankStatementRequestByIntegrationPartner request)
        {
            var startDate = await GetStartDateAsync(userContext, request.StartDate, request.IntegrationPartner).ConfigureAwait(false);
            var endDate = request.EndDate ?? DateTime.Today;

            if (NotBankIntegration.Contains(request.IntegrationPartner))
            {
                return await SendStatementRequestToNonBankIntegrationAsync(userContext, request, startDate, endDate).ConfigureAwait(false);
            }

            var isAccountingTask = userContext.HasAllRuleAsync(AccessRule.UsnAccountantTariff);
            var contextExtraDataTask = userContext.GetContextExtraDataAsync();
            var settlementAccountsTask = GetSettlementAccountsAsync(userContext, request);
            await Task.WhenAll(isAccountingTask, contextExtraDataTask, settlementAccountsTask).ConfigureAwait(false);

            var settlementAccounts = settlementAccountsTask.Result;
            var requestDtos = settlementAccounts.Select(x => MapToMovementListRequest(userContext.FirmId, startDate, endDate, x.SettlementNumber, x.Bik, x.IntegrationPartner, contextExtraDataTask.Result.Inn, isAccountingTask.Result)).ToArray();
            if (requestDtos.Length == 0)
            {
                throw new SettlementAccountNotFoundException();
            }

            var responseMessageList = new List<string>();
            var isSuccess = true;
            foreach (var requestDto in requestDtos)
            {
                var response = await bankOperationClient.RequestMovementListAsync(requestDto).ConfigureAwait(false);
                var message = response.Message;
                if (response.Status == IntegrationResponseStatusCode.NeedSms)
                {
                    return new BankStatementResponse
                    {
                        IsSuccess = true,
                        Message = message,
                        PhoneMask = response.PhoneMask
                    };
                }
                if (response.IsSuccess == false)
                {
                    logger.Error(TAG, $"RequestMovementList failed for request {response.RequestId}");
                    isSuccess = response.IsSuccess;
                    message = $"Не удалось запросить выписку за указанный период по р/сч: {requestDto.IdentityDto.SettlementNumber} ({response.Message})";
                }

                if (!responseMessageList.Contains(message))
                {
                    responseMessageList.Add(message);
                }
            }

            return new BankStatementResponse
            {
                IsSuccess = isSuccess,
                Message = string.Join(Environment.NewLine, responseMessageList),
                MessageList = responseMessageList
            };
        }

        public async Task<BankStatementResponse> SendPurseStatementRequestAsync(IUserContext userContext, StatementRequestByPurse request)
        {
            var kontragent = await kontragentsClient.GetByIdAsync(userContext.FirmId, userContext.UserId, (int)request.KontragentId).ConfigureAwait(false);
            if (kontragent?.PurseId == null)
            {
                throw new KontragentNotFoundException($"Для кошелька не найден контрагент {request.KontragentId}");
            }

            var purse =
                    (await purseClient.GetByIdsAsync(
                            userContext.FirmId,
                            userContext.UserId,
                            new List<int> { kontragent.PurseId.Value }
                        ).ConfigureAwait(false)
                    )
                    .Where(x => !x.IsDelete)
                    .FirstOrDefault();

            if (purse == null)
            {
                throw new PurseNotFoundException($"Не найден кошелек {kontragent.PurseId}");
            }

            var purseIntegrationPartner = PurseIntegrationHelper.GetPartnerByPurseName(purse.Name);
            if (purseIntegrationPartner == null)
            {
                throw new IntegrationNotFoundException($"Не найдена интеграция для кошелька {purse.Id}, {purse.Name}");
            }

            var statementRequest = new BankStatementRequestByIntegrationPartner
            {
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                IntegrationPartner = purseIntegrationPartner.Value
            };

            return await SendStatementRequestAsync(userContext, statementRequest).ConfigureAwait(false);
        }

        private async Task<List<SettlementAccountDto>> GetValidSettlementsForRequestMovementsAsync(List<SettlementAccountDto> settlementAccounts,
                                                                                                    IUserContext userContext, List<SettlementAccountStatusDto> settlementsForValidation)
        {
            var validSettlementAccounts = new List<SettlementAccountDto>();
            foreach (var settlement in settlementsForValidation)
            {
                var isAccountNumberValid = await settlementAccountValidationClient
                    .ValidateNumber(settlement.SettlementNumber, settlement.IntegrationPartner, userContext.FirmId).ConfigureAwait(false);

                if (isAccountNumberValid)
                {
                    validSettlementAccounts.Add(settlementAccounts.Where(x => x.Number == settlement.SettlementNumber).FirstOrDefault());
                }
            }

            return validSettlementAccounts;
        }

        private async Task<BankStatementResponse> SendStatementRequestToNonBankIntegrationAsync(IUserContext userContext, BankStatementRequestByIntegrationPartner request, DateTime startDate, DateTime endDate)
        {
            if (request.IntegrationPartner == IntegrationPartners.YandexKassa || request.IntegrationPartner == IntegrationPartners.YMoney)
            {
                var yandexMovementsRequestedEvent = new YandexMovementsRequestedEvent
                {
                    FirmId = userContext.FirmId,
                    StartDate = startDate,
                    EndDate = endDate,
                };
                await yandexMovementRequestedPublisher.PublishAsync(yandexMovementsRequestedEvent).ConfigureAwait(false);
                return new BankStatementResponse
                {
                    Message = "Ваш запрос поставлен в очередь на обработку",
                    IsSuccess = true
                };
            }

            var isAccountingTask = userContext.HasAllRuleAsync(AccessRule.UsnAccountantTariff);
            var contextExtraDataTask = userContext.GetContextExtraDataAsync();
            var settlementAccountsTask = GetSettlementAccountsAsync(userContext, request);
            await Task.WhenAll(isAccountingTask, contextExtraDataTask, settlementAccountsTask).ConfigureAwait(false);

            var requestDto = MapToMovementListRequest(userContext.FirmId, startDate, endDate, request.IntegrationPartner, contextExtraDataTask.Result.Inn, isAccountingTask.Result);
            var response = await bankOperationClient.RequestMovementListAsync(requestDto).ConfigureAwait(false);
            return new BankStatementResponse
            {
                Message = response.Message,
                IsSuccess = response.IsSuccess
            };
        }

        private async Task<BankStatement[]> FillDatesAsync(IUserContext userContext, DateTime? startDate, DateTime? endDate, IReadOnlyCollection<BankStatement> bankStatements)
        {
            var today = DateTime.Today;
            var fromDate = await GetMinStartDateAsync(userContext, startDate).ConfigureAwait(false);

            foreach (var bankStatement in bankStatements)
            {
                var adjustedStartDate = AdjustStartDateIfNeeded(
                    bankStatement.IntegrationPartner.Value, 
                    fromDate, 
                    today
                );
                bankStatement.StartDate = adjustedStartDate;
                bankStatement.EndDate = endDate ?? today;
            }

            return bankStatements.ToArray();
        }

        private async Task<DateTime> GetStartDateAsync(IUserContext userContext, DateTime? startDate, IntegrationPartners partner)
        {
            var today = DateTime.Today;
            var fromDate = await GetMinStartDateAsync(userContext, startDate).ConfigureAwait(false);
            return AdjustStartDateIfNeeded(partner, fromDate, today);
        }

        private async Task<DateTime> GetMinStartDateAsync(IUserContext userContext, DateTime? startDate)
        {
            var today = DateTime.Today;

            var fromDate = (startDate ??
                await moneyOperationReader.GetLastOperationDateUntilAsync(userContext.FirmId, today).ConfigureAwait(false) ??
                today).Date;

            return fromDate;
        }
        
        private DateTime AdjustStartDateIfNeeded(IntegrationPartners partner, DateTime fromDate, DateTime today)
        {
            if (!IntegrationPartnersExtentions.IsTodayStatementAvailable(partner) && fromDate >= today)
            {
                return today.AddDays(-1);
            }

            return fromDate;
        }

        private async Task<SettlementAccountDto> GetSettlementAccountAsync(IUserContext userContext, BankStatementRequestBySettlementAccount request)
        {
            var settlementAccount = await settlementAccountClient.GetByIdAsync(userContext.FirmId, userContext.UserId, request.SettlementAccountId).ConfigureAwait(false);
            if (string.IsNullOrEmpty(settlementAccount?.Number))
            {
                throw new SettlementAccountNotFoundException();
            }
            return settlementAccount;
        }

        private async Task<SettlementAccountStatusDto[]> GetSettlementAccountsAsync(IUserContext userContext, BankStatementRequestByIntegrationPartner request)
        {
            var settlementAccounts = await settlementAccountClient.GetAsync(userContext.FirmId, userContext.UserId).ConfigureAwait(false);
            var settlementsForValidation = await GetSettlementAccountsStasusForIntegrationAsync(userContext, settlementAccounts).ConfigureAwait(false);
            var validSettlementAccounts = await GetValidSettlementsForRequestMovementsAsync(settlementAccounts, userContext, settlementsForValidation).ConfigureAwait(false);
            if (validSettlementAccounts.Count == 0)
            {
                return Array.Empty<SettlementAccountStatusDto>();
            }

            return settlementsForValidation
                .Where(x => x.Status == SettlementIntegrationStatus.Enabled && x.IntegrationPartner == request.IntegrationPartner)
                .ToArray();
        }

        private async Task<BankStatement[]> FillBankStatementsAsync(IUserContext userContext, IReadOnlyCollection<BankStatement> bankStatements)
        {
            var activeIntegrations = await integratedUserClient.GetActiveIntegrationsForFirmAsync(userContext.FirmId).ConfigureAwait(false);
            if (activeIntegrations == null || !activeIntegrations.Any())
            {
                throw new IntegrationNotFoundException();
            }

            var bankIds = bankStatements.Select(x => x.BankId).Distinct().ToArray();
            var banks = await banksApiClient.GetByIdsAsync(bankIds).ConfigureAwait(false);
            if (!banks.Any())
            {
                throw new BankNotFoundException();
            }

            var settlementAccountV2Dtos = new List<SettlementAccountV2Dto>();
            foreach (var bankStatement in bankStatements)
            {
                var bank = banks.FirstOrDefault(b => b.Id == bankStatement.BankId);
                bankStatement.BankBik = bank.Bik;
                settlementAccountV2Dtos.Add(new SettlementAccountV2Dto
                {
                    SettlementNumber = bankStatement.SettlementAccountNumber,
                    BankFullName = bank.FullName,
                    Bik = bank.Bik
                });
            }

            var intSummary = await bankIntegrationsDataInformationClient.
                GetIntSummaryBySettlementsAsync(new IntSummaryBySettlementsRequestDto
                {
                    FirmId = userContext.FirmId,
                    UserId = userContext.UserId,
                    Settlements = settlementAccountV2Dtos
                }).ConfigureAwait(false);

            foreach (var integrationStatus in intSummary.Result)
            {
                if (integrationStatus.Status == SettlementIntegrationStatus.Enabled)
                {
                    var bankStatement = bankStatements.FirstOrDefault(s => s.BankBik == integrationStatus.Bik && s.SettlementAccountNumber == integrationStatus.SettlementNumber);
                    if (bankStatement != null)
                    {
                        bankStatement.IntegrationPartner = integrationStatus.IntegrationPartner;
                    }
                }
            }

            return bankStatements.Where(s => s.IntegrationPartner.HasValue && activeIntegrations.Any(i => i == s.IntegrationPartner.Value && s.IntegrationPartner != IntegrationPartners.Vtb24Bank && s.IntegrationPartner != IntegrationPartners.VtbBank)).ToArray();
        }

        private async Task<BankDto> GetBankAsync(IUserContext userContext, SettlementAccountDto settlementAccount)
        {
            var bank = (await banksApiClient.GetByIdsAsync(new[] { settlementAccount.BankId }).ConfigureAwait(false)).FirstOrDefault();

            if (string.IsNullOrEmpty(bank?.Bik))
            {
                throw new BankNotFoundException();
            }

            var settlementAccountV2Dto = new SettlementAccountV2Dto
            {
                SettlementNumber = settlementAccount.Number,
                BankFullName = bank.FullName,
                Bik = bank.Bik
            };

            // Правильное получение IntegrationPartner'a
            var intSummary = await bankIntegrationsDataInformationClient.
                GetIntSummaryBySettlementsAsync(new IntSummaryBySettlementsRequestDto
                {
                    FirmId = userContext.FirmId,
                    UserId = userContext.UserId,
                    Settlements = new List<SettlementAccountV2Dto> { settlementAccountV2Dto }
                }).ConfigureAwait(false);
            var integrations = intSummary.Result.ToDictionary(x => x.SettlementNumber);

            if (integrations.TryGetValue(settlementAccount.Number, out var integration) && bank.Bik == integration.Bik)
            {
                bank.IntegratedPartner = integration.IntegrationPartner;
            }

            return bank;
        }

        private async Task<IntegrationPartners> GetIntegrationPartner(IUserContext userContext, BankDto bank)
        {
            if (bank.IntegratedPartner.HasValue)
            {
                var activeIntegrations = (await integratedUserClient.GetActiveIntegrationsForFirmAsync(userContext.FirmId).ConfigureAwait(false))
                    ?? new List<IntegrationPartners>();
                if (activeIntegrations.Contains(bank.IntegratedPartner ?? IntegrationPartners.Undefined))
                {
                    return bank.IntegratedPartner.Value;
                }
            }
            throw new IntegrationNotFoundException();
        }

        private static RequestMovementListRequestDto MapToMovementListRequest(int firmId, DateTime startDate, DateTime endDate, IntegrationPartners integrationPartner, string inn, bool isAccounting)
        {
            return new RequestMovementListRequestDto
            {
                BeginDate = startDate,
                EndDate = endDate,
                IsManual = true,
                IsAccounting = isAccounting,
                IdentityDto = new IntegrationIdentityDto
                {
                    FirmId = firmId,
                    Inn = inn,
                    IntegrationPartner = integrationPartner
                }
            };
        }

        private static RequestMovementListRequestDto MapToMovementListRequest(int firmId, DateTime startDate, DateTime endDate, string settlementAccountNumber, string bankBik, IntegrationPartners integrationPartner, string inn, bool isAccounting)
        {
            return new RequestMovementListRequestDto
            {
                BeginDate = startDate,
                EndDate = endDate,
                IsManual = true,
                IsAccounting = isAccounting,
                IdentityDto = new IntegrationIdentityDto
                {
                    FirmId = firmId,
                    Inn = inn,
                    Bik = bankBik,
                    IntegrationPartner = integrationPartner,
                    SettlementNumber = settlementAccountNumber,
                }
            };
        }

        private async Task<List<SettlementAccountStatusDto>> GetSettlementAccountsStasusForIntegrationAsync(IUserContext userContext, List<SettlementAccountDto> settlementAccounts)
        {
            var banks = (await banksApiClient.GetByIdsAsync(settlementAccounts.Select(x => x.BankId).ToList()).ConfigureAwait(false)).ToDictionary(x => x.Id);

            var settlementAccountsDict = settlementAccounts.ToDictionary(x => x.Number);

            var settlements = settlementAccountsDict.Select(x =>
                new SettlementAccountV2Dto
                {
                    SettlementNumber = x.Value.Number,
                    BankFullName = banks[x.Value.BankId].FullName,
                    Bik = banks[x.Value.BankId].Bik
                }).ToList();

            //Правильное получение IntegrationPartner'a
            var intSummary = await bankIntegrationsDataInformationClient.GetIntSummaryBySettlementsAsync(
                new IntSummaryBySettlementsRequestDto
                {
                    FirmId = userContext.FirmId,
                    UserId = userContext.UserId,
                    Settlements = settlements.Where(x => !string.IsNullOrEmpty(x.Bik)).ToList()
                }).ConfigureAwait(false);

            return intSummary.Result;
        }
    }
}
