using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.TransferFromAccount;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Business.PaymentOrders.Outsource;
using Moedelo.Money.Domain.PaymentOrders.Incoming.TransferFromAccount;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.TransferFromAccount;

[InjectAsSingleton(typeof(ITransferFromAccountOutsourceProcessor))]
internal sealed class TransferFromAccountOutsourceProcessor : PaymentOrderOutsourceProcessor<TransferFromAccountSaveRequest>, ITransferFromAccountOutsourceProcessor
{
    private readonly ITransferFromAccountValidator validator;
    private readonly ITransferFromAccountReader reader;
    private readonly ITransferFromAccountUpdater updater;

    public TransferFromAccountOutsourceProcessor(
        ITransferFromAccountValidator validator,
        ITransferFromAccountReader reader,
        ITransferFromAccountUpdater updater,
        ILogger<TransferFromAccountOutsourceProcessor> logger) 
        : base(logger)
    {
        this.validator = validator;
        this.reader = reader;
        this.updater = updater;
    }

    public new Task<OutsourceConfirmResult> ConfirmAsync(TransferFromAccountSaveRequest request)
    {
        return base.ConfirmAsync(request);
    }

    protected override async Task<TransferFromAccountSaveRequest> MapToExistentAsync(TransferFromAccountSaveRequest request)
    {
        var existent = await reader.GetByBaseIdAsync(request.DocumentBaseId);
        return MapToExistent(existent, request);
    }

    private static TransferFromAccountSaveRequest MapToExistent(
        TransferFromAccountResponse existent,
        TransferFromAccountSaveRequest newValues)
    {
        var result = TransferFromAccountMapper.MapToSaveRequest(existent);

        result.FromSettlementAccountId = newValues.FromSettlementAccountId;
        result.OperationState = newValues.OperationState;
        result.OutsourceState = newValues.OutsourceState;

        return result;
    }

    protected override Task UpdateAsync(TransferFromAccountSaveRequest request) => updater.UpdateAsync(request);

    protected override async Task ValidateAsync(TransferFromAccountSaveRequest request)
    {
        try
        {
            await validator.ValidateAsync(request);
        }
        catch (BusinessValidationException e)
        {
            // для данной операциии любая ошибка валидации (кроме закрытого периода) должна приводить в "черную таблицу"
            if (e.Reason != ValidationFailedReason.ClosedPeriod)
            {
                e.Reason = ValidationFailedReason.OperationTypeNotAllowed;
            }

            throw;
        }
    }
}