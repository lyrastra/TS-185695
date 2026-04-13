using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.CurrencyTransferToAccount;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Business.PaymentOrders.Outsource;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencyTransferToAccount;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.CurrencyTransferToAccount;

[InjectAsSingleton]
internal sealed class CurrencyTransferToAccountOutsourceProcessor : PaymentOrderOutsourceProcessor<CurrencyTransferToAccountSaveRequest>, ICurrencyTransferToAccountOutsourceProcessor
{
    private readonly ICurrencyTransferToAccountValidator validator;
    private readonly ICurrencyTransferToAccountReader reader;
    private readonly ICurrencyTransferToAccountUpdater updater;

    public CurrencyTransferToAccountOutsourceProcessor(
        ICurrencyTransferToAccountValidator validator,
        ICurrencyTransferToAccountReader reader,
        ICurrencyTransferToAccountUpdater updater,
        ILogger<CurrencyTransferToAccountOutsourceProcessor> logger) 
        : base(logger)
    {
        this.validator = validator;
        this.reader = reader;
        this.updater = updater;
    }

    public new Task<OutsourceConfirmResult> ConfirmAsync(CurrencyTransferToAccountSaveRequest request)
    {
        return base.ConfirmAsync(request);
    }

    protected override async Task<CurrencyTransferToAccountSaveRequest> MapToExistentAsync(CurrencyTransferToAccountSaveRequest request)
    {
        var existent = await reader.GetByBaseIdAsync(request.DocumentBaseId);
        return MapToExistent(existent, request);
    }

    private static CurrencyTransferToAccountSaveRequest MapToExistent(
        CurrencyTransferToAccountResponse existent,
        CurrencyTransferToAccountSaveRequest newValues)
    {
        var result = CurrencyTransferToAccountMapper.MapToSaveRequest(existent);

        result.OperationState = newValues.OperationState;
        result.OutsourceState = newValues.OutsourceState;
        result.ToSettlementAccountId = newValues.ToSettlementAccountId;

        return result;
    }

    protected override Task UpdateAsync(CurrencyTransferToAccountSaveRequest request) => updater.UpdateAsync(request);

    protected override Task ValidateAsync(CurrencyTransferToAccountSaveRequest request) => validator.ValidateAsync(request);
}