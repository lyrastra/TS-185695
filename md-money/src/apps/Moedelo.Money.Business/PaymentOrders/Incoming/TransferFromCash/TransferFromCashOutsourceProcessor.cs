using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.TransferFromCash;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Business.PaymentOrders.Outsource;
using Moedelo.Money.Domain.PaymentOrders.Incoming.TransferFromCash;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.TransferFromCash;

[InjectAsSingleton(typeof(ITransferFromCashOutsourceProcessor))]
internal sealed class TransferFromCashOutsourceProcessor : PaymentOrderOutsourceProcessor<TransferFromCashSaveRequest>, ITransferFromCashOutsourceProcessor
{
    private readonly ITransferFromCashValidator validator;
    private readonly ITransferFromCashReader reader;
    private readonly ITransferFromCashUpdater updater;

    public TransferFromCashOutsourceProcessor(
        ITransferFromCashValidator validator,
        ITransferFromCashReader reader,
        ITransferFromCashUpdater updater,
        ILogger<TransferFromCashOutsourceProcessor> logger) 
        : base(logger)
    {
        this.validator = validator;
        this.reader = reader;
        this.updater = updater;
    }

    public new Task<OutsourceConfirmResult> ConfirmAsync(TransferFromCashSaveRequest request)
    {
        return base.ConfirmAsync(request);
    }

    protected override async Task<TransferFromCashSaveRequest> MapToExistentAsync(TransferFromCashSaveRequest request)
    {
        var existent = await reader.GetByBaseIdAsync(request.DocumentBaseId);
        return MapToExistent(existent, request);
    }

    private static TransferFromCashSaveRequest MapToExistent(
        TransferFromCashResponse existent,
        TransferFromCashSaveRequest newValues)
    {
        var result = TransferFromCashMapper.MapToSaveRequest(existent);

        result.OperationState = newValues.OperationState;
        result.OutsourceState = newValues.OutsourceState;

        return result;
    }

    protected override Task UpdateAsync(TransferFromCashSaveRequest request) => updater.UpdateAsync(request);

    protected override Task ValidateAsync(TransferFromCashSaveRequest request) => validator.ValidateAsync(request);
}