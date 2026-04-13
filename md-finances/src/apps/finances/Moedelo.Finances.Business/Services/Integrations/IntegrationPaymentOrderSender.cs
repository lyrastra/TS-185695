using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntegrationPartners = Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums.IntegrationPartners;
using Moedelo.BankIntegrations.ApiClient.Dto.BankOperation.Invoice;
using Moedelo.BankIntegrations.ApiClient.Dto.BankOperation;
using Moedelo.BankIntegrations.ApiClient.Dto.Payments;
using Moedelo.BankIntegrations.ApiClient.Framework.Abstractions.BankOperationLegacy;
using Moedelo.BankIntegrations.ApiClient.Framework.Abstractions.BankOperations;
using Moedelo.BankIntegrations.Enums.Invoices;
using Moedelo.BankIntegrations.Enums;
using Moedelo.BankIntegrationsV2.Client.DataInformation;
using Moedelo.BankIntegrationsV2.Dto.DataInformation;
using Moedelo.CatalogV2.Client.Banks;
using Moedelo.CatalogV2.Dto.Banks;
using Moedelo.Common.Enums.Enums.Access;
using Moedelo.CommonV2.Extensions.System;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.Finances.Business.Mappers.Integrations;
using Moedelo.Finances.Business.Services.Integrations.Models;
using Moedelo.Finances.Business.Services.Integrations.PaymentOrderCreators;
using Moedelo.Finances.Domain.Enums.Money;
using Moedelo.Finances.Domain.Interfaces.Business.Integrations;
using Moedelo.Finances.Domain.Interfaces.Business.Money.PaymentOrders;
using Moedelo.Finances.Domain.Models.Integrations;
using Moedelo.Finances.Domain.Models.Money.Operations.PaymentOrders;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.Logging;
using Moedelo.InfrastructureV2.Json;
using Moedelo.RequisitesV2.Client.SettlementAccounts;
using Moedelo.RequisitesV2.Dto.SettlementAccounts;

namespace Moedelo.Finances.Business.Services.Integrations
{
    [InjectAsSingleton]
    public class IntegrationPaymentOrderSender : IIntegrationPaymentOrderSender
    {
        private const string TAG = nameof(IntegrationPaymentOrderSender);
        private const string MsgNotHaveParams = "Платеж не может быть отправлен, произошла техническая ошибка.";
        private readonly ILogger logger;
        private readonly IBanksApiClient banksApiClient;
        private readonly ISettlementAccountClient settlementAccountClient;
        private readonly IBankOperationApiClient bankOperationApiClient;
        private readonly IPaymentOrderOperationReader paymentOrderOperationReader;
        private readonly IIntegrationAccPaymentOrderCreator integrationAccPaymentOrderCreator;
        private readonly IIntegrationBizPaymentOrderCreator integrationBizPaymentOrderCreator;
        private readonly IBankIntegrationsDataInformationClient integrationsDataInformationClient;
        private readonly IBankOperationsApiClient bankOperationsApiClient;

        public IntegrationPaymentOrderSender(
            ILogger logger,
            IBanksApiClient banksApiClient,
            ISettlementAccountClient settlementAccountClient,
            IBankOperationApiClient bankOperationApiClient,
            IPaymentOrderOperationReader paymentOrderOperationReader,
            IIntegrationAccPaymentOrderCreator integrationAccPaymentOrderCreator,
            IIntegrationBizPaymentOrderCreator integrationBizPaymentOrderCreator,
            IBankIntegrationsDataInformationClient integrationsDataInformationClient,
            IBankOperationsApiClient bankOperationsApiClient)
        {
            this.logger = logger;
            this.banksApiClient = banksApiClient;
            this.settlementAccountClient = settlementAccountClient;
            this.bankOperationApiClient = bankOperationApiClient;
            this.paymentOrderOperationReader = paymentOrderOperationReader;
            this.integrationAccPaymentOrderCreator = integrationAccPaymentOrderCreator;
            this.integrationBizPaymentOrderCreator = integrationBizPaymentOrderCreator;
            this.integrationsDataInformationClient = integrationsDataInformationClient;
            this.bankOperationsApiClient = bankOperationsApiClient;
        }

