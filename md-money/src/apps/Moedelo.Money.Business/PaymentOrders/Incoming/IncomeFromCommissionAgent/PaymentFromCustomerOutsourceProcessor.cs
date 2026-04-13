using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.IncomeFromCommissionAgent;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Business.CommissionAgents;
using Moedelo.Money.Business.PaymentOrders.Outsource;
using Moedelo.Money.Domain.PaymentOrders.Incoming.IncomeFromCommissionAgent;
using Moedelo.Money.Enums.Outsource;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.IncomeFromCommissionAgent;

[InjectAsSingleton(typeof(IIncomeFromCommissionAgentOutsourceProcessor))]
internal sealed class IncomeFromCommissionAgentOutsourceProcessor : PaymentOrderOutsourceProcessor<IncomeFromCommissionAgentSaveRequest>, IIncomeFromCommissionAgentOutsourceProcessor
{
    private readonly IIncomeFromCommissionAgentValidator validator;
    private readonly IIncomeFromCommissionAgentReader reader;
    private readonly IIncomeFromCommissionAgentUpdater updater;
    private readonly CommissionAgentsReader commissionAgentsReader;

    public IncomeFromCommissionAgentOutsourceProcessor(
        IIncomeFromCommissionAgentValidator validator,
        IIncomeFromCommissionAgentReader reader,
        IIncomeFromCommissionAgentUpdater updater,
        ILogger<IncomeFromCommissionAgentOutsourceProcessor> logger,
        CommissionAgentsReader commissionAgentsReader) 
        : base(logger)
    {
        this.validator = validator;
        this.reader = reader;
        this.updater = updater;
        this.commissionAgentsReader = commissionAgentsReader;
    }

    public new async Task<OutsourceConfirmResult> ConfirmAsync(IncomeFromCommissionAgentSaveRequest request)
    {
        var hasAccess = await commissionAgentsReader.HasAccessAsync();
        if (!hasAccess)
        {
            return new OutsourceConfirmResult
            {
                DocumentBaseId = request.DocumentBaseId,
                Status = OutsourceConfirmPaymentStatus.Error,
            };
        }

        return await base.ConfirmAsync(request);
    }

    protected override async Task<IncomeFromCommissionAgentSaveRequest> MapToExistentAsync(IncomeFromCommissionAgentSaveRequest request)
    {
        var existent = await reader.GetByBaseIdAsync(request.DocumentBaseId);
        return MapToExistent(existent, request);
    }

    protected override Task UpdateAsync(IncomeFromCommissionAgentSaveRequest request) => updater.UpdateAsync(request);

    protected override Task ValidateAsync(IncomeFromCommissionAgentSaveRequest request) => validator.ValidateAsync(request);

    private static IncomeFromCommissionAgentSaveRequest MapToExistent(
        IncomeFromCommissionAgentResponse existent,
        IncomeFromCommissionAgentSaveRequest newValues)
    {
        var result = IncomeFromCommissionAgentMapper.MapToSaveRequest(existent);

        result.Kontragent = newValues.Kontragent;
        result.IncludeNds = newValues.IncludeNds;
        result.NdsType = newValues.NdsType;
        result.NdsSum = newValues.NdsSum;
        result.OperationState = newValues.OperationState;
        result.OutsourceState = newValues.OutsourceState;
        
        // поменялся контрагент: нужно установить связанные поля в значения по умолчанию
        if (existent.Kontragent?.Id != newValues.Kontragent?.Id)
        {
            result.ContractBaseId = 0;
        }

        return result;
    }
}