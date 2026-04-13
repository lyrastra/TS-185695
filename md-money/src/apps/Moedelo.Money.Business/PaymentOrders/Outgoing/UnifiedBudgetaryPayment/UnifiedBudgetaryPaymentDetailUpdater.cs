using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.UnifiedBudgetaryPayment;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.UnifiedBudgetaryPayment;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.TaxPostings;
using Moedelo.Money.Enums;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy;
using System.Threading.Tasks;
using System;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.UnifiedBudgetaryPayment
{
    [OperationType(OperationType.PaymentOrderOutgoingUnifiedBudgetaryPayment)]
    [InjectAsSingleton(typeof(IConcretePaymentDetailUpdater))]
    class UnifiedBudgetaryPaymentDetailUpdater :
        ConcretePaymentDetailUpdaterBase<UnifiedBudgetaryPaymentResponse, UnifiedBudgetaryPaymentSaveRequest, PaymentOrderSaveResponse>
    {
        public UnifiedBudgetaryPaymentDetailUpdater(
            IUnifiedBudgetaryPaymentReader reader,
            IUnifiedBudgetaryPaymentUpdater updater,
            IExecutionInfoContextAccessor contextAccessor, 
            ISettlementAccountApiClient settlementAccountApiClient)
             : base(reader, updater, contextAccessor, settlementAccountApiClient)
        {
        }

        protected override Func<UnifiedBudgetaryPaymentResponse, UnifiedBudgetaryPaymentSaveRequest> Mapper =>
            UnifiedBudgetaryPaymentMapper.MapToSaveRequest;

        protected override async Task DetailUpdateAsync(UnifiedBudgetaryPaymentSaveRequest saveRequest, ChangeIsPaidRequestItem item)
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
            
            foreach (var subPayment in saveRequest.SubPayments)
            {
                subPayment.TaxPostings = new TaxPostingsData();
            }
        }
    }
}
