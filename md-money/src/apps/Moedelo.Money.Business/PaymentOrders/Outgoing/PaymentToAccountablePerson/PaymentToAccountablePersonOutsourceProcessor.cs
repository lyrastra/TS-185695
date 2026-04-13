using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.PaymentToAccountablePerson;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Business.PaymentOrders.Outsource;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.PaymentToAccountablePerson;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.PaymentToAccountablePerson;

[InjectAsSingleton(typeof(IPaymentToAccountablePersonOutsourceProcessor))]
internal class PaymentToAccountablePersonOutsourceProcessor : PaymentOrderOutsourceProcessor<PaymentToAccountablePersonSaveRequest>, IPaymentToAccountablePersonOutsourceProcessor
{
    private readonly IPaymentToAccountablePersonValidator validator;
    private readonly IPaymentToAccountablePersonReader reader;
    private readonly IPaymentToAccountablePersonUpdater updater;

    public PaymentToAccountablePersonOutsourceProcessor(
        IPaymentToAccountablePersonValidator validator,
        IPaymentToAccountablePersonReader reader,
        IPaymentToAccountablePersonUpdater updater,
        ILogger<PaymentToAccountablePersonOutsourceProcessor> logger) 
        : base(logger)
    {
        this.validator = validator;
        this.reader = reader;
        this.updater = updater;
    }

    public new Task<OutsourceConfirmResult> ConfirmAsync(PaymentToAccountablePersonSaveRequest request)
    {
        return base.ConfirmAsync(request);
    }

    protected override async Task<PaymentToAccountablePersonSaveRequest> MapToExistentAsync(PaymentToAccountablePersonSaveRequest request)
    {
        var existent = await reader.GetByBaseIdAsync(request.DocumentBaseId);
        return MapToExistent(existent, request);
    }

    protected override Task UpdateAsync(PaymentToAccountablePersonSaveRequest request) => updater.UpdateAsync(request);

    protected override Task ValidateAsync(PaymentToAccountablePersonSaveRequest request) => validator.ValidateUpdateRequestAsync(request);
    
    private static PaymentToAccountablePersonSaveRequest MapToExistent(
        PaymentToAccountablePersonResponse existent,
        PaymentToAccountablePersonSaveRequest newValues)
    {
        var result = PaymentToAccountablePersonMapper.MapToSaveRequest(existent);

        result.Employee = newValues.Employee;
        result.OperationState = newValues.OperationState;
        result.OutsourceState = newValues.OutsourceState;

        return result;
    }
}