        public async Task<SendPaymentOrdersResponse> SendAsync(IUserContext userContext, IReadOnlyCollection<long> baseIds)
        {
            if (baseIds.Count == 0)
            {
                return new SendPaymentOrdersResponse()
                {
                    ErrorCode = SendPaymentErrorCode.Common,
                    Message = MsgNotHaveParams,
                    StatusCode = IntegrationResponseStatusCode.Error
                };
            }

            var result = new SendPaymentOrdersResponse();

            var outgoingOperations = await GetOutgoingOperationsBySettlementAccountIdsAsync(userContext, baseIds).ConfigureAwait(false);
            if (outgoingOperations.Count == 0)
            {
                return result;
            }

            var context = await LoadSendPaymentOrdersContextAsync(outgoingOperations, userContext).ConfigureAwait(false);

            foreach (var operationsBySettlementAccount in context.OperationsBySettlementAccounts)
            {
                var integrationInfo = SettlementAccountIntegrationInfo.CreateFromContext(operationsBySettlementAccount.Key, context);

                var integrationPaymentOrderTasks = operationsBySettlementAccount.Value.Select(x => MapToIntegrationPaymentOrderAsync(userContext, x.Order, integrationInfo.IntegrationPartner));
                await Task.WhenAll(integrationPaymentOrderTasks).ConfigureAwait(false);
                var integrationPaymentOrders = integrationPaymentOrderTasks.Select(x => x.Result).ToList();

                var response = await SendPaymentOrdersToIntegrationAsync(userContext, integrationInfo, integrationPaymentOrders).ConfigureAwait(false);
                result.StatusCode = response.StatusCode;
                result.PhoneMask = response.PhoneMask;
                result.Message = response.Message;
                result.ErrorCode = response.ErrorCode;

                foreach (var externalDocument in response.ExternalDocumentIds)
                {
                    if (context.OperationsByGuid.TryGetValue(externalDocument.Id, out var paymentOrderWithGuid))
                    {
                        result.List.Add(new SendPaymentOrderResponse
                        {
                            DocumentBaseId = paymentOrderWithGuid.DocumentBaseId,
                            Date = paymentOrderWithGuid.Date,
                            Sum = paymentOrderWithGuid.Sum,
                            Number = paymentOrderWithGuid.Number,
                            ExternalId = externalDocument?.ExternalId,
                            Error = externalDocument?.DescriptionStatus,
                        });
                    }
                }
            }

            return result;
        }

        public async Task<SendBankInvoiceResponse> SendBankInvoiceAsync(IUserContext userContext, SendBankInvoiceRequest request)
        {
            var result = new SendBankInvoiceResponse();

            var outgoingOperations = await GetOutgoingOperationsBySettlementAccountIdsAsync(userContext, new[] { request.OperationId } ).ConfigureAwait(false);
            if (outgoingOperations.Count == 0)
            {
                return result;
            }

            var context = await LoadSendPaymentOrdersContextAsync(outgoingOperations, userContext).ConfigureAwait(false);
            var settlementAccountId = context.OperationsBySettlementAccounts.First().Key;
            var operation = context.OperationsBySettlementAccounts.First().Value.First();
            var integrationInfo = SettlementAccountIntegrationInfo.CreateFromContext(settlementAccountId, context);

            var integrationPaymentOrder = await MapToIntegrationPaymentOrderAsync(userContext, operation.Order, integrationInfo.IntegrationPartner).ConfigureAwait(false);

            var response = await SendBankInvoiceAsync(
                userContext, 
                integrationInfo,
                integrationPaymentOrder,
                request
            ).ConfigureAwait(false);
            
            result.StatusCode = response.StatusCode;
            result.InvoiceStatusCode = response.Data.InvoiceResponseStatusCode;
            result.Message = response.Data.ErrorMessageForUser;
            result.InvoiceUrl = response.Data.InvoiceUrl;

            return result;
        }

