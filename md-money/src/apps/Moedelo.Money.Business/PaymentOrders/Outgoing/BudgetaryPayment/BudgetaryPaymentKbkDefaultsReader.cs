using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.BudgetaryPayment;
using Moedelo.Money.Business.Kbks;
using Moedelo.Money.Domain.Operations;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.BudgetaryPayment;
using Moedelo.Money.Enums;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.BudgetaryPayment
{
    [InjectAsSingleton(typeof(IBudgetaryPaymentKbkDefaultsReader))]
    class BudgetaryPaymentKbkDefaultsReader : IBudgetaryPaymentKbkDefaultsReader
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly IKbkReader kbkReader;
        private readonly ITradingObjectApiClient tradingObjectApiClient;
        private readonly IFirmRequisitesApiClient firmRequisitesApiClient;
        private readonly BudgetaryRecipientRequisitesReader recipientRequisitesReader;

        public BudgetaryPaymentKbkDefaultsReader(
            IExecutionInfoContextAccessor contextAccessor,
            IKbkReader kbkReader,
            ITradingObjectApiClient tradingObjectApiClient,
            IFirmRequisitesApiClient firmRequisitesApiClient,
            BudgetaryRecipientRequisitesReader recipientRequisitesReader)
        {
            this.contextAccessor = contextAccessor;
            this.kbkReader = kbkReader;
            this.tradingObjectApiClient = tradingObjectApiClient;
            this.firmRequisitesApiClient = firmRequisitesApiClient;
            this.recipientRequisitesReader = recipientRequisitesReader;
        }

        public async Task<BudgetaryKbkDefaultsResponse> GetAsync(BudgetaryKbkDefaultsRequest request)
        {
            var firmRequisites = await GetFirmRequisitesAsync();

            var kbk = await GetKbkNumberAsync(request, firmRequisites.IsOoo);
            if (kbk == null)
            {
                return new BudgetaryKbkDefaultsResponse();
            }

            var tradingObject = await GetTradingObjectAsync(request);
            var fundsRequisites = await ResolveOrderDetailsForDefaultFieldsAsync(kbk.FundType, tradingObject);

            return BudgetaryPaymentKbkDefaultsMapper.Map(kbk, tradingObject, request.Period, fundsRequisites, firmRequisites);
        }

        private async Task<Kbk> GetKbkNumberAsync(BudgetaryKbkDefaultsRequest request, bool isOoo)
        {
            if (request.AccountCode == BudgetaryAccountCodes.OtherTaxes)
            {
                return new Kbk
                {
                    FundType = BudgetaryFundType.Fns,
                    Purpose = string.Empty
                };
            }
            return request.KbkId.HasValue
                ? await kbkReader.GetAsync(request.KbkId.Value, request.Period.GetEndDate(), request.Date, isOoo)
                : null;
        }

        private async Task<TradingObjectDto> GetTradingObjectAsync(BudgetaryKbkDefaultsRequest request)
        {
            var context = contextAccessor.ExecutionInfoContext;
            var isTradingFees = request.AccountCode == BudgetaryAccountCodes.TradingFees;
            return isTradingFees && request.TradingObjectId.HasValue
                ? await tradingObjectApiClient.GetByIdAsync(context.FirmId, context.UserId, request.TradingObjectId.Value)
                : null;
        }

        private Task<FirmRequisitesDto> GetFirmRequisitesAsync()
        {
            var context = contextAccessor.ExecutionInfoContext;
            return firmRequisitesApiClient.GetAsync(context.FirmId);
        }

        private Task<BudgetaryRecipient> ResolveOrderDetailsForDefaultFieldsAsync(BudgetaryFundType fundType, TradingObjectDto tradingObject)
        {
            return tradingObject != null
                ? recipientRequisitesReader.GetFnsRequisitesByCodeAndOktmoAsync(tradingObject.Ifns.ToString(), tradingObject.Oktmo)
                : recipientRequisitesReader.GetByFundTypeAsync(fundType);
        }
    }
}
