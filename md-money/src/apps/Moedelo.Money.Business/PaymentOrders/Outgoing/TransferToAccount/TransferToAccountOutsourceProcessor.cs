using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.TransferToAccount;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Business.PaymentOrders.Outsource;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.TransferToAccount;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.TransferToAccount;

[InjectAsSingleton]
internal sealed class TransferToAccountOutsourceProcessor : PaymentOrderOutsourceProcessor<TransferToAccountSaveRequest>, ITransferToAccountOutsourceProcessor
{
    private readonly ITransferToAccountValidator validator;
    private readonly ITransferToAccountReader reader;
    private readonly ITransferToAccountUpdater updater;

    public TransferToAccountOutsourceProcessor(
        ITransferToAccountValidator validator,
        ITransferToAccountReader reader,
        ITransferToAccountUpdater updater,
        ILogger<TransferToAccountOutsourceProcessor> logger) 
        : base(logger)
    {
        this.validator = validator;
        this.reader = reader;
        this.updater = updater;
    }

    public new Task<OutsourceConfirmResult> ConfirmAsync(TransferToAccountSaveRequest request)
    {
        return base.ConfirmAsync(request);
    }

    protected override async Task<TransferToAccountSaveRequest> MapToExistentAsync(TransferToAccountSaveRequest request)
    {
        var existent = await reader.GetByBaseIdAsync(request.DocumentBaseId);
        return MapToExistent(existent, request);
    }

    private static TransferToAccountSaveRequest MapToExistent(
        TransferToAccountResponse existent,
        TransferToAccountSaveRequest newValues)
    {
        var result = TransferToAccountMapper.MapToSaveRequest(existent);

        result.IsPaid = newValues.IsPaid;
        result.OperationState = newValues.OperationState;
        result.OutsourceState = newValues.OutsourceState;

        return result;
    }

    protected override Task UpdateAsync(TransferToAccountSaveRequest request) => updater.UpdateAsync(request);

    protected override Task ValidateAsync(TransferToAccountSaveRequest request) => validator.ValidateAsync(request);
}