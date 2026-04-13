using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.ContributionOfOwnFunds;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Business.PaymentOrders.Outsource;
using Moedelo.Money.Domain.PaymentOrders.Incoming.ContributionOfOwnFunds;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.ContributionOfOwnFunds;

[InjectAsSingleton(typeof(IContributionOfOwnFundsOutsourceProcessor))]
internal class ContributionOfOwnFundsOutsourceProcessor : PaymentOrderOutsourceProcessor<ContributionOfOwnFundsSaveRequest>, IContributionOfOwnFundsOutsourceProcessor
{
    private readonly IContributionOfOwnFundsValidator validator;
    private readonly IContributionOfOwnFundsReader reader;
    private readonly IContributionOfOwnFundsUpdater updater;

    public ContributionOfOwnFundsOutsourceProcessor(
        IContributionOfOwnFundsValidator validator,
        IContributionOfOwnFundsReader reader,
        IContributionOfOwnFundsUpdater updater,
        ILogger<ContributionOfOwnFundsOutsourceProcessor> logger)
    : base(logger)
    {
        this.validator = validator;
        this.reader = reader;
        this.updater = updater;
    }

    public new Task<OutsourceConfirmResult> ConfirmAsync(ContributionOfOwnFundsSaveRequest request)
    {
        return base.ConfirmAsync(request);
    }

    protected override async Task<ContributionOfOwnFundsSaveRequest> MapToExistentAsync(ContributionOfOwnFundsSaveRequest request)
    {
        var existent = await reader.GetByBaseIdAsync(request.DocumentBaseId);
        return MapToExistent(existent, request);
    }

    private static ContributionOfOwnFundsSaveRequest MapToExistent(
        ContributionOfOwnFundsResponse existent,
        ContributionOfOwnFundsSaveRequest newValues)
    {
        var result = ContributionOfOwnFundsMapper.MapToSaveRequest(existent);

        result.OperationState = newValues.OperationState;
        result.OutsourceState = newValues.OutsourceState;

        return result;
    }

    protected override Task UpdateAsync(ContributionOfOwnFundsSaveRequest request) => updater.UpdateAsync(request);

    protected override Task ValidateAsync(ContributionOfOwnFundsSaveRequest request) => validator.ValidateAsync(request);
}