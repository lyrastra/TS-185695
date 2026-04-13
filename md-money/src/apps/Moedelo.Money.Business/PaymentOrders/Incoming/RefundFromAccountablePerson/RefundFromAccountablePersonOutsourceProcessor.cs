using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.RefundFromAccountablePerson;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Business.PaymentOrders.Outsource;
using Moedelo.Money.Domain.PaymentOrders.Incoming.RefundFromAccountablePerson;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.RefundFromAccountablePerson;

[InjectAsSingleton(typeof(IRefundFromAccountablePersonOutsourceProcessor))]
internal class RefundFromAccountablePersonOutsourceProcessor : PaymentOrderOutsourceProcessor<RefundFromAccountablePersonSaveRequest>, IRefundFromAccountablePersonOutsourceProcessor
{
    private readonly IRefundFromAccountablePersonValidator validator;
    private readonly IRefundFromAccountablePersonReader reader;
    private readonly IRefundFromAccountablePersonUpdater updater;

    public RefundFromAccountablePersonOutsourceProcessor(
        IRefundFromAccountablePersonValidator validator,
        IRefundFromAccountablePersonReader reader,
        IRefundFromAccountablePersonUpdater updater,
        ILogger<RefundFromAccountablePersonOutsourceProcessor> logger)
    : base(logger)
    {
        this.validator = validator;
        this.reader = reader;
        this.updater = updater;
    }

    public new Task<OutsourceConfirmResult> ConfirmAsync(RefundFromAccountablePersonSaveRequest request)
    {
        return base.ConfirmAsync(request);
    }

    protected override async Task<RefundFromAccountablePersonSaveRequest> MapToExistentAsync(RefundFromAccountablePersonSaveRequest request)
    {
        var existent = await reader.GetByBaseIdAsync(request.DocumentBaseId);
        return MapToExistent(existent, request);
    }

    private static RefundFromAccountablePersonSaveRequest MapToExistent(
        RefundFromAccountablePersonResponse existent,
        RefundFromAccountablePersonSaveRequest newValues)
    {
        var result = RefundFromAccountablePersonMapper.MapToSaveRequest(existent);

        result.Employee = newValues.Employee;
        result.OperationState = newValues.OperationState;
        result.OutsourceState = newValues.OutsourceState;

        return result;
    }

    protected override Task UpdateAsync(RefundFromAccountablePersonSaveRequest request) => updater.UpdateAsync(request);

    protected override Task ValidateAsync(RefundFromAccountablePersonSaveRequest request) => validator.ValidateAsync(request);
}