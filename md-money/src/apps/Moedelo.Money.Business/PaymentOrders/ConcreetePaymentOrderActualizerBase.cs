using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Enums;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace Moedelo.Money.Business.PaymentOrders
{
    abstract class ConcreetePaymentOrderActualizerBase<TReadResponse, TSaveRequest, TSaveResponse> :
        IConcreetePaymentOrderActualizer,
        IPaymentOrderReadResponseToSaveRequestMapper<TReadResponse, TSaveRequest>
        where TReadResponse : IActualizableReadResponse
        where TSaveRequest : IActualizableSaveRequest
    {
        private readonly IClosedPeriodValidator closedPeriodValidator;
        private readonly IPaymentOrderReader<TReadResponse> reader;
        private readonly IPaymentOrderUpdater<TSaveRequest, TSaveResponse> updater;

        public ConcreetePaymentOrderActualizerBase(
            IClosedPeriodValidator closedPeriodValidator,
            IPaymentOrderReader<TReadResponse> reader,
            IPaymentOrderUpdater<TSaveRequest, TSaveResponse> updater)
        {
            this.closedPeriodValidator = closedPeriodValidator;
            this.reader = reader;
            this.updater = updater;
        }

        protected abstract Func<TReadResponse, TSaveRequest> Mapper { get; }

        Func<TReadResponse, TSaveRequest> IPaymentOrderReadResponseToSaveRequestMapper<TReadResponse, TSaveRequest>.Mapper => Mapper;

        protected abstract Task ActualizeAsync(TSaveRequest saveRequest, DateTime actualizeDate);

        public async Task ActualizeAsync(ActualizeRequestItem request)
        {
            var operationStatesForActualize = new[] { OperationState.Default, OperationState.OutsourceApproved };

            var response = await reader.GetByBaseIdAsync(request.DocumentBaseId);
            if (response.IsPaid || !operationStatesForActualize.Contains(response.OperationState))
            {
                return;
            }
            await closedPeriodValidator.ValidateAsync(response.Date);
            await closedPeriodValidator.ValidateAsync(request.Date);
            var saveRequest = Mapper(response);
            await ActualizeAsync(saveRequest, request.Date);

            saveRequest.OperationState = request.IsOutsourceApproved
                ? OperationState.OutsourceApproved
                : saveRequest.OperationState;
            
            await updater.UpdateAsync(saveRequest);
        }
    }
}