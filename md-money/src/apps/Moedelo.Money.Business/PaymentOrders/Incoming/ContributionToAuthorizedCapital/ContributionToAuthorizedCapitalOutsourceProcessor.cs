using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.ContributionToAuthorizedCapital;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Business.PaymentOrders.Outsource;
using Moedelo.Money.Domain.PaymentOrders.Incoming.ContributionToAuthorizedCapital;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.ContributionToAuthorizedCapital;

[InjectAsSingleton(typeof(IContributionToAuthorizedCapitalOutsourceProcessor))]
internal class ContributionToAuthorizedCapitalOutsourceProcessor : PaymentOrderOutsourceProcessor<ContributionToAuthorizedCapitalSaveRequest>, IContributionToAuthorizedCapitalOutsourceProcessor
{
    private readonly IContributionToAuthorizedCapitalValidator validator;
    private readonly IContributionToAuthorizedCapitalReader reader;
    private readonly IContributionToAuthorizedCapitalUpdater updater;

    public ContributionToAuthorizedCapitalOutsourceProcessor(
        IContributionToAuthorizedCapitalValidator validator,
        IContributionToAuthorizedCapitalReader reader,
        IContributionToAuthorizedCapitalUpdater updater,
        ILogger<ContributionToAuthorizedCapitalOutsourceProcessor> logger)
        : base(logger)
    {
        this.validator = validator;
        this.reader = reader;
        this.updater = updater;
    }

    public new Task<OutsourceConfirmResult> ConfirmAsync(ContributionToAuthorizedCapitalSaveRequest request)
    {
        return base.ConfirmAsync(request);
    }

    protected override async Task<ContributionToAuthorizedCapitalSaveRequest> MapToExistentAsync(ContributionToAuthorizedCapitalSaveRequest request)
    {
        var existent = await reader.GetByBaseIdAsync(request.DocumentBaseId);
        return MapToExistent(existent, request);
    }

    private static ContributionToAuthorizedCapitalSaveRequest MapToExistent(
        ContributionToAuthorizedCapitalResponse existent,
        ContributionToAuthorizedCapitalSaveRequest newValues)
    {
        var result = ContributionToAuthorizedCapitalMapper.MapToSaveRequest(existent);

        result.OperationState = newValues.OperationState;
        result.OutsourceState = newValues.OutsourceState;
        result.Kontragent = newValues.Kontragent;

        return result;
    }

    protected override Task UpdateAsync(ContributionToAuthorizedCapitalSaveRequest request) => updater.UpdateAsync(request);

    protected override Task ValidateAsync(ContributionToAuthorizedCapitalSaveRequest request) => validator.ValidateAsync(request);
}