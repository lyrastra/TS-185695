using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace Moedelo.Money.Business.PaymentOrders
{
    abstract class ConcretePaymentDetailUpdaterBase<TReadResponse, TSaveRequest, TSaveResponse> :
        IConcretePaymentDetailUpdater,
        IPaymentOrderReadResponseToSaveRequestMapper<TReadResponse, TSaveRequest>
        where TReadResponse : IActualizableReadResponse
        where TSaveRequest : IActualizableSaveRequest
    {
        private readonly IPaymentOrderReader<TReadResponse> reader;
        private readonly IPaymentOrderUpdater<TSaveRequest, TSaveResponse> updater;
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly ISettlementAccountApiClient settlementAccountApiClient;
        
        protected ConcretePaymentDetailUpdaterBase(
            IPaymentOrderReader<TReadResponse> reader,
            IPaymentOrderUpdater<TSaveRequest, TSaveResponse> updater,
            IExecutionInfoContextAccessor contextAccessor,
            ISettlementAccountApiClient settlementAccountApiClient)
        {
            this.reader = reader;
            this.updater = updater;
            this.contextAccessor = contextAccessor;
            this.settlementAccountApiClient = settlementAccountApiClient;
        }
        
        public virtual async Task UpdateAsync(ChangeIsPaidRequestItem item)
        {
            var response = await reader.GetByBaseIdAsync(item.DocumentBaseId);
            var saveRequest = Mapper(response);
            await DetailUpdateAsync(saveRequest, item);
            await updater.UpdateAsync(saveRequest);
        }

        protected async Task<int?> GetPayerSettlementNumberIdAsync(string payerSettlementNumber)
        {
            var context = contextAccessor.ExecutionInfoContext;
            var settlementAccounts = await settlementAccountApiClient.GetByNumbersAsync(context.FirmId, context.UserId, new[] { payerSettlementNumber });
            return settlementAccounts?.FirstOrDefault()?.Id;
        }

        protected abstract Func<TReadResponse, TSaveRequest> Mapper { get; }

        Func<TReadResponse, TSaveRequest> IPaymentOrderReadResponseToSaveRequestMapper<TReadResponse, TSaveRequest>.Mapper => Mapper;

        protected abstract Task DetailUpdateAsync(TSaveRequest saveRequest, ChangeIsPaidRequestItem item);
    }
}