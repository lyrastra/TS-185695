using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.CurrencyTransferFromAccount;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Business.PaymentOrders.Outsource;
using Moedelo.Money.Domain.PaymentOrders.Incoming.CurrencyTransferFromAccount;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.CurrencyTransferFromAccount;

[InjectAsSingleton(typeof(ICurrencyTransferFromAccountOutsourceProcessor))]
internal class CurrencyTransferFromAccountOutsourceProcessor : PaymentOrderOutsourceProcessor<CurrencyTransferFromAccountSaveRequest>, ICurrencyTransferFromAccountOutsourceProcessor
{
    private readonly ICurrencyTransferFromAccountValidator validator;
    private readonly ICurrencyTransferFromAccountReader reader;
    private readonly ICurrencyTransferFromAccountUpdater updater;

    public CurrencyTransferFromAccountOutsourceProcessor(
        ICurrencyTransferFromAccountValidator validator,
        ICurrencyTransferFromAccountReader reader,
        ICurrencyTransferFromAccountUpdater updater,
        ILogger<CurrencyTransferFromAccountOutsourceProcessor> logger) 
        : base(logger)
    {
        this.validator = validator;
        this.reader = reader;
        this.updater = updater;
    }

    public new Task<OutsourceConfirmResult> ConfirmAsync(CurrencyTransferFromAccountSaveRequest request)
    {
        return base.ConfirmAsync(request);
    }

    protected override async Task<CurrencyTransferFromAccountSaveRequest> MapToExistentAsync(CurrencyTransferFromAccountSaveRequest request)
    {
        var existent = await reader.GetByBaseIdAsync(request.DocumentBaseId);
        return MapToExistent(existent, request);
    }

    private static CurrencyTransferFromAccountSaveRequest MapToExistent(
        CurrencyTransferFromAccountResponse existent,
        CurrencyTransferFromAccountSaveRequest newValues)
    {
        var result = CurrencyTransferFromAccountMapper.MapToSaveRequest(existent);

        result.ProvideInAccounting = newValues.ProvideInAccounting;
        result.OperationState = newValues.OperationState;
        result.OutsourceState = newValues.OutsourceState;

        return result;
    }

    protected override Task UpdateAsync(CurrencyTransferFromAccountSaveRequest request) => updater.UpdateAsync(request);

    protected override Task ValidateAsync(CurrencyTransferFromAccountSaveRequest request) => validator.ValidateAsync(request);
}