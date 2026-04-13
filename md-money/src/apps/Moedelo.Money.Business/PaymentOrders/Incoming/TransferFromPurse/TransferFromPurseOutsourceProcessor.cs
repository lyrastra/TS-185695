using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.TransferFromPurse;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Business.PaymentOrders.Outsource;
using Moedelo.Money.Domain.PaymentOrders.Incoming.TransferFromPurse;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.TransferFromPurse;

[InjectAsSingleton(typeof(ITransferFromPurseOutsourceProcessor))]
internal sealed class TransferFromPurseOutsourceProcessor : PaymentOrderOutsourceProcessor<TransferFromPurseSaveRequest>, ITransferFromPurseOutsourceProcessor
{
    private readonly ITransferFromPurseValidator validator;
    private readonly ITransferFromPurseReader reader;
    private readonly ITransferFromPurseUpdater updater;

    public TransferFromPurseOutsourceProcessor(
        ITransferFromPurseValidator validator,
        ITransferFromPurseReader reader,
        ITransferFromPurseUpdater updater,
        ILogger<TransferFromPurseOutsourceProcessor> logger) 
        : base(logger)
    {
        this.validator = validator;
        this.reader = reader;
        this.updater = updater;
    }

    public new Task<OutsourceConfirmResult> ConfirmAsync(TransferFromPurseSaveRequest request)
    {
        return base.ConfirmAsync(request);
    }

    protected override async Task<TransferFromPurseSaveRequest> MapToExistentAsync(TransferFromPurseSaveRequest request)
    {
        var existent = await reader.GetByBaseIdAsync(request.DocumentBaseId);
        return MapToExistent(existent, request);
    }

    private static TransferFromPurseSaveRequest MapToExistent(
        TransferFromPurseResponse existent,
        TransferFromPurseSaveRequest newValues)
    {
        var result = TransferFromPurseMapper.MapToSaveRequest(existent);

        result.OperationState = newValues.OperationState;
        result.OutsourceState = newValues.OutsourceState;

        return result;
    }

    protected override Task UpdateAsync(TransferFromPurseSaveRequest request) => updater.UpdateAsync(request);

    protected override Task ValidateAsync(TransferFromPurseSaveRequest request) => validator.ValidateAsync(request);
}