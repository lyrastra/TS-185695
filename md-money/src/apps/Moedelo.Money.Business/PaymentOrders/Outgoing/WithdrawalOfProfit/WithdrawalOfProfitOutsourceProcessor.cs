using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.WithdrawalOfProfit;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Business.PaymentOrders.Outsource;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.WithdrawalOfProfit;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.WithdrawalOfProfit;

[InjectAsSingleton]
internal sealed class WithdrawalOfProfitOutsourceProcessor : PaymentOrderOutsourceProcessor<WithdrawalOfProfitSaveRequest>, IWithdrawalOfProfitOutsourceProcessor
{
    private readonly IWithdrawalOfProfitValidator validator;
    private readonly IWithdrawalOfProfitReader reader;
    private readonly IWithdrawalOfProfitUpdater updater;

    public WithdrawalOfProfitOutsourceProcessor(
        IWithdrawalOfProfitValidator validator,
        IWithdrawalOfProfitReader reader,
        IWithdrawalOfProfitUpdater updater,
        ILogger<WithdrawalOfProfitOutsourceProcessor> logger) 
        : base(logger)
    {
        this.validator = validator;
        this.reader = reader;
        this.updater = updater;
    }

    public new Task<OutsourceConfirmResult> ConfirmAsync(WithdrawalOfProfitSaveRequest request)
    {
        return base.ConfirmAsync(request);
    }

    protected override async Task<WithdrawalOfProfitSaveRequest> MapToExistentAsync(WithdrawalOfProfitSaveRequest request)
    {
        var existent = await reader.GetByBaseIdAsync(request.DocumentBaseId);
        return MapToExistent(existent, request);
    }

    private static WithdrawalOfProfitSaveRequest MapToExistent(
        WithdrawalOfProfitResponse existent,
        WithdrawalOfProfitSaveRequest newValues)
    {
        var result = WithdrawalOfProfitMapper.MapToSaveRequest(existent);

        result.OperationState = newValues.OperationState;
        result.OutsourceState = newValues.OutsourceState;
        result.Kontragent = newValues.Kontragent;

        return result;
    }

    protected override Task UpdateAsync(WithdrawalOfProfitSaveRequest request) => updater.UpdateAsync(request);

    protected override Task ValidateAsync(WithdrawalOfProfitSaveRequest request) => validator.ValidateAsync(request);
}