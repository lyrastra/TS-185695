using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.AgencyContract;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.AgencyContract;
using Moedelo.Money.Enums;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.AgencyContract
{
    [OperationType(OperationType.PaymentOrderOutgoingAgencyContract)]
    [InjectAsSingleton(typeof(IConcreetePaymentOrderActualizer))]
    class AgencyContractActualizer :
        ConcreetePaymentOrderActualizerBase<AgencyContractResponse, AgencyContractSaveRequest, PaymentOrderSaveResponse>
    {

        public AgencyContractActualizer(
            IClosedPeriodValidator closedPeriodValidator,
            IAgencyContractReader reader,
            IAgencyContractUpdater updater)
             : base(closedPeriodValidator, reader, updater)
        {
        }

        protected override Func<AgencyContractResponse, AgencyContractSaveRequest> Mapper =>
            AgencyContractMapper.MapToSaveRequest;

        protected override Task ActualizeAsync(AgencyContractSaveRequest saveRequest, DateTime actualizedDate)
        {
            saveRequest.Date = actualizedDate;
            saveRequest.IsPaid = true;
            saveRequest.ProvideInAccounting = true;
            return Task.CompletedTask;
        }
    }
}
