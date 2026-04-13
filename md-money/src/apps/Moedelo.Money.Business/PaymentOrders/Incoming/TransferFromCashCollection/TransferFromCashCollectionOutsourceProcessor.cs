using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.TransferFromCashCollection;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Business.PaymentOrders.Outsource;
using Moedelo.Money.Domain.PaymentOrders.Incoming.TransferFromCashCollection;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.TransferFromCashCollection;

[InjectAsSingleton(typeof(ITransferFromCashCollectionOutsourceProcessor))]
internal class TransferFromCashCollectionOutsourceProcessor : PaymentOrderOutsourceProcessor<TransferFromCashCollectionSaveRequest>, ITransferFromCashCollectionOutsourceProcessor
{
    private readonly ITransferFromCashCollectionValidator validator;
    private readonly ITransferFromCashCollectionReader reader;
    private readonly ITransferFromCashCollectionUpdater updater;

    public TransferFromCashCollectionOutsourceProcessor(
        ITransferFromCashCollectionValidator validator,
        ITransferFromCashCollectionReader reader,
        ITransferFromCashCollectionUpdater updater,
        ILogger<TransferFromCashCollectionOutsourceProcessor> logger)
    : base(logger)
    {
        this.validator = validator;
        this.reader = reader;
        this.updater = updater;
    }

    public new Task<OutsourceConfirmResult> ConfirmAsync(TransferFromCashCollectionSaveRequest request)
    {
        return base.ConfirmAsync(request);
    }

    protected override async Task<TransferFromCashCollectionSaveRequest> MapToExistentAsync(TransferFromCashCollectionSaveRequest request)
    {
        var existent = await reader.GetByBaseIdAsync(request.DocumentBaseId);
        return MapToExistent(existent, request);
    }

    private static TransferFromCashCollectionSaveRequest MapToExistent(
        TransferFromCashCollectionResponse existent,
        TransferFromCashCollectionSaveRequest newValues)
    {
        var result = TransferFromCashCollectionMapper.MapToSaveRequest(existent);

        result.OperationState = newValues.OperationState;
        result.OutsourceState = newValues.OutsourceState;

        return result;
    }

    protected override Task UpdateAsync(TransferFromCashCollectionSaveRequest request) => updater.UpdateAsync(request);

    protected override Task ValidateAsync(TransferFromCashCollectionSaveRequest request) => validator.ValidateAsync(request);
}