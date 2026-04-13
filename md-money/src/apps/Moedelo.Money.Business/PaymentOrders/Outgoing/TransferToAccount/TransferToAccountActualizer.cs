using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.TransferToAccount;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.TransferToAccount;
using Moedelo.Money.Enums;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.TransferToAccount
{
    [OperationType(OperationType.PaymentOrderOutgoingTransferToAccount)]
    [InjectAsSingleton(typeof(IConcreetePaymentOrderActualizer))]
    class TransferToAccountActualizer :
        ConcreetePaymentOrderActualizerBase<TransferToAccountResponse, TransferToAccountSaveRequest, PaymentOrderSaveResponse>
    {

        public TransferToAccountActualizer(
            IClosedPeriodValidator closedPeriodValidator,
            ITransferToAccountReader reader,
            ITransferToAccountUpdater updater)
             : base(closedPeriodValidator, reader, updater)
        {
        }

        protected override Func<TransferToAccountResponse, TransferToAccountSaveRequest> Mapper =>
            TransferToAccountMapper.MapToSaveRequest;

        protected override Task ActualizeAsync(TransferToAccountSaveRequest saveRequest, DateTime actualizedDate)
        {
            saveRequest.Date = actualizedDate;
            saveRequest.IsPaid = true;
            saveRequest.ProvideInAccounting = true;
            return Task.CompletedTask;
        }
    }
}
