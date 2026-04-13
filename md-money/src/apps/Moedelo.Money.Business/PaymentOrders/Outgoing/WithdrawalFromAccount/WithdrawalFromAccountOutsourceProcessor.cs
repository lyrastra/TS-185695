using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.WithdrawalFromAccount;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Business.PaymentOrders.Outsource;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.WithdrawalFromAccount;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.WithdrawalFromAccount;

[InjectAsSingleton]
internal sealed class WithdrawalFromAccountOutsourceProcessor : PaymentOrderOutsourceProcessor<WithdrawalFromAccountSaveRequest>, IWithdrawalFromAccountOutsourceProcessor
{
    private readonly IWithdrawalFromAccountValidator validator;
    private readonly IWithdrawalFromAccountReader reader;
    private readonly IWithdrawalFromAccountUpdater updater;

    public WithdrawalFromAccountOutsourceProcessor(
        IWithdrawalFromAccountValidator validator,
        IWithdrawalFromAccountReader reader,
        IWithdrawalFromAccountUpdater updater,
        ILogger<WithdrawalFromAccountOutsourceProcessor> logger) 
        : base(logger)
    {
        this.validator = validator;
        this.reader = reader;
        this.updater = updater;
    }

    public new Task<OutsourceConfirmResult> ConfirmAsync(WithdrawalFromAccountSaveRequest request)
    {
        return base.ConfirmAsync(request);
    }

    protected override async Task<WithdrawalFromAccountSaveRequest> MapToExistentAsync(WithdrawalFromAccountSaveRequest request)
    {
        var existent = await reader.GetByBaseIdAsync(request.DocumentBaseId);
        return MapToExistent(existent, request);
    }

    private static WithdrawalFromAccountSaveRequest MapToExistent(
        WithdrawalFromAccountResponse existent,
        WithdrawalFromAccountSaveRequest newValues)
    {
        var result = WithdrawalFromAccountMapper.MapToSaveRequest(existent);

        result.OperationState = newValues.OperationState;
        result.OutsourceState = newValues.OutsourceState;

        return result;
    }

    protected override Task UpdateAsync(WithdrawalFromAccountSaveRequest request) => updater.UpdateAsync(request);

    protected override Task ValidateAsync(WithdrawalFromAccountSaveRequest request) => validator.ValidateAsync(request);
}