        private async Task<List<OrderWithGuid>> GetOutgoingOperationsBySettlementAccountIdsAsync(IUserContext userContext, IReadOnlyCollection<long> baseIds)
        {
            var isBiz = await userContext.HasAllRuleAsync(AccessRule.BizPlatform).ConfigureAwait(false);
            if (isBiz)
            {
                throw new NotImplementedException();
            }
            var accOperations = await paymentOrderOperationReader.GetByBaseIdsAsync(userContext.FirmId, baseIds).ConfigureAwait(false);
            return accOperations.Where(x => x.SettlementAccountId.HasValue && x.Direction == MoneyDirection.Outgoing)
                .Select(MapToOrderWithGuid)
                .ToList();
        }

        private static OrderWithGuid MapToOrderWithGuid(PaymentOrderOperation operation)
        {
            return new OrderWithGuid
            {
                Guid = Guid.NewGuid(),
                DocumentBaseId = operation.DocumentBaseId,
                SettlementAccountId = operation.SettlementAccountId.Value,
                Date = operation.Date,
                Number = operation.Number,
                Sum = operation.Sum,
                Order = operation
            };
        }

        private async Task<Dictionary<string, SettlementAccountStatusDto>> GetIntegrationsAsync(IUserContext userContext, Dictionary<int, SettlementAccountDto> settlementAccounts, Dictionary<int, BankDto> banks)
        {
            var settlements = settlementAccounts.Select(x =>
                new SettlementAccountV2Dto
                {
                    SettlementNumber = x.Value.Number,
                    BankFullName = banks[x.Value.BankId].FullName,
                    Bik = banks[x.Value.BankId].Bik
                }).ToList();
            // Правильное получение IntegrationPartner'a
            var intSummary = await integrationsDataInformationClient.GetIntSummaryBySettlementsAsync(
                new IntSummaryBySettlementsRequestDto
                {
                    FirmId = userContext.FirmId,
                    UserId = userContext.UserId,
                    Settlements = settlements.Where(x => !string.IsNullOrEmpty(x.Bik)).ToList()
                }).ConfigureAwait(false);
            return intSummary.Result.ToDictionary(x => x.SettlementNumber);
        }

        private async Task<PaymentOrderDto> MapToIntegrationPaymentOrderAsync(IUserContext userContext, object order, IntegrationPartners? integrationPartner)
        {
            var isAccounting = await userContext.HasAllRuleAsync(AccessRule.UsnAccountantTariff).ConfigureAwait(false);
            if (isAccounting)
            {
                return await integrationAccPaymentOrderCreator.CreateAsync(Guid.NewGuid(), order, integrationPartner).ConfigureAwait(false);
            }
            return await integrationBizPaymentOrderCreator.CreateAsync(Guid.NewGuid(), order, integrationPartner).ConfigureAwait(false);
        }

        private async Task<SendPaymentOrderResponseDto> SendPaymentOrdersToIntegrationAsync(IUserContext userContext, SettlementAccountIntegrationInfo integrationInfo, List<PaymentOrderDto> integrationPaymentOrders)
        {
            var contextExtraData = await userContext.GetContextExtraDataAsync().ConfigureAwait(false);
            var identity = new IntegrationIdentityDto
            {
                FirmId = userContext.FirmId,
                Inn = contextExtraData.Inn,
                SettlementNumber = integrationInfo.SettlementAccount.Number,
                Bik = integrationInfo.Bank.Bik,
                IntegrationPartner = integrationInfo.IntegrationPartner
            };

            logger.Info(TAG, $"SendPaymentOrdersAsync request: {integrationPaymentOrders.ToJsonString()}", userContext.GetAuditContext());
            var response = await bankOperationApiClient
                .SendPaymentOrdersAsync(integrationPaymentOrders.Select(ReplaceWrongSymbols).ToList(), identity)
                .ConfigureAwait(false);
            logger.Info(TAG, $"SendPaymentOrdersAsync response: {response.ToJsonString()}", userContext.GetAuditContext());
            return response;

        }

