using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.Other;
using Moedelo.Money.Business.SettlementAccounts;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Domain.AccPostings;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.Other;
using Moedelo.Money.Domain.TaxPostings;
using Moedelo.Money.Enums;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.Other
{
    [OperationType(OperationType.PaymentOrderOutgoingOther)]
    [InjectAsSingleton(typeof(IConcreetePaymentOrderActualizer))]
    class OtherOutgoingActualizer :
        ConcreetePaymentOrderActualizerBase<OtherOutgoingResponse, OtherOutgoingSaveRequest, PaymentOrderSaveResponse>
    {
        private const int OtherIncomeAndOutgoSubcontoId = 1;
        private const int OtherIncomeAndOutgoDebitCode = 910201;

        private readonly ISettlementAccountsReader settlementAccountsReader;

        public OtherOutgoingActualizer(
            IClosedPeriodValidator closedPeriodValidator,
            IOtherOutgoingReader reader,
            IOtherOutgoingUpdater updater,
            ISettlementAccountsReader settlementAccountsReader)
             : base(closedPeriodValidator, reader, updater)
        {
            this.settlementAccountsReader = settlementAccountsReader;
        }

        protected override Func<OtherOutgoingResponse, OtherOutgoingSaveRequest> Mapper =>
            OtherOutgoingMapper.MapToSaveRequest;

        protected override async Task ActualizeAsync(OtherOutgoingSaveRequest saveRequest, DateTime actualizedDate)
        {
            saveRequest.Date = actualizedDate;
            saveRequest.IsPaid = true;
            saveRequest.ProvideInAccounting = true;
            saveRequest.TaxPostings = new TaxPostingsData();

            var settlementAccount = await settlementAccountsReader.GetByIdAsync(saveRequest.SettlementAccountId);

            saveRequest.AccPosting = new OtherOutgoingCustomAccPosting
            {
                Date = actualizedDate,
                Sum = saveRequest.Sum,
                CreditSubconto = settlementAccount.SubcontoId.Value,
                DebitCode = OtherIncomeAndOutgoDebitCode,
                DebitSubconto = new[]
                {
                    new Subconto { Id = OtherIncomeAndOutgoSubcontoId }
                }
            };
        }
    }
}
