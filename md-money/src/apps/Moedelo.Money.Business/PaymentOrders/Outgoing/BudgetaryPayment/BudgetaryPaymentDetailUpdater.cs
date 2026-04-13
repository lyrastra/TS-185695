using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.BudgetaryPayment;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.BudgetaryPayment;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.TaxPostings;
using Moedelo.Money.Enums;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy;
using System.Threading.Tasks;
using System;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.BudgetaryPayment
{
    [OperationType(OperationType.BudgetaryPayment)]
    [InjectAsSingleton(typeof(IConcretePaymentDetailUpdater))]
    class BudgetaryPaymentDetailUpdater :
        ConcretePaymentDetailUpdaterBase<BudgetaryPaymentResponse, BudgetaryPaymentSaveRequest, PaymentOrderSaveResponse>
    {
        public BudgetaryPaymentDetailUpdater(
            IBudgetaryPaymentReader reader,
            IBudgetaryPaymentUpdater updater,
            IExecutionInfoContextAccessor contextAccessor, 
            ISettlementAccountApiClient settlementAccountApiClient)
             : base(reader, updater, contextAccessor, settlementAccountApiClient)
        {
        }

        protected override Func<BudgetaryPaymentResponse, BudgetaryPaymentSaveRequest> Mapper =>
            BudgetaryPaymentMapper.MapToSaveRequest;

        protected override async Task DetailUpdateAsync(BudgetaryPaymentSaveRequest saveRequest, ChangeIsPaidRequestItem item)
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
            
            saveRequest.TaxPostings = new TaxPostingsData();
        }
    }
}
