using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.WithdrawalOfProfit;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.WithdrawalOfProfit;
using Moedelo.Money.Enums;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.WithdrawalOfProfit
{
    [OperationType(OperationType.PaymentOrderOutgoingWithdrawalOfProfit)]
    [InjectAsSingleton(typeof(IConcreetePaymentOrderActualizer))]
    class WithdrawalOfProfitActualizer :
        ConcreetePaymentOrderActualizerBase<WithdrawalOfProfitResponse, WithdrawalOfProfitSaveRequest, PaymentOrderSaveResponse>
    {

        public WithdrawalOfProfitActualizer(
            IClosedPeriodValidator closedPeriodValidator,
            IWithdrawalOfProfitReader reader,
            IWithdrawalOfProfitUpdater updater)
             : base(closedPeriodValidator, reader, updater)
        {
        }

        protected override Func<WithdrawalOfProfitResponse, WithdrawalOfProfitSaveRequest> Mapper =>
            WithdrawalOfProfitMapper.MapToSaveRequest;

        protected override Task ActualizeAsync(WithdrawalOfProfitSaveRequest saveRequest, DateTime actualizedDate)
        {
            saveRequest.Date = actualizedDate;
            saveRequest.IsPaid = true;
            return Task.CompletedTask;
        }
    }
}
