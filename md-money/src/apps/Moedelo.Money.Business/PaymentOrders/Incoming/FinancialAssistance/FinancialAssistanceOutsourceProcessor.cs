using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.FinancialAssistance;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Business.PaymentOrders.Outsource;
using Moedelo.Money.Domain.PaymentOrders.Incoming.FinancialAssistance;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.FinancialAssistance;

[InjectAsSingleton(typeof(IFinancialAssistanceOutsourceProcessor))]
internal class FinancialAssistanceOutsourceProcessor : PaymentOrderOutsourceProcessor<FinancialAssistanceSaveRequest>, IFinancialAssistanceOutsourceProcessor
{
    private readonly IFinancialAssistanceValidator validator;
    private readonly IFinancialAssistanceReader reader;
    private readonly IFinancialAssistanceUpdater updater;

    public FinancialAssistanceOutsourceProcessor(
        IFinancialAssistanceValidator validator,
        IFinancialAssistanceReader reader,
        IFinancialAssistanceUpdater updater,
        ILogger<FinancialAssistanceOutsourceProcessor> logger)
    : base(logger)
    {
        this.validator = validator;
        this.reader = reader;
        this.updater = updater;
    }

    public new Task<OutsourceConfirmResult> ConfirmAsync(FinancialAssistanceSaveRequest request)
    {
        return base.ConfirmAsync(request);
    }

    protected override async Task<FinancialAssistanceSaveRequest> MapToExistentAsync(FinancialAssistanceSaveRequest request)
    {
        var existent = await reader.GetByBaseIdAsync(request.DocumentBaseId);
        return MapToExistent(existent, request);
    }

    private static FinancialAssistanceSaveRequest MapToExistent(
        FinancialAssistanceResponse existent,
        FinancialAssistanceSaveRequest newValues)
    {
        var result = FinancialAssistanceMapper.MapToSaveRequest(existent);

        result.Kontragent = newValues.Kontragent;
        result.OperationState = newValues.OperationState;
        result.OutsourceState = newValues.OutsourceState;
        result.TaxPostings = newValues.TaxPostings;

        return result;
    }

    protected override Task UpdateAsync(FinancialAssistanceSaveRequest request) => updater.UpdateAsync(request);

    protected override Task ValidateAsync(FinancialAssistanceSaveRequest request) => validator.ValidateAsync(request);
}