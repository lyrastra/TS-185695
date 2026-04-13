using Moedelo.Common.Enums.Enums.Finances.Money;
using Moedelo.Common.Enums.Extensions.PostingEngine;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.Finances.Domain.Enums.Money;
using Moedelo.Finances.Domain.Interfaces.Business.Money.SourceReader;
using Moedelo.Finances.Domain.Interfaces.DataAccess.Money.Operations;
using Moedelo.Finances.Domain.Models.Money;
using Moedelo.Finances.Domain.Models.Money.SourceReader;
using Moedelo.Finances.Domain.Models.Money.Table;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.RequisitesV2.Client.SettlementAccounts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Finances.Business.Services.Money.Operations
{
    [InjectAsSingleton]
    public class CanSendToBankReader: IDI
    {
        private readonly IBankIntegrationDataReader integrationDataReader;
        private readonly IBankDataReader bankDataReader;
        private readonly ISettlementAccountClient settlementAccountClient;
        private readonly IPaymentOrderOperationDao paymentOrderOperationDao;

        public CanSendToBankReader(
            IBankIntegrationDataReader integrationDataReader,
            IBankDataReader bankDataReader,
            ISettlementAccountClient settlementAccountClient,
            IPaymentOrderOperationDao paymentOrderOperationDao)
        {
            this.integrationDataReader = integrationDataReader;
            this.bankDataReader = bankDataReader;
            this.settlementAccountClient = settlementAccountClient;
            this.paymentOrderOperationDao = paymentOrderOperationDao;
        }

        public async Task ApplyCanSendToBank(
            IUserContext userContext,
            IReadOnlyCollection<MoneyTableOperation> operations)
        {
            var outgoingOperations = operations
                .Where(x => x.Direction == MoneyDirection.Outgoing)
                .ToArray();
            var paymentOrders = await paymentOrderOperationDao
                .GetByBaseIdsAsync(
                    userContext.FirmId,
                    outgoingOperations.Select(x => x.DocumentBaseId).ToArray()
                ).ConfigureAwait(false);
            var settlementAccounts = await settlementAccountClient.GetByIdsAsync(
                    userContext.FirmId,
                    userContext.UserId,
                    paymentOrders
                        .Where(x => x.SettlementAccountId.HasValue)
                        .Select(x => x.SettlementAccountId.Value)
                        .Distinct()
                        .ToArray()
                ).ConfigureAwait(false);
            var sources = settlementAccounts
                .Select(x => new MoneySource
                {
                    Id = x.Id,
                    BankId = x.BankId,
                    Type = MoneySourceType.SettlementAccount,
                    Number = x.Number
                })
                .ToArray();
            var banks = await bankDataReader.GetBanksBySourcesAsync(userContext, sources).ConfigureAwait(false);
            foreach(var source in sources)
            {
                if (banks.TryGetValue(source.BankId.HasValue ? source.BankId.Value : 0, out SourceBankData bankData))
                    source.BankBik = bankData.Bik;
            }
            var banksIntegrationData = await integrationDataReader.GetBankIntegrationDataAsync(userContext.FirmId, userContext.UserId, sources, banks);

            foreach (var paymentOrder in paymentOrders)
            {
                if (!banksIntegrationData.TryGetValue(
                    paymentOrder.SettlementAccountId.HasValue ? paymentOrder.SettlementAccountId.Value : 0, 
                    out IntegrationData integrationData))
                {
                    continue;
                }

                var operation = outgoingOperations.First(x => x.DocumentBaseId == paymentOrder.DocumentBaseId);

                if (operation.OperationType.IsMemorialWarrant()
                || operation.OperationType.IsPurseOperation()
                || operation.OperationType.IsCurrencyOperation())
                {
                    continue;
                }

                operation.CanSendToBank = integrationData.HasActiveIntegration && integrationData.CanSendPaymentOrder;
            }
        }
    }
}
