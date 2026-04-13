using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Enums;
using Moedelo.Money.PaymentOrders.Business.Abstractions;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Incoming.TransferFromAccount;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Models;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.PaymentOrders.Business.PaymentOrders.Incoming.TransferFromAccount
{
    [InjectAsSingleton(typeof(ITransferFromAccountReader))]

    class TransferFromAccountReader : ITransferFromAccountReader
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly IPaymentOrderReader paymentOrderReader;
        private readonly ISettlementAccountApiClient settlementAccountApiClient;

        public TransferFromAccountReader(
            IExecutionInfoContextAccessor contextAccessor,
            IPaymentOrderReader paymentOrderReader,
            ISettlementAccountApiClient settlementAccountApiClient)
        {
            this.contextAccessor = contextAccessor;
            this.paymentOrderReader = paymentOrderReader;
            this.settlementAccountApiClient = settlementAccountApiClient;
        }

        public async Task<PaymentOrderResponse> GetByBaseIdAsync(long documentBaseId)
        {
            var response = await paymentOrderReader.GetByBaseIdAsync(documentBaseId, OperationType.PaymentOrderIncomingTransferFromAccount);
            await LoadTransferSettlmentAccountIdAsync(response);
            return response;
        }

        private async Task LoadTransferSettlmentAccountIdAsync(PaymentOrderResponse response)
        {
            if (response.PaymentOrder.TransferSettlementAccountId.HasValue)
            {
                return;
            }
            if (string.IsNullOrEmpty(response.PaymentOrderSnapshot.Payer.SettlementNumber))
            {
                return;
            }
            var context = contextAccessor.ExecutionInfoContext;
            var settlementAccounts = await settlementAccountApiClient.GetByNumbersAsync(context.FirmId, context.UserId, new[] { response.PaymentOrderSnapshot.Payer.SettlementNumber });
            var settlementAccount = settlementAccounts.FirstOrDefault();
            if (settlementAccount == null)
            {
                return;
            }
            response.PaymentOrder.TransferSettlementAccountId = settlementAccount.Id;
        }
    }
}
