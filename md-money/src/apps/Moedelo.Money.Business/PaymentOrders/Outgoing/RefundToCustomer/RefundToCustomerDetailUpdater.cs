using System;
using System.Threading.Tasks;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.RefundToCustomer;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.RefundToCustomer;
using Moedelo.Money.Enums;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.RefundToCustomer
{
    [OperationType(OperationType.PaymentOrderOutgoingRefundToCustomer)]
    [InjectAsSingleton(typeof(IConcretePaymentDetailUpdater))]
    class RefundToCustomerDetailUpdater :
    ConcretePaymentDetailUpdaterBase<RefundToCustomerResponse, RefundToCustomerSaveRequest, PaymentOrderSaveResponse>
    {
        public RefundToCustomerDetailUpdater(
            IRefundToCustomerReader reader,
            IRefundToCustomerUpdater updater,
            IExecutionInfoContextAccessor contextAccessor, 
            ISettlementAccountApiClient settlementAccountApiClient)
            : base(reader, updater, contextAccessor, settlementAccountApiClient)
        {
        }
        
        protected override Func<RefundToCustomerResponse, RefundToCustomerSaveRequest> Mapper =>
            RefundToCustomerMapper.MapToSaveRequest;
        
        protected override async Task DetailUpdateAsync(RefundToCustomerSaveRequest saveRequest, ChangeIsPaidRequestItem item)
        {
            if (item.IsPaid.HasValue)
            {
                saveRequest.IsPaid = item.IsPaid.Value;
            }
            
            if (!string.IsNullOrEmpty(item.PaymentNumber))
            {
                saveRequest.Number = item.PaymentNumber;
            }
            
            if (!string.IsNullOrEmpty(item.PayerSettlementNumber))
            {
                var payerSettlementNumberId = await GetPayerSettlementNumberIdAsync(item.PayerSettlementNumber);
                
                if (payerSettlementNumberId.HasValue)
                {
                    saveRequest.SettlementAccountId = payerSettlementNumberId.Value;
                }
            }
        }
    }
}