        private async Task<IntegrationResponseDto<SendBankInvoiceResponseDto>> SendBankInvoiceAsync(
            IUserContext userContext, 
            SettlementAccountIntegrationInfo integrationInfo,
            PaymentOrderDto integrationPaymentOrder,
            SendBankInvoiceRequest request
            )
        {
            var contextExtraData = await userContext.GetContextExtraDataAsync().ConfigureAwait(false);
            var identity = new IntegrationIdentityDto
            {
                FirmId = userContext.FirmId,
                Inn = contextExtraData.Inn,
                SettlementNumber = integrationInfo.SettlementAccount.Number,
                Bik = integrationInfo.Bank.Bik,
                IntegrationPartner = integrationInfo.IntegrationPartner
            };
            logger.Info(TAG, $"SendInvoiceAsync request: {integrationPaymentOrder.ToJsonString()}", userContext.GetAuditContext());
            var response = await bankOperationsApiClient.SendInvoiceAsync(
                userContext.FirmId,
                userContext.UserId,
                new SendBankInvoiceRequestDto
                {
                    Identity = identity,
                    BackUrl = request.BackUrl,
                    DocumentBaseId = request.OperationId,
                    Source = (InvoiceSource)request.SourceType,
                    PaymentOrder = ReplaceWrongSymbols(integrationPaymentOrder).Map()
                }
            ).ConfigureAwait(false);
            logger.Info(TAG, $"SendInvoiceAsync response: {response.ToJsonString()}", userContext.GetAuditContext());
            return response;
        }

        private static PaymentOrderDto ReplaceWrongSymbols(PaymentOrderDto paymentOrder)
        {
            paymentOrder.Payer.Name = paymentOrder.Payer.Name.ReplaceWrongSymbolsForExport().TrimEnd();
            paymentOrder.Recipient.Name = paymentOrder.Recipient.Name.ReplaceWrongSymbolsForExport();
            paymentOrder.Purpose = paymentOrder.Purpose.ReplaceWrongSymbolsForExport();
            
            return paymentOrder;
        }

        private async Task<SendPaymentOrdersContext> LoadSendPaymentOrdersContextAsync(List<OrderWithGuid> outgoingOperations, IUserContext userContext)
        {
            var operationsBySettlementAccounts = outgoingOperations.GroupBy(x => x.SettlementAccountId)
                .ToDictionary(x => x.Key, x => x.ToList());

            var operationsByGuid = operationsBySettlementAccounts.SelectMany(x => x.Value)
                .ToDictionary(x => x.Guid);

            var settlementAccountIds = operationsBySettlementAccounts.Keys;
            var settlementAccounts = (await settlementAccountClient.GetByIdsAsync(userContext.FirmId, userContext.UserId, settlementAccountIds).ConfigureAwait(false))
                .ToDictionary(x => x.Id);

            var bankIds = settlementAccounts.Values.Select(x => x.BankId).Distinct().ToList();
            var banks = (await banksApiClient.GetByIdsAsync(bankIds).ConfigureAwait(false)).ToDictionary(x => x.Id);

            var integrations = await GetIntegrationsAsync(userContext, settlementAccounts, banks).ConfigureAwait(false);

            return new SendPaymentOrdersContext
            {
                OperationsBySettlementAccounts = operationsBySettlementAccounts,
                OperationsByGuid = operationsByGuid,
                SettlementAccounts = settlementAccounts,
                Integrations = integrations,
                Banks = banks,
            };
        }
    }
}
